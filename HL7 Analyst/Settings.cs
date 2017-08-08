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
    /// Settings Class: Used to get settings from the disk and store them for use.
    /// </summary>
    class Settings
    {
        /// <summary>
        /// HideEmptyFields boolean property, used to determine if Empty HL7 Fields should be hidden by default
        /// </summary>
        public bool HideEmptyFields { get; set; }
        /// <summary>
        /// Extensions List property, used to setup default file extensions for opening files and searching
        /// </summary>
        public List<string> Extensions { get; set; }
        /// <summary>
        /// SearchPath string property, used to set the default search path folder
        /// </summary>
        public string SearchPath { get; set; }
        /// <summary>
        /// DefaultSegment Segments property, used to set the default segment selected in the Build Search Query form
        /// </summary>
        public Segments DefaultSegment { get; set; }
        /// <summary>
        /// CheckForUpdates, used to allow update checking at application start.
        /// </summary>
        public bool CheckForUpdates { get; set; }
        /// <summary>
        /// Pulls the settings from disk and sets the above properties
        /// </summary>
        public void GetSettings()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "Settings.xml")))
            {
                Extensions = new List<string>();
                HideEmptyFields = false;
                XmlTextReader xtr = new XmlTextReader(Path.Combine(Application.StartupPath, "Settings.xml"));
                xtr.Read();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(xtr);

                if (xDoc.SelectSingleNode("Settings/HideEmptyFields") != null)
                    HideEmptyFields = Convert.ToBoolean(xDoc.SelectSingleNode("Settings/HideEmptyFields").InnerText);
                else
                    HideEmptyFields = true;
                if (xDoc.SelectSingleNode("Settings/CheckForUpdates") != null)
                    CheckForUpdates = Convert.ToBoolean(xDoc.SelectSingleNode("Settings/CheckForUpdates").InnerText);
                else
                    CheckForUpdates = true;
                if (xDoc.SelectSingleNode("Settings/SearchPath") != null)
                    SearchPath = xDoc.SelectSingleNode("Settings/SearchPath").InnerText;
                else
                    SearchPath = "";
                if (xDoc.SelectSingleNode("Settings/DefaultSegment") != null)
                    DefaultSegment = ConvertToSegments(xDoc.SelectSingleNode("Settings/DefaultSegment").InnerText);
                else
                    DefaultSegment = Segments.PID;
                if (xDoc.SelectSingleNode("Settings/FileExtensions") != null)
                {
                    XmlNodeList extensionList = xDoc.SelectNodes("Settings/FileExtensions/Extension");
                    foreach (XmlNode ext in extensionList)
                    {
                        Extensions.Add(ext.InnerText);
                    }
                }
                else
                {                    
                    Extensions.Add("txt");
                    Extensions.Add("hl7");
                }
                xtr.Close();
            }
            else
            {
                HideEmptyFields = true;
                SearchPath = "";
                DefaultSegment = Segments.PID;
                Extensions = new List<string>();
                Extensions.Add("txt");
                Extensions.Add("hl7");
                SaveSettings();
            }
        }
        /// <summary>
        /// Saves settings to disk
        /// </summary>
        public void SaveSettings()
        {
            XmlTextWriter xtw = new XmlTextWriter(Path.Combine(Application.StartupPath, "Settings.xml"), Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Settings");
            xtw.WriteStartElement("HideEmptyFields");
            xtw.WriteString(HideEmptyFields.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("CheckForUpdates");
            xtw.WriteString(CheckForUpdates.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("SearchPath");
            xtw.WriteString(SearchPath);
            xtw.WriteEndElement();
            xtw.WriteStartElement("DefaultSegment");
            xtw.WriteString(DefaultSegment.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("FileExtensions");
            foreach (string ext in Extensions)
            {
                xtw.WriteStartElement("Extension");
                xtw.WriteString(ext);
                xtw.WriteEndElement();
            }
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.Close();
        }
        /// <summary>
        /// Converts the specified string to a Segments enum
        /// </summary>
        /// <param name="Seg">The segment string</param>
        /// <returns>The Segments enum</returns>
        public static Segments ConvertToSegments(string Seg)
        {
            Segments returnSeg = new Segments();
            foreach (Segments s in Enum.GetValues(typeof(Segments)))
            {
                if (s.ToString() == Seg.ToUpper())
                {
                    returnSeg = s;
                    break;
                }
            }
            return returnSeg;
        }
    }
}
