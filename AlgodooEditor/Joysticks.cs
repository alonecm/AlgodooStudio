using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dex.Canvas
{
    /// <summary>
    /// 拖动柄
    /// </summary>
    internal class Joysticks : IDrawable
    {
        private bool isDownInJoystick = false;
        private Selection selection;
        private Dictionary<JoysticksChoice, Rectangle> joysticks = new Dictionary<JoysticksChoice, Rectangle>();
        private JoysticksChoice joysticksChoice;

        public Joysticks(Selection selection)
        {
            this.selection = selection;
        }

        /// <summary>
        /// 拖动柄大小
        /// </summary>
        public int Size { get; set; } = 10;
        /// <summary>
        /// 是否显示拖动柄
        /// </summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>
        /// 鼠标是否在拖动柄上按下了
        /// </summary>
        public bool IsDownInJoystick { get => isDownInJoystick; }
        /// <summary>
        /// 鼠标所按下的拖动柄标志
        /// </summary>
        public JoysticksChoice JoysticksChoice { get => joysticksChoice; }

        /// <summary>
        /// 通过拖动柄标志获取指定的拖动柄
        /// </summary>
        /// <param name="choice">拖动柄标志</param>
        /// <returns>位于指定位置的拖动柄标志</returns>
        public Rectangle GetJoystick(JoysticksChoice choice)
        {
            return joysticks[choice];
        }


        /// <summary>
        /// 获取选择器选择后的拖动柄矩形
        /// </summary>
        private void GetJoysticks()
        {
            //选中项存在则添加拖动柄
            if (selection.SelectCount > 0)
            {
                Rectangle selectBound = selection.GetSelectedObjectsBounds();
                if (!joysticks.ContainsKey(JoysticksChoice.LeftUp))
                {
                    joysticks.Add(JoysticksChoice.LeftUp, new Rectangle(selectBound.X - Size / 2, selectBound.Y - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.LeftUp] = new Rectangle(selectBound.X - Size / 2, selectBound.Y - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.LeftDown))
                {
                    joysticks.Add(JoysticksChoice.LeftDown, new Rectangle(selectBound.X - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.LeftDown] = new Rectangle(selectBound.X - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.RightUp))
                {
                    joysticks.Add(JoysticksChoice.RightUp, new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.RightUp] = new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.RightDown))
                {
                    joysticks.Add(JoysticksChoice.RightDown, new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.RightDown] = new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.Right))
                {
                    joysticks.Add(JoysticksChoice.Right, new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y + selectBound.Height / 2 - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.Right] = new Rectangle(selectBound.X + selectBound.Width - Size / 2, selectBound.Y + selectBound.Height / 2 - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.Down))
                {
                    joysticks.Add(JoysticksChoice.Down, new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.Down] = new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y + selectBound.Height - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.Left))
                {
                    joysticks.Add(JoysticksChoice.Left, new Rectangle(selectBound.X - Size / 2, selectBound.Y + selectBound.Height / 2 - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.Left] = new Rectangle(selectBound.X - Size / 2, selectBound.Y + selectBound.Height / 2 - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.Up))
                {
                    joysticks.Add(JoysticksChoice.Up, new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.Up] = new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y - Size / 2, Size, Size);
                }
                if (!joysticks.ContainsKey(JoysticksChoice.Rotate))
                {
                    joysticks.Add(JoysticksChoice.Rotate, new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y - 20 - Size / 2, Size, Size));
                }
                else
                {
                    joysticks[JoysticksChoice.Rotate] = new Rectangle(selectBound.X + selectBound.Width / 2 - Size / 2, selectBound.Y - 20 - Size / 2, Size, Size);
                }
            }
        }

        /// <summary>
        /// 鼠标按下执行
        /// </summary>
        /// <param name="e"></param>
        internal void MouseDown(MouseEventArgs e)
        {
            //处于选定状态
            foreach (var item in joysticks)
            {
                //鼠标是否在填充柄中，在则标记按下了并结束
                if (item.Value.Contains(e.Location))
                {
                    isDownInJoystick = true;
                    joysticksChoice = item.Key;
                    break;
                }
            }
        }
        /// <summary>
        /// 鼠标抬起执行
        /// </summary>
        /// <param name="e"></param>
        internal void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDownInJoystick = false;
                joysticksChoice = JoysticksChoice.None;
            }
        }

        public void Drawing(Graphics g, DexCanvas canvas)
        {
            if (IsVisible)
            {
                //存在选中项则计算并获取
                if (selection.SelectCount > 0)
                {
                    GetJoysticks();
                    Rectangle[] rects = joysticks.Values.ToArray();
                    g.FillRectangles(canvas.PaintFactory.GetBrush(Color.White), rects);
                    g.DrawRectangles(canvas.PaintFactory.GetPen(Color.Black), rects);
                }
            }
        }
    }
}