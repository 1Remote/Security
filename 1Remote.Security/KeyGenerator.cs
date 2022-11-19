using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Remote.Security
{
    abstract class KeyGenerator
    {
        private const string ConstSalt = "12345678";
        private static readonly Random Random = new Random();

        public static string GetRandomKey()
        {
            var key = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                key.Append((char)Random.Next('0', 'z'));
            }
            key.Append(ConstSalt);
            return key.ToString();
        }

        public static string GetKetWithSalt(string randomKey)
        {
            return randomKey + Config.Salt;
        }
    }
}
