using System.Windows.Forms;

namespace Calculator
{
    public partial class stdWithHistory : Form
    {
        public stdWithHistory()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(3);
        }
    }
}
