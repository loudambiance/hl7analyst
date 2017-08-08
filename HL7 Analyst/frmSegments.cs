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

using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System;

namespace HL7_Analyst
{
    /// <summary>
    /// Filled Segments/Fields/Components Form
    /// </summary>
    public partial class frmSegments : Form
    {
        List<string> msgs = new List<string>();
        private delegate void AddListViewItemsDelegate(ListViewItem lvi);
        private delegate void UpdateFormCursorDelegate(Cursor c);
        /// <summary>
        /// Initialization Method
        /// </summary>
        /// <param name="Messages">The messages to check</param>
        public frmSegments(List<string> Messages)
        {
            InitializeComponent();
            msgs = Messages;
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Add list view item to filled fields list view
        /// </summary>
        /// <param name="lvi">List view item</param>
        private void AddListViewItems(ListViewItem lvi)
        {
            try
            {
                if (lvItems.IsHandleCreated)
                {
                    if (lvItems.InvokeRequired)
                        lvItems.Invoke(new AddListViewItemsDelegate(AddListViewItems), lvi);
                    else
                        lvItems.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Update forms cursor
        /// </summary>
        /// <param name="c">Cursor to use</param>
        private void UpdateFormCursor(Cursor c)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new UpdateFormCursorDelegate(UpdateFormCursor), c);
                    else
                        this.Cursor = c;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Form Load Event: sets up Background Worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSegments_Load(object sender, System.EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync();
        }
        /// <summary>
        /// Background Worker Do Work Event: fills in the filled fields list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateFormCursor(Cursors.WaitCursor);
                List<FilledFields> fieldList = FilledFields.Calculate(msgs);
                foreach (FilledFields ff in fieldList)
                {
                    ListViewItem lvi = new ListViewItem(ff.ID);
                    lvi.SubItems.Add(ff.Name);
                    lvi.SubItems.Add(ff.MinLength.ToString());
                    lvi.SubItems.Add(ff.AvergeLength.ToString());
                    lvi.SubItems.Add(ff.MaxLength.ToString());
                    AddListViewItems(lvi);
                }
                UpdateFormCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Saves the report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveReport_Click(object sender, System.EventArgs e)
        {
            SaveReport();
        }
        /// <summary>
        /// List View Item Key Down Event: Sets up shortcut key support for saving the report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.S:
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            SaveReport();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        #endregion

        /// <summary>
        /// Saves the current support to disk
        /// </summary>
        private void SaveReport()
        {
            try
            {
                DialogResult dr = sfdSaveReport.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfdSaveReport.FileName);
                    sw.WriteLine("Component ID,Name,Minimum Length,Average Length,Maximum Length");
                    foreach (ListViewItem lvi in lvItems.Items)
                        sw.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"", lvi.Text, lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text, lvi.SubItems[4].Text));
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
