using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Remote.Security
{
    abstract class Base
    {
        private const string Salt = "12345678";

        public static string GetSalt()
        {
            return Salt + Config.Salt;
        }
    }
}
