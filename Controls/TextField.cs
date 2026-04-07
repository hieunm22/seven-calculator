using System;
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

        public bool TBFocus()
        {
            return txtField.Focus();
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", LabelText, TabIndex);
        }
    }
}
