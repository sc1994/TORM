using ORM;
using System;

namespace Demo
{
    class Program
    {
        private static int Count { get; set; }
        static void Main(string[] args)
        {
            TORM.AutoTable<Test>();

            Console.WriteLine("OVER");
            Console.ReadLine();
        }
    }

    [Table("tally", DBTypeEnum.MySQL)]
    class Test
    {
        [Key, Identity]
        public long Id { get; set; }
        [Field(Length: 500, Comment: "这只是个描述")]
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Age { get; set; }
        [Field(Precision: 4)]
        public decimal Money { get; set; }
    }

    [Table("tally", DBTypeEnum.MySQL, "rules")]
    class rules
    {
        [Key, Identity, Field(NotNull: false)]
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; }
    }

    class rulesView
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; }
        public string content { get; set; }
    }

    [Table("tally", DBTypeEnum.MySQL)]
    class schedules
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public string content { get; set; }
    }

    [Table("test", DBTypeEnum.MySQL, "ModelTable")]
    class Model
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public DateTime Date { get; set; }
    }

    class Model2
    {
        public string Name2 { get; set; }
        public string Name12 { get; set; }
        public string Name22 { get; set; }
        public DateTime Date2 { get; set; }
    }
    class Model3
    {
        public string Name3 { get; set; }
        public string Name13 { get; set; }
        public string Name23 { get; set; }
        public DateTime Date3 { get; set; }
    }
}
