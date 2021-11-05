namespace EPGCentre
{
    partial class ConfigureVBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSatIp));
            this.label2 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.cboLocalAddress = new System.Windows.Forms.ComboBox();
            this.gpSatIp = new System.Windows.Forms.GroupBox();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.gpSatIp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Local IP address";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(85, 95);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 20;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(228, 95);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 21;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // cboLocalAddress
            // 
            this.cboLocalAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocalAddress.FormattingEnabled = true;
            this.cboLocalAddress.Location = new System.Drawing.Point(229, 25);
            this.cboLocalAddress.Name = "cboLocalAddress";
            this.cboLocalAddress.Size = new System.Drawing.Size(112, 21);
            this.cboLocalAddress.TabIndex = 4;
            // 
            // gpSatIp
            // 
            this.gpSatIp.Controls.Add(this.label2);
            this.gpSatIp.Controls.Add(this.cboLocalAddress);
            this.gpSatIp.Location = new System.Drawing.Point(12, 12);
            this.gpSatIp.Name = "gpSatIp";
            this.gpSatIp.Size = new System.Drawing.Size(357, 70);
            this.gpSatIp.TabIndex = 1;
            this.gpSatIp.TabStop = false;
            this.gpSatIp.Text = "VBox";
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(79, 10);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(59, 17);
            this.cbEnable.TabIndex = 23;
            this.cbEnable.Text = "Enable";
            this.cbEnable.UseVisualStyleBackColor = true;
            this.cbEnable.CheckedChanged += new System.EventHandler(this.cbEnable_CheckedChanged);
            // 
            // ConfigureSatIp
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(388, 132);
            this.Controls.Add(this.cbEnable);
            this.Controls.Add(this.gpSatIp);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureSatIp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - Configure VBox";
            this.gpSatIp.ResumeLayout(false);
            this.gpSatIp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ComboBox cboLocalAddress;
        private System.Windows.Forms.GroupBox gpSatIp;
        private System.Windows.Forms.CheckBox cbEnable;

    }
}