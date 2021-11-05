namespace EPGCentre
{
    /// <summary>
    /// 
    /// </summary>
    partial class ChannelScanControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.frequencySelectionControl = new FrequencySelectionControl();
            this.cmdScan = new System.Windows.Forms.Button();
            this.lbProgress = new System.Windows.Forms.ListBox();
            this.lvDvbResults = new System.Windows.Forms.ListView();
            this.columnFrequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProvider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnOnid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTsid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEncrypted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNowNext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSchedule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAtscResults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpTimeouts = new System.Windows.Forms.GroupBox();
            this.btTimeoutDefaults = new System.Windows.Forms.Button();
            this.nudSignalLockTimeout = new System.Windows.Forms.NumericUpDown();
            this.nudDataCollectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpTimeouts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // frequencySelectionControl
            // 
            this.frequencySelectionControl.Location = new System.Drawing.Point(9, 8);
            this.frequencySelectionControl.Name = "frequencySelectionControl";
            this.frequencySelectionControl.Size = new System.Drawing.Size(931, 449);
            this.frequencySelectionControl.TabIndex = 0;
            // 
            // cmdScan
            // 
            this.cmdScan.Location = new System.Drawing.Point(17, 524);
            this.cmdScan.Name = "cmdScan";
            this.cmdScan.Size = new System.Drawing.Size(88, 25);
            this.cmdScan.TabIndex = 101;
            this.cmdScan.Text = "Start Scan";
            this.cmdScan.UseVisualStyleBackColor = true;
            this.cmdScan.Click += new System.EventHandler(this.cmdScan_Click);
            // 
            // lbProgress
            // 
            this.lbProgress.FormattingEnabled = true;
            this.lbProgress.Location = new System.Drawing.Point(17, 559);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbProgress.Size = new System.Drawing.Size(913, 95);
            this.lbProgress.TabIndex = 102;
            // 
            // lvDvbResults
            // 
            this.lvDvbResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFrequency,
            this.columnName,
            this.columnProvider,
            this.columnType,
            this.columnOnid,
            this.columnTsid,
            this.columnSid,
            this.columnEncrypted,
            this.columnNowNext,
            this.columnSchedule});
            this.lvDvbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDvbResults.FullRowSelect = true;
            this.lvDvbResults.GridLines = true;
            this.lvDvbResults.Location = new System.Drawing.Point(0, 0);
            this.lvDvbResults.MultiSelect = false;
            this.lvDvbResults.Name = "lvDvbResults";
            this.lvDvbResults.Size = new System.Drawing.Size(950, 672);
            this.lvDvbResults.TabIndex = 103;
            this.lvDvbResults.UseCompatibleStateImageBehavior = false;
            this.lvDvbResults.View = System.Windows.Forms.View.Details;
            this.lvDvbResults.Visible = false;
            // 
            // columnFrequency
            // 
            this.columnFrequency.Text = "Frequency";
            this.columnFrequency.Width = 95;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 165;
            // 
            // columnProvider
            // 
            this.columnProvider.Text = "Provider";
            this.columnProvider.Width = 200;
            // 
            // columnType
            // 
            this.columnType.Text = "Type";
            // 
            // columnOnid
            // 
            this.columnOnid.Text = "ONID";
            // 
            // columnTsid
            // 
            this.columnTsid.Text = "TSID";
            // 
            // columnSid
            // 
            this.columnSid.Text = "SID";
            // 
            // columnEncrypted
            // 
            this.columnEncrypted.Text = "Encrypted";
            this.columnEncrypted.Width = 71;
            // 
            // columnNowNext
            // 
            this.columnNowNext.Text = "Now/Next";
            this.columnNowNext.Width = 74;
            // 
            // columnSchedule
            // 
            this.columnSchedule.Text = "Schedule";
            // 
            // lvAtscResults
            // 
            this.lvAtscResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lvAtscResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAtscResults.FullRowSelect = true;
            this.lvAtscResults.GridLines = true;
            this.lvAtscResults.Location = new System.Drawing.Point(0, 0);
            this.lvAtscResults.MultiSelect = false;
            this.lvAtscResults.Name = "lvAtscResults";
            this.lvAtscResults.Size = new System.Drawing.Size(950, 672);
            this.lvAtscResults.TabIndex = 104;
            this.lvAtscResults.UseCompatibleStateImageBehavior = false;
            this.lvAtscResults.View = System.Windows.Forms.View.Details;
            this.lvAtscResults.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Frequency";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Phys Channel";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Provider";
            this.columnHeader3.Width = 126;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ch No.";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Sub No";
            this.columnHeader6.Width = 54;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "TSID";
            this.columnHeader7.Width = 65;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Prog No";
            this.columnHeader8.Width = 71;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Hidden";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Guide Hidden";
            this.columnHeader11.Width = 90;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Access Controlled";
            this.columnHeader12.Width = 104;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "OOB";
            // 
            // gpTimeouts
            // 
            this.gpTimeouts.Controls.Add(this.btTimeoutDefaults);
            this.gpTimeouts.Controls.Add(this.nudSignalLockTimeout);
            this.gpTimeouts.Controls.Add(this.nudDataCollectionTimeout);
            this.gpTimeouts.Controls.Add(this.label2);
            this.gpTimeouts.Controls.Add(this.label1);
            this.gpTimeouts.Location = new System.Drawing.Point(17, 453);
            this.gpTimeouts.Name = "gpTimeouts";
            this.gpTimeouts.Size = new System.Drawing.Size(918, 60);
            this.gpTimeouts.TabIndex = 904;
            this.gpTimeouts.TabStop = false;
            this.gpTimeouts.Text = "Timeouts";
            // 
            // btTimeoutDefaults
            // 
            this.btTimeoutDefaults.Location = new System.Drawing.Point(391, 21);
            this.btTimeoutDefaults.Name = "btTimeoutDefaults";
            this.btTimeoutDefaults.Size = new System.Drawing.Size(75, 23);
            this.btTimeoutDefaults.TabIndex = 75;
            this.btTimeoutDefaults.Text = "Defaults";
            this.btTimeoutDefaults.UseVisualStyleBackColor = true;
            this.btTimeoutDefaults.Click += new System.EventHandler(this.btTimeoutDefaults_Click);
            // 
            // nudSignalLockTimeout
            // 
            this.nudSignalLockTimeout.Location = new System.Drawing.Point(105, 23);
            this.nudSignalLockTimeout.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSignalLockTimeout.Name = "nudSignalLockTimeout";
            this.nudSignalLockTimeout.Size = new System.Drawing.Size(48, 20);
            this.nudSignalLockTimeout.TabIndex = 72;
            this.nudSignalLockTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSignalLockTimeout.ValueChanged += new System.EventHandler(this.nudSignalLockTimeout_ValueChanged);
            // 
            // nudDataCollectionTimeout
            // 
            this.nudDataCollectionTimeout.Location = new System.Drawing.Point(313, 23);
            this.nudDataCollectionTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDataCollectionTimeout.Name = "nudDataCollectionTimeout";
            this.nudDataCollectionTimeout.Size = new System.Drawing.Size(48, 20);
            this.nudDataCollectionTimeout.TabIndex = 74;
            this.nudDataCollectionTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDataCollectionTimeout.ValueChanged += new System.EventHandler(this.nudDataCollectionTimeout_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Data Collection (sec)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Signal Lock (sec)";
            // 
            // ChannelScanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpTimeouts);
            this.Controls.Add(this.lvAtscResults);
            this.Controls.Add(this.lvDvbResults);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.cmdScan);
            this.Controls.Add(this.frequencySelectionControl);
            this.Name = "ChannelScanControl";
            this.Size = new System.Drawing.Size(950, 672);
            this.gpTimeouts.ResumeLayout(false);
            this.gpTimeouts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FrequencySelectionControl frequencySelectionControl;
        private System.Windows.Forms.Button cmdScan;
        private System.Windows.Forms.ListBox lbProgress;
        private System.Windows.Forms.ListView lvDvbResults;
        private System.Windows.Forms.ColumnHeader columnFrequency;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnProvider;
        private System.Windows.Forms.ColumnHeader columnOnid;
        private System.Windows.Forms.ColumnHeader columnTsid;
        private System.Windows.Forms.ColumnHeader columnSid;
        private System.Windows.Forms.ColumnHeader columnEncrypted;
        private System.Windows.Forms.ColumnHeader columnNowNext;
        private System.Windows.Forms.ColumnHeader columnSchedule;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ListView lvAtscResults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.GroupBox gpTimeouts;
        private System.Windows.Forms.Button btTimeoutDefaults;
        private System.Windows.Forms.NumericUpDown nudSignalLockTimeout;
        private System.Windows.Forms.NumericUpDown nudDataCollectionTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
