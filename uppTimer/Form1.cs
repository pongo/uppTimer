namespace uppTimer
{
    using System;
    using System.Windows.Forms;

    public sealed partial class Form1 : Form
    {
        private readonly Config config = new Config();

        private readonly Timer timer = new Timer();
        private TimeSpan elapsedTime;
        private int seconds;
        private TimerState timerState;

        public Form1()
        {
            this.InitializeComponent();

            this.config.Load();
            this.Text = string.Format("{0}", this.config.TimerName);
            this.labelTotalTime.Text = Config.GetTimeString(this.config.TotalTime);
            this.labelTotalTime.Visible = true;

            this.labelTime.Text = string.Empty;
            this.labelTime.Visible = true;

            this.timer.Interval = 1000;
            this.timer.Tick += this.Tick;
        }

        private enum TimerState
        {
            Stopped = 0,
            Started = 1, 
            Paused = 2
        }

        private void Tick(object sender, EventArgs e)
        {
            this.elapsedTime += TimeSpan.FromSeconds(1);

            this.seconds++;
            if (this.seconds >= 60)
            {
                this.config.TotalTime += TimeSpan.FromSeconds(60);
                this.config.Save();
                this.seconds = 0;
            }

            this.labelTime.Text = Config.GetTimeString(this.elapsedTime, true);
            this.labelTotalTime.Text = Config.GetTimeString(this.config.TotalTime);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            switch (this.timerState)
            {
                case TimerState.Stopped:
                    this.elapsedTime = TimeSpan.FromSeconds(0);
                    this.timer.Start();
                    this.labelTime.Text = Config.GetTimeString(TimeSpan.FromSeconds(0));
                    this.buttonStart.Text = @"Pause";
                    this.timerState = TimerState.Started;
                    this.buttonStop.Enabled = true;
                    break;
                case TimerState.Started:
                    this.timer.Stop();
                    this.buttonStart.Text = @"Start";
                    this.config.Save();
                    this.timerState = TimerState.Paused;
                    break;
                case TimerState.Paused:
                    this.timer.Start();
                    this.buttonStart.Text = @"Pause";
                    this.timerState = TimerState.Started;
                    break;
                default:
                    break;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.config.Save();
            this.labelTotalTime.Text = Config.GetTimeString(this.config.TotalTime);
            this.buttonStart.Text = @"Start";
            this.timerState = TimerState.Stopped;
            this.buttonStop.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();
        }
    }
}