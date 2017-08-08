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
    partial class frmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.cbHideEmpty = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExtensions = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchPath = new System.Windows.Forms.TextBox();
            this.btnSearchPath = new System.Windows.Forms.Button();
            this.fbSearchPath = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefaultSegment = new System.Windows.Forms.TextBox();
            this.cbCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbHideEmpty
            // 
            this.cbHideEmpty.AutoSize = true;
            this.cbHideEmpty.Location = new System.Drawing.Point(12, 6);
            this.cbHideEmpty.Name = "cbHideEmpty";
            this.cbHideEmpty.Size = new System.Drawing.Size(110, 17);
            this.cbHideEmpty.TabIndex = 0;
            this.cbHideEmpty.Text = "Hide Empty Fields";
            this.cbHideEmpty.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HL7 File Extensions: (One Per Line)";
            // 
            // txtExtensions
            // 
            this.txtExtensions.Location = new System.Drawing.Point(12, 100);
            this.txtExtensions.Multiline = true;
            this.txtExtensions.Name = "txtExtensions";
            this.txtExtensions.Size = new System.Drawing.Size(395, 131);
            this.txtExtensions.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(251, 237);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(332, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Default Search Path:";
            // 
            // txtSearchPath
            // 
            this.txtSearchPath.Location = new System.Drawing.Point(121, 29);
            this.txtSearchPath.Name = "txtSearchPath";
            this.txtSearchPath.Size = new System.Drawing.Size(257, 20);
            this.txtSearchPath.TabIndex = 1;
            // 
            // btnSearchPath
            // 
            this.btnSearchPath.Location = new System.Drawing.Point(384, 27);
            this.btnSearchPath.Name = "btnSearchPath";
            this.btnSearchPath.Size = new System.Drawing.Size(23, 23);
            this.btnSearchPath.TabIndex = 2;
            this.btnSearchPath.Text = "::";
            this.btnSearchPath.UseVisualStyleBackColor = true;
            this.btnSearchPath.Click += new System.EventHandler(this.btnSearchPath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Build Search Query Default Segment:";
            // 
            // txtDefaultSegment
            // 
            this.txtDefaultSegment.AutoCompleteCustomSource.AddRange(new string[] {
            "ABS",
            "ACC",
            "ADD",
            "AFF",
            "AIG",
            "AIL",
            "AIP",
            "AIS",
            "AL1",
            "APR",
            "ARQ",
            "AUT",
            "BHS",
            "BLC",
            "BLG",
            "BPO",
            "BPX",
            "BTS",
            "BTX",
            "CDM",
            "CER",
            "CM0",
            "CM1",
            "CM2",
            "CNS",
            "CON",
            "CSP",
            "CSR",
            "CSS",
            "CTD",
            "CTI",
            "DB1",
            "DG1",
            "DRG",
            "DSC",
            "DSP",
            "ECD",
            "ECR",
            "EDU",
            "EQL",
            "EQP",
            "EQU",
            "ERQ",
            "ERR",
            "EVN",
            "FAC",
            "FHS",
            "FT1",
            "FTS",
            "GOL",
            "GP1",
            "GP2",
            "GT1",
            "IAM",
            "IIM",
            "IN1",
            "IN2",
            "IN3",
            "INV",
            "IPC",
            "ISD",
            "LAN",
            "LCC",
            "LCH",
            "LDP",
            "LOC",
            "LRL",
            "MFA",
            "MFE",
            "MFI",
            "MRG",
            "MSA",
            "MSH",
            "NCK",
            "NDS",
            "NK1",
            "NPU",
            "NSC",
            "NST",
            "NTE",
            "OBR",
            "OBX",
            "ODS",
            "ODT",
            "OM1",
            "OM2",
            "OM3",
            "OM4",
            "OM5",
            "OM6",
            "OM7",
            "ORC",
            "ORG",
            "OVR",
            "PCR",
            "PD1",
            "PDA",
            "PDC",
            "PEO",
            "PES",
            "PID",
            "PR1",
            "PRA",
            "PRB",
            "PRC",
            "PRD",
            "PSH",
            "PTH",
            "PV1",
            "PV2",
            "QAK",
            "QID",
            "QPD",
            "QRD",
            "QRF",
            "QRI",
            "RCP",
            "RDF",
            "RDT",
            "RF1",
            "RGS",
            "RMI",
            "ROL",
            "RQ1",
            "RQD",
            "RXA",
            "RXC",
            "RXD",
            "RXE",
            "RXG",
            "RXO",
            "RXR",
            "SAC",
            "SCH",
            "SFT",
            "SID",
            "SPM",
            "SPR",
            "STF",
            "TCC",
            "TCD",
            "TQ1",
            "TQ2",
            "TXA",
            "UB1",
            "UB2",
            "URD",
            "URS",
            "VAR",
            "VTQ"});
            this.txtDefaultSegment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDefaultSegment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDefaultSegment.Location = new System.Drawing.Point(198, 55);
            this.txtDefaultSegment.Name = "txtDefaultSegment";
            this.txtDefaultSegment.Size = new System.Drawing.Size(208, 20);
            this.txtDefaultSegment.TabIndex = 3;
            // 
            // cbCheckForUpdates
            // 
            this.cbCheckForUpdates.AutoSize = true;
            this.cbCheckForUpdates.Location = new System.Drawing.Point(128, 6);
            this.cbCheckForUpdates.Name = "cbCheckForUpdates";
            this.cbCheckForUpdates.Size = new System.Drawing.Size(213, 17);
            this.cbCheckForUpdates.TabIndex = 8;
            this.cbCheckForUpdates.Text = "Check For Updates on Application Start";
            this.cbCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 269);
            this.Controls.Add(this.cbCheckForUpdates);
            this.Controls.Add(this.txtDefaultSegment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearchPath);
            this.Controls.Add(this.txtSearchPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtExtensions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbHideEmpty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbHideEmpty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExtensions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchPath;
        private System.Windows.Forms.Button btnSearchPath;
        private System.Windows.Forms.FolderBrowserDialog fbSearchPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDefaultSegment;
        private System.Windows.Forms.CheckBox cbCheckForUpdates;
    }
}