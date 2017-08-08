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
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// New Report Form: Gathers the new report name
    /// </summary>
    public partial class frmNewReport : Form
    {
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmNewReport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Report Name Texbox Key Down Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReportName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
