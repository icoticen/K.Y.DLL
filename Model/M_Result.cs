using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Model
{
    public class M_Result
    {
        public Int32 result { get; set; }
        public String msg { get; set; }
        public dynamic data { get; set; }
        public M_Result()
        {
            result = 0;
            msg = "";
            data = new List<Object>();
        }
        public M_Result(Int32 i)
        {
            result = i;
            msg = ((ResultCode)i).ToString();
            data = null;
        }
        public M_Result(Int32 i, dynamic d)
        {
            result = i;
            msg = ((ResultCode)i).ToString();
            data = d;
        }

        public M_Result(Int32 i, String m, dynamic d)
        {
            result = i;
            msg = m;
            data = d;
        }
        public String ToJson()
        {
            return this.Ex_ToJson();
        }


        public static M_Result NewJson(Int32 i)
        {
            var json = new M_Result(i);
            return json;
        }
        public static M_Result NewJson(Int32 i, dynamic d) 
        {
            var json = new M_Result(i, d);
            return json;
        }
        public static M_Result NewJson(Int32 i, String msg, dynamic d)
        {
            var json = new M_Result(i, msg, d);
            return json;
        }
        /// <summary>
        /// { data=[{},{}] }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        public static M_Result NewJson<T, V>(List<T> List, Func<T, V> F)
        {
            if (List == null) return new M_Result(-1, "服务器错误~", null);
            if (List.Count == 0) return new M_Result(2, "没有更多数据了~", null);
            if (F != null) return new M_Result(1, "操作成功~", List.Select(p => F(p)));
            return new M_Result(1, "操作成功~", List);
        }
        
        //public static M_Result NewJson<T>(List<T> List, T Model, M_Pagination Page, Func<T, dynamic> F)
        //{
        //    if (List == null) return new M_Result(-1, "服务器错误~", null);
        //    if (List.Count == 0) return new M_Result(2, "没有更多数据了~", null);
        //    return new M_Result(1, "操作成功~", new
        //    {
        //        Page = Page,
        //        Model = Model,
        //        Data = List.Select(p => F(p))
        //    });
        //}
        ///
        //public static M_Result NewJson<T>(dynamic List, T Model, M_Pagination Page)
        //{
        //    if (List == null) return new M_Result(-1, "服务器错误~", null);
        //    if (List.Count == 0) return new M_Result(2, "没有更多数据了~", null);
        //    return new M_Result(1, "操作成功~", new
        //    {
        //        Page = Page,
        //        Model = Model,
        //        Data = List
        //    });
        //}
    }

    public enum ResultCode
    {
        //1 Action

        //基本操作  1-19
        _____数据库操作失败 = -1,
        _000_参数错误 = 0,
        _001_操作成功 = 1,
        _002_操作成功但数据为空 = 0,
        _003_操作成功但数据不唯一 = 0,

        //字符串规则验证20-49
        _0_手机格式验证失败 = 0,

        //数据库错误100-149
        _114_数据库操作失败 = 114,
        _140_对象不存在 = 140,
        _152_对象不唯一 = 152,


        //登陆注册250-274
        _0_用户名已注册 = 520,
        _0_用户名不存在 = 506,
        _0_用户密码错误 = 504,
        _0_用户名重复 = 502,
        _0_密码不能相同 = 533,
        //用户权限275-299

        _0_用户禁言 = 0,
        _0_用户封禁 = 0,
        _0_Mac登陆权限限制 = 0,

        //
        //SMS短信验证300-309
        _0_验证码过于频繁 = 302,
        _0_验证码过期失效 = 304,
        _0_验证码已验证 = 305,
        _0_验证码错误 = 306,


    }

}
