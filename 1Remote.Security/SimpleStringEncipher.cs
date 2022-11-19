using System;
using System.Text;

namespace _1Remote.Security
{
    public static class SimpleStringEncipher
    {
        private static readonly Random Random = new Random();
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            string keyString;
            // Random key + Salt
            {
                var key = new StringBuilder();
                for (int i = 0; i < 32; i++)
                {
                    key.Append((char)Random.Next('0', 'z'));
                }
                keyString = key.ToString();
                var salt = Base.GetSalt();
                if (salt.Length < 32)
                {
                    keyString = keyString.Substring(0, 32 - salt.Length) + salt;
                }
            }


            // AES
            var aesText = EasyEncryption.AesThenHmac.SimpleEncryptWithPassword(plainText, keyString);


            // keyString insert into cipherText
            var cipher = new StringBuilder();
            int m = 0, n = 0;
            for (int i = 0; i < Math.Min(aesText.Length, keyString.Length); i++)
            {
                if (i % 2 == 0)
                {
                    cipher.Append(aesText[m]);
                    m++;
                }
                else
                {
                    cipher.Append(keyString[n]);
                    n++;
                }
            }

            if (m < aesText.Length)
            {
                for (int i = m; i < aesText.Length; i++)
                {
                    cipher.Append(aesText[m]);
                }
            }

            if (n < keyString.Length)
            {
                for (int i = m; i < keyString.Length; i++)
                {
                    cipher.Append(keyString[m]);
                }
            }

            return cipher.ToString();
        }


        public static string SimpleDecrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;
            if (cipherText.Length <= 64)
                return string.Empty;

            // keyString from into cipherText
            var keyString = new StringBuilder();
            var aesString = new StringBuilder();
            {
                int m = 0;
                for (m = 0; m < 64; m++)
                {
                    if (m % 2 == 0)
                    {
                        aesString.Append(cipherText[m]);
                    }
                    else
                    {
                        keyString.Append(cipherText[m]);
                    }
                }

                for (int i = m; i < cipherText.Length; i++)
                {
                    aesString.Append(cipherText[i]);
                }
            }

            var key = keyString.ToString();
            var str = aesString.ToString();
            try
            {
                return EasyEncryption.AesThenHmac.SimpleDecryptWithPassword(str, key);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}