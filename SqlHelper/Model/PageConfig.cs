namespace SqlHelper
{
    /// <summary>
    /// 分页配置
    /// </summary>
    public class PageConfig
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 0;

        /// <summary>
        /// 分页关键排序
        /// </summary>
        public string PageSortField { get; set; } = string.Empty;

        /// <summary>
        /// 排序类型
        /// </summary>
        public SortEnum SortEnum { get; set; }

        /// <summary>
        /// 多排序或者复杂排序用此字段
        /// </summary>
        public string PageSortSql { get; set; } = string.Empty;
    }
}
