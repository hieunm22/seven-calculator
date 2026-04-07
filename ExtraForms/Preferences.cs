using System;
using System.IO;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Preferences : Form
    {
        public Preferences(int Speed, bool Animate, bool Fast, bool IsSign)
        {
            InitializeComponent();
            transfer = Speed;
            animateCB.Checked = ani = Animate;
            fastFactCB.Checked = f3 = Fast;
            usedSignChkB.Checked = IsSign;
            collapsedSpdNUD.Value = (decimal)Speed;
        }

        public delegate void ICheckChanged(int spd, bool animate, bool fast, bool sign);
        public event ICheckChanged DoCheck;

        int transfer;
        bool mod = false, ani = true,f3 = false, sign = true;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (mod)
            {
                string path = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\calc.ini";
                string[] lines = File.ReadAllLines(path);
                lines[lines.Length - 3] = string.Format("Animate={0};", animateCB.Checked ? 1 : 0);
                lines[lines.Length - 2] = string.Format("FastFactorial={0};", fastFactCB.Checked ? 1 : 0);
                lines[lines.Length - 1] = string.Format("SignInteger={0};", usedSignChkB.Checked ? 1 : 0);
                lines[lines.Length - 4] = string.Format("CollapseSpeed={0};", collapsedSpdNUD.Value);
                File.WriteAllText(path, string.Join(Environment.NewLine, lines));
                if (DoCheck != null) DoCheck((int)collapsedSpdNUD.Value, usedSignChkB.Checked, fastFactCB.Checked, animateCB.Checked);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mod |= collapsedSpdNUD.Value != transfer;
        }

        private void fastFactCB_CheckedChanged(object sender, EventArgs e)
        {
            mod |= f3 != fastFactCB.Checked;
        }

        private void usedSignChkB_CheckedChanged(object sender, EventArgs e)
        {
            mod |= sign != usedSignChkB.Checked;
        }

        private void animateCB_CheckedChanged(object sender, EventArgs e)
        {
            collapsedSpdNUD.Enabled = label1.Enabled = animateCB.Checked;
            mod |= ani != animateCB.Checked;
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Misc.ReleaseCapture();
                Misc.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }
    }
}