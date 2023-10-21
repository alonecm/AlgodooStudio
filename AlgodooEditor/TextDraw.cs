using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 文字展示器，用于为绘制文字提供用于统一管理和绘制的方案
    /// </summary>
    public class TextDraw : IDrawable ,IDisposable
    {
        private DexCanvas canvas;

        public TextDraw(DexCanvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// 文本显示队列
        /// </summary>
        private Dictionary<int, TextArea> textList = new Dictionary<int, TextArea>();


        /// <summary>
        /// 添加文本区域到文字显示队列中
        /// </summary>
        /// <returns>文本区域的ID</returns>
        public int AddTextArea(string text, FontFamily fontFamily, RectangleF area, Color textColor, float textSize, float angle)
        {
            TextArea ta = new TextArea(canvas, text, fontFamily, area, textColor, textSize, angle);
            return AddTextArea(ta);
        }
        /// <summary>
        /// 添加文本区域到文字显示队列中
        /// </summary>
        /// <returns>文本区域的ID</returns>
        public int AddTextArea(string text, RectangleF area, float textSize, float angle)
        {
            TextArea ta = new TextArea(canvas, text, canvas.DrawingSetting.FontFamily, area, canvas.DrawingSetting.FontColor, textSize, angle);
            return AddTextArea(ta);
        }
        /// <summary>
        /// 添加文本区域到文字显示队列中
        /// </summary>
        /// <param name="area">文本区域</param>
        /// <returns>文本区域的ID</returns>
        public int AddTextArea(TextArea area)
        {
            textList.Add(area.ID, area);
            return area.ID;
        }

        /// <summary>
        /// 修改由文本区域ID标记的文本区域
        /// </summary>
        /// <param name="areaId">文本区域ID</param>
        /// <param name="text">新的文本</param>
        /// <returns>更改成功返回true,否则相反</returns>
        public bool ChangeTextAreaFromId(int areaId, string text, FontFamily fontFamily, RectangleF area, Color textColor, float textSize, float angle)
        {
            if (textList.ContainsKey(areaId))
            {
                TextArea t = textList[areaId];
                t.Text = text;
                t.FontFamily = fontFamily;
                t.Area = area;
                t.TextColor = textColor;
                t.TextSize = textSize;
                t.Angle = angle;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改当前文本绘制器中存放的由文本区域ID标记的文本区域的文本
        /// </summary>
        /// <param name="areaId">文本区域ID</param>
        /// <param name="text">新的文本</param>
        /// <returns>更改成功返回true,否则相反</returns>
        public bool ChangeTextAreaFromId(int areaId, string text)
        {
            if (textList.ContainsKey(areaId))
            {
                TextArea t = textList[areaId];
                t.Text = text;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 改变当前文本绘制器中存放的指定ID的文本区域的显示状态
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="isVisible">显示状态</param>
        /// <returns>修改成功返回true，否则返回false</returns>
        public bool ChangeShowTextAreaById(int areaId, bool isVisible)
        {
            TextArea area = GetTextAreaById(areaId);
            //找到了就修改
            if (area != null)
            {
                area.IsVisible = isVisible;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 改变在当前文本绘制器中存放的文本区域的显示状态
        /// </summary>
        /// <param name="area"></param>
        /// <param name="isVisible"></param>
        /// /// <returns>修改成功返回true，否则返回false</returns>
        public bool ChangeShowTextArea(TextArea area, bool isVisible)
        {
            if (textList.ContainsKey(area.ID))
            {
                area.IsVisible = isVisible;
                return true;
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取文本区域
        /// </summary>
        /// <param name="areaId">文本区域ID</param>
        /// <returns>找到则返回文本区域，未找到则返回null</returns>
        public TextArea GetTextAreaById(int areaId)
        {
            if (textList.ContainsKey(areaId))
            {
                return textList[areaId];
            }
            return null;
        }
        /// <summary>
        /// 通过位置获取文本区域
        /// </summary>
        /// <param name="point"></param>
        /// <returns>找到则返回文本区域，未找到则返回null</returns>
        public TextArea GetTextAreaByPosition(PointF point)
        {
            foreach (var item in textList.Values)
            {
                if (item.Area.Contains(point))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 移除指定的文本区域
        /// </summary>
        public void RemoveTextArea(TextArea area)
        {
            RemoveTextArea(area.ID);
        }
        /// <summary>
        /// 移除指定的文本区域
        /// </summary>
        public void RemoveTextArea(int areaId)
        {
            textList.Remove(areaId);
        }

        public void Drawing(Graphics g, DexCanvas canvas)
        {
            /*
             * 文字旋转无法在此直接实现
             * 步骤则是：
             * 1.创建一块临时性小画布
             * 2.旋转小画布
             * 3.绘制文字
             * 4.将小画布画在大画布上
             * 5.替换这里的所有字符串绘制算法
             * **/
            foreach (var item in textList.Values)
            {
                //检查当前矩形是否存在于视野内
                if (canvas.Camera.IsInView(item))
                {
                    if (item.IsVisible)
                    {
                        item.Ratio = canvas.Camera.Zoom;
                        g.DrawImage(item.DexText.GetTextImage(item.Angle, canvas.PaintFactory.GetBrush(item.TextColor)), item.Margin.Location);
                        g.DrawRectangle(canvas.PaintFactory.GetPen(Color.Red), DrawTools.ToRectangle(item.Area));
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in textList.Values)
            {
                item.DexText.Dispose();
            }
        }
    }
}
