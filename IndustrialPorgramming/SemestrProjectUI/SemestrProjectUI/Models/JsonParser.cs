using SemestrProjectUI.Exceptions;
using SemestrProjectUI.ModelsInterfaces;
using System.Text.Json;

namespace SemestrProjectUI.Models
{
    public class JsonParser : IParser
    {
        public List<string> GetExpressions(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                IEnumerable<string>? expressionContainer = JsonSerializer.Deserialize<IEnumerable<string>>(fs);
                if (expressionContainer is null)
                {
                    throw new FileWorkerException("Expressions container is null GetExpressionFormJson");
                }

                return expressionContainer.ToList();
            }
        }
    }
}
