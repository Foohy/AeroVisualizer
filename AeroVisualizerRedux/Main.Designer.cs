namespace AeroVisualizerRedux
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.numUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDeviceSelect = new System.Windows.Forms.ComboBox();
            this.checkSmooth = new System.Windows.Forms.CheckBox();
            this.trackSampleNum = new System.Windows.Forms.TrackBar();
            this.labelSampleInterval = new System.Windows.Forms.Label();
            this.numSampleInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSampleAmount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSampleNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelSampleAmount);
            this.groupBox1.Controls.Add(this.numSampleInterval);
            this.groupBox1.Controls.Add(this.labelSampleInterval);
            this.groupBox1.Controls.Add(this.trackSampleNum);
            this.groupBox1.Controls.Add(this.checkSmooth);
            this.groupBox1.Controls.Add(this.numUpdateInterval);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdvanced.Location = new System.Drawing.Point(213, 253);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(75, 23);
            this.btnAdvanced.TabIndex = 12;
            this.btnAdvanced.Text = "Edit rules...";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // numUpdateInterval
            // 
            this.numUpdateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpdateInterval.Location = new System.Drawing.Point(127, 19);
            this.numUpdateInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpdateInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpdateInterval.Name = "numUpdateInterval";
            this.numUpdateInterval.Size = new System.Drawing.Size(149, 20);
            this.numUpdateInterval.TabIndex = 11;
            this.numUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpdateInterval.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Update interval (ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device:";
            // 
            // comboDeviceSelect
            // 
            this.comboDeviceSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDeviceSelect.FormattingEnabled = true;
            this.comboDeviceSelect.Location = new System.Drawing.Point(62, 6);
            this.comboDeviceSelect.Name = "comboDeviceSelect";
            this.comboDeviceSelect.Size = new System.Drawing.Size(226, 21);
            this.comboDeviceSelect.TabIndex = 2;
            this.comboDeviceSelect.SelectedIndexChanged += new System.EventHandler(this.comboDeviceSelect_SelectedIndexChanged);
            // 
            // checkSmooth
            // 
            this.checkSmooth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkSmooth.AutoSize = true;
            this.checkSmooth.Checked = true;
            this.checkSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSmooth.Location = new System.Drawing.Point(6, 56);
            this.checkSmooth.Name = "checkSmooth";
            this.checkSmooth.Size = new System.Drawing.Size(110, 17);
            this.checkSmooth.TabIndex = 12;
            this.checkSmooth.Text = "Smooth FFT Data";
            this.checkSmooth.UseVisualStyleBackColor = true;
            this.checkSmooth.CheckedChanged += new System.EventHandler(this.checkSmooth_CheckedChanged);
            // 
            // trackSampleNum
            // 
            this.trackSampleNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackSampleNum.Location = new System.Drawing.Point(9, 109);
            this.trackSampleNum.Maximum = 15;
            this.trackSampleNum.Minimum = 1;
            this.trackSampleNum.Name = "trackSampleNum";
            this.trackSampleNum.Size = new System.Drawing.Size(261, 45);
            this.trackSampleNum.TabIndex = 13;
            this.trackSampleNum.Value = 8;
            this.trackSampleNum.Scroll += new System.EventHandler(this.trackSampleNum_Scroll);
            // 
            // labelSampleInterval
            // 
            this.labelSampleInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSampleInterval.AutoSize = true;
            this.labelSampleInterval.Location = new System.Drawing.Point(6, 162);
            this.labelSampleInterval.Name = "labelSampleInterval";
            this.labelSampleInterval.Size = new System.Drawing.Size(105, 13);
            this.labelSampleInterval.TabIndex = 15;
            this.labelSampleInterval.Text = "Sample Interval (ms):";
            // 
            // numSampleInterval
            // 
            this.numSampleInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numSampleInterval.Location = new System.Drawing.Point(128, 160);
            this.numSampleInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSampleInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSampleInterval.Name = "numSampleInterval";
            this.numSampleInterval.Size = new System.Drawing.Size(142, 20);
            this.numSampleInterval.TabIndex = 16;
            this.numSampleInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSampleInterval.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numSampleInterval.ValueChanged += new System.EventHandler(this.numSampleInterval_ValueChanged);
            // 
            // labelSampleAmount
            // 
            this.labelSampleAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSampleAmount.AutoSize = true;
            this.labelSampleAmount.Location = new System.Drawing.Point(6, 89);
            this.labelSampleAmount.Name = "labelSampleAmount";
            this.labelSampleAmount.Size = new System.Drawing.Size(96, 13);
            this.labelSampleAmount.TabIndex = 17;
            this.labelSampleAmount.Text = "Sample Amount (1)";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 288);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.comboDeviceSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Aero Visualizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSampleNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUpdateInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboDeviceSelect;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.CheckBox checkSmooth;
        private System.Windows.Forms.TrackBar trackSampleNum;
        private System.Windows.Forms.Label labelSampleAmount;
        private System.Windows.Forms.NumericUpDown numSampleInterval;
        private System.Windows.Forms.Label labelSampleInterval;
    }
}

