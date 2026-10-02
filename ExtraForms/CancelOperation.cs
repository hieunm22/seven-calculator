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
                if (value && !IsDisposed) Close();
            }
        }
        /// <summary>
        /// cập nhật tiến độ lên progress bar và dòng trạng thái (gọi từ UI thread)
        /// </summary>
        /// <param name="percent">phần trăm hoàn thành 0..100</param>
        /// <param name="status">mô tả bước đang thực hiện</param>
        public void UpdateProgress(int percent, string status)
        {
            if (IsDisposed) return;
            if (percent < progressBar.Minimum) percent = progressBar.Minimum;
            if (percent > progressBar.Maximum) percent = progressBar.Maximum;
            progressBar.Value = percent;
            lbPercent.Text = string.Format("{0}%", percent);
            if (!string.IsNullOrEmpty(status)) lbStatus.Text = status;
        }
        /// <summary>
        /// nếu phép tính đã xong trước khi form kịp hiện lên thì đóng luôn
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (finished) Close();
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
            progressBar.Value = 0;
            lbPercent.Text = "0%";
            lbStatus.Text = "Preparing...";
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
