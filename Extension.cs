using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

namespace K.Y.DLL
{
    public static class Extension
    {
        #region Base

        public static String Ex_ToString(this DateTime T, String Format = "yyyy-MM-dd HH:mm:ss")
        {
            return T.ToString(Format);
        }
        //public static String Ex_ToString(this DateTime? T)
        //{
        //    return T.Ex_ToString("yyyy-MM-dd HH:mm:ss");
        //}
        //public static String Ex_ToString(this DateTime? T, String Format)
        //{
        //    return T.Ex_ToString(,);
        //}
        public static String Ex_ToString(this DateTime? T, String Format = "yyyy-MM-dd HH:mm:ss", String Default = "")
        {
            if (T == null) return Default;
            return T.HasValue ? T.Value.ToString(String.IsNullOrEmpty(Format) ? "yyyy-MM-dd HH:mm:ss" : Format) : Default;
        }

        public static Decimal Ex_ToDecimal(this Object O)
        {
            if (O == null) return 0;
            var R = 0m;
            Decimal.TryParse(O.ToString(), out R);
            return R;
        }
        //public static Decimal Ex_ToDecimal(this Decimal T, Int32 Decimals)
        //{
        //    return Math.Round(T, Decimals);
        //}
        //public static Decimal Ex_ToDecimal(this Decimal? T)
        //{
        //    return T.Ex_ToDecimal(2);
        //}
        //public static Decimal Ex_ToDecimal(this Decimal? T, Int32 Decimals)
        //{
        //    return T.Ex_ToDecimal(Decimals, 0m);
        //}
        public static Decimal Ex_ToDecimal(this Decimal? T, Int32 Decimals = 2, Decimal Default = 0m)
        {
            if (T == null) return Default;
            return Math.Round(T.Value, Decimals);
        }

        //public static Double Ex_ToDouble(this Object O)
        //{
        //    if (O == null) return 0;
        //    var R = 0d;
        //    Double.TryParse(O.ToString(), out R);
        //    return R;
        //}
        //public static Double Ex_ToDouble(this Double? T)
        //{
        //    return T.Ex_ToDouble(2);
        //}
        //public static Double Ex_ToDouble(this Double? T, Int32 Decimals)
        //{
        //    return T.Ex_ToDouble(Decimals, 0);
        //}
        public static Double Ex_ToDouble(this String S, Int32 Decimals = 2, Double Default = 0.00d)
        {
            if (String.IsNullOrWhiteSpace(S)) return Default;
            var R = 0.00d;
            if (Double.TryParse(S, out R)) return R;
            return Default;
        }
        public static Double Ex_ToDouble(this Double? T, Int32 Decimals = 2, Double Default = 0.00d)
        {
            if (T == null) return Default;
            return Math.Round(T.Value, Decimals);
        }

        public static DateTime Ex_ToDateTime(this String S)
        {
            return S.Ex_ToDateTime(System.Data.SqlTypes.SqlDateTime.MinValue.Value);
        }
        public static DateTime Ex_ToDateTime(this String S, DateTime _Default)
        {
            DateTime R = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            if (DateTime.TryParse(S, out R)) return R;
            return _Default;
        }
        public static DateTime Ex_ToDateTime(this String S, String Format)
        {
            return S.Ex_ToDateTime(Format, System.Data.SqlTypes.SqlDateTime.MinValue.Value);
        }
        public static DateTime Ex_ToDateTime(this String S, String Format, DateTime _Default)
        {
            DateTime R = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            if (DateTime.TryParseExact(S, Format, null, System.Globalization.DateTimeStyles.None, out R)) return R;
            return _Default;
        }
        public static DateTime Ex_ToDateTime(this String S, String[] Format)
        {
            return S.Ex_ToDateTime(Format);
        }
        public static DateTime Ex_ToDateTime(this String S, String[] Format, DateTime _Default)
        {
            DateTime R = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            if (DateTime.TryParseExact(S, Format, null, System.Globalization.DateTimeStyles.None, out R)) return R;
            return _Default;
        }

        public static DateTime Ex_ToSqlDataTime(this DateTime Time)
        {
            Time = Time >= System.Data.SqlTypes.SqlDateTime.MinValue.Value ? Time : System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            Time = Time <= System.Data.SqlTypes.SqlDateTime.MaxValue.Value ? Time : System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
            return Time;
        }

