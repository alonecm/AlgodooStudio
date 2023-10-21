using AlgodooStudio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// Phn存档解析流，以流的形式读取存档中的设置组和物体对象
    /// </summary>
    internal sealed class PhnStream
    {
        /// <summary>
        /// 预览中的单元
        /// </summary>
        private PhnUnit peekUnit;
        /// <summary>
        /// 单元标志栈，用于区分当前是否处于单元内
        /// </summary>
        private Stack<string> unitFlag = new Stack<string>();
        /// <summary>
        /// 字符流
        /// </summary>
        private StringStream stream;

        /// <summary>
        /// 创建Phn存档解析流
        /// </summary>
        /// <param name="content">内容</param>
        internal PhnStream(string content)
        {
            this.stream = new StringStream(content);
        }

        /// <summary>
        /// 读取下一个存档单元但不移动
        /// </summary>
        /// <returns></returns>
        internal PhnUnit Peek()
        {
            if (peekUnit==null)
            {
                return peekUnit = Move();
            }
            return peekUnit;
        }
        /// <summary>
        /// 流是否读取完毕了
        /// </summary>
        /// <returns></returns>
        internal bool EndOfRead()
        {
            return stream.EndOfRead();
        }
        /// <summary>
        /// 读取并移动到下一个存档单元
        /// </summary>
        /// <returns>存档单元</returns>
        internal PhnUnit Next()
        {
            PhnUnit tmp;
            //如果已经预览过一次了则先提取预览的
            if (peekUnit!=null)
            {
                tmp = peekUnit;
                peekUnit = null;//提取了则说明移动了，要清空预览单元
                return tmp;
            }
            else
            {
                return Move();//如果清空了则直接向下移动
            }
        }
        /// <summary>
        /// 顺序移动
        /// </summary>
        /// <returns></returns>
        private PhnUnit Move()
        {
            string content = "";
            Container<char> name = new Container<char>();
            //检查是否未结束
            while (!stream.EndOfRead())
            {
                //检查是否为注释
                if (stream.Now() == '/' && stream.Peek() == '/')
                {
                    //循环检查换行符，检查到了跳过
                    while (stream.Now() != '\n')
                    {
                        stream.Next();
                    }
                    //跳过上方的那个换行符
                    stream.Next();
                    continue;
                }
                //如果是字符开头的则是以名称开头的
                if (Regex.IsMatch(stream.Now().ToString(), "[_a-zA-Z]"))
                {
                    //只要未出现特殊字符就持续添加
                    while (Regex.IsMatch(stream.Now().ToString(), "[\\w]"))
                    {
                        //如果现在的名称变成了Scene.my，则认为是自定义变量
                        if (Regex.IsMatch(new string(name), "Scene.my"))
                        {
                            return CreateSceneMyVar();
                        }
                        name.Add(stream.Now());
                        stream.Next();
                    }
                    name.Add(stream.Now());
                    //出现特殊字符结束了则跳过这个特殊字符
                    stream.Next();
                    continue;
                }
                //如果遇见了大括号则进入项记录模式
                if (stream.Now() == '{')
                {
                    //挪动一项，以防止反复添加
                    stream.Next();
                    //入栈记录
                    unitFlag.Push("{");
                    Container<char> tmp = new Container<char>();
                    tmp.Add('{');//记录代码块
                    //如果栈被清空了则结束读取
                    while (unitFlag.Count != 0)
                    {
                        switch (stream.Now())
                        {
                            case '{':
                                unitFlag.Push("{");
                                break;
                            case '}':
                                unitFlag.Pop();
                                break;
                        }
                        tmp.Add(stream.Now());
                        stream.Next();
                    }
                    //合并字符串并退出循环
                    content = new string(tmp);
                    break;
                }
                stream.Next();
            }
            //获取类型
            UnitType type;
            if (Regex.IsMatch(new string(name), RegexFormatStrings.addSceneObject))
            {
                type = UnitType.Object;
            }
            else
            {
                type = UnitType.SettingGroup;
            }
            return new PhnUnit(new string(name), type, SplitItem(content));
        }
        /// <summary>
        /// 创建场景变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="stream">字符流</param>
        /// <returns>单元</returns>
        private PhnUnit CreateSceneMyVar()
        {
            Container<char> name = new Container<char>();
            Container<char> value = new Container<char>();
            //记录值
            bool flag = false;
            //检查内容
            while (!stream.EndOfRead())
            {
                if (Regex.IsMatch(stream.Now().ToString(),"[_a-zA-Z]"))
                {
                    //记录名称
                    while (!stream.EndOfRead())
                    {
                        //碰上这种就作为分段符分段结束
                        if (stream.Now() == ':' && stream.Peek() == '=')
                        {
                            stream.Next();
                            stream.Next();
                            break;
                        }
                        else
                        {
                            if (stream.Now() == '=')
                            {
                                stream.Next();
                                break;
                            }
                            else
                            {
                                name.Add(stream.Now());
                            }
                        }
                        stream.Next();
                    }
                    //括号记录器
                    Stack<char> branch = new Stack<char>();
                    
                    while (!stream.EndOfRead())
                    {
                        //从头记录到尾
                        switch (stream.Now())
                        {
                            case '{':
                                branch.Push('{');
                                break;
                            case '}':
                                branch.Pop();
                                break;
                            case ';':
                                if (branch.Count==0)
                                {
                                    flag = true;//允许结束
                                }
                                break;
                        }
                        if (flag)
                        {
                            break;
                        }
                        else
                        {
                            value.Add(stream.Now());
                        }
                        stream.Next();
                    }
                }
                //允许结束
                if (flag)
                {
                    break;
                }
            }
            return new PhnUnit("Scene.my", UnitType.SceneMyVar, new UnitItem(new string(name), new string(value)));
        }

        /// <summary>
        /// 分离项
        /// </summary>
        /// <param name="content">内容项目</param>
        private Container<UnitItem> SplitItem(string content)
        {
            Container<UnitItem> items = new Container<UnitItem>();
            StringStream stream = new StringStream(content);//独立流
            stream.Next();//跳过开头大括号
            bool isEnd = false;//结束读取标志
            while (!stream.EndOfRead())
            {
                //跳过空格
                if (stream.Now()==' ')
                {
                    stream.Next();
                    continue;
                }
                //判断是否进入项记录状态
                if (Regex.IsMatch(stream.Now().ToString(),"[_a-zA-Z]"))
                {
                    Container<char> itemName = new Container<char>();
                    //只要不等于冒号或分号则持续执行名称记录
                    while (!stream.EndOfRead())
                    {
                        //设置项名称结束
                        if (stream.Now() == '=')
                        {
                            stream.Next();
                            break;
                        }
                        //实体项名称结束
                        if (stream.Now() == ':'&&stream.Peek()=='=')
                        {
                            stream.Next();
                            stream.Next();
                            break;
                        }
                        itemName.Add(stream.Now());
                        stream.Next();
                    }

                    //执行项记录
                    Container<char> itemValue = new Container<char>();
                    Stack<char> branch = new Stack<char>();//分支记录栈;
                    bool isBranch = false;//分支记录标志
                    while (!stream.EndOfRead())
                    {
                        //分支记录
                        switch (stream.Now())
                        {
                            case '{':
                                branch.Push('{');
                                isBranch = true;//检测到第一个大括号就证明是分支了
                                break;
                            case '}':
                                if (isBranch)
                                {
                                    branch.Pop();
                                    itemValue.Add(stream.Now());//确保分支能够正常结束
                                }
                                else
                                {
                                    //如果这个大括号不是分支的大括号则证明已经读取结束
                                    isEnd = true;
                                }
                                break;
                        }
                        //查看是否为分支
                        if (isBranch)
                        {
                            //清空掉了所有的分支后结束
                            if (branch.Count == 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            //不为分支就等待分号出现结束
                            if (stream.Now()==';')
                            {
                                break;
                            }
                        }
                        //未结束则记录
                        if (!isEnd)
                        {
                            itemValue.Add(stream.Now());
                        }
                        stream.Next();
                    }
                    //统计值
                    string name = new string(itemName);
                    string value = new string(itemValue);
                    items.Add(new UnitItem(name, value));
                    //是否处于结束状态
                    if (isEnd)
                    {
                        break;
                    }
                    else
                    {
                        stream.Next();
                    }
                }
                stream.Next();
            }
            return items;
        }
    }
}
