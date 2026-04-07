using System.Windows.Forms;

namespace Calculator
{
    partial class Info
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
            this.borderPN = new System.Windows.Forms.Panel();
            this.displayText = new System.Windows.Forms.TextBox();
            this.borderPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPN
            // 
            this.borderPN.BackColor = System.Drawing.Color.Transparent;
            this.borderPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.borderPN.Controls.Add(this.displayText);
            this.borderPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPN.Location = new System.Drawing.Point(0, 0);
            this.borderPN.Name = "borderPN";
            this.borderPN.Size = new System.Drawing.Size(324, 100);
            this.borderPN.TabIndex = 0;
            // 
            // displayText
            // 
            this.displayText.BackColor = System.Drawing.SystemColors.Info;
            this.displayText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.displayText.Location = new System.Drawing.Point(5, 5);
            this.displayText.Multiline = true;
            this.displayText.Name = "displayText";
            this.displayText.ReadOnly = true;
            this.displayText.Size = new System.Drawing.Size(312, 88);
            this.displayText.TabIndex = 0;
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(324, 100);
            this.Controls.Add(this.borderPN);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Info";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "toolTip";
            this.Activated += new System.EventHandler(this.toolTip_Activated);
            this.Deactivate += new System.EventHandler(this.toolTip_Click);
            this.Click += new System.EventHandler(this.toolTip_Click);
            this.borderPN.ResumeLayout(false);
            this.borderPN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel borderPN;
        private TextBox displayText;


    }
}