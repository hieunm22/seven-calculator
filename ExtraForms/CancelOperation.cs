using System;
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

        delegate void SetPBProgress(int _progress, int _ct, int _tt);

        private void SetProcess(int _progress, int _ct, int _tt)
        {
            progressBar.Value = _progress;
            label2.Text = string.Format("{0} / {1}", _ct - 1, _tt);
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
        /// <summary>
        /// di chuyển form mà không cần đưa con trỏ lên title bar
        /// </summary>
        private void MoveFormWithoutMouseAtTitleBar(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Misc.ReleaseCapture();
                Misc.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }
    }
}
