using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Preferences : Form
    {
        public Preferences(int _Speed, bool _Animate, bool _FastFact, bool _IsSign, bool _ReadDict, bool _FastInput)
        {
            InitializeComponent();
            LoadSettings(_Speed, _Animate, _FastFact, _IsSign, _ReadDict, _FastInput);
        }

        private void LoadSettings(int _Speed, bool _Animate, bool _Fast, bool _IsSign, bool _ReadDict, bool _FastInput)
        {
            transfer = _Speed;
            animateCB.Checked = ani = _Animate;
            fastFactCB.Checked = f3 = _Fast;
            usedSignChkB.Checked = sign = _IsSign;
            readDictChkB.Checked = readDict = _ReadDict;
            fastInputCkB.Checked = fastInput = _FastInput;
            collapsedSpdNUD.Value = (decimal)_Speed;
        }
        /// <summary>
        /// thay đổi cấu hình trên form
        /// </summary>
        public delegate void PreferencesChanged(int spd, bool animate, bool fast, bool sign, bool readdict, bool fastinput, bool restart);
        /// <summary>
        /// sự kiện thay đổi cấu hình trên form
        /// </summary>
        public event PreferencesChanged DoCheck;

        int transfer;
        bool mod = false, ani, f3, sign, readDict, fastInput, readDictChanged;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (mod)
            {
                DialogResult dr = DialogResult.No;
                if (readDictChanged)
                {
                    dr = MessageBox.Show("This setting requires application restart to take effect.\r\nDo you want to restart now?", "Calculator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (DoCheck != null) DoCheck((int)collapsedSpdNUD.Value, usedSignChkB.Checked, fastFactCB.Checked, animateCB.Checked, readDictChkB.Checked, fastInputCkB.Checked, dr == DialogResult.Yes);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void collapsedSpdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (mod && collapsedSpdNUD.Value == transfer) { mod = false; return; }
            mod |= collapsedSpdNUD.Value != transfer;
        }

        private void fastFactCB_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && fastFactCB.Checked == f3) { mod = false; return; }
            mod |= f3 != fastFactCB.Checked;
        }

        private void usedSignChkB_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && usedSignChkB.Checked == sign) { mod = false; return; }
            mod |= sign != usedSignChkB.Checked;
        }

        private void animateCB_CheckedChanged(object sender, EventArgs e)
        {
            collapsedSpdNUD.Enabled = spdLB.Enabled = animateCB.Checked;
            mod |= ani != animateCB.Checked;
        }

        private void fastInputCkB_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && fastInputCkB.Checked == sign) { mod = false; return; }
            mod |= fastInput != fastInputCkB.Checked;
        }

        private void readDictChkB_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && readDictChkB.Checked == readDict) { mod = false; return; }
            readDictChanged = readDict != readDictChkB.Checked;
            mod |= readDictChanged;
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