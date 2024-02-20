using Dex.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    public partial class ColorPicker : Form
    {
        private bool isPicking;

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            timer.Enabled = l_pickingTip.Visible = isPicking = !isPicking;
        }
        private Color GetColor()
        {
            IntPtr hdc = Program.GetDC(new IntPtr(0));//取到设备场景(0就是全屏的设备场景)
            int c = Program.GetPixel(hdc, MousePosition);//取指定点颜色
            int r = (c & 0xFF);//转换R
            int g = (c & 0xFF00) / 256;//转换G
            int b = (c & 0xFF0000) / 65536;//转换B
            return Color.FromArgb(r, g, b);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            var pickedColor = GetColor();
            var hex = pickedColor.ToHex();
            var rgba = ColorTools.HexToRGBA(hex);
            t_rgb.Text = $"[{rgba[1] / 255.0f},{rgba[2] / 255.0f},{rgba[3] / 255.0f},{rgba[0] / 255.0f}]";
            colorBox.BackColor = pickedColor;
        }
        /// <summary>
        /// 取色器窗口变灰时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorPicker_Deactivate(object sender, EventArgs e)
        {
            timer.Enabled = l_pickingTip.Visible = isPicking = false;
        }
        /// <summary>
        /// 复制到剪贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToClipbroad_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(t_rgb.Text);
        }
    }
}
