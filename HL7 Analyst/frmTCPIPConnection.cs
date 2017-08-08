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
using System.Net;
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// TCPIP Connection Form: Used to create a new TCP/IP Connection File
    /// </summary>
    public partial class frmTCPIPConnection : Form
    {
        /// <summary>
        /// The TCPIPOptions filled in by the user
        /// </summary>
        public TCPIPOptions TCPIPOps = new TCPIPOptions();
        /// <summary>
        /// The Connection Name entered by the user
        /// </summary>
        public string OptionsName = "";
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmTCPIPConnection()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Save Button Click Event: Saves the TCP/IP Options to File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtName.Text))
                {
                    IPAddress ip = ParseHostAddress(txtHostAddress.Text);
                    int port = ParsePort(txtPort.Text);

                    if (ip != null)
                    {
                        if (port != 0)
                        {
                            OptionsName = txtName.Text;
                            TCPIPOps.HostAddress = ip;
                            TCPIPOps.Port = port;
                            TCPIPOps.LLPHeader = LLP.GetLLPString(txtHeader.Text);
                            TCPIPOps.LLPTrailer = LLP.GetLLPString(txtTrailer.Text);
                            TCPIPOps.WaitForAck = cbWaitForAck.Checked;
                            TCPIPOps.SendAck = cbSendAck.Checked;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Please Add Port.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add Host Address.");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter a Unique Name for this Connection.");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Cancels the Dialog Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
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
        /// <summary>
        /// Parses a Host Address from a string
        /// </summary>
        /// <param name="s">The string to parse</param>
        /// <returns>The IPAddress</returns>
        private IPAddress ParseHostAddress(string s)
        {
            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(s, out ip))
                    return ip;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return null;
            }
        }
        /// <summary>
        /// Parses a port
        /// </summary>
        /// <param name="s">The string to parse</param>
        /// <returns>The port int</returns>
        private int ParsePort(string s)
        {
            try
            {
                int p = 0;
                if (Int32.TryParse(s, out p))
                    return p;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return 0;
            }
        }
        /// <summary>
        /// Loads the LLP form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeader_Click(object sender, EventArgs e)
        {
            try
            {
                frmLLP fl = new frmLLP();
                DialogResult dr = fl.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    foreach (LLP l in fl.LLPList)
                        txtHeader.Text += l.Hex;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the LLP Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrailer_Click(object sender, EventArgs e)
        {
            try
            {
                frmLLP fl = new frmLLP();
                DialogResult dr = fl.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    foreach (LLP l in fl.LLPList)
                        txtTrailer.Text += l.Hex;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
