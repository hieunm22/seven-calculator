using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Frequence : Form
    {
        public Frequence()
        {
            InitializeComponent();
        }

        private void Frequence_Load(object sender, EventArgs e)
        {
            frqTB.SelectAll();
        }

        public delegate void SendFrequence(double fq, bool isUpdate);
        public event SendFrequence AddFrequence = null;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int key_hc = keyData.GetHashCode();
            double vl = double.Parse(frqTB.Text.Replace(".", Misc.DecimalSeparator).Replace(",", Misc.DecimalSeparator));
            switch (key_hc)
            {
                case 13:
                    if (AddFrequence != null) AddFrequence(vl, IsUpdate);
                    this.Close();
                    break;
                case 27:
                    this.Close();
                    break;
                case 38:
                    frqTB.Text = (++vl).ToString();
                    frqTB.SelectAll();
                    break;
                case 40:
                    if (vl > 1) frqTB.Text = (--vl).ToString();
                    frqTB.SelectAll();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            double vl = double.Parse(frqTB.Text.Replace(".", Misc.DecimalSeparator).Replace(",", Misc.DecimalSeparator));
            if (e.Delta > 0) frqTB.Text = (++vl).ToString();
            if (e.Delta < 0 && vl > 1) frqTB.Text = (--vl).ToString();
            frqTB.SelectAll();
            base.OnMouseWheel(e);
        }
        /// <summary>
        /// true nếu là bảng sửa tần suất, false nếu là bảng thêm mới
        /// </summary>
        public bool IsUpdate { get; set; }

        private double oldFQ = 1;
        public double OldFQ
        {
            get { return oldFQ; }
            set
            {
                oldFQ = value;
                frqTB.Text = oldFQ.ToString();
            }
        }

    }
}
