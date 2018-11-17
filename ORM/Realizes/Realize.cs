using ORM.Interface;
using ORM.Interface.IQuery;
using System;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    /// <summary>
    /// 多表查询的实现基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public class BaseRealize<T, TFunc, TFuncBool> : RealizeQuery<T>, IQuerySelect<T, TFunc, TFuncBool>
    {
        public IQueryWhere<T, TFunc, TFuncBool> Where(params Expression<TFuncBool>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderA(params Expression<TFunc>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderD(params Expression<TFunc>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryHaving<T, TFunc, TFuncBool> Having(params Expression<TFuncBool>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQueryGroup<T, TFunc, TFuncBool> Group(params Expression<TFunc>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryJoin<T, TFunc, TFuncBool> Join(params Expression<TFuncBool>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQueryJoin<T, TFunc, TFuncBool> JoinL(params Expression<TFuncBool>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQueryJoin<T, TFunc, TFuncBool> JoinR(params Expression<TFuncBool>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQueryJoin<T, TFunc, TFuncBool> JoinF(params Expression<TFuncBool>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 单表查询实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Realize<T> : RealizeQuery<T>, IQuerySelect<T>
    {
        public IQueryWhere<T, Func<T, object>, Func<T, bool>> Where(params Expression<Func<T, bool>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, Func<T, object>> OrderA(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, Func<T, object>> OrderD(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryHaving<T, Func<T, object>, Func<T, bool>> Having(params Expression<Func<T, bool>>[] exp)
        {
            throw new NotImplementedException();
        }

        public IQueryGroup<T, Func<T, object>, Func<T, bool>> Group(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps)
        {
            throw new NotImplementedException();
        }
    }

    #region 多表查询定义
    public class Realize<T, TJoin> : BaseRealize<T, Func<T, TJoin, object>, Func<T, TJoin, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2> : BaseRealize<T, Func<T, TJoin1, TJoin2, object>, Func<T, TJoin1, TJoin2, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, object>, Func<T, TJoin1, TJoin2, TJoin3, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, bool>>
    { }
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12, bool>>
    { }
    #endregion
}
