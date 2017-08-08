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
using System.Linq;
using System.Text;

namespace HL7_Analyst
{
    /// <summary>
    /// EditItem Class: Used to store a new EditItem for use in message editing.
    /// </summary>
    public class EditItem
    {
        /// <summary>
        /// The ComponentID of the Component
        /// </summary>
        public string ComponentID { get; set; }
        /// <summary>
        /// The NewValue of the Component
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// The OldValue of the Component
        /// </summary>
        public string OldValue { get; set; }
        /// <summary>
        /// Constructor that sets the ComponentID
        /// </summary>
        /// <param name="_CID">ComponentID to set for this EditItem</param>
        public EditItem(string _CID)
        {
            ComponentID = _CID;
        }
        /// <summary>
        /// Constructur that sets the ComponentID, OldValue, and the NewValue
        /// </summary>
        /// <param name="_CID">ComponentID to set for this EditItem</param>
        /// <param name="_OldValue">OldValue to set for this EditItem</param>
        /// <param name="_NewValue">NewValue to set for this EditItem</param>
        public EditItem(string _CID, string _OldValue, string _NewValue)
        {
            ComponentID = _CID;
            NewValue = _NewValue;
            OldValue = _OldValue;
        }
    }
}
