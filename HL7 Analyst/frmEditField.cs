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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// Edit Field Form: Used to modify selected fields in the current message or all messages.
    /// </summary>
    public partial class frmEditField : Form
    {
        /// <summary>
        /// The list of edited items
        /// </summary>
        public List<EditItem> Items = new List<EditItem>();
        /// <summary>
        /// A bool value indicating if all messages should be edited or just the current message
        /// </summary>
        public bool EditAllMessages = false;
        /// <summary>
        /// Initialization Code: Takes a list of list view items that were selected for editing.
        /// </summary>
        /// <param name="lvis">The list view item list</param>
        public frmEditField(List<ListViewItem> lvis)
        {
            InitializeComponent();
            try
            {
                foreach (ListViewItem lvi in lvis)
                {
                    List<object> objs = new List<object>();
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                    {
                        objs.Add(lvi.SubItems[i].Text);
                    }
                    EditItem item = new EditItem(lvi.Text);
                    item.OldValue = lvi.SubItems[2].Text;
                    item.NewValue = lvi.SubItems[2].Text;
                    Items.Add(item);
                    dgvEditField.Rows.Add(objs.ToArray());
                }
                if (dgvEditField.Rows.Count > 0)
                    dgvEditField.CurrentCell = dgvEditField[2, 0];                
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Data Grid Cell End Edit Event: Sets the EditItem value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEditField_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dgvEditField["chID", e.RowIndex].Value.ToString();
                string v;
                if (dgvEditField["chValue", e.RowIndex].Value != null)
                    v = dgvEditField["chValue", e.RowIndex].Value.ToString();
                else
                    v = "";

                EditItem item = Items.Find(delegate(EditItem i) { return i.ComponentID == id; });
                Items.Remove(item);
                item.NewValue = v;
                Items.Add(item);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Edit Current Checked Changed Event: Sets the public method EditAllMessages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbEditCurrent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEditCurrent.Checked)
                {
                    EditAllMessages = false;
                }
                else
                {
                    EditAllMessages = true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
