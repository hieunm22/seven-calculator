using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CancelOperation : Form
    {
        public CancelOperation()
        {
            InitializeComponent();
        }

        public int total = 100;
        public int count = 0;
        private int progress = 0;
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                if (progressBar.InvokeRequired)
                {
                    SetPBProgress spbp = new SetPBProgress(SetProcess);
                    this.Invoke(spbp, progress, count, total);
                }
            }
        }

        delegate void SetPBProgress(int _progress, int ct, int tt);

        private void SetProcess(int _progress, int ct, int tt)
        {
            progressBar.Value = _progress;
            label2.Text = string.Format("{0} / {1}", ct - 1, tt);
        }

        private bool finished = false;
        public bool Finished
        {
            get { return finished; }
            set
            {
                finished = value;
                if (value) Close();
            }
        }

        public delegate void CancelProcess();
        public event CancelProcess DoCancel;
        //
        // nut cancel
        //
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        // dong form
        //
        protected override void OnClosed(EventArgs e)
        {
            if (DoCancel != null) DoCancel();
            base.OnClosed(e);
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Misc.ReleaseCapture();
                Misc.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }

        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private void CancelOperation_Load(object sender, EventArgs e)
        {
            var sm = GetSystemMenu(Handle, false);
            EnableMenuItem(sm, 0xF060, 2);
        }
    }
}
