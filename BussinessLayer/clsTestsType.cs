using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public  class clsTestsTypes
    {
        public int ID = -1;
        public string Title;
        public string Description;
        public double Fees;
        private clsTestsTypes(int ID, string Title, string Description ,double fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = fees;
        }
        public static clsTestsTypes Find(int ID)
        {
            string Title = "", Description = "";
            double fees = -1;
            if(clsTestsTypeDAC.FindApplicationType(ID, ref Title,ref Description, ref fees))
            {
                return new clsTestsTypes(ID,Title,Description,fees);
            }
            return null;
        }

        public static DataTable FetchAllApplicationTypes()
        {
            return clsTestsTypeDAC.GetTestTypes();
        }
        public static int AmountOfTypes()
        {
            return clsTestsTypeDAC.GetNumberOfTypes();
        }
        public bool Save()
        {
            return clsTestsTypeDAC.UpdateType(ID, Title, Description, Fees);
        }
        public static double GetTestTypeFees(int ID)
        {
            return clsTestsTypeDAC.getTestTypeFees(ID);
        }
        public static double GetRetakeTestFees()
        {
            return clsTestsTypeDAC.getRetakeTestFees();
        }
    }
}
