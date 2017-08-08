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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net;

namespace HL7_Analyst
{
    /// <summary>
    /// 
    /// </summary>
    class UpdateChecker
    {
        /// <summary>
        /// Reads the RSS feed from CodePlex for releases and parses the data out of it looking for new updates to the list labeled Released.
        /// </summary>
        /// <returns>Returns true if a new release is available.</returns>
        public static bool UpdateCheck()
        {
            try
            {
                bool returnValue = false;
                XDocument xDoc = XDocument.Load(@"http://hl7analyst.codeplex.com/project/feeds/rss?ProjectRSSFeed=codeplex%3a%2f%2frelease%2fhl7analyst");
                var items = from x in xDoc.Descendants("item") select new { d = x.Element("pubDate").Value, t = x.Element("title").Value };
                foreach (var i in items)
                {
                    DateTime pubDate = Convert.ToDateTime(i.d);
                    string title = i.t;
                    if (pubDate > ReadLastRunDate() && title.Contains("Released:"))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                return returnValue;
            }
            catch (WebException)
            {
                return false;
            }
        }
        /// <summary>
        /// Reads the last run date from disk
        /// </summary>
        /// <returns>Returns the last run date from disk if the file exists, if not it returns the current date/time</returns>
        private static DateTime ReadLastRunDate()
        {
            string lrdFile = Path.Combine(Application.StartupPath, "lrd.txt");
            if (File.Exists(lrdFile))
            {
                StreamReader sr = new StreamReader(lrdFile);
                string contents = sr.ReadToEnd();
                sr.Close();
                return Convert.ToDateTime(contents);
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// Saves the current date to file.
        /// </summary>
        public static void SaveLastRunDate()
        {
            string lrdFile = Path.Combine(Application.StartupPath, "lrd.txt");
            StreamWriter sw = new StreamWriter(lrdFile);
            sw.WriteLine(DateTime.Now.ToString());
            sw.Close();
        }
    }
}
