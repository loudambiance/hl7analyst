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
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// Options Form
    /// </summary>
    public partial class frmOptions : Form
    {
        /// <summary>
        /// Initialization Method
        /// </summary>
        public frmOptions()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Form Load Event: Sets up current settings and displays them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOptions_Load(object sender, EventArgs e)
        {
            try
            {
                Settings s = new Settings();
                s.GetSettings();
                cbHideEmpty.Checked = s.HideEmptyFields;
                cbCheckForUpdates.Checked = s.CheckForUpdates;
                txtSearchPath.Text = s.SearchPath;
                txtDefaultSegment.Text = s.DefaultSegment.ToString();
                foreach (string ext in s.Extensions)
                    txtExtensions.Text += ext + "\r\n";
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Search Path Button Click Event: Displays an folder browser dialog and sets the search path textbox to the selected folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchPath_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = fbSearchPath.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    txtSearchPath.Text = fbSearchPath.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Saves the settings changes that were made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Settings s = new Settings();
                s.HideEmptyFields = cbHideEmpty.Checked;
                s.CheckForUpdates = cbCheckForUpdates.Checked;
                s.SearchPath = txtSearchPath.Text;
                s.Extensions = new List<string>();
                s.DefaultSegment = Settings.ConvertToSegments(txtDefaultSegment.Text);
                string[] exts = txtExtensions.Text.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ext in exts)
                    s.Extensions.Add(ext);
                s.SaveSettings();
                this.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }                
    }
}
