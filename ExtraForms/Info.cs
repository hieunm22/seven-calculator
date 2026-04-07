using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Info : Form
    {
        public Info(Control control)
        {
            InitializeComponent();
            displayInfo(control);
        }

        public delegate void CustomHandler(Keys keys);
        /// <summary>
        /// send keys to main form before close
        /// </summary>
        public event CustomHandler Form_Closed;

        int wei;
        private void displayInfo(Control control)
        {
            string hotkey = "";
            switch (control.Text)
            {
                #region select by text
                case "1": case "2": case "3": case "4": case "5": case "6": case "7":
                case "8": case "9": case "0": case "+": case "-": case "*": case "/":
                case "%": case "(": case ")": case "A": case "B": case "D": case "E": case "F":
                    hotkey = control.Text;
                    break;
                case "←": hotkey = "Backspace";
                    break;
                case "=": case "Add": hotkey = "= or Enter";
                    break;
                case "CE": hotkey = "Delete";
                    break;
                case "C": if (control.Name == "btnC") hotkey = "C";
                    if (control.Name == "clearbt") hotkey = "Esc";
                    break;
                case "1/x": hotkey = "R";
                    break;
                case "±": hotkey = "F9";
                    break;
                case "M-": hotkey = "Ctrl-Q";
                    break;
                case "M+": hotkey = "Ctrl-P";
                    break;
                case "MS": hotkey = "Ctrl-M";
                    break;
                case "MR": hotkey = "Ctrl-R";
                    break;
                case "MC": hotkey = "Ctrl-L";
                    break;
                case "Inv": hotkey = "V";
                    break;
                case "√": hotkey = "@";
                    break;
                //------------------------------------------------------
                case "ln": case "eⁿ": hotkey = "N";
                    break;
                case "π": case "2*π":
                    hotkey = "P";
                    break;
                case "Int": case "Frac": hotkey = ";";
                    break;
                case "sinh": case "asinh": hotkey = "Ctrl-S";
                    break;
                case "cosh": case "acosh": hotkey = "Ctrl-O";
                    break;
                case "tanh": case "atanh": hotkey = "Ctrl-T";
                    break;
                case "sin": case "asin": hotkey = "S";
                    break;
                case "cos": case "acos": hotkey = "O";
                    break;
                case "tan": case "atan": hotkey = "T";
                    break;
                case "dms": case "deg": hotkey = "M";
                    break;
                case "F-E": hotkey = "V";
                    break;
                case "Exp": hotkey = "E or X";
                    break;
                case "Mod": case "CAD": hotkey = "D";
                    break;
                case "xⁿ": hotkey = "Y or ^";
                    break;
                case "x³": hotkey = "#";
                    break;
                case "x²": hotkey = "Q";
                    break;
                case "n!": hotkey = "!";
                    break;
                case "ⁿ√x": hotkey = "Ctrl-Y";
                    break;
                case "³√x": hotkey = "Ctrl-B";
                    break;
                case "10ⁿ": hotkey = "Ctrl-G";
                    break;
                //------------------------------------------------------
                case "log": hotkey = "L";
                    break;
                case "Qword": hotkey = "F12";
                    break;
                case "Dword": hotkey = "F2";
                    break;
                case "Degrees": case "Word": hotkey = "F3";
                    break;
                case "Radians": case "Byte": hotkey = "F4";
                    break;
                case "Grads": case "Hex": hotkey = "F5";
                    break;
                case "Dec": hotkey = "F6";
                    break;
                case "Oct": hotkey = "F7";
                    break;
                case "Bin": hotkey = "F8";
                    break;
                case "And": hotkey = "&";
                    break;
                case "Or": hotkey = "|";
                    break;
                case "Xor": hotkey = "^";
                    break;
                case "RoR": hotkey = "K";
                    break;
                case "RoL": hotkey = "J";
                    break;
                case "Rsh": hotkey = ">";
                    break;
                case "Lsh": hotkey = "<";
                    break;
                case "Not": hotkey = "~";
                    break;
                #endregion
            }
            switch (control.Name)
            {
                #region select by name
                case "x2cross": hotkey = "Ctrl-A";
                    break;
                case "xcross": hotkey = "A";
                    break;
                case "sigman_1BT": hotkey = "Ctrl-T";
                    break;
                case "sigmanBT": hotkey = "T";
                    break;
                case "sigmax2BT": hotkey = "Ctrl-S";
                    break;
                case "sigmaxBT": hotkey = "S";
                    break;
                case "bracketTime_lb":
                    displayText.Text = "Display the number of open bracket";
                    wei = 2 * displayText.Location.X + displayText.Size.Width;
                    return;
                #endregion
            }
            hotkeyLB.Text = hotkey;
            wei = hotkeyLB.Location.X + hotkeyLB.Size.Width + displayText.Location.X;
        }

        private void toolTip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolTip_Activated(object sender, EventArgs e)
        {
            Size = new Size(wei, 28);
            Location = new Point(Location.X - Size.Width / 2, Location.Y);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Close();
            if (Form_Closed != null) Form_Closed(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
