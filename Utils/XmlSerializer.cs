using AlgodooStudio.Attribute;
using AlgodooStudio.Base;
using AlgodooStudio.Window.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Zero.Core.XML;

namespace AlgodooStudio.Utils
{
    /// <summary>
    /// Xml序列化器，用对带有<see cref="XmlSerialize"/>标记的对象进行Xml序列化<br/>
    /// 注意：<br/>
    /// 1.所有需要序列化的类和字段都需要带有标记<br/>
    /// 2.使用的类应尽量为自定义的类<br/>
    /// 3.只能对使用了项目中给定的类型以及基本元类型字段进行序列化<br/>
    /// 4.确保定义的被序列化类能够在反序列化时将所有可能用到的字段赋值<br/>
    /// 5.确保定义的类中可以使用无参构造函数就能创建基本类
    /// </summary>
    public sealed class XmlSerializer : IDisposable
    {
        private EasyXml xml;

        /// <summary>
        /// 当前项目的全部类
        /// </summary>
        private static Dictionary<string, Type> projectTypes = new Dictionary<string, Type>();

        /// <summary>
        /// 所有可能的字段绑定标志
        /// </summary>
        public const BindingFlags fieldFlags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// xml根节点
        /// </summary>
        private Xml_Node RootNode { get => xml.RootNode; }

        /// <summary>
        /// 当前项目的全部类
        /// </summary>
        public static Dictionary<string, Type> ProjectTypes { get => projectTypes; }

        /// <summary>
        /// 从xml文件路径创建一个Xml序列化器
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        public XmlSerializer(string xmlPath)
        {
            //如果文件存在则加载
            if (File.Exists(xmlPath))
            {
                xml = new EasyXml(xmlPath);
            }
            else
            {
                //不存在则创建一个空XMl文件用于稍后的保存
                xml = EasyXml.Create(xmlPath);
            }
        }

        /// <summary>
        /// 序列化对象为XML文件
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="objName">对象名</param>
        public void SerializeObject(object obj, string objName)
        {
            //检查静态类序列化标记
            CheckNoFlagAttribute(obj.GetType());
            xml.RootNode = ObjectToXmlNode(obj, objName);
            xml.Save();
        }

