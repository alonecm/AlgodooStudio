using AlgodooStudio.Base;

namespace AlgodooStudio.Phun.Archive
{
    /// <summary>
    /// 存档中的场景对象集合
    /// </summary>
    public sealed class SceneObjects : Container<SceneObject>
    {
        /// <summary>
        /// 创建一个用于放置<see cref="SceneObject"/>的容器
        /// </summary>
        /// <param name="contents">需要放进去的<see cref="SceneObject"/></param>
        public SceneObjects(params SceneObject[] contents) : base(contents)
        {
        }
    }
}