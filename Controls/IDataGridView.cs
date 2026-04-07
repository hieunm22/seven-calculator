using System.ComponentModel;
using System.Windows.Forms;

namespace Calculator
{
    public class IDataGridView : DataGridView
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
        /// <summary>
        /// huỷ edit trên lưới
        /// </summary>
        public bool IsCancelEdit { get; set; }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            IsCancelEdit = keyData == Keys.Escape;
            // bấm esc và đang ở chế độ editmode
            if (IsCurrentCellInEditMode && keyData == Keys.Return)
            {
                if (IsCancelEdit) base.CancelEdit();
                return base.EndEdit();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// bắt sự kiện nhấn vào các phím dùng để định hướng
        /// </summary>
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                base.EndEdit();
                return base.ProcessEscapeKey(e.KeyData);
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (!IsCurrentCellInEditMode)
                    return base.ProcessDataGridViewKey(e);
                else return false;
            }
            return base.ProcessDataGridViewKey(e);
        }
        /// <summary>
        /// bắt sự kiện nhấn vào các phím như TAB, ESCAPE, ENTER, và ARROW
        /// </summary>
        /// <param name="keyData">phím được nhập</param>
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
}