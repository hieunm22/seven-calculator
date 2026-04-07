using System.Drawing;
using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ckbFastFact = new System.Windows.Forms.CheckBox();
            this.ckbUsedSign = new System.Windows.Forms.CheckBox();
            this.ckbReadDict = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckbStoreHistory = new System.Windows.Forms.CheckBox();
            this.cbbInputMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.nudCollapsedSpd = new Calculator.INumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudCollapsedSpd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Collapsed speed";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(40, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 27);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(135, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 27);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckbFastFact
            // 
            this.ckbFastFact.AutoSize = true;
            this.ckbFastFact.Location = new System.Drawing.Point(24, 46);
            this.ckbFastFact.Name = "ckbFastFact";
            this.ckbFastFact.Size = new System.Drawing.Size(93, 19);
            this.ckbFastFact.TabIndex = 3;
            this.ckbFastFact.Text = "&Fast factorial";
            this.toolTip1.SetToolTip(this.ckbFastFact, "Using an approximate method to calculate a big number, \r\nso that accuracy of the " +
        "result is less than normal method");
            this.ckbFastFact.UseVisualStyleBackColor = true;
            this.ckbFastFact.CheckedChanged += new System.EventHandler(this.ckbFastFact_CheckedChanged);
            // 
            // ckbUsedSign
            // 
            this.ckbUsedSign.AutoSize = true;
            this.ckbUsedSign.Checked = true;
            this.ckbUsedSign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUsedSign.Location = new System.Drawing.Point(24, 68);
            this.ckbUsedSign.Name = "ckbUsedSign";
            this.ckbUsedSign.Size = new System.Drawing.Size(269, 19);
            this.ckbUsedSign.TabIndex = 4;
            this.ckbUsedSign.Text = "U&se signed-integer calculation in programmer";
            this.toolTip1.SetToolTip(this.ckbUsedSign, resources.GetString("ckbUsedSign.ToolTip"));
            this.ckbUsedSign.UseVisualStyleBackColor = true;
            this.ckbUsedSign.CheckedChanged += new System.EventHandler(this.ckbUsedSign_CheckedChanged);
            // 
            // ckbReadDict
            // 
            this.ckbReadDict.AutoSize = true;
            this.ckbReadDict.Checked = true;
            this.ckbReadDict.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbReadDict.Location = new System.Drawing.Point(24, 90);
            this.ckbReadDict.Name = "ckbReadDict";
            this.ckbReadDict.Size = new System.Drawing.Size(267, 19);
            this.ckbReadDict.TabIndex = 5;
            this.ckbReadDict.Text = "R&ead and write factorial result from dictionary";
            this.toolTip1.SetToolTip(this.ckbReadDict, resources.GetString("ckbReadDict.ToolTip"));
            this.ckbReadDict.UseVisualStyleBackColor = true;
            this.ckbReadDict.CheckedChanged += new System.EventHandler(this.readDictChkB_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.ReshowDelay = 500;
            // 
            // ckbStoreHistory
            // 
            this.ckbStoreHistory.AutoSize = true;
            this.ckbStoreHistory.Location = new System.Drawing.Point(24, 112);
            this.ckbStoreHistory.Name = "ckbStoreHistory";
            this.ckbStoreHistory.Size = new System.Drawing.Size(155, 19);
            this.ckbStoreHistory.TabIndex = 13;
            this.ckbStoreHistory.Text = "S&tore history expressions";
            this.toolTip1.SetToolTip(this.ckbStoreHistory, "Store the calculation history and reload \r\nit on the next application start");
            this.ckbStoreHistory.UseVisualStyleBackColor = true;
            this.ckbStoreHistory.CheckedChanged += new System.EventHandler(this.ckbStoreHistory_CheckedChanged);
            // 
            // cbbInputMethod
            // 
            this.cbbInputMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInputMethod.FormattingEnabled = true;
            this.cbbInputMethod.Items.AddRange(new object[] {
            "Control-Enter                    ",
            "Enter                                   "});
            this.cbbInputMethod.Location = new System.Drawing.Point(159, 138);
            this.cbbInputMethod.Name = "cbbInputMethod";
            this.cbbInputMethod.Size = new System.Drawing.Size(159, 23);
            this.cbbInputMethod.TabIndex = 7;
            this.cbbInputMethod.SelectedIndexChanged += new System.EventHandler(this.countMethodCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter frequence using";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(230, 178);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(72, 27);
            this.btnDefault.TabIndex = 12;
            this.btnDefault.Text = "&Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // nudCollapsedSpd
            // 
            this.nudCollapsedSpd.Location = new System.Drawing.Point(146, 14);
            this.nudCollapsedSpd.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudCollapsedSpd.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudCollapsedSpd.Name = "nudCollapsedSpd";
            this.nudCollapsedSpd.Size = new System.Drawing.Size(78, 23);
            this.nudCollapsedSpd.TabIndex = 1;
            this.nudCollapsedSpd.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCollapsedSpd.ValueChanged += new System.EventHandler(this.nudCollapsedSpd_ValueChanged);
            // 
            // Preferences
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(343, 226);
            this.Controls.Add(this.ckbStoreHistory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbInputMethod);
            this.Controls.Add(this.ckbReadDict);
            this.Controls.Add(this.ckbUsedSign);
            this.Controls.Add(this.nudCollapsedSpd);
            this.Controls.Add(this.ckbFastFact);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Name = "Preferences";
            this.ShowIcon = false;
            this.Text = "Preferences";
            ((System.ComponentModel.ISupportInitialize)(this.nudCollapsedSpd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button btnOK;
        private Button btnCancel;
        private CheckBox ckbFastFact;
        private INumericUpDown nudCollapsedSpd;
        private CheckBox ckbUsedSign;
        private CheckBox ckbReadDict;
        private ToolTip toolTip1;
        private ComboBox cbbInputMethod;
        private Label label2;
        private Button btnDefault;
        private CheckBox ckbStoreHistory;
    }
}