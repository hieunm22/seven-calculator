using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    partial class calc : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        /// <summary>
        /// combobox item member
        /// </summary>
        private object[][] comboBoxItemMember = new object[][]{

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
            int key_hc = keyData.GetHashCode(); // key_hc = key hashcode
            // control W
            if (key_hc == 131159) this.Close();
            if ((key_hc == 131139 || key_hc == 131158) && !prcmdkey)
            {
                return false;
            }
            if (key_hc == 27)    // esc
            {
                pex = null;
                if (hisDGV.IsCurrentCellInEditMode) cancelEditHisMI_Click(null, null);
                else if (staDGV.IsCurrentCellInEditMode) cancelEditDSMI_Click(null, null);
                else clear_num(true);
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (prcmdkey && pex == null)
            {
                switch (key_hc)
                {
                    #region xu ly phim nhap vao
                    //cac phim so tu 0 den 9
                    case 96: case 48:
                        if (programmerMI.Checked) numinput_pro(num0BT);
                        else numinput(num0BT);
                        break;
                    case 97: case 49:
                        if (programmerMI.Checked) numinput_pro(num1BT);
                        else numinput(num1BT);
                        break;
                    case 98: case 50:
                        if (programmerMI.Checked) numinput_pro(num2BT);
                        else numinput(num2BT);
                        break;
                    case 99: case 51:
                        if (programmerMI.Checked) numinput_pro(num3BT);
                        else numinput(num3BT);
                        break;
                    case 100: case 52:
                        if (programmerMI.Checked) numinput_pro(num4BT);
                        else numinput(num4BT);
                        break;
                    case 101: case 53:
                        if (programmerMI.Checked) numinput_pro(num5BT);
                        else numinput(num5BT);
                        break;
                    case 102: case 54:
                        if (programmerMI.Checked) numinput_pro(num6BT);
                        else numinput(num6BT);
                        break;
                    case 103: case 55:
                        if (programmerMI.Checked) numinput_pro(num7BT);
                        else numinput(num7BT);
                        break;
                    case 104: case 56:
                        if (programmerMI.Checked) numinput_pro(num8BT);
                        else numinput(num8BT);
                        break;
                    case 105: case 57:
                        if (programmerMI.Checked) numinput_pro(num9BT);
                        else numinput(num9BT);
                        break;
                    case 187: case 13:      // = hoac enter
                        if (statisticsMI.Checked) AddstaBT_Click(null, null);
                        else equal_Click(null, null);
                        break;
                    case 110: case 190:   // .
                        numinput(btdot);
                        break;
                    case 107: case 65723:   // +
                        if (addbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(12);
                            if (scientificMI.Checked) sci_operation(12);
                            if (programmerMI.Checked) pro_operation(12);
                        }
                        break;
                    case 109: case 00189:   // -
                        if (minusbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(13);
                            if (scientificMI.Checked) sci_operation(13);
                            if (programmerMI.Checked) pro_operation(13);
                        }
                        break;
                    case 106: case 65592:   // *
                        if (mulbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(14);
                            if (scientificMI.Checked) sci_operation(14);
                            if (programmerMI.Checked) pro_operation(14);
                        }
                        break;
                    case 111: case 00191:   // /
                        if (mulbt.Enabled)
                        {
                            if (standardMI.Checked) std_operation(15);
                            if (scientificMI.Checked) sci_operation(15);
                            if (programmerMI.Checked) pro_operation(15);
                        }
                        break;
                    case 46:        // del
                        if (!statisticsMI.Checked)
                        {
                            ce_Click(null, null);
                        }
                        else if (staDGV.CurrentCell != null)
                        {
                            staDGV.Rows.RemoveAt(staDGV.CurrentCell.RowIndex);
                            countLB.Text = string.Format("Count = {0}", staDGV.RowCount);
                            try { rowIndex = staDGV.CurrentCell.RowIndex; }
                            catch { rowIndex = 0; }
                        }
                        break;
                    case 113:       // F2
                        if (hisDGV.Visible && hisDGV.RowCount > 0)
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
                    case 114:       // F3
                        if (scientificMI.Checked) degRB.Checked = true;
                        if (programmerMI.Checked) _wordRB.Checked = true;
                        break;
                    case 115:       // F4
                        if (scientificMI.Checked) radRB.Checked = true;
                        if (programmerMI.Checked) _byteRB.Checked = true;
                        break;
                    case 116:       // F5
                        if (scientificMI.Checked) graRB.Checked = true;
                        if (programmerMI.Checked) hexRB.Checked = true;
                        break;
                    case 117:       // F6
                        if (programmerMI.Checked) decRB.Checked = true;
                        break;
                    case 118:       // F7
                        if (programmerMI.Checked) octRB.Checked = true;
                        break;
                    case 119:       // F8
                        if (programmerMI.Checked) binRB.Checked = true;
                        break;
                    case 120:       // F9
                        if (programmerMI.Checked) numinput_pro(changesignBT);
                        else
                        {
                            if (!statisticsMI.Checked)
                            {
                                if (confirm_num) math_func(changesignBT);
                                else numinput(changesignBT);
                            }
                            else
                            {
                                if (str.StartsWith("-")) str = str.Substring(1);
                                else str = "-" + str;
                                DisplayToScreen();
                            }
                        }
                        break;
                    case 123:       // F12
                        if (programmerMI.Checked) qwordRB.Checked = true;
                        break;
                    case 186:       // ; int()
                        if (scientificMI.Checked) math_func(int_bt);
                        break;
                    // cac phim A den F
                    case 65:        // A
                        if (btnA.Enabled && programmerMI.Checked) buttonAF(btnA.Text);
                        if (statisticsMI.Checked) xcross_Click(null, null);
                        break;
                    case 66:        // B
                        if (btnB.Enabled && programmerMI.Checked) buttonAF(btnB.Text);
                        break;
                    case 67:        // C
                        if (btnC.Enabled && programmerMI.Checked) buttonAF(btnC.Text);
                        break;
                    case 68:        // D
                        if (btnD.Enabled && programmerMI.Checked) buttonAF(btnD.Text);
                        if (scientificMI.Checked) operatorBT_Click(modsciBT, null);
                        if (statisticsMI.Checked) CAD_Click(null, null);
                        break;
                    case 69:        // E
                        if (programmerMI.Checked && btnE.Enabled) buttonAF(btnE.Text);
                        if (scientificMI.Checked || statisticsMI.Checked) exp_bt_Click(null, null); // nut exp
                        break;
                    case 70:        // F
                        if (btnF.Enabled && programmerMI.Checked) buttonAF(btnF.Text);
                        break;
                    case 73:       // I Inv
                        if (scientificMI.Checked) inv_ChkBox.Checked = !inv_ChkBox.Checked;
                        break;
                    case 74:       // J RoL
                        if (programmerMI.Checked) rotateBT_Click(RoLBT, null);
                        break;
                    case 75:       // K RoR
                        if (programmerMI.Checked) rotateBT_Click(RoRBT, null);
                        break;
                    case 76:       // L log
                        if (scientificMI.Checked) math_func(log_bt); // 10^x
                        break;
                    case 77:       // M dms
                        if (scientificMI.Checked) math_func(dms_bt);
                        break;
                    case 78:        // N ln()
                        if (scientificMI.Checked) math_func(ln_bt);
                        break;
                    case 79:       // O cos
                        if (scientificMI.Checked) math_func(cos_bt);
                        break;
                    case 80:       // P pi
                        if (scientificMI.Checked) pi_bt_Click(null, null);
                        break;
                    case 81:       // Q x²
                        if (scientificMI.Checked) math_func(x2_bt);
                        break;
                    case 82:       // R 1/x
                        if (standardMI.Checked || scientificMI.Checked) math_func(invert_bt);
                        break;
                    case 83:       // S sin / ∑n
                        if (scientificMI.Checked) math_func(sin_bt);
                        if (statisticsMI.Checked) sigmaxBT_Click(null, null);
                        break;
                    case 84:       // T tan / σn
                        if (scientificMI.Checked) math_func(tan_bt);
                        if (statisticsMI.Checked) sigmanBT_Click(null, null);
                        break;
                    case 86:       // V
                        if (scientificMI.Checked) fe_ChkBox.Checked = !fe_ChkBox.Checked;
                        break;
                    case 88:       // X
                        if (scientificMI.Checked || statisticsMI.Checked) exp_bt_Click(null, null); // nut exp
                        break;
                    case 89:       // Y
                        if (scientificMI.Checked) sci_operation(33);
                        break;
                    case 008:       // Backspace
                        backspace_Click(null, null);
                        break;
                    case 65593: case 219:    // (
                        if (scientificMI.Checked) open_bracket_Click(null, null);
                        break;
                    case 65589:     // %
                        if (percent_bt.Enabled) percent_Click(null, null);
                        if (programmerMI.Checked) bitOperatorsBT_Click(modproBT, null);
                        break;
                    case 65728:     // ~ not
                        if (programmerMI.Checked) notBT_Click(null, null);
                        break;
                    case 65584: case 221:    // )
                        if (scientificMI.Checked) close_bracket_Click(null, null);
                        break;
                    case 65585:     // !
                        if (scientificMI.Checked) math_func(btFactorial);    //n!
                        break;
                    case 65587:     // #
                        if (scientificMI.Checked) math_func(x3_bt);    //n!
                        break;
                    case 65590: case 65591: case 65756: case 65724: case 65726: // ^ & | < >
                        if (programmerMI.Checked)
                        {
                            if (key_hc == 65590) bitOperatorsBT_Click(XorBT, null);
                            if (key_hc == 65591) bitOperatorsBT_Click(AndBT, null);
                            if (key_hc == 65756) bitOperatorsBT_Click(or_BT, null);
                            if (key_hc == 65724) bitOperatorsBT_Click(LshBT, null);
                            if (key_hc == 65726) bitOperatorsBT_Click(RshBT, null);
                        }
                        break;
                    case 65586:     // sqrt
                        if (scientificMI.Checked || standardMI.Checked) math_func(sqrt_bt);
                        break;
                    // cac to hop phim
                    case 131137:    // control A
                        if (statisticsMI.Checked) x2cross_Click(null, null);
                        break;
                    case 131138:    // control B
                        if (scientificMI.Enabled) math_func(_3vx_bt); // ³√x
                        break;
                    case 131139:    // control C
                        copyCTMN_Click(null, null); // Copy
                        break;
                    case 131140:    // control D
                        digitLoad(false);
                        break;
                    case 131143:    // control G
                        if (scientificMI.Enabled) math_func(_10x_bt); // 10^x
                        break;
                    case 131148:    // control L
                        memclear_Click(null, null);  //MC
                        break;
                    case 131149:    // control M
                        mem_process("MS"); //MS
                        break;
                    case 131151:    // control O
                        math_func(cosh_bt);  // cosh
                        break;
                    case 131152:    // control P
                        mem_process("M+"); //M+
                        break;
                    case 131153:    // control Q
                        mem_process("M-"); //M-
                        break;
                    case 131154:    // control R
                        if (pex == null) recallMemory();
                        break;
                    case 131155:    // control S
                        if (scientificMI.Checked) math_func(sinh_bt);  // sinh
                        if (statisticsMI.Checked) sigmax2BT_Click(null, null);
                        break;
                    case 131156:    // control T
                        if (scientificMI.Checked) math_func(tanh_bt);  // tanh
                        if (statisticsMI.Checked) sigman_1BT_Click(null, null);
                        break;
                    case 131158:    // control V
                        if (pasteMI.Enabled) { getPaste(null, null); pasteCTMN_Click(null, null); }
                        break;
                    case 131161:    // control Y
                        sci_operation(34);  // yx
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
            if (propertiesChange)
            {
                if (standardMI.Checked) configValue[0] = 0;
                if (scientificMI.Checked) configValue[0] = 1;
                if (programmerMI.Checked) configValue[0] = 2;
                if (statisticsMI.Checked) configValue[0] = 3;

                if (basicMI.Checked) configValue[2] = 0;
                if (unitConversionMI.Checked) configValue[2] = 1;
                if (dateCalculationMI.Checked) configValue[2] = 2;
                if (mortgageMI.Checked) configValue[2] = 3;
                if (vehicleLeaseMI.Checked) configValue[2] = 4;
                if (fe_MPG_MI.Checked) configValue[2] = 5;
                if (feL100_MI.Checked) configValue[2] = 6;
                string configNew = string.Format(@"[calc]
CalculatorType={0};
DigitGrouping={1};
ExtraFunction={2};
History={3};
MemoryNumber={4};
--------------------------------------------------
[UnitConversion]
TypeUnitCB={5};
--------------------------------------------------
[DateCalculation]
AutoCalculate={6};
Method={7};
--------------------------------------------------
[OtherOptions]
CollapseSpeed={8};
Animate={11};
FastFactorial={10};
SignInteger={9};",
                configValue[0], digitGroupingMI.Checked.GetHashCode(),
                configValue[2], historyMI.Checked.GetHashCode(), mem_num.StrValue,
                typeUnitCB.SelectedIndex * (typeUnitCB.SelectedIndex >= 0).GetHashCode(),
                autocal_date.Checked.GetHashCode(),
                datemethodCB.SelectedIndex * (datemethodCB.SelectedIndex >= 0).GetHashCode(),
                configValue[8],
                (configValue[9] == 1).GetHashCode(),
                (configValue[10] == 1).GetHashCode(),
                (configValue[11] == 1).GetHashCode());

                if (this.configContent != configNew)
                {
                    Misc.WriteAllText(configPath, configNew);
                }
                //else return;
            }
            base.OnClosing(e);
        }
        #endregion

        #region user's method
        /// <summary>
        /// thay đổi kích thước font cho vừa với số chữ số hiện trên màn hình
        /// </summary>
        private Font fontChanged(int length)
        {
            Font font = new Font("Consolas", 15.75F);
            if (standardMI.Checked || statisticsMI.Checked)
            {
                if (length < 13) font = new Font("Consolas", 15.75F);   // 13 van du
                if (length >= 13 && length < 21) font = new Font("Consolas", 10.25F);  // 13 van du
                if (length >= 21) font = new Font("Consolas", 9.75F);
            }
            if (scientificMI.Checked/* && pex == null*/)
            {
                if (length < 31) font = new Font("Consolas", 15.75F);
                if (length >= 31 && length < 37) font = new Font("Consolas", 13.75F);
                if (length >= 37) font = new Font("Consolas", 10.25F);
            }
            if (programmerMI.Checked)
            {
                if (length < 30) font = new Font("Consolas", 15.75F);
                //if (length >= 22 && length < 28) font = new Font("Consolas", 17.75F);
                if (length >= 30 && length < 36) font = new Font("Consolas", 13.75F);
                if (length >= 36 && length < 52) font = new Font("Consolas", 9.75F);
                if (length >= 52 && length < 73) font = new Font("Consolas", 6.75F);
                if (length >= 73) font = new Font("Consolas", 6F);
            }
            return font;
        }
        /// <summary>
        /// form standard
        /// </summary>
        private void stdLoad(bool isLoaded)
        {
            int his = historyMI.Checked.GetHashCode();
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked 
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(216 + (exf ? 376 : 0), 310 + 105 * his);     // dua ra ngoai de resize lai form
            FormSizeChanged(new Size(216 + (exf ? 376 : 0), 310 + 105 * his), isLoaded);
            if (!standardMI.Checked)
            {
                if (programmerMI.Checked) enableComponentByProgrammer();
                modeMethod(standardMI);
                EnableKeyboardAndChangeFocus();
                if (historyMI.Checked && historyMI.Enabled) stdWithHistory();
                else initializedForm(true);

                this.hisDGV.Size = new Size(190 + (scientificMI.Checked ? 195 : 0), hisDGV.Size.Height);
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(false);

                unitconvGB.Location = new Point(217, 12);
                unitconvGB.Size = new Size(360, 241 + 103 * his);

                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                feMPG_PN.Visible = (fe_MPG_MI.Checked || feL100_MI.Checked);

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
            int his = historyMI.Checked.GetHashCode();
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked
                || fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked;
            //this.Size = new Size(413 + (exf ? 376 : 0), 310 + 105 * his);
            FormSizeChanged(new Size(413 + (exf ? 376 : 0), 310 + 105 * his), isLoaded);
            if (!scientificMI.Checked)
            {
                if (programmerMI.Checked) enableComponentByProgrammer();
                modeMethod(scientificMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(true);
                hideProComponent(false);
                hideStaComponent(false);

                if (historyMI.Checked && historyMI.Enabled) sciWithHistory();
                else scientificLoad(true);

                this.hisDGV.Size = new Size(scientificMI.Checked ? 385 : 190, hisDGV.Size.Height);
                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                feMPG_PN.Visible = (fe_MPG_MI.Checked || feL100_MI.Checked);

                unitconvGB.Location = new Point(410, 12);
                unitconvGB.Size = new Size(360, 241 + 103 * his);
                if (exf) basicMI.Checked = false;

                gridPanel.Visible = hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = false;

                if (!isLoaded)
                {
                    clear_num(true);
                    propertiesChange = true;
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
            //this.Size = new Size(413 + 374 * exf.GetHashCode(), 374);
            FormSizeChanged(new Size(413 + 376 * exf.GetHashCode(), 374), isLoaded);
            if (!programmerMI.Checked)
            {
                if (bin_digit == null) InitBitNumberArray();
                modeMethod(programmerMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(true);
                hideStaComponent(false);
                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                feMPG_PN.Visible = (fe_MPG_MI.Checked || feL100_MI.Checked);
                programmerMode();

                historyMI.Enabled = false;
                unitconvGB.Location = new Point(410, 12);
                unitconvGB.Size = new Size(360, 304);

                gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
                hisDGV.Visible = false;
                staDGV.Visible = false;

                if (!isLoaded) propertiesChange = true;
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
            //this.Size = new Size(216 + (exf ? 376 : 0), 415);
            FormSizeChanged(new Size(216 + (exf ? 376 : 0), 415), isLoaded);
            if (!statisticsMI.Checked)
            {
                modeMethod(statisticsMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(false);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(true);
                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                feMPG_PN.Visible = (fe_MPG_MI.Checked || feL100_MI.Checked);

                gridPanel.Visible = true;
                hisDGV.Visible = false;
                staDGV.Visible = true;

                countLB.Text = "Count = 0";
                statisticsMode();
                //ctmnEnableAndVisible();

                enableComponentByProgrammer();
                unitconvGB.Location = new Point(217, 12);
                unitconvGB.Size = new Size(360, 344);

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

            historyOptionMI.Visible = historyMI.Enabled = !programmerMI.Checked && !statisticsMI.Checked;
            datasetMI.Visible = statisticsMI.Checked;
            sepCTMN.Visible = sepMI3.Visible = (mi != programmerMI);
            percent_bt.Enabled = mi == standardMI;

            while (hisDGV.RowCount > 0) hisDGV.Rows.RemoveAt(0);
        }
        /// <summary>
        /// nạp những setting từ lần sử dụng trước đó từ registry
        /// </summary>
        private void loadInfoFromFile()
        {
            configValue = readFromFile();
            getMemoryNumber();
            if (configValue[0] == 0) stdLoad(true);
            if (configValue[0] == 1) sciLoad(true);
            if (configValue[0] == 2) proLoad(true);
            if (configValue[0] == 3) staLoad(true);
            if (configValue[2] == 1)
            {
                exFunc(unitConversionMI, true);
                typeUnitCB.SelectedIndex = configValue[5];
            }
            if (configValue[2] == 2)
            {
                exFunc(dateCalculationMI, true);
                datemethodCB.SelectedIndex = configValue[7];
                autocal_date.Checked = (configValue[6] == 1);
            }
            if (configValue[2] == 3)
            {
                exFunc(mortgageMI, true);
            }
            if (configValue[2] == 4)
            {
                exFunc(vehicleLeaseMI, true);
            }
            if (configValue[2] == 5)
            {
                exFunc(fe_MPG_MI, true);
            }
            if (configValue[2] == 6)
            {
                exFunc(feL100_MI, true);
            }
            if (configValue[3] == 1)
            {
                if (historyMI.Enabled) formWithHistory(true);
                historyMI.Checked = true;
            }
            if (configValue[1] == 1) digitLoad(true);

            typeUnitCB.SelectedIndexChanged -= typeUnitCB_SelectedIndexChanged;
            typeUnitCB.SelectedIndex = configValue[5];
            typeUnitCB.SelectedIndexChanged += typeUnitCB_SelectedIndexChanged;

            datemethodCB.SelectedIndexChanged -= cal_method_SelectedIndexChanged;
            datemethodCB.SelectedIndex = configValue[7];
            calMethod_SIC(true, configValue[7]);
            datemethodCB.SelectedIndexChanged += cal_method_SelectedIndexChanged;

            btdot.Text = Misc.DecimalSeparator;
            resultCollection = new string[100];
        }
        /// <summary>
        /// thay đổi kích thước form giống win 7
        /// </summary>
        private void FormSizeChanged(Size newS, bool isLoaded)
        {
            if (isLoaded || configValue[9] == 0) { this.Size = newS; return; }
            int oldw = Size.Width, oldh = Size.Height;
            int neww = newS.Width, newh = newS.Height;
            //------chi thay doi chieu rong
            if (oldh == newh)
            {
                int inc_or_dec = (oldw < neww) ? 1 : -1;
                for (int i = 0; i < (int)(Math.Abs(neww - oldw) / configValue[8]); i++)
                    this.Size = new Size(Size.Width + configValue[8] * inc_or_dec, oldh);
                goto end;
            }
            //------chi thay doi chieu cao
            if (oldw == neww)
            {
                int inc_or_dec = (oldh < newh) ? 1 : -1;
                for (int i = 0; i < (int)(Math.Abs(newh - oldh) / configValue[8]); i++)
                    this.Size = new Size(oldw, Size.Height + configValue[8] * inc_or_dec);
                goto end;
            }
            //------thay doi ca chieu rong va chieu cao
            int verticalSpd = (oldw - neww) / (newh - oldh) * configValue[8];
            verticalSpd = Math.Abs(verticalSpd);
            if (oldw > neww)
            {
                int inc_or_dec = (oldh < newh) ? 1 : -1;
                for (int i = 0; i < (int)(Math.Abs(newh - oldh) / configValue[8]); i++)
                    this.Size = new Size(oldw -= verticalSpd, Size.Height + inc_or_dec * configValue[8]);
                goto end;
            }

            if (oldw < neww)
            {
                int inc_or_dec = (oldw < neww) ? 1 : -1;
                for (int i = 0; i < (int)(Math.Abs(neww - oldw) / configValue[8]); i++)
                    this.Size = new Size(oldw += verticalSpd, Size.Height + inc_or_dec * configValue[8]);
            }
            end: if (Size.Height != newh || Size.Width != neww) this.Size = new Size(neww, newh);
        }

        string configContent;
        /// <summary>
        /// đọc thuộc tính đã được ghi vào file, nếu không đúng thì sửa luôn
        /// </summary>
        private int[] readFromFile()
        {
            bool changed = false;
            int[] result = new int[12];
            string[] lines = new string[20];
            try
            {
                configContent = System.IO.File.ReadAllText(configPath);
                configContent = configContent.Replace(" ", "");

                //edit = configContent;
                //int index = 0;
                //while (edit.Contains("="))
                //{
                //    edit = edit.Substring(edit.IndexOf("=") + 1);
                //    if (index != 4) result[index] = int.Parse(edit.Substring(0, edit.IndexOf(";")));    // bỏ qua dòng thứ 5
                //    index++;
                //}

                lines = System.IO.File.ReadAllLines(configPath);
                // bắt buộc config phải theo thứ tự này
                result[00] = assignConfigValue(lines[01], "CalculatorType");
                result[01] = assignConfigValue(lines[02], "DigitGrouping");
                result[02] = assignConfigValue(lines[03], "ExtraFunction");
                result[03] = assignConfigValue(lines[04], "History");
                result[05] = assignConfigValue(lines[08], "TypeUnitCB");
                result[06] = assignConfigValue(lines[11], "AutoCalculate");
                result[07] = assignConfigValue(lines[12], "Method");
                result[08] = assignConfigValue(lines[15], "CollapseSpeed");
                result[09] = assignConfigValue(lines[16], "Animate");
                result[10] = assignConfigValue(lines[17], "FastFactorial");
                result[11] = assignConfigValue(lines[18], "SignInteger");

                changed |= checkIfValid("CalculatorType", ref result[0], 3);
                changed |= checkIfValid("DigitGrouping", ref result[1], 1);
                changed |= checkIfValid("ExtraFunction", ref result[2], 6);
                changed |= checkIfValid("History", ref result[3], 1);
                changed |= checkIfValid("TypeUnitCB", ref result[5], 10);
                changed |= checkIfValid("AutoCalculate", ref result[6], 1);
                changed |= checkIfValid("Method", ref result[7], 1);
                changed |= checkIfValid("CollapseSpeed", ref result[8], 12);
                changed |= checkIfValid("Animate", ref result[9], 1);
                changed |= checkIfValid("FastFactorial", ref result[10], 1);
                changed |= checkIfValid("SignInteger", ref result[11], 1);
                if (changed) Misc.WriteAllText(configPath, configContent);
            }
            catch (Exception)
            {
                configContent = @"[calc]
CalculatorType=0;
DigitGrouping=0;
ExtraFunction=0;
History=0;
MemoryNumber=0;
--------------------------------------------------
[UnitConversion]
TypeUnitCB=0;
--------------------------------------------------
[DateCalculation]
AutoCalculate=0;
Method=0;
--------------------------------------------------
[OtherOptions]
CollapseSpeed=10;
Animate=1;
FastFactorial=0;
SignInteger=1;";
                Misc.WriteAllText(configPath, configContent);
                //standardMI.Checked = true;
                historyMI.Checked = false;
                digitGroupingMI.Checked = false;
                //basicMI.Checked = true;
                result = new int[12];
                result[8] = 10;
                result[9] = 1;
                result[11] = 1;
                return result;
            }

            return result;
        }

        private int assignConfigValue(string line, string compare)
        {
            if (line.Contains(compare))
            {
                string value = line;
                value = value.Substring(0, value.IndexOf(";"));
                value = value.Substring(value.IndexOf("=") + 1);
                return int.Parse(value);
            }
            else
            {
                throw new Exception("Invalid compare value");
            }
        }
        /// <summary>
        /// kiểm tra giá trị được gán có nằm trong phạm vi cho phép không?
        /// </summary>
        private bool checkIfValid(string compare, ref int RTValue, int max)
        {
            int defaultvalue = 0;
            switch (compare)
            {
                case "CollapseSpeed": defaultvalue = 10;
                    break;
                case "SignInteger": case "Animate": defaultvalue = 1;
                    break;
            }
            if (RTValue > max || RTValue < (compare == "CollapseSpeed" ? 4 : 0))
            {
                configContent = configContent.Replace(compare + "=" + RTValue, compare + "=" + defaultvalue.ToString());
                RTValue = defaultvalue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// lấy giá trị của số M từ file
        /// </summary>
        private void getMemoryNumber()
        {
            string number = null;
            try
            {
                if (!configContent.Contains("MemoryNumber")) throw new ArgumentOutOfRangeException("No fucking reason");
                number = configContent.Substring(configContent.IndexOf("MemoryNumber") + "MemoryNumber".Length);
                number = number.Substring(0, number.IndexOf(";"));
                number = number.Substring(number.IndexOf("=") + 1);
                mem_num = number;
                mem_lb.Visible = (mem_num != 0);
                toolTip1.SetToolTip(mem_lb, string.Format("M = {0}", mem_num.StrValue));
            }
            catch (ArgumentOutOfRangeException)	// thieu dong luu memorynumber
            {
                configContent = string.Format(@"[calc]
CalculatorType={0};
DigitGrouping={1};
ExtraFunction={2};
History={3};
MemoryNumber={4};
--------------------------------------------------
[UnitConversion];
TypeUnitCB={5};
--------------------------------------------------
[DateCalculation]
AutoCalculate={6};
Method={7};
--------------------------------------------------
[OtherOptions]
CollapseSpeed={8};
Animate={11};
FastFactorial={10};
SignInteger={9};",
                configValue[0], configValue[1], configValue[2], configValue[3], mem_num.StrValue, configValue[5], configValue[6], configValue[7], configValue[8], configValue[9], configValue[10], configValue[11]);
                Misc.WriteAllText(configPath, configContent);
            }
            catch (Exception)
            {
                configContent = configContent.Replace("MemoryNumber=" + number, "MemoryNumber=0");
                Misc.WriteAllText(configPath, configContent);
            }
        }
        /// <summary>
        /// form with history - control - H
        /// </summary>
        private void formWithHistory(bool isLoaded)
        {
            historyMI.Checked = !historyMI.Checked;
            prcmdkey = true;
            int his = ((historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked).GetHashCode();
            int sci = scientificMI.Checked.GetHashCode();
            int pro = programmerMI.Checked.GetHashCode();
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked ||
                fe_MPG_MI.Checked || feL100_MI.Checked || mortgageMI.Checked || vehicleLeaseMI.Checked).GetHashCode();

            unitconvGB.Location = new Point(217 + 193 * (sci + pro), 12);
            unitconvGB.Size = new Size(360, 241 + 103 * his);

            hisDGV.EndEdit();

            if (historyMI.Checked && historyMI.Enabled)
            {
                if (standardMI.Checked) stdWithHistory();
                if (scientificMI.Checked)
                {
                    sciWithHistory();
                    if (hisDGV.RowCount == 0) hisDGV.CurrentCell = null;
                }
                hideProComponent(programmerMI.Checked);
                hideStaComponent(statisticsMI.Checked);
                hideSciComponent(scientificMI.Checked);   // phải đứng cuối
                if (hisDGV.RowCount > 0) hisDGV.CurrentCell = hisDGV[0, rowIndex];
            }
            else
            {
                if (standardMI.Checked) initializedForm(false);
                if (scientificMI.Checked) scientificLoad(false);
            }

            gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
            hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
            staDGV.Visible = /*countLB.Visible = */!historyMI.Checked || !historyMI.Enabled;

            countLB.Text = "";

            FormSizeChanged(new Size(216 + 197 * (sci + pro) + 376 * exf, 310 + 105 * his + 80 * pro), isLoaded);
            if (!isLoaded) propertiesChange = true;
        }
        /// <summary>
        /// 2 tính năng date input và unit conversion
        /// </summary>
        private void exFunc(MenuItem menuitem, bool isLoaded)
        {
            int his = (historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked ? 1 : 0;
            int pro = programmerMI.Checked ? 1 : 0;
            int sci = scientificMI.Checked ? 1 : 0;
            //if (!MI.Checked)
            FormSizeChanged(new Size(592 + 197 * (sci + pro), 310 + 105 * his + 64 * pro), isLoaded);

            datecalcGB.Visible = dateCalculationMI.Checked = (menuitem == dateCalculationMI);
            unitconvGB.Visible = unitConversionMI.Checked = (menuitem == unitConversionMI);
            feMPG_PN.Visible = (menuitem == fe_MPG_MI || menuitem == feL100_MI);
            morgagePN.Visible = (menuitem == mortgageMI);
            VhPN.Visible = (menuitem == vehicleLeaseMI);

            switch (menuitem.Index)
            {
                case 0: InitCustomControls(ref morgageTB, morgagePN, 4);
                    break;
                case 1: InitCustomControls(ref VhTB, VhPN, 5);
                    break;
                case 2: case 3: InitCustomControls(ref fempgTB, feMPG_PN, 2);
                    break;
            }

            mortgageMI.Checked = (menuitem == mortgageMI);
            vehicleLeaseMI.Checked = (menuitem == vehicleLeaseMI);
            fe_MPG_MI.Checked = (menuitem == fe_MPG_MI);
            feL100_MI.Checked = (menuitem == feL100_MI);

            unitconvGB.Size = new Size(360, 241 + 63 * pro + 103 * his);
            unitconvGB.Location = new Point(217 + 193 * (sci + pro), 12);

            basicMI.Checked = false;

            if (fe_MPG_MI.Checked || feL100_MI.Checked) this.AcceptButton = fuelEconomyBT;

            if (isLoaded)
            {
                datemethodCB.SelectedIndexChanged -= cal_method_SelectedIndexChanged;
                datemethodCB.SelectedIndex = configValue[7];
                datemethodCB.SelectedIndexChanged += cal_method_SelectedIndexChanged;
                //------------------------
                typeUnitCB.SelectedIndexChanged -= typeUnitCB_SelectedIndexChanged;
                typeUnitCB.SelectedIndex = configValue[5];
                typeUnitCB.SelectedIndexChanged += typeUnitCB_SelectedIndexChanged;
                EnableKeyboardAndChangeFocus();
            }
            toCombobox.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            assignDefaultIndex(typeUnitCB.SelectedIndex >= 0 ? typeUnitCB.SelectedIndex : 0);
            toCombobox.SelectedIndexChanged += fromCB_SelectedIndexChanged;

            typeFECB1.Visible = (menuitem == fe_MPG_MI);
            typeFECB2.Visible = (menuitem == feL100_MI);

            typeMorgageCB.SelectedIndex = 1;
            typeVhCB.SelectedIndex = 2;
            typeFECB1.SelectedIndex = 1;
            typeFECB2.SelectedIndex = 1;

            autocal_date.Checked = (configValue[6] == 1);
            if (dateCalculationMI.Checked) this.AcceptButton = calculate_date;
            if (fe_MPG_MI.Checked || feL100_MI.Checked) this.AcceptButton = fuelEconomyBT;

            prcmdkey = !typeUnitCB.Focused && !dtP1.Focused && !datemethodCB.Focused;

            if (!isLoaded)
            {
                propertiesChange = true;
                if (menuitem == unitConversionMI) { typeUnitCB.Focus(); typeUnitCB.Focus(); }
                if (menuitem == dateCalculationMI) datemethodCB.Focus();
                if (mortgageMI.Checked) typeMorgageCB.Focus();
                if (vehicleLeaseMI.Checked) typeMorgageCB.Focus();
                if (feL100_MI.Checked) typeFECB2.Focus();
                if (feL100_MI.Checked) typeFECB2.Focus();
            }
        }
        /// <summary>
        /// ẩn các control đặc biệt của form standard
        /// </summary>
        private void hideStdComponent(bool bl)
        {
            #region Hide standard functions
            ce.Visible = bl;
            addbt.Visible = bl;
            mulbt.Visible = bl;
            minusbt.Visible = bl;
            divbt.Visible = bl;
            equal.Visible = bl;
            invert_bt.Visible = bl;
            percent_bt.Visible = bl;
            sqrt_bt.Visible = bl;
            str = "0";
            #endregion
        }
        /// <summary>
        /// ẩn các control đặc biệt của form scientific
        /// </summary>
        private void hideSciComponent(bool bl)
        {
            #region Hide scientific functions
            angleGB.Visible = bl;
            inv_ChkBox.Visible = bl;
            sin_bt.Visible = bl;
            cos_bt.Visible = bl;
            tan_bt.Visible = bl;
            sinh_bt.Visible = bl;
            cosh_bt.Visible = bl;
            tanh_bt.Visible = bl;
            _10x_bt.Visible = bl;
            _3vx_bt.Visible = bl;
            nvx_bt.Visible = bl;
            log_bt.Visible = bl;
            ln_bt.Visible = bl;
            exp_bt.Visible = bl;
            pi_bt.Visible = bl;
            fe_ChkBox.Visible = bl;
            dms_bt.Visible = bl;
            int_bt.Visible = bl;
            x2_bt.Visible = bl;
            x3_bt.Visible = bl;
            xn_bt.Visible = bl;
            btFactorial.Visible = bl;
            modsciBT.Visible = bl;
            open_bracket.Visible = bl;
            close_bracket.Visible = bl;
            bracketTime_lb.Visible = bl;
            //str = "0";
            #endregion
        }
        /// <summary>
        /// ẩn các control đặc biệt của form programmer
        /// </summary>
        private void hideProComponent(bool bl)
        {
            #region Hide programmer functions
            unknownPN.Visible = bl;
            PNbinary.Visible = bl;
            basePN.Visible = bl;
            modsciBT.Visible = bl;
            RoRBT.Visible = bl;
            RoLBT.Visible = bl;
            RshBT.Visible = bl;
            LshBT.Visible = bl;
            or_BT.Visible = bl;
            XorBT.Visible = bl;
            NotBT.Visible = bl;
            AndBT.Visible = bl;
            bracketTime_lb.Visible = bl;
            openproBT.Visible = bl;
            closeproBT.Visible = bl;
            modproBT.Visible = bl;
            btnA.Visible = bl;
            btnB.Visible = bl;
            btnC.Visible = bl;
            btnD.Visible = bl;
            btnE.Visible = bl;
            btnF.Visible = bl;
            if (bl) str = "0";
            #endregion
        }
        /// <summary>
        /// ẩn các control đặc biệt của form programmer
        /// </summary>
        private void hideStaComponent(bool bl)
        {
            #region Hide statistics functions
            sigman_1BT.Visible = bl;
            sigmanBT.Visible = bl;
            sigmaxBT.Visible = bl;
            sigmax2BT.Visible = bl;
            xcross.Visible = bl;
            x2cross.Visible = bl;
            staDGV.Visible = /*countLB.Visible = */bl;
            CAD.Visible = bl;
            fe_ChkBox.Visible = bl;
            exp_bt.Visible = bl;
            AddstaBT.Visible = bl;
            datasetMI.Visible = bl;
            if (bl) str = "0";
            #endregion
        }
        /// <summary>
        /// enable các nút đã bị form programmer disable
        /// </summary>
        private void enableComponentByProgrammer()
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
            //this.num2BT.Location = new Point(267, 293);
            this.num2BT.Enabled = bl;
            //this.num3BT.Location = new Point(309, 293);
            this.num3BT.Enabled = bl;
            //this.num4BT.Location = new Point(225, 257);
            this.num4BT.Enabled = bl;
            //this.num5BT.Location = new Point(267, 257);
            this.num5BT.Enabled = bl;
            //this.num6BT.Location = new Point(309, 257);
            this.num6BT.Enabled = bl;
            //this.num7BT.Location = new Point(225, 221);
            this.num7BT.Enabled = bl;

            bl = (decRB.Checked || hexRB.Checked);
            //this.num8BT.Location = new Point(267, 221);
            this.num8BT.Enabled = bl;
            //this.num9BT.Location = new Point(309, 221);
            this.num9BT.Enabled = bl;
            ////
            //// base buttons
            ////
            //this.btnA.Location = new Point(183, 149);
            this.btnA.Enabled = hexRB.Checked;

            //this.btnB.Location = new Point(183, 185);
            this.btnB.Enabled = hexRB.Checked;

            //this.btnC.Location = new Point(183, 221);
            this.btnC.Enabled = hexRB.Checked;

            //this.btnD.Location = new Point(183, 257);
            this.btnD.Enabled = hexRB.Checked;

            //this.btnE.Location = new Point(183, 293);
            this.btnE.Enabled = hexRB.Checked;

            //this.btnF.Location = new Point(183, 329);
            this.btnF.Enabled = hexRB.Checked;
            string getClipboard = Clipboard.GetText().Trim();
            if (binRB.Checked) pasteMI.Enabled = Binary.CheckIsBin(getClipboard);
            if (octRB.Checked) pasteMI.Enabled = Binary.CheckIsOct(getClipboard);
            if (decRB.Checked) pasteMI.Enabled = Binary.CheckIsDec(getClipboard);
            if (hexRB.Checked) pasteMI.Enabled = Binary.CheckIsHex(getClipboard);
            pasteCTMN.Enabled = pasteMI.Enabled;
        }
        /// <summary>
        /// xử lý số trên bộ nhớ
        /// </summary>
        private void mem_process(string method)
        {
            if (pex == null)
            {
                if (method == "M+") mem_num = mem_num + str;  // M+
                if (method == "M-") mem_num = mem_num - str;  // M-
                if (method == "MS") mem_num = str;            // MS
                DisplayToScreen();

                mem_lb.Visible = (mem_num != 0);
                propertiesChange = true;
                toolTip1.SetToolTip(mem_lb, string.Format("M = {0}", mem_num.StrValue));
                confirm_num = true;
            }
        }

        string prevFunc = "0";
        bool isFuncClicked = false, slow = false;
        /// <summary>
        /// các hàm tính nâng cao
        /// </summary>
        private void math_func(Button bt)
        {
            BigNumber inp_num = str;
            pex = null;
            string parameter = str;
            int tabIndex = bt.TabIndex;
            if (isFuncClicked || sciexpAdd/* || str[0] == '-' || prevFunc[0] == '-'*/)
            {
                parameter = prevFunc;
            }
            if (pre_bt == 152) parameter = bracketExp[openBRK - closeBRK];
            int len = prevFunc.Length;
            switch (tabIndex)
            {
                #region Assign function
                // sin, cos, tan, sinh, cosh, tanh, log, int
                case 28: case 29: case 30:
                    prevFunc = string.Format("{0}({1})", bt.Text + parser.Angle, parameter);
                    break;
                case 35: case 36: case 37: case 42: case 44:
                    prevFunc = string.Format("{0}({1})", bt.Text, parameter);
                    break;
                case 45:
                    if (!inv_ChkBox.Checked) prevFunc = string.Format("dms({0})", parameter);
                    else prevFunc = string.Format("degrees({0})", parameter);
                    break;
                case 11:
                    prevFunc = string.Format("negate({0})", parameter);
                    break;
                case 17:
                    prevFunc = string.Format("reciproc({0})", parameter);
                    break;
                case 19:
                    prevFunc = string.Format("sqrt({0})", parameter);
                    break;
                case 31:
                    if (!inv_ChkBox.Checked) prevFunc = string.Format("ln({0})", parameter);
                    else prevFunc = string.Format("powe({0})", parameter);
                    break;
                case 32:
                    if (BigNumber.IsInteger(inp_num.StrValue))
                    {
                        if (configValue[10] == 1)
                            prevFunc = string.Format("fast({0})", parameter);
                        else
                            prevFunc = string.Format("fact({0})", parameter);
                    }
                    else return;
                    break;
                case 38:
                    prevFunc = string.Format("sqr({0})", parameter);
                    break;
                case 39:
                    prevFunc = string.Format("cube({0})", parameter);
                    break;
                case 40:
                    prevFunc = string.Format("powten({0})", parameter);
                    break;
                case 41:
                    prevFunc = string.Format("cuberoot({0})", parameter);
                    break;
                #endregion
            }

            // bo nhung dau ngoac thua cua prevFunc
            while (prevFunc.Contains("((")) { prevFunc = prevFunc.Replace("((", "(").Replace("))", ")"); }
            //if (pre_bt != 152) sciexpAdd = false;   // tai sao nhi
            if (pre_oprt == 0)
            {
                if (sci_exp.StartsWith("(")) sci_exp += prevFunc;
                else sci_exp = prevFunc;
                sciexpAdd = true;
            }
            else
            {
                // prevFunc bao gom ca 2 dau ngoac bao quanh nua~
                if (!isFuncClicked && pre_bt == 152 && prevFunc.StartsWith("(") && prevFunc.EndsWith(")"))
                    len += 2;
                if (sciexpAdd) sci_exp = sci_exp.Substring(0, sci_exp.Length - len) + prevFunc;
                else { sci_exp += prevFunc; sciexpAdd = true; }
            }

            if (inv_ChkBox.Checked) inv_ChkBox.Checked = false;
            if (standardMI.Checked) pex = parser.EvaluateStd(prevFunc);
            if (tabIndex != 32)
            {
                if (scientificMI.Checked) pex = parser.EvaluateSci(prevFunc);
                //--------------------------------------------------------------
                if (pex == null)
                {
                    str = parser.StringResult;
                    isFuncClicked = true;
                    confirm_num = true;
                }
                else
                {
                    str = pex.Message;//prevFunc);
                    scr_lb.Text = pex.Message;
                    scr_lb.Font = new Font("Consolas", pex.Message.Length > 40 ? 5.25F : 9.75F);

                    inv_ChkBox.Checked = false;
                    fe_ChkBox.Checked = false;
                }
                //pex = null;
                if (sciexpAdd) expressionTB.Text = Misc.StandardExpression(sci_exp);
                else expressionTB.Text = Misc.StandardExpression(sci_exp + prevFunc);
            }
            else
            {
                #region Calculate the factorial
                if (inp_num >= 1e5 && prevFunc.Contains("fact")) slow = true;
                if (slow)
                {
                    NewBGW(true);
                }
                else
                {
                    InitCancelOperation();
                    pex = parser.EvaluateSci(prevFunc);
                }
                if (pex == null) str = parser.StringResult;
                else str = pex.Message;
                if (str == "")
                {
                    pex = new Exception("Operation cancelled");
                    str = pex.Message;
                }
                isFuncClicked = confirm_num = true;   // funcID != 32;
                expressionTB.Text = Misc.StandardExpression(sci_exp);
                #endregion
            }
            pre_bt = tabIndex;
            DisplayToScreen();
        }

        private void NewBGW(bool isPref)
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerSupportsCancellation = true;
            if (isPref)
                mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWorkPrevFunc);
            else
                mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWorkExpr);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);
            mWorker.RunWorkerAsync();
            InitCancelOperation();
            co.ShowDialog();
        }

        private void recalculate(int rowID)
        {
            try
            {
                hisDGV.EndEdit();
                sci_exp = hisDGV[0, rowID].Value.ToString(); // sau khi ket thuc 1 phep tinh, sci_exp duoc gan ve ""
            }
            catch
            {
                sci_exp = oldValue.ToString();
                hisDGV[0, rowID].Value = oldValue;
            }
            evaluateExpression(rowID, true);
            //if (pex == null) DisplayToScreen();
        }
        /// <summary>
        /// đưa kết quả các phép tính +-*/ và nhị phân của form programmer lên màn hình
        /// </summary>
        private void programmerOperation()
        {
            /*
             * tìm nhị phân trước rồi mới tìm các hệ khác vì kết quả cuối cùng phụ thuộc vào nhị phân
             * (giá trị của kết quả nhị phân phụ thuộc vào các radiobutton qword, dword...)
             *
             * */
            try
            {
                binRB.Value = Binary.dec_to_other(resultpro.StrValue, 2, SizeBin, configValue[11] == 1);
                decRB.Value = Binary.other_to_dec(binRB.Value, 2, SizeBin, configValue[11] == 1);

                octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, configValue[11] == 1);
                hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, configValue[11] == 1);
            }
            catch (Exception ex)
            {
                pex = ex;
                scr_lb.TextChanged -= scr_lb_TextChanged;
                scr_lb.Text = str = "Result is too large";
                scr_lb.Font = new Font("Consolas", 9.75F);
                scr_lb.TextChanged += scr_lb_TextChanged;
                return;
            }
            binnum64 = binRB.Value.PadLeft(64, '0');
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(64 - (i + 1) * 4, 4);
            }

            if (binRB.Checked) str = binRB.Value;
            if (octRB.Checked) str = octRB.Value;
            if (decRB.Checked) str = decRB.Value;
            if (hexRB.Checked) str = hexRB.Value;
            scr_lb.Text = str;
        }
        /// <summary>
        /// nút xoá số
        /// </summary>
        /// <param name="c_bt">biến kiểm tra xem nút xoá có phải nút C hay không</param>
        private void clear_num(bool c_bt)
        {
            scr_lb.Text = str = "0";
            isFuncClicked = false;
            prcmdkey = confirm_num = true;

            scr_lb.Font = new Font("Consolas", 15.75F);
            if (pex != null) expressionTB.Text = sci_exp = "";

            if (c_bt)
            {
                pex = null;
                pre_oprt = pre_priority = priority = openBRK = closeBRK = 0;
                prevFunc = "0";
                expressionTB.Text = bracketTime_lb.Text = sci_exp = "";
                result = 0;
                pre_bt = -1;
                inv_ChkBox.Checked = fe_ChkBox.Checked = sciexpAdd = false;
                slow = false;
                mulDivFunc = power_Func = "";
            }
            if (bin_digit != null)
            {
                for (int i = 0; i < 16; i++) bin_digit[i].Text = "0000";
                ScreenToPanel();
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
                    if (decRB.Checked) scr_lb.Text = Misc.Group(str);
                    else if (octRB.Checked) scr_lb.Text = Misc.Group(str, 3, " ");
                    else if (binRB.Checked || hexRB.Checked) scr_lb.Text = Misc.Group(str, 4, " ");
                }
                else
                {
                    scr_lb.Text = str;
                }
            }
            else                      // nhóm cho form non-programmer
            {
                if (fe_ChkBox.Checked)
                {
                    scr_lb.Text = ((BigNumber)str).ToString();
                    return;	// không cần quan tâm đến digitGroupingMI nữa
                }
                if (!digitGroupingMI.Checked || str.ToUpper().Contains("E"))
                {
                    scr_lb.Text = str;
                }
                else
                {
                    scr_lb.Text = Misc.Group(str);
                }
            }
            if (hisDGV.Visible) hisDGV.Focus();
            if (staDGV.Visible) staDGV.Focus();
        }
        /// <summary>
        /// thay đổi số cho cụm ngoặc
        /// after press "2 * (3 + (4 + 5)", then press a number (eg: press 7)
        /// it will fix the expression to "2 + (3 * 7"
        /// </summary>
        private void FixNumberWhenChange()
        {
            // neu phim truoc do khong phai la phim mo ngoac
            if (sciexpAdd && pre_bt != 153)
            {
                sci_exp = sci_exp.Substring(0, sci_exp.Length - prevFunc.Length);
                sciexpAdd = false;
            }
            if (pre_bt == 152 && openBRK - closeBRK > 0)
            {
                string brkexp = bracketExp[openBRK - closeBRK - 1];
                brkexp = brkexp.Substring(0, brkexp.Length - prevFunc.Length);
                bracketExp[openBRK - closeBRK - 1] = brkexp;
                bracketExp[openBRK - closeBRK] = null;
            }
            if (pre_bt == 152 && openBRK - closeBRK == 0)
            {
                bracketExp = new string[20];
            }
        }
        /// <summary>
        /// nhập số cho form non-programmer
        /// </summary>
        private void numinput(object sender)
        {
            var bt = sender as Button;
            if (!bt.Enabled) return;
            int index = bt.TabIndex;
            string strTemp = "";
            if (index < 10 && pex == null)  // 0 đến 9
            {
                if (!confirm_num)
                {
                    sciexpAdd = false;
                    if (str.Contains("e"))
                    {
                        if (str.EndsWith("e+0")) str = str.Replace("e+0", "e+" + index.ToString());
                        else if (str.EndsWith("e-0")) str = str.Replace("e-0", "e-" + index.ToString());
                        else str += index.ToString();
                        if (str.Length >= 45) str = str.Substring(0, str.Length - 1);
                    }
                    else
                    {
                        if (str != "0") strTemp = str + index.ToString();
                        else { confirm_num = true; pre_bt = index; return; }
                        if (strTemp.Length < ((standardMI.Checked || statisticsMI.Checked) ? 16 : 42)
                            && (BigNumber)strTemp < "1e32") str = strTemp;
                        else return;
                    }
                }
                else
                {
                    //sciexpAdd = true;
                    str = index.ToString();
                    FixNumberWhenChange();
                }
                expressionTB.Text = Misc.StandardExpression(sci_exp);
                isFuncClicked = false;
                prevFunc = str;
                goto breakpoint;
            }
            if (index == 10 && pex == null)    // dấu thập phân
            {
                // chưa có dấu thập phân thì thêm vào, không có thì thôi
                if (confirm_num)
                {
                    str = "0" + Misc.DecimalSeparator;
                    if (sciexpAdd)
                        expressionTB.Text = Misc.StandardExpression(sci_exp = sci_exp.Substring(0, sci_exp.Length - prevFunc.Length));
                    else expressionTB.Text = Misc.StandardExpression(sci_exp);
                }
                if (!str.Contains(Misc.DecimalSeparator) && !str.Contains("e"))
                {
                    str += Misc.DecimalSeparator;
                    //if (!Misc.IsNumber(prevFunc))
                    //  prevFunc = prevFunc.Insert(prevFunc.IndexOf(")"), Misc.DecimalSeparator);
                }
                sciexpAdd = false;

                FixNumberWhenChange();
                prevFunc = str;

                isFuncClicked = false;
                goto breakpoint;
            }
            if (index == 11 && pex == null)    // nut doi dau
            {
                if (!confirm_num)
                {
                    if (str.Contains("e+")) { str = str.Replace("e+", "e-"); prevFunc = str; goto breakpoint; }
                    if (str.Contains("e-")) { str = str.Replace("e-", "e+"); prevFunc = str; goto breakpoint; }
                }
                if (str.StartsWith("-"))
                {
                    str = str.Substring(1);
                    prevFunc = str;
                }
                else
                {
                    if (str != "0") str = "-" + str;
                    prevFunc = string.Format("({0})", str);
                }

                pre_bt = index;
                DisplayToScreen();
                return;
            }
            breakpoint: confirm_num = false;
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
                if (programmerMI.Checked)
                {
                    if (decRB.Checked)
                    {
                        if (!confirm_num)
                        {
                            strTemp = str + index.ToString();
                            if (BigNumber.Two__.Pow(SizeBin - 1) >= strTemp) // strTemp chua vuot qua gia tri lon nhat
                                str += index.ToString();
                            else return;
                        }
                        else str = index.ToString();
                    }
                    else
                    {
                        if (!confirm_num)
                        {
                            strTemp = str + index.ToString();
                        }
                        else strTemp = index.ToString();
                        if (binRB.Checked)
                        {
                            if (strTemp.Length <= 64) str = strTemp;
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
                            BigNumber oct = strTemp;
                            if (oct <= max) str = strTemp;
                            else return;
                        }
                        if (hexRB.Checked)
                        {
                            if (strTemp.Length <= 16) str = strTemp;
                            else return;
                        }
                        // decrb.checked da xu ly o tren
                    }
                }
                //goto breakpoint;
            }
            if (index == 11)    // dấu âm
            {
                if (configValue[10] == 1)
                {
                    if (str.StartsWith("-")) { str = str.Substring(1); ScreenToPanel(); }
                    else
                    {
                        if (str == "0") return;
                        //quy doi ra he 10, lay phan bu voi 256, 65536,... roi tinh nguoc lai ve he cu
                        if (binRB.Checked)
                        {
                            decRB.Value = Binary.other_to_dec(str, 02, SizeBin, configValue[11] == 1);
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            str = binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, configValue[11] == 1);
                        }
                        if (octRB.Checked)
                        {
                            decRB.Value = Binary.other_to_dec(str, 08, SizeBin, configValue[11] == 1);
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            str = octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, configValue[11] == 1);
                            binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, configValue[11] == 1);
                        }
                        if (decRB.Checked)
                        {
                            decRB.Value = str = "-" + str;
                            binRB.Value = Binary.dec_to_other(decRB.Value, 2, SizeBin, configValue[11] == 1);
                        }
                        if (hexRB.Checked)
                        {
                            decRB.Value = Binary.other_to_dec(str, 16, SizeBin, configValue[11] == 1);
                            if (decRB.Value.StartsWith("-")) decRB.Value = decRB.Value.Substring(1);
                            else decRB.Value = "-" + decRB.Value;
                            str = hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, configValue[11] == 1);
                            binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, configValue[11] == 1);
                        }
                    }
                    binnum64 = binRB.Value.PadLeft(64, '0');
                    for (int i = 0; i < 16; i++)
                    {
                        bin_digit[i].Text = binnum64.Substring(60 - i * 4, 4);
                    }
                    confirm_num = true;
                }
                else
                {
                    if (str.StartsWith("-")) str = str.Substring(1);
                    else
                    {
                        if (str == "0") return;
                        str = (BigNumber.Two__.Pow(SizeBin) - decRB.Value).StrValue;
                        if (!decRB.Checked)
                        {
                            if (binRB.Checked) str = Binary.dec_to_other(str, 02, SizeBin, configValue[11] == 1);
                            if (octRB.Checked) str = Binary.dec_to_other(str, 08, SizeBin, configValue[11] == 1);
                            if (hexRB.Checked) str = Binary.dec_to_other(str, 16, SizeBin, configValue[11] == 1);
                        }
                        ScreenToPanel();
                        confirm_num = true;
                        return;
                    }
                }
                pre_bt = index;
                DisplayToScreen();
                return;
            }
            confirm_num = str == "0";
            pre_bt = index;
            ScreenToPanel();
        }
        /// <summary>
        /// các nút từ A đến F
        /// </summary>
        private void buttonAF(string text)
        {
            string strTemp = "";
            if (confirm_num)
            {
                str = text;
            }
            else
            {
                strTemp = str + text;
                int checkLen = 33;
                if (binRB.Checked) checkLen = 65;
                if (octRB.Checked) checkLen = 50;
                if (hexRB.Checked) checkLen = 17;
                if (binRB.Checked || octRB.Checked || hexRB.Checked)
                {
                    if (strTemp.Length < checkLen) str = strTemp;
                    else return;    // khong lam clgh
                }
            }
            confirm_num = false;
            prcmdkey = true;
            ScreenToPanel();
        }

        BigNumber result = "0";
        /// <summary>
        /// các toán tử +-*/ của form standard
        /// </summary>
        private void std_operation(int index)
        {
            string oper = "";
            if (index == 12) oper = "+";
            if (index == 13) oper = "-";
            if (index == 14) oper = "*";
            if (index == 15) oper = "/";
            if (pre_oprt != 0)
            {
                switch (pre_bt)
                {
                    case 12: case 13: case 14: case 15:
                        sci_exp = sci_exp.Substring(0, sci_exp.Length - 1) + oper;
                        goto breakpoint;
                }
                if (!sciexpAdd) sci_exp += prevFunc + oper; // chỉ việc ăn sẵn thôi
                else sci_exp += oper;
            }
            else    // pre_oprt != 0
            {
                if (isFuncClicked)
                {
                    if (!sciexpAdd) sci_exp += prevFunc + oper;
                    else sci_exp = prevFunc + oper;
                }
                else
                {
                    sci_exp += string.Format(" {0} {1}", str, oper);
                }
            }       // pre_oprt != 0

            pex = parser.EvaluateStd(sci_exp.Substring(0, sci_exp.Length - 1));
            str = parser.StringResult;

            breakpoint: confirm_num = true;
            expressionTB.Text = Misc.StandardExpression(sci_exp);
            DisplayToScreen();
            pre_oprt = pre_bt = index;
            prevFunc = str;
            isFuncClicked = false;
        }

        string power_Func = "", mulDivFunc = "";
        int priority, pre_priority, preoperLength = 0;
        #warning 08/04/2014 xử lý từ đây trước và hàm equal của nó
        /// <summary>
        /// các toán tử +-*/ của form scientific
        /// </summary>
        private void sci_operation(int index)
        {
            pre_priority = priority;
            string oper = "";
            if (index == 012) { oper = "+"; priority = 1; }
            if (index == 013) { oper = "-"; priority = 1; }
            if (index == 014) { oper = "*"; priority = 2; }
            if (index == 015) { oper = "/"; priority = 2; }
            if (index == 033) { oper = "^"; priority = 3; }
            if (index == 034) { oper = "yroot"; priority = 3; }
            if (index == 142) { oper = "mod"; priority = 2; }
            if (pre_oprt != 0)
            {
                switch (pre_bt)
                {
                    case 12: case 13: case 14: case 15: case 33: case 34: case 142:
                        if (pre_bt == 12 || pre_bt == 13)
                        {
                            mulDivFunc = prevFunc + oper;
                            power_Func = prevFunc + oper;
                        }
                        string format = "({0}){1}";
                        if (priority <= pre_priority || (sci_exp.StartsWith("(") && sci_exp[sci_exp.Length - 2] == ')')) format = "{0}{1}";
                        sci_exp = string.Format(format, sci_exp.Substring(0, sci_exp.Length - preoperLength), oper);
                        if (pre_bt == 14 || pre_bt == 15 || pre_bt == 142)
                            mulDivFunc = string.Format(format, mulDivFunc.Substring(0, mulDivFunc.Length - preoperLength), oper);
                        if (pre_bt == 33 || pre_bt == 34)
                            power_Func = string.Format(format, power_Func.Substring(0, power_Func.Length - preoperLength), oper);
                        goto breakpoint;    // thoat tai day
                }
                //case 17: case 19: case 28: case 29: case 30: case 31: case 35: case 36:
                //case 37: case 38: case 39: case 40: case 41: case 42: case 44: case 45:

                // nut truoc do la dong ngoac hay khong
                if (pre_bt == 46) { numinput(changesignBT); return; }
                if (sciexpAdd) sci_exp += oper;
                else sci_exp += prevFunc + oper;

                if (openBRK > closeBRK)
                {
                    if (!sciexpAdd) bracketExp[openBRK - closeBRK - 1] += prevFunc + oper;
                    else bracketExp[openBRK - closeBRK - 1] += oper;
                }
                if (index == 33 || index == 34)
                {
                    power_Func += (prevFunc + oper);
                    pex = parser.EvaluateSci(power_Func.Substring(0, power_Func.Length - 1));
                    str = parser.StringResult;
                }
                if (index == 14 || index == 15 || index == 142)
                {
                    if (pre_oprt == 33 || pre_oprt == 34)
                    {
                        power_Func += prevFunc;
                        pex = parser.EvaluateSci(power_Func);
                        mulDivFunc += parser.StringResult;
                        pex = parser.EvaluateSci(mulDivFunc);
                        str = parser.StringResult;
                        mulDivFunc += oper;
                    }
                    if (pre_oprt == 12 || pre_oprt == 13 || pre_oprt == 14 || pre_oprt == 15)
                    {
                        mulDivFunc += prevFunc;
                        pex = parser.EvaluateSci(mulDivFunc);
                        str = parser.StringResult;
                        mulDivFunc += oper;
                    }
                    power_Func = "";
                }
                if (index == 12 || index == 13)
                {
                    if (openBRK - closeBRK == 0) pex = parser.EvaluateSci(sci_exp.Substring(0, sci_exp.Length - 1));
                    else pex = parser.EvaluateSci(bracketExp[openBRK - closeBRK - 1].Substring(0, bracketExp[openBRK - closeBRK - 1].Length - 1));
                    str = parser.StringResult;
                    power_Func = "";
                    mulDivFunc = "";
                }
            }
            else    // pre_oprt == 0
            {
                if (pre_bt == 46) { numinput(changesignBT); return; }
                if (openBRK > 0)
                {
                    if (!sciexpAdd)
                    {
                        sci_exp += prevFunc + oper;
                        //bracketExp[openBRK - closeBRK - 1] += prevFunc + oper;
                    }
                    else
                    {
                        sci_exp += oper;
                    }
                    bracketExp[openBRK - closeBRK - 1] += prevFunc + oper;
                }
                else
                {
                    sci_exp = prevFunc + oper;
                }
                //-----------------------------------------------------------------
                if (index == 33 || index == 34)
                {
                    power_Func = prevFunc + oper;
                }
                if (index == 14 || index == 15 || index == 142)
                {
                    mulDivFunc = prevFunc + oper;
                }
            }       // pre_oprt != 0

            sciexpAdd = false;
            breakpoint: expressionTB.Text = Misc.StandardExpression(sci_exp);
            DisplayToScreen();
            preoperLength = oper.Length;
            pre_bt = pre_oprt = index;
            prevFunc = str;
            isFuncClicked = false;
            confirm_num = true;
        }

        BigNumber num1pro, num2pro = 0, resultpro = 0;
        /// <summary>
        /// các toán tử +-*/ của form programmer
        /// </summary>
        private void pro_operation(int index)
        {
            confirm_num = true;

            ScreenToPanel();
            if (pre_oprt == 0)
            {
                num1pro = decRB.Value;
                resultpro = num1pro;
            }
            else
            {
                switch (pre_oprt)
                {
                    case 12: case 13: case 14: case 15: case 211:
                        num1pro = resultpro;
                        num2pro = decRB.Value;
                        if (pre_oprt == 12) resultpro = num1pro + num2pro;
                        if (pre_oprt == 13) resultpro = num1pro - num2pro;
                        if (pre_oprt == 14) resultpro = num1pro * num2pro;
                        if (pre_oprt == 15)
                        {
                            if (num2pro != 0)
                            {
                                resultpro = num1pro / num2pro;
                            }
                            else
                            {
                                scr_lb.Text = "Cannot devide by zero";
                                goto jump;
                            }
                        }
                        if (pre_oprt == 211) resultpro = num1pro - num2pro * (num1pro / num2pro).Floor();
                        break;
                    default:
                        //Button tabIndex = modproBT;
                        //if (index == 199) tabIndex = or_BT;
                        //if (index == 200) tabIndex = LshBT;
                        //if (index == 204) tabIndex = AndBT;
                        //if (index == 205) tabIndex = XorBT;
                        //if (index == 210) tabIndex = RshBT;
                        bitWiseOperators(index);
                        resultpro = str;
                        break;
                }
                programmerOperation();
            }
            jump: pre_oprt = pre_bt = index;
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
            if (scientificMI.Checked) pex = parser.EvaluateSci(prevFunc);
        }
        //
        // tính giai thừa số lớn của biểu thức (sau khi ấn dấu bằng, tính lại biểu thức,...)
        //
        private void mWorker_DoWorkExpr(object sender, DoWorkEventArgs e)
        {
            lock (this)
            {
                if (scientificMI.Checked) pex = parser.EvaluateSci(sci_exp);
                if (mWorker.CancellationPending)
                {
                    e.Cancel = true;
                }
            }
        }

        private void mWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this)
            {
                if (co != null) co.Finished = true;
                mWorker.Dispose();
            }
        }

        private void co_DoCancel()
        {
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
                mWorker.Dispose();
                pex = new Exception("Operation cancelled");
            }
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
            this.num2BT = new System.Windows.Forms.Button();
            this.num3BT = new System.Windows.Forms.Button();
            this.num4BT = new System.Windows.Forms.Button();
            this.num5BT = new System.Windows.Forms.Button();
            this.num6BT = new System.Windows.Forms.Button();
            this.num7BT = new System.Windows.Forms.Button();
            this.num8BT = new System.Windows.Forms.Button();
            this.num9BT = new System.Windows.Forms.Button();
            this.num0BT = new System.Windows.Forms.Button();
            this.btdot = new System.Windows.Forms.Button();
            this.addbt = new System.Windows.Forms.Button();
            this.minusbt = new System.Windows.Forms.Button();
            this.mulbt = new System.Windows.Forms.Button();
            this.divbt = new System.Windows.Forms.Button();
            this.equal = new System.Windows.Forms.Button();
            this.invert_bt = new System.Windows.Forms.Button();
            this.percent_bt = new System.Windows.Forms.Button();
            this.backspacebt = new System.Windows.Forms.Button();
            this.ce = new System.Windows.Forms.Button();
            this.changesignBT = new System.Windows.Forms.Button();
            this.mem_clear = new System.Windows.Forms.Button();
            this.mem_store = new System.Windows.Forms.Button();
            this.mem_recall = new System.Windows.Forms.Button();
            this.sqrt_bt = new System.Windows.Forms.Button();
            this.clearbt = new System.Windows.Forms.Button();
            this.madd = new System.Windows.Forms.Button();
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
            this.btnF = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.XorBT = new System.Windows.Forms.Button();
            this.NotBT = new System.Windows.Forms.Button();
            this.AndBT = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.RshBT = new System.Windows.Forms.Button();
            this.RoRBT = new System.Windows.Forms.Button();
            this.LshBT = new System.Windows.Forms.Button();
            this.or_BT = new System.Windows.Forms.Button();
            this.RoLBT = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.openproBT = new System.Windows.Forms.Button();
            this.modproBT = new System.Windows.Forms.Button();
            this.closeproBT = new System.Windows.Forms.Button();
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
            this.menuStrip1 = new System.Windows.Forms.MainMenu(this.components);
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
            this.preferrencesMI = new System.Windows.Forms.MenuItem();
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
            this.helpCTMN = new System.Windows.Forms.ContextMenu();
            this.hotkeyMI = new System.Windows.Forms.MenuItem();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.mWorker = new System.ComponentModel.BackgroundWorker();
            this.VhPN = new Calculator.IPanel();
            this.typeVhLB = new System.Windows.Forms.Label();
            this.typeVhCB = new System.Windows.Forms.ComboBox();
            this.VhLB3 = new System.Windows.Forms.Label();
            this.VhLB1 = new System.Windows.Forms.Label();
            this.VhLB5 = new System.Windows.Forms.Label();
            this.VhLB4 = new System.Windows.Forms.Label();
            this.VhBT = new System.Windows.Forms.Button();
            this.VhLB2 = new System.Windows.Forms.Label();
            this.VhResultTB = new System.Windows.Forms.TextBox();
            this.morgagePN = new Calculator.IPanel();
            this.typeMorgageLB = new System.Windows.Forms.Label();
            this.typeMorgageCB = new System.Windows.Forms.ComboBox();
            this.morgageLB3 = new System.Windows.Forms.Label();
            this.morgageLB1 = new System.Windows.Forms.Label();
            this.morgageLB4 = new System.Windows.Forms.Label();
            this.morgageBT = new System.Windows.Forms.Button();
            this.morgageLB2 = new System.Windows.Forms.Label();
            this.morgageResultTB = new System.Windows.Forms.TextBox();
            this.feMPG_PN = new Calculator.IPanel();
            this.typeFEmpgLB = new System.Windows.Forms.Label();
            this.typeFECB2 = new System.Windows.Forms.ComboBox();
            this.typeFECB1 = new System.Windows.Forms.ComboBox();
            this.fempgLB2 = new System.Windows.Forms.Label();
            this.fempgLB1 = new System.Windows.Forms.Label();
            this.fuelEconomyBT = new System.Windows.Forms.Button();
            this.fempgResultTB = new System.Windows.Forms.TextBox();
            this.PNbinary = new Calculator.IPanel();
            this.basePN = new Calculator.IPanel();
            this.hexRB = new Calculator.IRadioButton();
            this.decRB = new Calculator.IRadioButton();
            this.octRB = new Calculator.IRadioButton();
            this.binRB = new Calculator.IRadioButton();
            this.angleGB = new Calculator.IPanel();
            this.graRB = new System.Windows.Forms.RadioButton();
            this.degRB = new System.Windows.Forms.RadioButton();
            this.radRB = new System.Windows.Forms.RadioButton();
            this.gridPanel = new Calculator.IPanel();
            this.countLB = new System.Windows.Forms.Label();
            this.hisDGV = new Calculator.IDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staDGV = new Calculator.IDataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.screenPN = new Calculator.IPanel();
            this.expressionTB = new Calculator.ILabel();
            this.mem_lb = new System.Windows.Forms.Label();
            this.scr_lb = new System.Windows.Forms.Label();
            this.unitconvGB = new Calculator.IPanel();
            this.toCombobox = new System.Windows.Forms.ComboBox();
            this.typeUnitLB = new System.Windows.Forms.Label();
            this.fromCB = new System.Windows.Forms.ComboBox();
            this.typeUnitCB = new System.Windows.Forms.ComboBox();
            this.invert_unit = new System.Windows.Forms.Button();
            this.toLB = new System.Windows.Forms.Label();
            this.toTB = new System.Windows.Forms.TextBox();
            this.fromLB = new System.Windows.Forms.Label();
            this.fromTB = new System.Windows.Forms.TextBox();
            this.datecalcGB = new Calculator.IPanel();
            this.result1 = new System.Windows.Forms.TextBox();
            this.result2 = new System.Windows.Forms.TextBox();
            this.calmethodLB = new System.Windows.Forms.Label();
            this.secondDate = new System.Windows.Forms.Label();
            this.dtP2 = new System.Windows.Forms.DateTimePicker();
            this.subrb = new System.Windows.Forms.RadioButton();
            this.dtP1 = new System.Windows.Forms.DateTimePicker();
            this.addrb = new System.Windows.Forms.RadioButton();
            this.addSubResultLB = new System.Windows.Forms.Label();
            this.firstDate = new System.Windows.Forms.Label();
            this.autocal_date = new System.Windows.Forms.CheckBox();
            this.dateDifferenceLB = new System.Windows.Forms.Label();
            this.calculate_date = new System.Windows.Forms.Button();
            this.yearAddSubLB = new System.Windows.Forms.Label();
            this.datemethodCB = new System.Windows.Forms.ComboBox();
            this.monthAddSubLB = new System.Windows.Forms.Label();
            this.dayAddSubLB = new System.Windows.Forms.Label();
            this.periodsDateUD = new Calculator.INumericUpDown();
            this.periodsMonthUD = new Calculator.INumericUpDown();
            this.periodsYearUD = new Calculator.INumericUpDown();
            this.unknownPN = new Calculator.IPanel();
            this._byteRB = new System.Windows.Forms.RadioButton();
            this.qwordRB = new System.Windows.Forms.RadioButton();
            this.dwordRB = new System.Windows.Forms.RadioButton();
            this._wordRB = new System.Windows.Forms.RadioButton();
            this.VhPN.SuspendLayout();
            this.morgagePN.SuspendLayout();
            this.feMPG_PN.SuspendLayout();
            this.basePN.SuspendLayout();
            this.angleGB.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).BeginInit();
            this.screenPN.SuspendLayout();
            this.unitconvGB.SuspendLayout();
            this.datecalcGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).BeginInit();
            this.unknownPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // num1BT
            // 
            this.num1BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // num2BT
            // 
            this.num2BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2BT.Location = new System.Drawing.Point(51, 194);
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
            // num3BT
            // 
            this.num3BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num3BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3BT.Location = new System.Drawing.Point(90, 194);
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
            // num4BT
            // 
            this.num4BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num4BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4BT.Location = new System.Drawing.Point(12, 162);
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
            // num5BT
            // 
            this.num5BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num5BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5BT.Location = new System.Drawing.Point(51, 162);
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
            // num6BT
            // 
            this.num6BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num6BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6BT.Location = new System.Drawing.Point(90, 162);
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
            // num7BT
            // 
            this.num7BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num7BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7BT.Location = new System.Drawing.Point(12, 130);
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
            // num8BT
            // 
            this.num8BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num8BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8BT.Location = new System.Drawing.Point(51, 130);
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
            // num9BT
            // 
            this.num9BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num9BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9BT.Location = new System.Drawing.Point(90, 130);
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
            // num0BT
            // 
            this.num0BT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // btdot
            // 
            this.btdot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(90, 226);
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
            // addbt
            // 
            this.addbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.minusbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.mulbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.divbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // equal
            // 
            this.equal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.equal.Location = new System.Drawing.Point(168, 194);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(34, 59);
            this.equal.TabIndex = 16;
            this.equal.TabStop = false;
            this.equal.Text = "=";
            this.equal.UseVisualStyleBackColor = true;
            this.equal.Click += new System.EventHandler(this.equal_Click);
            this.equal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.equal.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.equal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // invert_bt
            // 
            this.invert_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invert_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.invert_bt.Location = new System.Drawing.Point(168, 162);
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
            // percent_bt
            // 
            this.percent_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percent_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent_bt.Location = new System.Drawing.Point(168, 130);
            this.percent_bt.Name = "percent_bt";
            this.percent_bt.Size = new System.Drawing.Size(34, 27);
            this.percent_bt.TabIndex = 18;
            this.percent_bt.TabStop = false;
            this.percent_bt.Text = "%";
            this.percent_bt.UseVisualStyleBackColor = true;
            this.percent_bt.Click += new System.EventHandler(this.percent_Click);
            this.percent_bt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.percent_bt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.percent_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // backspacebt
            // 
            this.backspacebt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.backspacebt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspacebt.Location = new System.Drawing.Point(12, 98);
            this.backspacebt.Name = "backspacebt";
            this.backspacebt.Size = new System.Drawing.Size(34, 27);
            this.backspacebt.TabIndex = 22;
            this.backspacebt.TabStop = false;
            this.backspacebt.Text = "←";
            this.toolTip2.SetToolTip(this.backspacebt, "Backspace");
            this.backspacebt.UseVisualStyleBackColor = true;
            this.backspacebt.Click += new System.EventHandler(this.backspace_Click);
            this.backspacebt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.backspacebt.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.backspacebt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // ce
            // 
            this.ce.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.changesignBT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // mem_clear
            // 
            this.mem_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_clear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_clear.Location = new System.Drawing.Point(12, 66);
            this.mem_clear.Name = "mem_clear";
            this.mem_clear.Size = new System.Drawing.Size(34, 27);
            this.mem_clear.TabIndex = 23;
            this.mem_clear.TabStop = false;
            this.mem_clear.Text = "MC";
            this.mem_clear.UseVisualStyleBackColor = true;
            this.mem_clear.Click += new System.EventHandler(this.memclear_Click);
            this.mem_clear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_clear.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.mem_clear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mem_store
            // 
            this.mem_store.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mem_store.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_store.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_store.Location = new System.Drawing.Point(51, 66);
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
            this.mem_recall.Location = new System.Drawing.Point(90, 66);
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
            // sqrt_bt
            // 
            this.sqrt_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqrt_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sqrt_bt.Location = new System.Drawing.Point(168, 98);
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
            // clearbt
            // 
            this.clearbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // madd
            // 
            this.madd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.madd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.madd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.madd.Location = new System.Drawing.Point(129, 66);
            this.madd.Name = "madd";
            this.madd.Size = new System.Drawing.Size(34, 27);
            this.madd.TabIndex = 26;
            this.madd.TabStop = false;
            this.madd.Text = "M+";
            this.madd.UseVisualStyleBackColor = true;
            this.madd.Click += new System.EventHandler(this.memprocess_Click);
            this.madd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.madd.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.madd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
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
            this.close_bracket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_bracket.Location = new System.Drawing.Point(56, 332);
            this.close_bracket.Name = "close_bracket";
            this.close_bracket.Size = new System.Drawing.Size(34, 27);
            this.close_bracket.TabIndex = 152;
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
            this.open_bracket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.open_bracket.Location = new System.Drawing.Point(56, 332);
            this.open_bracket.Name = "open_bracket";
            this.open_bracket.Size = new System.Drawing.Size(34, 27);
            this.open_bracket.TabIndex = 153;
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
            this._10x_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.nvx_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.xn_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.log_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this._3vx_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.x3_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.x2_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.exp_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.modsciBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.modsciBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modsciBT.Location = new System.Drawing.Point(56, 332);
            this.modsciBT.Name = "modsciBT";
            this.modsciBT.Size = new System.Drawing.Size(34, 27);
            this.modsciBT.TabIndex = 142;
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
            this.ln_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dms_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tanh_bt.Font = new System.Drawing.Font("Tahoma", 7.5F);
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
            this.cosh_bt.Font = new System.Drawing.Font("Tahoma", 7.5F);
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
            this.int_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tan_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.sinh_bt.Font = new System.Drawing.Font("Tahoma", 8.25F);
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
            this.btFactorial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // btnF
            // 
            this.btnF.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnF.Location = new System.Drawing.Point(99, 332);
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
            // btnB
            // 
            this.btnB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnB.Location = new System.Drawing.Point(99, 332);
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
            // btnD
            // 
            this.btnD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnD.Location = new System.Drawing.Point(99, 332);
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
            // XorBT
            // 
            this.XorBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XorBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.XorBT.Location = new System.Drawing.Point(99, 332);
            this.XorBT.Name = "XorBT";
            this.XorBT.Size = new System.Drawing.Size(34, 27);
            this.XorBT.TabIndex = 205;
            this.XorBT.TabStop = false;
            this.XorBT.Text = "Xor";
            this.XorBT.UseVisualStyleBackColor = true;
            this.XorBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.XorBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.XorBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.XorBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // NotBT
            // 
            this.NotBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotBT.Location = new System.Drawing.Point(99, 332);
            this.NotBT.Name = "NotBT";
            this.NotBT.Size = new System.Drawing.Size(34, 27);
            this.NotBT.TabIndex = 203;
            this.NotBT.TabStop = false;
            this.NotBT.Text = "Not";
            this.NotBT.UseVisualStyleBackColor = true;
            this.NotBT.Click += new System.EventHandler(this.notBT_Click);
            this.NotBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.NotBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.NotBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // AndBT
            // 
            this.AndBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AndBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AndBT.Location = new System.Drawing.Point(99, 332);
            this.AndBT.Name = "AndBT";
            this.AndBT.Size = new System.Drawing.Size(34, 27);
            this.AndBT.TabIndex = 204;
            this.AndBT.TabStop = false;
            this.AndBT.Text = "And";
            this.AndBT.UseVisualStyleBackColor = true;
            this.AndBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.AndBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.AndBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AndBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnE.Location = new System.Drawing.Point(99, 332);
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
            // RshBT
            // 
            this.RshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RshBT.Location = new System.Drawing.Point(99, 332);
            this.RshBT.Name = "RshBT";
            this.RshBT.Size = new System.Drawing.Size(34, 27);
            this.RshBT.TabIndex = 210;
            this.RshBT.TabStop = false;
            this.RshBT.Text = "Rsh";
            this.RshBT.UseVisualStyleBackColor = true;
            this.RshBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.RshBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.RshBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.RshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // RoRBT
            // 
            this.RoRBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
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
            this.LshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LshBT.Location = new System.Drawing.Point(99, 332);
            this.LshBT.Name = "LshBT";
            this.LshBT.Size = new System.Drawing.Size(34, 27);
            this.LshBT.TabIndex = 200;
            this.LshBT.TabStop = false;
            this.LshBT.Text = "Lsh";
            this.LshBT.UseVisualStyleBackColor = true;
            this.LshBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.LshBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.LshBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.LshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // or_BT
            // 
            this.or_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.or_BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.or_BT.Location = new System.Drawing.Point(99, 332);
            this.or_BT.Name = "or_BT";
            this.or_BT.Size = new System.Drawing.Size(34, 27);
            this.or_BT.TabIndex = 199;
            this.or_BT.TabStop = false;
            this.or_BT.Text = "Or";
            this.or_BT.UseVisualStyleBackColor = true;
            this.or_BT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.or_BT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.or_BT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.or_BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // RoLBT
            // 
            this.RoLBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // btnA
            // 
            this.btnA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnA.Location = new System.Drawing.Point(99, 332);
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
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnC.Location = new System.Drawing.Point(99, 332);
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
            // openproBT
            // 
            this.openproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openproBT.Location = new System.Drawing.Point(99, 332);
            this.openproBT.Name = "openproBT";
            this.openproBT.Size = new System.Drawing.Size(34, 27);
            this.openproBT.TabIndex = 199;
            this.openproBT.TabStop = false;
            this.openproBT.Text = "(";
            this.openproBT.UseVisualStyleBackColor = true;
            this.openproBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.openproBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.openproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // modproBT
            // 
            this.modproBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.modproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modproBT.Location = new System.Drawing.Point(99, 332);
            this.modproBT.Name = "modproBT";
            this.modproBT.Size = new System.Drawing.Size(34, 27);
            this.modproBT.TabIndex = 211;
            this.modproBT.TabStop = false;
            this.modproBT.Text = "Mod";
            this.modproBT.UseVisualStyleBackColor = true;
            this.modproBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.modproBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.modproBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.modproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // closeproBT
            // 
            this.closeproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.closeproBT.Location = new System.Drawing.Point(99, 332);
            this.closeproBT.Name = "closeproBT";
            this.closeproBT.Size = new System.Drawing.Size(34, 27);
            this.closeproBT.TabIndex = 205;
            this.closeproBT.TabStop = false;
            this.closeproBT.Text = ")";
            this.closeproBT.UseVisualStyleBackColor = true;
            this.closeproBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.closeproBT.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.closeproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
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
            this.AddstaBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.CAD.Font = new System.Drawing.Font("Tahoma", 7.5F);
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
            this.inv_ChkBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            this.inv_ChkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // dnBT
            // 
            this.dnBT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dnBT.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnBT.Location = new System.Drawing.Point(167, 2);
            this.dnBT.Name = "dnBT";
            this.dnBT.Size = new System.Drawing.Size(17, 16);
            this.dnBT.TabIndex = 1;
            this.dnBT.TabStop = false;
            this.dnBT.Text = "▼";
            this.toolTip2.SetToolTip(this.dnBT, "Down");
            this.dnBT.UseVisualStyleBackColor = true;
            this.dnBT.Click += new System.EventHandler(this.dnBT_Click);
            this.dnBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.dnBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // upBT
            // 
            this.upBT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.upBT.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upBT.Location = new System.Drawing.Point(147, 2);
            this.upBT.Name = "upBT";
            this.upBT.Size = new System.Drawing.Size(17, 16);
            this.upBT.TabIndex = 1;
            this.upBT.TabStop = false;
            this.upBT.Text = "▲";
            this.toolTip2.SetToolTip(this.upBT, "Up");
            this.upBT.UseVisualStyleBackColor = true;
            this.upBT.Click += new System.EventHandler(this.upBT_Click);
            this.upBT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.upBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // fe_ChkBox
            // 
            this.fe_ChkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fe_ChkBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fe_ChkBox.Location = new System.Drawing.Point(186, 331);
            this.fe_ChkBox.Name = "fe_ChkBox";
            this.fe_ChkBox.Size = new System.Drawing.Size(34, 27);
            this.fe_ChkBox.TabIndex = 43;
            this.fe_ChkBox.TabStop = false;
            this.fe_ChkBox.Text = "F-E";
            this.fe_ChkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fe_ChkBox.UseVisualStyleBackColor = true;
            this.fe_ChkBox.CheckedChanged += new System.EventHandler(this.fe_ChkBox_CheckChanged);
            this.fe_ChkBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.fe_ChkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // bracketTime_lb
            // 
            this.bracketTime_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bracketTime_lb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bracketTime_lb.Location = new System.Drawing.Point(13, 332);
            this.bracketTime_lb.Name = "bracketTime_lb";
            this.bracketTime_lb.Size = new System.Drawing.Size(34, 27);
            this.bracketTime_lb.TabIndex = 129;
            this.bracketTime_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bracketTime_lb.TextChanged += new System.EventHandler(this.bracketTime_lb_TextChanged);
            this.bracketTime_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.bracketTime_lb.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // cos_bt
            // 
            this.cos_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.sin_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // menuStrip1
            // 
            this.menuStrip1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.preferrencesMI});
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
            // preferrencesMI
            // 
            this.preferrencesMI.Index = 6;
            this.preferrencesMI.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.preferrencesMI.Text = "&Preferences";
            this.preferrencesMI.Click += new System.EventHandler(this.preferrencesMI_Click);
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
            this.clearDatasetCTMN});
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
            // helpCTMN
            // 
            this.helpCTMN.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.hotkeyMI});
            // 
            // hotkeyMI
            // 
            this.hotkeyMI.Index = 0;
            this.hotkeyMI.Text = "What is this?";
            this.hotkeyMI.Click += new System.EventHandler(this.hotkeyMI_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(99, 437);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 17);
            this.radioButton1.TabIndex = 251;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // mWorker
            // 
            this.mWorker.WorkerSupportsCancellation = true;
            this.mWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mWorker_DoWorkPrevFunc);
            this.mWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mWorker_RunWorkerCompleted);
            // 
            // VhPN
            // 
            this.VhPN.BackColor = System.Drawing.Color.White;
            this.VhPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VhPN.Controls.Add(this.typeVhLB);
            this.VhPN.Controls.Add(this.typeVhCB);
            this.VhPN.Controls.Add(this.VhLB3);
            this.VhPN.Controls.Add(this.VhLB1);
            this.VhPN.Controls.Add(this.VhLB5);
            this.VhPN.Controls.Add(this.VhLB4);
            this.VhPN.Controls.Add(this.VhBT);
            this.VhPN.Controls.Add(this.VhLB2);
            this.VhPN.Controls.Add(this.VhResultTB);
            this.VhPN.Location = new System.Drawing.Point(236, 12);
            this.VhPN.Name = "VhPN";
            this.VhPN.Size = new System.Drawing.Size(356, 241);
            this.VhPN.TabIndex = 35;
            this.VhPN.Visible = false;
            this.VhPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeVhLB
            // 
            this.typeVhLB.AutoSize = true;
            this.typeVhLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeVhLB.Location = new System.Drawing.Point(12, 9);
            this.typeVhLB.Name = "typeVhLB";
            this.typeVhLB.Size = new System.Drawing.Size(194, 13);
            this.typeVhLB.TabIndex = 8;
            this.typeVhLB.Text = "Select the Speed you want to calculate";
            this.typeVhLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeVhCB
            // 
            this.typeVhCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeVhCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeVhCB.FormattingEnabled = true;
            this.typeVhCB.Items.AddRange(new object[] {
            "Lease period",
            "Lease Speed",
            "Periodic payment",
            "Residual Speed"});
            this.typeVhCB.Location = new System.Drawing.Point(12, 32);
            this.typeVhCB.MaxDropDownItems = 11;
            this.typeVhCB.Name = "typeVhCB";
            this.typeVhCB.Size = new System.Drawing.Size(330, 21);
            this.typeVhCB.TabIndex = 9;
            this.typeVhCB.SelectedIndexChanged += new System.EventHandler(this.typeVhCB_SelectedIndexChanged);
            this.typeVhCB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // VhLB3
            // 
            this.VhLB3.AutoSize = true;
            this.VhLB3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhLB3.Location = new System.Drawing.Point(12, 110);
            this.VhLB3.Name = "VhLB3";
            this.VhLB3.Size = new System.Drawing.Size(98, 13);
            this.VhLB3.TabIndex = 6;
            this.VhLB3.Text = "Payments per year";
            this.VhLB3.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhLB3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // VhLB1
            // 
            this.VhLB1.AutoSize = true;
            this.VhLB1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhLB1.Location = new System.Drawing.Point(12, 62);
            this.VhLB1.Name = "VhLB1";
            this.VhLB1.Size = new System.Drawing.Size(68, 13);
            this.VhLB1.TabIndex = 6;
            this.VhLB1.Text = "Lease Speed";
            this.VhLB1.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhLB1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // VhLB5
            // 
            this.VhLB5.AutoSize = true;
            this.VhLB5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhLB5.Location = new System.Drawing.Point(12, 158);
            this.VhLB5.Name = "VhLB5";
            this.VhLB5.Size = new System.Drawing.Size(91, 13);
            this.VhLB5.TabIndex = 8;
            this.VhLB5.Text = "Interest rate (%)";
            this.VhLB5.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhLB5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // VhLB4
            // 
            this.VhLB4.AutoSize = true;
            this.VhLB4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhLB4.Location = new System.Drawing.Point(12, 134);
            this.VhLB4.Name = "VhLB4";
            this.VhLB4.Size = new System.Drawing.Size(80, 13);
            this.VhLB4.TabIndex = 7;
            this.VhLB4.Text = "Residual Speed";
            this.VhLB4.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhLB4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // VhBT
            // 
            this.VhBT.Enabled = false;
            this.VhBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhBT.Location = new System.Drawing.Point(12, 210);
            this.VhBT.Name = "VhBT";
            this.VhBT.Size = new System.Drawing.Size(82, 22);
            this.VhBT.TabIndex = 12;
            this.VhBT.Text = "Calculate";
            this.VhBT.UseVisualStyleBackColor = true;
            this.VhBT.Click += new System.EventHandler(this.fempgCalculateBT_Click);
            this.VhBT.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // VhLB2
            // 
            this.VhLB2.AutoSize = true;
            this.VhLB2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhLB2.Location = new System.Drawing.Point(12, 86);
            this.VhLB2.Name = "VhLB2";
            this.VhLB2.Size = new System.Drawing.Size(68, 13);
            this.VhLB2.TabIndex = 7;
            this.VhLB2.Text = "Lease period";
            this.VhLB2.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhLB2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // VhResultTB
            // 
            this.VhResultTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.VhResultTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.VhResultTB.Location = new System.Drawing.Point(177, 212);
            this.VhResultTB.MaxLength = 20;
            this.VhResultTB.Name = "VhResultTB";
            this.VhResultTB.ReadOnly = true;
            this.VhResultTB.Size = new System.Drawing.Size(165, 21);
            this.VhResultTB.TabIndex = 15;
            this.VhResultTB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.VhResultTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.VhResultTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // morgagePN
            // 
            this.morgagePN.BackColor = System.Drawing.Color.White;
            this.morgagePN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.morgagePN.Controls.Add(this.typeMorgageLB);
            this.morgagePN.Controls.Add(this.typeMorgageCB);
            this.morgagePN.Controls.Add(this.morgageLB3);
            this.morgagePN.Controls.Add(this.morgageLB1);
            this.morgagePN.Controls.Add(this.morgageLB4);
            this.morgagePN.Controls.Add(this.morgageBT);
            this.morgagePN.Controls.Add(this.morgageLB2);
            this.morgagePN.Controls.Add(this.morgageResultTB);
            this.morgagePN.Location = new System.Drawing.Point(236, 12);
            this.morgagePN.Name = "morgagePN";
            this.morgagePN.Size = new System.Drawing.Size(356, 241);
            this.morgagePN.TabIndex = 34;
            this.morgagePN.Visible = false;
            this.morgagePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeMorgageLB
            // 
            this.typeMorgageLB.AutoSize = true;
            this.typeMorgageLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeMorgageLB.Location = new System.Drawing.Point(12, 9);
            this.typeMorgageLB.Name = "typeMorgageLB";
            this.typeMorgageLB.Size = new System.Drawing.Size(194, 13);
            this.typeMorgageLB.TabIndex = 8;
            this.typeMorgageLB.Text = "Select the Speed you want to calculate";
            this.typeMorgageLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeMorgageCB
            // 
            this.typeMorgageCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeMorgageCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeMorgageCB.FormattingEnabled = true;
            this.typeMorgageCB.Items.AddRange(new object[] {
            "Down payment",
            "Monthly payment",
            "Perchase price",
            "Term (years)"});
            this.typeMorgageCB.Location = new System.Drawing.Point(12, 32);
            this.typeMorgageCB.MaxDropDownItems = 11;
            this.typeMorgageCB.Name = "typeMorgageCB";
            this.typeMorgageCB.Size = new System.Drawing.Size(330, 21);
            this.typeMorgageCB.TabIndex = 9;
            this.typeMorgageCB.SelectedIndexChanged += new System.EventHandler(this.typeMorgageCB_SelectedIndexChanged);
            this.typeMorgageCB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // morgageLB3
            // 
            this.morgageLB3.AutoSize = true;
            this.morgageLB3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageLB3.Location = new System.Drawing.Point(12, 110);
            this.morgageLB3.Name = "morgageLB3";
            this.morgageLB3.Size = new System.Drawing.Size(69, 13);
            this.morgageLB3.TabIndex = 6;
            this.morgageLB3.Text = "Term (years)";
            this.morgageLB3.Enter += new System.EventHandler(this.DisableKeyboard);
            this.morgageLB3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // morgageLB1
            // 
            this.morgageLB1.AutoSize = true;
            this.morgageLB1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageLB1.Location = new System.Drawing.Point(12, 62);
            this.morgageLB1.Name = "morgageLB1";
            this.morgageLB1.Size = new System.Drawing.Size(77, 13);
            this.morgageLB1.TabIndex = 6;
            this.morgageLB1.Text = "Perchase price";
            this.morgageLB1.Enter += new System.EventHandler(this.DisableKeyboard);
            this.morgageLB1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // morgageLB4
            // 
            this.morgageLB4.AutoSize = true;
            this.morgageLB4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageLB4.Location = new System.Drawing.Point(12, 134);
            this.morgageLB4.Name = "morgageLB4";
            this.morgageLB4.Size = new System.Drawing.Size(97, 13);
            this.morgageLB4.TabIndex = 7;
            this.morgageLB4.Text = "Interest reate (%)";
            this.morgageLB4.Enter += new System.EventHandler(this.DisableKeyboard);
            this.morgageLB4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // morgageBT
            // 
            this.morgageBT.Enabled = false;
            this.morgageBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageBT.Location = new System.Drawing.Point(12, 210);
            this.morgageBT.Name = "morgageBT";
            this.morgageBT.Size = new System.Drawing.Size(82, 22);
            this.morgageBT.TabIndex = 12;
            this.morgageBT.Text = "Calculate";
            this.morgageBT.UseVisualStyleBackColor = true;
            this.morgageBT.Click += new System.EventHandler(this.fempgCalculateBT_Click);
            this.morgageBT.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // morgageLB2
            // 
            this.morgageLB2.AutoSize = true;
            this.morgageLB2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageLB2.Location = new System.Drawing.Point(12, 86);
            this.morgageLB2.Name = "morgageLB2";
            this.morgageLB2.Size = new System.Drawing.Size(79, 13);
            this.morgageLB2.TabIndex = 7;
            this.morgageLB2.Text = "Down payment";
            this.morgageLB2.Enter += new System.EventHandler(this.DisableKeyboard);
            this.morgageLB2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // morgageResultTB
            // 
            this.morgageResultTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.morgageResultTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.morgageResultTB.Location = new System.Drawing.Point(177, 212);
            this.morgageResultTB.MaxLength = 20;
            this.morgageResultTB.Name = "morgageResultTB";
            this.morgageResultTB.ReadOnly = true;
            this.morgageResultTB.Size = new System.Drawing.Size(165, 21);
            this.morgageResultTB.TabIndex = 15;
            this.morgageResultTB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.morgageResultTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.morgageResultTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // feMPG_PN
            // 
            this.feMPG_PN.BackColor = System.Drawing.Color.White;
            this.feMPG_PN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.feMPG_PN.Controls.Add(this.typeFEmpgLB);
            this.feMPG_PN.Controls.Add(this.typeFECB2);
            this.feMPG_PN.Controls.Add(this.typeFECB1);
            this.feMPG_PN.Controls.Add(this.fempgLB2);
            this.feMPG_PN.Controls.Add(this.fempgLB1);
            this.feMPG_PN.Controls.Add(this.fuelEconomyBT);
            this.feMPG_PN.Controls.Add(this.fempgResultTB);
            this.feMPG_PN.Location = new System.Drawing.Point(236, 12);
            this.feMPG_PN.Name = "feMPG_PN";
            this.feMPG_PN.Size = new System.Drawing.Size(356, 241);
            this.feMPG_PN.TabIndex = 33;
            this.feMPG_PN.Visible = false;
            this.feMPG_PN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeFEmpgLB
            // 
            this.typeFEmpgLB.AutoSize = true;
            this.typeFEmpgLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeFEmpgLB.Location = new System.Drawing.Point(12, 9);
            this.typeFEmpgLB.Name = "typeFEmpgLB";
            this.typeFEmpgLB.Size = new System.Drawing.Size(194, 13);
            this.typeFEmpgLB.TabIndex = 8;
            this.typeFEmpgLB.Text = "Select the Speed you want to calculate";
            this.typeFEmpgLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // typeFECB2
            // 
            this.typeFECB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeFECB2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeFECB2.FormattingEnabled = true;
            this.typeFECB2.Items.AddRange(new object[] {
            "Distance (kilometers)",
            "Fuel economy (L/100 km)",
            "Fuel used (liters)"});
            this.typeFECB2.Location = new System.Drawing.Point(12, 32);
            this.typeFECB2.MaxDropDownItems = 11;
            this.typeFECB2.Name = "typeFECB2";
            this.typeFECB2.Size = new System.Drawing.Size(330, 21);
            this.typeFECB2.TabIndex = 9;
            this.typeFECB2.SelectedIndexChanged += new System.EventHandler(this.typeFECB_SelectedIndexChanged);
            this.typeFECB2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeFECB1
            // 
            this.typeFECB1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeFECB1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeFECB1.FormattingEnabled = true;
            this.typeFECB1.Items.AddRange(new object[] {
            "Distance (miles)",
            "Fuel economy (mpg)",
            "Fuel used (gallons)"});
            this.typeFECB1.Location = new System.Drawing.Point(12, 32);
            this.typeFECB1.MaxDropDownItems = 11;
            this.typeFECB1.Name = "typeFECB1";
            this.typeFECB1.Size = new System.Drawing.Size(330, 21);
            this.typeFECB1.TabIndex = 9;
            this.typeFECB1.SelectedIndexChanged += new System.EventHandler(this.typeFECB_SelectedIndexChanged);
            this.typeFECB1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // fempgLB2
            // 
            this.fempgLB2.AutoSize = true;
            this.fempgLB2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fempgLB2.Location = new System.Drawing.Point(12, 86);
            this.fempgLB2.Name = "fempgLB2";
            this.fempgLB2.Size = new System.Drawing.Size(92, 13);
            this.fempgLB2.TabIndex = 7;
            this.fempgLB2.Text = "Fuel used (gallon)";
            this.fempgLB2.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fempgLB2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fempgLB1
            // 
            this.fempgLB1.AutoSize = true;
            this.fempgLB1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fempgLB1.Location = new System.Drawing.Point(12, 62);
            this.fempgLB1.Name = "fempgLB1";
            this.fempgLB1.Size = new System.Drawing.Size(82, 13);
            this.fempgLB1.TabIndex = 6;
            this.fempgLB1.Text = "Distance (miles)";
            this.fempgLB1.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fempgLB1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fuelEconomyBT
            // 
            this.fuelEconomyBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fuelEconomyBT.Location = new System.Drawing.Point(12, 210);
            this.fuelEconomyBT.Name = "fuelEconomyBT";
            this.fuelEconomyBT.Size = new System.Drawing.Size(82, 22);
            this.fuelEconomyBT.TabIndex = 12;
            this.fuelEconomyBT.Text = "Calculate";
            this.fuelEconomyBT.UseVisualStyleBackColor = true;
            this.fuelEconomyBT.Click += new System.EventHandler(this.fempgCalculateBT_Click);
            this.fuelEconomyBT.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // fempgResultTB
            // 
            this.fempgResultTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fempgResultTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fempgResultTB.Location = new System.Drawing.Point(177, 212);
            this.fempgResultTB.MaxLength = 20;
            this.fempgResultTB.Name = "fempgResultTB";
            this.fempgResultTB.ReadOnly = true;
            this.fempgResultTB.Size = new System.Drawing.Size(165, 21);
            this.fempgResultTB.TabIndex = 13;
            this.fempgResultTB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fempgResultTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.fempgResultTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // PNbinary
            // 
            this.PNbinary.BackColor = System.Drawing.Color.Transparent;
            this.PNbinary.Location = new System.Drawing.Point(186, 397);
            this.PNbinary.Name = "PNbinary";
            this.PNbinary.Size = new System.Drawing.Size(385, 60);
            this.PNbinary.TabIndex = 214;
            this.PNbinary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
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
            this.basePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // hexRB
            // 
            this.hexRB.AutoSize = true;
            this.hexRB.ContextMenu = this.helpCTMN;
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
            this.decRB.Location = new System.Drawing.Point(7, 25);
            this.decRB.Name = "decRB";
            this.decRB.Size = new System.Drawing.Size(43, 17);
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
            this.octRB.Location = new System.Drawing.Point(7, 46);
            this.octRB.Name = "octRB";
            this.octRB.Size = new System.Drawing.Size(42, 17);
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
            this.binRB.Location = new System.Drawing.Point(7, 67);
            this.binRB.Name = "binRB";
            this.binRB.Size = new System.Drawing.Size(39, 17);
            this.binRB.TabIndex = 119;
            this.binRB.Text = "Bin";
            this.binRB.UseVisualStyleBackColor = true;
            this.binRB.Value = "0";
            this.binRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            this.binRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.binRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // angleGB
            // 
            this.angleGB.Controls.Add(this.graRB);
            this.angleGB.Controls.Add(this.degRB);
            this.angleGB.Controls.Add(this.radRB);
            this.angleGB.Location = new System.Drawing.Point(12, 279);
            this.angleGB.Name = "angleGB";
            this.angleGB.Size = new System.Drawing.Size(190, 28);
            this.angleGB.TabIndex = 128;
            this.angleGB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // graRB
            // 
            this.graRB.AutoSize = true;
            this.graRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graRB.Location = new System.Drawing.Point(134, 6);
            this.graRB.Name = "graRB";
            this.graRB.Size = new System.Drawing.Size(53, 17);
            this.graRB.TabIndex = 116;
            this.graRB.Text = "Grads";
            this.graRB.UseVisualStyleBackColor = true;
            this.graRB.CheckedChanged += new System.EventHandler(this.angelRB_CheckedChanged);
            this.graRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.graRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // degRB
            // 
            this.degRB.AutoSize = true;
            this.degRB.Checked = true;
            this.degRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degRB.Location = new System.Drawing.Point(7, 6);
            this.degRB.Name = "degRB";
            this.degRB.Size = new System.Drawing.Size(65, 17);
            this.degRB.TabIndex = 114;
            this.degRB.TabStop = true;
            this.degRB.Text = "Degrees";
            this.degRB.UseVisualStyleBackColor = true;
            this.degRB.CheckedChanged += new System.EventHandler(this.angelRB_CheckedChanged);
            this.degRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.degRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // radRB
            // 
            this.radRB.AutoSize = true;
            this.radRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRB.Location = new System.Drawing.Point(72, 6);
            this.radRB.Name = "radRB";
            this.radRB.Size = new System.Drawing.Size(63, 17);
            this.radRB.TabIndex = 115;
            this.radRB.Text = "Radians";
            this.radRB.UseVisualStyleBackColor = true;
            this.radRB.CheckedChanged += new System.EventHandler(this.angelRB_CheckedChanged);
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
            this.countLB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.countLB.Location = new System.Drawing.Point(4, 4);
            this.countLB.Name = "countLB";
            this.countLB.Size = new System.Drawing.Size(56, 13);
            this.countLB.TabIndex = 2;
            this.countLB.Text = "Count = 0";
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hisDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.hisDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
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
            this.hisDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellClick);
            this.hisDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellEndEdit);
            this.hisDGV.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.historyDGV_CellStateChanged);
            this.hisDGV.DoubleClick += new System.EventHandler(this.hisDGV_DoubleClick);
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
            this.staDGV.AllowCellClick = false;
            this.staDGV.AllowCellDoubleClick = false;
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.staDGV.DefaultCellStyle = dataGridViewCellStyle3;
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
            this.staDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.statisticsDGV_CellEndEdit);
            this.staDGV.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.staDGV_CellStateChanged);
            this.staDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 190;
            // 
            // screenPN
            // 
            this.screenPN.BackColor = System.Drawing.Color.White;
            this.screenPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenPN.ContextMenu = this.mainContextMenu;
            this.screenPN.Controls.Add(this.expressionTB);
            this.screenPN.Controls.Add(this.mem_lb);
            this.screenPN.Controls.Add(this.scr_lb);
            this.screenPN.Location = new System.Drawing.Point(12, 12);
            this.screenPN.Name = "screenPN";
            this.screenPN.Size = new System.Drawing.Size(190, 47);
            this.screenPN.TabIndex = 154;
            this.screenPN.TabStop = true;
            this.screenPN.BackColorChanged += new System.EventHandler(this.screenPN_BackColorChanged);
            this.screenPN.SizeChanged += new System.EventHandler(this.screenPN_SizeChanged);
            this.screenPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // expressionTB
            // 
            this.expressionTB.AllowTextChanged = true;
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
            // mem_lb
            // 
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.Transparent;
            this.mem_lb.Location = new System.Drawing.Point(1, 26);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(15, 13);
            this.mem_lb.TabIndex = 21;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            this.mem_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // scr_lb
            // 
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
            // unitconvGB
            // 
            this.unitconvGB.BackColor = System.Drawing.Color.White;
            this.unitconvGB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unitconvGB.Controls.Add(this.toCombobox);
            this.unitconvGB.Controls.Add(this.typeUnitLB);
            this.unitconvGB.Controls.Add(this.fromCB);
            this.unitconvGB.Controls.Add(this.typeUnitCB);
            this.unitconvGB.Controls.Add(this.invert_unit);
            this.unitconvGB.Controls.Add(this.toLB);
            this.unitconvGB.Controls.Add(this.toTB);
            this.unitconvGB.Controls.Add(this.fromLB);
            this.unitconvGB.Controls.Add(this.fromTB);
            this.unitconvGB.Location = new System.Drawing.Point(236, 12);
            this.unitconvGB.Name = "unitconvGB";
            this.unitconvGB.Size = new System.Drawing.Size(356, 241);
            this.unitconvGB.TabIndex = 31;
            this.unitconvGB.Visible = false;
            this.unitconvGB.LocationChanged += new System.EventHandler(this.unitconvGB_LocationChanged);
            this.unitconvGB.SizeChanged += new System.EventHandler(this.unitconvGB_SizeChanged);
            this.unitconvGB.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.unitconvGB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // toCombobox
            // 
            this.toCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toCombobox.FormattingEnabled = true;
            this.toCombobox.Location = new System.Drawing.Point(12, 181);
            this.toCombobox.Name = "toCombobox";
            this.toCombobox.Size = new System.Drawing.Size(330, 21);
            this.toCombobox.TabIndex = 14;
            this.toCombobox.SelectedIndexChanged += new System.EventHandler(this.fromCB_SelectedIndexChanged);
            this.toCombobox.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeUnitLB
            // 
            this.typeUnitLB.AutoSize = true;
            this.typeUnitLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeUnitLB.Location = new System.Drawing.Point(12, 8);
            this.typeUnitLB.Name = "typeUnitLB";
            this.typeUnitLB.Size = new System.Drawing.Size(215, 13);
            this.typeUnitLB.TabIndex = 8;
            this.typeUnitLB.Text = "Select the type of unit you want to convert";
            this.typeUnitLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fromCB
            // 
            this.fromCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromCB.FormattingEnabled = true;
            this.fromCB.Location = new System.Drawing.Point(12, 104);
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
            this.typeUnitCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeUnitCB.FormattingEnabled = true;
            this.typeUnitCB.Items.AddRange(new object[] {
            "Angle",
            "Area",
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
            this.invert_unit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            this.toLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            this.toTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toTB.Location = new System.Drawing.Point(12, 154);
            this.toTB.Name = "toTB";
            this.toTB.ReadOnly = true;
            this.toTB.Size = new System.Drawing.Size(330, 21);
            this.toTB.TabIndex = 12;
            this.toTB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // fromLB
            // 
            this.fromLB.AutoSize = true;
            this.fromLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromLB.Location = new System.Drawing.Point(12, 61);
            this.fromLB.Name = "fromLB";
            this.fromLB.Size = new System.Drawing.Size(31, 13);
            this.fromLB.TabIndex = 6;
            this.fromLB.Text = "From";
            this.fromLB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fromLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // fromTB
            // 
            this.fromTB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fromTB.Location = new System.Drawing.Point(12, 77);
            this.fromTB.MaxLength = 20;
            this.fromTB.Name = "fromTB";
            this.fromTB.Size = new System.Drawing.Size(330, 23);
            this.fromTB.TabIndex = 10;
            this.fromTB.Text = "Enter value";
            this.fromTB.TextChanged += new System.EventHandler(this.fromTB_TextChanged);
            this.fromTB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.fromTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.fromTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // datecalcGB
            // 
            this.datecalcGB.BackColor = System.Drawing.Color.White;
            this.datecalcGB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datecalcGB.Controls.Add(this.result1);
            this.datecalcGB.Controls.Add(this.result2);
            this.datecalcGB.Controls.Add(this.calmethodLB);
            this.datecalcGB.Controls.Add(this.secondDate);
            this.datecalcGB.Controls.Add(this.dtP2);
            this.datecalcGB.Controls.Add(this.subrb);
            this.datecalcGB.Controls.Add(this.dtP1);
            this.datecalcGB.Controls.Add(this.addrb);
            this.datecalcGB.Controls.Add(this.addSubResultLB);
            this.datecalcGB.Controls.Add(this.firstDate);
            this.datecalcGB.Controls.Add(this.autocal_date);
            this.datecalcGB.Controls.Add(this.dateDifferenceLB);
            this.datecalcGB.Controls.Add(this.calculate_date);
            this.datecalcGB.Controls.Add(this.yearAddSubLB);
            this.datecalcGB.Controls.Add(this.datemethodCB);
            this.datecalcGB.Controls.Add(this.monthAddSubLB);
            this.datecalcGB.Controls.Add(this.dayAddSubLB);
            this.datecalcGB.Controls.Add(this.periodsDateUD);
            this.datecalcGB.Controls.Add(this.periodsMonthUD);
            this.datecalcGB.Controls.Add(this.periodsYearUD);
            this.datecalcGB.Location = new System.Drawing.Point(236, 12);
            this.datecalcGB.Name = "datecalcGB";
            this.datecalcGB.Size = new System.Drawing.Size(356, 241);
            this.datecalcGB.TabIndex = 32;
            this.datecalcGB.Visible = false;
            this.datecalcGB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // result1
            // 
            this.result1.Location = new System.Drawing.Point(12, 108);
            this.result1.Name = "result1";
            this.result1.ReadOnly = true;
            this.result1.Size = new System.Drawing.Size(330, 21);
            this.result1.TabIndex = 208;
            this.result1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // result2
            // 
            this.result2.Location = new System.Drawing.Point(12, 159);
            this.result2.Name = "result2";
            this.result2.ReadOnly = true;
            this.result2.Size = new System.Drawing.Size(330, 21);
            this.result2.TabIndex = 209;
            this.result2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // calmethodLB
            // 
            this.calmethodLB.AutoSize = true;
            this.calmethodLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.calmethodLB.Location = new System.Drawing.Point(12, 8);
            this.calmethodLB.Name = "calmethodLB";
            this.calmethodLB.Size = new System.Drawing.Size(155, 13);
            this.calmethodLB.TabIndex = 74;
            this.calmethodLB.Text = "Select the date input you want";
            this.calmethodLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // secondDate
            // 
            this.secondDate.AutoSize = true;
            this.secondDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.secondDate.Location = new System.Drawing.Point(214, 67);
            this.secondDate.Name = "secondDate";
            this.secondDate.Size = new System.Drawing.Size(23, 13);
            this.secondDate.TabIndex = 60;
            this.secondDate.Text = "To:";
            this.secondDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // dtP2
            // 
            this.dtP2.Checked = false;
            this.dtP2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtP2.Location = new System.Drawing.Point(243, 63);
            this.dtP2.Name = "dtP2";
            this.dtP2.Size = new System.Drawing.Size(99, 21);
            this.dtP2.TabIndex = 202;
            this.dtP2.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // subrb
            // 
            this.subrb.AutoSize = true;
            this.subrb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.subrb.Location = new System.Drawing.Point(254, 64);
            this.subrb.Name = "subrb";
            this.subrb.Size = new System.Drawing.Size(66, 17);
            this.subrb.TabIndex = 202;
            this.subrb.Text = "Subtract";
            this.subrb.UseVisualStyleBackColor = true;
            this.subrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // dtP1
            // 
            this.dtP1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtP1.Location = new System.Drawing.Point(60, 63);
            this.dtP1.Name = "dtP1";
            this.dtP1.Size = new System.Drawing.Size(99, 21);
            this.dtP1.TabIndex = 201;
            this.dtP1.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // addrb
            // 
            this.addrb.AutoSize = true;
            this.addrb.Checked = true;
            this.addrb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.addrb.Location = new System.Drawing.Point(176, 64);
            this.addrb.Name = "addrb";
            this.addrb.Size = new System.Drawing.Size(44, 17);
            this.addrb.TabIndex = 202;
            this.addrb.TabStop = true;
            this.addrb.Text = "Add";
            this.addrb.UseVisualStyleBackColor = true;
            this.addrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // addSubResultLB
            // 
            this.addSubResultLB.AutoSize = true;
            this.addSubResultLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.addSubResultLB.Location = new System.Drawing.Point(12, 140);
            this.addSubResultLB.Name = "addSubResultLB";
            this.addSubResultLB.Size = new System.Drawing.Size(91, 13);
            this.addSubResultLB.TabIndex = 67;
            this.addSubResultLB.Text = "Difference (days)";
            this.addSubResultLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // firstDate
            // 
            this.firstDate.AutoSize = true;
            this.firstDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.firstDate.Location = new System.Drawing.Point(12, 67);
            this.firstDate.Name = "firstDate";
            this.firstDate.Size = new System.Drawing.Size(35, 13);
            this.firstDate.TabIndex = 59;
            this.firstDate.Text = "From:";
            this.firstDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // autocal_date
            // 
            this.autocal_date.AutoSize = true;
            this.autocal_date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.autocal_date.Location = new System.Drawing.Point(12, 205);
            this.autocal_date.Name = "autocal_date";
            this.autocal_date.Size = new System.Drawing.Size(96, 17);
            this.autocal_date.TabIndex = 210;
            this.autocal_date.Text = "A&uto Calculate";
            this.autocal_date.UseVisualStyleBackColor = true;
            this.autocal_date.CheckedChanged += new System.EventHandler(this.autocal_cb_CheckedChanged);
            this.autocal_date.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // dateDifferenceLB
            // 
            this.dateDifferenceLB.AutoSize = true;
            this.dateDifferenceLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateDifferenceLB.Location = new System.Drawing.Point(12, 90);
            this.dateDifferenceLB.Name = "dateDifferenceLB";
            this.dateDifferenceLB.Size = new System.Drawing.Size(204, 13);
            this.dateDifferenceLB.TabIndex = 64;
            this.dateDifferenceLB.Text = "Difference (years, months, weeks, days)";
            this.dateDifferenceLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            // 
            // calculate_date
            // 
            this.calculate_date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.calculate_date.Location = new System.Drawing.Point(253, 199);
            this.calculate_date.Name = "calculate_date";
            this.calculate_date.Size = new System.Drawing.Size(75, 27);
            this.calculate_date.TabIndex = 211;
            this.calculate_date.TabStop = false;
            this.calculate_date.Text = "&Calculate";
            this.calculate_date.UseVisualStyleBackColor = true;
            this.calculate_date.Click += new System.EventHandler(this.calculate_bt_Click);
            this.calculate_date.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // yearAddSubLB
            // 
            this.yearAddSubLB.AutoSize = true;
            this.yearAddSubLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.yearAddSubLB.Location = new System.Drawing.Point(12, 102);
            this.yearAddSubLB.Name = "yearAddSubLB";
            this.yearAddSubLB.Size = new System.Drawing.Size(33, 13);
            this.yearAddSubLB.TabIndex = 64;
            this.yearAddSubLB.Text = "Year:";
            // 
            // datemethodCB
            // 
            this.datemethodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datemethodCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.datemethodCB.FormattingEnabled = true;
            this.datemethodCB.Items.AddRange(new object[] {
            "Calculate the difference between two dates",
            "Add or subtract days to a specified date"});
            this.datemethodCB.Location = new System.Drawing.Point(12, 31);
            this.datemethodCB.Name = "datemethodCB";
            this.datemethodCB.Size = new System.Drawing.Size(330, 21);
            this.datemethodCB.TabIndex = 200;
            this.datemethodCB.SelectedIndexChanged += new System.EventHandler(this.cal_method_SelectedIndexChanged);
            this.datemethodCB.Enter += new System.EventHandler(this.DisableKeyboard);
            this.datemethodCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typeCB_MouseDown);
            // 
            // monthAddSubLB
            // 
            this.monthAddSubLB.AutoSize = true;
            this.monthAddSubLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.monthAddSubLB.Location = new System.Drawing.Point(118, 102);
            this.monthAddSubLB.Name = "monthAddSubLB";
            this.monthAddSubLB.Size = new System.Drawing.Size(41, 13);
            this.monthAddSubLB.TabIndex = 64;
            this.monthAddSubLB.Text = "Month:";
            // 
            // dayAddSubLB
            // 
            this.dayAddSubLB.AutoSize = true;
            this.dayAddSubLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dayAddSubLB.Location = new System.Drawing.Point(240, 102);
            this.dayAddSubLB.Name = "dayAddSubLB";
            this.dayAddSubLB.Size = new System.Drawing.Size(30, 13);
            this.dayAddSubLB.TabIndex = 64;
            this.dayAddSubLB.Text = "Day:";
            // 
            // periodsDateUD
            // 
            this.periodsDateUD.Location = new System.Drawing.Point(282, 100);
            this.periodsDateUD.Maximum = new decimal(new int[] {
            730000,
            0,
            0,
            0});
            this.periodsDateUD.Name = "periodsDateUD";
            this.periodsDateUD.Size = new System.Drawing.Size(60, 21);
            this.periodsDateUD.TabIndex = 205;
            this.periodsDateUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsDateUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsDateUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsMonthUD
            // 
            this.periodsMonthUD.Location = new System.Drawing.Point(169, 100);
            this.periodsMonthUD.Maximum = new decimal(new int[] {
            24000,
            0,
            0,
            0});
            this.periodsMonthUD.Name = "periodsMonthUD";
            this.periodsMonthUD.Size = new System.Drawing.Size(44, 21);
            this.periodsMonthUD.TabIndex = 204;
            this.periodsMonthUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsMonthUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsMonthUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsYearUD
            // 
            this.periodsYearUD.Location = new System.Drawing.Point(60, 100);
            this.periodsYearUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.periodsYearUD.Name = "periodsYearUD";
            this.periodsYearUD.Size = new System.Drawing.Size(44, 21);
            this.periodsYearUD.TabIndex = 203;
            this.periodsYearUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsYearUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsYearUD.Enter += new System.EventHandler(this.DisableKeyboard);
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
            // 
            // _byteRB
            // 
            this._byteRB.AutoSize = true;
            this._byteRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._byteRB.Location = new System.Drawing.Point(7, 67);
            this._byteRB.Name = "_byteRB";
            this._byteRB.Size = new System.Drawing.Size(47, 17);
            this._byteRB.TabIndex = 2;
            this._byteRB.Text = "Byte";
            this._byteRB.UseVisualStyleBackColor = true;
            this._byteRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this._byteRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._byteRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // qwordRB
            // 
            this.qwordRB.AutoSize = true;
            this.qwordRB.Checked = true;
            this.qwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qwordRB.Location = new System.Drawing.Point(7, 4);
            this.qwordRB.Name = "qwordRB";
            this.qwordRB.Size = new System.Drawing.Size(57, 17);
            this.qwordRB.TabIndex = 16;
            this.qwordRB.TabStop = true;
            this.qwordRB.Text = "Qword";
            this.qwordRB.UseVisualStyleBackColor = true;
            this.qwordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this.qwordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.qwordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // dwordRB
            // 
            this.dwordRB.AutoSize = true;
            this.dwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwordRB.Location = new System.Drawing.Point(7, 25);
            this.dwordRB.Name = "dwordRB";
            this.dwordRB.Size = new System.Drawing.Size(56, 17);
            this.dwordRB.TabIndex = 8;
            this.dwordRB.Text = "Dword";
            this.dwordRB.UseVisualStyleBackColor = true;
            this.dwordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this.dwordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.dwordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // _wordRB
            // 
            this._wordRB.AutoSize = true;
            this._wordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._wordRB.Location = new System.Drawing.Point(7, 46);
            this._wordRB.Name = "_wordRB";
            this._wordRB.Size = new System.Drawing.Size(51, 17);
            this._wordRB.TabIndex = 4;
            this._wordRB.Text = "Word";
            this._wordRB.UseVisualStyleBackColor = true;
            this._wordRB.CheckedChanged += new System.EventHandler(this.architectureRB_CheckedChanged);
            this._wordRB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this._wordRB.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // calc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(228)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(214, 282);
            this.Controls.Add(this.datecalcGB);
            this.Controls.Add(this.VhPN);
            this.Controls.Add(this.morgagePN);
            this.Controls.Add(this.feMPG_PN);
            this.Controls.Add(this.PNbinary);
            this.Controls.Add(this.basePN);
            this.Controls.Add(this.angleGB);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.screenPN);
            this.Controls.Add(this.bracketTime_lb);
            this.Controls.Add(this.fe_ChkBox);
            this.Controls.Add(this.inv_ChkBox);
            this.Controls.Add(this.btnF);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.btnD);
            this.Controls.Add(this.closeproBT);
            this.Controls.Add(this.XorBT);
            this.Controls.Add(this.NotBT);
            this.Controls.Add(this.AndBT);
            this.Controls.Add(this.btnE);
            this.Controls.Add(this.RshBT);
            this.Controls.Add(this.modproBT);
            this.Controls.Add(this.RoRBT);
            this.Controls.Add(this.openproBT);
            this.Controls.Add(this.LshBT);
            this.Controls.Add(this.or_BT);
            this.Controls.Add(this.RoLBT);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.close_bracket);
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
            this.Controls.Add(this.sqrt_bt);
            this.Controls.Add(this.invert_bt);
            this.Controls.Add(this.equal);
            this.Controls.Add(this.divbt);
            this.Controls.Add(this.mulbt);
            this.Controls.Add(this.minusbt);
            this.Controls.Add(this.addbt);
            this.Controls.Add(this.btdot);
            this.Controls.Add(this.num0BT);
            this.Controls.Add(this.clearbt);
            this.Controls.Add(this.mem_minus_bt);
            this.Controls.Add(this.mem_recall);
            this.Controls.Add(this.changesignBT);
            this.Controls.Add(this.madd);
            this.Controls.Add(this.num9BT);
            this.Controls.Add(this.mem_store);
            this.Controls.Add(this.ce);
            this.Controls.Add(this.num6BT);
            this.Controls.Add(this.num8BT);
            this.Controls.Add(this.mem_clear);
            this.Controls.Add(this.num3BT);
            this.Controls.Add(this.backspacebt);
            this.Controls.Add(this.num5BT);
            this.Controls.Add(this.num7BT);
            this.Controls.Add(this.num2BT);
            this.Controls.Add(this.num4BT);
            this.Controls.Add(this.num1BT);
            this.Controls.Add(this.percent_bt);
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
            this.Controls.Add(this.unitconvGB);
            this.Controls.Add(this.unknownPN);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Menu = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(220, 310);
            this.Name = "calc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.calc_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveFormWithoutMouseAtTitleBar);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            this.VhPN.ResumeLayout(false);
            this.VhPN.PerformLayout();
            this.morgagePN.ResumeLayout(false);
            this.morgagePN.PerformLayout();
            this.feMPG_PN.ResumeLayout(false);
            this.feMPG_PN.PerformLayout();
            this.basePN.ResumeLayout(false);
            this.basePN.PerformLayout();
            this.angleGB.ResumeLayout(false);
            this.angleGB.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            this.gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).EndInit();
            this.screenPN.ResumeLayout(false);
            this.screenPN.PerformLayout();
            this.unitconvGB.ResumeLayout(false);
            this.unitconvGB.PerformLayout();
            this.datecalcGB.ResumeLayout(false);
            this.datecalcGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).EndInit();
            this.unknownPN.ResumeLayout(false);
            this.unknownPN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region form generation code by user
        /// <summary>
        /// Sắp xếp lại các nút của form standard trở lại vị trí ban đầu
        /// </summary>
        private void initializedForm(bool isReturn0)
        {
            this.num1BT.Location = new Point(12, 194);
            this.num2BT.Location = new Point(51, 194);
            this.num3BT.Location = new Point(90, 194);
            this.num4BT.Location = new Point(12, 162);
            this.num5BT.Location = new Point(51, 162);
            this.num6BT.Location = new Point(90, 162);
            this.num7BT.Location = new Point(12, 130);
            this.num8BT.Location = new Point(51, 130);
            this.num9BT.Location = new Point(90, 130);
            this.num0BT.Location = new Point(12, 226);
            this.btdot.Location = new Point(90, 226);
            this.addbt.Location = new Point(129, 226);
            this.minusbt.Location = new Point(129, 194);
            this.mulbt.Location = new Point(129, 162);
            this.divbt.Location = new Point(129, 130);
            this.equal.Location = new Point(168, 194);
            this.invert_bt.Location = new Point(168, 162);
            this.percent_bt.Location = new Point(168, 130);
            this.backspacebt.Location = new Point(12, 98);
            this.ce.Location = new Point(51, 98);
            this.changesignBT.Location = new Point(129, 98);
            this.mem_clear.Location = new Point(12, 66);
            this.mem_store.Location = new Point(51, 66);
            this.mem_recall.Location = new Point(90, 66);
            this.sqrt_bt.Location = new Point(168, 98);
            this.clearbt.Location = new Point(90, 98);
            this.madd.Location = new Point(129, 66);
            this.mem_minus_bt.Location = new Point(168, 66);
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
            this.close_bracket.Location = new Point(168, 98);
            this.btFactorial.Location = new Point(168, 130);
            this._10x_bt.Location = new Point(168, 226);
            this._3vx_bt.Location = new Point(168, 194);
            this.nvx_bt.Location = new Point(168, 162);

            this.open_bracket.Location = new Point(129, 98);
            this.xn_bt.Location = new Point(129, 162);
            this.log_bt.Location = new Point(129, 226);
            this.x3_bt.Location = new Point(129, 194);
            this.x2_bt.Location = new Point(129, 130);

            this.modsciBT.Location = new Point(90, 226);
            this.ln_bt.Location = new Point(90, 98);
            this.tan_bt.Location = new Point(90, 194);
            this.cos_bt.Location = new Point(90, 162);
            this.sin_bt.Location = new Point(90, 130);

            this.sqrt_bt.Location = new Point(363, 98);
            this.percent_bt.Location = new Point(363, 130);
            this.invert_bt.Location = new Point(363, 162);
            this.equal.Location = new Point(363, 194);
            this.mem_minus_bt.Location = new Point(363, 65);

            this.divbt.Location = new Point(324, 130);
            this.mulbt.Location = new Point(324, 162);
            this.minusbt.Location = new Point(324, 194);
            this.addbt.Location = new Point(324, 226);
            this.changesignBT.Location = new Point(324, 98);
            this.madd.Location = new Point(324, 65);

            this.btdot.Location = new Point(285, 226);
            this.num9BT.Location = new Point(285, 130);
            this.mem_store.Location = new Point(285, 65);
            this.clearbt.Location = new Point(285, 98);
            this.num6BT.Location = new Point(285, 162);
            this.num3BT.Location = new Point(285, 194);

            this.mem_recall.Location = new Point(246, 65);
            this.ce.Location = new Point(246, 98);
            this.num8BT.Location = new Point(246, 130);
            this.num5BT.Location = new Point(246, 162);
            this.num2BT.Location = new Point(246, 194);

            this.mem_clear.Location = new Point(207, 65);
            this.backspacebt.Location = new Point(207, 98);
            this.num7BT.Location = new Point(207, 130);
            this.num4BT.Location = new Point(207, 162);
            this.num1BT.Location = new Point(207, 194);
            this.num0BT.Location = new Point(207, 226);

            this.exp_bt.Location = new Point(51, 226);
            this.sinh_bt.Location = new Point(51, 130);
            this.cosh_bt.Location = new Point(51, 162);
            this.tanh_bt.Location = new Point(51, 194);
            this.inv_ChkBox.Location = new Point(51, 98);

            this.int_bt.Location = new Point(12, 130);
            this.dms_bt.Location = new Point(12, 162);
            this.pi_bt.Location = new Point(12, 194);
            this.fe_ChkBox.Location = new Point(12, 226);
            this.angleGB.Location = new Point(12, 66);
            this.bracketTime_lb.Location = new Point(12, 98);

            if (isReturn0) this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);
            //
            // screenPN
            //
            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47);

            hideSciComponent(true);
            //
            // Calculator
            //
            //this.Size = new Size(447, 340);
        }
        /// <summary>
        /// gọi các property ẩn của các control
        /// </summary>
        private void HiddenProperties()
        {
            num1BT.ContextMenu = helpCTMN;
            num2BT.ContextMenu = helpCTMN;
            num1BT.ContextMenu = helpCTMN;
            num2BT.ContextMenu = helpCTMN;
            num3BT.ContextMenu = helpCTMN;
            num4BT.ContextMenu = helpCTMN;
            num5BT.ContextMenu = helpCTMN;
            num6BT.ContextMenu = helpCTMN;
            num7BT.ContextMenu = helpCTMN;
            num8BT.ContextMenu = helpCTMN;
            num9BT.ContextMenu = helpCTMN;
            num0BT.ContextMenu = helpCTMN;
            btdot.ContextMenu = helpCTMN;
            addbt.ContextMenu = helpCTMN;
            mulbt.ContextMenu = helpCTMN;
            divbt.ContextMenu = helpCTMN;
            minusbt.ContextMenu = helpCTMN;
            equal.ContextMenu = helpCTMN;
            invert_bt.ContextMenu = helpCTMN;
            madd.ContextMenu = helpCTMN;
            mem_minus_bt.ContextMenu = helpCTMN;
            mem_clear.ContextMenu = helpCTMN;
            mem_recall.ContextMenu = helpCTMN;
            percent_bt.ContextMenu = helpCTMN;
            sqrt_bt.ContextMenu = helpCTMN;
            mem_store.ContextMenu = helpCTMN;
            changesignBT.ContextMenu = helpCTMN;
            clearbt.ContextMenu = helpCTMN;
            ce.ContextMenu = helpCTMN;
            backspacebt.ContextMenu = helpCTMN;
            //-----------------------------------------------------------------
            open_bracket.ContextMenu = helpCTMN;
            close_bracket.ContextMenu = helpCTMN;
            nvx_bt.ContextMenu = helpCTMN;
            btFactorial.ContextMenu = helpCTMN;
            _3vx_bt.ContextMenu = helpCTMN;
            _10x_bt.ContextMenu = helpCTMN;
            x2_bt.ContextMenu = helpCTMN;
            xn_bt.ContextMenu = helpCTMN;
            x3_bt.ContextMenu = helpCTMN;
            log_bt.ContextMenu = helpCTMN;
            sinh_bt.ContextMenu = helpCTMN;
            cosh_bt.ContextMenu = helpCTMN;
            tanh_bt.ContextMenu = helpCTMN;
            dms_bt.ContextMenu = helpCTMN;
            pi_bt.ContextMenu = helpCTMN;
            inv_ChkBox.ContextMenu = helpCTMN;
            int_bt.ContextMenu = helpCTMN;
            fe_ChkBox.ContextMenu = helpCTMN;
            sin_bt.ContextMenu = helpCTMN;
            cos_bt.ContextMenu = helpCTMN;
            tan_bt.ContextMenu = helpCTMN;
            ln_bt.ContextMenu = helpCTMN;
            exp_bt.ContextMenu = helpCTMN;
            degRB.ContextMenu = helpCTMN;
            radRB.ContextMenu = helpCTMN;
            graRB.ContextMenu = helpCTMN;
            bracketTime_lb.ContextMenu = helpCTMN;
            //-----------------------------------------------------------------
            RoLBT.ContextMenu = helpCTMN;
            or_BT.ContextMenu = helpCTMN;
            LshBT.ContextMenu = helpCTMN;
            modsciBT.ContextMenu = helpCTMN;
            RoRBT.ContextMenu = helpCTMN;
            RshBT.ContextMenu = helpCTMN;
            open_bracket.ContextMenu = helpCTMN;
            close_bracket.ContextMenu = helpCTMN;
            NotBT.ContextMenu = helpCTMN;
            AndBT.ContextMenu = helpCTMN;
            XorBT.ContextMenu = helpCTMN;
            modproBT.ContextMenu = helpCTMN;
            openproBT.ContextMenu = helpCTMN;
            closeproBT.ContextMenu = helpCTMN;
            btnA.ContextMenu = helpCTMN;
            btnB.ContextMenu = helpCTMN;
            btnC.ContextMenu = helpCTMN;
            btnD.ContextMenu = helpCTMN;
            btnE.ContextMenu = helpCTMN;
            btnF.ContextMenu = helpCTMN;
            qwordRB.ContextMenu = helpCTMN;
            dwordRB.ContextMenu = helpCTMN;
            _wordRB.ContextMenu = helpCTMN;
            _byteRB.ContextMenu = helpCTMN;
            //-----------------------------------------------------------------
            AddstaBT.ContextMenu = helpCTMN;
            CAD.ContextMenu = helpCTMN;
            sigmax2BT.ContextMenu = helpCTMN;
            sigmaxBT.ContextMenu = helpCTMN;
            sigman_1BT.ContextMenu = helpCTMN;
            sigmanBT.ContextMenu = helpCTMN;
            xcross.ContextMenu = helpCTMN;
            x2cross.ContextMenu = helpCTMN;
            //-----------------------------------------------------------------

            screenPN.ContextMenu = gridPanel.ContextMenu = mainContextMenu;
            //hideStdComponent(true);
            //hideSciComponent(scientificMI.Checked);
            //hideProComponent(programmerMI.Checked);
            //hideStaComponent(statisticsMI.Checked);

            configPath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\calc.ini";
            copyMI.Text = "&Copy\tCtrl+C";
            pasteMI.Text = "&Paste\tCtrl+V";
            cancelEditHisMI.Text = "Ca&ncel edit\tEsc";
            reCalculateMI.Text = "Re&calculate\t=";

            cancelEditDSMI.Text = "Ca&ncel edit\tEsc";
            commitDSMI.Text = "C&ommit\t=";

            clearDatasetMI.Text = "C&lear\tD";
            parser.Transfer += new Parser.SendValue(parser_Transfer);
        }

        private void parser_Transfer(int sentValue, int count, int total)
        {
            co.count = count;
            co.total = total;
            co.Progress = sentValue;
        }
        /// <summary>
        /// khởi tạo mảng các textbox trong hàm chức năng fuel economy, ...
        /// </summary>
        private void InitCustomControls(ref ITextBox[] itb, IPanel ipn, int capacity)
        {
            itb = new ITextBox[capacity];
            for (int i = 0; i < capacity; i++)
            {
                InitTextBoxesProperties(itb, i);
            }

            ipn.Controls.AddRange(itb);
        }

        private void InitTextBoxesProperties(ITextBox[] ilb, int index)
        {
            ilb[index] = new ITextBox();
            ilb[index].Text = "Enter value";
            ilb[index].Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            ilb[index].ForeColor = SystemColors.GrayText;
            ilb[index].Location = new Point(177, 59 + 24 * index);
            ilb[index].Size = new Size(165, 21);
            ilb[index].MaxLength = 20;
            ilb[index].NumberModeOnly = true;
            ilb[index].TabIndex = 10 + index;
            ilb[index].Enter += new EventHandler(DisableKeyboard);
        }

        private void InitCancelOperation()
        {
            if (co == null)
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
                    k = 8 * i + j;  // k = 8j + index
                    bin_digit[k] = new ILabel();
                    PNbinary.Controls.Add(bin_digit[k]);
                    bin_digit[k].Text = "0000";
                    bin_digit[k].TabIndex = k;
                    bin_digit[k].Font = new Font("Consolas", 9F);
                    bin_digit[k].Size = new Size(35, 15);
                    //bin_digit[k].Location = new Point(374 - 53 * index, 34 - 32 * index);
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
        /// <summary>
        /// set lại location cho các control của form standard có history
        /// </summary>
        private void stdWithHistory()
        {
            this.sqrt_bt.Location = new Point(168, 202);
            this.clearbt.Location = new Point(90, 202);
            this.changesignBT.Location = new Point(129, 202);
            this.ce.Location = new Point(51, 202);
            this.backspacebt.Location = new Point(12, 202);

            this.invert_bt.Location = new Point(168, 266);
            this.mulbt.Location = new Point(129, 266);
            this.num6BT.Location = new Point(90, 266);
            this.num5BT.Location = new Point(51, 266);
            this.num4BT.Location = new Point(12, 266);

            this.equal.Location = new Point(168, 298);
            this.minusbt.Location = new Point(129, 298);
            this.num3BT.Location = new Point(90, 298);
            this.num2BT.Location = new Point(51, 298);
            this.num1BT.Location = new Point(12, 298);

            this.addbt.Location = new Point(129, 330);
            this.btdot.Location = new Point(90, 330);
            this.num0BT.Location = new Point(12, 330);

            this.divbt.Location = new Point(129, 234);
            this.num9BT.Location = new Point(90, 234);
            this.num8BT.Location = new Point(51, 234);
            this.num7BT.Location = new Point(12, 234);
            this.percent_bt.Location = new Point(168, 234);

            this.mem_store.Location = new Point(51, 170);
            this.mem_minus_bt.Location = new Point(168, 170);
            this.mem_recall.Location = new Point(90, 170);
            this.madd.Location = new Point(129, 170);
            this.mem_clear.Location = new Point(12, 170);

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
            this._10x_bt.Location = new Point(168, 330);
            this._3vx_bt.Location = new Point(168, 298);
            this.nvx_bt.Location = new Point(168, 265);
            this.btFactorial.Location = new Point(168, 234);
            this.close_bracket.Location = new Point(168, 202);

            this.log_bt.Location = new Point(129, 330);
            this.x3_bt.Location = new Point(129, 298);
            this.xn_bt.Location = new Point(129, 265);
            this.x2_bt.Location = new Point(129, 234);
            this.open_bracket.Location = new Point(129, 202);

            this.fe_ChkBox.Location = new Point(12, 330);
            this.pi_bt.Location = new Point(12, 298);
            this.dms_bt.Location = new Point(12, 265);
            this.int_bt.Location = new Point(12, 234);
            this.bracketTime_lb.Location = new Point(12, 202);
            this.angleGB.Location = new Point(12, 170);

            this.modsciBT.Location = new Point(90, 330);
            this.tan_bt.Location = new Point(90, 298);
            this.cos_bt.Location = new Point(90, 265);
            this.sin_bt.Location = new Point(90, 234);
            this.ln_bt.Location = new Point(90, 202);

            this.exp_bt.Location = new Point(51, 330);
            this.tanh_bt.Location = new Point(51, 298);
            this.cosh_bt.Location = new Point(51, 265);
            this.sinh_bt.Location = new Point(51, 234);
            this.inv_ChkBox.Location = new Point(51, 202);

            this.equal.Location = new Point(363, 298);
            this.invert_bt.Location = new Point(363, 265);
            this.percent_bt.Location = new Point(363, 234);
            this.sqrt_bt.Location = new Point(363, 202);
            this.mem_minus_bt.Location = new Point(363, 170);

            this.addbt.Location = new Point(324, 330);
            this.minusbt.Location = new Point(324, 298);
            this.mulbt.Location = new Point(324, 265);
            this.divbt.Location = new Point(324, 234);
            this.changesignBT.Location = new Point(324, 202);
            this.madd.Location = new Point(324, 170);

            this.btdot.Location = new Point(285, 330);
            this.num3BT.Location = new Point(285, 298);
            this.num6BT.Location = new Point(285, 265);
            this.num9BT.Location = new Point(285, 234);
            this.clearbt.Location = new Point(285, 202);
            this.mem_store.Location = new Point(285, 170);

            this.num0BT.Location = new Point(207, 330);
            this.num1BT.Location = new Point(207, 298);
            this.num4BT.Location = new Point(207, 265);
            this.num7BT.Location = new Point(207, 234);
            this.backspacebt.Location = new Point(207, 202);
            this.mem_clear.Location = new Point(207, 170);

            this.num2BT.Location = new Point(246, 298);
            this.num5BT.Location = new Point(246, 265);
            this.num8BT.Location = new Point(246, 234);
            this.ce.Location = new Point(246, 202);
            this.mem_recall.Location = new Point(246, 170);
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

            hideSciComponent(true);

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
        private void programmerMode()
        {
            this.equal.Location = new Point(363, 257);
            this.divbt.Location = new Point(324, 193);
            this.mulbt.Location = new Point(324, 225);
            this.minusbt.Location = new Point(324, 257);
            this.addbt.Location = new Point(324, 289);
            this.clearbt.Location = new Point(285, 161);
            this.mem_minus_bt.Location = new Point(363, 129);
            this.mem_recall.Location = new Point(246, 129);
            this.changesignBT.Location = new Point(324, 161);
            this.madd.Location = new Point(324, 129);
            this.mem_store.Location = new Point(285, 129);
            this.mem_clear.Location = new Point(207, 129);
            this.ce.Location = new Point(246, 161);
            this.backspacebt.Location = new Point(207, 161);
            this.RoLBT.Location = new Point(90, 193);
            this.or_BT.Location = new Point(90, 225);
            this.LshBT.Location = new Point(90, 257);
            this.RoRBT.Location = new Point(129, 193);
            this.RshBT.Location = new Point(129, 257);
            this.basePN.Location = new Point(12, 129);
            this.open_bracket.Location = new Point(90, 161);
            this.close_bracket.Location = new Point(129, 161);
            this.bracketTime_lb.Location = new Point(90, 129);
            this.NotBT.Location = new Point(90, 289);
            this.unknownPN.Location = new Point(12, 225);
            this.AndBT.Location = new Point(129, 289);
            this.XorBT.Location = new Point(129, 225);
            this.PNbinary.Location = new Point(12, 64);
            this.modproBT.Location = new Point(129, 129);
            this.openproBT.Location = new Point(90, 161);
            this.closeproBT.Location = new Point(129, 161);
            this.num0BT.Location = new Point(207, 289);
            this.num1BT.Location = new Point(207, 257);
            this.num2BT.Location = new Point(246, 257);
            this.num3BT.Location = new Point(285, 257);
            this.num4BT.Location = new Point(207, 225);
            this.num5BT.Location = new Point(246, 225);
            this.num6BT.Location = new Point(285, 225);
            this.num7BT.Location = new Point(207, 193);
            this.num8BT.Location = new Point(246, 193);
            this.num9BT.Location = new Point(285, 193);
            this.btnA.Location = new Point(168, 129);
            this.btnB.Location = new Point(168, 161);
            this.btnC.Location = new Point(168, 193);
            this.btnD.Location = new Point(168, 225);
            this.btnE.Location = new Point(168, 257);
            this.btnF.Location = new Point(168, 289);

            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47);

            this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);

            this.sqrt_bt.Enabled = false;
            this.sqrt_bt.Location = new Point(363, 161);

            this.percent_bt.Location = new Point(363, 193);

            this.invert_bt.Enabled = false;
            this.invert_bt.Location = new Point(363, 225);

            this.btdot.Enabled = false;
            this.btdot.Location = new Point(285, 289);

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
            //this.Expsta_bt.Location = new Point(168, 202);
            this.exp_bt.Location = new Point(168, 202);
            this.clearbt.Location = new Point(90, 202);
            this.fe_ChkBox.Location = new Point(129, 202);
            this.CAD.Location = new Point(51, 202);
            this.backspacebt.Location = new Point(12, 202);

            this.sigmax2BT.Location = new Point(168, 266);
            this.sigmaxBT.Location = new Point(129, 266);
            this.num6BT.Location = new Point(90, 266);
            this.num5BT.Location = new Point(51, 266);
            this.num4BT.Location = new Point(12, 266);

            this.sigman_1BT.Location = new Point(168, 298);
            this.sigmanBT.Location = new Point(129, 298);
            this.num3BT.Location = new Point(90, 298);
            this.num2BT.Location = new Point(51, 298);
            this.num1BT.Location = new Point(12, 298);

            this.btdot.Location = new Point(90, 330);
            this.num0BT.Location = new Point(12, 330);
            this.AddstaBT.Location = new Point(168, 330);
            this.changesignBT.Location = new Point(129, 330);

            this.xcross.Location = new Point(129, 234);
            this.num9BT.Location = new Point(90, 234);
            this.num8BT.Location = new Point(51, 234);
            this.num7BT.Location = new Point(12, 234);
            this.x2cross.Location = new Point(168, 234);

            this.mem_store.Location = new Point(51, 170);
            this.mem_minus_bt.Location = new Point(168, 170);
            this.mem_recall.Location = new Point(90, 170);
            this.madd.Location = new Point(129, 170);
            this.mem_clear.Location = new Point(12, 170);

            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);

            this.gridPanel.Location = new Point(12, 12);
            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);
            this.staDGV.Size = new Size(190, 85);
            this.gridPanel.Size = new Size(190, 105);

            //this.staDGV.CurrentCell = null;
        }

        #endregion

        #region mousedown event
        Point location = new Point();

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            infoControl = sender as Control;
            if (infoControl is RadioButton)
            {
                location.X = this.Location.X + infoControl.Parent.Location.X + infoControl.Location.X + 4;
                location.Y = this.Location.Y + infoControl.Parent.Location.Y + infoControl.Location.Y + infoControl.Size.Height + 50;
            }
            else //if (infoControl is Button || infoControl is Label)
            {
                location.X = this.Location.X + infoControl.Location.X + 4;
                location.Y = this.Location.Y + infoControl.Location.Y + infoControl.Size.Height + 50;
            }
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            screenPN.Focus();
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
            DisableKeyboard(null, null);
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi
        /// thuộc tính focus của 2 textbox trong form unit conversion, hàm bình thường (nhập số trên form)
        /// </summary>
        private void EnableKeyboardAndChangeFocus()
        {
            prcmdkey = !hisDGV.IsCurrentCellInEditMode || !staDGV.IsCurrentCellInEditMode;
            fromTB_LostFocus(null, null);
            if (fromTB.Focused || toTB.Focused || typeUnitCB.Focused) toLB.Focus();
            if (datemethodCB.Focused || result2.Focused || result1.Focused
                || periodsDateUD.Focused || dtP1.Focused || dtP2.Focused)
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

            if (fempgTB != null)
                if (fempgTB[0].Focused || fempgTB[1].Focused)
                {
                    screenPN.Focus();
                }

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
        private Button btdot;
        private Button addbt;
        private Button minusbt;
        private Button mulbt;
        private Button divbt;
        private Button equal;
        private Label mem_lb;
        private Label scr_lb;
        private ILabel expressionTB;
        private Button invert_bt;
        private Button percent_bt;
        private Button backspacebt;
        private Button ce;
        private Button changesignBT;
        private Button mem_clear;
        private Button mem_store;
        private Button mem_recall;
        private Button sqrt_bt;
        private Button clearbt;
        private Button madd;
        private Button mem_minus_bt;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private IPanel screenPN;
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
        private RadioButton degRB;
        private RadioButton radRB;
        private RadioButton graRB;
        private CheckBox inv_ChkBox;
        private CheckBox fe_ChkBox;
        private IPanel angleGB;
        private IPanel gridPanel;
        private IDataGridView hisDGV;
        private DataGridViewTextBoxColumn Column1;
        //------------------------------------------------------------------
        private Button calculate_date;
        private CheckBox autocal_date;
        private ComboBox datemethodCB;
        private DateTimePicker dtP2;
        private INumericUpDown periodsYearUD;
        private INumericUpDown periodsMonthUD;
        private INumericUpDown periodsDateUD;
        private IPanel datecalcGB;
        private DateTimePicker dtP1;
        private Label addSubResultLB;
        private Label calmethodLB;
        private Label dateDifferenceLB;
        private Label dayAddSubLB;
        private Label firstDate;
        private Label monthAddSubLB;
        private Label secondDate;
        private Label yearAddSubLB;
        private RadioButton subrb;
        private RadioButton addrb;
        private TextBox result1;
        private TextBox result2;
        //------------------------------------------------------------------
        private Label typeUnitLB;
        private Label fromLB;
        private Label toLB;
        private TextBox fromTB;
        private TextBox toTB;
        private ComboBox typeUnitCB;
        private ComboBox fromCB;
        private ComboBox toCombobox;
        private Button invert_unit;
        private Button dnBT;
        private Button upBT;
        private IPanel unitconvGB;
        //------------------------------------------------------------------
        private MainMenu menuStrip1;
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
        private MenuItem preferrencesMI;
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
        //------------------------------------------------------------------
        private RadioButton qwordRB;
        private RadioButton dwordRB;
        private RadioButton _wordRB;
        private RadioButton _byteRB;
        private IRadioButton hexRB;
        private IRadioButton decRB;
        private IRadioButton octRB;
        private IRadioButton binRB;
        private IPanel PNbinary;
        private IPanel basePN;
        private IPanel unknownPN;
        private Button btnA;
        private Button btnB;
        private Button btnC;
        private Button btnD;
        private Button btnE;
        private Button btnF;
        private Button XorBT;
        private Button NotBT;
        private Button AndBT;
        private Button RshBT;
        private Button RoRBT;
        private Button LshBT;
        private Button or_BT;
        private Button RoLBT;
        private Button openproBT;
        private Button modproBT;
        private Button closeproBT;
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
        private IPanel feMPG_PN;
        private Label typeFEmpgLB;
        private ComboBox typeFECB1;
        private ComboBox typeFECB2;
        private Label fempgLB2;
        private Label fempgLB1;
        private TextBox fempgResultTB;
        private Button fuelEconomyBT;
        private ITextBox[] fempgTB;
        //------------------------------------------------------------------
        private RadioButton radioButton1;
        private BackgroundWorker mWorker;
        //------------------------------------------------------------------
        private IPanel morgagePN;
        private Label typeMorgageLB;
        private ComboBox typeMorgageCB;
        private Label morgageLB1;
        private Label morgageLB2;
        private Label morgageLB3;
        private Label morgageLB4;
        private Button morgageBT;
        private TextBox morgageResultTB;
        private ITextBox[] morgageTB;
        //------------------------------------------------------------------
        private IPanel VhPN;
        private Label typeVhLB;
        private ComboBox typeVhCB;
        private Label VhLB3;
        private Label VhLB1;
        private Label VhLB4;
        private Button VhBT;
        private Label VhLB2;
        private TextBox VhResultTB;
        private Label VhLB5;
        private ITextBox[] VhTB;
        #endregion
    }
}