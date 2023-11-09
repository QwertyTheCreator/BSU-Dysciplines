using Industrial_Programming.Controller;
using Industrial_Programming.Model;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Text.RegularExpressions;

namespace Industrial_Programming
{
    public class Program
    {
        static void Main(string[] args)
        {
            string zipFilePath = @"C:\Users\Admin\source\TestForIndustrialProgramming\ZIPtest.zip";
            string xmlFilePath = @"C:\Users\Admin\source\TestForIndustrialProgramming\XMLtest.xml";
            string txtFilePath = @"C:\Users\Admin\source\TestForIndustrialProgramming\TXTtest.txt";
            string jsonFilePath = @"C:\Users\Admin\source\TestForIndustrialProgramming\ToCreateZip\";

            FileWorker.CompressFile(jsonFilePath, zipFilePath);
        }
    }
}