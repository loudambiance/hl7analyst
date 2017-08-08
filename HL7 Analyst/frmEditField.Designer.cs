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
    partial class frmEditField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditField));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbEditAll = new System.Windows.Forms.RadioButton();
            this.rbEditCurrent = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvEditField = new System.Windows.Forms.DataGridView();
            this.chID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditField)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbEditAll);
            this.panel1.Controls.Add(this.rbEditCurrent);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 33);
            this.panel1.TabIndex = 0;
            // 
            // rbEditAll
            // 
            this.rbEditAll.AutoSize = true;
            this.rbEditAll.Location = new System.Drawing.Point(168, 6);
            this.rbEditAll.Name = "rbEditAll";
            this.rbEditAll.Size = new System.Drawing.Size(108, 17);
            this.rbEditAll.TabIndex = 3;
            this.rbEditAll.Text = "Edit All Messages";
            this.rbEditAll.UseVisualStyleBackColor = true;
            // 
            // rbEditCurrent
            // 
            this.rbEditCurrent.AutoSize = true;
            this.rbEditCurrent.Checked = true;
            this.rbEditCurrent.Location = new System.Drawing.Point(12, 6);
            this.rbEditCurrent.Name = "rbEditCurrent";
            this.rbEditCurrent.Size = new System.Drawing.Size(150, 17);
            this.rbEditCurrent.TabIndex = 2;
            this.rbEditCurrent.TabStop = true;
            this.rbEditCurrent.Text = "Edit Current Message Only";
            this.rbEditCurrent.UseVisualStyleBackColor = true;
            this.rbEditCurrent.CheckedChanged += new System.EventHandler(this.rbEditCurrent_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(371, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvEditField
            // 
            this.dgvEditField.AllowUserToAddRows = false;
            this.dgvEditField.AllowUserToDeleteRows = false;
            this.dgvEditField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEditField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chID,
            this.chName,
            this.chValue});
            this.dgvEditField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEditField.Location = new System.Drawing.Point(0, 0);
            this.dgvEditField.Name = "dgvEditField";
            this.dgvEditField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEditField.Size = new System.Drawing.Size(539, 229);
            this.dgvEditField.TabIndex = 1;
            this.dgvEditField.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEditField_CellEndEdit);
            // 
            // chID
            // 
            this.chID.HeaderText = "ID";
            this.chID.Name = "chID";
            this.chID.ReadOnly = true;
            this.chID.Width = 43;
            // 
            // chName
            // 
            this.chName.HeaderText = "Name";
            this.chName.Name = "chName";
            this.chName.ReadOnly = true;
            this.chName.Width = 60;
            // 
            // chValue
            // 
            this.chValue.HeaderText = "Edit Value";
            this.chValue.Name = "chValue";
            this.chValue.Width = 80;
            // 
            // frmEditField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 262);
            this.Controls.Add(this.dgvEditField);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditField";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Field Value";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvEditField;
        private System.Windows.Forms.DataGridViewTextBoxColumn chID;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValue;
        private System.Windows.Forms.RadioButton rbEditAll;
        private System.Windows.Forms.RadioButton rbEditCurrent;
    }
}