using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Tool
{

    public class T_Log 
    {
        public static void LogError(Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Exception:");
            sb.Append(ex.ToString());
            sb.Append("\r\n");

            T_Log.Log("Log/Error/", sb.ToString());
        }
        public static void LogError(String Msg, Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            sb.Append("Exception:");
            sb.Append(ex.ToString());
            sb.Append("\r\n");
            T_Log.Log("Log/Error/", sb.ToString());
        }
        public static void LogError(String Func, String Msg, Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");

            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            sb.Append("Exception:");
            sb.Append(ex.ToString());
            sb.Append("\r\n");

            T_Log.Log("Log/Error/", sb.ToString());
        }
        //public static void LogPage()
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
       
        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");

        //    T_Log.Log("Log/Error/", sb.ToString());
        //}
        //public static void LogPage(String Files)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    sb.Append("Files:");
        //    sb.Append(Files);
        //    sb.Append("\r\n");

        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");

        //    T_Log.Log("Log/Page/", sb.ToString());
        //}
        //public static void LogPage(String Param, String Files)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
          
        //    sb.Append("Param:");
        //    sb.Append(Param);
        //    sb.Append("\r\n");

        //    sb.Append("Files:");
        //    sb.Append(Files);
        //    sb.Append("\r\n");

        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");

        //    T_Log.Log("Log/Page/", sb.ToString());
        //}
        //public static void LogPage(String Msg, String Param, String Files)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
 
        //    sb.Append("Msg:");
        //    sb.Append(Msg);
        //    sb.Append("\r\n");

        //    sb.Append("Param:");
        //    sb.Append(Param);
        //    sb.Append("\r\n");

        //    sb.Append("Files:");
        //    sb.Append(Files);
        //    sb.Append("\r\n");

        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");

        //    T_Log.Log("Log/Page/", sb.ToString());
        //}
        //public static void LogPage(String Func, String Msg="")
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("Func:");
        //    sb.Append(Func);
        //    sb.Append("\r\n");

        //    sb.Append("Msg:");
        //    sb.Append(Msg);
        //    sb.Append("\r\n");

        //    sb.Append("Param:{\r\n");
        //    System.Web.HttpContext.Current.Request.Params.AllKeys.ToList().ForEach(p => sb.Append("\t\t" + p + ":" + System.Web.HttpContext.Current.Request.Params[p] + "\r\n"));
        //    sb.Append("}\r\n");

        //    sb.Append("Files:{\r\n");
        //    if (System.Web.HttpContext.Current.Request.Files.Count > 0)
        //        foreach (var K in System.Web.HttpContext.Current.Request.Files.AllKeys)
        //        {
        //            var F = System.Web.HttpContext.Current.Request.Files[K];
        //            sb.Append("\t\t------------------------[" + K + "]------------------------\r\n");
        //            sb.Append("\t\tFileName:" + F.FileName + "\r\n");
        //            sb.Append("\t\tContentLength:" + F.ContentLength + "\r\n");
        //            sb.Append("\t\tContentType:" + F.ContentType + "\r\n");
        //        }
        //    sb.Append("}\r\n");

        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");

        //    T_Log.Log("Log/Page/", sb.ToString());
        //}
        public static void LogMsg(String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            T_Log.Log("Log/Msg/", sb.ToString());
        }
        public static void LogMsg(String Func, String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");

            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");
            T_Log.Log("Log/Msg/", sb.ToString());
        }
        public static void LogRun(String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            T_Log.Log("Log/Run/", sb.ToString());
        }
        public static void LogRun(String Func, String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");

            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            T_Log.Log("Log/Run/", sb.ToString());
        }
        public static void LogTODO(String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            T_Log.Log("Log/TODO/", sb.ToString());
        }
        public static void LogTODO(String Func, String Msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");

            sb.Append("Msg:");
            sb.Append(Msg);
            sb.Append("\r\n");

            T_Log.Log("Log/TODO/", sb.ToString());
        }



        public static void LogRequest(String Func)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");
            sb.Append("Param:{\r\n");
            System.Web.HttpContext.Current.Request.Params.AllKeys.ToList().ForEach(p => sb.Append("\t\t" + p + ":" + System.Web.HttpContext.Current.Request.Params[p] + "\r\n"));
            sb.Append("}\r\n");
            sb.Append("Files:{\r\n");
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
                foreach (var K in System.Web.HttpContext.Current.Request.Files.AllKeys)
                {
                    var F = System.Web.HttpContext.Current.Request.Files[K];
                    sb.Append("\t\t------------------------[" + K + "]------------------------\r\n");
                    sb.Append("\t\tFileName:" + F.FileName + "\r\n");
                    sb.Append("\t\tContentLength:" + F.ContentLength + "\r\n");
                    sb.Append("\t\tContentType:" + F.ContentType + "\r\n");
                }
            sb.Append("}\r\n");
            sb.Append("####################################################################################################");
            //AR_API.LogRun(UrlMsg);          
            Log("Log/Request/", sb.ToString());
        }
        //public static void LogRequest(String Func, String Msg = "", Boolean HasFiles = false, Boolean HasParam = false)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("URL:");
        //    sb.Append(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    sb.Append("\r\n");
        //    sb.Append("Func:");
        //    sb.Append(Func);
        //    sb.Append("\r\n");
        //    sb.Append("Msg:");
        //    sb.Append(Msg);
        //    sb.Append("\r\n");
        //    if (HasFiles)
        //    {
        //        sb.Append("Files:{\r\n");
        //        if (System.Web.HttpContext.Current.Request.Files.Count > 0)
        //            foreach (var K in System.Web.HttpContext.Current.Request.Files.AllKeys)
        //            {
        //                var F = System.Web.HttpContext.Current.Request.Files[K];
        //                sb.Append("\t\t------------------------[" + K + "]------------------------\r\n");
        //                sb.Append("\t\tFileName:" + F.FileName + "\r\n");
        //                sb.Append("\t\tContentLength:" + F.ContentLength + "\r\n");
        //                sb.Append("\t\tContentType:" + F.ContentType + "\r\n");
        //            }
        //        sb.Append("}\r\n");
        //    }
        //    if (HasParam)
        //    {
        //        sb.Append("Param:{\r\n");
        //        System.Web.HttpContext.Current.Request.Params.AllKeys.ToList().ForEach(p => sb.Append("\t\t" + p + ":" + System.Web.HttpContext.Current.Request.Params[p] + "\r\n"));
        //        sb.Append("}\r\n");
        //    }
        //    sb.Append("####################################################################################################");
        //    //AR_API.LogRun(UrlMsg);          
        //    Log("Log/Request/", sb.ToString());
        //}
       
        public static void LogUpload(String Func)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Func:");
            sb.Append(Func);
            sb.Append("\r\n");
            sb.Append("Files:{\r\n");
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
                foreach (var K in System.Web.HttpContext.Current.Request.Files.AllKeys)
                {
                    var F = System.Web.HttpContext.Current.Request.Files[K];
                    sb.Append("\t\t------------------------[" + K + "]------------------------\r\n");
                    sb.Append("\t\tFileName:" + F.FileName + "\r\n");
                    sb.Append("\t\tContentLength:" + F.ContentLength + "\r\n");
                    sb.Append("\t\tContentType:" + F.ContentType + "\r\n");
                }
            sb.Append("}\r\n");
            sb.Append("####################################################################################################");

            Log("Log/Upload/", sb.ToString());
        }


        //public static void LogError(Exception ex)
        //{
        //    Log("Log/Error", ex.ToString());
        //}
        //public static void LogMsg(String Msg)
        //{
        //    Log("Log/Msg", Msg);
        //}
        //public static void LogRun(String Msg)
        //{
        //    Log("Log/Run", Msg);
        //}

        public static void Log(String DirPath, String Msg)
        {
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + "/" + DirPath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";//WWSS System.Web.HttpContext.Current.Server.MapPath("~/Msg/");
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            String FileName = DateTime.Now.ToString("yyyy-MM-dd HH") + ".log";
            FileInfo file = new FileInfo(FilePath + FileName);
            StringBuilder Txt = new StringBuilder("");
            try
            {
                if (file.Exists)
                {
                    using (StreamWriter sw = file.AppendText())
                    {
                        sw.WriteLine(Msg + "  >>>>>>  " + DateTime.Now.ToString() + "\n\r");
                    }
                }
                else
                {
                    using (StreamWriter sw = file.CreateText())
                    {
                        sw.WriteLine(Msg + "  >>>>>>  " + DateTime.Now.ToString() + "\n\r");
                    }
                }
            }
            catch { }
        }
    }
}
