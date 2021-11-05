namespace EPGCentre
{
    partial class ViewHistoryControl
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
            this.dgViewHistory = new System.Windows.Forms.DataGridView();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectionResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectionTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectionCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lookupResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lookupTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lookupRateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.softwareVersionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgViewHistory
            // 
            this.dgViewHistory.AllowUserToAddRows = false;
            this.dgViewHistory.AllowUserToDeleteRows = false;
            this.dgViewHistory.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateColumn,
            this.timeColumn,
            this.collectionResultColumn,
            this.collectionTimeColumn,
            this.collectionCountColumn,
            this.lookupResultColumn,
            this.lookupTimeColumn,
            this.lookupRateColumn,
            this.softwareVersionColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewHistory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewHistory.GridColor = System.Drawing.SystemColors.Control;
            this.dgViewHistory.Location = new System.Drawing.Point(0, 0);
            this.dgViewHistory.MultiSelect = false;
            this.dgViewHistory.Name = "dgViewHistory";
            this.dgViewHistory.ReadOnly = true;
            this.dgViewHistory.RowHeadersVisible = false;
            this.dgViewHistory.RowTemplate.Height = 16;
            this.dgViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewHistory.ShowCellErrors = false;
            this.dgViewHistory.ShowCellToolTips = false;
            this.dgViewHistory.ShowEditingIcon = false;
            this.dgViewHistory.ShowRowErrors = false;
            this.dgViewHistory.Size = new System.Drawing.Size(950, 672);
            this.dgViewHistory.TabIndex = 1;
            this.dgViewHistory.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.columnHeaderMouseClick);
            // 
            // dateColumn
            // 
            this.dateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.Width = 55;
            // 
            // timeColumn
            // 
            this.timeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeColumn.HeaderText = "Time";
            this.timeColumn.Name = "timeColumn";
            this.timeColumn.ReadOnly = true;
            this.timeColumn.Width = 55;
            // 
            // collectionResultColumn
            // 
            this.collectionResultColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.collectionResultColumn.HeaderText = "Collection Result";
            this.collectionResultColumn.Name = "collectionResultColumn";
            this.collectionResultColumn.ReadOnly = true;
            this.collectionResultColumn.Width = 102;
            // 
            // collectionTimeColumn
            // 
            this.collectionTimeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.collectionTimeColumn.HeaderText = "Collection Time";
            this.collectionTimeColumn.Name = "collectionTimeColumn";
            this.collectionTimeColumn.ReadOnly = true;
            this.collectionTimeColumn.Width = 96;
            // 
            // collectionCountColumn
            // 
            this.collectionCountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.collectionCountColumn.HeaderText = "EPG Records";
            this.collectionCountColumn.Name = "collectionCountColumn";
            this.collectionCountColumn.ReadOnly = true;
            this.collectionCountColumn.Width = 89;
            // 
            // lookupResultColumn
            // 
            this.lookupResultColumn.HeaderText = "Lookup Result";
            this.lookupResultColumn.Name = "lookupResultColumn";
            this.lookupResultColumn.ReadOnly = true;
            // 
            // lookupTimeColumn
            // 
            this.lookupTimeColumn.HeaderText = "Lookup Time";
            this.lookupTimeColumn.Name = "lookupTimeColumn";
            this.lookupTimeColumn.ReadOnly = true;
            // 
            // lookupRateColumn
            // 
            this.lookupRateColumn.HeaderText = "Lookup Rate (%)";
            this.lookupRateColumn.Name = "lookupRateColumn";
            this.lookupRateColumn.ReadOnly = true;
            // 
            // softwareVersionColumn
            // 
            this.softwareVersionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.softwareVersionColumn.HeaderText = "Software Version";
            this.softwareVersionColumn.Name = "softwareVersionColumn";
            this.softwareVersionColumn.ReadOnly = true;
            // 
            // ViewHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgViewHistory);
            this.Name = "ViewHistoryControl";
            this.Size = new System.Drawing.Size(950, 672);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgViewHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectionResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectionTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectionCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lookupResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lookupTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lookupRateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn softwareVersionColumn;
    }
}
