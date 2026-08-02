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

namespace DVLD.Applications.Types
{
    public partial class EditApplicationType : Form
    {
        int ID = -1;
        clsApplicationTypes type;
        private void _LoadData()
        {
            IDlb.Text = ID.ToString();
            TitleTB.Text = type.Title;
            FeesTB.Text = type.Fees.ToString();
        }
        public EditApplicationType(int ID)
        {
            InitializeComponent();
            this.ID = ID;
            type = clsApplicationTypes.Find(ID);
            _LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadDataIntoType()
        {
            type.Title = TitleTB.Text;
            type.Fees = Convert.ToDouble(FeesTB.Text);
        }
        private bool CheckTitle()
        {
            return (TitleTB.Text != string.Empty);
        }
        private bool CheckFees()
        {
            return (FeesTB.Text != string.Empty);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!CheckTitle()) 
            {
                TitleTB.Focus();
                errorProvider1.SetError(TitleTB, "Please Fill This Box");
                return;
            }
            if (!CheckFees())
            {
                FeesTB.Focus();
                errorProvider1.SetError(FeesTB, "Please Fill This Box");
                return;
            }
            _LoadDataIntoType();
            if (type.Save())
            {
                MessageBox.Show("Type Changed Successfuly !", "Success",MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("There Was an Error Please Check The Logs !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
