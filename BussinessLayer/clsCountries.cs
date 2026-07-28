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
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public clsCountries()
        {
            ID = -1;
            Name = "";
        }
        public clsCountries(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public static clsCountries Find(int ID)
        {
            string Name = "";
            if (clsCountriesDAC.FindCountry(ID, ref Name))
            {
                return new clsCountries(ID, Name);
            }
            return null;
        }
        
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
