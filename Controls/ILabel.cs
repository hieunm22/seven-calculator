using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// Lớp ILabel
    /// </summary>
    class ILabel : Label
    {
        private bool allowTextChanged = true;
        [Browsable(true)]
        [DefaultValue(true)]
        public virtual bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        private bool allowLinkClick = false;
        [Browsable(true)]
        [DefaultValue(false)]
        public bool AllowLinkClick
        {
            get { return allowLinkClick; }
            set { allowLinkClick = value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (allowTextChanged) base.OnTextChanged(e);
        }
        /// <summary>
        /// click vào label preview
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            if (allowLinkClick)
            {
                this.ForeColor = Color.Black;
                this.Cursor = Cursors.Arrow;
                this.Font = new Font(this.Font.Name, this.Font.Size, FontStyle.Regular); 
            }
            base.OnClick(e);
        }
        /// <summary>
        /// di chuột vào label preview
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (allowLinkClick)
            {
                if (this.ForeColor == Color.Red)
                {
                    this.Font = new Font(this.Font.Name, this.Font.Size, FontStyle.Regular);
                    this.Cursor = Cursors.Arrow;
                }
                else
                {
                    this.Font = new Font(this.Font.Name, this.Font.Size, FontStyle.Underline);
                    this.Cursor = Cursors.Hand;
                } 
            }
            base.OnMouseEnter(e);
        }
        /// <summary>
        /// di chuột ra khỏi label preview
        /// </summary>
        /// 
        protected override void OnMouseLeave(EventArgs e)
        {
            if (allowLinkClick)
            {
                this.Font = new Font(this.Font.Name, this.Font.Size, FontStyle.Regular); 
            }
            base.OnMouseLeave(e);
        }
    }
}