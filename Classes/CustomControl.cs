using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    class IDataGridView : DataGridView
    {
        /// <summary>
        /// property dùng để kiểm tra xem có đồng ý cho sự kiện CellStateChanged chạy hay không
        /// </summary>
        private bool allowCellStateChanged = true;
        public virtual bool AllowCellStateChanged
        {
            get { return allowCellStateChanged; }
            set { allowCellStateChanged = value; }
        }

        protected override void OnCellStateChanged(DataGridViewCellStateChangedEventArgs e)
        {
            if (AllowCellStateChanged) base.OnCellStateChanged(e);
        }
        /// <summary>
        /// property dùng để kiểm tra xem có đồng ý cho sự kiện CellDoubleClick chạy hay không
        /// </summary>
        private bool allowCellDoubleClick = true;
        public virtual bool AllowCellDoubleClick
        {
            get { return allowCellDoubleClick; }
            set { allowCellDoubleClick = value; }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (AllowCellDoubleClick) base.OnCellDoubleClick(e);
        }
        /// <summary>
        /// property dùng để kiểm tra xem có đồng ý cho sự kiện CellClick chạy hay không
        /// </summary>
        private bool allowCellClick = true;
        public virtual bool AllowCellClick
        {
            get { return allowCellClick; }
            set { allowCellClick = value; }
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            if (AllowCellClick) base.OnCellClick(e);
        }

        [Browsable(true)]
        public override ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set { base.ContextMenu = value; }
        }

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return base.ContextMenuStrip; }
            set { base.ContextMenuStrip = value; }
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                base.EndEdit();
                return base.ProcessEscapeKey(e.KeyData);
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (!IsCurrentCellInEditMode) return base.ProcessDataGridViewKey(e);
                else return false;
            }
            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Keys keys = keyData & Keys.KeyCode;
            if (keyData == Keys.Oemplus)
            {
                EndEdit();
                return ProcessEscapeKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }
    }
    /// <summary>
    /// Lớp IPanel
    /// </summary>
    class IPanel : Panel
    {
        [Browsable(true)]
        public override ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set { base.ContextMenu = value; }
        }

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return base.ContextMenuStrip; }
            set { base.ContextMenuStrip = value; }
        }

        private bool allowDrawBorder = true;
        [DefaultValue(true)]
        public bool AllowDrawBorder
        {
            get { return allowDrawBorder; }
            set { allowDrawBorder = value; }
        }

        public event EventHandler FocusEvent = null;
        protected override void OnGotFocus(EventArgs e)
        {
            if (FocusEvent != null) FocusEvent(this, e);
            base.OnGotFocus(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BorderStyle == BorderStyle.None && allowDrawBorder)
            {
                Graphics gr = e.Graphics;
                gr.DrawRectangle(new Pen(Color.FromArgb(134, 150, 170), 1.0f), 0, 0, Size.Width - 1, Size.Height - 1);
            }

            base.OnPaint(e);
        }
    }
    /// <summary>
    /// Lớp ILabel
    /// </summary>
    class ILabel : Label
    {
        private bool allowTextChanged = true;
        public virtual bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (allowTextChanged) base.OnTextChanged(e);
        }
    }
    /// <summary>
    /// Lớp ITextBox
    /// </summary>
    class ITextBox : TextBox
    {    
        public ITextBox()
        {
            InitializeComponent();
            tb.Text = WatermarkText;
        }

        public ITextBox(string text)
        {
            InitializeComponent();
            tb.Text = text;
        }

        private string watermarkText = "";
        public string WatermarkText
        {
            get { return watermarkText; }
            set
            {
                watermarkText = value;
                tb.Text = watermarkText;
            }
        }

        private void InitializeComponent()
        {
            tb = new TextBox();
            tb.Location = new Point(2, 0);
            tb.Font = new Font("Segoe UI", Font.Size);
            tb.ForeColor = SystemColors.GrayText;
            tb.BorderStyle = BorderStyle.None;
            tb.AcceptsTab = false;
            tb.ShortcutsEnabled = false;
            tb.GotFocus += new EventHandler(lb_GotFocus);
            GotFocus += new EventHandler(lb_GotFocus);
            tb.MouseEnter += new EventHandler(lb_GotFocus);
            this.Controls.Add(tb);
        }

        private void lb_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            tb.Visible = Text == "";
            if (!allowTextChanged) return;
            base.OnTextChanged(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            tb.Visible = (Enabled && !ReadOnly) && Text == "";
            base.OnEnabledChanged(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            tb.Visible = (Enabled && !ReadOnly) && Text == "";
            base.OnReadOnlyChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            tb.Font = new Font("Segoe UI", this.Font.Size);
            Point pt = tb.GetPositionFromCharIndex(tb.Text.Length - 1);
            tb.Size = new Size(pt.X + 30, pt.Y);
            base.OnFontChanged(e);
        }

        protected override void OnMultilineChanged(EventArgs e)
        {
            tb.Location = new Point(5, 1);
            base.OnMultilineChanged(e);
        }

        TextBox tb;
        private bool allowTextChanged = true;
        public bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        [DefaultValue(false)]
        public bool NumberModeOnly { get; set; }
        //
        // got focus
        //
        protected override void OnGotFocus(EventArgs e)
        {
            if (this.Text == "Enter value" && this.ForeColor == SystemColors.GrayText)
            {
                allowTextChanged = false;
                this.Text = "";
                allowTextChanged = true;
                this.ForeColor = SystemColors.ControlText;
                this.Font = new Font(this.Font, FontStyle.Regular);
            }
            base.OnGotFocus(e);
        }
        //
        // lost focus
        //
        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Text == "")
            {
                allowTextChanged = false;
                this.Text = "Enter value";
                allowTextChanged = true;
                this.ForeColor = SystemColors.GrayText;
                this.Font = new Font(this.Font, FontStyle.Italic);
            }
            base.OnLostFocus(e);
        }
        //
        // keypress
        //
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!NumberModeOnly) { base.OnKeyPress(e); return; }
            switch ((int)e.KeyChar)
            {
                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7':
                case '8': case '9': case '.': case ',': case 'e': case 'E': case '+': case '-':
                    e.Handled = false;
                    break;
                case 1: case 3: case 8: case 22: case 26: case 27:
                    base.OnKeyPress(e);
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
}

    class IRadioButton : RadioButton
    {
        /// <summary>
        /// giá trị ở hệ xx-phân mà radio này mang tên
        /// </summary>
        public string Value { get; set; }
    }

    class INumericUpDown : NumericUpDown
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0 && Value < Maximum)
            {
                Value++;
            }
            if (e.Delta < 0 && Value > Minimum)
            {
                Value--;
            }
        }
    }

    class IDateTimePicker : DateTimePicker
    {
        [DefaultValue(false)]
        public bool UsedMouseWheel
        {
            get { return usedMouseWheel; }
            set { usedMouseWheel = value; }
        }

        private bool usedMouseWheel = false;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!usedMouseWheel) return;
            DateTime added = Value.AddDays(1d * e.Delta / Math.Abs(e.Delta));
            if (e.Delta > 0)
            {
                if (added.Month == Value.Month)
                    Value = added;
                else
                    Value = new DateTime(Value.Year, Value.Month, 1);
            }
            else
            {
                if (added.Month == Value.Month)
                    Value = added;
                else
                    Value = new DateTime(Value.Year, Value.Month, DateTime.DaysInMonth(Value.Year, Value.Month));
            }
        }
    }
}