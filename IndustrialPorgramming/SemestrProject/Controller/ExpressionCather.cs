using IndustrialProgramming.Exceptions;
using IndustrialProgramming.Model;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace IndustrialProgramming.Controller
{
    public static class ExpressionCather
    {
        public static MathExpressionContainer? Container {  get; private set; }

        private static string _key = "Очень секретный ключ";
        private static string _ivSecret = "вектор";
        public static void GetExpressionsFromFile(string _path)
        {
            string extension = Path.GetExtension(_path);

            switch (extension)
            {
                case ".txt":
                    Container = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromTxt(_path));
                    break;

                case ".json":
                    Container = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromJson(_path));
                    break;

                case ".xml":
                    Container = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromXml(_path));
                    break;

                case ".zip":
                    Container = GetContainerFromZip(_path);
                    break;

                case ".enc":
                    Container = GetExpressionsFromEnc(_path);
                    break;

                default:
                    throw new FileWorkerException("No such file direction in this program");
                    
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
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromJson(directories[0]));
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
            if(IsZipEncrypted(encBuffer + @"\Decrypted.zip"))
            {
                return GetContainerFromZip(encBuffer + @"\Decrypted.zip");
            }

            FileWorker.DecryptFile(_path, encBuffer + @"\GetFileExt.txt", _key, _ivSecret);
            
            using var reader = new StreamReader(encBuffer + @"\GetFileExt.txt");
            string fileContent = reader.ReadToEnd();

            MathExpressionContainer expressions;
            string extencion = GetFileExtencion(fileContent);
            switch (extencion)
            {
                case ".txt":
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromTxt(encBuffer + @"\GetFileExt.txt"));
                    break;

                case ".json":
                    FileWorker.DecryptFile(_path, encBuffer + @"\expr.json", _key, _ivSecret);
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromJson(encBuffer + @"\expr.json"));
                    break;

                case ".xml":
                    FileWorker.DecryptFile(_path, encBuffer + @"\expr.xml", _key, _ivSecret);
                    expressions = new MathExpressionContainer(MathExpressionParser.GetExpressionsFromXml(encBuffer + @"\expr.xml"));
                    break;

                default:
                    throw new ArgumentException();
            }


            Directory.Delete(encBuffer);
            return expressions;
        }

        private static bool IsZipEncrypted(string _path)
        {
            bool isZipEnc = true;

            try
            {
                ZipFile.Open(_path, ZipArchiveMode.Read);
            }
            catch
            {
                isZipEnc = false;
            }

            return isZipEnc;
        }

        private static string GetFileExtencion(string fileContent)
        {
            string jsonPattern = @"\[.*?\]";
            Regex jsonRegex = new Regex(jsonPattern);
            if(jsonRegex.IsMatch(fileContent)) 
            {
                return ".json";
            }

            string xmlPattern = @"<?xml version";
            Regex xmlRegex = new Regex(xmlPattern);
            if(xmlRegex.IsMatch(fileContent)) 
            {
                return ".xml";
            }

            return ".txt";
        }
    }
}
