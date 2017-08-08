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
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace HL7_Analyst
{
    /// <summary>
    /// TCPIPOtions Class: Used to create a TCPIP Options object and perform operations on it.
    /// </summary>
    public class TCPIPOptions
    {
        private static string rootPath = Path.Combine(Application.StartupPath, "TCPIP");
        private IPAddress _HostAddress = IPAddress.Parse("127.0.0.1");
        private int _Port = 4200;
        private string _LLPHeader = LLP.GetLLPString("[0x0B]");
        private string _LLPTrailer = LLP.GetLLPString("[0x1C][0x0D]");
        private bool _WaitForAck = true;
        private bool _SendAck = true;
        /// <summary>
        /// The Host Address of this TCP/IP Object
        /// </summary>
        public IPAddress HostAddress
        {
            get { return _HostAddress; }
            set { _HostAddress = value; }
        }
        /// <summary>
        /// The Port of this TCP/IP Object
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        /// <summary>
        /// The LLP Header character(s) to use for this TCP/IP Object
        /// </summary>
        public string LLPHeader
        {
            get { return _LLPHeader; }
            set { _LLPHeader = value; }
        }
        /// <summary>
        /// The LLP Trailer character(s) to use for this TCP/IP Object
        /// </summary>
        public string LLPTrailer
        {
            get { return _LLPTrailer; }
            set { _LLPTrailer = value; }
        }
        /// <summary>
        /// The Wait for Ack option for this TCP/IP Object, used to determine if an ack should be waited for before sending the next message.
        /// </summary>
        public bool WaitForAck
        {
            get { return _WaitForAck; }
            set { _WaitForAck = value; }
        }
        /// <summary>
        /// The Send Ack option for this TCP/IP Object, used to determine if an ack should be sent for each received message.
        /// </summary>
        public bool SendAck
        {
            get { return _SendAck; }
            set { _SendAck = value; }
        }
        /// <summary>
        /// Pulls all TCP/IP Connection files from disk.
        /// </summary>
        /// <returns>The list of connection files</returns>
        public static List<string> GetTCPIPConnections()
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
        /// Loads a specific TCP/IP Options file from disk
        /// </summary>
        /// <param name="OptionsFile">The option file to load</param>
        /// <returns>The TCPIPOptions object created from the file</returns>
        public static TCPIPOptions Load(string OptionsFile)
        {
            XmlTextReader xtr = new XmlTextReader(Path.Combine(rootPath, OptionsFile + ".xml"));
            xtr.Read();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xtr);

            TCPIPOptions ops = new TCPIPOptions();
            if (xDoc.SelectSingleNode("TCPIPOptions/HostAddress") != null)
                ops.HostAddress = IPAddress.Parse(xDoc.SelectSingleNode("TCPIPOptions/HostAddress").InnerText);
            if (xDoc.SelectSingleNode("TCPIPOptions/Port") != null)
                ops.Port = Int32.Parse(xDoc.SelectSingleNode("TCPIPOptions/Port").InnerText);
            if (xDoc.SelectSingleNode("TCPIPOptions/LLPHeader") != null)
                ops.LLPHeader = xDoc.SelectSingleNode("TCPIPOptions/LLPHeader").InnerText;
            if (xDoc.SelectSingleNode("TCPIPOptions/LLPTrailer") != null)
                ops.LLPTrailer = xDoc.SelectSingleNode("TCPIPOptions/LLPTrailer").InnerText;
            if (xDoc.SelectSingleNode("TCPIPOptions/WaitForAck") != null)
                ops.WaitForAck = Convert.ToBoolean(xDoc.SelectSingleNode("TCPIPOptions/WaitForAck").InnerText);
            if (xDoc.SelectSingleNode("TCPIPOptions/SendAck") != null)
                ops.SendAck = Convert.ToBoolean(xDoc.SelectSingleNode("TCPIPOptions/SendAck").InnerText);
            xtr.Close();
            return ops;
        }
        /// <summary>
        /// Saves the specified connections to disk
        /// </summary>
        /// <param name="OptionsFile">The File name to use</param>
        /// <param name="Options">The TCP/IP Connections to use</param>
        public static void Save(string OptionsFile, TCPIPOptions Options)
        {
            XmlTextWriter xtw = new XmlTextWriter(Path.Combine(rootPath, Helper.RemoveUnsupportedChars(OptionsFile) + ".xml"), Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("TCPIPOptions");
            xtw.WriteStartElement("HostAddress");
            xtw.WriteString(Options.HostAddress.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("Port");
            xtw.WriteString(Options.Port.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("LLPHeader");
            xtw.WriteString(Options.LLPHeader);
            xtw.WriteEndElement();
            xtw.WriteStartElement("LLPTrailer");
            xtw.WriteString(Options.LLPTrailer);
            xtw.WriteEndElement();
            xtw.WriteStartElement("WaitForAck");
            xtw.WriteString(Options.WaitForAck.ToString());
            xtw.WriteEndElement();
            xtw.WriteStartElement("SendAck");
            xtw.WriteString(Options.SendAck.ToString());
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Close();
        }
        /// <summary>
        /// Deletes a TCP/IP Connection file from disk
        /// </summary>
        /// <param name="OptionsFile">The TCP/IP Connection file to delete</param>
        public static void Delete(string OptionsFile)
        {
            File.Delete(Path.Combine(rootPath, OptionsFile + ".xml"));
        }
    }
}
