﻿// ------------------------------------------------------------------------------------------------------
// LightningChart® example code: Chart with Multiple Axes.
//
// If you need any assistance, or notice error in this example code, please contact support@arction.com. 
//
// Permission to use this code in your application comes with LightningChart® license. 
//
// http://arction.com/ | support@arction.com | sales@arction.com
//
// © Arction Ltd 2009-2017. All rights reserved.  
// ------------------------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

// Arction namespaces
using Arction.WinForms.Charting;                // LightningChartUltimate and general types
using Arction.WinForms.Charting.Axes;           // Axes
using Arction.WinForms.Charting.SeriesXY;       // Series for 2D chart

namespace MultipleAxes_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Create chart instance and store it member variable.
            var chart = new LightningChartUltimate(/*Type your License key here...*/);

            // Set chart control into the parent container.
            chart.Parent = this;         //Set form as parent 
            chart.Dock = DockStyle.Fill; //Maximize to parent client area

            // Prepare data for line-series.
            var rand = new Random();
            int pointCounter = 70;

            var data = new SeriesPoint[pointCounter];
            for (int i = 0; i < pointCounter; i++)
            {
                data[i].X = (double)i;
                data[i].Y = rand.Next(0, 100);
            }

            // Add PointLineSeries for variable-interval data, progressing by X.
            var series = new PointLineSeries(chart.ViewXY, chart.ViewXY.XAxes[0], chart.ViewXY.YAxes[0]);
            series.LineStyle.Color = Color.Orange;
            series.Title.Text = "Random data";
            series.Points = data;
            chart.ViewXY.PointLineSeries.Add(series);
                        
            // Prepare new data for new line-series with differen algorithm.
            data = new SeriesPoint[pointCounter];
            for (int i = 0; i < pointCounter; i++)
            {
                data[i].X = (double)i;
                data[i].Y = Math.Sin(i * 0.2) * 50 + 50;
            }

            // Color for second axis and series
            Color color = Color.FromArgb(255, 255, 67, 0);

            // 1. Create y-axis instance
            var newAxisY = new AxisY(chart.ViewXY);
            newAxisY.AxisColor = color;
            newAxisY.MajorGrid.Visible = false;

            // 2. Add the y-axis into list of YAxes
            chart.ViewXY.YAxes.Add(newAxisY);

            // 3. Add one more PointLineSeries for sinusoidal data.
            //    Configure by setting another color and pattern for the line.
            var series2 = new PointLineSeries();    // = new PointLineSeries(chart.ViewXY, chart.ViewXY.XAxes[0], newAxisY /*(or chart.ViewXY.YAxes[1])*/);
            series2.LineStyle.Color = color;
            series2.LineStyle.Pattern = LinePattern.DashDot;
            series2.Title.Text = "Sinus data";
            series2.Points = data;

            // 4. If PointLineSeries constructor is empty or with wrong axes instances. 
            //    Assign axis index to apply current series to specific axes.
            series2.AssignXAxisIndex = 0;
            series2.AssignYAxisIndex = 1;

            // 5. Add the series into list of point-line-series.
            chart.ViewXY.PointLineSeries.Add(series2);

            // Auto-scale X and Y axes.
            chart.ViewXY.ZoomToFit();

            #region Hiden polishing

            CusomizeChart(chart);

            #endregion
        }

        void CusomizeChart(LightningChartUltimate chart)
        {
            chart.Background.Color = Color.FromArgb(255, 30, 30, 30);
            chart.Background.GradientFill = GradientFill.Solid;
            chart.ViewXY.GraphBackground.Color = Color.FromArgb(255, 20, 20, 20);
            chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            chart.Title.Color = Color.FromArgb(255, 249, 202, 3);
            chart.Title.MouseHighlight = MouseOverHighlight.None;

            foreach (var yAxis in chart.ViewXY.YAxes)
            {
                yAxis.Title.Color = Color.FromArgb(255, 249, 202, 3);
                yAxis.Title.MouseHighlight = MouseOverHighlight.None;
                yAxis.MajorGrid.Color = Color.FromArgb(35, 255, 255, 255);
                yAxis.MajorGrid.Pattern = LinePattern.Solid;
                yAxis.MinorDivTickStyle.Visible = false;
            }

            foreach (var xAxis in chart.ViewXY.XAxes)
            {
                xAxis.Title.Color = Color.FromArgb(255, 249, 202, 3);
                xAxis.Title.MouseHighlight = MouseOverHighlight.None;
                xAxis.MajorGrid.Color = Color.FromArgb(35, 255, 255, 255);
                xAxis.MajorGrid.Pattern = LinePattern.Solid;
                xAxis.MinorDivTickStyle.Visible = false;
                xAxis.ValueType = AxisValueType.Number;
            }
        }
    }
}
