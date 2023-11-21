using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Programming.Interfaces
{
    internal interface IMathExpression
    {
        void SetVariables(double[] variables);

        Task SolveExpression();

        public int GetVariablesCount();
    }
}
