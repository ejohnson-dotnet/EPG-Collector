namespace EPGCentre
{
    partial class RunWMCMaintenanceControl
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
            this.dgViewLog = new System.Windows.Forms.DataGridView();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dgViewLog
            // 
            this.dgViewLog.AllowUserToAddRows = false;
            this.dgViewLog.AllowUserToDeleteRows = false;
            this.dgViewLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeColumn,
            this.detailColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewLog.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewLog.GridColor = System.Drawing.SystemColors.Control;
            this.dgViewLog.Location = new System.Drawing.Point(0, 0);
            this.dgViewLog.Name = "dgViewLog";
            this.dgViewLog.ReadOnly = true;
            this.dgViewLog.RowHeadersVisible = false;
            this.dgViewLog.RowTemplate.Height = 16;
            this.dgViewLog.RowTemplate.ReadOnly = true;
            this.dgViewLog.ShowCellErrors = false;
            this.dgViewLog.ShowCellToolTips = false;
            this.dgViewLog.ShowEditingIcon = false;
            this.dgViewLog.ShowRowErrors = false;
            this.dgViewLog.Size = new System.Drawing.Size(950, 672);
            this.dgViewLog.TabIndex = 29;
            // 
            // typeColumn
            // 
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Width = 56;
            // 
            // detailColumn
            // 
            this.detailColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.detailColumn.HeaderText = "Detail";
            this.detailColumn.Name = "detailColumn";
            this.detailColumn.ReadOnly = true;
            // 
            // RunCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgViewLog);
            this.Name = "RunCollectionControl";
            this.Size = new System.Drawing.Size(950, 672);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgViewLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailColumn;
    }
}
