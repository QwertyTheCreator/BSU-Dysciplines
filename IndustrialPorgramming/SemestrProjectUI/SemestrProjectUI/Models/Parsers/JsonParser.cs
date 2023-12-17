// Ignore Spelling: Json

using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;
using SemesterProjectUI.Services;
using System.Text.Json;

namespace SemesterProjectUI.Models.Parsers
{
    public class JsonParser : IParser
    {
        public EquationsDirector GetExpressions(string path)
        {         
            List<string>? strEquations;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                IEnumerable<string>? expressionContainer = JsonSerializer.Deserialize<IEnumerable<string>>(fs);
                if (expressionContainer is null)
                {
                    throw new Exception("Файл пустой");
                }

                 strEquations = expressionContainer.ToList();
            }
            
            var equations = Converter.FromStringToIBaseEquation(strEquations);

            return new EquationsDirector(equations);
        }
    }
}
