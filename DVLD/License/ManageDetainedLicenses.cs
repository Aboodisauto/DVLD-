using BusinessLayer;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.License
{
    public partial class ManageDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;
        private void _RefreshDetainedLicenses()
        {
            _dtDetainedLicenses  = clsLicense.GetAllDetainedLicenses();
            dataGridView1.DataSource = _dtDetainedLicenses;
        }
        string[] Columns = { "None", "Detain ID", "Is Released", "National No", "Full Name", "Release Application ID"};
        public ManageDetainedLicenses()
        {
            InitializeComponent();
            cbFilterBy.DataSource = Columns;
            comboBox1.DataSource = new string[] {"All", "Yes", "No"};
            _RefreshDetainedLicenses();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new License.ReleaseLicense(-1);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new License.DetainLicense();
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.GetPersonIDByDriverID(clsLicense.Find((int)dataGridView1.SelectedRows[0].Cells["L.ID"].Value).DriverID);
            Form frm = new People.ShowPeopleForm(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsLicense.Find((int)dataGridView1.SelectedRows[0].Cells["L.ID"].Value).ApplicationID;
            Form frm = new License.ShowLicenseForm(ApplicationID);
            frm.ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsLicense.Find((int)dataGridView1.SelectedRows[0].Cells["L.ID"].Value).ApplicationID;
            Form frm = new License.LicenseHistory(ApplicationID);
            frm.ShowDialog();
            _RefreshDetainedLicenses();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtDetainedLicenses.DefaultView.RowFilter = "";

            if (cbFilterBy.Text == "None")
            {
                maskedTextBox1.Visible = false;
                comboBox1.Visible = false;
                return;
            }

            if (cbFilterBy.Text == "Is Released")
            {
                maskedTextBox1.Visible = false;
                comboBox1.Visible = true;
                comboBox1.Focus();
                comboBox1.SelectedIndex = 0; // Default to 'All'
            }
            else
            {
                maskedTextBox1.Visible = true;
                comboBox1.Visible = false;
                maskedTextBox1.Clear();
                maskedTextBox1.Focus();
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            string filterValue = maskedTextBox1.Text.Trim();

            // Map the dropdown selection to your actual DataTable column names
            switch (cbFilterBy.Text)
            {
                case "Detain ID": filterColumn = "D.ID"; break;
                case "National No": filterColumn = "N No"; break;
                case "Full Name": filterColumn = "Full Name"; break;
                case "Release Application ID": filterColumn = "Release App ID"; break;
                default: filterColumn = "None"; break;
            }

            if (filterValue == "" || filterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                return;
            }

            // Use RowFilter based on data type
            if (filterColumn == "D.ID" || filterColumn == "Release App ID")
            {
                // Numeric filter (Exact match)
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, filterValue);
            }
            else
            {
                // String filter (Starts with...)
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, filterValue);
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterColumn = "Is Released";
            string filterValue = comboBox1.Text;

            switch (filterValue)
            {
                case "All":
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    // Filter for true (1)
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = 1", filterColumn);
                    break;
                case "No":
                    // Filter for false (0)
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = 0", filterColumn);
                    break;
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.SelectedRows[0].Cells["L.ID"].Value;
            Form frm = new License.ReleaseLicense(LicenseID);
            frm.ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool isReleased = (bool)dataGridView1.SelectedRows[0].Cells["Is Released"].Value;
            if (!isReleased)
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
                return;
            }
            releaseDetainedLicenseToolStripMenuItem.Enabled = false;
        }
    }
}
