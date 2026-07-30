using BussinessLayer;
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
    public partial class FilterPeople : UserControl
    {
        public int PersonID = -1;
        public event Action<int> FoundPerson;
        protected virtual void OnFoundPerson(int PersonID)
        {
            Action<int> handler = FoundPerson;
            if (handler != null)
            {
                handler(PersonID);
            }
        }
        public FilterPeople()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            peopleInformation1.linkLabel1.Enabled = false;
        }
        private bool CheckForValidality(string Filter,ref int PersonId)
        {
            return int.TryParse(Filter, out PersonId);
        }
        private void FindPersonByID(string Filter)
        {
            if (!CheckForValidality(Filter,ref PersonID))
            {
                MessageBox.Show("Invalid Input type !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FilterTB.Focus();
                return;
            }
            if (!clsPerson.DoesPersonExist(PersonID))
            {
                MessageBox.Show("A person with that id doesn't exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            peopleInformation1.PersonId = PersonID;
            peopleInformation1.linkLabel1.Enabled = true;
            peopleInformation1._LoadData();
        }
        private void FindPersonByNationalNo(string NationalNo)
        {
            if (!clsPerson.DoesPersonExist(NationalNo))
            {
                MessageBox.Show("A person with that id doesn't exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            peopleInformation1.PersonId = PersonID =clsPerson.GetPersonID(NationalNo);
            peopleInformation1.linkLabel1.Enabled = true;
            peopleInformation1._LoadData();
        }
        
        private void Findbtn_Click(object sender, EventArgs e)
        {
            string Filter = comboBox1.SelectedItem.ToString();
            string keyWord = FilterTB.Text.ToString();
            switch (Filter)
            {
                case "PersonID":
                    FindPersonByID(keyWord);
                    break;
                case "NationalNo":
                    FindPersonByNationalNo(keyWord);
                    break;
            }
            if(FoundPerson != null)
                OnFoundPerson(PersonID);
        }
        private void OnCloseAdding(object sender, int PersonID)
        {
            FilterTB.Text = PersonID.ToString();
            this.PersonID = PersonID;
            peopleInformation1.PersonId = PersonID;
            peopleInformation1._LoadData();

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            SaveAddPeople frm = new SaveAddPeople();
            frm.dataBack += OnCloseAdding;
            frm.ShowDialog();
        }

        private void EditInfoLB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new SaveAddPeople(peopleInformation1.PersonId);
            frm.ShowDialog();
            peopleInformation1._LoadData();
        }
        public void LoadPersonData()
        {
            peopleInformation1.PersonId = PersonID;
            FilterTB.Text = PersonID.ToString();
            peopleInformation1._LoadData();
        }

        private void FilterTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Findbtn.PerformClick();
            }
            if(comboBox1.Text == "ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

        }
    }
}
