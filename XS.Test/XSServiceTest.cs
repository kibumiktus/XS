using System.Linq;
using NUnit.Framework;
using XS.Core;

namespace XS.Test
{
    [TestFixture]
    public class XSServiceTest
    {
        private Settings _setting;
        private XS_Service xs;

        [SetUp]
        public void Setup()
        {
            _setting = new Settings
            {
                Length = 7,
                AccessibleSymbols = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()
            };
            xs = new XS_Service(_setting);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1_000)]
        [TestCase(10_000)]
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        public void TestWrite(int count)
        {
            Write(xs, count);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1_000)]
        [TestCase(10_000)]
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        public void TestWriteRead(int count)
        {
            var wres = Write(xs, count);
            var rres = Read(xs, wres);
            int i = 0;
            Assert.True(rres.All(x=>x == i++.ToString()));
        }

        private static string[] Write(XS_Service xs, int count)
        {
            var x = new string[count];
            for (int i = 0; i < count; i++)
            {
                x[i] = xs.Store(i.ToString());
            }
            return x;
        }

        private static string[] Read(XS_Service xs, string[] valList)
        {
            string[] results = new string[valList.Length];
            for (int j = 0; j < valList.Length; j++)
            {
                results[j] = xs.Get(valList[j]);
            }

            return results;
        }
    }
}
