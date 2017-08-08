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
using HL7Lib.Base;
using System.ComponentModel;
using System.IO;

namespace HL7_Analyst
{
    /// <summary>
    /// Reports Form: Displays selected report values in a data grid.
    /// </summary>
    public partial class frmReports : Form
    {
        List<string> Messages = new List<string>();
        string ReportName = "";

        private delegate void AddColumnsDelegate(ReportColumn columnName);
        private delegate void AddRowsDelegate(List<object> objs);
        private delegate void UpdateFormTextDelegate(string s);
        private delegate void UpdateFormCursorDelegate(Cursor c);
        /// <summary>
        /// Initialization Method: Sets the messages and report name at runtime
        /// </summary>
        /// <param name="Msgs">Messages to pull records from</param>
        /// <param name="RN">Report name to use.</param>
        public frmReports(List<string> Msgs, string RN)
        {
            InitializeComponent();
            Messages = Msgs;
            ReportName = RN;
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Adds columns to the data grid
        /// </summary>
        /// <param name="columnName">The column values to add</param>
        private void AddColumns(ReportColumn columnName)
        {
            try
            {
                if (dgvReportItems.IsHandleCreated)
                {
                    if (dgvReportItems.InvokeRequired)
                        dgvReportItems.Invoke(new AddColumnsDelegate(AddColumns), columnName);
                    else
                        dgvReportItems.Columns.Add(columnName.Name, columnName.Header);
                }
            }

            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Adds rows to the data grid
        /// </summary>
        /// <param name="objs">The grid row object array to add</param>
        private void AddRows(List<object> objs)
        {
            try
            {
                if (dgvReportItems.IsHandleCreated)
                {
                    if (dgvReportItems.InvokeRequired)
                        dgvReportItems.Invoke(new AddRowsDelegate(AddRows), objs);
                    else
                        dgvReportItems.Rows.Add(objs.ToArray());
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Updates the forms title text
        /// </summary>
        /// <param name="s">The text to update to</param>
        private void UpdateFormText(string s)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new UpdateFormTextDelegate(UpdateFormText), s);
                    else
                        this.Text = s;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Updates the forms cursor to the specified cursor
        /// </summary>
        /// <param name="c">Cursor to update to</param>
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
        /// Form load event: Sets up background worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReports_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Report - " + ReportName;
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                bgw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Background Worker Do Work Event: Adds the reports rows to the data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateFormCursor(Cursors.WaitCursor);
                Reports r = new Reports();
                r.LoadReport(ReportName, Messages);
                foreach (ReportColumn rc in r.Columns)
                    AddColumns(rc);
                for (int i = 0; i < r.Items.Count; i++)
                {
                    List<object> objs = new List<object>();
                    for (int x = 0; x < r.Items[i].Count; x++)
                    {
                        objs.Add(FormatItem(r.Items[i][x]));
                    }
                    AddRows(objs);
                }
                UpdateFormText(String.Format("Reports - {0} - {1} Records Displayed", ReportName, r.Items.Count));
                UpdateFormCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens a save file dialog and passes the selected file name to the SaveReport method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveReport();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Data Grid View Report Items Key Down Event: Sets up shortcut key support for saving the report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReportItems_KeyDown(object sender, KeyEventArgs e)
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

        #region Private Methods
        /// <summary>
        /// FormatItem Method: Formats the value string for display
        /// </summary>
        /// <param name="item">Item Text</param>
        /// <returns>Returns the formatted object</returns>
        private object FormatItem(string item)
        {
            try
            {
                object returnObj;
                Nullable<DateTime> d = item.FromHL7Date();
                if (d != null)
                    returnObj = d.Value.ToString("MM/dd/yyyy HH:mm:ss");
                else
                    returnObj = item;

                return returnObj;
            }
            catch (Exception)
            {
                return (object)item;
            }
        }
        /// <summary>
        /// Saves the report to disk
        /// </summary>        
        private void SaveReport()
        {
            try
            {
                 DialogResult dr = sfdSaveReport.ShowDialog();

                 if (dr == DialogResult.OK)
                 {
                     StreamWriter sw = new StreamWriter(sfdSaveReport.FileName);
                     foreach (DataGridViewColumn column in dgvReportItems.Columns)
                     {
                         sw.Write(String.Format("{0},", column.HeaderText));
                     }
                     sw.Write("\r\n");
                     foreach (DataGridViewRow row in dgvReportItems.Rows)
                     {
                         foreach (DataGridViewCell cell in row.Cells)
                         {
                             sw.Write(String.Format("\"{0}\",", cell.Value));
                         }
                         sw.Write("\r\n");
                     }
                     sw.Close();
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
