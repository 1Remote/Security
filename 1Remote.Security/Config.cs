using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Remote.Security
{
    public static class Config
    {
        private static readonly Random Random = new Random();
        public static string Salt { get; private set; } = "";
        public static void SetSalt(string salt)
        {
            Salt = EasyEncryption.MD5.ComputeMD5Hash(salt);
            if(Salt.Length > 8)
                Salt = Salt.Substring(0, 8);
        }
    }
}
