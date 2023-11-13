using IndustrialProgramming.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace IndustrialProgramming.Controller
{
    public static class MathExpressionParser
    {
        public static List<string> GetExpressionsFromTxt(string path)
        {
            List<string>? expressionsContainer = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    expressionsContainer.Add(line);
                }
            }

            return expressionsContainer;
        }

        public static List<string> GetExpressionsFromXml(string path)
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

        public static List<string> GetExpressionsFromJson(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                IEnumerable<string>? expressionContainer = JsonSerializer.Deserialize<IEnumerable<string>>(fs);
                if (expressionContainer is null)
                {
                    throw new FileWorkerException("Expressions container is null GetExpressionFormJson");
                }

                return expressionContainer.ToList();
            }
        }
    }
}
