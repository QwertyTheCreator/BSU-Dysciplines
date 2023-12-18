using SemesterProjectUI.Models.EquationDirector;

namespace SemesterProjectUI.Models.Responses
{
    public class VariableResponse
    {
        public EquationsDirector? equations { get; set; }

        public List<List<double>?>? variablesValues { get; set; }

        public VariableResponse() 
        {
        }

        public VariableResponse(EquationsDirector equations)
        {
            this.equations = equations;

            variablesValues = new List<List<double>?>(equations.Equations!.Count());
            for (int i = 0; i < equations.Equations!.Count(); i++)
            {
                if (equations!.Equations![i].VariablesCount != 0)
                {
                    variablesValues.Add(new List<double>(equations!.Equations![i].VariablesCount));

                    for(int j = 0; j < equations!.Equations![i].VariablesCount; j++)
                    {
                        variablesValues[i]?.Add(0);
                    }
                }
            }
        }

        public void CreateAnswer()
        {
            equations!.SetVariables(variablesValues);
            equations.SolveAll();
        }
    }
}
