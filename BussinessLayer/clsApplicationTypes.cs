using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsApplicationTypes
    {
        public int ID = -1;
        public string Title;
        public double Fees;

        // Private constructor so only the Find method can create instances
        private clsApplicationTypes(int ID, string Title, double fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = fees;
        }

        public static clsApplicationTypes Find(int ID)
        {
            string Title = "";
            double fees = -1;

            // Updated to use the new clsApplicationTypeDAC
            if (clsApplicationTypesDAC.FindApplicationType(ID, ref Title, ref fees))
            {
                return new clsApplicationTypes(ID, Title, fees);
            }
            return null;
        }

        public static DataTable FetchAllApplicationTypes()
        {
            return clsApplicationTypesDAC.GetApplicationTypes();
        }

        public static int AmountOfTypes()
        {
            // Updated to match the method name in the DAC
            return clsApplicationTypesDAC.GetNumberOfApplicationTypes();
        }

        public bool Save()
        {
            // Updated to call UpdateApplicationType
            return clsApplicationTypesDAC.UpdateApplicationType(ID, Title, Fees);
        }
        public static string GetApplicationTitle(int ID)
        {
            return clsApplicationTypesDAC.GetApplicationTypeName(ID);
        }
        public static double GetApplicationFees(int ID)
        {
            return clsApplicationTypesDAC.GetApplicationTypeFees(ID);
        }
    }
}