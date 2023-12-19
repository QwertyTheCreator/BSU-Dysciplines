using NUnit.Framework;
using SemesterProjectUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemestrProjectUI.Tests.Services
{
    public class ArchiverTests
    {
        [Test]
        [TestCase("132132132132")]
        [TestCase("")]
        [TestCase("lkdsjfghadfgiljasdfil")]
        [TestCase("Хуй Говно и Муравей")]
        public void TestDataIntegrity(string inputData)
        {
            var directoryInfo = Directory.CreateDirectory("TestBuffer");
            var outputInfo = Directory.CreateDirectory("TestZipBuffer");
            var decompressedInfo = Directory.CreateDirectory("UnzipBuffer");

            try
            {
                using (var writer = new StreamWriter(directoryInfo.FullName + '/' + "Test.txt"))
                {
                    writer.WriteLine(inputData);
                }

                Archiver.CompressFile(directoryInfo.FullName, outputInfo.FullName + '/' + "Zip.zip");
                Archiver.DecompressFile(outputInfo.FullName + '/' + "Zip.zip", decompressedInfo.FullName);

                string? output;
                using (var reader = new StreamReader(decompressedInfo.FullName + '/' + "Test.txt"))
                {
                    output = reader.ReadLine();
                }

                Assert.That(output, Is.EqualTo(inputData));
            }
            finally
            {
                Directory.Delete(directoryInfo.FullName, true);
                Directory.Delete(outputInfo.FullName, true);
                Directory.Delete(decompressedInfo.FullName, true);
            }
        }
    }
}
