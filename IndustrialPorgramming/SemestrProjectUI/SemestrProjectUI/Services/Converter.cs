// Ignore Spelling: Eq

using SemesterProjectUI.Models.Equations;

namespace SemesterProjectUI.Services
{
    public static class Converter
    {
        public static List<IBaseEquation> FromStringToIBaseEquation(List<string> strEq)
        {
            var equations = new List<IBaseEquation>();
            foreach (var str in strEq)
            {
                equations.Add(new ScriptEquation(str));
            }

            return equations;
        }
    }
}
