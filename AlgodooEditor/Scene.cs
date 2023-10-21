using System;
using System.Collections.Generic;
using System.Drawing;
using Zero.Core.Interface;

namespace Dex.Canvas
{
    /// <summary>
    /// 用于绘制的场景
    /// </summary>
    public sealed class Scene : IBasicIdentity, IDisposable
    {
        /// <summary>
        /// 最后一次创建的场景ID
        /// </summary>
        private static int lastSceneID;
        /// <summary>
        /// 编号
        /// </summary>
        private int id;
        /// <summary>
        /// 场景包含的图层集合
        /// </summary>
        private Dictionary<int, Layer> layers;
        /// <summary>
        /// 场景包含的可编辑实体集合
        /// </summary>
        private Dictionary<int, EditableObject> objects;
        
        /// <summary>
        /// 创建场景
        /// </summary>
        public Scene()
        {
            id = lastSceneID++;
            layers = new Dictionary<int, Layer>();
            objects = new Dictionary<int, EditableObject>();
            //添加默认图层
            layers.Add(0, new Layer(this, 0));
        }


        public int ID => id;
        /// <summary>
        /// 图层集
        /// </summary>
        public Dictionary<int, Layer> Layers { get => layers; }
        /// <summary>
        /// 对象集
        /// </summary>
        public Dictionary<int, EditableObject> Objects { get => objects; }

        /// <summary>
        /// 添加图层
        /// </summary>
        /// <param name="layer">需要添加进的图层</param>
        /// <returns>添加成功则返回true,添加失败则返回false</returns>
        public bool AddLayer(Layer layer)
        {
            if (!layers.ContainsKey(layer.ID))
            {
                layers.Add(layer.ID, layer);
                return true;
            }
            return false;
        }
        /// <summary>
        ///添加空图层
        /// </summary>
        public void AddLayer()
        {
            layers.Add(layers.Count, new Layer(this, layers.Count));
        }
        /// <summary>
        /// 移除图层
        /// </summary>
        /// <param name="layerID">需要移除的图层ID</param>
        /// <returns>移除成功返回true，否则返回false</returns>
        public bool RemoveLayer(int layerID)
        {
            if (layers.ContainsKey(layerID))
            {
                Layer l = layers[layerID];
                //未锁定可以移除
                if (!l.IsLocked)
                {
                   
                    //从场景中移除对应的实体
                    foreach (var item in l.Objects.Values)
                    {
                        this.objects.Remove(item.ID);
                    }
                    //释放这个图层
                    l.Dispose();
                    //从场景中删除这个图层
                    layers.Remove(layerID);
                    return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// 添加对象到场景中
        /// </summary>
        /// <param name="objects"></param>
        public void AddObjects(int layerID, params EditableObject[] objects)
        {
            foreach (var item in objects)
            {
                //如果物体所在图层不是空的则直接添加到图层和场景中
                this.layers[layerID].ChangeLayer(item);
            }
        }
        /// <summary>
        /// 添加对象到场景中
        /// </summary>
        /// <param name="objects"></param>
        public void AddObjects(params EditableObject[] objects)
        {
            AddObjects(0, objects);
        }
        /// <summary>
        /// 通过ID移除场景中的指定对象集合
        /// </summary>
        /// <param name="objectIds"></param>
        public void RemoveObjects(params int[] objectIds)
        {
            foreach (var item in objectIds)
            {
                if (objects.ContainsKey(item))
                {
                    objects[item].Layer.Objects.Remove(item);//从实体的图层中移除指定的实体
                    objects.Remove(item);//从场景中移除
                }
            }
        }
        /// <summary>
        /// 按给定坐标判断是否位于对象的外形内部并由此获取场景中对应的对象
        /// </summary>
        /// <param name="point"></param>
        /// <returns>存在则返回符合要求的对象，否则将返回null</returns>
        public EditableObject GetObjectInSceneByPoint(Point point)
        {
            foreach (var item in Objects.Values)
            {
                if (item.IsPointInShape(point))
                {
                    return item;
                }
            }
            return null;
        }

        public void Dispose()
        {
            foreach (var item in layers.Values)
            {
                item.Dispose();
            }
            layers.Clear();
            objects.Clear();
        }
    }
}