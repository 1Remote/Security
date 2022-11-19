using System;
using System.Text;

namespace _1Remote.Security
{
    public static class SimpleStringEncipher
    {
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            string randomKey = KeyGenerator.GetRandomKey();
            string randomKeySalt = KeyGenerator.GetKetWithSalt(randomKey);

            // AES
            var aesText = EasyEncryption.AesThenHmac.SimpleEncryptWithPassword(plainText, randomKeySalt);


            // keyString insert into cipherText
            var cipher = new StringBuilder();
            int m = 0, n = 0;
            for (int i = 0; i < randomKey.Length * 2; i++)
            {
                if (i % 2 == 0)
                {
                    cipher.Append(aesText[m]);
                    m++;
                }
                else
                {
                    cipher.Append(randomKey[n]);
                    n++;
                }
            }

            if (m < aesText.Length)
            {
                for (int i = m; i < aesText.Length; i++)
                {
                    cipher.Append(aesText[i]);
                }
            }

            return cipher.ToString();
        }


        public static string? Decrypt(string cipherText)
        {
            if (cipherText == "")
                return "";
            if (cipherText.Length <= 64)
                return null;

            // keyString from into cipherText
            var keyString = new StringBuilder();
            var aesString = new StringBuilder();
            string rk = KeyGenerator.GetRandomKey();
            {
                int m = 0;
                for (m = 0; m < rk.Length * 2; m++)
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

            var randomKey = keyString.ToString();
            string randomKeySalt = KeyGenerator.GetKetWithSalt(randomKey);
            var str = aesString.ToString();
            try
            {
                return EasyEncryption.AesThenHmac.SimpleDecryptWithPassword(str, randomKeySalt);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}