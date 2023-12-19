using NUnit.Framework;
using SemesterProjectUI.Models.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemestrProjectUI.Tests.Models.Equations
{
    public class ScriptEquationsTests
    {
        [Test]
        [TestCase("1 + 2 + 3 + 4", 10)]
        [TestCase("1 - 2 + 2 - 1", 0)]
        [TestCase("2 * 5 - 3", 7)]
        public void Solve(string input, double expectedAnswer)
        {
            var equation = new ScriptEquation(input);

            equation.Solve();
            var answer = equation.Answer;

            Assert.That(answer, Is.EqualTo(expectedAnswer));
        }
    }
}
