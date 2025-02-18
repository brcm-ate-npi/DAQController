using IniParser.Model.Configuration;
using NationalInstruments.DAQmx;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DAQController
{
    public partial class Form1 : Form
    {
        private Avago Switch;
        private string sw1_name, sw2_name;
        public Form1()
        {
            InitializeComponent();

            physicalChannelComboBox.Items.AddRange(DaqSystem.Local.Devices);
            if (physicalChannelComboBox.Items.Count > 0)
                physicalChannelComboBox.SelectedIndex = 0;

            var parser = new IniParser.FileIniDataParser();
            parser.Parser.Configuration.SkipInvalidLines = true;
            var tt = parser.ReadFile(@"C:\Users\NI\Desktop\DAQController\file\Bedrosian_HBPAD_NF_Ver0.4_forBallad.txt");
            var mtt = tt["Switching Band"];

            foreach (var _t in mtt)
            {
                Debug.WriteLine($"{_t.KeyName}, {_t.Value}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Switch == null) Switch = new Avago(physicalChannelComboBox.Text);
            Switch.ActivatePath(textBox1.Text);
        }
    }
}