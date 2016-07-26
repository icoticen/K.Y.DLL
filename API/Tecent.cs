using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.API
{
    public class Tencent
    {
        public class WeiXin
        {

            public class WX_Back
            {
                //-----------错误样例
                public Int32 errcode { get; set; }
                public String errmsg { get; set; }

            }
            public class WebAPP
            {
                public enum SCOPE
                {
                    snsapi_login,
                    snsapi_base,
                    snsapi_userinfo,
                }
                public static String appid { get; private set; }
                public static String secret { get; private set; }
                public static void init(String _appid, String _secret)
                {
                    appid = _appid;
                    secret = _secret;
                }
                ////public String access_token { get; private set; }
                ////public String openid { get; private set; }


                //public WebAPP(String _appid, String _secret)
                //{
                //    this.appid = _appid;
                //    this.secret = _secret;
                //}

                /// <summary>
                /// 第一步：请求CODE\r
                /// 用户允许授权后 将会重定向到redirect_uri的网址上 并且带上code和state参数\r
                /// 若用户禁止授权 则重定向后不会带上code参数 仅会带上state参数\n
                /// </summary>
                /// <param name="appid">appid           是	    应用唯一标识</param>
                /// <param name="redirect_uri">redirect_uri    是	    重定向地址，需要进行UrlEncode</param>
                /// <param name="state">state	        否	    用于保持请求和回调的状态，授权请求后原样带回给第三方。该参数可用于防止csrf攻击（跨站请求伪造攻击），建议第三方带上该参数，可设置为简单的随机数加session进行校验</param>
                /// <returns>
                /// </returns>
                public static String qrconnect(String appid, String redirect_uri, String state, params SCOPE[] scope)
                {
                    String redirecturl = System.Web.HttpUtility.UrlEncode(redirect_uri);
                    /// response_type	是	    填code
                    /// scope	        是	    应用授权作用域，拥有多个作用域用逗号（,）分隔，网页应用目前仅填写snsapi_login即可
                    var absoluteurl =
                        //"https://open.weixin.qq.com/connect/qrconnect" +//网页跳出二维码授权登陆
                        "https://open.weixin.qq.com/connect/oauth2/authorize" +//微信内部浏览器授权登陆
                        "?appid=" + appid +
                        "&redirect_uri=" + redirecturl +
                        "&response_type=code" +
                        "&scope=snsapi_login" + // string.Join(",", (scope ?? new[] { SCOPE.snsapi_login }).Select(p => p.ToString())) +//snsapi_base,snsapi_login,snsapi_userinfo
                        "&state=" + state +
                        "#wechat_redirect";


                    return absoluteurl;
                }
                /// <summary>
                /// 第二步：通过code获取access_token
                /// { 
                ///"access_token":"ACCESS_TOKEN", 
                ///"expires_in":7200, 
                ///"refresh_token":"REFRESH_TOKEN",
                ///"openid":"OPENID", 
                ///"scope":"SCOPE",
                ///"unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"//当且仅当该网站应用已获得该用户的userinfo授权时，才会出现该字段。
                ///}
                /// </summary>
                /// <param name="appid"></param>
                /// <param name="secret"></param>
                /// <param name="code"></param>
                /// <returns></returns>
                public static WX_Back_Access access_token(String appid, String secret, String code)
                {
                    var url = "https://api.weixin.qq.com/sns/oauth2/access_token" +
                        "?appid=" + appid +
                        "&secret=" + secret +
                        "&code=" + code +
                        "&grant_type=authorization_code";
                    var Stream = K.Y.DLL.Tool.T_Web.HttpGet(url, "");
                    var R = Stream.Ex_ToEntity<WX_Back_Access>();
                    return R;
                }
                /// <summary>
                /// 刷新access_token有效期
                /// { 
                ///"access_token":"ACCESS_TOKEN", 
                ///"expires_in":7200, 
                ///"refresh_token":"REFRESH_TOKEN", 
                ///"openid":"OPENID", 
                ///"scope":"SCOPE" 
                ///}
                /// </summary>
                /// <param name="appid"></param>
                /// <param name="refresh_token"></param>
                /// <returns></returns>
                public static WX_Back_Access refresh_token(String appid, String refresh_token)
                {
                    var url =
                        "https://api.weixin.qq.com/sns/oauth2/refresh_token" +
                        "?appid=" + appid +
                        "&refresh_token=" + refresh_token +
                        "&grant_type=refresh_token";
                    var Stream = K.Y.DLL.Tool.T_Web.HttpGet(url, "");
                    var R = Stream.Ex_ToEntity<WX_Back_Access>();
                    return R;
                }
                /// <summary>
                /// 检验授权凭证（access_token）是否有效
                /// </summary>
                /// <param name="access_token"></param>
                /// <param name="openid"></param>
                /// <returns></returns>
                public static WX_Back_Access auth(String access_token, String openid)
                {
                    var url = "https://api.weixin.qq.com/sns/auth" +
                        "?access_token=" + access_token +
                        "&openid=" + openid;
                    var Stream = K.Y.DLL.Tool.T_Web.HttpGet(url, "");
                    var R = Stream.Ex_ToEntity<WX_Back_Access>();
                    return R;
                }
                /// <summary>
                /// 第三步：通过access_token调用接口
                /// 获取userinfo
                /// </summary>
                /// <param name="access_token"></param>
                /// <param name="openid"></param>
                /// <returns></returns>
                public static WX_Back_UserInfo userinfo(String access_token, String openid)
                {
                    var url = "https://api.weixin.qq.com/sns/userinfo" +
                        "?access_token=" + access_token +
                        "&openid=" + openid;
                    var Stream = K.Y.DLL.Tool.T_Web.HttpGet(url, "");
                    var R = Stream.Ex_ToEntity<WX_Back_UserInfo>();
                    return R;
                }


                public class WX_Back
                {
                    //-----------错误样例
                    public Int32 errcode { get; set; }
                    public String errmsg { get; set; }

                }
                public class WX_Back_Access : WX_Back
                {
                    //-----------正确返回
                    public String access_token { get; set; }
                    public Int32 expires_in { get; set; }
                    public String refresh_token { get; set; }
                    public String openid { get; set; }
                    public String scope { get; set; }
                    public String unionid { get; set; }
                }
                public class WX_Back_UserInfo : WX_Back
                {
                    public String openid { get; set; }
                    public String nickname { get; set; }
                    public Int32 sex { get; set; }
                    public String province { get; set; }
                    public String city { get; set; }
                    public String country { get; set; }
                    public String headimgurl { get; set; }
                    public List<String> privilege { get; set; }
                    public String unionid { get; set; }
                }
            }
            public class JS_SDK
            {

                public static String appid { get; private set; }
                public static String secret { get; private set; }
                public static void init(String _appid, String _secret)
                {
                    appid = _appid;
                    secret = _secret;
                }
                static DateTime _access_token_ftime { get; set; }
                static String _access_token { get; set; }
                public static String access_token
                {
                    get
                    {
                        //K.Y.DLL.Tool.T_Log.LogMessage("access_token", new { _access_token = _access_token, _access_token_ftime = _access_token_ftime }.Ex_ToJson());
                        if (!String.IsNullOrEmpty(_access_token))
                            if ((DateTime.Now - _access_token_ftime).TotalSeconds <= 7000) return _access_token;

                        String URL = "https://api.weixin.qq.com/cgi-bin/token" +
                            "?grant_type=client_credential" +
                            "&appid=" + appid +
                            "&secret=" + secret;
                        var Stream = K.Y.DLL.Tool.T_Web.HttpGet(URL);
                        var E = Stream.Ex_ToEntity<WX_Back_ClientAccessCode>();
                        if (!string.IsNullOrEmpty(E.access_token)) _access_token = E.access_token;
                        _access_token_ftime = DateTime.Now;
                        return _access_token;
                    }
                }
                public class Back_Ticket
                {

                    public Int32 errcode { get; set; }
                    public String errmsg { get; set; }
                    public String ticket { get; set; }
                    public Int32 expires_in { get; set; }

                }
                static DateTime ftime = DateTime.Now;
                /// <summary>
                /// 生成签名之前必须先了解一下jsapi_ticket，jsapi_ticket是公众号用于调用微信JS接口的临时票据。正常情况下，jsapi_ticket的有效期为7200秒，
                /// </summary>
                /// <param name="access_token"></param>
                /// <param name="refresh_token"></param>
                /// <returns></returns>
                public static Back_Ticket getticket()
                {
                    var url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket" +
                        "?access_token=" + access_token +
                        "&type=jsapi";
                    var Stream = K.Y.DLL.Tool.T_Web.HttpGet(url, "");
                    var R = Stream.Ex_ToEntity<Back_Ticket>();
                    return R;
                }
                //获取signature
                public static string signature(string url, String noncestr, Int64 timestamp)
                {
                    if (DateTime.Now.AddSeconds(-7000) > ftime)
                    {
                        ftime = DateTime.Now;
                    }
                    var BackTicket = getticket();
                    string str = "jsapi_ticket=" + BackTicket.ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
                    byte[] cleanBytes = Encoding.Default.GetBytes(str);
                    byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
                    string sign = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    return sign;
                }


                public class WX_Back_ClientAccessCode : WX_Back
                {
                    public String access_token { get; set; }
                    public Int32 expires_in { get; set; }
                }
            }
        }
    }
}
