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

using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.IO;

namespace HL7_Analyst
{
    /// <summary>
    /// About Form, displays copyright, version, and additional application information.
    /// </summary>
    public partial class frmAbout : Form
    {
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmAbout()
        {
            InitializeComponent();            
        }
        /// <summary>
        /// Form Load Event: Sets text displays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAbout_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblCopyright.Text = String.Format("Copyright {0} Jeremy Reagan, All Rights Reserved.", 2011);
                lblVersion.Text = String.Format("Version {0}", Application.ProductVersion);
                txtNotice.Text = "This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; under version 2 of the License.\r\n\r\nThis program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.";
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Link Label Click Event: Calls default internet browser to navigate to link site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFamFamFam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://www.famfamfam.com/lab/icons/silk");
            }                
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Link Label Click Event: Calls default internet browser to navigate to link site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblIconArchive_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://www.iconarchive.com/category/application/ekisho-icons-by-jonas-rask.html");
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Link Label Click Event: Calls default internet browser to navigate to link site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblZedGraph_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://sourceforge.net/projects/zedgraph/");
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// License Button Click Event: Currently Does Nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLicense_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "License.txt");
                if (File.Exists(path))
                {
                    Process.Start(path);
                }
                else
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(HL7_Analyst.Properties.Resources.License);
                    sw.Close();
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
