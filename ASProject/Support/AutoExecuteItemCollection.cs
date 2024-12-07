using AlgodooStudio.ASProject.Script.Parse;
using Dex.Common;
using Dex.IO;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Documents;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 自启动项集合
    /// </summary>
    public class AutoExecuteItemCollection : IEnumerable<AutoExecuteItem>
    {
        private List<AutoExecuteItem> items = new List<AutoExecuteItem>();

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
        /// <param name="index"></param>
        public void UpdateByIndex(int index)
        {
            //TODO:按索引更新未完成，请及时完成
            //NOTE:更新项目启用项目需要按索引和位置更新不能全文档重写
            var item = items[index];
            var enablePth = Program.Setting.AlgodooPath + "\\autoexec.cfg";
            var disablePth = ".\\Manage\\disabled_execute_item.manage";
            //如果启用
            if (item.IsEnabled)
            {
                //检查此代码是否经过了托管
                if (File.Exists(disablePth))
                {
                    ThymeParser parserInactive = new ThymeParser(
                        new ThymeTokenizer(
                        FileHandler.GetTextFileContent(disablePth, Encoding.UTF8)
                        ).Tokenize());
                    //在托管则从先从托管中移除
                    using (var stream = new FileStream(disablePth, FileMode.Open))
                    {
                        
                    }
                }
            }
            else
            {

            }
            if (File.Exists(enablePth))
            {
                using (var stream = new FileStream(enablePth, FileMode.Open)) ;
                {
                    
                }
            }
            else
            {
                MBox.ShowError("自启动文件不存在！");
            }
        }


        /// <summary>
        /// 移除指定索引项
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
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