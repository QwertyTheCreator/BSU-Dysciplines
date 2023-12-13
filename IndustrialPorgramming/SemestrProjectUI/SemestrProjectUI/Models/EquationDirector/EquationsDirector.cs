using SemesterProjectUI.Models.Equations;

namespace SemesterProjectUI.Models.EquationDirector
{
    public class EquationsDirector
    {
        private List<BaseEquation>? _equations;

        public IEnumerable<BaseEquation> Equations
        {
            get
            { 
                return _equations!; 
            }

            private set => Equations = value;
        }

        public EquationsDirector(List<BaseEquation> equations)
        {
            if (equations is null) throw new ArgumentNullException(nameof(equations));

            this._equations = equations;
        }

        public void SetVariables(List<List<double>?> variablesMatrix)
        {
            for(var i = 0; i < variablesMatrix.Count; i++)
            {
                if(i < _equations!.Count)
                {
                    _equations[i].SetVariables(variablesMatrix[i]);
                }
                else
                {
                    break;
                }
            }
        }

        public void SolveAll()
        {
            for (var i = 0; i < _equations!.Count; i++)
            {
                _equations[i].Solve();
            }
        }
    }
}
