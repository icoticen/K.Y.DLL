using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace K.Y.DLL.Tool
{
    public class T_Regular
    {
        public static String RegPhone = @"^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$";
        //public static String RegMail = @"^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$";
        //public static String RegMail = @"^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$";
        public static Boolean IsPhoneNum(String PhoneNum)
        {
            return Regex.IsMatch(PhoneNum ?? "", RegPhone);
        }
    }
}
