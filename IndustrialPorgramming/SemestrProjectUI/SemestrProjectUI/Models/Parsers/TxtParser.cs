using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;
using SemesterProjectUI.Services;

namespace SemesterProjectUI.Models.Parsers
{
    public class TxtParser : IParser
    {
        public EquationsDirector GetExpressions(string path)
        {
            var expressionsContainer = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    expressionsContainer.Add(line);
                }
            }

            if(expressionsContainer.Count == 0) 
            {
                throw new Exception("Файл пустой");
            }

            var equations = Converter.FromStringToIBaseEquation(expressionsContainer);

            return new EquationsDirector(equations);
        }
    }
}
