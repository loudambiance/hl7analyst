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
    partial class frmMessageStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessageStats));
            this.zgGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zgGraph
            // 
            this.zgGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgGraph.IsShowPointValues = true;
            this.zgGraph.Location = new System.Drawing.Point(0, 0);
            this.zgGraph.Name = "zgGraph";
            this.zgGraph.PanModifierKeys = System.Windows.Forms.Keys.None;
            this.zgGraph.ScrollGrace = 0D;
            this.zgGraph.ScrollMaxX = 0D;
            this.zgGraph.ScrollMaxY = 0D;
            this.zgGraph.ScrollMaxY2 = 0D;
            this.zgGraph.ScrollMinX = 0D;
            this.zgGraph.ScrollMinY = 0D;
            this.zgGraph.ScrollMinY2 = 0D;
            this.zgGraph.Size = new System.Drawing.Size(559, 472);
            this.zgGraph.TabIndex = 0;
            // 
            // frmMessageStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 472);
            this.Controls.Add(this.zgGraph);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMessageStats";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Statistics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMessageStats_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgGraph;

    }
}