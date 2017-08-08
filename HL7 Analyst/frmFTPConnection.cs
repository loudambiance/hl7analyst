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
using FTPLib;

namespace HL7_Analyst
{
    /// <summary>
    /// FTP Connection Form: Used to add a new connection options file.
    /// </summary>
    public partial class frmFTPConnection : Form
    {
        /// <summary>
        /// The FTPOptions filled in by the user
        /// </summary>
        public FTPOptions ftpOps = new FTPOptions();
        /// <summary>
        /// The Connection Name entered by the user
        /// </summary>
        public string ConnectionName = "";
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmFTPConnection()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Form Load Event: Sets the URL Textbox Selected Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFTPConnection_Load(object sender, EventArgs e)
        {
            try
            {
                txtURL.SelectionStart = txtURL.Text.Length;
                txtURL.SelectionLength = 1;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Use Anonymous Checked Changed Event: If Checked it disables User Name Password, if unchecked it enables them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUseAnonymous_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbUseAnonymous.Checked)
                {
                    txtUserName.Text = "Anonymous";
                    txtPassword.Text = "";
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else
                {
                    txtUserName.Enabled = true;
                    txtPassword.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Saves the FTP Connection Options File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtName.Text))
                {
                    if (txtURL.Text.Length > 6 && txtURL.Text.Contains("ftp://"))
                    {
                        ftpOps.AnonymousLogin = cbUseAnonymous.Checked;
                        ftpOps.UseSSL = cbUseSSL.Checked;
                        ftpOps.UsePassive = cbUsePassive.Checked;
                        ftpOps.FTPAddress = txtURL.Text;
                        ftpOps.UserName = txtUserName.Text;
                        ftpOps.UserPassword = txtPassword.Text;
                        ConnectionName = txtName.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a connection url.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a connection name.");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Cancel Button Click Event: Closes the dialog box with a DialogResult of cancelled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }        
    }
}
