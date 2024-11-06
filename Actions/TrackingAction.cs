using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPCAA.WinApp.Actions
{
    public class TrackingAction
    {
        private int _mouseclicks;
        private int _keyboardclicks;

        #region
        //Imports for getting active Application name
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        public string GetActiveWindow()
        {
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);

            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return "";
        }
        #endregion

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            _keyboardclicks++;
            Console.WriteLine(_keyboardclicks);
        }
        
        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            _mouseclicks++;
        }

        public void ResetClicks()
        {
            _keyboardclicks = _mouseclicks = 0;
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            _mouseclicks = 0;
            _keyboardclicks = 0;

            m_GlobalHook.Dispose();
        }

        public int GetMouseClicks()
        {
            return _mouseclicks;
        }

        public int GetKeyboardClicks()
        {
            return _keyboardclicks;
        }

    }
}
