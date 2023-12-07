namespace SemestrProjectUI.ModelsInterfaces
{
    public interface IMathExpression
    {
        void SetVariables(double[] variables);

        Task SolveExpression();

        public int GetVariablesCount();
    }
}
