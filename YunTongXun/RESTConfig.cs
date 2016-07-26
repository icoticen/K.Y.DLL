using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace K.Y.DLL.YunTongXun
{
    enum EBodyType : uint
    {
        EType_XML = 0,
        EType_JSON = 1,
    }

    public class YunTongXunAPI
    {
        public String m_restAddress = "sandboxapp.cloopen.com";
        public String m_restPort = "8883";
        public String m_mainAccount = "aaf98f894ee35d30014ef26f93850f4f";
        public String m_mainToken = "1ba86bb431024381b7ba0aa13aabbabc";
        public String m_softVer = "2013-12-26";


        //public EBodyType m_bodyType = EBodyType.EType_JSON;

        public bool Init(String mainAccount, String mainToken, String restAddress, String restPort, String softVer)
        {
            this.m_restAddress = restAddress;
            this.m_restPort = restPort;
            this.m_mainAccount = mainAccount;
            this.m_mainToken = mainToken;
            this.m_softVer = softVer;
            return true;
        }

        /// <summary>
        /// 模板短信
        /// </summary>
        /// <param name="to">短信接收端手机号码集合，用英文逗号分开，每批发送的手机号数量不得超过100个</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="data">可选字段 内容数据，用于替换模板中{序号}</param>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public String Request(String funcName, String JsonStr)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyyMMddhhmmss");

                // 构建URL内容
                string sigstr = MD5Encrypt(m_mainAccount + m_mainToken + date);
                string uriStr = string.Format("https://{0}:{1}/{2}/Accounts/{3}/SMS/{4}?sig={5}", m_restAddress, m_restPort, m_softVer, m_mainAccount, funcName, sigstr);
                Uri address = new Uri(uriStr);



                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                setCertificateValidationCallBack();

                // 构建Head
                request.Method = "POST";

                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                byte[] myByte = myEncoding.GetBytes(m_mainAccount + ":" + date);
                string authStr = Convert.ToBase64String(myByte);
                request.Headers.Add("Authorization", authStr);


                // 构建Body
                request.Accept = "application/json";
                request.ContentType = "application/json;charset=utf-8";

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(JsonStr);



                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();


                    if (responseStr != null && responseStr.Length > 0) return responseStr;
                    return new { statusCode = 172002, statusMsg = "无返回", data = "" }.Ex_ToJson();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        #region MD5 和 https交互函数定义



        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">原内容</param>
        /// <returns>加密后内容</returns>
        public static string MD5Encrypt(string source)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /// <summary>
        /// 设置服务器证书验证回调
        /// </summary>
        public void setCertificateValidationCallBack()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateValidationResult;
        }

        /// <summary>
        ///  证书验证回调函数  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CertificateValidationResult(object obj, System.Security.Cryptography.X509Certificates.X509Certificate cer, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }
        #endregion
    }
}
