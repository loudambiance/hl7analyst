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
    partial class frmReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
            this.dgvReportItems = new System.Windows.Forms.DataGridView();
            this.tsTopMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.sfdSaveReport = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportItems)).BeginInit();
            this.tsTopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReportItems
            // 
            this.dgvReportItems.AllowUserToAddRows = false;
            this.dgvReportItems.AllowUserToDeleteRows = false;
            this.dgvReportItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportItems.Location = new System.Drawing.Point(0, 25);
            this.dgvReportItems.Name = "dgvReportItems";
            this.dgvReportItems.ReadOnly = true;
            this.dgvReportItems.Size = new System.Drawing.Size(284, 237);
            this.dgvReportItems.TabIndex = 0;
            this.dgvReportItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvReportItems_KeyDown);
            // 
            // tsTopMenu
            // 
            this.tsTopMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.tsTopMenu.Location = new System.Drawing.Point(0, 0);
            this.tsTopMenu.Name = "tsTopMenu";
            this.tsTopMenu.Size = new System.Drawing.Size(284, 25);
            this.tsTopMenu.TabIndex = 1;
            this.tsTopMenu.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::HL7_Analyst.Properties.Resources.disk;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save Report";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // sfdSaveReport
            // 
            this.sfdSaveReport.DefaultExt = "csv";
            this.sfdSaveReport.Filter = "Comma Delimited|*.csv";
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.dgvReportItems);
            this.Controls.Add(this.tsTopMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportItems)).EndInit();
            this.tsTopMenu.ResumeLayout(false);
            this.tsTopMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReportItems;
        private System.Windows.Forms.ToolStrip tsTopMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SaveFileDialog sfdSaveReport;
    }
}