﻿namespace AlgodooStudio.ASProject.Interface
{
    /// <summary>
    /// 编辑接口
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        /// 复制
        /// </summary>
        void Copy();
        /// <summary>
        /// 粘贴
        /// </summary>
        void Paste();
        /// <summary>
        /// 剪切
        /// </summary>
        void Cut();
        /// <summary>
        /// 删除
        /// </summary>
        void Delete();
        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();
        /// <summary>
        /// 重做
        /// </summary>
        void Redo();
        /// <summary>
        /// 全选
        /// </summary>
        void SelectAll();
    }
}