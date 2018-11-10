using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ORM.ORM.Query<Model>()
                .Select(x => x.Name, x => x.Date)
                .And(x => x.Date == DateTime.Today && x.Name1 == "123123")
                .OrderA(x => x.Name1).Exist();


            Console.ReadLine();
        }
    }

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
