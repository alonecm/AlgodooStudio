using System.Collections.Generic;
using System.Drawing;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// Phun包
    /// </summary>
    public sealed class PhunPackage
    {
        /// <summary>
        /// 缩略图
        /// </summary>
        private Image thumb;

        /// <summary>
        /// scene文件
        /// </summary>
        private PhnSave scene;

        /// <summary>
        /// 检查号（相对路径|序列码）
        /// </summary>
        private Dictionary<string, string> checkNums;

        /// <summary>
        /// 贴图
        /// </summary>
        private Dictionary<string, Image> texture;

        public PhunPackage(PhnSave scene)
        {
            this.scene = scene;
        }

        public PhunPackage(PhnSave scene, Image thumb)
        {
            this.scene = scene;
            this.thumb = thumb;
        }

        public PhunPackage(PhnSave scene, Image thumb, Dictionary<string, string> checkNums)
        {
            this.scene = scene;
            this.thumb = thumb;
            this.checkNums = checkNums;
        }

        public PhunPackage(PhnSave scene, Image thumb, Dictionary<string, string> checkNums, Dictionary<string, Image> texture)
        {
            this.scene = scene;
            this.thumb = thumb;
            this.checkNums = checkNums;
            this.Texture = texture;
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        public Image Thumb { get => thumb; }

        /// <summary>
        /// scene文件
        /// </summary>
        public PhnSave Scene { get => scene; }

        /// <summary>
        /// 检查号（相对路径|序列码）
        /// </summary>
        public Dictionary<string, string> CheckSums { get => checkNums; }

        /// <summary>
        /// 贴图
        /// </summary>
        public Dictionary<string, Image> Texture { get => texture; set => texture = value; }
    }
}