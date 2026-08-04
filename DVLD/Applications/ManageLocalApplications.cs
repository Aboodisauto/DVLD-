using BusinessLayer;
using BussinessLayer;
using DVLD.Properties;
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
    public partial class ManageLocalApplications : Form
    {
        string[] comboBoxFilters = { "None", "LocalApplicationID", "NationalNo", "FullName", "Status" };
        string[] statusBoxFilters = { "Completed", "OnGoing", "Cancelled" };
        DataTable Applications  = clsApplication.FetchLocalApplications();
        private void _RefreshLocalApplications(DataTable Applications)
        {
            dataGridView1.DataSource = Applications;
        }
        public ManageLocalApplications()
        {
            InitializeComponent();
            comboBox1.DataSource = comboBoxFilters;
            comboBox2.DataSource = statusBoxFilters;
            _RefreshLocalApplications(Applications);
            Applications.DefaultView.RowFilter = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keyWord = comboBox1.SelectedItem.ToString();
            switch (keyWord)
            {
                case "None":
                    maskedTextBox1.Visible = false;
                    comboBox2.Visible = false;
                    break;
                case "LocalApplicationID":
                    maskedTextBox1.Visible = true;
                    comboBox2.Visible = false;
                    maskedTextBox1.Mask = "999999";
                    break;
                case "NationalNo":
                    maskedTextBox1.Visible = true;
                    maskedTextBox1.Mask = string.Empty;
                    comboBox2.Visible = false;
                    break;
                case "FullName":
                    maskedTextBox1.Visible = true;
                    maskedTextBox1.Mask = string.Empty;
                    comboBox2.Visible = false;
                    break;
                case "Status":
                    comboBox2.Visible = true;
                    maskedTextBox1.Visible = false;
                    break;
            }

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private void _FilterBasedOnStatusBox()
        {
            Applications.DefaultView.RowFilter = $"Status = '{comboBox2.SelectedItem.ToString()}'";
        }
        private void _FilterBasedOnMaskedBox()
        {
            if (maskedTextBox1.Text == string.Empty)
                Applications.DefaultView.RowFilter = string.Empty;
            if(int.TryParse(maskedTextBox1.Text, out int value) && comboBox1.SelectedItem.ToString() == "ApplicationID")
            {
                Applications.DefaultView.RowFilter = $"{comboBox1.SelectedItem.ToString()} = {maskedTextBox1.Text.ToString()}";
            }
            else
            {
                Applications.DefaultView.RowFilter = $"{comboBox1.SelectedItem.ToString()} LIKE '{maskedTextBox1.Text.ToString()}%'";
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterBasedOnStatusBox();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            _FilterBasedOnMaskedBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.SaveAddApplication(-1);
            frm.ShowDialog();
            Applications = clsApplication.FetchLocalApplications();
            _RefreshLocalApplications(Applications);
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationfrm frm = new ShowApplicationfrm((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalApplicationID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            clsLocalApplication tmApp = clsLocalApplication.Find(LocalApplicationID);
            if (MessageBox.Show($"Are you sure you want to cancel Application {tmApp.LocalApplicationID}", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            if (clsApplication.Cancel(tmApp.ApplicationID))
            {
                MessageBox.Show("Application Was Cancelled SuccessFully !", "Success", MessageBoxButtons.OK,MessageBoxIcon.None);
                Applications = clsApplication.FetchLocalApplications();
                _RefreshLocalApplications(Applications);
            }
            else
            {
                MessageBox.Show("Oops there's an error occured !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void contextMenuOptions(int PassCount)
        {
            switch (PassCount)
            {
                case 1:
                    {
                        visionTestToolStripMenuItem.Enabled = false;
                        writtenTestToolStripMenuItem.Enabled = true;
                        streetTestToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseToolStripMenuItem.Enabled = false;
                        editToolStripMenuItem.Enabled = true;
                        cancelToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = true;
                        break;
                    }
                case 2:
                    {
                        visionTestToolStripMenuItem.Enabled = false;
                        writtenTestToolStripMenuItem.Enabled = false;
                        streetTestToolStripMenuItem.Enabled = true;
                        showLicenseToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseToolStripMenuItem.Enabled = false;
                        editToolStripMenuItem.Enabled = true;
                        cancelToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = true;
                        break;
                    }
                case 3:
                    {
                        writtenTestToolStripMenuItem.Enabled = false;
                        streetTestToolStripMenuItem.Enabled = false;
                        visionTestToolStripMenuItem.Enabled = false;
                        editToolStripMenuItem.Enabled = false;
                        cancelToolStripMenuItem.Enabled = false;
                        deleteToolStripMenuItem.Enabled = false;
                        string Status = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
                        if (Status == "OnGoing")
                        {
                            issueDrivingLicenseToolStripMenuItem.Enabled = true;
                            showLicenseToolStripMenuItem.Enabled = false;
                            break;
                        }
                        issueDrivingLicenseToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = true;
                        break;
                    }
                default:
                    {
                        visionTestToolStripMenuItem.Enabled = true;
                        writtenTestToolStripMenuItem.Enabled = false;
                        streetTestToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseToolStripMenuItem.Enabled = false;
                        editToolStripMenuItem.Enabled = true;
                        cancelToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = true;
                        break;
                    }
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int PassedCount = (int)dataGridView1.SelectedRows[0].Cells["PassedCount"].Value;
            string status = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            if (status == "Cancelled")
            {
                visionTestToolStripMenuItem.Enabled = false;
                writtenTestToolStripMenuItem.Enabled = false;
                streetTestToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                cancelToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            contextMenuOptions(PassedCount);
        }
        private void _LoadTestTypeForm(int TestTypeID)
        {
            int LocalApplicationID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form frm = new Tests.Sechdule_Test_Appointment(LocalApplicationID, TestTypeID);
            frm.ShowDialog();
            Applications = clsLocalApplication.FetchLocalApplications(); _RefreshLocalApplications(Applications);
        }
        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LoadTestTypeForm(1);
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LoadTestTypeForm(2);
        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LoadTestTypeForm(3);
        }
        
        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalApplicationID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form frm = new License.IssueLicenseForm(LocalApplicationID);
            frm.ShowDialog();
            Applications = clsLocalApplication.FetchLocalApplications();
            _RefreshLocalApplications(Applications);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            
            if (MessageBox.Show($"Are you sure you want to delete Application {LID}", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            if (!clsLocalApplication.Delete(LID)){
                MessageBox.Show("There Was An Error Deleteing This Application, Propely Associated with other applications", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Application Deleted SuccessFuly !", "Success");
            Applications = clsLocalApplication.FetchLocalApplications();
            _RefreshLocalApplications(Applications);

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form frm = new License.ShowLicenseForm(clsApplication.GetApplicationID(LID));
            frm.ShowDialog();
        }

        private void showPersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form frm = new License.LicenseHistory(LID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAddApplication saveAddApplication = new SaveAddApplication((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            saveAddApplication.ShowDialog();
        }
    }
}
