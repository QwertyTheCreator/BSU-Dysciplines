using SemesterProjectUI.Models.Creators;
using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Responses;

namespace SemesterProjectUI.Services.OutputServices
{
    public class OutputService : IOutputService
    {
        public void CreateOutput(EquationsDirector equations, InputForm inputForm)
        {
            var bufferInfo = Directory.CreateDirectory("Buffer");

            string? sourceFile;
            ICreator? creator;
            switch (inputForm.AnswerFormat)
            {
                case OutputFormat.XML:
                    creator = new XmlCreator();
                    sourceFile = bufferInfo.FullName + "/" + inputForm.AnswerName + ".xml";
                    creator.Create(equations, sourceFile);
                    break;

                case OutputFormat.JSON:
                    creator = new JsonCreator();
                    sourceFile = bufferInfo.FullName + "/" + inputForm.AnswerName + ".json";
                    creator.Create(equations, sourceFile);
                    break;

                case OutputFormat.TXT:
                    creator = new TxtCreator();
                    sourceFile = bufferInfo.FullName + "/" + inputForm.AnswerName + ".txt";
                    creator.Create(equations, sourceFile);
                    break;

                default:
                    Directory.Delete(bufferInfo.FullName, true);
                    throw new ArgumentException();
            }

            var newBufferInfo = Directory.CreateDirectory("NewBuffer");
            switch (inputForm.AdditionalOutputFormat)
            {
                case AdditionalOutputFormat.OnlyEnc:

                    Crypter.EncryptFile(sourceFile, inputForm.DirectPath + inputForm.AnswerName + ".enc");
                    break;

                case AdditionalOutputFormat.OnlyZip:
                    Archiver.CompressFile(bufferInfo.FullName, inputForm.DirectPath + inputForm.AnswerName + ".zip");
                    break;

                case AdditionalOutputFormat.ZipThenEnc:
                    string newSource = newBufferInfo.FullName + "/" + inputForm.AnswerName + ".zip";
                    Archiver.CompressFile(bufferInfo.FullName, newSource);
                    Crypter.EncryptFile(newSource, inputForm.DirectPath + inputForm.AnswerName + ".enc");
                    break;

                case AdditionalOutputFormat.EncThenZip:
                    Crypter.EncryptFile(sourceFile, newBufferInfo.FullName + "/" + inputForm.AnswerName + ".enc");
                    Archiver.CompressFile(newBufferInfo.FullName, inputForm.DirectPath + inputForm.AnswerName + ".zip");
                    break;

                case AdditionalOutputFormat.Nothing:
                    File.Copy(sourceFile, inputForm.DirectPath + inputForm.AnswerName, true);
                    break;

                default:
                    Directory.Delete(newBufferInfo.FullName, true);
                    Directory.Delete(bufferInfo.FullName, true);
                    throw new ArgumentException();

            }

            Directory.Delete(newBufferInfo.FullName, true);
            Directory.Delete(bufferInfo.FullName, true);
        }
    }
}
