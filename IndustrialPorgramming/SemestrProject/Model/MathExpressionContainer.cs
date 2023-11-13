using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialProgramming.Model
{
    public class MathExpressionContainer
    {
        public List<MathExpression>? Expressions { get; private set; }

        public MathExpressionContainer(List<string> strExpressions, double[][] variableMatrix)//Add variable matrix
        {
            Expressions = new List<MathExpression>();

            foreach (var strExpression in strExpressions)
            {

                var expression = new MathExpression(strExpression);
                Expressions.Add(expression);
            }

            for (int i = 0; i < strExpressions.Count; i++)
            {
                if (variableMatrix is null)
                {
                    Expressions[i].SetVariables(null);
                    continue;
                }

                Expressions[i].SetVariables(variableMatrix[i]);
            }
        }

        public MathExpressionContainer(List<string> strExpressions)
        {
            Expressions = new List<MathExpression>();

            foreach (var strExpression in strExpressions)
            {

                var expression = new MathExpression(strExpression);
                Expressions.Add(expression);
            }
        }

        public void SetVariables(double[][] variableMatrix)
        {
            for (int i = 0; i < Expressions.Count; i++)
            {
                if (variableMatrix is null)
                {
                    Expressions[i].SetVariables(null);
                    continue;
                }

                Expressions[i].SetVariables(variableMatrix[i]);
            }
        }

        public async Task SolveAll()
        {
            for (int i = 0; i < Expressions.Count; i++)
            {
                await Expressions[i].SolveExpression();
            }
        }
    }
}
