using System.Collections.Generic;
using System.Diagnostics;
using XS.Core;

namespace XS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var Setting = new Settings
            {
                Length = 5,
                AccessibleSymbols = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()
            };
            var xs = new XS_Service(Setting);
            var key = xs.Store("messages123123");
            var s = Stopwatch.StartNew();
            //TestWrite(xs);
            var res = TestWrite(xs);
            s.Stop();
            System.Console.WriteLine("Write stop by {0} ms", s.Elapsed.TotalSeconds);
            s = Stopwatch.StartNew();
            TestRead(xs, res);
            s.Stop();
            System.Console.WriteLine("Read stop by {0} ms", s.Elapsed.TotalSeconds);
            var val = xs.Get(key);
            System.Console.WriteLine(key);
            System.Console.WriteLine(val);
            System.Console.ReadKey();
        }

        private static List<string> TestWrite(XS_Service xs)
        {
            var x = new List<string>();
            for (int i = 0; i < 10_000_000; i++)
            {
                x.Add(xs.Store(i.ToString()));
                //System.Console.WriteLine(i.ToString());
            }

            return x;
        }

        //private static void TestWrite(XS_Service xs)
        //{
        //    for (int i = 0; i < 10_000_000; i++)
        //    {
        //        xs.Store(i.ToString());
        //        //System.Console.WriteLine(i.ToString());
        //    }
        //}



        private static void TestRead(XS_Service xs, List<string> valList)
        {
            foreach (var item in valList)
            {
                xs.Get(item);
                //System.Console.WriteLine(i.ToString());
            }
        }

    }
}
