using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1Remote.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Remote.Security.Tests
{
    [TestClass()]
    public class SimpleStringEncipherTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            _1Remote.Security.Config.SetSalt("abc");
            var cipherText1 = _1Remote.Security.SimpleStringEncipher.Encrypt("hellO");
            {
                var cipherText2 = _1Remote.Security.SimpleStringEncipher.Encrypt("hellO");
                var cipherText3 = _1Remote.Security.SimpleStringEncipher.Encrypt("");
                var cipherText4 = _1Remote.Security.SimpleStringEncipher.Encrypt("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");

                Assert.AreNotEqual(cipherText1, cipherText2);

                var p1 = _1Remote.Security.SimpleStringEncipher.Decrypt(cipherText1);
                var p2 = _1Remote.Security.SimpleStringEncipher.Decrypt(cipherText2);
                var p3 = _1Remote.Security.SimpleStringEncipher.Decrypt(cipherText3);
                var p4 = _1Remote.Security.SimpleStringEncipher.Decrypt(cipherText4);

                Assert.AreEqual(p1, "hellO");
                Assert.AreEqual(p1, p2);
                Assert.IsTrue(p3 == "");
                Assert.IsTrue(p4 == "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            }

            _1Remote.Security.Config.SetSalt("def");
            {
                var p1 = _1Remote.Security.SimpleStringEncipher.Decrypt(cipherText1);
                Assert.IsTrue(p1 == null);
                var cipherText100 = _1Remote.Security.SimpleStringEncipher.Encrypt("hello");
                Assert.IsTrue(_1Remote.Security.SimpleStringEncipher.Decrypt(cipherText100) == "hello");
            }
        }
    }
}