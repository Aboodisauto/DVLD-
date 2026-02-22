using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
namespace DVLD.People
{
    public partial class PeopleManagementForm : Form
    {
        DataTable People = clsPerson.FetchPeople();
        string[] columns;


        private string[] GetDataTableColumns(DataTable table)
        {
            string[] columns = new string[table.Columns.Count + 1];
            columns[0] = "None";
            foreach(DataColumn col in table.Columns)
            {
                columns[col.Ordinal + 1] = col.ColumnName;
            }
            return columns;
        }
        
        private void _RefershPeopleList(DataTable People)
        {
            dataGridView1.DataSource = People;
        }
        public PeopleManagementForm()
        {
            InitializeComponent();
            _RefershPeopleList(People);
            columns = GetDataTableColumns(People);
            comboBox1.DataSource = columns;
        }
        private void FilterData()
        {
            string SearchValue = maskedTextBox1.Text.ToString();
            string SelectItem = comboBox1.SelectedItem.ToString();
            if(SelectItem == "None")
            {
                People.DefaultView.RowFilter = string.Empty;
            }

            if (SelectItem == "PersonID")
            {
                if(SearchValue != string.Empty)
                    People.DefaultView.RowFilter = $"{SelectItem} = {SearchValue}";
                else
                    People.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                People.DefaultView.RowFilter = $"{SelectItem} LIKE '{SearchValue}%'";
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Getting the index Number the visibility of the masked text box
            int Index = comboBox1.SelectedIndex;
            if (Index == 0)
            {
                maskedTextBox1.Visible = false;
                return;
            }
            maskedTextBox1.Visible = true;
            //masking the textbox to valdiate the input
            
            switch (Index)
            {
                case 1:
                    maskedTextBox1.Mask = "9999999999999999";
                    break;
                default:
                    maskedTextBox1.Mask = "";
                    break;
            }
            
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void AddPersonbtn_Click(object sender, EventArgs e)
        {
            Form AddSavefrm = new SaveAddPeople(-1);
            AddSavefrm.ShowDialog();
            People = clsPerson.FetchPeople();
            _RefershPeopleList(People);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AddSavefrm = new SaveAddPeople((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            AddSavefrm.ShowDialog();
            People = clsPerson.FetchPeople();
            _RefershPeopleList(People);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            if(MessageBox.Show("Are you sure you want to delete this person ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if(clsPerson.DeletePerson(PersonId))
            {
                MessageBox.Show("Person Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                People = clsPerson.FetchPeople();
                _RefershPeopleList(People);
            }
            else
            {
                MessageBox.Show("Cannot delete this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowPeopleForm((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }
    
    
    }
}
