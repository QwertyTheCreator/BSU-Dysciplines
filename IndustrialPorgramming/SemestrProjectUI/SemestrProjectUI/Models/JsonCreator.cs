using SemestrProjectUI.ModelsInterfaces;
using System.Text.Json;

namespace SemestrProjectUI.Models
{
    public class JsonCreator : ICreator
    {
        public void Create(MathExpressionContainer mathExpressions, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<MathExpression>>(fs, mathExpressions.Expressions);
            }
        }
    }
}
