namespace EPGCentre
{
    partial class ChannelScanParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelScanParameters));
            this.btLNBDefaults = new System.Windows.Forms.Button();
            this.txtLNBSwitch = new System.Windows.Forms.TextBox();
            this.txtLNBHigh = new System.Windows.Forms.TextBox();
            this.txtLNBLow = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbTuners = new System.Windows.Forms.CheckedListBox();
            this.gpDish = new System.Windows.Forms.GroupBox();
            this.cboLNBType = new System.Windows.Forms.ComboBox();
            this.label111 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpDiseqc = new System.Windows.Forms.GroupBox();
            this.cbUseSafeDiseqc = new System.Windows.Forms.CheckBox();
            this.cboDiseqc = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.cbSwitchAfterTune = new System.Windows.Forms.CheckBox();
            this.cboDiseqcHandler = new System.Windows.Forms.ComboBox();
            this.label94 = new System.Windows.Forms.Label();
            this.cbUseDiseqcCommands = new System.Windows.Forms.CheckBox();
            this.cbDisableDriverDiseqc = new System.Windows.Forms.CheckBox();
            this.cbSwitchAfterPlay = new System.Windows.Forms.CheckBox();
            this.cbRepeatDiseqc = new System.Windows.Forms.CheckBox();
            this.gpSatIp = new System.Windows.Forms.GroupBox();
            this.udDvbsSatIpFrontend = new System.Windows.Forms.DomainUpDown();
            this.label151 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.gpDish.SuspendLayout();
            this.gpDiseqc.SuspendLayout();
            this.gpSatIp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLNBDefaults
            // 
            this.btLNBDefaults.Location = new System.Drawing.Point(645, 31);
            this.btLNBDefaults.Name = "btLNBDefaults";
            this.btLNBDefaults.Size = new System.Drawing.Size(75, 23);
            this.btLNBDefaults.TabIndex = 19;
            this.btLNBDefaults.Text = "Defaults";
            this.btLNBDefaults.UseVisualStyleBackColor = true;
            this.btLNBDefaults.Click += new System.EventHandler(this.btLNBDefaults_Click);
            // 
            // txtLNBSwitch
            // 
            this.txtLNBSwitch.Location = new System.Drawing.Point(516, 33);
            this.txtLNBSwitch.Name = "txtLNBSwitch";
            this.txtLNBSwitch.Size = new System.Drawing.Size(92, 20);
            this.txtLNBSwitch.TabIndex = 16;
            // 
            // txtLNBHigh
            // 
            this.txtLNBHigh.Location = new System.Drawing.Point(317, 32);
            this.txtLNBHigh.Name = "txtLNBHigh";
            this.txtLNBHigh.Size = new System.Drawing.Size(92, 20);
            this.txtLNBHigh.TabIndex = 14;
            // 
            // txtLNBLow
            // 
            this.txtLNBLow.Location = new System.Drawing.Point(104, 33);
            this.txtLNBLow.Name = "txtLNBLow";
            this.txtLNBLow.Size = new System.Drawing.Size(92, 20);
            this.txtLNBLow.TabIndex = 12;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(435, 35);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 15;
            this.label31.Text = "LNB Switch";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 36);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(79, 13);
            this.label33.TabIndex = 11;
            this.label33.Text = "LNB Low Band";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(230, 36);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(81, 13);
            this.label32.TabIndex = 13;
            this.label32.Text = "LNB High Band";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbTuners);
            this.groupBox3.Location = new System.Drawing.Point(12, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(870, 113);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tuner";
            // 
            // clbTuners
            // 
            this.clbTuners.CheckOnClick = true;
            this.clbTuners.FormattingEnabled = true;
            this.clbTuners.Location = new System.Drawing.Point(13, 19);
            this.clbTuners.Name = "clbTuners";
            this.clbTuners.Size = new System.Drawing.Size(845, 79);
            this.clbTuners.TabIndex = 2;
            this.clbTuners.SelectedIndexChanged += new System.EventHandler(this.clbTuners_SelectedIndexChanged);
            // 
            // gpDish
            // 
            this.gpDish.Controls.Add(this.cboLNBType);
            this.gpDish.Controls.Add(this.label111);
            this.gpDish.Controls.Add(this.label33);
            this.gpDish.Controls.Add(this.txtLNBLow);
            this.gpDish.Controls.Add(this.btLNBDefaults);
            this.gpDish.Controls.Add(this.label32);
            this.gpDish.Controls.Add(this.txtLNBSwitch);
            this.gpDish.Controls.Add(this.txtLNBHigh);
            this.gpDish.Controls.Add(this.label31);
            this.gpDish.Location = new System.Drawing.Point(12, 140);
            this.gpDish.Name = "gpDish";
            this.gpDish.Size = new System.Drawing.Size(870, 106);
            this.gpDish.TabIndex = 10;
            this.gpDish.TabStop = false;
            this.gpDish.Text = "Dish Parameters";
            // 
            // cboLNBType
            // 
            this.cboLNBType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLNBType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLNBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLNBType.FormattingEnabled = true;
            this.cboLNBType.Location = new System.Drawing.Point(104, 66);
            this.cboLNBType.Name = "cboLNBType";
            this.cboLNBType.Size = new System.Drawing.Size(191, 21);
            this.cboLNBType.TabIndex = 18;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(10, 69);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(55, 13);
            this.label111.TabIndex = 17;
            this.label111.Text = "LNB Type";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(326, 476);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 70;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 476);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 71;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gpDiseqc
            // 
            this.gpDiseqc.Controls.Add(this.cbUseSafeDiseqc);
            this.gpDiseqc.Controls.Add(this.cboDiseqc);
            this.gpDiseqc.Controls.Add(this.label36);
            this.gpDiseqc.Controls.Add(this.cbSwitchAfterTune);
            this.gpDiseqc.Controls.Add(this.cboDiseqcHandler);
            this.gpDiseqc.Controls.Add(this.label94);
            this.gpDiseqc.Controls.Add(this.cbUseDiseqcCommands);
            this.gpDiseqc.Controls.Add(this.cbDisableDriverDiseqc);
            this.gpDiseqc.Controls.Add(this.cbSwitchAfterPlay);
            this.gpDiseqc.Controls.Add(this.cbRepeatDiseqc);
            this.gpDiseqc.Location = new System.Drawing.Point(12, 262);
            this.gpDiseqc.Name = "gpDiseqc";
            this.gpDiseqc.Size = new System.Drawing.Size(870, 112);
            this.gpDiseqc.TabIndex = 33;
            this.gpDiseqc.TabStop = false;
            this.gpDiseqc.Text = "DiSEqC Options";
            // 
            // cbUseSafeDiseqc
            // 
            this.cbUseSafeDiseqc.AutoSize = true;
            this.cbUseSafeDiseqc.Location = new System.Drawing.Point(292, 28);
            this.cbUseSafeDiseqc.Name = "cbUseSafeDiseqc";
            this.cbUseSafeDiseqc.Size = new System.Drawing.Size(296, 17);
            this.cbUseSafeDiseqc.TabIndex = 35;
            this.cbUseSafeDiseqc.Text = "Check tuner is not in use before changing DiSEqC switch";
            this.cbUseSafeDiseqc.UseVisualStyleBackColor = true;
            // 
            // cboDiseqc
            // 
            this.cboDiseqc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDiseqc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDiseqc.FormattingEnabled = true;
            this.cboDiseqc.Location = new System.Drawing.Point(104, 29);
            this.cboDiseqc.Name = "cboDiseqc";
            this.cboDiseqc.Size = new System.Drawing.Size(156, 21);
            this.cboDiseqc.TabIndex = 32;
            this.cboDiseqc.SelectedIndexChanged += new System.EventHandler(this.cboDiseqc_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(10, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(79, 13);
            this.label36.TabIndex = 31;
            this.label36.Text = "DiSEqC Switch";
            // 
            // cbSwitchAfterTune
            // 
            this.cbSwitchAfterTune.AutoSize = true;
            this.cbSwitchAfterTune.Location = new System.Drawing.Point(614, 28);
            this.cbSwitchAfterTune.Name = "cbSwitchAfterTune";
            this.cbSwitchAfterTune.Size = new System.Drawing.Size(244, 17);
            this.cbSwitchAfterTune.TabIndex = 38;
            this.cbSwitchAfterTune.Text = "Change DiSEqC switch only after tune request";
            this.cbSwitchAfterTune.UseVisualStyleBackColor = true;
            // 
            // cboDiseqcHandler
            // 
            this.cboDiseqcHandler.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDiseqcHandler.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDiseqcHandler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiseqcHandler.FormattingEnabled = true;
            this.cboDiseqcHandler.ItemHeight = 13;
            this.cboDiseqcHandler.Location = new System.Drawing.Point(104, 69);
            this.cboDiseqcHandler.MaxDropDownItems = 20;
            this.cboDiseqcHandler.Name = "cboDiseqcHandler";
            this.cboDiseqcHandler.Size = new System.Drawing.Size(156, 21);
            this.cboDiseqcHandler.TabIndex = 34;
            this.cboDiseqcHandler.SelectedIndexChanged += new System.EventHandler(this.cboDiseqcHandler_SelectedIndexChanged);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(10, 72);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(82, 13);
            this.label94.TabIndex = 132;
            this.label94.Text = "DiSEqC handler";
            // 
            // cbUseDiseqcCommands
            // 
            this.cbUseDiseqcCommands.AutoSize = true;
            this.cbUseDiseqcCommands.Location = new System.Drawing.Point(614, 78);
            this.cbUseDiseqcCommands.Name = "cbUseDiseqcCommands";
            this.cbUseDiseqcCommands.Size = new System.Drawing.Size(247, 17);
            this.cbUseDiseqcCommands.TabIndex = 40;
            this.cbUseDiseqcCommands.Text = "Use DiSEqC commands instead of LNB source";
            this.cbUseDiseqcCommands.UseVisualStyleBackColor = true;
            // 
            // cbDisableDriverDiseqc
            // 
            this.cbDisableDriverDiseqc.AutoSize = true;
            this.cbDisableDriverDiseqc.Location = new System.Drawing.Point(614, 53);
            this.cbDisableDriverDiseqc.Name = "cbDisableDriverDiseqc";
            this.cbDisableDriverDiseqc.Size = new System.Drawing.Size(189, 17);
            this.cbDisableDriverDiseqc.TabIndex = 39;
            this.cbDisableDriverDiseqc.Text = "Disable drivers DiSEqC commands";
            this.cbDisableDriverDiseqc.UseVisualStyleBackColor = true;
            // 
            // cbSwitchAfterPlay
            // 
            this.cbSwitchAfterPlay.AutoSize = true;
            this.cbSwitchAfterPlay.Location = new System.Drawing.Point(292, 78);
            this.cbSwitchAfterPlay.Name = "cbSwitchAfterPlay";
            this.cbSwitchAfterPlay.Size = new System.Drawing.Size(250, 17);
            this.cbSwitchAfterPlay.TabIndex = 37;
            this.cbSwitchAfterPlay.Text = "Change DiSEqC switch only after graph running";
            this.cbSwitchAfterPlay.UseVisualStyleBackColor = true;
            // 
            // cbRepeatDiseqc
            // 
            this.cbRepeatDiseqc.AutoSize = true;
            this.cbRepeatDiseqc.Location = new System.Drawing.Point(292, 53);
            this.cbRepeatDiseqc.Name = "cbRepeatDiseqc";
            this.cbRepeatDiseqc.Size = new System.Drawing.Size(236, 17);
            this.cbRepeatDiseqc.TabIndex = 36;
            this.cbRepeatDiseqc.Text = "Repeat DiSEqC command if first attempt fails";
            this.cbRepeatDiseqc.UseVisualStyleBackColor = true;
            // 
            // gpSatIp
            // 
            this.gpSatIp.Controls.Add(this.udDvbsSatIpFrontend);
            this.gpSatIp.Controls.Add(this.label151);
            this.gpSatIp.Location = new System.Drawing.Point(12, 394);
            this.gpSatIp.Name = "gpSatIp";
            this.gpSatIp.Size = new System.Drawing.Size(870, 68);
            this.gpSatIp.TabIndex = 60;
            this.gpSatIp.TabStop = false;
            this.gpSatIp.Text = "Sat>IP Options";
            // 
            // udDvbsSatIpFrontend
            // 
            this.udDvbsSatIpFrontend.Items.Add("n/a");
            this.udDvbsSatIpFrontend.Items.Add("1");
            this.udDvbsSatIpFrontend.Items.Add("2");
            this.udDvbsSatIpFrontend.Items.Add("3");
            this.udDvbsSatIpFrontend.Items.Add("4");
            this.udDvbsSatIpFrontend.Location = new System.Drawing.Point(104, 31);
            this.udDvbsSatIpFrontend.Name = "udDvbsSatIpFrontend";
            this.udDvbsSatIpFrontend.ReadOnly = true;
            this.udDvbsSatIpFrontend.Size = new System.Drawing.Size(50, 20);
            this.udDvbsSatIpFrontend.TabIndex = 62;
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(10, 33);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(84, 13);
            this.label151.TabIndex = 61;
            this.label151.Text = "Sat>IP Frontend";
            // 
            // ChannelScanParameters
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(898, 514);
            this.Controls.Add(this.gpSatIp);
            this.Controls.Add(this.gpDiseqc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gpDish);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChannelScanParameters";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - Select Parameters For Channel Scan";
            this.groupBox3.ResumeLayout(false);
            this.gpDish.ResumeLayout(false);
            this.gpDish.PerformLayout();
            this.gpDiseqc.ResumeLayout(false);
            this.gpDiseqc.PerformLayout();
            this.gpSatIp.ResumeLayout(false);
            this.gpSatIp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLNBDefaults;
        private System.Windows.Forms.TextBox txtLNBSwitch;
        private System.Windows.Forms.TextBox txtLNBHigh;
        private System.Windows.Forms.TextBox txtLNBLow;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbTuners;
        private System.Windows.Forms.GroupBox gpDish;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gpDiseqc;
        private System.Windows.Forms.CheckBox cbRepeatDiseqc;
        private System.Windows.Forms.CheckBox cbSwitchAfterPlay;
        private System.Windows.Forms.ComboBox cboDiseqcHandler;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.CheckBox cbUseDiseqcCommands;
        private System.Windows.Forms.CheckBox cbDisableDriverDiseqc;
        private System.Windows.Forms.CheckBox cbSwitchAfterTune;
        private System.Windows.Forms.ComboBox cboDiseqc;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox cbUseSafeDiseqc;
        private System.Windows.Forms.ComboBox cboLNBType;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.GroupBox gpSatIp;
        private System.Windows.Forms.DomainUpDown udDvbsSatIpFrontend;
        private System.Windows.Forms.Label label151;
    }
}