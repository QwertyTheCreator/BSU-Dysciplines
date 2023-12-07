using SemestrProjectUI.ModelsInterfaces;

namespace SemestrProjectUI.Models
{
    public class TxtParser : IParser
    {
        public List<string> GetExpressions(string path)
        {
            List<string>? expressionsContainer = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    expressionsContainer.Add(line);
                }
            }

            return expressionsContainer;
        }
    }
}
