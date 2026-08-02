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

namespace DVLD.Tests.Types
{
    public partial class TestTypesForm : Form
    {
        private void _RefreshApplicationTypes()
        {
            dataGridView1.DataSource = clsTestsTypes.FetchAllApplicationTypes();
        }
        private int _GetCounOfTypes()
        {
            return dataGridView1.Rows.Count - 1; // Subtract 1 to exclude the header row
        }
        public TestTypesForm()
        {
            InitializeComponent();
            _RefreshApplicationTypes();
            RecordsLB.Text = _GetCounOfTypes().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new Tests.Types.EditTestTypes((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreshApplicationTypes();
        }
    }
}
