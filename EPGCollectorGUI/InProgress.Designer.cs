namespace EPGCentre
{
    /// <summary>
    /// The In Progress dialog.
    /// </summary>
    partial class InProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InProgress));
            this.pbarProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbarProgress
            // 
            this.pbarProgress.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.pbarProgress.Location = new System.Drawing.Point(21, 12);
            this.pbarProgress.Maximum = 500;
            this.pbarProgress.Name = "pbarProgress";
            this.pbarProgress.Size = new System.Drawing.Size(386, 17);
            this.pbarProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarProgress.TabIndex = 314;
            // 
            // InProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 43);
            this.Controls.Add(this.pbarProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPG Centre - ";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbarProgress;
    }
}