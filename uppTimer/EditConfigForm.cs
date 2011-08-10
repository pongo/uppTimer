namespace uppTimer
{
    using System;
    using System.Windows.Forms;

    public partial class EditConfigForm : Form
    {
        public EditConfigForm()
        {
            InitializeComponent();

            numericUpDownHours.Maximum = int.MaxValue;
            numericUpDownMinutes.Maximum = int.MaxValue;

            this.Save = false;
        }

        public Config Config { get; set; }
        public bool Save { get; private set; }
        public string TimerName { get; private set; }
        public int Hours { get; private set; }
        public int Minutes { get; private set; }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            this.TimerName = textBoxTimeName.Text;
            this.Hours = (int)numericUpDownHours.Value;
            this.Minutes = (int)numericUpDownMinutes.Value;
            this.Save = true;
            this.Close();
        }

        private void buttonCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            this.Save = false;
            this.Close();
        }

        private void EditConfigForm_Load(object sender, EventArgs e)
        {
            textBoxTimeName.Text = this.Config.TimerName;
            numericUpDownHours.Value = this.Config.Hours;
            numericUpDownMinutes.Value = this.Config.Minutes;
        }
    }
}
