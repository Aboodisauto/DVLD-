using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class ShowPeopleForm : Form
    {
        public ShowPeopleForm(int ID)
        {
            InitializeComponent();
            peopleInformation1.PersonId = ID;
            peopleInformation1._LoadData();
        }
    }
}
