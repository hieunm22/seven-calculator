using System.Windows.Forms;

namespace Calculator
{
    class IRadioButton : RadioButton
    {
        [System.ComponentModel.Browsable(true)]
        public override ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set { base.ContextMenu = value; }
        }
        /// <summary>
        /// giá trị ở hệ xx-phân mà radio này được check
        /// </summary>
        public string Value { get; set; }
    }
}