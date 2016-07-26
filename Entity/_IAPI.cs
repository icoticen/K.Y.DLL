using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K.Y.DLL;
using K.Y.DLL.Tool;
using K.Y.DLL.Model;
using System.Linq.Expressions;

namespace K.Y.DLL.Entity
{
    public interface _IAPI<E, T>
        where T : class,new()
        where E : System.Data.Entity.DbContext, new()
    {
        T Model(Expression<Func<T, Boolean>> F);
        T Model(Int32 ID);//效率太低  PASS;

        M_Result Insert(T Model);
        M_Result Insert(T Model, Func<E, M_Result> F);

        M_Result Update(Int32 ID, Action<T> A);
        M_Result Update(Int32 ID, Func<E, T, M_Result> F, Action<T> A);

        M_Result Delete(Int32 ID);
        M_Result Delete(Int32 ID, Func<E, T, M_Result> F);

        List<T> Search();
        List<T> Search(Func<E, IQueryable<T>, IQueryable<T>> F);
        List<T> Search(M_Pagination Page, Func<E, IQueryable<T>, IQueryable<T>> F);

    }
}
