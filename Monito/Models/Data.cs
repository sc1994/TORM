using ORM;

namespace Monito.Models
{
    [Table("Log", DBTypeEnum.MySQL)]
    public class LongData : BaseData<long>
    { }

    [Table("Log", DBTypeEnum.MySQL)]
    public class DecimalData : BaseData<decimal>
    { }

    public class BaseData<T>
    {
        public T Value { get; set; }
    }
}
