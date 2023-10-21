using AlgodooStudio.Analyzer;
using AlgodooStudio.Phun.Archive;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// Phun存档工具集
    /// </summary>
    public static class PhunSaveTools
    {
        /// <summary>
        /// 解压指定路径下多个phz文件
        /// </summary>
        /// <param name="phzPath">指定的phz文件地址集合</param>
        /// <returns>phz文件解压后得到的Phun包集合</returns>
        public static PhunPackage[] DeCompress(string[] phzPath)
        {
            List<PhunPackage> packages = new List<PhunPackage>();
            foreach (var path in phzPath)
            {
                packages.Add(DeCompress(path));
            }
            return packages.ToArray();
        }

        /// <summary>
        /// 解压指定路径下单个phz文件
        /// </summary>
        /// <param name="phzPath">指定的phz文件</param>
        /// <returns>phz文件解压后得到的Phun包</returns>
        public static PhunPackage DeCompress(string phzPath)
        {
            //加载文件
            using (FileStream fs = new FileStream(phzPath, FileMode.Open))
            {
                //初始化关键子项
                Image img = null;
                PhnSave scene = null;
                Dictionary<string, string> checkNum = new Dictionary<string, string>();
                //初始化贴图文件集合
                Dictionary<string, Image> textures = new Dictionary<string, Image>();
                //解压文件
                using (ZipArchive phz = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    //赋值操作
                    foreach (var item in phz.Entries)//这块直接就读取文件无视内部文件夹
                    {
                        //启动解包流
                        using (DeflateStream tfs = (DeflateStream)item.Open())
                        {
                            //从流中解包
                            switch (item.Name)
                            {
                                case "thumb.png"://缩略图
                                    img = new Bitmap(tfs);
                                    break;

                                case "scene.phn"://场景信息
                                    StringBuilder sbs = new StringBuilder();
                                    byte[] buffers = new byte[item.Length];
                                    while (true)
                                    {
                                        if (tfs.Read(buffers, 0, buffers.Length) == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            sbs.Append(Encoding.UTF8.GetString(buffers));
                                        }
                                    }
                                    scene = AnalyzeScene(sbs.ToString());//分析并生成场景信息集
                                    break;

                                case "checksums.txt"://检查码
                                    using (StreamReader sr = new StreamReader(tfs))
                                    {
                                        //逐行读取
                                        while (true)
                                        {
                                            string s = sr.ReadLine();
                                            if (s == null)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                checkNum.Add(s.Substring(0, s.LastIndexOf(' ')), s.Substring(s.LastIndexOf(' ') + 1));
                                            }
                                        }
                                    }
                                    break;

                                default://贴图文件
                                    textures.Add(item.Name, new Bitmap(tfs));
                                    break;
                            }
                        }
                    }
                    //释放打开此压缩包所占用的资源
                }
                return new PhunPackage(scene, img, checkNum, textures);
            }
        }

        /// <summary>
        /// 压缩指定的Phun包
        /// </summary>
        /// <param name="package">指定的phun包</param>
        /// <param name="path">指定的存放路径</param>
        public static void Compress(PhunPackage package, string path)
        {
            //创建临时文件夹
            DirectoryInfo dirTemp = Directory.CreateDirectory(Path.GetDirectoryName(path) + "\\" + "Temp");
            DirectoryInfo textureTemp = Directory.CreateDirectory(Path.GetDirectoryName(path) + "\\" + "Temp\\texture");
            //创建缩略图
            using (FileStream fs = new FileStream(dirTemp.FullName + "\\thumb.png", FileMode.Create))
            {
                byte[] image = BitmapToByteArray(new Bitmap(package.Thumb));
                fs.Write(image, 0, image.Length);
            }
            //创建材质包
            foreach (var item in package.Texture)
            {
                using (FileStream fs = new FileStream(dirTemp.FullName + $"\\texture\\{item.Key}", FileMode.Create))
                {
                    byte[] image = BitmapToByteArray(new Bitmap(item.Value));
                    fs.Write(image, 0, image.Length);
                }
            }
            //创建存档文件(这里需要修一修)
            using (FileStream fs = new FileStream(dirTemp.FullName + "\\scene.phn", FileMode.Create))
            {
                //将设置信息罗列出来，之前怎么弄得就怎么弄回去，将对象信息加上去
                string strs = "// FileVersion 19\n// Algodoo scene created by Algodoo for Education v2.0.2 b10\n\n";
                //字符串化设置
                foreach (var item in package.Scene.SceneSettings)
                {
                    strs += item.ToString() + "\n";
                }
                foreach (var item in package.Scene.SceneObjects)
                {
                    strs += item.ToString() + "\n";
                }
                byte[] buffer = Encoding.UTF8.GetBytes(strs);
                fs.Write(buffer, 0, buffer.Length);
            }
            //创建checkNum文件
            using (FileStream fs = new FileStream(dirTemp.FullName + "\\checksums.txt", FileMode.Create))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in package.CheckSums)
                {
                    sb.Append(item.Key);
                    sb.Append(" ");
                    sb.Append(item.Value);
                    sb.Append("\n");
                }
                byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
                fs.Write(bytes, 0, bytes.Length);
            }
            //如果之前的文件存在则删除
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            //创建文件
            ZipFile.CreateFromDirectory(dirTemp.FullName, path, CompressionLevel.Fastest, false);
            //删除临时文件夹
            dirTemp.Delete(true);
        }

        /// <summary>
        /// 解析scene.phn文件成Phun存档
        /// </summary>
        /// <param name="sceneThyme">scene.phn 文件内容</param>
        /// <returns>Phun存档</returns>
        private static PhnSave AnalyzeScene(string sceneThyme)
        {
            PhunAnalyzer pa = new PhunAnalyzer();
            pa.Analyzer(sceneThyme);
            ArchiveInfo info = new ArchiveInfo();
            SceneSettings ss = pa.settings;
            foreach (var item in ss.Contents)
            {
                if (item.GroupName == "FileInfo")
                {
                    foreach (var settingItem in item.Contents)
                    {
                        switch (settingItem.Name)
                        {
                            case "title":
                                info.Title = settingItem.Value;
                                break;

                            case "author":
                                info.Author = settingItem.Value;
                                break;

                            case "version":
                                info.Version = Convert.ToInt32(settingItem.Value);
                                break;

                            case "algoboxID":
                                info.AlgoboxID = settingItem.Value;
                                break;

                            case "description":
                                info.Description = settingItem.Value;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            SceneObjects so = pa.objects;
            return new PhnSave(info, ss, so);
        }

        /// <summary>
        /// Bitmap转换为字节数组
        /// </summary>
        /// <param name="bitmap">指定bitmap</param>
        /// <returns>bitmap的字节数组</returns>
        private static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            byte[] bytes;
            //创建内存流
            using (MemoryStream ms = new MemoryStream())
            {
                //将bitmap导入到流中
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //重置流起始点
                ms.Seek(0, SeekOrigin.Begin);
                //创建字节数组并导入
                bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
            }
            return bytes;
        }
    }
}