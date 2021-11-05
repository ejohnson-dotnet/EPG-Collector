namespace EPGCentre
{
    /// <summary>
    /// Frequency Selection Control
    /// </summary>
    partial class FrequencySelectionControl
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
            this.tbcDeliverySystem = new System.Windows.Forms.TabControl();
            this.tbpSatellite = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.cbSwitchAfterTune = new System.Windows.Forms.CheckBox();
            this.cboDiseqcHandler = new System.Windows.Forms.ComboBox();
            this.label94 = new System.Windows.Forms.Label();
            this.cbUseDiseqcCommands = new System.Windows.Forms.CheckBox();
            this.cbDisableDriverDiseqc = new System.Windows.Forms.CheckBox();
            this.cbRepeatDiseqc = new System.Windows.Forms.CheckBox();
            this.cbSwitchAfterPlay = new System.Windows.Forms.CheckBox();
            this.cbUseSafeDiseqc = new System.Windows.Forms.CheckBox();
            this.cboDiseqc = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.udDvbsSatIpFrontend = new System.Windows.Forms.DomainUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbSatelliteTuners = new System.Windows.Forms.CheckedListBox();
            this.label151 = new System.Windows.Forms.Label();
            this.cboLNBType = new System.Windows.Forms.ComboBox();
            this.label111 = new System.Windows.Forms.Label();
            this.btLNBDefaults = new System.Windows.Forms.Button();
            this.txtLNBSwitch = new System.Windows.Forms.TextBox();
            this.txtLNBHigh = new System.Windows.Forms.TextBox();
            this.cboDVBSScanningFrequency = new System.Windows.Forms.ComboBox();
            this.lblSatelliteFrequency = new System.Windows.Forms.Label();
            this.txtLNBLow = new System.Windows.Forms.TextBox();
            this.cboSatellite = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.tbpTerrestrial = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.clbTerrestrialTuners = new System.Windows.Forms.CheckedListBox();
            this.udDvbtSatIpFrontend = new System.Windows.Forms.DomainUpDown();
            this.label152 = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.cboDVBTScanningFrequency = new System.Windows.Forms.ComboBox();
            this.lblTerrestrialFrequency = new System.Windows.Forms.Label();
            this.tbpCable = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.clbCableTuners = new System.Windows.Forms.CheckedListBox();
            this.udDvbcSatIpFrontend = new System.Windows.Forms.DomainUpDown();
            this.label153 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.cboCable = new System.Windows.Forms.ComboBox();
            this.lblCableFrequency = new System.Windows.Forms.Label();
            this.cboCableScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpAtsc = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.clbAtscTuners = new System.Windows.Forms.CheckedListBox();
            this.label50 = new System.Windows.Forms.Label();
            this.cboAtscProvider = new System.Windows.Forms.ComboBox();
            this.lblAtscChannel = new System.Windows.Forms.Label();
            this.cboAtscScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpClearQAM = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.clbClearQamTuners = new System.Windows.Forms.CheckedListBox();
            this.label57 = new System.Windows.Forms.Label();
            this.cboClearQamProvider = new System.Windows.Forms.ComboBox();
            this.lblClearQamChannel = new System.Windows.Forms.Label();
            this.cboClearQamScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpISDBSatellite = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.clbISDBSatelliteTuners = new System.Windows.Forms.CheckedListBox();
            this.btISDBLNBDefaults = new System.Windows.Forms.Button();
            this.txtISDBLNBSwitch = new System.Windows.Forms.TextBox();
            this.txtISDBLNBHigh = new System.Windows.Forms.TextBox();
            this.cboISDBSScanningFrequency = new System.Windows.Forms.ComboBox();
            this.lblISDBSFrequency = new System.Windows.Forms.Label();
            this.txtISDBLNBLow = new System.Windows.Forms.TextBox();
            this.cboISDBSatellite = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.tbpISDBTerrestrial = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.clbISDBTerrestrialTuners = new System.Windows.Forms.CheckedListBox();
            this.label67 = new System.Windows.Forms.Label();
            this.cboISDBTProvider = new System.Windows.Forms.ComboBox();
            this.lblISDBTChannel = new System.Windows.Forms.Label();
            this.cboISDBTScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpFile = new System.Windows.Forms.TabPage();
            this.btDeliveryFileBrowse = new System.Windows.Forms.Button();
            this.tbDeliveryFilePath = new System.Windows.Forms.TextBox();
            this.label140 = new System.Windows.Forms.Label();
            this.tbpStream = new System.Windows.Forms.TabPage();
            this.nudStreamMulticastSourcePort = new System.Windows.Forms.NumericUpDown();
            this.label147 = new System.Windows.Forms.Label();
            this.tbStreamMulticastSourceIP = new System.Windows.Forms.TextBox();
            this.label146 = new System.Windows.Forms.Label();
            this.tbStreamPath = new System.Windows.Forms.TextBox();
            this.label148 = new System.Windows.Forms.Label();
            this.btFindIPAddress = new System.Windows.Forms.Button();
            this.nudStreamPortNumber = new System.Windows.Forms.NumericUpDown();
            this.label145 = new System.Windows.Forms.Label();
            this.cboStreamProtocol = new System.Windows.Forms.ComboBox();
            this.label144 = new System.Windows.Forms.Label();
            this.tbStreamIpAddress = new System.Windows.Forms.TextBox();
            this.label143 = new System.Windows.Forms.Label();
            this.tbcDeliverySystem.SuspendLayout();
            this.tbpSatellite.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbpTerrestrial.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tbpCable.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tbpAtsc.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tbpClearQAM.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.tbpISDBSatellite.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tbpISDBTerrestrial.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.tbpFile.SuspendLayout();
            this.tbpStream.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStreamMulticastSourcePort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStreamPortNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcDeliverySystem
            // 
            this.tbcDeliverySystem.Controls.Add(this.tbpSatellite);
            this.tbcDeliverySystem.Controls.Add(this.tbpTerrestrial);
            this.tbcDeliverySystem.Controls.Add(this.tbpCable);
            this.tbcDeliverySystem.Controls.Add(this.tbpAtsc);
            this.tbcDeliverySystem.Controls.Add(this.tbpClearQAM);
            this.tbcDeliverySystem.Controls.Add(this.tbpISDBSatellite);
            this.tbcDeliverySystem.Controls.Add(this.tbpISDBTerrestrial);
            this.tbcDeliverySystem.Controls.Add(this.tbpFile);
            this.tbcDeliverySystem.Controls.Add(this.tbpStream);
            this.tbcDeliverySystem.Location = new System.Drawing.Point(7, 18);
            this.tbcDeliverySystem.Name = "tbcDeliverySystem";
            this.tbcDeliverySystem.SelectedIndex = 0;
            this.tbcDeliverySystem.Size = new System.Drawing.Size(917, 418);
            this.tbcDeliverySystem.TabIndex = 202;
            // 
            // tbpSatellite
            // 
            this.tbpSatellite.Controls.Add(this.groupBox14);
            this.tbpSatellite.Controls.Add(this.udDvbsSatIpFrontend);
            this.tbpSatellite.Controls.Add(this.groupBox3);
            this.tbpSatellite.Controls.Add(this.label151);
            this.tbpSatellite.Controls.Add(this.cboLNBType);
            this.tbpSatellite.Controls.Add(this.label111);
            this.tbpSatellite.Controls.Add(this.btLNBDefaults);
            this.tbpSatellite.Controls.Add(this.txtLNBSwitch);
            this.tbpSatellite.Controls.Add(this.txtLNBHigh);
            this.tbpSatellite.Controls.Add(this.cboDVBSScanningFrequency);
            this.tbpSatellite.Controls.Add(this.lblSatelliteFrequency);
            this.tbpSatellite.Controls.Add(this.txtLNBLow);
            this.tbpSatellite.Controls.Add(this.cboSatellite);
            this.tbpSatellite.Controls.Add(this.label4);
            this.tbpSatellite.Controls.Add(this.label31);
            this.tbpSatellite.Controls.Add(this.label33);
            this.tbpSatellite.Controls.Add(this.label32);
            this.tbpSatellite.Location = new System.Drawing.Point(4, 22);
            this.tbpSatellite.Name = "tbpSatellite";
            this.tbpSatellite.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSatellite.Size = new System.Drawing.Size(909, 392);
            this.tbpSatellite.TabIndex = 0;
            this.tbpSatellite.Text = "DVB Satellite";
            this.tbpSatellite.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cbSwitchAfterTune);
            this.groupBox14.Controls.Add(this.cboDiseqcHandler);
            this.groupBox14.Controls.Add(this.label94);
            this.groupBox14.Controls.Add(this.cbUseDiseqcCommands);
            this.groupBox14.Controls.Add(this.cbDisableDriverDiseqc);
            this.groupBox14.Controls.Add(this.cbRepeatDiseqc);
            this.groupBox14.Controls.Add(this.cbSwitchAfterPlay);
            this.groupBox14.Controls.Add(this.cbUseSafeDiseqc);
            this.groupBox14.Controls.Add(this.cboDiseqc);
            this.groupBox14.Controls.Add(this.label36);
            this.groupBox14.Location = new System.Drawing.Point(19, 275);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(865, 95);
            this.groupBox14.TabIndex = 301;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "DiSEqC Options";
            // 
            // cbSwitchAfterTune
            // 
            this.cbSwitchAfterTune.AutoSize = true;
            this.cbSwitchAfterTune.Location = new System.Drawing.Point(601, 20);
            this.cbSwitchAfterTune.Name = "cbSwitchAfterTune";
            this.cbSwitchAfterTune.Size = new System.Drawing.Size(244, 17);
            this.cbSwitchAfterTune.TabIndex = 314;
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
            this.cboDiseqcHandler.Location = new System.Drawing.Point(106, 54);
            this.cboDiseqcHandler.MaxDropDownItems = 20;
            this.cboDiseqcHandler.Name = "cboDiseqcHandler";
            this.cboDiseqcHandler.Size = new System.Drawing.Size(159, 21);
            this.cboDiseqcHandler.TabIndex = 310;
            this.cboDiseqcHandler.SelectedIndexChanged += new System.EventHandler(this.cboDiseqcHandler_SelectedIndexChanged);
            this.cboDiseqcHandler.Click += new System.EventHandler(this.cboDiseqcHandler_SelectedIndexChanged);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(9, 57);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(82, 13);
            this.label94.TabIndex = 309;
            this.label94.Text = "DiSEqC handler";
            // 
            // cbUseDiseqcCommands
            // 
            this.cbUseDiseqcCommands.AutoSize = true;
            this.cbUseDiseqcCommands.Location = new System.Drawing.Point(601, 43);
            this.cbUseDiseqcCommands.Name = "cbUseDiseqcCommands";
            this.cbUseDiseqcCommands.Size = new System.Drawing.Size(247, 17);
            this.cbUseDiseqcCommands.TabIndex = 315;
            this.cbUseDiseqcCommands.Text = "Use DiSEqC commands instead of LNB source";
            this.cbUseDiseqcCommands.UseVisualStyleBackColor = true;
            // 
            // cbDisableDriverDiseqc
            // 
            this.cbDisableDriverDiseqc.AutoSize = true;
            this.cbDisableDriverDiseqc.Location = new System.Drawing.Point(601, 66);
            this.cbDisableDriverDiseqc.Name = "cbDisableDriverDiseqc";
            this.cbDisableDriverDiseqc.Size = new System.Drawing.Size(189, 17);
            this.cbDisableDriverDiseqc.TabIndex = 316;
            this.cbDisableDriverDiseqc.Text = "Disable drivers DiSEqC commands";
            this.cbDisableDriverDiseqc.UseVisualStyleBackColor = true;
            // 
            // cbRepeatDiseqc
            // 
            this.cbRepeatDiseqc.AutoSize = true;
            this.cbRepeatDiseqc.Location = new System.Drawing.Point(295, 43);
            this.cbRepeatDiseqc.Name = "cbRepeatDiseqc";
            this.cbRepeatDiseqc.Size = new System.Drawing.Size(236, 17);
            this.cbRepeatDiseqc.TabIndex = 312;
            this.cbRepeatDiseqc.Text = "Repeat DiSEqC command if first attempt fails";
            this.cbRepeatDiseqc.UseVisualStyleBackColor = true;
            // 
            // cbSwitchAfterPlay
            // 
            this.cbSwitchAfterPlay.AutoSize = true;
            this.cbSwitchAfterPlay.Location = new System.Drawing.Point(295, 66);
            this.cbSwitchAfterPlay.Name = "cbSwitchAfterPlay";
            this.cbSwitchAfterPlay.Size = new System.Drawing.Size(255, 17);
            this.cbSwitchAfterPlay.TabIndex = 313;
            this.cbSwitchAfterPlay.Text = "Change DiSEqC switch only when graph running";
            this.cbSwitchAfterPlay.UseVisualStyleBackColor = true;
            // 
            // cbUseSafeDiseqc
            // 
            this.cbUseSafeDiseqc.AutoSize = true;
            this.cbUseSafeDiseqc.Location = new System.Drawing.Point(295, 20);
            this.cbUseSafeDiseqc.Name = "cbUseSafeDiseqc";
            this.cbUseSafeDiseqc.Size = new System.Drawing.Size(296, 17);
            this.cbUseSafeDiseqc.TabIndex = 311;
            this.cbUseSafeDiseqc.Text = "Check tuner is not in use before changing DiSEqC switch";
            this.cbUseSafeDiseqc.UseVisualStyleBackColor = true;
            // 
            // cboDiseqc
            // 
            this.cboDiseqc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDiseqc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDiseqc.FormattingEnabled = true;
            this.cboDiseqc.Location = new System.Drawing.Point(106, 23);
            this.cboDiseqc.MaxDropDownItems = 20;
            this.cboDiseqc.Name = "cboDiseqc";
            this.cboDiseqc.Size = new System.Drawing.Size(159, 21);
            this.cboDiseqc.TabIndex = 308;
            this.cboDiseqc.SelectedIndexChanged += new System.EventHandler(this.cboDiseqc_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(9, 27);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(39, 13);
            this.label36.TabIndex = 307;
            this.label36.Text = "Switch";
            // 
            // udDvbsSatIpFrontend
            // 
            this.udDvbsSatIpFrontend.Items.Add("n/a");
            this.udDvbsSatIpFrontend.Items.Add("1");
            this.udDvbsSatIpFrontend.Items.Add("2");
            this.udDvbsSatIpFrontend.Items.Add("3");
            this.udDvbsSatIpFrontend.Items.Add("4");
            this.udDvbsSatIpFrontend.Location = new System.Drawing.Point(130, 76);
            this.udDvbsSatIpFrontend.Name = "udDvbsSatIpFrontend";
            this.udDvbsSatIpFrontend.ReadOnly = true;
            this.udDvbsSatIpFrontend.Size = new System.Drawing.Size(50, 20);
            this.udDvbsSatIpFrontend.TabIndex = 209;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbSatelliteTuners);
            this.groupBox3.Location = new System.Drawing.Point(19, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(865, 121);
            this.groupBox3.TabIndex = 100;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tuners";
            // 
            // clbSatelliteTuners
            // 
            this.clbSatelliteTuners.CheckOnClick = true;
            this.clbSatelliteTuners.FormattingEnabled = true;
            this.clbSatelliteTuners.Location = new System.Drawing.Point(13, 19);
            this.clbSatelliteTuners.Name = "clbSatelliteTuners";
            this.clbSatelliteTuners.Size = new System.Drawing.Size(839, 94);
            this.clbSatelliteTuners.TabIndex = 101;
            this.clbSatelliteTuners.SelectedIndexChanged += new System.EventHandler(this.clbSatelliteTuners_SelectedIndexChanged);
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(31, 78);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(84, 13);
            this.label151.TabIndex = 208;
            this.label151.Text = "Sat>IP Frontend";
            // 
            // cboLNBType
            // 
            this.cboLNBType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLNBType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLNBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLNBType.FormattingEnabled = true;
            this.cboLNBType.Location = new System.Drawing.Point(566, 106);
            this.cboLNBType.Name = "cboLNBType";
            this.cboLNBType.Size = new System.Drawing.Size(221, 21);
            this.cboLNBType.TabIndex = 218;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(451, 108);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(55, 13);
            this.label111.TabIndex = 217;
            this.label111.Text = "LNB Type";
            // 
            // btLNBDefaults
            // 
            this.btLNBDefaults.Location = new System.Drawing.Point(678, 13);
            this.btLNBDefaults.Name = "btLNBDefaults";
            this.btLNBDefaults.Size = new System.Drawing.Size(75, 23);
            this.btLNBDefaults.TabIndex = 212;
            this.btLNBDefaults.Text = "Defaults";
            this.btLNBDefaults.UseVisualStyleBackColor = true;
            this.btLNBDefaults.Click += new System.EventHandler(this.btLNBDefaults_Click);
            // 
            // txtLNBSwitch
            // 
            this.txtLNBSwitch.Location = new System.Drawing.Point(566, 75);
            this.txtLNBSwitch.Name = "txtLNBSwitch";
            this.txtLNBSwitch.Size = new System.Drawing.Size(92, 20);
            this.txtLNBSwitch.TabIndex = 216;
            // 
            // txtLNBHigh
            // 
            this.txtLNBHigh.Location = new System.Drawing.Point(566, 45);
            this.txtLNBHigh.Name = "txtLNBHigh";
            this.txtLNBHigh.Size = new System.Drawing.Size(92, 20);
            this.txtLNBHigh.TabIndex = 214;
            // 
            // cboDVBSScanningFrequency
            // 
            this.cboDVBSScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDVBSScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDVBSScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDVBSScanningFrequency.FormattingEnabled = true;
            this.cboDVBSScanningFrequency.ItemHeight = 13;
            this.cboDVBSScanningFrequency.Location = new System.Drawing.Point(130, 45);
            this.cboDVBSScanningFrequency.MaxDropDownItems = 20;
            this.cboDVBSScanningFrequency.Name = "cboDVBSScanningFrequency";
            this.cboDVBSScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboDVBSScanningFrequency.TabIndex = 205;
            this.cboDVBSScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboDVBSScanningFrequency_SelectedIndexChanged);
            // 
            // lblSatelliteFrequency
            // 
            this.lblSatelliteFrequency.AutoSize = true;
            this.lblSatelliteFrequency.Location = new System.Drawing.Point(31, 48);
            this.lblSatelliteFrequency.Name = "lblSatelliteFrequency";
            this.lblSatelliteFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblSatelliteFrequency.TabIndex = 204;
            this.lblSatelliteFrequency.Text = "Frequency";
            // 
            // txtLNBLow
            // 
            this.txtLNBLow.Location = new System.Drawing.Point(566, 15);
            this.txtLNBLow.Name = "txtLNBLow";
            this.txtLNBLow.Size = new System.Drawing.Size(92, 20);
            this.txtLNBLow.TabIndex = 211;
            // 
            // cboSatellite
            // 
            this.cboSatellite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboSatellite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSatellite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSatellite.FormattingEnabled = true;
            this.cboSatellite.Location = new System.Drawing.Point(130, 15);
            this.cboSatellite.MaxDropDownItems = 20;
            this.cboSatellite.Name = "cboSatellite";
            this.cboSatellite.Size = new System.Drawing.Size(277, 21);
            this.cboSatellite.TabIndex = 203;
            this.cboSatellite.SelectedIndexChanged += new System.EventHandler(this.cboSatellite_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 202;
            this.label4.Text = "Satellite";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(451, 78);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 215;
            this.label31.Text = "LNB Switch";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(451, 18);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(79, 13);
            this.label33.TabIndex = 210;
            this.label33.Text = "LNB Low Band";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(451, 48);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(81, 13);
            this.label32.TabIndex = 213;
            this.label32.Text = "LNB High Band";
            // 
            // tbpTerrestrial
            // 
            this.tbpTerrestrial.Controls.Add(this.groupBox12);
            this.tbpTerrestrial.Controls.Add(this.udDvbtSatIpFrontend);
            this.tbpTerrestrial.Controls.Add(this.label152);
            this.tbpTerrestrial.Controls.Add(this.lblArea);
            this.tbpTerrestrial.Controls.Add(this.cboArea);
            this.tbpTerrestrial.Controls.Add(this.lblCountry);
            this.tbpTerrestrial.Controls.Add(this.cboCountry);
            this.tbpTerrestrial.Controls.Add(this.cboDVBTScanningFrequency);
            this.tbpTerrestrial.Controls.Add(this.lblTerrestrialFrequency);
            this.tbpTerrestrial.Location = new System.Drawing.Point(4, 22);
            this.tbpTerrestrial.Name = "tbpTerrestrial";
            this.tbpTerrestrial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTerrestrial.Size = new System.Drawing.Size(909, 392);
            this.tbpTerrestrial.TabIndex = 1;
            this.tbpTerrestrial.Text = "DVB Terrestrial";
            this.tbpTerrestrial.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.clbTerrestrialTuners);
            this.groupBox12.Location = new System.Drawing.Point(17, 157);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(876, 219);
            this.groupBox12.TabIndex = 227;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Tuners";
            // 
            // clbTerrestrialTuners
            // 
            this.clbTerrestrialTuners.CheckOnClick = true;
            this.clbTerrestrialTuners.FormattingEnabled = true;
            this.clbTerrestrialTuners.Location = new System.Drawing.Point(13, 19);
            this.clbTerrestrialTuners.Name = "clbTerrestrialTuners";
            this.clbTerrestrialTuners.Size = new System.Drawing.Size(846, 184);
            this.clbTerrestrialTuners.TabIndex = 101;
            this.clbTerrestrialTuners.SelectedIndexChanged += new System.EventHandler(this.clbTerrestrialTuners_SelectedIndexChanged);
            // 
            // udDvbtSatIpFrontend
            // 
            this.udDvbtSatIpFrontend.Items.Add("n/a");
            this.udDvbtSatIpFrontend.Items.Add("1");
            this.udDvbtSatIpFrontend.Items.Add("2");
            this.udDvbtSatIpFrontend.Items.Add("3");
            this.udDvbtSatIpFrontend.Items.Add("4");
            this.udDvbtSatIpFrontend.Location = new System.Drawing.Point(154, 119);
            this.udDvbtSatIpFrontend.Name = "udDvbtSatIpFrontend";
            this.udDvbtSatIpFrontend.ReadOnly = true;
            this.udDvbtSatIpFrontend.Size = new System.Drawing.Size(50, 20);
            this.udDvbtSatIpFrontend.TabIndex = 226;
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(24, 121);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(84, 13);
            this.label152.TabIndex = 225;
            this.label152.Text = "Sat>IP Frontend";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(24, 57);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(29, 13);
            this.lblArea.TabIndex = 131;
            this.lblArea.Text = "Area";
            // 
            // cboArea
            // 
            this.cboArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(154, 53);
            this.cboArea.MaxDropDownItems = 20;
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(221, 21);
            this.cboArea.TabIndex = 221;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(24, 25);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(43, 13);
            this.lblCountry.TabIndex = 129;
            this.lblCountry.Text = "Country";
            // 
            // cboCountry
            // 
            this.cboCountry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCountry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Location = new System.Drawing.Point(154, 22);
            this.cboCountry.MaxDropDownItems = 20;
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(221, 21);
            this.cboCountry.TabIndex = 220;
            this.cboCountry.SelectedIndexChanged += new System.EventHandler(this.cboCountry_SelectedIndexChanged);
            // 
            // cboDVBTScanningFrequency
            // 
            this.cboDVBTScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDVBTScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDVBTScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDVBTScanningFrequency.FormattingEnabled = true;
            this.cboDVBTScanningFrequency.Location = new System.Drawing.Point(154, 86);
            this.cboDVBTScanningFrequency.MaxDropDownItems = 20;
            this.cboDVBTScanningFrequency.Name = "cboDVBTScanningFrequency";
            this.cboDVBTScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboDVBTScanningFrequency.TabIndex = 222;
            // 
            // lblTerrestrialFrequency
            // 
            this.lblTerrestrialFrequency.AutoSize = true;
            this.lblTerrestrialFrequency.Location = new System.Drawing.Point(24, 89);
            this.lblTerrestrialFrequency.Name = "lblTerrestrialFrequency";
            this.lblTerrestrialFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblTerrestrialFrequency.TabIndex = 133;
            this.lblTerrestrialFrequency.Text = "Frequency";
            // 
            // tbpCable
            // 
            this.tbpCable.Controls.Add(this.groupBox13);
            this.tbpCable.Controls.Add(this.udDvbcSatIpFrontend);
            this.tbpCable.Controls.Add(this.label153);
            this.tbpCable.Controls.Add(this.label54);
            this.tbpCable.Controls.Add(this.cboCable);
            this.tbpCable.Controls.Add(this.lblCableFrequency);
            this.tbpCable.Controls.Add(this.cboCableScanningFrequency);
            this.tbpCable.Location = new System.Drawing.Point(4, 22);
            this.tbpCable.Name = "tbpCable";
            this.tbpCable.Size = new System.Drawing.Size(909, 392);
            this.tbpCable.TabIndex = 2;
            this.tbpCable.Text = "DVB Cable";
            this.tbpCable.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.clbCableTuners);
            this.groupBox13.Location = new System.Drawing.Point(22, 121);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(872, 250);
            this.groupBox13.TabIndex = 237;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Tuners";
            // 
            // clbCableTuners
            // 
            this.clbCableTuners.CheckOnClick = true;
            this.clbCableTuners.FormattingEnabled = true;
            this.clbCableTuners.Location = new System.Drawing.Point(15, 20);
            this.clbCableTuners.Name = "clbCableTuners";
            this.clbCableTuners.Size = new System.Drawing.Size(839, 214);
            this.clbCableTuners.TabIndex = 101;
            this.clbCableTuners.SelectedIndexChanged += new System.EventHandler(this.clbCableTuners_SelectedIndexChanged);
            // 
            // udDvbcSatIpFrontend
            // 
            this.udDvbcSatIpFrontend.Items.Add("n/a");
            this.udDvbcSatIpFrontend.Items.Add("1");
            this.udDvbcSatIpFrontend.Items.Add("2");
            this.udDvbcSatIpFrontend.Items.Add("3");
            this.udDvbcSatIpFrontend.Items.Add("4");
            this.udDvbcSatIpFrontend.Location = new System.Drawing.Point(139, 86);
            this.udDvbcSatIpFrontend.Name = "udDvbcSatIpFrontend";
            this.udDvbcSatIpFrontend.ReadOnly = true;
            this.udDvbcSatIpFrontend.Size = new System.Drawing.Size(50, 20);
            this.udDvbcSatIpFrontend.TabIndex = 236;
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(29, 88);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(84, 13);
            this.label153.TabIndex = 235;
            this.label153.Text = "Sat>IP Frontend";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(29, 21);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(46, 13);
            this.label54.TabIndex = 137;
            this.label54.Text = "Provider";
            // 
            // cboCable
            // 
            this.cboCable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCable.FormattingEnabled = true;
            this.cboCable.Location = new System.Drawing.Point(139, 18);
            this.cboCable.MaxDropDownItems = 20;
            this.cboCable.Name = "cboCable";
            this.cboCable.Size = new System.Drawing.Size(221, 21);
            this.cboCable.TabIndex = 231;
            this.cboCable.SelectedIndexChanged += new System.EventHandler(this.cboCable_SelectedIndexChanged);
            // 
            // lblCableFrequency
            // 
            this.lblCableFrequency.AutoSize = true;
            this.lblCableFrequency.Location = new System.Drawing.Point(29, 55);
            this.lblCableFrequency.Name = "lblCableFrequency";
            this.lblCableFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblCableFrequency.TabIndex = 139;
            this.lblCableFrequency.Text = "Frequency";
            // 
            // cboCableScanningFrequency
            // 
            this.cboCableScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCableScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCableScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCableScanningFrequency.FormattingEnabled = true;
            this.cboCableScanningFrequency.Location = new System.Drawing.Point(139, 52);
            this.cboCableScanningFrequency.MaxDropDownItems = 20;
            this.cboCableScanningFrequency.Name = "cboCableScanningFrequency";
            this.cboCableScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboCableScanningFrequency.TabIndex = 232;
            // 
            // tbpAtsc
            // 
            this.tbpAtsc.Controls.Add(this.groupBox18);
            this.tbpAtsc.Controls.Add(this.label50);
            this.tbpAtsc.Controls.Add(this.cboAtscProvider);
            this.tbpAtsc.Controls.Add(this.lblAtscChannel);
            this.tbpAtsc.Controls.Add(this.cboAtscScanningFrequency);
            this.tbpAtsc.Location = new System.Drawing.Point(4, 22);
            this.tbpAtsc.Name = "tbpAtsc";
            this.tbpAtsc.Size = new System.Drawing.Size(909, 392);
            this.tbpAtsc.TabIndex = 3;
            this.tbpAtsc.Text = "ATSC";
            this.tbpAtsc.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.clbAtscTuners);
            this.groupBox18.Location = new System.Drawing.Point(22, 95);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(873, 280);
            this.groupBox18.TabIndex = 245;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Tuners";
            // 
            // clbAtscTuners
            // 
            this.clbAtscTuners.CheckOnClick = true;
            this.clbAtscTuners.FormattingEnabled = true;
            this.clbAtscTuners.Location = new System.Drawing.Point(17, 22);
            this.clbAtscTuners.Name = "clbAtscTuners";
            this.clbAtscTuners.Size = new System.Drawing.Size(839, 244);
            this.clbAtscTuners.TabIndex = 101;
            this.clbAtscTuners.SelectedIndexChanged += new System.EventHandler(this.clbAtscTuners_SelectedIndexChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(30, 30);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(46, 13);
            this.label50.TabIndex = 137;
            this.label50.Text = "Provider";
            // 
            // cboAtscProvider
            // 
            this.cboAtscProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAtscProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAtscProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAtscProvider.FormattingEnabled = true;
            this.cboAtscProvider.Location = new System.Drawing.Point(140, 27);
            this.cboAtscProvider.MaxDropDownItems = 20;
            this.cboAtscProvider.Name = "cboAtscProvider";
            this.cboAtscProvider.Size = new System.Drawing.Size(221, 21);
            this.cboAtscProvider.TabIndex = 241;
            this.cboAtscProvider.SelectedIndexChanged += new System.EventHandler(this.cboAtscProvider_SelectedIndexChanged);
            // 
            // lblAtscChannel
            // 
            this.lblAtscChannel.AutoSize = true;
            this.lblAtscChannel.Location = new System.Drawing.Point(30, 64);
            this.lblAtscChannel.Name = "lblAtscChannel";
            this.lblAtscChannel.Size = new System.Drawing.Size(46, 13);
            this.lblAtscChannel.TabIndex = 139;
            this.lblAtscChannel.Text = "Channel";
            // 
            // cboAtscScanningFrequency
            // 
            this.cboAtscScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAtscScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAtscScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAtscScanningFrequency.FormattingEnabled = true;
            this.cboAtscScanningFrequency.Location = new System.Drawing.Point(140, 61);
            this.cboAtscScanningFrequency.MaxDropDownItems = 20;
            this.cboAtscScanningFrequency.Name = "cboAtscScanningFrequency";
            this.cboAtscScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboAtscScanningFrequency.TabIndex = 242;
            // 
            // tbpClearQAM
            // 
            this.tbpClearQAM.Controls.Add(this.groupBox19);
            this.tbpClearQAM.Controls.Add(this.label57);
            this.tbpClearQAM.Controls.Add(this.cboClearQamProvider);
            this.tbpClearQAM.Controls.Add(this.lblClearQamChannel);
            this.tbpClearQAM.Controls.Add(this.cboClearQamScanningFrequency);
            this.tbpClearQAM.Location = new System.Drawing.Point(4, 22);
            this.tbpClearQAM.Name = "tbpClearQAM";
            this.tbpClearQAM.Size = new System.Drawing.Size(909, 392);
            this.tbpClearQAM.TabIndex = 4;
            this.tbpClearQAM.Text = "Clear QAM";
            this.tbpClearQAM.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.clbClearQamTuners);
            this.groupBox19.Location = new System.Drawing.Point(22, 97);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(871, 273);
            this.groupBox19.TabIndex = 255;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Tuners";
            // 
            // clbClearQamTuners
            // 
            this.clbClearQamTuners.CheckOnClick = true;
            this.clbClearQamTuners.FormattingEnabled = true;
            this.clbClearQamTuners.Location = new System.Drawing.Point(13, 25);
            this.clbClearQamTuners.Name = "clbClearQamTuners";
            this.clbClearQamTuners.Size = new System.Drawing.Size(839, 229);
            this.clbClearQamTuners.TabIndex = 101;
            this.clbClearQamTuners.SelectedIndexChanged += new System.EventHandler(this.clbClearQamTuners_SelectedIndexChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(29, 30);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(46, 13);
            this.label57.TabIndex = 143;
            this.label57.Text = "Provider";
            // 
            // cboClearQamProvider
            // 
            this.cboClearQamProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboClearQamProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClearQamProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClearQamProvider.FormattingEnabled = true;
            this.cboClearQamProvider.Location = new System.Drawing.Point(139, 27);
            this.cboClearQamProvider.MaxDropDownItems = 20;
            this.cboClearQamProvider.Name = "cboClearQamProvider";
            this.cboClearQamProvider.Size = new System.Drawing.Size(221, 21);
            this.cboClearQamProvider.TabIndex = 251;
            this.cboClearQamProvider.SelectedIndexChanged += new System.EventHandler(this.cboClearQamProvider_SelectedIndexChanged);
            // 
            // lblClearQamChannel
            // 
            this.lblClearQamChannel.AutoSize = true;
            this.lblClearQamChannel.Location = new System.Drawing.Point(29, 64);
            this.lblClearQamChannel.Name = "lblClearQamChannel";
            this.lblClearQamChannel.Size = new System.Drawing.Size(46, 13);
            this.lblClearQamChannel.TabIndex = 145;
            this.lblClearQamChannel.Text = "Channel";
            // 
            // cboClearQamScanningFrequency
            // 
            this.cboClearQamScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboClearQamScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClearQamScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClearQamScanningFrequency.FormattingEnabled = true;
            this.cboClearQamScanningFrequency.Location = new System.Drawing.Point(139, 61);
            this.cboClearQamScanningFrequency.MaxDropDownItems = 20;
            this.cboClearQamScanningFrequency.Name = "cboClearQamScanningFrequency";
            this.cboClearQamScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboClearQamScanningFrequency.TabIndex = 252;
            // 
            // tbpISDBSatellite
            // 
            this.tbpISDBSatellite.Controls.Add(this.groupBox20);
            this.tbpISDBSatellite.Controls.Add(this.btISDBLNBDefaults);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBSwitch);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBHigh);
            this.tbpISDBSatellite.Controls.Add(this.cboISDBSScanningFrequency);
            this.tbpISDBSatellite.Controls.Add(this.lblISDBSFrequency);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBLow);
            this.tbpISDBSatellite.Controls.Add(this.cboISDBSatellite);
            this.tbpISDBSatellite.Controls.Add(this.label63);
            this.tbpISDBSatellite.Controls.Add(this.label64);
            this.tbpISDBSatellite.Controls.Add(this.label65);
            this.tbpISDBSatellite.Controls.Add(this.label66);
            this.tbpISDBSatellite.Location = new System.Drawing.Point(4, 22);
            this.tbpISDBSatellite.Name = "tbpISDBSatellite";
            this.tbpISDBSatellite.Size = new System.Drawing.Size(909, 392);
            this.tbpISDBSatellite.TabIndex = 5;
            this.tbpISDBSatellite.Text = "ISDB Satellite";
            this.tbpISDBSatellite.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.clbISDBSatelliteTuners);
            this.groupBox20.Location = new System.Drawing.Point(21, 119);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(872, 251);
            this.groupBox20.TabIndex = 227;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Tuners";
            // 
            // clbISDBSatelliteTuners
            // 
            this.clbISDBSatelliteTuners.CheckOnClick = true;
            this.clbISDBSatelliteTuners.FormattingEnabled = true;
            this.clbISDBSatelliteTuners.Location = new System.Drawing.Point(15, 21);
            this.clbISDBSatelliteTuners.Name = "clbISDBSatelliteTuners";
            this.clbISDBSatelliteTuners.Size = new System.Drawing.Size(839, 214);
            this.clbISDBSatelliteTuners.TabIndex = 101;
            this.clbISDBSatelliteTuners.SelectedIndexChanged += new System.EventHandler(this.clbISDBSatelliteTuners_SelectedIndexChanged);
            // 
            // btISDBLNBDefaults
            // 
            this.btISDBLNBDefaults.Location = new System.Drawing.Point(692, 20);
            this.btISDBLNBDefaults.Name = "btISDBLNBDefaults";
            this.btISDBLNBDefaults.Size = new System.Drawing.Size(75, 23);
            this.btISDBLNBDefaults.TabIndex = 225;
            this.btISDBLNBDefaults.Text = "Defaults";
            this.btISDBLNBDefaults.UseVisualStyleBackColor = true;
            this.btISDBLNBDefaults.Click += new System.EventHandler(this.btISDBLNBDefaults_Click);
            // 
            // txtISDBLNBSwitch
            // 
            this.txtISDBLNBSwitch.Location = new System.Drawing.Point(565, 86);
            this.txtISDBLNBSwitch.Name = "txtISDBLNBSwitch";
            this.txtISDBLNBSwitch.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBSwitch.TabIndex = 223;
            // 
            // txtISDBLNBHigh
            // 
            this.txtISDBLNBHigh.Location = new System.Drawing.Point(565, 54);
            this.txtISDBLNBHigh.Name = "txtISDBLNBHigh";
            this.txtISDBLNBHigh.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBHigh.TabIndex = 222;
            // 
            // cboISDBSScanningFrequency
            // 
            this.cboISDBSScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBSScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBSScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBSScanningFrequency.FormattingEnabled = true;
            this.cboISDBSScanningFrequency.ItemHeight = 13;
            this.cboISDBSScanningFrequency.Location = new System.Drawing.Point(153, 54);
            this.cboISDBSScanningFrequency.MaxDropDownItems = 20;
            this.cboISDBSScanningFrequency.Name = "cboISDBSScanningFrequency";
            this.cboISDBSScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboISDBSScanningFrequency.TabIndex = 219;
            // 
            // lblISDBSFrequency
            // 
            this.lblISDBSFrequency.AutoSize = true;
            this.lblISDBSFrequency.Location = new System.Drawing.Point(28, 57);
            this.lblISDBSFrequency.Name = "lblISDBSFrequency";
            this.lblISDBSFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblISDBSFrequency.TabIndex = 216;
            this.lblISDBSFrequency.Text = "Frequency";
            // 
            // txtISDBLNBLow
            // 
            this.txtISDBLNBLow.Location = new System.Drawing.Point(565, 22);
            this.txtISDBLNBLow.Name = "txtISDBLNBLow";
            this.txtISDBLNBLow.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBLow.TabIndex = 221;
            // 
            // cboISDBSatellite
            // 
            this.cboISDBSatellite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBSatellite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBSatellite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBSatellite.FormattingEnabled = true;
            this.cboISDBSatellite.Location = new System.Drawing.Point(153, 22);
            this.cboISDBSatellite.MaxDropDownItems = 20;
            this.cboISDBSatellite.Name = "cboISDBSatellite";
            this.cboISDBSatellite.Size = new System.Drawing.Size(221, 21);
            this.cboISDBSatellite.TabIndex = 218;
            this.cboISDBSatellite.SelectedIndexChanged += new System.EventHandler(this.cboISDBSatellite_SelectedIndexChanged);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(28, 27);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(44, 13);
            this.label63.TabIndex = 215;
            this.label63.Text = "Satellite";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(440, 89);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(63, 13);
            this.label64.TabIndex = 213;
            this.label64.Text = "LNB Switch";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(440, 25);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(79, 13);
            this.label65.TabIndex = 211;
            this.label65.Text = "LNB Low Band";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(440, 57);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(81, 13);
            this.label66.TabIndex = 212;
            this.label66.Text = "LNB High Band";
            // 
            // tbpISDBTerrestrial
            // 
            this.tbpISDBTerrestrial.Controls.Add(this.groupBox21);
            this.tbpISDBTerrestrial.Controls.Add(this.label67);
            this.tbpISDBTerrestrial.Controls.Add(this.cboISDBTProvider);
            this.tbpISDBTerrestrial.Controls.Add(this.lblISDBTChannel);
            this.tbpISDBTerrestrial.Controls.Add(this.cboISDBTScanningFrequency);
            this.tbpISDBTerrestrial.Location = new System.Drawing.Point(4, 22);
            this.tbpISDBTerrestrial.Name = "tbpISDBTerrestrial";
            this.tbpISDBTerrestrial.Size = new System.Drawing.Size(909, 392);
            this.tbpISDBTerrestrial.TabIndex = 6;
            this.tbpISDBTerrestrial.Text = "ISDB Terrestrial";
            this.tbpISDBTerrestrial.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.clbISDBTerrestrialTuners);
            this.groupBox21.Location = new System.Drawing.Point(22, 97);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(870, 273);
            this.groupBox21.TabIndex = 252;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Tuners";
            // 
            // clbISDBTerrestrialTuners
            // 
            this.clbISDBTerrestrialTuners.CheckOnClick = true;
            this.clbISDBTerrestrialTuners.FormattingEnabled = true;
            this.clbISDBTerrestrialTuners.Location = new System.Drawing.Point(13, 25);
            this.clbISDBTerrestrialTuners.Name = "clbISDBTerrestrialTuners";
            this.clbISDBTerrestrialTuners.Size = new System.Drawing.Size(839, 229);
            this.clbISDBTerrestrialTuners.TabIndex = 101;
            this.clbISDBTerrestrialTuners.SelectedIndexChanged += new System.EventHandler(this.clbISDBTerrestrialTuners_SelectedIndexChanged);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(30, 30);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(48, 13);
            this.label67.TabIndex = 245;
            this.label67.Text = "Location";
            // 
            // cboISDBTProvider
            // 
            this.cboISDBTProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBTProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBTProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBTProvider.FormattingEnabled = true;
            this.cboISDBTProvider.Location = new System.Drawing.Point(140, 27);
            this.cboISDBTProvider.MaxDropDownItems = 20;
            this.cboISDBTProvider.Name = "cboISDBTProvider";
            this.cboISDBTProvider.Size = new System.Drawing.Size(221, 21);
            this.cboISDBTProvider.TabIndex = 248;
            this.cboISDBTProvider.SelectedIndexChanged += new System.EventHandler(this.cboISDBTProvider_SelectedIndexChanged);
            // 
            // lblISDBTChannel
            // 
            this.lblISDBTChannel.AutoSize = true;
            this.lblISDBTChannel.Location = new System.Drawing.Point(30, 64);
            this.lblISDBTChannel.Name = "lblISDBTChannel";
            this.lblISDBTChannel.Size = new System.Drawing.Size(46, 13);
            this.lblISDBTChannel.TabIndex = 246;
            this.lblISDBTChannel.Text = "Channel";
            // 
            // cboISDBTScanningFrequency
            // 
            this.cboISDBTScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBTScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBTScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBTScanningFrequency.FormattingEnabled = true;
            this.cboISDBTScanningFrequency.Location = new System.Drawing.Point(140, 61);
            this.cboISDBTScanningFrequency.MaxDropDownItems = 20;
            this.cboISDBTScanningFrequency.Name = "cboISDBTScanningFrequency";
            this.cboISDBTScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboISDBTScanningFrequency.TabIndex = 249;
            // 
            // tbpFile
            // 
            this.tbpFile.Controls.Add(this.btDeliveryFileBrowse);
            this.tbpFile.Controls.Add(this.tbDeliveryFilePath);
            this.tbpFile.Controls.Add(this.label140);
            this.tbpFile.Location = new System.Drawing.Point(4, 22);
            this.tbpFile.Name = "tbpFile";
            this.tbpFile.Size = new System.Drawing.Size(909, 392);
            this.tbpFile.TabIndex = 7;
            this.tbpFile.Text = "File";
            this.tbpFile.UseVisualStyleBackColor = true;
            // 
            // btDeliveryFileBrowse
            // 
            this.btDeliveryFileBrowse.Location = new System.Drawing.Point(809, 23);
            this.btDeliveryFileBrowse.Name = "btDeliveryFileBrowse";
            this.btDeliveryFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.btDeliveryFileBrowse.TabIndex = 511;
            this.btDeliveryFileBrowse.Text = "Browse";
            this.btDeliveryFileBrowse.UseVisualStyleBackColor = true;
            this.btDeliveryFileBrowse.Click += new System.EventHandler(this.tbDeliveryFileBrowse_Click);
            // 
            // tbDeliveryFilePath
            // 
            this.tbDeliveryFilePath.Location = new System.Drawing.Point(136, 25);
            this.tbDeliveryFilePath.Name = "tbDeliveryFilePath";
            this.tbDeliveryFilePath.Size = new System.Drawing.Size(656, 20);
            this.tbDeliveryFilePath.TabIndex = 510;
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(21, 28);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(29, 13);
            this.label140.TabIndex = 509;
            this.label140.Text = "Path";
            // 
            // tbpStream
            // 
            this.tbpStream.Controls.Add(this.nudStreamMulticastSourcePort);
            this.tbpStream.Controls.Add(this.label147);
            this.tbpStream.Controls.Add(this.tbStreamMulticastSourceIP);
            this.tbpStream.Controls.Add(this.label146);
            this.tbpStream.Controls.Add(this.tbStreamPath);
            this.tbpStream.Controls.Add(this.label148);
            this.tbpStream.Controls.Add(this.btFindIPAddress);
            this.tbpStream.Controls.Add(this.nudStreamPortNumber);
            this.tbpStream.Controls.Add(this.label145);
            this.tbpStream.Controls.Add(this.cboStreamProtocol);
            this.tbpStream.Controls.Add(this.label144);
            this.tbpStream.Controls.Add(this.tbStreamIpAddress);
            this.tbpStream.Controls.Add(this.label143);
            this.tbpStream.Location = new System.Drawing.Point(4, 22);
            this.tbpStream.Name = "tbpStream";
            this.tbpStream.Size = new System.Drawing.Size(909, 392);
            this.tbpStream.TabIndex = 8;
            this.tbpStream.Text = "Stream";
            this.tbpStream.UseVisualStyleBackColor = true;
            // 
            // nudStreamMulticastSourcePort
            // 
            this.nudStreamMulticastSourcePort.Location = new System.Drawing.Point(264, 209);
            this.nudStreamMulticastSourcePort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudStreamMulticastSourcePort.Name = "nudStreamMulticastSourcePort";
            this.nudStreamMulticastSourcePort.Size = new System.Drawing.Size(80, 20);
            this.nudStreamMulticastSourcePort.TabIndex = 527;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(17, 211);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(105, 13);
            this.label147.TabIndex = 526;
            this.label147.Text = "Multicast source port";
            // 
            // tbStreamMulticastSourceIP
            // 
            this.tbStreamMulticastSourceIP.Location = new System.Drawing.Point(264, 169);
            this.tbStreamMulticastSourceIP.Name = "tbStreamMulticastSourceIP";
            this.tbStreamMulticastSourceIP.Size = new System.Drawing.Size(150, 20);
            this.tbStreamMulticastSourceIP.TabIndex = 525;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(18, 172);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(124, 13);
            this.label146.TabIndex = 524;
            this.label146.Text = "Multicast source address";
            // 
            // tbStreamPath
            // 
            this.tbStreamPath.Location = new System.Drawing.Point(264, 133);
            this.tbStreamPath.Name = "tbStreamPath";
            this.tbStreamPath.Size = new System.Drawing.Size(274, 20);
            this.tbStreamPath.TabIndex = 519;
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(18, 136);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(156, 13);
            this.label148.TabIndex = 518;
            this.label148.Text = "Path (optional for RTSP/HTTP)";
            // 
            // btFindIPAddress
            // 
            this.btFindIPAddress.Location = new System.Drawing.Point(468, 53);
            this.btFindIPAddress.Name = "btFindIPAddress";
            this.btFindIPAddress.Size = new System.Drawing.Size(75, 23);
            this.btFindIPAddress.TabIndex = 514;
            this.btFindIPAddress.Text = "Find";
            this.btFindIPAddress.UseVisualStyleBackColor = true;
            // 
            // nudStreamPortNumber
            // 
            this.nudStreamPortNumber.Location = new System.Drawing.Point(264, 95);
            this.nudStreamPortNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudStreamPortNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStreamPortNumber.Name = "nudStreamPortNumber";
            this.nudStreamPortNumber.Size = new System.Drawing.Size(80, 20);
            this.nudStreamPortNumber.TabIndex = 517;
            this.nudStreamPortNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(18, 97);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(64, 13);
            this.label145.TabIndex = 516;
            this.label145.Text = "Port number";
            // 
            // cboStreamProtocol
            // 
            this.cboStreamProtocol.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboStreamProtocol.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStreamProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStreamProtocol.FormattingEnabled = true;
            this.cboStreamProtocol.Items.AddRange(new object[] {
            "Rtsp",
            "Rtp",
            "Udp",
            "Http"});
            this.cboStreamProtocol.Location = new System.Drawing.Point(264, 16);
            this.cboStreamProtocol.Name = "cboStreamProtocol";
            this.cboStreamProtocol.Size = new System.Drawing.Size(150, 21);
            this.cboStreamProtocol.TabIndex = 511;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(18, 19);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(46, 13);
            this.label144.TabIndex = 510;
            this.label144.Text = "Protocol";
            // 
            // tbStreamIpAddress
            // 
            this.tbStreamIpAddress.Location = new System.Drawing.Point(264, 55);
            this.tbStreamIpAddress.Name = "tbStreamIpAddress";
            this.tbStreamIpAddress.Size = new System.Drawing.Size(193, 20);
            this.tbStreamIpAddress.TabIndex = 513;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(18, 58);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(225, 13);
            this.label143.TabIndex = 512;
            this.label143.Text = "IP address (not needed for RTP/UDP unicast)";
            // 
            // FrequencySelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcDeliverySystem);
            this.Name = "FrequencySelectionControl";
            this.Size = new System.Drawing.Size(931, 449);
            this.tbcDeliverySystem.ResumeLayout(false);
            this.tbpSatellite.ResumeLayout(false);
            this.tbpSatellite.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tbpTerrestrial.ResumeLayout(false);
            this.tbpTerrestrial.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.tbpCable.ResumeLayout(false);
            this.tbpCable.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.tbpAtsc.ResumeLayout(false);
            this.tbpAtsc.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.tbpClearQAM.ResumeLayout(false);
            this.tbpClearQAM.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.tbpISDBSatellite.ResumeLayout(false);
            this.tbpISDBSatellite.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.tbpISDBTerrestrial.ResumeLayout(false);
            this.tbpISDBTerrestrial.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.tbpFile.ResumeLayout(false);
            this.tbpFile.PerformLayout();
            this.tbpStream.ResumeLayout(false);
            this.tbpStream.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStreamMulticastSourcePort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStreamPortNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcDeliverySystem;
        private System.Windows.Forms.TabPage tbpSatellite;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.CheckBox cbSwitchAfterTune;
        private System.Windows.Forms.ComboBox cboDiseqcHandler;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.CheckBox cbUseDiseqcCommands;
        private System.Windows.Forms.CheckBox cbDisableDriverDiseqc;
        private System.Windows.Forms.CheckBox cbRepeatDiseqc;
        private System.Windows.Forms.CheckBox cbSwitchAfterPlay;
        private System.Windows.Forms.CheckBox cbUseSafeDiseqc;
        private System.Windows.Forms.ComboBox cboDiseqc;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DomainUpDown udDvbsSatIpFrontend;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbSatelliteTuners;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.ComboBox cboLNBType;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Button btLNBDefaults;
        private System.Windows.Forms.TextBox txtLNBSwitch;
        private System.Windows.Forms.TextBox txtLNBHigh;
        private System.Windows.Forms.ComboBox cboDVBSScanningFrequency;
        private System.Windows.Forms.Label lblSatelliteFrequency;
        private System.Windows.Forms.TextBox txtLNBLow;
        private System.Windows.Forms.ComboBox cboSatellite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TabPage tbpTerrestrial;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckedListBox clbTerrestrialTuners;
        private System.Windows.Forms.DomainUpDown udDvbtSatIpFrontend;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.ComboBox cboDVBTScanningFrequency;
        private System.Windows.Forms.Label lblTerrestrialFrequency;
        private System.Windows.Forms.TabPage tbpCable;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckedListBox clbCableTuners;
        private System.Windows.Forms.DomainUpDown udDvbcSatIpFrontend;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox cboCable;
        private System.Windows.Forms.Label lblCableFrequency;
        private System.Windows.Forms.ComboBox cboCableScanningFrequency;
        private System.Windows.Forms.TabPage tbpAtsc;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.CheckedListBox clbAtscTuners;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cboAtscProvider;
        private System.Windows.Forms.Label lblAtscChannel;
        private System.Windows.Forms.ComboBox cboAtscScanningFrequency;
        private System.Windows.Forms.TabPage tbpClearQAM;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.CheckedListBox clbClearQamTuners;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cboClearQamProvider;
        private System.Windows.Forms.Label lblClearQamChannel;
        private System.Windows.Forms.ComboBox cboClearQamScanningFrequency;
        private System.Windows.Forms.TabPage tbpISDBSatellite;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.CheckedListBox clbISDBSatelliteTuners;
        private System.Windows.Forms.Button btISDBLNBDefaults;
        private System.Windows.Forms.TextBox txtISDBLNBSwitch;
        private System.Windows.Forms.TextBox txtISDBLNBHigh;
        private System.Windows.Forms.ComboBox cboISDBSScanningFrequency;
        private System.Windows.Forms.Label lblISDBSFrequency;
        private System.Windows.Forms.TextBox txtISDBLNBLow;
        private System.Windows.Forms.ComboBox cboISDBSatellite;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TabPage tbpISDBTerrestrial;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.CheckedListBox clbISDBTerrestrialTuners;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ComboBox cboISDBTProvider;
        private System.Windows.Forms.Label lblISDBTChannel;
        private System.Windows.Forms.ComboBox cboISDBTScanningFrequency;
        private System.Windows.Forms.TabPage tbpFile;
        private System.Windows.Forms.Button btDeliveryFileBrowse;
        private System.Windows.Forms.TextBox tbDeliveryFilePath;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.TabPage tbpStream;
        private System.Windows.Forms.NumericUpDown nudStreamMulticastSourcePort;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.TextBox tbStreamMulticastSourceIP;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.TextBox tbStreamPath;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Button btFindIPAddress;
        private System.Windows.Forms.NumericUpDown nudStreamPortNumber;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.ComboBox cboStreamProtocol;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.TextBox tbStreamIpAddress;
        private System.Windows.Forms.Label label143;

    }
}
