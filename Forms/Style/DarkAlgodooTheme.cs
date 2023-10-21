using AlgodooStudio.Basic;
using AlgodooStudio.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

using WeifenLuo.WinFormsUI.ThemeVS2015;

namespace AlgodooStudio.Forms.Style
{
    /// <summary>
    /// 深色Algodoo式主题
    /// </summary>
    internal class DarkAlgodooTheme : VS2015BlueTheme
    {
        public DarkAlgodooTheme()
        {
            SetColor();
        }

        /// <summary>
        /// 设定主题的颜色
        /// </summary>
        private void SetColor()
        {
            //标签色
            base.ColorPalette.TabSelectedActive.Background = Color.Orange;
            base.ColorPalette.TabSelectedActive.Text = Color.White;
            //多重标签未选中时鼠标浮过的调色
            base.ColorPalette.TabUnselectedHovered.Background = Color.DarkOrange;

            //非中部窗口标题栏拖动条色
            //激活色
            base.ColorPalette.ToolWindowCaptionActive.Background = Color.Orange;
            base.ColorPalette.ToolWindowCaptionActive.Text = Color.White;
            base.ColorPalette.ToolWindowCaptionActive.Grip = Color.White;
            //未激活色
            base.ColorPalette.ToolWindowCaptionInactive.Background = Color.FromArgb(45, 45, 45);
            base.ColorPalette.ToolWindowCaptionInactive.Grip = Color.DimGray;

            //非中部窗口底部标签色
            //选中色
            base.ColorPalette.ToolWindowTabSelectedInactive.Background = Color.FromArgb(45, 45, 45);
            base.ColorPalette.ToolWindowTabSelectedInactive.Text = Color.Yellow;
            //未选中略过色
            base.ColorPalette.ToolWindowTabUnselectedHovered.Background = Color.FromArgb(55, 55, 55);
            base.ColorPalette.ToolWindowTabUnselectedHovered.Text = Color.DarkOrange;

            //窗口主界面底部颜色
            base.ColorPalette.MainWindowActive.Background = Color.FromArgb(45, 45, 45);

            //边框颜色
            base.ColorPalette.ToolWindowBorder = Color.DarkOrange;

            //标签最右侧下拉箭头内容色
            base.ColorPalette.CommandBarMenuPopupDefault.Border = ColorTool.HexToColor("#FFC500");
            base.ColorPalette.CommandBarMenuPopupDefault.IconBackground = ColorTool.HexToColor("");
            base.ColorPalette.CommandBarMenuPopupDefault.BackgroundBottom = ColorTool.HexToColor("#FEF9F0");
            //标签最右侧下拉箭头选中色
            base.ColorPalette.CommandBarMenuPopupHovered.ItemBackground = ColorTool.HexToColor("#FFC500");

            //侧边收缩栏按钮色
            base.ColorPalette.AutoHideStripDefault.Background = Color.FromArgb(45, 45, 45);
            base.ColorPalette.AutoHideStripDefault.Text = Color.White;
            base.ColorPalette.AutoHideStripHovered.Background = Color.FromArgb(65, 65, 65);
            base.ColorPalette.AutoHideStripHovered.Text = Color.Orange;

            //其他标签组已选定的标签在未激活时的颜色
            base.ColorPalette.TabSelectedInactive.Background = Color.FromArgb(65, 65, 65);
        }

        public DarkAlgodooTheme(Theme theme)
        {
            SetColor();
            base.ToolStripRenderer = ThemeToolStripRenderer.GetRenderer(theme);
        }
    }   
}
