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

namespace HL7_Analyst
{
    /// <summary>
    /// ReportColumn Class: Used to create a report column item
    /// </summary>
    class ReportColumn
    {
        /// <summary>
        /// The Name of the ReportColumn
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Header of the ReportColumn
        /// </summary>
        public string Header { get; set; }
    }
}
