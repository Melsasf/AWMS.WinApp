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
    public partial class DataPrivacyForm : Form
    {
        public DataPrivacyForm()
        {
            InitializeComponent();

            (new DropShadow()).ApplyShadows(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
