using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Tests
{
    [TestClass()]
    public class HeroTests
    {
        [TestMethod()]
        public void RunTest()
        {
            Hero elf = new Hero(new ElfFactory());
            string result = elf.Run();

            string expected = "Летим";
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void HitTest()
        {
            Hero elf = new Hero(new ElfFactory());
            string result = elf.Hit();

            string expected = "Стреляем из арбалета";
            Assert.AreEqual(expected, result);
        }
    }
}