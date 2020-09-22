using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncAwait
{
    class TT
    {
        

        //实例化
        public static void show1(string name)
        {
            Console.WriteLine("show1你好,{0}", name);
        }


        public static string show2(string name)
        {
            Console.WriteLine("show2你好,{0}", name);
            return name;
        }
        static void Main()
        {
            Entrust1 et1 = new Entrust1(show1);
          
            Entrust2 et2 = new Entrust2(show2); 


            //调用
            ttt.a(et1, et2);
        }
    }
    public delegate void Entrust1(string name);//声明委托（没有返回值，有参数）
    public delegate string Entrust2(string name);//声明委托（有返回值，有参数）
    public class ttt
    {
        public static void a(Entrust1 et1, Entrust2 et2)
        {
            et1("XL");
            string 啊啊 = et2.Invoke("少时诵诗书所所所所所所");
            Console.WriteLine(啊啊);
        }
    }
}
