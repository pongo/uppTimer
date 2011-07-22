using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uppTimer
{
    public partial class Form1 : Form
    {
        private readonly Config _config = new Config();
        //private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly Timer _timer = new Timer();
        private DateTime _startTime;
        private TimeSpan _elapsedTime;
        private int _seconds;
        
        public Form1()
        {
            InitializeComponent();

            _config.Load();
            base.Text = string.Format("{0}", _config.TimerName);
            labelTotalTime.Text = Config.GetTimeString(_config.TotalTime);
            labelTotalTime.Visible = true;

            labelTime.Text = string.Empty;
            labelTime.Visible = true;

            _timer.Interval = 1000;
            _timer.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            _elapsedTime = DateTime.Now - _startTime;

            _seconds++;
            if (_seconds >= 60)
            {
                _config.TotalTime += TimeSpan.FromSeconds(60);
                _config.Save();
                _seconds = 0;
            }

            labelTime.Text = Config.GetTimeString(_elapsedTime, true);
            labelTotalTime.Text = Config.GetTimeString(_config.TotalTime);
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            _startTime = DateTime.Now;
            _timer.Start();
            labelTime.Text = Config.GetTimeString(TimeSpan.FromSeconds(0));

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            _config.Save();
            labelTotalTime.Text = Config.GetTimeString(_config.TotalTime);

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
        }
    }
}
