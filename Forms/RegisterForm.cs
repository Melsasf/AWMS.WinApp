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
    public partial class RegisterForm : Form
    {
        AppUserRepository _appUserRepository = new AppUserRepository();
        public RegisterForm()
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) || 
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                AlertForm alertForm = new AlertForm("You must fill in all of the fields!");

                alertForm.ShowDialog();
            }
            else
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    AlertForm alertForm = new AlertForm("Passwords do not match.");

                    alertForm.ShowDialog();
                }
                else
                {
                    string result = await _appUserRepository.RegisterAsync(new AppUser
                    {
                        Username = txtUsername.Text,
                        Password = txtPassword.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        UserType = cmbUserType.Text
                    });

                    if (string.IsNullOrEmpty(result))
                    {
                        AlertForm alertForm = new AlertForm("User registered!");

                        alertForm.ShowDialog();

                        this.Close();
                    }
                    else
                    {
                        AlertForm alertForm = new AlertForm(result);

                        alertForm.ShowDialog();
                    }
                }
            }
        }
    }
}
