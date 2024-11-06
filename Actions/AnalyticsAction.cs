using ScottPlot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPCAA.WinApp.Enums;
using WPCAA.WinApp.Models;

namespace WPCAA.WinApp.Actions
{
    public class AnalyticsAction
    {
        private string[] hours = { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00",
                                    "16:00", "17:00", "18:00","19:00" ,"20:00" ,"21:00" ,"22:00" ,"23:00", "24:00" };

        private string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

        public AnalyticsAction()
        {
            
        }


        public void GenerateGraph(ref FormsPlot formsPlot, 
            List<ComputedDataForGraph> data, 
            EngagementBy selectedEngagementBy, 
            GraphFrequency selectedGraphFrequency)
        {
            string[] labels = new string[data.Count()];
            double[] positions = new double[data.Count()];
            double[] values = new double[data.Count()]; 

            if (data.Any())
            {
                if (selectedEngagementBy == EngagementBy.KeyboardClick ||
                    selectedEngagementBy == EngagementBy.MouseClick ||
                    selectedEngagementBy == EngagementBy.AverageKeyboardClick ||
                    selectedEngagementBy == EngagementBy.AverageMouseClick)
                {
                    formsPlot.Reset();

                    int i = 0;
                    foreach (var datum in data)
                    {
                        positions[i] = double.Parse(i.ToString());

                        // Engagement By
                        if (selectedEngagementBy == EngagementBy.KeyboardClick)
                        {
                            values[i] = double.Parse(datum.KeyboardClickSum.ToString());
                            formsPlot.Plot.YAxis.Label("Keyboard Clicks");
                        }
                        else if (selectedEngagementBy == EngagementBy.MouseClick)
                        {
                            values[i] = double.Parse(datum.MouseClickSum.ToString());
                            formsPlot.Plot.YAxis.Label("Mouse Clicks");
                        }
                        else if (selectedEngagementBy == EngagementBy.AverageKeyboardClick)
                        {
                            values[i] = double.Parse(datum.KeyboardClickAverage.ToString("F", CultureInfo.InvariantCulture));
                            formsPlot.Plot.YAxis.Label("Average Keyboard Clicks");
                        }
                        else if (selectedEngagementBy == EngagementBy.AverageMouseClick)
                        {
                            values[i] = double.Parse(datum.MouseClickAverage.ToString("F", CultureInfo.InvariantCulture));
                            formsPlot.Plot.YAxis.Label("Average Mouse Clicks");
                        }
                        else
                        {
                            // return;
                        }

                        // Graph Frequency
                        if (selectedGraphFrequency == GraphFrequency.Hourly)
                        {
                            labels[i++] = $"{datum.DateFrom.Month}-{datum.DateFrom.Day}-{datum.DateFrom.Year}\n{datum.DateFrom.Hour.ToString().PadLeft(2, '0')}-{datum.DateTo.Hour.ToString().PadLeft(2, '0')}";
                            formsPlot.Plot.XAxis.Label("Hourly");
                        }
                        else if (selectedGraphFrequency == GraphFrequency.Daily)
                        {
                            labels[i++] = datum.DateFrom.ToString("MMM dd yyyy");
                            formsPlot.Plot.XAxis.Label("Daily");
                        }
                        else if (selectedGraphFrequency == GraphFrequency.Monthly)
                        {
                            labels[i++] = datum.DateFrom.ToString("MMM yyyy");
                            formsPlot.Plot.XAxis.Label("Monthly");
                        }
                    }

                    var bar = formsPlot.Plot.AddBar(values, positions);
                    formsPlot.Plot.XTicks(positions, labels);
                    
                    
                    formsPlot.Plot.SetAxisLimits(yMin: 0);
                    formsPlot.Plot.YAxis.LockLimits(true);
                    // Then tell the axis to display tick labels using a time format
                    formsPlot.Plot.XAxis.DateTimeFormat(true);
                    bar.ShowValuesAboveBars = true;

                    formsPlot.Refresh();
                }
                else if (selectedEngagementBy == EngagementBy.TotalOfEachProcessType)
                {
                    if (selectedEngagementBy == EngagementBy.TotalOfEachProcessType)
                    {
                        formsPlot.Reset();

                        double[] pieValues = { data.FirstOrDefault().LifestyleProcessCount, 
                            data.FirstOrDefault().SocialMediaProcessCount, 
                            data.FirstOrDefault().EntertainmentProcessCount,
                            data.FirstOrDefault().ProductivityProcessCount,
                            data.FirstOrDefault().OtherProcessCount };

                        string[] pieLabels = { "Lifestyle", "Socials", "Entertainment", "Productivity", "Others" };

                        var pie = formsPlot.Plot.AddPie(pieValues);

                        pie.SliceLabels = pieLabels;
                        pie.ShowPercentages = true;
                        // pie.ShowValues = true;
                        pie.ShowLabels = false;

                        formsPlot.Plot.Legend();

                        formsPlot.Refresh();
                    }
                }
                
            }
            else
            {
                formsPlot.Reset();

                formsPlot.Refresh();
            }
        }
    }
}
