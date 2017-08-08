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

namespace HL7Lib.Base
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Pulls a specified segment from a list of segments by name.
        /// </summary>
        /// <param name="s">The list of segments to search.</param>
        /// <param name="SegmentName">The segment name to pull.</param>
        /// <returns>Returns the specified segment if it exists, else returns null.</returns>
        public static List<Segment> Get(this List<Segment> s, string SegmentName)
        {
            List<Segment> returnSegment = s.FindAll(delegate(Segment seg) { return seg.Name.ToUpper() == SegmentName.ToUpper(); });
            return returnSegment;
        }
        /// <summary>
        /// Pulls a specified Field from a list of Fields by Name
        /// </summary>
        /// <param name="fList">The list of fields to search</param>
        /// <param name="FieldName">The field name to pull</param>
        /// <returns>Returns the specified field if it exists, else returns null.</returns>
        public static Field Get(this List<Field> fList, string FieldName)
        {
            Field field = fList.Find(delegate(Field f) { return f.Name == FieldName; });
            return field;
        }
        /// <summary>
        /// Pulls a specified Component from a list of Components by Name
        /// </summary>
        /// <param name="cList">The list of components to search</param>
        /// <param name="ComponentName">The component name to pull</param>
        /// <returns>Returns the specified component if it exists, else returns null</returns>
        public static Component Get(this List<Component> cList, string ComponentName)
        {
            Component component = cList.Find(delegate(Component c) { return c.Name == ComponentName; });
            return component;
        }
        /// <summary>
        /// Pulls a specified Component from a list of Components byID
        /// </summary>
        /// <param name="cList">The list of components to search</param>
        /// <param name="ComponentID">The component ID to pull</param>
        /// <returns>Returns the specified component if it exists, else returns null</returns>
        public static Component GetByID(this List<Component> cList, string ComponentID)
        {
            Component component = cList.Find(delegate(Component c) { return c.ID == ComponentID; });
            return component;
        }
        /// <summary>
        /// Gets the specified component from a segment by the component ID
        /// </summary>
        /// <param name="s">The segment</param>
        /// <param name="ComponentID">The component ID</param>
        /// <returns>Returns the specified component if it exists, else returns null</returns>
        public static Component GetByID(this Segment s, string ComponentID)
        {
            var item = (from field in s.Fields where field.Components.GetByID(ComponentID) != null select field.Components.GetByID(ComponentID)).First();
            return (HL7Lib.Base.Component)item;
        }
        /// <summary>
        /// Takes a standard Component ID and converts it to a ComponentID object
        /// </summary>
        /// <param name="ComID">The Component ID string to convert</param>
        /// <returns></returns>
        public static ComponentID ConvertID(this string ComID)
        {
            ComponentID cid = new Base.ComponentID();
            string[] parts = ComID.Split('-');
            string[] idParts = parts.GetValue(1).ToString().Split('.');
            cid.SegmentName = parts.GetValue(0).ToString();
            cid.FieldIndex = Convert.ToInt32(idParts.GetValue(0).ToString());
            cid.ComponentIndex = Convert.ToInt32(idParts.GetValue(1).ToString()) - 1;
            return cid;
        }
        /// <summary>
        /// Gets a list of components from a single message by ID
        /// </summary>
        /// <param name="m">The message to search</param>
        /// <param name="ID">The ID of the component to pull</param>
        /// <returns>Returns the list of components</returns>
        public static List<Component> GetByID(this Message m, string ID)
        {
            if (!String.IsNullOrEmpty(ID))
            {
                ComponentID cid = ID.ConvertID();
                List<Component> returnValue = new List<Component>();
                var items = from com in m.Segments.Get(cid.SegmentName) where com.GetByID(ID) != null select com.GetByID(ID);
                foreach (Component c in items)
                    returnValue.Add(c);
                return returnValue;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Pulls a list of components from a list of messages based on the ID passed in
        /// </summary>
        /// <param name="msgs">The messages to pull from</param>
        /// <param name="ID">The component ID to pull</param>
        /// <returns>A list of components matching the ID passed in</returns>
        public static List<Component> GetByID(this List<Message> msgs, string ID)
        {
            var items = from com in msgs where com.GetByID(ID) != null select com.GetByID(ID);
            List<Component> coms = new List<Component>();
            foreach (List<Component> cs in items)
                foreach (Component c in cs)
                    coms.Add(c);
            return coms;
        }
        /// <summary>
        /// Gets a single component from a single message by ID and with the specified value
        /// </summary>
        /// <param name="m">The message to search</param>
        /// <param name="ID">The ID of the component to pull</param>
        /// <param name="ValueString">The value of the component to search for</param>
        /// <returns>The component returned</returns>
        public static Component GetByID(this Message m, string ID, string ValueString)
        {
            Component returnValue = new Component();
            if (!String.IsNullOrEmpty(ID))
            {
                ComponentID cid = ID.ConvertID();
                List<Segment> segments = m.Segments.Get(cid.SegmentName);
                foreach (Segment s in segments)
                {
                    Component c = s.GetByID(ID);
                    if (ValueString.ToUpper() == "NULL")
                    {
                        if (c != null && String.IsNullOrEmpty(c.Value))
                            returnValue = c;
                    }
                    else if (ValueString.ToUpper() == "!NULL")
                    {
                        if (c != null && !String.IsNullOrEmpty(c.Value))
                            returnValue = c;
                    }
                    else
                    {
                        if (c != null && c.Value != null)
                            if (c.Value.ToUpper() == ValueString.ToUpper())
                                returnValue = c;
                    }
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Removes patient identifying information from message and replaces it with made up patient data.
        /// </summary>
        /// <param name="m">The message to de-identify.</param>
        /// <returns>Returns the original message without identifying information.</returns>
        public static Message DeIdentify(this Message m)
        {
            Message msg = m;

            if (msg.Segments.Get("PID") != null)
            {
                string mrn = Helper.RandomMRN();
                string sex = msg.Segments.Get("PID")[0].Fields[8].Components[0].Value;

                msg.Segments.Get("PID")[0].Fields[2].Components[0].Value = mrn;
                msg.Segments.Get("PID")[0].Fields[3].Components[0].Value = mrn;
                msg.Segments.Get("PID")[0].Fields[4].Components[0].Value = mrn;
                msg.Segments.Get("PID")[0].Fields[5].Components[0].Value = Helper.RandomLastName();
                msg.Segments.Get("PID")[0].Fields[5].Components[1].Value = Helper.RandomFirstName(sex);
                msg.Segments.Get("PID")[0].Fields[6].Components[0].Value = Helper.RandomLastName();
                msg.Segments.Get("PID")[0].Fields[6].Components[1].Value = Helper.RandomFirstName("FEMALE");
                msg.Segments.Get("PID")[0].Fields[9].Components[0].Value = "";
                msg.Segments.Get("PID")[0].Fields[9].Components[1].Value = "";
                msg.Segments.Get("PID")[0].Fields[11].Components[0].Value = Helper.RandomAddress();
                msg.Segments.Get("PID")[0].Fields[13].Components[0].Value = "";
                msg.Segments.Get("PID")[0].Fields[13].Components[11].Value = "";
                msg.Segments.Get("PID")[0].Fields[14].Components[0].Value = "";
                msg.Segments.Get("PID")[0].Fields[14].Components[11].Value = "";
                msg.Segments.Get("PID")[0].Fields[18].Components[0].Value = mrn;
                msg.Segments.Get("PID")[0].Fields[19].Components[0].Value = "999999999";
            }
            return msg;
        }
        /// <summary>
        /// Converts a regular date into an HL7 date string.
        /// </summary>
        /// <param name="d">The date to convert.</param>
        /// <returns>Returns the HL7 date string.</returns>
        public static string ToHL7Date(this DateTime d)
        {
            return d.ToString("yyyyMMddHHmmss");
        }
        /// <summary>
        /// Converts an HL7 date string into a .Net date.
        /// </summary>
        /// <param name="HL7Date">The HL7 date string to convert.</param>
        /// <returns>Returns the date after conversion.</returns>
        public static Nullable<DateTime> FromHL7Date(this string HL7Date)
        {
            try
            {
                int y = 0;
                int M = 0;
                int d = 0;
                int H = 0;
                int m = 0;
                int s = 0;

                switch (HL7Date.Trim().Length)
                {
                    case 8:
                        y = Convert.ToInt32(HL7Date.Trim().Substring(0, 4));
                        M = Convert.ToInt32(HL7Date.Trim().Substring(4, 2));
                        d = Convert.ToInt32(HL7Date.Trim().Substring(6, 2));
                        break;
                    case 12:
                        y = Convert.ToInt32(HL7Date.Trim().Substring(0, 4));
                        M = Convert.ToInt32(HL7Date.Trim().Substring(4, 2));
                        d = Convert.ToInt32(HL7Date.Trim().Substring(6, 2));
                        H = Convert.ToInt32(HL7Date.Trim().Substring(8, 2));
                        m = Convert.ToInt32(HL7Date.Trim().Substring(10, 2));
                        break;
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        y = Convert.ToInt32(HL7Date.Trim().Substring(0, 4));
                        M = Convert.ToInt32(HL7Date.Trim().Substring(4, 2));
                        d = Convert.ToInt32(HL7Date.Trim().Substring(6, 2));
                        H = Convert.ToInt32(HL7Date.Trim().Substring(8, 2));
                        m = Convert.ToInt32(HL7Date.Trim().Substring(10, 2));
                        s = Convert.ToInt32(HL7Date.Trim().Substring(12, 2));
                        break;
                }
                return new DateTime(y, M, d, H, m, s);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the index of the specified field within the field list
        /// </summary>
        /// <param name="fList">The field list to pull</param>
        /// <param name="f">The field to get the index for</param>
        /// <returns>Returns the index of the specified field</returns>
        public static int GetIndex(this List<Field> fList, Field f)
        {
            int i = fList.FindIndex(delegate(Field field) { return field.Name == f.Name; });
            return i;
        }
        /// <summary>
        /// Gets the index of the specified component within the component list
        /// </summary>
        /// <param name="cList">The component list to pull</param>
        /// <param name="c">The component to get the index for</param>
        /// <returns>Returns the index of the specified component</returns>
        public static int GetIndex(this List<Component> cList, Component c)
        {
            int i = cList.FindIndex(delegate(Component component) { return component.Name == c.Name; });
            return i;
        }
        /// <summary>
        /// Unescapes escape sequences in the HL7 segment
        /// </summary>
        /// <param name="msg">The string to unescape</param>
        /// <param name="EscapeCharacter">The escape character being used</param>
        /// <returns>The unescaped string</returns>
        public static string UnEscape(this string msg, string EscapeCharacter)
        {
            string returnStr = msg;
            returnStr = returnStr.Replace(String.Format("{0}T{0}", EscapeCharacter), "&");
            returnStr = returnStr.Replace(String.Format("{0}S{0}", EscapeCharacter), "^");
            returnStr = returnStr.Replace(String.Format("{0}F{0}", EscapeCharacter), "|");
            returnStr = returnStr.Replace(String.Format("{0}R{0}", EscapeCharacter), "~");
            returnStr = returnStr.Replace(String.Format("{0}E{0}", EscapeCharacter), "\\");
            return returnStr;
        }
    }
}
