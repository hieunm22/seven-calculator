namespace Calculator
{
    partial class Statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            this.statisticsPN = new System.Windows.Forms.Panel();
            this.statisticsDGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.screentb = new System.Windows.Forms.Panel();
            this.scr_lb = new System.Windows.Forms.Label();
            this.mem_lb = new System.Windows.Forms.Label();
            this.operator_lb = new System.Windows.Forms.Label();
            this.Exppro_bt = new System.Windows.Forms.Button();
            this.sigmax2BT = new System.Windows.Forms.Button();
            this.AddproBT = new System.Windows.Forms.Button();
            this.xcross = new System.Windows.Forms.Button();
            this.sigmaxBT = new System.Windows.Forms.Button();
            this.sigmanBT = new System.Windows.Forms.Button();
            this.doidauproBT = new System.Windows.Forms.Button();
            this.btdot = new System.Windows.Forms.Button();
            this.num0 = new System.Windows.Forms.Button();
            this.clearbt = new System.Windows.Forms.Button();
            this.mmul = new System.Windows.Forms.Button();
            this.memrecall = new System.Windows.Forms.Button();
            this.FEProBT = new System.Windows.Forms.Button();
            this.madd = new System.Windows.Forms.Button();
            this.num9 = new System.Windows.Forms.Button();
            this.mstore = new System.Windows.Forms.Button();
            this.CAD = new System.Windows.Forms.Button();
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
            this.x2cross = new System.Windows.Forms.Button();
            this.sigman_1BT = new System.Windows.Forms.Button();
            this.statisticsPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statisticsDGV)).BeginInit();
            this.screentb.SuspendLayout();
            this.SuspendLayout();
            // 
            // statisticsPN
            // 
            this.statisticsPN.BackColor = System.Drawing.Color.White;
            this.statisticsPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statisticsPN.Controls.Add(this.statisticsDGV);
            this.statisticsPN.Location = new System.Drawing.Point(12, 17);
            this.statisticsPN.Name = "statisticsPN";
            this.statisticsPN.Size = new System.Drawing.Size(206, 132);
            this.statisticsPN.TabIndex = 245;
            // 
            // statisticsDGV
            // 
            this.statisticsDGV.AllowUserToDeleteRows = false;
            this.statisticsDGV.AllowUserToResizeColumns = false;
            this.statisticsDGV.AllowUserToResizeRows = false;
            this.statisticsDGV.BackgroundColor = System.Drawing.Color.White;
            this.statisticsDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.statisticsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statisticsDGV.ColumnHeadersVisible = false;
            this.statisticsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.statisticsDGV.Location = new System.Drawing.Point(-1, 24);
            this.statisticsDGV.Name = "statisticsDGV";
            this.statisticsDGV.ReadOnly = true;
            this.statisticsDGV.RowHeadersVisible = false;
            this.statisticsDGV.Size = new System.Drawing.Size(206, 107);
            this.statisticsDGV.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 204;
            // 
            // screentb
            // 
            this.screentb.BackColor = System.Drawing.Color.White;
            this.screentb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screentb.Controls.Add(this.scr_lb);
            this.screentb.Controls.Add(this.mem_lb);
            this.screentb.Controls.Add(this.operator_lb);
            this.screentb.Location = new System.Drawing.Point(12, 148);
            this.screentb.Name = "screentb";
            this.screentb.Size = new System.Drawing.Size(206, 42);
            this.screentb.TabIndex = 244;
            // 
            // scr_lb
            // 
            this.scr_lb.BackColor = System.Drawing.Color.White;
            this.scr_lb.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scr_lb.Location = new System.Drawing.Point(23, 9);
            this.scr_lb.Name = "scr_lb";
            this.scr_lb.Size = new System.Drawing.Size(178, 22);
            this.scr_lb.TabIndex = 22;
            this.scr_lb.Text = "0";
            this.scr_lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mem_lb
            // 
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.White;
            this.mem_lb.Location = new System.Drawing.Point(2, 21);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(18, 16);
            this.mem_lb.TabIndex = 21;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            // 
            // operator_lb
            // 
            this.operator_lb.AutoSize = true;
            this.operator_lb.BackColor = System.Drawing.Color.White;
            this.operator_lb.Location = new System.Drawing.Point(2, 2);
            this.operator_lb.Name = "operator_lb";
            this.operator_lb.Size = new System.Drawing.Size(15, 16);
            this.operator_lb.TabIndex = 21;
            this.operator_lb.Text = "E";
            this.operator_lb.Visible = false;
            // 
            // Expsta_bt
            // 
            this.Exppro_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Exppro_bt.Location = new System.Drawing.Point(182, 232);
            this.Exppro_bt.Name = "Expsta_bt";
            this.Exppro_bt.Size = new System.Drawing.Size(36, 30);
            this.Exppro_bt.TabIndex = 235;
            this.Exppro_bt.TabStop = false;
            this.Exppro_bt.Text = "Exp";
            this.Exppro_bt.UseVisualStyleBackColor = true;
            // 
            // sigmax2BT
            // 
            this.sigmax2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmax2BT.Image = ((System.Drawing.Image)(resources.GetObject("sigmax2BT.Image")));
            this.sigmax2BT.Location = new System.Drawing.Point(182, 304);
            this.sigmax2BT.Name = "sigmax2BT";
            this.sigmax2BT.Size = new System.Drawing.Size(36, 30);
            this.sigmax2BT.TabIndex = 233;
            this.sigmax2BT.TabStop = false;
            this.sigmax2BT.UseVisualStyleBackColor = true;
            this.sigmax2BT.Click += new System.EventHandler(this.invert_bt_Click);
            // 
            // AddstaBT
            // 
            this.AddproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddproBT.Location = new System.Drawing.Point(182, 377);
            this.AddproBT.Name = "AddstaBT";
            this.AddproBT.Size = new System.Drawing.Size(36, 30);
            this.AddproBT.TabIndex = 232;
            this.AddproBT.TabStop = false;
            this.AddproBT.Text = "Add";
            this.AddproBT.UseVisualStyleBackColor = true;
            // 
            // xcross
            // 
            this.xcross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xcross.Image = ((System.Drawing.Image)(resources.GetObject("xcross.Image")));
            this.xcross.Location = new System.Drawing.Point(139, 268);
            this.xcross.Name = "xcross";
            this.xcross.Size = new System.Drawing.Size(36, 30);
            this.xcross.TabIndex = 231;
            this.xcross.TabStop = false;
            this.xcross.UseVisualStyleBackColor = true;
            // 
            // sigmaxBT
            // 
            this.sigmaxBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmaxBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmaxBT.Image")));
            this.sigmaxBT.Location = new System.Drawing.Point(139, 304);
            this.sigmaxBT.Name = "sigmaxBT";
            this.sigmaxBT.Size = new System.Drawing.Size(36, 30);
            this.sigmaxBT.TabIndex = 230;
            this.sigmaxBT.TabStop = false;
            this.sigmaxBT.UseVisualStyleBackColor = true;
            // 
            // sigmanBT
            // 
            this.sigmanBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmanBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmanBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmanBT.Image")));
            this.sigmanBT.Location = new System.Drawing.Point(139, 340);
            this.sigmanBT.Name = "sigmanBT";
            this.sigmanBT.Size = new System.Drawing.Size(36, 30);
            this.sigmanBT.TabIndex = 229;
            this.sigmanBT.TabStop = false;
            this.sigmanBT.UseVisualStyleBackColor = true;
            // 
            // doidaustaBT
            // 
            this.doidauproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doidauproBT.Location = new System.Drawing.Point(139, 377);
            this.doidauproBT.Name = "doidaustaBT";
            this.doidauproBT.Size = new System.Drawing.Size(36, 30);
            this.doidauproBT.TabIndex = 228;
            this.doidauproBT.TabStop = false;
            this.doidauproBT.Text = "±";
            this.doidauproBT.UseVisualStyleBackColor = true;
            // 
            // btdot
            // 
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(96, 377);
            this.btdot.Name = "btdot";
            this.btdot.Size = new System.Drawing.Size(36, 30);
            this.btdot.TabIndex = 226;
            this.btdot.TabStop = false;
            this.btdot.Text = ".";
            this.btdot.UseVisualStyleBackColor = true;
            // 
            // num0
            // 
            this.num0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num0.Location = new System.Drawing.Point(12, 377);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(78, 30);
            this.num0.TabIndex = 216;
            this.num0.TabStop = false;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            // 
            // clearbt
            // 
            this.clearbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearbt.Location = new System.Drawing.Point(96, 232);
            this.clearbt.Name = "clearbt";
            this.clearbt.Size = new System.Drawing.Size(36, 30);
            this.clearbt.TabIndex = 236;
            this.clearbt.TabStop = false;
            this.clearbt.Text = "C";
            this.clearbt.UseVisualStyleBackColor = true;
            // 
            // mmul
            // 
            this.mmul.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mmul.Location = new System.Drawing.Point(182, 196);
            this.mmul.Name = "mmul";
            this.mmul.Size = new System.Drawing.Size(36, 30);
            this.mmul.TabIndex = 243;
            this.mmul.TabStop = false;
            this.mmul.Text = "M-";
            this.mmul.UseVisualStyleBackColor = true;
            // 
            // memrecall
            // 
            this.memrecall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memrecall.Location = new System.Drawing.Point(96, 196);
            this.memrecall.Name = "memrecall";
            this.memrecall.Size = new System.Drawing.Size(36, 30);
            this.memrecall.TabIndex = 241;
            this.memrecall.TabStop = false;
            this.memrecall.Text = "MR";
            this.memrecall.UseVisualStyleBackColor = true;
            // 
            // FEStaBT
            // 
            this.FEProBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FEProBT.Location = new System.Drawing.Point(139, 232);
            this.FEProBT.Name = "FEStaBT";
            this.FEProBT.Size = new System.Drawing.Size(36, 30);
            this.FEProBT.TabIndex = 227;
            this.FEProBT.TabStop = false;
            this.FEProBT.Text = "F-E";
            this.FEProBT.UseVisualStyleBackColor = true;
            // 
            // madd
            // 
            this.madd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.madd.Location = new System.Drawing.Point(139, 196);
            this.madd.Name = "madd";
            this.madd.Size = new System.Drawing.Size(36, 30);
            this.madd.TabIndex = 242;
            this.madd.TabStop = false;
            this.madd.Text = "M+";
            this.madd.UseVisualStyleBackColor = true;
            // 
            // num9
            // 
            this.num9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9.Location = new System.Drawing.Point(96, 268);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(36, 30);
            this.num9.TabIndex = 225;
            this.num9.TabStop = false;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            // 
            // mstore
            // 
            this.mstore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mstore.Location = new System.Drawing.Point(54, 196);
            this.mstore.Name = "mstore";
            this.mstore.Size = new System.Drawing.Size(36, 30);
            this.mstore.TabIndex = 240;
            this.mstore.TabStop = false;
            this.mstore.Text = "MS";
            this.mstore.UseVisualStyleBackColor = true;
            // 
            // CAD
            // 
            this.CAD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CAD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CAD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CAD.Location = new System.Drawing.Point(54, 232);
            this.CAD.Name = "CAD";
            this.CAD.Size = new System.Drawing.Size(36, 30);
            this.CAD.TabIndex = 237;
            this.CAD.TabStop = false;
            this.CAD.Text = "CAD";
            this.CAD.UseVisualStyleBackColor = true;
            // 
            // num6
            // 
            this.num6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6.Location = new System.Drawing.Point(96, 304);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(36, 30);
            this.num6.TabIndex = 222;
            this.num6.TabStop = false;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            // 
            // num8
            // 
            this.num8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8.Location = new System.Drawing.Point(54, 268);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(36, 30);
            this.num8.TabIndex = 224;
            this.num8.TabStop = false;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            // 
            // memclear
            // 
            this.memclear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memclear.Location = new System.Drawing.Point(12, 196);
            this.memclear.Name = "memclear";
            this.memclear.Size = new System.Drawing.Size(36, 30);
            this.memclear.TabIndex = 239;
            this.memclear.TabStop = false;
            this.memclear.Text = "MC";
            this.memclear.UseVisualStyleBackColor = true;
            // 
            // num3
            // 
            this.num3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3.Location = new System.Drawing.Point(96, 340);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(36, 30);
            this.num3.TabIndex = 219;
            this.num3.TabStop = false;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            // 
            // backspace
            // 
            this.backspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspace.Location = new System.Drawing.Point(12, 232);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(36, 30);
            this.backspace.TabIndex = 238;
            this.backspace.TabStop = false;
            this.backspace.Text = "←";
            this.backspace.UseVisualStyleBackColor = true;
            // 
            // num5
            // 
            this.num5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5.Location = new System.Drawing.Point(54, 304);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(36, 30);
            this.num5.TabIndex = 221;
            this.num5.TabStop = false;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            // 
            // num7
            // 
            this.num7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7.Location = new System.Drawing.Point(12, 268);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(36, 30);
            this.num7.TabIndex = 223;
            this.num7.TabStop = false;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            // 
            // num2
            // 
            this.num2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2.Location = new System.Drawing.Point(54, 340);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(36, 30);
            this.num2.TabIndex = 218;
            this.num2.TabStop = false;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            // 
            // num4
            // 
            this.num4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4.Location = new System.Drawing.Point(12, 304);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(36, 30);
            this.num4.TabIndex = 220;
            this.num4.TabStop = false;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            // 
            // num1
            // 
            this.num1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num1.Location = new System.Drawing.Point(12, 340);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(36, 30);
            this.num1.TabIndex = 217;
            this.num1.TabStop = false;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            // 
            // x2cross
            // 
            this.x2cross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2cross.Image = ((System.Drawing.Image)(resources.GetObject("x2cross.Image")));
            this.x2cross.Location = new System.Drawing.Point(182, 268);
            this.x2cross.Name = "x2cross";
            this.x2cross.Size = new System.Drawing.Size(36, 30);
            this.x2cross.TabIndex = 234;
            this.x2cross.TabStop = false;
            this.x2cross.UseVisualStyleBackColor = true;
            // 
            // sigman_1BT
            // 
            this.sigman_1BT.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigman_1BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigman_1BT.Image = ((System.Drawing.Image)(resources.GetObject("sigman_1BT.Image")));
            this.sigman_1BT.Location = new System.Drawing.Point(182, 341);
            this.sigman_1BT.Name = "sigman_1BT";
            this.sigman_1BT.Size = new System.Drawing.Size(36, 30);
            this.sigman_1BT.TabIndex = 232;
            this.sigman_1BT.TabStop = false;
            this.sigman_1BT.UseVisualStyleBackColor = true;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 420);
            this.Controls.Add(this.statisticsPN);
            this.Controls.Add(this.screentb);
            this.Controls.Add(this.Exppro_bt);
            this.Controls.Add(this.sigmax2BT);
            this.Controls.Add(this.sigman_1BT);
            this.Controls.Add(this.AddproBT);
            this.Controls.Add(this.xcross);
            this.Controls.Add(this.sigmaxBT);
            this.Controls.Add(this.sigmanBT);
            this.Controls.Add(this.doidauproBT);
            this.Controls.Add(this.btdot);
            this.Controls.Add(this.num0);
            this.Controls.Add(this.clearbt);
            this.Controls.Add(this.mmul);
            this.Controls.Add(this.memrecall);
            this.Controls.Add(this.FEProBT);
            this.Controls.Add(this.madd);
            this.Controls.Add(this.num9);
            this.Controls.Add(this.mstore);
            this.Controls.Add(this.CAD);
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
            this.Controls.Add(this.x2cross);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Statistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            this.statisticsPN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statisticsDGV)).EndInit();
            this.screentb.ResumeLayout(false);
            this.screentb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel statisticsPN;
        private System.Windows.Forms.DataGridView statisticsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Panel screentb;
        private System.Windows.Forms.Label scr_lb;
        private System.Windows.Forms.Label mem_lb;
        private System.Windows.Forms.Label operator_lb;
        private System.Windows.Forms.Button Exppro_bt;
        private System.Windows.Forms.Button sigmax2BT;
        private System.Windows.Forms.Button AddproBT;
        private System.Windows.Forms.Button xcross;
        private System.Windows.Forms.Button sigmaxBT;
        private System.Windows.Forms.Button sigmanBT;
        private System.Windows.Forms.Button doidauproBT;
        private System.Windows.Forms.Button btdot;
        private System.Windows.Forms.Button num0;
        private System.Windows.Forms.Button clearbt;
        private System.Windows.Forms.Button mmul;
        private System.Windows.Forms.Button memrecall;
        private System.Windows.Forms.Button FEProBT;
        private System.Windows.Forms.Button madd;
        private System.Windows.Forms.Button num9;
        private System.Windows.Forms.Button mstore;
        private System.Windows.Forms.Button CAD;
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
        private System.Windows.Forms.Button x2cross;
        private System.Windows.Forms.Button sigman_1BT;


    }
}