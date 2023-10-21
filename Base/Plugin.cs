using AlgodooStudio.Interface;

namespace AlgodooStudio.Base
{
    /// <summary>
    /// 插件
    /// </summary>
    public class Plugin : IBasicInformation
    {
        /// <summary>
        /// 编号
        /// </summary>
        protected int id = 0;

        /// <summary>
        /// 名称
        /// </summary>
        protected string name = "PluginObject";

        /// <summary>
        /// 版本号
        /// </summary>
        protected string version = "1.0.0";

        /// <summary>
        /// 是否启用
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// 是否错误
        /// </summary>
        private bool isError = false;

        /// <summary>
        /// 错误描述
        /// </summary>
        private string errorDescription;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get => id; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get => version; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get => isEnabled; }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { get => isError; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDescription { get => errorDescription; }

        /// <summary>
        /// 新建插件
        /// </summary>
        public Plugin()
        { }

        /// <summary>
        /// 新建插件
        /// </summary>
        /// <param name="id">编号</param>
        public Plugin(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// 新建插件
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        public Plugin(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// 新建插件
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="version">版本号</param>
        public Plugin(int id, string name, string version)
        {
            this.id = id;
            this.name = name;
            this.version = version;
        }

        /// <summary>
        /// 设置为启用
        /// </summary>
        public void SetEnable()
        {
            isEnabled = true;
        }

        /// <summary>
        /// 设置为禁用
        /// </summary>
        public void SetDisable()
        {
            isEnabled = false;
        }

        /// <summary>
        /// 不可逆地将插件标记为错误
        /// </summary>
        /// <param name="errorDescription">错误描述</param>
        public void SetError(string errorDescription)
        {
            isError = true;
            this.errorDescription = errorDescription;
        }

        /// <summary>
        /// 不可逆地将插件标记为错误
        /// </summary>
        public void SetError()
        {
            SetError("未知错误");
        }

        /// <summary>
        /// 加载时调用
        /// </summary>
        public virtual void OnLoad()
        { }

        /// <summary>
        /// 启用时调用
        /// </summary>
        public virtual void OnEnabled()
        { }

        /// <summary>
        /// 禁用时调用
        /// </summary>
        public virtual void OnDisabled()
        { }

        /// <summary>
        /// 卸载时调用
        /// </summary>
        public virtual void OnUnload()
        { }
    }
}