namespace HL7_Analyst
{
    partial class frmUpdateAvailable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateAvailable));
            this.label1 = new System.Windows.Forms.Label();
            this.llblDownloadLocation = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "A New Version of HL7 Analyst is Available!";
            // 
            // llblDownloadLocation
            // 
            this.llblDownloadLocation.AutoSize = true;
            this.llblDownloadLocation.Location = new System.Drawing.Point(14, 37);
            this.llblDownloadLocation.Margin = new System.Windows.Forms.Padding(5);
            this.llblDownloadLocation.Name = "llblDownloadLocation";
            this.llblDownloadLocation.Size = new System.Drawing.Size(215, 13);
            this.llblDownloadLocation.TabIndex = 1;
            this.llblDownloadLocation.TabStop = true;
            this.llblDownloadLocation.Text = "The New Version can be Downloaded here.";
            this.llblDownloadLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDownloadLocation_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(197, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUpdateAvailable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 88);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.llblDownloadLocation);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpdateAvailable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Version Available";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llblDownloadLocation;
        private System.Windows.Forms.Button btnClose;
    }
}