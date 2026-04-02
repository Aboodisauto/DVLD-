//#define Jiji
using System;
using Microsoft.Win32;
namespace ConsoleApp2
{


    internal class Program
    {
        static void Main()
        {
            string keyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\TestingApp";
            string keyName = "TestName";
            string keyValue = "TestValue";

            try
            {
                Registry.SetValue(keyPath, keyName, keyValue, RegistryValueKind.String );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:\n" + ex.Message);
                return;
            }

        }
    }
}
