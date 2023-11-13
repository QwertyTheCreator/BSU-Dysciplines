using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Industrial_Programming.Controller;
using Industrial_Programming.Exceptions;
using Industrial_Programming.Model;
using IndustrialProgramming.Exceptions;
using IndustrialProgramming.Model;

namespace IndustrialProgramming.Controller
{
    public static class BuisnessLogic
    {
        private static MathExpressionContainer _container;

        private static string _key = "Очень секретный ключ";
        private static string _ivSecret = "вектор";
        public static void GetExpressionsFromFile(string _path)
        {
            string extension = Path.GetExtension(_path);

            switch (extension)
            {
                case ".txt":
                    _container = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromTxt(_path));
                    break;

                case ".json":
                    _container = new MathExpressionContainer(MathExpressionParser.GetExpressionFromJson(_path));
                    break;

                case ".xml":
                    _container = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromXml(_path));
                    break;

                case ".zip":
                    _container = GetContainerFromZip(_path);
                    break;

                case ".enc":
                    break;

                default:
                    throw new FileWorkerException("No such file direction in this program");
                    break;
            }
        }

        private static MathExpressionContainer GetContainerFromZip(string _path)
        {
            string buffer = @"C:\Buffer";
            Directory.CreateDirectory(buffer);
            FileWorker.DecompressFile(_path, buffer);

            string[] directories = Directory.GetDirectories(_path);
            string innerExtension = Path.GetExtension(directories[0]);

            MathExpressionContainer? expressions;

            switch (innerExtension)
            {
                case ".txt":
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromTxt(directories[0]));
                    break;

                case ".json":
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionFromJson(directories[0]));
                    break;

                case ".xml":
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromXml(directories[0]));
                    break;

                case ".enc":
                    expressions = GetExpressionsFromEnc(directories[0]);
                    break;

                default:
                    throw new FileWorkerException("Нет поддерживаемых расширений внутри .zip файла");
            }


            Directory.Delete(buffer);
            return expressions;
        }

        private static MathExpressionContainer GetExpressionsFromEnc(string _path)
        {
            string encBuffer = @"C:\EncBuffer";
            Directory.CreateDirectory(encBuffer);

            FileWorker.DecryptFile(_path, encBuffer + @"\Decrypted.zip", _key, _ivSecret);
        }
    }
}
