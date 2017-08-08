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
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using FTPLib;
using HL7Lib.Base;
using System.Diagnostics;

namespace HL7_Analyst
{
    /// <summary>
    /// Main application form, displays HL7 messages in an understandable format and allows for analysis work to be performed on displayed messages.
    /// </summary>
    public partial class frmMain : Form
    {
        List<string> Messages = new List<string>();
        List<string> AllMessages = new List<string>();
        List<string> Extensions = new List<string>();
        List<string> FTPFiles = new List<string>();
        TCPIPOptions LoadedTCPIPOptions = new TCPIPOptions();
        FTPOptions LoadedFTPOptions = new FTPOptions();
        DatabaseOptions LoadedDBOptions = new DatabaseOptions();
        int currentMessage = 0;
        bool RunTCPIPServerLoop = true;

        private delegate void SegmentDisplayClearItemsDelegate();
        private delegate void SegmentChangerClearItemsDelegate();
        private delegate void MessageTotalSetTextDelegate(string s);
        private delegate void MessageBoxSetTextDelegate(string s);
        private delegate void CurrentMessageSetTextDelegate(string s);
        private delegate void FormSetTextDelegate(string s);
        private delegate void SegmentChangerAddItemDelegate(object item);
        private delegate void SegmentDisplayAddItemDelegate(ListViewItem item);
        private delegate void FormSetCursorDelegate(Cursor c);
        private delegate void TCPIPTransferDisplayAddRowDelegate(List<object> items);
        private delegate void MessageBoxSetFontFormatDelegate();
        /// <summary>
        /// Initialization Method
        /// </summary>        
        public frmMain()
        {
            InitializeComponent();
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Clears the segment display
        /// </summary>
        private void SegmentDisplayClearItems()
        {
            try
            {
                if (lvSegmentDisplay.IsHandleCreated)
                {
                    if (lvSegmentDisplay.InvokeRequired)
                        lvSegmentDisplay.Invoke(new SegmentDisplayClearItemsDelegate(SegmentDisplayClearItems));
                    else
                        lvSegmentDisplay.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Clears the segment changer
        /// </summary>
        private void SegmentChangerClearItems()
        {
            try
            {
                if (tsSegmentToolbar.IsHandleCreated)
                {
                    if (tsSegmentToolbar.InvokeRequired)
                        tsSegmentToolbar.Invoke(new SegmentChangerClearItemsDelegate(SegmentChangerClearItems));
                    else
                        cbSegmentChanger.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the Message Total text
        /// </summary>
        /// <param name="s">The string to use</param>
        private void MessageTotalSetText(string s)
        {
            try
            {
                if (tsSegmentToolbar.IsHandleCreated)
                {
                    if (tsSegmentToolbar.InvokeRequired)
                        tsSegmentToolbar.Invoke(new MessageTotalSetTextDelegate(MessageTotalSetText), s);
                    else
                        txtMessageTotal.Text = s;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the message box text
        /// </summary>
        /// <param name="s">The string to use</param>
        private void MessageBoxSetText(string s)
        {
            try
            {
                if (rtbMessageBox.IsHandleCreated)
                {
                    if (rtbMessageBox.InvokeRequired)
                        rtbMessageBox.Invoke(new MessageBoxSetTextDelegate(MessageBoxSetText), s);
                    else
                        rtbMessageBox.Text = s;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the Current Message text
        /// </summary>
        /// <param name="s">The string to use</param>
        private void CurrentMessageSetText(string s)
        {
            try
            {
                if (tsSegmentToolbar.IsHandleCreated)
                {
                    if (tsSegmentToolbar.InvokeRequired)
                        tsSegmentToolbar.Invoke(new CurrentMessageSetTextDelegate(CurrentMessageSetText), s);
                    else
                        txtCurrentMessage.Text = s;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the forms text
        /// </summary>
        /// <param name="s">The string to use</param>
        private void FormSetText(string s)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new FormSetTextDelegate(FormSetText), s);
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
        /// Adds items to the Segment Changer
        /// </summary>
        /// <param name="item">The item to add</param>
        private void SegmentChangerAddItem(object item)
        {
            try
            {
                if (tsSegmentToolbar.IsHandleCreated)
                {
                    if (tsSegmentToolbar.InvokeRequired)
                        tsSegmentToolbar.Invoke(new SegmentChangerAddItemDelegate(SegmentChangerAddItem), item);
                    else
                        cbSegmentChanger.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Adds items to the Segment Display
        /// </summary>
        /// <param name="item">The item to add</param>
        private void SegmentDisplayAddItem(ListViewItem item)
        {
            try
            {
                if (lvSegmentDisplay.IsHandleCreated)
                {
                    if (lvSegmentDisplay.InvokeRequired)
                        lvSegmentDisplay.Invoke(new SegmentDisplayAddItemDelegate(SegmentDisplayAddItem), item);
                    else
                        lvSegmentDisplay.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the forms cursor
        /// </summary>
        /// <param name="c">The cursor to use</param>
        private void FormSetCursor(Cursor c)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new FormSetCursorDelegate(FormSetCursor), c);
                    else
                        this.Cursor = c;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Adds a row to the TCPIP Transfer Display
        /// </summary>
        /// <param name="items">The item to add</param>
        private void TCPIPTransferDisplayAddRow(List<object> items)
        {
            try
            {
                if (dgvTCPIPTransferDisplay.IsHandleCreated)
                {
                    if (dgvTCPIPTransferDisplay.InvokeRequired)
                        dgvTCPIPTransferDisplay.Invoke(new TCPIPTransferDisplayAddRowDelegate(TCPIPTransferDisplayAddRow), items);
                    else
                        dgvTCPIPTransferDisplay.Rows.Add(items.ToArray());
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Sets the font format across threads.
        /// </summary>
        private void MessageBoxSetFontFormat()
        {
            try
            {
                if (rtbMessageBox.IsHandleCreated)
                {
                    if (rtbMessageBox.InvokeRequired)
                        rtbMessageBox.Invoke(new MessageBoxSetFontFormatDelegate(MessageBoxSetFontFormat));
                    else
                        SetRTBTextFormatOptions();
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
        /// Form Load Event: Loads application settings and the currently stored reports.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Settings settings = new Settings();
                settings.GetSettings();
               /* if (settings.CheckForUpdates)
                {
                    if (UpdateChecker.UpdateCheck())
                    {
                        frmUpdateAvailable fua = new frmUpdateAvailable();
                        fua.ShowDialog();
                    }
                    UpdateChecker.SaveLastRunDate();
                }*/
                tsmHideEmpty.Checked = settings.HideEmptyFields;
                Extensions = settings.Extensions;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < settings.Extensions.Count; i++)
                {
                    if (i != settings.Extensions.Count - 1)
                        sb.Append(settings.Extensions[i].ToUpper() + "|" + "*." + settings.Extensions[i] + "|");
                    else
                        sb.Append(settings.Extensions[i].ToUpper() + "|" + "*." + settings.Extensions[i]);
                }
                sb.Append("|All|*.*");
                ofdOpenFiles.Filter = sb.ToString();
                if (Directory.Exists(Path.Combine(Application.StartupPath, "Reports")))
                {
                    foreach (string f in Directory.GetFiles(Path.Combine(Application.StartupPath, "Reports"), "*.xml", SearchOption.TopDirectoryOnly))
                    {
                        FileInfo fi = new FileInfo(f);
                        cbReportSelector.Items.Add(fi.Name.Replace(".xml", ""));
                    }
                }
                foreach (string FTPConnectionFile in FTPOptions.GetFTPConnections())
                    cbFTPConnections.Items.Add(FTPConnectionFile);
                foreach (string TCPIPConnectionFile in TCPIPOptions.GetTCPIPConnections())
                    cbTCPIPConnections.Items.Add(TCPIPConnectionFile);
                foreach (string DatabaseConnectionFile in DatabaseOptions.GetDatabaseConnections())
                    cbDatabaseConnections.Items.Add(DatabaseConnectionFile);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Open File(s) Tool Strip Menu Item Click Event: Displays an Open File Dialog and Opens The Selected File(s) after dialog closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = ofdOpenFiles.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    BackgroundWorker LoadFilesBGW = new BackgroundWorker();
                    LoadFilesBGW.DoWork += new DoWorkEventHandler(LoadFilesBGW_DoWork);
                    LoadFilesBGW.RunWorkerAsync(ofdOpenFiles.FileNames);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads all selected files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LoadFilesBGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FormSetCursor(Cursors.WaitCursor);
                string[] files = (string[])e.Argument;
                foreach (string f in files)
                    SetMessage(f);
                currentMessage = 0;
                SetMessageDisplay(currentMessage);
                FormSetCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Open Folder Tool Stip Menu Item Click Event: Displays an Folder Selector Dialog and opens the files in selected folder and sub-folders after dialog closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = fbOpenFolder.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    BackgroundWorker LoadFoldersBGW = new BackgroundWorker();
                    LoadFoldersBGW.DoWork += new DoWorkEventHandler(LoadFoldersBGW_DoWork);
                    LoadFoldersBGW.RunWorkerAsync(fbOpenFolder.SelectedPath);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads all files in each folder in selected folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LoadFoldersBGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FormSetCursor(Cursors.WaitCursor);
                foreach (string ext in Extensions)
                {
                    foreach (string f in Directory.GetFiles((string)e.Argument, "*." + ext, SearchOption.AllDirectories))
                    {
                        SetMessage(f);
                    }
                }
                currentMessage = 0;
                SetMessageDisplay(currentMessage);
                FormSetCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Search For File(s) Tool Stip Menu Item Click Event: Calls the frmSearch dialog box to search for files, opens the returned files after dialog closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchForFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForFiles("");
        }
        /// <summary>
        /// Close Tool Stip Menu Item Click Event: Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Remove Message Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeMessage_Click(object sender, EventArgs e)
        {
            RemoveMessage();
        }
        /// <summary>
        /// Clear Session Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearSession_Click(object sender, EventArgs e)
        {
            ClearSessionDisplay();
        }
        /// <summary>
        /// Filter Records Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterRecords_Click(object sender, EventArgs e)
        {
            FilterRecords();
        }
        /// <summary>
        /// Clear Filter Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearFilter_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }
        /// <summary>
        /// First Record Click Event (Used by Tool Bar Button, Tool Strip Menu, and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void first_Click(object sender, EventArgs e)
        {
            DisplayFirstRecord();
        }
        /// <summary>
        /// Previous Record Click Event (Used by Tool Bar Button, Tool Strip Menu, and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previous_Click(object sender, EventArgs e)
        {
            DisplayPreviousRecord();
        }
        /// <summary>
        /// Next Record Click Event  (Used by Tool Bar Button, Tool Strip Menu, and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, EventArgs e)
        {
            DisplayNextRecord();
        }
        /// <summary>
        /// Last Record Click Event  (Used by Tool Bar Button, Tool Strip Menu, and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void last_Click(object sender, EventArgs e)
        {
            DisplayLastRecord();
        }
        /// <summary>
        /// Toggle Segment Display Click Event  (Used by Tool Bar Button, Tool Strip Menu, and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleSegmentDisplay_Click(object sender, EventArgs e)
        {
            SetSegmentDisplay();
        }
        /// <summary>
        /// Create Report Click Event  (Used by Tool Bar Button, Tool Strip Menu, and Context Menu): Builds list of ListViewItem(s) and then calls CreateReport Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvSegmentDisplay.SelectedIndices.Count > 0)
                {
                    List<ListViewItem> lvis = new List<ListViewItem>();
                    foreach (ListViewItem lvi in lvSegmentDisplay.SelectedItems)
                        lvis.Add(lvi);
                    CreateReport(lvis);
                }
                else
                {
                    MessageBox.Show("You must select which fields to create a report from to create a report.");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Delete Report Click Event: Calls the DeleteReport Method of the Reports class and then removes the item from the Report Selector box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbReportSelector.SelectedIndex > -1)
                {
                    Reports.DeleteReport(cbReportSelector.SelectedItem.ToString());
                    cbReportSelector.Items.Remove(cbReportSelector.SelectedItem);
                    cbReportSelector.Text = "";
                }
                else
                {
                    MessageBox.Show("You must select a report to delete a report.");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// View Filled Components Click Event: Calls the frmSegments form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewUsedComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Messages.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                frmSegments fs = new frmSegments(Messages);
                fs.Show();
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("You must open a message file to display the filled fields.");
            }
        }
        /// <summary>
        /// View Message Statistics Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewMessageTypeStatistics_Click(object sender, EventArgs e)
        {
            ViewStatistics();
        }
        /// <summary>
        /// View Hourly Traffic Statistics Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewHourlyTrafficStatistics_Click(object sender, EventArgs e)
        {
            ViewHourlyTrafficStatistics();
        }
        /// <summary>
        /// View Daily Traffic Statistics Click Event (Used by Tool Strip Menu and Context Menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewDailyTrafficStatistics_Click(object sender, EventArgs e)
        {
            ViewDailyTrafficStatistics();
        }
        /// <summary>
        /// Hide Empty Components Check State Changed Event: Removes empty components from the Segment Display List View for the current message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHideEmpty_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (Messages.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lvSegmentDisplay.Items.Clear();
                    HL7Lib.Base.Message m = new HL7Lib.Base.Message(Messages[currentMessage]);
                    foreach (Segment s in m.Segments)
                    {
                        SetListViewDisplay(s);
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Show Options Menu Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions fo = new frmOptions();
            fo.ShowDialog();
        }
        /// <summary>
        /// Show About Box Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.Show();
        }
        /// <summary>
        /// Segment Selector Selected Index Changed Event: Clears the Segment Display List View of Segments and Displays the Selected Segment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSegmentChanger_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbSegmentChanger.SelectedIndex > -1)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lvSegmentDisplay.Items.Clear();
                    HL7Lib.Base.Message m = new HL7Lib.Base.Message(Messages[currentMessage]);
                    if (cbSegmentChanger.SelectedItem.ToString().ToUpper() != "ALL SEGMENTS")
                    {
                        foreach (Segment s in m.Segments.Get(cbSegmentChanger.SelectedItem.ToString()))
                            SetListViewDisplay(s);
                    }
                    else
                    {
                        foreach (Segment s in m.Segments)
                        {
                            SetListViewDisplay(s);
                        }
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Current Message Text Box KeyDown Event: Sets the current message to the entered number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCurrentMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int outInt = 0;
                    if (Int32.TryParse(txtCurrentMessage.Text, out outInt))
                    {
                        currentMessage = outInt - 1;
                        if (currentMessage > -1 && currentMessage < Messages.Count)
                            SetMessageDisplay(currentMessage);
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sets support for copying Segment Display Values to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSegmentDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvSegmentDisplay.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.C && e.Control)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ListViewItem lvi in lvSegmentDisplay.SelectedItems)
                    {
                        if (lvi.SubItems.Count > 2)
                        {
                            sb.Append(lvi.SubItems[2].Text);
                            sb.Append("\r\n");
                        }
                    }
                    Clipboard.SetText(sb.ToString());
                }
            }
        }
        /// <summary>
        /// Segment Display Double Click Event: Calls EditFieldValues Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSegmentDisplay_DoubleClick(object sender, EventArgs e)
        {
            EditFieldValues();
        }
        /// <summary>
        /// Edit Selected Field Menu Item Click Event: Calls EditFieldValues Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editSelectedFieldValues_Click(object sender, EventArgs e)
        {
            EditFieldValues();
        }
        /// <summary>
        /// Save Current Message Menu Item Click Event: Opens a Save File Dialog and then saves the file specified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveCurrentMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = sfdSaveFile.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    SaveMessage(rtbMessageBox.Text, sfdSaveFile.FileName);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Save All Messages Menu Item Click Event: Opens a folder selection dialog and then saves all messages to that folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAllMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = fbOpenFolder.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < Messages.Count; i++)
                    {
                        string f = Path.Combine(fbOpenFolder.SelectedPath, String.Format("HL7 Analyst {0}{1}.hl7", DateTime.Now.ToString("MMddyyyyHHmmss"), i));
                        SaveMessage(Messages[i], f);
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// DeIdentifiy Message Menu Click Event: Calls the DeIdentifyMessages Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deIdentifyMessages_Click(object sender, EventArgs e)
        {
            DeIdentifyMessages();
        }
        /// <summary>
        /// Takes the selected FTP Connection Options and downloads the folders and files from the FTP site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFTPConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbFTPConnections.SelectedIndex > -1)
                {
                    tvFTPDisplay.Nodes.Clear();
                    this.Cursor = Cursors.WaitCursor;
                    LoadedFTPOptions = FTPOptions.Load(cbFTPConnections.SelectedItem.ToString());
                    TreeNode root = new TreeNode(LoadedFTPOptions.FTPAddress);
                    List<string> dl = FTPOperations.ListDirs(LoadedFTPOptions, LoadedFTPOptions.FTPAddress);
                    List<string> fl = FTPOperations.ListFiles(LoadedFTPOptions, LoadedFTPOptions.FTPAddress, Extensions);

                    foreach (string d in dl)
                        root.Nodes.Add(d);
                    foreach (string f in fl)
                        root.Nodes.Add(f);
                    tvFTPDisplay.Nodes.Add(root);
                    this.Cursor = Cursors.Default;
                    btnFTPUpload.Enabled = true;
                    btnFTPDownload.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Downloads the selected folders files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFTPDisplay_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cbFTPConnections.SelectedIndex > -1)
                {
                    if (tvFTPDisplay.SelectedNode != null)
                    {
                        if (tvFTPDisplay.SelectedNode.Text.IndexOf(".") == -1)
                        {
                            if (tvFTPDisplay.SelectedNode.Nodes.Count == 0)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                TreeNode root = tvFTPDisplay.SelectedNode;

                                string subDir = root.FullPath.Replace("\\", "/");
                                List<string> dl = FTPOperations.ListDirs(LoadedFTPOptions, subDir);
                                List<string> fl = FTPOperations.ListFiles(LoadedFTPOptions, subDir, Extensions);

                                foreach (string d in dl)
                                    tvFTPDisplay.SelectedNode.Nodes.Add(d);
                                foreach (string f in fl)
                                    tvFTPDisplay.SelectedNode.Nodes.Add(f);
                                tvFTPDisplay.SelectedNode.Expand();
                                this.Cursor = Cursors.Default;
                            }
                        }
                        else
                        {
                            if (tvFTPDisplay.SelectedNode.Checked)
                                tvFTPDisplay.SelectedNode.Checked = false;
                            else
                                tvFTPDisplay.SelectedNode.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Creates and runs a background worker to download the selected files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFTPDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbFTPConnections.SelectedIndex > -1 && FTPFiles.Count > 0)
                {
                    BackgroundWorker FTPLoadFilesBGW = new BackgroundWorker();
                    FTPLoadFilesBGW.DoWork += new DoWorkEventHandler(FTPLoadFilesBGW_DoWork);
                    FTPLoadFilesBGW.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Downloads selected files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FTPLoadFilesBGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FormSetCursor(Cursors.WaitCursor);
                foreach (string FTPFile in FTPFiles)
                {
                    string contents = FTPOperations.Get(LoadedFTPOptions, FTPFile);
                    SetDownloadedMessage(contents);
                }
                if (Messages.Count == 1)
                {
                    currentMessage = 0;
                    SetMessageDisplay(currentMessage);
                }
                else
                {
                    MessageTotalSetText(String.Format("{0:0,0}", Messages.Count));
                }
                FormSetCursor(Cursors.Default);
                MessageBox.Show("Download Complete");
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Uploads all currently open messages to the FTP server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFTPUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbFTPConnections.SelectedIndex > -1 && Messages.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < Messages.Count; i++)
                    {
                        string fName = "";
                        if (tvFTPDisplay.SelectedNode != null)
                            fName = FTPOperations.Send(LoadedFTPOptions, Messages[i], tvFTPDisplay.SelectedNode.FullPath.Replace("\\", "/"), i);
                        else
                            fName = FTPOperations.Send(LoadedFTPOptions, Messages[i], LoadedFTPOptions.FTPAddress, i);
                        tvFTPDisplay.SelectedNode.Nodes.Add(fName);
                    }
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Upload Complete");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the FTPConnection form to add a new FTP connection file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFTPConnection_Click(object sender, EventArgs e)
        {
            try
            {
                frmFTPConnection ffc = new frmFTPConnection();
                DialogResult dr = ffc.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    FTPOptions ftpOps = ffc.ftpOps;
                    ftpOps.Save(ftpOps, Helper.RemoveUnsupportedChars(ffc.ConnectionName));
                    cbFTPConnections.Items.Add(Helper.RemoveUnsupportedChars(ffc.ConnectionName));
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Deletes the selected FTP connection file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteFTPConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbFTPConnections.SelectedIndex > -1)
                {
                    FTPOptions.Delete(cbFTPConnections.SelectedItem.ToString());
                    cbFTPConnections.Items.Remove(cbFTPConnections.SelectedItem);
                    cbFTPConnections.Text = "";
                }
            }
            catch (IOException) { }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the FTP/TCPIP Transfer panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (scSidePanel.Panel2Collapsed)
                    scSidePanel.Panel2Collapsed = false;
                else
                    scSidePanel.Panel2Collapsed = true;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// If the item being checked is a folder all files are checked if not it just checks the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFTPDisplay_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Text.IndexOf(".") == -1 && e.Node.Checked)
                {
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                        e.Node.Nodes[i].Checked = true;
                }
                else if (e.Node.Text.IndexOf(".") == -1 && !e.Node.Checked)
                {
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                        e.Node.Nodes[i].Checked = false;
                }
                else
                {
                    if (e.Node.Checked)
                        FTPFiles.Add(e.Node.FullPath.Replace("\\", "/"));
                    else
                        FTPFiles.Remove(e.Node.FullPath.Replace("\\", "/"));
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the selected Connection File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTCPIPConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbTCPIPConnections.SelectedIndex > -1)
                {
                    LoadedTCPIPOptions = TCPIPOptions.Load(cbTCPIPConnections.SelectedItem.ToString());
                    btnServer.Enabled = true;
                    btnClient.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Creates a Background Worker and runs the TCPListener in it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServer_Click(object sender, EventArgs e)
        {
            try
            {
                BackgroundWorker tcpListenerBGW = new BackgroundWorker();
                tcpListenerBGW.DoWork += new DoWorkEventHandler(tcpListenerBGW_DoWork);
                tcpListenerBGW.WorkerSupportsCancellation = true;
                if (btnServer.Text == "Start Server")
                {
                    RunTCPIPServerLoop = true;
                    tcpListenerBGW.RunWorkerAsync();
                    btnServer.Text = "Stop Server";
                    btnClient.Enabled = false;
                }
                else
                {
                    RunTCPIPServerLoop = false;
                    tcpListenerBGW.CancelAsync();
                    btnServer.Text = "Start Server";
                    btnClient.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Downloads all messages sent to this TCP/IP Listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tcpListenerBGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TcpListener listener = new TcpListener(LoadedTCPIPOptions.HostAddress, LoadedTCPIPOptions.Port);
                listener.Start();
                while (RunTCPIPServerLoop)
                {
                    if (!e.Cancel)
                    {
                        if (listener.Pending())
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            NetworkStream stream = client.GetStream();
                            byte[] messageBuffer = new byte[4096];
                            StringBuilder sb = new StringBuilder();
                            int bytesRead;

                            while ((bytesRead = stream.Read(messageBuffer, 0, messageBuffer.Length)) != 0)
                            {
                                if (!e.Cancel)
                                {
                                    sb.AppendFormat("{0}", Encoding.ASCII.GetString(messageBuffer));
                                    if (sb.ToString().Contains(LoadedTCPIPOptions.LLPHeader) && sb.ToString().Contains(LoadedTCPIPOptions.LLPTrailer))
                                    {
                                        string[] msgStrs = sb.ToString().Split(new string[] { LoadedTCPIPOptions.LLPHeader }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (string msg in msgStrs)
                                        {
                                            if (!e.Cancel)
                                            {
                                                if (msg.Contains(LoadedTCPIPOptions.LLPTrailer))
                                                {
                                                    List<object> dgvItems = new List<object>();
                                                    HL7Lib.Base.Message m = new HL7Lib.Base.Message(msg.Replace(LoadedTCPIPOptions.LLPHeader, ""));
                                                    dgvItems.Add(m.Segments.Get("MSH")[0].GetByID("MSH-10.1").Value);
                                                    if (LoadedTCPIPOptions.SendAck)
                                                    {
                                                        HL7Lib.Base.Message ack = HL7Lib.Base.Helper.CreateAck(m);
                                                        byte[] ackBuffer = Encoding.ASCII.GetBytes(String.Format("{0}{1}{2}", LoadedTCPIPOptions.LLPHeader, ack.DisplayString, LoadedTCPIPOptions.LLPTrailer));
                                                        stream.Write(ackBuffer, 0, ackBuffer.Length);
                                                        dgvItems.Add(true);
                                                    }
                                                    else
                                                    {
                                                        dgvItems.Add(false);
                                                    }
                                                    TCPIPTransferDisplayAddRow(dgvItems);
                                                    SetDownloadedMessage(m.DisplayString);
                                                    if (Messages.Count == 1)
                                                    {
                                                        currentMessage = Messages.Count - 1;
                                                        SetMessageDisplay(currentMessage);
                                                    }
                                                    else
                                                    {
                                                        MessageTotalSetText(String.Format("{0:0,0}", Messages.Count));
                                                    }
                                                }
                                                else
                                                {
                                                    sb = new StringBuilder();
                                                    sb.Append(msg);
                                                }
                                            }
                                            else
                                            {
                                                listener.Stop();
                                                listener.Server.Close();
                                                stream.Close();
                                                client.Close();
                                                break;
                                            }
                                            sb = sb.Replace(msg, "");
                                        }
                                    }
                                }
                                else
                                {
                                    listener.Stop();
                                    listener.Server.Close();
                                    stream.Close();
                                    client.Close();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        listener.Stop();
                        listener.Server.Close();
                        break;
                    }
                }
                listener.Stop();
                listener.Server.Close();
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Creates a Background Worker and runs it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClient_Click(object sender, EventArgs e)
        {
            try
            {
                BackgroundWorker tcpClientBGW = new BackgroundWorker();
                tcpClientBGW.DoWork += new DoWorkEventHandler(tcpClientBGW_DoWork);
                tcpClientBGW.WorkerSupportsCancellation = true;
                if (btnClient.Text == "Start Client")
                {
                    tcpClientBGW.RunWorkerAsync();
                    btnClient.Text = "Stop Client";
                    btnServer.Enabled = false;
                }
                else
                {
                    tcpClientBGW.CancelAsync();
                    btnClient.Text = "Start Client";
                    btnServer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sends all currently open messages to the connected server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tcpClientBGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                IPEndPoint server = new IPEndPoint(LoadedTCPIPOptions.HostAddress, LoadedTCPIPOptions.Port);
                client.Connect(server);
                NetworkStream stream = client.GetStream();
                foreach (string msg in Messages)
                {
                    if (!e.Cancel)
                    {
                        List<object> dgvItems = new List<object>();
                        byte[] msgLLP = Encoding.ASCII.GetBytes(String.Format("{0}{1}{2}", LoadedTCPIPOptions.LLPHeader, msg, LoadedTCPIPOptions.LLPTrailer));
                        stream.Write(msgLLP, 0, msgLLP.Length);

                        HL7Lib.Base.Message outboundMsg = new HL7Lib.Base.Message(msg);
                        dgvItems.Add(outboundMsg.Segments.Get("MSH")[0].GetByID("MSH-10.1").Value);
                        if (LoadedTCPIPOptions.WaitForAck)
                        {
                            byte[] ackLLP = new byte[4096];
                            stream.Read(ackLLP, 0, ackLLP.Length);
                            string ackStr = Encoding.ASCII.GetString(ackLLP);
                            HL7Lib.Base.Message ackMsg = new HL7Lib.Base.Message(ackStr.Replace(LoadedTCPIPOptions.LLPHeader, "").Replace(LoadedTCPIPOptions.LLPTrailer, ""));
                            if (!HL7Lib.Base.Helper.ValidateAck(outboundMsg, ackMsg))
                                dgvItems.Add(false);
                            else
                                dgvItems.Add(true);
                        }
                        else
                        {
                            dgvItems.Add(false);
                        }
                        TCPIPTransferDisplayAddRow(dgvItems);
                    }
                    else
                    {
                        break;
                    }
                }
                stream.Close();
                client.Close();
                MessageBox.Show("Upload Complete");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the TCPIP Connection Form and creates a new TCP/IP Connection File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTCPIPConnection_Click(object sender, EventArgs e)
        {
            try
            {
                frmTCPIPConnection ftc = new frmTCPIPConnection();
                DialogResult dr = ftc.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    TCPIPOptions.Save(Helper.RemoveUnsupportedChars(ftc.OptionsName), ftc.TCPIPOps);
                    cbTCPIPConnections.Items.Add(Helper.RemoveUnsupportedChars(ftc.OptionsName));
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Deletes the selected TCP/IP Connection File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTCPIPConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTCPIPConnections.SelectedIndex > -1)
                {
                    TCPIPOptions.Delete(cbTCPIPConnections.SelectedItem.ToString());
                    cbTCPIPConnections.Items.Remove(cbTCPIPConnections.SelectedItem.ToString());
                    cbTCPIPConnections.Text = "";
                }
            }
            catch (IOException) { }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the selected report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            if (cbReportSelector.SelectedIndex > -1 && Messages.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                frmReports fr = new frmReports(Messages, cbReportSelector.SelectedItem.ToString());
                fr.Show();
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Message Box Rich Text Key Down Event: Allows for Copy and Paste Support.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.C:
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            Clipboard.SetText(rtbMessageBox.Text);
                            break;
                        case Keys.X:
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            Clipboard.SetText(rtbMessageBox.Text);
                            break;
                        case Keys.V:
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            SetDownloadedMessage(Clipboard.GetText());
                            currentMessage = 0;
                            SetMessageDisplay(currentMessage);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Takes all currently displayed messages and determines their unique values and the amount of occurances for each of those values and displays them in a form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayUniqueValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvSegmentDisplay.SelectedIndices.Count == 1 && Messages.Count > 0)
                {
                    frmUniqueValues fuv = new frmUniqueValues(lvSegmentDisplay.SelectedItems[0].Text, Messages);
                    fuv.Show();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the Database Connection form and adds a new item to the cbDatabaseConnections combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDatabaseConnection_Click(object sender, EventArgs e)
        {
            try
            {
                frmDatabaseConnection fdc = new frmDatabaseConnection();
                DialogResult dr = fdc.ShowDialog();

                if (dr == DialogResult.OK)
                    cbDatabaseConnections.Items.Add(Helper.RemoveUnsupportedChars(fdc.Controls["txtName"].Text));
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Deletes the selected database connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDatabaseConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDatabaseConnections.SelectedIndex > -1)
                {
                    DatabaseOptions.Delete(cbDatabaseConnections.SelectedItem.ToString());
                    cbDatabaseConnections.Items.Remove(cbDatabaseConnections.SelectedItem);
                    cbDatabaseConnections.Text = "";
                }
            }
            catch (IOException) { }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Loads the selected database connection and enables the Execute Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDatabaseConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDatabaseConnections.SelectedIndex > -1)
                {
                    txtDatabaseQuery.Text = "";
                    txtDatabaseWhereClause.Text = "";
                    btnExecute.Enabled = true;
                    LoadedDBOptions = DatabaseOptions.Load(cbDatabaseConnections.SelectedItem.ToString());
                    string[] queryParts = LoadedDBOptions.SQLQuery.Split(new string[] { "Where" }, StringSplitOptions.RemoveEmptyEntries);

                    txtDatabaseQuery.Text = queryParts.GetValue(0).ToString();
                    if (queryParts.Length == 2)
                        txtDatabaseWhereClause.Text = queryParts.GetValue(1).ToString();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sets up the background worker and executes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
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
        /// Executes the selected query and downloads the returned HL7 messages from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            SqlConnection con = new SqlConnection(LoadedDBOptions.SQLConnectionString);
            try
            {
                FormSetCursor(Cursors.WaitCursor);
                if (con.State == ConnectionState.Closed) con.Open();
                string query = txtDatabaseQuery.Text + ((txtDatabaseWhereClause.Text.Length > 0) ? "Where " + txtDatabaseWhereClause.Text : "");
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SetDownloadedMessage((string)reader[LoadedDBOptions.SQLColumn]);
                    if (Messages.Count == 1)
                    {
                        currentMessage = Messages.Count - 1;
                        SetMessageDisplay(currentMessage);
                    }
                    else
                    {
                        MessageTotalSetText(String.Format("{0:0,0}", Messages.Count));
                    }
                }
                if (con.State == ConnectionState.Open) con.Close();
                FormSetCursor(Cursors.Default);
                MessageBox.Show("Download Complete");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        /// <summary>
        /// Opens the default browser to the online documentation on CodePlex.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://hl7analyst.codeplex.com/documentation");
        }
        /// <summary>
        /// Opens the default browser to the online Issue Tracker on CodePlex.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reportBugSuggestFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://hl7analyst.codeplex.com/workitem/list/basic");
        }
        /// <summary>
        /// Pulls any selected field values and sets them to a search string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchUsingSelectedFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListViewItem lvi in lvSegmentDisplay.SelectedItems)
                    sb.AppendFormat("[{0}]{1} ", lvi.Text, lvi.SubItems[2].Text);
                SearchForFiles(sb.ToString());
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Calls the Hex form to display the current message in it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewMessageInHexViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Messages.Count > 0)
            {
                frmHex fh = new frmHex(Messages[currentMessage]);
                fh.Show();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reads the selected files contents and sets all HL7 messages to the Messages and AllMessages Lists.
        /// </summary>
        /// <param name="file">The File to Read</param>
        private void SetMessage(string file)
        {
            try
            {
                FileInfo fi = new FileInfo(file);
                StreamReader sr = new StreamReader(fi.FullName);
                string contents = sr.ReadToEnd();
                if (contents.ToUpper().Contains("MSH"))
                {
                    string[] msgs = contents.Split(new string[] { "MSH|" }, StringSplitOptions.RemoveEmptyEntries);
                    sr.Close();

                    foreach (string msg in msgs)
                    {
                        string m = "MSH|" + msg;
                        Messages.Add(m);
                        AllMessages.Add(m);
                    }
                }
            }
            catch (OutOfMemoryException oome)
            {
                Log.LogException(oome).ShowDialog();
            }
            catch (FileNotFoundException fnfe)
            {
                Log.LogException(fnfe).ShowDialog();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Reads the selected downloaded file contents and sets all HL7 messages to the Messages and AllMessages Lists.
        /// </summary>
        /// <param name="contents">The contents to Read</param>
        private void SetDownloadedMessage(string contents)
        {
            try
            {
                if (contents.ToUpper().Contains("MSH"))
                {
                    string[] msgs = contents.Split(new string[] { "MSH|" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string msg in msgs)
                    {
                        string m = "MSH|" + msg;
                        Messages.Add(m);
                        AllMessages.Add(m);
                    }
                }
            }
            catch (OutOfMemoryException oome)
            {
                Log.LogException(oome).ShowDialog();
            }
            catch (FileNotFoundException fnfe)
            {
                Log.LogException(fnfe).ShowDialog();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sets the selected message display items to their respective controls on the form.
        /// </summary>
        /// <param name="MessageIndex">The message to display</param>
        private void SetMessageDisplay(int MessageIndex)
        {
            try
            {
                if (Messages.Count > 0)
                {
                    SegmentDisplayClearItems();
                    SegmentChangerClearItems();

                    HL7Lib.Base.Message m = new HL7Lib.Base.Message(Messages[MessageIndex]);
                    MessageTotalSetText(String.Format("{0:0,0}", Messages.Count));
                    MessageBoxSetText(m.DisplayString);
                    CurrentMessageSetText(String.Format("{0}", MessageIndex + 1));

                    SegmentChangerAddItem("All Segments");
                    foreach (string segName in m.SegmentNames)
                        SegmentChangerAddItem(segName);

                    foreach (Segment s in m.Segments)
                        SetListViewDisplay(s);
                    if (m.GetByID("MSH-9.2") != null && m.GetByID("MSH-9.2").Count > 0 && m.GetByID("MSH-9.2")[0].Value != null)
                    {
                        HL7Lib.Base.Component c = m.GetByID("MSH-9.2")[0];
                        MessageType mt = new MessageType(c.Value);
                        FormSetText(String.Format("HL7 Analyst - {0}", mt.Description));
                    }
                    MessageBoxSetFontFormat();
                }
            }
            catch (ArgumentOutOfRangeException aore)
            {
                Log.LogException(aore).ShowDialog();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Sets the Segment Display List View to the selected segment
        /// </summary>
        /// <param name="s">The segment to display</param>
        private void SetListViewDisplay(Segment s)
        {
            try
            {
                List<Field> Fields = s.Fields;
                Fields.Sort(delegate(Field f1, Field f2) { return f1.Components[0].IDParts.FieldIndex.CompareTo(f2.Components[0].IDParts.FieldIndex); });
                foreach (Field f in Fields)
                {
                    List<HL7Lib.Base.Component> Components = f.Components;
                    Components.Sort(delegate(HL7Lib.Base.Component c1, HL7Lib.Base.Component c2) { return c1.IDParts.ComponentIndex.CompareTo(c2.IDParts.ComponentIndex); });
                    foreach (HL7Lib.Base.Component c in Components)
                    {
                        if (tsmHideEmpty.Checked && String.IsNullOrEmpty(c.Value))
                            continue;

                        ListViewItem lvi = new ListViewItem(c.ID);
                        if (!String.IsNullOrEmpty(c.Name))
                            lvi.SubItems.Add(f.Name + "-|-" + c.Name);
                        else
                            lvi.SubItems.Add(f.Name);
                        lvi.SubItems.Add(c.Value);
                        SegmentDisplayAddItem(lvi);
                    }
                }
                ListViewItem emptyItem = new ListViewItem("");
                emptyItem.BackColor = Color.CornflowerBlue;
                SegmentDisplayAddItem(emptyItem);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Toggles the segment display
        /// </summary>
        private void SetSegmentDisplay()
        {
            try
            {
                if (scSplitter.Panel1Collapsed)
                {
                    scSplitter.Panel1Collapsed = false;
                    btnMaximizeMinimize.Image = HL7_Analyst.Properties.Resources.MaximizeDisplay;
                }
                else
                {
                    scSplitter.Panel1Collapsed = true;
                    btnMaximizeMinimize.Image = HL7_Analyst.Properties.Resources.MinimizeDisplay;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Clears all messages and form controls
        /// </summary>
        private void ClearSessionDisplay()
        {
            try
            {
                Messages.Clear();
                AllMessages.Clear();
                rtbMessageBox.Text = "";
                lvSegmentDisplay.Items.Clear();
                cbSegmentChanger.Items.Clear();
                tvFTPDisplay.Nodes.Clear();
                dgvTCPIPTransferDisplay.Rows.Clear();
                txtCurrentMessage.Text = "0";
                txtMessageTotal.Text = "0";
                this.Text = "HL7 Analyst";
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Displays the first message
        /// </summary>
        private void DisplayFirstRecord()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    currentMessage = 0;
                    SetMessageDisplay(currentMessage);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Displays the previous message
        /// </summary>
        private void DisplayPreviousRecord()
        {
            try
            {
                if (currentMessage != 0 && Messages.Count > 0)
                {
                    currentMessage--;
                    SetMessageDisplay(currentMessage);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Displays the next record
        /// </summary>
        private void DisplayNextRecord()
        {
            try
            {
                if (currentMessage != (Messages.Count - 1) && Messages.Count > 0)
                {
                    currentMessage++;
                    SetMessageDisplay(currentMessage);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Displays the last record
        /// </summary>
        private void DisplayLastRecord()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    currentMessage = Messages.Count - 1;
                    SetMessageDisplay(currentMessage);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Filters the records based on selected list view item(s).
        /// </summary>
        private void FilterRecords()
        {
            try
            {
                if (lvSegmentDisplay.SelectedIndices.Count > 0)
                {
                    List<ListViewItem> lvis = new List<ListViewItem>();
                    foreach (ListViewItem lvi in lvSegmentDisplay.SelectedItems)
                        lvis.Add(lvi);

                    frmFilter ff = new frmFilter(lvis);
                    DialogResult dr = ff.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        List<string> DisplayMessages = new List<string>();

                        foreach (string m in Messages)
                        {
                            bool allFiltersMatch = false;
                            HL7Lib.Base.Message msg = new HL7Lib.Base.Message(m);
                            DataGridView dgv = (DataGridView)ff.Controls["dgvFilterOptions"];
                            foreach (DataGridViewRow dgvr in dgv.Rows)
                            {
                                HL7Lib.Base.Component c = msg.GetByID(dgvr.Cells["chID"].FormattedValue.ToString(), dgvr.Cells["chValue"].FormattedValue.ToString());

                                if (!String.IsNullOrEmpty(c.ID))
                                {
                                    allFiltersMatch = true;
                                }
                                else
                                {
                                    allFiltersMatch = false;
                                    break;
                                }
                            }
                            if (allFiltersMatch)
                                DisplayMessages.Add(m);
                        }
                        if (DisplayMessages.Count > 0)
                        {
                            Messages = DisplayMessages;
                            currentMessage = 0;
                            SetMessageDisplay(currentMessage);
                        }
                        else
                        {
                            Messages = new List<string>();
                            currentMessage = 0;
                            SegmentDisplayClearItems();
                            SegmentChangerClearItems();
                            rtbMessageBox.Text = "";
                            txtCurrentMessage.Text = "0";
                            txtMessageTotal.Text = "0";
                            MessageBox.Show("No messages found using entered filter");
                        }
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Clears any filters that are active
        /// </summary>
        private void ClearFilters()
        {
            try
            {
                Messages = new List<string>();
                foreach (string s in AllMessages)
                    Messages.Add(s);
                currentMessage = 0;
                SetMessageDisplay(currentMessage);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Creates a new report based on selected list view items
        /// </summary>
        /// <param name="lvis">The list of list view items to create report from.</param>
        private void CreateReport(List<ListViewItem> lvis)
        {
            try
            {
                frmNewReport fnr = new frmNewReport();
                DialogResult dr = fnr.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string reportName = fnr.Controls["txtReportName"].Text;
                    if (!String.IsNullOrEmpty(reportName))
                    {
                        List<string> reportItems = new List<string>();
                        foreach (ListViewItem lvi in lvis)
                        {
                            reportItems.Add(lvi.Text);
                        }
                        Reports r = new Reports();
                        r.SaveReport(reportItems, Helper.RemoveUnsupportedChars(reportName));
                        cbReportSelector.Items.Add(Helper.RemoveUnsupportedChars(reportName));
                        cbReportSelector.SelectedItem = Helper.RemoveUnsupportedChars(reportName);
                    }
                    else
                    {
                        MessageBox.Show("You must enter a report name to save it.");
                    }
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Log.LogException(dnfe).ShowDialog();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Removes the selected message from the Messages and AllMessages list and re-sets the display
        /// </summary>
        private void RemoveMessage()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    Messages.RemoveAt(currentMessage);
                    AllMessages.RemoveAt(currentMessage);
                    if (Messages.Count > currentMessage)
                        SetMessageDisplay(currentMessage);
                    else
                        SetMessageDisplay(currentMessage - 1);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the frmMessageStats form with the selected list view item.
        /// </summary>
        private void ViewStatistics()
        {
            try
            {
                if (lvSegmentDisplay.SelectedItems.Count == 1)
                {
                    string gTitle = String.Format("{0} Statistics", lvSegmentDisplay.SelectedItems[0].SubItems[1].Text);
                    frmMessageStats fms = new frmMessageStats(Messages, gTitle, lvSegmentDisplay.SelectedItems[0].Text, "STAT");
                    fms.Show();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the frmMessageStats with the MSH-7.1 Hourly option.
        /// </summary>
        private void ViewHourlyTrafficStatistics()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    string gTitle = String.Format("Hourly Message Traffic Statistics");
                    frmMessageStats fms = new frmMessageStats(Messages, gTitle, "MSH-7.1", "HOURLY");
                    fms.Show();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the frmMessageStats with the MSH-7.1 Daily option.
        /// </summary>
        private void ViewDailyTrafficStatistics()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    string gTitle = String.Format("Daily Message Traffic Statistics");
                    frmMessageStats fms = new frmMessageStats(Messages, gTitle, "MSH-7.1", "DAILY");
                    fms.Show();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// De-Identifies all Messages
        /// </summary>
        private void DeIdentifyMessages()
        {
            try
            {
                if (Messages.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<string> Msgs = new List<string>();
                    foreach (string msg in Messages)
                    {
                        HL7Lib.Base.Message m = new HL7Lib.Base.Message(msg);
                        List<Segment> segments = m.Segments.Get("PID");
                        if (segments.Count == 1)
                        {
                            Segment s = segments[0];

                            HL7Lib.Base.Component last = s.GetByID("PID-5.1");
                            HL7Lib.Base.Component first = s.GetByID("PID-5.2");
                            HL7Lib.Base.Component sex = s.GetByID("PID-8.1");
                            HL7Lib.Base.Component address = s.GetByID("PID-11.1");
                            HL7Lib.Base.Component mrn = s.GetByID("PID-18.1");
                            HL7Lib.Base.Component ssn = s.GetByID("PID-19.1");

                            List<EditItem> items = new List<EditItem>();
                            if (!String.IsNullOrEmpty(last.Value))
                                items.Add(new EditItem(last.ID, last.Value, HL7Lib.Base.Helper.RandomLastName()));
                            if (!String.IsNullOrEmpty(first.Value))
                                items.Add(new EditItem(first.ID, first.Value, HL7Lib.Base.Helper.RandomFirstName(sex.Value)));
                            if (!String.IsNullOrEmpty(address.Value))
                                items.Add(new EditItem(address.ID, address.Value, HL7Lib.Base.Helper.RandomAddress()));
                            if (!String.IsNullOrEmpty(mrn.Value))
                                items.Add(new EditItem(mrn.ID, mrn.Value, HL7Lib.Base.Helper.RandomMRN()));
                            if (!String.IsNullOrEmpty(ssn.Value))
                                items.Add(new EditItem(ssn.ID, ssn.Value, "999-99-9999"));

                            Msgs.Add(EditValues(msg, items));
                        }
                    }
                    Messages = Msgs;
                    SetMessageDisplay(currentMessage);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Opens the Edit Field Form and then edits the message(s)
        /// </summary>
        private void EditFieldValues()
        {
            try
            {
                if (lvSegmentDisplay.SelectedIndices.Count > 0)
                {
                    List<ListViewItem> lvis = new List<ListViewItem>();
                    foreach (ListViewItem lvi in lvSegmentDisplay.SelectedItems)
                    {
                        lvis.Add(lvi);
                    }

                    frmEditField fef = new frmEditField(lvis);
                    DialogResult dr = fef.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        bool EditAllMessages = fef.EditAllMessages;
                        List<EditItem> EditItems = fef.Items;

                        if (!EditAllMessages)
                        {
                            string msg = Messages[currentMessage];
                            Messages.RemoveAt(currentMessage);
                            Messages.Insert(currentMessage, EditValues(msg, EditItems));
                        }
                        else
                        {
                            List<string> EditMessages = new List<string>();
                            foreach (string msg in Messages)
                            {
                                EditMessages.Add(EditValues(msg, EditItems));
                            }
                            Messages = EditMessages;
                        }
                        AllMessages = Messages;
                        SetMessageDisplay(currentMessage);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Edits the message string
        /// </summary>
        /// <param name="msg">The message to edit</param>
        /// <param name="items">The items to edit in the message</param>
        /// <returns>The message string after editing</returns>
        private string EditValues(string msg, List<EditItem> items)
        {
            try
            {
                List<EditItem> finalList = new List<EditItem>();
                string returnMsg = msg;
                HL7Lib.Base.Message m = new HL7Lib.Base.Message(msg);
                foreach (EditItem item in items)
                {
                    List<HL7Lib.Base.Component> com = m.GetByID(item.ComponentID);
                    foreach (HL7Lib.Base.Component c in com)
                    {
                        finalList.Add(new EditItem(c.ID, c.Value, item.NewValue));
                    }
                }
                foreach (EditItem i in finalList)
                {
                    if (!String.IsNullOrEmpty(i.OldValue))
                        returnMsg = returnMsg.Replace(i.OldValue, i.NewValue);
                }
                return returnMsg;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return "";
            }
        }
        /// <summary>
        /// Saves the specified message to disk.
        /// </summary>
        /// <param name="msg">The message to save</param>
        /// <param name="f">The file path to save to</param>
        private void SaveMessage(string msg, string f)
        {
            try
            {
                StreamWriter sw = new StreamWriter(f);
                sw.Write(msg.Replace("\n", "\r") + "\r\n");
                sw.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Creates a message from the current text in the RTF Box and calls the formatting methods to format the text in the RTF Box
        /// </summary>
        private void SetRTBTextFormatOptions()
        {
            HL7Lib.Base.Message msg = new HL7Lib.Base.Message(rtbMessageBox.Text);
            SetRTBFontFormat(rtbMessageBox, msg);
        }
        /// <summary>
        /// Loops over each character in the text of the RTF Box and sets it's formatting based on what character it is.
        /// </summary>
        /// <param name="rtb">The RichTextBox to use</param>
        /// <param name="msg">The Message object to use</param>
        private void SetRTBFontFormat(RichTextBox rtb, HL7Lib.Base.Message msg)
        {
            try
            {
                for (int i = 0; i < rtb.Text.Length; i++)
                {
                    string c = rtb.Text[i].ToString();

                    if (c == msg.FieldSeperator)
                        SetRTBSelection(rtb, i, 1, Color.CornflowerBlue);
                    else if (c == msg.ComponentSeperator)
                        SetRTBSelection(rtb, i, 1, Color.Coral);
                    else if (c == msg.FieldRepeatSeperator)
                        SetRTBSelection(rtb, i, 1, Color.Turquoise);
                    else if (c == msg.SubComponentSeperator)
                        SetRTBSelection(rtb, i, 1, Color.Goldenrod);
                    else if (c == msg.EscapeCharacter)
                        SetRTBSelection(rtb, i, 1, Color.Fuchsia);
                }
                rtb.SelectionStart = 0;
                rtb.SelectionLength = 0;
            }
            catch (IndexOutOfRangeException) { }
            catch (ArgumentOutOfRangeException) { }
        }
        /// <summary>
        /// Sets the selection start and length and selection color of the RTF Box
        /// </summary>
        /// <param name="rtb">The RichTextBox to use</param>
        /// <param name="i">The selection start</param>
        /// <param name="len">The selection length</param>
        /// <param name="c">The color to set selection to</param>
        private void SetRTBSelection(RichTextBox rtb, int i, int len, Color c)
        {
            try
            {
                rtb.SelectionStart = i;
                rtb.SelectionLength = len;
                rtb.SelectionColor = c;
            }
            catch (IndexOutOfRangeException) { }
            catch (ArgumentOutOfRangeException) { }
        }
        /// <summary>
        /// Calls the search form and sets the returned messages to the message display
        /// </summary>
        /// <param name="searchTerms">The search query to use</param>
        private void SearchForFiles(string searchTerms)
        {
            try
            {
                frmSearch fs = new frmSearch(searchTerms);
                DialogResult dr = fs.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (fs.Messages.Count > 0)
                    {
                        foreach (string s in fs.Messages)
                        {
                            Messages.Add(s);
                            AllMessages.Add(s);
                        }
                        currentMessage = 0;
                        SetMessageDisplay(currentMessage);
                    }
                    else
                    {
                        MessageBox.Show("No messages returned by search");
                    }
                }
                else
                {
                    if (fs.Messages.Count > 0)
                    {
                        foreach (string s in fs.Messages)
                        {
                            Messages.Add(s);
                            AllMessages.Add(s);
                        }
                        currentMessage = 0;
                        SetMessageDisplay(currentMessage);
                    }
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
