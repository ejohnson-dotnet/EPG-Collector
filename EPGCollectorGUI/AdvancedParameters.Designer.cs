namespace EPGCentre
{
    partial class AdvancedParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedParameters));
            this.gpMiscellaneous = new System.Windows.Forms.GroupBox();
            this.cbSidMatchOnly = new System.Windows.Forms.CheckBox();
            this.cbCreateChannels = new System.Windows.Forms.CheckBox();
            this.cbUseStationLogos = new System.Windows.Forms.CheckBox();
            this.cboUseDescriptionAs = new System.Windows.Forms.ComboBox();
            this.lblUseDescriptionAs = new System.Windows.Forms.Label();
            this.dudEPGDays = new System.Windows.Forms.DomainUpDown();
            this.lblMaxDays = new System.Windows.Forms.Label();
            this.cboConvertTables = new System.Windows.Forms.ComboBox();
            this.lblByteConversion = new System.Windows.Forms.Label();
            this.cboEITFormatting = new System.Windows.Forms.ComboBox();
            this.lblTextFormatting = new System.Windows.Forms.Label();
            this.cbProcessAllStations = new System.Windows.Forms.CheckBox();
            this.cbCustomCategoriesOverride = new System.Windows.Forms.CheckBox();
            this.cbUseFreeSatTables = new System.Windows.Forms.CheckBox();
            this.cbUseContentSubtype = new System.Windows.Forms.CheckBox();
            this.gpLocationInformation = new System.Windows.Forms.GroupBox();
            this.cboCharacterSetPriority = new System.Windows.Forms.ComboBox();
            this.lblCharacterSetPriority = new System.Windows.Forms.Label();
            this.cboInputLanguage = new System.Windows.Forms.ComboBox();
            this.lblInputLanguage = new System.Windows.Forms.Label();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.cboLocationRegion = new System.Windows.Forms.ComboBox();
            this.cboLocationArea = new System.Windows.Forms.ComboBox();
            this.cboCharacterSet = new System.Windows.Forms.ComboBox();
            this.lblCharacterSet = new System.Windows.Forms.Label();
            this.lblLocationArea = new System.Windows.Forms.Label();
            this.lblLocationRegion = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.gpCustomPids = new System.Windows.Forms.GroupBox();
            this.nudSDTPid = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDishNetworkPid = new System.Windows.Forms.NumericUpDown();
            this.lblDishNetwork = new System.Windows.Forms.Label();
            this.nudMHW2Pid3 = new System.Windows.Forms.NumericUpDown();
            this.nudMHW2Pid2 = new System.Windows.Forms.NumericUpDown();
            this.nudMHW2Pid1 = new System.Windows.Forms.NumericUpDown();
            this.nudMHW1Pid2 = new System.Windows.Forms.NumericUpDown();
            this.nudMHW1Pid1 = new System.Windows.Forms.NumericUpDown();
            this.nudEITPid = new System.Windows.Forms.NumericUpDown();
            this.lblMHW2 = new System.Windows.Forms.Label();
            this.lblMHW1 = new System.Windows.Forms.Label();
            this.lblEIT = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.cbHexPids = new System.Windows.Forms.CheckBox();
            this.cboCarouselProfiles = new System.Windows.Forms.ComboBox();
            this.lblCarouselProfile = new System.Windows.Forms.Label();
            this.gpMiscellaneous.SuspendLayout();
            this.gpLocationInformation.SuspendLayout();
            this.gpCustomPids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSDTPid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDishNetworkPid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW1Pid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW1Pid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEITPid)).BeginInit();
            this.SuspendLayout();
            // 
            // gpMiscellaneous
            // 
            this.gpMiscellaneous.Controls.Add(this.cboCarouselProfiles);
            this.gpMiscellaneous.Controls.Add(this.lblCarouselProfile);
            this.gpMiscellaneous.Controls.Add(this.cbSidMatchOnly);
            this.gpMiscellaneous.Controls.Add(this.cbCreateChannels);
            this.gpMiscellaneous.Controls.Add(this.cbUseStationLogos);
            this.gpMiscellaneous.Controls.Add(this.cboUseDescriptionAs);
            this.gpMiscellaneous.Controls.Add(this.lblUseDescriptionAs);
            this.gpMiscellaneous.Controls.Add(this.dudEPGDays);
            this.gpMiscellaneous.Controls.Add(this.lblMaxDays);
            this.gpMiscellaneous.Controls.Add(this.cboConvertTables);
            this.gpMiscellaneous.Controls.Add(this.lblByteConversion);
            this.gpMiscellaneous.Controls.Add(this.cboEITFormatting);
            this.gpMiscellaneous.Controls.Add(this.lblTextFormatting);
            this.gpMiscellaneous.Controls.Add(this.cbProcessAllStations);
            this.gpMiscellaneous.Controls.Add(this.cbCustomCategoriesOverride);
            this.gpMiscellaneous.Controls.Add(this.cbUseFreeSatTables);
            this.gpMiscellaneous.Controls.Add(this.cbUseContentSubtype);
            this.gpMiscellaneous.Location = new System.Drawing.Point(12, 18);
            this.gpMiscellaneous.Name = "gpMiscellaneous";
            this.gpMiscellaneous.Size = new System.Drawing.Size(889, 178);
            this.gpMiscellaneous.TabIndex = 1;
            this.gpMiscellaneous.TabStop = false;
            this.gpMiscellaneous.Text = "Miscellaneous Options";
            // 
            // cbSidMatchOnly
            // 
            this.cbSidMatchOnly.AutoSize = true;
            this.cbSidMatchOnly.Location = new System.Drawing.Point(20, 154);
            this.cbSidMatchOnly.Name = "cbSidMatchOnly";
            this.cbSidMatchOnly.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSidMatchOnly.Size = new System.Drawing.Size(269, 17);
            this.cbSidMatchOnly.TabIndex = 8;
            this.cbSidMatchOnly.Text = "Match EPG data with channel using service ID only";
            this.cbSidMatchOnly.UseVisualStyleBackColor = true;
            // 
            // cbCreateChannels
            // 
            this.cbCreateChannels.AutoSize = true;
            this.cbCreateChannels.Location = new System.Drawing.Point(20, 131);
            this.cbCreateChannels.Name = "cbCreateChannels";
            this.cbCreateChannels.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbCreateChannels.Size = new System.Drawing.Size(256, 17);
            this.cbCreateChannels.TabIndex = 7;
            this.cbCreateChannels.Text = "Create channels from EPG data if they don\'t exist";
            this.cbCreateChannels.UseVisualStyleBackColor = true;
            // 
            // cbUseStationLogos
            // 
            this.cbUseStationLogos.AutoSize = true;
            this.cbUseStationLogos.Location = new System.Drawing.Point(20, 108);
            this.cbUseStationLogos.Name = "cbUseStationLogos";
            this.cbUseStationLogos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbUseStationLogos.Size = new System.Drawing.Size(225, 17);
            this.cbUseStationLogos.TabIndex = 6;
            this.cbUseStationLogos.Text = "Extract channel logos from broadcast data";
            this.cbUseStationLogos.UseVisualStyleBackColor = true;
            // 
            // cboUseDescriptionAs
            // 
            this.cboUseDescriptionAs.FormattingEnabled = true;
            this.cboUseDescriptionAs.Items.AddRange(new object[] {
            "Default",
            "Category",
            "Subtitle",
            "Ignore"});
            this.cboUseDescriptionAs.Location = new System.Drawing.Point(666, 105);
            this.cboUseDescriptionAs.Name = "cboUseDescriptionAs";
            this.cboUseDescriptionAs.Size = new System.Drawing.Size(177, 21);
            this.cboUseDescriptionAs.TabIndex = 16;
            // 
            // lblUseDescriptionAs
            // 
            this.lblUseDescriptionAs.AutoSize = true;
            this.lblUseDescriptionAs.Location = new System.Drawing.Point(455, 108);
            this.lblUseDescriptionAs.Name = "lblUseDescriptionAs";
            this.lblUseDescriptionAs.Size = new System.Drawing.Size(167, 13);
            this.lblUseDescriptionAs.TabIndex = 15;
            this.lblUseDescriptionAs.Text = "Use the programme description as";
            // 
            // dudEPGDays
            // 
            this.dudEPGDays.Location = new System.Drawing.Point(666, 25);
            this.dudEPGDays.Name = "dudEPGDays";
            this.dudEPGDays.ReadOnly = true;
            this.dudEPGDays.Size = new System.Drawing.Size(61, 20);
            this.dudEPGDays.TabIndex = 10;
            this.dudEPGDays.Text = "All";
            // 
            // lblMaxDays
            // 
            this.lblMaxDays.AutoSize = true;
            this.lblMaxDays.Location = new System.Drawing.Point(455, 26);
            this.lblMaxDays.Name = "lblMaxDays";
            this.lblMaxDays.Size = new System.Drawing.Size(196, 13);
            this.lblMaxDays.TabIndex = 9;
            this.lblMaxDays.Text = "Maximum number of days data to collect";
            // 
            // cboConvertTables
            // 
            this.cboConvertTables.FormattingEnabled = true;
            this.cboConvertTables.Items.AddRange(new object[] {
            "Not used"});
            this.cboConvertTables.Location = new System.Drawing.Point(666, 78);
            this.cboConvertTables.Name = "cboConvertTables";
            this.cboConvertTables.Size = new System.Drawing.Size(177, 21);
            this.cboConvertTables.TabIndex = 14;
            // 
            // lblByteConversion
            // 
            this.lblByteConversion.AutoSize = true;
            this.lblByteConversion.Location = new System.Drawing.Point(455, 81);
            this.lblByteConversion.Name = "lblByteConversion";
            this.lblByteConversion.Size = new System.Drawing.Size(109, 13);
            this.lblByteConversion.TabIndex = 13;
            this.lblByteConversion.Text = "Byte conversion table";
            // 
            // cboEITFormatting
            // 
            this.cboEITFormatting.FormattingEnabled = true;
            this.cboEITFormatting.Items.AddRange(new object[] {
            "Remove",
            "Replace with space",
            "Convert using default",
            "Use byte conversion table"});
            this.cboEITFormatting.Location = new System.Drawing.Point(666, 51);
            this.cboEITFormatting.Name = "cboEITFormatting";
            this.cboEITFormatting.Size = new System.Drawing.Size(177, 21);
            this.cboEITFormatting.TabIndex = 12;
            this.cboEITFormatting.SelectedIndexChanged += new System.EventHandler(this.cboEITFormatting_SelectedIndexChanged);
            // 
            // lblTextFormatting
            // 
            this.lblTextFormatting.AutoSize = true;
            this.lblTextFormatting.Location = new System.Drawing.Point(455, 54);
            this.lblTextFormatting.Name = "lblTextFormatting";
            this.lblTextFormatting.Size = new System.Drawing.Size(167, 13);
            this.lblTextFormatting.TabIndex = 11;
            this.lblTextFormatting.Text = "Process text formatting characters";
            // 
            // cbProcessAllStations
            // 
            this.cbProcessAllStations.AutoSize = true;
            this.cbProcessAllStations.Location = new System.Drawing.Point(20, 85);
            this.cbProcessAllStations.Name = "cbProcessAllStations";
            this.cbProcessAllStations.Size = new System.Drawing.Size(215, 17);
            this.cbProcessAllStations.TabIndex = 5;
            this.cbProcessAllStations.Text = "Process all channels irrespective of type";
            this.cbProcessAllStations.UseVisualStyleBackColor = true;
            // 
            // cbCustomCategoriesOverride
            // 
            this.cbCustomCategoriesOverride.AutoSize = true;
            this.cbCustomCategoriesOverride.Location = new System.Drawing.Point(20, 64);
            this.cbCustomCategoriesOverride.Name = "cbCustomCategoriesOverride";
            this.cbCustomCategoriesOverride.Size = new System.Drawing.Size(297, 17);
            this.cbCustomCategoriesOverride.TabIndex = 4;
            this.cbCustomCategoriesOverride.Text = "Custom program categories override broadcast categories";
            this.cbCustomCategoriesOverride.UseVisualStyleBackColor = true;
            // 
            // cbUseFreeSatTables
            // 
            this.cbUseFreeSatTables.AutoSize = true;
            this.cbUseFreeSatTables.Location = new System.Drawing.Point(20, 43);
            this.cbUseFreeSatTables.Name = "cbUseFreeSatTables";
            this.cbUseFreeSatTables.Size = new System.Drawing.Size(294, 17);
            this.cbUseFreeSatTables.TabIndex = 3;
            this.cbUseFreeSatTables.Text = "Use FreeSat Huffman tables to translate compressed text";
            this.cbUseFreeSatTables.UseVisualStyleBackColor = true;
            // 
            // cbUseContentSubtype
            // 
            this.cbUseContentSubtype.AutoSize = true;
            this.cbUseContentSubtype.Location = new System.Drawing.Point(20, 22);
            this.cbUseContentSubtype.Name = "cbUseContentSubtype";
            this.cbUseContentSubtype.Size = new System.Drawing.Size(246, 17);
            this.cbUseContentSubtype.TabIndex = 2;
            this.cbUseContentSubtype.Text = "Use program category subtype when decoding";
            this.cbUseContentSubtype.UseVisualStyleBackColor = true;
            // 
            // gpLocationInformation
            // 
            this.gpLocationInformation.Controls.Add(this.cboCharacterSetPriority);
            this.gpLocationInformation.Controls.Add(this.lblCharacterSetPriority);
            this.gpLocationInformation.Controls.Add(this.cboInputLanguage);
            this.gpLocationInformation.Controls.Add(this.lblInputLanguage);
            this.gpLocationInformation.Controls.Add(this.cboLocation);
            this.gpLocationInformation.Controls.Add(this.cboLocationRegion);
            this.gpLocationInformation.Controls.Add(this.cboLocationArea);
            this.gpLocationInformation.Controls.Add(this.cboCharacterSet);
            this.gpLocationInformation.Controls.Add(this.lblCharacterSet);
            this.gpLocationInformation.Controls.Add(this.lblLocationArea);
            this.gpLocationInformation.Controls.Add(this.lblLocationRegion);
            this.gpLocationInformation.Controls.Add(this.lblLocation);
            this.gpLocationInformation.Location = new System.Drawing.Point(12, 215);
            this.gpLocationInformation.Name = "gpLocationInformation";
            this.gpLocationInformation.Size = new System.Drawing.Size(889, 98);
            this.gpLocationInformation.TabIndex = 30;
            this.gpLocationInformation.TabStop = false;
            this.gpLocationInformation.Text = "Location Information";
            // 
            // cboCharacterSetPriority
            // 
            this.cboCharacterSetPriority.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCharacterSetPriority.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCharacterSetPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCharacterSetPriority.FormattingEnabled = true;
            this.cboCharacterSetPriority.Items.AddRange(new object[] {
            "User selection",
            "Broadcast data"});
            this.cboCharacterSetPriority.Location = new System.Drawing.Point(470, 65);
            this.cboCharacterSetPriority.MaxDropDownItems = 20;
            this.cboCharacterSetPriority.Name = "cboCharacterSetPriority";
            this.cboCharacterSetPriority.Size = new System.Drawing.Size(120, 21);
            this.cboCharacterSetPriority.TabIndex = 40;
            // 
            // lblCharacterSetPriority
            // 
            this.lblCharacterSetPriority.AutoSize = true;
            this.lblCharacterSetPriority.Location = new System.Drawing.Point(343, 69);
            this.lblCharacterSetPriority.Name = "lblCharacterSetPriority";
            this.lblCharacterSetPriority.Size = new System.Drawing.Size(106, 13);
            this.lblCharacterSetPriority.TabIndex = 39;
            this.lblCharacterSetPriority.Text = "Character Set Priority";
            // 
            // cboInputLanguage
            // 
            this.cboInputLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboInputLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboInputLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInputLanguage.FormattingEnabled = true;
            this.cboInputLanguage.Location = new System.Drawing.Point(716, 65);
            this.cboInputLanguage.MaxDropDownItems = 20;
            this.cboInputLanguage.Name = "cboInputLanguage";
            this.cboInputLanguage.Size = new System.Drawing.Size(161, 21);
            this.cboInputLanguage.Sorted = true;
            this.cboInputLanguage.TabIndex = 42;
            // 
            // lblInputLanguage
            // 
            this.lblInputLanguage.AutoSize = true;
            this.lblInputLanguage.Location = new System.Drawing.Point(614, 69);
            this.lblInputLanguage.Name = "lblInputLanguage";
            this.lblInputLanguage.Size = new System.Drawing.Size(82, 13);
            this.lblInputLanguage.TabIndex = 41;
            this.lblInputLanguage.Text = "Input Language";
            // 
            // cboLocation
            // 
            this.cboLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(96, 26);
            this.cboLocation.MaxDropDownItems = 20;
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(216, 21);
            this.cboLocation.Sorted = true;
            this.cboLocation.TabIndex = 32;
            this.cboLocation.SelectedIndexChanged += new System.EventHandler(this.cboLocation_SelectedIndexChanged);
            // 
            // cboLocationRegion
            // 
            this.cboLocationRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLocationRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLocationRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocationRegion.FormattingEnabled = true;
            this.cboLocationRegion.Location = new System.Drawing.Point(661, 27);
            this.cboLocationRegion.MaxDropDownItems = 20;
            this.cboLocationRegion.Name = "cboLocationRegion";
            this.cboLocationRegion.Size = new System.Drawing.Size(216, 21);
            this.cboLocationRegion.TabIndex = 36;
            // 
            // cboLocationArea
            // 
            this.cboLocationArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLocationArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLocationArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocationArea.FormattingEnabled = true;
            this.cboLocationArea.Location = new System.Drawing.Point(374, 26);
            this.cboLocationArea.MaxDropDownItems = 20;
            this.cboLocationArea.Name = "cboLocationArea";
            this.cboLocationArea.Size = new System.Drawing.Size(216, 21);
            this.cboLocationArea.TabIndex = 34;
            this.cboLocationArea.SelectedIndexChanged += new System.EventHandler(this.cboLocationArea_SelectedIndexChanged);
            // 
            // cboCharacterSet
            // 
            this.cboCharacterSet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCharacterSet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCharacterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCharacterSet.FormattingEnabled = true;
            this.cboCharacterSet.Location = new System.Drawing.Point(96, 65);
            this.cboCharacterSet.MaxDropDownItems = 20;
            this.cboCharacterSet.Name = "cboCharacterSet";
            this.cboCharacterSet.Size = new System.Drawing.Size(215, 21);
            this.cboCharacterSet.Sorted = true;
            this.cboCharacterSet.TabIndex = 38;
            // 
            // lblCharacterSet
            // 
            this.lblCharacterSet.AutoSize = true;
            this.lblCharacterSet.Location = new System.Drawing.Point(10, 69);
            this.lblCharacterSet.Name = "lblCharacterSet";
            this.lblCharacterSet.Size = new System.Drawing.Size(72, 13);
            this.lblCharacterSet.TabIndex = 37;
            this.lblCharacterSet.Text = "Character Set";
            // 
            // lblLocationArea
            // 
            this.lblLocationArea.AutoSize = true;
            this.lblLocationArea.Location = new System.Drawing.Point(340, 30);
            this.lblLocationArea.Name = "lblLocationArea";
            this.lblLocationArea.Size = new System.Drawing.Size(29, 13);
            this.lblLocationArea.TabIndex = 33;
            this.lblLocationArea.Text = "Area";
            // 
            // lblLocationRegion
            // 
            this.lblLocationRegion.AutoSize = true;
            this.lblLocationRegion.Location = new System.Drawing.Point(614, 30);
            this.lblLocationRegion.Name = "lblLocationRegion";
            this.lblLocationRegion.Size = new System.Drawing.Size(41, 13);
            this.lblLocationRegion.TabIndex = 35;
            this.lblLocationRegion.Text = "Region";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(10, 30);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(43, 13);
            this.lblLocation.TabIndex = 31;
            this.lblLocation.Text = "Country";
            // 
            // gpCustomPids
            // 
            this.gpCustomPids.Controls.Add(this.nudSDTPid);
            this.gpCustomPids.Controls.Add(this.label1);
            this.gpCustomPids.Controls.Add(this.nudDishNetworkPid);
            this.gpCustomPids.Controls.Add(this.lblDishNetwork);
            this.gpCustomPids.Controls.Add(this.nudMHW2Pid3);
            this.gpCustomPids.Controls.Add(this.nudMHW2Pid2);
            this.gpCustomPids.Controls.Add(this.nudMHW2Pid1);
            this.gpCustomPids.Controls.Add(this.nudMHW1Pid2);
            this.gpCustomPids.Controls.Add(this.nudMHW1Pid1);
            this.gpCustomPids.Controls.Add(this.nudEITPid);
            this.gpCustomPids.Controls.Add(this.lblMHW2);
            this.gpCustomPids.Controls.Add(this.lblMHW1);
            this.gpCustomPids.Controls.Add(this.lblEIT);
            this.gpCustomPids.Location = new System.Drawing.Point(12, 329);
            this.gpCustomPids.Name = "gpCustomPids";
            this.gpCustomPids.Size = new System.Drawing.Size(889, 167);
            this.gpCustomPids.TabIndex = 60;
            this.gpCustomPids.TabStop = false;
            this.gpCustomPids.Text = "Custom PID\'s";
            // 
            // nudSDTPid
            // 
            this.nudSDTPid.Location = new System.Drawing.Point(113, 22);
            this.nudSDTPid.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudSDTPid.Name = "nudSDTPid";
            this.nudSDTPid.Size = new System.Drawing.Size(63, 20);
            this.nudSDTPid.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "SDT";
            // 
            // nudDishNetworkPid
            // 
            this.nudDishNetworkPid.Location = new System.Drawing.Point(114, 136);
            this.nudDishNetworkPid.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudDishNetworkPid.Name = "nudDishNetworkPid";
            this.nudDishNetworkPid.Size = new System.Drawing.Size(63, 20);
            this.nudDishNetworkPid.TabIndex = 74;
            // 
            // lblDishNetwork
            // 
            this.lblDishNetwork.AutoSize = true;
            this.lblDishNetwork.Location = new System.Drawing.Point(11, 139);
            this.lblDishNetwork.Name = "lblDishNetwork";
            this.lblDishNetwork.Size = new System.Drawing.Size(71, 13);
            this.lblDishNetwork.TabIndex = 73;
            this.lblDishNetwork.Text = "Dish Network";
            // 
            // nudMHW2Pid3
            // 
            this.nudMHW2Pid3.Location = new System.Drawing.Point(313, 108);
            this.nudMHW2Pid3.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudMHW2Pid3.Name = "nudMHW2Pid3";
            this.nudMHW2Pid3.Size = new System.Drawing.Size(63, 20);
            this.nudMHW2Pid3.TabIndex = 72;
            // 
            // nudMHW2Pid2
            // 
            this.nudMHW2Pid2.Location = new System.Drawing.Point(213, 108);
            this.nudMHW2Pid2.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudMHW2Pid2.Name = "nudMHW2Pid2";
            this.nudMHW2Pid2.Size = new System.Drawing.Size(63, 20);
            this.nudMHW2Pid2.TabIndex = 71;
            // 
            // nudMHW2Pid1
            // 
            this.nudMHW2Pid1.Location = new System.Drawing.Point(113, 108);
            this.nudMHW2Pid1.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudMHW2Pid1.Name = "nudMHW2Pid1";
            this.nudMHW2Pid1.Size = new System.Drawing.Size(63, 20);
            this.nudMHW2Pid1.TabIndex = 70;
            // 
            // nudMHW1Pid2
            // 
            this.nudMHW1Pid2.Location = new System.Drawing.Point(213, 79);
            this.nudMHW1Pid2.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudMHW1Pid2.Name = "nudMHW1Pid2";
            this.nudMHW1Pid2.Size = new System.Drawing.Size(63, 20);
            this.nudMHW1Pid2.TabIndex = 68;
            // 
            // nudMHW1Pid1
            // 
            this.nudMHW1Pid1.Location = new System.Drawing.Point(113, 79);
            this.nudMHW1Pid1.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudMHW1Pid1.Name = "nudMHW1Pid1";
            this.nudMHW1Pid1.Size = new System.Drawing.Size(63, 20);
            this.nudMHW1Pid1.TabIndex = 67;
            // 
            // nudEITPid
            // 
            this.nudEITPid.Location = new System.Drawing.Point(113, 50);
            this.nudEITPid.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.nudEITPid.Name = "nudEITPid";
            this.nudEITPid.Size = new System.Drawing.Size(63, 20);
            this.nudEITPid.TabIndex = 65;
            // 
            // lblMHW2
            // 
            this.lblMHW2.AutoSize = true;
            this.lblMHW2.Location = new System.Drawing.Point(10, 111);
            this.lblMHW2.Name = "lblMHW2";
            this.lblMHW2.Size = new System.Drawing.Size(83, 13);
            this.lblMHW2.TabIndex = 69;
            this.lblMHW2.Text = "MediaHighway2";
            // 
            // lblMHW1
            // 
            this.lblMHW1.AutoSize = true;
            this.lblMHW1.Location = new System.Drawing.Point(10, 82);
            this.lblMHW1.Name = "lblMHW1";
            this.lblMHW1.Size = new System.Drawing.Size(83, 13);
            this.lblMHW1.TabIndex = 66;
            this.lblMHW1.Text = "MediaHighway1";
            // 
            // lblEIT
            // 
            this.lblEIT.AutoSize = true;
            this.lblEIT.Location = new System.Drawing.Point(10, 53);
            this.lblEIT.Name = "lblEIT";
            this.lblEIT.Size = new System.Drawing.Size(24, 13);
            this.lblEIT.TabIndex = 64;
            this.lblEIT.Text = "EIT";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(479, 506);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 101;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(365, 506);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 100;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // cbHexPids
            // 
            this.cbHexPids.AutoSize = true;
            this.cbHexPids.Location = new System.Drawing.Point(126, 327);
            this.cbHexPids.Name = "cbHexPids";
            this.cbHexPids.Size = new System.Drawing.Size(122, 17);
            this.cbHexPids.TabIndex = 61;
            this.cbHexPids.Text = "Hexadecimal Values";
            this.cbHexPids.UseVisualStyleBackColor = true;
            this.cbHexPids.CheckedChanged += new System.EventHandler(this.cbHexPids_CheckedChanged);
            // 
            // cboCarouselProfiles
            // 
            this.cboCarouselProfiles.FormattingEnabled = true;
            this.cboCarouselProfiles.Items.AddRange(new object[] {
            "Not used"});
            this.cboCarouselProfiles.Location = new System.Drawing.Point(666, 132);
            this.cboCarouselProfiles.Name = "cboCarouselProfiles";
            this.cboCarouselProfiles.Size = new System.Drawing.Size(177, 21);
            this.cboCarouselProfiles.TabIndex = 18;
            // 
            // lblCarouselProfile
            // 
            this.lblCarouselProfile.AutoSize = true;
            this.lblCarouselProfile.Location = new System.Drawing.Point(455, 135);
            this.lblCarouselProfile.Name = "lblCarouselProfile";
            this.lblCarouselProfile.Size = new System.Drawing.Size(79, 13);
            this.lblCarouselProfile.TabIndex = 17;
            this.lblCarouselProfile.Text = "Carousel profile";
            // 
            // AdvancedParameters
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(918, 540);
            this.Controls.Add(this.cbHexPids);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gpCustomPids);
            this.Controls.Add(this.gpLocationInformation);
            this.Controls.Add(this.gpMiscellaneous);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedParameters";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - Advanced Parameters For Frequency ";
            this.gpMiscellaneous.ResumeLayout(false);
            this.gpMiscellaneous.PerformLayout();
            this.gpLocationInformation.ResumeLayout(false);
            this.gpLocationInformation.PerformLayout();
            this.gpCustomPids.ResumeLayout(false);
            this.gpCustomPids.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSDTPid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDishNetworkPid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW2Pid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW1Pid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMHW1Pid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEITPid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpMiscellaneous;
        private System.Windows.Forms.ComboBox cboConvertTables;
        private System.Windows.Forms.Label lblByteConversion;
        private System.Windows.Forms.ComboBox cboEITFormatting;
        private System.Windows.Forms.Label lblTextFormatting;
        private System.Windows.Forms.CheckBox cbProcessAllStations;
        private System.Windows.Forms.CheckBox cbCustomCategoriesOverride;
        private System.Windows.Forms.CheckBox cbUseFreeSatTables;
        private System.Windows.Forms.CheckBox cbUseContentSubtype;
        private System.Windows.Forms.GroupBox gpLocationInformation;
        private System.Windows.Forms.ComboBox cboCharacterSetPriority;
        private System.Windows.Forms.Label lblCharacterSetPriority;
        private System.Windows.Forms.ComboBox cboInputLanguage;
        private System.Windows.Forms.Label lblInputLanguage;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.ComboBox cboLocationRegion;
        private System.Windows.Forms.ComboBox cboLocationArea;
        private System.Windows.Forms.ComboBox cboCharacterSet;
        private System.Windows.Forms.Label lblCharacterSet;
        private System.Windows.Forms.Label lblLocationArea;
        private System.Windows.Forms.Label lblLocationRegion;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.GroupBox gpCustomPids;
        private System.Windows.Forms.NumericUpDown nudDishNetworkPid;
        private System.Windows.Forms.Label lblDishNetwork;
        private System.Windows.Forms.NumericUpDown nudMHW2Pid3;
        private System.Windows.Forms.NumericUpDown nudMHW2Pid2;
        private System.Windows.Forms.NumericUpDown nudMHW2Pid1;
        private System.Windows.Forms.NumericUpDown nudMHW1Pid2;
        private System.Windows.Forms.NumericUpDown nudMHW1Pid1;
        private System.Windows.Forms.NumericUpDown nudEITPid;
        private System.Windows.Forms.Label lblMHW2;
        private System.Windows.Forms.Label lblMHW1;
        private System.Windows.Forms.Label lblEIT;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.CheckBox cbHexPids;
        private System.Windows.Forms.DomainUpDown dudEPGDays;
        private System.Windows.Forms.Label lblMaxDays;
        private System.Windows.Forms.ComboBox cboUseDescriptionAs;
        private System.Windows.Forms.Label lblUseDescriptionAs;
        private System.Windows.Forms.CheckBox cbUseStationLogos;
        private System.Windows.Forms.CheckBox cbCreateChannels;
        private System.Windows.Forms.CheckBox cbSidMatchOnly;
        private System.Windows.Forms.NumericUpDown nudSDTPid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCarouselProfiles;
        private System.Windows.Forms.Label lblCarouselProfile;
    }
}