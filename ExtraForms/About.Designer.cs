using System;
using System.Drawing;
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
            this.aboutLb = new Calculator.ILabel();
            this.emaillb = new Calculator.ILabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(149, 75);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 29);
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
            this.iLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iLabel2.Location = new System.Drawing.Point(61, 51);
            this.iLabel2.Name = "iLabel2";
            this.iLabel2.Size = new System.Drawing.Size(43, 13);
            this.iLabel2.TabIndex = 11;
            this.iLabel2.Text = "Email:";
            this.iLabel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // aboutLb
            // 
            this.aboutLb.AutoSize = true;
            this.aboutLb.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutLb.Location = new System.Drawing.Point(61, 22);
            this.aboutLb.Name = "aboutLb";
            this.aboutLb.Size = new System.Drawing.Size(329, 13);
            this.aboutLb.TabIndex = 10;
            this.aboutLb.Text = "Seven calculator v4.0 - Developed by Nguyen Minh Hieu";
            this.aboutLb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // emaillb
            // 
            this.emaillb.AutoSize = true;
            this.emaillb.ContextMenu = this.contextMenu;
            this.emaillb.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emaillb.ForeColor = System.Drawing.Color.Blue;
            this.emaillb.Location = new System.Drawing.Point(100, 51);
            this.emaillb.Name = "emaillb";
            this.emaillb.Size = new System.Drawing.Size(146, 13);
            this.emaillb.TabIndex = 9;
            this.emaillb.Text = "hieunm1987@gmail.com";
            this.emaillb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.emaillb_MouseClick);
            this.emaillb.MouseEnter += new System.EventHandler(this.emaillb_MouseEnter);
            this.emaillb.MouseLeave += new System.EventHandler(this.emaillb_MouseLeave);
            // 
            // About
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(397, 118);
            this.Controls.Add(this.iLabel2);
            this.Controls.Add(this.aboutLb);
            this.Controls.Add(this.emaillb);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "About";
            this.ShowIcon = false;
            this.Text = "About Calculator";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ILabel emaillb;
        private PictureBox pictureBox1;
        private ContextMenu contextMenu;
        private MenuItem copyEmailAddrTSMI;
        private Button btnOk;
        private ILabel aboutLb;
        private ILabel iLabel2;
    }
}