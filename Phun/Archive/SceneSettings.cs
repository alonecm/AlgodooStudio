using AlgodooStudio.Base;

namespace AlgodooStudio.Phun.Archive
{
    /// <summary>
    /// 场景设定
    /// </summary>
    public sealed class SceneSettings : Container<SettingGroup>
    {
        /// <summary>
        /// 创建一个用于放置<see cref="SettingGroup"/>的容器
        /// </summary>
        /// <param name="contents">需要放进去的<see cref="SettingGroup"/></param>
        public SceneSettings(params SettingGroup[] contents) : base(contents)
        {
        }
    }
}