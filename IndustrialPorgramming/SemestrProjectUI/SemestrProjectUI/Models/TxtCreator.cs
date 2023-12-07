using SemestrProjectUI.ModelsInterfaces;

namespace SemestrProjectUI.Models
{
    public class TxtCreator : ICreator
    {
        public void Create(MathExpressionContainer mathExpressions, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < mathExpressions.Expressions.Count; i++)
                {
                    int index = i + 1;
                    writer.WriteLine(index + ". " + mathExpressions.Expressions[i].EquationWithVariables + " = " + mathExpressions.Expressions[i].Answer);
                }
            }
        }
    }
}
