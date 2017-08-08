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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace HL7_Analyst
{
    /// <summary>
    /// Displays if an update is available
    /// </summary>
    public partial class frmUpdateAvailable : Form
    {
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmUpdateAvailable()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Link label click event: Navigates to the download location on CodePlex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblDownloadLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://hl7analyst.codeplex.com/releases");
        }
        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