        public static Double Ex_ToLinuxData(this DateTime Time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (Time - startTime).TotalSeconds;
            return intResult;
        }
        public static Double Ex_ToLinuxData()
        {
            return DateTime.Now.Ex_ToLinuxData();
        }

        public static String Ex_ToJson<T>(this T E)
        {

            //var jSetting = new JsonSerializerSettings { MissingMemberHandling = DateFormatHandling.MicrosoftDateFormat }; 
            //            var json = JsonConvert.SerializeObject(response, Formatting.Indented, jSetting);
            //var J=  new JavaScriptSerializer();
            //JavaScriptTypeResolver
            String Json = "";
            if (E != null)
                Json = (new JavaScriptSerializer()).Serialize(E);
            return Json;
        }
        public static T Ex_ToEntity<T>(this String Json)
        {
            return (new JavaScriptSerializer()).Deserialize<T>(Json);
        }

        //public static String Ex_ToString(this String S, Int32 MaxLength)
        //{
        //    return S.Ex_ToString(MaxLength, "");
        //}
        public static String Ex_ToString(this String S, Int32 MaxLength, String ExtensionStr = "")
        {
            S = S ?? "";
            return S.Length >= MaxLength ? S.Substring(0, MaxLength) + ExtensionStr : S;
        }

        public static List<String> Ex_ToList(this String String)
        {
            var SplitArg = new[] { ',', ' ', '\n', '\r', '\t', '，' };
            return String.Ex_ToList(SplitArg);
        }
        public static List<String> Ex_ToList(this String String, params Char[] SplitArg)
        {
            if (String.IsNullOrEmpty(String)) return new List<string>();
            return String.Split(SplitArg, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public static List<String> Ex_ToList(this String String, params String[] SplitArg)
        {
            if (String.IsNullOrEmpty(String)) return new List<string>();
            return String.Split(SplitArg, StringSplitOptions.RemoveEmptyEntries).ToList();
        }


        //public static Int32 Ex_ToInt32(this String S)
        //{
        //    //if (S == null) return 0;
        //    //var R = 0;
        //    //Int32.TryParse(S, out R);
        //    return S.Ex_ToInt32(0);
        //}
        public static Int32 Ex_ToInt32(this String S, Int32 _Default = 0)
        {
            if (String.IsNullOrWhiteSpace(S)) return _Default;
            var R = 0;
            if (Int32.TryParse(S, out R)) return R;
            return _Default;
        }

        public static List<Int32> Ex_ToInt32<T>(this IEnumerable<T> L)
        {
            if (L == null) return new List<int>();
            var R = L.Select(p => p.ToString().Ex_ToInt32()).ToList();
            return R;
        }
        public static List<Int32> Ex_ToInt32<T>(this IEnumerable<T> L, Int32 I)
        {
            if (L == null) return new List<int>();
            var R = L.Select(p => p.ToString().Ex_ToInt32(I)).ToList();
            return R;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="L">{{T,T},{T,T,T}}</param>
        /// <returns>{T,T,T,T,T}</returns>
        public static List<T> Ex_ToList<T>(this IEnumerable<IEnumerable<T>> L)
        {
            var R = new List<T>();
            foreach (var I in L)
            {
                R.AddRange(I);
            }
            return R;
        }
        #endregion

        #region SelectListItem
        public static String Ex_GetText(this IEnumerable<SelectListItem> iList, Int32? Value)
        {
            return iList.Ex_GetText(Value.ToString());
        }
        public static String Ex_GetText(this IEnumerable<SelectListItem> iList, String Value)
        {
            var item = iList.FirstOrDefault(p => p.Value == Value);
            if (item != null) return item.Text;
            return "--";
        }
        public static Int32 Ex_GetValue(this IEnumerable<SelectListItem> iList, String Text)
        {
            var item = iList.FirstOrDefault(p => p.Text == Text);
            if (item != null) return item.Value.Ex_ToInt32();
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iList"></param>
        /// <param name="Text"></param>
        /// <param name="Value"></param>
        /// <param name="Index">从0开始</param>
        /// <returns></returns>
        public static List<SelectListItem> Ex_Add(this IEnumerable<SelectListItem> iList, String Text, String Value, Int32 Index)
        {
            var List = iList.ToList() ?? new List<SelectListItem>();
            Index = Index > List.Count ? List.Count : Index;
            List.Insert(Index, new SelectListItem { Text = Text, Value = Value });
            return List;
        }
        public static List<SelectListItem> Ex_AddDefault(this IEnumerable<SelectListItem> iList)
        {
            var List = iList.ToList() ?? new List<SelectListItem>();
            List.Insert(0, new SelectListItem { Text = "--全部--", Value = "" });
            return List;
        }
        public static List<SelectListItem> Ex_AddDefault(this IEnumerable<SelectListItem> iList, String Text)
        {
            var List = iList.ToList() ?? new List<SelectListItem>();
            List.Insert(0, new SelectListItem { Text = Text, Value = "" });
            return List;
        }
        public static List<SelectListItem> Ex_AddDefault(this IEnumerable<SelectListItem> iList, String Text, Int32? Value)
        {
            var List = iList.ToList() ?? new List<SelectListItem>();
            List.Insert(0, new SelectListItem { Text = Text, Value = Value + "" });
            return List;
        }
        public static List<SelectListItem> Ex_SetSelected(this IEnumerable<SelectListItem> iList, Int32? Value)
        {
            var List = iList ?? new List<SelectListItem>();
            return List.Select(p =>
            {
                if (p.Value == (Value + "")) p.Selected = true; return p;
            }).ToList();
        }
        public static List<SelectListItem> Ex_SetSelected(this IEnumerable<SelectListItem> iList, String Text)
        {
            var List = iList ?? new List<SelectListItem>();
            return List.Select(p =>
            {
                if (p.Text == Text) p.Selected = true; return p;
            }).ToList();
        }

        #endregion

        #region Datatable<=>List

        /// <summary>    

        /// 转化一个DataTable    

        /// </summary>    

        /// <typeparam name="T"></typeparam>    
        /// <param name="list"></param>    
        /// <returns></returns>    
        public static DataTable Ex_ToDataTable<T>(this IEnumerable<T> L)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口    

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in L)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }


        /// <summary>    
        /// DataTable 转换为List 集合    
        /// </summary>    
        /// <typeparam name="TResult">类型</typeparam>    
        /// <param name="dt">DataTable</param>    
        /// <returns></returns>    
        public static List<T> Ex_ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            Type t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合    

            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                T ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }




        /// <summary>    
        /// 将集合类转换成DataTable    
        /// </summary>    
        /// <param name="list">集合</param>    
        /// <returns></returns>    
        public static DataTable Ex_ToDataTableRow(this IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        ///**/

        ///// <summary>    
        ///// 将泛型集合类转换成DataTable    

        ///// </summary>    
        ///// <typeparam name="T">集合项类型</typeparam>    

        ///// <param name="list">集合</param>    
        ///// <returns>数据集(表)</returns>    
        //public static DataTable ToDataTable<T>(IList<T> list)
        //{
        //    return ToDataTable<T>(list, null);
        //}

        /**/
        /// <summary>    
        /// 将泛型集合类转换成DataTable    
        /// </summary>    
        /// <typeparam name="T">集合项类型</typeparam>    
        /// <param name="list">集合</param>    
        /// <param name="propertyName">需要返回的列的列名</param>    
        /// <returns>数据集(表)</returns>    
        public static DataTable Ex_ToDataTable<T>(this IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {

                    if (propertyNameList.Count == 0)
                    {
                        //if (pi.PropertyType==typeof(Nullable<>))
                        //{
                        result.Columns.Add(pi.Name);
                        //}
                        //else
                        //    result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        #endregion


        #region ExpressionExtension
        public static IOrderedQueryable<T> Ex_OrderBy<T>(this IQueryable<T> source, params KeyValuePair<String, Boolean>[] OrderByPropertyList) where T : class
        {
            if (OrderByPropertyList.Count() == 1) return source.Ex_OrderBy(OrderByPropertyList[0].Key, OrderByPropertyList[0].Value);
            if (OrderByPropertyList.Count() > 1)
            {
                var type = typeof(T);
                var param = Expression.Parameter(type, type.Name);
                Func<String, dynamic> KeySelectorFunc = _PropertyName =>
                {
                    return Expression.Lambda(Expression.Property(param, _PropertyName), param);
                };
                IOrderedQueryable<T> OrderedQueryable = OrderByPropertyList[0].Value
                    ? Queryable.OrderBy(source, KeySelectorFunc(OrderByPropertyList[0].Key))
                    : Queryable.OrderByDescending(source, KeySelectorFunc(OrderByPropertyList[0].Key));
                for (int i = 1; i < OrderByPropertyList.Length; i++)
                {
                    OrderedQueryable = OrderByPropertyList[i].Value
                        ? Queryable.ThenBy(OrderedQueryable, KeySelectorFunc(OrderByPropertyList[i].Key))
                        : Queryable.ThenByDescending(OrderedQueryable, KeySelectorFunc(OrderByPropertyList[i].Key));
                }
                return OrderedQueryable;
            }
            return null;
        }
        public static IOrderedQueryable<T> Ex_OrderBy<T>(this IQueryable<T> source, string OrderByPropertyName, bool IsOrderByAsc = true) where T : class
        {
            string command = IsOrderByAsc ? "OrderBy" : "OrderByDescending";
            var type = typeof(T);
            var property = type.GetProperty(OrderByPropertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression) as IOrderedQueryable<T>;

            //
            //var param = Expression.Parameter(type, type.Name);
            //var body = Expression.Property(param, OrderByPropertyName);
            //dynamic KeySelector = Expression.Lambda(body, param);

            //return IsOrderByDesc ? Queryable.OrderByDescending(source, KeySelector) : Queryable.OrderBy(source, KeySelector);
        }
        public static IOrderedQueryable<T> Ex_ThenBy<T>(this IOrderedQueryable<T> OrderedQueryable, params KeyValuePair<String, Boolean>[] OrderByPropertyList) where T : class
        {
            if (OrderByPropertyList.Count() > 0)
            {
                var type = typeof(T);
                var param = Expression.Parameter(type, type.Name);
                Func<String, dynamic> KeySelectorFunc = _PropertyName =>
                {
                    return Expression.Lambda(Expression.Property(param, _PropertyName), param);
                };
                for (int i = 0; i < OrderByPropertyList.Length; i++)
                {
                    OrderedQueryable = OrderByPropertyList[i].Value
                        ? Queryable.ThenBy(OrderedQueryable, KeySelectorFunc(OrderByPropertyList[i].Key))
                        : Queryable.ThenByDescending(OrderedQueryable, KeySelectorFunc(OrderByPropertyList[i].Key));
                }
                return OrderedQueryable;
            }
            return null;
        }
        public static IOrderedQueryable<T> Ex_ThenBy<T>(this IOrderedQueryable<T> OrderedQueryable, string OrderByPropertyName, bool IsOrderByAsc = true) where T : class
        {
            string command = IsOrderByAsc ? "ThenBy" : "ThenByDescending";
            var type = typeof(T);
            //var property = type.GetProperty(OrderByPropertyName);
            //var parameter = Expression.Parameter(type, "p");
            //var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            //var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            //var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            //return source.Provider.CreateQuery<T>(resultExpression) as IOrderedQueryable<T>;

            var param = Expression.Parameter(type, type.Name);
            var body = Expression.Property(param, OrderByPropertyName);
            dynamic KeySelector = Expression.Lambda(body, param);

            return IsOrderByAsc ? Queryable.ThenBy(OrderedQueryable, KeySelector) : Queryable.ThenByDescending(OrderedQueryable, KeySelector);
        }
       
        #endregion
        public static List<T> Ex_GetPagination<T>(this IOrderedQueryable<T> OrderedQueryable, Int32 PageIndex, Int32 PageSize = 10) where T : class
        {
            var Page = new K.Y.DLL.Model.M_Pagination(PageIndex, PageSize);
            return Page.GetPagination(OrderedQueryable);
        }
        public static List<T> Ex_GetPagination<T>(this IOrderedQueryable<T> OrderedQueryable, K.Y.DLL.Model.M_Pagination Page) where T : class
        {
            return Page.GetPagination(OrderedQueryable);
        }

        #region MvcHtmlHelper
        private static string GetPropertyName<T, TKey>(Expression<Func<T, TKey>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }
        public static MvcHtmlString Ex_File<TModel>(this HtmlHelper<TModel> htmlHelper, String Name, IDictionary<string, object> htmlAttributes)
        {
            var S = "<a " + String.Join(" ", htmlAttributes.Select(p => p.Key + "=\"" + p.Value + "\"")) + " href=\"javascript:$('#f_" + Name + "').click();\">"
                      + "<input type=\"file\" id=\"f_" + Name + "\" name=\"f_" + Name + "\" onchange=\"PostFile(\'" + Name + "\')\" style=\"display:none\" />"
                      + "浏览..."
                  + "</a>";
            return new MvcHtmlString(S);
        }
        public static MvcHtmlString Ex_File<TModel>(this HtmlHelper<TModel> htmlHelper, String Name, object htmlAttributes = null)
        {
            return htmlHelper.Ex_File(Name, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString Ex_FileFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            var Name = GetPropertyName(expression);
            var S = "<a " + String.Join(" ", htmlAttributes.Select(p => p.Key + "=\"" + p.Value + "\"")) + " href=\"javascript:$('#f_" + Name + "').click();\">"
                      + "<input type=\"file\" id=\"f_" + Name + "\" name=\"f_" + Name + "\" onchange=\"PostFile(\'" + Name + "\')\" style=\"display:none\" />"
                      + "浏览..."
                  + "</a>";
            return new MvcHtmlString(S);
        }
        public static MvcHtmlString Ex_FileFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return htmlHelper.Ex_FileFor(expression, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            //var Name = GetPropertyName(expression);
            //var S = "<a " + String.Join(" ", htmlAttributes.Select(p => p.Key + "=\"" + p.Value + "\"")) + " href=\"javascript:$('#f_Icon').click();\">"
            //          + "<input type=\"file\" id=\"f_Icon\" name=\"f_Icon\" onchange=\"PostFile(\'Icon\')\" style=\"display:none\" />"
            //          + "浏览..."
            //      + "</a>";
            //return new MvcHtmlString(S);
        }
        public static MvcHtmlString Ex_DateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (!htmlAttributes.ContainsKey("onFocus")) htmlAttributes.Add("onFocus", "WdatePicker({dateFmt:'yyyy-MM-dd'})");
            return htmlHelper.TextBoxFor(expression, htmlAttributes);
        }
        public static MvcHtmlString Ex_DateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.Ex_DateFor(expression, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString Ex_Date(this HtmlHelper htmlHelper, String Name, Object Value, IDictionary<string, object> htmlAttributes)
        {
            if (!htmlAttributes.ContainsKey("onFocus")) htmlAttributes.Add("onFocus", "WdatePicker({dateFmt:'yyyy-MM-dd'})");
            return htmlHelper.TextBox(Name, Value, htmlAttributes);
        }
        public static MvcHtmlString Ex_Date(this HtmlHelper htmlHelper, String Name, Object Value, object htmlAttributes)
        {
            return htmlHelper.Ex_Date(Name, Value, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString Ex_DateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (!htmlAttributes.ContainsKey("onFocus")) htmlAttributes.Add("onFocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            return htmlHelper.TextBoxFor(expression, htmlAttributes);
        }
        public static MvcHtmlString Ex_DateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.Ex_DateTimeFor(expression, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString Ex_DateTime(this HtmlHelper htmlHelper, String Name, Object Value, IDictionary<string, object> htmlAttributes)
        {
            if (!htmlAttributes.ContainsKey("onFocus")) htmlAttributes.Add("onFocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            return htmlHelper.TextBox(Name, Value, htmlAttributes);
        }
        public static MvcHtmlString Ex_DateTime(this HtmlHelper htmlHelper, String Name, Object Value, object htmlAttributes)
        {
            return htmlHelper.Ex_DateTime(Name, Value, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
        }


        #endregion


        #region HttpPostedFileBase
        public static System.Web.HttpPostedFileBase Ex_GetFile(this System.Web.HttpFileCollectionBase Files, String FileName)
        {
            return Files[FileName];
        }
        public static System.Web.HttpPostedFileBase Ex_GetFile(this System.Web.HttpFileCollectionBase Files)
        {
            if (Files.Count > 0)
                return Files[0];
            return null;
        }
        public static List<System.Web.HttpPostedFileBase> Ex_GetFiles(this System.Web.HttpFileCollectionBase Files, String FileName)
        {
            var F = new List<System.Web.HttpPostedFileBase>();
            for (int i = 0; i < Files.Count; i++)
            {
                var f = Files[i];
                if (Files.AllKeys[i].Equals(FileName)) F.Add(f);
            }
            return F;
        }
        public static List<System.Web.HttpPostedFileBase> Ex_GetFiles(this System.Web.HttpFileCollectionBase Files)
        {
            var F = new List<System.Web.HttpPostedFileBase>();
            for (int i = 0; i < Files.Count; i++)
            {
                var f = Files[i];
                F.Add(f);
            }
            return F;
        }

        #endregion
    }
}
