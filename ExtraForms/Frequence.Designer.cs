namespace Calculator
{
    partial class Frequence
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
            this.frqTB = new Calculator.ITextBox();
            this.SuspendLayout();
            // 
            // frqTB
            // 
            this.frqTB.AllowTextChanged = true;
            this.frqTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frqTB.Location = new System.Drawing.Point(0, 0);
            this.frqTB.Name = "frqTB";
            this.frqTB.NumberModeOnly = true;
            this.frqTB.Size = new System.Drawing.Size(284, 22);
            this.frqTB.TabIndex = 0;
            this.frqTB.Text = "1";
            this.frqTB.WatermarkText = "Enter frequence of this number";
            // 
            // Frequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 19);
            this.Controls.Add(this.frqTB);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frequence";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frequence";
            this.Load += new System.EventHandler(this.Frequence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ITextBox frqTB;



    }
}