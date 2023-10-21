using System.Collections.Generic;
using System;
using Zero.Core.Interface;
using System.Drawing;

namespace Dex.Canvas
{
    public sealed class Layer : IBasicIdentity, IDrawable, IDisposable
    {
        private int id;
        /// <summary>
        /// 是否显示
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// 是否锁定
        /// </summary>
        private bool isLocked;
        /// <summary>
        /// 不透明度
        /// </summary>
        private byte alpha;
        /// <summary>
        /// 图层所属场景
        /// </summary>
        private Scene scene;
        /// <summary>
        /// 可编辑实体集合
        /// </summary>
        private Dictionary<int, EditableObject> objects = new Dictionary<int, EditableObject>();//初始化实体集合

        /// <summary>
        /// 创建图层
        /// </summary>
        /// <param name="scene">图层所属场景</param>
        /// <param name="id">图层编号</param>
        /// <param name="alpha">不透明度</param>
        /// <param name="isShow">是否显示场景</param>
        /// <param name="isLocked">是否锁定场景</param>
        /// <param name="objects">需要添加到当前图层的对象集合</param>
        public Layer(Scene scene, int id, byte alpha = 255, bool isShow = true, bool isLocked = false, params EditableObject[] objects) 
        {
            this.id = id;
            this.scene = scene;
            this.alpha = alpha;
            this.isVisible = isShow;
            this.isLocked = isLocked;
            foreach (var item in objects)
            {
                //给定的实体存在于场景中
                ChangeLayer(item);
            }
        }


        public int ID => id;
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get => isVisible; set => isVisible = value; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        /// <summary>
        /// 不透明度
        /// </summary>
        public byte Alpha { get => alpha; set => alpha = value; }
        /// <summary>
        /// 当前图层所在的场景
        /// </summary>
        public Scene Scene { get => scene; }
        /// <summary>
        /// 当前图层容纳的实体集合
        /// </summary>
        public Dictionary<int, EditableObject> Objects { get => objects; }

        /// <summary>
        /// 改变指定对象的图层到当前层，同时将不在当前场景的对象移动到当前场景中
        /// </summary>
        public void ChangeLayer(EditableObject obj)
        {
            //未锁定时可以操作
            if (!this.isLocked)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException("obj");
                }
                //如果所在的图层是空的则直接放入当前的图层并设置成当前场景的对象
                if (obj.Layer == null)
                {
                    //对象添加到当前图层中
                    this.objects.Add(obj.ID, obj);
                    //对象添加到当前图层所在场景中
                    this.scene.Objects.Add(obj.ID, obj);
                    //设置图层
                    obj.Layer = this;
                }
                else
                {
                    //如果图层ID不一致则变动图层
                    if (obj.Layer.id != this.id)
                    {
                        //物体先前所在的图层未锁定则可以移动
                        if (!obj.Layer.isLocked)
                        {
                            //场景有变动则
                            if (obj.Scene.ID != this.scene.ID)
                            {
                                //从之前的场景中移除
                                obj.Scene.Objects.Remove(obj.ID);
                                //对象添加到当前图层所在场景中
                                this.scene.Objects.Add(obj.ID, obj);
                            }

                            //从之前的图层中移除
                            obj.Layer.objects.Remove(obj.ID);
                            //对象添加到当前图层中
                            this.objects.Add(obj.ID, obj);

                            //设置图层
                            obj.Layer = this;
                        }
                    }
                    else
                    {
                        //图层ID一致就检查是否是非同ID实体
                        if (!objects.ContainsKey(obj.ID))
                        {
                            //物体先前所在的图层未锁定则可以移动
                            if (!obj.Layer.isLocked)
                            {
                                //场景有变动则
                                if (obj.Scene.ID != this.scene.ID)
                                {
                                    //从之前的场景中移除
                                    obj.Scene.Objects.Remove(obj.ID);
                                    //对象添加到当前图层所在场景中
                                    this.scene.Objects.Add(obj.ID, obj);
                                }

                                //从之前的图层中移除
                                obj.Layer.objects.Remove(obj.ID);
                                //对象添加到当前图层中
                                this.objects.Add(obj.ID, obj);

                                //设置图层
                                obj.Layer = this;
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in objects.Values)
            {
                item.Dispose();
            }
            objects.Clear();
        }

        
        public void Drawing(Graphics g, DexCanvas canvas)
        {
            if (isVisible)
            {
                if (alpha>0)
                {
                    foreach (var item in objects.Values)
                    {
                        item.Drawing(g, canvas);
                    }
                }
            }
        }


        /// <summary>
        /// 按给定坐标判断是否位于对象的外形内部并由此获取图层中对应的对象
        /// </summary>
        /// <param name="point"></param>
        /// <returns>存在则返回符合要求的对象，否则将返回null</returns>
        public EditableObject GetObjectInSceneByPoint(Point point)
        {
            foreach (var item in objects.Values)
            {
                if (item.IsPointInShape(point))
                {
                    return item;
                }
            }
            return null;
        }
    }
}