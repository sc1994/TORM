using Dapper;
using Flurl.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("start");

            ////var a = new ORMValue<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 });
            ////var c = a.Where(x => x > 0);
            //var sql = "";
            //using (var connection = new MySqlConnection(""))
            //{
            //    var m = connection.QueryMultiple(sql);
            //    var a = m.Read<string>();
            //    var b = m.Read<string>();
            //}



            var r = new List<int>();
            for (int i = 0; i < 1; i++)
            {
                r.Add(i);
            }

            System.Threading.Tasks.Parallel.ForEach(r,
                                                    async i =>
                                                     {
                                                         var s = DateTime.Now;
                                                         var a = await "http://tccxgc.t.51jiesan.com/query/api/DailySurprise/MatchFriend"
                                                                 .WithHeader("Content-Type", "application/json; charset=utf-8")
                                                                 .PostJsonAsync("Data=pf6THlnmxX+vUx6mErBBrisgkk5hibOz6H6HU56TLCBX2lERrE8jEFo+ME68YhvixXnOEd05lcYgRXcabhnrx8l2qYAiIGa3+//oaA8js6JE1ccjtUvF5izLOY8+PrFnEW7sDwnlnJP7C+tWJrBa2X/0K9IgNYoKcTsSNhIUOUbSpfROO3qu+MwWJWAviWOxvMHlptwYf3B2ZigothFQSKKWxhvMTwbBxRRRrO/Q8Q/O7lKvg87njxD6Dn47vvjlbf0V8uvqkZf+JwuGidgJKewRPgPiebOn0b/Av1NCKz601hjgS2esxaqbnDc2pbzv");
                                                         
                                                         Console.WriteLine("\r\n\r\n==================" + a + "\r\n\r\n=====================");
                                                         Console.WriteLine((DateTime.Now - s).TotalMilliseconds);
                                                     });
            while (true)
            {
                Console.WriteLine("keep live");
                Thread.Sleep(6000);
            }
        }
    }
}
