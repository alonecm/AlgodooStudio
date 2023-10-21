﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio
{
    /// <summary>
    /// 色彩工具
    /// </summary>
    public static class ColorTool
    {
        /// <summary>
        /// 字符串合并器
        /// </summary>
        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// RGB转换hex
        /// </summary>
        /// <param name="red">红</param>
        /// <param name="green">绿</param>
        /// <param name="blue">蓝</param>
        /// <returns>16进制色彩(无#)</returns>
        public static string RGBToHex(byte red, byte green, byte blue)
        {
            sb.Clear();
            sb.Append(red.ToString("X2"));
            sb.Append(green.ToString("X2"));
            sb.Append(blue.ToString("X2"));
            return sb.ToString();
        }
        /// <summary>
        /// RGBA转换hex
        /// </summary>
        /// <param name="red">红</param>
        /// <param name="green">绿</param>
        /// <param name="blue">蓝</param>
        /// <param name="alpha">透</param>
        /// <returns>16进制色彩(无#)</returns>
        public static string RGBAToHex(byte red, byte green, byte blue, byte alpha)
        {
            string colorHex = RGBToHex(red, green, blue);
            sb.Clear();
            sb.Append(alpha.ToString("X2"));
            sb.Append(colorHex);
            return sb.ToString();
        }
        /// <summary>
        /// Color转hex
        /// </summary>
        /// <param name="color">色彩</param>
        /// <returns>16进制色彩(不带#)</returns>
        public static string ColorToHex(Color color, bool alphaNeed)
        {
            if (alphaNeed)
            {
                return RGBAToHex(color.R, color.G, color.B, color.A);
            }
            else
            {
                return RGBToHex(color.R, color.G, color.B);
            }
        }
        /// <summary>
        /// 16进制色彩转换到RGBA色彩
        /// </summary>
        /// <param name="hex">16进制色彩(有8位字符)</param>
        /// <returns>RGBA色彩</returns>
        public static byte[] HexToRGBA(string hex)
        {
            if (hex!=null)//保证输入了内容
            {
                if (hex.Length != 0)//保证分号内有颜色
                {
                    //保证第一个井号会被去除
                    string color = hex;
                    if (hex[0]=='#')
                    {
                        color = hex.Substring(1, hex.Length - 1);
                    }
                    //填充缺失的
                    if (color.Length<8)
                    {
                        color = color.PadLeft(8, 'F');
                    }
                    byte[] rgba = new byte[4];
                    rgba[0] = byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    rgba[1] = byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    rgba[2] = byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    rgba[3] = byte.Parse(color.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                    return rgba;
                }
                else
                {
                    return new byte[] { 0, 0, 0, 0 };//透明色
                }
            }
            else
            {
                throw new Exception("色彩不得为空！");
            }
        }
        /// <summary>
        /// 16进制色彩转换到Color
        /// </summary>
        /// <param name="hex">16进制色彩(有8位字符)</param>
        /// <returns>Color</returns>
        public static Color HexToColor(string hex)
        {
            byte[] c = HexToRGBA(hex);
            return Color.FromArgb(c[0], c[1], c[2], c[3]);
        }
        /// <summary>
        ///  获取指定颜色的对比色
        /// </summary>
        /// <param name="color">指定颜色</param>
        /// <returns>对比色</returns>
        public static Color GetContrastColor(Color color)
        {
            return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }
        /// <summary>
        /// 转换指定颜色的一定量
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="n">偏移量</param>
        /// <returns>偏移一定值的颜色</returns>
        public static Color OffsetColor(Color color, sbyte n)
        {
            return Color.FromArgb(color.A, color.R + n, color.G + n, color.B + n);
        }
    }
}
