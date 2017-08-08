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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using HL7Lib.Base;

namespace HL7_Analyst
{
    /// <summary>
    /// Reports Class: Used to perform operations on Report Files.
    /// </summary>
    class Reports
    {
        /// <summary>
        /// The Columns assigned to this report
        /// </summary>
        public List<ReportColumn> Columns { get; set; }
        /// <summary>
        /// The Items for this report
        /// </summary>
        public List<List<string>> Items { get; set; }
        /// <summary>
        /// LoadReport Method: Loads the specified report with the values in a list of messages
        /// </summary>
        /// <param name="ReportName">The report to load</param>
        /// <param name="Messages">The list of Messages to use in the report</param>
        public void LoadReport(string ReportName, List<string> Messages)
        {
            Columns = new List<ReportColumn>();
            Items = new List<List<string>>();

            if (Directory.Exists(Path.Combine(Application.StartupPath, "Reports")))
            {
                if (File.Exists(Path.Combine(Path.Combine(Application.StartupPath, "Reports"), ReportName + ".xml")))
                {
                    XmlTextReader xtr = new XmlTextReader(Path.Combine(Path.Combine(Application.StartupPath, "Reports"), ReportName + ".xml"));
                    xtr.Read();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(xtr);

                    XmlNodeList nodes = xDoc.SelectNodes("Report/Column");

                    foreach (XmlNode node in nodes)
                    {
                        ReportColumn rc = new ReportColumn();
                        rc.Name = node.InnerText.Replace("-", "").Replace(".", "");
                        rc.Header = node.InnerText;
                        if (!Columns.Contains(rc))
                            Columns.Add(rc);
                    }

                    xtr.Close();

                    foreach (string m in Messages)
                    {
                        List<string> itemList = new List<string>();
                        HL7Lib.Base.Message msg = new HL7Lib.Base.Message(m);
                        foreach (Segment s in msg.Segments)
                        {
                            foreach (Field f in s.Fields)
                            {
                                foreach (Component c in f.Components)
                                {
                                    if (!String.IsNullOrEmpty(GetColumn(c.ID)))
                                    {
                                        itemList.Add(c.Value);
                                    }
                                }

                            }
                        }
                        Items.Add(itemList);
                    }
                }
            }
        }
        /// <summary>
        /// GetColumn Method: Pulls the specified column from the list of columns.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetColumn(string id)
        {
            string returnStr = "";

            if (Columns.Count > 0)
            {
                ReportColumn rc = Columns.Find(delegate(ReportColumn col) { return col.Header == id; });
                if (rc != null)
                    returnStr = rc.Header;
            }
            return returnStr;
        }
        /// <summary>
        /// SaveReport Method: Saves a report file with the specified name and columns
        /// </summary>
        /// <param name="ReportItems">The report items to use</param>
        /// <param name="ReportName">The report name to use</param>
        public void SaveReport(List<string> ReportItems, string ReportName)
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, "Reports")))
            {
                XmlTextWriter xtw = new XmlTextWriter(Path.Combine(Path.Combine(Application.StartupPath, "Reports"), Helper.RemoveUnsupportedChars(ReportName) + ".xml"), Encoding.UTF8);
                xtw.WriteStartDocument();
                xtw.WriteStartElement("Report");
                foreach (string item in ReportItems)
                {
                    xtw.WriteStartElement("Column");
                    xtw.WriteString(item);
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
                xtw.Close();
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Reports"));
                SaveReport(ReportItems, ReportName);
            }
        }
        /// <summary>
        /// Delete Report Method: Deletes the specified report file
        /// </summary>
        /// <param name="ReportName"></param>
        public static void DeleteReport(string ReportName)
        {
            File.Delete(Path.Combine(Path.Combine(Application.StartupPath, "Reports"), ReportName + ".xml"));
        }        
    }
}
