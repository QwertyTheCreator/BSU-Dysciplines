using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;

namespace SemesterProjectUI.Models.Parsers
{
    public interface IParser
    {
        public EquationsDirector GetExpressions(string path);
    }
}
