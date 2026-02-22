using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BussinessLayer
{
    public class clsCountries
    {
        public static string[] FetchCountries()
        {
            DataTable dt = clsCountriesDAC.FetchCountries();
            string[] names = new string[clsCountriesDAC.FetchCountries().Rows.Count];
            for(int i = 0; i < names.Length; i++)
            {
                names[i] = dt.Rows[i][0].ToString();
            }
            return names;
        }
        public static string CountryNamee(int ID)
        {
            return clsCountriesDAC.CountryNamee(ID);
        }
    }
}
