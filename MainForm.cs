
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPCAA.WinApp.Actions;
using WPCAA.WinApp.Customizations;
using WPCAA.WinApp.Enums;
using WPCAA.WinApp.Extensions;
using WPCAA.WinApp.Forms;
using WPCAA.WinApp.Helpers;
using WPCAA.WinApp.Models;
using WPCAA.WinApp.Properties;
using WPCAA.WinApp.Repositories;

namespace WPCAA.WinApp
{
    public partial class MainForm : Form
    {
        private string[] _lifestyleApps = { "Uber Eats", "Health", "Pay Maya", "Amazon", "eBay", "Spotify", "Peloton", "Door Dash", "Tinder", "Airbnb" };
        private string[] _socialsApps = { "Facebook", "Twitter", "Instagram", "Linkedin", "WhatsApp", "Viber", "WeChat", "Snapchat", "Reddit", "Quora", "Messenger", "Pinterest" };
        private string[] _entertainmentApps = { "Spotify", "Netflix", "Youtube", "Movies", "Music", "TikTok", "Podcast One", "Movies TV", "iTunes", "Duolingo", "Dub Smash" };
        private string[] _productivityApps = { "Zoom", "Microsoft Teams", "Microsoft Office", "Word", "Excel", "Google Sheets", "Google", "Canva", "Wikipedia", "Google Meet", "Google Classroom", "Calendar", "Reddit" };

        private const int DashboardIndex = 0;
        private const int AnalyticsIndex = 1;
        private const int TrackingIndex = 2;
        private const int ReportsIndex = 3;
        private const int AboutIndex = 4;

        private int _currentTrackingId = 0;
        private int _currentTrackingDetailId = 0;
        private int _selectedUserIdForGraph = 0;
        private int _selectedUserIdForReport = 0;

        private bool _isNavDashboardSelected;
        private bool _isNavAnalyticsSelected;
        private bool _isNavTrackingSelected;
        private bool _isNavReportsSelected;
        private bool _isNavAboutSelected;

        private bool _isNavShrink = false;
        private bool _isUserMenuOpened = false;
        private bool _isTrackingOngoing = false;

        private TrackingAction _trackingAction;

        private AnalyticsAction _analyticsAction;

        private AppUserRepository _appUserRepository;

        private AppUser _currentAppUser;

        private Form _parentLoginForm;

        private TrackingRepository _trackingRepository;
        //public MainForm()
        //{
        //    InitializeComponent();

        //    _trackingAction = new TrackingAction();
        //    _trackingRepository = new TrackingRepository();

        //    SetStateOfTimeInOutButtons();
        //    SetStateOfFileTypeLogo();

        //    _isNavDashboardSelected = true;
        //    _isNavAnalyticsSelected = _isNavTrackingSelected = _isNavReportsSelected = _isNavAboutSelected = false;
        //    SetActiveNavButton();
        //    HideTabHeaderOnTabControl();
        //}

        public MainForm(AppUser currentAppUser, LoginForm loginForm)
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);

            _trackingAction = new TrackingAction();
            _trackingRepository = new TrackingRepository();
            _analyticsAction = new AnalyticsAction();
            _appUserRepository = new AppUserRepository();

            SetStateOfTimeInOutButtons();
            SetStateOfGraphRadio();

            _isNavDashboardSelected = true;
            _isNavAnalyticsSelected = _isNavTrackingSelected = _isNavReportsSelected = _isNavAboutSelected = false;
            SetActiveNavButton();
            HideTabHeaderOnTabControl();

            _parentLoginForm = loginForm;
            _currentAppUser = currentAppUser;

            lblCurrentUsername.Text = $"{_currentAppUser.FirstName} {_currentAppUser.LastName}";
            lblUserType.Text = _currentAppUser.UserType.ToLower().ToCamelCase();
            lblDashboardDateTime.Text = DateTime.Now.ToString("MMM dd, yyyy");

