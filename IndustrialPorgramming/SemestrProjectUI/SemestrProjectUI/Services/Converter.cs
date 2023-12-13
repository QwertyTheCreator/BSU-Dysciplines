// Ignore Spelling: Eq

using SemesterProjectUI.Models.Equations;

namespace SemesterProjectUI.Services
{
    public static class Converter
    {
        public static List<BaseEquation> FromStringToBaseEquation(List<string> strEq)
        {
            var equations = new List<BaseEquation>();
            foreach (var str in strEq)
            {
                equations.Add(new ScriptEquation(str));
            }

            return equations;
        }
    }
}
