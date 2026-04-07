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
            this.borderPN = new Calculator.IPanel();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.borderPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPN
            // 
            this.borderPN.Controls.Add(this.rtbInfo);
            this.borderPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPN.Location = new System.Drawing.Point(0, 0);
            this.borderPN.Name = "borderPN";
            this.borderPN.Size = new System.Drawing.Size(324, 100);
            this.borderPN.TabIndex = 0;
            // 
            // rtbInfo
            // 
            this.rtbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbInfo.Location = new System.Drawing.Point(6, 6);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbInfo.Size = new System.Drawing.Size(312, 88);
            this.rtbInfo.TabIndex = 2;
            this.rtbInfo.Text = "";
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(324, 100);
            this.Controls.Add(this.borderPN);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Info";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "toolTip";
            this.Deactivate += new System.EventHandler(this.toolTip_Click);
            this.Click += new System.EventHandler(this.toolTip_Click);
            this.borderPN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private IPanel borderPN;
        private RichTextBox rtbInfo;


    }
}