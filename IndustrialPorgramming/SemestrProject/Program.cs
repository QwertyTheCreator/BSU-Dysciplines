using IndustrialProgramming.Controller;
using System;
using System.IO.Compression;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using IndustrialProgramming.View;

namespace IndustrialProgramming
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            string key = "Очень секретный ключ";
            string ivSecret = "вектор";

            ////FileWorker.EncryptFile("C:\\TestsForIndustrialProgramming\\TXTtest.txt", "C:\\TestsForIndustrialProgramming\\TxtDecrypted.enc", key, ivSecret);
            //FileWorker.DecryptFile("C:\\TestsForIndustrialProgramming\\TxtDecrypted.enc", "C:\\TestsForIndustrialProgramming\\TxtDecrypted.xml", key, ivSecret);

            CLI.Introduction();
            while (true)
            {
                CLI.UserPathInput();
            }

            //string path = @"D:\Temp\expr.txt";
            // FileWorker.EncryptFile(path, @"D:\Temp\expr.enc", key, ivSecret);
        }
    }
}