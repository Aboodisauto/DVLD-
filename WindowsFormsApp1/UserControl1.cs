using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UserControl1 : UserControl
    {
        //public event Action<string> SendTextHandler;
        //protected virtual void OnSendText(string text)
        //{
        //    Action<string> handler = SendTextHandler;
        //    if(handler != null)
        //    {
        //        handler(text);
        //    }
        //}

        public class DataEventArgs : EventArgs
        {
            public string Text { get; set; }
            public DateTime DateNow { get; set; }
            public DataEventArgs(string text, DateTime dateNow)
            {
                Text = text;
                DateNow = dateNow;
            }
        }
        public event EventHandler<DataEventArgs> SendTextHandler;
        public void SendText(string Text)
        {
            SendText(new DataEventArgs(Text,DateTime.Now));
        }
        protected virtual void SendText(DataEventArgs e)
        {
            SendTextHandler?.Invoke(this, e);
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Text = textBox1.Text;
            SendText(Text);
        }
    }
}
