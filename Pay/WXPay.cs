using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace K.Y.DLL.Pay
{
    public class WXPay
    {
        public enum WX_TradeStatus
        {
            TRADE_ERROR = 0,
            TRADE_CREAT = 1,
            //WAIT_BUYER_PAY = 2,//from zfb
            TRADE_SUCCESS = 3,//from wx  
            TRADE_FAILURE = 4,
        }
        public class CallBack
        {
            public WX_TradeStatus TradeStatus;
            public String ReturnStr;
            public String return_code { get; set; }
            public String return_msg { get; set; }
            public String appid { get; set; }
            public String mch_id { get; set; }
            public String device_info { get; set; }//
            public String nonce_str { get; set; }
            public String sign { get; set; }
            public String result_code { get; set; }
            public String err_code { get; set; }//
            public String err_code_des { get; set; }//
            public String openid { get; set; }
            public String is_subscribe { get; set; }//
            public String trade_type { get; set; }
            public String bank_type { get; set; }
            public Decimal total_fee { get; set; }
            public String fee_type { get; set; }//
            public Decimal cash_fee { get; set; }
            public String cash_fee_type { get; set; }//
            public Decimal coupon_fee { get; set; }//
            public Int32 coupon_count { get; set; }//
            //public String coupon_id_$n { get; set; }//
            //public String coupon_fee_$n { get; set; }//
            public String transaction_id { get; set; }
            public String out_trade_no { get; set; }
            public String attach { get; set; }//
            public String time_end { get; set; }//
            public static CallBack WxPayReturnXML_ToEntity()
            {
                try
                {
                    string XmlStr = "";
                    if (System.Web.HttpContext.Current.Request.InputStream != null)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(System.Web.HttpContext.Current.Request.InputStream);
                        XmlStr = sr.ReadToEnd();
                        sr.Close();
                    }
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XmlStr);
                    var mWxPay = new CallBack();
                    mWxPay.ReturnStr = "";
                    //类型
                    mWxPay.return_code = xml.SelectSingleNode("xml/return_code").ChildNodes[0].Value;
                    if (mWxPay.return_code.ToUpper() == "SUCCESS")
                    {
                        mWxPay.TradeStatus = WX_TradeStatus.TRADE_SUCCESS;
                        mWxPay.ReturnStr = "<xml> <return_code><![CDATA[SUCCESS]]></return_code> <return_msg> <![CDATA[OK]]></return_msg></xml> ";
                        mWxPay.appid = xml.SelectSingleNode("xml/appid").ChildNodes[0].Value;
                        mWxPay.mch_id = xml.SelectSingleNode("xml/mch_id").ChildNodes[0].Value;
                        //mWxPay.//device_info=xml.SelectSingleNode("xml///device_info").ChildNodes[0].Value;
                        mWxPay.nonce_str = xml.SelectSingleNode("xml/nonce_str").ChildNodes[0].Value;
                        mWxPay.sign = xml.SelectSingleNode("xml/sign").ChildNodes[0].Value;
                        mWxPay.result_code = xml.SelectSingleNode("xml/result_code").ChildNodes[0].Value;
                        //mWxPay.//err_code=xml.SelectSingleNode("xml///err_code").ChildNodes[0].Value;
                        //mWxPay.//err_code_des=xml.SelectSingleNode("xml///err_code_des").ChildNodes[0].Value;
                        mWxPay.openid = xml.SelectSingleNode("xml/openid").ChildNodes[0].Value;
                        //mWxPay.//is_subscribe=xml.SelectSingleNode("xml///is_subscribe").ChildNodes[0].Value;
                        mWxPay.trade_type = xml.SelectSingleNode("xml/trade_type").ChildNodes[0].Value;
                        mWxPay.bank_type = xml.SelectSingleNode("xml/bank_type").ChildNodes[0].Value;
                        mWxPay.total_fee = (xml.SelectSingleNode("xml/total_fee").ChildNodes[0].Value.Ex_ToDecimal()/ 100);
                        //mWxPay.//fee_type=xml.SelectSingleNode("xml///fee_type").ChildNodes[0].Value;
                        mWxPay.cash_fee = (xml.SelectSingleNode("xml/cash_fee").ChildNodes[0].Value.Ex_ToDecimal()/ 100);
                        //mWxPay.//cash_fee_type=xml.SelectSingleNode("xml///cash_fee_type").ChildNodes[0].Value;
                        //mWxPay.//coupon_fee=xml.SelectSingleNode("xml///coupon_fee").ChildNodes[0].Value;
                        //mWxPay.//coupon_count=xml.SelectSingleNode("xml///coupon_count").ChildNodes[0].Value;
                        mWxPay.transaction_id = xml.SelectSingleNode("xml/transaction_id").ChildNodes[0].Value;
                        mWxPay.out_trade_no = xml.SelectSingleNode("xml/out_trade_no").ChildNodes[0].Value;
                        //mWxPay.//attach=xml.SelectSingleNode("xml///attach").ChildNodes[0].Value;
                        mWxPay.time_end = xml.SelectSingleNode("xml/time_end").ChildNodes[0].Value;
                    }
                    else
                    {
                        mWxPay.TradeStatus = WX_TradeStatus.TRADE_FAILURE;
                        mWxPay.ReturnStr = "fail";
                        mWxPay.return_msg = xml.SelectSingleNode("xml/return_code").ChildNodes[0].Value;
                    }
                    return mWxPay;
                }
                catch
                {
                    return new CallBack()
                    {
                        TradeStatus = WX_TradeStatus.TRADE_ERROR,
                        return_code = "fail",
                        return_msg = "XML解析错误",
                    };
                }
            }
            //<xml>
            //  <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
            //  <attach><![CDATA[支付测试]]></attach>
            //  <bank_type><![CDATA[CFT]]></bank_type>
            //  <fee_type><![CDATA[CNY]]></fee_type>
            //  <is_subscribe><![CDATA[Y]]></is_subscribe>
            //  <mch_id><![CDATA[10000100]]></mch_id>
            //  <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
            //  <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
            //  <out_trade_no><![CDATA[1409811653]]></out_trade_no>
            //  <result_code><![CDATA[SUCCESS]]></result_code>
            //  <return_code><![CDATA[SUCCESS]]></return_code>
            //  <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
            //  <sub_mch_id><![CDATA[10000100]]></sub_mch_id>
            //  <time_end><![CDATA[20140903131540]]></time_end>
            //  <total_fee>1</total_fee>
            //  <trade_type><![CDATA[JSAPI]]></trade_type>
            //  <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
            //</xml>
        }
    }
}
