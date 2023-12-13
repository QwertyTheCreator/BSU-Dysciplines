using SemesterProjectUI.Models.EquationDirector;

namespace SemesterProjectUI.Services.ExpressionsServices
{
    public interface IEquationService
    {
        public EquationsDirector GetExpressionsFromFile(string _path);
    }
}
