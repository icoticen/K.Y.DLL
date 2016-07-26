using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace K.Y.DLL.Pay
{
    public class ZFBPay
    {
        public enum ZFB_TradeStatus
        {
            TRADE_ERROR = 0,
            TRADE_CREAT = 1,
            WAIT_BUYER_PAY = 2,//from zfb
            TRADE_SUCCESS = 3,//from zfb
            TRADE_FAILURE = 4,
        }

        private static readonly string partner = "2088411297150842";

        public class CallBack
        {
            public static  Boolean CallBack_NotifyID_Validate(string notify_id)
            {
                string sendURL = string.Format("https://mapi.alipay.com/gateway.do?service=notify_verify&partner={0}&notify_id={1}", partner, notify_id);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendURL);
                HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
                StreamReader stream = new StreamReader(respone.GetResponseStream(), Encoding.Default);
                //获取结果 　　
                string resultStr = stream.ReadToEnd();
                var IsVality = resultStr.ToLower() == "true";
                return IsVality;
            }
            public static CallBack CallBack_Check(string trade_no, string subject, string out_trade_no, string notify_time, string total_fee, string notify_id, string trade_status)
            {
                var mZFBPay = new CallBack
                {
                    trade_no = trade_no,
                    subject = subject,
                    out_trade_no = out_trade_no,
                    notify_time = notify_time,
                    total_fee = total_fee,
                    notify_id = notify_id,
                    trade_status = trade_status,
                    IsVality = false,
                    TradeStatus = ZFB_TradeStatus.TRADE_ERROR
                };
                mZFBPay.IsVality = CallBack_NotifyID_Validate(mZFBPay.notify_id);
                if (mZFBPay.IsVality)
                {
                    var TR = ZFB_TradeStatus.TRADE_FAILURE;
                    Enum.TryParse(mZFBPay.trade_status, out TR);
                    mZFBPay.TradeStatus = TR;
                }
                return mZFBPay;
            }

            public string trade_no;
            public string subject;
            public string out_trade_no;
            public string notify_time;
            public string total_fee;
            public string notify_id;
            public string trade_status;
            public Boolean IsVality = false;
            public ZFB_TradeStatus TradeStatus;
        }
    }
}
