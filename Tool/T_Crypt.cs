using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace K.Y.DLL.Tool
{
    public class T_Crypt
    {
        #region MD5
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content">加密文本</param>
        /// <returns></returns>
        public static string MD5_String(string content)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.ASCII.GetBytes(content));//
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static string MD5_File(string pathName)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;

            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                oFileStream = new System.IO.FileStream(pathName.Replace("\"", ""), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值

                oFileStream.Close();

                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”

                strHashData = System.BitConverter.ToString(arrbytHashValue);

                //替换-
                strHashData = strHashData.Replace("-", "");

                strResult = strHashData;
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strResult;
        }
        #endregion

        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string Guid16()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long Guid19()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        //该代码片段来自于: http://www.sharejs.com/codes/csharp/7040
        /// <summary>  
        /// 生成22位唯一的数字 并发可用  
        /// </summary>  
        /// <returns></returns>  
        public static string Guid22()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
            return strUnique;
        }

        #region SHA1
        public static String SHA1_EnCrypt_String(String content)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[]
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(content);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }
        #endregion

        #region RSA
        private static String publickey = @"<RSAKeyValue><Modulus>jJXbSfVMWO51qP4euakac0sdtzsi3p3XJiqZHmg4QuOSK2mX9FWoCHa39hchyZI1h2it1ANq5ZE2KDzPCdF5DeHKAKZnBRTM5sSNvvcNtRgiUZB15MJJZO39GDSiCpwDS9xH9Dugnhh+PasKlx6p/k7LpTn4zBfOJnLSL6ZQRf0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static String privatekey = @"<RSAKeyValue><Modulus>jJXbSfVMWO51qP4euakac0sdtzsi3p3XJiqZHmg4QuOSK2mX9FWoCHa39hchyZI1h2it1ANq5ZE2KDzPCdF5DeHKAKZnBRTM5sSNvvcNtRgiUZB15MJJZO39GDSiCpwDS9xH9Dugnhh+PasKlx6p/k7LpTn4zBfOJnLSL6ZQRf0=</Modulus><Exponent>AQAB</Exponent><P>xdH7+HgYbwphSeeXdRNQTCINq5kptTqquxRjmYpPamVJIZ5dQVX5jdz8WXLH+DVSZJ60ZfeiOC9aNXMhKfNQZw==</P><Q>te6fEZyEhlN08GNu1lQUAzAWXSZSRM+AGfmBIGxL1rxoof4fPDVhq4ek6Xq5JGQt1zGkIdVZ1m6g+MTQ2QFn+w==</Q><DP>EQlAdemB0S5HqqGzPXXoWGYmXzzVhrICuhHLchGjPTpzzd1hkprg3wLFCL8F0a5l5hx01MM6yTPqxOehV4eIyQ==</DP><DQ>LV/DCmhn4PyFiMKzzP6RMy5WFYtOL101DMVegBCiZX799ZDkh2ak4lvlNFnoPPxDNo1p6wpD6qgSu5iSodyo6w==</DQ><InverseQ>aa+ngdK9GJh8jEDdoTMc7pv9SFZD1MId0fXY2nXjW1+G2RKTwbw3POQ1pmdXqJbA57xyKLY/Z2GkB8NDydpZOg==</InverseQ><D>NXKz0EZmJFlkej9Cxys3VyXzwjnFZAV2SphfZmQRH70NUVvv3YDDRZR9FB5vRgdEOprdm4FBHs46XMnhnMX602/nmq331oY833jxLOx1arkuDGFQRD39a8UXC+dTTh6rLr66Uef+hVH0lrYueLOIOaZVzC7g4VLc8VY/nbeBkzE=</D></RSAKeyValue>";
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey">公钥</param>
        /// <param name="content">加密文本</param>
        /// <returns></returns>
        public static string RSAEncrypt(string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes).Replace("+", "%2B");
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey">私钥</param>
        /// <param name="content">加密文本</param>
        /// <returns></returns>
        public static string RSADecrypt(string content)
        {
            content = content.Replace(" ", "+");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        //public static void RSA_XML_To_PEM(String XMLPath, String PEMPath)
        //{
        //    var rsa = new RSACryptoServiceProvider();
        //    using (var sr = new StreamReader(XMLPath))
        //    {
        //        rsa.FromXmlString(sr.ReadToEnd());
        //    }
        //    var p = rsa.ExportParameters(true);

        //    var key = new RsaPrivateCrtKeyParameters(
        //        new BigInteger(1, p.Modulus), new BigInteger(1, p.Exponent), new BigInteger(1, p.D),
        //        new BigInteger(1, p.P), new BigInteger(1, p.Q), new BigInteger(1, p.DP), new BigInteger(1, p.DQ),
        //        new BigInteger(1, p.InverseQ));

        //    using (var sw = new StreamWriter(PEMPath))
        //    {
        //        var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(sw);
        //        pemWriter.WriteObject(key);
        //    }
        //}
        //public static void RSA_PEM_To_XML(String XMLPath, String PEMPath)
        //{
        //    AsymmetricCipherKeyPair keyPair;
        //    using (var sr = new StreamReader("e:\\key.pem"))
        //    {
        //        var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sr);
        //        keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
        //    }
        //    var key = (RsaPrivateCrtKeyParameters)keyPair.Private;
        //    var p = new RSAParameters
        //    {
        //        Modulus = key.Modulus.ToByteArrayUnsigned(),
        //        Exponent = key.PublicExponent.ToByteArrayUnsigned(),
        //        D = key.Exponent.ToByteArrayUnsigned(),
        //        P = key.P.ToByteArrayUnsigned(),
        //        Q = key.Q.ToByteArrayUnsigned(),
        //        DP = key.DP.ToByteArrayUnsigned(),
        //        DQ = key.DQ.ToByteArrayUnsigned(),
        //        InverseQ = key.QInv.ToByteArrayUnsigned(),
        //    };
        //    var rsa = new RSACryptoServiceProvider();
        //    rsa.ImportParameters(p);
        //    using (var sw = new StreamWriter("e:\\key.xml"))
        //    {
        //        sw.Write(rsa.ToXmlString(true));
        //    }
        //}

        public static void ExportPublicKeyToPEMFormat(String XMLPath, String PEMPath)
        {
            FileStream fs = new FileStream(PEMPath, FileMode.Create);
            StreamWriter outputStream = new StreamWriter(fs);
            //TextWriter outputStream = new StringWriter();
            var csp = new RSACryptoServiceProvider();
            using (var sr = new StreamReader(XMLPath))
            {
                csp.FromXmlString(sr.ReadToEnd());
            }
            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);

                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");

                outputStream.Close();
                fs.Close();
            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }
        #endregion

        #region K_
        public static String K_EnCrypt_SimpleDate()
        {
            return K_EnCrypt_SimpleDate(DateTime.Now);
        }
        public static String K_EnCrypt_SimpleDate(DateTime dt)
        {
            String BaseStr = "qa7zXSWedcVF2Rtgb0NHY6ujmKIo3lPQwEr84TyUiOpAsDf5GhJ9kLZxCvBn1M";
            return K_EnCrypt_SimpleDate(BaseStr, dt);
        }
        public static String K_EnCrypt_SimpleDate(String BaseStr, DateTime dt)
        {
            if (String.IsNullOrEmpty(BaseStr)) return "";
            return BaseStr[dt.Year % BaseStr.Length] + "" +
                BaseStr[dt.Month % BaseStr.Length] + "" +
                BaseStr[dt.Day % BaseStr.Length] + "" +
                BaseStr[dt.Hour % BaseStr.Length] + "" +
                BaseStr[dt.Minute % BaseStr.Length] + "" +
                BaseStr[dt.Second % BaseStr.Length] + "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseStr">字符库 包含支持的所有字符</param>
        /// <param name="SourceStr">要加密的字符串</param>
        /// <param name="KeyStr">秘钥</param>
        /// <param name="RadomIndex">随机数 默认为0</param>
        /// <returns></returns>
        ///      
        //蜜汁加密  UserID=>Token
        public static String K_EnCrypt_Token(String S, String Key, Int32 I)
        {
            //return K_EnCrypt(K_EnCrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + "OhhShitYouActuallyCrackedMyPassword", Key, I) + "FFFLLJCDFSOXSMW" + S, Key, I);
            String _ = K_EnCrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + "OhhShitYouActuallyCrackedMyPassword", Key, I);
            String __ = _ + "FFFLLJCDFSOXSMW" + S;
            String ___ = K_EnCrypt(__, Key, I);
            return ___;
        }
        public static String K_EnCrypt_Token(String B, String S, String Key, Int32 I)
        {
            //return K_EnCrypt(K_EnCrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + "OhhShitYouActuallyCrackedMyPassword", Key, I) + "FFFLLJCDFSOXSMW" + S, Key, I);
            String _ = K_EnCrypt(B, DateTime.Now.ToString("yyyyMMddHHmmss") + "OhhShitYouActuallyCrackedMyPassword", Key, I);
            String __ = _ + "FFFLLJCDFSOXSMW" + S;
            String ___ = K_EnCrypt(B, __, Key, I);
            return ___;
        }
        //蜜汁解密  Token=>UserID
        public static String K_DeCrypt_Token(String S, String Key, Int32 I)
        {
            try
            {

                //return T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries).Length>=2? T_Crypt.K_DeCrypt(T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[0] ?? "", Key, I).EndsWith("OhhShitYouActuallyCrackedMyPassword") ? T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[1] ?? "" : "":"";
                var __ = T_Crypt.K_DeCrypt(S, Key, I);
                var _o_ = __.Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries);
                if (_o_.Length >= 2)
                {
                    var _o = _o_[0] ?? "";
                    var o_ = _o_[1] ?? "";

                    var _ = T_Crypt.K_DeCrypt(_o, Key, I);
                    if (_.EndsWith("OhhShitYouActuallyCrackedMyPassword")) return o_;
                }
            }
            catch { }
            return "";
        }
        public static String K_DeCrypt_Token(String S, String Key, Int32 I, DateTime Expiration)
        {
            try
            {

                //return T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries).Length>=2? T_Crypt.K_DeCrypt(T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[0] ?? "", Key, I).EndsWith("OhhShitYouActuallyCrackedMyPassword") ? T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[1] ?? "" : "":"";
                var __ = T_Crypt.K_DeCrypt(S, Key, I);
                var _o_ = __.Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries);
                if (_o_.Length >= 2)
                {
                    var _o = _o_[0] ?? "";
                    var o_ = _o_[1] ?? "";

                    var _ = T_Crypt.K_DeCrypt(_o, Key, I);
                    if (_.EndsWith("OhhShitYouActuallyCrackedMyPassword"))
                    {
                        var dtStr = _.Replace("OhhShitYouActuallyCrackedMyPassword", "");
                        var dt = dtStr.Ex_ToDateTime("yyyyMMddHHmmss", DateTime.Now.AddDays(1));
                        if (dt > Expiration) return "";
                        return o_;
                    }
                }
            }
            catch { }
            return "";
        }
        public static String K_DeCrypt_Token(String B, String S, String Key, Int32 I)
        {
            try
            {

                //return T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries).Length>=2? T_Crypt.K_DeCrypt(T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[0] ?? "", Key, I).EndsWith("OhhShitYouActuallyCrackedMyPassword") ? T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[1] ?? "" : "":"";
                var __ = T_Crypt.K_DeCrypt(B, S, Key, I);
                var _o_ = __.Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries);
                if (_o_.Length >= 2)
                {
                    var _o = _o_[0] ?? "";
                    var o_ = _o_[1] ?? "";

                    var _ = T_Crypt.K_DeCrypt(B, _o, Key, I);
                    if (_.EndsWith("OhhShitYouActuallyCrackedMyPassword")) return o_;
                }
            }
            catch { }
            return "";
        }
        public static String K_DeCrypt_Token(String B, String S, String Key, Int32 I, DateTime Expiration)
        {
            try
            {

                //return T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries).Length>=2? T_Crypt.K_DeCrypt(T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[0] ?? "", Key, I).EndsWith("OhhShitYouActuallyCrackedMyPassword") ? T_Crypt.K_DeCrypt(S, Key, I).Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries)[1] ?? "" : "":"";
                var __ = T_Crypt.K_DeCrypt(B, S, Key, I);
                var _o_ = __.Split(new[] { "FFFLLJCDFSOXSMW", }, StringSplitOptions.RemoveEmptyEntries);
                if (_o_.Length >= 2)
                {
                    var _o = _o_[0] ?? "";
                    var o_ = _o_[1] ?? "";

                    var _ = T_Crypt.K_DeCrypt(B, _o, Key, I);
                    if (_.EndsWith("OhhShitYouActuallyCrackedMyPassword"))
                    {
                        var dtStr = _.Replace("OhhShitYouActuallyCrackedMyPassword", "");
                        var dt = dtStr.Ex_ToDateTime("yyyyMMddHHmmss", DateTime.Now.AddDays(1));
                        if (dt > Expiration) return "";
                        return o_;
                    }
                }
            }
            catch { }
            return "";
        }



        public static String K_EnCrypt(String SourceStr, String KeyStr, Int32 RadomIndex)
        {
            if (String.IsNullOrEmpty(SourceStr) || String.IsNullOrEmpty(KeyStr) || RadomIndex < 0) return "";
            String BaseStr = "qa7zXSWedcVF2Rtgb0NHY6ujmKIo3lPQwEr84TyUiOpAsDf5GhJ9kLZxCvBn1M";
            return K_EnCrypt(BaseStr, SourceStr, KeyStr, RadomIndex);
        }
        public static String K_EnCrypt(String BaseStr, String SourceStr, String KeyStr, Int32 RadomIndex)
        {
            if (String.IsNullOrEmpty(BaseStr) || String.IsNullOrEmpty(SourceStr) || String.IsNullOrEmpty(KeyStr) || RadomIndex < 0) return "";
            var ResultStr = "";
            var Index = RadomIndex;

            for (Int32 i = 0; i < SourceStr.Length; i++)
            {
                var c = SourceStr[i];//待加密的Char
                var k = KeyStr[i % KeyStr.Length];//秘钥
                var r = BaseStr[(BaseStr.IndexOf(c) + BaseStr.IndexOf(k) + Index) % BaseStr.Length];
                Index = BaseStr.IndexOf(r);
                ResultStr += r;
            }
            return ResultStr;
        }
        public static String K_DeCrypt(String SourceStr, String KeyStr, Int32 RadomIndex)
        {
            String BaseStr = "qa7zXSWedcVF2Rtgb0NHY6ujmKIo3lPQwEr84TyUiOpAsDf5GhJ9kLZxCvBn1M";
            return K_DeCrypt(BaseStr, SourceStr, KeyStr, RadomIndex);
        }
        public static String K_DeCrypt(String BaseStr, String SourceStr, String KeyStr, Int32 RadomIndex)
        {
            if (String.IsNullOrEmpty(BaseStr) || String.IsNullOrEmpty(SourceStr) || String.IsNullOrEmpty(KeyStr) || RadomIndex < 0) return "";
            var ResultStr = "";
            var Index = RadomIndex;

            for (Int32 i = 0; i < SourceStr.Length; i++)
            {
                var c = SourceStr[i];//待解密的Char
                var k = KeyStr[i % KeyStr.Length];//秘钥
                var r = BaseStr[(BaseStr.IndexOf(c) + BaseStr.Length + BaseStr.Length - BaseStr.IndexOf(k) - Index % BaseStr.Length) % BaseStr.Length];
                Index = BaseStr.IndexOf(c);
                ResultStr += r;
            }
            return ResultStr;
        }
        #endregion
    }
}
