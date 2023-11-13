using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialProgramming.View
{
    public static class CLI
    {
        private static string? _path;

        public static void Introduction()
        {
            Console.WriteLine("Здравствуйте, это приложение для подсчета разных математических выражений!!!");
            Console.WriteLine("Всё что вам нужно - это указать путь до файла(он может быть в формате .xml, .json, .txt");
            Console.WriteLine("Так же изначальный файл может быть заархивирован или зашифрован или и то и то");
        }

        public static void UserPathInput()
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
        }
    }
}
