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

namespace WPCAA.WinApp.Forms
{
    public partial class AboutUserForm : Form
    {
        AppUser _appUser;
        public AboutUserForm(AppUser appUser)
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);

            _appUser = appUser;
        }

        private void AboutUserForm_Load(object sender, EventArgs e)
        {
            lblFirstName.Text = _appUser.FirstName;
            lblLastName.Text = _appUser.LastName;
            lblUsername.Text = _appUser.Username;
            lblUserType.Text = _appUser.UserType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
