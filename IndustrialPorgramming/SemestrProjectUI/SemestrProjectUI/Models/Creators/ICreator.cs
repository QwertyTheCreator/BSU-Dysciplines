using SemesterProjectUI.Models.EquationDirector;

namespace SemesterProjectUI.Models.Creators
{
    public interface ICreator
    {
        public void Create(EquationsDirector equations, string path);
    }
}
