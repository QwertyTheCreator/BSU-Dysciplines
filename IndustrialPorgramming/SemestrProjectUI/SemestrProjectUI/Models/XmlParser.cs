using SemestrProjectUI.Exceptions;
using SemestrProjectUI.ModelsInterfaces;
using System.Xml;

namespace SemestrProjectUI.Models
{
    public class XmlParser : IParser
    {
        public List<string> GetExpressions(string path)
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
                throw new FileWorkerException("XML is empty");
            }

            return expressionContainer ?? throw new FileWorkerException("XML file is empty, or incorrect");
        }
    }
}
