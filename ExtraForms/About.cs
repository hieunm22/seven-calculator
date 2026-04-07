using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class About : BaseForm
    {
        public About()
        {
            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            aboutLb.Text = string.Format("Seven calculator v{0} - Developed by Nguyen Minh Hieu", fvi.FileVersion);
            Size = new Size(aboutLb.Location.X + aboutLb.Size.Width + pictureBox1.Location.X, Height);
            btnOk.Location = new Point((Size.Width - btnOk.Size.Width) / 2, btnOk.Location.Y);
            aboutLb.MouseDown += MoveForm;
            iLabel2.MouseDown += MoveForm;
            emaillb.MouseDown += MoveForm;
            pictureBox1.MouseDown += MoveForm;
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