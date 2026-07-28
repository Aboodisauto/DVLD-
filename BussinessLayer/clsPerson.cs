using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BussinessLayer
{
    public class clsPerson
    {
        enum enMode  { AddNew, Upate };
        enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public string NationalNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryID { get; set; }
        public clsCountries Country;
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ImagePath { get; set; }
        public int Gender { get; set; }

        public clsPerson()
        {
            ID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNo = "";
            BirthDate = DateTime.Now;
            CountryID = -1;
            Address = "";
            Email = "";
            MobileNo = "";
            ImagePath = "";
            Gender = -1;
        }
        private clsPerson(int ID, string FirstName,string SecondName,string ThirdName,string LastName
            ,string NationalNo, DateTime BirthDate, int CountryID, string Address, string Email, string MobileNo, string ImagePath, int Gender)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.BirthDate = BirthDate;
            this.CountryID = CountryID;
            this.Country = clsCountries.Find(CountryID);
            this.Address = Address;
            this.Email = Email;
            this.MobileNo = MobileNo;
            this.ImagePath = ImagePath;
            this.Gender = Gender;
            Mode = enMode.Upate;
        }
        private bool _AddPerson()
        {
            ID = clsPersonDAC.AddPerson(FirstName, SecondName, ThirdName, LastName, NationalNo, Address, MobileNo, Email, CountryID, Gender, BirthDate, ImagePath);
            return ( ID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonDAC.UpdatePerson(ID, FirstName, SecondName, ThirdName, LastName, NationalNo, Address, MobileNo, Email, CountryID, Gender, BirthDate, ImagePath);
        }
        public static DataTable FetchPeople()
        {
            return clsPersonDAC.FetchPeople();
        }
        public static clsPerson Find(int ID) 
        {
            string Firstname = "", SecondName = "", ThirdName = "", LastName = "";
            string NationalNo = "";
            DateTime BirthDate = DateTime.Now;
            int CountryID = -1;
            string Address = "", Email = "", MobileNo = "",
                ImagePath = "";
                int Gender = -1;
            if(clsPersonDAC.FindPerson(ID, ref Firstname, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref Address, ref MobileNo, ref Email, ref CountryID, ref Gender, ref BirthDate, ref ImagePath))
            {
                return new clsPerson(ID, Firstname, SecondName, ThirdName, LastName, NationalNo, BirthDate, CountryID, Address, Email, MobileNo, ImagePath, Gender);
            }
            return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            string Firstname = "", SecondName = "", ThirdName = "", LastName = "";
            int PersonID = -1;
            DateTime BirthDate = DateTime.Now;
            int CountryID = -1;
            string Address = "", Email = "", MobileNo = "",
                ImagePath = "";
            int Gender = -1;
            if (clsPersonDAC.FindPerson(NationalNo, ref Firstname, ref SecondName, ref ThirdName, ref LastName, ref PersonID, ref Address, ref MobileNo, ref Email, ref CountryID, ref Gender, ref BirthDate, ref ImagePath))
            {
                return new clsPerson(PersonID, Firstname, SecondName, ThirdName, LastName, NationalNo, BirthDate, CountryID, Address, Email, MobileNo, ImagePath, Gender);
            }
            return null;
        }
        public bool Save()
        {
            bool isSaved = false;
            switch (Mode)
            {
                case enMode.AddNew:
                    isSaved = _AddPerson();
                    break;
                case enMode.Upate:
                    isSaved = _UpdatePerson();
                    break;
            }
            return isSaved;
        }
        public static bool DeletePerson(int Id) {
            return clsPersonDAC.DeletePerson(Id);
        }
        public static bool DoesPersonExist(int ID)
        {
            return clsPersonDAC.DoesPersonExist(ID);
        }
        public static bool DoesPersonExist(string NationalNo)
        {
            return clsPersonDAC.DoesPersonExist(NationalNo);
        }
        public static int GetPersonID(string NationalNo)
        {
            return clsPersonDAC.GetPersonID(NationalNo);
        }
        public static string GetPersonNationalNo(int PersonID)
        {
            return clsPersonDAC.GetNationalNo(PersonID);
        }

        public static clsPerson FetchPersonByApplicationID(int ApplicationID)
        {
            clsApplication lApp = clsApplication.Find(ApplicationID);
            if(lApp != null)
            {
                return Find(lApp.ApplicantID);
            }
            return null;
        }

        public static int GetPersonIDByDriverID(int driverID)
        {
            return clsPersonDAC.GetPersonIDByDriverID(driverID);
        }
    }
}
