// Ignore Spelling: Crypter dest

using System.Security.Cryptography;
using System.Text;

namespace SemesterProjectUI.Services
{
    public static class Crypter
    {
        public static void DecryptFile(string sourceFile, string destFile, string keyStr = "Очень секретный ключ", string ivStr = "вектор")
        {
            if (File.Exists(sourceFile))
            {
                using FileStream fileStream = new(sourceFile, FileMode.Open); using Aes aes = Aes.Create();
                var iv = GetIV(ivStr);
                var key = GetKey(keyStr);
                aes.IV = iv; aes.Key = key;
                using CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                using FileStream outStream = new FileStream(destFile, FileMode.Create);
                using BinaryReader decryptReader = new(cryptoStream);
                int tempSize = 10;
                byte[] data;
                while (true)
                {
                    data = decryptReader.ReadBytes(tempSize); if (data.Length == 0)
                    {
                        break;
                    }
                    outStream.Write(data, 0, data.Length);
                }
            }
            else
            {
                throw new Exception("This file doesn`t exist, or you`ve written incorrect path to file");
            }
        }

        private static byte[] GetIV(string ivSectre)
        {
            using MD5 md5 = MD5.Create(); return md5.ComputeHash(Encoding.UTF8.GetBytes(ivSectre));
        }

        private static byte[] GetKey(string key)
        {
            using SHA256 sha256 = SHA256.Create(); return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        public static void EncryptFile(string sourceFile, string outputFile, string key = "Очень секретный ключ", string iv = "вектор")
        {
            if (File.Exists(sourceFile))
            {
                using Aes aes = Aes.Create(); aes.IV = GetIV(iv);
                aes.Key = GetKey(key);
                using FileStream inStream = new FileStream(sourceFile, FileMode.Open); using FileStream outStream = new FileStream(outputFile, FileMode.Create);
                CryptoStream encStream = new CryptoStream(outStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);
                long readTotal = 0;
                int len; int tempSize = 100;
                byte[] bin = new byte[tempSize];
                while (readTotal < inStream.Length)
                {
                    len = inStream.Read(bin, 0, tempSize); encStream.Write(bin, 0, len);
                    readTotal = readTotal + len;
                }
                encStream.Close();
                outStream.Close(); inStream.Close();
            }
            else
            {
                throw new Exception("This file doesn`t exist, or you`ve written incorrect path to file");
            }
        }
    }
}

