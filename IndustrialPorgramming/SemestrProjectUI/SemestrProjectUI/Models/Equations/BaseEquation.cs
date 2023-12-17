using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SemesterProjectUI.Models.Equations
{
    public abstract class BaseEquation : IBaseEquation
    {
        public string? Equation { get; set; }

        public double? Answer { get; protected set; }

        [JsonIgnore]
        public int VariablesCount { get; private set; }

        [JsonIgnore]
        private readonly Regex _variableRegex;

        public abstract void Solve();

        public BaseEquation(string equation)
        {
            _variableRegex = new Regex(@"\b[A-Za-z_][A-Za-z0-9_]*\b");
            Equation = equation;
            VariablesCount = MatchVariables().Count;
        }

        private MatchCollection MatchVariables()
        {
            return _variableRegex.Matches(Equation!);
        }

        public void SetVariables(List<double>? variables)
        {
            var matches = MatchVariables();

            if (matches.Count > 0)
            {
                if (variables is null)
                {
                    throw new ArgumentNullException();
                }

                for (int i = 0; i < matches.Count; i++)
                {
                    if (i < variables.Count)
                    {
                        Equation = _variableRegex.Replace(Equation!, variables[i].ToString(), 1);
                    }
                    else
                    {
                        throw new ArgumentException("Not enough variables");
                    }
                }
            }
        }
    }
}
