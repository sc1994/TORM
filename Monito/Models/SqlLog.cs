using System;
using ORM;

namespace Monito.Models
{
    [Table("Log", DBTypeEnum.MySQL)]
    public class SqlLog
    {
        [Key, Identity]
        public long Id { get; set; }
        [Field(Length: 4069)]
        public string SqlStr { get; set; }
        //public string DB { get; set; }
        //public string Table { get; set; }
        [Field(Length: 4069)]
        public string Param { get; set; }
        [Field(Length: 4069)]
        public string StackTrace { get; set; }
        [Field(Length: 4069)]
        public string ExMessage { get; set; }
        public DateTime EndTime { get; set; }
        [Field(Precision: 11)]
        public double ExplainSpan { get; set; }
        [Field(Precision: 11)]
        public double ConnectSpan { get; set; }
        [Field(Precision: 11)]
        public double ExecuteSpan { get; set; }
        public bool IsError
        {
            get => ExMessage.Length > 0;
            set => IsError = value;
        }
    }
}
