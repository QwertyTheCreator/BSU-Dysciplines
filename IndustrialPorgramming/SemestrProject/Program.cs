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
            //string key = "Очень секретный ключ";
            //string ivSecret = "вектор";

            ////FileWorker.EncryptFile("C:\\TestsForIndustrialProgramming\\TXTtest.txt", "C:\\TestsForIndustrialProgramming\\TxtDecrypted.enc", key, ivSecret);
            //FileWorker.DecryptFile("C:\\TestsForIndustrialProgramming\\TxtDecrypted.enc", "C:\\TestsForIndustrialProgramming\\TxtDecrypted.xml", key, ivSecret);

            CLI.Introduction();
            while (true)
            {
                await CLI.UserPathInput();
                CLI.UserOutputInput();
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine("Выберите нужную опцию:");
                Console.WriteLine("1. Продолжить");
                Console.WriteLine("2. Завержить программу");
                string temp = Console.ReadLine();
                int key = Int32.Parse(temp);
                if(key == 1)
                {
                    Console.Clear();
                }
                else if(key == 2)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неправильный ввод я буду считать как то что вы хотите еще раз");
                }
            }

            //string path = @"D:\Temp\expr.txt";
            // FileWorker.EncryptFile(path, @"D:\Temp\expr.enc", key, ivSecret);
        }
    }
}