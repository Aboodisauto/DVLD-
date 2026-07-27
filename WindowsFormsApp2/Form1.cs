using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void poolControl1_TimerStopped(object sender, PoolControl.TimerStoppedEventArgs e)
        {
            MessageBox.Show($"Timer stopped for table: {e.TableName}\n" +
                            $"Rate per hour: {e.RatePerHour}\n" +
                            $"Elapsed time: {e.ElapsedTime}\n" +
                            $"Total amount: {e.TotalAmount}");
        }

        private void poolControl5_Load(object sender, EventArgs e)
        {

        }

        private void poolControl4_TimerStopped(object sender, PoolControl.TimerStoppedEventArgs e)
        {
            MessageBox.Show($"Timer stopped for table: {e.TableName}\n" +
                            $"Rate per hour: {e.RatePerHour}\n" +
                            $"Elapsed time: {e.ElapsedTime}\n" +
                            $"Total amount: {e.TotalAmount}");
        }
    }
}
