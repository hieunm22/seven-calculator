using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CancelOperation : Form, IDisposable
    {
        public CancelOperation()
        {
            InitializeComponent();
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
            Close();
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
                Common.ReleaseCapture();
                Common.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }

        private void CancelOperation_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        string str = ".";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (str != ".".PadLeft(9, '.'))
            {
                str += ".";
            }
            else
            {
                str = ".";
            }
            lbTitle.Text = string.Format("Cancel this operation request now: {0}", str);
        }
    }
}
