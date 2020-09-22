using System;

namespace DemoConsoleAppCore
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisHelper rh = new RedisHelper();
            Console.WriteLine("请输入Value：");
            var s = Console.ReadLine();
            rh.Set("KeyAbc", s, 10);

            Console.WriteLine(rh.Get<string>("KeyAbc"));
            Console.ReadLine();
            Console.WriteLine(rh.Get<string>("KeyAbc"));
            Console.ReadLine();
        }
    }
}
