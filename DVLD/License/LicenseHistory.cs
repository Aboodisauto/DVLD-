using BusinessLayer;
using BussinessLayer;
using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class LicenseHistory : Form
    {
        clsPerson person;
        int ApplicationID;
        private void _LoadPersonHistory()
        {
            dataGridView1.DataSource = clsLicense.FetchLicensesForPerson(clsDriver.GetDriverIDByPersonID(person.ID));
            dataGridView2.DataSource = clsInternationalLicense.GetAllInternationalLicenseForDriver(clsDriver.GetDriverIDByPersonID(person.ID));
        }
        public LicenseHistory(int ApplicationID)
        {
            InitializeComponent();
            this.ApplicationID = ApplicationID;
            person = clsPerson.FetchPersonByApplicationID(ApplicationID);
            filterPeople1.PersonID = person.ID;
            filterPeople1.LoadPersonData();
            filterPeople1.Enabled = false;
            _LoadPersonHistory();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.ShowLicenseForm(ApplicationID);
                        frm.ShowDialog();
        }
    }
}
