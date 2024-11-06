using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPCAA.WinApp.Customizations;
using WPCAA.WinApp.Enums;
using WPCAA.WinApp.Extensions;
using WPCAA.WinApp.Models;

namespace WPCAA.WinApp.Forms
{
    public partial class ReportViewerForm : Form
    {
        private List<ComputedDataForGraph> _computedDataForGraph;
        private GraphFrequency _graphFrequency;
        private ReportContent _reportContent;
        private AppUser _appUser;

        private DateTime _dateFrom;
        private DateTime _dateTo;
        public ReportViewerForm(List<ComputedDataForGraph> computedDataForGraphs, GraphFrequency graphFrequency, AppUser appUser, DateTime dateFrom, DateTime dateTo, ReportContent reportContent)
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);

            _computedDataForGraph = computedDataForGraphs;
            _graphFrequency = graphFrequency;
            _appUser = appUser;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            _reportContent = reportContent;
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            if (_reportContent == ReportContent.KeyboardAndMouseClick)
            {
                if (_graphFrequency == GraphFrequency.Hourly)
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.HourlyClicksReport.rdlc";
                }
                else if (_graphFrequency == GraphFrequency.Daily)
                {
                    _computedDataForGraph.ForEach(a => a.DateFrom = DateTimeOffset.Parse(a.DateFrom.Date.ToString()));

                    reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.DailyClicksReport.rdlc";
                }
                else if (_graphFrequency == GraphFrequency.Monthly)
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.MonthlyClicksReport.rdlc";
                }
            }
            else if (_reportContent == ReportContent.Processes)
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.ProcessesReport.rdlc";
            }
            else if (_reportContent == ReportContent.Attendance)
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.AttendancesReport.rdlc";
            }
            else if (_reportContent == ReportContent.TrackingDetail)
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "WPCAA.WinApp.Reports.TrackingDetailReport.rdlc";
            }

            ReportDataSource reportDataSource = new ReportDataSource();
            // Must match the DataSource in the RDLC
            reportDataSource.Name = "ComputedDataForGraphDataSet";
            reportDataSource.Value = _computedDataForGraph.ToDataSet().Tables[0];

            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Set parameters
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("CurrentUser", _appUser.Username);
            parameters[1] = new ReportParameter("DateFrom", _dateFrom.ToString("D"));
            parameters[2] = new ReportParameter("DateTo", _dateTo.ToString("D"));
            reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            reportViewer1.RefreshReport();
        }
    }
}
