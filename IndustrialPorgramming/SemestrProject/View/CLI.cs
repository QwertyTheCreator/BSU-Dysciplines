using IndustrialProgramming.Controller;
using IndustrialProgramming.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialProgramming.View
{
    public static class CLI
    {
        private static string? _path;
        private static MathExpressionContainer? expressions;

        public static void Introduction()
        {
            Console.WriteLine("Здравствуйте, это приложение для подсчета разных математических выражений!!!");
            Console.WriteLine("Всё что вам нужно - это указать путь до файла(он может быть в формате .xml, .json, .txt");
            Console.WriteLine("Так же изначальный файл может быть заархивирован или зашифрован или и то и то");
        }

        public static async Task UserPathInput()
        {
            Console.WriteLine("\nНапишите путь до файла, который вы бы хотели выбрать");
            Console.WriteLine("(Путь должен быть полным, начиная от диска заканчивая расширением файла)");
            Console.WriteLine(@"Пример: C:\Tests\YourFile.xml");

            Console.WriteLine("");
            Console.WriteLine("Введите путь до нужного файла:");
            string? userFilePath = Console.ReadLine();

            while (true)
            {
                if (File.Exists(userFilePath))
                {
                    _path = userFilePath;
                    break;
                }
                else
                {
                    Console.WriteLine("\nIncorect input, this file, doesn`t exist. Try again");
                    userFilePath = Console.ReadLine();
                }
            }

            ExpressionCather.GetExpressionsFromFile(userFilePath);
            expressions = ExpressionCather.Container;
            if (expressions is null || expressions?.Expressions?.Count == 0)
            {
                Console.WriteLine("Файл пустой или backend косячный");
                throw new ArgumentException();
            }

            VariablesInput();
            await expressions.SolveAll();

            Console.WriteLine("\nЕсли ничего не упало, то вы счастливчик и получение выражения из файла прошло успешно\n");
        }

        private static void VariablesInput()
        {
            double[]?[]? variableMatrix = new double[expressions.Expressions.Count][];
            int iterator = 0;
            foreach (var equation in expressions?.Expressions ?? throw new ArgumentNullException())
            {
                if (equation.GetVariablesCount() > 0)
                {
                    Console.WriteLine("Нужно ввести значение переменных для этого выражения:");
                    Console.WriteLine("Введите столько чисел, сколько переменных в выражении");
                    Console.WriteLine(equation.EquationWithVariables);
                    double[] variablesValues = new double[equation.GetVariablesCount()];

                    for (int i = 0; i < variablesValues.Length; i++)
                    {
                        string? valueStr = Console.ReadLine();
                        if (valueStr is null)
                        {
                            variablesValues[i] = 0;
                            continue;
                        }

                        variablesValues[i] = Double.Parse(valueStr);
                    }

                    variableMatrix[iterator] = variablesValues;
                }
                else
                {
                    variableMatrix[iterator] = null;
                }

                iterator++;
            }

            expressions.SetVariables(variableMatrix);
        }

        public static void UserOutputInput()
        {
            bool isCorrect = false;

            int extencionKey;
            while (!isCorrect)
            {
                Console.WriteLine("Введите в каком формате вы бы хотели получить ответ:");
                Console.WriteLine("1. .txt");
                Console.WriteLine("2. .xml");
                Console.WriteLine("3. .json");

                extencionKey = Int32.Parse(Console.ReadLine());

                switch (extencionKey)
                {
                    case 1:
                        isCorrect = true;
                        break;

                    case 2:
                        isCorrect = true;
                        break;

                    case 3:
                        isCorrect = true;
                        break;

                    default:
                        Console.WriteLine("Ввод неверный, попробуй еще раз)");
                        break;
                }
            }

            isCorrect = false;
            int compresAndDecompesKey;
            while (!isCorrect)
            {
                Console.WriteLine("Выберите в какои формате вы бы хотели получить итоговый ответ:");
                Console.WriteLine("1. Вначале заархивирован, затем зашифрован");
                Console.WriteLine("2. Вначале зашифрован, затем заархивирован");
                Console.WriteLine("3. Только зашифрован");
                Console.WriteLine("4. Только архивирован");
                Console.WriteLine("5. Не производить дополнительных действий");

                compresAndDecompesKey = Int32.Parse(Console.ReadLine());

                switch (compresAndDecompesKey)
                {
                    case 1:
                        isCorrect = true;
                        break;

                    case 2:
                        isCorrect = true;
                        break;

                    case 3:
                        isCorrect = true;
                        break;

                    case 4:
                        isCorrect = true;
                        break;

                    case 5:
                        isCorrect = true;
                        break;

                    default:
                        Console.WriteLine("Ввод неверный, попробуй еще раз)");
                        break;
                }
            }

            link: 
            Console.WriteLine("Введите полный путь до папки в которой вы хотите получить ответ(В конце обязательно должен быть слеш):");
            string? folderPath = Console.ReadLine();

            if(folderPath is null || !Directory.Exists(folderPath))
            {
                Console.WriteLine("Неправильный путь, попробуй еще");
                goto link;
            }
            //Доделать вывод
            
        }
    }
}
