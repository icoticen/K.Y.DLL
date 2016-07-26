using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL
{
    public class Class1
    {

        enum T { 
        
        系统错误,

            数据库操作失败,

            数据库对象不存在,
            数据库对象已存在,
            数据库对象超出个数限制,
            数据库对象不足个数限制,


            数据库对象所属无权操作,
            数据库对象锁定无权操作,
            数据库对象状态无权操作,
            数据库对象冲突无权操作,

            
            参数不能为空,
            





        操作成功
        
        
        
        
        
        
        }


        void G(Int32 MaxCount,Int32 _pre)
        {
            var Str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Func<Int32, string> F = i =>
            {
                var L = Str[(int)(i / 100)];
                var R = i % 100;
                return L.ToString() + R;
            };


        }

    }
}
