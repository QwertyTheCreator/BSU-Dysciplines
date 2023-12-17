using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;
using SemesterProjectUI.Services;
using System.Xml;

namespace SemesterProjectUI.Models.Parsers
{
    public class XmlParser : IParser
    {
        public EquationsDirector GetExpressions(string path)
        {
            List<string> expressionContainer = new List<string>();

            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "exp")
                        {
                            expressionContainer.Add(childnode.InnerText);
                        }

                    }
                }
            }
            else
            {
                throw new Exception("XML is empty");
            }

            var equations = Converter.FromStringToIBaseEquation(expressionContainer);

            return new EquationsDirector(equations);
        }
    }
}
