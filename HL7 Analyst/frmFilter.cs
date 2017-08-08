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
    /// Filter Form: Displays the select list view items and their current values and allows you to change the filter values.
    /// </summary>
    public partial class frmFilter : Form
    {        
        /// <summary>
        /// Initialization Method: Takes a list of list view items and displays them
        /// </summary>
        /// <param name="lvis">List view item list to display</param>
        public frmFilter(List<ListViewItem> lvis)
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
                    dgvFilterOptions.Rows.Add(objs.ToArray());
                }
                if (dgvFilterOptions.Rows.Count > 0)
                    dgvFilterOptions.CurrentCell = dgvFilterOptions[2, 0];
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
    }
}
