using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K.Y.DLL;
using K.Y.DLL.Tool;
using K.Y.DLL.Model;
using System.Linq.Expressions;
using System.Data.Entity;

namespace K.Y.DLL.Entity
{
    public class _API<E, T>
        where T : class, new()
        where E : System.Data.Entity.DbContext, new()
    {
        static Object _Lock = new Object();
        //Func<E> F = () => { return new E(); };
        //E EF { get { return F(); } }
        //public _API(Func<E> _F) { if (_F != null) F = _F; }


        public static T Model(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
        {
            try
            {
                using (var EF = new E())
                {
                    var M = EF.Set<T>().FirstOrDefault(F);
                    if (M == null) return null;
                    if (CallBack != null)
                        CallBack(EF, M);
                    return M;
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static T Model(Expression<Func<T, Boolean>> F)", typeof(T).ToString(), ex);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">主键 不一定为ID</param>
        /// <returns></returns>
        public static T Model(Int32 ID, Action<E, T> CallBack = null)//效率太低  PASS//已修复
        {
            try
            {
                using (var EF = new E())
                {
                    var M = EF.Set<T>().Find(ID);
                    if (M == null) return null;
                    if (CallBack != null)
                        CallBack(EF, M);
                    return M;
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static T Model(Int32 ID)", typeof(T).ToString(), ex);
                return null;
            }
        }


        public static M_Result Insert(T Model, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var M = EF.Set<T>().Add(Model);
                        //EF.Entry<T>(Model).State = EntityState.Added;
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Insert(T Model)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }
        public static M_Result InsertRange(IEnumerable<T> Models, Action<E, IEnumerable<T>> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var lM = EF.Set<T>().AddRange(Models);
                        //EF.Entry<T>(Model).State = EntityState.Added;
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, lM);
                        return new M_Result() { result = 1, msg = "操作成功", data = lM };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result InsertRange(IEnumerable<T> Models)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }
        public static M_Result Insert(T Model, Func<E, M_Result> F, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        if (F != null)
                        {
                            var R = F(EF);
                            if (R != null) return R;
                        }
                        var M = EF.Set<T>().Add(Model);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result Insert(T Model, Func<E, M_Result> F)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }


        public static M_Result Update(Int32 ID, Action<T> A, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var M = EF.Set<T>().Find(ID);
                        if (M == null) return new M_Result() { result = 140, msg = "找不到对象" };

                        if (A != null) A(M);
                        //((dynamic)A).Name = "1231sssss3";
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result Update(Int32 ID, Action<T> A)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }
        public static M_Result Update(Expression<Func<T, Boolean>> F, Action<T> A, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var M = EF.Set<T>().FirstOrDefault(F);
                        if (M == null) return new M_Result() { result = 140, msg = "找不到对象" };

                        if (A != null) A(M);

                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Update(Expression<Func<T, Boolean>> F, Action<T> A)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }
        public static M_Result Update(Int32 ID, Func<E, T, M_Result> F, Action<T> A, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var M = EF.Set<T>().Find(ID);
                        if (M == null) return new M_Result() { result = 140, msg = "找不到对象" };

                        if (F != null)
                        {
                            var R = F(EF, M);
                            if (R != null) return R;
                        }
                        if (A != null) A(M);

                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result Update(Int32 ID, Func<E, T, M_Result> F, Action<T> A)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }
        public static M_Result Update(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F, Action<T> A, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var M = EF.Set<T>().FirstOrDefault(F_Express);
                        if (M == null) return new M_Result() { result = 140, msg = "找不到对象" };

                        if (F != null)
                        {
                            var R = F(EF, M);
                            if (R != null) return R;
                        }
                        if (A != null) A(M);

                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功", data = M };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Update(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F, Action<T> A)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" };
            }
        }

        public static M_Result Delete(Int32 ID, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        T M = EF.Set<T>().Find(ID);
                        if (M == null) return new M_Result() { result = 0, msg = "找不到对象" };
                        EF.Set<T>().Remove(M);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功" };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result Delete(Int32 ID)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" + ex };
            }
        }
        public static M_Result Delete(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        T M = EF.Set<T>().FirstOrDefault(F);
                        if (M == null) return new M_Result() { result = 0, msg = "找不到对象" };
                        EF.Set<T>().Remove(M);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功" };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Delete(Expression<Func<T, Boolean>> F)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" + ex };
            }
        }
        public static M_Result DeleteRange(Expression<Func<T, Boolean>> F, Action<E, IEnumerable<T>> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        var L = EF.Set<T>().Where(F);
                        if (L.Count() == 0) return new M_Result() { result = 0, msg = "找不到对象" };
                        var lM = EF.Set<T>().RemoveRange(L);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, lM);
                        return new M_Result() { result = 1, msg = "操作成功" };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static M_Result DeleteRange(Expression<Func<T, Boolean>> F)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" + ex };
            }
        }
        public static M_Result Delete(Int32 ID, Func<E, T, M_Result> F, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        T M = EF.Set<T>().Find(ID);
                        if (M == null) return new M_Result() { result = 0, msg = "找不到对象" };

                        if (F != null)
                        {
                            var R = F(EF, M);
                            if (R != null) return R;
                        }
                        EF.Set<T>().Remove(M);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功" };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Delete(Int32 ID, Func<E, T, M_Result> F)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" + ex };
            }
        }
        public static M_Result Delete(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F, Action<E, T> CallBack = null)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        T M = EF.Set<T>().FirstOrDefault(F_Express);
                        if (M == null) return new M_Result() { result = 0, msg = "找不到对象" };

                        if (F != null)
                        {
                            var R = F(EF, M);
                            if (R != null) return R;
                        }
                        EF.Set<T>().Remove(M);
                        EF.SaveChanges();
                        if (CallBack != null)
                            CallBack(EF, M);
                        return new M_Result() { result = 1, msg = "操作成功" };
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static M_Result Delete(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F)", typeof(T).ToString(), ex);
                return new M_Result() { result = -1, msg = "系统错误" + ex };
            }
        }


        public static List<T> Search()
        {
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static List<T> Search()", typeof(T).ToString(), ex);
                return new List<T>();
            }
        }
        public static List<T> Search(Expression<Func<T, Boolean>> F)
        {
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().Where(F);
                    #region 分页
                    return list.ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(Expression<Func<T, Boolean>> F)", typeof(T).ToString(), ex);
                return new List<T>(); ;
            }
        }
        public static List<T> Search(Func<IQueryable<T>, IQueryable<T>> F)
        {
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().AsQueryable();
                    #region 逻辑筛选
                    if (F != null) list = F(list);
                    #endregion
                    #region 分页
                    return list.ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(Func<E, IQueryable<T>, IQueryable<T>> F)", typeof(T).ToString(), ex);
                return new List<T>(); ;
            }
        }
        public static List<T> Search(Func<E, IQueryable<T>, IQueryable<T>> F)
        {
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().AsQueryable();
                    #region 逻辑筛选
                    if (F != null) list = F(EF, list);
                    #endregion
                    #region 分页
                    return list.ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(Func<E, IQueryable<T>, IQueryable<T>> F)", typeof(T).ToString(), ex);
                return new List<T>(); ;
            }
        }

        //public static List<T> Search<TKey>(M_Pagination Page, Expression<Func<T, TKey>> F)
        //{
        //    var iList = new List<T>();
        //    try
        //    {
        //        using (var EF = new E())
        //        {
        //            var list = EF.Set<T>().AsQueryable();
        //            #region 逻辑筛选
        //            if (F != null) list = list.OrderByDescending(F);
        //            #endregion
        //            #region 分页
        //            if (Page == null) return list.ToList(); Page.GetPagination(list, ref iList);
        //            #endregion
        //            return iList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(M_Pagination Page, Func<E, IQueryable<T>, IQueryable<T>> F)", typeof(T).ToString(), ex);
        //        return iList;
        //    }
        //}
        public static List<T> Search(M_Pagination Page, Func<IQueryable<T>, IOrderedQueryable<T>> F)
        {
            var iList = new List<T>();
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().AsQueryable();
                    #region 逻辑筛选
                    if (F != null) list = F(list);
                    #endregion
                    #region 分页
                    if (Page == null) return list.ToList(); Page.GetPagination(list, ref iList);
                    #endregion
                    return iList;
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(M_Pagination Page, Func<E, IQueryable<T>, IQueryable<T>> F)", typeof(T).ToString(), ex);
                return iList;
            }
        }
        public static List<T> Search(M_Pagination Page, Func<E, IQueryable<T>, IOrderedQueryable<T>> F)
        {
            var iList = new List<T>();
            try
            {
                using (var EF = new E())
                {
                    var list = EF.Set<T>().AsQueryable();
                    #region 逻辑筛选
                    if (F != null) list = F(EF, list);
                    #endregion
                    #region 分页
                    if (Page == null) return list.ToList(); Page.GetPagination(list, ref iList);
                    #endregion
                    return iList;
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError(" public static List<T> Search(M_Pagination Page, Func<E, IQueryable<T>, IQueryable<T>> F)", typeof(T).ToString(), ex);
                return iList;
            }
        }

    }

    public class _API<E>
         where E : System.Data.Entity.DbContext, new()
    {
        static Object _Lock = new Object();
        static void Exec(params Action<E>[] LA)
        {
            try
            {
                lock (_Lock)
                {
                    using (var EF = new E())
                    {
                        foreach (var A in LA)
                            A(EF);
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static void Exec(Action<E> A)", "", ex);
            }
        }
        public static void Exec(Action<E> A, Boolean IsLock = true)
        {
            try
            {
                if (IsLock)
                {
                    lock (_Lock)
                    {
                        using (var EF = new E())
                        {
                            A(EF);
                        }
                    }
                }
                else
                {
                    using (var EF = new E())
                    {
                        A(EF);
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static void Exec(Action<E> A)", "", ex);
            }
        }
        public static List<P> Exec<P>(Func<E, List<P>> F, Boolean IsLock = true)
        {
            try
            {
                if (IsLock)
                {
                    lock (_Lock)
                    {
                        using (var EF = new E())
                        {
                            return F(EF);
                        }
                    }
                }
                else
                {
                    using (var EF = new E())
                    {
                        return F(EF);
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static void Exec(Action<E> A)", "", ex);
                return new List<P>();
            }
        }
        public static P Exec<P>(Func<E, P> F, Boolean IsLock = true)
        {
            try
            {
                if (IsLock)
                {
                    lock (_Lock)
                    {
                        using (var EF = new E())
                        {
                            return F(EF);
                        }
                    }
                }
                else
                {
                    using (var EF = new E())
                    {
                        return F(EF);
                    }
                }
            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public static void Exec(Action<E> A)", "", ex);
                return default(P);
            }
        }
        public static List<P> ExecProc<P>(Func<E, System.Data.Entity.Core.Objects.ObjectResult<P>> F, Boolean IsLock = true)
        {
            try
            {
                if (IsLock)
                {
                    lock (_Lock)
                    {
                        using (var EF = new E())
                        {
                            if (F != null) return F(EF).ToList();
                            return new List<P>();
                        }
                    }
                }
                else
                {
                    using (var EF = new E())
                    {
                        if (F != null) return F(EF).ToList();
                        return new List<P>();
                    }
                }

            }
            catch (Exception ex)
            {
                K.Y.DLL.Tool.T_Log.LogError("public List<P> ExecProc<P>(Func<E, System.Data.Entity.Core.Objects.ObjectResult<P>> F) where P : class,new()", typeof(P).ToString(), ex);
                return new List<P>();
            }
        }


        public static T Model<T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Model(F, CallBack);
        }
        public static T Model<T>(Int32 ID, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Model(ID, CallBack);
        }

        public static M_Result Insert<T>(T Model, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Insert(Model, CallBack);
        }
        public static M_Result InsertRange<T>(IEnumerable<T> Models, Action<E, IEnumerable<T>> CallBack = null) where T : class,new()
        {
            return _API<E, T>.InsertRange(Models, CallBack);
        }
        public static M_Result Insert<T>(T Model, Func<E, M_Result> F, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Insert(Model, F, CallBack);
        }


        public static M_Result Update<T>(Int32 ID, Action<T> A, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Update(ID, A, CallBack);
        }
        public static M_Result Update<T>(Expression<Func<T, Boolean>> F, Action<T> A, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Update(F, A, CallBack);
        }
        public static M_Result Update<T>(Int32 ID, Func<E, T, M_Result> F, Action<T> A, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Update(ID, A, CallBack);
        }
        public static M_Result Update<T>(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F, Action<T> A, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Update(F_Express, A, CallBack);
        }

        public static M_Result Delete<T>(Int32 ID, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Delete(ID, CallBack);
        }
        public static M_Result Delete<T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Delete(F, CallBack);
        }
        public static M_Result DeleteRange<T>(Expression<Func<T, Boolean>> F, Action<E, IEnumerable<T>> CallBack = null) where T : class,new()
        {
            return _API<E, T>.DeleteRange(F, CallBack);
        }
        public static M_Result Delete<T>(Int32 ID, Func<E, T, M_Result> F, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Delete(ID, F, CallBack);
        }
        public static M_Result Delete<T>(Expression<Func<T, Boolean>> F_Express, Func<E, T, M_Result> F, Action<E, T> CallBack = null) where T : class,new()
        {
            return _API<E, T>.Delete(F_Express, F, CallBack);
        }


        public static List<T> Search<T>() where T : class,new()
        {
            return _API<E, T>.Search();
        }
        public static List<T> Search<T>(Expression<Func<T, Boolean>> F) where T : class,new()
        {
            return _API<E, T>.Search(F);
        }
        public static List<T> Search<T>(Func<IQueryable<T>, IQueryable<T>> F) where T : class,new()
        {
            return _API<E, T>.Search(F);
        }
        public static List<T> Search<T>(Func<E, IQueryable<T>, IQueryable<T>> F) where T : class,new()
        {
            return _API<E, T>.Search(F);
        }
        //public static List<T> Search<T, TKey>(M_Pagination Page, Expression<Func<T, TKey>> F) where T : class,new()
        //{
        //    return _API<E, T>.Search(Page, F);
        //}
        public static List<T> Search<T>(M_Pagination Page, Func<IQueryable<T>, IOrderedQueryable<T>> F) where T : class,new()
        {
            return _API<E, T>.Search(Page, F);
        }
        public static List<T> Search<T>(M_Pagination Page, Func<E, IQueryable<T>, IOrderedQueryable<T>> F) where T : class,new()
        {
            return _API<E, T>.Search(Page, F);
        }
    }
}
