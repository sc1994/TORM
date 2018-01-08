using Dapper;

namespace SqlHelper
{
    public class SqlAndParameter
    {
        public string SqlStr { get; set; } = string.Empty;
        public DynamicParameters Parameter { get; set; } = new DynamicParameters();
    }
}
