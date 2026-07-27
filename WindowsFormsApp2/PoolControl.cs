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
    public partial class PoolControl : UserControl
    {
        private static int InstanceCount = 0;
        public class TimerStoppedEventArgs : EventArgs
        {
            public string TableName { get; set; }
            public float RatePerHour { get; set; }
            public TimeSpan ElapsedTime { get; set; }
            public float TotalAmount { get; set; }
            public TimerStoppedEventArgs(float ratePerHour, TimeSpan elapsedTime, float totalAmount, string tableName)
            {
                RatePerHour = ratePerHour;
                ElapsedTime = elapsedTime;
                TotalAmount = totalAmount;
                TableName = tableName;
            }
        }
        public event EventHandler<TimerStoppedEventArgs> TimerStopped;
        public void StopTimer(float ratePerHour, TimeSpan elapsedTime, float totalAmount, string tableName)
        {
            timer1.Stop();
            _ElapsedTime = TimeSpan.Zero;
            label1.Text = "00:00:00";
            TimerStopped?.Invoke(this, new TimerStoppedEventArgs(ratePerHour, elapsedTime, totalAmount, tableName));
        }
        private string _TableName = "Table " + InstanceCount;
        [Category("Table Specifics"), Description("The name of the table associated with this control.")]
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; groupbox.Text = value; }
        }
        private float _RatePerHour;
        [Category("Table Specifics"), Description("The rate per hour for this table.")]
        public float RatePerHour
        {
            get { return _RatePerHour; }
            set { _RatePerHour = value; }
        }
        private TimeSpan _ElapsedTime = TimeSpan.Zero;

        public PoolControl()
        {
            InitializeComponent();
            label1.Text = "00:00:00";
            InstanceCount++;
            groupbox.Text = _TableName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            { 
                timer1.Stop();
                button1.Text = "Start";
            }
            else
            {
                timer1.Start();
                button1.Text = "Pause";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = _ElapsedTime.ToString(@"hh\:mm\:ss");
            _ElapsedTime = _ElapsedTime.Add(TimeSpan.FromSeconds(1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopTimer(_RatePerHour, _ElapsedTime, (float)_ElapsedTime.TotalHours * _RatePerHour * 1.0f, _TableName);
        }
    }
}
