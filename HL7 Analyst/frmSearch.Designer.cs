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
    partial class frmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearch));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchTerms = new System.Windows.Forms.TextBox();
            this.txtSearchPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchPath = new System.Windows.Forms.Button();
            this.fbSearchPath = new System.Windows.Forms.FolderBrowserDialog();
            this.cbSearchAll = new System.Windows.Forms.CheckBox();
            this.clbSubFolders = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMatchCount = new System.Windows.Forms.Label();
            this.btnComponents = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Terms:";
            // 
            // txtSearchTerms
            // 
            this.txtSearchTerms.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearchTerms.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchTerms.Location = new System.Drawing.Point(94, 6);
            this.txtSearchTerms.Name = "txtSearchTerms";
            this.txtSearchTerms.Size = new System.Drawing.Size(484, 20);
            this.txtSearchTerms.TabIndex = 0;
            this.txtSearchTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // txtSearchPath
            // 
            this.txtSearchPath.Location = new System.Drawing.Point(94, 32);
            this.txtSearchPath.Name = "txtSearchPath";
            this.txtSearchPath.Size = new System.Drawing.Size(484, 20);
            this.txtSearchPath.TabIndex = 2;
            this.txtSearchPath.TextChanged += new System.EventHandler(this.txtSearchPath_TextChanged);
            this.txtSearchPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search Path:";
            // 
            // btnSearchPath
            // 
            this.btnSearchPath.Location = new System.Drawing.Point(584, 30);
            this.btnSearchPath.Name = "btnSearchPath";
            this.btnSearchPath.Size = new System.Drawing.Size(23, 23);
            this.btnSearchPath.TabIndex = 3;
            this.btnSearchPath.Text = "::";
            this.btnSearchPath.UseVisualStyleBackColor = true;
            this.btnSearchPath.Click += new System.EventHandler(this.btnSearchPath_Click);
            this.btnSearchPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // cbSearchAll
            // 
            this.cbSearchAll.AutoSize = true;
            this.cbSearchAll.Location = new System.Drawing.Point(474, 58);
            this.cbSearchAll.Name = "cbSearchAll";
            this.cbSearchAll.Size = new System.Drawing.Size(133, 17);
            this.cbSearchAll.TabIndex = 4;
            this.cbSearchAll.Text = "Search All Sub-Folders";
            this.cbSearchAll.UseVisualStyleBackColor = true;
            this.cbSearchAll.CheckedChanged += new System.EventHandler(this.cbSearchAll_CheckedChanged);
            this.cbSearchAll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // clbSubFolders
            // 
            this.clbSubFolders.CheckOnClick = true;
            this.clbSubFolders.FormattingEnabled = true;
            this.clbSubFolders.Location = new System.Drawing.Point(15, 75);
            this.clbSubFolders.Name = "clbSubFolders";
            this.clbSubFolders.Size = new System.Drawing.Size(592, 184);
            this.clbSubFolders.TabIndex = 5;
            this.clbSubFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sub-Folders";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(451, 288);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(532, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoSize = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(81, 262);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentFile.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Current File:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Matches Found:";
            // 
            // lblMatchCount
            // 
            this.lblMatchCount.AutoSize = true;
            this.lblMatchCount.Location = new System.Drawing.Point(102, 293);
            this.lblMatchCount.Name = "lblMatchCount";
            this.lblMatchCount.Size = new System.Drawing.Size(0, 13);
            this.lblMatchCount.TabIndex = 13;
            // 
            // btnComponents
            // 
            this.btnComponents.Location = new System.Drawing.Point(584, 4);
            this.btnComponents.Name = "btnComponents";
            this.btnComponents.Size = new System.Drawing.Size(23, 23);
            this.btnComponents.TabIndex = 1;
            this.btnComponents.Text = "::";
            this.btnComponents.UseVisualStyleBackColor = true;
            this.btnComponents.Click += new System.EventHandler(this.btnComponents_Click);
            this.btnComponents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 316);
            this.Controls.Add(this.btnComponents);
            this.Controls.Add(this.lblMatchCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clbSubFolders);
            this.Controls.Add(this.cbSearchAll);
            this.Controls.Add(this.btnSearchPath);
            this.Controls.Add(this.txtSearchPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchTerms);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search for Files";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearch_FormClosed);
            this.Load += new System.EventHandler(this.frmSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTerms_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchTerms;
        private System.Windows.Forms.TextBox txtSearchPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchPath;
        private System.Windows.Forms.FolderBrowserDialog fbSearchPath;
        private System.Windows.Forms.CheckBox cbSearchAll;
        private System.Windows.Forms.CheckedListBox clbSubFolders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMatchCount;
        private System.Windows.Forms.Button btnComponents;
    }
}