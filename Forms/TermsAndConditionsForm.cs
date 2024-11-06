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

namespace WPCAA.WinApp.Forms
{
    public partial class TermsAndConditionsForm : Form
    {
        public TermsAndConditionsForm()
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);
        }

        private void linkPrivacyPolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dataPrivacyForm = new DataPrivacyForm();

            dataPrivacyForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
