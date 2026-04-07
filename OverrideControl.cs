using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Calculator
{
    class IDataGridView : DataGridView
    {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                base.EndEdit();
                return base.ProcessEscapeKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }

        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys keys = keyData & Keys.KeyCode;
            if (keys == Keys.Return)
            {
                base.EndEdit();
                return base.ProcessEscapeKey(keyData);
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
                base.ProcessUpKey(Keys.Up);
            }
            else
            {
                base.ProcessDownKey(Keys.Down);
            }
            base.OnMouseWheel(e);
        }
    }
    /// <summary>
    /// Lớp Panel
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
    }
    /// <summary>
    /// Lớp Label
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
    }
}