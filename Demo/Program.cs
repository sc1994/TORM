using System;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");



            while (true)
            {
                Console.WriteLine("keep live");
                Thread.Sleep(6000);
            }
        }
    }
}
