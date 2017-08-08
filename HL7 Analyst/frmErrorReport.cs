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
using System.Diagnostics;
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// Error Report Form: Displays the error that was passed to it and allows user to send error report to hl7analyst@gmail.com
    /// </summary>
    public partial class frmErrorReport : Form
    {
        /// <summary>
        /// Initialization Method: Sets the text displays to the values of the error message.
        /// </summary>
        /// <param name="err">Error that was thrown.</param>
        public frmErrorReport(Exception err)
        {
            InitializeComponent();
            txtErrorMessage.Text = err.Message;
            txtStackTrace.Text = err.StackTrace;
        }
        /// <summary>
        /// Send Button Click Event: Opens users default mail application and sets a message to to errors values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(String.Format("mailto:hl7analyst@gmail.com?subject=An Error Has Occurred&body={0}%0A{1}", txtErrorMessage.Text, txtStackTrace.Text));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Cancel Button Click Event: Closes Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
