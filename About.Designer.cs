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
            this.label1 = new System.Windows.Forms.Label();
            this.versionlb = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.emaillb = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.memorylb = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.namelb = new System.Windows.Forms.Label();
            this.orglb = new System.Windows.Forms.Label();
            this.eulb = new System.Windows.Forms.Label();
            this.lalb = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seven calculator - Designed by Nguyen Minh Hieu";
            // 
            // versionlb
            // 
            this.versionlb.AutoSize = true;
            this.versionlb.Location = new System.Drawing.Point(56, 47);
            this.versionlb.Name = "versionlb";
            this.versionlb.Size = new System.Drawing.Size(51, 14);
            this.versionlb.TabIndex = 1;
            this.versionlb.Text = "Version ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email:";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(381, 237);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // emaillb
            // 
            this.emaillb.AutoSize = true;
            this.emaillb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.emaillb.ForeColor = System.Drawing.Color.Blue;
            this.emaillb.Location = new System.Drawing.Point(93, 73);
            this.emaillb.Name = "emaillb";
            this.emaillb.Size = new System.Drawing.Size(140, 14);
            this.emaillb.TabIndex = 4;
            this.emaillb.Text = "hieunm1987@gmail.com";
            this.emaillb.MouseLeave += new System.EventHandler(this.emaillb_MouseLeave);
            this.emaillb.Click += new System.EventHandler(this.emaillb_Click);
            this.emaillb.MouseEnter += new System.EventHandler(this.emaillb_MouseEnter);
            // 
            // label05
            // 
            this.label05.AutoSize = true;
            this.label05.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label05.Location = new System.Drawing.Point(56, 192);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(399, 14);
            this.label05.TabIndex = 0;
            this.label05.Text = "________________________________________________________";
            // 
            // memorylb
            // 
            this.memorylb.AutoSize = true;
            this.memorylb.Location = new System.Drawing.Point(56, 220);
            this.memorylb.Name = "memorylb";
            this.memorylb.Size = new System.Drawing.Size(220, 14);
            this.memorylb.TabIndex = 0;
            this.memorylb.Text = "Physical memory avalable to Windows: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(266, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "This product is licensed under the terms of the";
            // 
            // namelb
            // 
            this.namelb.AutoSize = true;
            this.namelb.Location = new System.Drawing.Point(93, 151);
            this.namelb.Name = "namelb";
            this.namelb.Size = new System.Drawing.Size(38, 14);
            this.namelb.TabIndex = 2;
            this.namelb.Text = "Name";
            // 
            // orglb
            // 
            this.orglb.AutoSize = true;
            this.orglb.Location = new System.Drawing.Point(93, 177);
            this.orglb.Name = "orglb";
            this.orglb.Size = new System.Drawing.Size(50, 14);
            this.orglb.TabIndex = 2;
            this.orglb.Text = "Oganize";
            // 
            // eulb
            // 
            this.eulb.AutoSize = true;
            this.eulb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.eulb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eulb.ForeColor = System.Drawing.Color.Blue;
            this.eulb.Location = new System.Drawing.Point(320, 99);
            this.eulb.Name = "eulb";
            this.eulb.Size = new System.Drawing.Size(56, 14);
            this.eulb.TabIndex = 5;
            this.eulb.Text = "End-User";
            this.eulb.Click += new System.EventHandler(this.EULA_Click);
            this.eulb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EULA_MouseDown);
            // 
            // lalb
            // 
            this.lalb.AutoSize = true;
            this.lalb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lalb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalb.ForeColor = System.Drawing.Color.Blue;
            this.lalb.Location = new System.Drawing.Point(56, 125);
            this.lalb.Name = "lalb";
            this.lalb.Size = new System.Drawing.Size(110, 14);
            this.lalb.TabIndex = 6;
            this.lalb.Text = "License Agrrement";
            this.lalb.Click += new System.EventHandler(this.EULA_Click);
            this.lalb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EULA_MouseDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(163, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "to:";
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
            // About
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(472, 274);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lalb);
            this.Controls.Add(this.eulb);
            this.Controls.Add(this.emaillb);
            this.Controls.Add(this.orglb);
            this.Controls.Add(this.namelb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.versionlb);
            this.Controls.Add(this.memorylb);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label1);
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
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void initMenu()
        {
            this.contextMenuStrip1 = new ContextMenu();
            this.copyEmailAddrTSMI = new MenuItem("Copy &Email Address");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyEmailAddrTSMI});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // copyEmailAddrTSMI
            // 
            this.copyEmailAddrTSMI.Name = "copyEmailAddrTSMI";
            this.copyEmailAddrTSMI.Click += new System.EventHandler(this.copyEmailAddrTSMI_Click);

            this.emaillb.ContextMenu = this.contextMenuStrip1;
        }

        #region register components
        private Label label1;
        private Label versionlb;
        private Label label3;
        private Button btnOk;
        private Label emaillb;
        private Label label05;
        private Label memorylb;
        private Label label5;
        private Label namelb;
        private Label orglb;
        private Label eulb;
        private Label lalb;
        private Label label7;
        private PictureBox pictureBox1;
        private ContextMenu contextMenuStrip1;
        private MenuItem copyEmailAddrTSMI; 
        #endregion
    }
}