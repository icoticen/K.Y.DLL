using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace K.Y.DLL.Tool
{

    public class T_Web
    {
        public static String HttpGet(String Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url );
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, String.IsNullOrEmpty(response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(response.ContentEncoding));
            String retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
           
            return retString;
        }
        public static String HttpGet(String Url, String postDataStr)
        {
            if (!string.IsNullOrEmpty(postDataStr)) Url = Url + "?" + postDataStr;
            return HttpGet(Url);
        }

        public static String HttpPost(String Url, String postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, String.IsNullOrEmpty(response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(response.ContentEncoding));
            String retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public static String HttpPost_Plain(String Url, String ContextText)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "text/plain, charset=UTF-8";
            request.ContentLength = ContextText.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(ContextText);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            String retString = reader.ReadToEnd();
            return retString;
        }

        public static void HttpFileDownLoad(String FileName, String FilePath)
        {
            FileInfo FileInfo = new FileInfo(FilePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;Filename=" + FileName);
            HttpContext.Current.Response.AddHeader("Content-Length", FileInfo.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.WriteFile(FileInfo.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        #region 获取web客户端ip
        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {

            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;

        }
        #endregion

        public static String Tmpl_Javascript(String Content)
        {
            return "<script type='text/javascript'> " + Content + " </script>";
        }
        public static void Tmpl_Result_LayUI(K.Y.DLL.Model.M_Result R)
        {
            if (R == null) System.Web.HttpContext.Current.Response.Write(T_Web.Tmpl_Javascript("parent.K_LayPageClose('系统错误！',0)"));
            else if (R.result == 1) System.Web.HttpContext.Current.Response.Write(T_Web.Tmpl_Javascript("parent.K_LayPageClose('操作成功！',1)"));
            else System.Web.HttpContext.Current.Response.Write(T_Web.Tmpl_Javascript("parent.K_LayPageClose('操作失败！" + R.msg + "',0)"));
        }
    }
}
