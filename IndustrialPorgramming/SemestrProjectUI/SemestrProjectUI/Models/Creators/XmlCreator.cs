using SemesterProjectUI.Models.EquationDirector;
using System.Xml.Linq;

namespace SemesterProjectUI.Models.Creators
{
    public class XmlCreator : ICreator
    {
        public void Create(EquationsDirector equations, string path)
        {
            var baseExpressions = equations.Equations!.ToList();

            XDocument xdoc = new XDocument();
            XElement expressions = new XElement("expressions");
            for (int i = 0; i < baseExpressions.Count; i++)
            {
                var exprElem = new XElement("equation");
                var exprAtr = new XAttribute("index", i + 1);

                var expr = new XElement("exp", baseExpressions[i].Equation);
                var exprAnswer = new XElement("Answer", baseExpressions[i].Answer);

                exprElem.Add(exprAtr);
                exprElem.Add(expr);
                exprElem.Add(exprAnswer);

                expressions.Add(exprElem);
            }

            xdoc.Add(expressions);
            xdoc.Save(path);
        }
    }
}
