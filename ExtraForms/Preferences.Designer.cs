namespace Calculator
{
    partial class Preferences
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
            this.spdLB = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fastFactCB = new System.Windows.Forms.CheckBox();
            this.collapsedSpdNUD = new Calculator.INumericUpDown();
            this.usedSignChkB = new System.Windows.Forms.CheckBox();
            this.animateCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.collapsedSpdNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // spdLB
            // 
            this.spdLB.AutoSize = true;
            this.spdLB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spdLB.Location = new System.Drawing.Point(21, 14);
            this.spdLB.Name = "spdLB";
            this.spdLB.Size = new System.Drawing.Size(101, 13);
            this.spdLB.TabIndex = 0;
            this.spdLB.Text = "Collapsed speed";
            this.spdLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(78, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(198, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fastFactCB
            // 
            this.fastFactCB.AutoSize = true;
            this.fastFactCB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastFactCB.Location = new System.Drawing.Point(24, 69);
            this.fastFactCB.Name = "fastFactCB";
            this.fastFactCB.Size = new System.Drawing.Size(99, 17);
            this.fastFactCB.TabIndex = 3;
            this.fastFactCB.Text = "&Fast factorial";
            this.fastFactCB.UseVisualStyleBackColor = true;
            this.fastFactCB.CheckedChanged += new System.EventHandler(this.fastFactCB_CheckedChanged);
            // 
            // collapsedSpdNUD
            // 
            this.collapsedSpdNUD.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collapsedSpdNUD.Location = new System.Drawing.Point(146, 12);
            this.collapsedSpdNUD.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.collapsedSpdNUD.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.collapsedSpdNUD.Name = "collapsedSpdNUD";
            this.collapsedSpdNUD.Size = new System.Drawing.Size(78, 21);
            this.collapsedSpdNUD.TabIndex = 1;
            this.collapsedSpdNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.collapsedSpdNUD.ValueChanged += new System.EventHandler(this.collapsedSpdNUD_ValueChanged);
            // 
            // usedSignChkB
            // 
            this.usedSignChkB.AutoSize = true;
            this.usedSignChkB.Checked = true;
            this.usedSignChkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usedSignChkB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedSignChkB.Location = new System.Drawing.Point(24, 92);
            this.usedSignChkB.Name = "usedSignChkB";
            this.usedSignChkB.Size = new System.Drawing.Size(287, 17);
            this.usedSignChkB.TabIndex = 4;
            this.usedSignChkB.Text = "U&se signed-integer calculation in programmer";
            this.usedSignChkB.UseVisualStyleBackColor = true;
            this.usedSignChkB.CheckedChanged += new System.EventHandler(this.usedSignChkB_CheckedChanged);
            // 
            // animateCB
            // 
            this.animateCB.AutoSize = true;
            this.animateCB.Checked = true;
            this.animateCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animateCB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.animateCB.Location = new System.Drawing.Point(24, 46);
            this.animateCB.Name = "animateCB";
            this.animateCB.Size = new System.Drawing.Size(132, 17);
            this.animateCB.TabIndex = 2;
            this.animateCB.Text = "Animate &resization";
            this.animateCB.UseVisualStyleBackColor = true;
            this.animateCB.CheckedChanged += new System.EventHandler(this.animateCB_CheckedChanged);
            // 
            // Preferences
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(362, 157);
            this.Controls.Add(this.animateCB);
            this.Controls.Add(this.usedSignChkB);
            this.Controls.Add(this.collapsedSpdNUD);
            this.Controls.Add(this.fastFactCB);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.spdLB);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            ((System.ComponentModel.ISupportInitialize)(this.collapsedSpdNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label spdLB;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox fastFactCB;
        private INumericUpDown collapsedSpdNUD;
        private System.Windows.Forms.CheckBox usedSignChkB;
        private System.Windows.Forms.CheckBox animateCB;
    }
}