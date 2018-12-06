using ORM;

namespace Monito.Models
{
    public class ExplainErrorLog
    {
        [Key, Identity]
        public long Id { get; set; }
        [Field(Length: 1024)]
        public string Message { get; set; }
        [Field(Length: 1024)]
        public string HelpLink { get; set; }
        [Field(Length: 2048)]
        public string StackTrace { get; set; }
        [Field(Length: 2048)]
        public string Source { get; set; }
        [Field(Length: 2048)]
        public string All { get; set; }
    }
}
