﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace AlgodooStudio.Utils
{
    public class IconTools
    {
        /// <summary>
        /// 引用shell32文件的SHGetFileInfo API方法
        /// </summary>
        /// <param name="pszPath">指定的文件名,如果为""则返回文件夹的</param>
        /// <param name="dwFileAttributes">文件属性</param>
        /// <param name="sfi">返回获得的文件信息,是一个记录类型</param>
        /// <param name="cbFileInfo">文件的类型名</param>
        /// <param name="uFlags">文件信息标识</param>
        /// <returns>-1失败</returns>
        [DllImport("shell32", EntryPoint = "SHGetFileInfo", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SHGetFileInfo(string pszPath, FileAttribute dwFileAttributes, ref SHFileInfo sfi, uint cbFileInfo, SHFileInfoFlags uFlags);

        /// <summary>
        /// 返回系统设置的图标
        /// </summary>
        /// <param name="lpszFile">文件名,指定从exe文件或dll文件引入icon</param>
        /// <param name="nIconIndex">文件的图表中的第几个,指定icon的索引如果为0则从指定的文件中引入第1个icon</param>
        /// <param name="phiconLarge">返回的大图标的指针,大图标句柄如果为null则为没有大图标</param>
        /// <param name="phiconSmall">返回的小图标的指针,小图标句柄如果为null则为没有小图标</param>
        /// <param name="nIcons">ico个数,找几个图标</param>
        /// <returns></returns
        [DllImport("shell32.dll")]
        public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

        [DllImport("User32.dll", EntryPoint = "DestroyIcon")]
        public static extern int DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// 文件信息标识枚举类,所有枚举定义值前省略SHGFI投标，比如Icon 完整名称应为SHGFI_ICON
        /// </summary>
        [Flags]
        private enum SHFileInfoFlags : uint
        {
            /// <summary>
            /// 允许有叠加图案的文件图标，该标识必须和Icon同时使用
            /// </summary>
            AddOveylays = 0x20,         // SHGFI_AddOverlays = 0x000000020

            /// <summary>
            /// 只获取由参数FileAttribute指定的文件信息，并将其写入SHFileInfo结构的dwAttributes属性，如果不指定该标识，将同时获取所有文件信息。该标志不能和Icon标识同时使用
            /// </summary>
            Attr_Specified = 0x20000,   //  SHGFI_SpecifiedAttributes = 0x000020000

            /// <summary>
            /// 将获取的文件属性复制到SHFileInfo结构的dwAttributes属性中
            /// </summary>
            Attributes = 0x800,     // SHGFI_Attributes = 0x000000800

            /// <summary>
            /// 获取文件的显示名称（长文件名称），将其复制到SHFileInfo结构的dwAttributes属性中
            /// </summary>
            DisplayName = 0x200,    // SHGFI_DisplayName = 0x000000200

            /// <summary>
            /// 如果文件是可执行文件，将检索其信息，并将信息作为返回值返回
            /// </summary>
            ExeType = 0x2000,       // SHGFI_EXEType = 0x000002000

            /// <summary>
            /// 获得图标和索引，将图标句柄返回到SHFileInfo结构的hIcon属性中，索引返回到iIcon属性中
            /// </summary>
            Icon = 0x100,           // SHGFI_Icon = 0x000000100

            /// <summary>
            /// 检索包含图标的文件，并将文件名，图标句柄，图标索引号，放回到SHFileInfo结构中
            /// </summary>
            IconLocation = 0x1000,  // SHGFI_IconLocation = 0x000001000

            /// <summary>
            /// 获得大图标，该标识必须和Icon标识同时使用
            /// </summary>
            LargeIcon = 0x0,        // SHGFI_LargeIcon = 0x000000000

            /// <summary>
            /// 获取链接覆盖文件图标，该标识必须和Icon标识同时使用。
            /// </summary>
            LinkOverlay = 0x8000,   // SHGFI_LinkOverlay = 0x000008000

            /// <summary>
            /// 获取文件打开时的图标，该标识必须和Icon或SysIconIndex同时使用
            /// </summary>
            OpenIcon = 0x2,         //  SHGFI_OpenIcon = 0x000000002

            /// <summary>
            /// 获取链接覆盖文件图标索引，该标识必须和Icon标识同时使用。
            /// </summary>
            OverlayIndex = 0x40,    // SHGFI_OverlayIndex = 0x000000040

            /// <summary>
            /// 指示传入的路径是一个ITEMIDLIST结构的文件地址而不是一个路径名。
            /// </summary>
            Pidl = 0x8,             // SHGFI_PIDL = 0x000000008

            /// <summary>
            /// 获取系统的高亮显示图标，该标识必须和Icon标识同时使用。
            /// </summary>
            Selected = 0x10000,     // SHGFI_SelectedState = 0x000010000

            /// <summary>
            /// 获取 Shell-sized icon ，该标志必须和Icon标识同时使用。
            /// </summary>
            ShellIconSize = 0x4,    // SHGFI_ShellIconSize = 0x000000004

            /// <summary>
            /// 获得小图标，该标识必须和Icon或SysIconIndex同时使用。
            /// </summary>
            SmallIcon = 0x1,       // SHGFI_SmallIcon = 0x000000001

            /// <summary>
            /// 获取系统图像列表图标索引，返回系统图像列表句柄
            /// </summary>
            SysIconIndex = 0x4000,  // SHGFI_SysIconIndex = 0x000004000

            /// <summary>
            /// 获得文件类型，类型字符串被写入SHFileInfo结构的szTypeName属性中
            /// </summary>
            TypeName = 0x400,       // SHGFI_TypeName = 0x000000400

            /// <summary>
            /// 指示如果由pszPath指定的路径不存在，SHGetFileInfo方法变不会试图去操作文件。指示返回与文件类型相关的信息。该标识不能和Attributes、ExeType和Pidl同时使用
            /// </summary>
            UseFileAttributes = 0x10    // SHGFI_UserFileAttributes = 0x000000010,
        }

        /// <summary>
        /// 文件属性枚举
        /// </summary>
        [Flags]
        private enum FileAttribute
        {
            ReadOnly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,     //路径信息
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,        //普通文件信息
            Temporary = 0x00000100,
            Sparse_File = 0x00000200,
            Reparse_Point = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            Not_Content_Indexed = 0x00002000,
            Encrypted = 0x00004000
        }

        /// <summary>
        /// 定义返回的文件信息结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFileInfo
        {
            /// <summary>
            /// 文件的图标句柄
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            /// 图标的系统索引号
            /// </summary>
            public IntPtr iIcon;

            /// <summary>
            /// 文件的属性值,由FileAttribute指定的属性。
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            /// 文件的显示名,如果是64位系统，您可能需要制定SizeConst=258而非260才能够显示完整的 TypeName
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            /// <summary>
            /// 文件的类型名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        /// <summary>
        /// 当读取文件图标失败的默认图标索引号
        /// </summary>
        public static readonly long ErrorFileIndex = -2;

        /// <summary>
        /// 当读取文件夹图标失败的默认图标索引号
        /// </summary>
        public static readonly long ErrorFolderIndex = -4;

        /// <summary>
        /// 当读取磁盘驱动器图标失败的默认图标索引号
        /// </summary>
        public static readonly long ErrorDriverIndex = -8;

        /// <summary>
        /// 当读取可执行文件图标失败的默认图标索引号
        /// </summary>
        public static readonly long ErrorApplicationIndex = -16;

        /// <summary>
        /// 获取文件类型的关联图标
        /// </summary>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isSmallIcon">是否返回小图标</param>
        /// <returns>返回一个Icon类型的文件图标对象</returns>
        public static Icon GetFileIcon(string fileName, bool isSmallIcon)
        {
            long imageIndex;
            return GetFileIcon(fileName, isSmallIcon, out imageIndex);
        }

        /// <summary>
        /// 获取系统文件图标
        /// </summary>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径,如果是一个exe可执行文件，请提供完整的文件名（包含路径信息）</param>
        /// <param name="isSmallIcon">是否返回小图标</param>
        /// <param name="imageIndex">输出与返回图标对应的系统图标索引号</param>
        /// <returns>返回一个Icon类型的文件图标对象</returns>
        public static Icon GetFileIcon(string fileName, bool isSmallIcon, out long imageIndex)
        {
            imageIndex = ErrorFileIndex;
            if (String.IsNullOrEmpty(fileName))
                return null;

            SHFileInfo shfi = new SHFileInfo();
            SHFileInfoFlags uFlags = SHFileInfoFlags.Icon | SHFileInfoFlags.ShellIconSize;
            if (isSmallIcon)
                uFlags |= SHFileInfoFlags.SmallIcon;
            else
                uFlags |= SHFileInfoFlags.LargeIcon;
            FileInfo fi = new FileInfo(fileName);
            if (fi.Name.ToUpper().EndsWith(".EXE"))
                uFlags |= SHFileInfoFlags.ExeType;
            else
                uFlags |= SHFileInfoFlags.UseFileAttributes;

            int iTotal = (int)SHGetFileInfo(fileName, FileAttribute.Normal, ref shfi, (uint)Marshal.SizeOf(shfi), uFlags);
            //或int iTotal = (int)SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), uFlags);
            Icon icon = null;
            if (iTotal > 0)
            {
                icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
                imageIndex = shfi.iIcon.ToInt64();
            }
            DestroyIcon(shfi.hIcon); //释放资源
            return icon;
        }

        /// <summary>
        /// 获取系统文件夹默认图标
        /// </summary>
        /// <param name="isSmallIcon">是否返回小图标</param>
        /// <returns>返回一个Icon类型的文件夹图标对象</returns>
        public static Icon GetFolderIcon(bool isSmallIcon)
        {
            long imageIndex;
            return GetFolderIcon(isSmallIcon, out imageIndex);
        }

        /// <summary>
        /// 获取系统文件夹默认图标
        /// </summary>
        /// <param name="isSmallIcon">是否返回小图标</param>
        /// <param name="imageIndex">输出与返回图标对应的系统图标索引号</param>
        /// <returns>返回一个Icon类型的文件夹图标对象</returns>
        public static Icon GetFolderIcon(bool isSmallIcon, out long imageIndex)
        {
            return GetFolderIcon(Environment.SystemDirectory, isSmallIcon, out imageIndex);
        }

        /// <summary>
        /// 获取系统文件夹默认图标
        /// </summary>
        /// <param name="folderName">文件夹名称,如果想获取自定义文件夹图标，请指定完整的文件夹名称（如 F:\test)</param>
        /// <param name="isSmallIcon">是否返回小图标</param>
        /// <param name="imageIndex">输出与返回图标对应的系统图标索引号</param>
        /// <returns>返回一个Icon类型的文件夹图标对象</returns>
        public static Icon GetFolderIcon(string folderName, bool isSmallIcon, out long imageIndex)
        {
            imageIndex = ErrorFolderIndex;
            if (String.IsNullOrEmpty(folderName))
                return null;

            SHFileInfo shfi = new SHFileInfo();
            SHFileInfoFlags uFlags = SHFileInfoFlags.Icon | SHFileInfoFlags.ShellIconSize | SHFileInfoFlags.UseFileAttributes;
            if (isSmallIcon)
                uFlags |= SHFileInfoFlags.SmallIcon;
            else
                uFlags |= SHFileInfoFlags.LargeIcon;

            int iTotal = (int)SHGetFileInfo(folderName, FileAttribute.Directory, ref shfi, (uint)Marshal.SizeOf(shfi), uFlags);
            //或int iTotal = (int)SHGetFileInfo("", 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHFileInfoFlags.Icon | SHFileInfoFlags.SmallIcon);
            Icon icon = null;
            if (iTotal > 0)
            {
                icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
                imageIndex = shfi.iIcon.ToInt64();
            }
            DestroyIcon(shfi.hIcon); //释放资源
            return icon;
        }

        /// <summary>
        /// 获取磁盘驱动器图标
        /// </summary>
        /// <param name="driverMark">有效的磁盘标号，如C、D、I等等，不区分大小写</param>
        /// <param name="isSmallIcon">标识是获取小图标还是获取大图标</param>
        /// <param name="imageIndex">输出与返回图标对应的系统图标索引号</param>
        /// <returns>返回一个Icon类型的磁盘驱动器图标对象</returns>
        public static Icon GetDriverIcon(char driverMark, bool isSmallIcon)
        {
            long imageIndex;
            return GetDriverIcon(driverMark, isSmallIcon, out imageIndex);
        }

        /// <summary>
        /// 获取磁盘驱动器图标
        /// </summary>
        /// <param name="driverMark">有效的磁盘标号，如C、D、I等等，不区分大小写</param>
        /// <param name="isSmallIcon">标识是获取小图标还是获取大图标</param>
        /// <param name="imageIndex">输出与返回图标对应的系统图标索引号</param>
        /// <returns>返回一个Icon类型的磁盘驱动器图标对象</returns>
        public static Icon GetDriverIcon(char driverMark, bool isSmallIcon, out long imageIndex)
        {
            imageIndex = ErrorDriverIndex;
            //非有效盘符，返回封装的磁盘图标
            if (driverMark < 'a' && driverMark > 'z' && driverMark < 'A' && driverMark > 'Z')
            {
                return null;
            }
            string driverName = driverMark.ToString().ToUpper() + ":\\";

            SHFileInfo shfi = new SHFileInfo();
            SHFileInfoFlags uFlags = SHFileInfoFlags.Icon | SHFileInfoFlags.ShellIconSize | SHFileInfoFlags.UseFileAttributes;
            if (isSmallIcon)
                uFlags |= SHFileInfoFlags.SmallIcon;
            else
                uFlags |= SHFileInfoFlags.LargeIcon;
            int iTotal = (int)SHGetFileInfo(driverName, FileAttribute.Normal, ref shfi, (uint)Marshal.SizeOf(shfi), uFlags);
            //int iTotal = (int)SHGetFileInfo(driverName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), uFlags);
            Icon icon = null;
            if (iTotal > 0)
            {
                icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
                imageIndex = shfi.iIcon.ToInt64();
            }
            DestroyIcon(shfi.hIcon); //释放资源
            return icon;
        }

        /// <summary>
        /// 给出文件扩展名（.*），返回相应图标;若不以"."开头则返回文件夹的图标。
        /// </summary>
        /// <param name="fileType">文件扩展名</param>
        /// <param name="isLarge">是否返回大图标</param>
        /// <returns>返回一个Icon类型的图标对象</returns>
        public static Icon GetIconByFileType(string fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(string.Empty))
                return null;
            RegistryKey regVersion = null;
            string regFileType = null;
            string regIconString = null;
            string systemDirectory = Environment.SystemDirectory + "\\";
            if (fileType[0] == '.')
            {
                //读系统注册表中文件类型信息
                regVersion = Registry.ClassesRoot.OpenSubKey(fileType, true);
                if (regVersion != null)
                {
                    regFileType = regVersion.GetValue("") as string;
                    regVersion.Close();
                    regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", true);
                    if (regVersion != null)
                    {
                        regIconString = regVersion.GetValue("") as string;
                        regVersion.Close();
                    }
                }
                if (regIconString == null)
                {
                    //没有读取到文件类型注册信息，指定为未知文件类型的图标
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                //直接指定为文件夹图标
                regIconString = systemDirectory + "shell32.dll,3";
            }
            string[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标
                fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                //调用API方法读取图标
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            catch
            {
                fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
                //调用API方法读取图标
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            return resultIcon;
        }

        /// <summary>
        /// 获取指定文件的类型描述
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回文件所属类型的类型描述</returns>
        public static string GetTypeName(string fileName)
        {
            string typeName = GetTypeName(fileName, FileAttribute.Normal, SHFileInfoFlags.UseFileAttributes | SHFileInfoFlags.TypeName | SHFileInfoFlags.DisplayName);
            if (string.IsNullOrEmpty(typeName))
                return "";
            else
                return typeName.Trim();
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="path">文件名</param>
        /// <param name="dwAttr">文件信息</param>
        /// <param name="dwFlag">信息控制字</param>
        /// <returns></returns>
        private static string GetTypeName(string path, FileAttribute dwAttr, SHFileInfoFlags dwFlag)
        {
            try
            {
                SHFileInfo fi = new SHFileInfo();
                int iTotal = (int)SHGetFileInfo(path, dwAttr, ref fi, 0, dwFlag);
                if (iTotal > 0)
                {
                    return fi.szTypeName;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文件扩展名说明
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetFileTypeDescription(string ext)
        {
            RegistryKey rKey = null;
            RegistryKey sKey = null;
            string FileType = "";
            try
            {
                rKey = Registry.ClassesRoot;
                sKey = rKey.OpenSubKey(ext);
                if (sKey != null && (string)sKey.GetValue("", ext) != ext)
                {
                    sKey = rKey.OpenSubKey((string)sKey.GetValue("", ext));
                    FileType = (string)sKey.GetValue("");
                }
                else
                {
                    FileType = ext.Substring(ext.LastIndexOf('.') + 1).ToUpper() + " File";
                }
                return FileType;
            }
            finally
            {
                if (sKey != null)
                    sKey.Close();
                if (rKey != null)
                    rKey.Close();
            }
        }
    }
}