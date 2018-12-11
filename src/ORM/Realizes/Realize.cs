using ORM.Interface;
using ORM.Interface.IQuery;
using System;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    /// <summary>
    /// 多表实现基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public class BaseRealize<T, TFunc, TFuncBool> : RealizeQuery<T>, IQuerySelect<T, TFunc, TFuncBool>
    {
        /// <summary>
        /// WHERE
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryWhere<T, TFunc, TFuncBool> Where(params Expression<TFuncBool>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }

        /// <summary>
        /// ORDER BY  ASC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryOrder<T, TFunc> OrderA(params Expression<TFunc>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Asc));
            }
            return this;
        }

        /// <summary>
        /// ORDER BY  DESC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryOrder<T, TFunc> OrderD(params Expression<TFunc>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Desc));
            }
            return this;
        }

        /// <summary>
        /// HAVING
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryHaving<T, TFunc, TFuncBool> Having(params Expression<TFuncBool>[] exps)
        {
            _having.AddRange(exps);
            return this;
        }

        /// <summary>
        /// GROUP
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryGroup<T, TFunc, TFuncBool> Group(params Expression<TFunc>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        /// <summary>
        /// JOIN 
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryJoin<T, TFunc, TFuncBool> Join(params Expression<TFuncBool>[] exps)
        {
            foreach (var item in exps)
            {
                _join.Add((item, JoinEnum.Join));
            }
            return this;
        }

        /// <summary>
        /// JOIN LEFT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryJoin<T, TFunc, TFuncBool> JoinL(params Expression<TFuncBool>[] exps)
        {
            foreach (var item in exps)
            {
                _join.Add((item, JoinEnum.LeftJoin));
            }
            return this;
        }

        /// <summary>
        /// JOIN RIGHT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryJoin<T, TFunc, TFuncBool> JoinR(params Expression<TFuncBool>[] exps)
        {
            foreach (var item in exps)
            {
                _join.Add((item, JoinEnum.RightJoin));
            }
            return this;
        }

        /// <summary>
        /// JOIN FULL
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryJoin<T, TFunc, TFuncBool> JoinF(params Expression<TFuncBool>[] exps)
        {
            foreach (var item in exps)
            {
                _join.Add((item, JoinEnum.FullJoin));
            }
            return this;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps)
        {
            foreach (var (exp, alias) in exps)
            {
                _selectAlias.Add((exp, alias));
            }
            return this;
        }
    }

    /// <summary>
    /// 单表实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Realize<T> : RealizeQuery<T>, IQuerySelect<T>
    {
        /// <summary>
        /// WHERE
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryWhere<T, Func<T, object>, Func<T, bool>> Where(params Expression<Func<T, bool>>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }

        /// <summary>
        /// ORDER BY  ASC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryOrder<T, Func<T, object>> OrderA(params Expression<Func<T, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Asc));
            }
            return this;
        }

        /// <summary>
        /// ORDER BY  DESC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryOrder<T, Func<T, object>> OrderD(params Expression<Func<T, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Desc));
            }
            return this;
        }

        /// <summary>
        /// HAVING
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryHaving<T, Func<T, object>, Func<T, bool>> Having(params Expression<Func<T, bool>>[] exps)
        {
            _having.AddRange(exps);
            return this;
        }

        /// <summary>
        /// GROUP BY
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQueryGroup<T, Func<T, object>, Func<T, bool>> Group(params Expression<Func<T, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IQuerySelect<T> Select(Expression<Func<T, object>> exp, string alias)
        {
            _selectAlias.Add((exp, alias));
            return this;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps)
        {
            foreach (var (exp, alias) in exps)
            {
                _selectAlias.Add((exp, alias));
            }
            return this;
        }
    }

    #region 多表定义
    /// <summary>
    /// 2表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin"></typeparam>
    public class Realize<T, TJoin> : BaseRealize<T, Func<T, TJoin, object>, Func<T, TJoin, bool>>
    { }
    /// <summary>
    /// 3表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    public class Realize<T, TJoin1, TJoin2> : BaseRealize<T, Func<T, TJoin1, TJoin2, object>, Func<T, TJoin1, TJoin2, bool>>
    { }
    /// <summary>
    /// 4表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, object>, Func<T, TJoin1, TJoin2, TJoin3, bool>>
    { }
    /// <summary>
    /// 5表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>>
    { }
    /// <summary>
    /// 6表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, bool>>
    { }
    /// <summary>
    /// 7表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, bool>>
    { }
    /// <summary>
    /// 8表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, bool>>
    { }
    /// <summary>
    /// 9表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    /// <typeparam name="TJoin8"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, bool>>
    { }
    /// <summary>
    /// 10表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    /// <typeparam name="TJoin8"></typeparam>
    /// <typeparam name="TJoin9"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, bool>>
    { }
    /// <summary>
    /// 11表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    /// <typeparam name="TJoin8"></typeparam>
    /// <typeparam name="TJoin9"></typeparam>
    /// <typeparam name="TJoin10"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, bool>>
    { }
    /// <summary>
    /// 12表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    /// <typeparam name="TJoin8"></typeparam>
    /// <typeparam name="TJoin9"></typeparam>
    /// <typeparam name="TJoin10"></typeparam>
    /// <typeparam name="TJoin11"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, bool>>
    { }
    /// <summary>
    /// 13表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TJoin1"></typeparam>
    /// <typeparam name="TJoin2"></typeparam>
    /// <typeparam name="TJoin3"></typeparam>
    /// <typeparam name="TJoin4"></typeparam>
    /// <typeparam name="TJoin5"></typeparam>
    /// <typeparam name="TJoin6"></typeparam>
    /// <typeparam name="TJoin7"></typeparam>
    /// <typeparam name="TJoin8"></typeparam>
    /// <typeparam name="TJoin9"></typeparam>
    /// <typeparam name="TJoin10"></typeparam>
    /// <typeparam name="TJoin11"></typeparam>
    /// <typeparam name="TJoin12"></typeparam>
    public class Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12> : BaseRealize<T, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12, object>, Func<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12, bool>>
    { }
    #endregion
}
