namespace SqlHelper
{
    /// <summary>
    /// sql 中的 where 相关 配置
    /// </summary>
    public class WhereDictionary
    {
        public string Field { get; set; } = string.Empty;
        public object Value { get; set; } = new object();
        public RelationEnum Relation { get; set; }
        public CoexistEnum Coexist { get; set; }
    }
}
