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

namespace FTPLib
{
    /// <summary>
    /// 
    /// </summary>
    public class FTPOptions
    {
        /// <summary>
        /// The User Name to use with this FTP Connection
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The Password to use with this FTP Connection
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// The FTP Address to use for this FTP Connection
        /// </summary>
        public string FTPAddress { get; set; }
        /// <summary>
        /// Determines if thsi FTP Connection should use passive mode
        /// </summary>
        public bool UsePassive { get; set; }
        /// <summary>
        /// Determines if this FTP Connection should use Anonymous Login
        /// </summary>
        public bool AnonymousLogin { get; set; }
        /// <summary>
        /// Determines if SSL should be used with this FTP Connection
        /// </summary>
        public bool UseSSL { get; set; }
        /// <summary>
        /// Pulls the list of FTP Connection files
        /// </summary>
        /// <returns>The list of FTP Connection files</returns>
        public static List<string> GetFTPConnections()
        {
            string rootPath = Path.Combine(Application.StartupPath, "FTP");
            if (Directory.Exists(rootPath))
            {
                List<string> s = new List<string>();
                foreach (string f in Directory.GetFiles(rootPath, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    FileInfo fi = new FileInfo(f);
                    s.Add(fi.Name.Replace(fi.Extension, ""));
                }
                return s;
            }
            else
            {
                Directory.CreateDirectory(rootPath);
                return new List<string>();
            }
        }
        /// <summary>
        /// Loads the specified FTP Connection file
        /// </summary>
        /// <param name="FTPFile">The FTP Connection file to open</param>
        /// <returns>The FTP Options pulled from the FTP Connection file</returns>
        public static FTPOptions Load(string FTPFile)
        {
            string rootPath = Path.Combine(Application.StartupPath, "FTP");
            XmlTextReader xtr = new XmlTextReader(Path.Combine(rootPath, FTPFile + ".xml"));
            xtr.Read();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xtr);

            FTPOptions ftpo = new FTPOptions();
            if (xDoc.SelectSingleNode("FTPOptions/UserName") != null)
                ftpo.UserName = xDoc.SelectSingleNode("FTPOptions/UserName").InnerText;
            if (xDoc.SelectSingleNode("FTPOptions/UserPassword") != null)
                ftpo.UserPassword = xDoc.SelectSingleNode("FTPOptions/UserPassword").InnerText;
            if (xDoc.SelectSingleNode("FTPOptions/FTPAddress") != null)
                ftpo.FTPAddress = xDoc.SelectSingleNode("FTPOptions/FTPAddress").InnerText;
            if (xDoc.SelectSingleNode("FTPOptions/UsePassive") != null)
                ftpo.UsePassive = Convert.ToBoolean(xDoc.SelectSingleNode("FTPOptions/UsePassive").InnerText);
            if (xDoc.SelectSingleNode("FTPOptions/UserName") != null)
                ftpo.AnonymousLogin = Convert.ToBoolean(xDoc.SelectSingleNode("FTPOptions/AnonymousLogin").InnerText);
            if (xDoc.SelectSingleNode("FTPOptions/UseSSL") != null)
                ftpo.UseSSL = Convert.ToBoolean(xDoc.SelectSingleNode("FTPOptions/UseSSL").InnerText);
            xtr.Close();
            return ftpo;
        }
        /// <summary>
        /// Saves the specified FTP Options Connection File.
        /// </summary>
        /// <param name="ops">The FTP Options to save</param>
        /// <param name="conName">The file name to use</param>
        public void Save(FTPOptions ops, string conName)
        {
            string rootPath = Path.Combine(Application.StartupPath, "FTP");
            XmlTextWriter xtw = new XmlTextWriter(Path.Combine(rootPath, RemoveUnsupportedChars(conName) + ".xml"), Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("FTPOptions");
            xtw.WriteStartElement("UserName");
            xtw.WriteString(ops.UserName);
            xtw.WriteEndElement();
            xtw.WriteStartElement("UserPassword");
            xtw.WriteString(ops.UserPassword);
            xtw.WriteEndElement();
            xtw.WriteStartElement("FTPAddress");
            xtw.WriteString(ops.FTPAddress);
            xtw.WriteEndElement();
            xtw.WriteStartElement("UsePassive");
            xtw.WriteString(ops.UsePassive.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("AnonymousLogin");
            xtw.WriteString(ops.AnonymousLogin.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("UseSSL");
            xtw.WriteString(ops.UseSSL.ToString());
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Close();
        }
        /// <summary>
        /// Deletes the specified connection file
        /// </summary>
        /// <param name="conName">The connection file to delete</param>
        public static void Delete(string conName)
        {
            string rootPath = Path.Combine(Application.StartupPath, "FTP");
            File.Delete(Path.Combine(rootPath, conName + ".xml"));
        }
        /// <summary>
        /// Removes the specified characters from the file name.
        /// </summary>
        /// <param name="FileName">The File Name to Clean</param>
        /// <returns>The File Name after cleaning unsupported characters from it</returns>
        private string RemoveUnsupportedChars(string FileName)
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
