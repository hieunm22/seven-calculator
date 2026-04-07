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
            this.exitBT = new System.Windows.Forms.Button();
            this.formBorder = new System.Windows.Forms.Panel();
            this.hotkeyLB = new System.Windows.Forms.Label();
            this.displayText = new System.Windows.Forms.Label();
            this.formBorder.SuspendLayout();
            this.SuspendLayout();
            //
            // exitBT
            //
            this.exitBT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitBT.Location = new System.Drawing.Point(63, 5);
            this.exitBT.Name = "exitBT";
            this.exitBT.Size = new System.Drawing.Size(1, 1);
            this.exitBT.TabIndex = 1;
            this.exitBT.Text = "E&xit";
            this.exitBT.UseVisualStyleBackColor = true;
            this.exitBT.Click += new System.EventHandler(this.toolTip_Click);
            //
            // formBorder
            //
            this.formBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formBorder.Controls.Add(this.hotkeyLB);
            this.formBorder.Controls.Add(this.exitBT);
            this.formBorder.Controls.Add(this.displayText);
            this.formBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formBorder.Location = new System.Drawing.Point(0, 0);
            this.formBorder.Name = "formBorder";
            this.formBorder.Size = new System.Drawing.Size(128, 28);
            this.formBorder.TabIndex = 0;
            this.formBorder.Click += new System.EventHandler(this.toolTip_Click);
            //
            // hotkeyLB
            //
            this.hotkeyLB.AutoSize = true;
            this.hotkeyLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hotkeyLB.Location = new System.Drawing.Point(96, 7);
            this.hotkeyLB.Name = "hotkeyLB";
            this.hotkeyLB.Size = new System.Drawing.Size(0, 13);
            this.hotkeyLB.TabIndex = 1;
            this.hotkeyLB.Click += new System.EventHandler(this.toolTip_Click);
            //
            // displayText
            //
            this.displayText.AutoSize = true;
            this.displayText.Location = new System.Drawing.Point(12, 7);
            this.displayText.Name = "displayText";
            this.displayText.Size = new System.Drawing.Size(88, 13);
            this.displayText.TabIndex = 0;
            this.displayText.Text = "Key equivalent is";
            this.displayText.Click += new System.EventHandler(this.toolTip_Click);
            //
            // Info
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.CancelButton = this.exitBT;
            this.ClientSize = new System.Drawing.Size(128, 28);
            this.Controls.Add(this.formBorder);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Info";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "toolTip";
            this.Activated += new System.EventHandler(this.toolTip_Activated);
            this.Deactivate += new System.EventHandler(this.toolTip_Click);
            this.Click += new System.EventHandler(this.toolTip_Click);
            this.formBorder.ResumeLayout(false);
            this.formBorder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button exitBT;
        private Panel formBorder;
        private Label hotkeyLB;
        private Label displayText;
    }
}