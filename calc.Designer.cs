using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator
{
    partial class calc : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #region constants, readonly
        /// <summary>
        /// số lần mở ngoặc tối đa
        /// </summary>
        private const int MAX_BRACKET_LEVEL = 25;
        /// <summary>
        /// list options name
        /// </summary>
        private readonly string[] optionName = new string[]{

            #region init item members
            "CalculatorType",
            "UseSep",
            "ExtraFunction",
            "ShowHistory",
            "MemoryNumber",
            "ShowPreview",
            // --------------------------
            "TypeUnit",
            "AutoCalculate",
            "DateMethod",
            // --------------------------
            "CollapseSpeed",
            "FastFactorial",
            "SignInteger",
            "ReadDictionary",
            "StoreHistory",
            "FastInput",
            "CountMethod",
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        private readonly string[][] unitTypeItemMember = new string[][]{

            #region init item members
            new string[]{
            "Degree",
            "Gradian",
            "Radian"},
            new string[]{
            "Acres",
            "Hectares",
            "Square centimeter",
            "Square feet",
            "Square inch",
            "Square kilometer",
            "Square meters",
            "Square mile",
            "Square millimeter",
            "Square yard"},
            new string[]{
            "Bit (b)",
            "Byte (B)",
            "Exabit (Eb)",
            "Exabyte (EB)",
            "Gigabit (Gb)",
            "Gigabyte (GB)",
            "Kilobit (Kb)",
            "Kilobyte (KB)",
            "Megabit (Mb)",
            "Megabyte (MB)",
            "Nibble",
            "Petabit (Pb)",
            "Petabyte (PB)",
            "Terabit (Tb)",
            "Terabyte (TB)",
            "Yottabit (Yb)",
            "Yottabyte (YB)",
            "Zettabit (Zb)",
            "Zettabyte (ZB)",
            },
            new string[]{
            "British Thermal Unit",
            "Calorie",
            "Electron-volts",
            "Foot-pound",
            "Joule",
            "Kilocalorie",
            "Kilojoule"},
            new string[]{
            "Angstrom",
            "Centimeters",
            "Chain",
            "Fathom",
            "Feet",
            "Hand",
            "Inch",
            "Kilometers",
            "Light year",
            "Link",
            "Meter",
            "Microns",
            "Mile",
            "Millimeters",
            "Nanometers",
            "Nautical miles",
            "PICA",
            "Rods",
            "Span",
            "Yard"},
            new string[]{
            "BTU/minute",
            "Foot-pound/minute",
            "Horsepower",
            "Kilowatt",
            "Watt"},
            new string[]{
            "Atmosphere",
            "Bar",
            "Kilo Pascal",
            "Millimeter of mercury",
            "Pascal",
            "Pound per square inch (PSI)"},
            new string[]{
            "Degrees Celsius",
            "Degrees Fahrenheit",
            "Kelvin"},
            new string[]{
            "Day",
            "Hour",
            "Microsecond",
            "Milisecond",
            "Minute",
            "Second",
            "Week"},
            new string[]{
            "Centimeter per second",
            "Feet per second",
            "Kilometer per hour",
            "Knots",
            "Mach (at std.atm)",
            "Meter per second",
            "Miles per hour (mph)"},
            new string[]{
            "Cubic centimeter",
            "Cubic feet",
            "Cubic inch",
            "Cubic meter",
            "Cubic yard",
            "Fluid ounce (UK)",
            "Fluid ounce (US)",
            "Gallon (UK)",
            "Gallon (US)",
            "Liter",
            "Pint (UK)",
            "Pint (US)",
            "Quart (UK)",
            "Quart (US)"},
            new string[]{
            "Carat",
            "Centigram",
            "Decigram",
            "Dekagram",
            "Gram",
            "Hectogram",
            "Kilogram",
            "Long ton",
            "Milligram",
            "Ounce",
            "Pound",
            "Short ton",
            "Stone",
            "Tonne"}
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        private readonly string[][] worksheetsLabel = new string[][]{

            #region init item members
            new string[]{
            "Purchase price",
            "Term (years)",
            "Interest rate (%)",
            "Monthly payment",
            "Down payment"},
            new string[]{
            "Lease value",
            "Payments per year",
            "Residual value",
            "Interest rate (%)",
            "Periodic payment",
            "Lease period"},
            new string[]{
            "Fuel used (gallons)",
            "Distance (miles)",
            "Fuel economy (mpg)"},
            new string[]{
            "Fuel used (liters)",
            "Distance (kilometers)",
            "Fuel economy (L/100 km)"}
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        private readonly string[][] worksheetsComboBoxItems = new string[][]{

            #region init item members
            new string[]{
            "Down Payment",
            "Monthly Payment",
            "Purchase price",
            "Term (years)"},
            new string[]{
            "Lease period",
            "Lease value",
            "Periodic payment",
            "Residual value"},
            new string[]{
            "Distance (miles)",
            "Fuel economy (mpg)",
            "Fuel used (gallons)"},
            new string[]{
            "Distance (kilometers)",
            "Fuel economy (L/100 km)",
            "Fuel used (liters)"}
            #endregion

        };
        /// <summary>
        /// độ rộng form standard/statistic không có extra function = 220
        /// </summary>
        private const int APP_W1 = 220;
        /// <summary>
        /// độ rộng form scientific/programmer không có extra function = 413
        /// </summary>
        private const int APP_W2 = 413;
        /// <summary>
        /// độ rộng form standard/statistic có extra function = 591
        /// </summary>
        private const int APP_W3 = 591;
        /// <summary>
        /// độ rộng form scientific/programmer có extra function = 784
        /// </summary>
        private const int APP_W4 = 784;

        /// <summary>
        /// chiều cao form standard/scientific không có history = 310
        /// </summary>
        private const int APP_H1 = 310;
        /// <summary>
        /// chiều cao form programmer không có preview = 374
        /// </summary>
        private const int APP_H2 = 374;
        /// <summary>
        /// chiều cao form standard/scientific có history = 414
        /// </summary>
        private const int APP_H3 = 414;
        /// <summary>
        /// chiều cao form programmer có preview = 455
        /// </summary>
        private const int APP_H4 = 455;

        #endregion

        #region ham main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new calc());
        }
        #endregion

        #region override method
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
        /// <summary>
        /// _Progress a command key
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // control W
            if (keyData == (Keys.Control | Keys.W)) this.Close(); //131159
            // control c hoac control v khi dang edit textbox
            if ((keyData == (Keys.Control | Keys.C) 
                || keyData == (Keys.Control | Keys.V) 
                || keyData == (Keys.Shift | Keys.Insert)) && !prcmdkey)  // 131139 131158
            {
                return false;
            }

            if (keyData == Keys.Escape && prcmdkey)    // esc
            {
                //pex = null;
                if (hisDGV.IsCurrentCellInEditMode) cancelEditHisMI_Click(cancelEditDSMI, null);
                else if (staDGV.IsCurrentCellInEditMode) cancelEditDSMI_Click(cancelEditDSMI, null);
                else clear_num(true);
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (prcmdkey/* && pex == null*/)
            {
                //if (key_hc != 65585) co = null;
                switch (keyData)
                {
                    #region xu ly phim nhap vao
                    //cac phim so tu 0 den 9
                    case Keys.NumPad0: case Keys.D0:
                        if (programmerMI.Checked) numinput_pro(num0BT);
                        else numinput(num0BT);
                        break;
                    case Keys.NumPad1: case Keys.D1:
                        if (programmerMI.Checked) numinput_pro(num1BT);
                        else numinput(num1BT);
                        break;
                    case Keys.NumPad2: case Keys.D2:
                        if (programmerMI.Checked) numinput_pro(num2BT);
                        else numinput(num2BT);
                        break;
                    case Keys.NumPad3: case Keys.D3:
                        if (programmerMI.Checked) numinput_pro(num3BT);
                        else numinput(num3BT);
                        break;
                    case Keys.NumPad4: case Keys.D4:
                        if (programmerMI.Checked) numinput_pro(num4BT);
                        else numinput(num4BT);
                        break;
                    case Keys.NumPad5: case Keys.D5:
                        if (programmerMI.Checked) numinput_pro(num5BT);
                        else numinput(num5BT);
                        break;
                    case Keys.NumPad6: case Keys.D6:
                        if (programmerMI.Checked) numinput_pro(num6BT);
                        else numinput(num6BT);
                        break;
                    case Keys.NumPad7: case Keys.D7:
                        if (programmerMI.Checked) numinput_pro(num7BT);
                        else numinput(num7BT);
                        break;
                    case Keys.NumPad8: case Keys.D8:
                        if (programmerMI.Checked) numinput_pro(num8BT);
                        else numinput(num8BT);
                        break;
                    case Keys.NumPad9: case Keys.D9:
                        if (programmerMI.Checked) numinput_pro(num9BT);
                        else numinput(num9BT);
                        break;
                    case Keys.Enter:    // enter
                        if (statisticsMI.Checked) AddstaBT_Click(AddstaBT, null);
                        else equal_Click(equalBT, null);
                        // return true de tranh nham voi phim AcceptButton hien tai o tren form
                        return true;
                    //case (int)(Keys.Control | Keys.Return):
                    //    if (statisticsMI.Checked) AddstaBT_Click(AddstaBT, null);
                    //    break;
                    case Keys.Up:   // up
                        ProcessUpOrDown(1, true);
                        break;
                    case Keys.Down:   // down
                        ProcessUpOrDown(-1, true);
                        break;
                    case Keys.Oemplus:   // =
                        equal_Click(equalBT, null);
                        break;
                    case Keys.Decimal: case Keys.OemPeriod:   // .
                        if (!programmerMI.Checked) numinput(btdot);
                        break;
                    case Keys.Add: case Keys.Shift | Keys.Oemplus:   // + 65723
                        if (addbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(12);
                            if (scientificMI.Checked) sci_operation(12);
                            if (programmerMI.Checked) pro_operation(12);
                        }
                        break;
                    case Keys.Subtract: case Keys.OemMinus:   // -
                        if (minusbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(13);
                            if (scientificMI.Checked) sci_operation(13);
                            if (programmerMI.Checked) pro_operation(13);
                        }
                        break;
                    case Keys.Multiply: case Keys.Shift | Keys.D8:   // * 65592
                        if (mulbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(14);
                            if (scientificMI.Checked) sci_operation(14);
                            if (programmerMI.Checked) pro_operation(14);
                        }
                        break;
                    case Keys.Divide: case Keys.Oem2:   // /
                        if (mulbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(15);
                            if (scientificMI.Checked) sci_operation(15);
                            if (programmerMI.Checked) pro_operation(15);
                        }
                        break;
                    case Keys.Delete:        // del
                        if (!statisticsMI.Checked)
                        {
                            ce_Click(ce, null);
                        }
                        else if (staDGV.CurrentCell != null)
                        {
                            string curFrq = (string)staDGV[0, staDGV.CurrentCell.RowIndex].Value;
                            curFrq = curFrq.Substring(curFrq.IndexOf(" "));
                            total -= double.Parse(curFrq);
                            staDGV.Rows.RemoveAt(staDGV.CurrentCell.RowIndex);
                            DisplayCountNumber();
                        }
                        break;
                    case Keys.F2:       // F2
                        if (hisDGV.Visible && hisDGV.RowCount > 0/* && !standardMI.Checked*/)
                        {
                            hisDGV.ReadOnly = false;
                            hisDGV.BeginEdit(false);
                            //dgv_OnBeginEdit(hisDGV);
                        }
                        if (programmerMI.Checked) dwordRB.Checked = true;
                        if (staDGV.Visible && staDGV.RowCount > 0)
                        {
                            staDGV.ReadOnly = false;
                            staDGV.BeginEdit(false);
                            //dgv_OnBeginEdit(staDGV);
                        }
                        return true;
                    case Keys.F3:       // F3
                        if (scientificMI.Checked) degRB.Checked = true;
                        if (programmerMI.Checked) _wordRB.Checked = true;
                        break;
                    case Keys.F4:       // F4
                        if (scientificMI.Checked) radRB.Checked = true;
                        if (programmerMI.Checked) _byteRB.Checked = true;
                        break;
                    case Keys.F5:       // F5
                        if (scientificMI.Checked) graRB.Checked = true;
                        if (programmerMI.Checked) hexRB.Checked = true;
                        break;
                    case Keys.F6:       // F6
                        if (programmerMI.Checked) decRB.Checked = true;
                        break;
                    case Keys.F7:       // F7
                        if (programmerMI.Checked) octRB.Checked = true;
                        break;
                    case Keys.F8:       // F8
                        if (programmerMI.Checked) binRB.Checked = true;
                        break;
                    case Keys.F9:       // F9
                        if (programmerMI.Checked) numinput_pro(changesignBT);
                        else
                        {
                            if (!statisticsMI.Checked)
                            {
                                if (confirmNumber) MathFunction(changesignBT);
                                else numinput(changesignBT);
                            }
                            else
                            {
                                if (screenStr.StartsWith("-")) screenStr = new StringBuilder(screenStr.ToString(1, screenStr.Length - 1));
                                else screenStr = new StringBuilder("-").Append(screenStr);
                                DisplayToScreen();
                            }
                        }
                        break;
                    case Keys.F12:       // F12
                        if (programmerMI.Checked) qwordRB.Checked = true;
                        break;
                    case Keys.OemSemicolon:       // ; int()
                        if (scientificMI.Checked) MathFunction(int_bt);
                        break;
                    // cac phim A den F
                    case Keys.A:        // A
                        if (btnA.Enabled && programmerMI.Checked) buttonAF(btnA.Text);
                        if (statisticsMI.Checked) xcross_Click(xcross, null);
                        break;
                    case Keys.B:        // B
                        if (btnB.Enabled && programmerMI.Checked) buttonAF(btnB.Text);
                        break;
                    case Keys.C:        // C
                        if (btnC.Enabled && programmerMI.Checked) buttonAF(btnC.Text);
                        break;
                    case Keys.D:        // D
                        if (btnD.Enabled && programmerMI.Checked) buttonAF(btnD.Text);
                        if (scientificMI.Checked) operatorBT_Click(modsciBT, null);
                        if (statisticsMI.Checked) CAD_Click(CAD, null);
                        break;
                    case Keys.E:        // E
                        if (programmerMI.Checked && btnE.Enabled) buttonAF(btnE.Text);
                        if (scientificMI.Checked || statisticsMI.Checked) exp_bt_Click(exp_bt, null); // nut exp
                        break;
                    case Keys.F:        // F
                        if (btnF.Enabled && programmerMI.Checked) buttonAF(btnF.Text);
                        break;
                    case Keys.I:       // I Inv
                        if (scientificMI.Checked) inv_ChkBox.Checked = !inv_ChkBox.Checked;
                        break;
                    case Keys.J:       // J RoL
                        if (programmerMI.Checked) rotateBT_Click(RoLBT, null);
                        break;
                    case Keys.K:       // K RoR
                        if (programmerMI.Checked) rotateBT_Click(RoRBT, null);
                        break;
                    case Keys.L:       // L log
                        if (scientificMI.Checked) MathFunction(log_bt); // 10^x
                        break;
                    case Keys.M:       // M dms
                        if (scientificMI.Checked) MathFunction(dms_bt);
                        break;
                    case Keys.N:        // N ln()
                        if (scientificMI.Checked) MathFunction(ln_bt);
                        break;
                    case Keys.O:       // O cos
                        if (scientificMI.Checked) MathFunction(cos_bt);
                        break;
                    case Keys.P:       // P pi
                        if (scientificMI.Checked) pi_bt_Click(pi_bt, null);
                        break;
                    case Keys.Q:       // Q x²
                        if (scientificMI.Checked) MathFunction(x2_bt);
                        break;
                    case Keys.R:       // R 1/x
                        if (standardMI.Checked || scientificMI.Checked) MathFunction(invert_bt);
                        break;
                    case Keys.S:       // S sin / ∑n
                        if (scientificMI.Checked) MathFunction(sin_bt);
                        if (statisticsMI.Checked) sigmaxBT_Click(sigmaxBT, null);
                        break;
                    case Keys.T:       // T tan / σn
                        if (scientificMI.Checked) MathFunction(tan_bt);
                        if (statisticsMI.Checked) sigmanBT_Click(sigmanBT, null);
                        break;
                    case Keys.V:       // V
                        if (scientificMI.Checked) fe_ChkBox.Checked = !fe_ChkBox.Checked;
                        break;
                    case Keys.X:       // X
                        if (scientificMI.Checked || statisticsMI.Checked) exp_bt_Click(exp_bt, null); // nut exp
                        break;
                    case Keys.Y:       // Y
                        if (scientificMI.Checked) sci_operation(33);
                        break;
                    case Keys.Back:       // Backspace
                        backspacebt_Click(backspacebt, null);
                        break;
                    case Keys.Shift | Keys.D9: case Keys.OemOpenBrackets:    // ( 65593
                        open_bracket_Click(open_bracket, null);
                        break;
                    case Keys.Shift | Keys.D0: case Keys.OemCloseBrackets:    // ) 65584
                        close_bracket_Click(close_bracket, null);
                        break;
                    case Keys.Shift | Keys.D5:     // % 65589
                        if (percent_bt.Enabled) percent_bt_Click(percent_bt, null);
                        if (programmerMI.Checked) operatorBT_Click(modproBT, null);
                        break;
                    case Keys.Shift | Keys.Oem3:     // ~ not 65728
                        if (programmerMI.Checked) notBT_Click(notBT, null);
                        break;
                    case Keys.Shift | Keys.D1:     // ! 65585
                        if (scientificMI.Checked) MathFunction(btFactorial);    // n!
                        break;
                    case Keys.Shift | Keys.D3:     // # 65587
                        if (scientificMI.Checked) MathFunction(x3_bt);    // x³
                        break;
                    case Keys.Shift | Keys.D6: // ^ 65590
                        if (programmerMI.Checked) operatorBT_Click(XorBT, null);
                        break;
                    case Keys.Shift | Keys.D7:  // & 65591
                        if (programmerMI.Checked) operatorBT_Click(AndBT, null);
                        break;
                    case Keys.Shift | Keys.Oem5:    // | 65756
                        if (programmerMI.Checked) operatorBT_Click(or_BT, null);
                        break;
                    case Keys.Shift | Keys.Oemcomma:     // < 65724
                        if (programmerMI.Checked) operatorBT_Click(LshBT, null);
                        break;
                    case Keys.Shift | Keys.OemPeriod:   // > 65726
                        if (programmerMI.Checked) operatorBT_Click(RshBT, null);
                        break;
                    case Keys.Shift | Keys.D2:     // sqrt @ 65586
                        if (scientificMI.Checked || standardMI.Checked) MathFunction(sqrt_bt);
                        break;
                    // cac to hop phim
                    case Keys.Control | Keys.A:    // control A 131137
                        if (statisticsMI.Checked) x2cross_Click(x2cross, null);
                        break;
                    case Keys.Control | Keys.B:    // control B 131138
                        if (scientificMI.Checked) MathFunction(_3vx_bt); // ³√x
                        break;
                    case Keys.Control | Keys.C: case Keys.Control | Keys.Insert :  // control C 131139/control insert 131117
                        copyCTMN_Click(copyCTMN, null); // Copy
                        break;
                    case Keys.Control | Keys.D: // control D 131140
                        digitLoad(false);
                        break;
                    case Keys.Control | Keys.G:    // control G 131143
                        if (scientificMI.Checked) MathFunction(_10x_bt); // 10^x
                        break;
                    //case Keys.Control | Keys.K:    // control L 131147
                    //    preferrencesMI_Click(preferencesMI, null);  //MC
                    //    break;
                    case Keys.Control | Keys.L:    // control L 131148
                        memclearBT_Click(memclearBT, null);  //MC
                        break;
                    case Keys.Control | Keys.M:    // control M 131149
                        mem_process("MS"); //MS
                        break;
                    case Keys.Control | Keys.O:    // control O 131151
                        MathFunction(cosh_bt);  // cosh
                        break;
                    case Keys.Control | Keys.P:    // control P 131152
                        mem_process("M+"); //M+
                        break;
                    case Keys.Control | Keys.Q:    // control Q 131153
                        mem_process("M-"); //M-
                        break;
                    case Keys.Control | Keys.R:    // control R 131154
                        if (pex == null) recallMemory();
                        break;
                    case Keys.Control | Keys.S:    // control S 131155
                        if (scientificMI.Checked) MathFunction(sinh_bt);  // sinh
                        if (statisticsMI.Checked) sigmax2BT_Click(sigmax2BT, null);
                        break;
                    case Keys.Control | Keys.T:    // control t 131156
                        if (scientificMI.Checked) MathFunction(tanh_bt);  // tanh
                        if (statisticsMI.Checked) sigman_1BT_Click(sigman_1BT, null);
                        break;
                    case Keys.Control | Keys.V: case Keys.Shift | Keys.Insert:   // control V 131158/shift insert 65581
                        if (pasteMI.Enabled) { getPaste(null, null); pasteCTMN_Click(pasteCTMN, null); }
                        break;
                    case Keys.Control | Keys.Y:    // control Y 131161
                        if (scientificMI.Checked) sci_operation(34);  // yx
                        break;
                    case Keys.Control | Keys.Enter:    // control Enter 131085
                        if (statisticsMI.Checked) AddstaBT_Click(AddstaBT, null);
                        break;
                    case Keys.Alt | Keys.C:    // alt C 262211
                        if (dateCalculationMI.Checked) dateCalculationBT_Click(dateCalculationBT, null);
                        if (mortgageMI.Checked || vehicleLeaseMI.Checked || fe_MPG_MI.Checked || feL100_MI.Checked)
                            workSheetCalculateBT_Click(workSheetCalculateBT, null);
                        break;
                    #endregion
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
		/// <summary>
        /// lưu property vào file trước khi đóng chương trình
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            if ((int)currentConfig[Config._12_ReadDictionary] == 1)
            {
                Common.SaveDictionary(factDict);
            }
            if (scientificMI.Checked && (int)currentConfig[Config._13_StoreHistory] == 1)
            {
                Common.SaveHistory(historyObj);
            }
            if (!propertiesChange) return;

            if (standardMI.Checked) currentConfig[Config._00_CalculatorType] = 0;
            if (scientificMI.Checked) currentConfig[Config._00_CalculatorType] = 1;
            if (programmerMI.Checked) currentConfig[Config._00_CalculatorType] = 2;
            if (statisticsMI.Checked) currentConfig[Config._00_CalculatorType] = 3;

            if (basicMI.Checked) currentConfig[Config._02_ExtraFunction] = 0;
            if (unitConversionMI.Checked) currentConfig[Config._02_ExtraFunction] = 1;
            if (dateCalculationMI.Checked) currentConfig[Config._02_ExtraFunction] = 2;
            if (mortgageMI.Checked) currentConfig[Config._02_ExtraFunction] = 3;
            if (vehicleLeaseMI.Checked) currentConfig[Config._02_ExtraFunction] = 4;
            if (fe_MPG_MI.Checked) currentConfig[Config._02_ExtraFunction] = 5;
            if (feL100_MI.Checked) currentConfig[Config._02_ExtraFunction] = 6;

            currentConfig[Config._01_UseSep] = digitGroupingMI.Checked ? 1 : 0;
            currentConfig[Config._03_ShowHistory] = historyMI.Checked ? 1 : 0;
            currentConfig[Config._04_MemoryNumber] = mem_num.StrValue;
            currentConfig[Config._05_ShowPreview] = !showPreviewPaneCTMN.Visible ? 1 : 0;
            currentConfig[Config._06_TypeUnit] = typeUnitCB.SelectedIndex * (typeUnitCB.SelectedIndex >= 0 ? 1 : 0);
            currentConfig[Config._07_AutoCalculate] = autocal_date.Checked ? 1 : 0;
            currentConfig[Config._08_DateMethod] = datemethodCB.SelectedIndex * (datemethodCB.SelectedIndex >= 0 ? 1 : 0);

            if (!currentConfig.Equals(initConfig))
            {
                Common.SaveToRegistryBeforeExit(optionName, currentConfig);
            }
        }
        #endregion

        #region user's method
        /// <summary>
        /// thay đổi kích thước font cho vừa với số chữ số hiện trên màn hình
        /// </summary>
        private Font fontChanged(int length)
        {
            Font font = new Font(scr_lb.Font.FontFamily, 15.75F);
            if (standardMI.Checked || statisticsMI.Checked)
            {
                if (length < 13) font = new Font(scr_lb.Font.FontFamily, 15.75F);   // 13 van du
                if (length >= 13 && length < 21) font = new Font(scr_lb.Font.FontFamily, 10.25F);  // 13 van du
                if (length >= 21) font = new Font(scr_lb.Font.FontFamily, 9.75F);
            }
            if (scientificMI.Checked/* && pex == null*/)
            {
                if (length < 31) font = new Font(scr_lb.Font.FontFamily, 15.75F);
                if (length >= 31 && length < 37) font = new Font(scr_lb.Font.FontFamily, 13.75F);
                if (length >= 37 && length < 46) font = new Font(scr_lb.Font.FontFamily, 10.25F);
                if (length >= 46) font = new Font(scr_lb.Font.FontFamily, 9.25F);
            }
            if (programmerMI.Checked)
            {
                if (length < 30) font = new Font(scr_lb.Font.FontFamily, 15.75F);
                //if (length >= 22 && length < 28) font = new Font(scr_lb.Font.FontFamily, 17.75F);
                if (length >= 30 && length < 36) font = new Font(scr_lb.Font.FontFamily, 13.75F);
                if (length >= 36 && length < 52) font = new Font(scr_lb.Font.FontFamily, 9.75F);
                if (length >= 52 && length < 73) font = new Font(scr_lb.Font.FontFamily, 6.75F);
                if (length >= 73) font = new Font(scr_lb.Font.FontFamily, 6F);
            }
            return font;
        }
        /// <summary>
        /// form standard
        /// </summary>
        private void stdLoad(bool isLoaded)
        {
            int his = historyMI.Checked ? 1 : 0;
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(216 + (exf ? 376 : 0), 310 + 105 * his);     // dua ra ngoai de resize lai form
            FormSizeChanged(new Size(exf ? APP_W3 : APP_W1, historyMI.Checked ? APP_H3 : APP_H1), isLoaded);
            if (!standardMI.Checked)
            {
                parser.DecimalPrecision = 15;
                if (programmerMI.Checked) EnableComponentByProgrammer();
                modeMethod(standardMI);
                EnableKeyboardAndChangeFocus();
                if (historyMI.Checked && historyMI.Enabled) stdWithHistory();
                else initializedForm(true);

                this.hisDGV.Size = new Size(190 + (scientificMI.Checked ? 195 : 0), hisDGV.Size.Height);
                VisibleComponentsToForm(0);

                unitconvPN.Location = new Point(213, 12);
                unitconvPN.Size = new Size(356, 241 + 103 * his);

                datecalcPN.Visible = dateCalculationMI.Checked;
                unitconvPN.Visible = unitConversionMI.Checked;
                workSheetPN.Visible = (mortgageMI.Checked || vehicleLeaseMI.Checked || fe_MPG_MI.Checked || feL100_MI.Checked);

                gridPanel.Visible = hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = false;

                if (!isLoaded) propertiesChange = true;
                clear_num(false);
                prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused;
            }
        }
        /// <summary>
        /// form scientific
        /// </summary>
        private void sciLoad(bool isLoaded)
        {
            int his = historyMI.Checked ? 1 : 0;
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(413 + (exf ? 376 : 0), 310 + 105 * his);
            FormSizeChanged(new Size(exf ? APP_W4 : APP_W2, historyMI.Checked ? APP_H3 : APP_H1), isLoaded);
            if (!scientificMI.Checked)
            {
                parser.DecimalPrecision = 31;
                if (programmerMI.Checked) EnableComponentByProgrammer();
                modeMethod(scientificMI);
                EnableKeyboardAndChangeFocus();
                VisibleComponentsToForm(1);

                if (historyMI.Checked && historyMI.Enabled) sciWithHistory();
                else scientificLoad(true);

                this.hisDGV.Size = new Size(scientificMI.Checked ? 385 : 190, hisDGV.Size.Height);
                datecalcPN.Visible = dateCalculationMI.Checked;
                unitconvPN.Visible = unitConversionMI.Checked;
                workSheetPN.Visible = (mortgageMI.Checked || vehicleLeaseMI.Checked || fe_MPG_MI.Checked || feL100_MI.Checked);

                unitconvPN.Location = new Point(408, 12);
                unitconvPN.Size = new Size(356, 241 + 103 * his);
                if (exf) basicMI.Checked = false;

                gridPanel.Visible = hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = false;

                if (!isLoaded)
                {
                    clear_num(true);
                    propertiesChange = true;
                }
                if ((int)currentConfig[Config._13_StoreHistory] == 1)
                {
                    DisplayHistoryOnLoad(historyObj);
                }
                prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused;
            }
        }
        /// <summary>
        /// form programmer
        /// </summary>
        private void proLoad(bool isLoaded)
        {
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(413 + 374 * exf ? 1 : 0, 374);
            if (!programmerMI.Checked)
            {
                if (bin_digit == null) InitBitNumberArray();
                modeMethod(programmerMI);
                EnableKeyboardAndChangeFocus();
                VisibleComponentsToForm(2);
                datecalcPN.Visible = dateCalculationMI.Checked;
                unitconvPN.Visible = unitConversionMI.Checked;
                workSheetPN.Visible = (mortgageMI.Checked || vehicleLeaseMI.Checked || fe_MPG_MI.Checked || feL100_MI.Checked);

                showPreviewPaneCTMN.Visible = previewPanelHeight == 0;
                hidePreviewPaneCTMN.Visible = previewPanelHeight == 81;

                //FormSizeChanged(new Size(413 + (exf ? 371 : 0), 374 + previewPanelHeight), isLoaded);
                FormSizeChanged(new Size(exf ? APP_W4 : APP_W2, showPreviewPaneCTMN.Visible ? APP_H2 : APP_H4), isLoaded);
                programmerMode(showPreviewPaneCTMN.Visible);
                
                historyMI.Enabled = false;
                unitconvPN.Location = new Point(408, 12);
                unitconvPN.Size = new Size(356, 304 + previewPanelHeight);

                gridPanel.Visible = false;
                hisDGV.Visible = false;
                staDGV.Visible = false;
                bin_prvLb.Visible = !showPreviewPaneCTMN.Visible;

                if (!isLoaded)
                {
                    clear_num(true);
                    propertiesChange = true;
                }
                prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused;
            }
        }
        /// <summary>
        /// form statistics
        /// </summary>
        private void staLoad(bool isLoaded)
        {
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(216 + (exf ? 374 : 0), 414);
            FormSizeChanged(new Size(exf ? APP_W3 : APP_W1, APP_H3), isLoaded);
            if (!statisticsMI.Checked)
            {
                modeMethod(statisticsMI);
                EnableKeyboardAndChangeFocus();
                VisibleComponentsToForm(3);
                datecalcPN.Visible = dateCalculationMI.Checked;
                unitconvPN.Visible = unitConversionMI.Checked;
                workSheetPN.Visible = (mortgageMI.Checked || vehicleLeaseMI.Checked || fe_MPG_MI.Checked || feL100_MI.Checked);

                gridPanel.Visible = true;
                hisDGV.Visible = false;
                staDGV.Visible = true;

                countLB.Text = "∑n = 0";
                statisticsMode();
                //ctmnEnableAndVisible();

                EnableComponentByProgrammer();
                unitconvPN.Location = new Point(213, 12);
                unitconvPN.Size = new Size(356, 344);

                clear_num(false);
                if (!isLoaded) propertiesChange = true;
                prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused;
            }
        }
        /// <summary>
        /// khi chọn 4 dòng đầu tiên của menu view
        /// </summary>
        private void modeMethod(MenuItem mi)
        {
            prcmdkey = true;
            standardMI.Checked = false;
            scientificMI.Checked = false;
            programmerMI.Checked = false;
            statisticsMI.Checked = false;
            mi.Checked = true;
            currentConfig[Config._00_CalculatorType] = mi.Index;

            historyOptionMI.Visible = historyMI.Enabled = !programmerMI.Checked && !statisticsMI.Checked;
            datasetMI.Visible = statisticsMI.Checked;
            sepMI3.Visible = (mi != programmerMI);
            //sepCTMN.Visible = (mi != programmerMI);
            percent_bt.Enabled = mi == standardMI;

            while (hisDGV.RowCount > 0) hisDGV.Rows.RemoveAt(0);
            countLB.Text = "";
        }
        /// <summary>
        /// nạp những setting từ lần sử dụng trước đó từ registry
        /// </summary>
        private void LoadInfo()
        {
            initConfig = Common.ReadFromRegedit(optionName);
            //getMemoryNumber();
            currentConfig = new object[initConfig.Length];
            initConfig.CopyTo(currentConfig, 0);
            mem_num = initConfig[Config._04_MemoryNumber].ToString();

            if ((int)initConfig[Config._00_CalculatorType] == 0) stdLoad(true);
            if ((int)initConfig[Config._00_CalculatorType] == 1) sciLoad(true);
           
            previewPanelHeight = (int)initConfig[Config._05_ShowPreview] == 1 ? 81 : 0;
            if ((int)initConfig[Config._00_CalculatorType] == 2)
            {
                proLoad(true);
            }
            if ((int)initConfig[Config._00_CalculatorType] == 3) staLoad(true);
            if ((int)initConfig[Config._01_UseSep] == 1) digitLoad(true);

            switch ((int)initConfig[Config._02_ExtraFunction])
            {
                case 1:
                    exFunc(unitConversionMI, true);
                    typeUnitCB.SelectedIndex = (int)initConfig[Config._06_TypeUnit];
                    break;
                case 2:
                    exFunc(dateCalculationMI, true);
                    datemethodCB.SelectedIndex = (int)initConfig[Config._08_DateMethod];
                    autocal_date.Checked = ((int)initConfig[Config._07_AutoCalculate] == 1);
                    break;
                case 3:
                    exFunc(mortgageMI, true);
                    break;
                case 4:
                    exFunc(vehicleLeaseMI, true);
                    break;
                case 5:
                    exFunc(fe_MPG_MI, true);
                    break;
                case 6:
                    exFunc(feL100_MI, true);
                    break;
            }

            if ((int)initConfig[Config._03_ShowHistory] == 1)
            {
                if (historyMI.Enabled) formWithHistory(true);
                historyMI.Checked = true;
            }

            typeUnitCB.SelectedIndexChanged -= typeUnitCB_SelectedIndexChanged;
            typeUnitCB.SelectedIndex = (int)initConfig[Config._06_TypeUnit];
            typeUnitCB.SelectedIndexChanged += typeUnitCB_SelectedIndexChanged;

            datemethodCB.SelectedIndexChanged -= datemethodCB_SelectedIndexChanged;
            autocal_date.CheckedChanged -= autocal_date_CheckedChanged;

            datemethodCB.SelectedIndex = (int)initConfig[Config._08_DateMethod];
            calMethod_SIC(true, (int)initConfig[Config._08_DateMethod]);

            datemethodCB.SelectedIndexChanged += datemethodCB_SelectedIndexChanged;
            autocal_date.CheckedChanged += autocal_date_CheckedChanged;

            btdot.Text = Common.DecimalSeparator;
            //historyObj = new string[100];
        }
        /// <summary>
        /// animate resization
        /// </summary>
        private void FormSizeChanged(Size newS, bool isLoaded)
        {
            scr_lb.Focus();
            if (isLoaded) { this.Size = newS; return; }
            int old_w = Size.Width, old_h = Size.Height;
            int new_w = newS.Width, new_h = newS.Height;
            //------chi thay doi chieu rong
            int collapseSpeed = (int)currentConfig[Config._09_CollapseSpeed];
            int loopNumber = 0;
            int inc_or_dec = 0;

            if (old_h == new_h)
            {
                inc_or_dec = (old_w < new_w) ? 1 : -1;
                //loopNumber = (int)(Math.Abs(new_w - old_w) / collapseSpeed);
                loopNumber = (int)((new_w - old_w) * inc_or_dec / collapseSpeed);
                for (int i = 0; i < loopNumber; i++)
                    this.Size = new Size(Size.Width + collapseSpeed * inc_or_dec, old_h);
                goto end;
            }
            //------chi thay doi chieu cao
            if (old_w == new_w)
            {
                inc_or_dec = (old_h < new_h) ? 1 : -1;
                loopNumber = (int)((new_h - old_h) * inc_or_dec / collapseSpeed);
                for (int i = 0; i < loopNumber; i++)
                    this.Size = new Size(old_w, Size.Height + collapseSpeed * inc_or_dec);
                goto end;
            }
            //------thay doi ca chieu rong va chieu cao
            int verticalSpd = (new_w - old_w) / (new_h - old_h) * collapseSpeed;
            verticalSpd = Math.Abs(verticalSpd);

            inc_or_dec = (old_h < new_h) ? 1 : -1;
            if (old_w > new_w)
            {
                loopNumber = (int)((new_h - old_h) * inc_or_dec / verticalSpd);
                for (int i = 0; i < loopNumber; i++)
                    this.Size = new Size(old_w -= verticalSpd, Size.Height + inc_or_dec * collapseSpeed);
                goto end;
            }

            if (old_w < new_w)
            {
                loopNumber = (int)((new_w - old_w) * inc_or_dec / verticalSpd);
                for (int i = 0; i < loopNumber; i++)
                    this.Size = new Size(old_w += verticalSpd, Size.Height + inc_or_dec * collapseSpeed);
            }
            end: if (Size.Height != new_h || Size.Width != new_w) this.Size = new Size(new_w, new_h);
        }
        /// <summary>
        /// form with history - control - H
        /// </summary>
        private void formWithHistory(bool isLoaded)
        {
            historyMI.Checked = !historyMI.Checked;
            currentConfig[Config._03_ShowHistory] = historyMI.Checked ? 1 : 0;

            prcmdkey = true;
            int his = ((historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked) ? 1 : 0;
            int sci = scientificMI.Checked ? 1 : 0;
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked ||
                fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked) ? 1 : 0;

            unitconvPN.Location = new Point(213 + 195 * sci, 12);
            unitconvPN.Size = new Size(356, 241 + 103 * his);

            hisDGV.EndEdit();

            if (historyMI.Checked && historyMI.Enabled)
            {
                if (standardMI.Checked) stdWithHistory();
                if (scientificMI.Checked)
                {
                    sciWithHistory();
                    if (hisDGV.RowCount == 0) hisDGV.CurrentCell = null;
                }
                if (hisDGV.RowCount > 0) hisDGV.CurrentCell = hisDGV[0, hisDGV.CurrentCell.RowIndex];
            }
            else
            {
                if (standardMI.Checked) initializedForm(false);
                if (scientificMI.Checked) scientificLoad(false);
            }

            gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
            hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
            staDGV.Visible = /*countLB.Visible = */!historyMI.Checked || !historyMI.Enabled;

            //countLB.Text = "";

            //FormSizeChanged(new Size(220 + 193 * sci + 371 * exf, 310 + 104 * his), isLoaded);
            FormSizeChanged(new Size(220 + 193 * sci + 371 * exf, his == 1 ? APP_H3 : APP_H1), isLoaded);
            if (!isLoaded) propertiesChange = true;
        }
        /// <summary>
        /// form with preview
        /// </summary>
        private void formWithPreview(bool isLoaded)
        {
            currentConfig[Config._05_ShowPreview] = !showPreviewPaneCTMN.Visible ? 1 : 0;

            prcmdkey = true;
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked ||
                fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked) ? 1 : 0;

            unitconvPN.Location = new Point(408, 12);
            unitconvPN.Size = new Size(356, 304 + previewPanelHeight);

            programmerMode(showPreviewPaneCTMN.Visible);

            gridPanel.Visible = false;
            hisDGV.Visible = false;
            staDGV.Visible = false;
            bin_prvLb.Visible = !showPreviewPaneCTMN.Visible;

            //countLB.Text = "";

            //FormSizeChanged(new Size(413 + 371 * exf, 374 + previewPanelHeight), isLoaded);
            FormSizeChanged(new Size(exf == 1 ? APP_W4 : APP_W2, hidePreviewPaneCTMN.Visible ? APP_H4 : APP_H2), isLoaded);
            if (!isLoaded) propertiesChange = true;
        }
        /// <summary>
        /// 2 tính năng date input và unit conversion
        /// </summary>
        private void exFunc(MenuItem menuitem, bool isLoaded)
        {
            int his = (historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked ? 1 : 0;
            int std = standardMI.Checked || statisticsMI.Checked ? 5 : 0;
            int pro = programmerMI.Checked ? 1 : 0;
            int sci = scientificMI.Checked ? 1 : 0;
            //FormSizeChanged(new Size(216 + (exf ? 376 : 0), 310 + 105 * his), isLoaded);
            FormSizeChanged(new Size(591 + 193 * (sci + pro), 310 + 104 * his + 64 * pro + previewPanelHeight * pro), isLoaded);

            currentConfig[Config._02_ExtraFunction] = menuitem.Index + (menuitem.Parent == worksheetsMI ? 3 : -8);
            datecalcPN.Visible = dateCalculationMI.Checked = (menuitem == dateCalculationMI);
            unitconvPN.Visible = unitConversionMI.Checked = (menuitem == unitConversionMI);
            workSheetPN.Visible = (menuitem.Parent == worksheetsMI);

            mortgageMI.Checked = (menuitem == mortgageMI);
            vehicleLeaseMI.Checked = (menuitem == vehicleLeaseMI);
            fe_MPG_MI.Checked = (menuitem == fe_MPG_MI);
            feL100_MI.Checked = (menuitem == feL100_MI);

            typeWorkSheetCB.Items.Clear();
            if (menuitem.Parent == worksheetsMI) typeWorkSheetCB.Items.AddRange(worksheetsComboBoxItems[menuitem.Index]);
            switch (menuitem.Index)
            {
                case 0:
                    if (mortgageTF == null) mortgageTF = InitCustomControls(5, menuitem.Index);
                    HideWorkSheetTextField(mortgageTF);
                    typeWorkSheetCB.SelectedIndex = 1;
                    break;
                case 1:
                    if (VhTF == null) VhTF = InitCustomControls(6, menuitem.Index);
                    HideWorkSheetTextField(VhTF);
                    typeWorkSheetCB.SelectedIndex = 2;
                    break;
                case 2:
                    if (fe_MPGTF == null) fe_MPGTF = InitCustomControls(3, menuitem.Index);
                    HideWorkSheetTextField(fe_MPGTF);
                    typeWorkSheetCB.SelectedIndex = 1;
                    break;
                case 3:
                    if (feL100TF == null) feL100TF = InitCustomControls(3, menuitem.Index);
                    HideWorkSheetTextField(feL100TF);
                    typeWorkSheetCB.SelectedIndex = 1;
                    break;
            }

            unitconvPN.Size = new Size(356, 241 + 63 * pro + 103 * his + previewPanelHeight * pro);
            unitconvPN.Location = new Point(213 + 195 * (sci + pro), 12);

            basicMI.Checked = false;

            if (fe_MPG_MI.Checked || feL100_MI.Checked) this.AcceptButton = workSheetCalculateBT;

            typeWorkSheetCB.Visible = (menuitem.Parent == worksheetsMI);

            if (isLoaded)
            {
                datemethodCB.SelectedIndexChanged -= datemethodCB_SelectedIndexChanged;
                datemethodCB.SelectedIndex = (int)initConfig[Config._08_DateMethod];
                datemethodCB.SelectedIndexChanged += datemethodCB_SelectedIndexChanged;
                //------------------------
                typeUnitCB.SelectedIndexChanged -= typeUnitCB_SelectedIndexChanged;
                typeUnitCB.SelectedIndex = (int)initConfig[Config._06_TypeUnit];
                typeUnitCB.SelectedIndexChanged += typeUnitCB_SelectedIndexChanged;
            }
            else
            {
                propertiesChange = true;
                if (menuitem == unitConversionMI) { typeUnitCB.Focus(); typeUnitCB.Focus(); }
                if (menuitem == dateCalculationMI) datemethodCB.Focus();
                if (menuitem.Parent == worksheetsMI) typeWorkSheetCB.Focus();
            }
            toCB.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            assignDefaultIndex(typeUnitCB.SelectedIndex >= 0 ? typeUnitCB.SelectedIndex : 0);
            toCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;

            autocal_date.CheckedChanged -= autocal_date_CheckedChanged;
            autocal_date.Checked = (int)currentConfig[Config._07_AutoCalculate] == 1;
            if (autocal_date.Checked)
            {
                dtP_ValueChanged(dtP2, null);
            }
            autocal_date.CheckedChanged += autocal_date_CheckedChanged;
            if (dateCalculationMI.Checked) this.AcceptButton = dateCalculationBT;
            if (menuitem.Parent == worksheetsMI) this.AcceptButton = workSheetCalculateBT;

            prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused && !typeWorkSheetCB.Focused;
        }
        /// <summary>
        /// ẩn các text field của các hàm worksheet khác khi worksheet này được chọn
        /// </summary>
        /// <param name="itfList"></param>
        private void HideWorkSheetTextField(TextField[] itfList)
        {
            foreach (TextField[] item in new object[] { mortgageTF, VhTF, fe_MPGTF, feL100TF })
            {
                if (item != itfList && item != null)
                {
                    for (int i = 0; i < item.Length; i++)
                    {
                        item[i].Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// ẩn các control đặc biệt của form standard
        /// </summary>
        private void VisibleComponentsToForm(int idex)
        {
            // standard form
            ce.Visible = idex == 0 || idex == 1 || idex == 2;
            addbt.Visible = idex == 0 || idex == 1 || idex == 2;
            mulbt.Visible = idex == 0 || idex == 1 || idex == 2;
            minusbt.Visible = idex == 0 || idex == 1 || idex == 2;
            divbt.Visible = idex == 0 || idex == 1 || idex == 2;
            equalBT.Visible = idex == 0 || idex == 1 || idex == 2;
            invertbt_PN.Visible = idex == 0 || idex == 1 || idex == 2;
            percentbt_PN.Visible = idex == 0 || idex == 1 || idex == 2;
            sqrtbt_PN.Visible = idex == 0 || idex == 1 || idex == 2;
            screenStr = new StringBuilder("0");

            // scientific form
            anglePN.Visible = idex == 1;
            inv_ChkBox.Visible = idex == 1;
            sin_bt.Visible = idex == 1;
            cos_bt.Visible = idex == 1;
            tan_bt.Visible = idex == 1;
            sinh_bt.Visible = idex == 1;
            cosh_bt.Visible = idex == 1;
            tanh_bt.Visible = idex == 1;
            _10x_bt.Visible = idex == 1;
            _3vx_bt.Visible = idex == 1;
            nvx_bt.Visible = idex == 1;
            log_bt.Visible = idex == 1;
            ln_bt.Visible = idex == 1;
            exp_bt.Visible = idex == 1 || idex == 3;
            pi_bt.Visible = idex == 1;
            fe_ChkBox.Visible = idex == 1 || idex == 3;
            dms_bt.Visible = idex == 1;
            int_bt.Visible = idex == 1;
            x2_bt.Visible = idex == 1;
            x3_bt.Visible = idex == 1;
            xn_bt.Visible = idex == 1;
            btFactorial.Visible = idex == 1;
            modsciBT.Visible = idex == 1;
            open_bracket.Visible = idex == 1;
            close_bracket.Visible = idex == 1 || idex == 2;
            bracketTime_lb.Visible = idex == 1 || idex == 2;

            // programmer form
            unknownPN.Visible = idex == 2;
            PNbinary.Visible = idex == 2;
            basePN.Visible = idex == 2;
            RoRBT.Visible = idex == 2;
            RoLBT.Visible = idex == 2;
            RshBT.Visible = idex == 2;
            LshBT.Visible = idex == 2;
            or_BT.Visible = idex == 2;
            XorBT.Visible = idex == 2;
            notBT.Visible = idex == 2;
            AndBT.Visible = idex == 2;
            openProBT.Visible = idex == 2;
            modproBT.Visible = idex == 2;
            btnA_PN.Visible = idex == 2;
            btnB_PN.Visible = idex == 2;
            btnC_PN.Visible = idex == 2;
            btnD_PN.Visible = idex == 2;
            btnE_PN.Visible = idex == 2;
            btnF_PN.Visible = idex == 2;

            // statistics form
            sigman_1BT.Visible = idex == 3;
            sigmanBT.Visible = idex == 3;
            sigmaxBT.Visible = idex == 3;
            sigmax2BT.Visible = idex == 3;
            xcross.Visible = idex == 3;
            x2cross.Visible = idex == 3;
            staDGV.Visible = /*countLB.Visible = */idex == 3;
            CAD.Visible = idex == 3;
            AddstaBT.Visible = idex == 3;
            datasetMI.Visible = idex == 3;
        }
        /// <summary>
        /// enable các nút đã bị form programmer disable
        /// </summary>
        private void EnableComponentByProgrammer()
        {
            num2BT.Enabled = true;
            num3BT.Enabled = true;
            num4BT.Enabled = true;
            num5BT.Enabled = true;
            num5BT.Enabled = true;
            num6BT.Enabled = true;
            num7BT.Enabled = true;
            num8BT.Enabled = true;
            num9BT.Enabled = true;
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;

            btFactorial.Enabled = true;
            sqrt_bt.Enabled = true;
            invert_bt.Enabled = true;
            btdot.Enabled = true;
        }
        /// <summary>
        /// sự kiện radio control của form programmer checked changed:
        /// set lại thuộc tính enable cho các nút số và A-F
        /// </summary>
        private void baseRBCheckedChanged()
        {
            bool bl = (octRB.Checked || decRB.Checked || hexRB.Checked);
            this.num2BT.Enabled = bl;
            this.num3BT.Enabled = bl;
            this.num4BT.Enabled = bl;
            this.num5BT.Enabled = bl;
            this.num6BT.Enabled = bl;
            this.num7BT.Enabled = bl;

            bl = (decRB.Checked || hexRB.Checked);
            this.num8BT.Enabled = bl;
            this.num9BT.Enabled = bl;
            ////
            //// base buttons
            ////
            this.btnA.Enabled = hexRB.Checked;
            this.btnB.Enabled = hexRB.Checked;
            this.btnC.Enabled = hexRB.Checked;
            this.btnD.Enabled = hexRB.Checked;
            this.btnE.Enabled = hexRB.Checked;
            this.btnF.Enabled = hexRB.Checked;

            #region đổi màu label preview
            this.hex_prvLb.ForeColor = Color.Black;
            this.dec_prvLb.ForeColor = Color.Black;
            this.oct_prvLb.ForeColor = Color.Black;
            this.bin_prvLb.ForeColor = Color.Black;
            
            if (hexRB.Checked)
            {
                this.hex_prvLb.ForeColor = Color.Red;
            }
            if (decRB.Checked)
            {
                this.dec_prvLb.ForeColor = Color.Red;
            }
            if (octRB.Checked)
            {
                this.oct_prvLb.ForeColor = Color.Red;
            }
            if (binRB.Checked)
            {
                this.bin_prvLb.ForeColor = Color.Red;
            }
            #endregion
        }
        /// <summary>
        /// hiển thị label "M" nếu có chứa memory
        /// </summary>
        private void SetMemoryLabelVisible()
        {
            mem_lb.Visible = mem_num != 0;
            toolTip1.SetToolTip(mem_lb, string.Format("M = {0}", digitGroupingMI.Checked ? Common.Group(mem_num.StrValue) : mem_num.StrValue));
        }
        /// <summary>
        /// xử lý số trên bộ nhớ
        /// </summary>
        private void mem_process(string method)
        {
            if (pex == null)
            {
                if (method == "M+") mem_num = mem_num + screenStr.ToString();  // M+
                if (method == "M-") mem_num = mem_num - screenStr.ToString();  // M-
                if (method == "MS") mem_num = screenStr.ToString();            // MS
                currentConfig[Config._04_MemoryNumber] = mem_num.StrValue;
                DisplayToScreen();

                SetMemoryLabelVisible();
                propertiesChange = true;
                confirmNumber = true;
            }
        }
        /// <summary>
        /// gọi số trên bộ nhớ
        /// </summary>
        private void recallMemory()
        {
            screenStr = new StringBuilder(mem_num.StrValue);

            if (!programmerMI.Checked)
                DisplayToScreen();
            else
                ScreenToPanel();

            curOperand = screenStr.ToString();
            expAdded = false;
            pre_bt = 25;
            confirmNumber = true;
        }
        /// <summary>
        /// dán 1 chuỗi vào màn hình
        /// </summary>
        /// <param name="data">chuỗi cần dán</param>
        private void PasteData(string data)
        {
            data = StandardPasteData(data);

            #region process data
            if (data.Length > 255) data = data.Substring(0, 255);
            if (programmerMI.Checked)
            {
                if (hexRB.Checked) data = data.Replace("0X", "");
            }
            else
            {
                Regex r = new Regex(@"=-\d{1,}");
                Match m = r.Match(data);
                while (m.Success)
                {
                    // 'x' là keydata của F9
                    data = data.Replace(m.Groups[0].Value, m.Groups[0].Value.Remove(1, 1) + "x");
                    m = r.Match(data);
                }
                // start with negative number
                m = Regex.Match(data, @"^-\d+");
                //m = Regex.Match(data, @"^-(\d+,*\d*e[\+-]?\d+|\d+,*\d*)");
                if (m.Success)
                {
                    // 'x' là keydata của F9
                    // bỏ dấu - ở đầu số và thay bằng kí tự 'x' ở cuối
                    data = string.Format("{0}x{1}", m.Groups[0].Value.Substring(1), data.Substring(m.Groups[0].Value.Length));
                }
            } 
            #endregion

            int len = data.Length;
            Keys[] keyData = new Keys[len];
            // tạo bừa thôi, hàm ProcessCmdKey có dùng cái này đâu
            Message msg = Message.Create(IntPtr.Zero, 1, IntPtr.Zero, IntPtr.Zero);
            for (int i = 0; i < len; i++)
            {
                #region process key pressed
                keyData[i] = (Keys)(data[i]);
                switch ((int)data[i])
                {
                    case 10:
                    case 61: keyData[i] = Keys.Enter;
                        break;
                    case 33: keyData[i] = Keys.Shift | Keys.D1;
                        break;
                    case 35: keyData[i] = Keys.Shift | Keys.D3;
                        break;
                    case 37: keyData[i] = Keys.Shift | Keys.D5;
                        break;
                    case 38: keyData[i] = (Keys)65591;
                        break;
                    case 40: /*case 91: */keyData[i] = Keys.OemOpenBrackets;
                        break;
                    case 41: /*case 93: */keyData[i] = Keys.OemCloseBrackets;
                        break;
                    case 43: if (i > 0)
                        {
                            if (data[i - 1] != 88 && data[i - 1] != 69)
                                keyData[i] = Keys.Add;
                            else continue;
                        }
                        break;
                    case 42: keyData[i] = Keys.Multiply;
                        break;
                    case 44:
                    case 46: keyData[i] = Keys.Decimal;
                        break;
                    case 45:
                        if (i > 0)
                        {
                            if (data[i - 1] != 'E' && data[i - 1] != 'X') keyData[i] = Keys.OemMinus;
                            else if (programmerMI.Checked || hexRB.Checked) keyData[i] = Keys.OemMinus;
                            else keyData[i] = Keys.F9;
                        }
                        break;
                    case 47: keyData[i] = Keys.Divide;
                        break;
                    case 59: keyData[i] = Keys.OemSemicolon;
                        break;
                    case 64: keyData[i] = Keys.Shift | Keys.D2;
                        break;
                    case 66: if (scientificMI.Checked) keyData[i] = Keys.Control | Keys.B;
                        break;
                    case 71: keyData[i] = Keys.Control | Keys.G;
                        break;
                    case 94:
                        if (scientificMI.Checked) keyData[i] = Keys.Y;
                        if (programmerMI.Checked) keyData[i] = Keys.Shift | Keys.D6;    //(Keys)65590;
                        break;
                    case 124: keyData[i] = Keys.Shift | Keys.Oem5;  //(Keys)65756;
                        break;
                } 
                #endregion

                ProcessCmdKey(ref msg, keyData[i]);
            }
            if (programmerMI.Checked) ScreenToPanel();
            else DisplayToScreen();
        }
        /// <summary>
        /// xoá các ký tự thừa để paste vào cell
        /// </summary>
        /// <param name="vl">giá trị trước khi paste</param>
        /// <returns>giá trị sau khi chuẩn hoá</returns>
        private string StandardPasteData(object vl)
        {
            string valueString = Convert.ToString(vl);
            valueString = valueString.Replace("\n", "=");
            valueString = Regex.Replace(valueString, @"\s", "");
            valueString = valueString.Replace(Common.GroupSeparator, "");
            return valueString;
        }

        public static List<FactorialObject> factDict = new List<FactorialObject>();

        bool isFuncClicked = false;
        /// <summary>
        /// các hàm tính nâng cao
        /// </summary>
        private void MathFunction(Button bt)
        {
            if (pex != null) return;
            BigNumber inp_num = screenStr.ToString();
            int tabIndex = bt.TabIndex;
            string parameter = screenStr.ToString();
            if (isFuncClicked || expAdded)
            {
                parameter = curOperand;
            }
            else if (pre_bt == 153) parameter = bracketExp[openBrkLevel];
            int len = curOperand.Length;

            while (parameter.StartsWith("(") && parameter.EndsWith(")"))
            {
                parameter = parameter.Substring(1, parameter.Length - 2);
            }

            switch (tabIndex)
            {
                #region Assign function
                // sin, cos, tan, sinh, cosh, tanh, log, int
                case 28: case 29: case 30:
                    curOperand = string.Format("{0}{1}({2})", bt.Text, parser.Angle, parameter);
                    break;
                case 35: case 36: case 37: case 42: case 44:
                    curOperand = string.Format("{0}({1})", bt.Text, parameter);
                    break;
                case 45:
                    if (!inv_ChkBox.Checked) curOperand = string.Format("dms({0})", parameter);
                    else curOperand = string.Format("degrees({0})", parameter);
                    break;
                case 11:
                    curOperand = string.Format("negate({0})", parameter);
                    break;
                case 17:
                    curOperand = string.Format("reciproc({0})", parameter);
                    break;
                case 19:
                    curOperand = string.Format("sqrt({0})", parameter);
                    break;
                case 31:
                    if (!inv_ChkBox.Checked) curOperand = string.Format("ln({0})", parameter);
                    else curOperand = string.Format("powe({0})", parameter);
                    break;
                case 32:
                    if (BigNumber.IsInteger(inp_num.StrValue))
                    {
                        if (inp_num >= 100000 && co != null)
                        {
                            InitCancelOperation();
                        }
                        if ((int)currentConfig[Config._10_FastFactorial] == 1)
                            curOperand = string.Format("fast({0})", parameter);
                        else
                            curOperand = string.Format("fact({0})", parameter);
                    }
                    else return;
                    break;
                case 38:
                    curOperand = string.Format("sqr({0})", parameter);
                    break;
                case 39:
                    curOperand = string.Format("cube({0})", parameter);
                    break;
                case 40:
                    curOperand = string.Format("powten({0})", parameter);
                    break;
                case 41:
                    curOperand = string.Format("cuberoot({0})", parameter);
                    break;
                #endregion
            }

            if (expAdded)
            {
                mainExp = new StringBuilder(mainExp.ToString().Substring(0, mainExp.Length - len)).Append(curOperand);
                bracketExp[openBrkLevel] = bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - len) + curOperand;
            }
            else
            {
                mainExp = mainExp.Append(curOperand);
                bracketExp[openBrkLevel] += curOperand;
            }

            expAdded = true;

            if (inv_ChkBox.Checked) inv_ChkBox.Checked = false;
            //if (standardMI.Checked) pex = parser.EvaluateStd(curOperand);
            if (tabIndex != 32)
            {
                #region not a factorial function
                pex = parser.EvaluateSci(curOperand);
                //--------------------------------------------------------------
                if (pex == null)
                {
                    screenStr = new StringBuilder(parser.StringResult);
                    isFuncClicked = true;
                    confirmNumber = true;
                }
                else
                {
                    screenStr = new StringBuilder(pex.Message);
                    scr_lb.Text = pex.Message;
                    scr_lb.Font = new Font("Consolas", pex.Message.Length > 40 ? 5.25F : 9.75F);

                    inv_ChkBox.Checked = false;
                    fe_ChkBox.Checked = false;
                }
                //pex = null; 
                #endregion
            }
            else
            {
                #region Calculate the factorial
                if ((BigNumber)inp_num > long.MaxValue)
                {
                    pex = new Exception("Value is too large to calculate.");
                    //pre_bt = tabIndex;
                    DisplayToScreen();
                    return;
                }
                if (inp_num >= 1e5 && curOperand.Contains("fact"))
                {
                    int idex = Array.IndexOf(factDict.SelectValue(), inp_num.StrValue);
                    if (idex < 0)   // khong tim thay
                    {
                        ShowFactorialProgress_Pref();
                        if (pex == null)
                            screenStr = new StringBuilder(parser.StringResult);
                        else
                            screenStr = new StringBuilder(pex.Message);
                    }
                    else            // tim thay
                    {
                        screenStr = new StringBuilder(factDict[idex].Result);
                    }
                }
                else
                {
                    InitCancelOperation();
                    pex = parser.EvaluateSci(curOperand);
                    screenStr = new StringBuilder(parser.StringResult);
                }
                isFuncClicked = confirmNumber = true;   // funcID != 32;
                #endregion
            }

            expressionTB.Text = mainExp.ToString().Trim();
            pre_bt = tabIndex;
            DisplayToScreen();
        }
        /// <summary>
        /// hiện progress của hàm giai thừa khi thực hiện hàm tính
        /// </summary>
        private void ShowFactorialProgress_Pref()
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerSupportsCancellation = true;
            mWorker.WorkerReportsProgress = true;
            mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWorkPrevFunc);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);
            mWorker.RunWorkerAsync();
            InitCancelOperation();
            co.ShowDialog();
        }
        /// <summary>
        /// hiện progress của hàm giai thừa cho cả biểu thức
        /// </summary>
        private void ShowFactorialProgress_Expr()
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerSupportsCancellation = true;
            mWorker.WorkerReportsProgress = true;
            mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWorkExpr);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);
            mWorker.RunWorkerAsync();
            InitCancelOperation();
            co.ShowDialog();
        }
        /// <summary>
        /// hàm tính lại biểu thức
        /// </summary>
        /// <param name="rowID">dòng được tính lại</param>
        private void reCalculate(int rowID)
        {
            hisDGV.EndEdit();
            mainExp = new StringBuilder(hisDGV[0, rowID].Value.ToString()); // sau khi ket thuc 1 phep tinh, mainExp duoc gan ve ""
            evaluateExpression(rowID, true);
        }
        /// <summary>
        /// đưa các giá trị của các biến liên quan đến thứ tự ưu tiên các phép tính về mặc định
        /// </summary>
        private void NewPriorityExpression()
        {
            priorityExpression = new string[MAX_BRACKET_LEVEL + 1][];
            for (int i = 0; i < MAX_BRACKET_LEVEL + 1; i++)
            {
                priorityExpression[i] = new string[7];
                lowestOperator[i] = 7;
                curPriority[i] = new Operator();
                prePriority[i] = new Operator();
            }
        }
        /// <summary>
        /// nút xoá số
        /// </summary>
        /// <param name="c_bt">biến kiểm tra xem nút xoá có phải nút C hay không</param>
        private void clear_num(bool c_bt)
        {
            if (isFuncClicked && pex == null)
            {
                if (prePriority[openBrkLevel].Index < 0 && pre_bt != 32) equal_Click(equalBT, null);
                if (prePriority[openBrkLevel].Index >= 0 && pre_bt != 32 && !c_bt)
                {
                    mainExp = new StringBuilder(mainExp.ToString().Substring(0, mainExp.Length - curOperand.Length).Trim());
                    if (scientificMI.Checked) expressionTB.Text = mainExp.ToString().Trim();
                }
            }
            scr_lb.Text = "0";
            screenStr = new StringBuilder("0");
            isFuncClicked = false;
            expAdded = false;
            prcmdkey = confirmNumber = true;

            scr_lb.Font = new Font("Consolas", 15.75F);
            if (pex != null)
            {
                expressionTB.Text = "";
                mainExp = new StringBuilder();
            }

            if (c_bt)
            {
                pex = null;
                openBrkLevel = 0;
                prePriority = new Operator[MAX_BRACKET_LEVEL + 1];
                curPriority = new Operator[MAX_BRACKET_LEVEL + 1];
                curOperand = "0";
                expressionTB.Text = bracketTime_lb.Text = "";
                mainExp = new StringBuilder();
                pre_bt = -1;
                inv_ChkBox.Checked = fe_ChkBox.Checked = false;
                bracketExp = new string[MAX_BRACKET_LEVEL + 1];
                NewPriorityExpression();
            }
            if (bin_digit != null)
            {
                for (int i = 0; i < 16; i++) bin_digit[i].Text = "0000";
                ScreenToPanel();
            }
        }
        /// <summary>
        /// di chuyển form mà không cần đưa con trỏ lên title bar
        /// </summary>
        private void MoveFormWithoutMouseAtTitleBar(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Common.ReleaseCapture();
                Common.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }
        /// <summary>
        /// hiển thị số lên màn hình
        /// </summary>
        private void DisplayToScreen()
        {
            if (pex != null)
            {
                scr_lb.TextChanged -= scr_lb_TextChanged;
                scr_lb.Text = pex.Message;
                scr_lb.Font = new Font("Consolas", 9.75F);
                scr_lb.TextChanged += scr_lb_TextChanged;
                return;
            }
            if (programmerMI.Checked) // nhóm cho form programmer
            {
                if (digitGroupingMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = Common.Group(screenStr);
                    else if (octRB.Checked) scr_lb.Text = Common.Group(screenStr, " ", 3);
                    else if (binRB.Checked || hexRB.Checked) scr_lb.Text = Common.Group(screenStr, " ", 4);
                }
                else
                {
                    scr_lb.Text = screenStr.ToString();
                }
            }
            else    // nhóm cho form non-programmer
            {
                screenStr = screenStr.Replace(".", Common.DecimalSeparator).Replace(",", Common.DecimalSeparator);
                if (fe_ChkBox.Checked)
                {
                    scr_lb.Text = ((BigNumber)screenStr.ToString()).ToString();
                    return;	// không cần quan tâm đến digitGroupingMI nữa
                }
                if (!digitGroupingMI.Checked || screenStr.ToString().ToUpper().Contains("E"))
                {
                    scr_lb.Text = screenStr.ToString();
                }
                else
                {
                    scr_lb.Text = Common.Group(screenStr);
                }
            }
            if (hisDGV.Visible) hisDGV.Focus();
            if (staDGV.Visible) staDGV.Focus();
        }
        /// <summary>
        /// hàm digit grouping
        /// </summary>
        /// <param name="isLoaded">load lần đầu tiên</param>
        private void digitLoad(bool isLoaded)
        {
            digitGroupingMI.Checked = !digitGroupingMI.Checked;
            currentConfig[Config._01_UseSep] = digitGroupingMI.Checked ? 1 : 0;

            if (!isLoaded) propertiesChange = true;
            SetMemoryLabelVisible();

            GroupWorksheetsFunctionResult(toTB, unitconvPN);
            GroupWorksheetsFunctionResult(workSheetResultTB, workSheetPN);

            if (!programmerMI.Checked)
            {
                if (Common.IsNumber(screenStr.ToString())) DisplayToScreen();
            }
            else
            {
                if (digitGroupingMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = Common.Group(screenStr, Common.GroupSeparator, 3);
                    if (octRB.Checked) scr_lb.Text = Common.Group(screenStr, " ", 3);
                    if (binRB.Checked || hexRB.Checked) scr_lb.Text = Common.Group(screenStr, " ", 4);
                }
                else
                {
                    if (decRB.Checked) scr_lb.Text = scr_lb.Text.Replace(Common.GroupSeparator, "");
                    if (octRB.Checked || binRB.Checked || hexRB.Checked)
                        scr_lb.Text = scr_lb.Text.Replace(" ", "");
                }
            }
        }
        /// <summary>
        /// nhóm kết quả tính được ở các textbox extra function
        /// </summary>
        /// <param name="tbWaterMark">textbox kết quả</param>
        /// <param name="pn">panel chứa kết quả đó</param>
        private void GroupWorksheetsFunctionResult(TextBox tb, IPanel pn)
        {
            if (Common.IsNumber(tb.Text) && pn.Visible)
            {
                if (digitGroupingMI.Checked)
                {
                    if (!tb.Text.Contains("E")) tb.Text = Common.Group(tb.Text);
                }
                else tb.Text = tb.Text.Replace(Common.GroupSeparator, "");
            }
        }
        /// <summary>
        /// thay đổi số cho cụm ngoặc
        /// after press "2 * (3 + (4 + 5)", then press a number (eg: press 7)
        /// it will fix the expression to "2 + (3 * 7"
        /// </summary>
        private void FixNumberWhenChange()
        {
            // neu phim truoc do khong phai la phim mo ngoac - WHY???
            if (expAdded/* && pre_bt != 152*/)
            {
                mainExp = new StringBuilder(mainExp.ToString().Substring(0, mainExp.Length - curOperand.Length));
                expAdded = false;
                expressionTB.Text = mainExp.ToString().Trim();
                if (openBrkLevel > 0)
                {
                    bracketExp[openBrkLevel] = bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - curOperand.Length);
                }
                else
                {
                    bracketExp[0] = mainExp.ToString();
                }
            }
        }
        /// <summary>
        /// nhập số cho form non-programmer
        /// </summary>
        private void numinput(object sender)
        {
            if (prePriority[openBrkLevel].Index < 0 && curPriority[openBrkLevel].Index < 0 && isFuncClicked)
            {
                equal_Click(equalBT, null);
            }
            if (pex != null) return;

            var bt = sender as Button;
            int index = bt.TabIndex;
            string strTemp = "";
            if (index < 10)  // 0 đến 9
            {
                if (!confirmNumber)
                {
                    expAdded = false;
                    if (screenStr.ToString().Contains("e"))
                    {
                        if (screenStr.ToString().EndsWith("e+0")) screenStr = screenStr.Replace("e+0", "e+" + index.ToString());
                        else if (screenStr.ToString().EndsWith("e-0")) screenStr = screenStr.Replace("e-0", "e-" + index.ToString());
                        else screenStr = screenStr.Append(index.ToString());
                        if (screenStr.Length >= 56) screenStr = new StringBuilder(screenStr.ToString(0, screenStr.Length - 1));
                    }
                    else
                    {
                        if (screenStr != new StringBuilder("0")) strTemp = screenStr + index.ToString();
                        else
                        {
                            confirmNumber = true; 
                            pre_bt = index; 
                            return;
                        }
                        if (strTemp.Length < ((standardMI.Checked || statisticsMI.Checked) ? 16 : 42)
                            && (BigNumber)strTemp < "1e32") screenStr = new StringBuilder(strTemp);
                        else return;
                    }
                    isFuncClicked = false;
                }
                else
                {
                    screenStr = new StringBuilder(index.ToString());
                    FixNumberWhenChange();
                    isFuncClicked = false;
                }
                curOperand = screenStr.ToString();
            }
            if (index == 10 && pex == null)    // dấu thập phân
            {
                // chưa có dấu thập phân thì thêm vào, không có thì thôi
                if (confirmNumber)
                {
                    screenStr = new StringBuilder().AppendFormat("0{0}", Common.DecimalSeparator);
                    if (expAdded)
                    {
                        expressionTB.Text = mainExp.ToString().Substring(0, mainExp.Length - curOperand.Length);
                        mainExp = new StringBuilder(expressionTB.Text);
                    }
                    else expressionTB.Text = mainExp.ToString();
                }
                if (!screenStr.ToString().Contains(Common.DecimalSeparator) && !screenStr.ToString().Contains("e"))
                {
                    screenStr = screenStr.Append(Common.DecimalSeparator);
                    //if (!Common.IsNumber(curOperand))
                    //  curOperand = curOperand.Insert(curOperand.IndexOf(")"), Common.DecimalSeparator);
                }
                expAdded = false;

                FixNumberWhenChange();
                curOperand = screenStr.ToString();

                isFuncClicked = false;
            }
            if (index == 11 && pex == null)    // nut doi dau
            {
                if (!confirmNumber)
                {
                    if (screenStr.ToString().Contains("e+")) 
                    { 
                        screenStr = screenStr.Replace("e+", "e-"); 
                        curOperand = screenStr.ToString(); 
                        goto breakpoint; 
                    }
                    if (screenStr.ToString().Contains("e-")) 
                    { 
                        screenStr = screenStr.Replace("e-", "e+");
                        curOperand = screenStr.ToString(); 
                        goto breakpoint; 
                    }
                }
                if (screenStr.StartsWith("-"))
                {
                    screenStr = new StringBuilder(screenStr.ToString(1, screenStr.Length - 1));
                    curOperand = screenStr.ToString();
                }
                else
                {
                    if (screenStr != new StringBuilder("0")) screenStr = new StringBuilder("-").Append(screenStr.ToString());
                    curOperand = screenStr.ToString();
                }

                pre_bt = index;
                DisplayToScreen();
                return;
            }
            breakpoint: confirmNumber = screenStr == new StringBuilder("0");
            pre_bt = index;
            DisplayToScreen();
        }
        /// <summary>
        /// nhập số cho form programmer
        /// </summary>
        private void numinput_pro(object sender)
        {
            var bt = sender as Button;
            if (!bt.Enabled) return;
            int index = bt.TabIndex;
            if (index < 10) // 0 đến 9
            {
                string strTemp = "";
                if (decRB.Checked)
                {
                    if (!confirmNumber)
                    {
                        strTemp = screenStr + index.ToString();
                        if (BigNumber.Two.Pow(SizeBin - 1) >= strTemp) // strTemp chua vuot qua gia tri lon nhat
                            screenStr = screenStr.Append(index.ToString());
                        else return;
                    }
                    else screenStr = new StringBuilder(index.ToString());
                }
                else
                {
                    if (!confirmNumber)
                    {
                        strTemp = screenStr + index.ToString();
                    }
                    else strTemp = index.ToString();
                    if (binRB.Checked)
                    {
                        if (strTemp.Length <= SizeBin) screenStr = new StringBuilder(strTemp);
                        else return;
                    }
                    if (hexRB.Checked)
                    {
                        if (strTemp.Length <= SizeBin / 4) screenStr = new StringBuilder(strTemp);
                        else return;
                    }
                    if (octRB.Checked)
                    {
                        string max = "";
                        switch (SizeBin)
                        {
                            case 08: max = "377";
                                break;
                            case 16: max = "177777";
                                break;
                            case 32: max = "37777777777";
                                break;
                            case 64: max = "1777777777777777777777";
                                break;
                        }
                        if ((BigNumber)strTemp <= max) screenStr = new StringBuilder(strTemp);
                        else return;
                    }
                }
                ScreenToPanel();
                curOperand = decRB.Value;
                confirmNumber = screenStr == new StringBuilder("0");
                pre_bt = index;
            }
            if (index == 11)    // dấu âm
            {
                bool signNum = (int)currentConfig[Config._11_SignInteger] == 1;
                if (signNum)   // su dung so nguyen co dau
                {
                    if (screenStr.StartsWith("-")) { screenStr = new StringBuilder(screenStr.ToString(1, screenStr.Length - 1)); ScreenToPanel(); }
                    else
                    {
                        if (screenStr == new StringBuilder("0")) return;
                        //quy doi ra he 10, lay phan bu voi 256, 65536,... roi tinh nguoc lai ve he cu
                        if (binRB.Checked)
                        {
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, signNum);
                            screenStr = new StringBuilder(binRB.Value);
                        }
                        if (octRB.Checked)
                        {
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, signNum);
                            screenStr = new StringBuilder(octRB.Value);
                            binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, signNum);
                        }
                        if (decRB.Checked)
                        {
                            decRB.Value = string.Format("-{0}", screenStr);
                            screenStr = new StringBuilder(decRB.Value);
                            binRB.Value = Binary.dec_to_other(decRB.Value, 2, SizeBin, signNum);
                        }
                        if (hexRB.Checked)
                        {
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, signNum);
                            screenStr = new StringBuilder(hexRB.Value);
                            binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, signNum);
                        }
                        ScreenToPanel(true);
                    }
                }
                else    // su dung so nguyen khong dau
                {
                    if (screenStr.StartsWith("-")) screenStr = new StringBuilder(screenStr.ToString(1, screenStr.Length - 1));
                    else
                    {
                        if (screenStr == new StringBuilder("0")) return;
                        screenStr = new StringBuilder((BigNumber.Two.Pow(SizeBin) - decRB.Value).StrValue);
                        if (!decRB.Checked)
                        {
                            if (binRB.Checked) screenStr = new StringBuilder(Binary.dec_to_other(screenStr.ToString(), 02, SizeBin, signNum));
                            if (octRB.Checked) screenStr = new StringBuilder(Binary.dec_to_other(screenStr.ToString(), 08, SizeBin, signNum));
                            if (hexRB.Checked) screenStr = new StringBuilder(Binary.dec_to_other(screenStr.ToString(), 16, SizeBin, signNum));
                        }
                    }
                    ScreenToPanel();
                }
                curOperand = screenStr.ToString();
                pre_bt = index;
                confirmNumber = true;
                return;
            }
        }
        /// <summary>
        /// các nút từ A đến F
        /// </summary>
        private void buttonAF(string text)
        {
            if (!confirmNumber)
            {
                string strTemp = screenStr + text;
                if (strTemp.Length <= SizeBin / 4) screenStr = new StringBuilder(strTemp);
                else return;    // khong lam clgh
            }
            else
            {
                screenStr = new StringBuilder(text);
            }
            confirmNumber = false;
            prcmdkey = true;
            ScreenToPanel();
        }

        double leftNum = 0;
        /// <summary>
        /// các toán tử +-*/ của form standard
        /// </summary>
        private void std_operation(int index)
        {
            prePriority[0] = curPriority[0].Clone();
            string oper = "";
            switch (index)
            {
                case 12: oper = "+";
                    break;
                case 13: oper = "-";
                    break;
                case 14: oper = "*";
                    break;
                case 15: oper = "/";
                    break;
            }
            curPriority[0] = new Operator(oper, 6);
            if (prePriority[0].Index >= 0)
            {
                if (Array.IndexOf(new int[] { 12, 13, 14, 15 }, pre_bt) >= 0)    // pre_bt la cac nut co thuoc mang tren
                {
                    mainExp = new StringBuilder().AppendFormat("{0}{1} ", mainExp.ToString().Substring(0, mainExp.Length - 2), oper);
                    goto breakpoint;
                }
                if (!expAdded) mainExp = mainExp.Append(curOperand);
                mainExp = mainExp.AppendFormat(" {0} ", oper);

                double right = double.Parse(screenStr.ToString());
                string scrStr = null;
                switch (prePriority[0].PText)
                {
                    case "+": scrStr = (leftNum + right).ToString();
                        break;
                    case "-": scrStr = (leftNum - right).ToString();
                        break;
                    case "*": scrStr = (leftNum * right).ToString();
                        break;
                    case "/": if (screenStr != new StringBuilder("0"))
                        {
                            scrStr = (leftNum / right).ToString();
                        }
                        else
                        {
                            pex = new DivideByZeroException("Cannot divided by zero");
                        }
                        break;
                }
                screenStr = new StringBuilder(scrStr);
            }
            else    // prePriority[0].Value < 0 && curPriority[0].Value < 0
            {
                if (isFuncClicked)
                {
                    if (!expAdded) mainExp = mainExp.AppendFormat("{0} {1} ", curOperand, oper);
                    else mainExp = new StringBuilder().AppendFormat("{0} {1} ", curOperand, oper);
                }
                else
                {
                    mainExp = mainExp.AppendFormat(" {0} {1} ", screenStr, oper);
                }
                leftNum = double.Parse(screenStr.ToString());
            }       // prePriority[0].Value >= 0
            leftNum = double.Parse(screenStr.ToString());

            breakpoint: confirmNumber = true;
            expressionTB.Text = mainExp.ToString().Trim();
            DisplayToScreen();
            pre_bt = index;
            curOperand = screenStr.ToString();
            isFuncClicked = false;
            expAdded = false;
        }

        int[] lowestOperator = new int[MAX_BRACKET_LEVEL + 1];
        string[][] priorityExpression = new string[MAX_BRACKET_LEVEL + 1][];
        Operator[] curPriority = new Operator[MAX_BRACKET_LEVEL + 1];
        Operator[] prePriority = new Operator[MAX_BRACKET_LEVEL + 1];
        /// <summary>
        /// các toán tử +-*/ của form scientific
        /// </summary>
        private void sci_operation(int index)
        {
            prePriority[openBrkLevel] = curPriority[openBrkLevel].Clone();
            string oper = "";
            if (index == 12) oper = "+";
            if (index == 13) oper = "-";
            if (index == 14) oper = "*";
            if (index == 15) oper = "/";
            if (index == 33) oper = "^";
            if (index == 43) oper = "mod";
            if (index == 34) oper = "yroot";
            curPriority[openBrkLevel] = new Operator(oper, 6);
            if (prePriority[openBrkLevel].Index >= 0)
            {
                // neu phim truoc day la 1 phep tinh
                if (Array.IndexOf(new int[] { 12, 13, 14, 15, 33, 34, 43 }, pre_bt) >= 0)    // pre_bt la cac nut co thuoc mang tren
                {
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] = curOperand + oper;
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] = null;
                    string format = "({0}) {1} ";
                    if (curPriority[openBrkLevel].Index <= prePriority[openBrkLevel].Index ||
                        (mainExp[0] == '(' && mainExp[mainExp.Length - prePriority[openBrkLevel].PText.Length - 3] == ')'))
                            format = "{0} {1} ";
                    mainExp = new StringBuilder().AppendFormat(format, mainExp.ToString().Substring(0, mainExp.Length - prePriority[openBrkLevel].PText.Length - 2), oper);
                    bracketExp[openBrkLevel] = string.Format(format, bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - prePriority[openBrkLevel].PText.Length - 2), oper);
                    if (curPriority[openBrkLevel].Index < lowestOperator[openBrkLevel]) lowestOperator[openBrkLevel] = curPriority[openBrkLevel].Index;
                    goto breakpoint;    // thoat tai day
                }

                // biểu thức chính và biểu thức trong ngoặc ở level ngoặc hiện tại
                if (!expAdded)
                {
                    mainExp = mainExp.Append(curOperand);
                    bracketExp[openBrkLevel] += curOperand;
                }

                // biểu thức ưu tiên
                // hiện tại có mức ưu tiên CAO hơn phép tính trước đó
                if (prePriority[openBrkLevel].Index < curPriority[openBrkLevel].Index)
                {
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] = curOperand;
                }
                // hiện tại có mức ưu tiên THẤP hơn phép tính trước đó
                else if (curPriority[openBrkLevel].Index < prePriority[openBrkLevel].Index)
                {
                    // hoàn thiện biểu thức ưu tiên có thứ tự cao hơn của phép tính TRƯỚC ĐÓ
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] += curOperand;

                    // lấy kết quả của phép tính có thứ tự cao hơn TRƯỚC ĐÓ, trả về screenStr
                    pex = parser.EvaluateSci(SimplyFactorialExpression(priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index]));
                    screenStr = new StringBuilder(parser.StringResult);

                    // sau đó, gán biểu thức ưu tiên của phép tính TRƯỚC ĐÓ trở về rỗng
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] = null;

                    // biểu thức ưu tiên của phép tính HIỆN TẠI thêm kết quả của phép tính có thứ tự cao hơn trước đó
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += screenStr.ToString();

                    // tính giá trị của biểu thức ưu tiên của phép tính HIỆN TẠI
                    pex = parser.EvaluateSci(SimplyFactorialExpression(priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index]));
                    screenStr = new StringBuilder(parser.StringResult);

                    // nếu phép tính hiện tại có mức ưu tiên là thấp nhất trong biểu thức thì tính luôn cả biểu thức rồi gán vào biến screenStr
                    if (curPriority[openBrkLevel].Index <= lowestOperator[openBrkLevel])
                    {
                        if (openBrkLevel == 0)  // đang ở biểu thức chính
                        {
                            pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()));
                        }
                        else
                        {
                            //pex = parser.EvaluateSci(bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - 1));
                            pex = parser.EvaluateSci(SimplyFactorialExpression(bracketExp[openBrkLevel]));
                        }
                        screenStr = new StringBuilder(parser.StringResult);
                    }
                }
                // hiện tại có mức ưu tiên BẰNG phép tính trước đó
                else
                {
                    // hoàn thiện biểu thức ưu tiên HIỆN TẠI
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += curOperand;

                    // tính kết quả của phép tính này, trả về screenStr
                    pex = parser.EvaluateSci(SimplyFactorialExpression(priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index]));
                    screenStr = new StringBuilder(parser.StringResult);

                    // nếu phép tính hiện tại có mức ưu tiên thấp nhất trong cả biểu thức thì tính luôn cả biểu thức rồi gán vào biến screenStr
                    if (curPriority[openBrkLevel].Index <= lowestOperator[openBrkLevel])
                    {
                        if (openBrkLevel == 0)  // đang ở biểu thức chính
                        {
                            pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()));
                        }
                        else
                        {
                            pex = parser.EvaluateSci(SimplyFactorialExpression(bracketExp[openBrkLevel]));
                        }
                        screenStr = new StringBuilder(parser.StringResult);
                    }
                }

                // hoàn thiện nốt các biểu thức bằng cách thêm dấu phép tính cho biểu thức đó
                priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += string.Format(" {0} ", oper);
                mainExp = mainExp.AppendFormat(" {0} ", oper);
                bracketExp[openBrkLevel] += string.Format(" {0} ", oper);
            }
            else    // prePriority[openBrkLevel].Value < 0
            {
                lowestOperator[openBrkLevel] = curPriority[openBrkLevel].Index;

                // biểu thức chính và biểu thức trong ngoặc
                if (!expAdded)
                {
                    mainExp = mainExp.Append(curOperand);
                    bracketExp[openBrkLevel] += curOperand;
                    //priorityExpression[openBrkLevel][curPriority[openBrkLevel].Value] += curOperand;
                }
                priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += string.Format("{0} {1} ", curOperand, oper);
                mainExp = mainExp.AppendFormat(" {0} ", oper);
                bracketExp[openBrkLevel] += string.Format(" {0} ", oper);
            }       // prePriority[openBrkLevel].Value >= 0

            expAdded = false;
            breakpoint: expressionTB.Text = mainExp.ToString().Trim();
            DisplayToScreen();
            pre_bt = index;
            curOperand = screenStr.ToString();
            isFuncClicked = false;
            confirmNumber = true;
        }
        /// <summary>
        /// các toán tử +-*/ của form programmer
        /// </summary>
        private void pro_operation(int index)
        {
            prePriority[openBrkLevel] = curPriority[openBrkLevel].Clone();
            string oper = "";
            if (index == 012) oper = "+";
            if (index == 013) oper = "-";
            if (index == 014) oper = "*";
            if (index == 015) oper = "/";
            if (index == 211) oper = "%";
            if (index == 150) oper = "<<";
            if (index == 151) oper = ">>";
            if (index == 160) oper = "&";
            if (index == 161) oper = "^";
            if (index == 162) oper = "|";

            curPriority[openBrkLevel] = new Operator(oper, 1);

            if (prePriority[openBrkLevel].Index >= 0)
            {
                // nếu phím trước đó là 1 phép tính
                if (Array.IndexOf(new int[] { 12, 13, 14, 15, 150, 151, 160, 161, 162, 211 }, pre_bt) >= 0)// pre_bt la cac nut co thuoc mang tren
                {
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] = decRB.Value + oper;
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] = null;
                    string format = "({0}){1}";
                    if (curPriority[openBrkLevel].Index <= prePriority[openBrkLevel].Index ||
                        (mainExp[0] == '(' && mainExp[mainExp.Length - prePriority[openBrkLevel].PText.Length - 1] == ')'))
                        format = "{0}{1}";
                    mainExp = new StringBuilder().AppendFormat(format, mainExp.ToString().Substring(0, mainExp.Length - prePriority[openBrkLevel].PText.Length), oper);
                    bracketExp[openBrkLevel] = string.Format(format, bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - prePriority[openBrkLevel].PText.Length), oper);
                    if (curPriority[openBrkLevel].Index < lowestOperator[openBrkLevel]) lowestOperator[openBrkLevel] = curPriority[openBrkLevel].Index;
                    goto breakpoint;    // thoat tai day
                }

                // biểu thức chính và biểu thức trong ngoặc ở level ngoặc hiện tại
                mainExp = mainExp.Append(curOperand);
                bracketExp[openBrkLevel] += curOperand;

                // biểu thức ưu tiên
                // hiện tại có mức ưu tiên CAO hơn phép tính trước đó
                if (prePriority[openBrkLevel].Index < curPriority[openBrkLevel].Index)
                {
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] = curOperand;
                }
                // hiện tại có mức ưu tiên THẤP hơn phép tính trước đó
                else if (curPriority[openBrkLevel].Index < prePriority[openBrkLevel].Index)
                {
                    // hoàn thiện biểu thức ưu tiên có thứ tự cao hơn của phép tính TRƯỚC ĐÓ
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] += curOperand;

					// lấy kết quả của phép tính có thứ tự cao hơn TRƯỚC ĐÓ, trả về screenStr
                    pex = parser.EvaluatePro(priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index], (int)currentConfig[Config._11_SignInteger] == 1);
                    screenStr = new StringBuilder(parser.StringResult);

                    // sau đó, gán biểu thức ưu tiên của phép tính TRƯỚC ĐÓ trở về rỗng
                    priorityExpression[openBrkLevel][prePriority[openBrkLevel].Index] = null;

                    // biểu thức ưu tiên của phép tính HIỆN TẠI thêm kết quả của phép tính có thứ tự cao hơn trước đó
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += screenStr.ToString();

                    // tính giá trị của biểu thức ưu tiên của phép tính HIỆN TẠI
                    pex = parser.EvaluatePro(priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index], (int)currentConfig[Config._11_SignInteger] == 1);
                    screenStr = new StringBuilder(parser.StringResult);

                    // nếu phép tính hiện tại có mức ưu tiên là thấp nhất trong biểu thức thì tính luôn cả biểu thức rồi gán vào biến screenStr
                    if (curPriority[openBrkLevel].Index <= lowestOperator[openBrkLevel])
                    {
                        if (openBrkLevel == 0)  // dang o bieu thuc chinh
                        {
                            //pex = parser.EvaluatePro(mainExp.Substring(0, mainExp.Length - 1));
                            pex = parser.EvaluatePro(mainExp.ToString(), (int)currentConfig[Config._11_SignInteger] == 1);
                        }
                        else
                        {
                            //pex = parser.EvaluatePro(bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - 1));
                            pex = parser.EvaluatePro(bracketExp[openBrkLevel], (int)currentConfig[Config._11_SignInteger] == 1);
                        }
                        screenStr = new StringBuilder(parser.StringResult);
                    }
                }
                // hiện tại có mức ưu tiên BẰNG phép tính trước đó
                else
                {
                    // hoàn thiện biểu thức ưu tiên HIỆN TẠI
                    priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += curOperand;

                    // tính kết quả của phép tính này, trả về screenStr
                    pex = parser.EvaluatePro(priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index], (int)currentConfig[Config._11_SignInteger] == 1);
                    screenStr = new StringBuilder(parser.StringResult);

                    // nếu phép tính hiện tại có mức ưu tiên thấp nhất trong cả biểu thức thì tính luôn cả biểu thức rồi gán vào biến screenStr
                    if (curPriority[openBrkLevel].Index <= lowestOperator[openBrkLevel])
                    {
                        if (openBrkLevel == 0)
                        {
                            //pex = parser.EvaluatePro(mainExp.Substring(0, mainExp.Length - 1));
                            pex = parser.EvaluatePro(mainExp.ToString(), (int)currentConfig[Config._11_SignInteger] == 1);
                        }
                        else
                        {
                            pex = parser.EvaluatePro(bracketExp[openBrkLevel], (int)currentConfig[Config._11_SignInteger] == 1);
                        }
                        screenStr = new StringBuilder(parser.StringResult);
                    }
                }

                // hoàn thiện nốt các biểu thức bằng cách thêm dấu phép tính cho biểu thức đó
                priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += oper;
                mainExp = mainExp.Append(oper);
                bracketExp[openBrkLevel] += oper;
            }
            else    // prePriority[openBrkLevel].Value < 0
            {
                lowestOperator[openBrkLevel] = curPriority[openBrkLevel].Index;

                // biểu thức chính và biểu thức trong ngoặc và biểu thức ưu tiên
                if (!expAdded)
                {
                    mainExp = mainExp.Append(curOperand);
                    bracketExp[openBrkLevel] += curOperand;
                }
                priorityExpression[openBrkLevel][curPriority[openBrkLevel].Index] += curOperand + oper;
                mainExp = mainExp.Append(oper);
                bracketExp[openBrkLevel] += oper;
            }       // prePriority[openBrkLevel].Value >= 0

            breakpoint: expAdded = false;
            DisplayToScreen();
            //curOperand = screenStr.ToString();
            pre_bt = index;
            confirmNumber = true;
        }
        /// <summary>
        /// tính lại biểu thức giai thừa
        /// </summary>
        /// <param name="exp">biểu thức giai thừa</param>
        /// <returns>kết quả sau khi tính</returns>
        private string SimplyFactorialExpression(string exp)
        {
            if (!exp.Contains("fact")) return exp;
            int searchIndex = 0;
            int open = 0;
            //open = exp.IndexOf("fact(", searchIndex) + 4;
            while ((open = exp.IndexOf("fact(", searchIndex) + 4) >= 4)
            {
                string subExp = "";
                int close = Common.GetCloseBracketIndex(exp, open, ref subExp);
                searchIndex = close + 1;
                parser.EvaluateSci(subExp);
                int idex = Array.IndexOf(factDict.SelectValue(), parser.StringResult);
                if (idex >= 0)
                {
                    exp = exp.Replace(string.Format("fact({0})", subExp), factDict[idex].Result);
                }
                //open = exp.IndexOf("fact(", searchIndex) + 4;
            }

            return exp;
        }

        CancelOperation co;
        //
        // tính giai thừa số lớn của hàm (bấm phím n!)
        //
        private void mWorker_DoWorkPrevFunc(object sender, DoWorkEventArgs e)
        {
            if (mWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            pex = parser.EvaluateSci(curOperand);
        }
        //
        // tính giai thừa số lớn của biểu thức (sau khi ấn dấu bằng, tính lại biểu thức,...)
        //
        private void mWorker_DoWorkExpr(object sender, DoWorkEventArgs e)
        {
            lock (this)
            {
                if (mWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()));
            }
        }
        //
        // kết thúc tính giai thừa số lớn của biểu thức
        //
        private void mWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this)
            {
                if (co != null) co.Finished = true;
                mWorker.Dispose();
                co.Dispose();
            }
        }
        //
        // huỷ hàm tính giai thừa số lớn
        //
        private void co_DoCancel()
        {
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
                mWorker.Dispose();
                pex = new Exception("Operation cancelled");
                co.Dispose();
            }
        }
        /// <summary>
        /// xử lý nút lên hoặc xuống hoặc lăn chuột
        /// </summary>
        /// <param name="dir">hướng di chuyển (1=lên, -1=xuống)</param>
        /// <param name="isKeyBoardUpDown">check hàm có phải của sự kiện bấm phím hay không</param>
        private void ProcessUpOrDown(int dir, bool isKeyBoardUpDown)
        {
            if (hisDGV.CurrentCell != null && (standardMI.Checked || scientificMI.Checked))
            {
                if ((dir == 1 && hisDGV.CurrentCell.RowIndex >= 1) || (dir == -1 && hisDGV.CurrentCell.RowIndex <= hisDGV.RowCount - 2))
                {
                    evaluateExpression(hisDGV.CurrentRow.Index - dir, false);
                    if (!isKeyBoardUpDown) hisDGV[0, hisDGV.CurrentRow.Index - dir].Selected = true;
                    curOperand = screenStr.ToString();
                }
            }
            if (staDGV.CurrentCell != null && statisticsMI.Checked)
            {
                if ((dir == 1 && staDGV.CurrentCell.RowIndex >= 1) || (dir == -1 && staDGV.CurrentCell.RowIndex <= staDGV.RowCount - 2))
                {
                    //screenStr = staDGV[0, staDGV.CurrentRow.Value - dir].Value.ToString();
                    if (!isKeyBoardUpDown) staDGV[0, staDGV.CurrentRow.Index - dir].Selected = true;
                    DisplayToScreen();
                    DisplayCountNumber();
                }
            }
        }

        private void workSheetPN_FocusEvent(object sender, EventArgs e)
        {
            typeWorkSheetCB.Focus();
        }
        /// <summary>
        /// hàm tính tổng
        /// </summary>
        /// <param name="isSquare">tham số kiểm tra xem có tính tổng bình phương các số hay không</param>
        /// <returns>tổng</returns>
        private double sum(bool isSquare)
        {
            double sum = 0;
            if (staDGV.RowCount == 0) return 0;
            for (int i = 0; i < staDGV.RowCount; i++)
            {
                double frq = double.Parse(staDGV[0, i].ToolTipText.Substring(staDGV[0, i].ToolTipText.LastIndexOf(' ')));
                double number = double.Parse(staDGV[0, i].Value.ToString());
                if (isSquare) sum += number * number * frq;
                else sum += number * frq;
            }
            return sum;
        }
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(calc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.num1BT = new System.Windows.Forms.Button();
            this.num0BT = new System.Windows.Forms.Button();
            this.addbt = new System.Windows.Forms.Button();
            this.minusbt = new System.Windows.Forms.Button();
            this.mulbt = new System.Windows.Forms.Button();
            this.divbt = new System.Windows.Forms.Button();
            this.equalBT = new System.Windows.Forms.Button();
            this.backspacebt = new System.Windows.Forms.Button();
            this.ce = new System.Windows.Forms.Button();
            this.changesignBT = new System.Windows.Forms.Button();
            this.memclearBT = new System.Windows.Forms.Button();
            this.mem_store = new System.Windows.Forms.Button();
            this.mem_recall = new System.Windows.Forms.Button();
            this.clearbt = new System.Windows.Forms.Button();
            this.mem_add_bt = new System.Windows.Forms.Button();
            this.mem_minus_bt = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.close_bracket = new System.Windows.Forms.Button();
            this.open_bracket = new System.Windows.Forms.Button();
            this._10x_bt = new System.Windows.Forms.Button();
            this.nvx_bt = new System.Windows.Forms.Button();
            this.xn_bt = new System.Windows.Forms.Button();
            this.log_bt = new System.Windows.Forms.Button();
            this._3vx_bt = new System.Windows.Forms.Button();
            this.x3_bt = new System.Windows.Forms.Button();
            this.x2_bt = new System.Windows.Forms.Button();
            this.pi_bt = new System.Windows.Forms.Button();
            this.exp_bt = new System.Windows.Forms.Button();
            this.modsciBT = new System.Windows.Forms.Button();
            this.ln_bt = new System.Windows.Forms.Button();
            this.dms_bt = new System.Windows.Forms.Button();
            this.tanh_bt = new System.Windows.Forms.Button();
            this.cosh_bt = new System.Windows.Forms.Button();
            this.int_bt = new System.Windows.Forms.Button();
            this.tan_bt = new System.Windows.Forms.Button();
            this.sinh_bt = new System.Windows.Forms.Button();
            this.btFactorial = new System.Windows.Forms.Button();
            this.XorBT = new System.Windows.Forms.Button();
            this.notBT = new System.Windows.Forms.Button();
            this.AndBT = new System.Windows.Forms.Button();
            this.RshBT = new System.Windows.Forms.Button();
            this.RoRBT = new System.Windows.Forms.Button();
            this.LshBT = new System.Windows.Forms.Button();
            this.or_BT = new System.Windows.Forms.Button();
            this.RoLBT = new System.Windows.Forms.Button();
            this.modproBT = new System.Windows.Forms.Button();
            this.sigmax2BT = new System.Windows.Forms.Button();
            this.sigman_1BT = new System.Windows.Forms.Button();
            this.AddstaBT = new System.Windows.Forms.Button();
            this.xcross = new System.Windows.Forms.Button();
            this.sigmaxBT = new System.Windows.Forms.Button();
            this.sigmanBT = new System.Windows.Forms.Button();
            this.CAD = new System.Windows.Forms.Button();
            this.x2cross = new System.Windows.Forms.Button();
            this.inv_ChkBox = new System.Windows.Forms.CheckBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.dnBT = new System.Windows.Forms.Button();
            this.upBT = new System.Windows.Forms.Button();
            this.fe_ChkBox = new System.Windows.Forms.CheckBox();
            this.bracketTime_lb = new System.Windows.Forms.Label();
            this.cos_bt = new System.Windows.Forms.Button();
            this.sin_bt = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.viewMI = new System.Windows.Forms.MenuItem();
            this.standardMI = new System.Windows.Forms.MenuItem();
            this.scientificMI = new System.Windows.Forms.MenuItem();
            this.programmerMI = new System.Windows.Forms.MenuItem();
            this.statisticsMI = new System.Windows.Forms.MenuItem();
            this.sepMI1 = new System.Windows.Forms.MenuItem();
            this.historyMI = new System.Windows.Forms.MenuItem();
            this.digitGroupingMI = new System.Windows.Forms.MenuItem();
            this.sepMI2 = new System.Windows.Forms.MenuItem();
            this.basicMI = new System.Windows.Forms.MenuItem();
            this.unitConversionMI = new System.Windows.Forms.MenuItem();
            this.dateCalculationMI = new System.Windows.Forms.MenuItem();
            this.worksheetsMI = new System.Windows.Forms.MenuItem();
            this.mortgageMI = new System.Windows.Forms.MenuItem();
            this.vehicleLeaseMI = new System.Windows.Forms.MenuItem();
            this.fe_MPG_MI = new System.Windows.Forms.MenuItem();
            this.feL100_MI = new System.Windows.Forms.MenuItem();
            this.editMI = new System.Windows.Forms.MenuItem();
            this.copyMI = new System.Windows.Forms.MenuItem();
            this.pasteMI = new System.Windows.Forms.MenuItem();
            this.sepMI3 = new System.Windows.Forms.MenuItem();
            this.historyOptionMI = new System.Windows.Forms.MenuItem();
            this.copyHistoryMI = new System.Windows.Forms.MenuItem();
            this.editHistoryMI = new System.Windows.Forms.MenuItem();
            this.reCalculateMI = new System.Windows.Forms.MenuItem();
            this.cancelEditHisMI = new System.Windows.Forms.MenuItem();
            this.clearHistoryMI = new System.Windows.Forms.MenuItem();
            this.datasetMI = new System.Windows.Forms.MenuItem();
            this.copyDatasetMI = new System.Windows.Forms.MenuItem();
            this.editDatasetMI = new System.Windows.Forms.MenuItem();
            this.commitDSMI = new System.Windows.Forms.MenuItem();
            this.cancelEditDSMI = new System.Windows.Forms.MenuItem();
            this.clearDatasetMI = new System.Windows.Forms.MenuItem();
            this.sepMI6 = new System.Windows.Forms.MenuItem();
            this.preferencesMI = new System.Windows.Forms.MenuItem();
            this.helpMI = new System.Windows.Forms.MenuItem();
            this.helpTopicsTSMI = new System.Windows.Forms.MenuItem();
            this.sepMI5 = new System.Windows.Forms.MenuItem();
            this.aboutMI = new System.Windows.Forms.MenuItem();
            this.mainContextMenu = new System.Windows.Forms.ContextMenu();
            this.copyCTMN = new System.Windows.Forms.MenuItem();
            this.pasteCTMN = new System.Windows.Forms.MenuItem();
            this.sepCTMN = new System.Windows.Forms.MenuItem();
            this.showHistoryCTMN = new System.Windows.Forms.MenuItem();
            this.hideHistoryCTMN = new System.Windows.Forms.MenuItem();
            this.clearHistoryCTMN = new System.Windows.Forms.MenuItem();
            this.clearDatasetCTMN = new System.Windows.Forms.MenuItem();
            this.showPreviewPaneCTMN = new System.Windows.Forms.MenuItem();
            this.hidePreviewPaneCTMN = new System.Windows.Forms.MenuItem();
            this.helpCTMN = new System.Windows.Forms.ContextMenu();
            this.hotkeyMI = new System.Windows.Forms.MenuItem();
            this.mWorker = new System.ComponentModel.BackgroundWorker();
            this.openProBT = new System.Windows.Forms.Button();
            this.datecalcPN = new Calculator.IPanel();
            this.tbResult1 = new System.Windows.Forms.TextBox();
            this.tbResult2 = new System.Windows.Forms.TextBox();
            this.calmethodLB = new System.Windows.Forms.Label();
            this.secondDate = new System.Windows.Forms.Label();
            this.dtP2 = new Calculator.IDateTimePicker();
            this.subrb = new System.Windows.Forms.RadioButton();
            this.dtP1 = new Calculator.IDateTimePicker();
            this.addrb = new System.Windows.Forms.RadioButton();
            this.lbResult2 = new System.Windows.Forms.Label();
            this.firstDate = new System.Windows.Forms.Label();
            this.autocal_date = new System.Windows.Forms.CheckBox();
            this.lbResult1 = new System.Windows.Forms.Label();
            this.dateCalculationBT = new System.Windows.Forms.Button();
            this.datemethodCB = new System.Windows.Forms.ComboBox();
            this.yearAddSubLB = new System.Windows.Forms.Label();
            this.monthAddSubLB = new System.Windows.Forms.Label();
            this.dayAddSubLB = new System.Windows.Forms.Label();
            this.periodsDateUD = new Calculator.INumericUpDown();
            this.periodsMonthUD = new Calculator.INumericUpDown();
            this.periodsYearUD = new Calculator.INumericUpDown();
            this.num9BT_PN = new Calculator.IPanel();
            this.num9BT = new System.Windows.Forms.Button();
            this.num7BT_PN = new Calculator.IPanel();
            this.num7BT = new System.Windows.Forms.Button();
            this.num8BT_PN = new Calculator.IPanel();
            this.num8BT = new System.Windows.Forms.Button();
            this.num6BT_PN = new Calculator.IPanel();
            this.num6BT = new System.Windows.Forms.Button();
            this.num5BT_PN = new Calculator.IPanel();
            this.num5BT = new System.Windows.Forms.Button();
            this.num3BT_PN = new Calculator.IPanel();
            this.num3BT = new System.Windows.Forms.Button();
            this.num4BT_PN = new Calculator.IPanel();
            this.num4BT = new System.Windows.Forms.Button();
            this.num2BT_PN = new Calculator.IPanel();
            this.num2BT = new System.Windows.Forms.Button();
            this.percentbt_PN = new Calculator.IPanel();
            this.percent_bt = new System.Windows.Forms.Button();
            this.btdot_PN = new Calculator.IPanel();
            this.btdot = new System.Windows.Forms.Button();
            this.invertbt_PN = new Calculator.IPanel();
            this.invert_bt = new System.Windows.Forms.Button();
            this.sqrtbt_PN = new Calculator.IPanel();
            this.sqrt_bt = new System.Windows.Forms.Button();
            this.btnF_PN = new Calculator.IPanel();
            this.btnF = new System.Windows.Forms.Button();
            this.btnC_PN = new Calculator.IPanel();
            this.btnC = new System.Windows.Forms.Button();
            this.btnE_PN = new Calculator.IPanel();
            this.btnE = new System.Windows.Forms.Button();
            this.btnB_PN = new Calculator.IPanel();
            this.btnB = new System.Windows.Forms.Button();
            this.btnD_PN = new Calculator.IPanel();
            this.btnD = new System.Windows.Forms.Button();
            this.btnA_PN = new Calculator.IPanel();
            this.btnA = new System.Windows.Forms.Button();
            this.PNbinary = new Calculator.IPanel();
            this.screenPN = new Calculator.IPanel();
            this.bin_prvLb = new Calculator.ILabel();
            this.expressionTB = new Calculator.ILabel();
            this.oct_prvLb = new Calculator.ILabel();
            this.mem_lb = new System.Windows.Forms.Label();
            this.dec_prvLb = new Calculator.ILabel();
            this.scr_lb = new System.Windows.Forms.Label();
            this.hex_prvLb = new Calculator.ILabel();
            this.anglePN = new Calculator.IPanel();
            this.graRB = new Calculator.IRadioButton();
            this.degRB = new Calculator.IRadioButton();
            this.radRB = new Calculator.IRadioButton();
            this.gridPanel = new Calculator.IPanel();
            this.countLB = new System.Windows.Forms.Label();
            this.hisDGV = new Calculator.IDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staDGV = new Calculator.IDataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basePN = new Calculator.IPanel();
            this.hexRB = new Calculator.IRadioButton();
            this.decRB = new Calculator.IRadioButton();
            this.octRB = new Calculator.IRadioButton();
            this.binRB = new Calculator.IRadioButton();
            this.unknownPN = new Calculator.IPanel();
            this._byteRB = new Calculator.IRadioButton();
            this.qwordRB = new Calculator.IRadioButton();
            this.dwordRB = new Calculator.IRadioButton();
            this._wordRB = new Calculator.IRadioButton();
            this.unitconvPN = new Calculator.IPanel();
            this.toCB = new Calculator.IComboBox();
            this.typeUnitLB = new System.Windows.Forms.Label();
            this.fromCB = new Calculator.IComboBox();
            this.typeUnitCB = new System.Windows.Forms.ComboBox();
            this.invert_unit = new System.Windows.Forms.Button();
            this.toLB = new System.Windows.Forms.Label();
            this.toTB = new System.Windows.Forms.TextBox();
            this.fromLB = new System.Windows.Forms.Label();
            this.fromTB = new Calculator.ITextBox();
            this.workSheetPN = new Calculator.IPanel();
            this.typeMorgageLB = new System.Windows.Forms.Label();
            this.typeWorkSheetCB = new System.Windows.Forms.ComboBox();
            this.workSheetCalculateBT = new System.Windows.Forms.Button();
            this.workSheetResultTB = new System.Windows.Forms.TextBox();
            this.datecalcPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).BeginInit();
            this.num9BT_PN.SuspendLayout();
            this.num7BT_PN.SuspendLayout();
            this.num8BT_PN.SuspendLayout();
            this.num6BT_PN.SuspendLayout();
            this.num5BT_PN.SuspendLayout();
            this.num3BT_PN.SuspendLayout();
            this.num4BT_PN.SuspendLayout();
            this.num2BT_PN.SuspendLayout();
            this.percentbt_PN.SuspendLayout();
            this.btdot_PN.SuspendLayout();
            this.invertbt_PN.SuspendLayout();
            this.sqrtbt_PN.SuspendLayout();
            this.btnF_PN.SuspendLayout();
            this.btnC_PN.SuspendLayout();
            this.btnE_PN.SuspendLayout();
            this.btnB_PN.SuspendLayout();
            this.btnD_PN.SuspendLayout();
            this.btnA_PN.SuspendLayout();
            this.screenPN.SuspendLayout();
            this.anglePN.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).BeginInit();
            this.basePN.SuspendLayout();
            this.unknownPN.SuspendLayout();
            this.unitconvPN.SuspendLayout();
            this.workSheetPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // num1BT
            // 
            this.num1BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num1BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num1BT.Location = new System.Drawing.Point(12, 194);
            this.num1BT.Name = "num1BT";
            this.num1BT.Size = new System.Drawing.Size(34, 27);
            this.num1BT.TabIndex = 1;
            this.num1BT.TabStop = false;
            this.num1BT.Text = "1";
            this.num1BT.UseVisualStyleBackColor = true;
            this.num1BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num1BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num1BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num1BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num0BT
            // 
            this.num0BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num0BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num0BT.Location = new System.Drawing.Point(12, 226);
            this.num0BT.Name = "num0BT";
            this.num0BT.Size = new System.Drawing.Size(73, 27);
            this.num0BT.TabIndex = 0;
            this.num0BT.TabStop = false;
            this.num0BT.Text = "0";
            this.num0BT.UseVisualStyleBackColor = true;
            this.num0BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num0BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num0BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num0BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // addbt
            // 
            this.addbt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addbt.Location = new System.Drawing.Point(129, 226);
            this.addbt.Name = "addbt";
            this.addbt.Size = new System.Drawing.Size(34, 27);
            this.addbt.TabIndex = 12;
            this.addbt.TabStop = false;
            this.addbt.Text = "+";
            this.addbt.UseVisualStyleBackColor = true;
            this.addbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.addbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.addbt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.addbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // minusbt
            // 
            this.minusbt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minusbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.minusbt.Location = new System.Drawing.Point(129, 194);
            this.minusbt.Name = "minusbt";
            this.minusbt.Size = new System.Drawing.Size(34, 27);
            this.minusbt.TabIndex = 13;
            this.minusbt.TabStop = false;
            this.minusbt.Text = "-";
            this.minusbt.UseVisualStyleBackColor = true;
            this.minusbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.minusbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.minusbt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.minusbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mulbt
            // 
            this.mulbt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mulbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mulbt.Location = new System.Drawing.Point(129, 162);
            this.mulbt.Name = "mulbt";
            this.mulbt.Size = new System.Drawing.Size(34, 27);
            this.mulbt.TabIndex = 14;
            this.mulbt.TabStop = false;
            this.mulbt.Text = "*";
            this.mulbt.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mulbt.UseVisualStyleBackColor = true;
            this.mulbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.mulbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mulbt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mulbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // divbt
            // 
            this.divbt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.divbt.Location = new System.Drawing.Point(129, 130);
            this.divbt.Name = "divbt";
            this.divbt.Size = new System.Drawing.Size(34, 27);
            this.divbt.TabIndex = 15;
            this.divbt.TabStop = false;
            this.divbt.Text = "/";
            this.divbt.UseVisualStyleBackColor = true;
            this.divbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.divbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.divbt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.divbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // equalBT
            // 
            this.equalBT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equalBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.equalBT.Location = new System.Drawing.Point(168, 194);
            this.equalBT.Name = "equalBT";
            this.equalBT.Size = new System.Drawing.Size(34, 59);
            this.equalBT.TabIndex = 16;
            this.equalBT.TabStop = false;
            this.equalBT.Text = "=";
            this.equalBT.UseVisualStyleBackColor = true;
            this.equalBT.Click += new System.EventHandler(this.equal_Click);
            this.equalBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.equalBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.equalBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // backspacebt
            // 
            this.backspacebt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backspacebt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspacebt.Location = new System.Drawing.Point(12, 98);
            this.backspacebt.Name = "backspacebt";
            this.backspacebt.Size = new System.Drawing.Size(34, 27);
            this.backspacebt.TabIndex = 22;
            this.backspacebt.TabStop = false;
            this.backspacebt.Text = "←";
            this.toolTip2.SetToolTip(this.backspacebt, "Backspace");
            this.backspacebt.UseVisualStyleBackColor = true;
            this.backspacebt.Click += new System.EventHandler(this.backspacebt_Click);
            this.backspacebt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.backspacebt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.backspacebt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // ce
            // 
            this.ce.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ce.Location = new System.Drawing.Point(51, 98);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(34, 27);
            this.ce.TabIndex = 21;
            this.ce.TabStop = false;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            this.ce.Click += new System.EventHandler(this.ce_Click);
            this.ce.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.ce.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.ce.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // changesignBT
            // 
            this.changesignBT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changesignBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.changesignBT.Location = new System.Drawing.Point(129, 98);
            this.changesignBT.Name = "changesignBT";
            this.changesignBT.Size = new System.Drawing.Size(34, 27);
            this.changesignBT.TabIndex = 11;
            this.changesignBT.TabStop = false;
            this.changesignBT.Text = "±";
            this.changesignBT.UseVisualStyleBackColor = true;
            this.changesignBT.Click += new System.EventHandler(this.changesignBT_Click);
            this.changesignBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.changesignBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.changesignBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // memclearBT
            // 
            this.memclearBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.memclearBT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memclearBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memclearBT.Location = new System.Drawing.Point(12, 66);
            this.memclearBT.Name = "memclearBT";
            this.memclearBT.Size = new System.Drawing.Size(34, 27);
            this.memclearBT.TabIndex = 23;
            this.memclearBT.TabStop = false;
            this.memclearBT.Text = "MC";
            this.memclearBT.UseVisualStyleBackColor = true;
            this.memclearBT.Click += new System.EventHandler(this.memclearBT_Click);
            this.memclearBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.memclearBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.memclearBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mem_store
            // 
            this.mem_store.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_store.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_store.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_store.Location = new System.Drawing.Point(90, 66);
            this.mem_store.Name = "mem_store";
            this.mem_store.Size = new System.Drawing.Size(34, 27);
            this.mem_store.TabIndex = 24;
            this.mem_store.TabStop = false;
            this.mem_store.Text = "MS";
            this.mem_store.UseVisualStyleBackColor = true;
            this.mem_store.Click += new System.EventHandler(this.memprocess_Click);
            this.mem_store.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_store.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mem_store.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mem_recall
            // 
            this.mem_recall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_recall.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_recall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_recall.Location = new System.Drawing.Point(51, 66);
            this.mem_recall.Name = "mem_recall";
            this.mem_recall.Size = new System.Drawing.Size(34, 27);
            this.mem_recall.TabIndex = 25;
            this.mem_recall.TabStop = false;
            this.mem_recall.Text = "MR";
            this.mem_recall.UseVisualStyleBackColor = true;
            this.mem_recall.Click += new System.EventHandler(this.memrecall_Click);
            this.mem_recall.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_recall.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mem_recall.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // clearbt
            // 
            this.clearbt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearbt.Location = new System.Drawing.Point(90, 98);
            this.clearbt.Name = "clearbt";
            this.clearbt.Size = new System.Drawing.Size(34, 27);
            this.clearbt.TabIndex = 20;
            this.clearbt.TabStop = false;
            this.clearbt.Text = "C";
            this.clearbt.UseVisualStyleBackColor = true;
            this.clearbt.Click += new System.EventHandler(this.clear_Click);
            this.clearbt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.clearbt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.clearbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mem_add_bt
            // 
            this.mem_add_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_add_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_add_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_add_bt.Location = new System.Drawing.Point(129, 66);
            this.mem_add_bt.Name = "mem_add_bt";
            this.mem_add_bt.Size = new System.Drawing.Size(34, 27);
            this.mem_add_bt.TabIndex = 26;
            this.mem_add_bt.TabStop = false;
            this.mem_add_bt.Text = "M+";
            this.mem_add_bt.UseVisualStyleBackColor = true;
            this.mem_add_bt.Click += new System.EventHandler(this.memprocess_Click);
            this.mem_add_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_add_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mem_add_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mem_minus_bt
            // 
            this.mem_minus_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_minus_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_minus_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_minus_bt.Location = new System.Drawing.Point(168, 66);
            this.mem_minus_bt.Name = "mem_minus_bt";
            this.mem_minus_bt.Size = new System.Drawing.Size(34, 27);
            this.mem_minus_bt.TabIndex = 27;
            this.mem_minus_bt.TabStop = false;
            this.mem_minus_bt.Text = "M-";
            this.mem_minus_bt.UseVisualStyleBackColor = true;
            this.mem_minus_bt.Click += new System.EventHandler(this.memprocess_Click);
            this.mem_minus_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_minus_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mem_minus_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // close_bracket
            // 
            this.close_bracket.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_bracket.Location = new System.Drawing.Point(56, 332);
            this.close_bracket.Name = "close_bracket";
            this.close_bracket.Size = new System.Drawing.Size(34, 27);
            this.close_bracket.TabIndex = 153;
            this.close_bracket.TabStop = false;
            this.close_bracket.Text = ")";
            this.close_bracket.UseVisualStyleBackColor = true;
            this.close_bracket.Click += new System.EventHandler(this.close_bracket_Click);
            this.close_bracket.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.close_bracket.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.close_bracket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // open_bracket
            // 
            this.open_bracket.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.open_bracket.Location = new System.Drawing.Point(56, 332);
            this.open_bracket.Name = "open_bracket";
            this.open_bracket.Size = new System.Drawing.Size(34, 27);
            this.open_bracket.TabIndex = 152;
            this.open_bracket.TabStop = false;
            this.open_bracket.Text = "(";
            this.open_bracket.UseVisualStyleBackColor = true;
            this.open_bracket.Click += new System.EventHandler(this.open_bracket_Click);
            this.open_bracket.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.open_bracket.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.open_bracket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // _10x_bt
            // 
            this._10x_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._10x_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._10x_bt.Location = new System.Drawing.Point(56, 332);
            this._10x_bt.Name = "_10x_bt";
            this._10x_bt.Size = new System.Drawing.Size(34, 27);
            this._10x_bt.TabIndex = 40;
            this._10x_bt.TabStop = false;
            this._10x_bt.Text = "10ⁿ";
            this._10x_bt.UseVisualStyleBackColor = true;
            this._10x_bt.Click += new System.EventHandler(this.functionBT_Click);
            this._10x_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._10x_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this._10x_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // nvx_bt
            // 
            this.nvx_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nvx_bt.Location = new System.Drawing.Point(56, 332);
            this.nvx_bt.Name = "nvx_bt";
            this.nvx_bt.Size = new System.Drawing.Size(34, 27);
            this.nvx_bt.TabIndex = 34;
            this.nvx_bt.TabStop = false;
            this.nvx_bt.Text = "ⁿ√x";
            this.nvx_bt.UseVisualStyleBackColor = true;
            this.nvx_bt.Click += new System.EventHandler(this.PowerBT_Click);
            this.nvx_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.nvx_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.nvx_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // xn_bt
            // 
            this.xn_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xn_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xn_bt.Location = new System.Drawing.Point(56, 332);
            this.xn_bt.Name = "xn_bt";
            this.xn_bt.Size = new System.Drawing.Size(34, 27);
            this.xn_bt.TabIndex = 33;
            this.xn_bt.TabStop = false;
            this.xn_bt.Text = "xⁿ";
            this.xn_bt.UseVisualStyleBackColor = true;
            this.xn_bt.Click += new System.EventHandler(this.PowerBT_Click);
            this.xn_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.xn_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.xn_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // log_bt
            // 
            this.log_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.log_bt.Location = new System.Drawing.Point(56, 332);
            this.log_bt.Name = "log_bt";
            this.log_bt.Size = new System.Drawing.Size(34, 27);
            this.log_bt.TabIndex = 42;
            this.log_bt.TabStop = false;
            this.log_bt.Text = "log";
            this.log_bt.UseVisualStyleBackColor = true;
            this.log_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.log_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.log_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.log_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // _3vx_bt
            // 
            this._3vx_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._3vx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._3vx_bt.Location = new System.Drawing.Point(56, 332);
            this._3vx_bt.Name = "_3vx_bt";
            this._3vx_bt.Size = new System.Drawing.Size(34, 27);
            this._3vx_bt.TabIndex = 41;
            this._3vx_bt.TabStop = false;
            this._3vx_bt.Text = "³√x";
            this._3vx_bt.UseVisualStyleBackColor = true;
            this._3vx_bt.Click += new System.EventHandler(this.functionBT_Click);
            this._3vx_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._3vx_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this._3vx_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // x3_bt
            // 
            this.x3_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x3_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x3_bt.Location = new System.Drawing.Point(56, 332);
            this.x3_bt.Name = "x3_bt";
            this.x3_bt.Size = new System.Drawing.Size(34, 27);
            this.x3_bt.TabIndex = 39;
            this.x3_bt.TabStop = false;
            this.x3_bt.Text = "x³";
            this.x3_bt.UseVisualStyleBackColor = true;
            this.x3_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.x3_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.x3_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.x3_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // x2_bt
            // 
            this.x2_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x2_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2_bt.Location = new System.Drawing.Point(56, 332);
            this.x2_bt.Name = "x2_bt";
            this.x2_bt.Size = new System.Drawing.Size(34, 27);
            this.x2_bt.TabIndex = 38;
            this.x2_bt.TabStop = false;
            this.x2_bt.Text = "x²";
            this.x2_bt.UseVisualStyleBackColor = true;
            this.x2_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.x2_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.x2_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.x2_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // pi_bt
            // 
            this.pi_bt.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pi_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pi_bt.Location = new System.Drawing.Point(56, 332);
            this.pi_bt.Name = "pi_bt";
            this.pi_bt.Size = new System.Drawing.Size(34, 27);
            this.pi_bt.TabIndex = 144;
            this.pi_bt.TabStop = false;
            this.pi_bt.Text = "π";
            this.pi_bt.UseVisualStyleBackColor = true;
            this.pi_bt.Click += new System.EventHandler(this.pi_bt_Click);
            this.pi_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.pi_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.pi_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // exp_bt
            // 
            this.exp_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exp_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exp_bt.Location = new System.Drawing.Point(56, 332);
            this.exp_bt.Name = "exp_bt";
            this.exp_bt.Size = new System.Drawing.Size(34, 27);
            this.exp_bt.TabIndex = 46;
            this.exp_bt.TabStop = false;
            this.exp_bt.Text = "Exp";
            this.exp_bt.UseVisualStyleBackColor = true;
            this.exp_bt.Click += new System.EventHandler(this.exp_bt_Click);
            this.exp_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.exp_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.exp_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // modsciBT
            // 
            this.modsciBT.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modsciBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modsciBT.Location = new System.Drawing.Point(56, 332);
            this.modsciBT.Name = "modsciBT";
            this.modsciBT.Size = new System.Drawing.Size(34, 27);
            this.modsciBT.TabIndex = 43;
            this.modsciBT.TabStop = false;
            this.modsciBT.Text = "Mod";
            this.modsciBT.UseVisualStyleBackColor = true;
            this.modsciBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.modsciBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.modsciBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.modsciBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // ln_bt
            // 
            this.ln_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ln_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ln_bt.Location = new System.Drawing.Point(56, 332);
            this.ln_bt.Name = "ln_bt";
            this.ln_bt.Size = new System.Drawing.Size(34, 27);
            this.ln_bt.TabIndex = 31;
            this.ln_bt.TabStop = false;
            this.ln_bt.Text = "ln";
            this.ln_bt.UseVisualStyleBackColor = true;
            this.ln_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.ln_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.ln_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.ln_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // dms_bt
            // 
            this.dms_bt.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.dms_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dms_bt.Location = new System.Drawing.Point(56, 332);
            this.dms_bt.Name = "dms_bt";
            this.dms_bt.Size = new System.Drawing.Size(34, 27);
            this.dms_bt.TabIndex = 45;
            this.dms_bt.TabStop = false;
            this.dms_bt.Text = "dms";
            this.dms_bt.UseVisualStyleBackColor = true;
            this.dms_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.dms_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.dms_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.dms_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // tanh_bt
            // 
            this.tanh_bt.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.tanh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tanh_bt.Location = new System.Drawing.Point(56, 332);
            this.tanh_bt.Name = "tanh_bt";
            this.tanh_bt.Size = new System.Drawing.Size(34, 27);
            this.tanh_bt.TabIndex = 37;
            this.tanh_bt.TabStop = false;
            this.tanh_bt.Text = "tanh";
            this.tanh_bt.UseVisualStyleBackColor = true;
            this.tanh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.tanh_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.tanh_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.tanh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // cosh_bt
            // 
            this.cosh_bt.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.cosh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cosh_bt.Location = new System.Drawing.Point(56, 332);
            this.cosh_bt.Name = "cosh_bt";
            this.cosh_bt.Size = new System.Drawing.Size(34, 27);
            this.cosh_bt.TabIndex = 36;
            this.cosh_bt.TabStop = false;
            this.cosh_bt.Text = "cosh";
            this.cosh_bt.UseVisualStyleBackColor = true;
            this.cosh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.cosh_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.cosh_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.cosh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // int_bt
            // 
            this.int_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.int_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.int_bt.Location = new System.Drawing.Point(56, 332);
            this.int_bt.Name = "int_bt";
            this.int_bt.Size = new System.Drawing.Size(34, 27);
            this.int_bt.TabIndex = 44;
            this.int_bt.TabStop = false;
            this.int_bt.Text = "Int";
            this.int_bt.UseVisualStyleBackColor = true;
            this.int_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.int_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.int_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.int_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // tan_bt
            // 
            this.tan_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tan_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tan_bt.Location = new System.Drawing.Point(56, 332);
            this.tan_bt.Name = "tan_bt";
            this.tan_bt.Size = new System.Drawing.Size(34, 27);
            this.tan_bt.TabIndex = 30;
            this.tan_bt.TabStop = false;
            this.tan_bt.Text = "tan";
            this.tan_bt.UseVisualStyleBackColor = true;
            this.tan_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.tan_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.tan_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.tan_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sinh_bt
            // 
            this.sinh_bt.Font = new System.Drawing.Font("Segoe UI", 7.25F);
            this.sinh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sinh_bt.Location = new System.Drawing.Point(56, 332);
            this.sinh_bt.Name = "sinh_bt";
            this.sinh_bt.Size = new System.Drawing.Size(34, 27);
            this.sinh_bt.TabIndex = 35;
            this.sinh_bt.TabStop = false;
            this.sinh_bt.Text = "sinh";
            this.sinh_bt.UseVisualStyleBackColor = true;
            this.sinh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sinh_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sinh_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sinh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btFactorial
            // 
            this.btFactorial.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFactorial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btFactorial.Location = new System.Drawing.Point(56, 332);
            this.btFactorial.Name = "btFactorial";
            this.btFactorial.Size = new System.Drawing.Size(34, 27);
            this.btFactorial.TabIndex = 32;
            this.btFactorial.TabStop = false;
            this.btFactorial.Text = "n!";
            this.btFactorial.UseVisualStyleBackColor = true;
            this.btFactorial.Click += new System.EventHandler(this.functionBT_Click);
            this.btFactorial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btFactorial.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btFactorial.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // XorBT
            // 
            this.XorBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.XorBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.XorBT.Location = new System.Drawing.Point(99, 332);
            this.XorBT.Name = "XorBT";
            this.XorBT.Size = new System.Drawing.Size(34, 27);
            this.XorBT.TabIndex = 161;
            this.XorBT.TabStop = false;
            this.XorBT.Text = "Xor";
            this.XorBT.UseVisualStyleBackColor = true;
            this.XorBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.XorBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.XorBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.XorBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // notBT
            // 
            this.notBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.notBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.notBT.Location = new System.Drawing.Point(99, 332);
            this.notBT.Name = "notBT";
            this.notBT.Size = new System.Drawing.Size(34, 27);
            this.notBT.TabIndex = 203;
            this.notBT.TabStop = false;
            this.notBT.Text = "Not";
            this.notBT.UseVisualStyleBackColor = true;
            this.notBT.Click += new System.EventHandler(this.notBT_Click);
            this.notBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.notBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.notBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // AndBT
            // 
            this.AndBT.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.AndBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AndBT.Location = new System.Drawing.Point(99, 332);
            this.AndBT.Name = "AndBT";
            this.AndBT.Size = new System.Drawing.Size(34, 27);
            this.AndBT.TabIndex = 160;
            this.AndBT.TabStop = false;
            this.AndBT.Text = "And";
            this.AndBT.UseVisualStyleBackColor = true;
            this.AndBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.AndBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.AndBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AndBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // RshBT
            // 
            this.RshBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.RshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RshBT.Location = new System.Drawing.Point(99, 332);
            this.RshBT.Name = "RshBT";
            this.RshBT.Size = new System.Drawing.Size(34, 27);
            this.RshBT.TabIndex = 151;
            this.RshBT.TabStop = false;
            this.RshBT.Text = "Rsh";
            this.RshBT.UseVisualStyleBackColor = true;
            this.RshBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.RshBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.RshBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.RshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // RoRBT
            // 
            this.RoRBT.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.RoRBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoRBT.Location = new System.Drawing.Point(99, 332);
            this.RoRBT.Name = "RoRBT";
            this.RoRBT.Size = new System.Drawing.Size(34, 27);
            this.RoRBT.TabIndex = 211;
            this.RoRBT.TabStop = false;
            this.RoRBT.Text = "RoR";
            this.RoRBT.UseVisualStyleBackColor = true;
            this.RoRBT.Click += new System.EventHandler(this.rotateBT_Click);
            this.RoRBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.RoRBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.RoRBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // LshBT
            // 
            this.LshBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.LshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LshBT.Location = new System.Drawing.Point(99, 332);
            this.LshBT.Name = "LshBT";
            this.LshBT.Size = new System.Drawing.Size(34, 27);
            this.LshBT.TabIndex = 150;
            this.LshBT.TabStop = false;
            this.LshBT.Text = "Lsh";
            this.LshBT.UseVisualStyleBackColor = true;
            this.LshBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.LshBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.LshBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.LshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // or_BT
            // 
            this.or_BT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.or_BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.or_BT.Location = new System.Drawing.Point(99, 332);
            this.or_BT.Name = "or_BT";
            this.or_BT.Size = new System.Drawing.Size(34, 27);
            this.or_BT.TabIndex = 162;
            this.or_BT.TabStop = false;
            this.or_BT.Text = "Or";
            this.or_BT.UseVisualStyleBackColor = true;
            this.or_BT.Click += new System.EventHandler(this.operatorBT_Click);
            this.or_BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.or_BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.or_BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // RoLBT
            // 
            this.RoLBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.RoLBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoLBT.Location = new System.Drawing.Point(99, 332);
            this.RoLBT.Name = "RoLBT";
            this.RoLBT.Size = new System.Drawing.Size(34, 27);
            this.RoLBT.TabIndex = 198;
            this.RoLBT.TabStop = false;
            this.RoLBT.Text = "RoL";
            this.RoLBT.UseVisualStyleBackColor = true;
            this.RoLBT.Click += new System.EventHandler(this.rotateBT_Click);
            this.RoLBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.RoLBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.RoLBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // modproBT
            // 
            this.modproBT.Font = new System.Drawing.Font("Segoe UI", 6.75F);
            this.modproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modproBT.Location = new System.Drawing.Point(99, 332);
            this.modproBT.Name = "modproBT";
            this.modproBT.Size = new System.Drawing.Size(34, 27);
            this.modproBT.TabIndex = 211;
            this.modproBT.TabStop = false;
            this.modproBT.Text = "Mod";
            this.modproBT.UseVisualStyleBackColor = true;
            this.modproBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.modproBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.modproBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.modproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sigmax2BT
            // 
            this.sigmax2BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmax2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmax2BT.Image = ((System.Drawing.Image)(resources.GetObject("sigmax2BT.Image")));
            this.sigmax2BT.Location = new System.Drawing.Point(141, 332);
            this.sigmax2BT.Name = "sigmax2BT";
            this.sigmax2BT.Size = new System.Drawing.Size(34, 27);
            this.sigmax2BT.TabIndex = 245;
            this.sigmax2BT.TabStop = false;
            this.sigmax2BT.UseVisualStyleBackColor = true;
            this.sigmax2BT.Click += new System.EventHandler(this.sigmax2BT_Click);
            this.sigmax2BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sigmax2BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sigmax2BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sigman_1BT
            // 
            this.sigman_1BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigman_1BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigman_1BT.Image = ((System.Drawing.Image)(resources.GetObject("sigman_1BT.Image")));
            this.sigman_1BT.Location = new System.Drawing.Point(141, 332);
            this.sigman_1BT.Name = "sigman_1BT";
            this.sigman_1BT.Size = new System.Drawing.Size(34, 27);
            this.sigman_1BT.TabIndex = 244;
            this.sigman_1BT.TabStop = false;
            this.sigman_1BT.UseVisualStyleBackColor = true;
            this.sigman_1BT.Click += new System.EventHandler(this.sigman_1BT_Click);
            this.sigman_1BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sigman_1BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sigman_1BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // AddstaBT
            // 
            this.AddstaBT.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.AddstaBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddstaBT.Location = new System.Drawing.Point(141, 332);
            this.AddstaBT.Name = "AddstaBT";
            this.AddstaBT.Size = new System.Drawing.Size(34, 27);
            this.AddstaBT.TabIndex = 243;
            this.AddstaBT.TabStop = false;
            this.AddstaBT.Text = "Add";
            this.AddstaBT.UseVisualStyleBackColor = true;
            this.AddstaBT.Click += new System.EventHandler(this.AddstaBT_Click);
            this.AddstaBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.AddstaBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddstaBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // xcross
            // 
            this.xcross.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xcross.Image = ((System.Drawing.Image)(resources.GetObject("xcross.Image")));
            this.xcross.Location = new System.Drawing.Point(141, 332);
            this.xcross.Name = "xcross";
            this.xcross.Size = new System.Drawing.Size(34, 27);
            this.xcross.TabIndex = 242;
            this.xcross.TabStop = false;
            this.xcross.UseVisualStyleBackColor = true;
            this.xcross.Click += new System.EventHandler(this.xcross_Click);
            this.xcross.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.xcross.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.xcross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sigmaxBT
            // 
            this.sigmaxBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmaxBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmaxBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmaxBT.Image")));
            this.sigmaxBT.Location = new System.Drawing.Point(141, 332);
            this.sigmaxBT.Name = "sigmaxBT";
            this.sigmaxBT.Size = new System.Drawing.Size(34, 27);
            this.sigmaxBT.TabIndex = 241;
            this.sigmaxBT.TabStop = false;
            this.sigmaxBT.UseVisualStyleBackColor = true;
            this.sigmaxBT.Click += new System.EventHandler(this.sigmaxBT_Click);
            this.sigmaxBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sigmaxBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sigmaxBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sigmanBT
            // 
            this.sigmanBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmanBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmanBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmanBT.Image")));
            this.sigmanBT.Location = new System.Drawing.Point(141, 332);
            this.sigmanBT.Name = "sigmanBT";
            this.sigmanBT.Size = new System.Drawing.Size(34, 27);
            this.sigmanBT.TabIndex = 240;
            this.sigmanBT.TabStop = false;
            this.sigmanBT.UseVisualStyleBackColor = true;
            this.sigmanBT.Click += new System.EventHandler(this.sigmanBT_Click);
            this.sigmanBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sigmanBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sigmanBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // CAD
            // 
            this.CAD.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.CAD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CAD.Location = new System.Drawing.Point(141, 332);
            this.CAD.Name = "CAD";
            this.CAD.Size = new System.Drawing.Size(34, 27);
            this.CAD.TabIndex = 248;
            this.CAD.TabStop = false;
            this.CAD.Text = "CAD";
            this.CAD.UseVisualStyleBackColor = true;
            this.CAD.Click += new System.EventHandler(this.CAD_Click);
            this.CAD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.CAD.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.CAD.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // x2cross
            // 
            this.x2cross.BackColor = System.Drawing.SystemColors.Control;
            this.x2cross.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x2cross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2cross.Image = ((System.Drawing.Image)(resources.GetObject("x2cross.Image")));
            this.x2cross.Location = new System.Drawing.Point(141, 332);
            this.x2cross.Name = "x2cross";
            this.x2cross.Size = new System.Drawing.Size(34, 27);
            this.x2cross.TabIndex = 246;
            this.x2cross.TabStop = false;
            this.x2cross.UseVisualStyleBackColor = false;
            this.x2cross.Click += new System.EventHandler(this.x2cross_Click);
            this.x2cross.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.x2cross.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.x2cross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // inv_ChkBox
            // 
            this.inv_ChkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.inv_ChkBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.inv_ChkBox.Location = new System.Drawing.Point(186, 332);
            this.inv_ChkBox.Name = "inv_ChkBox";
            this.inv_ChkBox.Size = new System.Drawing.Size(34, 27);
            this.inv_ChkBox.TabIndex = 250;
            this.inv_ChkBox.TabStop = false;
            this.inv_ChkBox.Text = "Inv";
            this.inv_ChkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.inv_ChkBox.UseVisualStyleBackColor = true;
            this.inv_ChkBox.CheckedChanged += new System.EventHandler(this.inv_ChkBox_CheckedChanged);
            this.inv_ChkBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.inv_ChkBox.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.inv_ChkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // dnBT
            // 
            this.dnBT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dnBT.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnBT.Location = new System.Drawing.Point(167, 2);
            this.dnBT.Name = "dnBT";
            this.dnBT.Size = new System.Drawing.Size(17, 16);
            this.dnBT.TabIndex = 1;
            this.dnBT.TabStop = false;
            this.dnBT.Text = "▼";
            this.toolTip2.SetToolTip(this.dnBT, "Down");
            this.dnBT.UseVisualStyleBackColor = true;
            this.dnBT.Click += new System.EventHandler(this.directionBT_Click);
            this.dnBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.dnBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // upBT
            // 
            this.upBT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.upBT.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upBT.Location = new System.Drawing.Point(147, 2);
            this.upBT.Name = "upBT";
            this.upBT.Size = new System.Drawing.Size(17, 16);
            this.upBT.TabIndex = 3;
            this.upBT.TabStop = false;
            this.upBT.Text = "▲";
            this.toolTip2.SetToolTip(this.upBT, "Up");
            this.upBT.UseVisualStyleBackColor = true;
            this.upBT.Click += new System.EventHandler(this.directionBT_Click);
            this.upBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.upBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // fe_ChkBox
            // 
            this.fe_ChkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fe_ChkBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fe_ChkBox.Location = new System.Drawing.Point(186, 331);
            this.fe_ChkBox.Name = "fe_ChkBox";
            this.fe_ChkBox.Size = new System.Drawing.Size(34, 27);
            this.fe_ChkBox.TabIndex = 47;
            this.fe_ChkBox.TabStop = false;
            this.fe_ChkBox.Text = "F-E";
            this.fe_ChkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fe_ChkBox.UseVisualStyleBackColor = true;
            this.fe_ChkBox.CheckedChanged += new System.EventHandler(this.fe_ChkBox_CheckChanged);
            this.fe_ChkBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.fe_ChkBox.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.fe_ChkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // bracketTime_lb
            // 
            this.bracketTime_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bracketTime_lb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bracketTime_lb.Location = new System.Drawing.Point(13, 332);
            this.bracketTime_lb.Name = "bracketTime_lb";
            this.bracketTime_lb.Size = new System.Drawing.Size(34, 27);
            this.bracketTime_lb.TabIndex = 129;
            this.bracketTime_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bracketTime_lb.TextChanged += new System.EventHandler(this.bracketTime_lb_TextChanged);
            this.bracketTime_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FocusAndMoveForm);
            this.bracketTime_lb.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // cos_bt
            // 
            this.cos_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cos_bt.Location = new System.Drawing.Point(55, 332);
            this.cos_bt.Name = "cos_bt";
            this.cos_bt.Size = new System.Drawing.Size(34, 27);
            this.cos_bt.TabIndex = 29;
            this.cos_bt.TabStop = false;
            this.cos_bt.Text = "cos";
            this.cos_bt.UseCompatibleTextRendering = true;
            this.cos_bt.UseVisualStyleBackColor = true;
            this.cos_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.cos_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.cos_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.cos_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sin_bt
            // 
            this.sin_bt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sin_bt.Location = new System.Drawing.Point(56, 332);
            this.sin_bt.Name = "sin_bt";
            this.sin_bt.Size = new System.Drawing.Size(34, 27);
            this.sin_bt.TabIndex = 28;
            this.sin_bt.TabStop = false;
            this.sin_bt.Text = "sin";
            this.sin_bt.UseVisualStyleBackColor = true;
            this.sin_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sin_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sin_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sin_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.viewMI,
            this.editMI,
            this.helpMI});
            // 
            // viewMI
            // 
            this.viewMI.Index = 0;
            this.viewMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.standardMI,
            this.scientificMI,
            this.programmerMI,
            this.statisticsMI,
            this.sepMI1,
            this.historyMI,
            this.digitGroupingMI,
            this.sepMI2,
            this.basicMI,
            this.unitConversionMI,
            this.dateCalculationMI,
            this.worksheetsMI});
            this.viewMI.Text = "&View";
            // 
            // standardMI
            // 
            this.standardMI.Index = 0;
            this.standardMI.RadioCheck = true;
            this.standardMI.Shortcut = System.Windows.Forms.Shortcut.Alt1;
            this.standardMI.Text = "S&tandard";
            this.standardMI.Click += new System.EventHandler(this.standardMI_Click);
            // 
            // scientificMI
            // 
            this.scientificMI.Index = 1;
            this.scientificMI.RadioCheck = true;
            this.scientificMI.Shortcut = System.Windows.Forms.Shortcut.Alt2;
            this.scientificMI.Text = "&Scientific";
            this.scientificMI.Click += new System.EventHandler(this.scientificMI_Click);
            // 
            // programmerMI
            // 
            this.programmerMI.Index = 2;
            this.programmerMI.RadioCheck = true;
            this.programmerMI.Shortcut = System.Windows.Forms.Shortcut.Alt3;
            this.programmerMI.Text = "&Programmer";
            this.programmerMI.Click += new System.EventHandler(this.programmerMI_Click);
            // 
            // statisticsMI
            // 
            this.statisticsMI.Index = 3;
            this.statisticsMI.RadioCheck = true;
            this.statisticsMI.Shortcut = System.Windows.Forms.Shortcut.Alt4;
            this.statisticsMI.Text = "St&atistics";
            this.statisticsMI.Click += new System.EventHandler(this.statisticsMI_Click);
            // 
            // sepMI1
            // 
            this.sepMI1.Index = 4;
            this.sepMI1.Text = "-";
            // 
            // historyMI
            // 
            this.historyMI.Index = 5;
            this.historyMI.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.historyMI.Text = "Histor&y";
            this.historyMI.Click += new System.EventHandler(this.historyMI_Click);
            // 
            // digitGroupingMI
            // 
            this.digitGroupingMI.Index = 6;
            this.digitGroupingMI.Text = "D&igit grouping";
            this.digitGroupingMI.Click += new System.EventHandler(this.digitGroupingMI_Click);
            // 
            // sepMI2
            // 
            this.sepMI2.Index = 7;
            this.sepMI2.Text = "-";
            // 
            // basicMI
            // 
            this.basicMI.Checked = true;
            this.basicMI.Index = 8;
            this.basicMI.RadioCheck = true;
            this.basicMI.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
            this.basicMI.Text = "&Basic";
            this.basicMI.Click += new System.EventHandler(this.basicMI_Click);
            // 
            // unitConversionMI
            // 
            this.unitConversionMI.Index = 9;
            this.unitConversionMI.RadioCheck = true;
            this.unitConversionMI.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.unitConversionMI.Text = "&Unit conversion";
            this.unitConversionMI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // dateCalculationMI
            // 
            this.dateCalculationMI.Index = 10;
            this.dateCalculationMI.RadioCheck = true;
            this.dateCalculationMI.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.dateCalculationMI.Text = "&Date calculation";
            this.dateCalculationMI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // worksheetsMI
            // 
            this.worksheetsMI.Index = 11;
            this.worksheetsMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mortgageMI,
            this.vehicleLeaseMI,
            this.fe_MPG_MI,
            this.feL100_MI});
            this.worksheetsMI.Text = "&Worksheets";
            // 
            // mortgageMI
            // 
            this.mortgageMI.Index = 0;
            this.mortgageMI.RadioCheck = true;
            this.mortgageMI.Text = "&Mortgage";
            this.mortgageMI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // vehicleLeaseMI
            // 
            this.vehicleLeaseMI.Index = 1;
            this.vehicleLeaseMI.RadioCheck = true;
            this.vehicleLeaseMI.Text = "&Vehicle lease";
            this.vehicleLeaseMI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // fe_MPG_MI
            // 
            this.fe_MPG_MI.Index = 2;
            this.fe_MPG_MI.RadioCheck = true;
            this.fe_MPG_MI.Text = "&Fuel economy (mpg)";
            this.fe_MPG_MI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // feL100_MI
            // 
            this.feL100_MI.Index = 3;
            this.feL100_MI.RadioCheck = true;
            this.feL100_MI.Text = "F&uel economy (L/100 km)";
            this.feL100_MI.Click += new System.EventHandler(this.extraFunctionMI_Click);
            // 
            // editMI
            // 
            this.editMI.Index = 1;
            this.editMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyMI,
            this.pasteMI,
            this.sepMI3,
            this.historyOptionMI,
            this.datasetMI,
            this.sepMI6,
            this.preferencesMI});
            this.editMI.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.editMI.Text = "&Edit";
            this.editMI.Popup += new System.EventHandler(this.getPaste);
            // 
            // copyMI
            // 
            this.copyMI.Index = 0;
            this.copyMI.Text = "&Copy";
            this.copyMI.Click += new System.EventHandler(this.copyCTMN_Click);
            // 
            // pasteMI
            // 
            this.pasteMI.Enabled = false;
            this.pasteMI.Index = 1;
            this.pasteMI.Text = "&Paste";
            this.pasteMI.Click += new System.EventHandler(this.pasteCTMN_Click);
            // 
            // sepMI3
            // 
            this.sepMI3.Index = 2;
            this.sepMI3.Text = "-";
            // 
            // historyOptionMI
            // 
            this.historyOptionMI.Index = 3;
            this.historyOptionMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyHistoryMI,
            this.editHistoryMI,
            this.reCalculateMI,
            this.cancelEditHisMI,
            this.clearHistoryMI});
            this.historyOptionMI.Text = "&History";
            this.historyOptionMI.Popup += new System.EventHandler(this.historyAndDatasetOptionMI_Popup);
            // 
            // copyHistoryMI
            // 
            this.copyHistoryMI.Enabled = false;
            this.copyHistoryMI.Index = 0;
            this.copyHistoryMI.Text = "Copy h&istory";
            this.copyHistoryMI.Click += new System.EventHandler(this.copyHistoryMI_Click);
            // 
            // editHistoryMI
            // 
            this.editHistoryMI.Enabled = false;
            this.editHistoryMI.Index = 1;
            this.editHistoryMI.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.editHistoryMI.Text = "&Edit";
            this.editHistoryMI.Click += new System.EventHandler(this.editHistoryMI_Click);
            // 
            // reCalculateMI
            // 
            this.reCalculateMI.Index = 2;
            this.reCalculateMI.Text = "Re&calculate";
            this.reCalculateMI.Visible = false;
            this.reCalculateMI.Click += new System.EventHandler(this.reCalculate_Click);
            // 
            // cancelEditHisMI
            // 
            this.cancelEditHisMI.Enabled = false;
            this.cancelEditHisMI.Index = 3;
            this.cancelEditHisMI.Text = "Ca&ncel edit";
            this.cancelEditHisMI.Click += new System.EventHandler(this.cancelEditHisMI_Click);
            // 
            // clearHistoryMI
            // 
            this.clearHistoryMI.Enabled = false;
            this.clearHistoryMI.Index = 4;
            this.clearHistoryMI.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftD;
            this.clearHistoryMI.Text = "C&lear";
            this.clearHistoryMI.Click += new System.EventHandler(this.clearHistoryMI_Click);
            // 
            // datasetMI
            // 
            this.datasetMI.Index = 4;
            this.datasetMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyDatasetMI,
            this.editDatasetMI,
            this.commitDSMI,
            this.cancelEditDSMI,
            this.clearDatasetMI});
            this.datasetMI.Text = "&Dataset";
            this.datasetMI.Popup += new System.EventHandler(this.historyAndDatasetOptionMI_Popup);
            // 
            // copyDatasetMI
            // 
            this.copyDatasetMI.Index = 0;
            this.copyDatasetMI.Text = "Copy D&ataset";
            this.copyDatasetMI.Click += new System.EventHandler(this.copyDatasetMI_Click);
            // 
            // editDatasetMI
            // 
            this.editDatasetMI.Index = 1;
            this.editDatasetMI.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.editDatasetMI.Text = "&Edit";
            this.editDatasetMI.Click += new System.EventHandler(this.editDatasetMI_Click);
            // 
            // commitDSMI
            // 
            this.commitDSMI.Index = 2;
            this.commitDSMI.Text = "C&ommit";
            this.commitDSMI.Visible = false;
            this.commitDSMI.Click += new System.EventHandler(this.commitDSMI_Click);
            // 
            // cancelEditDSMI
            // 
            this.cancelEditDSMI.Enabled = false;
            this.cancelEditDSMI.Index = 3;
            this.cancelEditDSMI.Text = "Ca&ncel edit";
            this.cancelEditDSMI.Click += new System.EventHandler(this.cancelEditDSMI_Click);
            // 
            // clearDatasetMI
            // 
            this.clearDatasetMI.Index = 4;
            this.clearDatasetMI.Text = "C&lear";
            this.clearDatasetMI.Click += new System.EventHandler(this.CAD_Click);
            // 
            // sepMI6
            // 
            this.sepMI6.Index = 5;
            this.sepMI6.Text = "-";
            // 
            // preferencesMI
            // 
            this.preferencesMI.Index = 6;
            this.preferencesMI.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.preferencesMI.Text = "P&references";
            this.preferencesMI.Click += new System.EventHandler(this.preferrencesMI_Click);
            // 
            // helpMI
            // 
            this.helpMI.Index = 2;
            this.helpMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.helpTopicsTSMI,
            this.sepMI5,
            this.aboutMI});
            this.helpMI.Text = "&Help";
            // 
            // helpTopicsTSMI
            // 
            this.helpTopicsTSMI.Index = 0;
            this.helpTopicsTSMI.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.helpTopicsTSMI.Text = "&View Help";
            this.helpTopicsTSMI.Click += new System.EventHandler(this.helpTopicsMI_Click);
            // 
            // sepMI5
            // 
            this.sepMI5.Index = 1;
            this.sepMI5.Text = "-";
            // 
            // aboutMI
            // 
            this.aboutMI.Index = 2;
            this.aboutMI.Text = "&About Calculator";
            this.aboutMI.Click += new System.EventHandler(this.aboutMI_Click);
            // 
            // mainContextMenu
            // 
            this.mainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyCTMN,
            this.pasteCTMN,
            this.sepCTMN,
            this.showHistoryCTMN,
            this.hideHistoryCTMN,
            this.clearHistoryCTMN,
            this.clearDatasetCTMN,
            this.showPreviewPaneCTMN,
            this.hidePreviewPaneCTMN});
            this.mainContextMenu.Popup += new System.EventHandler(this.mainContextMenu_Popup);
            // 
            // copyCTMN
            // 
            this.copyCTMN.Index = 0;
            this.copyCTMN.Text = "&Copy";
            this.copyCTMN.Click += new System.EventHandler(this.copyCTMN_Click);
            // 
            // pasteCTMN
            // 
            this.pasteCTMN.Enabled = false;
            this.pasteCTMN.Index = 1;
            this.pasteCTMN.Text = "&Paste";
            this.pasteCTMN.Click += new System.EventHandler(this.pasteCTMN_Click);
            // 
            // sepCTMN
            // 
            this.sepCTMN.Index = 2;
            this.sepCTMN.Text = "-";
            // 
            // showHistoryCTMN
            // 
            this.showHistoryCTMN.Index = 3;
            this.showHistoryCTMN.Text = "&Show history";
            this.showHistoryCTMN.Click += new System.EventHandler(this.historyMI_Click);
            // 
            // hideHistoryCTMN
            // 
            this.hideHistoryCTMN.Index = 4;
            this.hideHistoryCTMN.Text = "&Hide history";
            this.hideHistoryCTMN.Visible = false;
            this.hideHistoryCTMN.Click += new System.EventHandler(this.historyMI_Click);
            // 
            // clearHistoryCTMN
            // 
            this.clearHistoryCTMN.Index = 5;
            this.clearHistoryCTMN.Text = "C&lear history";
            this.clearHistoryCTMN.Visible = false;
            this.clearHistoryCTMN.Click += new System.EventHandler(this.clearHistoryMI_Click);
            // 
            // clearDatasetCTMN
            // 
            this.clearDatasetCTMN.Index = 6;
            this.clearDatasetCTMN.Text = "C&lear Dataset";
            this.clearDatasetCTMN.Visible = false;
            this.clearDatasetCTMN.Click += new System.EventHandler(this.CAD_Click);
            // 
            // showPreviewPaneCTMN
            // 
            this.showPreviewPaneCTMN.Index = 7;
            this.showPreviewPaneCTMN.Text = "&Show Preview";
            this.showPreviewPaneCTMN.Click += new System.EventHandler(this.previewPanelCTMN_Click);
            // 
            // hidePreviewPaneCTMN
            // 
            this.hidePreviewPaneCTMN.Index = 8;
            this.hidePreviewPaneCTMN.Text = "&Hide Preview";
            this.hidePreviewPaneCTMN.Click += new System.EventHandler(this.previewPanelCTMN_Click);
            // 
            // helpCTMN
            // 
            this.helpCTMN.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.hotkeyMI});
            // 
            // hotkeyMI
            // 
            this.hotkeyMI.Index = 0;
            this.hotkeyMI.Text = "&What is this?";
            this.hotkeyMI.Click += new System.EventHandler(this.hotkeyMI_Click);
            // 
            // mWorker
            // 
            this.mWorker.WorkerSupportsCancellation = true;
            this.mWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mWorker_DoWorkPrevFunc);
            this.mWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mWorker_RunWorkerCompleted);
            // 
            // openProBT
            // 
            this.openProBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openProBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openProBT.Location = new System.Drawing.Point(56, 332);
            this.openProBT.Name = "openProBT";
            this.openProBT.Size = new System.Drawing.Size(34, 27);
            this.openProBT.TabIndex = 152;
            this.openProBT.TabStop = false;
            this.openProBT.Text = "(";
            this.openProBT.UseVisualStyleBackColor = true;
            this.openProBT.Click += new System.EventHandler(this.open_bracket_Click);
            this.openProBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.openProBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.openProBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // datecalcPN
            // 
            this.datecalcPN.BackColor = System.Drawing.Color.White;
            this.datecalcPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datecalcPN.Controls.Add(this.tbResult1);
            this.datecalcPN.Controls.Add(this.tbResult2);
            this.datecalcPN.Controls.Add(this.calmethodLB);
            this.datecalcPN.Controls.Add(this.secondDate);
            this.datecalcPN.Controls.Add(this.dtP2);
            this.datecalcPN.Controls.Add(this.subrb);
            this.datecalcPN.Controls.Add(this.dtP1);
            this.datecalcPN.Controls.Add(this.addrb);
            this.datecalcPN.Controls.Add(this.lbResult2);
            this.datecalcPN.Controls.Add(this.firstDate);
            this.datecalcPN.Controls.Add(this.autocal_date);
            this.datecalcPN.Controls.Add(this.lbResult1);
            this.datecalcPN.Controls.Add(this.dateCalculationBT);
            this.datecalcPN.Controls.Add(this.datemethodCB);
            this.datecalcPN.Controls.Add(this.yearAddSubLB);
            this.datecalcPN.Controls.Add(this.monthAddSubLB);
            this.datecalcPN.Controls.Add(this.dayAddSubLB);
            this.datecalcPN.Controls.Add(this.periodsDateUD);
            this.datecalcPN.Controls.Add(this.periodsMonthUD);
            this.datecalcPN.Controls.Add(this.periodsYearUD);
            this.datecalcPN.Location = new System.Drawing.Point(234, 12);
            this.datecalcPN.Name = "datecalcPN";
            this.datecalcPN.Size = new System.Drawing.Size(356, 241);
            this.datecalcPN.TabIndex = 32;
            this.datecalcPN.Visible = false;
            this.datecalcPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // tbResult1
            // 
            this.tbResult1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResult1.Location = new System.Drawing.Point(12, 108);
            this.tbResult1.Name = "tbResult1";
            this.tbResult1.ReadOnly = true;
            this.tbResult1.Size = new System.Drawing.Size(330, 22);
            this.tbResult1.TabIndex = 208;
            this.tbResult1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // tbResult2
            // 
            this.tbResult2.BackColor = System.Drawing.SystemColors.Control;
            this.tbResult2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResult2.Location = new System.Drawing.Point(12, 159);
            this.tbResult2.Name = "tbResult2";
            this.tbResult2.ReadOnly = true;
            this.tbResult2.Size = new System.Drawing.Size(330, 22);
            this.tbResult2.TabIndex = 209;
            this.tbResult2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // calmethodLB
            // 
            this.calmethodLB.AutoSize = true;
            this.calmethodLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calmethodLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.calmethodLB.Location = new System.Drawing.Point(12, 8);
            this.calmethodLB.Name = "calmethodLB";
            this.calmethodLB.Size = new System.Drawing.Size(193, 13);
            this.calmethodLB.TabIndex = 74;
            this.calmethodLB.Text = "Select the date calculation you want";
            this.calmethodLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // secondDate
            // 
            this.secondDate.AutoSize = true;
            this.secondDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.secondDate.Location = new System.Drawing.Point(214, 67);
            this.secondDate.Name = "secondDate";
            this.secondDate.Size = new System.Drawing.Size(19, 13);
            this.secondDate.TabIndex = 60;
            this.secondDate.Text = "To";
            this.secondDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // dtP2
            // 
            this.dtP2.Checked = false;
            this.dtP2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtP2.Location = new System.Drawing.Point(243, 63);
            this.dtP2.Name = "dtP2";
            this.dtP2.Size = new System.Drawing.Size(99, 22);
            this.dtP2.TabIndex = 202;
            this.dtP2.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // subrb
            // 
            this.subrb.AutoSize = true;
            this.subrb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subrb.Location = new System.Drawing.Point(254, 64);
            this.subrb.Name = "subrb";
            this.subrb.Size = new System.Drawing.Size(68, 17);
            this.subrb.TabIndex = 202;
            this.subrb.Text = "Subtract";
            this.subrb.UseVisualStyleBackColor = true;
            this.subrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // dtP1
            // 
            this.dtP1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtP1.Location = new System.Drawing.Point(60, 63);
            this.dtP1.Name = "dtP1";
            this.dtP1.Size = new System.Drawing.Size(99, 22);
            this.dtP1.TabIndex = 201;
            this.dtP1.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // addrb
            // 
            this.addrb.AutoSize = true;
            this.addrb.Checked = true;
            this.addrb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addrb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.addrb.Location = new System.Drawing.Point(176, 64);
            this.addrb.Name = "addrb";
            this.addrb.Size = new System.Drawing.Size(46, 17);
            this.addrb.TabIndex = 202;
            this.addrb.TabStop = true;
            this.addrb.Text = "Add";
            this.addrb.UseVisualStyleBackColor = true;
            this.addrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // lbResult2
            // 
            this.lbResult2.AutoSize = true;
            this.lbResult2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.lbResult2.Location = new System.Drawing.Point(12, 140);
            this.lbResult2.Name = "lbResult2";
            this.lbResult2.Size = new System.Drawing.Size(92, 13);
            this.lbResult2.TabIndex = 67;
            this.lbResult2.Text = "Difference (days)";
            this.lbResult2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // firstDate
            // 
            this.firstDate.AutoSize = true;
            this.firstDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.firstDate.Location = new System.Drawing.Point(12, 67);
            this.firstDate.Name = "firstDate";
            this.firstDate.Size = new System.Drawing.Size(33, 13);
            this.firstDate.TabIndex = 59;
            this.firstDate.Text = "From";
            this.firstDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // autocal_date
            // 
            this.autocal_date.AutoSize = true;
            this.autocal_date.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autocal_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.autocal_date.Location = new System.Drawing.Point(12, 205);
            this.autocal_date.Name = "autocal_date";
            this.autocal_date.Size = new System.Drawing.Size(101, 17);
            this.autocal_date.TabIndex = 210;
            this.autocal_date.Text = "A&uto Calculate";
            this.autocal_date.UseVisualStyleBackColor = true;
            this.autocal_date.CheckedChanged += new System.EventHandler(this.autocal_date_CheckedChanged);
            this.autocal_date.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // lbResult1
            // 
            this.lbResult1.AutoSize = true;
            this.lbResult1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.lbResult1.Location = new System.Drawing.Point(12, 90);
            this.lbResult1.Name = "lbResult1";
            this.lbResult1.Size = new System.Drawing.Size(207, 13);
            this.lbResult1.TabIndex = 64;
            this.lbResult1.Text = "Difference (years, months, weeks, days)";
            this.lbResult1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // dateCalculationBT
            // 
            this.dateCalculationBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalculationBT.Location = new System.Drawing.Point(260, 201);
            this.dateCalculationBT.Name = "dateCalculationBT";
            this.dateCalculationBT.Size = new System.Drawing.Size(82, 22);
            this.dateCalculationBT.TabIndex = 211;
            this.dateCalculationBT.TabStop = false;
            this.dateCalculationBT.Text = "&Calculate";
            this.dateCalculationBT.UseVisualStyleBackColor = true;
            this.dateCalculationBT.Click += new System.EventHandler(this.dateCalculationBT_Click);
            this.dateCalculationBT.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // datemethodCB
            // 
            this.datemethodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datemethodCB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datemethodCB.FormattingEnabled = true;
            this.datemethodCB.Items.AddRange(new object[] {
            "Calculate the difference between two dates",
            "Add or subtract days to a specified date",
            "Convert to lunar date from a solar date"});
            this.datemethodCB.Location = new System.Drawing.Point(12, 31);
            this.datemethodCB.Name = "datemethodCB";
            this.datemethodCB.Size = new System.Drawing.Size(330, 21);
            this.datemethodCB.TabIndex = 200;
            this.datemethodCB.SelectedIndexChanged += new System.EventHandler(this.datemethodCB_SelectedIndexChanged);
            this.datemethodCB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.datemethodCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typeCB_MouseDown);
            // 
            // yearAddSubLB
            // 
            this.yearAddSubLB.AutoSize = true;
            this.yearAddSubLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearAddSubLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.yearAddSubLB.Location = new System.Drawing.Point(12, 107);
            this.yearAddSubLB.Name = "yearAddSubLB";
            this.yearAddSubLB.Size = new System.Drawing.Size(39, 13);
            this.yearAddSubLB.TabIndex = 64;
            this.yearAddSubLB.Text = "Year(s)";
            // 
            // monthAddSubLB
            // 
            this.monthAddSubLB.AutoSize = true;
            this.monthAddSubLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthAddSubLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.monthAddSubLB.Location = new System.Drawing.Point(125, 107);
            this.monthAddSubLB.Name = "monthAddSubLB";
            this.monthAddSubLB.Size = new System.Drawing.Size(53, 13);
            this.monthAddSubLB.TabIndex = 64;
            this.monthAddSubLB.Text = "Month(s)";
            // 
            // dayAddSubLB
            // 
            this.dayAddSubLB.AutoSize = true;
            this.dayAddSubLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayAddSubLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.dayAddSubLB.Location = new System.Drawing.Point(243, 107);
            this.dayAddSubLB.Name = "dayAddSubLB";
            this.dayAddSubLB.Size = new System.Drawing.Size(37, 13);
            this.dayAddSubLB.TabIndex = 64;
            this.dayAddSubLB.Text = "Day(s)";
            // 
            // periodsDateUD
            // 
            this.periodsDateUD.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsDateUD.Location = new System.Drawing.Point(284, 105);
            this.periodsDateUD.Maximum = new decimal(new int[] {
            730000,
            0,
            0,
            0});
            this.periodsDateUD.Name = "periodsDateUD";
            this.periodsDateUD.Size = new System.Drawing.Size(58, 22);
            this.periodsDateUD.TabIndex = 205;
            this.periodsDateUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsDateUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsMonthUD
            // 
            this.periodsMonthUD.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsMonthUD.Location = new System.Drawing.Point(181, 105);
            this.periodsMonthUD.Maximum = new decimal(new int[] {
            24000,
            0,
            0,
            0});
            this.periodsMonthUD.Name = "periodsMonthUD";
            this.periodsMonthUD.Size = new System.Drawing.Size(44, 22);
            this.periodsMonthUD.TabIndex = 204;
            this.periodsMonthUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsMonthUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsYearUD
            // 
            this.periodsYearUD.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsYearUD.Location = new System.Drawing.Point(60, 105);
            this.periodsYearUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.periodsYearUD.Name = "periodsYearUD";
            this.periodsYearUD.Size = new System.Drawing.Size(44, 22);
            this.periodsYearUD.TabIndex = 203;
            this.periodsYearUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsYearUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // num9BT_PN
            // 
            this.num9BT_PN.AllowDrawBorder = false;
            this.num9BT_PN.ContextMenu = this.helpCTMN;
            this.num9BT_PN.Controls.Add(this.num9BT);
            this.num9BT_PN.Location = new System.Drawing.Point(90, 131);
            this.num9BT_PN.Name = "num9BT_PN";
            this.num9BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num9BT_PN.TabIndex = 252;
            this.num9BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num9BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num9BT
            // 
            this.num9BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num9BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9BT.Location = new System.Drawing.Point(0, 0);
            this.num9BT.Name = "num9BT";
            this.num9BT.Size = new System.Drawing.Size(34, 27);
            this.num9BT.TabIndex = 9;
            this.num9BT.TabStop = false;
            this.num9BT.Text = "9";
            this.num9BT.UseVisualStyleBackColor = true;
            this.num9BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num9BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num9BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num9BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num7BT_PN
            // 
            this.num7BT_PN.AllowDrawBorder = false;
            this.num7BT_PN.ContextMenu = this.helpCTMN;
            this.num7BT_PN.Controls.Add(this.num7BT);
            this.num7BT_PN.Location = new System.Drawing.Point(12, 130);
            this.num7BT_PN.Name = "num7BT_PN";
            this.num7BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num7BT_PN.TabIndex = 252;
            this.num7BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num7BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num7BT
            // 
            this.num7BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num7BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7BT.Location = new System.Drawing.Point(0, 0);
            this.num7BT.Name = "num7BT";
            this.num7BT.Size = new System.Drawing.Size(34, 27);
            this.num7BT.TabIndex = 7;
            this.num7BT.TabStop = false;
            this.num7BT.Text = "7";
            this.num7BT.UseVisualStyleBackColor = true;
            this.num7BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num7BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num7BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num7BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num8BT_PN
            // 
            this.num8BT_PN.AllowDrawBorder = false;
            this.num8BT_PN.ContextMenu = this.helpCTMN;
            this.num8BT_PN.Controls.Add(this.num8BT);
            this.num8BT_PN.Location = new System.Drawing.Point(51, 130);
            this.num8BT_PN.Name = "num8BT_PN";
            this.num8BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num8BT_PN.TabIndex = 252;
            this.num8BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num8BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num8BT
            // 
            this.num8BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num8BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8BT.Location = new System.Drawing.Point(0, 0);
            this.num8BT.Name = "num8BT";
            this.num8BT.Size = new System.Drawing.Size(34, 27);
            this.num8BT.TabIndex = 8;
            this.num8BT.TabStop = false;
            this.num8BT.Text = "8";
            this.num8BT.UseVisualStyleBackColor = true;
            this.num8BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num8BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num8BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num8BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num6BT_PN
            // 
            this.num6BT_PN.AllowDrawBorder = false;
            this.num6BT_PN.ContextMenu = this.helpCTMN;
            this.num6BT_PN.Controls.Add(this.num6BT);
            this.num6BT_PN.Location = new System.Drawing.Point(90, 162);
            this.num6BT_PN.Name = "num6BT_PN";
            this.num6BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num6BT_PN.TabIndex = 252;
            this.num6BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num6BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num6BT
            // 
            this.num6BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num6BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6BT.Location = new System.Drawing.Point(0, 0);
            this.num6BT.Name = "num6BT";
            this.num6BT.Size = new System.Drawing.Size(34, 27);
            this.num6BT.TabIndex = 6;
            this.num6BT.TabStop = false;
            this.num6BT.Text = "6";
            this.num6BT.UseVisualStyleBackColor = true;
            this.num6BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num6BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num6BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num6BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num5BT_PN
            // 
            this.num5BT_PN.AllowDrawBorder = false;
            this.num5BT_PN.ContextMenu = this.helpCTMN;
            this.num5BT_PN.Controls.Add(this.num5BT);
            this.num5BT_PN.Location = new System.Drawing.Point(51, 162);
            this.num5BT_PN.Name = "num5BT_PN";
            this.num5BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num5BT_PN.TabIndex = 252;
            this.num5BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num5BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num5BT
            // 
            this.num5BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num5BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5BT.Location = new System.Drawing.Point(0, 0);
            this.num5BT.Name = "num5BT";
            this.num5BT.Size = new System.Drawing.Size(34, 27);
            this.num5BT.TabIndex = 5;
            this.num5BT.TabStop = false;
            this.num5BT.Text = "5";
            this.num5BT.UseVisualStyleBackColor = true;
            this.num5BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num5BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num5BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num5BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num3BT_PN
            // 
            this.num3BT_PN.AllowDrawBorder = false;
            this.num3BT_PN.ContextMenu = this.helpCTMN;
            this.num3BT_PN.Controls.Add(this.num3BT);
            this.num3BT_PN.Location = new System.Drawing.Point(90, 194);
            this.num3BT_PN.Name = "num3BT_PN";
            this.num3BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num3BT_PN.TabIndex = 252;
            this.num3BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num3BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num3BT
            // 
            this.num3BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num3BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3BT.Location = new System.Drawing.Point(0, 0);
            this.num3BT.Name = "num3BT";
            this.num3BT.Size = new System.Drawing.Size(34, 27);
            this.num3BT.TabIndex = 3;
            this.num3BT.TabStop = false;
            this.num3BT.Text = "3";
            this.num3BT.UseVisualStyleBackColor = true;
            this.num3BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num3BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num3BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num3BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num4BT_PN
            // 
            this.num4BT_PN.AllowDrawBorder = false;
            this.num4BT_PN.ContextMenu = this.helpCTMN;
            this.num4BT_PN.Controls.Add(this.num4BT);
            this.num4BT_PN.Location = new System.Drawing.Point(12, 162);
            this.num4BT_PN.Name = "num4BT_PN";
            this.num4BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num4BT_PN.TabIndex = 252;
            this.num4BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num4BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num4BT
            // 
            this.num4BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num4BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4BT.Location = new System.Drawing.Point(0, 0);
            this.num4BT.Name = "num4BT";
            this.num4BT.Size = new System.Drawing.Size(34, 27);
            this.num4BT.TabIndex = 4;
            this.num4BT.TabStop = false;
            this.num4BT.Text = "4";
            this.num4BT.UseVisualStyleBackColor = true;
            this.num4BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num4BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num4BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num4BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // num2BT_PN
            // 
            this.num2BT_PN.AllowDrawBorder = false;
            this.num2BT_PN.ContextMenu = this.helpCTMN;
            this.num2BT_PN.Controls.Add(this.num2BT);
            this.num2BT_PN.Location = new System.Drawing.Point(51, 194);
            this.num2BT_PN.Name = "num2BT_PN";
            this.num2BT_PN.Size = new System.Drawing.Size(34, 27);
            this.num2BT_PN.TabIndex = 252;
            this.num2BT_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.num2BT_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // num2BT
            // 
            this.num2BT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2BT.Location = new System.Drawing.Point(0, 0);
            this.num2BT.Name = "num2BT";
            this.num2BT.Size = new System.Drawing.Size(34, 27);
            this.num2BT.TabIndex = 2;
            this.num2BT.TabStop = false;
            this.num2BT.Text = "2";
            this.num2BT.UseVisualStyleBackColor = true;
            this.num2BT.Click += new System.EventHandler(this.numberinput_Click);
            this.num2BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.num2BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.num2BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // percentbt_PN
            // 
            this.percentbt_PN.AllowDrawBorder = false;
            this.percentbt_PN.ContextMenu = this.helpCTMN;
            this.percentbt_PN.Controls.Add(this.percent_bt);
            this.percentbt_PN.Location = new System.Drawing.Point(168, 130);
            this.percentbt_PN.Name = "percentbt_PN";
            this.percentbt_PN.Size = new System.Drawing.Size(34, 27);
            this.percentbt_PN.TabIndex = 252;
            this.percentbt_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.percentbt_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // percent_bt
            // 
            this.percent_bt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percent_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent_bt.Location = new System.Drawing.Point(0, 0);
            this.percent_bt.Name = "percent_bt";
            this.percent_bt.Size = new System.Drawing.Size(34, 27);
            this.percent_bt.TabIndex = 18;
            this.percent_bt.TabStop = false;
            this.percent_bt.Text = "%";
            this.percent_bt.UseVisualStyleBackColor = true;
            this.percent_bt.Click += new System.EventHandler(this.percent_bt_Click);
            this.percent_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.percent_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.percent_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btdot_PN
            // 
            this.btdot_PN.AllowDrawBorder = false;
            this.btdot_PN.ContextMenu = this.helpCTMN;
            this.btdot_PN.Controls.Add(this.btdot);
            this.btdot_PN.Location = new System.Drawing.Point(90, 226);
            this.btdot_PN.Name = "btdot_PN";
            this.btdot_PN.Size = new System.Drawing.Size(34, 27);
            this.btdot_PN.TabIndex = 252;
            this.btdot_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btdot_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btdot
            // 
            this.btdot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(0, 0);
            this.btdot.Name = "btdot";
            this.btdot.Size = new System.Drawing.Size(34, 27);
            this.btdot.TabIndex = 10;
            this.btdot.TabStop = false;
            this.btdot.Text = ".";
            this.btdot.UseVisualStyleBackColor = true;
            this.btdot.Click += new System.EventHandler(this.numberinput_Click);
            this.btdot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btdot.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btdot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // invertbt_PN
            // 
            this.invertbt_PN.AllowDrawBorder = false;
            this.invertbt_PN.ContextMenu = this.helpCTMN;
            this.invertbt_PN.Controls.Add(this.invert_bt);
            this.invertbt_PN.Location = new System.Drawing.Point(168, 162);
            this.invertbt_PN.Name = "invertbt_PN";
            this.invertbt_PN.Size = new System.Drawing.Size(34, 27);
            this.invertbt_PN.TabIndex = 252;
            this.invertbt_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.invertbt_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // invert_bt
            // 
            this.invert_bt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invert_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.invert_bt.Location = new System.Drawing.Point(0, 0);
            this.invert_bt.Name = "invert_bt";
            this.invert_bt.Size = new System.Drawing.Size(34, 27);
            this.invert_bt.TabIndex = 17;
            this.invert_bt.TabStop = false;
            this.invert_bt.Text = "1/x";
            this.invert_bt.UseVisualStyleBackColor = true;
            this.invert_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.invert_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.invert_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.invert_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // sqrtbt_PN
            // 
            this.sqrtbt_PN.AllowDrawBorder = false;
            this.sqrtbt_PN.ContextMenu = this.helpCTMN;
            this.sqrtbt_PN.Controls.Add(this.sqrt_bt);
            this.sqrtbt_PN.Location = new System.Drawing.Point(168, 98);
            this.sqrtbt_PN.Name = "sqrtbt_PN";
            this.sqrtbt_PN.Size = new System.Drawing.Size(34, 27);
            this.sqrtbt_PN.TabIndex = 252;
            this.sqrtbt_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.sqrtbt_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // sqrt_bt
            // 
            this.sqrt_bt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqrt_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sqrt_bt.Location = new System.Drawing.Point(0, 0);
            this.sqrt_bt.Name = "sqrt_bt";
            this.sqrt_bt.Size = new System.Drawing.Size(34, 27);
            this.sqrt_bt.TabIndex = 19;
            this.sqrt_bt.TabStop = false;
            this.sqrt_bt.Text = "√";
            this.sqrt_bt.UseVisualStyleBackColor = true;
            this.sqrt_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sqrt_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.sqrt_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.sqrt_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnF_PN
            // 
            this.btnF_PN.AllowDrawBorder = false;
            this.btnF_PN.ContextMenu = this.helpCTMN;
            this.btnF_PN.Controls.Add(this.btnF);
            this.btnF_PN.Location = new System.Drawing.Point(99, 371);
            this.btnF_PN.Name = "btnF_PN";
            this.btnF_PN.Size = new System.Drawing.Size(34, 27);
            this.btnF_PN.TabIndex = 252;
            this.btnF_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnF_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnF
            // 
            this.btnF.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnF.Location = new System.Drawing.Point(0, 0);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(34, 27);
            this.btnF.TabIndex = 206;
            this.btnF.TabStop = false;
            this.btnF.Text = "F";
            this.btnF.UseVisualStyleBackColor = true;
            this.btnF.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnF.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnF.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnC_PN
            // 
            this.btnC_PN.AllowDrawBorder = false;
            this.btnC_PN.ContextMenu = this.helpCTMN;
            this.btnC_PN.Controls.Add(this.btnC);
            this.btnC_PN.Location = new System.Drawing.Point(99, 371);
            this.btnC_PN.Name = "btnC_PN";
            this.btnC_PN.Size = new System.Drawing.Size(34, 27);
            this.btnC_PN.TabIndex = 252;
            this.btnC_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnC_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnC.Location = new System.Drawing.Point(0, 0);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(34, 27);
            this.btnC.TabIndex = 203;
            this.btnC.TabStop = false;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnC.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnE_PN
            // 
            this.btnE_PN.AllowDrawBorder = false;
            this.btnE_PN.ContextMenu = this.helpCTMN;
            this.btnE_PN.Controls.Add(this.btnE);
            this.btnE_PN.Location = new System.Drawing.Point(99, 371);
            this.btnE_PN.Name = "btnE_PN";
            this.btnE_PN.Size = new System.Drawing.Size(34, 27);
            this.btnE_PN.TabIndex = 252;
            this.btnE_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnE_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnE.Location = new System.Drawing.Point(0, 0);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(34, 27);
            this.btnE.TabIndex = 205;
            this.btnE.TabStop = false;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnE.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnB_PN
            // 
            this.btnB_PN.AllowDrawBorder = false;
            this.btnB_PN.ContextMenu = this.helpCTMN;
            this.btnB_PN.Controls.Add(this.btnB);
            this.btnB_PN.Location = new System.Drawing.Point(99, 371);
            this.btnB_PN.Name = "btnB_PN";
            this.btnB_PN.Size = new System.Drawing.Size(34, 27);
            this.btnB_PN.TabIndex = 252;
            this.btnB_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnB_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnB
            // 
            this.btnB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnB.Location = new System.Drawing.Point(0, 0);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(34, 27);
            this.btnB.TabIndex = 202;
            this.btnB.TabStop = false;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnD_PN
            // 
            this.btnD_PN.AllowDrawBorder = false;
            this.btnD_PN.ContextMenu = this.helpCTMN;
            this.btnD_PN.Controls.Add(this.btnD);
            this.btnD_PN.Location = new System.Drawing.Point(99, 371);
            this.btnD_PN.Name = "btnD_PN";
            this.btnD_PN.Size = new System.Drawing.Size(34, 27);
            this.btnD_PN.TabIndex = 252;
            this.btnD_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnD_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnD
            // 
            this.btnD.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnD.Location = new System.Drawing.Point(0, 0);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(34, 27);
            this.btnD.TabIndex = 204;
            this.btnD.TabStop = false;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnD.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnD.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnA_PN
            // 
            this.btnA_PN.AllowDrawBorder = false;
            this.btnA_PN.ContextMenu = this.helpCTMN;
            this.btnA_PN.Controls.Add(this.btnA);
            this.btnA_PN.Location = new System.Drawing.Point(99, 371);
            this.btnA_PN.Name = "btnA_PN";
            this.btnA_PN.Size = new System.Drawing.Size(34, 27);
            this.btnA_PN.TabIndex = 252;
            this.btnA_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.btnA_PN.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnA
            // 
            this.btnA.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnA.Location = new System.Drawing.Point(0, 0);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(34, 27);
            this.btnA.TabIndex = 201;
            this.btnA.TabStop = false;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.btnA.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // PNbinary
            // 
            this.PNbinary.BackColor = System.Drawing.Color.Transparent;
            this.PNbinary.Location = new System.Drawing.Point(186, 415);
            this.PNbinary.Name = "PNbinary";
            this.PNbinary.Size = new System.Drawing.Size(385, 60);
            this.PNbinary.TabIndex = 214;
            this.PNbinary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // screenPN
            // 
            this.screenPN.BackColor = System.Drawing.Color.White;
            this.screenPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenPN.ContextMenu = this.mainContextMenu;
            this.screenPN.Controls.Add(this.bin_prvLb);
            this.screenPN.Controls.Add(this.expressionTB);
            this.screenPN.Controls.Add(this.oct_prvLb);
            this.screenPN.Controls.Add(this.mem_lb);
            this.screenPN.Controls.Add(this.dec_prvLb);
            this.screenPN.Controls.Add(this.scr_lb);
            this.screenPN.Controls.Add(this.hex_prvLb);
            this.screenPN.Location = new System.Drawing.Point(12, 12);
            this.screenPN.Name = "screenPN";
            this.screenPN.Size = new System.Drawing.Size(190, 47);
            this.screenPN.TabIndex = 154;
            this.screenPN.TabStop = true;
            this.screenPN.BackColorChanged += new System.EventHandler(this.screenPN_BackColorChanged);
            this.screenPN.SizeChanged += new System.EventHandler(this.screenPN_SizeChanged);
            this.screenPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // bin_prvLb
            // 
            this.bin_prvLb.AllowLinkClick = true;
            this.bin_prvLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bin_prvLb.AutoEllipsis = true;
            this.bin_prvLb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bin_prvLb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.bin_prvLb.Location = new System.Drawing.Point(3, -15);
            this.bin_prvLb.Name = "bin_prvLb";
            this.bin_prvLb.Size = new System.Drawing.Size(36, 13);
            this.bin_prvLb.TabIndex = 0;
            this.bin_prvLb.Text = "Bin: 0";
            this.bin_prvLb.TextChanged += new System.EventHandler(this.prvlbBin_TextChanged);
            this.bin_prvLb.Click += new System.EventHandler(this.prvlb_Click);
            // 
            // expressionTB
            // 
            this.expressionTB.BackColor = System.Drawing.Color.Transparent;
            this.expressionTB.ContextMenu = this.mainContextMenu;
            this.expressionTB.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.expressionTB.Location = new System.Drawing.Point(13, 0);
            this.expressionTB.Name = "expressionTB";
            this.expressionTB.Size = new System.Drawing.Size(175, 13);
            this.expressionTB.TabIndex = 251;
            this.expressionTB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.expressionTB.TextChanged += new System.EventHandler(this.expressionTB_TextChanged);
            this.expressionTB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // oct_prvLb
            // 
            this.oct_prvLb.AllowLinkClick = true;
            this.oct_prvLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.oct_prvLb.AutoSize = true;
            this.oct_prvLb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.oct_prvLb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.oct_prvLb.Location = new System.Drawing.Point(3, -33);
            this.oct_prvLb.Name = "oct_prvLb";
            this.oct_prvLb.Size = new System.Drawing.Size(37, 13);
            this.oct_prvLb.TabIndex = 0;
            this.oct_prvLb.Text = "Oct: 0";
            this.oct_prvLb.Click += new System.EventHandler(this.prvlb_Click);
            // 
            // mem_lb
            // 
            this.mem_lb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.Transparent;
            this.mem_lb.Location = new System.Drawing.Point(1, 26);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(17, 13);
            this.mem_lb.TabIndex = 21;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            this.mem_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // dec_prvLb
            // 
            this.dec_prvLb.AllowLinkClick = true;
            this.dec_prvLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dec_prvLb.AutoSize = true;
            this.dec_prvLb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dec_prvLb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dec_prvLb.ForeColor = System.Drawing.Color.Red;
            this.dec_prvLb.Location = new System.Drawing.Point(3, -51);
            this.dec_prvLb.Name = "dec_prvLb";
            this.dec_prvLb.Size = new System.Drawing.Size(38, 13);
            this.dec_prvLb.TabIndex = 0;
            this.dec_prvLb.Text = "Dec: 0";
            this.dec_prvLb.Click += new System.EventHandler(this.prvlb_Click);
            // 
            // scr_lb
            // 
            this.scr_lb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scr_lb.BackColor = System.Drawing.Color.Transparent;
            this.scr_lb.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.scr_lb.Location = new System.Drawing.Point(13, 0);
            this.scr_lb.Name = "scr_lb";
            this.scr_lb.Size = new System.Drawing.Size(175, 45);
            this.scr_lb.TabIndex = 22;
            this.scr_lb.Text = "0";
            this.scr_lb.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.scr_lb.TextChanged += new System.EventHandler(this.scr_lb_TextChanged);
            this.scr_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.scr_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // hex_prvLb
            // 
            this.hex_prvLb.AllowLinkClick = true;
            this.hex_prvLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hex_prvLb.AutoSize = true;
            this.hex_prvLb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hex_prvLb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hex_prvLb.Location = new System.Drawing.Point(3, -69);
            this.hex_prvLb.Name = "hex_prvLb";
            this.hex_prvLb.Size = new System.Drawing.Size(38, 13);
            this.hex_prvLb.TabIndex = 0;
            this.hex_prvLb.Text = "Hex: 0";
            this.hex_prvLb.Click += new System.EventHandler(this.prvlb_Click);
            // 
            // anglePN
            // 
            this.anglePN.Controls.Add(this.graRB);
            this.anglePN.Controls.Add(this.degRB);
            this.anglePN.Controls.Add(this.radRB);
            this.anglePN.Location = new System.Drawing.Point(12, 279);
            this.anglePN.Name = "anglePN";
            this.anglePN.Size = new System.Drawing.Size(190, 28);
            this.anglePN.TabIndex = 128;
            this.anglePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FocusAndMoveForm);
            // 
            // graRB
            // 
            this.graRB.AutoSize = true;
            this.graRB.ContextMenu = this.helpCTMN;
            this.graRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graRB.Location = new System.Drawing.Point(134, 6);
            this.graRB.Name = "graRB";
            this.graRB.Size = new System.Drawing.Size(55, 17);
            this.graRB.TabIndex = 116;
            this.graRB.Text = "Grads";
            this.graRB.UseVisualStyleBackColor = true;
            this.graRB.Value = null;
            this.graRB.CheckedChanged += new System.EventHandler(this.angelPN_CheckedChanged);
            this.graRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.graRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // degRB
            // 
            this.degRB.AutoSize = true;
            this.degRB.Checked = true;
            this.degRB.ContextMenu = this.helpCTMN;
            this.degRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degRB.Location = new System.Drawing.Point(4, 6);
            this.degRB.Name = "degRB";
            this.degRB.Size = new System.Drawing.Size(67, 17);
            this.degRB.TabIndex = 114;
            this.degRB.TabStop = true;
            this.degRB.Text = "Degrees";
            this.degRB.UseVisualStyleBackColor = true;
            this.degRB.Value = null;
            this.degRB.CheckedChanged += new System.EventHandler(this.angelPN_CheckedChanged);
            this.degRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.degRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // radRB
            // 
            this.radRB.AutoSize = true;
            this.radRB.ContextMenu = this.helpCTMN;
            this.radRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRB.Location = new System.Drawing.Point(71, 6);
            this.radRB.Name = "radRB";
            this.radRB.Size = new System.Drawing.Size(66, 17);
            this.radRB.TabIndex = 115;
            this.radRB.Text = "Radians";
            this.radRB.UseVisualStyleBackColor = true;
            this.radRB.Value = null;
            this.radRB.CheckedChanged += new System.EventHandler(this.angelPN_CheckedChanged);
            this.radRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.radRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // gridPanel
            // 
            this.gridPanel.BackColor = System.Drawing.Color.White;
            this.gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridPanel.Controls.Add(this.dnBT);
            this.gridPanel.Controls.Add(this.upBT);
            this.gridPanel.Controls.Add(this.countLB);
            this.gridPanel.Controls.Add(this.hisDGV);
            this.gridPanel.Controls.Add(this.staDGV);
            this.gridPanel.Location = new System.Drawing.Point(236, 291);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(190, 105);
            this.gridPanel.TabIndex = 186;
            this.gridPanel.TabStop = true;
            this.gridPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // countLB
            // 
            this.countLB.AutoSize = true;
            this.countLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countLB.Location = new System.Drawing.Point(4, 4);
            this.countLB.Name = "countLB";
            this.countLB.Size = new System.Drawing.Size(41, 13);
            this.countLB.TabIndex = 2;
            this.countLB.Text = "∑n = 0";
            this.countLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // hisDGV
            // 
            this.hisDGV.AllowCellClick = true;
            this.hisDGV.AllowCellDoubleClick = true;
            this.hisDGV.AllowCellStateChanged = true;
            this.hisDGV.AllowUserToAddRows = false;
            this.hisDGV.AllowUserToDeleteRows = false;
            this.hisDGV.AllowUserToResizeColumns = false;
            this.hisDGV.AllowUserToResizeRows = false;
            this.hisDGV.BackgroundColor = System.Drawing.Color.White;
            this.hisDGV.ColumnHeadersVisible = false;
            this.hisDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hisDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.hisDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.hisDGV.IsCancelEdit = false;
            this.hisDGV.Location = new System.Drawing.Point(-1, 21);
            this.hisDGV.MultiSelect = false;
            this.hisDGV.Name = "hisDGV";
            this.hisDGV.ReadOnly = true;
            this.hisDGV.RowHeadersVisible = false;
            this.hisDGV.RowHeadersWidth = 35;
            this.hisDGV.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.hisDGV.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hisDGV.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hisDGV.RowTemplate.Height = 20;
            this.hisDGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.hisDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.hisDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.hisDGV.Size = new System.Drawing.Size(190, 85);
            this.hisDGV.TabIndex = 0;
            this.hisDGV.TabStop = false;
            this.hisDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.historyDGV_CellBeginEdit);
            this.hisDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.hisDGV_CellClick);
            this.hisDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.hisDGV_CellEndEdit);
            this.hisDGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.hisDGV_RowsAdded);
            this.hisDGV.DoubleClick += new System.EventHandler(this.dGV_DoubleClick);
            this.hisDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // staDGV
            // 
            this.staDGV.AllowCellClick = true;
            this.staDGV.AllowCellDoubleClick = true;
            this.staDGV.AllowCellStateChanged = false;
            this.staDGV.AllowUserToAddRows = false;
            this.staDGV.AllowUserToDeleteRows = false;
            this.staDGV.AllowUserToResizeColumns = false;
            this.staDGV.AllowUserToResizeRows = false;
            this.staDGV.BackgroundColor = System.Drawing.Color.White;
            this.staDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.staDGV.ColumnHeadersVisible = false;
            this.staDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.staDGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.staDGV.IsCancelEdit = false;
            this.staDGV.Location = new System.Drawing.Point(-1, 21);
            this.staDGV.MultiSelect = false;
            this.staDGV.Name = "staDGV";
            this.staDGV.ReadOnly = true;
            this.staDGV.RowHeadersVisible = false;
            this.staDGV.RowHeadersWidth = 20;
            this.staDGV.RowTemplate.Height = 20;
            this.staDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.staDGV.Size = new System.Drawing.Size(190, 85);
            this.staDGV.TabIndex = 0;
            this.staDGV.TabStop = false;
            this.staDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.staDGV_CellBeginEdit);
            this.staDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.staDGV_CellClick);
            this.staDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.staDGV_CellEndEdit);
            this.staDGV.DoubleClick += new System.EventHandler(this.dGV_DoubleClick);
            this.staDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 190;
            // 
            // basePN
            // 
            this.basePN.Controls.Add(this.hexRB);
            this.basePN.Controls.Add(this.decRB);
            this.basePN.Controls.Add(this.octRB);
            this.basePN.Controls.Add(this.binRB);
            this.basePN.Location = new System.Drawing.Point(12, 366);
            this.basePN.Name = "basePN";
            this.basePN.Size = new System.Drawing.Size(73, 91);
            this.basePN.TabIndex = 212;
            this.basePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FocusAndMoveForm);
            // 
            // hexRB
            // 
            this.hexRB.AutoSize = true;
            this.hexRB.ContextMenu = this.helpCTMN;
            this.hexRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.hexRB.Location = new System.Drawing.Point(7, 4);
            this.hexRB.Name = "hexRB";
            this.hexRB.Size = new System.Drawing.Size(44, 17);
            this.hexRB.TabIndex = 116;
            this.hexRB.Text = "Hex";
            this.hexRB.UseVisualStyleBackColor = true;
            this.hexRB.Value = "0";
            this.hexRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            this.hexRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.hexRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // decRB
            // 
            this.decRB.AutoSize = true;
            this.decRB.Checked = true;
            this.decRB.ContextMenu = this.helpCTMN;
            this.decRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.decRB.Location = new System.Drawing.Point(7, 25);
            this.decRB.Name = "decRB";
            this.decRB.Size = new System.Drawing.Size(44, 17);
            this.decRB.TabIndex = 117;
            this.decRB.TabStop = true;
            this.decRB.Text = "Dec";
            this.decRB.UseVisualStyleBackColor = true;
            this.decRB.Value = "0";
            this.decRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            this.decRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.decRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // octRB
            // 
            this.octRB.AutoSize = true;
            this.octRB.ContextMenu = this.helpCTMN;
            this.octRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.octRB.Location = new System.Drawing.Point(7, 46);
            this.octRB.Name = "octRB";
            this.octRB.Size = new System.Drawing.Size(43, 17);
            this.octRB.TabIndex = 118;
            this.octRB.Text = "Oct";
            this.octRB.UseVisualStyleBackColor = true;
            this.octRB.Value = "0";
            this.octRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            this.octRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.octRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // binRB
            // 
            this.binRB.AutoSize = true;
            this.binRB.ContextMenu = this.helpCTMN;
            this.binRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.binRB.Location = new System.Drawing.Point(7, 67);
            this.binRB.Name = "binRB";
            this.binRB.Size = new System.Drawing.Size(42, 17);
            this.binRB.TabIndex = 119;
            this.binRB.Text = "Bin";
            this.binRB.UseVisualStyleBackColor = true;
            this.binRB.Value = "0";
            this.binRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            this.binRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.binRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // unknownPN
            // 
            this.unknownPN.Controls.Add(this._byteRB);
            this.unknownPN.Controls.Add(this.qwordRB);
            this.unknownPN.Controls.Add(this.dwordRB);
            this.unknownPN.Controls.Add(this._wordRB);
            this.unknownPN.Location = new System.Drawing.Point(12, 366);
            this.unknownPN.Name = "unknownPN";
            this.unknownPN.Size = new System.Drawing.Size(73, 91);
            this.unknownPN.TabIndex = 213;
            this.unknownPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FocusAndMoveForm);
            // 
            // _byteRB
            // 
            this._byteRB.AutoSize = true;
            this._byteRB.ContextMenu = this.helpCTMN;
            this._byteRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._byteRB.Location = new System.Drawing.Point(7, 67);
            this._byteRB.Name = "_byteRB";
            this._byteRB.Size = new System.Drawing.Size(47, 17);
            this._byteRB.TabIndex = 2;
            this._byteRB.Text = "Byte";
            this._byteRB.UseVisualStyleBackColor = true;
            this._byteRB.Value = null;
            this._byteRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this._byteRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._byteRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // qwordRB
            // 
            this.qwordRB.AutoSize = true;
            this.qwordRB.Checked = true;
            this.qwordRB.ContextMenu = this.helpCTMN;
            this.qwordRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qwordRB.Location = new System.Drawing.Point(7, 4);
            this.qwordRB.Name = "qwordRB";
            this.qwordRB.Size = new System.Drawing.Size(61, 17);
            this.qwordRB.TabIndex = 16;
            this.qwordRB.TabStop = true;
            this.qwordRB.Text = "Qword";
            this.qwordRB.UseVisualStyleBackColor = true;
            this.qwordRB.Value = null;
            this.qwordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this.qwordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.qwordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // dwordRB
            // 
            this.dwordRB.AutoSize = true;
            this.dwordRB.ContextMenu = this.helpCTMN;
            this.dwordRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwordRB.Location = new System.Drawing.Point(7, 25);
            this.dwordRB.Name = "dwordRB";
            this.dwordRB.Size = new System.Drawing.Size(60, 17);
            this.dwordRB.TabIndex = 8;
            this.dwordRB.Text = "Dword";
            this.dwordRB.UseVisualStyleBackColor = true;
            this.dwordRB.Value = null;
            this.dwordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this.dwordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.dwordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // _wordRB
            // 
            this._wordRB.AutoSize = true;
            this._wordRB.ContextMenu = this.helpCTMN;
            this._wordRB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._wordRB.Location = new System.Drawing.Point(7, 46);
            this._wordRB.Name = "_wordRB";
            this._wordRB.Size = new System.Drawing.Size(54, 17);
            this._wordRB.TabIndex = 4;
            this._wordRB.Text = "Word";
            this._wordRB.UseVisualStyleBackColor = true;
            this._wordRB.Value = null;
            this._wordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this._wordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._wordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // unitconvPN
            // 
            this.unitconvPN.BackColor = System.Drawing.Color.White;
            this.unitconvPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unitconvPN.Controls.Add(this.toCB);
            this.unitconvPN.Controls.Add(this.typeUnitLB);
            this.unitconvPN.Controls.Add(this.fromCB);
            this.unitconvPN.Controls.Add(this.typeUnitCB);
            this.unitconvPN.Controls.Add(this.invert_unit);
            this.unitconvPN.Controls.Add(this.toLB);
            this.unitconvPN.Controls.Add(this.toTB);
            this.unitconvPN.Controls.Add(this.fromLB);
            this.unitconvPN.Controls.Add(this.fromTB);
            this.unitconvPN.Location = new System.Drawing.Point(234, 12);
            this.unitconvPN.Name = "unitconvPN";
            this.unitconvPN.Size = new System.Drawing.Size(356, 241);
            this.unitconvPN.TabIndex = 31;
            this.unitconvPN.Visible = false;
            this.unitconvPN.LocationChanged += new System.EventHandler(this.unitconvGB_LocationChanged);
            this.unitconvPN.SizeChanged += new System.EventHandler(this.unitconvGB_SizeChanged);
            this.unitconvPN.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.unitconvPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // toCB
            // 
            this.toCB.AllowFilter = true;
            this.toCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toCB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toCB.FormattingEnabled = true;
            this.toCB.Location = new System.Drawing.Point(12, 181);
            this.toCB.MainDataSource = new string[0];
            this.toCB.Name = "toCB";
            this.toCB.Size = new System.Drawing.Size(330, 21);
            this.toCB.TabIndex = 14;
            this.toCB.SelectedIndexChanged += new System.EventHandler(this.fromCB_SelectedIndexChanged);
            this.toCB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeUnitLB
            // 
            this.typeUnitLB.AutoSize = true;
            this.typeUnitLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeUnitLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.typeUnitLB.Location = new System.Drawing.Point(12, 8);
            this.typeUnitLB.Name = "typeUnitLB";
            this.typeUnitLB.Size = new System.Drawing.Size(226, 13);
            this.typeUnitLB.TabIndex = 8;
            this.typeUnitLB.Text = "Select the type of unit you want to convert";
            this.typeUnitLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fromCB
            // 
            this.fromCB.AllowFilter = true;
            this.fromCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromCB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromCB.FormattingEnabled = true;
            this.fromCB.Location = new System.Drawing.Point(12, 104);
            this.fromCB.MainDataSource = new string[0];
            this.fromCB.MaxDropDownItems = 14;
            this.fromCB.Name = "fromCB";
            this.fromCB.Size = new System.Drawing.Size(330, 21);
            this.fromCB.TabIndex = 11;
            this.fromCB.SelectedIndexChanged += new System.EventHandler(this.fromCB_SelectedIndexChanged);
            this.fromCB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeUnitCB
            // 
            this.typeUnitCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeUnitCB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeUnitCB.FormattingEnabled = true;
            this.typeUnitCB.Items.AddRange(new object[] {
            "Angle",
            "Area",
            "Data",
            "Energy",
            "Length",
            "Power",
            "Pressure",
            "Temperature",
            "Time",
            "Velocity",
            "Volume",
            "Weight / Mass"});
            this.typeUnitCB.Location = new System.Drawing.Point(12, 31);
            this.typeUnitCB.MaxDropDownItems = 11;
            this.typeUnitCB.Name = "typeUnitCB";
            this.typeUnitCB.Size = new System.Drawing.Size(330, 21);
            this.typeUnitCB.TabIndex = 9;
            this.typeUnitCB.SelectedIndexChanged += new System.EventHandler(this.typeUnitCB_SelectedIndexChanged);
            this.typeUnitCB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.typeUnitCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typeCB_MouseDown);
            // 
            // invert_unit
            // 
            this.invert_unit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.invert_unit.Location = new System.Drawing.Point(11, 208);
            this.invert_unit.Name = "invert_unit";
            this.invert_unit.Size = new System.Drawing.Size(61, 27);
            this.invert_unit.TabIndex = 15;
            this.invert_unit.Text = "&Invert";
            this.invert_unit.UseVisualStyleBackColor = true;
            this.invert_unit.Click += new System.EventHandler(this.invert_unit_Click);
            this.invert_unit.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // toLB
            // 
            this.toLB.AutoSize = true;
            this.toLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.toLB.Location = new System.Drawing.Point(12, 138);
            this.toLB.Name = "toLB";
            this.toLB.Size = new System.Drawing.Size(19, 13);
            this.toLB.TabIndex = 7;
            this.toLB.Text = "To";
            this.toLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // toTB
            // 
            this.toTB.BackColor = System.Drawing.SystemColors.Control;
            this.toTB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toTB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toTB.Location = new System.Drawing.Point(12, 154);
            this.toTB.Name = "toTB";
            this.toTB.ReadOnly = true;
            this.toTB.Size = new System.Drawing.Size(330, 22);
            this.toTB.TabIndex = 12;
            this.toTB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // fromLB
            // 
            this.fromLB.AutoSize = true;
            this.fromLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.fromLB.Location = new System.Drawing.Point(12, 61);
            this.fromLB.Name = "fromLB";
            this.fromLB.Size = new System.Drawing.Size(33, 13);
            this.fromLB.TabIndex = 6;
            this.fromLB.Text = "From";
            this.fromLB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fromLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fromTB
            // 
            this.fromTB.AllowTextChanged = true;
            this.fromTB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.fromTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fromTB.Location = new System.Drawing.Point(12, 77);
            this.fromTB.MaxLength = 20;
            this.fromTB.Name = "fromTB";
            this.fromTB.NumberModeOnly = true;
            this.fromTB.Size = new System.Drawing.Size(330, 23);
            this.fromTB.SuggestText = "Enter value";
            this.fromTB.SuggestType = Calculator.SuggestType.WatermarkText;
            this.fromTB.TabIndex = 10;
            this.fromTB.Text = "Enter value";
            this.fromTB.TextChanged += new System.EventHandler(this.fromTB_TextChanged);
            this.fromTB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fromTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.fromTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // workSheetPN
            // 
            this.workSheetPN.BackColor = System.Drawing.Color.White;
            this.workSheetPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.workSheetPN.Controls.Add(this.typeMorgageLB);
            this.workSheetPN.Controls.Add(this.typeWorkSheetCB);
            this.workSheetPN.Controls.Add(this.workSheetCalculateBT);
            this.workSheetPN.Controls.Add(this.workSheetResultTB);
            this.workSheetPN.Location = new System.Drawing.Point(234, 12);
            this.workSheetPN.Name = "workSheetPN";
            this.workSheetPN.Size = new System.Drawing.Size(356, 241);
            this.workSheetPN.TabIndex = 34;
            this.workSheetPN.Visible = false;
            this.workSheetPN.FocusEvent += new System.EventHandler(this.workSheetPN_FocusEvent);
            this.workSheetPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeMorgageLB
            // 
            this.typeMorgageLB.AutoSize = true;
            this.typeMorgageLB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeMorgageLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(121)))));
            this.typeMorgageLB.Location = new System.Drawing.Point(12, 9);
            this.typeMorgageLB.Name = "typeMorgageLB";
            this.typeMorgageLB.Size = new System.Drawing.Size(200, 13);
            this.typeMorgageLB.TabIndex = 8;
            this.typeMorgageLB.Text = "Select the value you want to calculate";
            this.typeMorgageLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeWorkSheetCB
            // 
            this.typeWorkSheetCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeWorkSheetCB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeWorkSheetCB.FormattingEnabled = true;
            this.typeWorkSheetCB.Items.AddRange(new object[] {
            "Down payment",
            "Monthly payment",
            "Purchase price",
            "Term (years)"});
            this.typeWorkSheetCB.Location = new System.Drawing.Point(12, 32);
            this.typeWorkSheetCB.MaxDropDownItems = 11;
            this.typeWorkSheetCB.Name = "typeWorkSheetCB";
            this.typeWorkSheetCB.Size = new System.Drawing.Size(330, 21);
            this.typeWorkSheetCB.TabIndex = 9;
            this.typeWorkSheetCB.SelectedIndexChanged += new System.EventHandler(this.typeWorkSheetCB_SelectedIndexChanged);
            this.typeWorkSheetCB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // workSheetCalculateBT
            // 
            this.workSheetCalculateBT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.workSheetCalculateBT.Location = new System.Drawing.Point(12, 210);
            this.workSheetCalculateBT.Name = "workSheetCalculateBT";
            this.workSheetCalculateBT.Size = new System.Drawing.Size(82, 22);
            this.workSheetCalculateBT.TabIndex = 14;
            this.workSheetCalculateBT.Text = "Calculate";
            this.workSheetCalculateBT.UseVisualStyleBackColor = true;
            this.workSheetCalculateBT.Click += new System.EventHandler(this.workSheetCalculateBT_Click);
            this.workSheetCalculateBT.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // workSheetResultTB
            // 
            this.workSheetResultTB.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.workSheetResultTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.workSheetResultTB.Location = new System.Drawing.Point(177, 212);
            this.workSheetResultTB.MaxLength = 20;
            this.workSheetResultTB.Name = "workSheetResultTB";
            this.workSheetResultTB.ReadOnly = true;
            this.workSheetResultTB.Size = new System.Drawing.Size(165, 22);
            this.workSheetResultTB.TabIndex = 15;
            this.workSheetResultTB.GotFocus += new System.EventHandler(this.resultTextBox_GotFocus);
            this.workSheetResultTB.LostFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // calc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(228)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(214, 302);
            this.Controls.Add(this.datecalcPN);
            this.Controls.Add(this.num9BT_PN);
            this.Controls.Add(this.num7BT_PN);
            this.Controls.Add(this.num8BT_PN);
            this.Controls.Add(this.num6BT_PN);
            this.Controls.Add(this.num5BT_PN);
            this.Controls.Add(this.num3BT_PN);
            this.Controls.Add(this.num4BT_PN);
            this.Controls.Add(this.num2BT_PN);
            this.Controls.Add(this.percentbt_PN);
            this.Controls.Add(this.btdot_PN);
            this.Controls.Add(this.invertbt_PN);
            this.Controls.Add(this.sqrtbt_PN);
            this.Controls.Add(this.btnF_PN);
            this.Controls.Add(this.btnC_PN);
            this.Controls.Add(this.btnE_PN);
            this.Controls.Add(this.btnB_PN);
            this.Controls.Add(this.btnD_PN);
            this.Controls.Add(this.btnA_PN);
            this.Controls.Add(this.PNbinary);
            this.Controls.Add(this.screenPN);
            this.Controls.Add(this.anglePN);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.bracketTime_lb);
            this.Controls.Add(this.inv_ChkBox);
            this.Controls.Add(this.XorBT);
            this.Controls.Add(this.notBT);
            this.Controls.Add(this.AndBT);
            this.Controls.Add(this.RshBT);
            this.Controls.Add(this.modproBT);
            this.Controls.Add(this.RoRBT);
            this.Controls.Add(this.LshBT);
            this.Controls.Add(this.or_BT);
            this.Controls.Add(this.RoLBT);
            this.Controls.Add(this.close_bracket);
            this.Controls.Add(this.openProBT);
            this.Controls.Add(this.open_bracket);
            this.Controls.Add(this._10x_bt);
            this.Controls.Add(this.nvx_bt);
            this.Controls.Add(this.xn_bt);
            this.Controls.Add(this.log_bt);
            this.Controls.Add(this._3vx_bt);
            this.Controls.Add(this.x3_bt);
            this.Controls.Add(this.x2_bt);
            this.Controls.Add(this.pi_bt);
            this.Controls.Add(this.exp_bt);
            this.Controls.Add(this.modsciBT);
            this.Controls.Add(this.ln_bt);
            this.Controls.Add(this.dms_bt);
            this.Controls.Add(this.tanh_bt);
            this.Controls.Add(this.cosh_bt);
            this.Controls.Add(this.int_bt);
            this.Controls.Add(this.tan_bt);
            this.Controls.Add(this.sinh_bt);
            this.Controls.Add(this.btFactorial);
            this.Controls.Add(this.equalBT);
            this.Controls.Add(this.divbt);
            this.Controls.Add(this.mulbt);
            this.Controls.Add(this.minusbt);
            this.Controls.Add(this.addbt);
            this.Controls.Add(this.num0BT);
            this.Controls.Add(this.clearbt);
            this.Controls.Add(this.mem_minus_bt);
            this.Controls.Add(this.mem_recall);
            this.Controls.Add(this.changesignBT);
            this.Controls.Add(this.mem_add_bt);
            this.Controls.Add(this.mem_store);
            this.Controls.Add(this.ce);
            this.Controls.Add(this.memclearBT);
            this.Controls.Add(this.backspacebt);
            this.Controls.Add(this.num1BT);
            this.Controls.Add(this.cos_bt);
            this.Controls.Add(this.sin_bt);
            this.Controls.Add(this.sigmax2BT);
            this.Controls.Add(this.sigman_1BT);
            this.Controls.Add(this.AddstaBT);
            this.Controls.Add(this.xcross);
            this.Controls.Add(this.sigmaxBT);
            this.Controls.Add(this.sigmanBT);
            this.Controls.Add(this.CAD);
            this.Controls.Add(this.x2cross);
            this.Controls.Add(this.basePN);
            this.Controls.Add(this.unknownPN);
            this.Controls.Add(this.fe_ChkBox);
            this.Controls.Add(this.unitconvPN);
            this.Controls.Add(this.workSheetPN);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(220, 310);
            this.Name = "calc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.calc_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.datecalcPN.ResumeLayout(false);
            this.datecalcPN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).EndInit();
            this.num9BT_PN.ResumeLayout(false);
            this.num7BT_PN.ResumeLayout(false);
            this.num8BT_PN.ResumeLayout(false);
            this.num6BT_PN.ResumeLayout(false);
            this.num5BT_PN.ResumeLayout(false);
            this.num3BT_PN.ResumeLayout(false);
            this.num4BT_PN.ResumeLayout(false);
            this.num2BT_PN.ResumeLayout(false);
            this.percentbt_PN.ResumeLayout(false);
            this.btdot_PN.ResumeLayout(false);
            this.invertbt_PN.ResumeLayout(false);
            this.sqrtbt_PN.ResumeLayout(false);
            this.btnF_PN.ResumeLayout(false);
            this.btnC_PN.ResumeLayout(false);
            this.btnE_PN.ResumeLayout(false);
            this.btnB_PN.ResumeLayout(false);
            this.btnD_PN.ResumeLayout(false);
            this.btnA_PN.ResumeLayout(false);
            this.screenPN.ResumeLayout(false);
            this.screenPN.PerformLayout();
            this.anglePN.ResumeLayout(false);
            this.anglePN.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            this.gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).EndInit();
            this.basePN.ResumeLayout(false);
            this.basePN.PerformLayout();
            this.unknownPN.ResumeLayout(false);
            this.unknownPN.PerformLayout();
            this.unitconvPN.ResumeLayout(false);
            this.unitconvPN.PerformLayout();
            this.workSheetPN.ResumeLayout(false);
            this.workSheetPN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region form generation code by user
        /// <summary>
        /// Sắp xếp lại các nút của form standard trở lại vị trí ban đầu
        /// </summary>
        private void initializedForm(bool isReturn0)
        {
            this.memclearBT.Location = new Point(12, 66);
            this.mem_recall.Location = new Point(51, 66);
            this.mem_store.Location = new Point(90, 66);
            this.mem_add_bt.Location = new Point(129, 66);
            this.mem_minus_bt.Location = new Point(168, 66);

            this.backspacebt.Location = new Point(12, 98);
            this.ce.Location = new Point(51, 98);
            this.clearbt.Location = new Point(90, 98);
            this.changesignBT.Location = new Point(129, 98);
            this.sqrtbt_PN.Location = new Point(168, 98);

            this.num7BT_PN.Location = new Point(12, 130);
            this.num8BT_PN.Location = new Point(51, 130);
            this.num9BT_PN.Location = new Point(90, 130);
            this.divbt.Location = new Point(129, 130);
            this.percentbt_PN.Location = new Point(168, 130);

            this.num4BT_PN.Location = new Point(12, 162);
            this.num5BT_PN.Location = new Point(51, 162);
            this.num6BT_PN.Location = new Point(90, 162);
            this.mulbt.Location = new Point(129, 162);
            this.invertbt_PN.Location = new Point(168, 162);

            this.num1BT.Location = new Point(12, 194);
            this.num2BT_PN.Location = new Point(51, 194);
            this.num3BT_PN.Location = new Point(90, 194);
            this.minusbt.Location = new Point(129, 194);
            this.equalBT.Location = new Point(168, 194);

            this.num0BT.Location = new Point(12, 226);
            this.btdot_PN.Location = new Point(90, 226);
            this.addbt.Location = new Point(129, 226);

            this.mem_lb.Visible = (mem_num != 0);

            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(190, 47);
            //
            // scr_lb
            //
            if (isReturn0) this.scr_lb.Text = "0";
            //
            // form
            //
            //this.Size = new Size(222, 297);
        }
        /// <summary>
        /// Hàm do người dùng viết để sắp xếp và thêm các nút lên form scientific
        /// </summary>
        private void scientificLoad(bool isReturn0)
        {
            this.anglePN.Location = new Point(12, 66);

            this.bracketTime_lb.Location = new Point(12, 98);
            this.int_bt.Location = new Point(12, 130);
            this.dms_bt.Location = new Point(12, 162);
            this.pi_bt.Location = new Point(12, 194);
            this.fe_ChkBox.Location = new Point(12, 226);

            this.inv_ChkBox.Location = new Point(51, 98);
            this.sinh_bt.Location = new Point(51, 130);
            this.cosh_bt.Location = new Point(51, 162);
            this.tanh_bt.Location = new Point(51, 194);
            this.exp_bt.Location = new Point(51, 226);

            this.ln_bt.Location = new Point(90, 98);
            this.sin_bt.Location = new Point(90, 130);
            this.cos_bt.Location = new Point(90, 162);
            this.tan_bt.Location = new Point(90, 194);
            this.modsciBT.Location = new Point(90, 226);

            this.open_bracket.Location = new Point(129, 98);
            this.x2_bt.Location = new Point(129, 130);
            this.xn_bt.Location = new Point(129, 162);
            this.x3_bt.Location = new Point(129, 194);
            this.log_bt.Location = new Point(129, 226);

            this.close_bracket.Location = new Point(168, 98);
            this.btFactorial.Location = new Point(168, 130);
            this.nvx_bt.Location = new Point(168, 162);
            this._3vx_bt.Location = new Point(168, 194);
            this._10x_bt.Location = new Point(168, 226);

            this.memclearBT.Location = new Point(207, 66);
            this.backspacebt.Location = new Point(207, 98);
            this.num7BT_PN.Location = new Point(207, 130);
            this.num4BT_PN.Location = new Point(207, 162);
            this.num1BT.Location = new Point(207, 194);
            this.num0BT.Location = new Point(207, 226);

            this.mem_recall.Location = new Point(246, 66);
            this.ce.Location = new Point(246, 98);
            this.num8BT_PN.Location = new Point(246, 130);
            this.num5BT_PN.Location = new Point(246, 162);
            this.num2BT_PN.Location = new Point(246, 194);

            this.mem_store.Location = new Point(285, 66);
            this.clearbt.Location = new Point(285, 98);
            this.num9BT_PN.Location = new Point(285, 130);
            this.num6BT_PN.Location = new Point(285, 162);
            this.num3BT_PN.Location = new Point(285, 194);
            this.btdot_PN.Location = new Point(285, 226);

            this.mem_add_bt.Location = new Point(324, 66);
            this.changesignBT.Location = new Point(324, 98);
            this.divbt.Location = new Point(324, 130);
            this.mulbt.Location = new Point(324, 162);
            this.minusbt.Location = new Point(324, 194);
            this.addbt.Location = new Point(324, 226);

            this.mem_minus_bt.Location = new Point(363, 66);
            this.sqrtbt_PN.Location = new Point(363, 98);
            this.percentbt_PN.Location = new Point(363, 130);
            this.invertbt_PN.Location = new Point(363, 162);
            this.equalBT.Location = new Point(363, 194);

            if (isReturn0) this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);
            //
            // screenPN
            //
            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47);
            //
            // Calculator
            //
            //this.Size = new Size(447, 340);
        }
        /// <summary>
        /// set lại location cho các control của form standard có history
        /// </summary>
        private void stdWithHistory()
        {
            this.memclearBT.Location = new Point(12, 170);
            this.mem_recall.Location = new Point(51, 170);
            this.mem_store.Location = new Point(90, 170);
            this.mem_add_bt.Location = new Point(129, 170);
            this.mem_minus_bt.Location = new Point(168, 170);

            this.backspacebt.Location = new Point(12, 202);
            this.ce.Location = new Point(51, 202);
            this.clearbt.Location = new Point(90, 202);
            this.changesignBT.Location = new Point(129, 202);
            this.sqrtbt_PN.Location = new Point(168, 202);

            this.num7BT_PN.Location = new Point(12, 234);
            this.num8BT_PN.Location = new Point(51, 234);
            this.num9BT_PN.Location = new Point(90, 234);
            this.divbt.Location = new Point(129, 234);
            this.percentbt_PN.Location = new Point(168, 234);

            this.num4BT_PN.Location = new Point(12, 266);
            this.num5BT_PN.Location = new Point(51, 266);
            this.num6BT_PN.Location = new Point(90, 266);
            this.mulbt.Location = new Point(129, 266);
            this.invertbt_PN.Location = new Point(168, 266);

            this.num1BT.Location = new Point(12, 298);
            this.num2BT_PN.Location = new Point(51, 298);
            this.num3BT_PN.Location = new Point(90, 298);
            this.minusbt.Location = new Point(129, 298);
            this.equalBT.Location = new Point(168, 298);

            this.num0BT.Location = new Point(12, 330);
            this.btdot_PN.Location = new Point(90, 330);
            this.addbt.Location = new Point(129, 330);

            this.gridPanel.Location = new Point(12, 12);
            this.gridPanel.Size = new Size(190, 105);
            this.gridPanel.Visible = true;

            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);
            //
            // Column1
            //
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 190;
            //this.Size = new Size(237, 470);
        }
        /// <summary>
        /// set lại location cho các control của form scientific có history
        /// </summary>
        private void sciWithHistory()
        {
            this.anglePN.Location = new Point(12, 170);
            this.bracketTime_lb.Location = new Point(12, 202);
            this.int_bt.Location = new Point(12, 234);
            this.dms_bt.Location = new Point(12, 265);
            this.pi_bt.Location = new Point(12, 298);
            this.fe_ChkBox.Location = new Point(12, 330);

            this.inv_ChkBox.Location = new Point(51, 202);
            this.sinh_bt.Location = new Point(51, 234);
            this.cosh_bt.Location = new Point(51, 265);
            this.tanh_bt.Location = new Point(51, 298);
            this.exp_bt.Location = new Point(51, 330);

            this.ln_bt.Location = new Point(90, 202);
            this.sin_bt.Location = new Point(90, 234);
            this.cos_bt.Location = new Point(90, 265);
            this.tan_bt.Location = new Point(90, 298);
            this.modsciBT.Location = new Point(90, 330);

            this.open_bracket.Location = new Point(129, 202);
            this.x2_bt.Location = new Point(129, 234);
            this.xn_bt.Location = new Point(129, 265);
            this.x3_bt.Location = new Point(129, 298);
            this.log_bt.Location = new Point(129, 330);

            this.close_bracket.Location = new Point(168, 202);
            this.btFactorial.Location = new Point(168, 234);
            this.nvx_bt.Location = new Point(168, 265);
            this._3vx_bt.Location = new Point(168, 298);
            this._10x_bt.Location = new Point(168, 330);

            this.memclearBT.Location = new Point(207, 170);
            this.backspacebt.Location = new Point(207, 202);
            this.num7BT_PN.Location = new Point(207, 234);
            this.num4BT_PN.Location = new Point(207, 265);
            this.num1BT.Location = new Point(207, 298);
            this.num0BT.Location = new Point(207, 330);

            this.mem_recall.Location = new Point(246, 170);
            this.ce.Location = new Point(246, 202);
            this.num8BT_PN.Location = new Point(246, 234);
            this.num5BT_PN.Location = new Point(246, 265);
            this.num2BT_PN.Location = new Point(246, 298);

            this.mem_store.Location = new Point(285, 170);
            this.clearbt.Location = new Point(285, 202);
            this.num9BT_PN.Location = new Point(285, 234);
            this.num6BT_PN.Location = new Point(285, 265);
            this.num3BT_PN.Location = new Point(285, 298);
            this.btdot_PN.Location = new Point(285, 330);

            this.mem_add_bt.Location = new Point(324, 170);
            this.changesignBT.Location = new Point(324, 202);
            this.divbt.Location = new Point(324, 234);
            this.mulbt.Location = new Point(324, 265);
            this.minusbt.Location = new Point(324, 298);
            this.addbt.Location = new Point(324, 330);

            this.mem_minus_bt.Location = new Point(363, 170);
            this.sqrtbt_PN.Location = new Point(363, 202);
            this.percentbt_PN.Location = new Point(363, 234);
            this.invertbt_PN.Location = new Point(363, 265);
            this.equalBT.Location = new Point(363, 298);
            //
            // mem_lb
            //
            this.mem_lb.Size = new Size(18, 16);
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = (mem_num != 0);
            //
            // screenPN
            //
            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(385, 47);

            this.gridPanel.Location = new Point(12, 12);
            this.gridPanel.Size = new Size(385, gridPanel.Size.Height);
            this.gridPanel.Visible = true;
            //
            // Column1
            //
            //this.Column1.Width = 383;
            //
            // Calculator
            //
            //this.Size = new Size(447, 470);
        }
        /// <summary>
        /// set lại location cho các control của form programmer mode
        /// </summary>
        private void programmerMode(bool isPreview)
        {
            this.PNbinary.Location = new Point(12, 64 + previewPanelHeight);
            this.basePN.Location = new Point(12, 129 + previewPanelHeight);
            this.unknownPN.Location = new Point(12, 225 + previewPanelHeight);

            this.bracketTime_lb.Location = new Point(90, 129 + previewPanelHeight);
            this.openProBT.Location = new Point(90, 161 + previewPanelHeight);
            this.RoLBT.Location = new Point(90, 193 + previewPanelHeight);
            this.or_BT.Location = new Point(90, 225 + previewPanelHeight);
            this.LshBT.Location = new Point(90, 257 + previewPanelHeight);
            this.notBT.Location = new Point(90, 289 + previewPanelHeight);

            this.modproBT.Location = new Point(129, 129 + previewPanelHeight);
            this.close_bracket.Location = new Point(129, 161 + previewPanelHeight);
            this.RoRBT.Location = new Point(129, 193 + previewPanelHeight);
            this.XorBT.Location = new Point(129, 225 + previewPanelHeight);
            this.RshBT.Location = new Point(129, 257 + previewPanelHeight);
            this.AndBT.Location = new Point(129, 289 + previewPanelHeight);

            this.btnA_PN.Location = new Point(168, 129 + previewPanelHeight);
            this.btnB_PN.Location = new Point(168, 161 + previewPanelHeight);
            this.btnC_PN.Location = new Point(168, 193 + previewPanelHeight);
            this.btnD_PN.Location = new Point(168, 225 + previewPanelHeight);
            this.btnE_PN.Location = new Point(168, 257 + previewPanelHeight);
            this.btnF_PN.Location = new Point(168, 289 + previewPanelHeight);

            this.memclearBT.Location = new Point(207, 129 + previewPanelHeight);
            this.backspacebt.Location = new Point(207, 161 + previewPanelHeight);
            this.num7BT_PN.Location = new Point(207, 193 + previewPanelHeight);
            this.num4BT_PN.Location = new Point(207, 225 + previewPanelHeight);
            this.num1BT.Location = new Point(207, 257 + previewPanelHeight);
            this.num0BT.Location = new Point(207, 289 + previewPanelHeight);

            this.mem_recall.Location = new Point(246, 129 + previewPanelHeight);
            this.ce.Location = new Point(246, 161 + previewPanelHeight);
            this.num8BT_PN.Location = new Point(246, 193 + previewPanelHeight);
            this.num5BT_PN.Location = new Point(246, 225 + previewPanelHeight);
            this.num2BT_PN.Location = new Point(246, 257 + previewPanelHeight);

            this.mem_store.Location = new Point(285, 129 + previewPanelHeight);
            this.clearbt.Location = new Point(285, 161 + previewPanelHeight);
            this.num9BT_PN.Location = new Point(285, 193 + previewPanelHeight);
            this.num6BT_PN.Location = new Point(285, 225 + previewPanelHeight);
            this.num3BT_PN.Location = new Point(285, 257 + previewPanelHeight);
            this.btdot_PN.Location = new Point(285, 289 + previewPanelHeight);

            this.mem_add_bt.Location = new Point(324, 129 + previewPanelHeight);
            this.changesignBT.Location = new Point(324, 161 + previewPanelHeight);
            this.divbt.Location = new Point(324, 193 + previewPanelHeight);
            this.mulbt.Location = new Point(324, 225 + previewPanelHeight);
            this.minusbt.Location = new Point(324, 257 + previewPanelHeight);
            this.addbt.Location = new Point(324, 289 + previewPanelHeight);

            this.mem_minus_bt.Location = new Point(363, 129 + previewPanelHeight);
            this.sqrtbt_PN.Location = new Point(363, 161 + previewPanelHeight);
            this.percentbt_PN.Location = new Point(363, 193 + previewPanelHeight);
            this.invertbt_PN.Location = new Point(363, 225 + previewPanelHeight);
            this.equalBT.Location = new Point(363, 257 + previewPanelHeight);

            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47 + previewPanelHeight);
            bin_prvLb.Width = 385;

            //this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);

            this.sqrt_bt.Enabled = false;
            this.invert_bt.Enabled = false;
            this.btdot.Enabled = false;

            baseRBCheckedChanged();
            //copyHistoryMI.Enabled = false;
            //
            // Programmer
            //
            //this.Size = new Size(447, 420);
        }
        /// <summary>
        /// set lại location cho các control của statistics mode
        /// </summary>
        private void statisticsMode()
        {
            this.memclearBT.Location = new Point(12, 170);
            this.mem_recall.Location = new Point(51, 170);
            this.mem_store.Location = new Point(90, 170);
            this.mem_add_bt.Location = new Point(129, 170);
            this.mem_minus_bt.Location = new Point(168, 170);

            this.backspacebt.Location = new Point(12, 202);
            this.CAD.Location = new Point(51, 202);
            this.clearbt.Location = new Point(90, 202);
            this.fe_ChkBox.Location = new Point(129, 202);
            this.exp_bt.Location = new Point(168, 202);

            this.num7BT_PN.Location = new Point(12, 234);
            this.num8BT_PN.Location = new Point(51, 234);
            this.num9BT_PN.Location = new Point(90, 234);
            this.xcross.Location = new Point(129, 234);
            this.x2cross.Location = new Point(168, 234);

            this.num4BT_PN.Location = new Point(12, 266);
            this.num5BT_PN.Location = new Point(51, 266);
            this.num6BT_PN.Location = new Point(90, 266);
            this.sigmaxBT.Location = new Point(129, 266);
            this.sigmax2BT.Location = new Point(168, 266);

            this.num1BT.Location = new Point(12, 298);
            this.num2BT_PN.Location = new Point(51, 298);
            this.num3BT_PN.Location = new Point(90, 298);
            this.sigmanBT.Location = new Point(129, 298);
            this.sigman_1BT.Location = new Point(168, 298);

            this.num0BT.Location = new Point(12, 330);
            this.btdot_PN.Location = new Point(90, 330);
            this.changesignBT.Location = new Point(129, 330);
            this.AddstaBT.Location = new Point(168, 330);

            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);

            this.gridPanel.Location = new Point(12, 12);
            this.staDGV.Size = new Size(190, 85);
            this.gridPanel.Size = new Size(190, 105);

            //this.staDGV.CurrentCell = null;
        }
        /// <summary>
        /// khởi tạo mảng các textbox trong hàm chức năng fuel economy, ...
        /// </summary>
        /// <param name="capacity">số textfield của form</param>
        /// <param name="lbIndex">index menu trong submenu worksheet</param>
        private TextField[] InitCustomControls(int capacity, int lbIndex)
        {
            var itf = new TextField[capacity];
            for (int i = 0; i < capacity; i++)
            {
                itf[i] = new TextField();
                itf[i].LabelText = worksheetsLabel[lbIndex][i];
                itf[i].Location = new Point(9, 58 + 24 * i);
                itf[i].Visible = i != capacity - 1;
                itf[i].TextBoxGotFocus += new EventHandler(DisableKeyboard);
                itf[i].MouseDownEvent += new MouseEventHandler(MoveFormWithoutMouseAtTitleBar);
            }

            workSheetPN.Controls.AddRange(itf);
            return itf;
        }
        /// <summary>
        /// khởi tạo form cancel
        /// </summary>
        private void InitCancelOperation()
        {
            //if (co == null)
            {
                co = new CancelOperation();
                co.DoCancel += new CancelOperation.CancelProcess(co_DoCancel);
            }
        }
        /// <summary>
        /// khởi tạo mảng các label trong panel dưới màn hình của form programmer
        /// </summary>
        private void InitBitNumberArray()
        {
            #region init bit number
            int k;
            bin_digit = new ILabel[16];
            for (int i = 1; i >= 0; i--)
                for (int j = 7; j >= 0; j--)
                {
                    k = 8 * i + j;  // k = 8j + i
                    bin_digit[k] = new ILabel();
                    PNbinary.Controls.Add(bin_digit[k]);
                    bin_digit[k].Text = "0000";
                    bin_digit[k].TabIndex = k;
                    bin_digit[k].Font = new Font("Consolas", 9F);
                    bin_digit[k].Size = new Size(35, 15);
                    bin_digit[k].Location = new Point(345 - 49 * j, 27 - 25 * i);
                    bin_digit[k].MouseDown += new MouseEventHandler(bin_digit_MouseDown);
                }
            #endregion

            #region init index binary number
            flagpoint = new ILabel[6];
            for (int i = 1; i >= 0; i--)
            {
                for (int j = 2; j >= 0; j--)
                {
                    k = 3 * i + j;  // k = 3i + j
                    int idex = 16 * (k - i) - (j == 0 ? 0 : 1);  // j != 0 ? 1 : 0
                    flagpoint[k] = new ILabel();
                    PNbinary.Controls.Add(flagpoint[k]);
                    flagpoint[k].Text = idex.ToString();
                    flagpoint[k].TabIndex = k;
                    if (j == 0) flagpoint[k].Text = idex.ToString().PadLeft(4);
                    flagpoint[k].ForeColor = SystemColors.GrayText;
                    flagpoint[k].Font = new Font("Consolas", 9F);
                    flagpoint[k].Location = new Point(394 - 196 * j - (j == 0 ? 49 : 0), 40 - 25 * i);
                    flagpoint[k].MouseDown += new MouseEventHandler(MoveFormWithoutMouseAtTitleBar);
                }
            }

            #endregion
        }

        #endregion

        #region mousedown event
        Point location = new Point();

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            infoControl = sender as Control;
            if (infoControl is RadioButton)
            {
                location.X = this.Location.X + infoControl.Parent.Location.X + infoControl.Location.X;
                location.Y = this.Location.Y + infoControl.Parent.Location.Y + infoControl.Location.Y + 50;
            }
            else //if (infoControl is Button || infoControl is Label || infoControl is Panel)
            {
                if (infoControl.Parent is calc)
                {
                    location.X = this.Location.X + infoControl.Location.X;
                    location.Y = this.Location.Y + infoControl.Location.Y + 50;
                }
                if (infoControl.Parent is Panel)
                {
                    location.X = this.Location.X + infoControl.Parent.Location.X + infoControl.Location.X;
                    location.Y = this.Location.Y + infoControl.Parent.Location.Y + infoControl.Location.Y + 50;
                }
            }
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            // standard hoac scientific form, co su dung history
            if (bt == equalBT && (standardMI.Checked || scientificMI.Checked) && historyMI.Checked)
            {
                hisDGV.Focus();
            }
            // statistics form
            else if (bt == AddstaBT && statisticsMI.Checked)
            {
                staDGV.Focus();
            }
            else
            {
                screenPN.Focus();
            }
        }
        /// <summary>
        /// Tắt tính năng bắt sự kiện phím của chương trình (nhập số textbox)
        /// </summary>
        private void DisableKeyboard(object sender, EventArgs e)
        {
            screenPN.BackColor = BackColor;
            for (int i = 0; i < hisDGV.RowCount; i++)
            {
                hisDGV.Rows[i].DefaultCellStyle.BackColor = this.BackColor;
            }
            for (int i = 0; i < staDGV.RowCount; i++)
            {
                staDGV.Rows[i].DefaultCellStyle.BackColor = this.BackColor;
            }
            prcmdkey = false;
            //fromTB_LostFocus(null, null);
        }
        /// <summary>
        /// Tắt tính năng bắt sự kiện phím của chương trình (nhập số textbox)
        /// </summary>
        private void DisableKeyboard(object sender, MouseEventArgs e)
        {
            DisableKeyboard(sender, e as EventArgs);
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi
        /// thuộc tính focus của 2 textbox trong form unit conversion, hàm bình thường (nhập số trên form)
        /// </summary>
        private void EnableKeyboardAndChangeFocus()
        {
            prcmdkey = !hisDGV.IsCurrentCellInEditMode || !staDGV.IsCurrentCellInEditMode;
            //hisDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 217, 255);
            //fromTB_LostFocus(fromTB, null);
            if (fromTB.Focused || toTB.Focused || typeUnitCB.Focused) toLB.Focus();
            if (datemethodCB.Focused || tbResult2.Focused || tbResult1.Focused || dtP1.Focused || dtP2.Focused
                || periodsDateUD.Focused || periodsMonthUD.Focused || periodsYearUD.Focused)
            {
                firstDate.Focus();
            }
            screenPN.BackColor = Color.White;
            if (hisDGV.IsCurrentCellInEditMode || staDGV.IsCurrentCellInEditMode)
            {
                screenPN.BackColor = staDGV.BackgroundColor = hisDGV.BackgroundColor = BackColor;
                hisDGV.EndEdit(); staDGV.EndEdit();
            }
            else
                screenPN.BackColor = staDGV.BackgroundColor = hisDGV.BackgroundColor = Color.White;

            if (mortgageTF != null)
                if (mortgageTF[0].TBFocused || mortgageTF[1].TBFocused || mortgageTF[2].TBFocused || mortgageTF[3].TBFocused || mortgageTF[4].TBFocused)
                {
                    screenPN.Focus();
                }
            if (VhTF != null)
                if (VhTF[0].TBFocused || VhTF[1].TBFocused || VhTF[2].TBFocused || VhTF[3].TBFocused || VhTF[4].TBFocused || VhTF[5].TBFocused)
                {
                    screenPN.Focus();
                }
            if (fe_MPGTF != null)
                if (fe_MPGTF[0].TBFocused || fe_MPGTF[1].TBFocused || fe_MPGTF[2].TBFocused)
                {
                    screenPN.Focus();
                }
            if (feL100TF != null)
                if (feL100TF[0].TBFocused || feL100TF[1].TBFocused || feL100TF[2].TBFocused)
                {
                    screenPN.Focus();
                }

            if (workSheetResultTB.Focused) screenPN.Focus();

            for (int i = 0; i < hisDGV.RowCount; i++)
            {
                hisDGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            for (int i = 0; i < staDGV.RowCount; i++)
            {
                staDGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi
        /// thuộc tính focus của 2 textbox trong form unit conversion, sự kiện chuột
        /// </summary>
        private void EnableKeyboardAndChangeFocus(object sender, MouseEventArgs e)
        {
            EnableKeyboardAndChangeFocus();
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi
        /// thuộc tính focus của 2 textbox trong form unit conversion, sự kiện bình thường
        /// </summary>
        private void EnableKeyboardAndChangeFocus(object sender, EventArgs e)
        {
            EnableKeyboardAndChangeFocus();
        }

        private void FocusAndMoveForm(object sender, MouseEventArgs e)
        {
            EnableKeyboardAndChangeFocus();
            MoveFormWithoutMouseAtTitleBar(sender, e);
        }

        #endregion

        #region regist component

        private Button num1BT;
        private Button num2BT;
        private Button num3BT;
        private Button num4BT;
        private Button num5BT;
        private Button num6BT;
        private Button num7BT;
        private Button num8BT;
        private Button num9BT;
        private Button num0BT;
        private IPanel num2BT_PN;
        private IPanel num3BT_PN;
        private IPanel num6BT_PN;
        private IPanel num7BT_PN;
        private IPanel num4BT_PN;
        private IPanel num5BT_PN;
        private IPanel num8BT_PN;
        private IPanel num9BT_PN;
        private Button btdot;
        private Button addbt;
        private Button minusbt;
        private Button mulbt;
        private Button divbt;
        private Button equalBT;
        private Label mem_lb;
        private Label scr_lb;
        private ILabel expressionTB;
        private Button invert_bt;
        private Button percent_bt;
        private IPanel sqrtbt_PN;
        private IPanel invertbt_PN;
        private IPanel percentbt_PN;
        private IPanel btdot_PN;
        private Button backspacebt;
        private Button ce;
        private Button changesignBT;
        private Button memclearBT;
        private Button mem_store;
        private Button mem_recall;
        private Button sqrt_bt;
        private Button clearbt;
        private Button mem_add_bt;
        private Button mem_minus_bt;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private IPanel screenPN;
        private ILabel hex_prvLb;
        private ILabel bin_prvLb;
        private ILabel oct_prvLb;
        private ILabel dec_prvLb;
        private ContextMenu helpCTMN;
        private MenuItem hotkeyMI;
        //------------------------------------------------------------------
        private Label bracketTime_lb;
        private Button close_bracket;
        private Button open_bracket;
        private Button _10x_bt;
        private Button nvx_bt;
        private Button xn_bt;
        private Button log_bt;
        private Button _3vx_bt;
        private Button x3_bt;
        private Button x2_bt;
        private Button pi_bt;
        private Button exp_bt;
        private Button modsciBT;
        private Button ln_bt;
        private Button dms_bt;
        private Button cosh_bt;
        private Button tanh_bt;
        private Button int_bt;
        private Button sin_bt;
        private Button sinh_bt;
        private Button cos_bt;
        private Button tan_bt;
        private Button btFactorial;
        private IRadioButton degRB;
        private IRadioButton radRB;
        private IRadioButton graRB;
        private CheckBox inv_ChkBox;
        private CheckBox fe_ChkBox;
        private IPanel anglePN;
        private IPanel gridPanel;
        private IDataGridView hisDGV;
        private DataGridViewTextBoxColumn Column1;
        //------------------------------------------------------------------
        private Button dateCalculationBT;
        private CheckBox autocal_date;
        private ComboBox datemethodCB;
        private IDateTimePicker dtP1;
        private IDateTimePicker dtP2;
        private INumericUpDown periodsYearUD;
        private INumericUpDown periodsMonthUD;
        private INumericUpDown periodsDateUD;
        private IPanel datecalcPN;
        private Label lbResult2;
        private Label calmethodLB;
        private Label lbResult1;
        private Label dayAddSubLB;
        private Label firstDate;
        private Label monthAddSubLB;
        private Label secondDate;
        private Label yearAddSubLB;
        private RadioButton subrb;
        private RadioButton addrb;
        private TextBox tbResult1;
        private TextBox tbResult2;
        //------------------------------------------------------------------
        private Label typeUnitLB;
        private Label fromLB;
        private Label toLB;
        private ITextBox fromTB;
        private TextBox toTB;
        private ComboBox typeUnitCB;
        private IComboBox fromCB;
        private IComboBox toCB;
        private Button invert_unit;
        private Button dnBT;
        private Button upBT;
        private IPanel unitconvPN;
        //------------------------------------------------------------------
        private MainMenu mainMenu;
        private MenuItem viewMI;
        private MenuItem standardMI;
        private MenuItem scientificMI;
        private MenuItem programmerMI;
        private MenuItem statisticsMI;
        private MenuItem sepMI1;
        private MenuItem historyMI;
        private MenuItem digitGroupingMI;
        private MenuItem sepMI2;
        private MenuItem basicMI;
        private MenuItem unitConversionMI;
        private MenuItem dateCalculationMI;
        private MenuItem editMI;
        private MenuItem copyMI;
        private MenuItem pasteMI;
        private MenuItem sepMI3;
        private MenuItem historyOptionMI;
        private MenuItem copyHistoryMI;
        private MenuItem editHistoryMI;
        private MenuItem cancelEditHisMI;
        private MenuItem reCalculateMI;
        private MenuItem clearHistoryMI;
        private MenuItem datasetMI;
        private MenuItem copyDatasetMI;
        private MenuItem editDatasetMI;
        private MenuItem cancelEditDSMI;
        private MenuItem commitDSMI;
        private MenuItem clearDatasetMI;
        private MenuItem sepMI6;
        private MenuItem preferencesMI;
        private MenuItem helpMI;
        private MenuItem helpTopicsTSMI;
        private MenuItem sepMI5;
        private MenuItem aboutMI;
        private MenuItem worksheetsMI;
        private MenuItem mortgageMI;
        private MenuItem vehicleLeaseMI;
        private MenuItem fe_MPG_MI;
        private MenuItem feL100_MI;
        //------------------------------------------------------------------
        private ContextMenu mainContextMenu;
        private MenuItem copyCTMN;
        private MenuItem pasteCTMN;
        private MenuItem sepCTMN;
        private MenuItem showHistoryCTMN;
        private MenuItem hideHistoryCTMN;
        private MenuItem clearDatasetCTMN;
        private MenuItem clearHistoryCTMN;
        private MenuItem showPreviewPaneCTMN;
        private MenuItem hidePreviewPaneCTMN;
        //------------------------------------------------------------------
        private IRadioButton qwordRB;
        private IRadioButton dwordRB;
        private IRadioButton _wordRB;
        private IRadioButton _byteRB;
        private IRadioButton hexRB;
        private IRadioButton decRB;
        private IRadioButton octRB;
        private IRadioButton binRB;
        private IPanel PNbinary;
        private IPanel basePN;
        private IPanel unknownPN;
        private IPanel btnA_PN;
        private IPanel btnB_PN;
        private IPanel btnC_PN;
        private IPanel btnD_PN;
        private IPanel btnE_PN;
        private IPanel btnF_PN;
        private Button btnA;
        private Button btnB;
        private Button btnC;
        private Button btnD;
        private Button btnE;
        private Button btnF;
        private Button XorBT;
        private Button notBT;
        private Button AndBT;
        private Button RshBT;
        private Button RoRBT;
        private Button LshBT;
        private Button or_BT;
        private Button RoLBT;
        private Button modproBT;
        private Button openProBT;
        private ILabel[] bin_digit;
        private ILabel[] flagpoint;
        //------------------------------------------------------------------
        private IDataGridView staDGV;
        private DataGridViewTextBoxColumn Column2;
        private Button sigmax2BT;
        private Button sigman_1BT;
        private Button AddstaBT;
        private Button xcross;
        private Button sigmaxBT;
        private Button sigmanBT;
        private Button CAD;
        private Button x2cross;
        private Label countLB;
        //------------------------------------------------------------------
        private BackgroundWorker mWorker;
        //------------------------------------------------------------------
        private IPanel workSheetPN;
        private Label typeMorgageLB;
        private ComboBox typeWorkSheetCB;
        private Button workSheetCalculateBT;
        private TextBox workSheetResultTB;
        private TextField[] mortgageTF;
        private TextField[] VhTF;
        private TextField[] feL100TF;
        private TextField[] fe_MPGTF;
        #endregion
    }
}