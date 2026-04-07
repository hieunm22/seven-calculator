using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// Lớp ITextBox
    /// </summary>
    public class ITextBox : TextBox
    {
        private string suggestText = "";
        public string SuggestText
        {
            get { return suggestText; }
            set
            {
                suggestText = value;
                if (suggestType == SuggestType.WatermarkText)
                {
                    allowTextChanged = false;
                    Text = value;
                    allowTextChanged = true;
                    ForeColor = SystemColors.GrayText;
                    Font = new Font(Font, FontStyle.Italic);
                }
                if (suggestType == SuggestType.PlaceHolder)
                {
                    tbSuggest.Text = value;
                    Font = new Font(Font, FontStyle.Regular);
                    ForeColor = Color.Black;
                }
            }
        }

        private SuggestType suggestType = SuggestType.None;

        [DefaultValue(SuggestType.None)]
        public SuggestType SuggestType
        {
            get { return suggestType; }
            set
            {
                suggestType = value;
                switch (value)
                {
                    case SuggestType.None:
                        if (Controls.Contains(tbSuggest))
                        {
                            Controls.Remove(tbSuggest);
                        }
                        break;
                    case SuggestType.WatermarkText:
                        if (Controls.Contains(tbSuggest))
                        {
                            Controls.Remove(tbSuggest);
                        }
                        ForeColor = SystemColors.GrayText;
                        Font = new Font(Font, FontStyle.Italic);
                        allowTextChanged = false;
                        Text = suggestText;
                        allowTextChanged = true;
                        break;
                    case SuggestType.PlaceHolder:
                        tbSuggest = new TextBox();
                        tbSuggest.AcceptsTab = false;
                        tbSuggest.BorderStyle = BorderStyle.None;
                        tbSuggest.Font = new Font(Font, FontStyle.Regular);
                        tbSuggest.ForeColor = SystemColors.GrayText;
                        tbSuggest.Location = new Point(2, 1);
                        tbSuggest.ShortcutsEnabled = false;
                        tbSuggest.Text = suggestText;
                        tbSuggest.Enter += new EventHandler(tbSuggest_GotFocus);
                        tbSuggest.GotFocus += new EventHandler(tbSuggest_GotFocus);
                        Font = new Font(Font, FontStyle.Regular);
                        ForeColor = Color.Black;
                        allowTextChanged = false;
                        Text = "";
                        allowTextChanged = true;
                        Controls.Add(tbSuggest);
                        break;
                }
            }
        }

        private void tbSuggest_GotFocus(object sender, EventArgs e)
        {
            Focus();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!allowTextChanged) return;
            if (suggestType == SuggestType.WatermarkText)
            {
                ForeColor = SystemColors.ControlText;
            }
            else if (suggestType == SuggestType.PlaceHolder)
            {
                tbSuggest.Visible = Text == "";
            }
            base.OnTextChanged(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (suggestType == SuggestType.PlaceHolder)
            {
                tbSuggest.Visible = (Enabled && !ReadOnly) && Text == "";
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            if (suggestType == SuggestType.PlaceHolder)
            {
                tbSuggest.Visible = Enabled && !ReadOnly && Text == "";
            }
            base.OnReadOnlyChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (suggestType == SuggestType.PlaceHolder)
            {
                tbSuggest.Font = Font;
                Point pt = tbSuggest.GetPositionFromCharIndex(tbSuggest.Text.Length - 1);
                tbSuggest.Size = new Size(pt.X + 30, pt.Y);
            }
            base.OnFontChanged(e);
        }

        protected override void OnMultilineChanged(EventArgs e)
        {
            if (suggestType == SuggestType.PlaceHolder)
            {
                tbSuggest.Location = new Point(5, 1);
            }
            base.OnMultilineChanged(e);
        }

        private bool allowTextChanged = true;
        public bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        [DefaultValue(false)]
        /// <summary>
        /// chỉ cho nhập số
        /// </summary>
        public bool NumberModeOnly { get; set; }
        /// <summary>
        /// got focus
        /// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            if (suggestType == SuggestType.WatermarkText && Text == suggestText && ForeColor == SystemColors.GrayText)
            {
                allowTextChanged = false;
                Text = "";
                allowTextChanged = true;
                ForeColor = SystemColors.ControlText;
                Font = new Font(Font, FontStyle.Regular);
            }
            base.OnGotFocus(e);
        }
        /// <summary>
        /// lost focus
        /// </summary>
        protected override void OnLostFocus(EventArgs e)
        {
            if (Text == "" && suggestType == SuggestType.WatermarkText)
            {
                allowTextChanged = false;
                Text = suggestText;
                allowTextChanged = true;
                ForeColor = SystemColors.GrayText;
                Font = new Font(Font, FontStyle.Italic);
            }
            base.OnLostFocus(e);
        }
        /// <summary>
        /// keypress
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!NumberModeOnly) { base.OnKeyPress(e); return; }
            switch ((int)e.KeyChar)
            {
                case '.': case ',': case 'e': case 'E': case '+': case '-':
                    e.Handled = false;
                    break;
                case 'x': case 'X': case 'c': case 'C': case 'v': case 'V':
                case 1: case 3: case 8: case 22: case 24: case 26: case 27:
                    break;
                default:
                    e.Handled = !char.IsDigit(e.KeyChar);
                    break;
            }
            base.OnKeyPress(e);
        }

        private TextBox tbSuggest;
    }
}
