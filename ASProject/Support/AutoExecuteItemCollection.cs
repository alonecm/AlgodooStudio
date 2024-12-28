using AlgodooStudio.ASProject.Script;
using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Script.Parse.Expr;
using Dex.Common;
using Dex.IO;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 自启动项集合
    /// </summary>
    public class AutoExecuteItemCollection : IEnumerable<AutoExecuteItem>
    {
        private List<AutoExecuteItem> items = new List<AutoExecuteItem>();
        private readonly string enablePth = Program.Setting.AlgodooAutoExecuteFilePath;
        private readonly string disablePth = ".\\Manage\\disabled_execute_item.manage";
        public AutoExecuteItem this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public AutoExecuteItemCollection()
        {
        }

        /// <summary>
        /// 切换指定项的状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="status"></param>
        public void SetStatus(int index, bool status)
        {
            if (index >= 0 && index < items.Count)
                this[index].IsEnabled = status;
        }


        /// <summary>
        /// 添加启动项
        /// </summary>
        /// <returns>是否成功添加</returns>
        public AutoExecuteItem Add(bool isEnabled, AutoExecuteItemType type, string content, Range range)
        {
            var item = new AutoExecuteItem(isEnabled, type, content, range);
            this.items.Add(item);
            return item;
        }

        /// <summary>
        /// 通过索引更新项
        /// </summary>
        /// <returns>更新成功还是失败</returns>
        public void UpdateByIndex(params int[] indexes)
        {
            //托管文件不存在则创建托管文件
            if (!File.Exists(disablePth))
            {
                using (File.Create(disablePth)) ;
            }

            foreach (var index in indexes)
            {
                var item = items[index];
                //如果这个自启动项的有所变动则执行以下部分
                if (item.LastStatus != item.IsEnabled)
                {
                    if (item.IsEnabled)//此处证明启动了应该 从托管文件中移除 然后 加入到自启动文件中
                    {
                        RemoveFrom(disablePth, item);
                        AddTo(enablePth, item);
                    }
                    else//此处证明关闭了应该 从自启动文件中移除 然后 加入到托管文件中
                    {
                        RemoveFrom(enablePth, item);
                        AddTo(disablePth, item);
                    }
                }
            }
        }

        private void AddTo(string path, AutoExecuteItem item)
        {
            using (var sw = new StreamWriter(path,true,Encoding.UTF8))
            {
                sw.WriteLine(item.Content + ";");
            }
        }

        private void RemoveFrom(string path, AutoExecuteItem item)
        {
            var thymeRg = new ThymeReGenerator();
            //获取指定文件的内容
            var content = File.ReadAllText(path, Encoding.UTF8);
            //获取该文件的ast树
            var ast = ThymeParser.GetAST(content, false);
            foreach (var node in ast.Item1.Nodes)
            {
                //转文本并与指定项的内容进行比较，如果存在则将此节点的范围作用于文件修改上
                if (thymeRg.ReGenerate(node).Replace(" ", "").Replace("\n", "").Replace("\r", "").Contains(item.Content.Replace(" ", "").Replace("\n", "").Replace("\r", "")))
                {
                    //找到就删除并写入、返回
                    //删除
                    int start = (int)node.Range.Min;
                    int length = (int)node.Range.Max - start + 1;
                    content = content.Remove(start, length).Replace("\0", "");
                    //写入
                    File.WriteAllText(path, content, Encoding.UTF8);
                    break;
                }
            }
        }
        
        /// <summary>
        /// 移除指定索引项
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            var item = items[index];
            RemoveFrom(items[index].IsEnabled ? enablePth : disablePth, item);
            items.RemoveAt(index);
        }

        public IEnumerator<AutoExecuteItem> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}