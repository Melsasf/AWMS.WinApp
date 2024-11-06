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
    public partial class AlertForm : Form
    {
        private string _text = string.Empty;
        public AlertForm(string text)
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);

            _text = text;
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            lblMessage.Text = _text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
