using AlgodooStudio.Attribute;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Window.Style
{
    /// <summary>
    /// 深色Algodoo式主题
    /// </summary>
    [XmlSerialize]
    internal class DarkAlgodooTheme : VS2015BlueTheme
    {
        public DarkAlgodooTheme()
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

            //标签最右侧下拉箭头边框色
            base.ColorPalette.CommandBarMenuPopupDefault.Border = Color.FromArgb(25, 25, 25);
            //标签最右侧下拉箭头后的默认ICON的背景色
            base.ColorPalette.CommandBarMenuPopupDefault.IconBackground = Color.FromArgb(25, 25, 25);
            //标签最右侧下拉箭头后的默认背景色
            base.ColorPalette.CommandBarMenuPopupDefault.BackgroundBottom = Color.FromArgb(25, 25, 25);
            //标签最右侧下拉箭头后的默认文字色
            base.ColorPalette.CommandBarMenuDefault.Text = Color.FromArgb(200, 200, 200);
            //标签最右侧下拉箭头选中后的文字色
            base.ColorPalette.CommandBarMenuTopLevelHeaderHovered.Text = Color.Orange;
            //标签最右侧下拉箭头选中后的部件背景色
            base.ColorPalette.CommandBarMenuPopupHovered.ItemBackground = Color.FromArgb(45, 45, 45);
            base.ColorPalette.CommandBarMenuTopLevelHeaderHovered.Border = Color.FromArgb(45, 45, 45);

            //侧边收缩栏按钮色
            base.ColorPalette.AutoHideStripDefault.Background = Color.FromArgb(45, 45, 45);
            base.ColorPalette.AutoHideStripDefault.Text = Color.White;
            base.ColorPalette.AutoHideStripHovered.Background = Color.FromArgb(65, 65, 65);
            base.ColorPalette.AutoHideStripHovered.Text = Color.Orange;
            //其他标签组已选定的标签在未激活时的颜色
            base.ColorPalette.TabSelectedInactive.Background = Color.FromArgb(65, 65, 65);
        }
    }
}