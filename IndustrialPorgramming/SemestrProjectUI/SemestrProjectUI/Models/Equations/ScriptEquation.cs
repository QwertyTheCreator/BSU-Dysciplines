using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace SemesterProjectUI.Models.Equations
{
    public class ScriptEquation : BaseEquation
    {
        public ScriptEquation(string equation) : base(equation)
        {
        }

        public override async void Solve()
        {
            try
            {
                Answer = await CSharpScript.EvaluateAsync<double>(Equation);
            }
            catch
            {
                Answer = Double.PositiveInfinity;
            }
        }
    }
}
