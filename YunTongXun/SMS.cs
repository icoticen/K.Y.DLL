using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.YunTongXun
{
    public class SMS
    {
        private static YunTongXunAPI API = new YunTongXunAPI();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_appId"></param>
        /// <param name="_to"></param>
        /// <param name="_templateId"></param>
        /// <param name="_datas">【游达网络聚宝应用】您{1}的验证码是{2},请于{3}分钟内正确输入</param>
        /// <returns></returns>
        public static SMS_TemplateSMS_Receive SMS_TemplateSMS(String _appId, String _to, String _templateId, String[] _datas)
        {
            SMS_TemplateSMS_Request request = new SMS_TemplateSMS_Request(_appId, _to, _templateId, _datas);
            return new SMS_TemplateSMS_Receive(API.Request("TemplateSMS", request.ToJson()));
        }

    }
    public class SMS_TemplateSMS_Request
    {
        public String to { get; set; }
        public String appId { get; set; }
        public String templateId { get; set; }
        public String[] datas { get; set; }
        public String ToJson() { return this.Ex_ToJson(); }
        public SMS_TemplateSMS_Request(String _appId, String _to, String _templateId, String[] _datas)
        {
            to = _to;
            appId = _appId;
            templateId = _templateId;
            datas = _datas;
        }
    }
    public class SMS_TemplateSMS_Receive
    {
        public String statusCode { get; set; }//String	必选	请求状态码，取值000000（成功），可参考Rest 错误代码。
        public String statusMsg { get; set; }
        public SMS_TemplateSMS_Receive_SMS templateSMS { get; set; }
        public SMS_TemplateSMS_Receive(String JsonStr)
        {
            var x = JsonStr.Ex_ToEntity<SMS_TemplateSMS_Receive>();// YCCom.Tool.T_Prase.ToEntity<SMS_TemplateSMS_Receive>();
            statusCode = x.statusCode;
            statusMsg = x.statusMsg;
            templateSMS = x.templateSMS;
        }
        public SMS_TemplateSMS_Receive()
        {
            templateSMS = new SMS_TemplateSMS_Receive_SMS();
        }
    }
    public class SMS_TemplateSMS_Receive_SMS
    {
        public String smsMessageSid { get; set; }//String	必选	短信唯一标识符
        public String dateCreated { get; set; }//
    }
}
