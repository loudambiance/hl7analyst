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
    /// Helper Class: Provides helper methods
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Removes the specified characters from the file name.
        /// </summary>
        /// <param name="FileName">The File Name to Clean</param>
        /// <returns>The File Name after cleaning unsupported characters from it</returns>
        public static string RemoveUnsupportedChars(string FileName)
        {
            string s = FileName;
            s = s.Replace("\\", "");
            s = s.Replace("/", "");
            s = s.Replace(":", "");
            s = s.Replace("*", "");
            s = s.Replace("?", "");
            s = s.Replace("\"", "");
            s = s.Replace("<", "");
            s = s.Replace(">", "");
            s = s.Replace("|", "");
            return s;
        }
    }
}
