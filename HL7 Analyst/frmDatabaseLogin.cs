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
using System.Text;
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// Database Login Form: Sets up the database connection string object to use for this database connection
    /// </summary>
    public partial class frmDatabaseLogin : Form
    {
        /// <summary>
        /// The SQL Connection String built from the information entered into this form
        /// </summary>
        public StringBuilder SQLConnectionString = new StringBuilder();
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmDatabaseLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Authentication Type Selected Index Changed Event: Based on the selected index sets the User Name and Password textboxes to enabled or not-enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAuthenticationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbAuthenticationType.SelectedIndex > -1)
                {
                    bool enabled = false;
                    if (cbAuthenticationType.SelectedIndex == 0)
                        enabled = false;
                    else
                        enabled = true;
                    txtUserName.Enabled = enabled;
                    txtPassword.Enabled = enabled;

                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Form Load Event: Sets the default selected authentication type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDatabaseLogin_Load(object sender, EventArgs e)
        {
            try
            {
                cbAuthenticationType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sets up the connection object to pass to the database connection form then closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SQLConnectionString.AppendFormat("Data Source={0};Initial Catalog={1};", txtServer.Text, txtDatabase.Text);
                if (cbAuthenticationType.SelectedIndex == 0)
                    SQLConnectionString.AppendFormat("Integrated Security=SSPI;");
                else
                    SQLConnectionString.AppendFormat("User ID={0};Password={1};", txtUserName.Text, txtPassword.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
