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
using System.Text.RegularExpressions;

namespace HL7_Analyst
{
    /// <summary>
    /// LLP Class: Defines an LLP object
    /// </summary>
    public class LLP
    {
        /// <summary>
        /// The Char Value to use in the LLP Wrapper
        /// </summary>
        public char CharValue { get; set; }
        /// <summary>
        /// The Hex Value of the Char Code
        /// </summary>
        public string Hex { get; set; }
        /// <summary>
        /// The Hex Code Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public LLP() { }
        /// <summary>
        /// Constructs an LLP object with the specified values
        /// </summary>
        /// <param name="_CharValue">The Char Value to use in the LLP Wrapper</param>
        /// <param name="_Hex">The Hex Value of the Char Code</param>
        /// <param name="_Desc">The Hex Code Description</param>
        public LLP(char _CharValue, string _Hex, string _Desc)
        {
            CharValue = _CharValue;
            Hex = _Hex;
            Description = _Desc;
        }
        /// <summary>
        /// Pulls an LLP Object from the list of LLP objects
        /// </summary>
        /// <param name="_Hex">The Hex Value of the Char Code</param>
        /// <returns>Returns the LLP object</returns>
        public static LLP LoadLLP(string _Hex)
        {
            LLP llp = LoadLLPList().Find(delegate(LLP l) { return l.Hex == _Hex; });
            return llp;
        }
        /// <summary>
        /// Pulls the LLP Char Value based on the Hex code passed in
        /// </summary>
        /// <param name="s">The hex code to search for</param>
        /// <returns>The LLP Char Value</returns>
        public static string GetLLPString(string s)
        {
            StringBuilder sb = new StringBuilder();
            Regex reg = new Regex("\\[0x[A-Za-z0-9]+\\]");
            MatchCollection matches = reg.Matches(s);            
            foreach(Match match in matches)
            {
                LLP l = LoadLLP(match.Value);
                sb.Append(l.CharValue);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Loads the list of LLP accepted values
        /// </summary>
        /// <returns>The List of LLP values</returns>
        public static List<LLP> LoadLLPList()
        {
            List<LLP> LLPList = new List<LLP>();
            LLPList.Add(new LLP((char)0, "[0x0]", "Null char"));
            LLPList.Add(new LLP((char)1, "[0x1]", "Start of Heading"));
            LLPList.Add(new LLP((char)2, "[0x2]", "Start of Text"));
            LLPList.Add(new LLP((char)3, "[0x3]", "End of Text"));
            LLPList.Add(new LLP((char)4, "[0x4]", "End of Transmission"));
            LLPList.Add(new LLP((char)5, "[0x5]", "Inquiry"));
            LLPList.Add(new LLP((char)6, "[0x6]", "Acknowledgment"));
            LLPList.Add(new LLP((char)7, "[0x7]", "Bell"));
            LLPList.Add(new LLP((char)8, "[0x8]", "Back Space"));
            LLPList.Add(new LLP((char)9, "[0x9]", "Horizontal Tab"));
            LLPList.Add(new LLP((char)10, "[0x0A]", "Line Feed"));
            LLPList.Add(new LLP((char)11, "[0x0B]", "Vertical Tab"));
            LLPList.Add(new LLP((char)12, "[0x0C]", "Form Feed"));
            LLPList.Add(new LLP((char)13, "[0x0D]", "Carriage Return"));
            LLPList.Add(new LLP((char)14, "[0x0E]", "Shift Out / X-On"));
            LLPList.Add(new LLP((char)15, "[0x0F]", "Shift In / X-Off"));
            LLPList.Add(new LLP((char)16, "[0x10]", "Data Line Escape"));
            LLPList.Add(new LLP((char)17, "[0x11]", "Device Control 1 (oft. XON)"));
            LLPList.Add(new LLP((char)18, "[0x12]", "Device Control 2"));
            LLPList.Add(new LLP((char)19, "[0x13]", "Device Control 3 (oft. XOFF)"));
            LLPList.Add(new LLP((char)20, "[0x14]", "Device Control 4"));
            LLPList.Add(new LLP((char)21, "[0x15]", "Negative Acknowledgment"));
            LLPList.Add(new LLP((char)22, "[0x16]", "Synchronous Idle"));
            LLPList.Add(new LLP((char)23, "[0x17]", "End of Transmit Block"));
            LLPList.Add(new LLP((char)24, "[0x18]", "Cancel"));
            LLPList.Add(new LLP((char)25, "[0x19]", "End of Medium"));
            LLPList.Add(new LLP((char)26, "[0x1A]", "Substitute"));
            LLPList.Add(new LLP((char)27, "[0x1B]", "Escape"));
            LLPList.Add(new LLP((char)28, "[0x1C]", "File Separator"));
            LLPList.Add(new LLP((char)29, "[0x1D]", "Group Separator"));
            LLPList.Add(new LLP((char)30, "[0x1E]", "Record Separator"));
            LLPList.Add(new LLP((char)31, "[0x1F]", "Unit Separator"));
            return LLPList;
        }
    }
}
