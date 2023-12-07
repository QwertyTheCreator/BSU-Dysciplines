using SemestrProjectUI.ModelsInterfaces;
using System.Xml.Linq;

namespace SemestrProjectUI.Models
{
    public class XmlCreator : ICreator
    {
        public void Create(MathExpressionContainer mathExpressions, string path)
        {
            XDocument xdoc = new XDocument();
            XElement expressions = new XElement("expressions");
            for (int i = 0; i < mathExpressions.Expressions.Count; i++)
            {
                var exprElem = new XElement("equation");
                var exprAtr = new XAttribute("index", i + 1);

                var expr = new XElement("exp", mathExpressions.Expressions[i].EquationWithVariables);
                var exprAnswer = new XElement("Answer", mathExpressions.Expressions[i].Answer);

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
