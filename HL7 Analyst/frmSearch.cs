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
using System.IO;
using System.Windows.Forms;
using HL7Lib.Base;

namespace HL7_Analyst
{
    /// <summary>
    /// Search Form: Searches files in specified locations for messages containing the search terms.
    /// </summary>
    public partial class frmSearch : Form
    {
        /// <summary>
        /// The Messages being returned after the search
        /// </summary>
        public List<string> Messages = new List<string>();
        List<string> Extensions = new List<string>();
        List<string> PreviousSearches = new List<string>();
        BackgroundWorker bgw = new BackgroundWorker();

        private delegate void UpdateCurrentFileDelegate(string item);
        private delegate void UpdateMatchCountDelegate(string item);
        private delegate void CloseFormDelegate();
        /// <summary>
        /// Initialization Method
        /// </summary>        
        public frmSearch(string st)
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(st))
                txtSearchTerms.Text = st;
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Update the current file label
        /// </summary>
        /// <param name="v">The file to update to</param>
        private void UpdateCurrentFile(string v)
        {
            try
            {
                if (lblCurrentFile.IsHandleCreated)
                {
                    if (lblCurrentFile.InvokeRequired)
                    {
                        lblCurrentFile.Invoke(new UpdateCurrentFileDelegate(UpdateCurrentFile), v);
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(v);
                        lblCurrentFile.Text = fi.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Updates the match count label
        /// </summary>
        /// <param name="v">The count to update to</param>
        private void UpdateMatchCount(string v)
        {
            try
            {
                if (lblMatchCount.IsHandleCreated)
                {
                    if (lblMatchCount.InvokeRequired)
                        lblMatchCount.Invoke(new UpdateMatchCountDelegate(UpdateMatchCount), v);
                    else
                        lblMatchCount.Text = v;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Closes the form
        /// </summary>
        private void CloseForm()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new CloseFormDelegate(CloseForm));
                    else
                        this.Close();
                }
            }
            catch (ObjectDisposedException)
            {
                CloseForm();
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Form Load Event: Loads settings and sets up Background Worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                Settings s = new Settings();
                s.GetSettings();
                txtSearchPath.Text = s.SearchPath;
                Extensions = s.Extensions;

                PreviousSearches = SearchTerm.PullPreviousQueries();
                txtSearchTerms.AutoCompleteCustomSource.AddRange(PreviousSearches.ToArray());

                bgw.WorkerSupportsCancellation = true;
                bgw.WorkerReportsProgress = true;
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Background Worker Do Work Event: Performs search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string root = txtSearchPath.Text;
                int matchCount = 0;
                foreach (string ext in Extensions)
                {
                    if (!e.Cancel)
                    {
                        foreach (string file in Directory.GetFiles(root, "*." + ext, SearchOption.TopDirectoryOnly))
                        {
                            UpdateCurrentFile(file);
                            if (!e.Cancel)
                            {
                                List<string> m = SearchFile(file);
                                if (m.Count > 0)
                                {
                                    foreach (string msg in m)
                                    {
                                        if (!e.Cancel)
                                        {
                                            matchCount++;
                                            UpdateMatchCount(matchCount.ToString());
                                            Messages.Add(msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (string d in clbSubFolders.CheckedItems)
                        {
                            if (!e.Cancel)
                            {
                                foreach (string f in Directory.GetFiles(Path.Combine(root, d), "*." + ext, SearchOption.TopDirectoryOnly))
                                {
                                    UpdateCurrentFile(f);
                                    if (!e.Cancel)
                                    {
                                        List<string> m = SearchFile(f);
                                        if (m.Count > 0)
                                        {
                                            foreach (string msg in m)
                                            {
                                                if (!e.Cancel)
                                                {
                                                    matchCount++;
                                                    UpdateMatchCount(matchCount.ToString());
                                                    Messages.Add(msg);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                this.DialogResult = DialogResult.OK;
                CloseForm();
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Populates the sub-folders selection box if the entered path exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                clbSubFolders.Items.Clear();
                if (Directory.Exists(txtSearchPath.Text))
                {
                    DirectoryInfo di = new DirectoryInfo(txtSearchPath.Text);
                    foreach (DirectoryInfo d in di.GetDirectories())
                    {
                        clbSubFolders.Items.Add(d.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Cancels the background workers operations and closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (bgw.IsBusy) bgw.CancelAsync();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Starts the search using the background worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            cbSearchAll.Enabled = false;
            clbSubFolders.Enabled = false;
            txtSearchPath.Enabled = false;
            txtSearchTerms.Enabled = false;
            btnComponents.Enabled = false;
            btnSearchPath.Enabled = false;
            StartSearch();
        }
        /// <summary>
        /// Opens the Build Search Query form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComponents_Click(object sender, EventArgs e)
        {
            try
            {
                frmBuildSearch fbs = new frmBuildSearch(txtSearchTerms.Text);
                DialogResult dr = fbs.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    txtSearchTerms.Text = fbs.returnSearch.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
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

        private void txtSearchTerms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    StartSearch();
                }
            }
        }

        private void frmSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            SearchTerm.SavePreviousQueries(PreviousSearches);
        }

        private void cbSearchAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchAll.Checked)
            {
                for (int i = 0; i < clbSubFolders.Items.Count; i++)
                    clbSubFolders.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < clbSubFolders.Items.Count; i++)
                    clbSubFolders.SetItemChecked(i, false);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Searches the specified file
        /// </summary>
        /// <param name="f">The file to search</param>
        /// <returns>A list of messages that match search query</returns>
        private List<string> SearchFile(string f)
        {
            try
            {
                List<string> msgList = new List<string>();
                FileInfo fi = new FileInfo(f);
                StreamReader sr = new StreamReader(fi.FullName);
                string contents = sr.ReadToEnd();
                sr.Close();

                string[] msgs = contents.Split(new string[] { "MSH|" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string msg in msgs)
                {
                    string m = "MSH|" + msg;
                    HL7Lib.Base.Message message = new HL7Lib.Base.Message(m);
                    if (SearchMessage(message))
                    {
                        msgList.Add(message.DisplayString);
                    }
                }
                return msgList;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return new List<string>();
            }
        }
        /// <summary>
        /// Searches the message using the search query.
        /// </summary>
        /// <param name="m">The message to search</param>
        /// <returns>Returns true if the message matches the search query</returns>
        private bool SearchMessage(HL7Lib.Base.Message m)
        {
            try
            {
                bool returnValue = false;

                foreach (string item in txtSearchTerms.Text.Split('|'))
                {
                    bool allMatched = false;
                    List<SearchTerm> searchTerms = SearchTerm.GetSearchTerms(item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                    foreach (SearchTerm st in searchTerms)
                    {
                        HL7Lib.Base.Component c = m.GetByID(st.ID, st.Value.ToUpper());
                        if (!String.IsNullOrEmpty(c.ID))
                        {
                            allMatched = true;
                        }
                        else
                        {
                            allMatched = false;
                            break;
                        }
                    }
                    if (allMatched)
                    {
                        returnValue = true;
                        break;
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return false;
            }
        }

        private void StartSearch()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtSearchPath.Text) && !String.IsNullOrEmpty(txtSearchTerms.Text))
                {
                    if (!PreviousSearches.Contains(txtSearchTerms.Text))
                    {
                        PreviousSearches.Add(txtSearchTerms.Text);
                    }
                    this.Cursor = Cursors.WaitCursor;
                    btnSearch.Enabled = false;
                    bgw.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        #endregion        
    }
}
