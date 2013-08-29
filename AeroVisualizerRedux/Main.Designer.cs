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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numMultiplier = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.labelColor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.slideScrollSpeed = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numSampleCutoff = new System.Windows.Forms.NumericUpDown();
            this.labelHue = new System.Windows.Forms.Label();
            this.sliderHue = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDevice = new System.Windows.Forms.Label();
            this.SlideTimer = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.numUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slideScrollSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleCutoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numUpdateInterval);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numMultiplier);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelColor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.slideScrollSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numSampleCutoff);
            this.groupBox1.Controls.Add(this.labelHue);
            this.groupBox1.Controls.Add(this.sliderHue);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // numMultiplier
            // 
            this.numMultiplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMultiplier.Location = new System.Drawing.Point(121, 45);
            this.numMultiplier.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMultiplier.Name = "numMultiplier";
            this.numMultiplier.Size = new System.Drawing.Size(149, 20);
            this.numMultiplier.TabIndex = 9;
            this.numMultiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMultiplier.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Color Multiplier: ";
            // 
            // labelColor
            // 
            this.labelColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(245, 119);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(13, 13);
            this.labelColor.TabIndex = 7;
            this.labelColor.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Speed:";
            // 
            // slideScrollSpeed
            // 
            this.slideScrollSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slideScrollSpeed.Location = new System.Drawing.Point(6, 119);
            this.slideScrollSpeed.Maximum = 200;
            this.slideScrollSpeed.Name = "slideScrollSpeed";
            this.slideScrollSpeed.Size = new System.Drawing.Size(239, 45);
            this.slideScrollSpeed.TabIndex = 5;
            this.slideScrollSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.slideScrollSpeed.Scroll += new System.EventHandler(this.slideScrollSpeed_Scroll);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hue:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sampling Cutoff:";
            // 
            // numSampleCutoff
            // 
            this.numSampleCutoff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numSampleCutoff.Location = new System.Drawing.Point(121, 19);
            this.numSampleCutoff.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.numSampleCutoff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSampleCutoff.Name = "numSampleCutoff";
            this.numSampleCutoff.Size = new System.Drawing.Size(149, 20);
            this.numSampleCutoff.TabIndex = 2;
            this.numSampleCutoff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSampleCutoff.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numSampleCutoff.ValueChanged += new System.EventHandler(this.numSampleCutoff_ValueChanged);
            // 
            // labelHue
            // 
            this.labelHue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHue.AutoSize = true;
            this.labelHue.Location = new System.Drawing.Point(245, 195);
            this.labelHue.Name = "labelHue";
            this.labelHue.Size = new System.Drawing.Size(19, 13);
            this.labelHue.TabIndex = 1;
            this.labelHue.Text = "64";
            // 
            // sliderHue
            // 
            this.sliderHue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderHue.Location = new System.Drawing.Point(6, 183);
            this.sliderHue.Maximum = 359;
            this.sliderHue.Name = "sliderHue";
            this.sliderHue.Size = new System.Drawing.Size(239, 45);
            this.sliderHue.TabIndex = 0;
            this.sliderHue.TickFrequency = 15;
            this.sliderHue.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.sliderHue.Scroll += new System.EventHandler(this.sliderHue_Scroll);
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
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(62, 9);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(33, 13);
            this.labelDevice.TabIndex = 2;
            this.labelDevice.Text = "None";
            // 
            // SlideTimer
            // 
            this.SlideTimer.Enabled = true;
            this.SlideTimer.Interval = 10;
            this.SlideTimer.Tick += new System.EventHandler(this.SlideTimer_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Update interval (ms):";
            // 
            // numUpdateInterval
            // 
            this.numUpdateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpdateInterval.Location = new System.Drawing.Point(121, 71);
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
            35,
            0,
            0,
            0});
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 288);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(230, 270);
            this.Name = "Main";
            this.Text = "Aero Visualizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slideScrollSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleCutoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.TrackBar sliderHue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSampleCutoff;
        private System.Windows.Forms.Label labelHue;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar slideScrollSpeed;
        private System.Windows.Forms.Timer SlideTimer;
        private System.Windows.Forms.NumericUpDown numMultiplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numUpdateInterval;
        private System.Windows.Forms.Label label6;
    }
}

