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

        public IActionResult Index()
        {
            Console.WriteLine("MyView");
            return View("MyView");
        }

        [HttpGet]
        public IActionResult GetPath()
        {
            Console.WriteLine("GetPathView");
            return View();
        }

        [HttpPost]
        public IActionResult GetPath(OutputResponse? outputResponse)
        {

            if (!System.IO.File.Exists(outputResponse?.StarterPath))
            {
                return View("ErrorView");
            }

            _inputPath = outputResponse?.StarterPath;

            return View("CreatePath");
        }

        [HttpGet]
        public IActionResult CreatePath()
        {
            Console.WriteLine("CreatePathView");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePath(OutputResponse response)
        {
            ExpressionCather.GetExpressionsFromFile(response.StarterPath
                ?? throw new ArgumentNullException());

            if (response == null)
            {
                return View();
            }

            await CreateAnswerFile(response, ExpressionCather.Container!);

            return View("Thanks");
        }

        private async Task CreateAnswerFile(OutputResponse response, MathExpressionContainer expressions)
        {
            await expressions.SolveAll();
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
            string? folderPath = response.DestinationPath;
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
                    System.IO.File.Copy(buffer + fileName + extencion, folderPath + answerName + extencion);
                    break;
            }

            Directory.Delete(extBuffer, true);
        }
    }
}
