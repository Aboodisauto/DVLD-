using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Util
{
    public class clsUtil
    {
        public static bool CreateDistinationFolder(string distinationFolder)
        {
            if (!Directory.Exists(distinationFolder))
            {
                try
                {
                    Directory.CreateDirectory(distinationFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public static string GenerateGUIDForFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            return Guid.NewGuid().ToString() + extension;
        }
        public static bool CopyFileToImageFolder(ref string sourceFileName)
        {
            string DistinationFolder = @"C:\DVLDPhotos";
            if (!CreateDistinationFolder(DistinationFolder))
            {
                return false;
            }
            string newFileDistination = Path.Combine(DistinationFolder, GenerateGUIDForFile(sourceFileName));
            try
            {
                File.Copy(sourceFileName, newFileDistination, true);
                sourceFileName = newFileDistination;
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string HashPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert input string to byte array
                byte[] bytes = Encoding.UTF8.GetBytes(Password);

                // Compute hash bytes
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convert byte array to hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public static bool RemeberMe(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD\RememberMe";
            string keyValue = Username + ":" + Password;
            string valueName = "RememberMe";
            try
            {
                Registry.SetValue(keyPath, valueName, keyValue, RegistryValueKind.String);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error While Clearing Data: \n" + ex.Message);
                return false;
            }
            return true;
        }
    }
}
        
