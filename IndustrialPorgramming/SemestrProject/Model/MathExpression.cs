using Industrial_Programming.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IndustrialProgramming.Model
{
    public class MathExpression : IMathExpression
    {
        public string? EquationWithVariables { get; set; }

        [JsonIgnore]
        private string? EquationWithoutVariables { get; set; }

        [JsonIgnore]
        public List<string>? Variables { get; private set; }

        [JsonIgnore]
        public List<double>? VariableValues { get; set; }

        public double Answer { get; private set; }

        public MathExpression(string expression)
        {
            EquationWithVariables = expression;

            DetectVariables();

        }

        private void DetectVariables()
        {
            Regex regex = new Regex(@"\b[A-Za-z_][A-Za-z0-9_]*\b");

            MatchCollection matches = regex.Matches(EquationWithVariables);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Variables.Add(match.Value);
                }
            }
        }

        private void FillVariablesValues(double[] variablesValues)
        {
            for (int i = 0; i < Variables.Count; i++)
            {
                VariableValues.Add(variablesValues[i]);
            }
        }

        private void CreateEquationWithoutVariables()
        {
            EquationWithoutVariables = EquationWithVariables;
            const string regular = @"\b[A-Za-z_][A-Za-z0-9_]*\b";

            int counter = -1;
            EquationWithoutVariables = Regex.Replace(EquationWithoutVariables,
                regular,
                (match) =>
                {
                    counter++;
                    return VariableValues[counter].ToString();

                });
        }

        public void SetVariables(double[]? variablesValues)
        {
            if (variablesValues is null)
            {
                EquationWithoutVariables = EquationWithVariables;
                return;
            }

            FillVariablesValues(variablesValues);

            CreateEquationWithoutVariables();
        }

        public async Task SolveExpression()
        {
            Answer = await Solve(EquationWithoutVariables);
        }

        private static async Task<double> Solve(string? EquationWithoutVariables)
        {
            return CSharpScript.EvaluateAsync<double>(EquationWithoutVariables).Result;
        }

        public int GetVariablesCount()
        {
            Regex regex = new Regex(@"\b[A-Za-z_][A-Za-z0-9_]*\b");

            MatchCollection matches = regex.Matches(EquationWithVariables);

            return matches.Count;
        }
    }
}
