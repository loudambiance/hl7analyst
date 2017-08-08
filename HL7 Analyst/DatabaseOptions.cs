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

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace HL7_Analyst
{
    /// <summary>
    /// Database Options Class: Used to store and manage database options objects used in database connections.
    /// </summary>
    public class DatabaseOptions
    {
        private static string rootPath = Path.Combine(Application.StartupPath, "Database");
        /// <summary>
        /// The SQL Connection String for this instance
        /// </summary>
        public string SQLConnectionString { get; set; }
        /// <summary>
        /// The SQL Query String for this instance
        /// </summary>
        public string SQLQuery { get; set; }
        /// <summary>
        /// The SQL Column that holds the HL7 messages to download for this instance
        /// </summary>
        public string SQLColumn { get; set; }
        /// <summary>
        /// Pulls all Database Connection files from the disk and builds a list from them
        /// </summary>
        /// <returns>Returns a list of all database connection files from the disk</returns>
        public static List<string> GetDatabaseConnections()
        {
            if (Directory.Exists(rootPath))
            {
                List<string> sl = new List<string>();
                foreach (string f in Directory.GetFiles(rootPath, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    FileInfo fi = new FileInfo(f);
                    sl.Add(fi.Name.Replace(fi.Extension, ""));
                }
                return sl;
            }
            else
            {
                Directory.CreateDirectory(rootPath);
                return new List<string>();
            }
        }
        /// <summary>
        /// Loads the specified database connection file into a DatabaseOptions object
        /// </summary>
        /// <param name="OptionsFile">The options file to load</param>
        /// <returns>Returns a DatabaseOptions object built from the file</returns>
        public static DatabaseOptions Load(string OptionsFile)
        {
            XmlTextReader xtr = new XmlTextReader(Path.Combine(rootPath, OptionsFile + ".xml"));
            xtr.Read();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xtr);

            DatabaseOptions dbOps = new DatabaseOptions();
            if (xDoc.SelectSingleNode("DatabaseOptions/ConnectionString") != null)
                dbOps.SQLConnectionString = xDoc.SelectSingleNode("DatabaseOptions/ConnectionString").InnerText;
            if (xDoc.SelectSingleNode("DatabaseOptions/Query") != null)
                dbOps.SQLQuery = xDoc.SelectSingleNode("DatabaseOptions/Query").InnerText;
            if (xDoc.SelectSingleNode("DatabaseOptions/Column") != null)
                dbOps.SQLColumn = xDoc.SelectSingleNode("DatabaseOptions/Column").InnerText;
            return dbOps;
        }
        /// <summary>
        /// Saves the specified Database Options object to disk
        /// </summary>
        /// <param name="OptionsFile">The options file name to use</param>
        /// <param name="DBOptions">The database options object to save</param>
        public static void Save(string OptionsFile, DatabaseOptions DBOptions)
        {
            XmlTextWriter xtw = new XmlTextWriter(Path.Combine(rootPath, Helper.RemoveUnsupportedChars(OptionsFile) + ".xml"), Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("DatabaseOptions");
            xtw.WriteStartElement("ConnectionString");
            xtw.WriteString(DBOptions.SQLConnectionString);
            xtw.WriteEndElement();
            xtw.WriteStartElement("Query");
            xtw.WriteString(DBOptions.SQLQuery);
            xtw.WriteEndElement();
            xtw.WriteStartElement("Column");
            xtw.WriteString(DBOptions.SQLColumn);
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Close();
        }
        /// <summary>
        /// Deletes the specified options file from disk
        /// </summary>
        /// <param name="OptionsFile">The options file to delete</param>
        public static void Delete(string OptionsFile)
        {
            File.Delete(Path.Combine(rootPath, OptionsFile + ".xml"));
        }
    }
}
