/***************************************************************
* Copyright (C) 2011 Jeremy Reagan, All Rights Reserved.
* I may be reached via email at: jeremy.reagan@live.com
* 
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; under version 2
* of the License.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
****************************************************************/

namespace HL7_Analyst
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.pbAppImage = new System.Windows.Forms.PictureBox();
            this.btnLicense = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFamFamFam = new System.Windows.Forms.LinkLabel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblIconArchive = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblZedGraph = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNotice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAppImage
            // 
            this.pbAppImage.Image = global::HL7_Analyst.Properties.Resources.Application;
            this.pbAppImage.Location = new System.Drawing.Point(12, 12);
            this.pbAppImage.Name = "pbAppImage";
            this.pbAppImage.Size = new System.Drawing.Size(128, 128);
            this.pbAppImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbAppImage.TabIndex = 0;
            this.pbAppImage.TabStop = false;
            // 
            // btnLicense
            // 
            this.btnLicense.Location = new System.Drawing.Point(484, 194);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(75, 23);
            this.btnLicense.TabIndex = 2;
            this.btnLicense.Text = "License";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 214);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Toolbar Icons:";
            // 
            // lblFamFamFam
            // 
            this.lblFamFamFam.AutoSize = true;
            this.lblFamFamFam.Location = new System.Drawing.Point(101, 214);
            this.lblFamFamFam.Margin = new System.Windows.Forms.Padding(3);
            this.lblFamFamFam.Name = "lblFamFamFam";
            this.lblFamFamFam.Size = new System.Drawing.Size(108, 13);
            this.lblFamFamFam.TabIndex = 4;
            this.lblFamFamFam.TabStop = true;
            this.lblFamFamFam.Text = "www.famfamfam.com";
            this.lblFamFamFam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFamFamFam_LinkClicked);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(146, 53);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(3);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(51, 13);
            this.lblCopyright.TabIndex = 6;
            this.lblCopyright.Text = "Copyright";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(146, 34);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(3);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "Version";
            // 
            // lblIconArchive
            // 
            this.lblIconArchive.AutoSize = true;
            this.lblIconArchive.Location = new System.Drawing.Point(101, 194);
            this.lblIconArchive.Margin = new System.Windows.Forms.Padding(3);
            this.lblIconArchive.Name = "lblIconArchive";
            this.lblIconArchive.Size = new System.Drawing.Size(112, 13);
            this.lblIconArchive.TabIndex = 10;
            this.lblIconArchive.TabStop = true;
            this.lblIconArchive.Text = "www.iconarchive.com";
            this.lblIconArchive.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblIconArchive_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 194);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Application Icon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(146, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "HL7 Analyst";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Credits";
            // 
            // lblZedGraph
            // 
            this.lblZedGraph.AutoSize = true;
            this.lblZedGraph.Location = new System.Drawing.Point(101, 233);
            this.lblZedGraph.Margin = new System.Windows.Forms.Padding(3);
            this.lblZedGraph.Name = "lblZedGraph";
            this.lblZedGraph.Size = new System.Drawing.Size(69, 13);
            this.lblZedGraph.TabIndex = 15;
            this.lblZedGraph.TabStop = true;
            this.lblZedGraph.Text = "zedgraph.org";
            this.lblZedGraph.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblZedGraph_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 233);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Graph Control:";
            // 
            // txtNotice
            // 
            this.txtNotice.BackColor = System.Drawing.Color.White;
            this.txtNotice.Location = new System.Drawing.Point(150, 72);
            this.txtNotice.Multiline = true;
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.ReadOnly = true;
            this.txtNotice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotice.Size = new System.Drawing.Size(409, 116);
            this.txtNotice.TabIndex = 16;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 260);
            this.Controls.Add(this.txtNotice);
            this.Controls.Add(this.lblZedGraph);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblFamFamFam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIconArchive);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.pbAppImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About HL7 Analyst";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAppImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAppImage;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblFamFamFam;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lblIconArchive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblZedGraph;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNotice;
    }
}