namespace EPGCentre
{
    partial class FindEPGControl
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
            this.btTimeoutDefaults = new System.Windows.Forms.Button();
            this.nudSignalLockTimeout = new System.Windows.Forms.NumericUpDown();
            this.frequencySelectionControl = new FrequencySelectionControl();
            this.dgViewResults = new System.Windows.Forms.DataGridView();
            this.frequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epgTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nudDataCollectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpTimeouts = new System.Windows.Forms.GroupBox();
            this.cmdScan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).BeginInit();
            this.gpTimeouts.SuspendLayout();
            this.SuspendLayout();
            // 
            // btTimeoutDefaults
            // 
            this.btTimeoutDefaults.Location = new System.Drawing.Point(391, 21);
            this.btTimeoutDefaults.Name = "btTimeoutDefaults";
            this.btTimeoutDefaults.Size = new System.Drawing.Size(75, 23);
            this.btTimeoutDefaults.TabIndex = 75;
            this.btTimeoutDefaults.Text = "Defaults";
            this.btTimeoutDefaults.UseVisualStyleBackColor = true;
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
            // dgViewResults
            // 
            this.dgViewResults.AllowUserToAddRows = false;
            this.dgViewResults.AllowUserToDeleteRows = false;
            this.dgViewResults.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frequencyColumn,
            this.epgTypeColumn,
            this.commentColumn});
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
            this.dgViewResults.TabIndex = 912;
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
            // epgTypeColumn
            // 
            this.epgTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.epgTypeColumn.HeaderText = "EPG Type";
            this.epgTypeColumn.Name = "epgTypeColumn";
            this.epgTypeColumn.ReadOnly = true;
            this.epgTypeColumn.Width = 81;
            // 
            // commentColumn
            // 
            this.commentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentColumn.HeaderText = "Comment";
            this.commentColumn.Name = "commentColumn";
            this.commentColumn.ReadOnly = true;
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
            // gpTimeouts
            // 
            this.gpTimeouts.Controls.Add(this.btTimeoutDefaults);
            this.gpTimeouts.Controls.Add(this.nudSignalLockTimeout);
            this.gpTimeouts.Controls.Add(this.nudDataCollectionTimeout);
            this.gpTimeouts.Controls.Add(this.label2);
            this.gpTimeouts.Controls.Add(this.label1);
            this.gpTimeouts.Location = new System.Drawing.Point(18, 471);
            this.gpTimeouts.Name = "gpTimeouts";
            this.gpTimeouts.Size = new System.Drawing.Size(918, 60);
            this.gpTimeouts.TabIndex = 911;
            this.gpTimeouts.TabStop = false;
            this.gpTimeouts.Text = "Timeouts";
            // 
            // frequencySelectionControl
            // 
            this.frequencySelectionControl.Location = new System.Drawing.Point(10, 10);
            this.frequencySelectionControl.Name = "frequencySelectionControl";
            this.frequencySelectionControl.Size = new System.Drawing.Size(930, 453);
            this.frequencySelectionControl.TabIndex = 910;
            // 
            // cmdScan
            // 
            this.cmdScan.Location = new System.Drawing.Point(17, 634);
            this.cmdScan.Name = "cmdScan";
            this.cmdScan.Size = new System.Drawing.Size(88, 25);
            this.cmdScan.TabIndex = 906;
            this.cmdScan.Text = "Start Find";
            this.cmdScan.UseVisualStyleBackColor = true;
            this.cmdScan.Click += new System.EventHandler(this.cmdScan_Click);
            // 
            // FindEPGControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgViewResults);
            this.Controls.Add(this.gpTimeouts);
            this.Controls.Add(this.frequencySelectionControl);
            this.Controls.Add(this.cmdScan);
            this.Name = "FindEPGControl";
            this.Size = new System.Drawing.Size(950, 672);
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).EndInit();
            this.gpTimeouts.ResumeLayout(false);
            this.gpTimeouts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btTimeoutDefaults;
        private System.Windows.Forms.NumericUpDown nudSignalLockTimeout;
        private System.Windows.Forms.DataGridView dgViewResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequencyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epgTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentColumn;
        private System.Windows.Forms.NumericUpDown nudDataCollectionTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpTimeouts;
        private FrequencySelectionControl frequencySelectionControl;
        private System.Windows.Forms.Button cmdScan;

    }
}
