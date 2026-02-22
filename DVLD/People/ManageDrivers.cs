using BusinessLayer;
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
    public partial class ManageDrivers : Form
    {
        DataTable Drivers = clsDriver.GetAllDrivers();
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
        private void _RefreshDrivers()
        {
            Drivers = clsDriver.GetAllDrivers();
            dataGridView1.DataSource = Drivers;
        }
        private void FilterData()
        {
            string SearchValue = maskedTextBox1.Text.ToString();
            string SelectItem = comboBox1.SelectedItem.ToString();
            if (SelectItem == "None")
            {
                Drivers.DefaultView.RowFilter = string.Empty;
            }

            if (SelectItem == "Person ID" || SelectItem == "Driver ID" || SelectItem == "Active Licenses")
            {
                if (SearchValue != string.Empty)
                    Drivers.DefaultView.RowFilter = $"[{SelectItem}] = {SearchValue}";
                else
                    Drivers.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                Drivers.DefaultView.RowFilter = $"[{SelectItem}]LIKE '{SearchValue}%'";
            }
        }
        public ManageDrivers()
        {
            InitializeComponent();
            _RefreshDrivers();
            comboBox1.DataSource = GetDataTableColumns(Drivers);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "None":
                    Drivers.DefaultView.RowFilter = string.Empty;
                    maskedTextBox1.Visible = false;
                    break;
                case "Active Licenses":
                case "Person ID":
                case "Driver ID":
                    maskedTextBox1.Mask = "9999999999999999";
                    maskedTextBox1.Visible = true;
                    break;
                default:
                    maskedTextBox1.Mask = "";
                    maskedTextBox1.Visible = true;
                    break;
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }
    }
}
