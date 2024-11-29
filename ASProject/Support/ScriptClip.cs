using System;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 代码片段
    /// </summary>
    [Serializable]
    internal class ScriptClip
    {
        public ScriptClip()
        { }

        public ScriptClip(string description = "", string script = "")
        {
            Description = description;
            Script = script;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Script { get; set; }
    }
}