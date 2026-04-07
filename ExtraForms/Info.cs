using System;
using System.Drawing;
using System.Text.RegularExpressions;
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

        private readonly string KeyText = "Keyboard equivalent = ";

        public delegate void CustomHandler(Keys keys);
        /// <summary>
        /// send keys to main form before close
        /// </summary>
        public event CustomHandler Form_Closed;

        string description = @"";
        string hotkey = "";
        TextBox virtualTB;
        private void displayInfo(Control control)
        {
            virtualTB = new TextBox();
            virtualTB.Font = displayText.Font;
            switch (control.Text)
            {
                #region select by comparing to text
                case "1": case "2": case "3": case "4": case "5": 
                case "6": case "7": case "8": case "9": case "0":
                    description = @"Puts this number in the calculator display.";
                    hotkey = "0-9";
                    break;
                case "+":
                    description = @"Adds.";
                    hotkey = "+";
                    break;
                case "-":
                    description = @"Subtracts.";
                    hotkey = "-";
                    break;
                case "*":
                    description = @"Multiplies.";
                    hotkey = "*";
                    break;
                case "/":
                    description = @"Divides.";
                    hotkey = "/";
                    break;
                case "%":
                    description = @"Displays the result of multiplication as a percentage.
Enter one number, click *, enter the second number, and then 
click %. For example, 50 * 25% will display 12.5.
You can also perform operations with percentages. Enter one 
number, click the operator (+, -, *, or /), enter the second 
number, click %, and then click =. For example 50 + 25% (of 
50) = 62.5.";
                    hotkey = "%";
                    break;
                case "←":
                    description = @"Deletes the last digit of the displayed number.";
                    hotkey = "BACKSPACE";
                    break;
                case "=": 
                    description = @"Performs any operation on the previous two numbers. To 
repeat the last operation, click = again.";
                    hotkey = "ENTER";
                    break;
                case "CE": 
                    description = @"Clears the displayed number";
                    hotkey = "DELETE";
                    break;
                case "C":
                    if (control.Name == "btnC")
                    {
                        description = @"Enters the selected letter in the value.
This button is available only if hexadecimal mode is turned on.";
                        hotkey = "A-F";
                    }
                    if (control.Name == "clearbt")
                    {
                        description = @"Clears the current calculation.";
                        hotkey = "ESC";
                    }
                    break;
                case "1/x": 
                    description = @"Calculates the reciprocal of the displayed number.";
                    hotkey = "R";
                    break;
                case "±":
                    description = @"Changes the sign of the displayed number.";
                    hotkey = "F9";
                    break;
                case "M+":
                    description = @"Adds the displayed number to any number already in memory 
but does not display the sum of these numbers.";
                    hotkey = "Ctrl-P";
                    break;
                case "M-":
                    description = @"Subtracts the displayed number to any number already in memory 
but does not display the sum of these numbers.";
                    hotkey = "Ctrl-Q";
                    break;
                case "MS": 
                    description = @"Stores the displayed number in memory.";
                    hotkey = "Ctrl-M";
                    break;
                case "MR":
                    description = @"Recalls the number stored in memory. The number remains in 
memory.";
                    hotkey = "Ctrl-R";
                    break;
                case "MC": 
                    description = @"Clears any number stored in memory.";
                    hotkey = "Ctrl-L";
                    break;
                case "√":
                    description = @"Squares root the displayed number.";
                    hotkey = "@";
                    break;
                //------------------------------------------------------
                case "(":
                    description = @"Starts a new level of parentheses. The current number of 
levels appears in the box above the Int button. The maximum 
number of levels is 25.";
                    hotkey = "(";
                    break;
                case ")":
                    description = @"Close the current level of parentheses";
                    hotkey = ")";
                    break;
                case "Inv":
                    description = @"Sets the inverse function for sin, cos, tan, 
sinh, cosh, tanh, PI, ln, dms and int.
The functions automatically turn off the inverse function after a 
calculation is completed.";
                    hotkey = "I";
                    break;
                case "F-E":
                    description = @"Turns scientific notation on and off. Numbers larger than 10^32
are always displayed exponentially. You can use F-E only with 
the decimal number system.";
                    hotkey = "V";
                    break;
                case "π": case "2*π":
                    description = @"Displays the value of pi (3.1415...). To display 2 * pi (6.18...),
use Inv+Pi. You can use pi only with the decimal number 
system.";
                    hotkey = "P";
                    break;
                case "ln": case "eⁿ": 
					description = @"Calculates natural (base e) logarithm. To calculate e raised to 
the xth power, where x is the current number, use Inv+ln.";
                    hotkey = "N";
                    break;
                case "Int": case "Frac": 
                    description = @"Displays the integer portion of a decimal value. To display the 
fractional portion of a decimal value, use Inv+Int.";
                    hotkey = ";";
                    break;
                case "sin": case "asin": 
                    description = @"Calculates the sine of the diplayed number. To calculate the
arc sine, use Inv+sin. You can use sin only with the decimal 
number system.";
                    hotkey = "S";
                    break;
                case "cos": case "acos":
                    description = @"Calculates the cosine of the diplayed number. To calculate the
arc cosine, use Inv+cos. You can use cos only with the decimal 
number system.";
                    hotkey = "O";
                    break;
                case "tan": case "atan":
                    description = @"Calculates the tangent of the diplayed number. To calculate the
arc tangent, use Inv+tan. You can use tan only with the decimal 
number system.";
                    hotkey = "T";
                    break;
                case "sinh": case "asinh":
                    description = @"Calculates the hyperbolic sine of the diplayed number. To 
calculate the arc hyperbolic sine, use Inv+sin. You can use 
sinh only with the decimal number system.";
                    hotkey = "Ctrl-S";
                    break;
                case "cosh": case "acosh":
                    description = @"Calculates the hyperbolic cosine of the diplayed number. To 
calculate the arc hyperbolic cosine, use Inv+cosh. You can use 
cosh only with the decimal number system.";
                    hotkey = "Ctrl-O";
                    break;
                case "tanh": case "atanh":
                    description = @"Calculates the hyperbolic tangent of the diplayed number. To 
calculate the arc hyperbolic tangent, use Inv+tanh. You can use 
tanh only with the decimal number system.";
                    hotkey = "Ctrl-T";
                    break;
                case "dms": case "deg": 
                    description = @"Converts the displayed number to degree-minute-second format
(assuming that the displayed number is in degrees). To convert 
the displayed number to degrees (assuming that the displayed
number is in degree-minute-second format), use Inv+dms.
You can use dms only with the decimal number system.";
                    hotkey = "M";
                    break;
                case "Exp":
                    description = @"Allows entry of scientific-notation numbers. The exponent is 
unlimited. You can use only decimal digits (keys 0 through 9) 
in the exponent. You can use Exp only with the decimal 
number system.";
                    hotkey = "E or X";
                    break;
                case "xⁿ": 
                    description = @"Computes x raised to the yth power. Use this button as a binary 
operator. For example, to find 2 raised to the 4th power, click 
2 xⁿ 4 =, which equals 16.";
                    hotkey = "Y or ^";
                    break;
                case "ⁿ√x":
                    description = @"Calculate the yth root of x. Use this button as a binary 
operator. For example, to find 5th root of 32, click 32
ⁿ√x 5 =, which equals 5.";
                    hotkey = "Ctrl-Y";
                    break;
                case "x³": 
                    description = @"Cubes the displayed number.";
                    hotkey = "#";
                    break;
                case "³√x":
                    description = @"Cubes root the displayed number.";
                    hotkey = "Ctrl-B";
                    break;
                case "x²":
                    description = @"Squares the displayed number.";
                    hotkey = "Q";
                    break;
                case "n!":
                    description = @"Calculates the factorial of the displayed number.";
                    hotkey = "!";
                    break;
                case "log":
                    description = @"Calculates the common (base 10) logarithm.";
                    hotkey = "L";
                    break;
                case "10ⁿ":
                    description = @"Calculate 10 raised to the xth power.";
                    hotkey = "Ctrl-G";
                    break;
                case "Degrees":
                    description = @"Set trigonometric input for degrees when in decimal mode.";
                    hotkey = "F3";
                    break;
                case "Radians":
                    description = @"Set trigonometric input for radians when in decimal mode.";
                    hotkey = "F4";
                    break;
                case "Grads":
                    description = @"Set trigonometric input for grads when in decimal mode.";
                    hotkey = "F5";
                    break;
                //------------------------------------------------------
                case "A": case "B": case "D": case "E": case "F":
                    description = @"Enters the selected letter in the value.
This button is available only if hexadecimal mode is turned on.";
                    hotkey = "A-F";
                    break;
                case "Qword":
                    description = @"Converts the displayed number to 64-bit representation.";
                    hotkey = "F12";
                    break;
                case "Dword":
                    description = @"Converts the displayed number to 32-bit representation.";
                    hotkey = "F2";
                    break;
                case "Word":
                    description = @"Converts the displayed number to 16-bit representation.";
                    hotkey = "F3";
                    break;
                case "Byte":
                    description = @"Converts the displayed number to 8-bit representation.";
                    hotkey = "F4";
                    break;
                case "Hex":
                    description = @"Converts the displayed number to the hexadecimal number system.
The maximum unsigned hexadecimal value is 64 bits, all set to 1.";
                    hotkey = "F5";
                    break;
                case "Dec":
                    description = @"Convert the displayed number to the decimal number system.";
                    hotkey = "F6";
                    break;
                case "Oct":
                    description = @"Converts the displayed number to the octal number system.
The maximum unsigned octal value is an expression of 64 bits,
all set to 1.";
                    hotkey = "F7";
                    break;
                case "Bin":
                    description = @"Converts the displayed number to the binary number system.
The maximum unsigned binary value is an expression of 64 
bits, all set to 1.";
                    hotkey = "F8";
                    break;
                case "And": 
                    description = @"Calculates bitwise AND.
Logical operators will truncate the decimal portion of a number
before perorming any bitwise operation.";
                    hotkey = "&";
                    break;
                case "Or":
                    description = @"Calculates bitwise OR.
Logical operators will truncate the decimal portion of a number
before perorming any bitwise operation.";
                    hotkey = "|";
                    break;
                case "Xor":
                    description = @"Calculates bitwise exculusive OR.
Logical operators will truncate the decimal portion of a number
before perorming any bitwise operation.";
                    hotkey = "^";
                    break;
                case "RoR":
                    description = @"Shifts 1 position to the right.";
                    hotkey = "K";
                    break;
                case "RoL":
                    description = @"Shifts 1 position to the left.";
                    hotkey = "J";
                    break;
                case "Rsh":
                    description = @"Shifts right. After clicking this button, you must specify (in 
binary) how many positions to the right you want to shift 
the number in the display area, and then click =.
Logical operators will truncate the decimal portion of a 
number before performing any bitwise operation";
                    hotkey = ">";
                    break;
                case "Lsh": 
                    description = @"Shifts left. After clicking this button, you must specify (in 
binary) how many positions to the left you want to shift 
the number in the display area, and then click =.
Logical operators will truncate the decimal portion of a 
number before performing any bitwise operation.";
                    hotkey = "<";
                    break;
                case "Not": 
                    description = @"Calculates bitwise inverse.
