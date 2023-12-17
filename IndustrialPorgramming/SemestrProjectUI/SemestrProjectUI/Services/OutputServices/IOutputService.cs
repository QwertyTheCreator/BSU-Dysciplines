using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Responses;

namespace SemesterProjectUI.Services.OutputServices
{
    public interface IOutputService
    {
        void CreateOutput(EquationsDirector equations, InputForm inputForm);
    }
}