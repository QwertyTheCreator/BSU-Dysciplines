using Microsoft.CodeAnalysis.CSharp.Scripting;
using Moq;
using NUnit.Framework;
using SemesterProjectUI.Models.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterProjectUI.Models.EquationDirector;

namespace SemestrProjectUI.Tests.Models.EquationsDirector
{
    public class EquationsDirectorTests
    {
        [Test]
        public void CheckSolver()
        {
            var strEquations = new List<string>()
            {
                "1 + 2 + 3",
                "1 - 2 * 3",
                "555 - 555",
                "22 * 3",
                "13 - 3"
            };

            var answers = new List<double>()
            { 6, -5, 0, 66, 10};

            var equations = new List<IBaseEquation>();
            for(int i = 0; i < strEquations.Count; i++)
            {
                equations.Add(new ScriptEquation(strEquations[i]));
            }

            var director = new SemesterProjectUI.Models.EquationDirector.EquationsDirector(equations);
            director.SolveAll();

            for(int i = 0; i < director.Equations!.Count; i++)
            {
                Assert.That(director.Equations[i].Answer, Is.EqualTo(answers[i]));
            }

        }
    }
}
