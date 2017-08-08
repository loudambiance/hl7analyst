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
    /// GraphItems Class: Used to store information about each graph item used in the graphs.
    /// </summary>
    public class GraphItems
    {
        /// <summary>
        /// The Name of the item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Count of items with the specified name
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public GraphItems() { }
        /// <summary>
        /// GraphItem constructor
        /// </summary>
        /// <param name="_Name">The name to assign to the GraphItem</param>
        /// <param name="_Count">The value to assign to the GraphItem</param>
        public GraphItems(string _Name, int _Count)
        {
            Name = _Name;
            Count = _Count;
        }
        /// <summary>
        /// Pulls the specified GraphItem from a list
        /// </summary>
        /// <param name="items">The list to search</param>
        /// <param name="_Name">The name of the GraphItem to find</param>
        /// <returns>The GraphItem that was found.</returns>
        public static GraphItems GetGraphItem(List<GraphItems> items, string _Name)
        {
            GraphItems gi = items.Find(delegate(GraphItems g) { return g.Name == _Name; });
            return gi;
        }
    }
}
