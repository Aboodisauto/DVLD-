using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    public partial class Traffic_Light : UserControl
    {
        public enum LightState
        {
            Red,
            Orange,
            Green
        }
        private int _RedTime = 10;
        private int _OrangeTime = 5;
        private int _GreenTime = 10;
        private LightState _lightTurn = LightState.Red;
        private LightState _CurrentLight;
        private int _CurrentTime = 2;
        public LightState CurrentLight
        {
            get { return _CurrentLight; }
            set
            {
                _CurrentLight = value;
                switch (_CurrentLight)
                {
                    case LightState.Red:
                        pictureBox1.Image = Resources.Red;
                        label1.Text = _RedTime.ToString();
                        break;
                    case LightState.Orange:
                        pictureBox1.Image = Resources.Orange;
                        label1.Text = _OrangeTime.ToString();
                        break;
                    case LightState.Green:
                        pictureBox1.Image = Resources.Green;
                        label1.Text = _GreenTime.ToString();
                        break;
                }
            }
        }
        
        public int RedTime
        {
            get { return _RedTime; }
            set { _RedTime = value; }
        }
        public int OrangeTime
        {
            get { return _OrangeTime; }
            set { _OrangeTime = value; }
        }
        public int GreenTime
        {
            get { return _GreenTime; }
            set { _GreenTime = value; }

        }
        public void Start()
        {
            CurrentLight = LightState.Red;
            _CurrentTime = _RedTime;
            timer1.Start();
        }
        public class TrafficLightEventArgs : EventArgs
        {
            public LightState NewLight { get; set; }
            public int TimeRemaining { get; set; }
            public TrafficLightEventArgs(LightState newLight, int timeRemaining)
            {
                NewLight = newLight;
                TimeRemaining = timeRemaining;
            }
        }
        public event EventHandler<TrafficLightEventArgs> LightChanged;
        protected virtual void OnLightChanged(LightState newLight, int timeRemaining)
        {
            LightChanged?.Invoke(this, new TrafficLightEventArgs(newLight, timeRemaining));
        }
        public void ChangeLight()
        {
            switch (CurrentLight)
            {
                case LightState.Red:
                    _lightTurn = LightState.Green;
                    CurrentLight = LightState.Orange;
                    _CurrentTime = _OrangeTime;
                    OnLightChanged(CurrentLight, _OrangeTime);
                    break;
                case LightState.Orange:
                    if(_lightTurn == LightState.Green)
                    {
                        CurrentLight = LightState.Green;
                        _CurrentTime = _GreenTime;
                        OnLightChanged(CurrentLight, _GreenTime);
                    }
                    else
                    {
                        CurrentLight = LightState.Red;
                        _CurrentTime = _RedTime;
                        OnLightChanged(CurrentLight, _RedTime);
                    }
                    break;
                case LightState.Green:
                    _lightTurn = LightState.Red;
                    CurrentLight = LightState.Orange;
                    _CurrentTime = _OrangeTime;
                    OnLightChanged(CurrentLight, _OrangeTime);
                    break;
            }
        }
        public Traffic_Light()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = _CurrentTime.ToString();
            if(_CurrentTime == 0)
            {
                ChangeLight();
            }
            else
            {
                --_CurrentTime;
            }
        }
    }
}
