using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    }
}
