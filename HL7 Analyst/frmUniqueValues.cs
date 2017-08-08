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
using System.Windows.Forms;
using HL7Lib.Base;
using System.Linq;

namespace HL7_Analyst
{
    /// <summary>
    /// Unique Values Form: Used to display unique values of a selected component and how many times that particular value occures in the open messages.
    /// </summary>
    public partial class frmUniqueValues : Form
    {
        string componentID = "";
        List<string> messages = new List<string>();
        private delegate void AddListViewItemsDelegate(ListViewItem lvi);
        private delegate void UpdateFormCursorDelegate(Cursor c);
        /// <summary>
        /// Initialization Method
        /// </summary>
        /// <param name="cID">The Component ID selected</param>
        /// <param name="msgs">The messages passed in from frmMain</param>
        public frmUniqueValues(string cID, List<string> msgs)
        {
            InitializeComponent();
            componentID = cID;
            messages = msgs;
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Add list view item to list view
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
        /// Sets up the background worker and kicks it off to setup the form display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUniqueValues_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = String.Format("Unique Values - {0}", componentID);
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
        /// Background Worker Do Work Event: Loops over all messages and each component that matches the ID passed to the form and calculates the unique values and occurance counts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateFormCursor(Cursors.WaitCursor);
                List<HL7Lib.Base.Message> msgs = new List<HL7Lib.Base.Message>();
                foreach (string msg in messages)
                    msgs.Add(new HL7Lib.Base.Message(msg));

                var items = (from com in msgs.GetByID(componentID) group com by com.Value into g select new { Value = g.Key, Count = g.Count() }).Distinct();
                foreach (var g in items)
                {
                    ListViewItem lvi = new ListViewItem(g.Value);
                    lvi.SubItems.Add(g.Count.ToString());
                    lvi.SubItems.Add(String.Format("{0}%", Math.Round((Convert.ToDouble(g.Count) / Convert.ToDouble(messages.Count)) * 100)));
                    AddListViewItems(lvi);
                }
                UpdateFormCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        #endregion
    }
}
