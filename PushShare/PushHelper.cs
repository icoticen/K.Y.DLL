using PushSharp;
using PushSharp.Core;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using K.Y.DLL;
using K.Y.DLL.Tool;

namespace K.Y.DLL.PushShare
{
    public static class PushHelper
    {
        private static Object Lock_iPhone = new object();
        private static PushBroker push;
        public static String PushiPhone(String appleCertFilePath, String appleCertPrivateKey, List<PushModel> iList)
        {
            try
            {
                lock (Lock_iPhone)
                {
                    //创建一个推送对象
                    push = new PushBroker();
                    //关联推送状态事件
                    push.OnNotificationSent += NotificationSent;
                    push.OnChannelException += ChannelException;
                    push.OnServiceException += ServiceException;
                    push.OnNotificationFailed += NotificationFailed;
                    push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
                    push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
                    push.OnChannelCreated += ChannelCreated;
                    push.OnChannelDestroyed += ChannelDestroyed;

                    var appleCert = File.ReadAllBytes(appleCertFilePath);//将证书流式读取
                    push.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, appleCertPrivateKey));//注册推送通道
                    foreach (var P in iList)
                    {
                        foreach (var T in P.Token)
                        {
                            push.QueueNotification(new AppleNotification()
                                .ForDeviceToken(T)//手机token
                                .WithAlert(P.PushContent)//推送消息内容
                                .WithSound("default")//提示声音
                                .WithBadge(1)//设备图标显示的未读数(图标右上角的小标志)
                               .WithCustomItem("url", P.PushLink)//用户自定义额外参数
                                );
                        }
                    }
                    //push.StopAllServices();//停止推送服务.
                }
                return iList.Ex_ToJson();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return ex.ToString();
            }
        }

        static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            //Console.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
        }
        // 推送成功
        static void NotificationSent(object sender, INotification notification)
        {
            LogRun("NotificationSent: " + sender + " -> " + notification);
        }
        // 推送失败
        static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            LogRun("NotificationFailed: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
        }

        static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            LogRun("ChannelException: " + sender + " -> " + channel + "  -> " + exception.Message);
        }

        static void ServiceException(object sender, Exception exception)
        {
            LogRun("ServiceException: " + sender + " -> " + exception);
        }

        static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            LogRun("DeviceSubscriptionExpired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        static void ChannelDestroyed(object sender)
        {
            LogRun("ChannelDestroyed: " + sender);
        }

        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            LogRun("ChannelCreated: " + sender);
        }

        static void LogRun(String Msg)
        {
            T_Log.Log("Run/Push", Msg);
        }
        static void LogError(Exception ex)
        {
            T_Log.Log("Error/Push", ex.ToString());
        }
    }
    public class PushModel
    {
        public Int32 PushID { get; set; }
        public String PushTitle { get; set; }
        public DateTime PushTime { get; set; }
        public String PushContent { get; set; }
        public String PushImage { get; set; }
        public String PushLink { get; set; }
        public List<String> Token { get; set; }
    }
}
