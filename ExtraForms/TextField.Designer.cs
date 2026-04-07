namespace Calculator
{
    partial class TextField
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl = new System.Windows.Forms.Label();
            this.txtField = new Calculator.ITextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(3, 4);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(38, 13);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "label1";
            this.lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDown);
            // 
            // txtField
            // 
            this.txtField.AllowTextChanged = true;
            this.txtField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtField.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtField.Location = new System.Drawing.Point(165, 0);
            this.txtField.MaxLength = 20;
            this.txtField.Name = "txtField";
            this.txtField.NumberModeOnly = true;
            this.txtField.Size = new System.Drawing.Size(168, 23);
            this.txtField.TabIndex = 3;
            this.txtField.Text = "Enter value";
            this.txtField.TextChanged += new System.EventHandler(this.txtField_TextChanged);
            this.txtField.GotFocus += new System.EventHandler(this.txtField_GotFocus);
            this.txtField.LostFocus += new System.EventHandler(this.txtField_LostFocus);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1, 1);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TextField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtField);
            this.Controls.Add(this.lbl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TextField";
            this.Size = new System.Drawing.Size(337, 23);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ITextBox txtField;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button button1;
    }
}
