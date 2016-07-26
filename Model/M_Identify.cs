using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K.Y.DLL;

namespace K.Y.DLL.Model
{
    public class M_Identify
    {
        public Int32 ID { get; set; }
        public Int32 Identify { get; set; }
        public String IdentifyCode { get; set; }
        public dynamic Data { get; set; }

        public static M_Identify Get()
        {
            return Get("_Admin");
        }
        public static M_Identify Get(String CookieName)
        {
            try
            {
                var Cookie = System.Web.HttpContext.Current.Request.Cookies[CookieName];
                if (Cookie == null) return null;
                var T = System.Web.Security.FormsAuthentication.Decrypt(Cookie.Value);
                var M = T.Name.Ex_ToEntity<M_Identify>();
                return M;
            }
            catch
            {
                return null;
            }
        }

        public static void Set(M_Identify M)
        {
            Set("_Admin", M.Ex_ToJson());
        }
        public static void Set(String CookieName, M_Identify M)
        {
            Set(CookieName, M.Ex_ToJson());
        }
        public static void Set(String S)
        {
            Set("_Admin", S);
            //System.Web.Security.FormsAuthentication.SetAuthCookie(M.Ex_ToJson(), true, System.Web.Security.FormsAuthentication.FormsCookiePath);
        }
        public static void Set(String CookieName, String S)
        {
            var TS = System.Web.Security.FormsAuthentication.Encrypt(new System.Web.Security.FormsAuthenticationTicket(
                S, true, 1));
            var Cookie = new System.Web.HttpCookie(CookieName, TS);
            System.Web.HttpContext.Current.Response.SetCookie(Cookie);
            //System.Web.Security.FormsAuthentication.SetAuthCookie(M.Ex_ToJson(), true, System.Web.Security.FormsAuthentication.FormsCookiePath);
        }

        public static void Clear()
        {
            Clear("_Admin");
        }
        public static void Clear(String CookieName)
        {
            var Cookie = new System.Web.HttpCookie(CookieName, "");
            Cookie.Expires = DateTime.Now;
            System.Web.HttpContext.Current.Response.SetCookie(Cookie);
            //System.Web.Security.FormsAuthentication.SignOut();
        }
    }
}
