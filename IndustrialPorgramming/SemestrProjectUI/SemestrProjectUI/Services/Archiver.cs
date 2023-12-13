using System.IO.Compression;

namespace SemesterProjectUI.Services
{
    public static class Archiver
    {
        public static void DecompressFile(string nameOfZip, string targetFolder)
        {
            try
            {
                ZipFile.ExtractToDirectory(nameOfZip, targetFolder);
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("This file doesn`t exist, or you`ve written incorrect path to file");
            }
            catch (FileNotFoundException)
            {
                throw new Exception("This file doesn`t exist, or you`ve written incorrect path to file");
            }
        }

        public static void CompressFile(string sourceFolder, string newZipFile) // Source должен указывать на папку котоурую надо заархивировать
        {                                                                       // newZip создает новую папку, если таковой нет, то выкидывает exception
            try
            {
                ZipFile.CreateFromDirectory(sourceFolder, newZipFile);
            }
            catch
            {
                throw new Exception("This file doesn`t exist, or you`ve written incorrect path to file");
            }
        }
    }
}
