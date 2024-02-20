namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 插件
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        private bool isEnabled = true;
        /// <summary>
        /// 是否错误
        /// </summary>
        private bool isError = false;


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; }


        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled => isEnabled;

        
        protected Plugin() { }
        /// <summary>
        /// 新建插件
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="version">版本号</param>
        protected Plugin(string name = "MyPlugin", string version = "1.0.0", string author = "")
        {
            Name = name;
            Version = version;
            Author = author;
        }

        /// <summary>
        /// 设置为启用
        /// </summary>
        public void SetEnable()
        {
            isEnabled = true;
            OnEnabled();
        }
        /// <summary>
        /// 设置为禁用
        /// </summary>
        public void SetDisable()
        {
            isEnabled = false;
            OnDisabled();
        }


        /// <summary>
        /// 加载时调用
        /// </summary>
        public abstract void OnLoad();
        /// <summary>
        /// 启用时调用
        /// </summary>
        public abstract void OnEnabled();
        /// <summary>
        /// 禁用时调用
        /// </summary>
        public abstract void OnDisabled();
    }
}