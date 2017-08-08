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
    partial class frmTCPIPConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTCPIPConnection));
            this.label1 = new System.Windows.Forms.Label();
            this.txtHostAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTrailer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbWaitForAck = new System.Windows.Forms.CheckBox();
            this.cbSendAck = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHeader = new System.Windows.Forms.Button();
            this.btnTrailer = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host Address:";
            // 
            // txtHostAddress
            // 
            this.txtHostAddress.Location = new System.Drawing.Point(91, 38);
            this.txtHostAddress.Name = "txtHostAddress";
            this.txtHostAddress.Size = new System.Drawing.Size(181, 20);
            this.txtHostAddress.TabIndex = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(91, 64);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(181, 20);
            this.txtPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(91, 90);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(152, 20);
            this.txtHeader.TabIndex = 5;
            this.txtHeader.Text = "[0x0B]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "LLP Header:";
            // 
            // txtTrailer
            // 
            this.txtTrailer.Location = new System.Drawing.Point(91, 116);
            this.txtTrailer.Name = "txtTrailer";
            this.txtTrailer.Size = new System.Drawing.Size(152, 20);
            this.txtTrailer.TabIndex = 7;
            this.txtTrailer.Text = "[0x1C][0x0D]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "LLP Trailer:";
            // 
            // cbWaitForAck
            // 
            this.cbWaitForAck.AutoSize = true;
            this.cbWaitForAck.Location = new System.Drawing.Point(187, 143);
            this.cbWaitForAck.Name = "cbWaitForAck";
            this.cbWaitForAck.Size = new System.Drawing.Size(85, 17);
            this.cbWaitForAck.TabIndex = 8;
            this.cbWaitForAck.Text = "Wait for Ack";
            this.cbWaitForAck.UseVisualStyleBackColor = true;
            // 
            // cbSendAck
            // 
            this.cbSendAck.AutoSize = true;
            this.cbSendAck.Location = new System.Drawing.Point(91, 143);
            this.cbSendAck.Name = "cbSendAck";
            this.cbSendAck.Size = new System.Drawing.Size(73, 17);
            this.cbSendAck.TabIndex = 9;
            this.cbSendAck.Text = "Send Ack";
            this.cbSendAck.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(116, 166);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHeader
            // 
            this.btnHeader.Location = new System.Drawing.Point(249, 88);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(23, 23);
            this.btnHeader.TabIndex = 12;
            this.btnHeader.Text = "::";
            this.btnHeader.UseVisualStyleBackColor = true;
            this.btnHeader.Click += new System.EventHandler(this.btnHeader_Click);
            // 
            // btnTrailer
            // 
            this.btnTrailer.Location = new System.Drawing.Point(249, 114);
            this.btnTrailer.Name = "btnTrailer";
            this.btnTrailer.Size = new System.Drawing.Size(23, 23);
            this.btnTrailer.TabIndex = 13;
            this.btnTrailer.Text = "::";
            this.btnTrailer.UseVisualStyleBackColor = true;
            this.btnTrailer.Click += new System.EventHandler(this.btnTrailer_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(181, 20);
            this.txtName.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Name:";
            // 
            // frmTCPIPConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 200);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnTrailer);
            this.Controls.Add(this.btnHeader);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbSendAck);
            this.Controls.Add(this.cbWaitForAck);
            this.Controls.Add(this.txtTrailer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHostAddress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTCPIPConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add TCP/IP Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHostAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrailer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbWaitForAck;
        private System.Windows.Forms.CheckBox cbSendAck;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHeader;
        private System.Windows.Forms.Button btnTrailer;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
    }
}