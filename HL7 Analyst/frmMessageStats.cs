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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HL7Lib.Base;
using ZedGraph;

namespace HL7_Analyst
{
    /// <summary>
    /// Message Stats form: Displays a graph (Using ZedGraph) of the specified statistics from the messages currently loaded into the HL7 Analyst.
    /// </summary>
    public partial class frmMessageStats : Form
    {
        List<string> Messages = new List<string>();
        string gTitle = "";
        string componentID = "";
        string graphType = "";

        private delegate void UpdateGraphCursorDelegate(Cursor c);
        private delegate void RefreshGraphDelegate();        
        /// <summary>
        /// Initialization Method
        /// </summary>
        /// <param name="msgs">The currently loaded messages</param>
        /// <param name="gt">The graph title to use</param>
        /// <param name="cID">The component ID to pull stats for</param>
        /// <param name="gType">The type of graph to build</param>
        public frmMessageStats(List<string> msgs, string gt, string cID, string gType)
        {
            InitializeComponent();
            Messages = msgs;
            gTitle = gt;
            componentID = cID;
            graphType = gType;
        }

        #region Cross Thread Invoke Methods
        /// <summary>
        /// Sets the graphs cursor to the specified cursor
        /// </summary>
        /// <param name="c">The cursor to use</param>
        private void UpdateGraphCursor(Cursor c)
        {
            try
            {
                if (zgGraph.IsHandleCreated)
                {
                    if (zgGraph.InvokeRequired)
                        zgGraph.Invoke(new UpdateGraphCursorDelegate(UpdateGraphCursor), c);
                    else
                        zgGraph.Cursor = c;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }
        /// <summary>
        /// Refreshes the graphs display
        /// </summary>
        private void RefreshGraph()
        {
            try
            {
                if (zgGraph.IsHandleCreated)
                {
                    if (zgGraph.InvokeRequired)
                        zgGraph.Invoke(new RefreshGraphDelegate(RefreshGraph));
                    else
                        zgGraph.Refresh();
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// Form Load Event: Sets the initial display of the graph and sets up the background worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMessageStats_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = String.Format("Message Statistics - {0}", gTitle);
                SetInitialGraphDisplay();
                zgGraph.PanButtons = MouseButtons.Left;
                zgGraph.PanModifierKeys = Keys.None;
                zgGraph.ZoomButtons = MouseButtons.Left;
                zgGraph.ZoomModifierKeys = Keys.Control;

                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                bgw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        }
        /// <summary>
        /// Background Worker Do Work Event: Performs calculation logic and creates the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateGraphCursor(Cursors.WaitCursor);
                List<GraphItems> gItems = new List<GraphItems>();
                switch (graphType.ToUpper())
                {
                    case "STAT":
                        gItems = ProcessStatChartItems();
                        break;
                    case "HOURLY":
                        gItems = ProcessHourlyStatChartItems();
                        break;
                    case "DAILY":
                        gItems = ProcessDailyStatChartItems();
                        break;
                }
                if (gItems.Count > 0)
                {
                    CreateChart(gItems);
                }
                UpdateGraphCursor(Cursors.Default);
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
            }
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculates the stat chart values on the selected message components.
        /// </summary>
        /// <returns>The list of GraphItems to display in the chart.</returns>
        private List<GraphItems> ProcessStatChartItems()
        {
            try
            {
                List<GraphItems> gItems = new List<GraphItems>();
                foreach (string m in Messages)
                {
                    HL7Lib.Base.Message msg = new HL7Lib.Base.Message(m);
                    List<HL7Lib.Base.Component> coms = msg.GetByID(componentID);
                    if (coms != null && coms.Count == 1)
                    {
                        GraphItems gi = GraphItems.GetGraphItem(gItems, coms[0].Value);
                        if (gi != null)
                        {
                            gItems.Remove(gi);
                            int count = gi.Count + 1;
                            gi = new GraphItems(coms[0].Value, count);
                            gItems.Add(gi);
                        }
                        else
                        {
                            gi = new GraphItems(coms[0].Value, 1);
                            gItems.Add(gi);
                        }
                    }
                }
                return gItems;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return new List<GraphItems>();
            }
        }
        /// <summary>
        /// Calculates the stat chart values on the Message Date/Time component of the MSH segment for Hourly Traffic Stats.
        /// </summary>
        /// <returns>The list of GraphItems to display in the chart.</returns>
        private List<GraphItems> ProcessHourlyStatChartItems()
        {
            try
            {
                int h0 = 0;
                int h1 = 0;
                int h2 = 0;
                int h3 = 0;
                int h4 = 0;
                int h5 = 0;
                int h6 = 0;
                int h7 = 0;
                int h8 = 0;
                int h9 = 0;
                int h10 = 0;
                int h11 = 0;
                int h12 = 0;
                int h13 = 0;
                int h14 = 0;
                int h15 = 0;
                int h16 = 0;
                int h17 = 0;
                int h18 = 0;
                int h19 = 0;
                int h20 = 0;
                int h21 = 0;
                int h22 = 0;
                int h23 = 0;
                List<GraphItems> gItems = new List<GraphItems>();

                foreach (string m in Messages)
                {
                    HL7Lib.Base.Message msg = new HL7Lib.Base.Message(m);
                    List<HL7Lib.Base.Component> coms = msg.GetByID(componentID);
                    if (coms != null && coms.Count == 1)
                    {
                        Nullable<DateTime> d = coms[0].Value.FromHL7Date();
                        if (d != null)
                        {
                            switch (d.Value.ToString("HH"))
                            {
                                case "00": h0++; break;
                                case "01": h1++; break;
                                case "02": h2++; break;
                                case "03": h3++; break;
                                case "04": h4++; break;
                                case "05": h5++; break;
                                case "06": h6++; break;
                                case "07": h7++; break;
                                case "08": h8++; break;
                                case "09": h9++; break;
                                case "10": h10++; break;
                                case "11": h11++; break;
                                case "12": h12++; break;
                                case "13": h13++; break;
                                case "14": h14++; break;
                                case "15": h15++; break;
                                case "16": h16++; break;
                                case "17": h17++; break;
                                case "18": h18++; break;
                                case "19": h19++; break;
                                case "20": h20++; break;
                                case "21": h21++; break;
                                case "22": h22++; break;
                                case "23": h23++; break;
                            }
                        }
                    }
                }
                gItems.Add(new GraphItems("01", h1));
                gItems.Add(new GraphItems("02", h2));
                gItems.Add(new GraphItems("03", h3));
                gItems.Add(new GraphItems("04", h4));
                gItems.Add(new GraphItems("05", h5));
                gItems.Add(new GraphItems("06", h6));
                gItems.Add(new GraphItems("07", h7));
                gItems.Add(new GraphItems("08", h8));
                gItems.Add(new GraphItems("09", h9));
                gItems.Add(new GraphItems("10", h10));
                gItems.Add(new GraphItems("11", h11));
                gItems.Add(new GraphItems("12", h12));
                gItems.Add(new GraphItems("13", h13));
                gItems.Add(new GraphItems("14", h14));
                gItems.Add(new GraphItems("15", h15));
                gItems.Add(new GraphItems("16", h16));
                gItems.Add(new GraphItems("17", h17));
                gItems.Add(new GraphItems("18", h18));
                gItems.Add(new GraphItems("19", h19));
                gItems.Add(new GraphItems("20", h20));
                gItems.Add(new GraphItems("21", h21));
                gItems.Add(new GraphItems("22", h22));
                gItems.Add(new GraphItems("23", h23));
                gItems.Add(new GraphItems("00", h0));
                return gItems;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return new List<GraphItems>();
            }
        }
        /// <summary>
        /// Calculates the stat chart values on the Message Date/Time component of the MSH segment for Daily Traffic Stats.
        /// </summary>
        /// <returns>The list of GraphItems to display in the chart.</returns>
        private List<GraphItems> ProcessDailyStatChartItems()
        {
            try
            {
                int d1 = 0;
                int d2 = 0;
                int d3 = 0;
                int d4 = 0;
                int d5 = 0;
                int d6 = 0;
                int d7 = 0;
                List<GraphItems> gItems = new List<GraphItems>();

                foreach (string m in Messages)
                {
                    HL7Lib.Base.Message msg = new HL7Lib.Base.Message(m);
                    List<HL7Lib.Base.Component> coms = msg.GetByID(componentID);
                    if (coms != null && coms.Count == 1)
                    {
                        Nullable<DateTime> d = coms[0].Value.FromHL7Date();
                        if (d != null)
                        {
                            switch (d.Value.DayOfWeek)
                            {
                                case DayOfWeek.Sunday: d1++; break;
                                case DayOfWeek.Monday: d2++; break;
                                case DayOfWeek.Tuesday: d3++; break;
                                case DayOfWeek.Wednesday: d4++; break;
                                case DayOfWeek.Thursday: d5++; break;
                                case DayOfWeek.Friday: d6++; break;
                                case DayOfWeek.Saturday: d7++; break;
                            }
                        }
                    }
                }
                gItems.Add(new GraphItems("Sunday", d1));
                gItems.Add(new GraphItems("Monday", d2));
                gItems.Add(new GraphItems("Tuesday", d3));
                gItems.Add(new GraphItems("Wednesday", d4));
                gItems.Add(new GraphItems("Thursday", d5));
                gItems.Add(new GraphItems("Friday", d6));
                gItems.Add(new GraphItems("Saturday", d7));
                return gItems;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();
                return new List<GraphItems>();
            }
        }
        /// <summary>
        /// Creates the chart using the specified GraphItems.
        /// </summary>
        /// <param name="gItems">GraphItems to use</param>
        private void CreateChart(List<GraphItems> gItems)
        {
            try
            {
                List<double> y = new List<double>();
                List<string> lbls = new List<string>();

                for (int i = 0; i < gItems.Count; i++)
                {
                    y.Add((double)gItems[i].Count);
                    if (!String.IsNullOrEmpty(gItems[i].Name))
                        lbls.Add(gItems[i].Name);
                    else
                        lbls.Add("Blank");
                }
                GraphPane myPane = zgGraph.GraphPane;
                myPane.XAxis.Scale.TextLabels = lbls.ToArray();

                BarItem myCurve = myPane.AddBar(gTitle, null, y.ToArray(), Color.White);
                myCurve.Bar.Fill.Color = Color.CornflowerBlue;
                myCurve.Bar.Fill.Type = FillType.GradientByY;
                myCurve.Label.IsVisible = true;

                zgGraph.AxisChange();
                RefreshGraph();
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();                
            }
        }
        /// <summary>
        /// Sets the initial display values for the graph.
        /// </summary>
        private void SetInitialGraphDisplay()
        {
            try
            {
                GraphPane myPane = zgGraph.GraphPane;
                myPane.Title.Text = gTitle;
                myPane.Title.FontSpec.IsItalic = true;
                myPane.Title.FontSpec.Size = 24f;
                myPane.Title.FontSpec.Family = "Times New Roman";
                myPane.Fill = new Fill(Color.White, Color.Goldenrod, 45.0f);
                myPane.Chart.Fill.Type = FillType.None;
                myPane.XAxis.MajorTic.IsBetweenLabels = true;
                myPane.XAxis.Type = AxisType.Text;
                myPane.XAxis.Scale.FontSpec.Angle = 45;
                myPane.XAxis.Title.Text = "Item Name";
                myPane.YAxis.Title.Text = "Count";
                myPane.Legend.IsVisible = false;
            }
            catch (Exception ex)
            {
                Log.LogException(ex).ShowDialog();                
            }
        } 
        #endregion
    }
}
