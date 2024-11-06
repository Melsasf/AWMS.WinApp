using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPCAA.WinApp.Helpers
{
    public static class ButtonHelper
    {
        public static void EnableDisableButton(this Button button, bool state, double alpha, int red, int green, int blue)
        {
            button.Enabled = state;
            button.BackColor = Color.FromArgb((int)(alpha * 255), red, green, blue);
        }

        public static void SetActiveStateOfNavButton(this Button button, bool state)
        {
            if (state)
            {
                button.BackColor = Color.FromArgb(255, 88, 124, 157);
            }
            else
            {
                button.BackColor = Color.Transparent;
            }
        }
    }
}
