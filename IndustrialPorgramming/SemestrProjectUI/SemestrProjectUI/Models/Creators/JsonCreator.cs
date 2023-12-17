// Ignore Spelling: Json

using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;
using System.Text.Json;
using System.Xml.XPath;

namespace SemesterProjectUI.Models.Creators
{
    public class JsonCreator : ICreator
    {
        public void Create(EquationsDirector equations, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<IBaseEquation>>(fs, equations.Equations!.ToList());
            }
        }
    }
}
