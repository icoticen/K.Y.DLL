using K.Y.DLL.Tool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace K.Y.DLL.Tool
{
    public class T_File
    {
        #region FileOperate

        //public static Int32 File_Delete(String FilePath)
        //{
        //    try
        //    {
        //        if (File.Exists(FilePath))
        //        {
        //            File.Delete(FilePath);
        //            return 1;
        //        }
        //        return 0;
        //    }
        //    catch { return -1; }
        //}
        //public static Int32 Dir_Delete(String FilePath)
        //{
        //    try
        //    {
        //        if (Directory.Exists(FilePath))
        //        {
        //            Directory.Delete(FilePath);
        //            return 1;
        //        }
        //        return 0;
        //    }
        //    catch { return -1; }
        //}
        //public static Int32 Dir_Creat(String FilePath)
        //{
        //    try
        //    {
        //        if (!Directory.Exists(FilePath))
        //        {
        //            Directory.CreateDirectory(FilePath);
        //            return 1;
        //        }
        //        return 0;
        //    }
        //    catch { return -1; }
        //}
        ////获取当前工作根目录
        //public static string Dir_GetRootDir()
        //{
        //    return System.AppDomain.CurrentDomain.BaseDirectory;
        //}
        ////CreatDirectory
        //public static void Dir_CreatDirectory(string DirPath)
        //{
        //    DirectoryInfo dir = new DirectoryInfo(DirPath);
        //    if (!dir.Exists)
        //    {
        //        Directory.CreateDirectory(DirPath);
        //    }
        //}
        ////遍历目录
        //public static List<FileInfo> File_GetFiles(string DirPath, Boolean SearchChildDir)
        //{
        //    List<FileInfo> lFileList = new List<FileInfo>();
        //    DirectoryInfo dir = new DirectoryInfo(DirPath);
        //    lFileList.AddRange(dir.GetFiles());

        //    if (SearchChildDir)
        //    {
        //        //获取子目录
        //        var lDir = dir.GetDirectories();
        //        if (lDir.Count() > 0)
        //        {
        //            foreach (var d in lDir)
        //            {

        //                lFileList.AddRange(File_GetFiles(d.FullName, SearchChildDir));
        //            }
        //        }
        //    }
        //    return lFileList;
        //}
        ////遍历目录，获取指定文件类型文件列表
        //public static List<FileInfo> File_GetFilesByExtensions(string DirPath, string Extension)
        //{
        //    List<FileInfo> lFileList = new List<FileInfo>();
        //    DirectoryInfo dir = new DirectoryInfo(DirPath);
        //    var lFile = dir.GetFiles();
        //    if (lFile.Count() > 0)
        //    {
        //        foreach (var m in lFile)
        //        {
        //            if (string.IsNullOrEmpty(Extension) ? true : Extension.Contains(m.Extension))
        //            {
        //                lFileList.Add(m);
        //            }
        //        }
        //    }
        //    //获取子目录
        //    var lDir = dir.GetDirectories();
        //    if (lDir.Count() > 0)
        //    {
        //        foreach (var d in lDir)
        //        {

        //            lFileList.AddRange(File_GetFilesByExtensions(d.FullName, Extension));
        //        }
        //    }
        //    return lFileList;
        //}
        ////读取列表文件~
        //public static List<FileInfo> File_GetFilesFromStream(String FilePath)
        //{
        //    StreamReader sr = new StreamReader(FilePath);
        //    String str = sr.ReadToEnd();
        //    List<string> lstr = str.Replace('\r', '\n').Split('\n').Where(p => !string.IsNullOrEmpty(p)).ToList();
        //    var lFileInfo = new List<FileInfo>();
        //    lstr.ForEach(p =>
        //    {
        //        FileInfo file = new FileInfo(p);
        //        if (file.Exists)
        //        {
        //            lFileInfo.Add(file);
        //        }
        //    });
        //    return lFileInfo;
        //}
        ////保存文件名称列表到文件
        //public static void File_SaveFilesToStream(String FilePath, List<FileInfo> lFileInfo)
        //{
        //    StreamWriter sw = new StreamWriter(FilePath);
        //    foreach (var m in lFileInfo)
        //    {
        //        sw.WriteLine(m.FullName);
        //    }
        //    sw.Flush();
        //    sw.Close();
        //    sw.Dispose();
        //}
        public static String File_OpenText(String FilePath, Boolean CreatFile = true, Boolean CreateDir = true)
        {
            try
            {
                FileInfo File = new FileInfo(FilePath);

                if (!File.Directory.Exists)
                {
                    if (!CreateDir) return "";
                    File.Directory.Create();
                }

                var Content = "";
                if (File.Exists)
                {
                    using (StreamReader sr = File.OpenText())
                        Content = sr.ReadToEnd();
                }
                else
                {
                    if (!CreatFile) return "";
                    using (StreamWriter sw = File.CreateText())
                    {
                        sw.WriteLine(Content);
                    }
                }

                return Content;
            }
            catch (Exception ex) { T_Log.LogError(ex); return ""; }
        }
        public static Int32 File_CreateText(String FilePath, String Content, Boolean CreateDir = true)
        {
            try
            {
                FileInfo File = new FileInfo(FilePath);
                if (!File.Directory.Exists)
                {
                    if (!CreateDir) return 0;
                    File.Directory.Create();
                }

                using (StreamWriter sw = File.CreateText())
                {
                    sw.WriteLine(Content);
                }
                return 1;
            }
            catch (Exception ex) { T_Log.LogError(ex); return -1; }
        }
        public static Int32 File_AppendText(String FilePath, String Content, Boolean CreatFile = true, Boolean CreateDir = true)
        {
            try
            {
                FileInfo File = new FileInfo(FilePath);

                if (!File.Directory.Exists)
                {
                    if (!CreateDir) return 0;
                    File.Directory.Create();
                }

                if (File.Exists)
                {
                    using (StreamWriter sw = File.AppendText())
                    {
                        sw.WriteLine(Content);
                    }
                }
                else
                {
                    if (!CreatFile) return 0;
                    using (StreamWriter sw = File.CreateText())
                    {
                        sw.WriteLine(Content);
                    }
                }
                return 1;
            }
            catch (Exception ex) { T_Log.LogError(ex); return -1; }
        }

        public static XmlDocument XML_LoadXml(String Content)
        {
            var XML = new XmlDocument();
            XML.LoadXml(Content);
            return XML;
        }
        public static XmlDocument XML_Load(String FilePath)
        {
            var XML = new XmlDocument();
            XML.Load(FilePath);
            return XML;
        }

        #endregion

        #region Image_Thumbnail
        //public static String Image_Thumbnail(String OriginalFilePath)
        //{
        //    var FileInfo = new FileInfo(OriginalFilePath);
        //    if (!FileInfo.Exists) return "";
        //    return Image_Thumbnail(OriginalFilePath, 128);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, Boolean OverWrite)
        //{
        //    var FileInfo = new FileInfo(OriginalFilePath);
        //    if (!FileInfo.Exists) return "";
        //    return Image_Thumbnail(OriginalFilePath, 128, OverWrite);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, Int32 Size)
        //{
        //    return Image_Thumbnail(OriginalFilePath, Size, true);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, Int32 Size, Boolean OverWrite)
        //{
        //    var FileInfo = new FileInfo(OriginalFilePath);
        //    if (!FileInfo.Exists) return "";
        //    return Image_Thumbnail(OriginalFilePath, FileInfo.DirectoryName + "\\" + FileInfo.Name.Ex_ToList('.').FirstOrDefault() + "_" + Size + "_" + Size + FileInfo.Extension, Size, OverWrite);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, String ThumbnailFilePath)
        //{
        //    return Image_Thumbnail(OriginalFilePath, ThumbnailFilePath, 128);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, String ThumbnailFilePath, Boolean OverWrite)
        //{
        //    return Image_Thumbnail(OriginalFilePath, ThumbnailFilePath, 128, OverWrite);
        //}
        //public static String Image_Thumbnail(String OriginalFilePath, String ThumbnailFilePath, Int32 Size)
        //{
        //    return Image_Thumbnail(OriginalFilePath, ThumbnailFilePath, Size, true);
        //}
        /// <summary>
        /// 不建议使用  留着因为以前项目里有用到 早晚会被清理掉的
        /// </summary>
        /// <param name="OriginalFilePath"></param>
        /// <param name="ThumbnailFilePath"></param>
        /// <param name="Size"></param>
        /// <param name="OverWrite"></param>
        /// <returns></returns>
        public static String Image_Thumbnail(String OriginalFilePath, String ThumbnailFilePath = default(String), Int32 Size = 128, Boolean OverWrite = true)
        {
            var FileInfo = new FileInfo(OriginalFilePath);
            if (!FileInfo.Exists) return "";
            if (String.IsNullOrWhiteSpace(ThumbnailFilePath)) ThumbnailFilePath = FileInfo.DirectoryName + "\\" + FileInfo.Name.Ex_ToList('.').FirstOrDefault() + "_" + Size + "_" + Size + FileInfo.Extension;

            if (File.Exists(ThumbnailFilePath))
            {
                if (!OverWrite)
                    return ThumbnailFilePath;
                else
                    File.Delete(ThumbnailFilePath);
            }

            System.Drawing.Image image = System.Drawing.Image.FromFile(OriginalFilePath);
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            if (srcHeight <= Size && srcWidth <= Size)
            {
                File.Copy(OriginalFilePath, ThumbnailFilePath);
                return ThumbnailFilePath;
            }
            int thumbWidth = srcWidth > srcHeight ? Size : (Int32)(((float)srcWidth / srcHeight) * Size);
            int thumbHeight = srcHeight > srcWidth ? Size : (Int32)(((float)srcHeight / srcWidth) * Size);

            Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);
            //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量

            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High

            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //把原始图像绘制成上面所设置宽高的缩小图

            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

            //保存图像，大功告成！

            bmp.Save(ThumbnailFilePath);

            //最后别忘了释放资源
            bmp.Dispose();
            image.Dispose();
            return ThumbnailFilePath;
        }

        #endregion

        #region HttpPostedFileBase
        /// <summary>
        /// 返回 /AppData/PostedFiles/
        /// </summary>
        /// <param name="File"></param>
        /// <param name="IncludeExtension"></param>
        /// <returns></returns>
        //public static String File_Save(HttpPostedFileBase File, String IncludeExtension)
        //{
        //    return File_Save(File, System.Web.Configuration.WebConfigurationManager.AppSettings[""], IncludeExtension);
        //}
        public static String File_Save(HttpPostedFileBase File, String DirPath, String IncludeExtension)
        {
            return File_Save(File, DirPath, "_", IncludeExtension);
        }
        public static String File_Save(HttpPostedFileBase File, String DirPath, Boolean IntoDirDay, String IncludeExtension)
        {
            return File_Save(File, DirPath, "_", IntoDirDay, IncludeExtension);
        }
        public static String File_Save(HttpPostedFileBase File, String DirPath, String PreStr, String IncludeExtension)
        {
            if (File == null)
            {
                return "0";
            }
            if (File.FileName.LastIndexOf('.') > 0)
            {
                var ExtensionName = File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower();
                if (String.IsNullOrWhiteSpace(IncludeExtension) || IncludeExtension.Contains(ExtensionName))
                {
                    Int32 Index = 0;//同时多张
                    String MapPath = "/PostFile/" + DirPath + "/" + DateTime.Now.ToString("yyyy-MM") + "/" + DateTime.Now.ToString("dd") + "/";
                    String AutoFileName = PreStr + "_" + DateTime.Now.TimeOfDay.TotalMilliseconds + "_";
                    String Path = AppDomain.CurrentDomain.BaseDirectory + MapPath;
                    if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
                    do
                    {
                        Index++;
                    } while (System.IO.File.Exists(Path + AutoFileName + Index + ExtensionName));
                    File.SaveAs(Path + AutoFileName + Index + ExtensionName);
                    return MapPath + AutoFileName + Index + ExtensionName;
                }
            }
            return "-1";
        }
        public static String File_Save(HttpPostedFileBase File, String DirPath, String PreStr, Boolean IntoDirDay, String IncludeExtension)
        {
            if (File == null)
            {
                return "0";
            }
            if (File.FileName.LastIndexOf('.') > 0)
            {
                var ExtensionName = File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower();
                if (String.IsNullOrWhiteSpace(IncludeExtension) || IncludeExtension.Contains(ExtensionName))
                {
                    Int32 Index = 0;//同时多张
                    String MapPath = "/PostFile/" + DirPath + "/" + (IntoDirDay ? (DateTime.Now.ToString("yyyy-MM") + "/" + DateTime.Now.ToString("dd") + "/") : "");
                    String AutoFileName = PreStr + "_" + DateTime.Now.TimeOfDay.TotalMilliseconds + "_";
                    String Path = AppDomain.CurrentDomain.BaseDirectory + MapPath;
                    if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
                    do
                    {
                        Index++;
                    } while (System.IO.File.Exists(Path + AutoFileName + Index + ExtensionName));
                    File.SaveAs(Path + AutoFileName + Index + ExtensionName);
                    return MapPath + AutoFileName + Index + ExtensionName;
                }
            }
            return "-1";
        }
        public static List<String> File_Save(IEnumerable<HttpPostedFileBase> Files, String DirPath, String IncludeExtension)
        {
            return File_Save(Files, DirPath, "_", true, IncludeExtension);
        }
        public static List<String> File_Save(IEnumerable<HttpPostedFileBase> Files, String DirPath, Boolean IntoDirDay, String IncludeExtension)
        {
            return File_Save(Files, DirPath, "_", IntoDirDay, IncludeExtension);
        }
        public static List<String> File_Save(IEnumerable<HttpPostedFileBase> Files, String DirPath, String PreStr, String IncludeExtension)
        {
            return File_Save(Files, DirPath, PreStr, true, IncludeExtension);
        }
        public static List<String> File_Save(IEnumerable<HttpPostedFileBase> Files, String DirPath, String PreStr, Boolean IntoDirDay, String IncludeExtension)
        {
            var R = new List<String>();
            if (Files.Count() == 0)
            {
                return null;
            }
            foreach (var File in Files)
            {
                if (File.FileName.LastIndexOf('.') > 0)
                {
                    var ExtensionName = File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower();
                    if (!String.IsNullOrWhiteSpace(IncludeExtension) && !IncludeExtension.Contains(ExtensionName)) return R;
                }
            }
            Int32 Index = 0;//同时多张
            String MapPath = "/PostFile/" + DirPath + "/" + (IntoDirDay ? (DateTime.Now.ToString("yyyy-MM") + "/" + DateTime.Now.ToString("dd") + "/") : "");

            String Path = AppDomain.CurrentDomain.BaseDirectory + MapPath;
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            foreach (var File in Files)
            {
                var ExtensionName = File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower();
                String AutoFileName = PreStr + "_" + DateTime.Now.TimeOfDay.TotalMilliseconds + "_";
                do
                {
                    Index++;
                } while (System.IO.File.Exists(Path + AutoFileName + Index + ExtensionName));
                File.SaveAs(Path + AutoFileName + Index + ExtensionName);
                R.Add(MapPath + AutoFileName + Index + ExtensionName);
            }
            return R;
        }
        public static String File_Replace(HttpPostedFileBase File, String AbsoluteFilepath, String IncludeExtension)
        {
            if (File == null)
            {
                return "0";
            }
            if (File.FileName.LastIndexOf('.') > 0)
            {
                var ExtensionName = File.FileName.Substring(File.FileName.LastIndexOf('.')).ToLower();
                if (String.IsNullOrWhiteSpace(IncludeExtension) || IncludeExtension.Contains(ExtensionName))
                {
                    FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + AbsoluteFilepath);

                    if (!Directory.Exists(fi.Directory.FullName)) Directory.CreateDirectory(fi.Directory.FullName);
                    ;
                    File.SaveAs(fi.FullName);
                    return AbsoluteFilepath;
                }
            }
            return "-1";
        }
        #endregion



        //public class File_
        //{

        //    System.Web.HttpPostedFileBase PostedFile { set; }
        //    Func<System.Web.HttpPostedFileBase, String> F_Save { set; }
        //    public File_(System.Web.HttpPostedFileBase F,String K,Int32 I) {
        //        this.PostedFile = F;
        //        this.Key = K;
        //        this.Index = I;
        //        this.Length = F.ContentLength;

        //    }
        //    private void File_Save() { 

        //    }
        //    public String Key { get; private set; }
        //    public Int32 Index { get; private set; }
        //    public Int32 FileName { get; private set; }
        //    public Int32 ExtensionName { get; private set; }
        //    public String OriginalURL { get; private set; }
        //    public String ThumbnailURL(Int32 Size, Boolean Creat)
        //    {
        //        return "";
        //    }

        //}
    }
}
