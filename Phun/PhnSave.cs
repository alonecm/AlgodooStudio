using AlgodooStudio.Phun.Archive;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// Phun存档
    /// </summary>
    public sealed class PhnSave
    {
        /// <summary>
        /// 存档信息
        /// </summary>
        private ArchiveInfo info;

        /// <summary>
        /// 存档设定
        /// </summary>
        private SceneSettings sceneSettings;

        /// <summary>
        /// 存档中包含的场景对象集合
        /// </summary>
        private SceneObjects sceneObjects;

        /// <summary>
        /// 存档信息
        /// </summary>
        public ArchiveInfo Info { get => info; }

        /// <summary>
        /// 存档设定
        /// </summary>
        public SceneSettings SceneSettings { get => sceneSettings; }

        /// <summary>
        /// 存档中包含的场景对象集合
        /// </summary>
        public SceneObjects SceneObjects { get => sceneObjects; }

        /// <summary>
        /// 创建Phun存档
        /// </summary>
        public PhnSave(ArchiveInfo info, SceneSettings sceneSettings, SceneObjects sceneObjects)
        {
            this.info = info;
            this.sceneSettings = sceneSettings;
            this.sceneObjects = sceneObjects;
        }
    }
}