            GenerateDashboardGraph();

            if (_currentAppUser.UserType == "ADMIN")
            {
                grpBoxAnalyticsSelectEmployee.Visible = true;
                grpReportsSelectEmployee.Visible = true;

                LoadUsers();
            }
            else
            {
                _selectedUserIdForGraph = _currentAppUser.AppUserId;
                _selectedUserIdForReport = _currentAppUser.AppUserId;

                grpBoxAnalyticsSelectEmployee.Visible = false;
                grpReportsSelectEmployee.Visible = false;

                btnGenerateGraph.Top = 333;
            }

        }
        private async void LoadUsers()
        {
            var users = await _appUserRepository.GetUsers();

            cmbAnalyticsUsers.Items.Clear();
            cmbReportsUsers.Items.Clear();

            foreach(var user in users)
            {
                cmbAnalyticsUsers.Items.Add($"{user.AppUserId}. {user.FirstName} {user.LastName}");
                cmbReportsUsers.Items.Add($"{user.AppUserId}. {user.FirstName} {user.LastName}");
            }
        }

        #region Navigation
        private void OpenDashboard()
        {
            _isNavDashboardSelected = true;
            _isNavAnalyticsSelected = _isNavTrackingSelected = _isNavReportsSelected = _isNavAboutSelected = false;

            SetActiveNavButton();

            tabControlContent.SelectedTab = tabControlContent.TabPages[DashboardIndex];
        }
        private void OpenAnalytics()
        {
            _isNavAnalyticsSelected = true;
            _isNavDashboardSelected = _isNavTrackingSelected = _isNavReportsSelected = _isNavAboutSelected = false;
            SetActiveNavButton();

            tabControlContent.SelectedTab = tabControlContent.TabPages[AnalyticsIndex];
        }
        private void OpenTracking()
        {
            _isNavTrackingSelected = true;
            _isNavDashboardSelected = _isNavAnalyticsSelected = _isNavReportsSelected = _isNavAboutSelected = false;
            SetActiveNavButton();

            tabControlContent.SelectedTab = tabControlContent.TabPages[TrackingIndex];
        }
        private void OpenReports()
        {
            _isNavReportsSelected = true;
            _isNavDashboardSelected = _isNavAnalyticsSelected = _isNavTrackingSelected = _isNavAboutSelected = false;
            SetActiveNavButton();

            tabControlContent.SelectedTab = tabControlContent.TabPages[ReportsIndex];
        }
        private void OpenAbout()
        {
            _isNavAboutSelected = true;
            _isNavDashboardSelected = _isNavAnalyticsSelected = _isNavTrackingSelected = _isNavReportsSelected = false;
            SetActiveNavButton();

            tabControlContent.SelectedTab = tabControlContent.TabPages[AboutIndex];
        }
        private void HideTabHeaderOnTabControl()
        {
            tabControlContent.Appearance = TabAppearance.FlatButtons;
            tabControlContent.ItemSize = new Size(0, 1);
            tabControlContent.SizeMode = TabSizeMode.Fixed;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenDashboard();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            OpenAnalytics();
        }

        private void btnTracking_Click(object sender, EventArgs e)
        {
            OpenTracking();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenReports();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            OpenAbout();
        }

        private void SetActiveNavButton()
        {
            btnDashboard.SetActiveStateOfNavButton(_isNavDashboardSelected);
            btnAnalytics.SetActiveStateOfNavButton(_isNavAnalyticsSelected);
            btnTracking.SetActiveStateOfNavButton(_isNavTrackingSelected);
            btnReports.SetActiveStateOfNavButton(_isNavReportsSelected);
            btnAbout.SetActiveStateOfNavButton(_isNavAboutSelected);
        }
        private void btnBurger_Click(object sender, EventArgs e)
        {
            ResizeNavPanel();
        }

        private void ResizeNavPanel()
        {
            if (_isNavShrink)
            {
                panelNavigation.Width = 244;

                _isNavShrink = false;

                lblShortAppName.Visible = true;
                lblFullAppName.Visible = true;

                ShowHideIcons();
            }
            else
            {
                // Nav shrink
                panelNavigation.Width = 60;

                _isNavShrink = true;

                lblShortAppName.Visible = false;
                lblFullAppName.Visible = false;

                ShowHideIcons();
            }
        }

        private void ShowHideIcons()
        {
            if (_isNavShrink)
            {
                btnAnalytics.Text = btnDashboard.Text = btnTracking.Text = btnReports.Text = btnAbout.Text = "";
            }
            else
            {
                btnAnalytics.Text = "Analytics";
                btnDashboard.Text = "Dashboard";
                btnTracking.Text = "Tracking";
                btnReports.Text = "Reports";
                btnAbout.Text = "About";
            }
        }

        #endregion End of Navigation

        #region Tracking
        private void btnTimeIn_Click(object sender, EventArgs e) => TimeIn();

        private void btnTimeOut_Click(object sender, EventArgs e) => TimeOut();


        private ProcessType FindProcessType(string processName)
        {
            for(int i = 0; i < _lifestyleApps.Length; i++)
            {
                if(processName.ToUpper().Contains(_lifestyleApps[i].ToUpper()))
                {
                    return ProcessType.Lifestyle;
                }
            }

            for (int i = 0; i < _socialsApps.Length; i++)
            {
                if (processName.ToUpper().Contains(_socialsApps[i].ToUpper()))
                {
                    return ProcessType.Socials;
                }
            }

            for (int i = 0; i < _entertainmentApps.Length; i++)
            {
                if (processName.ToUpper().Contains(_entertainmentApps[i].ToUpper()))
                {
                    return ProcessType.Entertainment;
                }
            }

            for (int i = 0; i < _productivityApps.Length; i++)
            {
                if (processName.ToUpper().Contains(_productivityApps[i].ToUpper()))
                {
                    return ProcessType.Productivity;
                }
            }

            return ProcessType.Unknown;
        }

        private void timerToUpdateLabel_Tick(object sender, EventArgs e)
        {
           
            int kClicks = _trackingAction.GetKeyboardClicks();
            int mClicks = _trackingAction.GetMouseClicks();
            string processName = _trackingAction.GetActiveWindow();

            lblTotalKeystrokes.Text = kClicks.ToString();
            lblTotalMouseclicks.Text = mClicks.ToString();
            lblActiveWindow.Text = processName;

            // TODO
            // if _currentTrasckingDetailId > 0 
            // _currentTrackingDetailId = startTrackingDetail
            // else
            // _currentTrackingDetailId = endTrackingDetail

            if (_currentTrackingDetailId == 0)
            {
                Task.Run(async () =>
                {
                    _currentTrackingDetailId = await _trackingRepository.StartTrackingDetailAsync(
                    new TrackingDetail
                    {
                        StartTrackDateTime = DateTimeOffset.Now,
                        TrackingId = _currentTrackingId
                    }, _currentAppUser.Token);
                });
            }
            else
            {
                Task.Run(async () =>
                {
                    _currentTrackingDetailId = await _trackingRepository.EndTrackingDetailAsync(
                    new TrackingDetail
                    {
                        EndTrackDateTime = DateTimeOffset.Now,
                        KeyboardClickCount = kClicks,
                        MouseClickCount = mClicks,
                        ProcessName = processName,
                        ProcessTypeId = (int)FindProcessType(processName),
                        TrackingDetailId = _currentTrackingDetailId,
                        TrackingId = _currentTrackingId
                    }, _currentAppUser.Token);

                    _trackingAction.ResetClicks();
                });

                
            }

        }


        private async void TimeIn()
        {
            try
            {
                _currentTrackingId = await _trackingRepository.StartTrackingAsync(new Tracking { TimeInDate = DateTimeOffset.Now, AppUserId = _currentAppUser.AppUserId }, _currentAppUser.Token);

                _trackingAction.Subscribe();

                timerToUpdateLabel.Enabled = _isTrackingOngoing = true;
                SetStateOfTimeInOutButtons();
            }
            catch (Exception ex) {
                MessageBox.Show($"Oops! An error occured. \nDetails: {ex}");
            }
            
        }

        private async void TimeOut()
        {
            try
            {
                _currentTrackingId = await _trackingRepository.EndTrackingAsync(new Tracking { TimeOutDate = DateTimeOffset.Now, TrackingId = _currentTrackingId }, _currentAppUser.Token);

                _trackingAction.Unsubscribe();

                timerToUpdateLabel.Enabled = _isTrackingOngoing = false;
                SetStateOfTimeInOutButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops! An error occured. \nDetails: {ex}");
            }
            // API POST
        }

        private void SetStateOfTimeInOutButtons()
        {
            if (_isTrackingOngoing)
            {
                btnTimeIn.EnableDisableButton(!_isTrackingOngoing, .3, 65, 227, 179);
                btnTimeOut.EnableDisableButton(_isTrackingOngoing, 1, 255, 0, 0);
            }
            else
            {
                btnTimeIn.EnableDisableButton(!_isTrackingOngoing, 1, 65, 227, 179);
                btnTimeOut.EnableDisableButton(_isTrackingOngoing, .3, 255, 0, 0);
            }
        }

        #endregion End of Tracking

        #region Reports

        private void btnPreviewReport_Click(object sender, EventArgs e)
        {
            var graphFrequency = GetReportFrequency();

            var reportContent = GetReportContent();

            OpenReport(reportContent, graphFrequency);
        }

        private EngagementBy GetEngagementBy()
        {
            if (radEngagementByKeyboardClick.Checked) return EngagementBy.KeyboardClick;
            else if (radEngagementByMouseClick.Checked) return EngagementBy.MouseClick;
            else if (radEngagementByAverageKeyboardClick.Checked) return EngagementBy.AverageKeyboardClick;
            else if (radEngagementByAverageMouseClick.Checked) return EngagementBy.AverageMouseClick;
            else if (radEngagementByTotalOfEachProcessType.Checked) return EngagementBy.TotalOfEachProcessType;
            else return EngagementBy.KeyboardClick;
        }

        private ReportContent GetReportContent()
        {
            if (radKeyboardMouseClick.Checked) return ReportContent.KeyboardAndMouseClick;
            else if (radProcesses.Checked) return ReportContent.Processes;
            else if (radAttendance.Checked) return ReportContent.Attendance;
            else if (radTrackingDetail.Checked) return ReportContent.TrackingDetail;
            else return ReportContent.KeyboardAndMouseClick;
        }
        private GraphFrequency GetGraphFrequency()
        {
            if (radGraphHourly.Checked) return GraphFrequency.Hourly;
            else if (radGraphDaily.Checked) return GraphFrequency.Daily;
            else if (radGraphMonthly.Checked) return GraphFrequency.Monthly;
            else return GraphFrequency.Hourly;
        }

        private GraphFrequency GetReportFrequency()
        {
            if (radReportHourly.Checked) return GraphFrequency.Hourly;
            else if (radReportDaily.Checked) return GraphFrequency.Daily;
            else if (radReportMonthly.Checked) return GraphFrequency.Monthly;
            else return GraphFrequency.Hourly;
        }

        private async void OpenReport(ReportContent reportContent, GraphFrequency graphFrequency)
        {
            this.Cursor = Cursors.WaitCursor;
            panelReportsLoading.Visible = true;

            panelNavigation.Enabled = false;
            grpBoxReportContent.Enabled = false;
            grpBoxReportFrequency.Enabled = false;
            grpBoxReportDateRange.Enabled = false;
            grpReportsSelectEmployee.Enabled = false;
            btnPreviewReport.Enabled = false;

            var list = new List<ComputedDataForGraph>();

            if (reportContent == ReportContent.KeyboardAndMouseClick)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForReport,
                        DateTimeOffset.Parse($"{dateReportFrom.Value.Year}-{dateReportFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateReportFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        DateTimeOffset.Parse($"{dateReportTo.Value.Year}-{dateReportTo.Value.Month.ToString().PadLeft(2, '0')}-{dateReportTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        graphFrequency,
                        isForClicks: true,
                        _currentAppUser.Token);
            }
            else if (reportContent == ReportContent.Processes)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForReport,
                        DateTimeOffset.Parse($"{dateReportFrom.Value.Year}-{dateReportFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateReportFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        DateTimeOffset.Parse($"{dateReportTo.Value.Year}-{dateReportTo.Value.Month.ToString().PadLeft(2, '0')}-{dateReportTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        graphFrequency,
                        isForClicks: false,
                        _currentAppUser.Token);
            }
            else if (reportContent == ReportContent.Attendance)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForReport,
                        DateTimeOffset.Parse($"{dateReportFrom.Value.Year}-{dateReportFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateReportFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        DateTimeOffset.Parse($"{dateReportTo.Value.Year}-{dateReportTo.Value.Month.ToString().PadLeft(2, '0')}-{dateReportTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        graphFrequency,
                        isForClicks: false,
                        _currentAppUser.Token,
                        isForAttendance: true);
            }
            else if (reportContent == ReportContent.TrackingDetail)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForReport,
                        DateTimeOffset.Parse($"{dateReportFrom.Value.Year}-{dateReportFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateReportFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        DateTimeOffset.Parse($"{dateReportTo.Value.Year}-{dateReportTo.Value.Month.ToString().PadLeft(2, '0')}-{dateReportTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                        graphFrequency,
                        isForClicks: false,
                        _currentAppUser.Token,
                        isForTrackingDetail: true);
            }


            var reportViewerForm = new ReportViewerForm(list, graphFrequency, _currentAppUser, dateReportFrom.Value, dateReportTo.Value, reportContent);

            reportViewerForm.ShowDialog();

            this.Cursor = Cursors.Default;
            panelReportsLoading.Visible = false;

            panelNavigation.Enabled = true;
            grpBoxReportContent.Enabled = true;
            grpBoxReportFrequency.Enabled = true;
            grpBoxReportDateRange.Enabled = true;
            grpReportsSelectEmployee.Enabled = true;
            btnPreviewReport.Enabled = true;
        }

        private void ReportContent_CheckedChanged(object sender, EventArgs e)
        {
            grpBoxReportFrequency.Enabled = radKeyboardMouseClick.Checked;

            grpBoxReportDateRange.Enabled = !radAttendance.Checked;
        }

        #endregion End of Reports

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _parentLoginForm.Show();
            this.Dispose();
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            SetStateOfGraphRadio();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            SetStateOfGraphRadio();
        }

        private void SetStateOfGraphRadio()
        {
            dateTo.MinDate = dateFrom.Value;

            var spanBetween = dateTo.Value.Date - dateFrom.Value.Date;

            // radGraphDaily.Enabled = spanBetween.Days > 7;
            radGraphMonthly.Enabled = spanBetween.Days > 30;
        }

        private async void btnGenerateGraph_Click(object sender, EventArgs e)
        {
            if (_selectedUserIdForGraph == 0)
            {
                MessageBox.Show("Select a user first!");

                return;
            }

            this.Cursor = Cursors.WaitCursor;
            panelAnalyticsLoading.Visible = true;

            panelNavigation.Enabled = false;
            grpBoxEngagementBy.Enabled = false;
            grpBoxGraphFrequency.Enabled = false;
            grpBoxGraphDateRange.Enabled = false;
            grpBoxAnalyticsSelectEmployee.Enabled = false;
            btnGenerateGraph.Enabled = false;

            var list = new List<ComputedDataForGraph>();

            var frequency = GetGraphFrequency();

            var engagementBy = GetEngagementBy();

            if (radEngagementByKeyboardClick.Checked ||
                radEngagementByMouseClick.Checked ||
                radEngagementByAverageKeyboardClick.Checked ||
                radEngagementByAverageMouseClick.Checked)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForGraph,
                            DateTimeOffset.Parse($"{dateFrom.Value.Year}-{dateFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                            DateTimeOffset.Parse($"{dateTo.Value.Year}-{dateTo.Value.Month.ToString().PadLeft(2, '0')}-{dateTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                            frequency,
                            true,
                            _currentAppUser.Token);

                _analyticsAction.GenerateGraph(ref plotAnalytics, list, engagementBy, frequency);
            }
            else if (radEngagementByTotalOfEachProcessType.Checked)
            {
                list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_selectedUserIdForGraph,
                            DateTimeOffset.Parse($"{dateFrom.Value.Year}-{dateFrom.Value.Month.ToString().PadLeft(2, '0')}-{dateFrom.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                            DateTimeOffset.Parse($"{dateTo.Value.Year}-{dateTo.Value.Month.ToString().PadLeft(2, '0')}-{dateTo.Value.Day.ToString().PadLeft(2, '0')} 00:00"),
                            frequency,
                            false,
                            _currentAppUser.Token);

                _analyticsAction.GenerateGraph(ref plotAnalytics, list, engagementBy, frequency);
            }
            
            if (list.Any())
            {
                if (radEngagementByKeyboardClick.Checked)
                {
                    lblAnalyticsText.Text = 
                        $"The data shows the obtained keyboard clicks based from the engagements of {_currentAppUser.FirstName} {_currentAppUser.LastName} " +
                        $"from {dateFrom.Value.Date.ToString("MMM dd, yyyy")} to {dateTo.Value.Date.ToString("MMM dd, yyyy")}. The overall average is {list.Select(a => a.KeyboardClickSum).Average().ToString("F")}.";
                        
                }
                else if (radEngagementByMouseClick.Checked)
                {
                    lblAnalyticsText.Text =
                        $"The data shows the obtained mouse clicks based from the engagements of {_currentAppUser.FirstName} {_currentAppUser.LastName} " +
                        $"from {dateFrom.Value.Date.ToString("MMM dd, yyyy")} to {dateTo.Value.Date.ToString("MMM dd, yyyy")}. The overall average is {list.Select(a => a.MouseClickSum).Average().ToString("F")}.";
                }
                else if (radEngagementByAverageKeyboardClick.Checked)
                {
                    lblAnalyticsText.Text =
                        $"The data shows the obtained average keyboard clicks based from the engagements of {_currentAppUser.FirstName} {_currentAppUser.LastName} " +
                        $"from {dateFrom.Value.Date.ToString("MMM dd, yyyy")} to {dateTo.Value.Date.ToString("MMM dd, yyyy")}. The overall average is {list.Select(a => a.KeyboardClickAverage).Average().ToString("F")}.";
                }
                else if (radEngagementByAverageMouseClick.Checked)
                {
                    lblAnalyticsText.Text =
                        $"The data shows the obtained average mouse clicks based from the engagements of {_currentAppUser.FirstName} {_currentAppUser.LastName} " +
                        $"from {dateFrom.Value.Date.ToString("MMM dd, yyyy")} to {dateTo.Value.Date.ToString("MMM dd, yyyy")}. The overall average is {list.Select(a => a.MouseClickAverage).Average().ToString("F")}.";
                }
                else if (radEngagementByTotalOfEachProcessType.Checked)
                {
                    lblAnalyticsText.Text = "";
                }
                
            }
            else
            {
                lblAnalyticsText.Text = "No data found.";
            }

            this.Cursor = Cursors.Default;
            panelAnalyticsLoading.Visible = false;
            panelNavigation.Enabled = true;
            grpBoxEngagementBy.Enabled = true;
            grpBoxGraphFrequency.Enabled = true;
            grpBoxGraphDateRange.Enabled = true;
            grpBoxAnalyticsSelectEmployee.Enabled = true;
            btnGenerateGraph.Enabled = true;
        }

        private void radEngagementByRadButtons_Click(object sender, EventArgs e)
        {
            grpBoxGraphFrequency.Enabled = !radEngagementByTotalOfEachProcessType.Checked;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDashboardIcon_Click(object sender, EventArgs e)
        {
            OpenDashboard();
        }

        private void btnAnalyticsIcon_Click(object sender, EventArgs e)
        {
            OpenAnalytics();
        }

        private void btnTrackingIcon_Click(object sender, EventArgs e)
        {
            OpenTracking();
        }

        private void btnReportIcon_Click(object sender, EventArgs e)
        {
            OpenReports();
        }

        private void btnAboutIcon_Click(object sender, EventArgs e)
        {
            OpenAbout();
        }

        private void cmbUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedString = cmbAnalyticsUsers.SelectedItem.ToString();

            var userId = selectedString.Substring(0, selectedString.IndexOf('.'));

            if (!string.IsNullOrWhiteSpace(userId))
            {
                _selectedUserIdForGraph = int.Parse(userId);
            }
        }

        private void cmbReportsUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedString = cmbReportsUsers.SelectedItem.ToString();

            var userId = selectedString.Substring(0, selectedString.IndexOf('.'));

            if (!string.IsNullOrWhiteSpace(userId))
            {
                _selectedUserIdForReport = int.Parse(userId);
            }
        }

        private void dropdownUserMenu_Click(object sender, EventArgs e)
        {
            if (_currentAppUser.UserType == "ADMIN")
            {
                btnRegisterNewUser.Visible = true;
            }
            else
            {
                btnRegisterNewUser.Visible = false;
            }

            if (_isUserMenuOpened)
            {
                dropdownUserMenu.BackColor = Color.FromArgb(224, 224, 224);
                dropdownUserMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
                dropdownUserMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
                dropdownUserMenu.Image = Resources.down;

                panelUserMenu.Visible = false;
            }
            else
            {
                dropdownUserMenu.BackColor = Color.FromArgb(46, 64, 81);
                dropdownUserMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 64, 81);
                dropdownUserMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 64, 81);
                dropdownUserMenu.Image = Resources.arrowhead_up;

                panelUserMenu.Visible = true;
            }

            _isUserMenuOpened = !_isUserMenuOpened;
        }

        private void btnRegisterNewUser_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();

            registerForm.ShowDialog();
        }

        private void btnAboutUser_Click(object sender, EventArgs e)
        {
            var aboutUserForm = new AboutUserForm(_currentAppUser);

            aboutUserForm.ShowDialog();
        }

        private async void GenerateDashboardGraph()
        {
            var list = new List<ComputedDataForGraph>();

            var frequency = GetGraphFrequency();

            list = await _trackingRepository.GetGraphDataByUserAndDateRangeAsync(_currentAppUser.AppUserId,
                            DateTimeOffset.Parse($"{DateTimeOffset.Now.AddDays(-7).Year}-{DateTimeOffset.Now.AddDays(-7).Month.ToString().PadLeft(2, '0')}-{DateTimeOffset.Now.AddDays(-7).Day.ToString().PadLeft(2, '0')} 00:00"),
                            DateTimeOffset.Parse($"{DateTimeOffset.Now.Year}-{DateTimeOffset.Now.Month.ToString().PadLeft(2, '0')}-{DateTimeOffset.Now.Day.ToString().PadLeft(2, '0')} 00:00"),
                            frequency,
                            false,
                            _currentAppUser.Token);

            _analyticsAction.GenerateGraph(ref plotDashboard, list, EngagementBy.TotalOfEachProcessType, frequency);

        }
    }
}
