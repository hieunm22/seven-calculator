using System;
using System.Windows.Forms;

namespace Calculator
{
    class IDateTimePicker : DateTimePicker
    {
        private int FocusMask = 0;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int x = e.X;
            switch (FocusMask)
            {
                case 0:
                    DateTime addDays = Value.AddDays(1d * e.Delta / Math.Abs(e.Delta));
                    if (addDays.Month == Value.Month)
                    {
                        Value = addDays;
                    }
                    else
                    {
                        if (e.Delta > 0)
                        {
                            if (addDays.Month != Value.Month)
                                Value = new DateTime(Value.Year, Value.Month, 1);
                        }
                        else
                        {
                            if (addDays.Month != Value.Month)
                                Value = new DateTime(Value.Year, Value.Month, DateTime.DaysInMonth(Value.Year, Value.Month));
                        }
                    }
                    break;
                case 1:
                    DateTime addMonths = Value.AddMonths(e.Delta / Math.Abs(e.Delta));
                    Value = addMonths.AddYears(Value.Year - addMonths.Year);
                    break;
                case 2:
                    Value = Value.AddYears(e.Delta / Math.Abs(e.Delta));
                    break;
                default:
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X;
            if (x <= 17)
            {
                FocusMask = 0;
            }
            else if (x < 34)
            {
                FocusMask = 1;
            }
            else if (x <= 64)
            {
                FocusMask = 2;
            }
            base.OnMouseDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (--FocusMask == -1)
                    {
                        FocusMask = 2;
                    }
                    break;
                case Keys.Right:
                    if (++FocusMask == 3)
                    {
                        FocusMask = 0;
                    }
                    break;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}