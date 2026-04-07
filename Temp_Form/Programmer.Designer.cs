namespace Calculator
{
    partial class Programmer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.screentb = new System.Windows.Forms.Panel();
            this.scr_lb = new System.Windows.Forms.Label();
            this.operator_lb = new System.Windows.Forms.Label();
            this.mem_lb = new System.Windows.Forms.Label();
            this.sqrt_bt = new System.Windows.Forms.Button();
            this.percent = new System.Windows.Forms.Button();
            this.invert_bt = new System.Windows.Forms.Button();
            this.equal = new System.Windows.Forms.Button();
            this.divbt = new System.Windows.Forms.Button();
            this.mulbt = new System.Windows.Forms.Button();
            this.devbt = new System.Windows.Forms.Button();
            this.addbt = new System.Windows.Forms.Button();
            this.btdot = new System.Windows.Forms.Button();
            this.num0 = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.mmul = new System.Windows.Forms.Button();
            this.memrecall = new System.Windows.Forms.Button();
            this.doidau = new System.Windows.Forms.Button();
            this.madd = new System.Windows.Forms.Button();
            this.num9 = new System.Windows.Forms.Button();
            this.mstore = new System.Windows.Forms.Button();
            this.ce = new System.Windows.Forms.Button();
            this.num6 = new System.Windows.Forms.Button();
            this.num8 = new System.Windows.Forms.Button();
            this.memclear = new System.Windows.Forms.Button();
            this.num3 = new System.Windows.Forms.Button();
            this.backspace = new System.Windows.Forms.Button();
            this.num5 = new System.Windows.Forms.Button();
            this.num7 = new System.Windows.Forms.Button();
            this.num2 = new System.Windows.Forms.Button();
            this.num4 = new System.Windows.Forms.Button();
            this.num1 = new System.Windows.Forms.Button();
            this.percent_bt = new System.Windows.Forms.Button();
            this.binaryPN = new System.Windows.Forms.Panel();
            this.btnC = new System.Windows.Forms.Button();
            this.RoLBT = new System.Windows.Forms.Button();
            this.orBT = new System.Windows.Forms.Button();
            this.LshBT = new System.Windows.Forms.Button();
            this.mod_bt = new System.Windows.Forms.Button();
            this.RoRBT = new System.Windows.Forms.Button();
            this.RshBT = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.AndBT = new System.Windows.Forms.Button();
            this.XorBT = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.btnF = new System.Windows.Forms.Button();
            this.open_bracket = new System.Windows.Forms.Button();
            this.close_bracket = new System.Windows.Forms.Button();
            this.nonameTB2 = new System.Windows.Forms.TextBox();
            this.NotBT = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.binRB = new System.Windows.Forms.RadioButton();
            this.octRB = new System.Windows.Forms.RadioButton();
            this.decRB = new System.Windows.Forms.RadioButton();
            this.hexRB = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.byteRB = new System.Windows.Forms.RadioButton();
            this.qwordRB = new System.Windows.Forms.RadioButton();
            this.wordRB = new System.Windows.Forms.RadioButton();
            this.dwordRB = new System.Windows.Forms.RadioButton();
            this.screentb.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // screentb
            // 
            this.screentb.BackColor = System.Drawing.Color.White;
            this.screentb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screentb.Controls.Add(this.scr_lb);
            this.screentb.Controls.Add(this.operator_lb);
            this.screentb.Controls.Add(this.mem_lb);
            this.screentb.Location = new System.Drawing.Point(12, 17);
            this.screentb.Name = "screentb";
            this.screentb.Size = new System.Drawing.Size(418, 56);
            this.screentb.TabIndex = 195;
            // 
            // scr_lb
            // 
            this.scr_lb.BackColor = System.Drawing.Color.White;
            this.scr_lb.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scr_lb.Location = new System.Drawing.Point(20, 19);
            this.scr_lb.Name = "scr_lb";
            this.scr_lb.Size = new System.Drawing.Size(393, 30);
            this.scr_lb.TabIndex = 108;
            this.scr_lb.Text = "0";
            this.scr_lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // operator_lb
            // 
            this.operator_lb.AutoSize = true;
            this.operator_lb.BackColor = System.Drawing.Color.White;
            this.operator_lb.Location = new System.Drawing.Point(4, 5);
            this.operator_lb.Name = "operator_lb";
            this.operator_lb.Size = new System.Drawing.Size(15, 16);
            this.operator_lb.TabIndex = 104;
            this.operator_lb.Text = "E";
            this.operator_lb.Visible = false;
            // 
            // mem_lb
            // 
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.White;
            this.mem_lb.Location = new System.Drawing.Point(1, 30);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(18, 16);
            this.mem_lb.TabIndex = 106;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            // 
            // sqrt_bt
            // 
            this.sqrt_bt.Enabled = false;
            this.sqrt_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sqrt_bt.Location = new System.Drawing.Point(394, 185);
            this.sqrt_bt.Name = "sqrt_bt";
            this.sqrt_bt.Size = new System.Drawing.Size(36, 30);
            this.sqrt_bt.TabIndex = 158;
            this.sqrt_bt.Text = "√";
            this.sqrt_bt.UseVisualStyleBackColor = true;
            // 
            // percent
            // 
            this.percent.Enabled = false;
            this.percent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent.Location = new System.Drawing.Point(394, 221);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(36, 30);
            this.percent.TabIndex = 157;
            this.percent.Text = "%";
            this.percent.UseVisualStyleBackColor = true;
            // 
            // invert_bt
            // 
            this.invert_bt.Enabled = false;
            this.invert_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.invert_bt.Location = new System.Drawing.Point(394, 257);
            this.invert_bt.Name = "invert_bt";
            this.invert_bt.Size = new System.Drawing.Size(36, 30);
            this.invert_bt.TabIndex = 156;
            this.invert_bt.Text = "1/x";
            this.invert_bt.UseVisualStyleBackColor = true;
            // 
            // equal
            // 
            this.equal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.equal.Location = new System.Drawing.Point(394, 293);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(36, 67);
            this.equal.TabIndex = 155;
            this.equal.Text = "=";
            this.equal.UseVisualStyleBackColor = true;
            // 
            // divbt
            // 
            this.divbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.divbt.Location = new System.Drawing.Point(352, 221);
            this.divbt.Name = "divbt";
            this.divbt.Size = new System.Drawing.Size(37, 30);
            this.divbt.TabIndex = 154;
            this.divbt.Text = "/";
            this.divbt.UseVisualStyleBackColor = true;
            // 
            // mulbt
            // 
            this.mulbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mulbt.Location = new System.Drawing.Point(352, 257);
            this.mulbt.Name = "mulbt";
            this.mulbt.Size = new System.Drawing.Size(37, 30);
            this.mulbt.TabIndex = 153;
            this.mulbt.Text = "*";
            this.mulbt.UseVisualStyleBackColor = true;
            // 
            // devbt
            // 
            this.devbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devbt.Location = new System.Drawing.Point(352, 293);
            this.devbt.Name = "devbt";
            this.devbt.Size = new System.Drawing.Size(37, 30);
            this.devbt.TabIndex = 152;
            this.devbt.Text = "-";
            this.devbt.UseVisualStyleBackColor = true;
            // 
            // addbt
            // 
            this.addbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addbt.Location = new System.Drawing.Point(352, 329);
            this.addbt.Name = "addbt";
            this.addbt.Size = new System.Drawing.Size(37, 30);
            this.addbt.TabIndex = 151;
            this.addbt.Text = "+";
            this.addbt.UseVisualStyleBackColor = true;
            // 
            // btdot
            // 
            this.btdot.Enabled = false;
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(309, 329);
            this.btdot.Name = "btdot";
            this.btdot.Size = new System.Drawing.Size(37, 30);
            this.btdot.TabIndex = 149;
            this.btdot.Text = ".";
            this.btdot.UseVisualStyleBackColor = true;
            // 
            // num0
            // 
            this.num0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num0.Location = new System.Drawing.Point(225, 329);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(79, 30);
            this.num0.TabIndex = 139;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            // 
            // clear
            // 
            this.clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clear.Location = new System.Drawing.Point(309, 185);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(37, 30);
            this.clear.TabIndex = 160;
            this.clear.Text = "C";
            this.clear.UseVisualStyleBackColor = true;
            // 
            // mmul
            // 
            this.mmul.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mmul.Location = new System.Drawing.Point(394, 149);
            this.mmul.Name = "mmul";
            this.mmul.Size = new System.Drawing.Size(36, 30);
            this.mmul.TabIndex = 167;
            this.mmul.Text = "M-";
            this.mmul.UseVisualStyleBackColor = true;
            // 
            // memrecall
            // 
            this.memrecall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memrecall.Location = new System.Drawing.Point(267, 149);
            this.memrecall.Name = "memrecall";
            this.memrecall.Size = new System.Drawing.Size(37, 30);
            this.memrecall.TabIndex = 165;
            this.memrecall.Text = "MR";
            this.memrecall.UseVisualStyleBackColor = true;
            // 
            // doidau
            // 
            this.doidau.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doidau.Location = new System.Drawing.Point(352, 185);
            this.doidau.Name = "doidau";
            this.doidau.Size = new System.Drawing.Size(37, 30);
            this.doidau.TabIndex = 150;
            this.doidau.Text = "±";
            this.doidau.UseVisualStyleBackColor = true;
            // 
            // madd
            // 
            this.madd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.madd.Location = new System.Drawing.Point(352, 149);
            this.madd.Name = "madd";
            this.madd.Size = new System.Drawing.Size(37, 30);
            this.madd.TabIndex = 166;
            this.madd.Text = "M+";
            this.madd.UseVisualStyleBackColor = true;
            // 
            // num9
            // 
            this.num9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9.Location = new System.Drawing.Point(309, 221);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(37, 30);
            this.num9.TabIndex = 148;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            // 
            // mstore
            // 
            this.mstore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mstore.Location = new System.Drawing.Point(309, 149);
            this.mstore.Name = "mstore";
            this.mstore.Size = new System.Drawing.Size(37, 30);
            this.mstore.TabIndex = 164;
            this.mstore.Text = "MS";
            this.mstore.UseVisualStyleBackColor = true;
            // 
            // ce
            // 
            this.ce.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ce.Location = new System.Drawing.Point(267, 185);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(37, 30);
            this.ce.TabIndex = 161;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            // 
            // num6
            // 
            this.num6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6.Location = new System.Drawing.Point(309, 257);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(37, 30);
            this.num6.TabIndex = 145;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            // 
            // num8
            // 
            this.num8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8.Location = new System.Drawing.Point(267, 221);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(37, 30);
            this.num8.TabIndex = 147;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            // 
            // memclear
            // 
            this.memclear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memclear.Location = new System.Drawing.Point(225, 149);
            this.memclear.Name = "memclear";
            this.memclear.Size = new System.Drawing.Size(37, 30);
            this.memclear.TabIndex = 163;
            this.memclear.Text = "MC";
            this.memclear.UseVisualStyleBackColor = true;
            // 
            // num3
            // 
            this.num3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3.Location = new System.Drawing.Point(309, 293);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(37, 30);
            this.num3.TabIndex = 142;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            // 
            // backspace
            // 
            this.backspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspace.Location = new System.Drawing.Point(225, 185);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(37, 30);
            this.backspace.TabIndex = 162;
            this.backspace.Text = "←";
            this.backspace.UseVisualStyleBackColor = true;
            // 
            // num5
            // 
            this.num5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5.Location = new System.Drawing.Point(267, 257);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(37, 30);
            this.num5.TabIndex = 144;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            // 
            // num7
            // 
            this.num7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7.Location = new System.Drawing.Point(225, 221);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(37, 30);
            this.num7.TabIndex = 146;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            // 
            // num2
            // 
            this.num2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2.Location = new System.Drawing.Point(267, 293);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(37, 30);
            this.num2.TabIndex = 141;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            // 
            // num4
            // 
            this.num4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4.Location = new System.Drawing.Point(225, 257);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(37, 30);
            this.num4.TabIndex = 143;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            // 
            // num1
            // 
            this.num1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num1.Location = new System.Drawing.Point(225, 293);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(37, 30);
            this.num1.TabIndex = 140;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            // 
            // percent_bt
            // 
            this.percent_bt.Enabled = false;
            this.percent_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent_bt.Location = new System.Drawing.Point(395, 221);
            this.percent_bt.Name = "percent_bt";
            this.percent_bt.Size = new System.Drawing.Size(36, 30);
            this.percent_bt.TabIndex = 168;
            this.percent_bt.Text = "n!";
            this.percent_bt.UseVisualStyleBackColor = true;
            this.percent_bt.Visible = false;
            // 
            // binaryPN
            // 
            this.binaryPN.BackColor = System.Drawing.Color.Transparent;
            this.binaryPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.binaryPN.Location = new System.Drawing.Point(12, 80);
            this.binaryPN.Name = "binaryPN";
            this.binaryPN.Size = new System.Drawing.Size(418, 63);
            this.binaryPN.TabIndex = 195;
            // 
            // btnC
            // 
            this.btnC.Enabled = false;
            this.btnC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnC.Location = new System.Drawing.Point(183, 221);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(37, 30);
            this.btnC.TabIndex = 179;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            // 
            // RoLBT
            // 
            this.RoLBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoLBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoLBT.Location = new System.Drawing.Point(98, 221);
            this.RoLBT.Name = "RoLBT";
            this.RoLBT.Size = new System.Drawing.Size(37, 30);
            this.RoLBT.TabIndex = 172;
            this.RoLBT.Text = "RoL";
            this.RoLBT.UseVisualStyleBackColor = true;
            // 
            // orBT
            // 
            this.orBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.orBT.Location = new System.Drawing.Point(98, 257);
            this.orBT.Name = "orBT";
            this.orBT.Size = new System.Drawing.Size(37, 30);
            this.orBT.TabIndex = 174;
            this.orBT.Text = "Or";
            this.orBT.UseVisualStyleBackColor = true;
            // 
            // LshBT
            // 
            this.LshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LshBT.Location = new System.Drawing.Point(98, 293);
            this.LshBT.Name = "LshBT";
            this.LshBT.Size = new System.Drawing.Size(37, 30);
            this.LshBT.TabIndex = 177;
            this.LshBT.Text = "Lsh";
            this.LshBT.UseVisualStyleBackColor = true;
            // 
            // modsciBT
            // 
            this.mod_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mod_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mod_bt.Location = new System.Drawing.Point(141, 149);
            this.mod_bt.Name = "modsciBT";
            this.mod_bt.Size = new System.Drawing.Size(37, 30);
            this.mod_bt.TabIndex = 182;
            this.mod_bt.Text = "Mod";
            this.mod_bt.UseVisualStyleBackColor = true;
            // 
            // RoRBT
            // 
            this.RoRBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoRBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoRBT.Location = new System.Drawing.Point(141, 221);
            this.RoRBT.Name = "RoRBT";
            this.RoRBT.Size = new System.Drawing.Size(37, 30);
            this.RoRBT.TabIndex = 190;
            this.RoRBT.Text = "RoR";
            this.RoRBT.UseVisualStyleBackColor = true;
            // 
            // RshBT
            // 
            this.RshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RshBT.Location = new System.Drawing.Point(141, 293);
            this.RshBT.Name = "RshBT";
            this.RshBT.Size = new System.Drawing.Size(37, 30);
            this.RshBT.TabIndex = 189;
            this.RshBT.Text = "Rsh";
            this.RshBT.UseVisualStyleBackColor = true;
            // 
            // btnE
            // 
            this.btnE.Enabled = false;
            this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnE.Location = new System.Drawing.Point(183, 293);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(37, 30);
            this.btnE.TabIndex = 188;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            // 
            // AndBT
            // 
            this.AndBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AndBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AndBT.Location = new System.Drawing.Point(141, 329);
            this.AndBT.Name = "AndBT";
            this.AndBT.Size = new System.Drawing.Size(37, 30);
            this.AndBT.TabIndex = 184;
            this.AndBT.Text = "And";
            this.AndBT.UseVisualStyleBackColor = true;
            // 
            // XorBT
            // 
            this.XorBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XorBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.XorBT.Location = new System.Drawing.Point(141, 257);
            this.XorBT.Name = "XorBT";
            this.XorBT.Size = new System.Drawing.Size(37, 30);
            this.XorBT.TabIndex = 185;
            this.XorBT.Text = "Xor";
            this.XorBT.UseVisualStyleBackColor = true;
            // 
            // btnD
            // 
            this.btnD.Enabled = false;
            this.btnD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnD.Location = new System.Drawing.Point(183, 257);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(37, 30);
            this.btnD.TabIndex = 186;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = true;
            // 
            // btnF
            // 
            this.btnF.Enabled = false;
            this.btnF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnF.Location = new System.Drawing.Point(183, 329);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(37, 30);
            this.btnF.TabIndex = 187;
            this.btnF.Text = "F";
            this.btnF.UseVisualStyleBackColor = true;
            // 
            // open_bracket
            // 
            this.open_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.open_bracket.Location = new System.Drawing.Point(98, 185);
            this.open_bracket.Name = "open_bracket";
            this.open_bracket.Size = new System.Drawing.Size(37, 30);
            this.open_bracket.TabIndex = 192;
            this.open_bracket.Text = "(";
            this.open_bracket.UseVisualStyleBackColor = true;
            // 
            // close_bracket
            // 
            this.close_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_bracket.Location = new System.Drawing.Point(141, 185);
            this.close_bracket.Name = "close_bracket";
            this.close_bracket.Size = new System.Drawing.Size(37, 30);
            this.close_bracket.TabIndex = 191;
            this.close_bracket.Text = ")";
            this.close_bracket.UseVisualStyleBackColor = true;
            // 
            // nonameTB2
            // 
            this.nonameTB2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nonameTB2.Enabled = false;
            this.nonameTB2.Location = new System.Drawing.Point(98, 149);
            this.nonameTB2.Multiline = true;
            this.nonameTB2.Name = "nonameTB2";
            this.nonameTB2.ReadOnly = true;
            this.nonameTB2.Size = new System.Drawing.Size(37, 30);
            this.nonameTB2.TabIndex = 159;
            this.nonameTB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NotBT
            // 
            this.NotBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotBT.Location = new System.Drawing.Point(98, 329);
            this.NotBT.Name = "NotBT";
            this.NotBT.Size = new System.Drawing.Size(37, 30);
            this.NotBT.TabIndex = 184;
            this.NotBT.Text = "Not";
            this.NotBT.UseVisualStyleBackColor = true;
            // 
            // btnA
            // 
            this.btnA.Enabled = false;
            this.btnA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnA.Location = new System.Drawing.Point(183, 149);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(37, 30);
            this.btnA.TabIndex = 179;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            // 
            // btnB
            // 
            this.btnB.Enabled = false;
            this.btnB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnB.Location = new System.Drawing.Point(183, 185);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(37, 30);
            this.btnB.TabIndex = 186;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            // 
            // basePN
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.binRB);
            this.panel1.Controls.Add(this.octRB);
            this.panel1.Controls.Add(this.decRB);
            this.panel1.Controls.Add(this.hexRB);
            this.panel1.Location = new System.Drawing.Point(12, 149);
            this.panel1.Name = "basePN";
            this.panel1.Size = new System.Drawing.Size(80, 102);
            this.panel1.TabIndex = 196;
            // 
            // binRB
            // 
            this.binRB.AutoSize = true;
            this.binRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binRB.Location = new System.Drawing.Point(7, 73);
            this.binRB.Name = "binRB";
            this.binRB.Size = new System.Drawing.Size(39, 17);
            this.binRB.TabIndex = 0;
            this.binRB.Text = "Bin";
            this.binRB.UseVisualStyleBackColor = true;
            // 
            // octRB
            // 
            this.octRB.AutoSize = true;
            this.octRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.octRB.Location = new System.Drawing.Point(7, 52);
            this.octRB.Name = "octRB";
            this.octRB.Size = new System.Drawing.Size(42, 17);
            this.octRB.TabIndex = 0;
            this.octRB.Text = "Oct";
            this.octRB.UseVisualStyleBackColor = true;
            // 
            // decRB
            // 
            this.decRB.AutoSize = true;
            this.decRB.Checked = true;
            this.decRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decRB.Location = new System.Drawing.Point(7, 31);
            this.decRB.Name = "decRB";
            this.decRB.Size = new System.Drawing.Size(43, 17);
            this.decRB.TabIndex = 0;
            this.decRB.TabStop = true;
            this.decRB.Text = "Dec";
            this.decRB.UseVisualStyleBackColor = true;
            // 
            // hexRB
            // 
            this.hexRB.AutoSize = true;
            this.hexRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexRB.Location = new System.Drawing.Point(7, 10);
            this.hexRB.Name = "hexRB";
            this.hexRB.Size = new System.Drawing.Size(44, 17);
            this.hexRB.TabIndex = 0;
            this.hexRB.Text = "Hex";
            this.hexRB.UseVisualStyleBackColor = true;
            // 
            // unknownPN
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.byteRB);
            this.panel2.Controls.Add(this.qwordRB);
            this.panel2.Controls.Add(this.wordRB);
            this.panel2.Controls.Add(this.dwordRB);
            this.panel2.Location = new System.Drawing.Point(12, 257);
            this.panel2.Name = "unknownPN";
            this.panel2.Size = new System.Drawing.Size(80, 102);
            this.panel2.TabIndex = 196;
            // 
            // byteRB
            // 
            this.byteRB.AutoSize = true;
            this.byteRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.byteRB.Location = new System.Drawing.Point(7, 73);
            this.byteRB.Name = "byteRB";
            this.byteRB.Size = new System.Drawing.Size(47, 17);
            this.byteRB.TabIndex = 0;
            this.byteRB.Text = "Byte";
            this.byteRB.UseVisualStyleBackColor = true;
            // 
            // qwordRB
            // 
            this.qwordRB.AutoSize = true;
            this.qwordRB.Checked = true;
            this.qwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qwordRB.Location = new System.Drawing.Point(7, 10);
            this.qwordRB.Name = "qwordRB";
            this.qwordRB.Size = new System.Drawing.Size(57, 17);
            this.qwordRB.TabIndex = 0;
            this.qwordRB.TabStop = true;
            this.qwordRB.Text = "Qword";
            this.qwordRB.UseVisualStyleBackColor = true;
            // 
            // wordRB
            // 
            this.wordRB.AutoSize = true;
            this.wordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordRB.Location = new System.Drawing.Point(7, 52);
            this.wordRB.Name = "wordRB";
            this.wordRB.Size = new System.Drawing.Size(51, 17);
            this.wordRB.TabIndex = 0;
            this.wordRB.Text = "Word";
            this.wordRB.UseVisualStyleBackColor = true;
            // 
            // dwordRB
            // 
            this.dwordRB.AutoSize = true;
            this.dwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwordRB.Location = new System.Drawing.Point(7, 31);
            this.dwordRB.Name = "dwordRB";
            this.dwordRB.Size = new System.Drawing.Size(56, 17);
            this.dwordRB.TabIndex = 0;
            this.dwordRB.Text = "Dword";
            this.dwordRB.UseVisualStyleBackColor = true;
            // 
            // Programmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 371);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nonameTB2);
            this.Controls.Add(this.binaryPN);
            this.Controls.Add(this.screentb);
            this.Controls.Add(this.close_bracket);
            this.Controls.Add(this.open_bracket);
            this.Controls.Add(this.btnF);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.btnD);
            this.Controls.Add(this.XorBT);
            this.Controls.Add(this.NotBT);
            this.Controls.Add(this.AndBT);
            this.Controls.Add(this.btnE);
            this.Controls.Add(this.RshBT);
            this.Controls.Add(this.RoRBT);
            this.Controls.Add(this.mod_bt);
            this.Controls.Add(this.LshBT);
            this.Controls.Add(this.orBT);
            this.Controls.Add(this.RoLBT);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.sqrt_bt);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.invert_bt);
            this.Controls.Add(this.equal);
            this.Controls.Add(this.divbt);
            this.Controls.Add(this.mulbt);
            this.Controls.Add(this.devbt);
            this.Controls.Add(this.addbt);
            this.Controls.Add(this.btdot);
            this.Controls.Add(this.num0);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.mmul);
            this.Controls.Add(this.memrecall);
            this.Controls.Add(this.doidau);
            this.Controls.Add(this.madd);
            this.Controls.Add(this.num9);
            this.Controls.Add(this.mstore);
            this.Controls.Add(this.ce);
            this.Controls.Add(this.num6);
            this.Controls.Add(this.num8);
            this.Controls.Add(this.memclear);
            this.Controls.Add(this.num3);
            this.Controls.Add(this.backspace);
            this.Controls.Add(this.num5);
            this.Controls.Add(this.num7);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.num4);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.percent_bt);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Programmer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programmer";
            this.screentb.ResumeLayout(false);
            this.screentb.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel screentb;
        private System.Windows.Forms.Label scr_lb;
        private System.Windows.Forms.Label mem_lb;
        private System.Windows.Forms.Button sqrt_bt;
        private System.Windows.Forms.Button percent;
        private System.Windows.Forms.Button invert_bt;
        private System.Windows.Forms.Button equal;
        private System.Windows.Forms.Button divbt;
        private System.Windows.Forms.Button mulbt;
        private System.Windows.Forms.Button devbt;
        private System.Windows.Forms.Button addbt;
        private System.Windows.Forms.Button btdot;
        private System.Windows.Forms.Button num0;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button mmul;
        private System.Windows.Forms.Button memrecall;
        private System.Windows.Forms.Button doidau;
        private System.Windows.Forms.Button madd;
        private System.Windows.Forms.Button num9;
        private System.Windows.Forms.Button mstore;
        private System.Windows.Forms.Button ce;
        private System.Windows.Forms.Button num6;
        private System.Windows.Forms.Button num8;
        private System.Windows.Forms.Button memclear;
        private System.Windows.Forms.Button num3;
        private System.Windows.Forms.Button backspace;
        private System.Windows.Forms.Button num5;
        private System.Windows.Forms.Button num7;
        private System.Windows.Forms.Button num2;
        private System.Windows.Forms.Button num4;
        private System.Windows.Forms.Button num1;
        private System.Windows.Forms.Button percent_bt;
        private System.Windows.Forms.Panel binaryPN;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button RoLBT;
        private System.Windows.Forms.Button orBT;
        private System.Windows.Forms.Button LshBT;
        private System.Windows.Forms.Button mod_bt;
        private System.Windows.Forms.Button RoRBT;
        private System.Windows.Forms.Button RshBT;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button AndBT;
        private System.Windows.Forms.Button XorBT;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnF;
        private System.Windows.Forms.Button open_bracket;
        private System.Windows.Forms.Button close_bracket;
        private System.Windows.Forms.TextBox nonameTB2;
        private System.Windows.Forms.Button NotBT;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton hexRB;
        private System.Windows.Forms.RadioButton binRB;
        private System.Windows.Forms.RadioButton octRB;
        private System.Windows.Forms.RadioButton decRB;
        private System.Windows.Forms.RadioButton byteRB;
        private System.Windows.Forms.RadioButton qwordRB;
        private System.Windows.Forms.RadioButton wordRB;
        private System.Windows.Forms.RadioButton dwordRB;
        private System.Windows.Forms.Label operator_lb;
    }
}