        /// <summary>
        /// 反序列化XML文件为对象
        /// </summary>
        /// <param name="obj">被反序列化的对象</param>
        public void UnSerializeObject(object obj)
        {
            Type type = obj.GetType();
            //检查类序列化标记
            CheckNoFlagAttribute(type);
            //获取被打标的序列化属性或字段
            FieldInfo[] fields = GetSerializedFields(type.GetFields(fieldFlags), true);
            foreach (var item in xml.RootNode.ChildNodes)
            {
                foreach (var f in fields)
                {
                    if (f.Name == item.Name)
                    {
                        object t = CreateObjectByXml(item, fieldFlags);
                        f.SetValue(obj, t);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 序列化静态类为XML文件
        /// </summary>
        /// <param name="staticType">静态类的类型</param>
        /// <param name="flags">用于检索的字段和属性的绑定标识(自带静态标识)</param>
        public void SerializeStaticClass(Type staticType)
        {
            //检查静态类序列化标记
            CheckNoFlagAttribute(staticType);
            xml.RootNode = StaticObjectToXmlNode(staticType);
            xml.Save();
        }

        /// <summary>
        /// 反序列化XML文件为静态类
        /// </summary>
        /// <param name="flags">用于检索的字段和属性的绑定标识(自带静态标识)</param>
        /// <param name="staticType">静态类的类型</param>
        public void UnSerializeStaticClass(Type staticType)
        {
            //检查静态类序列化标记
            CheckNoFlagAttribute(staticType);
            //获取被打标的序列化属性或字段
            FieldInfo[] fields = GetSerializedFields(staticType.GetFields(fieldFlags), true);
            foreach (var item in xml.RootNode.ChildNodes)
            {
                foreach (var f in fields)
                {
                    if (f.Name == item.Name)
                    {
                        object e = CreateObjectByXml(item, fieldFlags);
                        f.SetValue(f, e);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 释放序列化和反序列化占用的资源
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)xml).Dispose();
        }

        /// <summary>
        /// 无<see cref="XmlSerialize"/>特性异常
        /// </summary>
        private class NoFlagAttributeException : Exception
        {
            public NoFlagAttributeException(string typeName) : base(typeName + "未使用序列化标记")
            {
            }
        }

        /// <summary>
        /// 从节点创建对象(要求创建对象的构造函数参数名要与字段名相同)
        /// </summary>
        /// <param name="node">根节点</param>
        /// <param name="flag">标记符</param>
        public static object CreateObjectByXml(Xml_Node node, BindingFlags flag)
        {
            Type type = Type.GetType(node.Attribute[0].Content);
            //如果子节点数量大于0则继续向下加载
            if (node.ChildNodes.Count > 0)
            {
                object[] objValues = new object[node.ChildNodes.Count];//当前对象的值
                //获取当前对象的值
                int i = 0;
                foreach (var childNode in node.ChildNodes)
                {
                    objValues[i] = CreateObjectByXml(childNode, fieldFlags);
                    i++;
                }
                object nodeObj;
                //判断是否为数组
                if (type.IsArray)
                {
                    //数组
                    Array tmp = Array.CreateInstance(type.GetElementType(), objValues.Length);
                    int index = 0;
                    foreach (var value in objValues)
                    {
                        tmp.SetValue(value, index);
                        index++;
                    }
                    nodeObj = tmp;
                }
                else
                {
                    //创建默认对象
                    nodeObj = CreateInstanceByType(type);
                    //提取有效字段
                    FieldInfo[] objFields = GetSerializedFields(type.GetFields(fieldFlags), true);
                    //使用索引从搞定的值中一次读取到字段中
                    int v = 0;
                    foreach (var item in objFields)
                    {
                        if (IsDirectType(item.FieldType))
                        {
                            //为可直接赋值的类型
                            item.SetValue(nodeObj, Convert.ChangeType(objValues[v], item.FieldType));
                        }
                        else if (item.FieldType.Equals(typeof(Color)))
                        {
                            //颜色
                            item.SetValue(nodeObj, ColorTools.GetColorFromToString(objValues[v].ToString()));
                        }
                        else if (item.FieldType.IsArray)
                        {
                            //数组
                            Array tmp = Array.CreateInstance(item.FieldType.GetElementType(), ((Array)objValues[0]).Length);
                            int index = 0;
                            foreach (var value in ((Array)objValues[0]))
                            {
                                tmp.SetValue(value, index);
                                index++;
                            }
                            item.SetValue(nodeObj, tmp);
                        }
                        else
                        {
                            //特殊类型直接赋值
                            item.SetValue(nodeObj, objValues[v]);
                        }
                        v++;
                    }
                }
                //生成当前的对象
                return nodeObj;
            }
            else
            {
                //节点数等于认为这个实例可以被创建了
                object nodeObj = CreateInstanceByType(type);//创建默认对象
                //出现了抽象对象的特殊情况则创建个由节点内容组成的对象
                if (nodeObj == null)
                {
                    //获取节点内容对应类型的构造函数集合
                    var tp = Type.GetType(node.Content);
                    ConstructorInfo[] cs = tp.GetConstructors(fieldFlags);
                    //优先使用无参数构造函数创建对象
                    foreach (var item in cs)
                    {
                        if (item.GetParameters().Length == 0)
                        {
                            return nodeObj = item.Invoke(new object[0]);
                        }
                    }
                    //如果不存在无参构造函数则使用默认的第一个构造函数创建对象并报错
                    ParameterInfo[] ps = cs[0].GetParameters();
                    if (ps.Length > 0)
                    {
                        MBox.ShowWarning(tp.FullName + "中不存在无参构造函数,请检查");
                    }
                    //创建与之对应的默认参数数组
                    object[] para = new object[ps.Length];
                    //为构造函数创建基本默认内容，如果这个内容很特殊就这样吧，无所谓了
                    for (int i = 0; i < ps.Length; i++)
                    {
                        para[i] = CreateInstanceByType(ps[i].ParameterType);
                    }
                    //有构造函数用第一个创建实例
                    nodeObj = Activator.CreateInstance(Type.GetType(node.Content), para);
                }
                else
                {
                    nodeObj = node.Content;//赋值
                }
                return nodeObj;
            }
        }

        /// <summary>
        /// 通过给定的类型生成默认实例
        /// </summary>
        /// <param name="type">需要用于创建对象的实例</param>
        /// <returns>创建的实例，如果给定的类型是抽象的则返回空</returns>
        public static object CreateInstanceByType(Type type)
        {
            //判断对象是否为值类型
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                //如果当前为抽象类则直接使用默认值
                if (type.IsAbstract)
                {
                    return null;
                }
                //优先使用无参数构造函数创建对象
                var cs = type.GetConstructors(fieldFlags);
                foreach (var item in cs)
                {
                    if (item.GetParameters().Length == 0)
                    {
                        return item.Invoke(new object[0]);
                    }
                }
                //有构造函数用第一个创建实例
                return cs[0].Invoke(new object[cs[0].GetParameters().Length]);
            }
        }

        /// <summary>
        /// 对象转xml节点
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="objName">对象名</param>
        /// <returns>类对象表示的xml节点</returns>
        public static Xml_Node ObjectToXmlNode(object obj, string objName)
        {
            Type type = obj.GetType();
            //无序列化标识不能继续
            CheckNoFlagAttribute(type);
            return CreateXmlNode(objName, null, type, GetAllFields(obj, fieldFlags).ToArray());
        }

        /// <summary>
        /// 静态对象转xml节点
        /// </summary>
        /// <param name="type">静态对象的类型</param>
        /// <returns>类对象表示的xml节点</returns>
        public static Xml_Node StaticObjectToXmlNode(Type type)
        {
            //如果不是官方的类型就检查
            CheckNoFlagAttribute(type);
            return CreateXmlNode(type.Name, null, type, GetAllFields(type, fieldFlags).ToArray());
        }

        /// <summary>
        /// 分析类对象的具有序列化的所有字段并组成节点集合
        /// </summary>
        /// <param name="obj">类对象</param>
        /// <param name="flag">带有指定标记的字段</param>
        private static Container<Xml_Node> GetAllFields(object obj, BindingFlags flag)
        {
            Container<Xml_Node> node = new Container<Xml_Node>();
            FieldInfo[] fields = GetSerializedFields(obj.GetType().GetFields(flag));
            foreach (var item in fields)
            {
                //如果是数组则按数组创建子节点
                if (item.FieldType.IsArray)
                {
                    //转换成数组
                    Array arr = item.GetValue(obj) as Array;
                    Type elementType = item.FieldType.GetElementType();
                    //创建数组父节点
                    Xml_Node childNode = CreateXmlNode(item.Name, null, elementType);
                    //是基础类型直接生成
                    if (IsNotProjectType(elementType))
                    {
                        //判断是否是可直接赋值的类型
                        if (IsDirectType(item.FieldType) || item.FieldType == typeof(Color))
                        {
                            foreach (var e in arr)
                            {
                                childNode.AddNodeToChild(CreateXmlNode(elementType.Name, e.ToString(), elementType));
                            }
                        }
                        else
                        {
                            //此处创建的对象是由对象类型名+对象所在程序集名构建的，仅能够创建无构造函数的对象
                            foreach (var e in arr)
                            {
                                childNode.AddNodeToChild(CreateXmlNode(elementType.Name, e.ToString() + ", " + e.GetType().Assembly.FullName, elementType));
                            }
                        }
                    }
                    else
                    {
                        //非基础类型需要递归生成
                        foreach (var e in arr)
                        {
                            childNode.AddNodeToChild(ObjectToXmlNode(e, item.Name));
                        }
                    }
                    //移除因意外添加的属性并加入正确的属性
                    childNode.Attribute.Remove(new Xml_Attb("obj_type", $"{elementType.FullName}, {elementType.Assembly.FullName}"));
                    childNode.Attribute.Insert(0, new Xml_Attb("obj_type", $"{item.FieldType.FullName}, {item.FieldType.Assembly.FullName}"));
                    //由于创建节点时不能使用数组所以数组的属性应单独添加
                    childNode.Attribute.Add(new Xml_Attb("is_array", "true"));
                    node.Add(childNode);
                }
                else
                {
                    //判断是否是项目中的类型
                    if (IsNotProjectType(item.FieldType))
                    {
                        //判断是否是可直接赋值的类型
                        if (IsDirectType(item.FieldType) || item.FieldType == typeof(Color))
                        {
                            node.Add(CreateXmlNode(item.Name, item.GetValue(obj).ToString(), item.FieldType));
                        }
                        else
                        {
                            //此处创建的对象是由对象类型名+对象所在程序集名构建的，仅能够创建无构造函数的对象
                            object o = item.GetValue(obj);
                            node.Add(CreateXmlNode(item.Name, o.ToString() + ", " + o.GetType().Assembly.FullName, item.FieldType));
                        }
                    }
                    else
                    {
                        node.Add(ObjectToXmlNode(item.GetValue(obj), item.Name));
                    }
                }
            }
            return node;
        }

        /// <summary>
        /// 分析类对象的具有序列化的所有字段并组成节点集合
        /// </summary>
        /// <param name="statictype">静态对象</param>
        /// <param name="flag">带有指定标记的字段</param>
        private static Container<Xml_Node> GetAllFields(Type statictype, BindingFlags flag)
        {
            Container<Xml_Node> node = new Container<Xml_Node>();
            //获取被打标的全部序列化字段
            FieldInfo[] fields = GetSerializedFields(statictype.GetFields(flag));
            foreach (var item in fields)
            {
                //如果是数组则按数组创建子节点
                if (item.FieldType.IsArray)
                {
                    //转换成数组
                    Array arr = item.GetValue(item) as Array;
                    Type elementType = item.FieldType.GetElementType();
                    //创建数组父节点
                    Xml_Node childNode = CreateXmlNode(item.Name, null, elementType);
                    //判断是否为项目中的类型
                    if (IsNotProjectType(elementType))
                    {
                        //判断是否是可直接赋值的类型
                        if (IsDirectType(item.FieldType) || item.FieldType == typeof(Color))
                        {
                            foreach (var e in arr)
                            {
                                childNode.AddNodeToChild(CreateXmlNode(elementType.Name, e.ToString(), elementType));
                            }
                        }
                        else
                        {
                            //此处创建的对象是由对象类型名+对象所在程序集名构建的，仅能够创建无构造函数的对象
                            foreach (var e in arr)
                            {
                                childNode.AddNodeToChild(CreateXmlNode(elementType.Name, e.ToString() + ", " + e.GetType().Assembly.FullName, elementType));
                            }
                        }
                    }
                    else
                    {
                        //是则需要递归读取
                        foreach (var e in arr)
                        {
                            childNode.AddNodeToChild(ObjectToXmlNode(e, elementType.Name));
                        }
                    }
                    //移除因意外添加的属性并加入正确的属性
                    childNode.Attribute.Remove(new Xml_Attb("obj_type", $"{elementType.FullName}, {elementType.Assembly.FullName}"));
                    childNode.Attribute.Insert(0, new Xml_Attb("obj_type", $"{item.FieldType.FullName}, {item.FieldType.Assembly.FullName}"));
                    //由于创建节点时不能使用数组所以数组的属性应单独添加
                    childNode.Attribute.Add(new Xml_Attb("is_array", "true"));
                    node.Add(childNode);
                }
                else
                {
                    //是基础类型直接读取
                    if (IsNotProjectType(item.FieldType))
                    {
                        //判断是否是可直接赋值的类型
                        if (IsDirectType(item.FieldType) || item.FieldType == typeof(Color))
                        {
                            node.Add(CreateXmlNode(item.Name, item.GetValue(null).ToString(), item.FieldType));
                        }
                        else
                        {
                            //此处创建的对象是由对象类型名+对象所在程序集名构建的，仅能够创建无构造函数的对象
                            object o = item.GetValue(null);
                            node.Add(CreateXmlNode(item.Name, o.ToString() + ", " + o.GetType().Assembly.FullName, item.FieldType));
                        }
                        //node.Add(CreateXmlNode(item.Name, item.GetValue(item).ToString(), item.FieldType));
                    }
                    else
                    {
                        node.Add(ObjectToXmlNode(item.GetValue(item), item.Name));
                    }
                }
            }
            return node;
        }

        /// <summary>
        /// 创建对象的Xml节点
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <param name="content">对象内容(可为空)</param>
        /// <param name="childNode">子节点(可为空)</param>
        /// <returns>Xml节点</returns>
        public static Xml_Node CreateXmlNode(string name, string content, params Xml_Node[] childNode)
        {
            return CreateXmlNode(name, content, null, childNode);
        }

        /// <summary>
        /// 创建对象的Xml节点
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <param name="objType">对象类型(可为空)</param>
        /// <param name="content">对象内容(可为空)</param>
        /// <param name="childNode">子节点(可为空)</param>
        /// <returns>Xml节点</returns>
        public static Xml_Node CreateXmlNode(string name, string content, Type objType, params Xml_Node[] childNode)
        {
            Xml_Node node = new Xml_Node(name);
            if (content != null)
            {
                node.Content = content;
            }
            //判断当前是否用于表示对象
            if (objType != null)
            {
                node.Attribute.Add(new Xml_Attb("obj_type", $"{objType.FullName}, {objType.Assembly.FullName}"));
                node.Attribute.AddRange(GetGenericTypesFromArray(objType));
                //判断对象是否是数组(数组优先级大于泛型)
                if (objType.IsArray)
                {
                    node.Attribute.Add(new Xml_Attb("is_array", "true"));
                }
            }
            if (childNode != null && childNode.Length > 0)
            {
                node.ChildNodes = childNode.ToList();
            }
            return node;
        }

        /// <summary>
        /// 从数组类型中获取泛型标示
        /// </summary>
        /// <param name="arrayType">数组对象类型</param>
        /// <returns>数组对象中的泛型参数</returns>
        public static Container<Xml_Attb> GetGenericTypesFromArray(Type arrayType)
        {
            //如果为数组
            if (arrayType.IsArray)
            {
                return GetGenericTypesFromArray(arrayType.GetElementType());
            }
            else
            {
                if (arrayType.IsGenericType)
                {
                    Container<Xml_Attb> gene = new Container<Xml_Attb>();
                    Type[] generic = arrayType.GenericTypeArguments;
                    int index = 0;
                    foreach (var item in generic)
                    {
                        gene.Add(new Xml_Attb("generic_type" + index, $"{item.FullName}, {item.Assembly.FullName}"));
                        index++;
                    }
                    return gene;
                }
                else
                {
                    return new Container<Xml_Attb>();
                }
            }
        }

        /// <summary>
        /// 截取字符串中某字符之前的全部字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="char"></param>
        /// <returns></returns>
        private static string SubstringChar(string str, char @char)
        {
            int index = str.IndexOf(@char);
            if (index > -1)
            {
                return str.Substring(0, str.IndexOf(@char));
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 检查并抛出无标志异常
        /// </summary>
        /// <param name="type">打标类型</param>
        public static void CheckNoFlagAttribute(Type type)
        {
            if (!CheckTypeFlag(type))
            {
                throw new NoFlagAttributeException(type.Name);
            }
        }

        /// <summary>
        /// 获取当前项目的全部类型
        /// </summary>
        /// <returns>当前项目的全部类型的字典结构</returns>
        public static Dictionary<string, Type> GetProjectTypes()
        {
            Dictionary<string, Type> dic = new Dictionary<string, Type>();
            IEnumerable<TypeInfo> tmp = Assembly.GetExecutingAssembly().DefinedTypes;
            foreach (var item in tmp)
            {
                if (!dic.ContainsKey(item.Name))
                {
                    dic.Add(item.Name, item.AsType());
                }
            }
            projectTypes = dic;
            return dic;
        }

        /// <summary>
        /// 判断该给定类型是否不为本项目中定义的类型
        /// </summary>
        /// <param name="type">需要判断的类型</param>
        /// <returns>不是则返回真，是返回假</returns>
        public static bool IsNotProjectType(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || !projectTypes.ContainsKey(type.Name);
        }

        /// <summary>
        /// 判断该给定类型是否为可直接赋值类型
        /// </summary>
        /// <param name="type">需要判断的类型</param>
        /// <returns>是返回真，不是返回假</returns>
        public static bool IsDirectType(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string));
        }

        /// <summary>
        /// 判断该给定类型是否为本项目中定义的类型
        /// </summary>
        /// <param name="type">需要判断的类型</param>
        /// <returns>是返回真，不是返回假</returns>
        public static bool IsProjectType(Type type)
        {
            return projectTypes.ContainsKey(type.Name);
        }

        /// <summary>
        /// 获取标记过<see cref="XmlSerialize"/>的所有字段
        /// </summary>
        /// <param name="fields">给定的字段集合</param>
        /// <returns>含有<see cref="XmlSerialize"/>的所有字段</returns>
        public static FieldInfo[] GetSerializedFields(FieldInfo[] fields)
        {
            return GetSerializedFields(fields, false);
        }

        /// <summary>
        /// 获取标记过<see cref="XmlSerialize"/>的所有字段
        /// </summary>
        /// <param name="fields">给定的字段集合</param>
        /// <param name="checkWriteOnly">检查是否含有只写标识</param>
        /// <returns>含有<see cref="XmlSerialize"/>的所有字段</returns>
        public static FieldInfo[] GetSerializedFields(FieldInfo[] fields, bool checkWriteOnly)
        {
            Container<FieldInfo> infos = new Container<FieldInfo>();
            //检查静态类是否挂接了序列化标签
            foreach (FieldInfo item in fields)
            {
                if (CheckFieldFlag(item, checkWriteOnly))
                {
                    infos.Add(item, 0);
                }
            }
            return infos.Contents;
        }

        /// <summary>
        /// 检查字段是否含有标记过的<see cref="XmlSerialize"/>
        /// </summary>
        /// <param name="field">需要被检查的字段</param>
        /// <returns>含有则返回真否则返回假</returns>
        public static bool CheckFieldFlag(FieldInfo field)
        {
            return CheckFieldFlag(field, false);
        }

        /// <summary>
        /// 检查字段是否含有标记过的<see cref="XmlSerialize"/>
        /// </summary>
        /// <param name="field">需要被检查的字段</param>
        /// <param name="checkWriteOnly">检查是否含有只写标识<br/><br/>
        /// 为真则表示如果检查到序列化标识存在就再检查只写标识，如果只写开了就返回false</param>
        /// <returns>含有则返回真否则返回假</returns>
        public static bool CheckFieldFlag(FieldInfo field, bool checkWriteOnly)
        {
            IEnumerable<CustomAttributeData> cds = field.CustomAttributes;
            if (checkWriteOnly)
            {
                //检查静态类是否挂接了序列化标签
                foreach (var item in cds)
                {
                    if (item.AttributeType == typeof(XmlSerialize))
                    {
                        //是否开起了只写
                        if (field.GetCustomAttribute<XmlSerialize>().WriteOnly)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            else
            {
                //检查静态类是否挂接了序列化标签
                foreach (var item in cds)
                {
                    if (item.AttributeType == typeof(XmlSerialize))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查类型是否含有标记过的<see cref="XmlSerialize"/>
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>含有则返回真否则返回假</returns>
        public static bool CheckTypeFlag(Type type)
        {
            //检查静态类是否挂接了序列化标签
            object[] attrs = type.GetCustomAttributes(false);
            foreach (var item in attrs)
            {
                if (item.GetType() == typeof(XmlSerialize))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 序列化对象成xml文件
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="objName">对象名</param>
        /// <param name="filePath">文件路径</param>
        public static void SerializeObjectToXml(object obj, string objName, string filePath)
        {
            using (XmlSerializer xml = new XmlSerializer(filePath))
            {
                xml.SerializeObject(obj, objName);
            }
        }

        /// <summary>
        /// 反序列化xml文件成以当前项目中创建的类型的对象
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static object UnSerializeXmlToObject(string filePath)
        {
            if (File.Exists(filePath))
            {
                object o;
                using (XmlSerializer xml = new XmlSerializer(filePath))
                {
                    Type rootType = Type.GetType(xml.RootNode.Attribute[0].Content);
                    try
                    {
                        //创建对象
                        o = CreateInstanceByType(rootType);
                        //序列化对象
                        xml.UnSerializeObject(o);
                    }
                    catch (Exception e)
                    {
                        MBox.ShowWarning(e.Message);
                        return new object();
                    }
                }
                return o;
            }
            return null;
        }

        /// <summary>
        /// 序列化静态类成xml文件
        /// </summary>
        /// <param name="type">需要序列化的静态类</param>
        /// <param name="filePath">文件路径</param>
        public static void SerializeStaticClassToXml(Type type, string filePath)
        {
            using (XmlSerializer xml = new XmlSerializer(filePath))
            {
                xml.SerializeStaticClass(type);
            }
        }

        /// <summary>
        /// 反序列化xml文件给指定的静态类赋值
        /// </summary>
        /// <param name="type">需要反序列化的静态类</param>
        /// <param name="filePath">文件路径</param>
        public static void UnSerializeXmlToStaticClass(Type type, string filePath)
        {
            using (XmlSerializer xml = new XmlSerializer(filePath))
            {
                xml.UnSerializeStaticClass(type);
            }
        }
    }
}