namespace EPGCentre
{
    partial class ChangeSdLineups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSdLineups));
            this.btAdd = new System.Windows.Forms.Button();
            this.label161 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbZipPostCode = new System.Windows.Forms.TextBox();
            this.cboSatelliteTransmitter = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cboMethod = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.tvLineups = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lvSelectedLineups = new System.Windows.Forms.ListView();
            this.lineupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lineupLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lineupTransport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btDelete = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.gpLineups = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label159 = new System.Windows.Forms.Label();
            this.btSdVerify = new System.Windows.Forms.Button();
            this.tbSdUserName = new System.Windows.Forms.TextBox();
            this.label158 = new System.Windows.Forms.Label();
            this.tbSdPassword = new System.Windows.Forms.TextBox();
            this.gpLineups.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.SuspendLayout();
            // 
            // btAdd
            // 
            this.btAdd.Enabled = false;
            this.btAdd.Location = new System.Drawing.Point(581, 188);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(79, 23);
            this.btAdd.TabIndex = 29;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Location = new System.Drawing.Point(84, -58);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(43, 13);
            this.label161.TabIndex = 323;
            this.label161.Text = "Method";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(84, -95);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(43, 13);
            this.label160.TabIndex = 321;
            this.label160.Text = "Country";
            // 
            // cboCountry
            // 
            this.cboCountry.DropDownHeight = 200;
            this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.IntegralHeight = false;
            this.cboCountry.Location = new System.Drawing.Point(153, 64);
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(190, 21);
            this.cboCountry.Sorted = true;
            this.cboCountry.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Country";
            // 
            // tbZipPostCode
            // 
            this.tbZipPostCode.Location = new System.Drawing.Point(154, 100);
            this.tbZipPostCode.Name = "tbZipPostCode";
            this.tbZipPostCode.Size = new System.Drawing.Size(190, 20);
            this.tbZipPostCode.TabIndex = 15;
            // 
            // cboSatelliteTransmitter
            // 
            this.cboSatelliteTransmitter.DropDownHeight = 200;
            this.cboSatelliteTransmitter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSatelliteTransmitter.FormattingEnabled = true;
            this.cboSatelliteTransmitter.IntegralHeight = false;
            this.cboSatelliteTransmitter.Items.AddRange(new object[] {
            "Satellite",
            "Transmitter",
            "Zip code/postcode"});
            this.cboSatelliteTransmitter.Location = new System.Drawing.Point(153, 99);
            this.cboSatelliteTransmitter.Name = "cboSatelliteTransmitter";
            this.cboSatelliteTransmitter.Size = new System.Drawing.Size(190, 21);
            this.cboSatelliteTransmitter.Sorted = true;
            this.cboSatelliteTransmitter.TabIndex = 21;
            this.cboSatelliteTransmitter.Visible = false;
            this.cboSatelliteTransmitter.SelectedIndexChanged += new System.EventHandler(this.cboSatelliteTransmitter_SelectedIndexChanged);
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(31, 102);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(98, 13);
            this.lblMethod.TabIndex = 19;
            this.lblMethod.Text = "Zip code/postcode";
            // 
            // cboMethod
            // 
            this.cboMethod.DropDownHeight = 200;
            this.cboMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMethod.FormattingEnabled = true;
            this.cboMethod.IntegralHeight = false;
            this.cboMethod.Items.AddRange(new object[] {
            "Zip code/postcode",
            "Satellite",
            "Transmitter"});
            this.cboMethod.Location = new System.Drawing.Point(153, 28);
            this.cboMethod.Name = "cboMethod";
            this.cboMethod.Size = new System.Drawing.Size(190, 21);
            this.cboMethod.TabIndex = 13;
            this.cboMethod.SelectedIndexChanged += new System.EventHandler(this.cboMethod_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Method";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(397, 610);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 39;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // tvLineups
            // 
            this.tvLineups.FullRowSelect = true;
            this.tvLineups.Location = new System.Drawing.Point(34, 159);
            this.tvLineups.Name = "tvLineups";
            this.tvLineups.Size = new System.Drawing.Size(528, 197);
            this.tvLineups.TabIndex = 25;
            this.tvLineups.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvLineups_BeforeExpand);
            this.tvLineups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLineups_AfterSelect);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Lineups Available";
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(581, 159);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(79, 23);
            this.btRefresh.TabIndex = 27;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Selected Lineups";
            // 
            // lvSelectedLineups
            // 
            this.lvSelectedLineups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lineupName,
            this.lineupLocation,
            this.lineupTransport});
            this.lvSelectedLineups.FullRowSelect = true;
            this.lvSelectedLineups.GridLines = true;
            this.lvSelectedLineups.Location = new System.Drawing.Point(34, 386);
            this.lvSelectedLineups.Name = "lvSelectedLineups";
            this.lvSelectedLineups.Size = new System.Drawing.Size(528, 97);
            this.lvSelectedLineups.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSelectedLineups.TabIndex = 33;
            this.lvSelectedLineups.UseCompatibleStateImageBehavior = false;
            this.lvSelectedLineups.View = System.Windows.Forms.View.Details;
            this.lvSelectedLineups.SelectedIndexChanged += new System.EventHandler(this.lvSelectedLineups_SelectedIndexChanged);
            // 
            // lineupName
            // 
            this.lineupName.Text = "Name";
            this.lineupName.Width = 174;
            // 
            // lineupLocation
            // 
            this.lineupLocation.Text = "Location";
            this.lineupLocation.Width = 172;
            // 
            // lineupTransport
            // 
            this.lineupTransport.Text = "Transport";
            this.lineupTransport.Width = 100;
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(581, 386);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(79, 23);
            this.btDelete.TabIndex = 35;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(218, 610);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(79, 23);
            this.btOK.TabIndex = 37;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gpLineups
            // 
            this.gpLineups.Controls.Add(this.cboMethod);
            this.gpLineups.Controls.Add(this.btAdd);
            this.gpLineups.Controls.Add(this.label1);
            this.gpLineups.Controls.Add(this.btDelete);
            this.gpLineups.Controls.Add(this.cboCountry);
            this.gpLineups.Controls.Add(this.lvSelectedLineups);
            this.gpLineups.Controls.Add(this.label3);
            this.gpLineups.Controls.Add(this.label2);
            this.gpLineups.Controls.Add(this.lblMethod);
            this.gpLineups.Controls.Add(this.btRefresh);
            this.gpLineups.Controls.Add(this.cboSatelliteTransmitter);
            this.gpLineups.Controls.Add(this.label4);
            this.gpLineups.Controls.Add(this.tbZipPostCode);
            this.gpLineups.Controls.Add(this.tvLineups);
            this.gpLineups.Location = new System.Drawing.Point(27, 91);
            this.gpLineups.Name = "gpLineups";
            this.gpLineups.Size = new System.Drawing.Size(676, 503);
            this.gpLineups.TabIndex = 9;
            this.gpLineups.TabStop = false;
            this.gpLineups.Text = "Lineups";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label159);
            this.groupBox11.Controls.Add(this.btSdVerify);
            this.groupBox11.Controls.Add(this.tbSdUserName);
            this.groupBox11.Controls.Add(this.label158);
            this.groupBox11.Controls.Add(this.tbSdPassword);
            this.groupBox11.Location = new System.Drawing.Point(27, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(676, 58);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Credentials";
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(21, 27);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(58, 13);
            this.label159.TabIndex = 2;
            this.label159.Text = "User name";
            // 
            // btSdVerify
            // 
            this.btSdVerify.Location = new System.Drawing.Point(581, 21);
            this.btSdVerify.Name = "btSdVerify";
            this.btSdVerify.Size = new System.Drawing.Size(79, 23);
            this.btSdVerify.TabIndex = 7;
            this.btSdVerify.Text = "Verify";
            this.btSdVerify.UseVisualStyleBackColor = true;
            this.btSdVerify.Click += new System.EventHandler(this.btSdVerify_Click);
            // 
            // tbSdUserName
            // 
            this.tbSdUserName.Location = new System.Drawing.Point(123, 23);
            this.tbSdUserName.Name = "tbSdUserName";
            this.tbSdUserName.Size = new System.Drawing.Size(138, 20);
            this.tbSdUserName.TabIndex = 3;
            this.tbSdUserName.TextChanged += new System.EventHandler(this.tbSdUserName_TextChanged);
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(287, 27);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(53, 13);
            this.label158.TabIndex = 4;
            this.label158.Text = "Password";
            // 
            // tbSdPassword
            // 
            this.tbSdPassword.Location = new System.Drawing.Point(370, 23);
            this.tbSdPassword.Name = "tbSdPassword";
            this.tbSdPassword.Size = new System.Drawing.Size(165, 20);
            this.tbSdPassword.TabIndex = 5;
            this.tbSdPassword.TextChanged += new System.EventHandler(this.tbSdPassword_TextChanged);
            // 
            // ChangeSdLineups
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(728, 650);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.gpLineups);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.label161);
            this.Controls.Add(this.label160);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeSdLineups";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - Change Schedule Direct Lineups";
            this.gpLineups.ResumeLayout(false);
            this.gpLineups.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbZipPostCode;
        private System.Windows.Forms.ComboBox cboSatelliteTransmitter;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.ComboBox cboMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TreeView tvLineups;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvSelectedLineups;
        private System.Windows.Forms.ColumnHeader lineupName;
        private System.Windows.Forms.ColumnHeader lineupLocation;
        private System.Windows.Forms.ColumnHeader lineupTransport;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox gpLineups;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Button btSdVerify;
        private System.Windows.Forms.TextBox tbSdUserName;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.TextBox tbSdPassword;
    }
}