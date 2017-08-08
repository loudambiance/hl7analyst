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
    partial class frmSegments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSegments));
            this.lvItems = new System.Windows.Forms.ListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAverage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sfdSaveReport = new System.Windows.Forms.SaveFileDialog();
            this.tsTopMenu = new System.Windows.Forms.ToolStrip();
            this.btnSaveReport = new System.Windows.Forms.ToolStripButton();
            this.tsTopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvItems
            // 
            this.lvItems.AllowColumnReorder = true;
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chName,
            this.chMin,
            this.chAverage,
            this.chMax});
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.FullRowSelect = true;
            this.lvItems.GridLines = true;
            this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvItems.Location = new System.Drawing.Point(0, 25);
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(778, 237);
            this.lvItems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvItems.TabIndex = 0;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            this.lvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvItems_KeyDown);
            // 
            // chID
            // 
            this.chID.Text = "ID";
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 400;
            // 
            // chMin
            // 
            this.chMin.Text = "Minimum Length";
            this.chMin.Width = 100;
            // 
            // chAverage
            // 
            this.chAverage.Text = "Average Length";
            this.chAverage.Width = 100;
            // 
            // chMax
            // 
            this.chMax.Text = "Maximum Length";
            this.chMax.Width = 100;
            // 
            // sfdSaveReport
            // 
            this.sfdSaveReport.DefaultExt = "csv";
            this.sfdSaveReport.Filter = "Comma Delimited|*.csv";
            // 
            // tsTopMenu
            // 
            this.tsTopMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveReport});
            this.tsTopMenu.Location = new System.Drawing.Point(0, 0);
            this.tsTopMenu.Name = "tsTopMenu";
            this.tsTopMenu.Size = new System.Drawing.Size(778, 25);
            this.tsTopMenu.TabIndex = 1;
            this.tsTopMenu.Text = "toolStrip1";
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveReport.Image = global::HL7_Analyst.Properties.Resources.disk;
            this.btnSaveReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new System.Drawing.Size(23, 22);
            this.btnSaveReport.Text = "Save Report";
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // frmSegments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 262);
            this.Controls.Add(this.lvItems);
            this.Controls.Add(this.tsTopMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSegments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filled Fields";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSegments_Load);
            this.tsTopMenu.ResumeLayout(false);
            this.tsTopMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chMin;
        private System.Windows.Forms.ColumnHeader chAverage;
        private System.Windows.Forms.ColumnHeader chMax;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.SaveFileDialog sfdSaveReport;
        private System.Windows.Forms.ToolStrip tsTopMenu;
        private System.Windows.Forms.ToolStripButton btnSaveReport;
    }
}