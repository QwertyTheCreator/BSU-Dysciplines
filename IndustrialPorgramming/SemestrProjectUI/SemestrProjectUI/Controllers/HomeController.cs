using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using SemestrProjectUI.Models;
using SemestrProjectUI.ModelsInterfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SemestrProjectUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string? _inputPath;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View("MyView");
        }

        [HttpGet]
        public ViewResult GetPath()
        {
            return View();
        }

        [HttpPost]
        public ViewResult GetPath(string path)
        {

            if (!System.IO.File.Exists(path))
            {
                return View("ErrorView");
            }

            return View("CreatePath");
        }

        [HttpGet]
        public ViewResult CreatePath()
        {
            return View();
        }

        [HttpPost]
        public ViewResult CreatePath(OutputResponse response)
        {
            ExpressionCather.GetExpressionsFromFile(_inputPath
                ?? throw new ArgumentNullException());

            if (response == null)
            {
                return View();
            }

            CreateAnswerFile(response, ExpressionCather.Container!);

            return View("Thanks");
        }

        private void CreateAnswerFile(OutputResponse response, MathExpressionContainer expressions)
        {
            bool isCorrect = false;

            string buffer = "C:/Buffer";
            string extBuffer = "C:/Buffer";
            Directory.CreateDirectory(buffer);
            buffer += "/";

            int extencionKey;
            string fileName = "Asnwer";
            string? extencion = null;

            ICreator? creator;

            switch (response.fileFormat)
            {
                case Format.TXT:
                    creator = new TxtCreator();
                    isCorrect = true;
                    extencion = ".txt";
                    creator.Create(expressions, buffer + fileName + ".txt");
                    break;

                case Format.XML:
                    isCorrect = true;
                    extencion = ".xml";
                    creator = new XmlCreator();
                    creator.Create(expressions, buffer + fileName + ".xml");
                    break;

                case Format.JSON:
                    isCorrect = true;
                    extencion = ".json";
                    creator = new JsonCreator();
                    creator.Create(expressions, buffer + fileName + ".json");
                    break;

                default:
                    throw new ArgumentNullException();
            }

            string _key = "Очень секретный ключ";
            string _ivSecret = "вектор";
            //Доделать вывод
            string? answerName = response.AnswerName;
            string? folderPath = response.outPath;
            switch (response.format)
            {
                case ZipEncFormat.FirstZipThenEnc:
                    string newBuf = "C:/newBuf";
                    Directory.CreateDirectory(newBuf);
                    FileWorker.CompressFile(extBuffer, newBuf + "/zipped.zip");
                    FileWorker.EncryptFile(newBuf + "/zipped.zip", folderPath + answerName + ".enc", _key, _ivSecret);
                    Directory.Delete(newBuf, true);
                    break;

                case ZipEncFormat.FirstEncThenZip:
                    string newBuffer = "C:/NewBuffer";
                    Directory.CreateDirectory(newBuffer);
                    FileWorker.EncryptFile(buffer + fileName + extencion, newBuffer + "/encrypted.enc", _key, _ivSecret);
                    FileWorker.CompressFile(newBuffer, folderPath + answerName + ".zip");
                    Directory.Delete(newBuffer, true);
                    break;

                case ZipEncFormat.OnlyEnc:
                    FileWorker.EncryptFile(buffer + fileName + extencion, folderPath + answerName + ".enc", _key, _ivSecret);
                    break;

                case ZipEncFormat.OnlyZip:
                    FileWorker.CompressFile(extBuffer, folderPath + answerName + ".zip");
                    break;

                case ZipEncFormat.None:
                    System.IO.File.Copy(buffer + fileName + extencion, folderPath + answerName + extencion);
                    break;

                default:
                    break;
            }

            Directory.Delete(extBuffer, true);
        }
    }
}
