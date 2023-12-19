using NUnit.Framework;
using NUnit.Framework.Constraints;
using SemesterProjectUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemestrProjectUI.Tests.Services
{
    public class CrypterTests
    {
        [Test]
        [TestCase("123123123")]
        [TestCase("kadfsjhagsdfkjgadskafsdadfsfadsadfsadfs")]
        [TestCase("")]
        public void TestDataIntegrity(string inputData)
        {
            var directoryInfo = Directory.CreateDirectory("TestBuffer");

            try
            {
                using (var writer = new StreamWriter(directoryInfo.FullName + '/' + "Test.txt"))
                {
                    writer.WriteLine(inputData);
                }

                Crypter.EncryptFile(directoryInfo.FullName + '/' + "Test.txt", directoryInfo.FullName + '/' + "Enc.enc");
                Crypter.DecryptFile(directoryInfo.FullName + '/' + "Enc.enc", directoryInfo.FullName + '/' + "Dec.txt");

                string? output;
                using (var reader = new StreamReader(directoryInfo.FullName + '/' + "Dec.txt"))
                {
                    output = reader.ReadLine();
                }

                Assert.That(output, Is.EqualTo(inputData));
            }
            finally
            {
                Directory.Delete(directoryInfo.FullName, true);
            }
        }

    }
}
