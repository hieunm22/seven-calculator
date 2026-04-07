using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        private const int CLOSE_BY_POSITION = 1024;

        protected override void OnLoad(EventArgs e)
        {
            IntPtr sysMenu = GetSystemMenu(this.Handle, false);

            RemoveMenu(sysMenu, 0, CLOSE_BY_POSITION);
            RemoveMenu(sysMenu, 2, CLOSE_BY_POSITION);
            RemoveMenu(sysMenu, 3, CLOSE_BY_POSITION);
            RemoveMenu(sysMenu, 2, CLOSE_BY_POSITION);
            RemoveMenu(sysMenu, 1, CLOSE_BY_POSITION);
            base.OnLoad(e);
        }

        protected void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Common.ReleaseCapture();
                Common.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }
    }
}
