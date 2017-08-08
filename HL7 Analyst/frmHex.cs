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
using System.Windows.Forms;

namespace HL7_Analyst
{
    /// <summary>
    /// Displays the message passed to it in hex format
    /// </summary>
    public partial class frmHex : Form
    {        
        private int messageScrollPos = 0;
        private int hexScrollPos = 0;
        /// <summary>
        /// Initialization Method
        /// </summary>
        /// <param name="message">The message to display in hex format</param>
        public frmHex(string message)
        {
            InitializeComponent();
            SetHexDisplay(message.ToCharArray());
        }
        /// <summary>
        /// Processes the character array passed to it into each of the respective data grid views on the form
        /// </summary>
        /// <param name="message">The hex array to process</param>
        private void SetHexDisplay(char[] message)
        {
            try
            {
                dgvMessage.Rows.Add();
                dgvHex.Rows.Add();
                int x = 0;

                for (int i = 0; i < message.Length; i++)
                {
                    int tmp = message[i];
                    dgvMessage.Rows[dgvMessage.Rows.Count - 1].Cells[x].Value = message[i].ToString();
                    dgvHex.Rows[dgvHex.Rows.Count - 1].Cells[x].Value = String.Format("{0:x2}", (uint)Convert.ToUInt32(tmp.ToString()));

                    if (x == 9)
                    {
                        dgvMessage.Rows.Add();
                        dgvHex.Rows.Add();
                        x = 0;
                    }
                    else
                    {
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Changes the selected cell in the Hex data grid to match the selected cell in the Message data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMessage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                    dgvHex[e.ColumnIndex, e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Changes the selected cell in the Message data grid to match the selected cell in the Hex data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHex_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                    dgvMessage[e.ColumnIndex, e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        } 
        /// <summary>
        /// Passes the scrolling position to the Hex data grid if the caller is the Message data grid, if not it sets the Message data grid scroll position to the Hex data grids scroll position.
        /// This allows both data grids to scroll together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMessage_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if ((sender == dgvMessage) && (e.ScrollOrientation == ScrollOrientation.VerticalScroll))
                {
                    messageScrollPos = dgvMessage.FirstDisplayedScrollingRowIndex;
                    dgvHex_Scroll(dgvMessage, new ScrollEventArgs(ScrollEventType.ThumbPosition, e.NewValue, ScrollOrientation.VerticalScroll));
                }
                if ((sender == dgvHex) && (e.ScrollOrientation == ScrollOrientation.VerticalScroll))
                {
                    dgvMessage.FirstDisplayedScrollingRowIndex = hexScrollPos;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Passes the scrolling position to the Message data grid if the caller is the Hex data grid, if not it sets the Hex data grid scroll position to the Message data grids scroll position.
        /// This allows both data grids to scroll together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHex_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if ((sender == dgvHex) && (e.ScrollOrientation == ScrollOrientation.VerticalScroll))
                {
                    hexScrollPos = dgvHex.FirstDisplayedScrollingRowIndex;
                    dgvMessage_Scroll(dgvHex, new ScrollEventArgs(ScrollEventType.ThumbPosition, e.NewValue, ScrollOrientation.VerticalScroll));
                }
                if ((sender == dgvMessage) && (e.ScrollOrientation == ScrollOrientation.VerticalScroll))
                {
                    dgvHex.FirstDisplayedScrollingRowIndex = messageScrollPos;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }

               
    }
}
