using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Calculator    // version 1.72
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            initMenu();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emaillb_Click(object sender, EventArgs e)
        {
            //Process.Start("mailto:hieunm1987@gmail.com");
        }

        private void emaillb_MouseEnter(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Red;
        }

        private void emaillb_MouseLeave(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Blue;
        }

        private void copyEmailAddrTSMI_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(emaillb.Text);
        }
    }
}