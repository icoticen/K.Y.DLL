using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.API
{
     interface ITencent
    {
    }
     interface IWeiXin
    {
    }
     interface IWebAPP
    {
          String qrconnect(String appid, String redirect_uri, String state);
          IWX_Back_Access access_token(String appid, String secret, String code);
          IWX_Back_Access refresh_token(String appid, String refresh_token);
        //检验授权凭证（access_token）是否有效
          IWX_Back_Access auth(String access_token, String openid);
          IWX_Back_UserInfo userinfo(String access_token, String openid);
    }
     interface IWX_Back
    {
        //-----------错误样例
         Int32 errcode { get; set; }
         String errmsg { get; set; }
    }
     interface IWX_Back_Access : IWX_Back
    {
        //-----------正确返回
         String accsee_token { get; set; }
         Int32 expires_in { get; set; }
         String refresh_token { get; set; }
         String openid { get; set; }
         String scope { get; set; }
         String unionid { get; set; }
    }
     interface IWX_Back_UserInfo : IWX_Back
    {
         String openid { get; set; }
         String nickname { get; set; }
         Int32 sex { get; set; }
         String province { get; set; }
         String city { get; set; }
         String country { get; set; }
         String headimgurl { get; set; }
         List<String> privilege { get; set; }
         String unionid { get; set; }
    }
}
