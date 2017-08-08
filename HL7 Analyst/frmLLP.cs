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
    /// LLP Form: Used to selected LLP Header and Trailer hex codes
    /// </summary>
    public partial class frmLLP : Form
    {
        /// <summary>
        /// The list of LLPObjects selected by the user.
        /// </summary>
        public List<LLP> LLPList = new List<LLP>();
        /// <summary>
        /// Initialization Method: Loads the LLP List and sets the List View to the values returned.
        /// </summary>
        public frmLLP()
        {
            InitializeComponent();
            try
            {
                foreach (LLP l in LLP.LoadLLPList())
                {
                    List<object> objs = new List<object>();
                    objs.Add((object)l.Hex);
                    objs.Add((object)l.Description);
                    dgvLLP.Rows.Add(objs.ToArray());
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Checks if it's a dirty cell state and if it is commits the edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLLP_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvLLP.IsCurrentCellDirty)
                    dgvLLP.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Checks the checked cell state, if it's checked it adds it to the LLP List if not it removes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLLP_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvLLP.Columns[e.ColumnIndex].Name == "cSelect")
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvLLP.Rows[e.RowIndex].Cells["cSelect"];
                    bool cellChecked = (Boolean)checkCell.Value;
                    string hex = dgvLLP.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (cellChecked)
                        LLPList.Add(LLP.LoadLLP(hex));
                    else
                        LLPList.Remove(LLP.LoadLLP(hex));
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
