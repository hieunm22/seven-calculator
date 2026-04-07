using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class TextField : UserControl
    {
        public TextField()
        {
            InitializeComponent();
        }

        public event EventHandler TextBoxGotFocus = null;
        public event EventHandler TextBoxLostFocus = null;
        public event MouseEventHandler MouseDownEvent = null;

        /// <summary>
        /// get/set thuộc tính text của label
        /// </summary>
        public string LabelText
        {
            get { return lbl.Text; }
            set { lbl.Text = value; }
        }
        /// <summary>
        /// get/set thuộc tính text của textbox
        /// </summary>
        public string TextBoxText
        {
            get { return txtField.Text; }
            set { txtField.Text = value; }
        }

        private bool allowTextChanged = true;
        public bool AllowTextChanged
        {
            get { return allowTextChanged; }
            set { allowTextChanged = value; }
        }

        public bool TBFocused
        {
            get { return txtField.Focused; }
        }

        private void txtField_TextChanged(object sender, EventArgs e)
        {
            if (!lbl.Text.EndsWith("*") && allowTextChanged) lbl.Text += " *";
        }
        /// <summary>
        /// dua su kien got focus ra form chinh
        /// </summary>
        private void txtField_GotFocus(object sender, EventArgs e)
        {
            if (TextBoxGotFocus != null) TextBoxGotFocus(sender, e);
        }
        /// <summary>
        /// dua su kien lost focus ra form chinh
        /// </summary>
        private void txtField_LostFocus(object sender, EventArgs e)
        {
            if (TextBoxLostFocus != null) TextBoxLostFocus(sender, e);
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDownEvent != null)
            {
                Parent.Focus();
                MouseDownEvent(sender, e);
            }
            txtField.Focus();
        }
        /// <summary>
        /// lay font cua textbox
        /// </summary>
        public Font TBFont
        {
            get { return txtField.Font; }
            set { txtField.Font = value; }
        }
        /// <summary>
        /// lay mau chu cua textbox
        /// </summary>
        public Color Fore_Color
        {
            get { return txtField.ForeColor; }
            set { txtField.ForeColor = value; }
        }

        public bool TBFocus()
        {
            return txtField.Focus();//|| txtField.Focus();
        }

        public override string ToString()
        {
            return LabelText;
        }
    }
}
