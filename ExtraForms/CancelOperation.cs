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
    }
}
