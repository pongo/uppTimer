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
        private DateTime startTime;

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

        private void Tick(object sender, EventArgs e)
        {
            this.elapsedTime = DateTime.Now - this.startTime;

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
            this.startTime = DateTime.Now;
            this.timer.Start();
            this.labelTime.Text = Config.GetTimeString(TimeSpan.FromSeconds(0));

            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.config.Save();
            this.labelTotalTime.Text = Config.GetTimeString(this.config.TotalTime);

            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();
        }
    }
}