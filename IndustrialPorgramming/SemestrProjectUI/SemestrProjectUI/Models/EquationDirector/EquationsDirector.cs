using SemesterProjectUI.Models.Equations;

namespace SemesterProjectUI.Models.EquationDirector
{
    public class EquationsDirector
    {
        public List<IBaseEquation>? Equations { get; set; }

        public EquationsDirector(List<IBaseEquation> equations)
        {
            if (equations is null) throw new ArgumentNullException(nameof(equations));

            this.Equations = equations;
        }

        public void SetVariables(List<List<double>?>? variablesMatrix)
        {
            if (variablesMatrix is null)
            {
                return;
            }

            for (var i = 0; i < variablesMatrix.Count; i++)
            {
                if (i < Equations!.Count)
                {
                    Equations[i].SetVariables(variablesMatrix[i]);
                }
                else
                {
                    break;
                }
            }
        }

        public void SolveAll()
        {
            for (var i = 0; i < Equations!.Count; i++)
            {
                Equations[i].Solve();
            }
        }

        public int GetVariablesCount()
        {
            int count = 0;
            foreach (var equation in Equations!)
            {
                count += equation.VariablesCount;
            }

            return count;
        }
    }
}
