using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Scientific : Form
    {
        public Scientific()
        {
            InitializeComponent();
        }

        #region MyRegion
        //private void sciLoad()
        //{
        //    if (!scientificTSMI.Checked)
        //    {
        //        menugroup1_clicked(scientificTSMI);
        //        //scientificLoad();
        //        Scientific scifrm = new Scientific(this);
        //        //screenPN.Size = new System.Drawing.Size(422, 42);
        //        //removeComponent(true);
        //        clear_num(true);
        //        //scientificEvents();
        //        this.Hide();
        //        scifrm.Show();
        //    }
        //} 
        #endregion

        private void Scientific_Load(object sender, EventArgs e)
        {

        }

        private void standardTSMI_Click(object sender, EventArgs e)
        {
        
        }
    }
}
