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
            this.pictureBox1 = new PictureBox();
            this.btnOk = new Button();
            this.contextMenu = new ContextMenu();
            this.copyEmailAddrTSMI = new MenuItem();
            this.iLabel2 = new ILabel();
            this.iLabel1 = new ILabel();
            this.emaillb = new ILabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // pictureBox1
            //
            this.pictureBox1.Image = ((Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new Point(13, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(31, 32);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            //
            // btnOk
            //
            this.btnOk.Location = new Point(146, 70);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 27);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            //
            // contextMenu
            //
            this.contextMenu.MenuItems.AddRange(new MenuItem[] {
            this.copyEmailAddrTSMI});
            //
            // copyEmailAddrTSMI
            //
            this.copyEmailAddrTSMI.Index = 0;
            this.copyEmailAddrTSMI.Text = "Copy &Email Address";
            this.copyEmailAddrTSMI.Click += new EventHandler(this.copyEmailAddrTSMI_Click);
            //
            // iLabel2
            //
            this.iLabel2.AutoSize = true;
            this.iLabel2.Location = new Point(56, 47);
            this.iLabel2.Name = "iLabel2";
            this.iLabel2.Size = new Size(38, 14);
            this.iLabel2.TabIndex = 11;
            this.iLabel2.Text = "Email:";
            //
            // iLabel1
            //
            this.iLabel1.AutoSize = true;
            this.iLabel1.Location = new Point(56, 20);
            this.iLabel1.Name = "iLabel1";
            this.iLabel1.Size = new Size(286, 14);
            this.iLabel1.TabIndex = 10;
            this.iLabel1.Text = "Seven calculator - Developed by Nguyen Minh Hieu";
            //
            // emaillb
            //
            this.emaillb.AutoSize = true;
            this.emaillb.ContextMenu = this.contextMenu;
            this.emaillb.ForeColor = Color.Blue;
            this.emaillb.Location = new Point(93, 47);
            this.emaillb.Name = "emaillb";
            this.emaillb.Size = new Size(140, 14);
            this.emaillb.TabIndex = 9;
            this.emaillb.Text = "hieunm1987@gmail.com";
            this.emaillb.Click += new EventHandler(this.emaillb_Click);
            this.emaillb.MouseDown += new MouseEventHandler(this.emaillb_MouseDown);
            this.emaillb.MouseEnter += new EventHandler(this.emaillb_MouseEnter);
            this.emaillb.MouseLeave += new EventHandler(this.emaillb_MouseLeave);
            //
            // About
            //
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new Size(367, 110);
            this.Controls.Add(this.iLabel2);
            this.Controls.Add(this.iLabel1);
            this.Controls.Add(this.emaillb);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "About Calculator";
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
        private ILabel iLabel1;
        private ILabel iLabel2;
    }
}