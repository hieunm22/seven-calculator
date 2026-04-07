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

        public delegate void SendFrequence(double frq, bool isUpdate);
        public event SendFrequence AddFrequence = null;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int key_hc = keyData.GetHashCode();
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            toolTip1.Hide(frqTB);

            double frqValue = 0d;

            switch (keyData)
            {
                case Keys.Enter:
                    toolTip1.Hide(frqTB);
                    if (string.IsNullOrWhiteSpace(frqTB.Text))
                    {
                        var pt = frqTB.GetPositionFromCharIndex(frqTB.Text.Length - 1);
                        toolTip1.Show("You must enter a value!", frqTB, pt.X - 5, -40, 3000);
                        this.DialogResult = DialogResult.OK;
                        return false;
                    }
                    try
                    {
                        frqValue = double.Parse(frqTB.Text.Replace(Common.GroupSeparator, ""));
                    }
                    catch (Exception)
                    {
                        var pt = frqTB.GetPositionFromCharIndex(frqTB.Text.Length - 1);
                        toolTip1.Show("Input string is not valid!", frqTB, pt.X - 5, -40, 3000);
                        this.DialogResult = DialogResult.Cancel;
                        return false;
                    }
                    if (AddFrequence != null) AddFrequence(frqValue, IsUpdate);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case Keys.Up:
                    frqTB.Text = (++frqValue).ToString();
                    frqTB.SelectAll();
                    break;
                case Keys.Down:
                    if (frqValue > 1) frqTB.Text = (--frqValue).ToString();
                    frqTB.SelectAll();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            double frqValue = 0d;
            try
            {
                frqValue = double.Parse(frqTB.Text.Replace(Common.GroupSeparator, ""));
            }
            catch (FormatException fe)
            {
                var pt = frqTB.GetPositionFromCharIndex(frqTB.Text.Length - 1);
                toolTip1.Show(fe.Message, frqTB, pt.X - 5, -40, 3000);
                return;
            }
            if (e.Delta > 0) frqTB.Text = (++frqValue).ToString();
            if (e.Delta < 0 && frqValue > 1) frqTB.Text = (--frqValue).ToString();
            frqTB.SelectAll();
            base.OnMouseWheel(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            base.OnClosing(e);
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
                frqTB.Text = value.ToString();
            }
        }
    }
}
