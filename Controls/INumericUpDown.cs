using System.Windows.Forms;

namespace Calculator
{
    class INumericUpDown : NumericUpDown
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0 && Value < Maximum)
            {
                Value++;
            }
            if (e.Delta < 0 && Value > Minimum)
            {
                Value--;
            }
        }
    }
}