Logical operators will truncate the decimal portion of a number
before performing any bitwise operation.";
                    hotkey = "~";
                    break;
                //------------------------------------------------------
                case "CAD":
                    description = @"Deletes all number from the statistics box.";
                    hotkey = "D";
                    break;
                case "Add":
                    description = @"Enters the displayed number in the statistics box.";
                    hotkey = "ENTER";
                    break;
                #endregion
            }
            switch (control.Name)
            {
                #region select by name
                case "xcross":
                    description = @"Calculates the mean of the values displayed in the statistics box.";
                    hotkey = "A";
                    break;
                case "x2cross":
                    description = @"Calculates the mean of the squares displayed in the statistics box.";
                    hotkey = "Ctrl-A";
                    break;
                case "sigman_1BT":
                    description = @"Calculates standard deviation with the population parameter as n-1.";
                    hotkey = "Ctrl-T";
                    break;
                case "sigmanBT":
                    description = @"Calculates standard deviation with the population parameter as n.";
                    hotkey = "T";
                    break;
                case "sigmax2BT":
                    description = @"Calculates the sum of the squares displayed in the statistics box.";
                    hotkey = "Ctrl-S";
                    break;
                case "sigmaxBT": 
                    description = @"Calculates the sum of the values displayed in the statistics box.";
                    hotkey = "S";
                    break;
                case "bracketTime_lb":
                    displayText.Text = @"Shows the number of opening (left) parentheses without
corresponding closing (right) parentheses.
For example, if you type 1 * (4 + (7 ^ 3) / (2 + 8, this button
shows (=2, which means that you have two left parentheses
with no corresponding right parentheses.";
                    AutoFit();
                    return;
                case "modproBT": case "modsciBT":
                    description = @"Displays the modulus, or remainder, of x/y. Use this button as a
binary operator.
For example, to find the modulus of 5 divided by 3, click 5 mod 
3 =, which equals 2.";
                    if (control.Name == "modproBT") hotkey = "%";
                    else hotkey = "D";
                    break;
                #endregion
            }
            displayText.Text = string.Format("{0}{1}{1}{2}{3}", description, Environment.NewLine, KeyText, hotkey);
            //displayText.Text = description + Environment.NewLine + Environment.NewLine + KeyText + hotkey;
            AutoFit();
        }

        private void toolTip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolTip_Activated(object sender, EventArgs e)
        {
            Location = new Point(Location.X - Size.Width / 2, Location.Y);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.Close();
            if (Form_Closed != null) Form_Closed(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AutoFit()
        {
            displayText.Size = TextRenderer.MeasureText(displayText.Text, displayText.Font);
            this.Height = displayText.Height + 12;
            this.Width = displayText.Width + 12;
            //using (Graphics g = CreateGraphics())
            //{
            //    displayText.Height = (int)g.MeasureString(displayText.Text, displayText.Font, displayText.Width).Height;
            //    this.Height = displayText.Height + 10;
            //}
        }
    }
}
