using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// 自定义方法
    /// </summary>
    public static class ORMTool
    {
        public static bool NotIn<TValue>(this object field, IEnumerable<TValue> values)
        {
            return true;
        }
        public static bool In<TValue>(this object field, IEnumerable<TValue> values)
        {
            return true;
        }
        public static bool LikeF<TValue>(this string field, string value)
        {
            return true;
        }
        public static bool LikeR<TValue>(this string field, string value)
        {
            return true;
        }
        public static bool LikeL<TValue>(this string field, string value)
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
