namespace EPGCentre
{
    /// <summary>
    /// Change Collector Parameters.
    /// </summary>
    partial class CollectorParametersControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nudScanRetries = new System.Windows.Forms.NumericUpDown();
            this.nudSignalLockTimeout = new System.Windows.Forms.NumericUpDown();
            this.tbcParameters = new System.Windows.Forms.TabControl();
            this.tabDVBS = new System.Windows.Forms.TabPage();
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
            this.btAddSatellite = new System.Windows.Forms.Button();
            this.txtLNBSwitch = new System.Windows.Forms.TextBox();
            this.cboDVBSCollectionType = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtLNBHigh = new System.Windows.Forms.TextBox();
            this.cboDVBSScanningFrequency = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
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
            this.btAddTerrestrial = new System.Windows.Forms.Button();
            this.lblArea = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.cboDVBTCollectionType = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.cboDVBTScanningFrequency = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbpCable = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.clbCableTuners = new System.Windows.Forms.CheckedListBox();
            this.udDvbcSatIpFrontend = new System.Windows.Forms.DomainUpDown();
            this.label153 = new System.Windows.Forms.Label();
            this.btAddCable = new System.Windows.Forms.Button();
            this.label54 = new System.Windows.Forms.Label();
            this.cboCable = new System.Windows.Forms.ComboBox();
            this.cboCableCollectionType = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.cboCableScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpAtsc = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.clbAtscTuners = new System.Windows.Forms.CheckedListBox();
            this.btAddAtsc = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.cboAtscProvider = new System.Windows.Forms.ComboBox();
            this.cboAtscCollectionType = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.cboAtscScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpClearQAM = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.clbClearQamTuners = new System.Windows.Forms.CheckedListBox();
            this.btAddClearQam = new System.Windows.Forms.Button();
            this.label57 = new System.Windows.Forms.Label();
            this.cboClearQamProvider = new System.Windows.Forms.ComboBox();
            this.cboClearQamCollectionType = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.cboClearQamScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpISDBSatellite = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.clbISDBSatelliteTuners = new System.Windows.Forms.CheckedListBox();
            this.btISDBLNBDefaults = new System.Windows.Forms.Button();
            this.btAddISDBSatellite = new System.Windows.Forms.Button();
            this.txtISDBLNBSwitch = new System.Windows.Forms.TextBox();
            this.cboISDBSCollectionType = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtISDBLNBHigh = new System.Windows.Forms.TextBox();
            this.cboISDBSScanningFrequency = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.txtISDBLNBLow = new System.Windows.Forms.TextBox();
            this.cboISDBSatellite = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.tbpISDBTerrestrial = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.clbISDBTerrestrialTuners = new System.Windows.Forms.CheckedListBox();
            this.btAddISDBTerrestrial = new System.Windows.Forms.Button();
            this.label67 = new System.Windows.Forms.Label();
            this.cboISDBTProvider = new System.Windows.Forms.ComboBox();
            this.cboISDBTCollectionType = new System.Windows.Forms.ComboBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.cboISDBTScanningFrequency = new System.Windows.Forms.ComboBox();
            this.tbpFile = new System.Windows.Forms.TabPage();
            this.btDeliveryFileAdd = new System.Windows.Forms.Button();
            this.cboDeliveryFileCollectionType = new System.Windows.Forms.ComboBox();
            this.label141 = new System.Windows.Forms.Label();
            this.tbDeliveryFileBrowse = new System.Windows.Forms.Button();
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
            this.btStreamAdd = new System.Windows.Forms.Button();
            this.nudStreamPortNumber = new System.Windows.Forms.NumericUpDown();
            this.label145 = new System.Windows.Forms.Label();
            this.cboStreamProtocol = new System.Windows.Forms.ComboBox();
            this.label144 = new System.Windows.Forms.Label();
            this.cboStreamCollectionType = new System.Windows.Forms.ComboBox();
            this.label142 = new System.Windows.Forms.Label();
            this.tbStreamIpAddress = new System.Windows.Forms.TextBox();
            this.label143 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btTuningLoadFromScan = new System.Windows.Forms.Button();
            this.btChange = new System.Windows.Forms.Button();
            this.btTuningParameters = new System.Windows.Forms.Button();
            this.btSelectedFrequencyDetails = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.lvSelectedFrequencies = new System.Windows.Forms.ListView();
            this.Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Provider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Collection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btDelete = new System.Windows.Forms.Button();
            this.tabXMLTV = new System.Windows.Forms.TabPage();
            this.cbDontCreateImportChannels = new System.Windows.Forms.CheckBox();
            this.btXmltvClear = new System.Windows.Forms.Button();
            this.btXmltvExcludeAll = new System.Windows.Forms.Button();
            this.btXmltvIncludeAll = new System.Windows.Forms.Button();
            this.btXmltvLoadFiles = new System.Windows.Forms.Button();
            this.dgXmltvChannelChanges = new System.Windows.Forms.DataGridView();
            this.xmltvDisplayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmltvChannelNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmltvExcludedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.xmltvNewNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmltvCallSignColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmltvChannelChangeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label117 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btXmltvDelete = new System.Windows.Forms.Button();
            this.lvXmltvSelectedFiles = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label112 = new System.Windows.Forms.Label();
            this.gpbFiles = new System.Windows.Forms.GroupBox();
            this.cbXmltvProcessLiveTag = new System.Windows.Forms.CheckBox();
            this.label101 = new System.Windows.Forms.Label();
            this.cboXmltvStoreImagesLocally = new System.Windows.Forms.ComboBox();
            this.cbXmltvSetPreviouslyShownDefault = new System.Windows.Forms.CheckBox();
            this.label160 = new System.Windows.Forms.Label();
            this.cbXmltvProcessNewTag = new System.Windows.Forms.CheckBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.cbIgnoreEpisodeTags = new System.Windows.Forms.CheckBox();
            this.label108 = new System.Windows.Forms.Label();
            this.cboXmltvTimeZone = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.cboXmltvIdFormat = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbXmltvNoLookup = new System.Windows.Forms.CheckBox();
            this.label120 = new System.Windows.Forms.Label();
            this.cboXmltvPrecedence = new System.Windows.Forms.ComboBox();
            this.label115 = new System.Windows.Forms.Label();
            this.btXmltvAdd = new System.Windows.Forms.Button();
            this.cboXmltvLanguage = new System.Windows.Forms.ComboBox();
            this.label114 = new System.Windows.Forms.Label();
            this.tbXmltvPath = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.btXmltvBrowse = new System.Windows.Forms.Button();
            this.tabSchedulesDirect = new System.Windows.Forms.TabPage();
            this.cbSdEnabled = new System.Windows.Forms.CheckBox();
            this.gpSchedulesDirect = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.cboSdStoreImagesLocally = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSdUseLongDescriptions = new System.Windows.Forms.CheckBox();
            this.label158 = new System.Windows.Forms.Label();
            this.btSdClear = new System.Windows.Forms.Button();
            this.btSdExcludeAll = new System.Windows.Forms.Button();
            this.dgSdChannels = new System.Windows.Forms.DataGridView();
            this.sdChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelCallSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelLineup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelIdentification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelExcluded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sdChannelUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelUserNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdChannelUserCallSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btSdIncludeAll = new System.Windows.Forms.Button();
            this.btSdRemove = new System.Windows.Forms.Button();
            this.label164 = new System.Windows.Forms.Label();
            this.label165 = new System.Windows.Forms.Label();
            this.btSdAdd = new System.Windows.Forms.Button();
            this.lvSdLineups = new System.Windows.Forms.ListView();
            this.sdName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sdLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sdTransport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btSdChangeLineups = new System.Windows.Forms.Button();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cbDvbViewerOutputEnabled = new System.Windows.Forms.CheckBox();
            this.cbWmcOutputEnabled = new System.Windows.Forms.CheckBox();
            this.cbXmltvOutputEnabled = new System.Windows.Forms.CheckBox();
            this.gpXmltvOptions = new System.Windows.Forms.GroupBox();
            this.tbXmltvIconTagPathPrefix = new System.Windows.Forms.TextBox();
            this.label150 = new System.Windows.Forms.Label();
            this.cbCreatePlexEpisodeNumTag = new System.Windows.Forms.CheckBox();
            this.cbPrefixSubtitleWithSeasonEpisode = new System.Windows.Forms.CheckBox();
            this.cbPrefixDescWithAirDate = new System.Windows.Forms.CheckBox();
            this.cbOmitPartNumber = new System.Windows.Forms.CheckBox();
            this.tbChannelLogoPath = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.btBrowseLogoPath = new System.Windows.Forms.Button();
            this.cbElementPerTag = new System.Windows.Forms.CheckBox();
            this.cbCreateADTag = new System.Windows.Forms.CheckBox();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbUseLCN = new System.Windows.Forms.CheckBox();
            this.cboEpisodeTagFormat = new System.Windows.Forms.ComboBox();
            this.label119 = new System.Windows.Forms.Label();
            this.cboChannelIDFormat = new System.Windows.Forms.ComboBox();
            this.label118 = new System.Windows.Forms.Label();
            this.gpDVBViewerOptions = new System.Windows.Forms.GroupBox();
            this.tbDVBViewerIPAddress = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.cbDVBViewerSubtitleVisible = new System.Windows.Forms.CheckBox();
            this.label56 = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.cbDVBViewerClear = new System.Windows.Forms.CheckBox();
            this.cbRecordingServiceImport = new System.Windows.Forms.CheckBox();
            this.cbUseDVBViewer = new System.Windows.Forms.CheckBox();
            this.cbDVBViewerImport = new System.Windows.Forms.CheckBox();
            this.gpWMCOptions = new System.Windows.Forms.GroupBox();
            this.cbWmcRunTasks = new System.Windows.Forms.CheckBox();
            this.cbWmcNoDummyAffiliates = new System.Windows.Forms.CheckBox();
            this.cboWMCSeries = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cbDisableInbandLoader = new System.Windows.Forms.CheckBox();
            this.cbWMCFourStarSpecial = new System.Windows.Forms.CheckBox();
            this.txtImportName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbAutoMapEPG = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbNoInvalidEntries = new System.Windows.Forms.CheckBox();
            this.cbCreateSameDataIfNotPresent = new System.Windows.Forms.CheckBox();
            this.cbNoDataNoFile = new System.Windows.Forms.CheckBox();
            this.cbAddSeasonEpisodeToDesc = new System.Windows.Forms.CheckBox();
            this.cbTcRelevantChannels = new System.Windows.Forms.CheckBox();
            this.cbNoLogExcluded = new System.Windows.Forms.CheckBox();
            this.cbCreateSameData = new System.Windows.Forms.CheckBox();
            this.cbRemoveExtractedData = new System.Windows.Forms.CheckBox();
            this.cbRoundTime = new System.Windows.Forms.CheckBox();
            this.cbAllowBreaks = new System.Windows.Forms.CheckBox();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.cbChannelDefinitionFile = new System.Windows.Forms.CheckBox();
            this.gpChannelDefinitionFile = new System.Windows.Forms.GroupBox();
            this.tbChannelDefinitionFileName = new System.Windows.Forms.TextBox();
            this.label116 = new System.Windows.Forms.Label();
            this.btBrowseChannelDefinitionFile = new System.Windows.Forms.Button();
            this.cbSatelliteDefFiles = new System.Windows.Forms.CheckBox();
            this.gpSatelliteDefFiles = new System.Windows.Forms.GroupBox();
            this.tbSatelliteDefDirectoryName = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.btBrowseSatelliteDefDirectory = new System.Windows.Forms.Button();
            this.cbSageTVFile = new System.Windows.Forms.CheckBox();
            this.gpSageTVFile = new System.Windows.Forms.GroupBox();
            this.tbSageTVSatelliteNumber = new System.Windows.Forms.TextBox();
            this.label139 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.cbSageTVFileNoEPG = new System.Windows.Forms.CheckBox();
            this.tbSageTVFileName = new System.Windows.Forms.TextBox();
            this.label137 = new System.Windows.Forms.Label();
            this.btBrowseSageTVFile = new System.Windows.Forms.Button();
            this.cbBladeRunnerFile = new System.Windows.Forms.CheckBox();
            this.gpBladeRunnerFile = new System.Windows.Forms.GroupBox();
            this.tbBladeRunnerFileName = new System.Windows.Forms.TextBox();
            this.label136 = new System.Windows.Forms.Label();
            this.btBrowseBladeRunnerFile = new System.Windows.Forms.Button();
            this.cbAreaRegionFile = new System.Windows.Forms.CheckBox();
            this.gpAreaRegionFile = new System.Windows.Forms.GroupBox();
            this.tbAreaRegionFileName = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.btBrowseAreaRegionFile = new System.Windows.Forms.Button();
            this.tabServices = new System.Windows.Forms.TabPage();
            this.cbChannelTuningErrors = new System.Windows.Forms.CheckBox();
            this.pbarChannels = new System.Windows.Forms.ProgressBar();
            this.lbScanningFrequencies = new System.Windows.Forms.ListBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lblScanning = new System.Windows.Forms.Label();
            this.cmdClearScan = new System.Windows.Forms.Button();
            this.cmdSelectNone = new System.Windows.Forms.Button();
            this.cmdSelectAll = new System.Windows.Forms.Button();
            this.cmdScan = new System.Windows.Forms.Button();
            this.dgServices = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originalNetworkIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transportStreamIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excludedByUserColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.logicalChannelNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvStationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbpFilters = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.tbExcludedMaxChannel = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.cboFilterFrequency = new System.Windows.Forms.ComboBox();
            this.label80 = new System.Windows.Forms.Label();
            this.tbExcludeSIDEnd = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.btExcludeDelete = new System.Windows.Forms.Button();
            this.lvExcludedIdentifiers = new System.Windows.Forms.ListView();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btExcludeAdd = new System.Windows.Forms.Button();
            this.tbExcludeSIDStart = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.tbExcludeTSID = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.tbExcludeONID = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.tbpOffsets = new System.Windows.Forms.TabPage();
            this.cbTimeshiftTuningErrors = new System.Windows.Forms.CheckBox();
            this.lvPlusSelectedChannels = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btPlusDelete = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label70 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.nudPlusIncrement = new System.Windows.Forms.NumericUpDown();
            this.label73 = new System.Windows.Forms.Label();
            this.btPlusAdd = new System.Windows.Forms.Button();
            this.lbPlusDestinationChannel = new System.Windows.Forms.ListBox();
            this.label72 = new System.Windows.Forms.Label();
            this.lbPlusSourceChannel = new System.Windows.Forms.ListBox();
            this.label71 = new System.Windows.Forms.Label();
            this.pbarPlusScan = new System.Windows.Forms.ProgressBar();
            this.lblPlusScanning = new System.Windows.Forms.Label();
            this.btPlusScan = new System.Windows.Forms.Button();
            this.tabRepeats = new System.Windows.Forms.TabPage();
            this.gpRepeatExclusions = new System.Windows.Forms.GroupBox();
            this.tbPhrasesToIgnore = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.btRepeatDelete = new System.Windows.Forms.Button();
            this.lvRepeatPrograms = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btRepeatAdd = new System.Windows.Forms.Button();
            this.tbRepeatDescription = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.tbRepeatTitle = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbIgnoreWMCRecordings = new System.Windows.Forms.CheckBox();
            this.cbNoSimulcastRepeats = new System.Windows.Forms.CheckBox();
            this.cbCheckForRepeats = new System.Windows.Forms.CheckBox();
            this.tabEdit = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btEditDelete = new System.Windows.Forms.Button();
            this.lvEditSpecs = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cboEditReplaceMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbEditReplacementText = new System.Windows.Forms.TextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.cboEditLocation = new System.Windows.Forms.ComboBox();
            this.label127 = new System.Windows.Forms.Label();
            this.btEditAdd = new System.Windows.Forms.Button();
            this.cboEditApplyTo = new System.Windows.Forms.ComboBox();
            this.label128 = new System.Windows.Forms.Label();
            this.tbEditText = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.tabLookups = new System.Windows.Forms.TabPage();
            this.cbTVLookupEnabled = new System.Windows.Forms.CheckBox();
            this.cbMovieLookupEnabled = new System.Windows.Forms.CheckBox();
            this.gpLookupMisc = new System.Windows.Forms.GroupBox();
            this.label157 = new System.Windows.Forms.Label();
            this.cbLookupOverwrite = new System.Windows.Forms.CheckBox();
            this.nudLookupMatchThreshold = new System.Windows.Forms.NumericUpDown();
            this.label62 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.cbLookupImageNameTitle = new System.Windows.Forms.CheckBox();
            this.label44 = new System.Windows.Forms.Label();
            this.cbLookupImagesInBase = new System.Windows.Forms.CheckBox();
            this.udIgnorePhraseSeparator = new System.Windows.Forms.DomainUpDown();
            this.label110 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.btLookupBaseBrowse = new System.Windows.Forms.Button();
            this.tbLookupImagePath = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.cbLookupIgnoreCategories = new System.Windows.Forms.CheckBox();
            this.cbLookupReload = new System.Windows.Forms.CheckBox();
            this.cbxLookupMatching = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.tbLookupIgnoredPhrases = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.nudLookupErrors = new System.Windows.Forms.NumericUpDown();
            this.cbLookupNotFound = new System.Windows.Forms.CheckBox();
            this.label86 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.nudLookupTime = new System.Windows.Forms.NumericUpDown();
            this.gpTVLookup = new System.Windows.Forms.GroupBox();
            this.cboTVProvider = new System.Windows.Forms.ComboBox();
            this.label134 = new System.Windows.Forms.Label();
            this.tbTVDBPin = new System.Windows.Forms.TextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.cboLookupsEpisodeSearchPriority = new System.Windows.Forms.ComboBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.cboxTVLookupImageType = new System.Windows.Forms.ComboBox();
            this.label89 = new System.Windows.Forms.Label();
            this.cbLookupProcessAsTVSeries = new System.Windows.Forms.CheckBox();
            this.gpMovieLookup = new System.Windows.Forms.GroupBox();
            this.btLookupChangeNotMovie = new System.Windows.Forms.Button();
            this.cboLookupNotMovie = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.udMoviePhraseSeparator = new System.Windows.Forms.DomainUpDown();
            this.label109 = new System.Windows.Forms.Label();
            this.nudLookupMovieHighDuration = new System.Windows.Forms.NumericUpDown();
            this.label93 = new System.Windows.Forms.Label();
            this.tbLookupMoviePhrases = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.cboxMovieLookupImageType = new System.Windows.Forms.ComboBox();
            this.label88 = new System.Windows.Forms.Label();
            this.nudLookupMovieLowDuration = new System.Windows.Forms.NumericUpDown();
            this.label87 = new System.Windows.Forms.Label();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.cbDVBLinkUpdateEnabled = new System.Windows.Forms.CheckBox();
            this.gpDVBLink = new System.Windows.Forms.GroupBox();
            this.cbAutoExcludeNew = new System.Windows.Forms.CheckBox();
            this.label149 = new System.Windows.Forms.Label();
            this.cbUpdateChannelNumbers = new System.Windows.Forms.CheckBox();
            this.label124 = new System.Windows.Forms.Label();
            this.cbReloadChannelData = new System.Windows.Forms.CheckBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.nudEPGScanInterval = new System.Windows.Forms.NumericUpDown();
            this.label122 = new System.Windows.Forms.Label();
            this.cbLogNetworkMap = new System.Windows.Forms.CheckBox();
            this.label121 = new System.Windows.Forms.Label();
            this.cbChildLock = new System.Windows.Forms.CheckBox();
            this.cboEPGScanner = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.cboMergeMethod = new System.Windows.Forms.ComboBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.tabTranslations = new System.Windows.Forms.TabPage();
            this.cbTranslateEnabled = new System.Windows.Forms.CheckBox();
            this.gpTranslate = new System.Windows.Forms.GroupBox();
            this.lblTranslateScanning = new System.Windows.Forms.Label();
            this.pbarTranslateScanning = new System.Windows.Forms.ProgressBar();
            this.btTranslateRefresh = new System.Windows.Forms.Button();
            this.label131 = new System.Windows.Forms.Label();
            this.btTranslateScan = new System.Windows.Forms.Button();
            this.tbTranslateApiKey = new System.Windows.Forms.TextBox();
            this.lbTranslateChannels = new System.Windows.Forms.ListBox();
            this.label155 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.cboTranslateOutputLanguage = new System.Windows.Forms.ComboBox();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.cbManualTime = new System.Windows.Forms.CheckBox();
            this.gpTimeAdjustments = new System.Windows.Forms.GroupBox();
            this.nudCurrentOffsetMinutes = new System.Windows.Forms.NumericUpDown();
            this.nudNextOffsetMinutes = new System.Windows.Forms.NumericUpDown();
            this.label49 = new System.Windows.Forms.Label();
            this.nudChangeMinutes = new System.Windows.Forms.NumericUpDown();
            this.label48 = new System.Windows.Forms.Label();
            this.nudChangeHours = new System.Windows.Forms.NumericUpDown();
            this.label47 = new System.Windows.Forms.Label();
            this.tbChangeDate = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.nudNextOffsetHours = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.nudCurrentOffsetHours = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbFromService = new System.Windows.Forms.CheckBox();
            this.cbUseStoredStationInfo = new System.Windows.Forms.CheckBox();
            this.cbStoreStationInfo = new System.Windows.Forms.CheckBox();
            this.gpTimeouts = new System.Windows.Forms.GroupBox();
            this.nudBufferSize = new System.Windows.Forms.NumericUpDown();
            this.label103 = new System.Windows.Forms.Label();
            this.nudBufferFills = new System.Windows.Forms.NumericUpDown();
            this.label102 = new System.Windows.Forms.Label();
            this.btTimeoutDefaults = new System.Windows.Forms.Button();
            this.nudDataCollectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabDiagnostics = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbTraceIDs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDebugIDs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.logosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboFrequencies = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdNewOutputFile = new System.Windows.Forms.Button();
            this.cboCollectionType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOptions = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.rbSatellite = new System.Windows.Forms.RadioButton();
            this.rbDVBT = new System.Windows.Forms.RadioButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.gbSatellite = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmdOpenINIFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.cmdSaveGeneral = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudScanRetries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).BeginInit();
            this.tbcParameters.SuspendLayout();
            this.tabDVBS.SuspendLayout();
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
            this.panel1.SuspendLayout();
            this.tabXMLTV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXmltvChannelChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmltvChannelChangeBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.gpbFiles.SuspendLayout();
            this.tabSchedulesDirect.SuspendLayout();
            this.gpSchedulesDirect.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSdChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdBindingSource)).BeginInit();
            this.tabGeneral.SuspendLayout();
            this.gpXmltvOptions.SuspendLayout();
            this.gpDVBViewerOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.gpWMCOptions.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.gpChannelDefinitionFile.SuspendLayout();
            this.gpSatelliteDefFiles.SuspendLayout();
            this.gpSageTVFile.SuspendLayout();
            this.gpBladeRunnerFile.SuspendLayout();
            this.gpAreaRegionFile.SuspendLayout();
            this.tabServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvStationBindingSource)).BeginInit();
            this.tbpFilters.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tbpOffsets.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlusIncrement)).BeginInit();
            this.tabRepeats.SuspendLayout();
            this.gpRepeatExclusions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabEdit.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabLookups.SuspendLayout();
            this.gpLookupMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMatchThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupTime)).BeginInit();
            this.gpTVLookup.SuspendLayout();
            this.gpMovieLookup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMovieHighDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMovieLowDuration)).BeginInit();
            this.tabUpdate.SuspendLayout();
            this.gpDVBLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEPGScanInterval)).BeginInit();
            this.tabTranslations.SuspendLayout();
            this.gpTranslate.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.gpTimeAdjustments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentOffsetMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNextOffsetMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNextOffsetHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentOffsetHours)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.gpTimeouts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferFills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).BeginInit();
            this.tabDiagnostics.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logosBindingSource)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.gbSatellite.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudScanRetries
            // 
            this.nudScanRetries.Location = new System.Drawing.Point(177, 94);
            this.nudScanRetries.Name = "nudScanRetries";
            this.nudScanRetries.Size = new System.Drawing.Size(48, 20);
            this.nudScanRetries.TabIndex = 410;
            // 
            // nudSignalLockTimeout
            // 
            this.nudSignalLockTimeout.Location = new System.Drawing.Point(177, 26);
            this.nudSignalLockTimeout.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSignalLockTimeout.Name = "nudSignalLockTimeout";
            this.nudSignalLockTimeout.Size = new System.Drawing.Size(48, 20);
            this.nudSignalLockTimeout.TabIndex = 406;
            // 
            // tbcParameters
            // 
            this.tbcParameters.Controls.Add(this.tabDVBS);
            this.tbcParameters.Controls.Add(this.tabXMLTV);
            this.tbcParameters.Controls.Add(this.tabSchedulesDirect);
            this.tbcParameters.Controls.Add(this.tabGeneral);
            this.tbcParameters.Controls.Add(this.tabFiles);
            this.tbcParameters.Controls.Add(this.tabServices);
            this.tbcParameters.Controls.Add(this.tbpFilters);
            this.tbcParameters.Controls.Add(this.tbpOffsets);
            this.tbcParameters.Controls.Add(this.tabRepeats);
            this.tbcParameters.Controls.Add(this.tabEdit);
            this.tbcParameters.Controls.Add(this.tabLookups);
            this.tbcParameters.Controls.Add(this.tabUpdate);
            this.tbcParameters.Controls.Add(this.tabTranslations);
            this.tbcParameters.Controls.Add(this.tabAdvanced);
            this.tbcParameters.Controls.Add(this.tabDiagnostics);
            this.tbcParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcParameters.Location = new System.Drawing.Point(0, 0);
            this.tbcParameters.Name = "tbcParameters";
            this.tbcParameters.SelectedIndex = 0;
            this.tbcParameters.Size = new System.Drawing.Size(950, 672);
            this.tbcParameters.TabIndex = 23;
            this.tbcParameters.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcParametersDeselecting);
            // 
            // tabDVBS
            // 
            this.tabDVBS.Controls.Add(this.tbcDeliverySystem);
            this.tabDVBS.Controls.Add(this.panel1);
            this.tabDVBS.Location = new System.Drawing.Point(4, 22);
            this.tabDVBS.Name = "tabDVBS";
            this.tabDVBS.Size = new System.Drawing.Size(942, 646);
            this.tabDVBS.TabIndex = 2;
            this.tabDVBS.Text = "Tuning";
            this.tabDVBS.UseVisualStyleBackColor = true;
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
            this.tbcDeliverySystem.Location = new System.Drawing.Point(12, 13);
            this.tbcDeliverySystem.Multiline = true;
            this.tbcDeliverySystem.Name = "tbcDeliverySystem";
            this.tbcDeliverySystem.SelectedIndex = 0;
            this.tbcDeliverySystem.Size = new System.Drawing.Size(917, 442);
            this.tbcDeliverySystem.TabIndex = 201;
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
            this.tbpSatellite.Controls.Add(this.btAddSatellite);
            this.tbpSatellite.Controls.Add(this.txtLNBSwitch);
            this.tbpSatellite.Controls.Add(this.cboDVBSCollectionType);
            this.tbpSatellite.Controls.Add(this.label28);
            this.tbpSatellite.Controls.Add(this.txtLNBHigh);
            this.tbpSatellite.Controls.Add(this.cboDVBSScanningFrequency);
            this.tbpSatellite.Controls.Add(this.label29);
            this.tbpSatellite.Controls.Add(this.txtLNBLow);
            this.tbpSatellite.Controls.Add(this.cboSatellite);
            this.tbpSatellite.Controls.Add(this.label4);
            this.tbpSatellite.Controls.Add(this.label31);
            this.tbpSatellite.Controls.Add(this.label33);
            this.tbpSatellite.Controls.Add(this.label32);
            this.tbpSatellite.Location = new System.Drawing.Point(4, 22);
            this.tbpSatellite.Name = "tbpSatellite";
            this.tbpSatellite.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSatellite.Size = new System.Drawing.Size(909, 416);
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
            this.groupBox14.Location = new System.Drawing.Point(19, 278);
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
            this.cboDiseqc.Location = new System.Drawing.Point(108, 23);
            this.cboDiseqc.MaxDropDownItems = 20;
            this.cboDiseqc.Name = "cboDiseqc";
            this.cboDiseqc.Size = new System.Drawing.Size(157, 21);
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
            this.udDvbsSatIpFrontend.Items.Add("Not used");
            this.udDvbsSatIpFrontend.Items.Add("1");
            this.udDvbsSatIpFrontend.Items.Add("2");
            this.udDvbsSatIpFrontend.Items.Add("3");
            this.udDvbsSatIpFrontend.Items.Add("4");
            this.udDvbsSatIpFrontend.Location = new System.Drawing.Point(130, 106);
            this.udDvbsSatIpFrontend.Name = "udDvbsSatIpFrontend";
            this.udDvbsSatIpFrontend.ReadOnly = true;
            this.udDvbsSatIpFrontend.Size = new System.Drawing.Size(150, 20);
            this.udDvbsSatIpFrontend.TabIndex = 209;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbSatelliteTuners);
            this.groupBox3.Location = new System.Drawing.Point(19, 143);
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
            this.clbSatelliteTuners.SelectedIndexChanged += new System.EventHandler(this.clbTuners_SelectedIndexChanged);
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(31, 108);
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
            // btAddSatellite
            // 
            this.btAddSatellite.Enabled = false;
            this.btAddSatellite.Location = new System.Drawing.Point(19, 382);
            this.btAddSatellite.Name = "btAddSatellite";
            this.btAddSatellite.Size = new System.Drawing.Size(75, 23);
            this.btAddSatellite.TabIndex = 320;
            this.btAddSatellite.Text = "Update";
            this.btAddSatellite.UseVisualStyleBackColor = true;
            this.btAddSatellite.Click += new System.EventHandler(this.btAddSatellite_Click);
            // 
            // txtLNBSwitch
            // 
            this.txtLNBSwitch.Location = new System.Drawing.Point(566, 75);
            this.txtLNBSwitch.Name = "txtLNBSwitch";
            this.txtLNBSwitch.Size = new System.Drawing.Size(92, 20);
            this.txtLNBSwitch.TabIndex = 216;
            this.txtLNBSwitch.TextChanged += new System.EventHandler(this.txtLNBSwitch_TextChanged);
            this.txtLNBSwitch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLNBNumeric);
            // 
            // cboDVBSCollectionType
            // 
            this.cboDVBSCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDVBSCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDVBSCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDVBSCollectionType.FormattingEnabled = true;
            this.cboDVBSCollectionType.Location = new System.Drawing.Point(130, 75);
            this.cboDVBSCollectionType.Name = "cboDVBSCollectionType";
            this.cboDVBSCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboDVBSCollectionType.TabIndex = 207;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(31, 78);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 13);
            this.label28.TabIndex = 206;
            this.label28.Text = "Collection Type";
            // 
            // txtLNBHigh
            // 
            this.txtLNBHigh.Location = new System.Drawing.Point(566, 45);
            this.txtLNBHigh.Name = "txtLNBHigh";
            this.txtLNBHigh.Size = new System.Drawing.Size(92, 20);
            this.txtLNBHigh.TabIndex = 214;
            this.txtLNBHigh.TextChanged += new System.EventHandler(this.txtLNBHigh_TextChanged);
            this.txtLNBHigh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLNBNumeric);
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
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(31, 48);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(57, 13);
            this.label29.TabIndex = 204;
            this.label29.Text = "Frequency";
            // 
            // txtLNBLow
            // 
            this.txtLNBLow.Location = new System.Drawing.Point(566, 15);
            this.txtLNBLow.Name = "txtLNBLow";
            this.txtLNBLow.Size = new System.Drawing.Size(92, 20);
            this.txtLNBLow.TabIndex = 211;
            this.txtLNBLow.TextChanged += new System.EventHandler(this.txtLNBLow_TextChanged);
            this.txtLNBLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLNBNumeric);
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
            this.tbpTerrestrial.Controls.Add(this.btAddTerrestrial);
            this.tbpTerrestrial.Controls.Add(this.lblArea);
            this.tbpTerrestrial.Controls.Add(this.cboArea);
            this.tbpTerrestrial.Controls.Add(this.cboDVBTCollectionType);
            this.tbpTerrestrial.Controls.Add(this.lblCountry);
            this.tbpTerrestrial.Controls.Add(this.label23);
            this.tbpTerrestrial.Controls.Add(this.cboCountry);
            this.tbpTerrestrial.Controls.Add(this.cboDVBTScanningFrequency);
            this.tbpTerrestrial.Controls.Add(this.label24);
            this.tbpTerrestrial.Location = new System.Drawing.Point(4, 22);
            this.tbpTerrestrial.Name = "tbpTerrestrial";
            this.tbpTerrestrial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTerrestrial.Size = new System.Drawing.Size(909, 416);
            this.tbpTerrestrial.TabIndex = 1;
            this.tbpTerrestrial.Text = "DVB Terrestrial";
            this.tbpTerrestrial.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.clbTerrestrialTuners);
            this.groupBox12.Location = new System.Drawing.Point(17, 178);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(876, 188);
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
            this.clbTerrestrialTuners.Size = new System.Drawing.Size(846, 154);
            this.clbTerrestrialTuners.TabIndex = 101;
            this.clbTerrestrialTuners.SelectedIndexChanged += new System.EventHandler(this.clbTerrestrialTuners_SelectedIndexChanged);
            // 
            // udDvbtSatIpFrontend
            // 
            this.udDvbtSatIpFrontend.Items.Add("Not used");
            this.udDvbtSatIpFrontend.Items.Add("1");
            this.udDvbtSatIpFrontend.Items.Add("2");
            this.udDvbtSatIpFrontend.Items.Add("3");
            this.udDvbtSatIpFrontend.Items.Add("4");
            this.udDvbtSatIpFrontend.Location = new System.Drawing.Point(154, 147);
            this.udDvbtSatIpFrontend.Name = "udDvbtSatIpFrontend";
            this.udDvbtSatIpFrontend.ReadOnly = true;
            this.udDvbtSatIpFrontend.Size = new System.Drawing.Size(150, 20);
            this.udDvbtSatIpFrontend.TabIndex = 226;
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(24, 149);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(84, 13);
            this.label152.TabIndex = 225;
            this.label152.Text = "Sat>IP Frontend";
            // 
            // btAddTerrestrial
            // 
            this.btAddTerrestrial.Location = new System.Drawing.Point(19, 378);
            this.btAddTerrestrial.Name = "btAddTerrestrial";
            this.btAddTerrestrial.Size = new System.Drawing.Size(75, 23);
            this.btAddTerrestrial.TabIndex = 224;
            this.btAddTerrestrial.Text = "Update";
            this.btAddTerrestrial.UseVisualStyleBackColor = true;
            this.btAddTerrestrial.Click += new System.EventHandler(this.btAddTerrestrial_Click);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(24, 56);
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
            // cboDVBTCollectionType
            // 
            this.cboDVBTCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDVBTCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDVBTCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDVBTCollectionType.FormattingEnabled = true;
            this.cboDVBTCollectionType.Location = new System.Drawing.Point(154, 115);
            this.cboDVBTCollectionType.Name = "cboDVBTCollectionType";
            this.cboDVBTCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboDVBTCollectionType.TabIndex = 223;
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
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(24, 118);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 13);
            this.label23.TabIndex = 135;
            this.label23.Text = "Collection Type";
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
            this.cboDVBTScanningFrequency.Location = new System.Drawing.Point(154, 84);
            this.cboDVBTScanningFrequency.MaxDropDownItems = 20;
            this.cboDVBTScanningFrequency.Name = "cboDVBTScanningFrequency";
            this.cboDVBTScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboDVBTScanningFrequency.TabIndex = 222;
            this.cboDVBTScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboDVBTScanningFrequency_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(24, 87);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(57, 13);
            this.label24.TabIndex = 133;
            this.label24.Text = "Frequency";
            // 
            // tbpCable
            // 
            this.tbpCable.Controls.Add(this.groupBox13);
            this.tbpCable.Controls.Add(this.udDvbcSatIpFrontend);
            this.tbpCable.Controls.Add(this.label153);
            this.tbpCable.Controls.Add(this.btAddCable);
            this.tbpCable.Controls.Add(this.label54);
            this.tbpCable.Controls.Add(this.cboCable);
            this.tbpCable.Controls.Add(this.cboCableCollectionType);
            this.tbpCable.Controls.Add(this.label52);
            this.tbpCable.Controls.Add(this.label51);
            this.tbpCable.Controls.Add(this.cboCableScanningFrequency);
            this.tbpCable.Location = new System.Drawing.Point(4, 22);
            this.tbpCable.Name = "tbpCable";
            this.tbpCable.Size = new System.Drawing.Size(909, 416);
            this.tbpCable.TabIndex = 2;
            this.tbpCable.Text = "DVB Cable";
            this.tbpCable.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.clbCableTuners);
            this.groupBox13.Location = new System.Drawing.Point(22, 148);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(872, 218);
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
            this.clbCableTuners.Size = new System.Drawing.Size(839, 184);
            this.clbCableTuners.TabIndex = 101;
            this.clbCableTuners.SelectedIndexChanged += new System.EventHandler(this.clbCableTuners_SelectedIndexChanged);
            // 
            // udDvbcSatIpFrontend
            // 
            this.udDvbcSatIpFrontend.Items.Add("Not used");
            this.udDvbcSatIpFrontend.Items.Add("1");
            this.udDvbcSatIpFrontend.Items.Add("2");
            this.udDvbcSatIpFrontend.Items.Add("3");
            this.udDvbcSatIpFrontend.Items.Add("4");
            this.udDvbcSatIpFrontend.Location = new System.Drawing.Point(139, 121);
            this.udDvbcSatIpFrontend.Name = "udDvbcSatIpFrontend";
            this.udDvbcSatIpFrontend.ReadOnly = true;
            this.udDvbcSatIpFrontend.Size = new System.Drawing.Size(150, 20);
            this.udDvbcSatIpFrontend.TabIndex = 236;
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(29, 123);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(84, 13);
            this.label153.TabIndex = 235;
            this.label153.Text = "Sat>IP Frontend";
            // 
            // btAddCable
            // 
            this.btAddCable.Location = new System.Drawing.Point(19, 378);
            this.btAddCable.Name = "btAddCable";
            this.btAddCable.Size = new System.Drawing.Size(75, 23);
            this.btAddCable.TabIndex = 234;
            this.btAddCable.Text = "Update";
            this.btAddCable.UseVisualStyleBackColor = true;
            this.btAddCable.Click += new System.EventHandler(this.btAddCable_Click);
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
            // cboCableCollectionType
            // 
            this.cboCableCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCableCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCableCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCableCollectionType.FormattingEnabled = true;
            this.cboCableCollectionType.Location = new System.Drawing.Point(139, 86);
            this.cboCableCollectionType.Name = "cboCableCollectionType";
            this.cboCableCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboCableCollectionType.TabIndex = 233;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(29, 55);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(57, 13);
            this.label52.TabIndex = 139;
            this.label52.Text = "Frequency";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(29, 89);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(80, 13);
            this.label51.TabIndex = 141;
            this.label51.Text = "Collection Type";
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
            this.cboCableScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboCableScanningFrequency_SelectedIndexChanged);
            // 
            // tbpAtsc
            // 
            this.tbpAtsc.Controls.Add(this.groupBox18);
            this.tbpAtsc.Controls.Add(this.btAddAtsc);
            this.tbpAtsc.Controls.Add(this.label50);
            this.tbpAtsc.Controls.Add(this.cboAtscProvider);
            this.tbpAtsc.Controls.Add(this.cboAtscCollectionType);
            this.tbpAtsc.Controls.Add(this.label53);
            this.tbpAtsc.Controls.Add(this.label55);
            this.tbpAtsc.Controls.Add(this.cboAtscScanningFrequency);
            this.tbpAtsc.Location = new System.Drawing.Point(4, 22);
            this.tbpAtsc.Name = "tbpAtsc";
            this.tbpAtsc.Size = new System.Drawing.Size(909, 416);
            this.tbpAtsc.TabIndex = 3;
            this.tbpAtsc.Text = "ATSC";
            this.tbpAtsc.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.clbAtscTuners);
            this.groupBox18.Location = new System.Drawing.Point(22, 127);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(873, 239);
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
            this.clbAtscTuners.Size = new System.Drawing.Size(839, 199);
            this.clbAtscTuners.TabIndex = 101;
            this.clbAtscTuners.SelectedIndexChanged += new System.EventHandler(this.clbAtscTuners_SelectedIndexChanged);
            // 
            // btAddAtsc
            // 
            this.btAddAtsc.Location = new System.Drawing.Point(19, 378);
            this.btAddAtsc.Name = "btAddAtsc";
            this.btAddAtsc.Size = new System.Drawing.Size(75, 23);
            this.btAddAtsc.TabIndex = 244;
            this.btAddAtsc.Text = "Update";
            this.btAddAtsc.UseVisualStyleBackColor = true;
            this.btAddAtsc.Click += new System.EventHandler(this.btAddAtsc_Click);
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
            // cboAtscCollectionType
            // 
            this.cboAtscCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAtscCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAtscCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAtscCollectionType.FormattingEnabled = true;
            this.cboAtscCollectionType.Location = new System.Drawing.Point(140, 93);
            this.cboAtscCollectionType.Name = "cboAtscCollectionType";
            this.cboAtscCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboAtscCollectionType.TabIndex = 243;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(30, 63);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(46, 13);
            this.label53.TabIndex = 139;
            this.label53.Text = "Channel";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(30, 96);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(80, 13);
            this.label55.TabIndex = 141;
            this.label55.Text = "Collection Type";
            // 
            // cboAtscScanningFrequency
            // 
            this.cboAtscScanningFrequency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAtscScanningFrequency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAtscScanningFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAtscScanningFrequency.FormattingEnabled = true;
            this.cboAtscScanningFrequency.Location = new System.Drawing.Point(140, 60);
            this.cboAtscScanningFrequency.MaxDropDownItems = 20;
            this.cboAtscScanningFrequency.Name = "cboAtscScanningFrequency";
            this.cboAtscScanningFrequency.Size = new System.Drawing.Size(150, 21);
            this.cboAtscScanningFrequency.TabIndex = 242;
            this.cboAtscScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboAtscScanningFrequency_SelectedIndexChanged);
            // 
            // tbpClearQAM
            // 
            this.tbpClearQAM.Controls.Add(this.groupBox19);
            this.tbpClearQAM.Controls.Add(this.btAddClearQam);
            this.tbpClearQAM.Controls.Add(this.label57);
            this.tbpClearQAM.Controls.Add(this.cboClearQamProvider);
            this.tbpClearQAM.Controls.Add(this.cboClearQamCollectionType);
            this.tbpClearQAM.Controls.Add(this.label58);
            this.tbpClearQAM.Controls.Add(this.label59);
            this.tbpClearQAM.Controls.Add(this.cboClearQamScanningFrequency);
            this.tbpClearQAM.Location = new System.Drawing.Point(4, 22);
            this.tbpClearQAM.Name = "tbpClearQAM";
            this.tbpClearQAM.Size = new System.Drawing.Size(909, 416);
            this.tbpClearQAM.TabIndex = 4;
            this.tbpClearQAM.Text = "Clear QAM";
            this.tbpClearQAM.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.clbClearQamTuners);
            this.groupBox19.Location = new System.Drawing.Point(22, 132);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(871, 234);
            this.groupBox19.TabIndex = 255;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Tuners";
            // 
            // clbClearQamTuners
            // 
            this.clbClearQamTuners.CheckOnClick = true;
            this.clbClearQamTuners.FormattingEnabled = true;
            this.clbClearQamTuners.Location = new System.Drawing.Point(13, 21);
            this.clbClearQamTuners.Name = "clbClearQamTuners";
            this.clbClearQamTuners.Size = new System.Drawing.Size(839, 199);
            this.clbClearQamTuners.TabIndex = 101;
            this.clbClearQamTuners.SelectedIndexChanged += new System.EventHandler(this.clbClearQamTuners_SelectedIndexChanged);
            // 
            // btAddClearQam
            // 
            this.btAddClearQam.Location = new System.Drawing.Point(19, 377);
            this.btAddClearQam.Name = "btAddClearQam";
            this.btAddClearQam.Size = new System.Drawing.Size(75, 23);
            this.btAddClearQam.TabIndex = 254;
            this.btAddClearQam.Text = "Update";
            this.btAddClearQam.UseVisualStyleBackColor = true;
            this.btAddClearQam.Click += new System.EventHandler(this.btAddClearQam_Click);
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
            // cboClearQamCollectionType
            // 
            this.cboClearQamCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboClearQamCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClearQamCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClearQamCollectionType.FormattingEnabled = true;
            this.cboClearQamCollectionType.Location = new System.Drawing.Point(139, 95);
            this.cboClearQamCollectionType.Name = "cboClearQamCollectionType";
            this.cboClearQamCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboClearQamCollectionType.TabIndex = 253;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(29, 64);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(46, 13);
            this.label58.TabIndex = 145;
            this.label58.Text = "Channel";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(29, 98);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(80, 13);
            this.label59.TabIndex = 147;
            this.label59.Text = "Collection Type";
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
            this.cboClearQamScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboClearQamScanningFrequency_SelectedIndexChanged);
            // 
            // tbpISDBSatellite
            // 
            this.tbpISDBSatellite.Controls.Add(this.groupBox20);
            this.tbpISDBSatellite.Controls.Add(this.btISDBLNBDefaults);
            this.tbpISDBSatellite.Controls.Add(this.btAddISDBSatellite);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBSwitch);
            this.tbpISDBSatellite.Controls.Add(this.cboISDBSCollectionType);
            this.tbpISDBSatellite.Controls.Add(this.label60);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBHigh);
            this.tbpISDBSatellite.Controls.Add(this.cboISDBSScanningFrequency);
            this.tbpISDBSatellite.Controls.Add(this.label61);
            this.tbpISDBSatellite.Controls.Add(this.txtISDBLNBLow);
            this.tbpISDBSatellite.Controls.Add(this.cboISDBSatellite);
            this.tbpISDBSatellite.Controls.Add(this.label63);
            this.tbpISDBSatellite.Controls.Add(this.label64);
            this.tbpISDBSatellite.Controls.Add(this.label65);
            this.tbpISDBSatellite.Controls.Add(this.label66);
            this.tbpISDBSatellite.Location = new System.Drawing.Point(4, 22);
            this.tbpISDBSatellite.Name = "tbpISDBSatellite";
            this.tbpISDBSatellite.Size = new System.Drawing.Size(909, 416);
            this.tbpISDBSatellite.TabIndex = 5;
            this.tbpISDBSatellite.Text = "ISDB Satellite";
            this.tbpISDBSatellite.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.clbISDBSatelliteTuners);
            this.groupBox20.Location = new System.Drawing.Point(21, 115);
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
            // btAddISDBSatellite
            // 
            this.btAddISDBSatellite.Location = new System.Drawing.Point(19, 378);
            this.btAddISDBSatellite.Name = "btAddISDBSatellite";
            this.btAddISDBSatellite.Size = new System.Drawing.Size(75, 23);
            this.btAddISDBSatellite.TabIndex = 226;
            this.btAddISDBSatellite.Text = "Update";
            this.btAddISDBSatellite.UseVisualStyleBackColor = true;
            this.btAddISDBSatellite.Click += new System.EventHandler(this.btAddISDBSatellite_Click);
            // 
            // txtISDBLNBSwitch
            // 
            this.txtISDBLNBSwitch.Location = new System.Drawing.Point(565, 86);
            this.txtISDBLNBSwitch.Name = "txtISDBLNBSwitch";
            this.txtISDBLNBSwitch.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBSwitch.TabIndex = 223;
            this.txtISDBLNBSwitch.TextChanged += new System.EventHandler(this.txtISDBLNBSwitch_TextChanged);
            this.txtISDBLNBSwitch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtISDBLNBNumeric);
            // 
            // cboISDBSCollectionType
            // 
            this.cboISDBSCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBSCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBSCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBSCollectionType.FormattingEnabled = true;
            this.cboISDBSCollectionType.Location = new System.Drawing.Point(153, 86);
            this.cboISDBSCollectionType.Name = "cboISDBSCollectionType";
            this.cboISDBSCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboISDBSCollectionType.TabIndex = 220;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(28, 89);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(80, 13);
            this.label60.TabIndex = 217;
            this.label60.Text = "Collection Type";
            // 
            // txtISDBLNBHigh
            // 
            this.txtISDBLNBHigh.Location = new System.Drawing.Point(565, 54);
            this.txtISDBLNBHigh.Name = "txtISDBLNBHigh";
            this.txtISDBLNBHigh.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBHigh.TabIndex = 222;
            this.txtISDBLNBHigh.TextChanged += new System.EventHandler(this.txtISDBLNBHigh_TextChanged);
            this.txtISDBLNBHigh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtISDBLNBNumeric);
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
            this.cboISDBSScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboISDBSScanningFrequency_SelectedIndexChanged);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(28, 57);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(57, 13);
            this.label61.TabIndex = 216;
            this.label61.Text = "Frequency";
            // 
            // txtISDBLNBLow
            // 
            this.txtISDBLNBLow.Location = new System.Drawing.Point(565, 22);
            this.txtISDBLNBLow.Name = "txtISDBLNBLow";
            this.txtISDBLNBLow.Size = new System.Drawing.Size(92, 20);
            this.txtISDBLNBLow.TabIndex = 221;
            this.txtISDBLNBLow.TextChanged += new System.EventHandler(this.txtISDBLNBLow_TextChanged);
            this.txtISDBLNBLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtISDBLNBNumeric);
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
            this.tbpISDBTerrestrial.Controls.Add(this.btAddISDBTerrestrial);
            this.tbpISDBTerrestrial.Controls.Add(this.label67);
            this.tbpISDBTerrestrial.Controls.Add(this.cboISDBTProvider);
            this.tbpISDBTerrestrial.Controls.Add(this.cboISDBTCollectionType);
            this.tbpISDBTerrestrial.Controls.Add(this.label68);
            this.tbpISDBTerrestrial.Controls.Add(this.label69);
            this.tbpISDBTerrestrial.Controls.Add(this.cboISDBTScanningFrequency);
            this.tbpISDBTerrestrial.Location = new System.Drawing.Point(4, 22);
            this.tbpISDBTerrestrial.Name = "tbpISDBTerrestrial";
            this.tbpISDBTerrestrial.Size = new System.Drawing.Size(909, 416);
            this.tbpISDBTerrestrial.TabIndex = 6;
            this.tbpISDBTerrestrial.Text = "ISDB Terrestrial";
            this.tbpISDBTerrestrial.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.clbISDBTerrestrialTuners);
            this.groupBox21.Location = new System.Drawing.Point(22, 129);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(870, 237);
            this.groupBox21.TabIndex = 252;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Tuners";
            // 
            // clbISDBTerrestrialTuners
            // 
            this.clbISDBTerrestrialTuners.CheckOnClick = true;
            this.clbISDBTerrestrialTuners.FormattingEnabled = true;
            this.clbISDBTerrestrialTuners.Location = new System.Drawing.Point(13, 21);
            this.clbISDBTerrestrialTuners.Name = "clbISDBTerrestrialTuners";
            this.clbISDBTerrestrialTuners.Size = new System.Drawing.Size(839, 199);
            this.clbISDBTerrestrialTuners.TabIndex = 101;
            this.clbISDBTerrestrialTuners.SelectedIndexChanged += new System.EventHandler(this.clbISDBTerrestrialTuners_SelectedIndexChanged);
            // 
            // btAddISDBTerrestrial
            // 
            this.btAddISDBTerrestrial.Location = new System.Drawing.Point(19, 378);
            this.btAddISDBTerrestrial.Name = "btAddISDBTerrestrial";
            this.btAddISDBTerrestrial.Size = new System.Drawing.Size(75, 23);
            this.btAddISDBTerrestrial.TabIndex = 251;
            this.btAddISDBTerrestrial.Text = "Update";
            this.btAddISDBTerrestrial.UseVisualStyleBackColor = true;
            this.btAddISDBTerrestrial.Click += new System.EventHandler(this.btAddISDBTerrestrial_Click);
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
            // cboISDBTCollectionType
            // 
            this.cboISDBTCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboISDBTCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboISDBTCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboISDBTCollectionType.FormattingEnabled = true;
            this.cboISDBTCollectionType.Location = new System.Drawing.Point(140, 95);
            this.cboISDBTCollectionType.Name = "cboISDBTCollectionType";
            this.cboISDBTCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboISDBTCollectionType.TabIndex = 250;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(30, 64);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(46, 13);
            this.label68.TabIndex = 246;
            this.label68.Text = "Channel";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(30, 98);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(80, 13);
            this.label69.TabIndex = 247;
            this.label69.Text = "Collection Type";
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
            this.cboISDBTScanningFrequency.SelectedIndexChanged += new System.EventHandler(this.cboISDBTScanningFrequency_SelectedIndexChanged);
            // 
            // tbpFile
            // 
            this.tbpFile.Controls.Add(this.btDeliveryFileAdd);
            this.tbpFile.Controls.Add(this.cboDeliveryFileCollectionType);
            this.tbpFile.Controls.Add(this.label141);
            this.tbpFile.Controls.Add(this.tbDeliveryFileBrowse);
            this.tbpFile.Controls.Add(this.tbDeliveryFilePath);
            this.tbpFile.Controls.Add(this.label140);
            this.tbpFile.Location = new System.Drawing.Point(4, 22);
            this.tbpFile.Name = "tbpFile";
            this.tbpFile.Size = new System.Drawing.Size(909, 416);
            this.tbpFile.TabIndex = 7;
            this.tbpFile.Text = "File";
            this.tbpFile.UseVisualStyleBackColor = true;
            // 
            // btDeliveryFileAdd
            // 
            this.btDeliveryFileAdd.Enabled = false;
            this.btDeliveryFileAdd.Location = new System.Drawing.Point(19, 378);
            this.btDeliveryFileAdd.Name = "btDeliveryFileAdd";
            this.btDeliveryFileAdd.Size = new System.Drawing.Size(75, 23);
            this.btDeliveryFileAdd.TabIndex = 520;
            this.btDeliveryFileAdd.Text = "Update";
            this.btDeliveryFileAdd.UseVisualStyleBackColor = true;
            this.btDeliveryFileAdd.Click += new System.EventHandler(this.btDeliveryFileAdd_Click);
            // 
            // cboDeliveryFileCollectionType
            // 
            this.cboDeliveryFileCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDeliveryFileCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDeliveryFileCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeliveryFileCollectionType.FormattingEnabled = true;
            this.cboDeliveryFileCollectionType.Location = new System.Drawing.Point(136, 61);
            this.cboDeliveryFileCollectionType.Name = "cboDeliveryFileCollectionType";
            this.cboDeliveryFileCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboDeliveryFileCollectionType.TabIndex = 513;
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(21, 64);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(80, 13);
            this.label141.TabIndex = 512;
            this.label141.Text = "Collection Type";
            // 
            // tbDeliveryFileBrowse
            // 
            this.tbDeliveryFileBrowse.Location = new System.Drawing.Point(809, 23);
            this.tbDeliveryFileBrowse.Name = "tbDeliveryFileBrowse";
            this.tbDeliveryFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.tbDeliveryFileBrowse.TabIndex = 511;
            this.tbDeliveryFileBrowse.Text = "Browse";
            this.tbDeliveryFileBrowse.UseVisualStyleBackColor = true;
            this.tbDeliveryFileBrowse.Click += new System.EventHandler(this.tbDeliveryFileBrowse_Click);
            // 
            // tbDeliveryFilePath
            // 
            this.tbDeliveryFilePath.Location = new System.Drawing.Point(136, 25);
            this.tbDeliveryFilePath.Name = "tbDeliveryFilePath";
            this.tbDeliveryFilePath.Size = new System.Drawing.Size(656, 20);
            this.tbDeliveryFilePath.TabIndex = 510;
            this.tbDeliveryFilePath.TextChanged += new System.EventHandler(this.tbDeliveryFilePath_TextChanged);
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
            this.tbpStream.Controls.Add(this.btStreamAdd);
            this.tbpStream.Controls.Add(this.nudStreamPortNumber);
            this.tbpStream.Controls.Add(this.label145);
            this.tbpStream.Controls.Add(this.cboStreamProtocol);
            this.tbpStream.Controls.Add(this.label144);
            this.tbpStream.Controls.Add(this.cboStreamCollectionType);
            this.tbpStream.Controls.Add(this.label142);
            this.tbpStream.Controls.Add(this.tbStreamIpAddress);
            this.tbpStream.Controls.Add(this.label143);
            this.tbpStream.Location = new System.Drawing.Point(4, 22);
            this.tbpStream.Name = "tbpStream";
            this.tbpStream.Size = new System.Drawing.Size(909, 416);
            this.tbpStream.TabIndex = 8;
            this.tbpStream.Text = "Stream";
            this.tbpStream.UseVisualStyleBackColor = true;
            // 
            // nudStreamMulticastSourcePort
            // 
            this.nudStreamMulticastSourcePort.Location = new System.Drawing.Point(264, 251);
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
            this.label147.Location = new System.Drawing.Point(17, 253);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(105, 13);
            this.label147.TabIndex = 526;
            this.label147.Text = "Multicast source port";
            // 
            // tbStreamMulticastSourceIP
            // 
            this.tbStreamMulticastSourceIP.Location = new System.Drawing.Point(264, 211);
            this.tbStreamMulticastSourceIP.Name = "tbStreamMulticastSourceIP";
            this.tbStreamMulticastSourceIP.Size = new System.Drawing.Size(150, 20);
            this.tbStreamMulticastSourceIP.TabIndex = 525;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(18, 214);
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
            this.btFindIPAddress.Click += new System.EventHandler(this.btFindIPAddress_Click);
            // 
            // btStreamAdd
            // 
            this.btStreamAdd.Location = new System.Drawing.Point(19, 376);
            this.btStreamAdd.Name = "btStreamAdd";
            this.btStreamAdd.Size = new System.Drawing.Size(75, 23);
            this.btStreamAdd.TabIndex = 528;
            this.btStreamAdd.Text = "Update";
            this.btStreamAdd.UseVisualStyleBackColor = true;
            this.btStreamAdd.Click += new System.EventHandler(this.btStreamAdd_Click);
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
            // cboStreamCollectionType
            // 
            this.cboStreamCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboStreamCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboStreamCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStreamCollectionType.FormattingEnabled = true;
            this.cboStreamCollectionType.Location = new System.Drawing.Point(264, 172);
            this.cboStreamCollectionType.Name = "cboStreamCollectionType";
            this.cboStreamCollectionType.Size = new System.Drawing.Size(150, 21);
            this.cboStreamCollectionType.TabIndex = 522;
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(18, 175);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(80, 13);
            this.label142.TabIndex = 521;
            this.label142.Text = "Collection Type";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btTuningLoadFromScan);
            this.panel1.Controls.Add(this.btChange);
            this.panel1.Controls.Add(this.btTuningParameters);
            this.panel1.Controls.Add(this.btSelectedFrequencyDetails);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.lvSelectedFrequencies);
            this.panel1.Controls.Add(this.btDelete);
            this.panel1.Location = new System.Drawing.Point(12, 474);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 161);
            this.panel1.TabIndex = 400;
            // 
            // btTuningLoadFromScan
            // 
            this.btTuningLoadFromScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTuningLoadFromScan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btTuningLoadFromScan.Location = new System.Drawing.Point(774, 129);
            this.btTuningLoadFromScan.Name = "btTuningLoadFromScan";
            this.btTuningLoadFromScan.Size = new System.Drawing.Size(113, 23);
            this.btTuningLoadFromScan.TabIndex = 406;
            this.btTuningLoadFromScan.Text = "Load From Scan";
            this.btTuningLoadFromScan.UseVisualStyleBackColor = true;
            this.btTuningLoadFromScan.Click += new System.EventHandler(this.btTuningLoadFromScan_Click);
            // 
            // btChange
            // 
            this.btChange.Enabled = false;
            this.btChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChange.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btChange.Location = new System.Drawing.Point(22, 129);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(75, 23);
            this.btChange.TabIndex = 402;
            this.btChange.Text = "Change";
            this.btChange.UseVisualStyleBackColor = true;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // btTuningParameters
            // 
            this.btTuningParameters.Enabled = false;
            this.btTuningParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTuningParameters.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btTuningParameters.Location = new System.Drawing.Point(186, 129);
            this.btTuningParameters.Name = "btTuningParameters";
            this.btTuningParameters.Size = new System.Drawing.Size(75, 23);
            this.btTuningParameters.TabIndex = 404;
            this.btTuningParameters.Text = "Advanced";
            this.btTuningParameters.UseVisualStyleBackColor = true;
            this.btTuningParameters.Click += new System.EventHandler(this.btTuningParameters_Click);
            // 
            // btSelectedFrequencyDetails
            // 
            this.btSelectedFrequencyDetails.Enabled = false;
            this.btSelectedFrequencyDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectedFrequencyDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btSelectedFrequencyDetails.Location = new System.Drawing.Point(267, 129);
            this.btSelectedFrequencyDetails.Name = "btSelectedFrequencyDetails";
            this.btSelectedFrequencyDetails.Size = new System.Drawing.Size(75, 23);
            this.btSelectedFrequencyDetails.TabIndex = 405;
            this.btSelectedFrequencyDetails.Text = "Details";
            this.btSelectedFrequencyDetails.UseVisualStyleBackColor = true;
            this.btSelectedFrequencyDetails.Click += new System.EventHandler(this.btSelectedFrequencyDetails_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Location = new System.Drawing.Point(19, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 13);
            this.label25.TabIndex = 138;
            this.label25.Text = "Selected Frequencies";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvSelectedFrequencies
            // 
            this.lvSelectedFrequencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Frequency,
            this.Provider,
            this.Type,
            this.Collection});
            this.lvSelectedFrequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSelectedFrequencies.FullRowSelect = true;
            this.lvSelectedFrequencies.GridLines = true;
            this.lvSelectedFrequencies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSelectedFrequencies.HideSelection = false;
            this.lvSelectedFrequencies.Location = new System.Drawing.Point(22, 26);
            this.lvSelectedFrequencies.MultiSelect = false;
            this.lvSelectedFrequencies.Name = "lvSelectedFrequencies";
            this.lvSelectedFrequencies.Size = new System.Drawing.Size(865, 97);
            this.lvSelectedFrequencies.TabIndex = 401;
            this.lvSelectedFrequencies.UseCompatibleStateImageBehavior = false;
            this.lvSelectedFrequencies.View = System.Windows.Forms.View.Details;
            this.lvSelectedFrequencies.SelectedIndexChanged += new System.EventHandler(this.lvSelectedFrequencies_SelectedIndexChanged);
            this.lvSelectedFrequencies.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSelectedFrequencies_DoubleClick);
            // 
            // Frequency
            // 
            this.Frequency.Text = "Frequency";
            this.Frequency.Width = 103;
            // 
            // Provider
            // 
            this.Provider.Text = "Provider";
            this.Provider.Width = 523;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 115;
            // 
            // Collection
            // 
            this.Collection.Text = "Collection Type";
            this.Collection.Width = 118;
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btDelete.Location = new System.Drawing.Point(105, 129);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 403;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // tabXMLTV
            // 
            this.tabXMLTV.Controls.Add(this.cbDontCreateImportChannels);
            this.tabXMLTV.Controls.Add(this.btXmltvClear);
            this.tabXMLTV.Controls.Add(this.btXmltvExcludeAll);
            this.tabXMLTV.Controls.Add(this.btXmltvIncludeAll);
            this.tabXMLTV.Controls.Add(this.btXmltvLoadFiles);
            this.tabXMLTV.Controls.Add(this.dgXmltvChannelChanges);
            this.tabXMLTV.Controls.Add(this.label117);
            this.tabXMLTV.Controls.Add(this.panel3);
            this.tabXMLTV.Controls.Add(this.gpbFiles);
            this.tabXMLTV.Location = new System.Drawing.Point(4, 22);
            this.tabXMLTV.Name = "tabXMLTV";
            this.tabXMLTV.Size = new System.Drawing.Size(942, 646);
            this.tabXMLTV.TabIndex = 11;
            this.tabXMLTV.Text = "Imports";
            this.tabXMLTV.UseVisualStyleBackColor = true;
            // 
            // cbDontCreateImportChannels
            // 
            this.cbDontCreateImportChannels.AutoSize = true;
            this.cbDontCreateImportChannels.Location = new System.Drawing.Point(122, 617);
            this.cbDontCreateImportChannels.Name = "cbDontCreateImportChannels";
            this.cbDontCreateImportChannels.Size = new System.Drawing.Size(192, 17);
            this.cbDontCreateImportChannels.TabIndex = 305;
            this.cbDontCreateImportChannels.Text = "Don\'t create channels not in the list";
            this.cbDontCreateImportChannels.UseVisualStyleBackColor = true;
            // 
            // btXmltvClear
            // 
            this.btXmltvClear.Enabled = false;
            this.btXmltvClear.Location = new System.Drawing.Point(619, 612);
            this.btXmltvClear.Name = "btXmltvClear";
            this.btXmltvClear.Size = new System.Drawing.Size(88, 25);
            this.btXmltvClear.TabIndex = 310;
            this.btXmltvClear.Text = "Clear";
            this.btXmltvClear.UseVisualStyleBackColor = true;
            this.btXmltvClear.Click += new System.EventHandler(this.btXmltvClear_Click);
            // 
            // btXmltvExcludeAll
            // 
            this.btXmltvExcludeAll.Enabled = false;
            this.btXmltvExcludeAll.Location = new System.Drawing.Point(820, 612);
            this.btXmltvExcludeAll.Name = "btXmltvExcludeAll";
            this.btXmltvExcludeAll.Size = new System.Drawing.Size(97, 25);
            this.btXmltvExcludeAll.TabIndex = 312;
            this.btXmltvExcludeAll.Text = "Exclude All";
            this.btXmltvExcludeAll.UseVisualStyleBackColor = true;
            this.btXmltvExcludeAll.Click += new System.EventHandler(this.btXmltvExcludeAll_Click);
            // 
            // btXmltvIncludeAll
            // 
            this.btXmltvIncludeAll.Enabled = false;
            this.btXmltvIncludeAll.Location = new System.Drawing.Point(715, 612);
            this.btXmltvIncludeAll.Name = "btXmltvIncludeAll";
            this.btXmltvIncludeAll.Size = new System.Drawing.Size(97, 25);
            this.btXmltvIncludeAll.TabIndex = 311;
            this.btXmltvIncludeAll.Text = "Include All";
            this.btXmltvIncludeAll.UseVisualStyleBackColor = true;
            this.btXmltvIncludeAll.Click += new System.EventHandler(this.btXmltvIncludeAll_Click);
            // 
            // btXmltvLoadFiles
            // 
            this.btXmltvLoadFiles.Enabled = false;
            this.btXmltvLoadFiles.Location = new System.Drawing.Point(14, 612);
            this.btXmltvLoadFiles.Name = "btXmltvLoadFiles";
            this.btXmltvLoadFiles.Size = new System.Drawing.Size(75, 23);
            this.btXmltvLoadFiles.TabIndex = 304;
            this.btXmltvLoadFiles.Text = "Load Files";
            this.btXmltvLoadFiles.UseVisualStyleBackColor = true;
            this.btXmltvLoadFiles.Click += new System.EventHandler(this.btXmltvLoadFiles_Click);
            // 
            // dgXmltvChannelChanges
            // 
            this.dgXmltvChannelChanges.AllowUserToAddRows = false;
            this.dgXmltvChannelChanges.AllowUserToDeleteRows = false;
            this.dgXmltvChannelChanges.AutoGenerateColumns = false;
            this.dgXmltvChannelChanges.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgXmltvChannelChanges.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgXmltvChannelChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgXmltvChannelChanges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xmltvDisplayNameColumn,
            this.xmltvChannelNumberColumn,
            this.xmltvExcludedColumn,
            this.xmltvNewNameColumn,
            this.xmltvCallSignColumn});
            this.dgXmltvChannelChanges.DataSource = this.xmltvChannelChangeBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgXmltvChannelChanges.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgXmltvChannelChanges.GridColor = System.Drawing.SystemColors.Control;
            this.dgXmltvChannelChanges.Location = new System.Drawing.Point(15, 388);
            this.dgXmltvChannelChanges.Name = "dgXmltvChannelChanges";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgXmltvChannelChanges.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgXmltvChannelChanges.RowHeadersVisible = false;
            this.dgXmltvChannelChanges.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgXmltvChannelChanges.RowTemplate.Height = 18;
            this.dgXmltvChannelChanges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgXmltvChannelChanges.Size = new System.Drawing.Size(901, 214);
            this.dgXmltvChannelChanges.TabIndex = 303;
            this.dgXmltvChannelChanges.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.onXmltvCellFormatting);
            this.dgXmltvChannelChanges.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgXmltvChannelChanges_CellValueChanged);
            this.dgXmltvChannelChanges.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgXmltvChannelChanges_ColumnHeaderMouseClick);
            this.dgXmltvChannelChanges.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgXmltvChannelChanges_CurrentCellDirtyStateChanged);
            this.dgXmltvChannelChanges.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgXmltvChannelChanges_EditingControlShowing);
            // 
            // xmltvDisplayNameColumn
            // 
            this.xmltvDisplayNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.xmltvDisplayNameColumn.DataPropertyName = "DisplayName";
            this.xmltvDisplayNameColumn.HeaderText = "Display Name";
            this.xmltvDisplayNameColumn.Name = "xmltvDisplayNameColumn";
            this.xmltvDisplayNameColumn.ReadOnly = true;
            this.xmltvDisplayNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.xmltvDisplayNameColumn.Width = 89;
            // 
            // xmltvChannelNumberColumn
            // 
            this.xmltvChannelNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.xmltvChannelNumberColumn.DataPropertyName = "DisplayedChannelNumber";
            this.xmltvChannelNumberColumn.HeaderText = "Channel Number";
            this.xmltvChannelNumberColumn.Name = "xmltvChannelNumberColumn";
            this.xmltvChannelNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.xmltvChannelNumberColumn.Width = 102;
            // 
            // xmltvExcludedColumn
            // 
            this.xmltvExcludedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.xmltvExcludedColumn.DataPropertyName = "Excluded";
            this.xmltvExcludedColumn.HeaderText = "Excluded";
            this.xmltvExcludedColumn.Name = "xmltvExcludedColumn";
            this.xmltvExcludedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.xmltvExcludedColumn.Width = 76;
            // 
            // xmltvNewNameColumn
            // 
            this.xmltvNewNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.xmltvNewNameColumn.DataPropertyName = "NewName";
            this.xmltvNewNameColumn.HeaderText = "New Name";
            this.xmltvNewNameColumn.MaxInputLength = 256;
            this.xmltvNewNameColumn.Name = "xmltvNewNameColumn";
            this.xmltvNewNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.xmltvNewNameColumn.Width = 79;
            // 
            // xmltvCallSignColumn
            // 
            this.xmltvCallSignColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.xmltvCallSignColumn.DataPropertyName = "CallSign";
            this.xmltvCallSignColumn.HeaderText = "Call Sign";
            this.xmltvCallSignColumn.Name = "xmltvCallSignColumn";
            this.xmltvCallSignColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // xmltvChannelChangeBindingSource
            // 
            this.xmltvChannelChangeBindingSource.DataSource = typeof(DomainObjects.ImportChannelChange);
            this.xmltvChannelChangeBindingSource.Sort = "";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(12, 370);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(91, 13);
            this.label117.TabIndex = 21;
            this.label117.Text = "Channel Changes";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btXmltvDelete);
            this.panel3.Controls.Add(this.lvXmltvSelectedFiles);
            this.panel3.Controls.Add(this.label112);
            this.panel3.Location = new System.Drawing.Point(15, 210);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(901, 148);
            this.panel3.TabIndex = 20;
            // 
            // btXmltvDelete
            // 
            this.btXmltvDelete.Enabled = false;
            this.btXmltvDelete.Location = new System.Drawing.Point(807, 114);
            this.btXmltvDelete.Name = "btXmltvDelete";
            this.btXmltvDelete.Size = new System.Drawing.Size(75, 23);
            this.btXmltvDelete.TabIndex = 23;
            this.btXmltvDelete.Text = "Delete";
            this.btXmltvDelete.UseVisualStyleBackColor = true;
            this.btXmltvDelete.Click += new System.EventHandler(this.btXmltvDelete_Click);
            // 
            // lvXmltvSelectedFiles
            // 
            this.lvXmltvSelectedFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader22,
            this.columnHeader21,
            this.columnHeader20,
            this.columnHeader18,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader26,
            this.columnHeader25});
            this.lvXmltvSelectedFiles.FullRowSelect = true;
            this.lvXmltvSelectedFiles.GridLines = true;
            this.lvXmltvSelectedFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvXmltvSelectedFiles.Location = new System.Drawing.Point(20, 30);
            this.lvXmltvSelectedFiles.Name = "lvXmltvSelectedFiles";
            this.lvXmltvSelectedFiles.Size = new System.Drawing.Size(863, 77);
            this.lvXmltvSelectedFiles.TabIndex = 22;
            this.lvXmltvSelectedFiles.UseCompatibleStateImageBehavior = false;
            this.lvXmltvSelectedFiles.View = System.Windows.Forms.View.Details;
            this.lvXmltvSelectedFiles.SelectedIndexChanged += new System.EventHandler(this.lvXmltvSelectedFiles_SelectedIndexChanged);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "File Name";
            this.columnHeader10.Width = 301;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Language";
            this.columnHeader11.Width = 66;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Mode";
            this.columnHeader12.Width = 173;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Channel ID";
            this.columnHeader22.Width = 93;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Time Zone";
            this.columnHeader21.Width = 75;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Local Images";
            this.columnHeader20.Width = 90;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "No Lookups";
            this.columnHeader18.Width = 90;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "Ignore Tags";
            this.columnHeader23.Width = 90;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Annotate New";
            this.columnHeader24.Width = 90;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "Annotate Live";
            this.columnHeader26.Width = 90;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Prev Shown Default";
            this.columnHeader25.Width = 120;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(17, 11);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(73, 13);
            this.label112.TabIndex = 21;
            this.label112.Text = "Selected Files";
            // 
            // gpbFiles
            // 
            this.gpbFiles.Controls.Add(this.cbXmltvProcessLiveTag);
            this.gpbFiles.Controls.Add(this.label101);
            this.gpbFiles.Controls.Add(this.cboXmltvStoreImagesLocally);
            this.gpbFiles.Controls.Add(this.cbXmltvSetPreviouslyShownDefault);
            this.gpbFiles.Controls.Add(this.label160);
            this.gpbFiles.Controls.Add(this.cbXmltvProcessNewTag);
            this.gpbFiles.Controls.Add(this.label156);
            this.gpbFiles.Controls.Add(this.label46);
            this.gpbFiles.Controls.Add(this.cbIgnoreEpisodeTags);
            this.gpbFiles.Controls.Add(this.label108);
            this.gpbFiles.Controls.Add(this.cboXmltvTimeZone);
            this.gpbFiles.Controls.Add(this.label84);
            this.gpbFiles.Controls.Add(this.cboXmltvIdFormat);
            this.gpbFiles.Controls.Add(this.label26);
            this.gpbFiles.Controls.Add(this.cbXmltvNoLookup);
            this.gpbFiles.Controls.Add(this.label120);
            this.gpbFiles.Controls.Add(this.cboXmltvPrecedence);
            this.gpbFiles.Controls.Add(this.label115);
            this.gpbFiles.Controls.Add(this.btXmltvAdd);
            this.gpbFiles.Controls.Add(this.cboXmltvLanguage);
            this.gpbFiles.Controls.Add(this.label114);
            this.gpbFiles.Controls.Add(this.tbXmltvPath);
            this.gpbFiles.Controls.Add(this.label113);
            this.gpbFiles.Controls.Add(this.btXmltvBrowse);
            this.gpbFiles.Location = new System.Drawing.Point(15, 11);
            this.gpbFiles.Name = "gpbFiles";
            this.gpbFiles.Size = new System.Drawing.Size(901, 183);
            this.gpbFiles.TabIndex = 0;
            this.gpbFiles.TabStop = false;
            this.gpbFiles.Text = "File";
            // 
            // cbXmltvProcessLiveTag
            // 
            this.cbXmltvProcessLiveTag.AutoSize = true;
            this.cbXmltvProcessLiveTag.Location = new System.Drawing.Point(725, 129);
            this.cbXmltvProcessLiveTag.Name = "cbXmltvProcessLiveTag";
            this.cbXmltvProcessLiveTag.Size = new System.Drawing.Size(15, 14);
            this.cbXmltvProcessLiveTag.TabIndex = 29;
            this.cbXmltvProcessLiveTag.UseVisualStyleBackColor = true;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(445, 129);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(193, 13);
            this.label101.TabIndex = 28;
            this.label101.Text = "Annotate live programmes if tag present";
            // 
            // cboXmltvStoreImagesLocally
            // 
            this.cboXmltvStoreImagesLocally.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXmltvStoreImagesLocally.FormattingEnabled = true;
            this.cboXmltvStoreImagesLocally.Items.AddRange(new object[] {
            "None",
            "Channels",
            "Programmes",
            "Channels and Programmes"});
            this.cboXmltvStoreImagesLocally.Location = new System.Drawing.Point(155, 153);
            this.cboXmltvStoreImagesLocally.Name = "cboXmltvStoreImagesLocally";
            this.cboXmltvStoreImagesLocally.Size = new System.Drawing.Size(196, 21);
            this.cboXmltvStoreImagesLocally.TabIndex = 15;
            // 
            // cbXmltvSetPreviouslyShownDefault
            // 
            this.cbXmltvSetPreviouslyShownDefault.AutoSize = true;
            this.cbXmltvSetPreviouslyShownDefault.Location = new System.Drawing.Point(725, 156);
            this.cbXmltvSetPreviouslyShownDefault.Name = "cbXmltvSetPreviouslyShownDefault";
            this.cbXmltvSetPreviouslyShownDefault.Size = new System.Drawing.Size(15, 14);
            this.cbXmltvSetPreviouslyShownDefault.TabIndex = 31;
            this.cbXmltvSetPreviouslyShownDefault.UseVisualStyleBackColor = true;
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(445, 156);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(232, 13);
            this.label160.TabIndex = 30;
            this.label160.Text = "Set empty previously shown tags to default date";
            // 
            // cbXmltvProcessNewTag
            // 
            this.cbXmltvProcessNewTag.AutoSize = true;
            this.cbXmltvProcessNewTag.Location = new System.Drawing.Point(725, 102);
            this.cbXmltvProcessNewTag.Name = "cbXmltvProcessNewTag";
            this.cbXmltvProcessNewTag.Size = new System.Drawing.Size(15, 14);
            this.cbXmltvProcessNewTag.TabIndex = 27;
            this.cbXmltvProcessNewTag.UseVisualStyleBackColor = true;
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(445, 102);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(197, 13);
            this.label156.TabIndex = 25;
            this.label156.Text = "Annotate new programmes if tag present";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(25, 156);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(100, 13);
            this.label46.TabIndex = 13;
            this.label46.Text = "Store images locally";
            // 
            // cbIgnoreEpisodeTags
            // 
            this.cbIgnoreEpisodeTags.AutoSize = true;
            this.cbIgnoreEpisodeTags.Location = new System.Drawing.Point(725, 76);
            this.cbIgnoreEpisodeTags.Name = "cbIgnoreEpisodeTags";
            this.cbIgnoreEpisodeTags.Size = new System.Drawing.Size(15, 14);
            this.cbIgnoreEpisodeTags.TabIndex = 23;
            this.cbIgnoreEpisodeTags.UseVisualStyleBackColor = true;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(445, 76);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(165, 13);
            this.label108.TabIndex = 21;
            this.label108.Text = "Ignore non-standard episode tags";
            // 
            // cboXmltvTimeZone
            // 
            this.cboXmltvTimeZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboXmltvTimeZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXmltvTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXmltvTimeZone.FormattingEnabled = true;
            this.cboXmltvTimeZone.Location = new System.Drawing.Point(155, 126);
            this.cboXmltvTimeZone.MaxDropDownItems = 20;
            this.cboXmltvTimeZone.Name = "cboXmltvTimeZone";
            this.cboXmltvTimeZone.Size = new System.Drawing.Size(196, 21);
            this.cboXmltvTimeZone.Sorted = true;
            this.cboXmltvTimeZone.TabIndex = 11;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(25, 129);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(56, 13);
            this.label84.TabIndex = 10;
            this.label84.Text = "Time zone";
            // 
            // cboXmltvIdFormat
            // 
            this.cboXmltvIdFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboXmltvIdFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXmltvIdFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXmltvIdFormat.FormattingEnabled = true;
            this.cboXmltvIdFormat.Items.AddRange(new object[] {
            " -- Undefined --",
            "Service ID",
            "User channel number",
            "Full channel identification",
            "Channel name",
            "Schedules Direct"});
            this.cboXmltvIdFormat.Location = new System.Drawing.Point(155, 99);
            this.cboXmltvIdFormat.MaxDropDownItems = 20;
            this.cboXmltvIdFormat.Name = "cboXmltvIdFormat";
            this.cboXmltvIdFormat.Size = new System.Drawing.Size(197, 21);
            this.cboXmltvIdFormat.TabIndex = 9;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(25, 102);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(124, 13);
            this.label26.TabIndex = 8;
            this.label26.Text = "Format of the channel ID";
            // 
            // cbXmltvNoLookup
            // 
            this.cbXmltvNoLookup.AutoSize = true;
            this.cbXmltvNoLookup.Location = new System.Drawing.Point(724, 50);
            this.cbXmltvNoLookup.Name = "cbXmltvNoLookup";
            this.cbXmltvNoLookup.Size = new System.Drawing.Size(15, 14);
            this.cbXmltvNoLookup.TabIndex = 19;
            this.cbXmltvNoLookup.UseVisualStyleBackColor = true;
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(445, 50);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(196, 13);
            this.label120.TabIndex = 17;
            this.label120.Text = "Don\'t lookup metadata for imported data";
            // 
            // cboXmltvPrecedence
            // 
            this.cboXmltvPrecedence.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboXmltvPrecedence.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXmltvPrecedence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXmltvPrecedence.FormattingEnabled = true;
            this.cboXmltvPrecedence.Items.AddRange(new object[] {
            "Merge, broadcast takes precedence",
            "Merge, import takes precedence",
            "Append import to broadcast",
            "Replace broadcast with import"});
            this.cboXmltvPrecedence.Location = new System.Drawing.Point(155, 73);
            this.cboXmltvPrecedence.MaxDropDownItems = 20;
            this.cboXmltvPrecedence.Name = "cboXmltvPrecedence";
            this.cboXmltvPrecedence.Size = new System.Drawing.Size(197, 21);
            this.cboXmltvPrecedence.TabIndex = 7;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(26, 76);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(88, 13);
            this.label115.TabIndex = 6;
            this.label115.Text = "Processing mode";
            // 
            // btXmltvAdd
            // 
            this.btXmltvAdd.Enabled = false;
            this.btXmltvAdd.Location = new System.Drawing.Point(808, 149);
            this.btXmltvAdd.Name = "btXmltvAdd";
            this.btXmltvAdd.Size = new System.Drawing.Size(75, 23);
            this.btXmltvAdd.TabIndex = 33;
            this.btXmltvAdd.Text = "Add";
            this.btXmltvAdd.UseVisualStyleBackColor = true;
            this.btXmltvAdd.Click += new System.EventHandler(this.btXmltvAdd_Click);
            // 
            // cboXmltvLanguage
            // 
            this.cboXmltvLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboXmltvLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXmltvLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXmltvLanguage.FormattingEnabled = true;
            this.cboXmltvLanguage.Location = new System.Drawing.Point(155, 47);
            this.cboXmltvLanguage.MaxDropDownItems = 20;
            this.cboXmltvLanguage.Name = "cboXmltvLanguage";
            this.cboXmltvLanguage.Size = new System.Drawing.Size(197, 21);
            this.cboXmltvLanguage.Sorted = true;
            this.cboXmltvLanguage.TabIndex = 5;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(26, 50);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(55, 13);
            this.label114.TabIndex = 4;
            this.label114.Text = "Language";
            // 
            // tbXmltvPath
            // 
            this.tbXmltvPath.Location = new System.Drawing.Point(155, 22);
            this.tbXmltvPath.Name = "tbXmltvPath";
            this.tbXmltvPath.Size = new System.Drawing.Size(634, 20);
            this.tbXmltvPath.TabIndex = 2;
            this.tbXmltvPath.TextChanged += new System.EventHandler(this.tbXmltvPath_TextChanged);
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(26, 25);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(29, 13);
            this.label113.TabIndex = 1;
            this.label113.Text = "Path";
            // 
            // btXmltvBrowse
            // 
            this.btXmltvBrowse.Location = new System.Drawing.Point(808, 19);
            this.btXmltvBrowse.Name = "btXmltvBrowse";
            this.btXmltvBrowse.Size = new System.Drawing.Size(75, 24);
            this.btXmltvBrowse.TabIndex = 3;
            this.btXmltvBrowse.Text = "Browse...";
            this.btXmltvBrowse.UseVisualStyleBackColor = true;
            this.btXmltvBrowse.Click += new System.EventHandler(this.btXmltvBrowse_Click);
            // 
            // tabSchedulesDirect
            // 
            this.tabSchedulesDirect.Controls.Add(this.cbSdEnabled);
            this.tabSchedulesDirect.Controls.Add(this.gpSchedulesDirect);
            this.tabSchedulesDirect.Location = new System.Drawing.Point(4, 22);
            this.tabSchedulesDirect.Name = "tabSchedulesDirect";
            this.tabSchedulesDirect.Size = new System.Drawing.Size(942, 646);
            this.tabSchedulesDirect.TabIndex = 15;
            this.tabSchedulesDirect.Text = "Schedules Direct";
            this.tabSchedulesDirect.UseVisualStyleBackColor = true;
            // 
            // cbSdEnabled
            // 
            this.cbSdEnabled.AutoSize = true;
            this.cbSdEnabled.Location = new System.Drawing.Point(143, 15);
            this.cbSdEnabled.Name = "cbSdEnabled";
            this.cbSdEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbSdEnabled.TabIndex = 17;
            this.cbSdEnabled.Text = "Enabled";
            this.cbSdEnabled.UseVisualStyleBackColor = true;
            this.cbSdEnabled.CheckedChanged += new System.EventHandler(this.cbSdEnabled_CheckedChanged);
            // 
            // gpSchedulesDirect
            // 
            this.gpSchedulesDirect.Controls.Add(this.groupBox11);
            this.gpSchedulesDirect.Controls.Add(this.btSdClear);
            this.gpSchedulesDirect.Controls.Add(this.btSdExcludeAll);
            this.gpSchedulesDirect.Controls.Add(this.dgSdChannels);
            this.gpSchedulesDirect.Controls.Add(this.btSdIncludeAll);
            this.gpSchedulesDirect.Controls.Add(this.btSdRemove);
            this.gpSchedulesDirect.Controls.Add(this.label164);
            this.gpSchedulesDirect.Controls.Add(this.label165);
            this.gpSchedulesDirect.Controls.Add(this.btSdAdd);
            this.gpSchedulesDirect.Controls.Add(this.lvSdLineups);
            this.gpSchedulesDirect.Controls.Add(this.btSdChangeLineups);
            this.gpSchedulesDirect.Location = new System.Drawing.Point(31, 16);
            this.gpSchedulesDirect.Name = "gpSchedulesDirect";
            this.gpSchedulesDirect.Size = new System.Drawing.Size(874, 603);
            this.gpSchedulesDirect.TabIndex = 16;
            this.gpSchedulesDirect.TabStop = false;
            this.gpSchedulesDirect.Text = "Schedules Direct";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.cboSdStoreImagesLocally);
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Controls.Add(this.cbSdUseLongDescriptions);
            this.groupBox11.Controls.Add(this.label158);
            this.groupBox11.Location = new System.Drawing.Point(30, 526);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(665, 61);
            this.groupBox11.TabIndex = 25;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Options";
            // 
            // cboSdStoreImagesLocally
            // 
            this.cboSdStoreImagesLocally.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSdStoreImagesLocally.FormattingEnabled = true;
            this.cboSdStoreImagesLocally.Items.AddRange(new object[] {
            "None",
            "Channels",
            "Programmes",
            "Channels and Programmes"});
            this.cboSdStoreImagesLocally.Location = new System.Drawing.Point(148, 27);
            this.cboSdStoreImagesLocally.Name = "cboSdStoreImagesLocally";
            this.cboSdStoreImagesLocally.Size = new System.Drawing.Size(158, 21);
            this.cboSdStoreImagesLocally.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(373, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Use long descriptions";
            // 
            // cbSdUseLongDescriptions
            // 
            this.cbSdUseLongDescriptions.AutoSize = true;
            this.cbSdUseLongDescriptions.Location = new System.Drawing.Point(515, 30);
            this.cbSdUseLongDescriptions.Name = "cbSdUseLongDescriptions";
            this.cbSdUseLongDescriptions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbSdUseLongDescriptions.Size = new System.Drawing.Size(15, 14);
            this.cbSdUseLongDescriptions.TabIndex = 33;
            this.cbSdUseLongDescriptions.UseVisualStyleBackColor = true;
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(32, 30);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(100, 13);
            this.label158.TabIndex = 27;
            this.label158.Text = "Store images locally";
            // 
            // btSdClear
            // 
            this.btSdClear.Enabled = false;
            this.btSdClear.Location = new System.Drawing.Point(716, 243);
            this.btSdClear.Name = "btSdClear";
            this.btSdClear.Size = new System.Drawing.Size(108, 25);
            this.btSdClear.TabIndex = 19;
            this.btSdClear.Text = "Clear";
            this.btSdClear.UseVisualStyleBackColor = true;
            this.btSdClear.Click += new System.EventHandler(this.btSdClear_Click);
            // 
            // btSdExcludeAll
            // 
            this.btSdExcludeAll.Enabled = false;
            this.btSdExcludeAll.Location = new System.Drawing.Point(716, 212);
            this.btSdExcludeAll.Name = "btSdExcludeAll";
            this.btSdExcludeAll.Size = new System.Drawing.Size(108, 25);
            this.btSdExcludeAll.TabIndex = 17;
            this.btSdExcludeAll.Text = "Exclude All";
            this.btSdExcludeAll.UseVisualStyleBackColor = true;
            this.btSdExcludeAll.Click += new System.EventHandler(this.btSdExcludeAll_Click);
            // 
            // dgSdChannels
            // 
            this.dgSdChannels.AllowUserToAddRows = false;
            this.dgSdChannels.AllowUserToDeleteRows = false;
            this.dgSdChannels.AutoGenerateColumns = false;
            this.dgSdChannels.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSdChannels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSdChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSdChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sdChannelName,
            this.sdChannelCallSign,
            this.sdChannelLineup,
            this.sdChannelIdentification,
            this.sdChannelExcluded,
            this.sdChannelUserName,
            this.sdChannelUserNumber,
            this.sdChannelUserCallSign});
            this.dgSdChannels.DataSource = this.sdBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSdChannels.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgSdChannels.GridColor = System.Drawing.SystemColors.Control;
            this.dgSdChannels.Location = new System.Drawing.Point(30, 181);
            this.dgSdChannels.Name = "dgSdChannels";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSdChannels.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgSdChannels.RowHeadersVisible = false;
            this.dgSdChannels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgSdChannels.RowTemplate.Height = 18;
            this.dgSdChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSdChannels.Size = new System.Drawing.Size(665, 329);
            this.dgSdChannels.TabIndex = 14;
            this.dgSdChannels.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSdChannelsCellFormatting);
            this.dgSdChannels.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgSdChannelsCellPainting);
            this.dgSdChannels.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSdChannelsCellValueChanged);
            this.dgSdChannels.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSdChannelsColumnHeaderMouseClick);
            this.dgSdChannels.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgSdChannelsCurrentCellDirtyStateChanged);
            this.dgSdChannels.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgSdChannelsEditingControlShowing);
            // 
            // sdChannelName
            // 
            this.sdChannelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelName.DataPropertyName = "Name";
            this.sdChannelName.HeaderText = "Name";
            this.sdChannelName.Name = "sdChannelName";
            this.sdChannelName.ReadOnly = true;
            this.sdChannelName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelName.Width = 60;
            // 
            // sdChannelCallSign
            // 
            this.sdChannelCallSign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelCallSign.DataPropertyName = "CallSign";
            this.sdChannelCallSign.HeaderText = "Call Sign";
            this.sdChannelCallSign.Name = "sdChannelCallSign";
            this.sdChannelCallSign.ReadOnly = true;
            this.sdChannelCallSign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelCallSign.Width = 68;
            // 
            // sdChannelLineup
            // 
            this.sdChannelLineup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelLineup.DataPropertyName = "LineupName";
            this.sdChannelLineup.HeaderText = "Lineup";
            this.sdChannelLineup.Name = "sdChannelLineup";
            this.sdChannelLineup.ReadOnly = true;
            this.sdChannelLineup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelLineup.Width = 64;
            // 
            // sdChannelIdentification
            // 
            this.sdChannelIdentification.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelIdentification.DataPropertyName = "ChannelIdentification";
            this.sdChannelIdentification.HeaderText = "Identification";
            this.sdChannelIdentification.Name = "sdChannelIdentification";
            this.sdChannelIdentification.ReadOnly = true;
            this.sdChannelIdentification.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelIdentification.Width = 92;
            // 
            // sdChannelExcluded
            // 
            this.sdChannelExcluded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelExcluded.DataPropertyName = "Excluded";
            this.sdChannelExcluded.HeaderText = "Excluded";
            this.sdChannelExcluded.Name = "sdChannelExcluded";
            this.sdChannelExcluded.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelExcluded.Width = 76;
            // 
            // sdChannelUserName
            // 
            this.sdChannelUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelUserName.DataPropertyName = "UserName";
            this.sdChannelUserName.HeaderText = "New Name";
            this.sdChannelUserName.Name = "sdChannelUserName";
            this.sdChannelUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelUserName.Width = 79;
            // 
            // sdChannelUserNumber
            // 
            this.sdChannelUserNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdChannelUserNumber.DataPropertyName = "UserChannelNumber";
            this.sdChannelUserNumber.HeaderText = "New Channel No.";
            this.sdChannelUserNumber.Name = "sdChannelUserNumber";
            this.sdChannelUserNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.sdChannelUserNumber.Width = 91;
            // 
            // sdChannelUserCallSign
            // 
            this.sdChannelUserCallSign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sdChannelUserCallSign.DataPropertyName = "UserCallSign";
            this.sdChannelUserCallSign.HeaderText = "New Call Sign";
            this.sdChannelUserCallSign.Name = "sdChannelUserCallSign";
            this.sdChannelUserCallSign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // sdBindingSource
            // 
            this.sdBindingSource.DataSource = typeof(DomainObjects.SchedulesDirectChannel);
            this.sdBindingSource.Sort = "";
            // 
            // btSdIncludeAll
            // 
            this.btSdIncludeAll.Enabled = false;
            this.btSdIncludeAll.Location = new System.Drawing.Point(716, 181);
            this.btSdIncludeAll.Name = "btSdIncludeAll";
            this.btSdIncludeAll.Size = new System.Drawing.Size(108, 25);
            this.btSdIncludeAll.TabIndex = 15;
            this.btSdIncludeAll.Text = "Include All";
            this.btSdIncludeAll.UseVisualStyleBackColor = true;
            this.btSdIncludeAll.Click += new System.EventHandler(this.btSdIncludeAll_Click);
            // 
            // btSdRemove
            // 
            this.btSdRemove.Enabled = false;
            this.btSdRemove.Location = new System.Drawing.Point(716, 111);
            this.btSdRemove.Name = "btSdRemove";
            this.btSdRemove.Size = new System.Drawing.Size(108, 23);
            this.btSdRemove.TabIndex = 13;
            this.btSdRemove.Text = "Remove Channels";
            this.btSdRemove.UseVisualStyleBackColor = true;
            this.btSdRemove.Click += new System.EventHandler(this.btSdRemove_Click);
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Location = new System.Drawing.Point(27, 161);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(96, 13);
            this.label164.TabIndex = 304;
            this.label164.Text = "Selected Channels";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(27, 31);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(44, 13);
            this.label165.TabIndex = 317;
            this.label165.Text = "Lineups";
            // 
            // btSdAdd
            // 
            this.btSdAdd.Enabled = false;
            this.btSdAdd.Location = new System.Drawing.Point(716, 78);
            this.btSdAdd.Name = "btSdAdd";
            this.btSdAdd.Size = new System.Drawing.Size(108, 23);
            this.btSdAdd.TabIndex = 12;
            this.btSdAdd.Text = "Add Channels";
            this.btSdAdd.UseVisualStyleBackColor = true;
            this.btSdAdd.Click += new System.EventHandler(this.btSdAdd_Click);
            // 
            // lvSdLineups
            // 
            this.lvSdLineups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sdName,
            this.sdLocation,
            this.sdTransport});
            this.lvSdLineups.FullRowSelect = true;
            this.lvSdLineups.GridLines = true;
            this.lvSdLineups.Location = new System.Drawing.Point(30, 50);
            this.lvSdLineups.Name = "lvSdLineups";
            this.lvSdLineups.Size = new System.Drawing.Size(665, 97);
            this.lvSdLineups.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSdLineups.TabIndex = 9;
            this.lvSdLineups.UseCompatibleStateImageBehavior = false;
            this.lvSdLineups.View = System.Windows.Forms.View.Details;
            this.lvSdLineups.SelectedIndexChanged += new System.EventHandler(this.lvSdLineups_SelectedIndexChanged);
            // 
            // sdName
            // 
            this.sdName.Text = "Name";
            this.sdName.Width = 249;
            // 
            // sdLocation
            // 
            this.sdLocation.Text = "Location";
            this.sdLocation.Width = 260;
            // 
            // sdTransport
            // 
            this.sdTransport.Text = "Transport";
            this.sdTransport.Width = 151;
            // 
            // btSdChangeLineups
            // 
            this.btSdChangeLineups.Location = new System.Drawing.Point(716, 49);
            this.btSdChangeLineups.Name = "btSdChangeLineups";
            this.btSdChangeLineups.Size = new System.Drawing.Size(108, 23);
            this.btSdChangeLineups.TabIndex = 11;
            this.btSdChangeLineups.Text = "Change Lineups";
            this.btSdChangeLineups.UseVisualStyleBackColor = true;
            this.btSdChangeLineups.Click += new System.EventHandler(this.btSdChangeLineup_Click);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.cbDvbViewerOutputEnabled);
            this.tabGeneral.Controls.Add(this.cbWmcOutputEnabled);
            this.tabGeneral.Controls.Add(this.cbXmltvOutputEnabled);
            this.tabGeneral.Controls.Add(this.gpXmltvOptions);
            this.tabGeneral.Controls.Add(this.gpDVBViewerOptions);
            this.tabGeneral.Controls.Add(this.gpWMCOptions);
            this.tabGeneral.Controls.Add(this.groupBox4);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(942, 646);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Output";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cbDvbViewerOutputEnabled
            // 
            this.cbDvbViewerOutputEnabled.AutoSize = true;
            this.cbDvbViewerOutputEnabled.Location = new System.Drawing.Point(209, 507);
            this.cbDvbViewerOutputEnabled.Name = "cbDvbViewerOutputEnabled";
            this.cbDvbViewerOutputEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbDvbViewerOutputEnabled.TabIndex = 90;
            this.cbDvbViewerOutputEnabled.Text = "Enabled";
            this.cbDvbViewerOutputEnabled.UseVisualStyleBackColor = true;
            this.cbDvbViewerOutputEnabled.CheckedChanged += new System.EventHandler(this.cbDvbViewerOutputEnabled_CheckedChanged);
            // 
            // cbWmcOutputEnabled
            // 
            this.cbWmcOutputEnabled.AutoSize = true;
            this.cbWmcOutputEnabled.Location = new System.Drawing.Point(209, 379);
            this.cbWmcOutputEnabled.Name = "cbWmcOutputEnabled";
            this.cbWmcOutputEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbWmcOutputEnabled.TabIndex = 70;
            this.cbWmcOutputEnabled.Text = "Enabled";
            this.cbWmcOutputEnabled.UseVisualStyleBackColor = true;
            this.cbWmcOutputEnabled.CheckedChanged += new System.EventHandler(this.cbWmcOutputEnabled_CheckedChanged);
            // 
            // cbXmltvOutputEnabled
            // 
            this.cbXmltvOutputEnabled.AutoSize = true;
            this.cbXmltvOutputEnabled.Location = new System.Drawing.Point(209, 160);
            this.cbXmltvOutputEnabled.Name = "cbXmltvOutputEnabled";
            this.cbXmltvOutputEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbXmltvOutputEnabled.TabIndex = 50;
            this.cbXmltvOutputEnabled.Text = "Enabled";
            this.cbXmltvOutputEnabled.UseVisualStyleBackColor = true;
            this.cbXmltvOutputEnabled.CheckedChanged += new System.EventHandler(this.cbXmltvOutputEnabled_CheckedChanged);
            // 
            // gpXmltvOptions
            // 
            this.gpXmltvOptions.Controls.Add(this.tbXmltvIconTagPathPrefix);
            this.gpXmltvOptions.Controls.Add(this.label150);
            this.gpXmltvOptions.Controls.Add(this.cbCreatePlexEpisodeNumTag);
            this.gpXmltvOptions.Controls.Add(this.cbPrefixSubtitleWithSeasonEpisode);
            this.gpXmltvOptions.Controls.Add(this.cbPrefixDescWithAirDate);
            this.gpXmltvOptions.Controls.Add(this.cbOmitPartNumber);
            this.gpXmltvOptions.Controls.Add(this.tbChannelLogoPath);
            this.gpXmltvOptions.Controls.Add(this.label96);
            this.gpXmltvOptions.Controls.Add(this.btBrowseLogoPath);
            this.gpXmltvOptions.Controls.Add(this.cbElementPerTag);
            this.gpXmltvOptions.Controls.Add(this.cbCreateADTag);
            this.gpXmltvOptions.Controls.Add(this.txtOutputFile);
            this.gpXmltvOptions.Controls.Add(this.lblOutputFile);
            this.gpXmltvOptions.Controls.Add(this.btnBrowse);
            this.gpXmltvOptions.Controls.Add(this.cbUseLCN);
            this.gpXmltvOptions.Controls.Add(this.cboEpisodeTagFormat);
            this.gpXmltvOptions.Controls.Add(this.label119);
            this.gpXmltvOptions.Controls.Add(this.cboChannelIDFormat);
            this.gpXmltvOptions.Controls.Add(this.label118);
            this.gpXmltvOptions.Location = new System.Drawing.Point(15, 162);
            this.gpXmltvOptions.Name = "gpXmltvOptions";
            this.gpXmltvOptions.Size = new System.Drawing.Size(914, 203);
            this.gpXmltvOptions.TabIndex = 49;
            this.gpXmltvOptions.TabStop = false;
            this.gpXmltvOptions.Text = "XMLTV Output";
            // 
            // tbXmltvIconTagPathPrefix
            // 
            this.tbXmltvIconTagPathPrefix.Location = new System.Drawing.Point(148, 114);
            this.tbXmltvIconTagPathPrefix.Name = "tbXmltvIconTagPathPrefix";
            this.tbXmltvIconTagPathPrefix.Size = new System.Drawing.Size(222, 20);
            this.tbXmltvIconTagPathPrefix.TabIndex = 64;
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(20, 117);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(98, 13);
            this.label150.TabIndex = 63;
            this.label150.Text = "Icon tag path prefix";
            // 
            // cbCreatePlexEpisodeNumTag
            // 
            this.cbCreatePlexEpisodeNumTag.AutoSize = true;
            this.cbCreatePlexEpisodeNumTag.Location = new System.Drawing.Point(666, 150);
            this.cbCreatePlexEpisodeNumTag.Name = "cbCreatePlexEpisodeNumTag";
            this.cbCreatePlexEpisodeNumTag.Size = new System.Drawing.Size(157, 17);
            this.cbCreatePlexEpisodeNumTag.TabIndex = 71;
            this.cbCreatePlexEpisodeNumTag.Text = "Create Plex compatible tags";
            this.cbCreatePlexEpisodeNumTag.UseVisualStyleBackColor = true;
            // 
            // cbPrefixSubtitleWithSeasonEpisode
            // 
            this.cbPrefixSubtitleWithSeasonEpisode.AutoSize = true;
            this.cbPrefixSubtitleWithSeasonEpisode.Location = new System.Drawing.Point(666, 120);
            this.cbPrefixSubtitleWithSeasonEpisode.Name = "cbPrefixSubtitleWithSeasonEpisode";
            this.cbPrefixSubtitleWithSeasonEpisode.Size = new System.Drawing.Size(227, 17);
            this.cbPrefixSubtitleWithSeasonEpisode.TabIndex = 70;
            this.cbPrefixSubtitleWithSeasonEpisode.Text = "Prefix subtitle with season/episode number";
            this.cbPrefixSubtitleWithSeasonEpisode.UseVisualStyleBackColor = true;
            // 
            // cbPrefixDescWithAirDate
            // 
            this.cbPrefixDescWithAirDate.AutoSize = true;
            this.cbPrefixDescWithAirDate.Location = new System.Drawing.Point(416, 180);
            this.cbPrefixDescWithAirDate.Name = "cbPrefixDescWithAirDate";
            this.cbPrefixDescWithAirDate.Size = new System.Drawing.Size(209, 17);
            this.cbPrefixDescWithAirDate.TabIndex = 69;
            this.cbPrefixDescWithAirDate.Text = "Prefix description with previous air date";
            this.cbPrefixDescWithAirDate.UseVisualStyleBackColor = true;
            // 
            // cbOmitPartNumber
            // 
            this.cbOmitPartNumber.AutoSize = true;
            this.cbOmitPartNumber.Location = new System.Drawing.Point(416, 150);
            this.cbOmitPartNumber.Name = "cbOmitPartNumber";
            this.cbOmitPartNumber.Size = new System.Drawing.Size(187, 17);
            this.cbOmitPartNumber.TabIndex = 68;
            this.cbOmitPartNumber.Text = "Omit part number from episode tag";
            this.cbOmitPartNumber.UseVisualStyleBackColor = true;
            // 
            // tbChannelLogoPath
            // 
            this.tbChannelLogoPath.Location = new System.Drawing.Point(148, 50);
            this.tbChannelLogoPath.Name = "tbChannelLogoPath";
            this.tbChannelLogoPath.Size = new System.Drawing.Size(635, 20);
            this.tbChannelLogoPath.TabIndex = 55;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(19, 58);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(93, 13);
            this.label96.TabIndex = 54;
            this.label96.Text = "Channel logo path";
            // 
            // btBrowseLogoPath
            // 
            this.btBrowseLogoPath.Location = new System.Drawing.Point(801, 48);
            this.btBrowseLogoPath.Name = "btBrowseLogoPath";
            this.btBrowseLogoPath.Size = new System.Drawing.Size(92, 24);
            this.btBrowseLogoPath.TabIndex = 56;
            this.btBrowseLogoPath.Text = "Browse...";
            this.btBrowseLogoPath.UseVisualStyleBackColor = true;
            this.btBrowseLogoPath.Click += new System.EventHandler(this.btBrowseLogoPath_Click);
            // 
            // cbElementPerTag
            // 
            this.cbElementPerTag.AutoSize = true;
            this.cbElementPerTag.Location = new System.Drawing.Point(416, 120);
            this.cbElementPerTag.Name = "cbElementPerTag";
            this.cbElementPerTag.Size = new System.Drawing.Size(225, 17);
            this.cbElementPerTag.TabIndex = 67;
            this.cbElementPerTag.Text = "Output category elements in separate tags";
            this.cbElementPerTag.UseVisualStyleBackColor = true;
            // 
            // cbCreateADTag
            // 
            this.cbCreateADTag.AutoSize = true;
            this.cbCreateADTag.Location = new System.Drawing.Point(21, 180);
            this.cbCreateADTag.Name = "cbCreateADTag";
            this.cbCreateADTag.Size = new System.Drawing.Size(173, 17);
            this.cbCreateADTag.TabIndex = 66;
            this.cbCreateADTag.Text = "Create an audio description tag";
            this.cbCreateADTag.UseVisualStyleBackColor = true;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(148, 22);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(635, 20);
            this.txtOutputFile.TabIndex = 52;
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(19, 30);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(63, 13);
            this.lblOutputFile.TabIndex = 51;
            this.lblOutputFile.Text = "Output path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(801, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(92, 24);
            this.btnBrowse.TabIndex = 53;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbUseLCN
            // 
            this.cbUseLCN.AutoSize = true;
            this.cbUseLCN.Location = new System.Drawing.Point(21, 150);
            this.cbUseLCN.Name = "cbUseLCN";
            this.cbUseLCN.Size = new System.Drawing.Size(286, 17);
            this.cbUseLCN.TabIndex = 65;
            this.cbUseLCN.Text = "Create an LCN tag containing the user channel number";
            this.cbUseLCN.UseVisualStyleBackColor = true;
            // 
            // cboEpisodeTagFormat
            // 
            this.cboEpisodeTagFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboEpisodeTagFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboEpisodeTagFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEpisodeTagFormat.FormattingEnabled = true;
            this.cboEpisodeTagFormat.Items.AddRange(new object[] {
            "Valid season/episode numbers only",
            "BSEPG compatible",
            "Full content reference ID",
            "Numeric part of content reference ID",
            "VBox compatible",
            "No output"});
            this.cboEpisodeTagFormat.Location = new System.Drawing.Point(561, 81);
            this.cboEpisodeTagFormat.MaxDropDownItems = 20;
            this.cboEpisodeTagFormat.Name = "cboEpisodeTagFormat";
            this.cboEpisodeTagFormat.Size = new System.Drawing.Size(222, 21);
            this.cboEpisodeTagFormat.TabIndex = 62;
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(413, 87);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(109, 13);
            this.label119.TabIndex = 61;
            this.label119.Text = "Format of episode tag";
            // 
            // cboChannelIDFormat
            // 
            this.cboChannelIDFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboChannelIDFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboChannelIDFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChannelIDFormat.FormattingEnabled = true;
            this.cboChannelIDFormat.Items.AddRange(new object[] {
            "Service ID",
            "User channel number",
            "Sequential number",
            "Full channel identification",
            "Channel name"});
            this.cboChannelIDFormat.Location = new System.Drawing.Point(148, 81);
            this.cboChannelIDFormat.MaxDropDownItems = 20;
            this.cboChannelIDFormat.Name = "cboChannelIDFormat";
            this.cboChannelIDFormat.Size = new System.Drawing.Size(222, 21);
            this.cboChannelIDFormat.TabIndex = 60;
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(18, 87);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(124, 13);
            this.label118.TabIndex = 59;
            this.label118.Text = "Format of the channel ID";
            // 
            // gpDVBViewerOptions
            // 
            this.gpDVBViewerOptions.Controls.Add(this.tbDVBViewerIPAddress);
            this.gpDVBViewerOptions.Controls.Add(this.label132);
            this.gpDVBViewerOptions.Controls.Add(this.cbDVBViewerSubtitleVisible);
            this.gpDVBViewerOptions.Controls.Add(this.label56);
            this.gpDVBViewerOptions.Controls.Add(this.nudPort);
            this.gpDVBViewerOptions.Controls.Add(this.cbDVBViewerClear);
            this.gpDVBViewerOptions.Controls.Add(this.cbRecordingServiceImport);
            this.gpDVBViewerOptions.Controls.Add(this.cbUseDVBViewer);
            this.gpDVBViewerOptions.Controls.Add(this.cbDVBViewerImport);
            this.gpDVBViewerOptions.Location = new System.Drawing.Point(15, 508);
            this.gpDVBViewerOptions.Name = "gpDVBViewerOptions";
            this.gpDVBViewerOptions.Size = new System.Drawing.Size(914, 122);
            this.gpDVBViewerOptions.TabIndex = 89;
            this.gpDVBViewerOptions.TabStop = false;
            this.gpDVBViewerOptions.Text = "DVBViewer Output";
            // 
            // tbDVBViewerIPAddress
            // 
            this.tbDVBViewerIPAddress.Location = new System.Drawing.Point(620, 91);
            this.tbDVBViewerIPAddress.Name = "tbDVBViewerIPAddress";
            this.tbDVBViewerIPAddress.Size = new System.Drawing.Size(154, 20);
            this.tbDVBViewerIPAddress.TabIndex = 97;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(518, 94);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(96, 13);
            this.label132.TabIndex = 96;
            this.label132.Text = "Use IP address(es)";
            // 
            // cbDVBViewerSubtitleVisible
            // 
            this.cbDVBViewerSubtitleVisible.AutoSize = true;
            this.cbDVBViewerSubtitleVisible.Enabled = false;
            this.cbDVBViewerSubtitleVisible.Location = new System.Drawing.Point(521, 49);
            this.cbDVBViewerSubtitleVisible.Name = "cbDVBViewerSubtitleVisible";
            this.cbDVBViewerSubtitleVisible.Size = new System.Drawing.Size(195, 17);
            this.cbDVBViewerSubtitleVisible.TabIndex = 94;
            this.cbDVBViewerSubtitleVisible.Text = "Format data so that subtitle is visible";
            this.cbDVBViewerSubtitleVisible.UseVisualStyleBackColor = true;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(780, 94);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(25, 13);
            this.label56.TabIndex = 98;
            this.label56.Text = "port";
            // 
            // nudPort
            // 
            this.nudPort.Enabled = false;
            this.nudPort.Location = new System.Drawing.Point(813, 92);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(80, 20);
            this.nudPort.TabIndex = 99;
            this.nudPort.Value = new decimal(new int[] {
            8089,
            0,
            0,
            0});
            // 
            // cbDVBViewerClear
            // 
            this.cbDVBViewerClear.AutoSize = true;
            this.cbDVBViewerClear.Enabled = false;
            this.cbDVBViewerClear.Location = new System.Drawing.Point(521, 71);
            this.cbDVBViewerClear.Name = "cbDVBViewerClear";
            this.cbDVBViewerClear.Size = new System.Drawing.Size(190, 17);
            this.cbDVBViewerClear.TabIndex = 95;
            this.cbDVBViewerClear.Text = "Clear existing data before importing";
            this.cbDVBViewerClear.UseVisualStyleBackColor = true;
            // 
            // cbRecordingServiceImport
            // 
            this.cbRecordingServiceImport.AutoSize = true;
            this.cbRecordingServiceImport.Location = new System.Drawing.Point(474, 24);
            this.cbRecordingServiceImport.Name = "cbRecordingServiceImport";
            this.cbRecordingServiceImport.Size = new System.Drawing.Size(218, 17);
            this.cbRecordingServiceImport.TabIndex = 93;
            this.cbRecordingServiceImport.Text = "Import the data to the Recording Service";
            this.cbRecordingServiceImport.UseVisualStyleBackColor = true;
            this.cbRecordingServiceImport.CheckedChanged += new System.EventHandler(this.cbRecordingServiceImport_CheckedChanged);
            // 
            // cbUseDVBViewer
            // 
            this.cbUseDVBViewer.AutoSize = true;
            this.cbUseDVBViewer.Location = new System.Drawing.Point(21, 24);
            this.cbUseDVBViewer.Name = "cbUseDVBViewer";
            this.cbUseDVBViewer.Size = new System.Drawing.Size(384, 17);
            this.cbUseDVBViewer.TabIndex = 91;
            this.cbUseDVBViewer.Text = "Create the output file in a format that can be input to DVBViewer using Xepg";
            this.cbUseDVBViewer.UseVisualStyleBackColor = true;
            this.cbUseDVBViewer.CheckedChanged += new System.EventHandler(this.cbUseDVBViewer_CheckedChanged);
            // 
            // cbDVBViewerImport
            // 
            this.cbDVBViewerImport.AutoSize = true;
            this.cbDVBViewerImport.Location = new System.Drawing.Point(21, 51);
            this.cbDVBViewerImport.Name = "cbDVBViewerImport";
            this.cbDVBViewerImport.Size = new System.Drawing.Size(166, 17);
            this.cbDVBViewerImport.TabIndex = 92;
            this.cbDVBViewerImport.Text = "Import the data to DVBViewer";
            this.cbDVBViewerImport.UseVisualStyleBackColor = true;
            this.cbDVBViewerImport.CheckedChanged += new System.EventHandler(this.cbDVBViewerImport_CheckedChanged);
            // 
            // gpWMCOptions
            // 
            this.gpWMCOptions.Controls.Add(this.cbWmcRunTasks);
            this.gpWMCOptions.Controls.Add(this.cbWmcNoDummyAffiliates);
            this.gpWMCOptions.Controls.Add(this.cboWMCSeries);
            this.gpWMCOptions.Controls.Add(this.label30);
            this.gpWMCOptions.Controls.Add(this.cbDisableInbandLoader);
            this.gpWMCOptions.Controls.Add(this.cbWMCFourStarSpecial);
            this.gpWMCOptions.Controls.Add(this.txtImportName);
            this.gpWMCOptions.Controls.Add(this.label27);
            this.gpWMCOptions.Controls.Add(this.cbAutoMapEPG);
            this.gpWMCOptions.Location = new System.Drawing.Point(15, 380);
            this.gpWMCOptions.Name = "gpWMCOptions";
            this.gpWMCOptions.Size = new System.Drawing.Size(914, 107);
            this.gpWMCOptions.TabIndex = 69;
            this.gpWMCOptions.TabStop = false;
            this.gpWMCOptions.Text = "Windows Media Centre Output";
            // 
            // cbWmcRunTasks
            // 
            this.cbWmcRunTasks.AutoSize = true;
            this.cbWmcRunTasks.Location = new System.Drawing.Point(669, 46);
            this.cbWmcRunTasks.Name = "cbWmcRunTasks";
            this.cbWmcRunTasks.Size = new System.Drawing.Size(173, 17);
            this.cbWmcRunTasks.TabIndex = 79;
            this.cbWmcRunTasks.Text = "Run standard tasks after import";
            this.cbWmcRunTasks.UseVisualStyleBackColor = true;
            // 
            // cbWmcNoDummyAffiliates
            // 
            this.cbWmcNoDummyAffiliates.AutoSize = true;
            this.cbWmcNoDummyAffiliates.Location = new System.Drawing.Point(669, 19);
            this.cbWmcNoDummyAffiliates.Name = "cbWmcNoDummyAffiliates";
            this.cbWmcNoDummyAffiliates.Size = new System.Drawing.Size(161, 17);
            this.cbWmcNoDummyAffiliates.TabIndex = 78;
            this.cbWmcNoDummyAffiliates.Text = "Don\'t create dummy affiliates";
            this.cbWmcNoDummyAffiliates.UseVisualStyleBackColor = true;
            // 
            // cboWMCSeries
            // 
            this.cboWMCSeries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboWMCSeries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboWMCSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWMCSeries.FormattingEnabled = true;
            this.cboWMCSeries.Items.AddRange(new object[] {
            "Use programme title without merge",
            "Use programme title with merge",
            "Use broadcaster references"});
            this.cboWMCSeries.Location = new System.Drawing.Point(148, 63);
            this.cboWMCSeries.MaxDropDownItems = 20;
            this.cboWMCSeries.Name = "cboWMCSeries";
            this.cboWMCSeries.Size = new System.Drawing.Size(222, 21);
            this.cboWMCSeries.TabIndex = 74;
            this.cboWMCSeries.SelectedIndexChanged += new System.EventHandler(this.cboWMCSeries_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(18, 66);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(95, 13);
            this.label30.TabIndex = 73;
            this.label30.Text = "Series and repeats";
            // 
            // cbDisableInbandLoader
            // 
            this.cbDisableInbandLoader.AutoSize = true;
            this.cbDisableInbandLoader.Location = new System.Drawing.Point(416, 74);
            this.cbDisableInbandLoader.Name = "cbDisableInbandLoader";
            this.cbDisableInbandLoader.Size = new System.Drawing.Size(160, 17);
            this.cbDisableInbandLoader.TabIndex = 77;
            this.cbDisableInbandLoader.Text = "Disable in-band guide loader";
            this.cbDisableInbandLoader.UseVisualStyleBackColor = true;
            // 
            // cbWMCFourStarSpecial
            // 
            this.cbWMCFourStarSpecial.AutoSize = true;
            this.cbWMCFourStarSpecial.Location = new System.Drawing.Point(416, 46);
            this.cbWMCFourStarSpecial.Name = "cbWMCFourStarSpecial";
            this.cbWMCFourStarSpecial.Size = new System.Drawing.Size(212, 17);
            this.cbWMCFourStarSpecial.TabIndex = 76;
            this.cbWMCFourStarSpecial.Text = "Flag 4 star rated programmes as special";
            this.cbWMCFourStarSpecial.UseVisualStyleBackColor = true;
            // 
            // txtImportName
            // 
            this.txtImportName.Location = new System.Drawing.Point(148, 27);
            this.txtImportName.Name = "txtImportName";
            this.txtImportName.Size = new System.Drawing.Size(222, 20);
            this.txtImportName.TabIndex = 72;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 30);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 13);
            this.label27.TabIndex = 71;
            this.label27.Text = "Import name";
            // 
            // cbAutoMapEPG
            // 
            this.cbAutoMapEPG.AutoSize = true;
            this.cbAutoMapEPG.Location = new System.Drawing.Point(416, 19);
            this.cbAutoMapEPG.Name = "cbAutoMapEPG";
            this.cbAutoMapEPG.Size = new System.Drawing.Size(218, 17);
            this.cbAutoMapEPG.TabIndex = 75;
            this.cbAutoMapEPG.Text = "Automatically map EPG data to channels";
            this.cbAutoMapEPG.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbNoInvalidEntries);
            this.groupBox4.Controls.Add(this.cbCreateSameDataIfNotPresent);
            this.groupBox4.Controls.Add(this.cbNoDataNoFile);
            this.groupBox4.Controls.Add(this.cbAddSeasonEpisodeToDesc);
            this.groupBox4.Controls.Add(this.cbTcRelevantChannels);
            this.groupBox4.Controls.Add(this.cbNoLogExcluded);
            this.groupBox4.Controls.Add(this.cbCreateSameData);
            this.groupBox4.Controls.Add(this.cbRemoveExtractedData);
            this.groupBox4.Controls.Add(this.cbRoundTime);
            this.groupBox4.Controls.Add(this.cbAllowBreaks);
            this.groupBox4.Location = new System.Drawing.Point(15, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(916, 133);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "General Options";
            // 
            // cbNoInvalidEntries
            // 
            this.cbNoInvalidEntries.AutoSize = true;
            this.cbNoInvalidEntries.Location = new System.Drawing.Point(418, 106);
            this.cbNoInvalidEntries.Name = "cbNoInvalidEntries";
            this.cbNoInvalidEntries.Size = new System.Drawing.Size(267, 17);
            this.cbNoInvalidEntries.TabIndex = 31;
            this.cbNoInvalidEntries.Text = "Don\'t create entries with incorrect start or end times";
            this.cbNoInvalidEntries.UseVisualStyleBackColor = true;
            // 
            // cbCreateSameDataIfNotPresent
            // 
            this.cbCreateSameDataIfNotPresent.AutoSize = true;
            this.cbCreateSameDataIfNotPresent.Location = new System.Drawing.Point(23, 45);
            this.cbCreateSameDataIfNotPresent.Name = "cbCreateSameDataIfNotPresent";
            this.cbCreateSameDataIfNotPresent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbCreateSameDataIfNotPresent.Size = new System.Drawing.Size(349, 17);
            this.cbCreateSameDataIfNotPresent.TabIndex = 23;
            this.cbCreateSameDataIfNotPresent.Text = "Create data for channels with the same name only if data not present";
            this.cbCreateSameDataIfNotPresent.UseVisualStyleBackColor = true;
            this.cbCreateSameDataIfNotPresent.CheckedChanged += new System.EventHandler(this.cbCreateSameDataIfNotPresent_CheckedChanged);
            // 
            // cbNoDataNoFile
            // 
            this.cbNoDataNoFile.AutoSize = true;
            this.cbNoDataNoFile.Location = new System.Drawing.Point(418, 85);
            this.cbNoDataNoFile.Name = "cbNoDataNoFile";
            this.cbNoDataNoFile.Size = new System.Drawing.Size(241, 17);
            this.cbNoDataNoFile.TabIndex = 30;
            this.cbNoDataNoFile.Text = "Don\'t create an output file if no data collected";
            this.cbNoDataNoFile.UseVisualStyleBackColor = true;
            // 
            // cbAddSeasonEpisodeToDesc
            // 
            this.cbAddSeasonEpisodeToDesc.AutoSize = true;
            this.cbAddSeasonEpisodeToDesc.Location = new System.Drawing.Point(23, 105);
            this.cbAddSeasonEpisodeToDesc.Name = "cbAddSeasonEpisodeToDesc";
            this.cbAddSeasonEpisodeToDesc.Size = new System.Drawing.Size(251, 17);
            this.cbAddSeasonEpisodeToDesc.TabIndex = 26;
            this.cbAddSeasonEpisodeToDesc.Text = "Append season/episode numbers to description";
            this.cbAddSeasonEpisodeToDesc.UseVisualStyleBackColor = true;
            // 
            // cbTcRelevantChannels
            // 
            this.cbTcRelevantChannels.AutoSize = true;
            this.cbTcRelevantChannels.Location = new System.Drawing.Point(23, 85);
            this.cbTcRelevantChannels.Name = "cbTcRelevantChannels";
            this.cbTcRelevantChannels.Size = new System.Drawing.Size(194, 17);
            this.cbTcRelevantChannels.TabIndex = 25;
            this.cbTcRelevantChannels.Text = "Only output data if channel relevant";
            this.cbTcRelevantChannels.UseVisualStyleBackColor = true;
            // 
            // cbNoLogExcluded
            // 
            this.cbNoLogExcluded.AutoSize = true;
            this.cbNoLogExcluded.Location = new System.Drawing.Point(418, 65);
            this.cbNoLogExcluded.Name = "cbNoLogExcluded";
            this.cbNoLogExcluded.Size = new System.Drawing.Size(160, 17);
            this.cbNoLogExcluded.TabIndex = 29;
            this.cbNoLogExcluded.Text = "Don\'t log excluded channels";
            this.cbNoLogExcluded.UseVisualStyleBackColor = true;
            // 
            // cbCreateSameData
            // 
            this.cbCreateSameData.AutoSize = true;
            this.cbCreateSameData.Location = new System.Drawing.Point(23, 25);
            this.cbCreateSameData.Name = "cbCreateSameData";
            this.cbCreateSameData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbCreateSameData.Size = new System.Drawing.Size(239, 17);
            this.cbCreateSameData.TabIndex = 22;
            this.cbCreateSameData.Text = "Create data for channels with the same name";
            this.cbCreateSameData.UseVisualStyleBackColor = true;
            this.cbCreateSameData.CheckedChanged += new System.EventHandler(this.cbCreateSameData_CheckedChanged);
            // 
            // cbRemoveExtractedData
            // 
            this.cbRemoveExtractedData.AutoSize = true;
            this.cbRemoveExtractedData.Location = new System.Drawing.Point(418, 25);
            this.cbRemoveExtractedData.Name = "cbRemoveExtractedData";
            this.cbRemoveExtractedData.Size = new System.Drawing.Size(287, 17);
            this.cbRemoveExtractedData.TabIndex = 27;
            this.cbRemoveExtractedData.Text = "Don\'t remove extracted data from titles and descriptions";
            this.cbRemoveExtractedData.UseVisualStyleBackColor = true;
            // 
            // cbRoundTime
            // 
            this.cbRoundTime.AutoSize = true;
            this.cbRoundTime.Location = new System.Drawing.Point(23, 65);
            this.cbRoundTime.Name = "cbRoundTime";
            this.cbRoundTime.Size = new System.Drawing.Size(274, 17);
            this.cbRoundTime.TabIndex = 24;
            this.cbRoundTime.Text = "Round the programme times to the nearest 5 minutes";
            this.cbRoundTime.UseVisualStyleBackColor = true;
            // 
            // cbAllowBreaks
            // 
            this.cbAllowBreaks.AutoSize = true;
            this.cbAllowBreaks.Location = new System.Drawing.Point(418, 45);
            this.cbAllowBreaks.Name = "cbAllowBreaks";
            this.cbAllowBreaks.Size = new System.Drawing.Size(224, 17);
            this.cbAllowBreaks.TabIndex = 28;
            this.cbAllowBreaks.Text = "Don\'t log small gaps between programmes";
            this.cbAllowBreaks.UseVisualStyleBackColor = true;
            // 
            // tabFiles
            // 
            this.tabFiles.Controls.Add(this.cbChannelDefinitionFile);
            this.tabFiles.Controls.Add(this.gpChannelDefinitionFile);
            this.tabFiles.Controls.Add(this.cbSatelliteDefFiles);
            this.tabFiles.Controls.Add(this.gpSatelliteDefFiles);
            this.tabFiles.Controls.Add(this.cbSageTVFile);
            this.tabFiles.Controls.Add(this.gpSageTVFile);
            this.tabFiles.Controls.Add(this.cbBladeRunnerFile);
            this.tabFiles.Controls.Add(this.gpBladeRunnerFile);
            this.tabFiles.Controls.Add(this.cbAreaRegionFile);
            this.tabFiles.Controls.Add(this.gpAreaRegionFile);
            this.tabFiles.Location = new System.Drawing.Point(4, 22);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Size = new System.Drawing.Size(942, 646);
            this.tabFiles.TabIndex = 13;
            this.tabFiles.Text = "Files";
            this.tabFiles.UseVisualStyleBackColor = true;
            // 
            // cbChannelDefinitionFile
            // 
            this.cbChannelDefinitionFile.AutoSize = true;
            this.cbChannelDefinitionFile.BackColor = System.Drawing.SystemColors.Control;
            this.cbChannelDefinitionFile.Location = new System.Drawing.Point(196, 499);
            this.cbChannelDefinitionFile.Name = "cbChannelDefinitionFile";
            this.cbChannelDefinitionFile.Size = new System.Drawing.Size(65, 17);
            this.cbChannelDefinitionFile.TabIndex = 35;
            this.cbChannelDefinitionFile.Text = "Enabled";
            this.cbChannelDefinitionFile.UseVisualStyleBackColor = false;
            this.cbChannelDefinitionFile.CheckedChanged += new System.EventHandler(this.cbChannelDefinitionFile_CheckedChanged);
            // 
            // gpChannelDefinitionFile
            // 
            this.gpChannelDefinitionFile.Controls.Add(this.tbChannelDefinitionFileName);
            this.gpChannelDefinitionFile.Controls.Add(this.label116);
            this.gpChannelDefinitionFile.Controls.Add(this.btBrowseChannelDefinitionFile);
            this.gpChannelDefinitionFile.Location = new System.Drawing.Point(21, 500);
            this.gpChannelDefinitionFile.Name = "gpChannelDefinitionFile";
            this.gpChannelDefinitionFile.Size = new System.Drawing.Size(894, 71);
            this.gpChannelDefinitionFile.TabIndex = 34;
            this.gpChannelDefinitionFile.TabStop = false;
            this.gpChannelDefinitionFile.Text = "Channel Definition File";
            // 
            // tbChannelDefinitionFileName
            // 
            this.tbChannelDefinitionFileName.Location = new System.Drawing.Point(82, 32);
            this.tbChannelDefinitionFileName.Name = "tbChannelDefinitionFileName";
            this.tbChannelDefinitionFileName.Size = new System.Drawing.Size(635, 20);
            this.tbChannelDefinitionFileName.TabIndex = 23;
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(21, 36);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(29, 13);
            this.label116.TabIndex = 22;
            this.label116.Text = "Path";
            // 
            // btBrowseChannelDefinitionFile
            // 
            this.btBrowseChannelDefinitionFile.Location = new System.Drawing.Point(732, 29);
            this.btBrowseChannelDefinitionFile.Name = "btBrowseChannelDefinitionFile";
            this.btBrowseChannelDefinitionFile.Size = new System.Drawing.Size(92, 24);
            this.btBrowseChannelDefinitionFile.TabIndex = 24;
            this.btBrowseChannelDefinitionFile.Text = "Browse...";
            this.btBrowseChannelDefinitionFile.UseVisualStyleBackColor = true;
            this.btBrowseChannelDefinitionFile.Click += new System.EventHandler(this.btBrowseChannelDefinitionFile_Click);
            // 
            // cbSatelliteDefFiles
            // 
            this.cbSatelliteDefFiles.AutoSize = true;
            this.cbSatelliteDefFiles.BackColor = System.Drawing.SystemColors.Control;
            this.cbSatelliteDefFiles.Location = new System.Drawing.Point(197, 390);
            this.cbSatelliteDefFiles.Name = "cbSatelliteDefFiles";
            this.cbSatelliteDefFiles.Size = new System.Drawing.Size(65, 17);
            this.cbSatelliteDefFiles.TabIndex = 33;
            this.cbSatelliteDefFiles.Text = "Enabled";
            this.cbSatelliteDefFiles.UseVisualStyleBackColor = false;
            this.cbSatelliteDefFiles.CheckedChanged += new System.EventHandler(this.cbSatelliteDefFiles_CheckedChanged);
            // 
            // gpSatelliteDefFiles
            // 
            this.gpSatelliteDefFiles.Controls.Add(this.tbSatelliteDefDirectoryName);
            this.gpSatelliteDefFiles.Controls.Add(this.label130);
            this.gpSatelliteDefFiles.Controls.Add(this.btBrowseSatelliteDefDirectory);
            this.gpSatelliteDefFiles.Location = new System.Drawing.Point(22, 391);
            this.gpSatelliteDefFiles.Name = "gpSatelliteDefFiles";
            this.gpSatelliteDefFiles.Size = new System.Drawing.Size(894, 86);
            this.gpSatelliteDefFiles.TabIndex = 32;
            this.gpSatelliteDefFiles.TabStop = false;
            this.gpSatelliteDefFiles.Text = "Satellite Definition Files";
            // 
            // tbSatelliteDefDirectoryName
            // 
            this.tbSatelliteDefDirectoryName.Location = new System.Drawing.Point(82, 31);
            this.tbSatelliteDefDirectoryName.Name = "tbSatelliteDefDirectoryName";
            this.tbSatelliteDefDirectoryName.Size = new System.Drawing.Size(635, 20);
            this.tbSatelliteDefDirectoryName.TabIndex = 33;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(21, 35);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(49, 13);
            this.label130.TabIndex = 32;
            this.label130.Text = "Directory";
            // 
            // btBrowseSatelliteDefDirectory
            // 
            this.btBrowseSatelliteDefDirectory.Location = new System.Drawing.Point(732, 28);
            this.btBrowseSatelliteDefDirectory.Name = "btBrowseSatelliteDefDirectory";
            this.btBrowseSatelliteDefDirectory.Size = new System.Drawing.Size(92, 24);
            this.btBrowseSatelliteDefDirectory.TabIndex = 34;
            this.btBrowseSatelliteDefDirectory.Text = "Browse...";
            this.btBrowseSatelliteDefDirectory.UseVisualStyleBackColor = true;
            this.btBrowseSatelliteDefDirectory.Click += new System.EventHandler(this.btBrowseSatelliteDefDirectory_Click);
            // 
            // cbSageTVFile
            // 
            this.cbSageTVFile.AutoSize = true;
            this.cbSageTVFile.BackColor = System.Drawing.SystemColors.Control;
            this.cbSageTVFile.Location = new System.Drawing.Point(195, 218);
            this.cbSageTVFile.Name = "cbSageTVFile";
            this.cbSageTVFile.Size = new System.Drawing.Size(65, 17);
            this.cbSageTVFile.TabIndex = 31;
            this.cbSageTVFile.Text = "Enabled";
            this.cbSageTVFile.UseVisualStyleBackColor = false;
            this.cbSageTVFile.CheckedChanged += new System.EventHandler(this.cbSageTVFile_CheckedChanged);
            // 
            // gpSageTVFile
            // 
            this.gpSageTVFile.Controls.Add(this.tbSageTVSatelliteNumber);
            this.gpSageTVFile.Controls.Add(this.label139);
            this.gpSageTVFile.Controls.Add(this.label138);
            this.gpSageTVFile.Controls.Add(this.cbSageTVFileNoEPG);
            this.gpSageTVFile.Controls.Add(this.tbSageTVFileName);
            this.gpSageTVFile.Controls.Add(this.label137);
            this.gpSageTVFile.Controls.Add(this.btBrowseSageTVFile);
            this.gpSageTVFile.Location = new System.Drawing.Point(20, 219);
            this.gpSageTVFile.Name = "gpSageTVFile";
            this.gpSageTVFile.Size = new System.Drawing.Size(894, 147);
            this.gpSageTVFile.TabIndex = 30;
            this.gpSageTVFile.TabStop = false;
            this.gpSageTVFile.Text = "SageTV Frequency File";
            // 
            // tbSageTVSatelliteNumber
            // 
            this.tbSageTVSatelliteNumber.Location = new System.Drawing.Point(226, 106);
            this.tbSageTVSatelliteNumber.Name = "tbSageTVSatelliteNumber";
            this.tbSageTVSatelliteNumber.Size = new System.Drawing.Size(50, 20);
            this.tbSageTVSatelliteNumber.TabIndex = 38;
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(21, 109);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(151, 13);
            this.label139.TabIndex = 37;
            this.label139.Text = "Satellite number (multi-sat only)";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(21, 75);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(224, 13);
            this.label138.TabIndex = 36;
            this.label138.Text = "Don\'t create channel if no EPG data collected";
            // 
            // cbSageTVFileNoEPG
            // 
            this.cbSageTVFileNoEPG.AutoSize = true;
            this.cbSageTVFileNoEPG.Location = new System.Drawing.Point(261, 75);
            this.cbSageTVFileNoEPG.Name = "cbSageTVFileNoEPG";
            this.cbSageTVFileNoEPG.Size = new System.Drawing.Size(15, 14);
            this.cbSageTVFileNoEPG.TabIndex = 35;
            this.cbSageTVFileNoEPG.UseVisualStyleBackColor = true;
            // 
            // tbSageTVFileName
            // 
            this.tbSageTVFileName.Location = new System.Drawing.Point(82, 31);
            this.tbSageTVFileName.Name = "tbSageTVFileName";
            this.tbSageTVFileName.Size = new System.Drawing.Size(635, 20);
            this.tbSageTVFileName.TabIndex = 33;
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(21, 35);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(29, 13);
            this.label137.TabIndex = 32;
            this.label137.Text = "Path";
            // 
            // btBrowseSageTVFile
            // 
            this.btBrowseSageTVFile.Location = new System.Drawing.Point(732, 28);
            this.btBrowseSageTVFile.Name = "btBrowseSageTVFile";
            this.btBrowseSageTVFile.Size = new System.Drawing.Size(92, 24);
            this.btBrowseSageTVFile.TabIndex = 34;
            this.btBrowseSageTVFile.Text = "Browse...";
            this.btBrowseSageTVFile.UseVisualStyleBackColor = true;
            this.btBrowseSageTVFile.Click += new System.EventHandler(this.btBrowseSageTVFile_Click);
            // 
            // cbBladeRunnerFile
            // 
            this.cbBladeRunnerFile.AutoSize = true;
            this.cbBladeRunnerFile.BackColor = System.Drawing.SystemColors.Control;
            this.cbBladeRunnerFile.Location = new System.Drawing.Point(195, 120);
            this.cbBladeRunnerFile.Name = "cbBladeRunnerFile";
            this.cbBladeRunnerFile.Size = new System.Drawing.Size(65, 17);
            this.cbBladeRunnerFile.TabIndex = 21;
            this.cbBladeRunnerFile.Text = "Enabled";
            this.cbBladeRunnerFile.UseVisualStyleBackColor = false;
            this.cbBladeRunnerFile.CheckedChanged += new System.EventHandler(this.cbBladeRunnerFile_CheckedChanged);
            // 
            // gpBladeRunnerFile
            // 
            this.gpBladeRunnerFile.Controls.Add(this.tbBladeRunnerFileName);
            this.gpBladeRunnerFile.Controls.Add(this.label136);
            this.gpBladeRunnerFile.Controls.Add(this.btBrowseBladeRunnerFile);
            this.gpBladeRunnerFile.Location = new System.Drawing.Point(20, 121);
            this.gpBladeRunnerFile.Name = "gpBladeRunnerFile";
            this.gpBladeRunnerFile.Size = new System.Drawing.Size(894, 71);
            this.gpBladeRunnerFile.TabIndex = 20;
            this.gpBladeRunnerFile.TabStop = false;
            this.gpBladeRunnerFile.Text = "BladeRunner Channel File";
            // 
            // tbBladeRunnerFileName
            // 
            this.tbBladeRunnerFileName.Location = new System.Drawing.Point(82, 32);
            this.tbBladeRunnerFileName.Name = "tbBladeRunnerFileName";
            this.tbBladeRunnerFileName.Size = new System.Drawing.Size(635, 20);
            this.tbBladeRunnerFileName.TabIndex = 23;
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(21, 36);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(29, 13);
            this.label136.TabIndex = 22;
            this.label136.Text = "Path";
            // 
            // btBrowseBladeRunnerFile
            // 
            this.btBrowseBladeRunnerFile.Location = new System.Drawing.Point(732, 29);
            this.btBrowseBladeRunnerFile.Name = "btBrowseBladeRunnerFile";
            this.btBrowseBladeRunnerFile.Size = new System.Drawing.Size(92, 24);
            this.btBrowseBladeRunnerFile.TabIndex = 24;
            this.btBrowseBladeRunnerFile.Text = "Browse...";
            this.btBrowseBladeRunnerFile.UseVisualStyleBackColor = true;
            this.btBrowseBladeRunnerFile.Click += new System.EventHandler(this.btBrowseBladeRunnerFile_Click);
            // 
            // cbAreaRegionFile
            // 
            this.cbAreaRegionFile.AutoSize = true;
            this.cbAreaRegionFile.BackColor = System.Drawing.SystemColors.Control;
            this.cbAreaRegionFile.Location = new System.Drawing.Point(195, 23);
            this.cbAreaRegionFile.Name = "cbAreaRegionFile";
            this.cbAreaRegionFile.Size = new System.Drawing.Size(65, 17);
            this.cbAreaRegionFile.TabIndex = 11;
            this.cbAreaRegionFile.Text = "Enabled";
            this.cbAreaRegionFile.UseVisualStyleBackColor = false;
            this.cbAreaRegionFile.CheckedChanged += new System.EventHandler(this.cbAreaRegionFile_CheckedChanged);
            // 
            // gpAreaRegionFile
            // 
            this.gpAreaRegionFile.Controls.Add(this.tbAreaRegionFileName);
            this.gpAreaRegionFile.Controls.Add(this.label135);
            this.gpAreaRegionFile.Controls.Add(this.btBrowseAreaRegionFile);
            this.gpAreaRegionFile.Location = new System.Drawing.Point(20, 25);
            this.gpAreaRegionFile.Name = "gpAreaRegionFile";
            this.gpAreaRegionFile.Size = new System.Drawing.Size(894, 71);
            this.gpAreaRegionFile.TabIndex = 10;
            this.gpAreaRegionFile.TabStop = false;
            this.gpAreaRegionFile.Text = "Area/Region Channel File";
            // 
            // tbAreaRegionFileName
            // 
            this.tbAreaRegionFileName.Location = new System.Drawing.Point(82, 32);
            this.tbAreaRegionFileName.Name = "tbAreaRegionFileName";
            this.tbAreaRegionFileName.Size = new System.Drawing.Size(635, 20);
            this.tbAreaRegionFileName.TabIndex = 13;
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(21, 36);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(29, 13);
            this.label135.TabIndex = 12;
            this.label135.Text = "Path";
            // 
            // btBrowseAreaRegionFile
            // 
            this.btBrowseAreaRegionFile.Location = new System.Drawing.Point(732, 29);
            this.btBrowseAreaRegionFile.Name = "btBrowseAreaRegionFile";
            this.btBrowseAreaRegionFile.Size = new System.Drawing.Size(92, 24);
            this.btBrowseAreaRegionFile.TabIndex = 14;
            this.btBrowseAreaRegionFile.Text = "Browse...";
            this.btBrowseAreaRegionFile.UseVisualStyleBackColor = true;
            this.btBrowseAreaRegionFile.Click += new System.EventHandler(this.btBrowseAreaRegionFile_Click);
            // 
            // tabServices
            // 
            this.tabServices.Controls.Add(this.cbChannelTuningErrors);
            this.tabServices.Controls.Add(this.pbarChannels);
            this.tabServices.Controls.Add(this.lbScanningFrequencies);
            this.tabServices.Controls.Add(this.label34);
            this.tabServices.Controls.Add(this.lblScanning);
            this.tabServices.Controls.Add(this.cmdClearScan);
            this.tabServices.Controls.Add(this.cmdSelectNone);
            this.tabServices.Controls.Add(this.cmdSelectAll);
            this.tabServices.Controls.Add(this.cmdScan);
            this.tabServices.Controls.Add(this.dgServices);
            this.tabServices.Location = new System.Drawing.Point(4, 22);
            this.tabServices.Name = "tabServices";
            this.tabServices.Size = new System.Drawing.Size(942, 646);
            this.tabServices.TabIndex = 4;
            this.tabServices.Text = "Channels";
            this.tabServices.UseVisualStyleBackColor = true;
            // 
            // cbChannelTuningErrors
            // 
            this.cbChannelTuningErrors.AutoSize = true;
            this.cbChannelTuningErrors.Location = new System.Drawing.Point(805, 611);
            this.cbChannelTuningErrors.Name = "cbChannelTuningErrors";
            this.cbChannelTuningErrors.Size = new System.Drawing.Size(117, 17);
            this.cbChannelTuningErrors.TabIndex = 311;
            this.cbChannelTuningErrors.Text = "Ignore tuning errors";
            this.cbChannelTuningErrors.UseVisualStyleBackColor = true;
            // 
            // pbarChannels
            // 
            this.pbarChannels.Enabled = false;
            this.pbarChannels.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.pbarChannels.Location = new System.Drawing.Point(124, 606);
            this.pbarChannels.Maximum = 500;
            this.pbarChannels.Name = "pbarChannels";
            this.pbarChannels.Size = new System.Drawing.Size(270, 14);
            this.pbarChannels.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarChannels.TabIndex = 310;
            this.pbarChannels.Visible = false;
            // 
            // lbScanningFrequencies
            // 
            this.lbScanningFrequencies.BackColor = System.Drawing.SystemColors.Window;
            this.lbScanningFrequencies.FormattingEnabled = true;
            this.lbScanningFrequencies.Location = new System.Drawing.Point(725, 39);
            this.lbScanningFrequencies.Name = "lbScanningFrequencies";
            this.lbScanningFrequencies.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbScanningFrequencies.Size = new System.Drawing.Size(197, 550);
            this.lbScanningFrequencies.TabIndex = 303;
            this.lbScanningFrequencies.TabStop = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(723, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(162, 13);
            this.label34.TabIndex = 301;
            this.label34.Text = "Frequencies that will be scanned";
            // 
            // lblScanning
            // 
            this.lblScanning.Location = new System.Drawing.Point(121, 622);
            this.lblScanning.Name = "lblScanning";
            this.lblScanning.Size = new System.Drawing.Size(264, 18);
            this.lblScanning.TabIndex = 306;
            this.lblScanning.Text = "Scanning";
            this.lblScanning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScanning.Visible = false;
            // 
            // cmdClearScan
            // 
            this.cmdClearScan.Location = new System.Drawing.Point(406, 606);
            this.cmdClearScan.Name = "cmdClearScan";
            this.cmdClearScan.Size = new System.Drawing.Size(88, 25);
            this.cmdClearScan.TabIndex = 307;
            this.cmdClearScan.Text = "Clear";
            this.cmdClearScan.UseVisualStyleBackColor = true;
            this.cmdClearScan.Click += new System.EventHandler(this.cmdClearScan_Click);
            // 
            // cmdSelectNone
            // 
            this.cmdSelectNone.Location = new System.Drawing.Point(607, 606);
            this.cmdSelectNone.Name = "cmdSelectNone";
            this.cmdSelectNone.Size = new System.Drawing.Size(97, 25);
            this.cmdSelectNone.TabIndex = 309;
            this.cmdSelectNone.Text = "Exclude All";
            this.cmdSelectNone.UseVisualStyleBackColor = true;
            this.cmdSelectNone.Click += new System.EventHandler(this.cmdExcludeAll_Click);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Location = new System.Drawing.Point(502, 606);
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(97, 25);
            this.cmdSelectAll.TabIndex = 308;
            this.cmdSelectAll.Text = "Include All";
            this.cmdSelectAll.UseVisualStyleBackColor = true;
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdIncludeAll_Click);
            // 
            // cmdScan
            // 
            this.cmdScan.Location = new System.Drawing.Point(18, 606);
            this.cmdScan.Name = "cmdScan";
            this.cmdScan.Size = new System.Drawing.Size(88, 25);
            this.cmdScan.TabIndex = 304;
            this.cmdScan.Text = "Start Scan";
            this.cmdScan.UseVisualStyleBackColor = true;
            this.cmdScan.Click += new System.EventHandler(this.cmdScan_Click);
            // 
            // dgServices
            // 
            this.dgServices.AllowUserToAddRows = false;
            this.dgServices.AllowUserToDeleteRows = false;
            this.dgServices.AutoGenerateColumns = false;
            this.dgServices.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.originalNetworkIDColumn,
            this.transportStreamIDColumn,
            this.serviceIDColumn,
            this.excludedByUserColumn,
            this.logicalChannelNumberColumn,
            this.newNameColumn});
            this.dgServices.DataSource = this.tvStationBindingSource;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgServices.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgServices.GridColor = System.Drawing.SystemColors.Control;
            this.dgServices.Location = new System.Drawing.Point(18, 19);
            this.dgServices.Name = "dgServices";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgServices.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgServices.RowHeadersVisible = false;
            this.dgServices.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgServices.RowTemplate.Height = 18;
            this.dgServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgServices.Size = new System.Drawing.Size(685, 572);
            this.dgServices.TabIndex = 302;
            this.dgServices.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.onCellFormatting);
            this.dgServices.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgServices_CellValueChanged);
            this.dgServices.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgServices_ColumnHeaderMouseClick);
            this.dgServices.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgServices_CurrentCellDirtyStateChanged);
            this.dgServices.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgServices_EditingControlShowing);
            // 
            // nameColumn
            // 
            this.nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.MaxInputLength = 256;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.nameColumn.Width = 60;
            // 
            // originalNetworkIDColumn
            // 
            this.originalNetworkIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.originalNetworkIDColumn.DataPropertyName = "OriginalNetworkID";
            this.originalNetworkIDColumn.HeaderText = "ONID";
            this.originalNetworkIDColumn.MaxInputLength = 5;
            this.originalNetworkIDColumn.Name = "originalNetworkIDColumn";
            this.originalNetworkIDColumn.ReadOnly = true;
            this.originalNetworkIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.originalNetworkIDColumn.Width = 59;
            // 
            // transportStreamIDColumn
            // 
            this.transportStreamIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.transportStreamIDColumn.DataPropertyName = "TransportStreamID";
            this.transportStreamIDColumn.HeaderText = "TSID";
            this.transportStreamIDColumn.MaxInputLength = 5;
            this.transportStreamIDColumn.Name = "transportStreamIDColumn";
            this.transportStreamIDColumn.ReadOnly = true;
            this.transportStreamIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.transportStreamIDColumn.Width = 57;
            // 
            // serviceIDColumn
            // 
            this.serviceIDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.serviceIDColumn.DataPropertyName = "ServiceID";
            this.serviceIDColumn.HeaderText = "SID";
            this.serviceIDColumn.MaxInputLength = 5;
            this.serviceIDColumn.Name = "serviceIDColumn";
            this.serviceIDColumn.ReadOnly = true;
            this.serviceIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.serviceIDColumn.Width = 50;
            // 
            // excludedByUserColumn
            // 
            this.excludedByUserColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.excludedByUserColumn.DataPropertyName = "ExcludedByUser";
            this.excludedByUserColumn.HeaderText = "Excluded";
            this.excludedByUserColumn.Name = "excludedByUserColumn";
            this.excludedByUserColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.excludedByUserColumn.Width = 76;
            // 
            // logicalChannelNumberColumn
            // 
            this.logicalChannelNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.logicalChannelNumberColumn.DataPropertyName = "DisplayedLogicalChannelNumber";
            this.logicalChannelNumberColumn.HeaderText = "Channel No.";
            this.logicalChannelNumberColumn.MaxInputLength = 5;
            this.logicalChannelNumberColumn.Name = "logicalChannelNumberColumn";
            this.logicalChannelNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.logicalChannelNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.logicalChannelNumberColumn.Width = 91;
            // 
            // newNameColumn
            // 
            this.newNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.newNameColumn.DataPropertyName = "NewName";
            this.newNameColumn.HeaderText = "New Name";
            this.newNameColumn.MaxInputLength = 256;
            this.newNameColumn.Name = "newNameColumn";
            this.newNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tvStationBindingSource
            // 
            this.tvStationBindingSource.DataSource = typeof(DomainObjects.TVStation);
            this.tvStationBindingSource.Sort = "";
            // 
            // tbpFilters
            // 
            this.tbpFilters.Controls.Add(this.groupBox17);
            this.tbpFilters.Controls.Add(this.groupBox16);
            this.tbpFilters.Location = new System.Drawing.Point(4, 22);
            this.tbpFilters.Name = "tbpFilters";
            this.tbpFilters.Size = new System.Drawing.Size(942, 646);
            this.tbpFilters.TabIndex = 7;
            this.tbpFilters.Text = "Filters";
            this.tbpFilters.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.tbExcludedMaxChannel);
            this.groupBox17.Controls.Add(this.label78);
            this.groupBox17.Location = new System.Drawing.Point(27, 537);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(894, 78);
            this.groupBox17.TabIndex = 303;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Miscellaneous";
            // 
            // tbExcludedMaxChannel
            // 
            this.tbExcludedMaxChannel.Location = new System.Drawing.Point(189, 35);
            this.tbExcludedMaxChannel.Name = "tbExcludedMaxChannel";
            this.tbExcludedMaxChannel.Size = new System.Drawing.Size(100, 20);
            this.tbExcludedMaxChannel.TabIndex = 1;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(34, 38);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(102, 13);
            this.label78.TabIndex = 0;
            this.label78.Text = "Maximum service ID";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.cboFilterFrequency);
            this.groupBox16.Controls.Add(this.label80);
            this.groupBox16.Controls.Add(this.tbExcludeSIDEnd);
            this.groupBox16.Controls.Add(this.label79);
            this.groupBox16.Controls.Add(this.btExcludeDelete);
            this.groupBox16.Controls.Add(this.lvExcludedIdentifiers);
            this.groupBox16.Controls.Add(this.btExcludeAdd);
            this.groupBox16.Controls.Add(this.tbExcludeSIDStart);
            this.groupBox16.Controls.Add(this.label77);
            this.groupBox16.Controls.Add(this.tbExcludeTSID);
            this.groupBox16.Controls.Add(this.label76);
            this.groupBox16.Controls.Add(this.tbExcludeONID);
            this.groupBox16.Controls.Add(this.label75);
            this.groupBox16.Location = new System.Drawing.Point(27, 17);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(894, 495);
            this.groupBox16.TabIndex = 302;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Included Channels";
            // 
            // cboFilterFrequency
            // 
            this.cboFilterFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterFrequency.FormattingEnabled = true;
            this.cboFilterFrequency.Location = new System.Drawing.Point(189, 46);
            this.cboFilterFrequency.Name = "cboFilterFrequency";
            this.cboFilterFrequency.Size = new System.Drawing.Size(100, 21);
            this.cboFilterFrequency.TabIndex = 1;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(34, 53);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(57, 13);
            this.label80.TabIndex = 0;
            this.label80.Text = "Frequency";
            // 
            // tbExcludeSIDEnd
            // 
            this.tbExcludeSIDEnd.Location = new System.Drawing.Point(189, 194);
            this.tbExcludeSIDEnd.Name = "tbExcludeSIDEnd";
            this.tbExcludeSIDEnd.Size = new System.Drawing.Size(100, 20);
            this.tbExcludeSIDEnd.TabIndex = 9;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(34, 197);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(79, 13);
            this.label79.TabIndex = 8;
            this.label79.Text = "End Service ID";
            // 
            // btExcludeDelete
            // 
            this.btExcludeDelete.Enabled = false;
            this.btExcludeDelete.Location = new System.Drawing.Point(796, 458);
            this.btExcludeDelete.Name = "btExcludeDelete";
            this.btExcludeDelete.Size = new System.Drawing.Size(75, 23);
            this.btExcludeDelete.TabIndex = 12;
            this.btExcludeDelete.Text = "Delete";
            this.btExcludeDelete.UseVisualStyleBackColor = true;
            this.btExcludeDelete.Click += new System.EventHandler(this.btExcludeDelete_Click);
            // 
            // lvExcludedIdentifiers
            // 
            this.lvExcludedIdentifiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvExcludedIdentifiers.FullRowSelect = true;
            this.lvExcludedIdentifiers.GridLines = true;
            this.lvExcludedIdentifiers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvExcludedIdentifiers.Location = new System.Drawing.Point(311, 28);
            this.lvExcludedIdentifiers.Name = "lvExcludedIdentifiers";
            this.lvExcludedIdentifiers.Size = new System.Drawing.Size(560, 410);
            this.lvExcludedIdentifiers.TabIndex = 11;
            this.lvExcludedIdentifiers.UseCompatibleStateImageBehavior = false;
            this.lvExcludedIdentifiers.View = System.Windows.Forms.View.Details;
            this.lvExcludedIdentifiers.SelectedIndexChanged += new System.EventHandler(this.lvExcludedIdentifiers_SelectedIndexChanged);
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Frequency";
            this.columnHeader17.Width = 107;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Original Network ID";
            this.columnHeader4.Width = 112;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Transport Stream ID";
            this.columnHeader5.Width = 117;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Start Service ID";
            this.columnHeader6.Width = 116;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "End Service ID";
            this.columnHeader7.Width = 104;
            // 
            // btExcludeAdd
            // 
            this.btExcludeAdd.Location = new System.Drawing.Point(214, 234);
            this.btExcludeAdd.Name = "btExcludeAdd";
            this.btExcludeAdd.Size = new System.Drawing.Size(75, 23);
            this.btExcludeAdd.TabIndex = 10;
            this.btExcludeAdd.Text = "Add";
            this.btExcludeAdd.UseVisualStyleBackColor = true;
            this.btExcludeAdd.Click += new System.EventHandler(this.btExcludeAdd_Click);
            // 
            // tbExcludeSIDStart
            // 
            this.tbExcludeSIDStart.Location = new System.Drawing.Point(189, 158);
            this.tbExcludeSIDStart.Name = "tbExcludeSIDStart";
            this.tbExcludeSIDStart.Size = new System.Drawing.Size(100, 20);
            this.tbExcludeSIDStart.TabIndex = 7;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(34, 161);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(82, 13);
            this.label77.TabIndex = 6;
            this.label77.Text = "Start Service ID";
            // 
            // tbExcludeTSID
            // 
            this.tbExcludeTSID.Location = new System.Drawing.Point(189, 122);
            this.tbExcludeTSID.Name = "tbExcludeTSID";
            this.tbExcludeTSID.Size = new System.Drawing.Size(100, 20);
            this.tbExcludeTSID.TabIndex = 5;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(34, 125);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(102, 13);
            this.label76.TabIndex = 4;
            this.label76.Text = "Transport Stream ID";
            // 
            // tbExcludeONID
            // 
            this.tbExcludeONID.Location = new System.Drawing.Point(189, 86);
            this.tbExcludeONID.Name = "tbExcludeONID";
            this.tbExcludeONID.Size = new System.Drawing.Size(100, 20);
            this.tbExcludeONID.TabIndex = 3;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(34, 89);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(99, 13);
            this.label75.TabIndex = 2;
            this.label75.Text = "Original Network ID";
            // 
            // tbpOffsets
            // 
            this.tbpOffsets.Controls.Add(this.cbTimeshiftTuningErrors);
            this.tbpOffsets.Controls.Add(this.lvPlusSelectedChannels);
            this.tbpOffsets.Controls.Add(this.btPlusDelete);
            this.tbpOffsets.Controls.Add(this.panel2);
            this.tbpOffsets.Controls.Add(this.groupBox15);
            this.tbpOffsets.Controls.Add(this.pbarPlusScan);
            this.tbpOffsets.Controls.Add(this.lblPlusScanning);
            this.tbpOffsets.Controls.Add(this.btPlusScan);
            this.tbpOffsets.Location = new System.Drawing.Point(4, 22);
            this.tbpOffsets.Name = "tbpOffsets";
            this.tbpOffsets.Size = new System.Drawing.Size(942, 646);
            this.tbpOffsets.TabIndex = 6;
            this.tbpOffsets.Text = "Timeshift";
            this.tbpOffsets.UseVisualStyleBackColor = true;
            // 
            // cbTimeshiftTuningErrors
            // 
            this.cbTimeshiftTuningErrors.AutoSize = true;
            this.cbTimeshiftTuningErrors.Location = new System.Drawing.Point(815, 614);
            this.cbTimeshiftTuningErrors.Name = "cbTimeshiftTuningErrors";
            this.cbTimeshiftTuningErrors.Size = new System.Drawing.Size(117, 17);
            this.cbTimeshiftTuningErrors.TabIndex = 404;
            this.cbTimeshiftTuningErrors.Text = "Ignore tuning errors";
            this.cbTimeshiftTuningErrors.UseVisualStyleBackColor = true;
            // 
            // lvPlusSelectedChannels
            // 
            this.lvPlusSelectedChannels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvPlusSelectedChannels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPlusSelectedChannels.FullRowSelect = true;
            this.lvPlusSelectedChannels.GridLines = true;
            this.lvPlusSelectedChannels.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPlusSelectedChannels.Location = new System.Drawing.Point(27, 449);
            this.lvPlusSelectedChannels.Name = "lvPlusSelectedChannels";
            this.lvPlusSelectedChannels.Size = new System.Drawing.Size(804, 139);
            this.lvPlusSelectedChannels.TabIndex = 325;
            this.lvPlusSelectedChannels.UseCompatibleStateImageBehavior = false;
            this.lvPlusSelectedChannels.View = System.Windows.Forms.View.Details;
            this.lvPlusSelectedChannels.SelectedIndexChanged += new System.EventHandler(this.lvPlusSelectedChannels_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Source Channel";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Destination Channel";
            this.columnHeader2.Width = 348;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Offset (Hours)";
            this.columnHeader3.Width = 102;
            // 
            // btPlusDelete
            // 
            this.btPlusDelete.Enabled = false;
            this.btPlusDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPlusDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btPlusDelete.Location = new System.Drawing.Point(842, 565);
            this.btPlusDelete.Name = "btPlusDelete";
            this.btPlusDelete.Size = new System.Drawing.Size(75, 23);
            this.btPlusDelete.TabIndex = 326;
            this.btPlusDelete.Text = "Delete";
            this.btPlusDelete.UseVisualStyleBackColor = true;
            this.btPlusDelete.Click += new System.EventHandler(this.btPlusDelete_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label70);
            this.panel2.Location = new System.Drawing.Point(15, 418);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(917, 181);
            this.panel2.TabIndex = 403;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(9, 15);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(96, 13);
            this.label70.TabIndex = 0;
            this.label70.Text = "Selected Channels";
            // 
            // groupBox15
            // 
            this.groupBox15.BackColor = System.Drawing.Color.Transparent;
            this.groupBox15.Controls.Add(this.nudPlusIncrement);
            this.groupBox15.Controls.Add(this.label73);
            this.groupBox15.Controls.Add(this.btPlusAdd);
            this.groupBox15.Controls.Add(this.lbPlusDestinationChannel);
            this.groupBox15.Controls.Add(this.label72);
            this.groupBox15.Controls.Add(this.lbPlusSourceChannel);
            this.groupBox15.Controls.Add(this.label71);
            this.groupBox15.Location = new System.Drawing.Point(15, 13);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(917, 392);
            this.groupBox15.TabIndex = 402;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Channels";
            // 
            // nudPlusIncrement
            // 
            this.nudPlusIncrement.Location = new System.Drawing.Point(852, 38);
            this.nudPlusIncrement.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudPlusIncrement.Name = "nudPlusIncrement";
            this.nudPlusIncrement.Size = new System.Drawing.Size(40, 20);
            this.nudPlusIncrement.TabIndex = 323;
            this.nudPlusIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(835, 23);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(70, 13);
            this.label73.TabIndex = 324;
            this.label73.Text = "Offset (hours)";
            // 
            // btPlusAdd
            // 
            this.btPlusAdd.Location = new System.Drawing.Point(827, 357);
            this.btPlusAdd.Name = "btPlusAdd";
            this.btPlusAdd.Size = new System.Drawing.Size(75, 23);
            this.btPlusAdd.TabIndex = 324;
            this.btPlusAdd.Text = "Add";
            this.btPlusAdd.UseVisualStyleBackColor = true;
            this.btPlusAdd.Click += new System.EventHandler(this.btPlusAdd_Click);
            // 
            // lbPlusDestinationChannel
            // 
            this.lbPlusDestinationChannel.FormattingEnabled = true;
            this.lbPlusDestinationChannel.Location = new System.Drawing.Point(426, 38);
            this.lbPlusDestinationChannel.Name = "lbPlusDestinationChannel";
            this.lbPlusDestinationChannel.Size = new System.Drawing.Size(390, 342);
            this.lbPlusDestinationChannel.Sorted = true;
            this.lbPlusDestinationChannel.TabIndex = 322;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(423, 23);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(102, 13);
            this.label72.TabIndex = 321;
            this.label72.Text = "Destination Channel";
            // 
            // lbPlusSourceChannel
            // 
            this.lbPlusSourceChannel.FormattingEnabled = true;
            this.lbPlusSourceChannel.Location = new System.Drawing.Point(12, 38);
            this.lbPlusSourceChannel.Name = "lbPlusSourceChannel";
            this.lbPlusSourceChannel.Size = new System.Drawing.Size(390, 342);
            this.lbPlusSourceChannel.Sorted = true;
            this.lbPlusSourceChannel.TabIndex = 320;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(9, 23);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(83, 13);
            this.label71.TabIndex = 319;
            this.label71.Text = "Source Channel";
            // 
            // pbarPlusScan
            // 
            this.pbarPlusScan.Enabled = false;
            this.pbarPlusScan.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.pbarPlusScan.Location = new System.Drawing.Point(120, 607);
            this.pbarPlusScan.Maximum = 500;
            this.pbarPlusScan.Name = "pbarPlusScan";
            this.pbarPlusScan.Size = new System.Drawing.Size(270, 14);
            this.pbarPlusScan.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarPlusScan.TabIndex = 313;
            this.pbarPlusScan.Visible = false;
            // 
            // lblPlusScanning
            // 
            this.lblPlusScanning.Location = new System.Drawing.Point(117, 623);
            this.lblPlusScanning.Name = "lblPlusScanning";
            this.lblPlusScanning.Size = new System.Drawing.Size(264, 18);
            this.lblPlusScanning.TabIndex = 312;
            this.lblPlusScanning.Text = "Scanning";
            this.lblPlusScanning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlusScanning.Visible = false;
            // 
            // btPlusScan
            // 
            this.btPlusScan.Location = new System.Drawing.Point(14, 609);
            this.btPlusScan.Name = "btPlusScan";
            this.btPlusScan.Size = new System.Drawing.Size(88, 25);
            this.btPlusScan.TabIndex = 327;
            this.btPlusScan.Text = "Start Scan";
            this.btPlusScan.UseVisualStyleBackColor = true;
            this.btPlusScan.Click += new System.EventHandler(this.cmdScan_Click);
            // 
            // tabRepeats
            // 
            this.tabRepeats.Controls.Add(this.gpRepeatExclusions);
            this.tabRepeats.Controls.Add(this.groupBox1);
            this.tabRepeats.Location = new System.Drawing.Point(4, 22);
            this.tabRepeats.Name = "tabRepeats";
            this.tabRepeats.Size = new System.Drawing.Size(942, 646);
            this.tabRepeats.TabIndex = 8;
            this.tabRepeats.Text = "Repeats";
            this.tabRepeats.UseVisualStyleBackColor = true;
            // 
            // gpRepeatExclusions
            // 
            this.gpRepeatExclusions.Controls.Add(this.tbPhrasesToIgnore);
            this.gpRepeatExclusions.Controls.Add(this.label83);
            this.gpRepeatExclusions.Controls.Add(this.btRepeatDelete);
            this.gpRepeatExclusions.Controls.Add(this.lvRepeatPrograms);
            this.gpRepeatExclusions.Controls.Add(this.btRepeatAdd);
            this.gpRepeatExclusions.Controls.Add(this.tbRepeatDescription);
            this.gpRepeatExclusions.Controls.Add(this.label82);
            this.gpRepeatExclusions.Controls.Add(this.tbRepeatTitle);
            this.gpRepeatExclusions.Controls.Add(this.label81);
            this.gpRepeatExclusions.Location = new System.Drawing.Point(32, 143);
            this.gpRepeatExclusions.Name = "gpRepeatExclusions";
            this.gpRepeatExclusions.Size = new System.Drawing.Size(895, 483);
            this.gpRepeatExclusions.TabIndex = 20;
            this.gpRepeatExclusions.TabStop = false;
            this.gpRepeatExclusions.Text = "Exclusions";
            // 
            // tbPhrasesToIgnore
            // 
            this.tbPhrasesToIgnore.Location = new System.Drawing.Point(33, 448);
            this.tbPhrasesToIgnore.Name = "tbPhrasesToIgnore";
            this.tbPhrasesToIgnore.Size = new System.Drawing.Size(756, 20);
            this.tbPhrasesToIgnore.TabIndex = 29;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(29, 431);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(94, 13);
            this.label83.TabIndex = 28;
            this.label83.Text = "Phrases To Ignore";
            // 
            // btRepeatDelete
            // 
            this.btRepeatDelete.Enabled = false;
            this.btRepeatDelete.Location = new System.Drawing.Point(799, 393);
            this.btRepeatDelete.Name = "btRepeatDelete";
            this.btRepeatDelete.Size = new System.Drawing.Size(75, 23);
            this.btRepeatDelete.TabIndex = 27;
            this.btRepeatDelete.Text = "Delete";
            this.btRepeatDelete.UseVisualStyleBackColor = true;
            this.btRepeatDelete.Click += new System.EventHandler(this.btRepeatDelete_Click);
            // 
            // lvRepeatPrograms
            // 
            this.lvRepeatPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9});
            this.lvRepeatPrograms.FullRowSelect = true;
            this.lvRepeatPrograms.GridLines = true;
            this.lvRepeatPrograms.Location = new System.Drawing.Point(32, 70);
            this.lvRepeatPrograms.Name = "lvRepeatPrograms";
            this.lvRepeatPrograms.Size = new System.Drawing.Size(757, 346);
            this.lvRepeatPrograms.TabIndex = 26;
            this.lvRepeatPrograms.UseCompatibleStateImageBehavior = false;
            this.lvRepeatPrograms.View = System.Windows.Forms.View.Details;
            this.lvRepeatPrograms.SelectedIndexChanged += new System.EventHandler(this.lvRepeatPrograms_SelectedIndexChanged);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Title";
            this.columnHeader8.Width = 238;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Description";
            this.columnHeader9.Width = 571;
            // 
            // btRepeatAdd
            // 
            this.btRepeatAdd.Enabled = false;
            this.btRepeatAdd.Location = new System.Drawing.Point(799, 31);
            this.btRepeatAdd.Name = "btRepeatAdd";
            this.btRepeatAdd.Size = new System.Drawing.Size(75, 23);
            this.btRepeatAdd.TabIndex = 25;
            this.btRepeatAdd.Text = "Add";
            this.btRepeatAdd.UseVisualStyleBackColor = true;
            this.btRepeatAdd.Click += new System.EventHandler(this.btRepeatAdd_Click);
            // 
            // tbRepeatDescription
            // 
            this.tbRepeatDescription.Location = new System.Drawing.Point(322, 33);
            this.tbRepeatDescription.Name = "tbRepeatDescription";
            this.tbRepeatDescription.Size = new System.Drawing.Size(467, 20);
            this.tbRepeatDescription.TabIndex = 24;
            this.tbRepeatDescription.TextChanged += new System.EventHandler(this.tbRepeatDescription_TextChanged);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(256, 36);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(60, 13);
            this.label82.TabIndex = 23;
            this.label82.Text = "Description";
            // 
            // tbRepeatTitle
            // 
            this.tbRepeatTitle.Location = new System.Drawing.Point(63, 33);
            this.tbRepeatTitle.Name = "tbRepeatTitle";
            this.tbRepeatTitle.Size = new System.Drawing.Size(177, 20);
            this.tbRepeatTitle.TabIndex = 22;
            this.tbRepeatTitle.TextChanged += new System.EventHandler(this.tbRepeatTitle_TextChanged);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(30, 36);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(27, 13);
            this.label81.TabIndex = 21;
            this.label81.Text = "Title";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbIgnoreWMCRecordings);
            this.groupBox1.Controls.Add(this.cbNoSimulcastRepeats);
            this.groupBox1.Controls.Add(this.cbCheckForRepeats);
            this.groupBox1.Location = new System.Drawing.Point(32, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbIgnoreWMCRecordings
            // 
            this.cbIgnoreWMCRecordings.AutoSize = true;
            this.cbIgnoreWMCRecordings.Location = new System.Drawing.Point(32, 73);
            this.cbIgnoreWMCRecordings.Name = "cbIgnoreWMCRecordings";
            this.cbIgnoreWMCRecordings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbIgnoreWMCRecordings.Size = new System.Drawing.Size(221, 17);
            this.cbIgnoreWMCRecordings.TabIndex = 4;
            this.cbIgnoreWMCRecordings.Text = "Ignore Windows Media Centre recordings";
            this.cbIgnoreWMCRecordings.UseVisualStyleBackColor = true;
            // 
            // cbNoSimulcastRepeats
            // 
            this.cbNoSimulcastRepeats.AutoSize = true;
            this.cbNoSimulcastRepeats.Location = new System.Drawing.Point(32, 50);
            this.cbNoSimulcastRepeats.Name = "cbNoSimulcastRepeats";
            this.cbNoSimulcastRepeats.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbNoSimulcastRepeats.Size = new System.Drawing.Size(215, 17);
            this.cbNoSimulcastRepeats.TabIndex = 3;
            this.cbNoSimulcastRepeats.Text = "Don\'t flag simulcast programs as repeats";
            this.cbNoSimulcastRepeats.UseVisualStyleBackColor = true;
            // 
            // cbCheckForRepeats
            // 
            this.cbCheckForRepeats.AutoSize = true;
            this.cbCheckForRepeats.Location = new System.Drawing.Point(32, 27);
            this.cbCheckForRepeats.Name = "cbCheckForRepeats";
            this.cbCheckForRepeats.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbCheckForRepeats.Size = new System.Drawing.Size(165, 17);
            this.cbCheckForRepeats.TabIndex = 2;
            this.cbCheckForRepeats.Text = "Check for programme repeats";
            this.cbCheckForRepeats.UseVisualStyleBackColor = true;
            this.cbCheckForRepeats.CheckedChanged += new System.EventHandler(this.cbCheckForRepeats_CheckedChanged);
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.panel4);
            this.tabEdit.Controls.Add(this.groupBox7);
            this.tabEdit.Location = new System.Drawing.Point(4, 22);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.Size = new System.Drawing.Size(942, 646);
            this.tabEdit.TabIndex = 12;
            this.tabEdit.Text = "Edit";
            this.tabEdit.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btEditDelete);
            this.panel4.Controls.Add(this.lvEditSpecs);
            this.panel4.Location = new System.Drawing.Point(22, 213);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(901, 417);
            this.panel4.TabIndex = 500;
            // 
            // btEditDelete
            // 
            this.btEditDelete.Enabled = false;
            this.btEditDelete.Location = new System.Drawing.Point(807, 385);
            this.btEditDelete.Name = "btEditDelete";
            this.btEditDelete.Size = new System.Drawing.Size(75, 23);
            this.btEditDelete.TabIndex = 522;
            this.btEditDelete.Text = "Delete";
            this.btEditDelete.UseVisualStyleBackColor = true;
            this.btEditDelete.Click += new System.EventHandler(this.btEditDelete_Click);
            // 
            // lvEditSpecs
            // 
            this.lvEditSpecs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader19});
            this.lvEditSpecs.FullRowSelect = true;
            this.lvEditSpecs.GridLines = true;
            this.lvEditSpecs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvEditSpecs.Location = new System.Drawing.Point(20, 13);
            this.lvEditSpecs.Name = "lvEditSpecs";
            this.lvEditSpecs.Size = new System.Drawing.Size(863, 364);
            this.lvEditSpecs.TabIndex = 501;
            this.lvEditSpecs.UseCompatibleStateImageBehavior = false;
            this.lvEditSpecs.View = System.Windows.Forms.View.Details;
            this.lvEditSpecs.SelectedIndexChanged += new System.EventHandler(this.lvEditSpecs_SelectedIndexChanged);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Text To Edit";
            this.columnHeader13.Width = 247;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Apply To";
            this.columnHeader14.Width = 150;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Location";
            this.columnHeader15.Width = 120;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Replacement Text";
            this.columnHeader16.Width = 217;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Mode";
            this.columnHeader19.Width = 124;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cboEditReplaceMode);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.tbEditReplacementText);
            this.groupBox7.Controls.Add(this.label126);
            this.groupBox7.Controls.Add(this.cboEditLocation);
            this.groupBox7.Controls.Add(this.label127);
            this.groupBox7.Controls.Add(this.btEditAdd);
            this.groupBox7.Controls.Add(this.cboEditApplyTo);
            this.groupBox7.Controls.Add(this.label128);
            this.groupBox7.Controls.Add(this.tbEditText);
            this.groupBox7.Controls.Add(this.label129);
            this.groupBox7.Location = new System.Drawing.Point(22, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(901, 175);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Current entry";
            // 
            // cboEditReplaceMode
            // 
            this.cboEditReplaceMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboEditReplaceMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboEditReplaceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditReplaceMode.FormattingEnabled = true;
            this.cboEditReplaceMode.Items.AddRange(new object[] {
            "Specified text only",
            "Specified text and following text",
            "Specified text and preceeding text",
            "All text"});
            this.cboEditReplaceMode.Location = new System.Drawing.Point(134, 140);
            this.cboEditReplaceMode.MaxDropDownItems = 20;
            this.cboEditReplaceMode.Name = "cboEditReplaceMode";
            this.cboEditReplaceMode.Size = new System.Drawing.Size(199, 21);
            this.cboEditReplaceMode.TabIndex = 428;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 427;
            this.label10.Text = "Replacement mode";
            // 
            // tbEditReplacementText
            // 
            this.tbEditReplacementText.Location = new System.Drawing.Point(134, 109);
            this.tbEditReplacementText.Name = "tbEditReplacementText";
            this.tbEditReplacementText.Size = new System.Drawing.Size(655, 20);
            this.tbEditReplacementText.TabIndex = 426;
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(26, 112);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(90, 13);
            this.label126.TabIndex = 425;
            this.label126.Text = "Replacement text";
            // 
            // cboEditLocation
            // 
            this.cboEditLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboEditLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboEditLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditLocation.FormattingEnabled = true;
            this.cboEditLocation.Items.AddRange(new object[] {
            "Start",
            "End",
            "Anywhere"});
            this.cboEditLocation.Location = new System.Drawing.Point(134, 80);
            this.cboEditLocation.MaxDropDownItems = 20;
            this.cboEditLocation.Name = "cboEditLocation";
            this.cboEditLocation.Size = new System.Drawing.Size(199, 21);
            this.cboEditLocation.TabIndex = 7;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(26, 83);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(48, 13);
            this.label127.TabIndex = 6;
            this.label127.Text = "Location";
            // 
            // btEditAdd
            // 
            this.btEditAdd.Enabled = false;
            this.btEditAdd.Location = new System.Drawing.Point(809, 138);
            this.btEditAdd.Name = "btEditAdd";
            this.btEditAdd.Size = new System.Drawing.Size(75, 23);
            this.btEditAdd.TabIndex = 429;
            this.btEditAdd.Text = "Add";
            this.btEditAdd.UseVisualStyleBackColor = true;
            this.btEditAdd.Click += new System.EventHandler(this.btEditAdd_Click);
            // 
            // cboEditApplyTo
            // 
            this.cboEditApplyTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboEditApplyTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboEditApplyTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditApplyTo.FormattingEnabled = true;
            this.cboEditApplyTo.Items.AddRange(new object[] {
            "Titles",
            "Descriptions",
            "Titles and descriptions"});
            this.cboEditApplyTo.Location = new System.Drawing.Point(134, 52);
            this.cboEditApplyTo.MaxDropDownItems = 20;
            this.cboEditApplyTo.Name = "cboEditApplyTo";
            this.cboEditApplyTo.Size = new System.Drawing.Size(199, 21);
            this.cboEditApplyTo.TabIndex = 5;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(26, 57);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(45, 13);
            this.label128.TabIndex = 4;
            this.label128.Text = "Apply to";
            // 
            // tbEditText
            // 
            this.tbEditText.Location = new System.Drawing.Point(134, 27);
            this.tbEditText.Name = "tbEditText";
            this.tbEditText.Size = new System.Drawing.Size(655, 20);
            this.tbEditText.TabIndex = 2;
            this.tbEditText.TextChanged += new System.EventHandler(this.tbEditText_TextChanged);
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(26, 30);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(79, 13);
            this.label129.TabIndex = 1;
            this.label129.Text = "Text to change";
            // 
            // tabLookups
            // 
            this.tabLookups.Controls.Add(this.cbTVLookupEnabled);
            this.tabLookups.Controls.Add(this.cbMovieLookupEnabled);
            this.tabLookups.Controls.Add(this.gpLookupMisc);
            this.tabLookups.Controls.Add(this.gpTVLookup);
            this.tabLookups.Controls.Add(this.gpMovieLookup);
            this.tabLookups.Location = new System.Drawing.Point(4, 22);
            this.tabLookups.Name = "tabLookups";
            this.tabLookups.Size = new System.Drawing.Size(942, 646);
            this.tabLookups.TabIndex = 9;
            this.tabLookups.Text = "Lookups";
            this.tabLookups.UseVisualStyleBackColor = true;
            // 
            // cbTVLookupEnabled
            // 
            this.cbTVLookupEnabled.AutoSize = true;
            this.cbTVLookupEnabled.BackColor = System.Drawing.SystemColors.Control;
            this.cbTVLookupEnabled.Location = new System.Drawing.Point(150, 199);
            this.cbTVLookupEnabled.Name = "cbTVLookupEnabled";
            this.cbTVLookupEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbTVLookupEnabled.TabIndex = 21;
            this.cbTVLookupEnabled.Text = "Enabled";
            this.cbTVLookupEnabled.UseVisualStyleBackColor = false;
            this.cbTVLookupEnabled.CheckedChanged += new System.EventHandler(this.cbTVLookupEnabled_CheckedChanged);
            // 
            // cbMovieLookupEnabled
            // 
            this.cbMovieLookupEnabled.AutoSize = true;
            this.cbMovieLookupEnabled.BackColor = System.Drawing.SystemColors.Control;
            this.cbMovieLookupEnabled.Location = new System.Drawing.Point(150, 17);
            this.cbMovieLookupEnabled.Name = "cbMovieLookupEnabled";
            this.cbMovieLookupEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbMovieLookupEnabled.TabIndex = 1;
            this.cbMovieLookupEnabled.Text = "Enabled";
            this.cbMovieLookupEnabled.UseVisualStyleBackColor = false;
            this.cbMovieLookupEnabled.CheckedChanged += new System.EventHandler(this.cbMovieLookupEnabled_CheckedChanged);
            // 
            // gpLookupMisc
            // 
            this.gpLookupMisc.Controls.Add(this.label157);
            this.gpLookupMisc.Controls.Add(this.cbLookupOverwrite);
            this.gpLookupMisc.Controls.Add(this.nudLookupMatchThreshold);
            this.gpLookupMisc.Controls.Add(this.label62);
            this.gpLookupMisc.Controls.Add(this.label45);
            this.gpLookupMisc.Controls.Add(this.cbLookupImageNameTitle);
            this.gpLookupMisc.Controls.Add(this.label44);
            this.gpLookupMisc.Controls.Add(this.cbLookupImagesInBase);
            this.gpLookupMisc.Controls.Add(this.udIgnorePhraseSeparator);
            this.gpLookupMisc.Controls.Add(this.label110);
            this.gpLookupMisc.Controls.Add(this.label106);
            this.gpLookupMisc.Controls.Add(this.label105);
            this.gpLookupMisc.Controls.Add(this.label104);
            this.gpLookupMisc.Controls.Add(this.btLookupBaseBrowse);
            this.gpLookupMisc.Controls.Add(this.tbLookupImagePath);
            this.gpLookupMisc.Controls.Add(this.label95);
            this.gpLookupMisc.Controls.Add(this.cbLookupIgnoreCategories);
            this.gpLookupMisc.Controls.Add(this.cbLookupReload);
            this.gpLookupMisc.Controls.Add(this.cbxLookupMatching);
            this.gpLookupMisc.Controls.Add(this.label91);
            this.gpLookupMisc.Controls.Add(this.tbLookupIgnoredPhrases);
            this.gpLookupMisc.Controls.Add(this.label90);
            this.gpLookupMisc.Controls.Add(this.nudLookupErrors);
            this.gpLookupMisc.Controls.Add(this.cbLookupNotFound);
            this.gpLookupMisc.Controls.Add(this.label86);
            this.gpLookupMisc.Controls.Add(this.label85);
            this.gpLookupMisc.Controls.Add(this.nudLookupTime);
            this.gpLookupMisc.Location = new System.Drawing.Point(24, 326);
            this.gpLookupMisc.Name = "gpLookupMisc";
            this.gpLookupMisc.Size = new System.Drawing.Size(894, 309);
            this.gpLookupMisc.TabIndex = 30;
            this.gpLookupMisc.TabStop = false;
            this.gpLookupMisc.Text = "Miscellaneous";
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(25, 278);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(192, 13);
            this.label157.TabIndex = 58;
            this.label157.Text = "Lookup data overwrites broadcast data";
            // 
            // cbLookupOverwrite
            // 
            this.cbLookupOverwrite.AutoSize = true;
            this.cbLookupOverwrite.Location = new System.Drawing.Point(407, 277);
            this.cbLookupOverwrite.Name = "cbLookupOverwrite";
            this.cbLookupOverwrite.Size = new System.Drawing.Size(15, 14);
            this.cbLookupOverwrite.TabIndex = 59;
            this.cbLookupOverwrite.UseVisualStyleBackColor = true;
            // 
            // nudLookupMatchThreshold
            // 
            this.nudLookupMatchThreshold.Location = new System.Drawing.Point(703, 95);
            this.nudLookupMatchThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLookupMatchThreshold.Name = "nudLookupMatchThreshold";
            this.nudLookupMatchThreshold.ReadOnly = true;
            this.nudLookupMatchThreshold.Size = new System.Drawing.Size(68, 20);
            this.nudLookupMatchThreshold.TabIndex = 40;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(524, 98);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(153, 13);
            this.label62.TabIndex = 39;
            this.label62.Text = "Threshold for nearest matching";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(25, 252);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(168, 13);
            this.label45.TabIndex = 56;
            this.label45.Text = "Name images from programme title";
            // 
            // cbLookupImageNameTitle
            // 
            this.cbLookupImageNameTitle.AutoSize = true;
            this.cbLookupImageNameTitle.Location = new System.Drawing.Point(407, 251);
            this.cbLookupImageNameTitle.Name = "cbLookupImageNameTitle";
            this.cbLookupImageNameTitle.Size = new System.Drawing.Size(15, 14);
            this.cbLookupImageNameTitle.TabIndex = 57;
            this.cbLookupImageNameTitle.UseVisualStyleBackColor = true;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(25, 225);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(148, 13);
            this.label44.TabIndex = 54;
            this.label44.Text = "Store images in base directory";
            // 
            // cbLookupImagesInBase
            // 
            this.cbLookupImagesInBase.AutoSize = true;
            this.cbLookupImagesInBase.Location = new System.Drawing.Point(407, 225);
            this.cbLookupImagesInBase.Name = "cbLookupImagesInBase";
            this.cbLookupImagesInBase.Size = new System.Drawing.Size(15, 14);
            this.cbLookupImagesInBase.TabIndex = 55;
            this.cbLookupImagesInBase.UseVisualStyleBackColor = true;
            // 
            // udIgnorePhraseSeparator
            // 
            this.udIgnorePhraseSeparator.Items.Add(",");
            this.udIgnorePhraseSeparator.Items.Add(";");
            this.udIgnorePhraseSeparator.Items.Add(":");
            this.udIgnorePhraseSeparator.Items.Add("!");
            this.udIgnorePhraseSeparator.Items.Add("$");
            this.udIgnorePhraseSeparator.Items.Add("%");
            this.udIgnorePhraseSeparator.Items.Add("&");
            this.udIgnorePhraseSeparator.Items.Add("*");
            this.udIgnorePhraseSeparator.Items.Add("+");
            this.udIgnorePhraseSeparator.Items.Add("<");
            this.udIgnorePhraseSeparator.Items.Add(">");
            this.udIgnorePhraseSeparator.Items.Add("?");
            this.udIgnorePhraseSeparator.Location = new System.Drawing.Point(833, 121);
            this.udIgnorePhraseSeparator.Name = "udIgnorePhraseSeparator";
            this.udIgnorePhraseSeparator.ReadOnly = true;
            this.udIgnorePhraseSeparator.Size = new System.Drawing.Size(40, 20);
            this.udIgnorePhraseSeparator.TabIndex = 44;
            this.udIgnorePhraseSeparator.Text = "domainUpDown2";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(777, 123);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(53, 13);
            this.label110.TabIndex = 43;
            this.label110.Text = "Separator";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(25, 74);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(171, 13);
            this.label106.TabIndex = 35;
            this.label106.Text = "Ignore categories from broadcaster";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(25, 49);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(281, 13);
            this.label105.TabIndex = 33;
            this.label105.Text = "Always lookup programmes that return no matching entries";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(25, 24);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(101, 13);
            this.label104.TabIndex = 31;
            this.label104.Text = "Reload all metadata";
            // 
            // btLookupBaseBrowse
            // 
            this.btLookupBaseBrowse.Location = new System.Drawing.Point(783, 192);
            this.btLookupBaseBrowse.Name = "btLookupBaseBrowse";
            this.btLookupBaseBrowse.Size = new System.Drawing.Size(92, 24);
            this.btLookupBaseBrowse.TabIndex = 51;
            this.btLookupBaseBrowse.Text = "Browse...";
            this.btLookupBaseBrowse.UseVisualStyleBackColor = true;
            this.btLookupBaseBrowse.Click += new System.EventHandler(this.btLookupBaseBrowse_Click);
            // 
            // tbLookupImagePath
            // 
            this.tbLookupImagePath.Location = new System.Drawing.Point(407, 195);
            this.tbLookupImagePath.Name = "tbLookupImagePath";
            this.tbLookupImagePath.Size = new System.Drawing.Size(364, 20);
            this.tbLookupImagePath.TabIndex = 50;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(25, 198);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(209, 13);
            this.label95.TabIndex = 49;
            this.label95.Text = "Base path for storing movie and TV images";
            // 
            // cbLookupIgnoreCategories
            // 
            this.cbLookupIgnoreCategories.AutoSize = true;
            this.cbLookupIgnoreCategories.Location = new System.Drawing.Point(407, 74);
            this.cbLookupIgnoreCategories.Name = "cbLookupIgnoreCategories";
            this.cbLookupIgnoreCategories.Size = new System.Drawing.Size(15, 14);
            this.cbLookupIgnoreCategories.TabIndex = 36;
            this.cbLookupIgnoreCategories.UseVisualStyleBackColor = true;
            // 
            // cbLookupReload
            // 
            this.cbLookupReload.AutoSize = true;
            this.cbLookupReload.Location = new System.Drawing.Point(407, 24);
            this.cbLookupReload.Name = "cbLookupReload";
            this.cbLookupReload.Size = new System.Drawing.Size(15, 14);
            this.cbLookupReload.TabIndex = 32;
            this.cbLookupReload.UseVisualStyleBackColor = true;
            // 
            // cbxLookupMatching
            // 
            this.cbxLookupMatching.FormattingEnabled = true;
            this.cbxLookupMatching.Items.AddRange(new object[] {
            "Exact",
            "Contains",
            "Nearest"});
            this.cbxLookupMatching.Location = new System.Drawing.Point(407, 95);
            this.cbxLookupMatching.Name = "cbxLookupMatching";
            this.cbxLookupMatching.Size = new System.Drawing.Size(100, 21);
            this.cbxLookupMatching.TabIndex = 38;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(25, 98);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(269, 13);
            this.label91.TabIndex = 37;
            this.label91.Text = "Lookup matching method when multiple results returned";
            // 
            // tbLookupIgnoredPhrases
            // 
            this.tbLookupIgnoredPhrases.Location = new System.Drawing.Point(407, 121);
            this.tbLookupIgnoredPhrases.Name = "tbLookupIgnoredPhrases";
            this.tbLookupIgnoredPhrases.Size = new System.Drawing.Size(364, 20);
            this.tbLookupIgnoredPhrases.TabIndex = 42;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(25, 123);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(185, 13);
            this.label90.TabIndex = 41;
            this.label90.Text = "Phrases to be ignored when matching";
            // 
            // nudLookupErrors
            // 
            this.nudLookupErrors.Location = new System.Drawing.Point(407, 171);
            this.nudLookupErrors.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLookupErrors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLookupErrors.Name = "nudLookupErrors";
            this.nudLookupErrors.Size = new System.Drawing.Size(68, 20);
            this.nudLookupErrors.TabIndex = 48;
            this.nudLookupErrors.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbLookupNotFound
            // 
            this.cbLookupNotFound.AutoSize = true;
            this.cbLookupNotFound.Location = new System.Drawing.Point(407, 49);
            this.cbLookupNotFound.Name = "cbLookupNotFound";
            this.cbLookupNotFound.Size = new System.Drawing.Size(15, 14);
            this.cbLookupNotFound.TabIndex = 34;
            this.cbLookupNotFound.UseVisualStyleBackColor = true;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(25, 173);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(283, 13);
            this.label86.TabIndex = 47;
            this.label86.Text = "Maximum number of consecutive errors before abandoning";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(25, 148);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(261, 13);
            this.label85.TabIndex = 45;
            this.label85.Text = "Maximum time allowed for lookup processing (minutes)";
            // 
            // nudLookupTime
            // 
            this.nudLookupTime.Location = new System.Drawing.Point(407, 146);
            this.nudLookupTime.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudLookupTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLookupTime.Name = "nudLookupTime";
            this.nudLookupTime.Size = new System.Drawing.Size(68, 20);
            this.nudLookupTime.TabIndex = 46;
            this.nudLookupTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // gpTVLookup
            // 
            this.gpTVLookup.Controls.Add(this.cboTVProvider);
            this.gpTVLookup.Controls.Add(this.label134);
            this.gpTVLookup.Controls.Add(this.tbTVDBPin);
            this.gpTVLookup.Controls.Add(this.label125);
            this.gpTVLookup.Controls.Add(this.cboLookupsEpisodeSearchPriority);
            this.gpTVLookup.Controls.Add(this.label74);
            this.gpTVLookup.Controls.Add(this.label107);
            this.gpTVLookup.Controls.Add(this.cboxTVLookupImageType);
            this.gpTVLookup.Controls.Add(this.label89);
            this.gpTVLookup.Controls.Add(this.cbLookupProcessAsTVSeries);
            this.gpTVLookup.Location = new System.Drawing.Point(24, 201);
            this.gpTVLookup.Name = "gpTVLookup";
            this.gpTVLookup.Size = new System.Drawing.Size(894, 107);
            this.gpTVLookup.TabIndex = 20;
            this.gpTVLookup.TabStop = false;
            this.gpTVLookup.Text = "TV Series Lookup";
            // 
            // cboTVProvider
            // 
            this.cboTVProvider.FormattingEnabled = true;
            this.cboTVProvider.Items.AddRange(new object[] {
            "TVDB",
            "TMDB"});
            this.cboTVProvider.Location = new System.Drawing.Point(703, 21);
            this.cboTVProvider.Name = "cboTVProvider";
            this.cboTVProvider.Size = new System.Drawing.Size(68, 21);
            this.cboTVProvider.TabIndex = 31;
            this.cboTVProvider.SelectedIndexChanged += new System.EventHandler(this.cboTVProvider_SelectedIndexChanged);
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(624, 28);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(46, 13);
            this.label134.TabIndex = 30;
            this.label134.Text = "Provider";
            // 
            // tbTVDBPin
            // 
            this.tbTVDBPin.Location = new System.Drawing.Point(703, 72);
            this.tbTVDBPin.Name = "tbTVDBPin";
            this.tbTVDBPin.Size = new System.Drawing.Size(68, 20);
            this.tbTVDBPin.TabIndex = 33;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(624, 75);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(53, 13);
            this.label125.TabIndex = 32;
            this.label125.Text = "TVDB pin";
            // 
            // cboLookupsEpisodeSearchPriority
            // 
            this.cboLookupsEpisodeSearchPriority.FormattingEnabled = true;
            this.cboLookupsEpisodeSearchPriority.Items.AddRange(new object[] {
            "Season/episode numbers",
            "Subtitle"});
            this.cboLookupsEpisodeSearchPriority.Location = new System.Drawing.Point(407, 72);
            this.cboLookupsEpisodeSearchPriority.Name = "cboLookupsEpisodeSearchPriority";
            this.cboLookupsEpisodeSearchPriority.Size = new System.Drawing.Size(161, 21);
            this.cboLookupsEpisodeSearchPriority.TabIndex = 27;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(25, 75);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(113, 13);
            this.label74.TabIndex = 26;
            this.label74.Text = "Episode search priority";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(25, 51);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(288, 13);
            this.label107.TabIndex = 24;
            this.label107.Text = "Process every programme that is not a movie as a TV series";
            // 
            // cboxTVLookupImageType
            // 
            this.cboxTVLookupImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTVLookupImageType.FormattingEnabled = true;
            this.cboxTVLookupImageType.Items.AddRange(new object[] {
            "Poster",
            "Banner",
            "Fanart",
            "Thumbnail poster",
            "Thumbnail fanart",
            "None"});
            this.cboxTVLookupImageType.Location = new System.Drawing.Point(407, 24);
            this.cboxTVLookupImageType.Name = "cboxTVLookupImageType";
            this.cboxTVLookupImageType.Size = new System.Drawing.Size(161, 21);
            this.cboxTVLookupImageType.TabIndex = 23;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(25, 28);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(135, 13);
            this.label89.TabIndex = 22;
            this.label89.Text = "Type of image to download";
            // 
            // cbLookupProcessAsTVSeries
            // 
            this.cbLookupProcessAsTVSeries.AutoSize = true;
            this.cbLookupProcessAsTVSeries.Location = new System.Drawing.Point(407, 52);
            this.cbLookupProcessAsTVSeries.Name = "cbLookupProcessAsTVSeries";
            this.cbLookupProcessAsTVSeries.Size = new System.Drawing.Size(15, 14);
            this.cbLookupProcessAsTVSeries.TabIndex = 25;
            this.cbLookupProcessAsTVSeries.UseVisualStyleBackColor = true;
            // 
            // gpMovieLookup
            // 
            this.gpMovieLookup.Controls.Add(this.btLookupChangeNotMovie);
            this.gpMovieLookup.Controls.Add(this.cboLookupNotMovie);
            this.gpMovieLookup.Controls.Add(this.label43);
            this.gpMovieLookup.Controls.Add(this.udMoviePhraseSeparator);
            this.gpMovieLookup.Controls.Add(this.label109);
            this.gpMovieLookup.Controls.Add(this.nudLookupMovieHighDuration);
            this.gpMovieLookup.Controls.Add(this.label93);
            this.gpMovieLookup.Controls.Add(this.tbLookupMoviePhrases);
            this.gpMovieLookup.Controls.Add(this.label92);
            this.gpMovieLookup.Controls.Add(this.cboxMovieLookupImageType);
            this.gpMovieLookup.Controls.Add(this.label88);
            this.gpMovieLookup.Controls.Add(this.nudLookupMovieLowDuration);
            this.gpMovieLookup.Controls.Add(this.label87);
            this.gpMovieLookup.Location = new System.Drawing.Point(24, 18);
            this.gpMovieLookup.Name = "gpMovieLookup";
            this.gpMovieLookup.Size = new System.Drawing.Size(894, 169);
            this.gpMovieLookup.TabIndex = 0;
            this.gpMovieLookup.TabStop = false;
            this.gpMovieLookup.Text = "Movie Lookup";
            // 
            // btLookupChangeNotMovie
            // 
            this.btLookupChangeNotMovie.Location = new System.Drawing.Point(783, 133);
            this.btLookupChangeNotMovie.Name = "btLookupChangeNotMovie";
            this.btLookupChangeNotMovie.Size = new System.Drawing.Size(92, 24);
            this.btLookupChangeNotMovie.TabIndex = 14;
            this.btLookupChangeNotMovie.Text = "Change...";
            this.btLookupChangeNotMovie.UseVisualStyleBackColor = true;
            this.btLookupChangeNotMovie.Click += new System.EventHandler(this.btLookupChangeNotMovie_Click);
            // 
            // cboLookupNotMovie
            // 
            this.cboLookupNotMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLookupNotMovie.FormattingEnabled = true;
            this.cboLookupNotMovie.Location = new System.Drawing.Point(407, 134);
            this.cboLookupNotMovie.Name = "cboLookupNotMovie";
            this.cboLookupNotMovie.Size = new System.Drawing.Size(364, 21);
            this.cboLookupNotMovie.Sorted = true;
            this.cboLookupNotMovie.TabIndex = 13;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(25, 139);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(158, 13);
            this.label43.TabIndex = 12;
            this.label43.Text = "Programmes that are not movies";
            // 
            // udMoviePhraseSeparator
            // 
            this.udMoviePhraseSeparator.Items.Add(",");
            this.udMoviePhraseSeparator.Items.Add(";");
            this.udMoviePhraseSeparator.Items.Add(":");
            this.udMoviePhraseSeparator.Items.Add("!");
            this.udMoviePhraseSeparator.Items.Add("$");
            this.udMoviePhraseSeparator.Items.Add("%");
            this.udMoviePhraseSeparator.Items.Add("&");
            this.udMoviePhraseSeparator.Items.Add("*");
            this.udMoviePhraseSeparator.Items.Add("+");
            this.udMoviePhraseSeparator.Items.Add("<");
            this.udMoviePhraseSeparator.Items.Add(">");
            this.udMoviePhraseSeparator.Items.Add("?");
            this.udMoviePhraseSeparator.Location = new System.Drawing.Point(833, 107);
            this.udMoviePhraseSeparator.Name = "udMoviePhraseSeparator";
            this.udMoviePhraseSeparator.ReadOnly = true;
            this.udMoviePhraseSeparator.Size = new System.Drawing.Size(40, 20);
            this.udMoviePhraseSeparator.TabIndex = 11;
            this.udMoviePhraseSeparator.Text = "domainUpDown1";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(776, 109);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(53, 13);
            this.label109.TabIndex = 10;
            this.label109.Text = "Separator";
            // 
            // nudLookupMovieHighDuration
            // 
            this.nudLookupMovieHighDuration.Location = new System.Drawing.Point(407, 84);
            this.nudLookupMovieHighDuration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudLookupMovieHighDuration.Name = "nudLookupMovieHighDuration";
            this.nudLookupMovieHighDuration.Size = new System.Drawing.Size(68, 20);
            this.nudLookupMovieHighDuration.TabIndex = 7;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(25, 87);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(335, 13);
            this.label93.TabIndex = 6;
            this.label93.Text = "Maximum duration of a programme to be considered a movie (minutes)";
            // 
            // tbLookupMoviePhrases
            // 
            this.tbLookupMoviePhrases.Location = new System.Drawing.Point(407, 109);
            this.tbLookupMoviePhrases.Name = "tbLookupMoviePhrases";
            this.tbLookupMoviePhrases.Size = new System.Drawing.Size(364, 20);
            this.tbLookupMoviePhrases.TabIndex = 9;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(25, 112);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(159, 13);
            this.label92.TabIndex = 8;
            this.label92.Text = "Phrases used to identify a movie";
            // 
            // cboxMovieLookupImageType
            // 
            this.cboxMovieLookupImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMovieLookupImageType.FormattingEnabled = true;
            this.cboxMovieLookupImageType.Items.AddRange(new object[] {
            "Thumbnail",
            "Poster",
            "None"});
            this.cboxMovieLookupImageType.Location = new System.Drawing.Point(407, 32);
            this.cboxMovieLookupImageType.Name = "cboxMovieLookupImageType";
            this.cboxMovieLookupImageType.Size = new System.Drawing.Size(161, 21);
            this.cboxMovieLookupImageType.TabIndex = 3;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(25, 35);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(135, 13);
            this.label88.TabIndex = 2;
            this.label88.Text = "Type of image to download";
            // 
            // nudLookupMovieLowDuration
            // 
            this.nudLookupMovieLowDuration.Location = new System.Drawing.Point(407, 58);
            this.nudLookupMovieLowDuration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudLookupMovieLowDuration.Name = "nudLookupMovieLowDuration";
            this.nudLookupMovieLowDuration.Size = new System.Drawing.Size(68, 20);
            this.nudLookupMovieLowDuration.TabIndex = 5;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(25, 60);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(332, 13);
            this.label87.TabIndex = 4;
            this.label87.Text = "Minimum duration of a programme to be considered a movie (minutes)";
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.cbDVBLinkUpdateEnabled);
            this.tabUpdate.Controls.Add(this.gpDVBLink);
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Size = new System.Drawing.Size(942, 646);
            this.tabUpdate.TabIndex = 10;
            this.tabUpdate.Text = "Update";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // cbDVBLinkUpdateEnabled
            // 
            this.cbDVBLinkUpdateEnabled.AutoSize = true;
            this.cbDVBLinkUpdateEnabled.BackColor = System.Drawing.SystemColors.Control;
            this.cbDVBLinkUpdateEnabled.Location = new System.Drawing.Point(170, 26);
            this.cbDVBLinkUpdateEnabled.Name = "cbDVBLinkUpdateEnabled";
            this.cbDVBLinkUpdateEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbDVBLinkUpdateEnabled.TabIndex = 3;
            this.cbDVBLinkUpdateEnabled.Text = "Enabled";
            this.cbDVBLinkUpdateEnabled.UseVisualStyleBackColor = false;
            this.cbDVBLinkUpdateEnabled.CheckedChanged += new System.EventHandler(this.cbDVBLinkUpdateEnabled_CheckedChanged);
            // 
            // gpDVBLink
            // 
            this.gpDVBLink.Controls.Add(this.cbAutoExcludeNew);
            this.gpDVBLink.Controls.Add(this.label149);
            this.gpDVBLink.Controls.Add(this.cbUpdateChannelNumbers);
            this.gpDVBLink.Controls.Add(this.label124);
            this.gpDVBLink.Controls.Add(this.cbReloadChannelData);
            this.gpDVBLink.Controls.Add(this.label98);
            this.gpDVBLink.Controls.Add(this.label123);
            this.gpDVBLink.Controls.Add(this.nudEPGScanInterval);
            this.gpDVBLink.Controls.Add(this.label122);
            this.gpDVBLink.Controls.Add(this.cbLogNetworkMap);
            this.gpDVBLink.Controls.Add(this.label121);
            this.gpDVBLink.Controls.Add(this.cbChildLock);
            this.gpDVBLink.Controls.Add(this.cboEPGScanner);
            this.gpDVBLink.Controls.Add(this.label97);
            this.gpDVBLink.Controls.Add(this.cboMergeMethod);
            this.gpDVBLink.Controls.Add(this.label99);
            this.gpDVBLink.Controls.Add(this.label100);
            this.gpDVBLink.Location = new System.Drawing.Point(24, 27);
            this.gpDVBLink.Name = "gpDVBLink";
            this.gpDVBLink.Size = new System.Drawing.Size(894, 274);
            this.gpDVBLink.TabIndex = 2;
            this.gpDVBLink.TabStop = false;
            this.gpDVBLink.Text = "DVBLink Update";
            // 
            // cbAutoExcludeNew
            // 
            this.cbAutoExcludeNew.AutoSize = true;
            this.cbAutoExcludeNew.Location = new System.Drawing.Point(314, 243);
            this.cbAutoExcludeNew.Name = "cbAutoExcludeNew";
            this.cbAutoExcludeNew.Size = new System.Drawing.Size(15, 14);
            this.cbAutoExcludeNew.TabIndex = 22;
            this.cbAutoExcludeNew.UseVisualStyleBackColor = true;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(30, 243);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(178, 13);
            this.label149.TabIndex = 21;
            this.label149.Text = "Automatically exclude new channels";
            // 
            // cbUpdateChannelNumbers
            // 
            this.cbUpdateChannelNumbers.AutoSize = true;
            this.cbUpdateChannelNumbers.Location = new System.Drawing.Point(314, 156);
            this.cbUpdateChannelNumbers.Name = "cbUpdateChannelNumbers";
            this.cbUpdateChannelNumbers.Size = new System.Drawing.Size(15, 14);
            this.cbUpdateChannelNumbers.TabIndex = 16;
            this.cbUpdateChannelNumbers.UseVisualStyleBackColor = true;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(30, 156);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(126, 13);
            this.label124.TabIndex = 15;
            this.label124.Text = "Update channel numbers";
            // 
            // cbReloadChannelData
            // 
            this.cbReloadChannelData.AutoSize = true;
            this.cbReloadChannelData.Location = new System.Drawing.Point(314, 214);
            this.cbReloadChannelData.Name = "cbReloadChannelData";
            this.cbReloadChannelData.Size = new System.Drawing.Size(15, 14);
            this.cbReloadChannelData.TabIndex = 20;
            this.cbReloadChannelData.UseVisualStyleBackColor = true;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(30, 214);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(164, 13);
            this.label98.TabIndex = 19;
            this.label98.Text = "Reload all DVBLink channel data";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(390, 131);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(43, 13);
            this.label123.TabIndex = 12;
            this.label123.Text = "minutes";
            // 
            // nudEPGScanInterval
            // 
            this.nudEPGScanInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudEPGScanInterval.Location = new System.Drawing.Point(314, 129);
            this.nudEPGScanInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudEPGScanInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudEPGScanInterval.Name = "nudEPGScanInterval";
            this.nudEPGScanInterval.Size = new System.Drawing.Size(70, 20);
            this.nudEPGScanInterval.TabIndex = 11;
            this.nudEPGScanInterval.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(30, 127);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(92, 13);
            this.label122.TabIndex = 10;
            this.label122.Text = "EPG scan interval";
            // 
            // cbLogNetworkMap
            // 
            this.cbLogNetworkMap.AutoSize = true;
            this.cbLogNetworkMap.Location = new System.Drawing.Point(314, 185);
            this.cbLogNetworkMap.Name = "cbLogNetworkMap";
            this.cbLogNetworkMap.Size = new System.Drawing.Size(15, 14);
            this.cbLogNetworkMap.TabIndex = 18;
            this.cbLogNetworkMap.UseVisualStyleBackColor = true;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(30, 185);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(220, 13);
            this.label121.TabIndex = 17;
            this.label121.Text = "Log the providers transponders and channels";
            // 
            // cbChildLock
            // 
            this.cbChildLock.AutoSize = true;
            this.cbChildLock.Location = new System.Drawing.Point(314, 103);
            this.cbChildLock.Name = "cbChildLock";
            this.cbChildLock.Size = new System.Drawing.Size(15, 14);
            this.cbChildLock.TabIndex = 9;
            this.cbChildLock.UseVisualStyleBackColor = true;
            // 
            // cboEPGScanner
            // 
            this.cboEPGScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEPGScanner.FormattingEnabled = true;
            this.cboEPGScanner.Items.AddRange(new object[] {
            "None",
            "Default",
            "EPG Collector",
            "EIT Scanner",
            "XMLTV"});
            this.cboEPGScanner.Location = new System.Drawing.Point(314, 70);
            this.cboEPGScanner.Name = "cboEPGScanner";
            this.cboEPGScanner.Size = new System.Drawing.Size(170, 21);
            this.cboEPGScanner.TabIndex = 7;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(30, 98);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(152, 13);
            this.label97.TabIndex = 8;
            this.label97.Text = "Child lock on for new channels";
            // 
            // cboMergeMethod
            // 
            this.cboMergeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMergeMethod.FormattingEnabled = true;
            this.cboMergeMethod.Items.AddRange(new object[] {
            "None",
            "By name",
            "By channel number",
            "By channel name and number"});
            this.cboMergeMethod.Location = new System.Drawing.Point(314, 37);
            this.cboMergeMethod.Name = "cboMergeMethod";
            this.cboMergeMethod.Size = new System.Drawing.Size(170, 21);
            this.cboMergeMethod.TabIndex = 5;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(30, 40);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(200, 13);
            this.label99.TabIndex = 4;
            this.label99.Text = "Channel merge method for new channels";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(30, 69);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(154, 13);
            this.label100.TabIndex = 6;
            this.label100.Text = "EPG scanner for new channels";
            // 
            // tabTranslations
            // 
            this.tabTranslations.Controls.Add(this.cbTranslateEnabled);
            this.tabTranslations.Controls.Add(this.gpTranslate);
            this.tabTranslations.Location = new System.Drawing.Point(4, 22);
            this.tabTranslations.Name = "tabTranslations";
            this.tabTranslations.Size = new System.Drawing.Size(942, 646);
            this.tabTranslations.TabIndex = 14;
            this.tabTranslations.Text = "Translate";
            this.tabTranslations.UseVisualStyleBackColor = true;
            // 
            // cbTranslateEnabled
            // 
            this.cbTranslateEnabled.AutoSize = true;
            this.cbTranslateEnabled.Location = new System.Drawing.Point(142, 23);
            this.cbTranslateEnabled.Name = "cbTranslateEnabled";
            this.cbTranslateEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbTranslateEnabled.TabIndex = 14;
            this.cbTranslateEnabled.Text = "Enabled";
            this.cbTranslateEnabled.UseVisualStyleBackColor = true;
            this.cbTranslateEnabled.CheckedChanged += new System.EventHandler(this.cbTranslateEnabled_CheckedChanged);
            // 
            // gpTranslate
            // 
            this.gpTranslate.Controls.Add(this.lblTranslateScanning);
            this.gpTranslate.Controls.Add(this.pbarTranslateScanning);
            this.gpTranslate.Controls.Add(this.btTranslateRefresh);
            this.gpTranslate.Controls.Add(this.label131);
            this.gpTranslate.Controls.Add(this.btTranslateScan);
            this.gpTranslate.Controls.Add(this.tbTranslateApiKey);
            this.gpTranslate.Controls.Add(this.lbTranslateChannels);
            this.gpTranslate.Controls.Add(this.label155);
            this.gpTranslate.Controls.Add(this.label133);
            this.gpTranslate.Controls.Add(this.label154);
            this.gpTranslate.Controls.Add(this.cboTranslateOutputLanguage);
            this.gpTranslate.Location = new System.Drawing.Point(30, 24);
            this.gpTranslate.Name = "gpTranslate";
            this.gpTranslate.Size = new System.Drawing.Size(884, 598);
            this.gpTranslate.TabIndex = 13;
            this.gpTranslate.TabStop = false;
            this.gpTranslate.Text = "Text Translation";
            // 
            // lblTranslateScanning
            // 
            this.lblTranslateScanning.Location = new System.Drawing.Point(213, 573);
            this.lblTranslateScanning.Name = "lblTranslateScanning";
            this.lblTranslateScanning.Size = new System.Drawing.Size(281, 18);
            this.lblTranslateScanning.TabIndex = 12;
            this.lblTranslateScanning.Text = "Scanning";
            this.lblTranslateScanning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTranslateScanning.Visible = false;
            // 
            // pbarTranslateScanning
            // 
            this.pbarTranslateScanning.Enabled = false;
            this.pbarTranslateScanning.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.pbarTranslateScanning.Location = new System.Drawing.Point(215, 557);
            this.pbarTranslateScanning.Maximum = 500;
            this.pbarTranslateScanning.Name = "pbarTranslateScanning";
            this.pbarTranslateScanning.Size = new System.Drawing.Size(279, 14);
            this.pbarTranslateScanning.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarTranslateScanning.TabIndex = 11;
            this.pbarTranslateScanning.Visible = false;
            // 
            // btTranslateRefresh
            // 
            this.btTranslateRefresh.Location = new System.Drawing.Point(320, 65);
            this.btTranslateRefresh.Name = "btTranslateRefresh";
            this.btTranslateRefresh.Size = new System.Drawing.Size(75, 23);
            this.btTranslateRefresh.TabIndex = 7;
            this.btTranslateRefresh.Text = "Refresh";
            this.btTranslateRefresh.UseVisualStyleBackColor = true;
            this.btTranslateRefresh.Click += new System.EventHandler(this.btTranslateRefresh_Click);
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(20, 38);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(44, 13);
            this.label131.TabIndex = 2;
            this.label131.Text = "API key";
            // 
            // btTranslateScan
            // 
            this.btTranslateScan.Location = new System.Drawing.Point(109, 557);
            this.btTranslateScan.Name = "btTranslateScan";
            this.btTranslateScan.Size = new System.Drawing.Size(88, 25);
            this.btTranslateScan.TabIndex = 10;
            this.btTranslateScan.Text = "Start Scan";
            this.btTranslateScan.UseVisualStyleBackColor = true;
            this.btTranslateScan.Click += new System.EventHandler(this.btTranslateScan_Click);
            // 
            // tbTranslateApiKey
            // 
            this.tbTranslateApiKey.Location = new System.Drawing.Point(112, 35);
            this.tbTranslateApiKey.Name = "tbTranslateApiKey";
            this.tbTranslateApiKey.Size = new System.Drawing.Size(494, 20);
            this.tbTranslateApiKey.TabIndex = 3;
            // 
            // lbTranslateChannels
            // 
            this.lbTranslateChannels.FormattingEnabled = true;
            this.lbTranslateChannels.Location = new System.Drawing.Point(112, 104);
            this.lbTranslateChannels.Name = "lbTranslateChannels";
            this.lbTranslateChannels.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbTranslateChannels.Size = new System.Drawing.Size(384, 433);
            this.lbTranslateChannels.Sorted = true;
            this.lbTranslateChannels.TabIndex = 9;
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(21, 108);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(51, 13);
            this.label155.TabIndex = 8;
            this.label155.Text = "Channels";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(612, 38);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(222, 13);
            this.label133.TabIndex = 4;
            this.label133.Text = "http://translate.yandex.com/developers/keys";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Location = new System.Drawing.Point(20, 73);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(86, 13);
            this.label154.TabIndex = 5;
            this.label154.Text = "Output language";
            // 
            // cboTranslateOutputLanguage
            // 
            this.cboTranslateOutputLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTranslateOutputLanguage.FormattingEnabled = true;
            this.cboTranslateOutputLanguage.Location = new System.Drawing.Point(112, 67);
            this.cboTranslateOutputLanguage.MaxDropDownItems = 20;
            this.cboTranslateOutputLanguage.Name = "cboTranslateOutputLanguage";
            this.cboTranslateOutputLanguage.Size = new System.Drawing.Size(190, 21);
            this.cboTranslateOutputLanguage.Sorted = true;
            this.cboTranslateOutputLanguage.TabIndex = 6;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.cbManualTime);
            this.tabAdvanced.Controls.Add(this.gpTimeAdjustments);
            this.tabAdvanced.Controls.Add(this.groupBox5);
            this.tabAdvanced.Controls.Add(this.gpTimeouts);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(942, 646);
            this.tabAdvanced.TabIndex = 3;
            this.tabAdvanced.Text = "Advanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // cbManualTime
            // 
            this.cbManualTime.AutoSize = true;
            this.cbManualTime.BackColor = System.Drawing.SystemColors.Control;
            this.cbManualTime.Location = new System.Drawing.Point(202, 301);
            this.cbManualTime.Name = "cbManualTime";
            this.cbManualTime.Size = new System.Drawing.Size(65, 17);
            this.cbManualTime.TabIndex = 435;
            this.cbManualTime.Text = "Enabled";
            this.cbManualTime.UseVisualStyleBackColor = false;
            this.cbManualTime.CheckedChanged += new System.EventHandler(this.cbManualTime_CheckedChanged);
            // 
            // gpTimeAdjustments
            // 
            this.gpTimeAdjustments.Controls.Add(this.nudCurrentOffsetMinutes);
            this.gpTimeAdjustments.Controls.Add(this.nudNextOffsetMinutes);
            this.gpTimeAdjustments.Controls.Add(this.label49);
            this.gpTimeAdjustments.Controls.Add(this.nudChangeMinutes);
            this.gpTimeAdjustments.Controls.Add(this.label48);
            this.gpTimeAdjustments.Controls.Add(this.nudChangeHours);
            this.gpTimeAdjustments.Controls.Add(this.label47);
            this.gpTimeAdjustments.Controls.Add(this.tbChangeDate);
            this.gpTimeAdjustments.Controls.Add(this.label42);
            this.gpTimeAdjustments.Controls.Add(this.label41);
            this.gpTimeAdjustments.Controls.Add(this.label40);
            this.gpTimeAdjustments.Controls.Add(this.nudNextOffsetHours);
            this.gpTimeAdjustments.Controls.Add(this.label39);
            this.gpTimeAdjustments.Controls.Add(this.label38);
            this.gpTimeAdjustments.Controls.Add(this.label37);
            this.gpTimeAdjustments.Controls.Add(this.nudCurrentOffsetHours);
            this.gpTimeAdjustments.Controls.Add(this.label35);
            this.gpTimeAdjustments.Location = new System.Drawing.Point(17, 302);
            this.gpTimeAdjustments.Name = "gpTimeAdjustments";
            this.gpTimeAdjustments.Size = new System.Drawing.Size(898, 168);
            this.gpTimeAdjustments.TabIndex = 434;
            this.gpTimeAdjustments.TabStop = false;
            this.gpTimeAdjustments.Text = "Manual Time Adjustments";
            // 
            // nudCurrentOffsetMinutes
            // 
            this.nudCurrentOffsetMinutes.Location = new System.Drawing.Point(223, 30);
            this.nudCurrentOffsetMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudCurrentOffsetMinutes.Name = "nudCurrentOffsetMinutes";
            this.nudCurrentOffsetMinutes.Size = new System.Drawing.Size(40, 20);
            this.nudCurrentOffsetMinutes.TabIndex = 439;
            // 
            // nudNextOffsetMinutes
            // 
            this.nudNextOffsetMinutes.Location = new System.Drawing.Point(223, 60);
            this.nudNextOffsetMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudNextOffsetMinutes.Name = "nudNextOffsetMinutes";
            this.nudNextOffsetMinutes.Size = new System.Drawing.Size(40, 20);
            this.nudNextOffsetMinutes.TabIndex = 444;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(265, 131);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(28, 13);
            this.label49.TabIndex = 452;
            this.label49.Text = "mins";
            // 
            // nudChangeMinutes
            // 
            this.nudChangeMinutes.Location = new System.Drawing.Point(223, 128);
            this.nudChangeMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudChangeMinutes.Name = "nudChangeMinutes";
            this.nudChangeMinutes.Size = new System.Drawing.Size(40, 20);
            this.nudChangeMinutes.TabIndex = 451;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(194, 131);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(21, 13);
            this.label48.TabIndex = 450;
            this.label48.Text = "hrs";
            // 
            // nudChangeHours
            // 
            this.nudChangeHours.Location = new System.Drawing.Point(151, 128);
            this.nudChangeHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudChangeHours.Name = "nudChangeHours";
            this.nudChangeHours.Size = new System.Drawing.Size(40, 20);
            this.nudChangeHours.TabIndex = 449;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(11, 131);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(84, 13);
            this.label47.TabIndex = 448;
            this.label47.Text = "Time Of Change";
            // 
            // tbChangeDate
            // 
            this.tbChangeDate.Location = new System.Drawing.Point(151, 93);
            this.tbChangeDate.Name = "tbChangeDate";
            this.tbChangeDate.Size = new System.Drawing.Size(112, 20);
            this.tbChangeDate.TabIndex = 447;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(11, 96);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(129, 13);
            this.label42.TabIndex = 446;
            this.label42.Text = "Date of Change (ddmmyy)";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(268, 63);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(28, 13);
            this.label41.TabIndex = 445;
            this.label41.Text = "mins";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(193, 63);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(21, 13);
            this.label40.TabIndex = 443;
            this.label40.Text = "hrs";
            // 
            // nudNextOffsetHours
            // 
            this.nudNextOffsetHours.Location = new System.Drawing.Point(152, 61);
            this.nudNextOffsetHours.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.nudNextOffsetHours.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.nudNextOffsetHours.Name = "nudNextOffsetHours";
            this.nudNextOffsetHours.Size = new System.Drawing.Size(40, 20);
            this.nudNextOffsetHours.TabIndex = 442;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(10, 63);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(86, 13);
            this.label39.TabIndex = 441;
            this.label39.Text = "Next Time Offset";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(268, 32);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(28, 13);
            this.label38.TabIndex = 440;
            this.label38.Text = "mins";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(193, 32);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(21, 13);
            this.label37.TabIndex = 438;
            this.label37.Text = "hrs";
            // 
            // nudCurrentOffsetHours
            // 
            this.nudCurrentOffsetHours.Location = new System.Drawing.Point(152, 29);
            this.nudCurrentOffsetHours.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.nudCurrentOffsetHours.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.nudCurrentOffsetHours.Name = "nudCurrentOffsetHours";
            this.nudCurrentOffsetHours.Size = new System.Drawing.Size(40, 20);
            this.nudCurrentOffsetHours.TabIndex = 437;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(10, 31);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(98, 13);
            this.label35.TabIndex = 436;
            this.label35.Text = "Current Time Offset";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbFromService);
            this.groupBox5.Controls.Add(this.cbUseStoredStationInfo);
            this.groupBox5.Controls.Add(this.cbStoreStationInfo);
            this.groupBox5.Location = new System.Drawing.Point(17, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(898, 101);
            this.groupBox5.TabIndex = 400;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Miscellaneous Options";
            // 
            // cbFromService
            // 
            this.cbFromService.AutoSize = true;
            this.cbFromService.Location = new System.Drawing.Point(13, 25);
            this.cbFromService.Name = "cbFromService";
            this.cbFromService.Size = new System.Drawing.Size(291, 17);
            this.cbFromService.TabIndex = 410;
            this.cbFromService.Text = "Don\'t monitor the keyboard during the collection process";
            this.cbFromService.UseVisualStyleBackColor = true;
            // 
            // cbUseStoredStationInfo
            // 
            this.cbUseStoredStationInfo.AutoSize = true;
            this.cbUseStoredStationInfo.Location = new System.Drawing.Point(13, 70);
            this.cbUseStoredStationInfo.Name = "cbUseStoredStationInfo";
            this.cbUseStoredStationInfo.Size = new System.Drawing.Size(307, 17);
            this.cbUseStoredStationInfo.TabIndex = 414;
            this.cbUseStoredStationInfo.Text = "Use retained channel information in place of broadcast data";
            this.cbUseStoredStationInfo.UseVisualStyleBackColor = true;
            this.cbUseStoredStationInfo.CheckedChanged += new System.EventHandler(this.cbUseStoredStationInfo_CheckedChanged);
            // 
            // cbStoreStationInfo
            // 
            this.cbStoreStationInfo.AutoSize = true;
            this.cbStoreStationInfo.Location = new System.Drawing.Point(13, 48);
            this.cbStoreStationInfo.Name = "cbStoreStationInfo";
            this.cbStoreStationInfo.Size = new System.Drawing.Size(274, 17);
            this.cbStoreStationInfo.TabIndex = 413;
            this.cbStoreStationInfo.Text = "Retain channel information for use in later collections";
            this.cbStoreStationInfo.UseVisualStyleBackColor = true;
            this.cbStoreStationInfo.CheckedChanged += new System.EventHandler(this.cbStoreStationInfo_CheckedChanged);
            // 
            // gpTimeouts
            // 
            this.gpTimeouts.Controls.Add(this.nudBufferSize);
            this.gpTimeouts.Controls.Add(this.label103);
            this.gpTimeouts.Controls.Add(this.nudBufferFills);
            this.gpTimeouts.Controls.Add(this.label102);
            this.gpTimeouts.Controls.Add(this.btTimeoutDefaults);
            this.gpTimeouts.Controls.Add(this.nudScanRetries);
            this.gpTimeouts.Controls.Add(this.nudSignalLockTimeout);
            this.gpTimeouts.Controls.Add(this.nudDataCollectionTimeout);
            this.gpTimeouts.Controls.Add(this.label12);
            this.gpTimeouts.Controls.Add(this.label2);
            this.gpTimeouts.Controls.Add(this.label1);
            this.gpTimeouts.Location = new System.Drawing.Point(17, 141);
            this.gpTimeouts.Name = "gpTimeouts";
            this.gpTimeouts.Size = new System.Drawing.Size(898, 132);
            this.gpTimeouts.TabIndex = 404;
            this.gpTimeouts.TabStop = false;
            this.gpTimeouts.Text = "Timeouts/ Retries/Buffers";
            // 
            // nudBufferSize
            // 
            this.nudBufferSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudBufferSize.Location = new System.Drawing.Point(455, 29);
            this.nudBufferSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBufferSize.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudBufferSize.Name = "nudBufferSize";
            this.nudBufferSize.Size = new System.Drawing.Size(48, 20);
            this.nudBufferSize.TabIndex = 412;
            this.nudBufferSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(295, 31);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(83, 13);
            this.label103.TabIndex = 411;
            this.label103.Text = "Buffer Size (MB)";
            // 
            // nudBufferFills
            // 
            this.nudBufferFills.Location = new System.Drawing.Point(455, 57);
            this.nudBufferFills.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBufferFills.Name = "nudBufferFills";
            this.nudBufferFills.Size = new System.Drawing.Size(48, 20);
            this.nudBufferFills.TabIndex = 414;
            this.nudBufferFills.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(295, 60);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(66, 13);
            this.label102.TabIndex = 413;
            this.label102.Text = "Buffer Refills";
            // 
            // btTimeoutDefaults
            // 
            this.btTimeoutDefaults.Location = new System.Drawing.Point(549, 26);
            this.btTimeoutDefaults.Name = "btTimeoutDefaults";
            this.btTimeoutDefaults.Size = new System.Drawing.Size(75, 23);
            this.btTimeoutDefaults.TabIndex = 415;
            this.btTimeoutDefaults.Text = "Defaults";
            this.btTimeoutDefaults.UseVisualStyleBackColor = true;
            this.btTimeoutDefaults.Click += new System.EventHandler(this.btTimeoutDefaults_Click);
            // 
            // nudDataCollectionTimeout
            // 
            this.nudDataCollectionTimeout.Location = new System.Drawing.Point(177, 57);
            this.nudDataCollectionTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDataCollectionTimeout.Name = "nudDataCollectionTimeout";
            this.nudDataCollectionTimeout.Size = new System.Drawing.Size(48, 20);
            this.nudDataCollectionTimeout.TabIndex = 408;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 13);
            this.label12.TabIndex = 409;
            this.label12.Text = "Number of Retries";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 407;
            this.label2.Text = "Data Collection (sec)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 405;
            this.label1.Text = "Signal Lock (sec)";
            // 
            // tabDiagnostics
            // 
            this.tabDiagnostics.Controls.Add(this.groupBox6);
            this.tabDiagnostics.Location = new System.Drawing.Point(4, 22);
            this.tabDiagnostics.Name = "tabDiagnostics";
            this.tabDiagnostics.Size = new System.Drawing.Size(942, 646);
            this.tabDiagnostics.TabIndex = 5;
            this.tabDiagnostics.Text = "Diagnostics";
            this.tabDiagnostics.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tbTraceIDs);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.tbDebugIDs);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(22, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(898, 116);
            this.groupBox6.TabIndex = 500;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Diagnostics";
            // 
            // tbTraceIDs
            // 
            this.tbTraceIDs.Location = new System.Drawing.Point(100, 70);
            this.tbTraceIDs.Name = "tbTraceIDs";
            this.tbTraceIDs.Size = new System.Drawing.Size(776, 20);
            this.tbTraceIDs.TabIndex = 504;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 503;
            this.label5.Text = "Trace ID\'s";
            // 
            // tbDebugIDs
            // 
            this.tbDebugIDs.Location = new System.Drawing.Point(100, 28);
            this.tbDebugIDs.Name = "tbDebugIDs";
            this.tbDebugIDs.Size = new System.Drawing.Size(776, 20);
            this.tbDebugIDs.TabIndex = 502;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 501;
            this.label3.Text = "Debug ID\'s";
            // 
            // logosBindingSource
            // 
            this.logosBindingSource.DataMember = "Logos";
            this.logosBindingSource.DataSource = this.sdBindingSource;
            // 
            // cboFrequencies
            // 
            this.cboFrequencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFrequencies.FormattingEnabled = true;
            this.cboFrequencies.Location = new System.Drawing.Point(154, 371);
            this.cboFrequencies.Name = "cboFrequencies";
            this.cboFrequencies.Size = new System.Drawing.Size(139, 21);
            this.cboFrequencies.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 374);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Scanning Frequency:";
            // 
            // cmdNewOutputFile
            // 
            this.cmdNewOutputFile.Location = new System.Drawing.Point(467, 336);
            this.cmdNewOutputFile.Name = "cmdNewOutputFile";
            this.cmdNewOutputFile.Size = new System.Drawing.Size(61, 20);
            this.cmdNewOutputFile.TabIndex = 37;
            this.cmdNewOutputFile.Text = "Create...";
            this.cmdNewOutputFile.UseVisualStyleBackColor = true;
            // 
            // cboCollectionType
            // 
            this.cboCollectionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCollectionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCollectionType.FormattingEnabled = true;
            this.cboCollectionType.Location = new System.Drawing.Point(395, 371);
            this.cboCollectionType.Name = "cboCollectionType";
            this.cboCollectionType.Size = new System.Drawing.Size(139, 21);
            this.cboCollectionType.TabIndex = 40;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(306, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Collection Type:";
            // 
            // txtOptions
            // 
            this.txtOptions.Enabled = false;
            this.txtOptions.Location = new System.Drawing.Point(154, 407);
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size(380, 20);
            this.txtOptions.TabIndex = 36;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(306, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(255, 25);
            this.button3.TabIndex = 9;
            this.button3.Text = "Apply...";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 410);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Advanced Options (see Tab):";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(158, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(32, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(185, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Area:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Number of Times to Retry:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button3);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Controls.Add(this.comboBox4);
            this.groupBox9.Controls.Add(this.label21);
            this.groupBox9.Controls.Add(this.comboBox5);
            this.groupBox9.Location = new System.Drawing.Point(154, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(380, 92);
            this.groupBox9.TabIndex = 28;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Location";
            // 
            // comboBox4
            // 
            this.comboBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(185, 40);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(188, 21);
            this.comboBox4.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Country:";
            // 
            // comboBox5
            // 
            this.comboBox5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(6, 40);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(173, 21);
            this.comboBox5.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(361, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(31, 20);
            this.textBox2.TabIndex = 3;
            // 
            // rbSatellite
            // 
            this.rbSatellite.AutoSize = true;
            this.rbSatellite.Location = new System.Drawing.Point(6, 52);
            this.rbSatellite.Name = "rbSatellite";
            this.rbSatellite.Size = new System.Drawing.Size(62, 17);
            this.rbSatellite.TabIndex = 0;
            this.rbSatellite.TabStop = true;
            this.rbSatellite.Text = "Satellite";
            this.rbSatellite.UseVisualStyleBackColor = true;
            // 
            // rbDVBT
            // 
            this.rbDVBT.AutoSize = true;
            this.rbDVBT.Location = new System.Drawing.Point(6, 29);
            this.rbDVBT.Name = "rbDVBT";
            this.rbDVBT.Size = new System.Drawing.Size(71, 17);
            this.rbDVBT.TabIndex = 1;
            this.rbDVBT.TabStop = true;
            this.rbDVBT.Text = "Terrestrial";
            this.rbDVBT.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.rbDVBT);
            this.groupBox10.Controls.Add(this.rbSatellite);
            this.groupBox10.Location = new System.Drawing.Point(6, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(142, 92);
            this.groupBox10.TabIndex = 24;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Transmission Type";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 339);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 13);
            this.label22.TabIndex = 25;
            this.label22.Text = "Output File:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(7, 433);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(527, 163);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(78, 336);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(320, 20);
            this.textBox4.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(152, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Max wait for Signal Lock (sec):";
            // 
            // gbSatellite
            // 
            this.gbSatellite.Controls.Add(this.label13);
            this.gbSatellite.Controls.Add(this.label7);
            this.gbSatellite.Controls.Add(this.label14);
            this.gbSatellite.Controls.Add(this.label15);
            this.gbSatellite.Location = new System.Drawing.Point(6, 247);
            this.gbSatellite.Name = "gbSatellite";
            this.gbSatellite.Size = new System.Drawing.Size(528, 74);
            this.gbSatellite.TabIndex = 31;
            this.gbSatellite.TabStop = false;
            this.gbSatellite.Text = "Satellite LNB Parameters";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 13;
            // 
            // cmdOpenINIFile
            // 
            this.cmdOpenINIFile.Enabled = false;
            this.cmdOpenINIFile.Location = new System.Drawing.Point(16, 483);
            this.cmdOpenINIFile.Name = "cmdOpenINIFile";
            this.cmdOpenINIFile.Size = new System.Drawing.Size(83, 25);
            this.cmdOpenINIFile.TabIndex = 34;
            this.cmdOpenINIFile.Text = "Open File...";
            this.cmdOpenINIFile.UseVisualStyleBackColor = true;
            this.cmdOpenINIFile.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.comboBox3);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(6, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 55);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tuner Selection";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(508, 21);
            this.button2.TabIndex = 9;
            this.button2.Text = "Find Tuners...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(137, 19);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(286, 21);
            this.comboBox3.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Tuner for EPG Collector:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(196, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(168, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Max wait for Data Collection (sec):";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox1);
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.textBox2);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.textBox3);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Location = new System.Drawing.Point(7, 165);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(528, 76);
            this.groupBox8.TabIndex = 30;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Timeouts";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(158, 19);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(32, 20);
            this.textBox3.TabIndex = 1;
            // 
            // cmdSaveGeneral
            // 
            this.cmdSaveGeneral.Location = new System.Drawing.Point(445, 483);
            this.cmdSaveGeneral.Name = "cmdSaveGeneral";
            this.cmdSaveGeneral.Size = new System.Drawing.Size(83, 25);
            this.cmdSaveGeneral.TabIndex = 33;
            this.cmdSaveGeneral.Text = "Save";
            this.cmdSaveGeneral.UseVisualStyleBackColor = true;
            this.cmdSaveGeneral.Visible = false;
            // 
            // CollectorParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tbcParameters);
            this.Controls.Add(this.cboFrequencies);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmdNewOutputFile);
            this.Controls.Add(this.cboCollectionType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOptions);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.gbSatellite);
            this.Controls.Add(this.cmdOpenINIFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.cmdSaveGeneral);
            this.Name = "CollectorParametersControl";
            this.Size = new System.Drawing.Size(950, 672);
            ((System.ComponentModel.ISupportInitialize)(this.nudScanRetries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSignalLockTimeout)).EndInit();
            this.tbcParameters.ResumeLayout(false);
            this.tabDVBS.ResumeLayout(false);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabXMLTV.ResumeLayout(false);
            this.tabXMLTV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXmltvChannelChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmltvChannelChangeBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.gpbFiles.ResumeLayout(false);
            this.gpbFiles.PerformLayout();
            this.tabSchedulesDirect.ResumeLayout(false);
            this.tabSchedulesDirect.PerformLayout();
            this.gpSchedulesDirect.ResumeLayout(false);
            this.gpSchedulesDirect.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSdChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdBindingSource)).EndInit();
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.gpXmltvOptions.ResumeLayout(false);
            this.gpXmltvOptions.PerformLayout();
            this.gpDVBViewerOptions.ResumeLayout(false);
            this.gpDVBViewerOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.gpWMCOptions.ResumeLayout(false);
            this.gpWMCOptions.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabFiles.ResumeLayout(false);
            this.tabFiles.PerformLayout();
            this.gpChannelDefinitionFile.ResumeLayout(false);
            this.gpChannelDefinitionFile.PerformLayout();
            this.gpSatelliteDefFiles.ResumeLayout(false);
            this.gpSatelliteDefFiles.PerformLayout();
            this.gpSageTVFile.ResumeLayout(false);
            this.gpSageTVFile.PerformLayout();
            this.gpBladeRunnerFile.ResumeLayout(false);
            this.gpBladeRunnerFile.PerformLayout();
            this.gpAreaRegionFile.ResumeLayout(false);
            this.gpAreaRegionFile.PerformLayout();
            this.tabServices.ResumeLayout(false);
            this.tabServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvStationBindingSource)).EndInit();
            this.tbpFilters.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tbpOffsets.ResumeLayout(false);
            this.tbpOffsets.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlusIncrement)).EndInit();
            this.tabRepeats.ResumeLayout(false);
            this.gpRepeatExclusions.ResumeLayout(false);
            this.gpRepeatExclusions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabEdit.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabLookups.ResumeLayout(false);
            this.tabLookups.PerformLayout();
            this.gpLookupMisc.ResumeLayout(false);
            this.gpLookupMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMatchThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupTime)).EndInit();
            this.gpTVLookup.ResumeLayout(false);
            this.gpTVLookup.PerformLayout();
            this.gpMovieLookup.ResumeLayout(false);
            this.gpMovieLookup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMovieHighDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLookupMovieLowDuration)).EndInit();
            this.tabUpdate.ResumeLayout(false);
            this.tabUpdate.PerformLayout();
            this.gpDVBLink.ResumeLayout(false);
            this.gpDVBLink.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEPGScanInterval)).EndInit();
            this.tabTranslations.ResumeLayout(false);
            this.tabTranslations.PerformLayout();
            this.gpTranslate.ResumeLayout(false);
            this.gpTranslate.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.gpTimeAdjustments.ResumeLayout(false);
            this.gpTimeAdjustments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentOffsetMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNextOffsetMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNextOffsetHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCurrentOffsetHours)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gpTimeouts.ResumeLayout(false);
            this.gpTimeouts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferFills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataCollectionTimeout)).EndInit();
            this.tabDiagnostics.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logosBindingSource)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.gbSatellite.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudScanRetries;
        private System.Windows.Forms.NumericUpDown nudSignalLockTimeout;
        private System.Windows.Forms.TabControl tbcParameters;
        private System.Windows.Forms.TabPage tabDVBS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbSatelliteTuners;
        private System.Windows.Forms.ListView lvSelectedFrequencies;
        private System.Windows.Forms.ColumnHeader Frequency;
        private System.Windows.Forms.ColumnHeader Provider;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Collection;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbRoundTime;
        private System.Windows.Forms.CheckBox cbAllowBreaks;
        private System.Windows.Forms.TabPage tabServices;
        private System.Windows.Forms.Label lblScanning;
        private System.Windows.Forms.Button cmdClearScan;
        private System.Windows.Forms.Button cmdSelectNone;
        private System.Windows.Forms.Button cmdSelectAll;
        private System.Windows.Forms.Button cmdScan;
        private System.Windows.Forms.DataGridView dgServices;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox gpTimeouts;
        private System.Windows.Forms.NumericUpDown nudDataCollectionTimeout;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFrequencies;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdNewOutputFile;
        private System.Windows.Forms.ComboBox cboCollectionType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.CheckBox cbLookupNotFound;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton rbSatellite;
        private System.Windows.Forms.RadioButton rbDVBT;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox gbSatellite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button cmdOpenINIFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button cmdSaveGeneral;
        private System.Windows.Forms.Button btTimeoutDefaults;
        private System.Windows.Forms.BindingSource tvStationBindingSource;
        private System.Windows.Forms.TabPage tabDiagnostics;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tbTraceIDs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDebugIDs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbScanningFrequencies;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox gpTimeAdjustments;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.NumericUpDown nudCurrentOffsetHours;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox tbChangeDate;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown nudNextOffsetHours;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.NumericUpDown nudChangeMinutes;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.NumericUpDown nudChangeHours;
        private System.Windows.Forms.NumericUpDown nudNextOffsetMinutes;
        private System.Windows.Forms.NumericUpDown nudCurrentOffsetMinutes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox cbManualTime;
        private System.Windows.Forms.CheckBox cbUseDVBViewer;
        private System.Windows.Forms.GroupBox gpWMCOptions;
        private System.Windows.Forms.GroupBox gpDVBViewerOptions;
        private System.Windows.Forms.CheckBox cbDVBViewerImport;
        private System.Windows.Forms.CheckBox cbDVBViewerClear;
        private System.Windows.Forms.CheckBox cbRecordingServiceImport;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.TabControl tbcDeliverySystem;
        private System.Windows.Forms.TabPage tbpSatellite;
        private System.Windows.Forms.Button btLNBDefaults;
        private System.Windows.Forms.Button btAddSatellite;
        private System.Windows.Forms.TextBox txtLNBSwitch;
        private System.Windows.Forms.ComboBox cboDVBSCollectionType;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtLNBHigh;
        private System.Windows.Forms.ComboBox cboDVBSScanningFrequency;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtLNBLow;
        private System.Windows.Forms.ComboBox cboSatellite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TabPage tbpTerrestrial;
        private System.Windows.Forms.Button btAddTerrestrial;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ComboBox cboDVBTCollectionType;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.ComboBox cboDVBTScanningFrequency;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TabPage tbpCable;
        private System.Windows.Forms.Button btAddCable;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox cboCable;
        private System.Windows.Forms.ComboBox cboCableCollectionType;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cboCableScanningFrequency;
        private System.Windows.Forms.TabPage tbpAtsc;
        private System.Windows.Forms.Button btAddAtsc;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cboAtscProvider;
        private System.Windows.Forms.ComboBox cboAtscCollectionType;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ComboBox cboAtscScanningFrequency;
        private System.Windows.Forms.TabPage tbpClearQAM;
        private System.Windows.Forms.Button btAddClearQam;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cboClearQamProvider;
        private System.Windows.Forms.ComboBox cboClearQamCollectionType;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.ComboBox cboClearQamScanningFrequency;
        private System.Windows.Forms.TabPage tbpISDBSatellite;
        private System.Windows.Forms.Button btISDBLNBDefaults;
        private System.Windows.Forms.Button btAddISDBSatellite;
        private System.Windows.Forms.TextBox txtISDBLNBSwitch;
        private System.Windows.Forms.ComboBox cboISDBSCollectionType;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txtISDBLNBHigh;
        private System.Windows.Forms.ComboBox cboISDBSScanningFrequency;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txtISDBLNBLow;
        private System.Windows.Forms.ComboBox cboISDBSatellite;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TabPage tbpISDBTerrestrial;
        private System.Windows.Forms.Button btAddISDBTerrestrial;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ComboBox cboISDBTProvider;
        private System.Windows.Forms.ComboBox cboISDBTCollectionType;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.ComboBox cboISDBTScanningFrequency;
        private System.Windows.Forms.ProgressBar pbarChannels;
        private System.Windows.Forms.CheckBox cbStoreStationInfo;
        private System.Windows.Forms.CheckBox cbUseStoredStationInfo;
        private System.Windows.Forms.TabPage tbpOffsets;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Button btPlusAdd;
        private System.Windows.Forms.ListBox lbPlusDestinationChannel;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.ListBox lbPlusSourceChannel;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.ProgressBar pbarPlusScan;
        private System.Windows.Forms.Label lblPlusScanning;
        private System.Windows.Forms.Button btPlusScan;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.ListView lvPlusSelectedChannels;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btPlusDelete;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.NumericUpDown nudPlusIncrement;
        private System.Windows.Forms.CheckBox cbAutoMapEPG;
        private System.Windows.Forms.TabPage tbpFilters;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TextBox tbExcludedMaxChannel;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Button btExcludeDelete;
        private System.Windows.Forms.ListView lvExcludedIdentifiers;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btExcludeAdd;
        private System.Windows.Forms.TextBox tbExcludeSIDStart;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox tbExcludeTSID;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox tbExcludeONID;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox tbExcludeSIDEnd;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TabPage tabRepeats;
        private System.Windows.Forms.CheckBox cbRemoveExtractedData;
        private System.Windows.Forms.CheckBox cbCreateSameData;
        private System.Windows.Forms.CheckBox cbFromService;
        private System.Windows.Forms.TextBox txtImportName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabPage tabLookups;
        private System.Windows.Forms.NumericUpDown nudLookupTime;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.GroupBox gpTVLookup;
        private System.Windows.Forms.GroupBox gpMovieLookup;
        private System.Windows.Forms.GroupBox gpLookupMisc;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.NumericUpDown nudLookupErrors;
        private System.Windows.Forms.CheckBox cbTVLookupEnabled;
        private System.Windows.Forms.CheckBox cbMovieLookupEnabled;
        private System.Windows.Forms.NumericUpDown nudLookupMovieLowDuration;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.ComboBox cboxMovieLookupImageType;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.ComboBox cboxTVLookupImageType;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox tbLookupIgnoredPhrases;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.ComboBox cbxLookupMatching;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.CheckBox cbLookupReload;
        private System.Windows.Forms.CheckBox cbWMCFourStarSpecial;
        private System.Windows.Forms.TextBox tbLookupMoviePhrases;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.NumericUpDown nudLookupMovieHighDuration;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.CheckBox cbLookupProcessAsTVSeries;
        private System.Windows.Forms.CheckBox cbLookupIgnoreCategories;
        private System.Windows.Forms.CheckBox cbDisableInbandLoader;
        private System.Windows.Forms.TextBox tbLookupImagePath;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Button btLookupBaseBrowse;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.CheckBox cbDVBLinkUpdateEnabled;
        private System.Windows.Forms.GroupBox gpDVBLink;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.ComboBox cboMergeMethod;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.CheckBox cbChildLock;
        private System.Windows.Forms.ComboBox cboEPGScanner;
        private System.Windows.Forms.NumericUpDown nudBufferFills;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.NumericUpDown nudBufferSize;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.CheckBox cbNoLogExcluded;
        private System.Windows.Forms.CheckBox cbDVBViewerSubtitleVisible;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.DomainUpDown udIgnorePhraseSeparator;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.DomainUpDown udMoviePhraseSeparator;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.ComboBox cboLNBType;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.TabPage tabXMLTV;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btXmltvDelete;
        private System.Windows.Forms.ListView lvXmltvSelectedFiles;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.GroupBox gpbFiles;
        private System.Windows.Forms.Button btXmltvAdd;
        private System.Windows.Forms.ComboBox cboXmltvLanguage;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox tbXmltvPath;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Button btXmltvBrowse;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ComboBox cboXmltvPrecedence;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Button btXmltvLoadFiles;
        private System.Windows.Forms.DataGridView dgXmltvChannelChanges;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Button btXmltvClear;
        private System.Windows.Forms.Button btXmltvExcludeAll;
        private System.Windows.Forms.Button btXmltvIncludeAll;
        private System.Windows.Forms.BindingSource xmltvChannelChangeBindingSource;
        private System.Windows.Forms.GroupBox gpXmltvOptions;
        private System.Windows.Forms.CheckBox cbUseLCN;
        private System.Windows.Forms.ComboBox cboEpisodeTagFormat;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.ComboBox cboChannelIDFormat;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox cbLogNetworkMap;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.NumericUpDown nudEPGScanInterval;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.CheckBox cbReloadChannelData;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.CheckBox cbUpdateChannelNumbers;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.GroupBox gpRepeatExclusions;
        private System.Windows.Forms.TextBox tbPhrasesToIgnore;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Button btRepeatDelete;
        private System.Windows.Forms.ListView lvRepeatPrograms;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button btRepeatAdd;
        private System.Windows.Forms.TextBox tbRepeatDescription;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox tbRepeatTitle;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbNoSimulcastRepeats;
        private System.Windows.Forms.CheckBox cbCheckForRepeats;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalNetworkIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transportStreamIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceIDColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn excludedByUserColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn logicalChannelNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn newNameColumn;
        private System.Windows.Forms.TabPage tabEdit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btEditDelete;
        private System.Windows.Forms.ListView lvEditSpecs;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbEditReplacementText;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.ComboBox cboEditLocation;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Button btEditAdd;
        private System.Windows.Forms.ComboBox cboEditApplyTo;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.TextBox tbEditText;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.TextBox tbDVBViewerIPAddress;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.ComboBox cboFilterFrequency;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.CheckBox cbXmltvNoLookup;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.CheckBox cbCreateADTag;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.CheckBox cbSageTVFile;
        private System.Windows.Forms.GroupBox gpSageTVFile;
        private System.Windows.Forms.TextBox tbSageTVFileName;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Button btBrowseSageTVFile;
        private System.Windows.Forms.CheckBox cbBladeRunnerFile;
        private System.Windows.Forms.GroupBox gpBladeRunnerFile;
        private System.Windows.Forms.TextBox tbBladeRunnerFileName;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Button btBrowseBladeRunnerFile;
        private System.Windows.Forms.CheckBox cbAreaRegionFile;
        private System.Windows.Forms.GroupBox gpAreaRegionFile;
        private System.Windows.Forms.TextBox tbAreaRegionFileName;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Button btBrowseAreaRegionFile;
        private System.Windows.Forms.CheckBox cbSageTVFileNoEPG;
        private System.Windows.Forms.CheckBox cbTcRelevantChannels;
        private System.Windows.Forms.Button btSelectedFrequencyDetails;
        private System.Windows.Forms.TextBox tbSageTVSatelliteNumber;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.TabPage tbpFile;
        private System.Windows.Forms.ComboBox cboDeliveryFileCollectionType;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Button tbDeliveryFileBrowse;
        private System.Windows.Forms.TextBox tbDeliveryFilePath;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.TabPage tbpStream;
        private System.Windows.Forms.NumericUpDown nudStreamPortNumber;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.ComboBox cboStreamProtocol;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.ComboBox cboStreamCollectionType;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.TextBox tbStreamIpAddress;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Button btDeliveryFileAdd;
        private System.Windows.Forms.Button btStreamAdd;
        private System.Windows.Forms.Button btFindIPAddress;
        private System.Windows.Forms.TextBox tbStreamPath;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.CheckBox cbAutoExcludeNew;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.CheckBox cbIgnoreWMCRecordings;
        private System.Windows.Forms.DomainUpDown udDvbsSatIpFrontend;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.DomainUpDown udDvbtSatIpFrontend;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.DomainUpDown udDvbcSatIpFrontend;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.NumericUpDown nudStreamMulticastSourcePort;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.TextBox tbStreamMulticastSourceIP;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Button btTuningParameters;
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
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckedListBox clbTerrestrialTuners;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckedListBox clbCableTuners;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.CheckedListBox clbAtscTuners;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.CheckedListBox clbClearQamTuners;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.CheckedListBox clbISDBSatelliteTuners;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.CheckedListBox clbISDBTerrestrialTuners;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.CheckBox cbChannelTuningErrors;
        private System.Windows.Forms.CheckBox cbTimeshiftTuningErrors;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ComboBox cboEditReplaceMode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboXmltvIdFormat;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cboWMCSeries;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox cbDvbViewerOutputEnabled;
        private System.Windows.Forms.CheckBox cbWmcOutputEnabled;
        private System.Windows.Forms.CheckBox cbXmltvOutputEnabled;
        private System.Windows.Forms.Button btLookupChangeNotMovie;
        private System.Windows.Forms.ComboBox cboLookupNotMovie;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.CheckBox cbLookupImageNameTitle;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.CheckBox cbLookupImagesInBase;
        private System.Windows.Forms.CheckBox cbElementPerTag;
        private System.Windows.Forms.CheckBox cbAddSeasonEpisodeToDesc;
        private System.Windows.Forms.CheckBox cbNoDataNoFile;
        private System.Windows.Forms.NumericUpDown nudLookupMatchThreshold;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox tbChannelLogoPath;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Button btBrowseLogoPath;
        private System.Windows.Forms.CheckBox cbOmitPartNumber;
        private System.Windows.Forms.ComboBox cboXmltvTimeZone;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.CheckBox cbPrefixSubtitleWithSeasonEpisode;
        private System.Windows.Forms.CheckBox cbPrefixDescWithAirDate;
        private System.Windows.Forms.CheckBox cbCreatePlexEpisodeNumTag;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.CheckBox cbIgnoreEpisodeTags;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.TabPage tabTranslations;
        private System.Windows.Forms.ProgressBar pbarTranslateScanning;
        private System.Windows.Forms.Label lblTranslateScanning;
        private System.Windows.Forms.Button btTranslateScan;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.ListBox lbTranslateChannels;
        private System.Windows.Forms.ComboBox cboTranslateOutputLanguage;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox tbTranslateApiKey;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Button btTranslateRefresh;
        private System.Windows.Forms.GroupBox gpTranslate;
        private System.Windows.Forms.CheckBox cbTranslateEnabled;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.CheckBox cbXmltvProcessNewTag;
        private System.Windows.Forms.Label label156;
        private System.Windows.Forms.CheckBox cbWmcNoDummyAffiliates;
        private System.Windows.Forms.TextBox tbXmltvIconTagPathPrefix;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.CheckBox cbWmcRunTasks;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.CheckBox cbLookupOverwrite;
        private System.Windows.Forms.Button btTuningLoadFromScan;
        private System.Windows.Forms.TabPage tabSchedulesDirect;
        private System.Windows.Forms.CheckBox cbSdEnabled;
        private System.Windows.Forms.GroupBox gpSchedulesDirect;
        private System.Windows.Forms.BindingSource sdBindingSource;
        private System.Windows.Forms.CheckBox cbXmltvSetPreviouslyShownDefault;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.Button btSdRemove;
        private System.Windows.Forms.Button btSdAdd;
        private System.Windows.Forms.DataGridView dgSdChannels;
        private System.Windows.Forms.ListView lvSdLineups;
        private System.Windows.Forms.ColumnHeader sdName;
        private System.Windows.Forms.ColumnHeader sdLocation;
        private System.Windows.Forms.ColumnHeader sdTransport;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Button btSdChangeLineups;
        private System.Windows.Forms.Button btSdExcludeAll;
        private System.Windows.Forms.Button btSdIncludeAll;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.Button btSdClear;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbSdUseLongDescriptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn xmltvDisplayNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xmltvChannelNumberColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn xmltvExcludedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xmltvNewNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xmltvCallSignColumn;
        private System.Windows.Forms.ComboBox cboXmltvStoreImagesLocally;
        private System.Windows.Forms.ComboBox cboSdStoreImagesLocally;
        private System.Windows.Forms.BindingSource logosBindingSource;
        private System.Windows.Forms.ComboBox cboLookupsEpisodeSearchPriority;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelCallSign;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelLineup;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelIdentification;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sdChannelExcluded;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelUserNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdChannelUserCallSign;
        private System.Windows.Forms.CheckBox cbCreateSameDataIfNotPresent;
        private System.Windows.Forms.CheckBox cbXmltvProcessLiveTag;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.CheckBox cbChannelDefinitionFile;
        private System.Windows.Forms.GroupBox gpChannelDefinitionFile;
        private System.Windows.Forms.TextBox tbChannelDefinitionFileName;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Button btBrowseChannelDefinitionFile;
        private System.Windows.Forms.CheckBox cbSatelliteDefFiles;
        private System.Windows.Forms.GroupBox gpSatelliteDefFiles;
        private System.Windows.Forms.TextBox tbSatelliteDefDirectoryName;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Button btBrowseSatelliteDefDirectory;
        private System.Windows.Forms.CheckBox cbDontCreateImportChannels;
        private System.Windows.Forms.CheckBox cbNoInvalidEntries;
        private System.Windows.Forms.TextBox tbTVDBPin;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.ComboBox cboTVProvider;
        private System.Windows.Forms.Label label134;
    }
}
