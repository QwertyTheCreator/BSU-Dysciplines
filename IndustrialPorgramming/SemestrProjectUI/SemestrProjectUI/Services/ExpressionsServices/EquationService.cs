using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Parsers;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace SemesterProjectUI.Services.ExpressionsServices
{
    public class EquationService : IEquationService
    {
        public EquationsDirector GetExpressionsFromFile(string _path)
        {
            string extension = Path.GetExtension(_path);

            IParser? parser;
            switch (extension)
            {
                case ".txt":
                    parser = new TxtParser();
                    return parser.GetExpressions(_path);

                case ".json":
                    parser = new JsonParser();
                    return parser.GetExpressions(_path);

                case ".xml":
                    parser = new XmlParser();
                    return parser.GetExpressions(_path);

                case ".zip":
                    return GetContainerFromZip(_path);

                case ".enc":
                    return GetExpressionsFromEnc(_path);

                default:
                    throw new Exception("No such file direction in this program");

            }
        }

        private EquationsDirector GetContainerFromZip(string _path)
        {
            string buffer = @"C:\Buffer";
            try
            {
                Directory.CreateDirectory(buffer);
                Archiver.DecompressFile(_path, buffer);

                string[] directories = Directory.GetFiles(buffer);
                string innerExtension = Path.GetExtension(directories[0]);

                EquationsDirector? expressions;

                IParser? parser;
                switch (innerExtension)
                {
                    case ".txt":
                        parser = new TxtParser();
                        expressions = parser.GetExpressions(directories[0]);
                        break;

                    case ".json":
                        parser = new JsonParser();
                        expressions = parser.GetExpressions(directories[0]);
                        break;

                    case ".xml":
                        parser = new XmlParser();
                        expressions = parser.GetExpressions(directories[0]);
                        break;

                    case ".enc":
                        expressions = GetExpressionsFromEnc(directories[0]);
                        break;

                    default:
                        throw new Exception("Нет поддерживаемых расширений внутри .zip файла");
                }

                return expressions;
            }
            finally
            {
                Directory.Delete(buffer, true);
            }

        }

        private EquationsDirector GetExpressionsFromEnc(string _path)
        {
            string encBuffer = @"C:\EncBuffer";

            Directory.CreateDirectory(encBuffer);

            Crypter.DecryptFile(_path, encBuffer + @"\Decrypted.zip");
            if (IsZipEncrypted(encBuffer + @"\Decrypted.zip"))
            {
                var result = GetContainerFromZip(encBuffer + @"\Decrypted.zip");
                Directory.Delete(encBuffer, true);
                return result;
            }

            Crypter.DecryptFile(_path, encBuffer + @"\GetFileExt.txt");

            using var reader = new StreamReader(encBuffer + @"\GetFileExt.txt");
            string fileContent = reader.ReadToEnd();

            EquationsDirector expressions;
            string extencion = GetFileExtencion(fileContent);

            IParser parser;
            switch (extencion)
            {
                case ".txt":
                    parser = new TxtParser();
                    expressions = parser.GetExpressions(encBuffer + @"\GetFileExt.txt");
                    break;

                case ".json":
                    Crypter.DecryptFile(_path, encBuffer + @"\expr.json");
                    parser = new JsonParser();
                    expressions = parser.GetExpressions(encBuffer + @"\expr.json");
                    break;

                case ".xml":
                    Crypter.DecryptFile(_path, encBuffer + @"\expr.xml");
                    parser = new XmlParser();
                    expressions = parser.GetExpressions(encBuffer + @"\expr.xml");
                    break;

                default:
                    throw new ArgumentException();
            }

            Directory.Delete(encBuffer);
            return expressions;
        }

        private bool IsZipEncrypted(string _path)
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

        private string GetFileExtencion(string fileContent)
        {
            string jsonPattern = @"\[.*?\]";
            Regex jsonRegex = new Regex(jsonPattern);
            if (jsonRegex.IsMatch(fileContent))
            {
                return ".json";
            }

            string xmlPattern = @"<?xml version";
            Regex xmlRegex = new Regex(xmlPattern);
            if (xmlRegex.IsMatch(fileContent))
            {
                return ".xml";
            }

            return ".txt";
        }
    }
}
