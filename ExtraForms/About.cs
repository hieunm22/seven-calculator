using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class About : ExtraForm
    {
        public About()
        {
            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            string version = fvi.FileVersion;
            aboutLb.Text = string.Format("Seven calculator v{0} - Developed by Nguyen Minh Hieu", version);
            this.Size = new Size(aboutLb.Location.X + aboutLb.Size.Width + pictureBox1.Location.X, this.Height);
            btnOk.Location = new Point((this.Size.Width - btnOk.Size.Width) / 2, btnOk.Location.Y);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void emaillb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Process.Start("mailto:hieunm1987@gmail.com?subject=Seven%20Calculator");
        }
        //
        // di chuột vào link
        //
        private void emaillb_MouseEnter(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Red;
            emaillb.Font = new Font(emaillb.Font.FontFamily, 8.25f, FontStyle.Underline);
            emaillb.Cursor = Cursors.Hand;
        }
        //
        // rời chuột khỏi link
        //
        private void emaillb_MouseLeave(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Blue;
            emaillb.Font = new Font(emaillb.Font.FontFamily, 8.25f, FontStyle.Regular);
            emaillb.Cursor = Cursors.Arrow;
        }
        //
        // copy email address
        //
        private void copyEmailAddrTSMI_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(emaillb.Text);
        }
    }
}