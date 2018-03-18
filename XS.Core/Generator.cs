using System;
using System.Text;
namespace XS.Core
{
    public class Generator : IGenerator<string>
    {
        private readonly Settings _settings;
        private Random gn;

        public Generator(Settings settings)
        {
            _settings = settings;
            gn = new Random();
        }


        private int count = 0;
        private int countToRenew = 50000;
        private int GetNext(int max)
        {
            if (count > countToRenew)
            {
                gn = new Random();
                count = 0;
            }
            count++;
            return gn.Next() % max;
        }

        public string Generate(Predicate<string> validation)
        {
            Predicate<string> isValid = validation ?? (x=>false);
            string result;
            do
            {                
                var sb = new StringBuilder(_settings.Length);
                for (int i = 0; i < _settings.Length; i++)
                {
                    var symbolIndex = GetNext(_settings.AccessibleSymbols.Length - 1);
                    sb.Append(_settings.AccessibleSymbols[symbolIndex]);
                }
                result = sb.ToString();
            }
            while (!isValid(result));
            return result;
        }
    }
}
