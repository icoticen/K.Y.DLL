using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Tool
{
    public static class T_Prase
    {
        public static List<System.Web.Mvc.SelectListItem> GetSelectListItemFromEnum<T, U>()
            where T : struct
            where U : struct
        {
            var iList = new List<System.Web.Mvc.SelectListItem>();

            if (typeof(T).IsEnum)
                foreach (var Item in Enum.GetValues(typeof(T)))
                {
                    //Text = ((Config.JuHe_ToolAction)Item).ToString(), Value = Item + ""
                    var Text = Item.ToString();
                    var Value = (U)Item + "";
                    iList.Add(new System.Web.Mvc.SelectListItem
                    {
                        Text = Text,
                        Value = Value,
                    });
                }
            return iList;
        }
        public static List<System.Web.Mvc.SelectListItem> GetSelectListItemFromEnum<T>()
            where T : struct
        {
            var iList = new List<System.Web.Mvc.SelectListItem>();
            if (typeof(T).IsEnum)
                foreach (var Item in Enum.GetValues(typeof(T)))
                {
                    //Text = ((Config.JuHe_ToolAction)Item).ToString(), Value = Item + ""
                    var Text = Item.ToString();
                    var Value = (Int32)Item + "";
                    iList.Add(new System.Web.Mvc.SelectListItem{
                    Text=Text,
                    Value=Value,
                    });
                }
            return iList;
        }
    }
}
