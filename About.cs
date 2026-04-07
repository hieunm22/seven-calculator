using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
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
            Process.Start("mailto:hieunm1987@gmail.com");
        }

        private void emaillb_MouseEnter(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Red;
        }

        private void emaillb_MouseLeave(object sender, EventArgs e)
        {
            emaillb.ForeColor = Color.Blue;
        }

        private void About_Load(object sender, EventArgs e)
        {
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
            versionlb.Text += regkey.GetValue("CurrentVersion") + " (Build";
            versionlb.Text += regkey.GetValue("BuildLab") + " : ";
            versionlb.Text += regkey.GetValue("CSDVersion") + ")";
            namelb.Text = "" + regkey.GetValue("RegisteredOwner");
            orglb.Text = "" + regkey.GetValue("RegisteredOrganization");
            regkey.Close();
        }

        private void EULA_MouseDown(object sender, MouseEventArgs e)
        {
            eulb.ForeColor = Color.Indigo;
            lalb.ForeColor = Color.Indigo;
        }

        private void EULA_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Environment.SystemDirectory + "\\eula.txt");
            }
            catch { }
        }

        private void copyEmailAddrTSMI_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(emaillb.Text);
        }
    }
}