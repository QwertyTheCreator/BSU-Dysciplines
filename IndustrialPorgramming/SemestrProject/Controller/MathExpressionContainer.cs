using Industrial_Programming.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Programming.Controller
{
    public class MathExpressionContainer
    {
        List<MathExpression>? Expressions {  get; set; }

        public MathExpressionContainer(List<string> strExpressions, ) 
        {
            foreach (var strExpression in strExpressions) 
            {
                var expression = new MathExpression(strExpression);
                Expressions.Add(expression);
            }
        }
    }
}
