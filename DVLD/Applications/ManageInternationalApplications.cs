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

namespace DVLD.Applications
{
    public partial class ManageInternationalApplications : Form
    {
        DataTable Applications;
        string[] columns;
        private string[] GetDataTableColumns(DataTable table)
        {
            string[] columns = new string[table.Columns.Count + 1];
            columns[0] = "None";
            foreach (DataColumn col in table.Columns)
            {
                columns[col.Ordinal + 1] = col.ColumnName;
            }
            return columns;
        }
        private void _RefreshApplications()
        {
            Applications = clsInternationalLicense.GetAllInternationalLicenses();
            dataGridView1.DataSource = Applications;
        }
        private void FilterData()
        {
            string SearchValue = maskedTextBox1.Text.ToString();
            string SelectItem = comboBox1.SelectedItem.ToString();
            if (SelectItem == "None")
            {
                Applications.DefaultView.RowFilter = string.Empty;
            }

            if (SelectItem == "Person ID" || SelectItem == "Driver ID" || SelectItem == "Active Licenses")
            {
                if (SearchValue != string.Empty)
                    Applications.DefaultView.RowFilter = $"[{SelectItem}] = {SearchValue}";
                else
                    Applications.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                Applications.DefaultView.RowFilter = $"[{SelectItem}]LIKE '{SearchValue}%'";
            }
        }
        public ManageInternationalApplications()
        {
            InitializeComponent();
            _RefreshApplications();
            comboBox1.DataSource = GetDataTableColumns(Applications);
            comboBox2.DataSource = new[] { "None", "Yes", "No" };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "None":
                    Applications.DefaultView.RowFilter = string.Empty;
                    maskedTextBox1.Visible = false;
                    comboBox2.Visible = false;
                    break;
                case "Is Active":
                    comboBox2.Visible = true;
                    maskedTextBox1.Visible = false;
                    break;
                case "Application ID":
                case "L License ID":
                case "Int License ID":
                case "Driver ID":
                    maskedTextBox1.Mask = "9999999999999999";
                    maskedTextBox1.Visible = true;
                    comboBox2.Visible = false;
                    break;
                default:
                    maskedTextBox1.Mask = "";
                    maskedTextBox1.Visible = true;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox2.SelectedItem.ToString())
            {
                case "None":
                    Applications.DefaultView.RowFilter = string.Empty;
                    break;
                case "Yes":
                    Applications.DefaultView.RowFilter = $"[Is Active] = true";
                    break;
                case "No":
                    Applications.DefaultView.RowFilter = $"[Is Active] = false";
                    break;
                
            }
        }

        private void ManageInternationalApplications_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.InternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshApplications();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int driverID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Driver ID"].Value);
            Form frm = new People.ShowPeopleForm(clsPerson.GetPersonIDByDriverID(driverID));
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.InternationalLicenseForm(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Int License ID"].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.LicenseHistory(clsLocalApplication.GetLocalApplicationIDByLicenseID(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["L License ID"].Value)));
            frm.ShowDialog();
        }
    }
}
