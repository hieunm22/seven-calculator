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
                else return true;
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
        /// <summary>
        /// sự kiện lăn chuột
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (!IsCurrentCellInEditMode) ProcessUpKey(Keys.None);
            }
            else
            {
                if (!IsCurrentCellInEditMode) ProcessDownKey(Keys.None);
            }
            base.OnMouseWheel(e);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BorderStyle == BorderStyle.None)
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

        private bool allowTextChanged = true;
        public virtual bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            if (AllowTextChanged) base.OnTextChanged(e);
        }
        /// <summary>
        /// nút chuột được click vào khi đối tượng
        /// </summary>
        public MouseButtons MouseButtonClicked { get; set; }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseButtonClicked = e.Button;
            base.OnMouseDown(e);
        }
        /// <summary>
        /// hoành độ của con trỏ khi di qua đối tượng
        /// </summary>
        public int MouseX { get; set; }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseX = e.X;
            base.OnMouseMove(e);
        }
    }
    /// <summary>
    /// Lớp ITextBox
    /// </summary>
    class ITextBox : TextBox
    {
        private bool allowTextChanged = true;
        public bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        public Point MouseLocation { get; set; }
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
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
            base.OnGotFocus(e);
        }
        //
        // lost focus
        //
        protected override void OnLostFocus(System.EventArgs e)
        {
            if (this.Text == "")
            {
                allowTextChanged = false;
                this.Text = "Enter value";
                allowTextChanged = true;
                this.ForeColor = SystemColors.GrayText;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            }
            base.OnLostFocus(e);
        }
        //
        // mouse move
        //
        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseLocation = new Point(e.X, e.Y);
            base.OnMouseMove(e);
        }
        //
        // keypress
        //
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (NumberModeOnly)
            {
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
            base.OnKeyPress(e);
        }
    }

    class IPictureBox : PictureBox
    {

    }
}