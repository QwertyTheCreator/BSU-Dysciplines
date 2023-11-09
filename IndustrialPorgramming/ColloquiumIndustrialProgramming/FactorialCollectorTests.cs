using Microsoft.VisualStudio.TestTools.UnitTesting;
using FactorialColloquium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace FactorialColloquium.Tests
{
    [TestClass()]
    public class FactorialCollectorTests
    {
        [TestMethod()]
        public void GetFactorialRowTest() // Add other tests the same way
        {
            long testNumber = 5;

            List<long> factorials = FactorialCollector.GetFactorialRow(testNumber).ToList();
            
            StringBuilder factorialStr = new StringBuilder();
            foreach (var factorial in factorials) 
            {
                factorialStr.Append(factorial.ToString() + " ");
            }

            string rightStr = "1 2 6 24 120 ";

            Assert.AreEqual(rightStr, factorialStr.ToString());
        }
    }
}