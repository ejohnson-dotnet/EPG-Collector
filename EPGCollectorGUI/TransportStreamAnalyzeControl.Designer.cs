namespace EPGCentre
{
    partial class TransportStreamAnalyzeControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdScan = new System.Windows.Forms.Button();
            this.gpTimeouts = new System.Windows.Forms.GroupBox();
            this.btTimeoutDefaults = new System.Windows.Forms.Button();
            this.nudSignalLockTimeout = new System.Windows.Forms.NumericUpDown();
            this.nudDataCollectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgViewResults = new System.Windows.Forms.DataGridView();
            this.frequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frequencySelectionControl = new FrequencySelectionControl();
            this.gpTimeouts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdScan
            // 
            this.cmdScan.Location = new System.Drawing.Point(17, 629);
            this.cmdScan.Name = "cmdScan";
            this.cmdScan.Size = new System.Drawing.Size(88, 25);
            this.cmdScan.TabIndex = 800;
            this.cmdScan.Text = "Start Analysis";
            this.cmdScan.UseVisualStyleBackColor = true;
            this.cmdScan.Click += new System.EventHandler(this.cmdScan_Click);
            // 
            // gpTimeouts
            // 
            this.gpTimeouts.Controls.Add(this.btTimeoutDefaults);
            this.gpTimeouts.Controls.Add(this.nudSignalLockTimeout);
            this.gpTimeouts.Controls.Add(this.nudDataCollectionTimeout);
            this.gpTimeouts.Controls.Add(this.label2);
            this.gpTimeouts.Controls.Add(this.label1);
            this.gpTimeouts.Location = new System.Drawing.Point(18, 467);
            this.gpTimeouts.Name = "gpTimeouts";
            this.gpTimeouts.Size = new System.Drawing.Size(918, 60);
            this.gpTimeouts.TabIndex = 903;
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
            10,
            0,
            0,
            0});
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
            60,
            0,
            0,
            0});
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
            // dgViewResults
            // 
            this.dgViewResults.AllowUserToAddRows = false;
            this.dgViewResults.AllowUserToDeleteRows = false;
            this.dgViewResults.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frequencyColumn,
            this.pidColumn,
            this.tableColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewResults.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewResults.GridColor = System.Drawing.SystemColors.Control;
            this.dgViewResults.Location = new System.Drawing.Point(0, 0);
            this.dgViewResults.MultiSelect = false;
            this.dgViewResults.Name = "dgViewResults";
            this.dgViewResults.ReadOnly = true;
            this.dgViewResults.RowHeadersVisible = false;
            this.dgViewResults.RowTemplate.Height = 14;
            this.dgViewResults.RowTemplate.ReadOnly = true;
            this.dgViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewResults.ShowCellErrors = false;
            this.dgViewResults.ShowCellToolTips = false;
            this.dgViewResults.ShowEditingIcon = false;
            this.dgViewResults.ShowRowErrors = false;
            this.dgViewResults.Size = new System.Drawing.Size(950, 672);
            this.dgViewResults.TabIndex = 904;
            this.dgViewResults.Visible = false;
            // 
            // frequencyColumn
            // 
            this.frequencyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.frequencyColumn.HeaderText = "Frequency";
            this.frequencyColumn.Name = "frequencyColumn";
            this.frequencyColumn.ReadOnly = true;
            this.frequencyColumn.Width = 82;
            // 
            // pidColumn
            // 
            this.pidColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pidColumn.HeaderText = "PID";
            this.pidColumn.Name = "pidColumn";
            this.pidColumn.ReadOnly = true;
            this.pidColumn.Width = 50;
            // 
            // tableColumn
            // 
            this.tableColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tableColumn.HeaderText = "Tables";
            this.tableColumn.Name = "tableColumn";
            this.tableColumn.ReadOnly = true;
            // 
            // frequencySelectionControl
            // 
            this.frequencySelectionControl.Location = new System.Drawing.Point(10, 1);
            this.frequencySelectionControl.Name = "frequencySelectionControl";
            this.frequencySelectionControl.Size = new System.Drawing.Size(930, 450);
            this.frequencySelectionControl.TabIndex = 902;
            // 
            // TransportStreamAnalyzeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgViewResults);
            this.Controls.Add(this.gpTimeouts);
            this.Controls.Add(this.frequencySelectionControl);
            this.Controls.Add(this.cmdScan);
            this.Name = "TransportStreamAnalyzeControl";
            this.Size = new System.Drawing.Size(950, 672);
            this.gpTimeouts.ResumeLayout(false);
            this.gpTimeouts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdScan;
        private FrequencySelectionControl frequencySelectionControl;
        private System.Windows.Forms.GroupBox gpTimeouts;
        private System.Windows.Forms.Button btTimeoutDefaults;
        private System.Windows.Forms.NumericUpDown nudSignalLockTimeout;
        private System.Windows.Forms.NumericUpDown nudDataCollectionTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgViewResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequencyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableColumn;
    }
}
