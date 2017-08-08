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
using HL7Lib.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace HL7_Analyst
{
    /// <summary>
    /// Build Search Query Form: Used to assist in file search query generation and execution.
    /// </summary>
    public partial class frmBuildSearch : Form
    {
        /// <summary>
        /// The returned search string after building the query
        /// </summary>
        public StringBuilder returnSearch = new StringBuilder();
        private List<List<SearchTerm>> searchQuery = new List<List<SearchTerm>>();
        private List<SearchTerm> searchGroup = new List<SearchTerm>();
        private delegate void AddRowsDelegate(List<object> objs);
        private delegate void UpdateFormCursorDelegate(Cursor c);
        private delegate void AddItemsDelegate(object obj);
        private delegate void UpdateTextDelegate(string t);
        BackgroundWorker bgw = new BackgroundWorker();
        Settings settings = new Settings();
        /// <summary>
        /// Initialization Method: Sets the current search query for display
        /// </summary>
        /// <param name="query">The current search query</param>
        public frmBuildSearch(string query)
        {
            InitializeComponent();
            searchQuery = SearchTerm.GetSearchTerms(query);
            txtCurrentQuery.Text = SearchTerm.BuildSearchQueryString(searchQuery);
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// AddRows Method: Adds rows to the data grid across threads.
        /// </summary>
        /// <param name="objs">The rows cell object array</param>
        private void AddRows(List<object> objs)
        {
            try
            {
                if (dgvComponents.IsHandleCreated)
                {
                    if (dgvComponents.InvokeRequired)
                        dgvComponents.Invoke(new AddRowsDelegate(AddRows), objs);
                    else
                        dgvComponents.Rows.Add(objs.ToArray());
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// UpdateFormCursor Method: Updates the forms cursor across threads.
        /// </summary>
        /// <param name="c">The cursor to use</param>
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
        /// <summary>
        /// AddItems Method: Adds items to the Segment Selector across threads.
        /// </summary>
        /// <param name="obj">The object to add</param>
        private void AddItems(object obj)
        {
            try
            {
                if (cbSegmentSelector.IsHandleCreated)
                {
                    if (cbSegmentSelector.InvokeRequired)
                        cbSegmentSelector.Invoke(new AddItemsDelegate(AddItems), obj);
                    else
                        cbSegmentSelector.Items.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// UpdateText Method: Updates the current text of the Segment Selector across threads.
        /// </summary>
        /// <param name="t">The text to display</param>
        private void UpdateText(string t)
        {
            try
            {
                if (cbSegmentSelector.IsHandleCreated)
                {
                    if (cbSegmentSelector.InvokeRequired)
                        cbSegmentSelector.Invoke(new UpdateTextDelegate(UpdateText), t);
                    else
                        cbSegmentSelector.Text = t;
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
        /// Form Load Event: Gets settings from file and sets up event handler for the background worker DoWork event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBuildSearch_Load(object sender, EventArgs e)
        {
            try
            {
                settings.GetSettings();
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                bgw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Background Worker Do Work Event: Sets the data grids display and adds all items to the segment selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateFormCursor(Cursors.WaitCursor);
                List<Segments> segs = new List<Segments>();
                foreach (Segments s in Enum.GetValues(typeof(Segments)))
                {
                    segs.Add(s);
                    AddItems(s);
                }
                UpdateFormCursor(Cursors.Default);
                if (segs.Count > 0)
                {
                    LoadDataGrid(settings.DefaultSegment);
                    UpdateText(settings.DefaultSegment.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Segment Selector Selected Index Changed Event: Loads the selected segments fields and components to the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSegmentSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbSegmentSelector.SelectedIndex > -1)
                {
                    Segments s = (Segments)cbSegmentSelector.SelectedItem;
                    dgvComponents.Rows.Clear();
                    LoadDataGrid(s);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Data Grid Cell End Edit Event: Sets the entered value to the search query string builder for use in the frmSearch search terms box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvComponents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dgvComponents["cID", e.RowIndex].Value.ToString();
                string v = dgvComponents["cValue", e.RowIndex].Value.ToString();

                if (searchQuery.Contains(searchGroup))
                    searchQuery.Remove(searchGroup);

                if (!String.IsNullOrEmpty(v))
                    AddSearchQueryItem(id, v);
                else
                    RemoveSearchQueryItem(id);
                searchQuery.Add(searchGroup);
                txtCurrentQuery.Text = SearchTerm.BuildSearchQueryString(searchQuery);
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Add Button Click Event: Adds a new search query group to the search query string builder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                searchGroup = new List<SearchTerm>();
                txtCurrentQuery.Text = SearchTerm.BuildSearchQueryString(searchQuery) + "|";
                dgvComponents.Rows.Clear();
                bgw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// OK Button Click Event: Returns the txtCurrentQuery text after building the search query, then closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                returnSearch.Append(txtCurrentQuery.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Cancel Button Click Event: Cancels the dialog box
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

        private void cbSegmentSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (txtCurrentQuery.Text.Length > 0)
                    {
                        returnSearch.Append(txtCurrentQuery.Text);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Loads the data grid with the selected segments fields and components
        /// </summary>
        /// <param name="s">The segment to display</param>
        private void LoadDataGrid(Segments s)
        {
            try
            {
                Segment seg = new Segment(s);
                foreach (Field f in seg.Fields)
                {
                    foreach (HL7Lib.Base.Component c in f.Components)
                    {
                        if (!String.IsNullOrEmpty(c.ID))
                        {
                            string nameColumn = "";
                            if (!String.IsNullOrEmpty(c.Name))
                                nameColumn = f.Name + "-|-" + c.Name;
                            else
                                nameColumn = f.Name;
                            List<object> objs = new List<object>();
                            objs.Add((object)c.ID);
                            objs.Add((object)nameColumn);
                            objs.Add((object)"");
                            AddRows(objs);
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
        /// Adds a search item to the search query
        /// </summary>
        /// <param name="id">The ID of the component to set a value for</param>
        /// <param name="v">The value to use in the search term</param>
        private void AddSearchQueryItem(string id, string v)
        {
            try
            {
                SearchTerm old = GetSearchQueryItem(id);
                if (old != null)
                    RemoveSearchQueryItem(id);
                SearchTerm st = new SearchTerm();
                st.ID = id;
                st.Value = v;
                searchGroup.Add(st);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Pulls the specified search term
        /// </summary>
        /// <param name="id">The Component ID to pull</param>
        /// <returns>The Search Term that was found</returns>
        private SearchTerm GetSearchQueryItem(string id)
        {
            try
            {
                SearchTerm st = searchGroup.Find(delegate(SearchTerm s) { return s.ID == id; });
                return st;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return null;
            }
        }
        /// <summary>
        /// Removes a search term from the list with the specified component id
        /// </summary>
        /// <param name="id">The component ID to remove</param>
        private void RemoveSearchQueryItem(string id)
        {
            try
            {
                SearchTerm st = GetSearchQueryItem(id);
                if (st != null)
                    searchGroup.Remove(st);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        #endregion        
    }
}
