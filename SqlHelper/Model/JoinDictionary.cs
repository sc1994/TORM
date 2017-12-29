namespace SqlHelper
{
    public class JoinDictionary
    {
        public JoinEnum RelationJoin { get; set; }
        public string ThatTable { get; set; } = string.Empty;
        public string ThatAlia { get; set; } = string.Empty;
        public string RelationField { get; set; } = string.Empty;
        public string ThatRelationField { get; set; } = string.Empty;
        public string Where { get; set; } = string.Empty;
    }
}
