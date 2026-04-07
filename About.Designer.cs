using System;
using System.Windows.Forms;

namespace Calculator
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.copyEmailAddrTSMI = new System.Windows.Forms.MenuItem();
            this.iLabel2 = new Calculator.ILabel();
            this.iLabel1 = new Calculator.ILabel();
            this.emaillb = new Calculator.ILabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(146, 70);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyEmailAddrTSMI});
            // 
            // copyEmailAddrTSMI
            // 
            this.copyEmailAddrTSMI.Index = 0;
            this.copyEmailAddrTSMI.Text = "Copy &Email Address";
            this.copyEmailAddrTSMI.Click += new System.EventHandler(this.copyEmailAddrTSMI_Click);
            // 
            // iLabel2
            // 
            this.iLabel2.AutoSize = true;
            this.iLabel2.Location = new System.Drawing.Point(56, 47);
            this.iLabel2.Name = "iLabel2";
            this.iLabel2.Size = new System.Drawing.Size(38, 14);
            this.iLabel2.TabIndex = 11;
            this.iLabel2.Text = "Email:";
            // 
            // iLabel1
            // 
            this.iLabel1.AutoSize = true;
            this.iLabel1.Location = new System.Drawing.Point(56, 20);
            this.iLabel1.Name = "iLabel1";
            this.iLabel1.Size = new System.Drawing.Size(286, 14);
            this.iLabel1.TabIndex = 10;
            this.iLabel1.Text = "Seven calculator - Developed by Nguyen Minh Hieu";
            // 
            // emaillb
            // 
            this.emaillb.AutoSize = true;
            this.emaillb.ContextMenu = this.contextMenu;
            this.emaillb.ForeColor = System.Drawing.Color.Blue;
            this.emaillb.Location = new System.Drawing.Point(93, 47);
            this.emaillb.Name = "emaillb";
            this.emaillb.Size = new System.Drawing.Size(140, 14);
            this.emaillb.TabIndex = 9;
            this.emaillb.Text = "hieunm1987@gmail.com";
            this.emaillb.Click += new System.EventHandler(this.emaillb_Click);
            this.emaillb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.emaillb_MouseDown);
            this.emaillb.MouseEnter += new System.EventHandler(this.emaillb_MouseEnter);
            this.emaillb.MouseLeave += new System.EventHandler(this.emaillb_MouseLeave);
            // 
            // About
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(367, 110);
            this.Controls.Add(this.iLabel2);
            this.Controls.Add(this.iLabel1);
            this.Controls.Add(this.emaillb);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region register components

        private ILabel emaillb;
        private PictureBox pictureBox1;
        private ContextMenu contextMenu;
        private MenuItem copyEmailAddrTSMI; 
        private Button btnOk;
        #endregion
        private ILabel iLabel1;
        private ILabel iLabel2;
    }
}