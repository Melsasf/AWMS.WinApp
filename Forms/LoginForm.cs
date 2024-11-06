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
using WPCAA.WinApp.Models;
using WPCAA.WinApp.Repositories;

namespace WPCAA.WinApp.Forms
{
    public partial class LoginForm : Form
    {
        private AppUserRepository _appUserRepository;
        public LoginForm()
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);

            _appUserRepository = new AppUserRepository();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!checkBoxTermsAndConditions.Checked)
            {
                AlertForm alertForm = new AlertForm("Before you can procced, you must have read and accept the desktop application's terms and conditions and privacy policy.");

                alertForm.ShowDialog();

                return;
            }

            
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                AlertForm alertForm = new AlertForm("Username or password should not be empty!");

                alertForm.ShowDialog();

                return;
            }
            var appUserToLogin = new AppUser();

            appUserToLogin.Username = txtUsername.Text;
            appUserToLogin.Password = txtPassword.Text;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnLogin.Enabled = false;
                btnClose.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                panelLoading.Visible = true;

                var appUserInfo = await _appUserRepository.LoginAsync(appUserToLogin);

                this.Cursor = Cursors.Default;
                panelLoading.Visible = false;
                btnLogin.Enabled = true;
                btnClose.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;

                if (appUserInfo != null)
                {
                    AlertForm alertForm = new AlertForm(appUserInfo.Message);

                    alertForm.ShowDialog();
                }
                else
                {
                    AlertForm alertForm = new AlertForm("Unable to login. Please verify if internet connection exists.");

                    alertForm.ShowDialog();

                    return;
                }

                if (appUserInfo.IsLoginSuccess)
                {
                    ClearTextboxes();

                    MainForm mainForm = new MainForm(appUserInfo, this);

                    this.Hide();
                    mainForm.Show();
                }
            }
            catch (Exception ex)
            {
                AlertForm alertForm = new AlertForm("Unable to login. Please verify if internet connection exists.");

                alertForm.ShowDialog();

                this.Cursor = Cursors.Default;
                panelLoading.Visible = false;
                btnLogin.Enabled = true;
                btnClose.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void ClearTextboxes()
        {
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
        }

        private void linkTermsAndAgreements_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var termsAndConditionsForm = new TermsAndConditionsForm();

            termsAndConditionsForm.ShowDialog();
        }

        private void linkPrivacyPolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dataPrivacyForm = new DataPrivacyForm();

            dataPrivacyForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
