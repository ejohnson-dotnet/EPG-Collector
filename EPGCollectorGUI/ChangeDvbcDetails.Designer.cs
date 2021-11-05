namespace EPGCentre
{
    partial class ChangeDvbcDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeDvbcDetails));
            this.tbFrequency = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSymbolRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboModulation = new System.Windows.Forms.ComboBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbProvider = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbFrequency
            // 
            this.tbFrequency.Location = new System.Drawing.Point(149, 49);
            this.tbFrequency.Name = "tbFrequency";
            this.tbFrequency.Size = new System.Drawing.Size(116, 20);
            this.tbFrequency.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Modulation";
            // 
            // tbSymbolRate
            // 
            this.tbSymbolRate.Location = new System.Drawing.Point(149, 80);
            this.tbSymbolRate.Name = "tbSymbolRate";
            this.tbSymbolRate.Size = new System.Drawing.Size(116, 20);
            this.tbSymbolRate.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Symbol Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Frequency";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Provider";
            // 
            // cboModulation
            // 
            this.cboModulation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboModulation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModulation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulation.FormattingEnabled = true;
            this.cboModulation.Location = new System.Drawing.Point(149, 110);
            this.cboModulation.MaxDropDownItems = 20;
            this.cboModulation.Name = "cboModulation";
            this.cboModulation.Size = new System.Drawing.Size(150, 21);
            this.cboModulation.TabIndex = 13;
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(101, 142);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 30;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(215, 142);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 31;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "hz";
            // 
            // tbProvider
            // 
            this.tbProvider.Location = new System.Drawing.Point(149, 18);
            this.tbProvider.Name = "tbProvider";
            this.tbProvider.ReadOnly = true;
            this.tbProvider.Size = new System.Drawing.Size(222, 20);
            this.tbProvider.TabIndex = 2;
            this.tbProvider.TabStop = false;
            // 
            // ChangeDvbcDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(390, 177);
            this.Controls.Add(this.tbProvider);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cboModulation);
            this.Controls.Add(this.tbFrequency);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSymbolRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeDvbcDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - Change DVB Cable Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFrequency;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSymbolRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboModulation;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbProvider;
    }
}