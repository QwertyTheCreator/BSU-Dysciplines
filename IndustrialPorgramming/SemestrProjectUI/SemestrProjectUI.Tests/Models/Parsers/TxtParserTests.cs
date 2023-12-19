using Microsoft.AspNetCore.Components.Forms;
using NUnit.Framework;
using SemesterProjectUI.Models.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SemestrProjectUI.Tests.Models.Parsers
{
    public class TxtParserTests
    {
        [Test]
        [TestCase("1 + 2 - 1")]
        [TestCase("-2 * 23")]
        [TestCase("-5 + - -23")]
        public void GetEquationsTest(string? input)
        {
            var directoryInfo = Directory.CreateDirectory("NewBuffer");

            try
            {
                using (var writer = new StreamWriter(directoryInfo.FullName + '/' + "Test.txt"))
                {
                    writer.WriteLine(input);
                }

                IParser parser = new TxtParser();
                var equations = parser.GetExpressions(directoryInfo.FullName + '/' + "Test.txt");

                for(int i = 0; i < equations.Equations!.Count; i++)
                {
                    Assert.That(equations.Equations[i].Equation, Is.EqualTo(input));
                }
            }
            finally
            {
                Directory.Delete(directoryInfo.FullName, true);
            }
        }
    }
}
