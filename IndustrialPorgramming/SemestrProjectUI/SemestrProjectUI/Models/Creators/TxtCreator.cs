using SemesterProjectUI.Models.EquationDirector;

namespace SemesterProjectUI.Models.Creators
{
    public class TxtCreator : ICreator
    {
        public void Create(EquationsDirector equations, string path)
        {
            var expressions = equations.Equations!.ToList();

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < expressions.Count; i++)
                {
                    writer.WriteLine(expressions[i].Equation + " = " + expressions[i].Answer);
                }
            }
        }
    }
}
