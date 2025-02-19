namespace DAQController
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxSwCommand = new System.Windows.Forms.TextBox();
            this.btnWriteSwitch = new System.Windows.Forms.Button();
            this.physicalChannelComboBox = new System.Windows.Forms.ComboBox();
            this.cbxEnableZTM = new System.Windows.Forms.CheckBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtSCPI = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxSwCommand
            // 
            this.tbxSwCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSwCommand.Location = new System.Drawing.Point(12, 41);
            this.tbxSwCommand.Name = "tbxSwCommand";
            this.tbxSwCommand.Size = new System.Drawing.Size(558, 22);
            this.tbxSwCommand.TabIndex = 0;
            // 
            // btnWriteSwitch
            // 
            this.btnWriteSwitch.Location = new System.Drawing.Point(576, 41);
            this.btnWriteSwitch.Name = "btnWriteSwitch";
            this.btnWriteSwitch.Size = new System.Drawing.Size(112, 22);
            this.btnWriteSwitch.TabIndex = 1;
            this.btnWriteSwitch.Text = "Send Switch";
            this.btnWriteSwitch.UseVisualStyleBackColor = true;
            this.btnWriteSwitch.Click += new System.EventHandler(this.btnWriteSwitch_Click);
            // 
            // physicalChannelComboBox
            // 
            this.physicalChannelComboBox.FormattingEnabled = true;
            this.physicalChannelComboBox.Location = new System.Drawing.Point(12, 11);
            this.physicalChannelComboBox.Name = "physicalChannelComboBox";
            this.physicalChannelComboBox.Size = new System.Drawing.Size(121, 21);
            this.physicalChannelComboBox.TabIndex = 2;
            // 
            // cbxEnableZTM
            // 
            this.cbxEnableZTM.AutoSize = true;
            this.cbxEnableZTM.Location = new System.Drawing.Point(139, 11);
            this.cbxEnableZTM.Name = "cbxEnableZTM";
            this.cbxEnableZTM.Size = new System.Drawing.Size(106, 17);
            this.cbxEnableZTM.TabIndex = 3;
            this.cbxEnableZTM.Text = "Enable ZTM-325";
            this.cbxEnableZTM.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 113);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(676, 445);
            this.dgv.TabIndex = 4;
            this.dgv.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.dgv.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_DragEnter);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(12, 87);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(676, 20);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 564);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1244, 125);
            this.txtLog.TabIndex = 0;
            // 
            // txtSCPI
            // 
            this.txtSCPI.Location = new System.Drawing.Point(694, 87);
            this.txtSCPI.Multiline = true;
            this.txtSCPI.Name = "txtSCPI";
            this.txtSCPI.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSCPI.Size = new System.Drawing.Size(562, 471);
            this.txtSCPI.TabIndex = 0;
            this.txtSCPI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSCPI_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 695);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.cbxEnableZTM);
            this.Controls.Add(this.physicalChannelComboBox);
            this.Controls.Add(this.btnWriteSwitch);
            this.Controls.Add(this.txtSCPI);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.tbxSwCommand);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSwCommand;
        private System.Windows.Forms.Button btnWriteSwitch;
        private System.Windows.Forms.ComboBox physicalChannelComboBox;
        private System.Windows.Forms.CheckBox cbxEnableZTM;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtSCPI;
    }
}

