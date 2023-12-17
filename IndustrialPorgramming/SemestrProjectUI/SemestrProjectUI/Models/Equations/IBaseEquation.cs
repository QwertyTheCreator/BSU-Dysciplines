namespace SemesterProjectUI.Models.Equations
{
    public interface IBaseEquation
    {
        double? Answer { get; }
        string? Equation { get; set; }
        int VariablesCount { get; }

        void SetVariables(List<double>? variables);

        void Solve();
    }
}