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
using HL7Lib.Base;

namespace HL7_Analyst
{
    /// <summary>
    /// FilledFields class: Used to store calculated information about each non-empty field or component in a message
    /// </summary>
    class FilledFields
    {
        /// <summary>
        /// The Minimum length of a list of items
        /// </summary>
        public int MinLength { get; set; }
        /// <summary>
        /// The Maximum length of a list of items
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// The Average length of a list of items
        /// </summary>
        public int AvergeLength { get; set; }
        /// <summary>
        /// The Name of the item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ID of the item
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Calculates the filled in fields/components in the selected message. 
        /// </summary>
        /// <param name="Messages">The message to check</param>
        /// <returns>A list of FilledFields with calculation logic for each object</returns>
        public static List<FilledFields> Calculate(List<string> Messages)
        {
            Dictionary<string, List<int>> fieldList = new Dictionary<string, List<int>>();
            List<FilledFields> fieldItems = new List<FilledFields>();
            
            foreach (string m in Messages)
            {
                HL7Lib.Base.Message msg = new Message(m);
                foreach (Segment s in msg.Segments)
                {
                    foreach (Field f in s.Fields)
                    {
                        foreach (Component c in f.Components)
                        {
                            if (!String.IsNullOrEmpty(c.Value))
                            {
                                string n = "";
                                if (String.IsNullOrEmpty(c.Name))
                                    n = c.ID + "+++|+++" + f.Name;
                                else
                                    n = c.ID + "+++|+++" + f.Name + "-|-" + c.Name;

                                if (fieldList.ContainsKey(n))
                                {
                                    List<int> outLengths = new List<int>();
                                    if (fieldList.TryGetValue(n, out outLengths))
                                    {
                                        fieldList.Remove(n);
                                        outLengths.Add(c.Value.Length);
                                        fieldList.Add(n, outLengths);
                                    }
                                }
                                else
                                {
                                    List<int> lengths = new List<int>();
                                    lengths.Add(c.Value.Length);
                                    fieldList.Add(n, lengths);
                                }
                            }
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, List<int>> kvp in fieldList)
            {
                FilledFields ff = new FilledFields();
                ff.ID = kvp.Key.Split(new string[] { "+++|+++" }, StringSplitOptions.None).GetValue(0).ToString();
                ff.Name = kvp.Key.Split(new string[] { "+++|+++" }, StringSplitOptions.None).GetValue(1).ToString();
                ff.MinLength = kvp.Value.Min();
                ff.MaxLength = kvp.Value.Max();
                ff.AvergeLength = kvp.Value.Sum() / kvp.Value.Count;
                if (ff.Name != "Segment Name")
                {
                    fieldItems.Add(ff);
                }
            }
            return fieldItems;
        }
    }
}
