using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public class IComboBox : ComboBox
    {
        public IComboBox() { }

        private bool allowFilter = false;

        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Indicates whether the control can accept a filter row on when it dropdowns")]
        public bool AllowFilter
        {
            get { return allowFilter; }
            set
            {
                if (allowFilter = value)
                {
                    tbSearch = new ITextBox();
                    tbSearch.SuggestType = SuggestType.PlaceHolder;
                    tbSearch.SuggestText = "Filter here...";
                    tbSearch.Location = new System.Drawing.Point(0, 0);
                    tbSearch.Visible = false;
                    tbSearch.ShortcutsEnabled = false;
                    tbSearch.Cursor = Cursors.IBeam;
                    tbSearch.BringToFront();
                    tbSearch.TextChanged += new EventHandler(tb_TextChanged);
                    Controls.Add(tbSearch);
                }
            }
        }

        private string[] mainDataSource = new string[0];
        [Browsable(false)]
        public string[] MainDataSource
        {
            get { return mainDataSource; }
            set { mainDataSource = value; }
        }
        /// <summary>
        /// nhập giá trị vào textbox filter
        /// </summary>
        void tb_TextChanged(object sender, EventArgs e)
        {
            var filtered = mainDataSource
                .Where(w => string.IsNullOrWhiteSpace(tbSearch.Text) 
                    || w.ToUpper().Contains(tbSearch.Text.ToUpper())).ToArray();
            Items.Clear();
            Items.AddRange(filtered);
        }
        /// <summary>
        /// đổi font
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            if (allowFilter)
            {
                tbSearch.Font = this.Font; 
            }
            base.OnFontChanged(e);
        }
        /// <summary>
        /// đổi SelectedIndex THÀNH CÔNG
        /// </summary>
        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            int index = Array.IndexOf(mainDataSource, Text);
            tbSearch.Visible = false;
            tbSearch.Text = ""; // phải gọi trước vì sau khi chạy qua đây, items sẽ được renew lại => SelectedIndex bị thay đổi
            lastIndexSelected = SelectedIndex = index;
            base.OnSelectionChangeCommitted(e);
        }
        /// <summary>
        /// xổ dropdown xuống
        /// </summary>
        protected override void OnDropDown(EventArgs e)
        {
            if (allowFilter)
            {
                tbSearch.Visible = true;
                tbSearch.Font = Font;
                tbSearch.Size = Size;
                tbSearch.Focus();
            }
            base.OnDropDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (SelectedIndex == 0)
                    {
                        tbSearch.Focus();
                    }
                    break;
                case Keys.Down: 
                    if (tbSearch.Focused)
                    {
                        this.Select();
                    }
                    break;
                case Keys.Escape:
                    this.DroppedDown = false;
                    break;
                case Keys.Control | Keys.A:
                    this.tbSearch.SelectAll();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        int lastIndexSelected = 0;
        protected override void OnDropDownClosed(EventArgs e)
        {
            if (allowFilter)
            {
                tbSearch.Visible = false;
                tbSearch.Text = "";
                if (SelectedIndex == -1)
                {
                    // restore về index trước đó
                    SelectedIndex = lastIndexSelected;
                } 
            }
            Focus();

            base.OnDropDownClosed(e);
        }

        private ITextBox tbSearch;
        [Browsable(true)]
        public ITextBox TbSearch
        {
            get { return tbSearch; }
            set { tbSearch = value; }
        }
        
    }
}
