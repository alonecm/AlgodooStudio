using AlgodooStudio.Base;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AlgodooStudio.Window.Style
{
    /// <summary>
    /// QuickTools渲染器
    /// </summary>
    internal class QuickToolsRenderer : ToolStripRenderer
    {
        /// <summary>
        /// 手柄颜色/按钮底色
        /// </summary>
        private Color gripColor;

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color borderColor;

        /// <summary>
        /// 文字颜色
        /// </summary>
        private Color textColor;

        /// <summary>
        /// 修改掉的组合框
        /// </summary>
        private ToolStripComboBox tsCombox;

        /// <summary>
        /// 组合框背景色
        /// </summary>
        private Color comboBoxBackColor;

        /// <summary>
        /// 组合框项背景色
        /// </summary>
        private Color comboBoxItemBackColor;

        /// <summary>
        /// 组合框选中项背景色
        /// </summary>
        private Color comboBoxSelectedItemBackColor;

        /// <summary>
        /// 组合框选中项文字色
        /// </summary>
        private Color comboBoxSelectedItemForeColor;

        /// <summary>
        /// 手柄颜色/按钮底色
        /// </summary>
        internal Color GripColor { get => gripColor; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        internal Color BorderColor { get => borderColor; }

        /// <summary>
        /// 文字颜色
        /// </summary>
        internal Color TextColor { get => textColor; }

        internal Color ComboBoxBackColor { get => comboBoxBackColor; }
        internal Color ComboBoxItemBackColor { get => comboBoxItemBackColor; }
        internal Color ComboBoxSelectedItemBackColor { get => comboBoxSelectedItemBackColor; }
        internal Color ComboBoxSelectedItemForeColor { get => comboBoxSelectedItemForeColor; }

        /// <summary>
        /// 设定手柄、按钮底色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetGripColor(Color color)
        {
            gripColor = color;
        }

        /// <summary>
        /// 设定边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetBorderColor(Color color)
        {
            borderColor = color;
        }

        /// <summary>
        /// 设定文字颜色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetTextColor(Color color)
        {
            textColor = color;
        }

        /// <summary>
        /// 设定组合框背景色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetComboBoxBackColor(Color color)
        {
            comboBoxBackColor = color;
        }

        /// <summary>
        /// 设定组合框项背景色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetComboBoxItemBackColor(Color color)
        {
            comboBoxItemBackColor = color;
        }

        /// <summary>
        /// 设定组合框选中项背景色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetComboBoxSelectedItemBackColor(Color color)
        {
            comboBoxSelectedItemBackColor = color;
        }

        /// <summary>
        /// 设定组合框选中项文字色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetComboBoxSelectedItemForeColor(Color color)
        {
            comboBoxSelectedItemForeColor = color;
        }

        /// <summary>
        /// 绘制手柄
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            Pen p = new Pen(gripColor);
            p.Width = e.GripBounds.Width / 1.5f;
            p.DashStyle = DashStyle.Dot;
            e.Graphics.DrawLine(p, e.GripBounds.X, e.GripBounds.Y, e.GripBounds.X, e.GripBounds.Bottom);
            p.Dispose();
            base.OnRenderGrip(e);
        }

        /// <summary>
        /// 绘制按钮背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            SolidBrush br;
            //如果鼠标在按钮上
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(br = new SolidBrush(gripColor), e.Item.ContentRectangle);
            }
            else
            {
                e.Graphics.FillRectangle(br = new SolidBrush(Color.Transparent), e.Item.ContentRectangle);
            }
            br.Dispose();

            //base.OnRenderButtonBackground(e);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Rectangle tmp = e.AffectedBounds;
            tmp.X--;
            tmp.Y--;
            tmp.Width++;
            Pen p = new Pen(borderColor);
            e.Graphics.DrawRectangle(p, tmp);
            p.Dispose();
            //base.OnRenderToolStripBorder(e);
        }

        /// <summary>
        /// 绘制分离器
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            SolidBrush br;
            Rectangle rt = e.Item.ContentRectangle;
            rt.Width = (int)(rt.Width / 1.2f);
            rt.Height = (int)(rt.Height / 1.2f);
            rt.Y += 2;
            //如果鼠标在按钮上
            e.Graphics.FillRectangle(br = new SolidBrush(gripColor), rt);
            rt.X -= 1;
            e.Graphics.FillRectangle(br = new SolidBrush(borderColor), rt);
            br.Dispose();
        }

        /// <summary>
        /// 绘制项的颜色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = textColor;
            base.OnRenderItemText(e);
        }

        /// <summary>
        /// 初始化<see cref="ToolStripItem"/>
        /// </summary>
        /// <param name="item">被初始化的项</param>
        protected override void InitializeItem(ToolStripItem item)
        {
            //判断是否为ComboBox,是则重新绘制其样式
            if (item.GetType() == typeof(ToolStripComboBox))
            {
                tsCombox = item as ToolStripComboBox;
                tsCombox.BackColor = comboBoxBackColor;
                tsCombox.ForeColor = textColor;
                tsCombox.ComboBox.BackColor = comboBoxBackColor;
                tsCombox.ComboBox.ForeColor = textColor;
                //启用手动绘制模式
                tsCombox.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
                tsCombox.ComboBox.DrawItem += DrawComboxItemSelectedItemColor;
                return;
            }
            base.InitializeItem(item);
        }

        /// <summary>
        /// 绘制ComboBox下拉项目选择时的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawComboxItemSelectedItemColor(object sender, DrawItemEventArgs e)
        {
            SolidBrush str;
            SolidBrush bc;
            //绘制背景
            if (e.State == DrawItemState.Selected)
            {
                //绘制文字
                e.Graphics.FillRectangle(bc = new SolidBrush(comboBoxSelectedItemBackColor), e.Bounds);
                e.Graphics.DrawString(tsCombox.Items[e.Index].ToString(), e.Font, str = new SolidBrush(comboBoxSelectedItemForeColor), e.Bounds);
            }
            else
            {
                //绘制文字
                e.Graphics.FillRectangle(bc = new SolidBrush(comboBoxItemBackColor), e.Bounds);
                e.Graphics.DrawString(tsCombox.Items[e.Index].ToString(), e.Font, str = new SolidBrush(textColor), e.Bounds);
            }
            bc.Dispose();
            str.Dispose();
        }

        /// <summary>
        /// 根据指定主题获取渲染器
        /// </summary>
        /// <param name="theme">主题</param>
        /// <returns>生成的渲染器</returns>
        internal static QuickToolsRenderer GetRenderer(Theme theme)
        {
            QuickToolsRenderer qtr = new QuickToolsRenderer();
            qtr.SetBorderColor(theme.BorderColor);
            qtr.SetGripColor(theme.ItemBackColor);
            qtr.SetTextColor(theme.VarNameColor);
            qtr.SetComboBoxBackColor(theme.BackColor2);
            qtr.SetComboBoxItemBackColor(theme.BackColor1);
            qtr.SetComboBoxSelectedItemBackColor(theme.StringColor);
            qtr.SetComboBoxSelectedItemForeColor(theme.BackColor1);
            return qtr;
        }

        /// <summary>
        /// 根据设置里的主题获取渲染器
        /// </summary>
        /// <returns>生成的渲染器</returns>
        internal static QuickToolsRenderer GetRenderer()
        {
            return GetRenderer(Setting.theme);
        }
    }
}