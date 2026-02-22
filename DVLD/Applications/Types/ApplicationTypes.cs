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
    public partial class ApplicationTypes : Form
    {
        private void _RefreshApplicationTypes()
        {
            dataGridView1.DataSource = clsApplicationTypes.FetchAllApplicationTypes();
        }
        private int _GetCounOfTypes()
        {
            return clsTestsTypes.AmountOfTypes();
        }
        public ApplicationTypes()
        {
            InitializeComponent();
            _RefreshApplicationTypes();
            RecordsLB.Text = _GetCounOfTypes().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.Types.EditApplicationType((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreshApplicationTypes();
        }
    }
}
