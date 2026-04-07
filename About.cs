using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator    // version 1.72
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        MouseButtons mb;
        private void emaillb_MouseDown(object sender, MouseEventArgs e)
        {
            mb = e.Button;
        }

        private void emaillb_Click(object sender, EventArgs e)
        {
            if (mb == MouseButtons.Left) Process.Start("mailto:hieunm1987@gmail.com");
        }

        private void emaillb_MouseEnter(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Red;
            emaillb.Font = new Font("Tahoma", 9F, FontStyle.Underline);
        }

        private void emaillb_MouseLeave(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Blue;
            emaillb.Font = new Font("Tahoma", 9F, FontStyle.Regular);
        }

        private void copyEmailAddrTSMI_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(emaillb.Text);
        }
    }
}