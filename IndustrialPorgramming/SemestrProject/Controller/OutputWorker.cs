using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using IndustrialProgramming.Model;
using static System.Net.Mime.MediaTypeNames;

namespace IndustrialProgramming.Controller
{
    public static class OutputWorker
    {
        public static void CreateXmlFile(MathExpressionContainer mathExpressions, string path)
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

        public static void CreateJsonFile(MathExpressionContainer mathExpressions, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<MathExpression>>(fs, mathExpressions.Expressions);
            }
        }

        public static void CreateTxtFile(MathExpressionContainer mathExpressions, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < mathExpressions.Expressions.Count; i++)
                {
                    int index = i + 1;
                    writer.WriteLine(index + ". " + mathExpressions.Expressions[i].EquationWithVariables + " = " + mathExpressions.Expressions[i].Answer);
                }
            }
        }

    }
}

