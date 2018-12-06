using ORM;

namespace Monito.Models
{
    [Table("Log", DBTypeEnum.MySQL)]
    public class ExplainErrorLog
    {
        [Key, Identity]
        public long Id { get; set; }
        [Field(Length: 1024, NotNull: false)]
        public string Message { get; set; }
        [Field(Length: 1024, NotNull: false)]
        public string HelpLink { get; set; }
        [Field(Length: 2048, NotNull: false)]
        public string StackTrace { get; set; }
        [Field(Length: 2048, NotNull: false)]
        public string Source { get; set; }
        [Field(Length: 2048)]
        public string All { get; set; }
    }
}
