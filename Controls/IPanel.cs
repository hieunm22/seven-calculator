using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
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
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(134, 150, 170), 1f), 0, 0, Width - 1, Height - 1);
            }

            base.OnPaint(e);
        }
    }
}