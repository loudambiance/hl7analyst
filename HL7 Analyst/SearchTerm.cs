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
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace HL7_Analyst
{
    /// <summary>
    /// SearchTerm Class: Used to parse a string for search terms
    /// </summary>
    public class SearchTerm
    {
        /// <summary>
        /// The ID of the search term
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// The Value of the search term
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SearchTerm() { }
        /// <summary>
        /// SearchTerm Constructor
        /// </summary>
        /// <param name="term"></param>
        public SearchTerm(string term)
        {
            SearchTerm st = GetSearchTerm(term);
            if (st != null)
            {
                ID = st.ID;
                Value = st.Value;
            }
            else
            {
                ID = "";
                Value = "";
            }
        }
        /// <summary>
        /// Gets the search term from the specified search string
        /// </summary>
        /// <param name="term">The term to get</param>
        /// <returns>Returns the SearchTerm</returns>
        public static SearchTerm GetSearchTerm(string term)
        {
            SearchTerm st = new SearchTerm();
            if (term.Contains("]"))
            {
                Regex idReg = new Regex("(?<=\\[)[A-Za-z0-9]+-[0-9]+.[0-9]+(?=\\])");
                Match idMatch = idReg.Match(term);
                st.ID = idMatch.Value;
                st.Value = idReg.Replace(term, "");
                st.Value = st.Value.Replace("[", "");
                st.Value = st.Value.Replace("]", "");
            }
            return st;
        }
        /// <summary>
        /// Builds a list of SearchTerms from a string array
        /// </summary>
        /// <param name="terms">The string array</param>
        /// <returns>A list of SearchTerms</returns>
        public static List<SearchTerm> GetSearchTerms(string[] terms)
        {
            List<SearchTerm> returnValue = new List<SearchTerm>();
            foreach (string term in terms)
            {
                SearchTerm st = GetSearchTerm(term);
                returnValue.Add(st);
            }
            return returnValue;
        }
        /// <summary>
        /// Gets the search terms from a string of search terms
        /// </summary>
        /// <param name="terms">The string of terms to search</param>
        /// <returns>The double list of search terms and search term groups</returns>
        public static List<List<SearchTerm>> GetSearchTerms(string terms)
        {
            List<List<SearchTerm>> returnValue = new List<List<SearchTerm>>();
            if (terms.Length > 0)
            {
                foreach (string term in terms.Split('|'))
                {
                    List<SearchTerm> searchGroup = new List<SearchTerm>();
                    foreach (string t in term.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        SearchTerm st = GetSearchTerm(t);
                        searchGroup.Add(st);
                    }
                    returnValue.Add(searchGroup);
                }
            }
            else
            {
                returnValue = new List<List<SearchTerm>>();
            }
            return returnValue;
        }
        /// <summary>
        /// Takes a list of search terms and builds a string representation of them
        /// </summary>
        /// <param name="terms">The list of search terms</param>
        /// <returns>The string of search terms</returns>
        public static string BuildSearchQueryString(List<SearchTerm> terms)
        {
            StringBuilder sb = new StringBuilder();

            foreach (SearchTerm term in terms)
            {
                sb.AppendFormat(" [{0}]{1}", term.ID, term.Value);
            }

            return sb.ToString().Trim();
        }
        /// <summary>
        /// Takes a list of search terms and builds a string representation of them
        /// </summary>
        /// <param name="terms">The list of search terms</param>
        /// <returns>The string of search terms</returns>
        public static string BuildSearchQueryString(List<List<SearchTerm>> terms)
        {
            StringBuilder sb = new StringBuilder();
            foreach (List<SearchTerm> list in terms)
            {
                foreach (SearchTerm term in list)
                {
                    sb.AppendFormat(" [{0}]{1}", term.ID, term.Value);
                }
                if (terms.Count > 1 && terms.LastIndexOf(list) != terms.Count - 1)
                    sb.AppendFormat("|");
            }
            return sb.ToString().Trim();
        }
        /// <summary>
        /// Pulls the previous searches from disk for autocomplete in the search terms box
        /// </summary>
        /// <returns>Returns the list of previously ran searches</returns>
        public static List<string> PullPreviousQueries()
        {
            string rootPath = Path.Combine(Application.StartupPath, "Previous Queries.xml");
            if (File.Exists(rootPath))
            {
                List<string> items = new List<string>();
                XDocument xDoc = XDocument.Load(rootPath);
                var list = from x in xDoc.Descendants("Query") select new { item = x.Value };
                foreach (var l in list)
                    items.Add(l.item.Trim());
                return items;
            }
            else
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Saves the list of previous ran searches
        /// </summary>
        /// <param name="list">The list of previously ran search queries</param>
        public static void SavePreviousQueries(List<string> list)
        {
            string rootPath = Path.Combine(Application.StartupPath, "Previous Queries.xml");
            XElement rootElement = new XElement("Searches");
            foreach (string l in list)
            {
                XElement element = new XElement("Query");
                XCData cdata = new XCData(l);
                element.Add(cdata);
                rootElement.Add(element);
            }
            rootElement.Save(rootPath);
        }
    }
}
