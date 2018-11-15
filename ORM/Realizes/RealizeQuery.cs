using ORM.Interface;
using System;
using System.Collections.Generic;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 查询 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeQuery<T> : RealizeToSql<T>, IQuery<T>
    {
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <returns></returns>
        public bool Exist()
        {
            var sql = $"SELECT COUNT(1) FROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查找第一条数据
        /// </summary>
        /// <returns></returns>
        public T First()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查找第一条数据
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <returns></returns>
        public TOther First<TOther>()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()} {GetGroup()} {GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Find()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <returns></returns>
        public IEnumerable<TOther> Find<TOther>()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()} {GetGroup()} {GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="top">限制获取数量</param>
        /// <returns></returns>
        public IEnumerable<T> Find(int top)
        {
            var t = ToTop(top);
            var sql = string.Format(t, $" \r\nFROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()}");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <param name="top">限制获取数量</param>
        /// <returns></returns>
        public IEnumerable<TOther> Find<TOther>(int top)
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()} {GetGroup()} {GetOrder()};";
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public (IEnumerable<T> data, int total) Page(int index, int size)
        {
            var t = ToPage(index, size);
            var sql = string.Format(t, $"\r\nFROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()}");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <param name="index">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size)
        {
            //var = new StringBuilder($"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()} {GetGroup()} {GetOrder()};");
            var t = ToPage(index, size);
            var sql = string.Format(t, $"\r\nFROM {GetTable()}{GetJoin()}{GetWhere()}{GetGroup()}{GetOrder()}");
            throw new NotImplementedException();
        }
    }
}
