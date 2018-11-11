using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM
{
    /// <summary>
    /// 自定义方法
    /// </summary>
    public static class ORMTool
    {
        public static bool NotIn<TValue>(this TValue field, IEnumerable<TValue> values)
        {
            return true;
        }
        public static bool In<TValue>(this TValue field, IEnumerable<TValue> values)
        {
            return true;
        }
        public static int Count(object value)
        {
            return 0;
        }
        public static int Max(object value)
        {
            return 0;
        }
        public static int Min(object value)
        {
            return 0;
        }
    }
}
