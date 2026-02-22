using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BussinessLayer
{
    public static class clsLicenseClass
    {
        public static List<string> LicenseClasses()
        {
            return clsDriverLicensesClassDAC.getClassesNames();
        }
        public static decimal getClassFees(string ClassName)
        {
            return clsDriverLicensesClassDAC.getLicenseFee(ClassName);
        }
        public static decimal getClassFees(int ClassID)
        {
            return clsDriverLicensesClassDAC.getLicenseFee(ClassID);
        }
        public static short GetClassID(string ClassName)
        {
            return clsDriverLicensesClassDAC.GetlicenseID(ClassName);
        }
        public static string GetClassName(int ClassID)
        {
            return clsDriverLicensesClassDAC.GetClassName(ClassID);
        }
        public static int GetLicensePeriodInYears(int LicenseClassID)
        {
            return clsDriverLicensesClassDAC.GetLicenseValidationPeriod(LicenseClassID);
        }
    }
}
