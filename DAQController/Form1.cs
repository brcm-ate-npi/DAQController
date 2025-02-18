using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DAQController
{
    public partial class Form1 : Form
    {
        private Avago SwDIO;
        public ZTM SwZTM;

        private string sw1_name;
        private BindingSource bindingSource;
        private List<KeyValuePair<string, string>> originalData;

        public Form1()
        {
            InitializeComponent();

            physicalChannelComboBox.Items.AddRange(DaqSystem.Local.Devices);
            if (physicalChannelComboBox.Items.Count > 0)
                physicalChannelComboBox.SelectedIndex = 0;

            originalData = new List<KeyValuePair<string, string>>();
            bindingSource = new BindingSource();
            bindingSource.DataSource = new BindingList<KeyValuePair<string, string>>(originalData);
            dgv.DataSource = bindingSource;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.CellDoubleClick += dgv_CellDoubleClick;

            EnableDoubleBuffering(dgv);
        }

        private void EnableDoubleBuffering(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgv, new object[] { true });
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string value = dgv.Rows[e.RowIndex].Cells[1].Value?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    Clipboard.SetText(value);
                    tbxSwCommand.Text = value;
                    btnWriteSwitch_Click(null, null);
                }
            }
        }

        private void btnWriteSwitch_Click(object sender, EventArgs e)
        {
            if (cbxEnableZTM.Checked && SwZTM == null) SwZTM = new ZTM();

            if (SwDIO == null || sw1_name != physicalChannelComboBox.Text)
            {
                SwDIO = new Avago(physicalChannelComboBox.Text);
                sw1_name = physicalChannelComboBox.Text;
            }

            var _path = tbxSwCommand.Text.Split(new char[] { '@' });
            foreach (var _sw in _path)
            {
                if (_sw.ToUpper().Contains("STATE"))
                {
                    if (SwZTM?.ActivatePath(_sw) ?? false)
                        AppendLog($"ZTM Writed, {_sw}");
                    else
                        AppendLog($"ZTM Write failed");
                }
                else if (_sw.Contains(";"))
                {
                    SwDIO.ActivatePath(_sw);
                    AppendLog($"DIO Writed, {_sw}");
                }
                else
                {
                    SwDIO.ActivatePath(_sw, false);
                    AppendLog($"DIO Writed, {_sw}");
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtFilter.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filterText))
            {
                bindingSource.DataSource = new BindingList<KeyValuePair<string, string>>(originalData);
            }
            else
            {
                var filteredData = originalData.Where(kvp => kvp.Key.ToLower().Contains(filterText)).ToList();
                bindingSource.DataSource = new BindingList<KeyValuePair<string, string>>(filteredData);
            }
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string filePath = files[0];
                LoadFileData(filePath);
            }
        }

        private void LoadFileData(string filePath)
        {
            try
            {
                var newData = new List<KeyValuePair<string, string>>();

                if (filePath.ToLower().EndsWith("txt"))
                {
                    var parser = new IniParser.FileIniDataParser();
                    parser.Parser.Configuration.SkipInvalidLines = true;
                    var nf_switch = parser.ReadFile(filePath);

                    foreach (var sect in new string[] { "Switching Band", "Switching Band_HotNF" })
                    {
                        if (nf_switch.Sections.Any(t => t.SectionName.Equals(sect)))
                        {
                            var sw_section = nf_switch[sect];

                            foreach (var kv in sw_section)
                            {
                                newData.Add(new KeyValuePair<string, string>(kv.KeyName.Trim(), kv.Value.Trim()));
                            }
                        }
                    }
                }

                if (newData.Count > 0)
                {
                    AppendLog($"Switch commandpool added, {Path.GetFileName(filePath)}");

                    if (originalData.Count > 0) originalData.AddRange(newData);
                    else originalData = newData;

                    bindingSource.DataSource = new BindingList<KeyValuePair<string, string>>(originalData);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => AppendLog(message)));
            }
            else
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
        }
    }
}