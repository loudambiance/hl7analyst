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
    partial class frmLLP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLLP));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvLLP = new System.Windows.Forms.DataGridView();
            this.chHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLLP)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 227);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 35);
            this.panel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(252, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(333, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgvLLP
            // 
            this.dgvLLP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLLP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chHex,
            this.chDesc,
            this.cSelect});
            this.dgvLLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLLP.Location = new System.Drawing.Point(0, 0);
            this.dgvLLP.Name = "dgvLLP";
            this.dgvLLP.Size = new System.Drawing.Size(420, 227);
            this.dgvLLP.TabIndex = 2;
            this.dgvLLP.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLLP_CellValueChanged);
            this.dgvLLP.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvLLP_CurrentCellDirtyStateChanged);
            // 
            // chHex
            // 
            this.chHex.HeaderText = "Hex Code";
            this.chHex.Name = "chHex";
            this.chHex.ReadOnly = true;
            // 
            // chDesc
            // 
            this.chDesc.HeaderText = "Description";
            this.chDesc.Name = "chDesc";
            this.chDesc.ReadOnly = true;
            // 
            // cSelect
            // 
            this.cSelect.HeaderText = "Selection";
            this.cSelect.Name = "cSelect";
            // 
            // frmLLP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 262);
            this.Controls.Add(this.dgvLLP);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLLP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lower Layer Protocol";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLLP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvLLP;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn chDesc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSelect;
    }
}