using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    partial class calc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private string systempath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME") + "\\calc.ini"
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\calc.ini";

        #region dll import
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        IntPtr nextClipboardViewer;
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
        /// process a command key
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int key_hc = keyData.GetHashCode(); // key_hc = key hashcode
            if (isMathError && key_hc == 27)    // esc
            {
                isMathError = false;
                if (!hisDGV.IsCurrentCellInEditMode || !staDGV.IsCurrentCellInEditMode) clear_num(true);
                return base.ProcessCmdKey(ref msg, keyData);
            }
            if (isMathError && key_hc == 32)    // space
            {
                return true;
            }
            if (prcmdkey && !isMathError)
            {
                // ctrl W
                if (key_hc == 131159)/*&& isMathError*/ Close();
                if ((!hisDGV.IsCurrentCellInEditMode || !staDGV.IsCurrentCellInEditMode) && key_hc == 27) clear_num(true);
                switch (key_hc)
                {
                    #region xu ly phim nhap vao
                    //cac phim so tu 0 den 9
                    case 096: case 48:
                        if (programmerMI.Checked) numinput_pro(0);
                        else numinput(0);
                        break;
                    case 097: case 49:
                        if (programmerMI.Checked) numinput_pro(1);
                        else numinput(1);
                        break;
                    case 98: case 50:
                        if (programmerMI.Checked)
                        {
                            if (num2.Enabled) numinput_pro(2);
                        }
                        else numinput(2);
                        break;
                    case 99: case 51:
                        if (programmerMI.Checked)
                        {
                            if (num3.Enabled) numinput_pro(3);
                        }
                        else numinput(3);
                        break;
                    case 100: case 52:
                        if (programmerMI.Checked)
                        {
                            if (num4.Enabled) numinput_pro(4);
                        }
                        else numinput(4);
                        break;
                    case 101: case 53:
                        if (programmerMI.Checked)
                        {
                            if (num5.Enabled) numinput_pro(5);
                        }
                        else numinput(5);
                        break;
                    case 102: case 54:
                        if (programmerMI.Checked)
                        {
                            if (num6.Enabled) numinput_pro(6);
                        }
                        else numinput(6);
                        break;
                    case 103: case 55:
                        if (programmerMI.Checked)
                        {
                            if (num7.Enabled) numinput_pro(7);
                        }
                        else numinput(7);
                        break;
                    case 104: case 56:
                        if (programmerMI.Checked)
                        {
                            if (num8.Enabled) numinput_pro(8);
                        }
                        else numinput(8);
                        break;
                    case 105: case 57:
                        if (programmerMI.Checked)
                        {
                            if (num9.Enabled) numinput_pro(9);
                        }
                        else numinput(9);
                        break;
                    case 187: case 13:      // = hoac enter
                        if (statisticsMI.Checked) AddstaBT_Click(null, null);
                        else equal_Click(null, null);
                        return true;        //break;
                    case 110: case 190:   // .
                        if (btdot.Enabled) numinput(10);
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
                        else
                        {
                            if (staDGV.CurrentCell != null)
                            {
                                staDGV.Rows.RemoveAt(staDGV.CurrentCell.RowIndex);
                                countlb.Text = string.Format("Count = {0}", staDGV.RowCount);
                                rowIndex = staDGV.CurrentCell.RowIndex;
                            }
                        }
                        break;
                    case 114:       // F3
                        if (scientificMI.Checked) deg_rb.Checked = true;
                        if (programmerMI.Checked) _wordRB.Checked = true;
                        break;
                    case 115:       // F4
                        if (scientificMI.Checked) rad_rb.Checked = true;
                        if (programmerMI.Checked) _byteRB.Checked = true;
                        break;
                    case 116:       // F5
                        if (scientificMI.Checked) gra_rb.Checked = true;
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
                        if (programmerMI.Checked) numinput_pro(11);
                        else numinput(11);
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
                        if (btnE.Enabled && programmerMI.Checked) buttonAF(btnE.Text);
                        break;
                    case 70:        // F
                        if (btnF.Enabled && programmerMI.Checked) buttonAF(btnF.Text);
                        break;
                    case 073:       // I
                        if (scientificMI.Checked) inv_ChkBox.Checked = !inv_ChkBox.Checked;
                        break;
                    case 074:       // J
                        if (statisticsMI.Checked) rotateBT_Click(RoLBT, null);
                        break;
                    case 075:       // K
                        if (statisticsMI.Checked) rotateBT_Click(RoRBT, null);
                        break;
                    case 077:       // M dms
                        if (scientificMI.Checked) math_func(dms_bt);
                        break;
                    case 78:        // N ln()
                        if (scientificMI.Checked) math_func(ln_bt);
                        break;
                    case 079:       // O cos
                        if (scientificMI.Checked) math_func(cos_bt);
                        break;
                    case 080:       // P pi
                        if (scientificMI.Checked) pi_bt_Click(null, null);
                        break;
                    case 081:       // Q x^2
                        if (scientificMI.Checked) math_func(x2_bt);
                        break;
                    case 082:       // R
                        if (invert_bt.Enabled) math_func(invert_bt);
                        break;
                    case 083:       // S
                        if (scientificMI.Checked) math_func(sin_bt);
                        if (statisticsMI.Checked) sigmanBT_Click(null, null);
                        break;
                    case 084:       // T
                        if (invert_bt.Enabled && scientificMI.Checked) math_func(tan_bt);
                        break;
                    case 086:       // V
                        if (scientificMI.Checked) fe_ChkBox.Checked = !fe_ChkBox.Checked;
                        break;
                    case 088:       // X
                        //if (scientificMI.Checked) ; // nut exp
                        break;
                    case 089:       // Y
                        if (scientificMI.Checked) sci_operation(30);
                        break;
                    case 008:       // Backspace
                        backspace_Click(null, null);
                        break;
                    case 65593:     // (
                        if (scientificMI.Checked) open_bracket_Click(null, null);
                        break;
                    case 65589:     // %
                        if (percent_bt.Enabled) percent_Click(null, null);
                        if (programmerMI.Checked) modproBT_Click(null, null);
                        break;
                    case 65728:     // ~ not
                        if (programmerMI.Checked) notBT_Click(null, null);
                        break;
                    case 65584:     // )
                        if (scientificMI.Checked) close_bracket_Click(null, null);
                        break;
                    case 65585:     // !
                        if (scientificMI.Checked) math_func(btFactorial);    //n!
                        break;
                    case 65724: case 65726:// < >
                        if (programmerMI.Checked) bitWiseOperators();    //Lsh , Rsh
                        break;
                    case 65590: case 65591: case 65756: // ^&|
                        if (programmerMI.Checked)
                        {
                            if (key_hc == 65590) bitOperatorsBT_Click(XorBT, null);
                            if (key_hc == 65591) bitOperatorsBT_Click(AndBT, null);
                            if (key_hc == 65756) bitOperatorsBT_Click(or_BT, null);
                        }
                        break;
                    case 65586:     // sqrt
                        if (scientificMI.Checked || standardMI.Checked) math_func(sqrt_bt);
                        break;
                    // cac to hop phim
                    case 131137:    // ctrl A
                        if (statisticsMI.Checked) x2cross_Click(null, null);
                        break;
                    case 131138:    // ctrl B
                        if (scientificMI.Enabled) math_func(_3vx_bt); // 3x
                        break;
                    case 131143:    // ctrl G
                        if (scientificMI.Enabled) math_func(_10x_bt); // 10^x
                        break;
                    case 131148:    // ctrl L
                        clearMemory();  //MC
                        break;
                    case 131149:    // ctrl M
                        mem_process(3); //MS
                        break;
                    case 131151:    // ctrl O
                        math_func(cosh_bt);  // cosh
                        break;
                    case 131152:    // ctrl P
                        mem_process(1); //M+
                        break;
                    case 131153:    // ctrl Q
                        mem_process(2); //M-
                        break;
                    case 131154:    // ctrl R
                        recallMemory();
                        break;
                    case 131155:    // ctrl S
                        if (scientificMI.Checked) math_func(sinh_bt);  // sinh
                        if (statisticsMI.Checked) sigmax2BT_Click(null, null);
                        break;
                    case 131156:    // ctrl T
                        math_func(tanh_bt);  // tanh
                        break;
                    //case 131158:    // ctrl V
                    //    if (pasteMI.Enabled) pasteCTMN_Click(null, null);
                    //    break;
                    case 131161:    // ctrl Y
                        sci_operation(34);  // yx
                        break;
                    #endregion
                }
            }
            else
            {
                if ((hisDGV.IsCurrentCellInEditMode || staDGV.IsCurrentCellInEditMode) && (key_hc == 131139 || key_hc == 131158))
                {
                    return false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
            //return prcmdkey;
        }
        /// <summary>
        /// clipboard monitor
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x30D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    try
                    {
                        getPaste();
                    }
                    catch { }
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (propertiesChange)
            {
                string content = @"[calc]
CalculatorType=
DigitGrouping=
ExtraFunction=
History=
MemoryNumber=
------------------------------------------------
[UnitConversion]
TypeCB=
------------------------------------------------
[DateCalculation]
AutoCalculate=
Method=
------------------------------------------------";
                int read = readFromFile("CalculatorType");
                if (standardMI.Checked) content = content.Replace("CalculatorType=", "CalculatorType=0");
                if (scientificMI.Checked) content = content.Replace("CalculatorType=", "CalculatorType=1");
                if (programmerMI.Checked) content = content.Replace("CalculatorType=", "CalculatorType=2");
                if (statisticsMI.Checked) content = content.Replace("CalculatorType=", "CalculatorType=3");
                content = content.Replace("DigitGrouping=", "DigitGrouping=" + digitGroupingMI.Checked.GetHashCode());
                content = content.Replace("History=", "History=" + historyMI.Checked.GetHashCode());
                read = readFromFile("ExtraFunction");
                if (unitConversionMI.Checked) content = content.Replace("ExtraFunction=", "ExtraFunction=1");
                if (dateCalculationMI.Checked) content = content.Replace("ExtraFunction=", "ExtraFunction=2");
                if (basicMI.Checked) content = content.Replace("ExtraFunction=", "ExtraFunction=0");
                if (calmethodCB.SelectedIndex >= 0)
                    content = content.Replace("Method=", "Method=" + calmethodCB.SelectedIndex);
                else
                    content = content.Replace("Method=", "Method=0");
                content = content.Replace("AutoCalculate=", "AutoCalculate=" + autocal_date.Checked.GetHashCode());
                if (typeCB.SelectedIndex >= 0)
                    content = content.Replace("TypeCB=", "TypeCB=" + typeCB.SelectedIndex);
                else
                    content = content.Replace("TypeCB=", "TypeCB=0");
                content = content.Replace("MemoryNumber=", "MemoryNumber=" + mem_num.StringValue);
                StreamReader sr = new StreamReader(systempath);
                string contentfile = sr.ReadToEnd();
                sr.Close();
                if (content == contentfile) return;
                else File.WriteAllText(systempath, content);
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
            bool bl = (length > 36);
            Font font = new Font("Consolas", 21.75F);
            if (standardMI.Checked || statisticsMI.Checked)
            {
                if (length < 11) font = new Font("Consolas", 21.75F);
                if (length >= 11 && length < 21) font = new Font("Consolas", 10.75F);
                if (length >= 21) font = new Font("Consolas", 9.75F);
            }
            if (scientificMI.Checked/* && !isMathError*/)
            {
                if (length < 22) font = new Font("Consolas", 21.75F);
                if (length >= 22 && length < 27) font = new Font("Consolas", 16.75F);
                if (length >= 27 && length < 36) font = new Font("Consolas", 13.75F);
                if (length >= 36) font = new Font("Consolas", 10.25F);
            }
            if (programmerMI.Checked)
            {
                if (length < 24) font = new Font("Consolas", 21.75F);
                if (length >= 24 && length < 30) font = new Font("Consolas", 17.75F);
                if (length >= 30 && length < 39) font = new Font("Consolas", 13.75F);
                if (length >= 39 && length < 55) font = new Font("Consolas", 9.75F);
                if (length >= 55 && length < 80) font = new Font("Consolas", 6.75F);
                //if (length >= 66 && length < 80)
                //    font = new Font("Consolas", 2.1F, FontStyle.Regular, GraphicsUnit.Millimeter);
            }
            return font;
        }
        /// <summary>
        /// form standard
        /// </summary>
        private void stdLoad(bool isLoaded)
        {
            int his = historyMI.Checked.GetHashCode();
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked).GetHashCode();
            if (!standardMI.Checked)
            {
                modeMethod(standardMI);
                EnableKeyboardAndChangeFocus();
                if (historyMI.Checked && historyMI.Enabled) stdWithHistory();
                else initializedForm(true);

                enableComponentByProgrammer();
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(false);
                unitconvGB.Location = datecalcGB.Location = new Point(217, 3);
                unitconvGB.Size = datecalcGB.Size = new Size(360, 250 + 103 * his);
                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
                hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = countlb.Visible = !historyMI.Checked || !historyMI.Enabled;
                if (!isLoaded) propertiesChange = true;
                clear_num(false);
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
            this.Size = new Size(216 + 376 * exf, 315 + 105 * his);     // dua ra ngoai de resize lai form
        }
        /// <summary>
        /// form scientific
        /// </summary>
        private void sciLoad(bool isLoaded)
        {
            int his = historyMI.Checked.GetHashCode();
            bool ex = dateCalculationMI.Checked || unitConversionMI.Checked;
            if (!scientificMI.Checked)
            {
                modeMethod(scientificMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(true);
                hideProComponent(false);
                hideStaComponent(false);
                enableComponentByProgrammer();

                if (historyMI.Checked && historyMI.Enabled) sciWithHistory();
                else scientificLoad(true);

                dateCalculationMI.Checked = datecalcGB.Visible;
                unitConversionMI.Checked = unitconvGB.Visible;
                unitconvGB.Location = datecalcGB.Location = new Point(410, 3);
                unitconvGB.Size = datecalcGB.Size = new Size(360, 250 + 103 * his);
                if (ex) basicMI.Checked = false;

                gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
                hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = countlb.Visible = !historyMI.Checked || !historyMI.Enabled;

                this.Size = new Size(413 + 376 * ex.GetHashCode(), 315 + 105 * his);
                if (!isLoaded) clear_num(true);
                if (!isLoaded) propertiesChange = true;
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
        }
        /// <summary>
        /// form programmer
        /// </summary>
        private void proLoad(bool isLoaded)
        {
            bool exf = dateCalculationMI.Checked || unitConversionMI.Checked;
            if (!programmerMI.Checked)
            {
                modeMethod(programmerMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(true);
                hideStaComponent(false);
                dateCalculationMI.Checked = datecalcGB.Visible;
                unitConversionMI.Checked = unitconvGB.Visible;
                programmerMode();
                this.Size = new Size(413 + 374 * exf.GetHashCode(), 374);

                unitconvGB.Location = datecalcGB.Location = new Point(410, 3);
                unitconvGB.Size = datecalcGB.Size = new Size(360, 313);

                gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
                hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = countlb.Visible = !historyMI.Checked || !historyMI.Enabled;

                if (!isLoaded) propertiesChange = true;
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
                if (_byteRB.Checked) SizeBin = 08;
                if (_wordRB.Checked) SizeBin = 16;
                if (dwordRB.Checked) SizeBin = 32;
                if (qwordRB.Checked) SizeBin = 64;
            }
        }
        /// <summary>
        /// form statistics
        /// </summary>
        private void staLoad(bool isLoaded)
        {
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked).GetHashCode();
            if (!statisticsMI.Checked)
            {
                modeMethod(statisticsMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(false);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(true);
                //datecalcGB.Visible = readFromRegistry("Date");
                //unitconvGB.Visible = readFromRegistry("Unit");
                dateCalculationMI.Checked = datecalcGB.Visible;
                unitConversionMI.Checked = unitconvGB.Visible;

                gridPanel.Visible = (historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked;
                hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
                staDGV.Visible = countlb.Visible = !historyMI.Checked || !historyMI.Enabled;

                statisticsMode();
                ctmnEnableAndVisible();

                enableComponentByProgrammer();
                //datecalcGB.Location = unitconvGB.Location = new Point(234, 9);
                //datecalcGB.Size = unitconvGB.Size = new Size(360, 400);
                datecalcGB.Location = unitconvGB.Location = new Point(217, 3);
                datecalcGB.Size = unitconvGB.Size = new Size(360, 353);

                datecalcGB.Visible = dateCalculationMI.Checked;
                unitconvGB.Visible = unitConversionMI.Checked;
                //this.Size = new Size(237 + 363 * exf, 470);
                this.Size = new Size(216 + 376 * exf, 420);
                clear_num(false);
                if (!isLoaded) propertiesChange = true;
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
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

            screenPN.ContextMenu = hisDGV.ContextMenu = scr_lb.ContextMenu = mainContextMenu;
            pasteMI.Enabled = Binary.CheckIsHex(Clipboard.GetText().Trim());

            historyMI.Enabled = !programmerMI.Checked && !statisticsMI.Checked;
            datasetMI.Visible = statisticsMI.Checked;
            historyOptionMI.Visible = historyMI.Enabled;
            datasetMI.Visible = statisticsMI.Checked;
            sepMI3.Visible = (mi != programmerMI);
            pasteMI.Enabled = Binary.CheckIsDec(Clipboard.GetText().Trim());

            sepCTMN.Visible = sepMI3.Visible;
            contextMenuVisible();
        }
        /// <summary>
        /// ẩn hoặc vô hiệu hoá các menuitem trong context menu
        /// </summary>
        private void contextMenuVisible()
        {
            showHistoryCTMN.Visible = !historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
            hideHistoryCTMN.Visible = historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
            clearHistoryCTMN.Visible = historyMI.Checked && historyMI.Enabled;
            clearHistoryCTMN.Enabled = historyMI.Checked && historyMI.Enabled && hisDGV.RowCount > 0;
            clearDatasetCTMN.Visible = statisticsMI.Checked;
            clearDatasetCTMN.Enabled = staDGV.RowCount > 0;   // can dieu chinh lai dieu kien nay
            pasteCTMN.Enabled = Misc.isNumber(Clipboard.GetText());
        }
        /// <summary>
        /// nạp những setting từ lần sử dụng trước đó từ registry
        /// </summary>
        private void loadInfoFromFile()
        {
            //int read = readFromRegistry("CalculatorType");
            int read = readFromFile("CalculatorType");
            if (read == 0) stdLoad(true);
            if (read == 1) sciLoad(true);
            if (read == 2) proLoad(true);
            if (read == 3) staLoad(true);
            // dùng biến read này để đọc tiếp các reg value khác, đỡ phải khai báo biến
            read = readFromFile("ExtraFunction");
            if (read == 1)
            {
                exFunc(unitConversionMI, true);
                typeCB.SelectedIndex = readFromFile("TypeCB");
            }
            if (read == 2)
            {
                exFunc(dateCalculationMI, true);
                calmethodCB.SelectedIndex = readFromFile("Method");
                autocal_date.Checked = (readFromFile("AutoCalculate") == 1);
            }
            read = readFromFile("History");
            if (read == 1)
            {
                if (historyMI.Enabled) formWithHistory(true);
                historyMI.Checked = true;
            }
            read = readFromFile("DigitGrouping");
            if (read == 1) digitLoad(true);
            btdot.Text = Misc.DecimalSym;
        }
        /// <summary>
        /// đọc thuộc tính đã được ghi vào file, nếu không đúng thì sửa luôn
        /// </summary>
        private int readFromFile(string name)
        {
            string content, number;
            int result = 0;
            try
            {
                using (StreamReader sr = new StreamReader(systempath))
                {
                    content = sr.ReadToEnd();
                    number = content.Substring(content.IndexOf(name) + name.Length + 1);
                    number = number.Substring(0, number.IndexOf(Environment.NewLine));
                    result = int.Parse(number);
                }
            }
            catch (Exception)
            {
                content = @"[calc]
CalculatorType=0
DigitGrouping=0
ExtraFunction=0
History=0
MemoryNumber=0
------------------------------------------------
[UnitConversion]
TypeCB=0
------------------------------------------------
[DateCalculation]
AutoCalculate=0
Method=0
------------------------------------------------";
                File.WriteAllText(systempath, content);
                standardMI.Checked = true;
                historyMI.Checked = false;
                digitGroupingMI.Checked = false;
                basicMI.Checked = true;
                return 0;
            }
            switch (name)
            {
                case "TypeCB":
                    if (result > 11 || result < 0)
                    {
                        File.WriteAllText(systempath, content.Replace(name + "=" + result, name + "=0"));
                        result = 0;
                    }
                    break;
                case "CalculatorType":
                    if (result > 4 || result < 0)
                    {
                        File.WriteAllText(systempath, content.Replace(name + "=" + result, name + "=0"));
                        result = 0;
                    }
                    break;
                case "ExtraFunction":
                    if (result > 2 || result < 0)
                    {
                        File.WriteAllText(systempath, content.Replace(name + "=" + result, name + "=0"));
                        result = 0;
                    }
                    break;
                case "DigitGrouping": case "History": case "Method": case "AutoCalculate":
                    if (result > 1 || result < 0)
                    {
                        File.WriteAllText(systempath, content.Replace(name + "=" + result, name + "=0"));
                        result = 0;
                    }
                    break;
            }
            return result;
        }
        /// <summary>
        /// lưu các thuộc tính checked của 1 menuitem vào registry
        /// </summary>
        private void writeToFile(MenuItem MI)
        {
            string systempath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            systempath += "\\calc.ini";
            string content = null;
            using (StreamReader sr = new StreamReader(systempath))
            {
                content = sr.ReadToEnd();
            }

            int read = readFromFile("CalculatorType");
            if (MI == standardMI) File.WriteAllText(systempath, content.Replace("CalculatorType=" + read, "CalculatorType=0"));
            if (MI == scientificMI) File.WriteAllText(systempath, content.Replace("CalculatorType=" + read, "CalculatorType=1"));
            if (MI == programmerMI) File.WriteAllText(systempath, content.Replace("CalculatorType=" + read, "CalculatorType=2"));
            if (MI == statisticsMI) File.WriteAllText(systempath, content.Replace("CalculatorType=" + read, "CalculatorType=3"));
            read = readFromFile("DigitGrouping");
            if (MI == digitGroupingMI) File.WriteAllText(systempath, content.Replace("DigitGrouping=" + read, "DigitGrouping=" + historyMI.Checked.GetHashCode()));
            read = readFromFile("History");
            if (MI == historyMI) File.WriteAllText(systempath, content.Replace("History=" + read, "History=" + historyMI.Checked.GetHashCode()));
            read = readFromFile("ExtraFunction");
            if (MI == unitConversionMI) File.WriteAllText(systempath, content.Replace("ExtraFunction=" + read, "ExtraFunction=2"));
            if (MI == dateCalculationMI) File.WriteAllText(systempath, content.Replace("ExtraFunction=" + read, "ExtraFunction=1"));
            if (MI == basicMI) File.WriteAllText(systempath, content.Replace("ExtraFunction=" + read, "ExtraFunction=0"));
        }
        /// <summary>
        /// ghi thông tin về các thuộc tính các control vào registry
        /// </summary>
        private void writeToFile(string name)
        {

            string content = null;
            using (StreamReader sr = new StreamReader(systempath))
            {
                content = sr.ReadToEnd();
            }

            int read = 0;
            if (name == "AutoCalculate")
            {
                read = readFromFile("AutoCalculate");
                File.WriteAllText(systempath, content.Replace("AutoCalculate=" + read, "AutoCalculate=" + autocal_date.Checked.GetHashCode()));
            }
            if (name == "TypeCB")
            {
                read = readFromFile("TypeCB");
                File.WriteAllText(systempath, content.Replace("TypeCB=" + read, "TypeCB=" + typeCB.SelectedIndex));
            }
        }
        /// <summary>
        /// lấy giá trị của số M từ file
        /// </summary>
        private void getMemoryNumber()
        {
            string content = null, number = null;
            try
            {
                using (StreamReader sr = new StreamReader(systempath))
                {
                    content = sr.ReadToEnd();
                    number = content.Substring(content.IndexOf("MemoryNumber") + "MemoryNumber=".Length);
                    number = number.Substring(0, number.IndexOf(Environment.NewLine));
                    if (Misc.isNumber(number))
                    {
                        mem_num = number;
                        mem_lb.Visible = (mem_num != "0");
                        toolTip1.SetToolTip(mem_lb, string.Format("M = {0}", mem_num.StringValue));
                    }
                    else throw new Exception();
                }
            }
            catch (ArgumentOutOfRangeException)	// thieu mot trong cac truong thuoc tinh
            {
                content = string.Format(@"[calc]
CalculatorType={0}
DigitGrouping={1}
ExtraFunction={2}
History={3}
MemoryNumber={4}
------------------------------------------------
[UnitConversion]
TypeCB={5}
------------------------------------------------
[DateCalculation]
AutoCalculate={6}
Method={7}
------------------------------------------------", readFromFile("CalculatorType"), readFromFile("DigitGrouping"),
                                                 readFromFile("ExtraFunction"), readFromFile("History"),
                                                 mem_num.StringValue, readFromFile("TypeCB"),
                                                 readFromFile("AutoCalculate"), readFromFile("Method"));
                File.WriteAllText(systempath, content);
            }
            catch (Exception)
            {
                content = content.Replace("MemoryNumber=" + number, "MemoryNumber=0");
                File.WriteAllText(systempath, content);
            }
        }
        /// <summary>
        /// form with history - ctrl - H
        /// </summary>
        private void formWithHistory(bool isLoaded)
        {
            historyMI.Checked = !historyMI.Checked;
            prcmdkey = true;
            int his = ((historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked).GetHashCode();
            int sci = scientificMI.Checked.GetHashCode();
            int pro = programmerMI.Checked.GetHashCode();
            int exf = (dateCalculationMI.Checked || unitConversionMI.Checked).GetHashCode();
            
            datecalcGB.Location = unitconvGB.Location = new Point(217 + 193 * (sci + pro), 3);
            datecalcGB.Size = unitconvGB.Size = new Size(360, 250 + 103 * his);

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
                if (scientificMI.Checked)
                {
                    scientificLoad(false);
                }
            }
            gridPanel.Visible = historyMI.Checked && historyMI.Enabled;
            hisDGV.Visible = historyMI.Checked && historyMI.Enabled;
            staDGV.Visible = countlb.Visible = !historyMI.Checked || !historyMI.Enabled;

            historyOptionEnabled();
            //this.screenPN.Size = new Size(scr_lb.Size.Width, 47);
            this.scr_lb.Size = new Size(170 + 195 * sci, 45);
            this.scr_lb.Location = new Point(18, 0);
            this.Size = new Size(216 + 197 * (sci + pro) + 376 * exf, 315 + 105 * his + 80 * pro);
            if (!isLoaded) propertiesChange = true;
        }
        /// <summary>
        /// 2 tính năng date calculation và unit conversion
        /// </summary>
        private void exFunc(MenuItem menuitem, bool isLoad)
        {
            int his = ((historyMI.Checked && historyMI.Enabled) || statisticsMI.Checked).GetHashCode();
            int pro = programmerMI.Checked.GetHashCode();
            int sci = scientificMI.Checked.GetHashCode();
            //if (!MI.Checked)
            datecalcGB.Visible = dateCalculationMI.Checked = (menuitem == dateCalculationMI);
            unitconvGB.Visible = unitConversionMI.Checked = (menuitem == unitConversionMI);

            datecalcGB.Size = unitconvGB.Size = new Size(360, 250 + 63 * pro + 103 * his);

            basicMI.Checked = false;

            if (isLoad) typeCB.SelectedIndexChanged -= typeCB_SelectedIndexChanged;
            typeCB.SelectedIndex = readFromFile("TypeCB");
            if (isLoad) typeCB.SelectedIndexChanged += typeCB_SelectedIndexChanged;
            assignDefaultIndex();
            //toTB.Text = getToTBText(fromTB.Text);
            //this.AcceptButton = calculate_unit;

            if (isLoad) calmethodCB.SelectedIndexChanged -= cal_method_SelectedIndexChanged;
            calmethodCB.SelectedIndex = readFromFile("Method");
            if (isLoad)
            {
                calmethodCB.SelectedIndexChanged += cal_method_SelectedIndexChanged;
                control_visible(calmethodCB.SelectedIndex == 0);
            }

            autocal_date.Checked = (readFromFile("AutoCalculate") == 1);
            this.AcceptButton = calculate_date;

            prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            
            this.Size = new Size(592 + 197 * (sci + pro), 315 + 105 * his + 66 * pro);

            if (!isLoad) propertiesChange = true;
        }
        /// <summary>
        /// ẩn các control đặc biệt của form standard
        /// </summary>
        private void hideStdComponent(bool bl)
        {
            #region Hide standard functions
            ce.Visible = bl;
            clearbt.Visible = bl;
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
            staDGV.Visible = countlb.Visible = bl;
            CAD.Visible = bl;
            clearsta.Visible = bl;
            fe_ChkBox.Visible = bl;
            Expsta_bt.Visible = bl;
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
            num2.Enabled = true;
            num3.Enabled = true;
            num4.Enabled = true;
            num5.Enabled = true;
            num5.Enabled = true;
            num6.Enabled = true;
            num7.Enabled = true;
            num8.Enabled = true;
            num9.Enabled = true;
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;

            btFactorial.Enabled = true;
            sqrt_bt.Enabled = true;
            percent_bt.Enabled = true;
            invert_bt.Enabled = true;
            btdot.Enabled = true;
        }
        /// <summary>
        /// sự kiện radio button của form programmer checked changed:
        /// set lại thuộc tính enable cho các nút số và A-F
        /// </summary>
        private void baseRBCheckedChanged()
        {
            bool bl = (octRB.Checked || decRB.Checked || hexRB.Checked);
            //this.num2.Location = new Point(267, 293);
            this.num2.Enabled = bl;
            //this.num3.Location = new Point(309, 293);
            this.num3.Enabled = bl;
            //this.num4.Location = new Point(225, 257);
            this.num4.Enabled = bl;
            //this.num5.Location = new Point(267, 257);
            this.num5.Enabled = bl;
            //this.num6.Location = new Point(309, 257);
            this.num6.Enabled = bl;
            //this.num7.Location = new Point(225, 221);
            this.num7.Enabled = bl;

            bl = (decRB.Checked || hexRB.Checked);
            //this.num8.Location = new Point(267, 221);
            this.num8.Enabled = bl;
            //this.num9.Location = new Point(309, 221);
            this.num9.Enabled = bl;
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
        private void mem_process(int method)
        {
            if (!isMathError)
            {
                if (method == 1) mem_num = mem_num + str;  // M+
                if (method == 2) mem_num = mem_num - str;  // M-
                if (method == 3) mem_num = str;            // MS
                displayToScreen();

                mem_lb.Visible = (mem_num != "0");
                propertiesChange = true;
                toolTip1.SetToolTip(mem_lb, string.Format("M = {0}", mem_num.StringValue));
                confirm_num = true; 
            }
        }

        string preFunc = "0";
        bool isFuncClicked = false;
        /// <summary>
        /// các hàm tính nâng cao
        /// </summary>
        private void math_func(Button bt)
        {
            BigNumber inp_num = str;
            BigNumber resultLong = 1;   // biến này để tính giai thừa
            if (deg_rb.Checked) parser.Mode = Mode.DEG;
            // mặc định mode là mode.rad rồi
            if (gra_rb.Checked) parser.Mode = Mode.GRA;

            string parameter = inp_num.StringValue;
            int tabIndex = bt.TabIndex;
            if (isFuncClicked || str[0] == '-' || preFunc[0] == '-') parameter = preFunc;
            switch (tabIndex)
            {
                // sin, cos, tan, sinh, cosh, tanh, log, int
                case 28: case 29: case 30: case 35: case 36: case 37: case 42: case 44: case 45:
                    preFunc = string.Format("{0}({1})", bt.Text, parameter);
                    break;
                case 17:
                    preFunc = string.Format("reciproc({0})", parameter);
                    break;
                case 19:
                    preFunc = string.Format("sqrt({0})", parameter);
                    break;
                case 31:
                    if (!inv_ChkBox.Checked) preFunc = string.Format("ln({0})", parameter);
                    else preFunc = string.Format("powe({0})", parameter);
                    break;
                case 38:
                    preFunc = string.Format("sqr({0})", parameter);
                    break;
                case 39:
                    preFunc = string.Format("cube({0})", parameter);
                    break;
                case 40:
                    preFunc = string.Format("powten({0})", parameter);
                    break;
                case 41:
                    preFunc = string.Format("cuberoot({0})", parameter);
                    break;
            }
            if (inv_ChkBox.Checked) inv_ChkBox.Checked = false;
            if (standardMI.Checked) pex = parser.EvaluateStd(preFunc);
            if (scientificMI.Checked) pex = parser.EvaluateSci(preFunc);
            str = parser.strResult;

            if (bt.TabIndex == 32)
            {
                #region Calculate the factorial
                inp_num = str;
                DialogResult dr;

                if (inp_num > "9999")
                {
                    dr = MessageBox.Show(@"It'll take a long time to calculate the factorial!
Do you wish to perform a fast factorial calcualtion that'll take a shorten time than normal,
but it's expression will show a less accuracy than normal", "Calculator", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (BigNumber.IsInteger(inp_num))
                    {
                        if (isFuncClicked)
                        {
                            sci_exp = sci_exp.Substring(0, sci_exp.LastIndexOf(preFunc));
                            if (str == parser.strResult)
                            {
                                sci_exp = string.Format("{0} fact({1})", sci_exp, preFunc);
                            }
                            else
                            {
                                sci_exp = string.Format("{0} fact({1})", sci_exp, inp_num.StringValue);
                            }
                            preFunc = string.Format("fact({0})", inp_num.StringValue);
                        }
                        else
                        {
                            sci_exp = string.Format("{0} fact({1})", sci_exp, inp_num.StringValue);
                            preFunc = string.Format("fact({0})", inp_num.StringValue);
                        }
                    }

                    fastFlag = (dr == DialogResult.Yes);
                    if (dr == DialogResult.No)
                    {
                        parser.EvaluateSci(preFunc);
                        str = parser.strResult;
                    }
                    if (dr == DialogResult.Yes)
                    {
                        resultLong = BigNumber.FastFactorial(str);
                        str = resultLong.StringValue;
                    }
                }
                else
                {
                    if (BigNumber.IsInteger(inp_num))
                    {
                        if (isFuncClicked)
                        {
                            //sci_exp = sci_exp.Substring(0, sci_exp.LastIndexOf(preFunc));
                            //sci_exp = string.Format("{0} fact({1})", sci_exp, preFunc);
                            preFunc = string.Format("fact({0})", preFunc);
                        }
                        else
                        {
                            //sci_exp = string.Format("{0} fact({1})", sci_exp, inp_num.StringValue);
                            preFunc = string.Format("fact({0})", inp_num.StringValue);
                        }
                        parser.EvaluateSci(preFunc);
                        str = parser.strResult;
                    }
                }
                displayToScreen();
                isFuncClicked = isFuncClicked = true;   // funcID != 32;
                pre_bt = tabIndex;
                return;
                #endregion
            }
            else
            {
                isMathError = (pex != null);
                if (pex == null)
                {
                    if (standardMI.Checked)
                    {
                        str = new BigNumber(parser.strResult).Round(15).StringValue;
                    }
                    else
                    {
                        str = parser.strResult;
                    }
                    displayToScreen();
                    isFuncClicked = confirm_num = true;   // funcID != 32;
                }
                else
                {
                    scr_lb.Text = pex.Message;
                    operator_lb.Visible = false;
                    if (pex.Message.Length > 45)
                        scr_lb.Font = new Font(scr_lb.Font.FontFamily, 9.75F);
                    else
                        scr_lb.Font = new Font(scr_lb.Font.FontFamily, 12.75F);
                    disableAllFunction(true);
                }
                pex = null;
            }

            pre_bt = tabIndex;
        }
        /// <summary>
        /// vô hiệu hoá toàn bộ các chức năng khi thực hiện 1 phép toán lỗi (logarit 1 số âm...)
        /// </summary>
        private void disableAllFunction(bool bl)
        {
            historyOptionEnabled();
            isMathError = true;
        }

        private void recalculate(int rowID)
        {
            hisDGV.EndEdit();
            try
            {
                sci_exp = hisDGV[0, rowID].Value.ToString(); // sau khi ket thuc 1 phep tinh, sci_exp duoc gan ve ""
            }
            catch
            {
                sci_exp = oldValue.ToString();
                hisDGV[0, rowID].Value = oldValue;
            }
            evaluateExpression(rowID);
            hisDGV.ReadOnly = prcmdkey = true;
            isFuncClicked = false;
            sci_exp = "";
            historyOptionEnabled();
            if (pex == null) displayToScreen();
        }
        /// <summary>
        /// biến cờ xác định xem người dùng có cần tính nhanh biểu thức giai thừa hay không
        /// </summary>
        static bool fastFlag = false;
        /// <summary>
        /// [F]ast [F]actorial [F]lag
        /// biến cờ xác định xem người dùng có cần tính nhanh biểu thức giai thừa hay không
        /// </summary>
        public static bool F3
        {
            get { return fastFlag; }
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
            binnum = Binary.dec_to_other(resultpro.StringValue, 2, SizeBin);
            decnum = Binary.other_to_dec(binnum, 2, SizeBin);

            octnum = Binary.dec_to_other(decnum, 08, SizeBin);
            hexnum = Binary.dec_to_other(decnum, 16, SizeBin);
            binnum64 = binnum.PadLeft(64, '0');
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(64 - (i + 1) * 4, 4);
            }

            if (binRB.Checked) str = binnum;
            if (octRB.Checked) str = octnum;
            if (decRB.Checked) str = decnum;
            if (hexRB.Checked) str = hexnum;
            scr_lb.Text = str;
        }
        /// <summary>
        /// nút xoá số
        /// </summary>
        /// <param name="c_bt">biến kiểm tra xem nút xoá có phải nút C hay không</param>
        private void clear_num(bool c_bt)
        {
            scr_lb.Text = str = "0";
            isMathError = isFuncClicked = false;
            prcmdkey = confirm_num = true;

            scr_lb.Font = new Font(scr_lb.Font.FontFamily, 21.75F);

            if (c_bt)
            {
                pre_oprt = pre_priority = priority = openBRK = closeBRK = 0;
                preFunc = "0";
                bracketTime_lb.Text = sci_exp = "";
                result = 0;
                pre_bt = -1;
                operator_lb.Visible = inv_ChkBox.Checked = fe_ChkBox.Checked = false;
            }
            for (int i = 0; i < 16; i++) bin_digit[i].Text = "0000";
            screenToPanel();
        }
        /// <summary>
        /// hiển thị số lên màn hình
        /// </summary>
        private void displayToScreen()
        {
            if (programmerMI.Checked) // nhóm cho form programmer
            {
                if (digitGroupingMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = Misc.grouping(str);
                    if (octRB.Checked) scr_lb.Text = Misc.grouping(str, 3);
                    if (binRB.Checked || hexRB.Checked) scr_lb.Text = Misc.grouping(str, 4);
                }
                else scr_lb.Text = Misc.de_group(str);
            }
            else                      // nhóm cho form non-programmer
            {
                if (fe_ChkBox.Checked)
                {
                    scr_lb.Text = new BigNumber(str).ToString();
                    return;	// không cần quan tâm đến digitGroupingMI nữa
                }
                if (digitGroupingMI.Checked)
                {
                    if (!str.ToUpper().Contains("E"))
                        scr_lb.Text = Misc.grouping(str);
                    else scr_lb.Text = str;
                }
                else
                {
                    scr_lb.Text = str;
                }
            }
            if (hisDGV.Visible) hisDGV.Focus();
            if (staDGV.Visible) staDGV.Focus();
        }
        /// <summary>
        /// nhập số cho form non-programmer
        /// </summary>
        private void numinput(int index)
        {
            string strTemp = "";
            if (index < 10) // 0 đến 9
            {
                if (!confirm_num)
                {
                    if (str != "0") strTemp = str + index.ToString();
                    else { confirm_num = true; pre_bt = index; return; }
                    if (strTemp.Length < 40 && new BigNumber(strTemp) < "1e32") str = strTemp;
                }
                else
                {
                    str = index.ToString();
                }
                isFuncClicked = false;
                preFunc = str;
                goto breakpoint;
            }
            if (index == 10)    // dấu thập phân
            {
                // chưa có dấu thập phân thì thêm vào, không có thì thôi
                if (confirm_num) str = "0" + Misc.DecimalSym;
                if (!str.Contains(Misc.DecimalSym)) str += Misc.DecimalSym;
                goto breakpoint;
            }
            if (index == 11)    // dấu âm
            {
                #warning vẫn còn lỗi phần đổi dấu
                //update 19-04: về cơ bản là đã hết lỗi
                if (str[0] == '-') str = str.Substring(1);
                else
                {
                    if (new BigNumber(str) != "0") str = "-" + str;
                    else return;
                }
                if (isFuncClicked)
                {
                    if (pre_bt != 11)
                    {
                        if (pre_oprt != 0) preFunc = string.Format("(-{0})", preFunc);
                        else preFunc = string.Format("-{0}", preFunc);
                    }
                    else
                    {
                        if (pre_oprt != 0)
                        {
                            string pref = preFunc;
                            if (preFunc[0] == '-' || preFunc[1] == '-') preFunc = preFunc.Substring(2, preFunc.Length - 3);
                            else preFunc = string.Format("(-{0})", preFunc);
                        }
                        else
                        {
                            if (preFunc[0] == '-' || preFunc[1] == '-') preFunc = preFunc.Substring(1);
                            else preFunc = string.Format("-{0}", preFunc);
                        }
                    }
                }
                else
                {
                    if (str[0] == '-')
                    {
                        preFunc = string.Format("({0})", str);
                    }
                    else
                    {
                        preFunc = str;
                    }
                }
                pre_bt = index;
                displayToScreen();
                return;
            }
            if ((confirm_num || str == "0") && index != 11)
            {
                str = index.ToString();
                confirm_num = false;
            }
            breakpoint: confirm_num = false;
            pre_bt = index;
            displayToScreen();
        }
        /// <summary>
        /// nhập số cho form programmer
        /// </summary>
        private void numinput_pro(int index)
        {
            if (index < 10) // 0 đến 9
            {
                string strTemp = "";
                if (programmerMI.Checked)
                {
                    if (decRB.Checked)
                    {
                        if (!confirm_num)
                        {
                            if (new BigNumber(str + index.ToString()) < BigNumber.Two.Pow(SizeBin))
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
                            if (strTemp.Length < 65) str = strTemp;
                            else return;
                        }
                        if (octRB.Checked)
                        {
                            string pn = "";
                            switch (SizeBin)
                            {
                                case 08: pn = (byte.MaxValue + 1).ToString();
                                    break;
                                case 16: pn = (ushort.MaxValue + 1).ToString();
                                    break;
                                case 32: pn = (new BigNumber(uint.MaxValue) + 1).StringValue;
                                    break;
                                case 64: pn = (new BigNumber(ulong.MaxValue) + 1).StringValue;
                                    break;
                            }
                            string max = Binary.dec_to_other(pn, 8, SizeBin);
                            BigNumber oct = strTemp;
                            if (oct <= max) str = strTemp;
                            else return;
                        }
                        if (hexRB.Checked)
                        {
                            if (strTemp.Length < 17) strTemp = Binary.other_to_dec(strTemp, 16, SizeBin);
                            else return;
                        }
                        // decrb.checked da xu ly o tren
                    }
                }
                //goto breakpoint;
            }
            if (index == 11)    // dấu âm
            {
                if (str[0] == '-') str = str.Substring(1);
                else
                {
                    if (str == "0") return;
                    str = (new BigNumber("-" + decnum) + BigNumber.Two.Pow(SizeBin)).StringValue;
                    if (!decRB.Checked)
                    {
                        if (binRB.Checked) str = Binary.dec_to_other(str, 02, SizeBin);
                        if (octRB.Checked) str = Binary.dec_to_other(str, 08, SizeBin);
                        if (hexRB.Checked) str = Binary.dec_to_other(str, 16, SizeBin);
                    }
                    screenToPanel();
                    confirm_num = true;
                    return;
                }
                pre_bt = index;
                displayToScreen();
                return;
            }
            // eo hieu doan duoi nay lam clgn
            if ((confirm_num || str == "0") && index != 11)
            {
                str = index.ToString();
            }
            confirm_num = false;
            pre_bt = index;
            screenToPanel();
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
                int checkLen = 65 * binRB.Checked.GetHashCode() + 50 * octRB.Checked.GetHashCode() + 17 * hexRB.Checked.GetHashCode();
                if (binRB.Checked || octRB.Checked || hexRB.Checked)
                {
                    if (strTemp.Length < checkLen) str = strTemp;
                    else return;    // khong lam clgh
                }
                else if (Convert.ToDouble(strTemp) < (double)Binary.powan(2, SizeBin)) //18.446.744.073.709.551.615 = 2^64
                {
                    if (!confirm_num)
                    {
                        if (str != "0") str += text;
                    }
                    else str = text;
                }
                else return;
            }
            confirm_num = false;
            prcmdkey = true;
            screenToPanel();
        }

        BigNumber result = "0";
        /// <summary>
        /// các toán tử +-*/ của form standard
        /// </summary>
        private void std_operation(int index)
        {
            if (index == 12) operator_lb.Text = "+";
            if (index == 13) operator_lb.Text = "-";
            if (index == 14) operator_lb.Text = "*";
            if (index == 15) operator_lb.Text = "/";
            if (pre_oprt != 0)
            {
                switch (pre_bt)
                {
                    case 12: case 13: case 14: case 15: case 30: case 34:
                        sci_exp = sci_exp.Substring(0, sci_exp.Length - 1) + operator_lb.Text;
                        goto breakpoint;
                    default:
                        break;
                }
                sci_exp += string.Format(" {0} {1}", preFunc, operator_lb.Text); // chỉ việc ăn sẵn thôi
            }
            else    // pre_oprt != 0
            {
                if (isFuncClicked)
                {
                    sci_exp = string.Format("{0} {1}", preFunc, operator_lb.Text);
                }
                else
                {
                    sci_exp = string.Format("{0} {1}", str, operator_lb.Text);
                }
            }       // pre_oprt != 0

            pex = parser.EvaluateStd(sci_exp.Substring(0, sci_exp.Length - 2));
            str = parser.strResult;

            breakpoint:confirm_num = true;
            displayToScreen();
            pre_oprt = index;
            pre_bt = index;
            operator_lb.Visible = true;
            isFuncClicked = false;
        }

        string powerFunc = "", mulDivFunc = "";
        #warning 13/05/2013 xử lý từ đây trước và hàm equal của nó
        /// <summary>
        /// các toán tử +-*/ của form scientific
        /// </summary>
        private void sci_operation(int index)
        {
            pre_priority = priority;
            switch (index)
            {
                case 12:
                    operator_lb.Text = "+"; priority = 1;
                    break;
                case 13:
                    operator_lb.Text = "-"; priority = 1;
                    break;
                case 14:
                    operator_lb.Text = "*"; priority = 2;
                    break;
                case 15:
                    operator_lb.Text = "/"; priority = 2;
                    break;
                case 30:
                    operator_lb.Text = "^"; priority = 3;
                    break;
                case 34:
                    operator_lb.Text = "yroot"; priority = 3; //√
                    break;
                case 142:
                    operator_lb.Text = "mod"; priority = 2;
                    break;
            }
            if (pre_oprt != 0)
            {
                switch (pre_bt)
                {
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 30:
                    case 34:
                    case 142:
                        if (priority <= pre_priority) sci_exp = sci_exp.Substring(0, sci_exp.LastIndexOf(" ")) + " " + operator_lb.Text;
                        else sci_exp = string.Format("({0}) {1}", sci_exp.Substring(0, sci_exp.LastIndexOf(" ")), operator_lb.Text);
                        goto breakpoint;
                }
                sci_exp += string.Format(" {0} {1}", preFunc, operator_lb.Text); // chỉ việc ăn sẵn thôi
                if (index == 30 || index == 34)
                {
                    powerFunc += (preFunc + operator_lb.Text);
                    pex = parser.EvaluateSci(powerFunc.Substring(0, powerFunc.Length - 1));
                    str = parser.strResult;
                }
                if (index == 14 || index == 15)
                {
                    if (pre_oprt == 30 || pre_oprt == 34)
                    {
                        powerFunc += preFunc;
                        pex = parser.EvaluateSci(powerFunc);
                        mulDivFunc += parser.strResult;
                        pex = parser.EvaluateSci(mulDivFunc);
                        str = parser.strResult;
                        mulDivFunc += operator_lb.Text;
                    }
                    if (pre_oprt == 12 || pre_oprt == 13 || pre_oprt == 14 || pre_oprt == 15)
                    {
                        mulDivFunc += str;
                        pex = parser.EvaluateSci(mulDivFunc);
                        str = parser.strResult;
                        mulDivFunc += operator_lb.Text;
                    }
                    powerFunc = "";
                }
                if (index == 12 || index == 13)
                {
                    pex = parser.EvaluateSci(sci_exp.Substring(0, sci_exp.Length - 2));
                    str = parser.strResult;
                    powerFunc = "";
                    mulDivFunc = "";
                }
            }
            else    // pre_oprt != 0
            {
                if (isFuncClicked)
                {
                    sci_exp = string.Format("{0} {1}", preFunc, operator_lb.Text);
                }
                else
                {
                    sci_exp = string.Format("{0} {1}", str, operator_lb.Text);
                }

                if (index == 30 || index == 34)
                {
                    powerFunc = string.Format("{0} {1}", preFunc, operator_lb.Text);
                }
                if (index == 14 || index == 15)
                {
                    mulDivFunc = string.Format("{0} {1}", preFunc, operator_lb.Text);
                }
            }       // pre_oprt != 0

             breakpoint: displayToScreen();
            pre_bt = pre_oprt = index;
            operator_lb.Visible = true;
            confirm_num = true;
        }

        BigNumber num1pro, num2pro = 0, resultpro = 0;
        /// <summary>
        /// các toán tử +-*/ của form programmer
        /// </summary>
        private void pro_operation(int index)
        {
            confirm_num = true;

            if (index == 12) operator_lb.Text = "+";
            if (index == 13) operator_lb.Text = "-";
            if (index == 14) operator_lb.Text = "*";
            if (index == 15) operator_lb.Text = "/";

            screenToPanel();
            if (pre_oprt == 0)
            {
                num1pro = decnum;
                resultpro = num1pro;
            }
            else
            {
                switch (pre_oprt)
                {
                    case 12: case 13: case 14: case 15: case 211:
                        num1pro = resultpro;
                        num2pro = decnum;
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
                        bitWiseOperators();
                        resultpro = str;
                        break;
                }
                programmerOperation();
            }
            jump: pre_oprt = pre_bt = index;
            operator_lb.Visible = true;
        }
        /// <summary>
        /// kiểm tra thuộc tính visible của các contextmenu history và dataset
        /// </summary>
        private void ctmnEnableAndVisible()
        {
            this.hideHistoryCTMN.Visible = (standardMI.Checked || scientificMI.Checked) && historyMI.Checked;
            this.showHistoryCTMN.Visible = (standardMI.Checked || scientificMI.Checked) && !historyMI.Checked;
            this.clearHistoryCTMN.Visible = hideHistoryCTMN.Visible;
            this.clearDatasetCTMN.Visible = statisticsMI.Checked;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.num1 = new System.Windows.Forms.Button();
            this.num2 = new System.Windows.Forms.Button();
            this.num3 = new System.Windows.Forms.Button();
            this.num4 = new System.Windows.Forms.Button();
            this.num5 = new System.Windows.Forms.Button();
            this.num6 = new System.Windows.Forms.Button();
            this.num7 = new System.Windows.Forms.Button();
            this.num8 = new System.Windows.Forms.Button();
            this.num9 = new System.Windows.Forms.Button();
            this.num0 = new System.Windows.Forms.Button();
            this.btdot = new System.Windows.Forms.Button();
            this.addbt = new System.Windows.Forms.Button();
            this.minusbt = new System.Windows.Forms.Button();
            this.mulbt = new System.Windows.Forms.Button();
            this.divbt = new System.Windows.Forms.Button();
            this.equal = new System.Windows.Forms.Button();
            this.invert_bt = new System.Windows.Forms.Button();
            this.percent_bt = new System.Windows.Forms.Button();
            this.backspace = new System.Windows.Forms.Button();
            this.ce = new System.Windows.Forms.Button();
            this.doidau = new System.Windows.Forms.Button();
            this.memclear = new System.Windows.Forms.Button();
            this.mstore = new System.Windows.Forms.Button();
            this.memrecall = new System.Windows.Forms.Button();
            this.sqrt_bt = new System.Windows.Forms.Button();
            this.clearbt = new System.Windows.Forms.Button();
            this.madd = new System.Windows.Forms.Button();
            this.mem_minus_bt = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.autocal_date = new System.Windows.Forms.CheckBox();
            this.calmethodCB = new System.Windows.Forms.ComboBox();
            this.calculate_date = new System.Windows.Forms.Button();
            this.result2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtP2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtP1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.result1 = new System.Windows.Forms.TextBox();
            this.periodsDateUD = new System.Windows.Forms.NumericUpDown();
            this.typeLB = new System.Windows.Forms.Label();
            this.fromLB = new System.Windows.Forms.Label();
            this.toLB = new System.Windows.Forms.Label();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.fromTB = new System.Windows.Forms.TextBox();
            this.toTB = new System.Windows.Forms.TextBox();
            this.unitconvGB = new System.Windows.Forms.GroupBox();
            this.invert_unit = new System.Windows.Forms.Button();
            this.datecalcGB = new System.Windows.Forms.GroupBox();
            this.subrb = new System.Windows.Forms.RadioButton();
            this.addrb = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.periodsYearUD = new System.Windows.Forms.NumericUpDown();
            this.periodsMonthUD = new System.Windows.Forms.NumericUpDown();
            this.angleGB = new System.Windows.Forms.GroupBox();
            this.gra_rb = new System.Windows.Forms.RadioButton();
            this.rad_rb = new System.Windows.Forms.RadioButton();
            this.deg_rb = new System.Windows.Forms.RadioButton();
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
            this.unknownPN = new System.Windows.Forms.Panel();
            this._byteRB = new System.Windows.Forms.RadioButton();
            this.qwordRB = new System.Windows.Forms.RadioButton();
            this._wordRB = new System.Windows.Forms.RadioButton();
            this.dwordRB = new System.Windows.Forms.RadioButton();
            this.basePN = new System.Windows.Forms.Panel();
            this.binRB = new System.Windows.Forms.RadioButton();
            this.octRB = new System.Windows.Forms.RadioButton();
            this.decRB = new System.Windows.Forms.RadioButton();
            this.hexRB = new System.Windows.Forms.RadioButton();
            this.PNbinary = new System.Windows.Forms.Panel();
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
            this.Expsta_bt = new System.Windows.Forms.Button();
            this.sigmax2BT = new System.Windows.Forms.Button();
            this.sigman_1BT = new System.Windows.Forms.Button();
            this.AddstaBT = new System.Windows.Forms.Button();
            this.xcross = new System.Windows.Forms.Button();
            this.sigmaxBT = new System.Windows.Forms.Button();
            this.sigmanBT = new System.Windows.Forms.Button();
            this.CAD = new System.Windows.Forms.Button();
            this.x2cross = new System.Windows.Forms.Button();
            this.clearsta = new System.Windows.Forms.Button();
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
            this.feMPG_MI = new System.Windows.Forms.MenuItem();
            this.fe_L100_MI = new System.Windows.Forms.MenuItem();
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
            this.sepMI4 = new System.Windows.Forms.MenuItem();
            this.standardExpr = new System.Windows.Forms.MenuItem();
            this.datasetMI = new System.Windows.Forms.MenuItem();
            this.copyDatasetMI = new System.Windows.Forms.MenuItem();
            this.editDatasetMI = new System.Windows.Forms.MenuItem();
            this.commitDSMI = new System.Windows.Forms.MenuItem();
            this.cancelEditDSMI = new System.Windows.Forms.MenuItem();
            this.clearDatasetMI = new System.Windows.Forms.MenuItem();
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
            this.gridPanel = new Calculator.IPanel();
            this.staDGV = new Calculator.IDataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hisDGV = new Calculator.IDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countlb = new System.Windows.Forms.Label();
            this.screenPN = new Calculator.IPanel();
            this.operator_lb = new System.Windows.Forms.Label();
            this.scr_lb = new System.Windows.Forms.Label();
            this.mem_lb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).BeginInit();
            this.unitconvGB.SuspendLayout();
            this.datecalcGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).BeginInit();
            this.angleGB.SuspendLayout();
            this.unknownPN.SuspendLayout();
            this.basePN.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).BeginInit();
            this.screenPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // num1
            // 
            this.num1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num1.Location = new System.Drawing.Point(13, 194);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(34, 27);
            this.num1.TabIndex = 1;
            this.num1.TabStop = false;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            this.num1.Click += new System.EventHandler(this.numberinput_Click);
            this.num1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num2
            // 
            this.num2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2.Location = new System.Drawing.Point(52, 194);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(34, 27);
            this.num2.TabIndex = 2;
            this.num2.TabStop = false;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            this.num2.Click += new System.EventHandler(this.numberinput_Click);
            this.num2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num3
            // 
            this.num3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3.Location = new System.Drawing.Point(91, 194);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(34, 27);
            this.num3.TabIndex = 3;
            this.num3.TabStop = false;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            this.num3.Click += new System.EventHandler(this.numberinput_Click);
            this.num3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num4
            // 
            this.num4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4.Location = new System.Drawing.Point(13, 162);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(34, 27);
            this.num4.TabIndex = 4;
            this.num4.TabStop = false;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            this.num4.Click += new System.EventHandler(this.numberinput_Click);
            this.num4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num5
            // 
            this.num5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5.Location = new System.Drawing.Point(52, 162);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(34, 27);
            this.num5.TabIndex = 5;
            this.num5.TabStop = false;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            this.num5.Click += new System.EventHandler(this.numberinput_Click);
            this.num5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num6
            // 
            this.num6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6.Location = new System.Drawing.Point(91, 162);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(34, 27);
            this.num6.TabIndex = 6;
            this.num6.TabStop = false;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            this.num6.Click += new System.EventHandler(this.numberinput_Click);
            this.num6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num7
            // 
            this.num7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7.Location = new System.Drawing.Point(13, 130);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(34, 27);
            this.num7.TabIndex = 7;
            this.num7.TabStop = false;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            this.num7.Click += new System.EventHandler(this.numberinput_Click);
            this.num7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num8
            // 
            this.num8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8.Location = new System.Drawing.Point(52, 130);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(34, 27);
            this.num8.TabIndex = 8;
            this.num8.TabStop = false;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            this.num8.Click += new System.EventHandler(this.numberinput_Click);
            this.num8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num9
            // 
            this.num9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9.Location = new System.Drawing.Point(91, 130);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(34, 27);
            this.num9.TabIndex = 9;
            this.num9.TabStop = false;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            this.num9.Click += new System.EventHandler(this.numberinput_Click);
            this.num9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // num0
            // 
            this.num0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num0.Location = new System.Drawing.Point(13, 226);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(73, 27);
            this.num0.TabIndex = 0;
            this.num0.TabStop = false;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            this.num0.Click += new System.EventHandler(this.numberinput_Click);
            this.num0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btdot
            // 
            this.btdot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(91, 226);
            this.btdot.Name = "btdot";
            this.btdot.Size = new System.Drawing.Size(34, 27);
            this.btdot.TabIndex = 10;
            this.btdot.TabStop = false;
            this.btdot.Text = ".";
            this.btdot.UseVisualStyleBackColor = true;
            this.btdot.Click += new System.EventHandler(this.numberinput_Click);
            this.btdot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // addbt
            // 
            this.addbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addbt.Location = new System.Drawing.Point(130, 226);
            this.addbt.Name = "addbt";
            this.addbt.Size = new System.Drawing.Size(34, 27);
            this.addbt.TabIndex = 12;
            this.addbt.TabStop = false;
            this.addbt.Text = "+";
            this.addbt.UseVisualStyleBackColor = true;
            this.addbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.addbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // minusbt
            // 
            this.minusbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minusbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.minusbt.Location = new System.Drawing.Point(130, 194);
            this.minusbt.Name = "minusbt";
            this.minusbt.Size = new System.Drawing.Size(34, 27);
            this.minusbt.TabIndex = 13;
            this.minusbt.TabStop = false;
            this.minusbt.Text = "-";
            this.minusbt.UseVisualStyleBackColor = true;
            this.minusbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.minusbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // mulbt
            // 
            this.mulbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mulbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mulbt.Location = new System.Drawing.Point(130, 162);
            this.mulbt.Name = "mulbt";
            this.mulbt.Size = new System.Drawing.Size(34, 27);
            this.mulbt.TabIndex = 14;
            this.mulbt.TabStop = false;
            this.mulbt.Text = "*";
            this.mulbt.UseVisualStyleBackColor = true;
            this.mulbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.mulbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // divbt
            // 
            this.divbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.divbt.Location = new System.Drawing.Point(130, 130);
            this.divbt.Name = "divbt";
            this.divbt.Size = new System.Drawing.Size(34, 27);
            this.divbt.TabIndex = 15;
            this.divbt.TabStop = false;
            this.divbt.Text = "/";
            this.divbt.UseVisualStyleBackColor = true;
            this.divbt.Click += new System.EventHandler(this.operatorBT_Click);
            this.divbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // equal
            // 
            this.equal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.equal.Location = new System.Drawing.Point(169, 194);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(34, 59);
            this.equal.TabIndex = 16;
            this.equal.TabStop = false;
            this.equal.Text = "=";
            this.equal.UseVisualStyleBackColor = true;
            this.equal.Click += new System.EventHandler(this.equal_Click);
            this.equal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // invert_bt
            // 
            this.invert_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invert_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.invert_bt.Location = new System.Drawing.Point(169, 162);
            this.invert_bt.Name = "invert_bt";
            this.invert_bt.Size = new System.Drawing.Size(34, 27);
            this.invert_bt.TabIndex = 17;
            this.invert_bt.TabStop = false;
            this.invert_bt.Text = "1/x";
            this.invert_bt.UseVisualStyleBackColor = true;
            this.invert_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.invert_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // percent_bt
            // 
            this.percent_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percent_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent_bt.Location = new System.Drawing.Point(169, 130);
            this.percent_bt.Name = "percent_bt";
            this.percent_bt.Size = new System.Drawing.Size(34, 27);
            this.percent_bt.TabIndex = 18;
            this.percent_bt.TabStop = false;
            this.percent_bt.Text = "%";
            this.percent_bt.UseVisualStyleBackColor = true;
            this.percent_bt.Click += new System.EventHandler(this.percent_Click);
            this.percent_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // backspace
            // 
            this.backspace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspace.Location = new System.Drawing.Point(13, 98);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(34, 27);
            this.backspace.TabIndex = 22;
            this.backspace.TabStop = false;
            this.backspace.Text = "←";
            this.toolTip2.SetToolTip(this.backspace, "Backspace");
            this.backspace.UseVisualStyleBackColor = true;
            this.backspace.Click += new System.EventHandler(this.backspace_Click);
            this.backspace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // ce
            // 
            this.ce.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ce.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ce.Location = new System.Drawing.Point(52, 98);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(34, 27);
            this.ce.TabIndex = 21;
            this.ce.TabStop = false;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            this.ce.Click += new System.EventHandler(this.ce_Click);
            this.ce.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // doidau
            // 
            this.doidau.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doidau.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doidau.Location = new System.Drawing.Point(130, 98);
            this.doidau.Name = "doidau";
            this.doidau.Size = new System.Drawing.Size(34, 27);
            this.doidau.TabIndex = 11;
            this.doidau.TabStop = false;
            this.doidau.Text = "±";
            this.doidau.UseVisualStyleBackColor = true;
            this.doidau.Click += new System.EventHandler(this.numberinput_Click);
            this.doidau.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // memclear
            // 
            this.memclear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memclear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memclear.Location = new System.Drawing.Point(13, 66);
            this.memclear.Name = "memclear";
            this.memclear.Size = new System.Drawing.Size(34, 27);
            this.memclear.TabIndex = 23;
            this.memclear.TabStop = false;
            this.memclear.Text = "MC";
            this.memclear.UseVisualStyleBackColor = true;
            this.memclear.Click += new System.EventHandler(this.memclear_Click);
            this.memclear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // mstore
            // 
            this.mstore.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mstore.Location = new System.Drawing.Point(52, 66);
            this.mstore.Name = "mstore";
            this.mstore.Size = new System.Drawing.Size(34, 27);
            this.mstore.TabIndex = 24;
            this.mstore.TabStop = false;
            this.mstore.Text = "MS";
            this.mstore.UseVisualStyleBackColor = true;
            this.mstore.Click += new System.EventHandler(this.mstore_Click);
            this.mstore.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // memrecall
            // 
            this.memrecall.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memrecall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memrecall.Location = new System.Drawing.Point(91, 66);
            this.memrecall.Name = "memrecall";
            this.memrecall.Size = new System.Drawing.Size(34, 27);
            this.memrecall.TabIndex = 25;
            this.memrecall.TabStop = false;
            this.memrecall.Text = "MR";
            this.memrecall.UseVisualStyleBackColor = true;
            this.memrecall.Click += new System.EventHandler(this.memrecall_Click);
            this.memrecall.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sqrt_bt
            // 
            this.sqrt_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqrt_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sqrt_bt.Location = new System.Drawing.Point(169, 98);
            this.sqrt_bt.Name = "sqrt_bt";
            this.sqrt_bt.Size = new System.Drawing.Size(34, 27);
            this.sqrt_bt.TabIndex = 19;
            this.sqrt_bt.TabStop = false;
            this.sqrt_bt.Text = "√";
            this.sqrt_bt.UseVisualStyleBackColor = true;
            this.sqrt_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sqrt_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // clearbt
            // 
            this.clearbt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.clearbt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearbt.Location = new System.Drawing.Point(91, 98);
            this.clearbt.Name = "clearbt";
            this.clearbt.Size = new System.Drawing.Size(34, 27);
            this.clearbt.TabIndex = 20;
            this.clearbt.TabStop = false;
            this.clearbt.Text = "C";
            this.clearbt.UseVisualStyleBackColor = true;
            this.clearbt.Click += new System.EventHandler(this.clear_Click);
            this.clearbt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // madd
            // 
            this.madd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.madd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.madd.Location = new System.Drawing.Point(130, 66);
            this.madd.Name = "madd";
            this.madd.Size = new System.Drawing.Size(34, 27);
            this.madd.TabIndex = 26;
            this.madd.TabStop = false;
            this.madd.Text = "M+";
            this.madd.UseVisualStyleBackColor = true;
            this.madd.Click += new System.EventHandler(this.madd_Click);
            this.madd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // mem_minus_bt
            // 
            this.mem_minus_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mem_minus_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_minus_bt.Location = new System.Drawing.Point(169, 66);
            this.mem_minus_bt.Name = "mem_minus_bt";
            this.mem_minus_bt.Size = new System.Drawing.Size(34, 27);
            this.mem_minus_bt.TabIndex = 27;
            this.mem_minus_bt.TabStop = false;
            this.mem_minus_bt.Text = "M-";
            this.mem_minus_bt.UseVisualStyleBackColor = true;
            this.mem_minus_bt.Click += new System.EventHandler(this.mmul_Click);
            this.mem_minus_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // autocal_date
            // 
            this.autocal_date.AutoSize = true;
            this.autocal_date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.autocal_date.Location = new System.Drawing.Point(12, 214);
            this.autocal_date.Name = "autocal_date";
            this.autocal_date.Size = new System.Drawing.Size(96, 17);
            this.autocal_date.TabIndex = 208;
            this.autocal_date.Text = "A&uto Calculate";
            this.autocal_date.UseVisualStyleBackColor = true;
            this.autocal_date.CheckedChanged += new System.EventHandler(this.autocal_cb_CheckedChanged);
            // 
            // calmethodCB
            // 
            this.calmethodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calmethodCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.calmethodCB.FormattingEnabled = true;
            this.calmethodCB.Items.AddRange(new object[] {
            "Calculate the difference between two dates",
            "Add or subtract days to a specified date"});
            this.calmethodCB.Location = new System.Drawing.Point(12, 40);
            this.calmethodCB.Name = "calmethodCB";
            this.calmethodCB.Size = new System.Drawing.Size(330, 21);
            this.calmethodCB.TabIndex = 200;
            this.calmethodCB.SelectedIndexChanged += new System.EventHandler(this.cal_method_SelectedIndexChanged);
            this.calmethodCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typeCB_MouseDown);
            // 
            // calculate_date
            // 
            this.calculate_date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.calculate_date.Location = new System.Drawing.Point(253, 208);
            this.calculate_date.Name = "calculate_date";
            this.calculate_date.Size = new System.Drawing.Size(75, 27);
            this.calculate_date.TabIndex = 209;
            this.calculate_date.TabStop = false;
            this.calculate_date.Text = "Calculate";
            this.calculate_date.UseVisualStyleBackColor = true;
            this.calculate_date.Click += new System.EventHandler(this.calculate_bt_Click);
            // 
            // result2
            // 
            this.result2.BackColor = System.Drawing.Color.White;
            this.result2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.result2.Location = new System.Drawing.Point(12, 168);
            this.result2.Name = "result2";
            this.result2.ReadOnly = true;
            this.result2.Size = new System.Drawing.Size(330, 21);
            this.result2.TabIndex = 207;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(12, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Difference (days)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 13);
            this.label4.TabIndex = 64;
            this.label4.Text = "Difference (years, months, weeks, days)";
            // 
            // dtP2
            // 
            this.dtP2.Checked = false;
            this.dtP2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtP2.Location = new System.Drawing.Point(243, 72);
            this.dtP2.Name = "dtP2";
            this.dtP2.Size = new System.Drawing.Size(99, 21);
            this.dtP2.TabIndex = 202;
            this.dtP2.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(214, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "To:";
            // 
            // dtP1
            // 
            this.dtP1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtP1.Location = new System.Drawing.Point(60, 72);
            this.dtP1.Name = "dtP1";
            this.dtP1.Size = new System.Drawing.Size(99, 21);
            this.dtP1.TabIndex = 201;
            this.dtP1.ValueChanged += new System.EventHandler(this.dtP_ValueChanged);
            this.dtP1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "From:";
            // 
            // result1
            // 
            this.result1.BackColor = System.Drawing.Color.White;
            this.result1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.result1.Location = new System.Drawing.Point(12, 117);
            this.result1.Name = "result1";
            this.result1.ReadOnly = true;
            this.result1.Size = new System.Drawing.Size(330, 21);
            this.result1.TabIndex = 206;
            // 
            // periodsDateUD
            // 
            this.periodsDateUD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.periodsDateUD.Location = new System.Drawing.Point(282, 109);
            this.periodsDateUD.Maximum = new decimal(new int[] {
            730000,
            0,
            0,
            0});
            this.periodsDateUD.Name = "periodsDateUD";
            this.periodsDateUD.Size = new System.Drawing.Size(60, 21);
            this.periodsDateUD.TabIndex = 205;
            this.periodsDateUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsDateUD.ThousandsSeparator = true;
            this.periodsDateUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsDateUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeLB
            // 
            this.typeLB.AutoSize = true;
            this.typeLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeLB.Location = new System.Drawing.Point(12, 17);
            this.typeLB.Name = "typeLB";
            this.typeLB.Size = new System.Drawing.Size(215, 13);
            this.typeLB.TabIndex = 8;
            this.typeLB.Text = "Select the type of unit you want to convert";
            // 
            // fromLB
            // 
            this.fromLB.AutoSize = true;
            this.fromLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromLB.Location = new System.Drawing.Point(12, 70);
            this.fromLB.Name = "fromLB";
            this.fromLB.Size = new System.Drawing.Size(31, 13);
            this.fromLB.TabIndex = 6;
            this.fromLB.Text = "From";
            this.fromLB.Click += new System.EventHandler(this.fromLB_Click);
            // 
            // toLB
            // 
            this.toLB.AutoSize = true;
            this.toLB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toLB.Location = new System.Drawing.Point(12, 147);
            this.toLB.Name = "toLB";
            this.toLB.Size = new System.Drawing.Size(19, 13);
            this.toLB.TabIndex = 7;
            this.toLB.Text = "To";
            // 
            // typeCB
            // 
            this.typeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Items.AddRange(new object[] {
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
            this.typeCB.Location = new System.Drawing.Point(12, 40);
            this.typeCB.MaxDropDownItems = 11;
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(330, 21);
            this.typeCB.TabIndex = 9;
            this.typeCB.SelectedIndexChanged += new System.EventHandler(this.typeCB_SelectedIndexChanged);
            this.typeCB.GotFocus += new System.EventHandler(this.EnableKeyboard);
            // 
            // fromTB
            // 
            this.fromTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.fromTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fromTB.Location = new System.Drawing.Point(12, 86);
            this.fromTB.MaxLength = 20;
            this.fromTB.Name = "fromTB";
            this.fromTB.Size = new System.Drawing.Size(330, 21);
            this.fromTB.TabIndex = 10;
            this.fromTB.Text = "Enter value";
            this.fromTB.TextChanged += new System.EventHandler(this.fromTB_TextChanged);
            this.fromTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.fromTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // toTB
            // 
            this.toTB.BackColor = System.Drawing.SystemColors.Control;
            this.toTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.toTB.Location = new System.Drawing.Point(12, 163);
            this.toTB.Name = "toTB";
            this.toTB.ReadOnly = true;
            this.toTB.Size = new System.Drawing.Size(330, 21);
            this.toTB.TabIndex = 12;
            this.toTB.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // unitconvGB
            // 
            this.unitconvGB.Controls.Add(this.invert_unit);
            this.unitconvGB.Controls.Add(this.toTB);
            this.unitconvGB.Controls.Add(this.typeLB);
            this.unitconvGB.Controls.Add(this.fromTB);
            this.unitconvGB.Controls.Add(this.fromLB);
            this.unitconvGB.Controls.Add(this.toLB);
            this.unitconvGB.Controls.Add(this.typeCB);
            this.unitconvGB.Location = new System.Drawing.Point(236, 3);
            this.unitconvGB.Name = "unitconvGB";
            this.unitconvGB.Size = new System.Drawing.Size(356, 250);
            this.unitconvGB.TabIndex = 31;
            this.unitconvGB.TabStop = false;
            this.unitconvGB.Visible = false;
            this.unitconvGB.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // invert_unit
            // 
            this.invert_unit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.invert_unit.Location = new System.Drawing.Point(11, 217);
            this.invert_unit.Name = "invert_unit";
            this.invert_unit.Size = new System.Drawing.Size(61, 27);
            this.invert_unit.TabIndex = 14;
            this.invert_unit.Text = "&Invert";
            this.invert_unit.UseVisualStyleBackColor = true;
            this.invert_unit.Click += new System.EventHandler(this.invert_unit_Click);
            this.invert_unit.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // datecalcGB
            // 
            this.datecalcGB.Controls.Add(this.label3);
            this.datecalcGB.Controls.Add(this.dtP2);
            this.datecalcGB.Controls.Add(this.subrb);
            this.datecalcGB.Controls.Add(this.addrb);
            this.datecalcGB.Controls.Add(this.label6);
            this.datecalcGB.Controls.Add(this.autocal_date);
            this.datecalcGB.Controls.Add(this.calculate_date);
            this.datecalcGB.Controls.Add(this.calmethodCB);
            this.datecalcGB.Controls.Add(this.result1);
            this.datecalcGB.Controls.Add(this.label8);
            this.datecalcGB.Controls.Add(this.label7);
            this.datecalcGB.Controls.Add(this.label1);
            this.datecalcGB.Controls.Add(this.label4);
            this.datecalcGB.Controls.Add(this.label2);
            this.datecalcGB.Controls.Add(this.label5);
            this.datecalcGB.Controls.Add(this.dtP1);
            this.datecalcGB.Controls.Add(this.result2);
            this.datecalcGB.Controls.Add(this.periodsYearUD);
            this.datecalcGB.Controls.Add(this.periodsMonthUD);
            this.datecalcGB.Controls.Add(this.periodsDateUD);
            this.datecalcGB.Location = new System.Drawing.Point(236, 3);
            this.datecalcGB.Name = "datecalcGB";
            this.datecalcGB.Size = new System.Drawing.Size(356, 250);
            this.datecalcGB.TabIndex = 32;
            this.datecalcGB.TabStop = false;
            this.datecalcGB.Visible = false;
            this.datecalcGB.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // subrb
            // 
            this.subrb.AutoSize = true;
            this.subrb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.subrb.Location = new System.Drawing.Point(254, 73);
            this.subrb.Name = "subrb";
            this.subrb.Size = new System.Drawing.Size(66, 17);
            this.subrb.TabIndex = 202;
            this.subrb.Text = "Subtract";
            this.subrb.UseVisualStyleBackColor = true;
            this.subrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // addrb
            // 
            this.addrb.AutoSize = true;
            this.addrb.Checked = true;
            this.addrb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.addrb.Location = new System.Drawing.Point(176, 73);
            this.addrb.Name = "addrb";
            this.addrb.Size = new System.Drawing.Size(44, 17);
            this.addrb.TabIndex = 202;
            this.addrb.TabStop = true;
            this.addrb.Text = "Add";
            this.addrb.UseVisualStyleBackColor = true;
            this.addrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(12, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Select the date calculation you want";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(240, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 64;
            this.label8.Text = "Day:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(110, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Month:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Year:";
            // 
            // periodsYearUD
            // 
            this.periodsYearUD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.periodsYearUD.Location = new System.Drawing.Point(60, 109);
            this.periodsYearUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.periodsYearUD.Name = "periodsYearUD";
            this.periodsYearUD.Size = new System.Drawing.Size(44, 21);
            this.periodsYearUD.TabIndex = 203;
            this.periodsYearUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsYearUD.ThousandsSeparator = true;
            this.periodsYearUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsYearUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsMonthUD
            // 
            this.periodsMonthUD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.periodsMonthUD.Location = new System.Drawing.Point(169, 109);
            this.periodsMonthUD.Maximum = new decimal(new int[] {
            24000,
            0,
            0,
            0});
            this.periodsMonthUD.Name = "periodsMonthUD";
            this.periodsMonthUD.Size = new System.Drawing.Size(44, 21);
            this.periodsMonthUD.TabIndex = 204;
            this.periodsMonthUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsMonthUD.ThousandsSeparator = true;
            this.periodsMonthUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsMonthUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // angleGB
            // 
            this.angleGB.Controls.Add(this.gra_rb);
            this.angleGB.Controls.Add(this.rad_rb);
            this.angleGB.Controls.Add(this.deg_rb);
            this.angleGB.Location = new System.Drawing.Point(12, 270);
            this.angleGB.Name = "angleGB";
            this.angleGB.Size = new System.Drawing.Size(190, 33);
            this.angleGB.TabIndex = 128;
            this.angleGB.TabStop = false;
            // 
            // gra_rb
            // 
            this.gra_rb.AutoSize = true;
            this.gra_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gra_rb.Location = new System.Drawing.Point(134, 13);
            this.gra_rb.Name = "gra_rb";
            this.gra_rb.Size = new System.Drawing.Size(53, 17);
            this.gra_rb.TabIndex = 116;
            this.gra_rb.Text = "Grads";
            this.gra_rb.UseVisualStyleBackColor = true;
            // 
            // rad_rb
            // 
            this.rad_rb.AutoSize = true;
            this.rad_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_rb.Location = new System.Drawing.Point(72, 13);
            this.rad_rb.Name = "rad_rb";
            this.rad_rb.Size = new System.Drawing.Size(63, 17);
            this.rad_rb.TabIndex = 115;
            this.rad_rb.Text = "Radians";
            this.rad_rb.UseVisualStyleBackColor = true;
            // 
            // deg_rb
            // 
            this.deg_rb.AutoSize = true;
            this.deg_rb.Checked = true;
            this.deg_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deg_rb.Location = new System.Drawing.Point(8, 13);
            this.deg_rb.Name = "deg_rb";
            this.deg_rb.Size = new System.Drawing.Size(65, 17);
            this.deg_rb.TabIndex = 114;
            this.deg_rb.TabStop = true;
            this.deg_rb.Text = "Degrees";
            this.deg_rb.UseVisualStyleBackColor = true;
            // 
            // close_bracket
            // 
            this.close_bracket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_bracket.Location = new System.Drawing.Point(56, 322);
            this.close_bracket.Name = "close_bracket";
            this.close_bracket.Size = new System.Drawing.Size(34, 27);
            this.close_bracket.TabIndex = 152;
            this.close_bracket.TabStop = false;
            this.close_bracket.Text = ")";
            this.close_bracket.UseVisualStyleBackColor = true;
            this.close_bracket.Click += new System.EventHandler(this.close_bracket_Click);
            this.close_bracket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // open_bracket
            // 
            this.open_bracket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.open_bracket.Location = new System.Drawing.Point(56, 322);
            this.open_bracket.Name = "open_bracket";
            this.open_bracket.Size = new System.Drawing.Size(34, 27);
            this.open_bracket.TabIndex = 153;
            this.open_bracket.TabStop = false;
            this.open_bracket.Text = "(";
            this.open_bracket.UseVisualStyleBackColor = true;
            this.open_bracket.Click += new System.EventHandler(this.open_bracket_Click);
            this.open_bracket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // _10x_bt
            // 
            this._10x_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._10x_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._10x_bt.Location = new System.Drawing.Point(56, 322);
            this._10x_bt.Name = "_10x_bt";
            this._10x_bt.Size = new System.Drawing.Size(34, 27);
            this._10x_bt.TabIndex = 40;
            this._10x_bt.TabStop = false;
            this._10x_bt.Text = "10ⁿ";
            this._10x_bt.UseVisualStyleBackColor = true;
            this._10x_bt.Click += new System.EventHandler(this.functionBT_Click);
            this._10x_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // nvx_bt
            // 
            this.nvx_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nvx_bt.Location = new System.Drawing.Point(56, 322);
            this.nvx_bt.Name = "nvx_bt";
            this.nvx_bt.Size = new System.Drawing.Size(34, 27);
            this.nvx_bt.TabIndex = 34;
            this.nvx_bt.TabStop = false;
            this.nvx_bt.Text = "ⁿ√x";
            this.nvx_bt.UseVisualStyleBackColor = true;
            this.nvx_bt.Click += new System.EventHandler(this.nvx_bt_Click);
            this.nvx_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // xn_bt
            // 
            this.xn_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xn_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xn_bt.Location = new System.Drawing.Point(56, 322);
            this.xn_bt.Name = "xn_bt";
            this.xn_bt.Size = new System.Drawing.Size(34, 27);
            this.xn_bt.TabIndex = 30;
            this.xn_bt.TabStop = false;
            this.xn_bt.Text = "xⁿ";
            this.xn_bt.UseVisualStyleBackColor = true;
            this.xn_bt.Click += new System.EventHandler(this.xn_bt_Click);
            this.xn_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // log_bt
            // 
            this.log_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.log_bt.Location = new System.Drawing.Point(56, 322);
            this.log_bt.Name = "log_bt";
            this.log_bt.Size = new System.Drawing.Size(34, 27);
            this.log_bt.TabIndex = 42;
            this.log_bt.TabStop = false;
            this.log_bt.Text = "log";
            this.log_bt.UseVisualStyleBackColor = true;
            this.log_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.log_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // _3vx_bt
            // 
            this._3vx_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._3vx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._3vx_bt.Location = new System.Drawing.Point(56, 322);
            this._3vx_bt.Name = "_3vx_bt";
            this._3vx_bt.Size = new System.Drawing.Size(34, 27);
            this._3vx_bt.TabIndex = 41;
            this._3vx_bt.TabStop = false;
            this._3vx_bt.Text = "³√x";
            this._3vx_bt.UseVisualStyleBackColor = true;
            this._3vx_bt.Click += new System.EventHandler(this.functionBT_Click);
            this._3vx_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // x3_bt
            // 
            this.x3_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x3_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x3_bt.Location = new System.Drawing.Point(56, 322);
            this.x3_bt.Name = "x3_bt";
            this.x3_bt.Size = new System.Drawing.Size(34, 27);
            this.x3_bt.TabIndex = 39;
            this.x3_bt.TabStop = false;
            this.x3_bt.Text = "x³";
            this.x3_bt.UseVisualStyleBackColor = true;
            this.x3_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.x3_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // x2_bt
            // 
            this.x2_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x2_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2_bt.Location = new System.Drawing.Point(56, 322);
            this.x2_bt.Name = "x2_bt";
            this.x2_bt.Size = new System.Drawing.Size(34, 27);
            this.x2_bt.TabIndex = 38;
            this.x2_bt.TabStop = false;
            this.x2_bt.Text = "x²";
            this.x2_bt.UseVisualStyleBackColor = true;
            this.x2_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.x2_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // pi_bt
            // 
            this.pi_bt.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pi_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pi_bt.Location = new System.Drawing.Point(56, 322);
            this.pi_bt.Name = "pi_bt";
            this.pi_bt.Size = new System.Drawing.Size(34, 27);
            this.pi_bt.TabIndex = 144;
            this.pi_bt.TabStop = false;
            this.pi_bt.Text = "π";
            this.pi_bt.UseVisualStyleBackColor = true;
            this.pi_bt.Click += new System.EventHandler(this.pi_bt_Click);
            this.pi_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // exp_bt
            // 
            this.exp_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exp_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exp_bt.Location = new System.Drawing.Point(56, 322);
            this.exp_bt.Name = "exp_bt";
            this.exp_bt.Size = new System.Drawing.Size(34, 27);
            this.exp_bt.TabIndex = 33;
            this.exp_bt.TabStop = false;
            this.exp_bt.Text = "Exp";
            this.exp_bt.UseVisualStyleBackColor = true;
            this.exp_bt.Click += new System.EventHandler(this.exp_bt_Click);
            this.exp_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // modsciBT
            // 
            this.modsciBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.modsciBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modsciBT.Location = new System.Drawing.Point(56, 322);
            this.modsciBT.Name = "modsciBT";
            this.modsciBT.Size = new System.Drawing.Size(34, 27);
            this.modsciBT.TabIndex = 142;
            this.modsciBT.TabStop = false;
            this.modsciBT.Text = "Mod";
            this.modsciBT.UseVisualStyleBackColor = true;
            this.modsciBT.Click += new System.EventHandler(this.operatorBT_Click);
            this.modsciBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // ln_bt
            // 
            this.ln_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ln_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ln_bt.Location = new System.Drawing.Point(56, 322);
            this.ln_bt.Name = "ln_bt";
            this.ln_bt.Size = new System.Drawing.Size(34, 27);
            this.ln_bt.TabIndex = 31;
            this.ln_bt.TabStop = false;
            this.ln_bt.Text = "ln";
            this.ln_bt.UseVisualStyleBackColor = true;
            this.ln_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.ln_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // dms_bt
            // 
            this.dms_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dms_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dms_bt.Location = new System.Drawing.Point(56, 322);
            this.dms_bt.Name = "dms_bt";
            this.dms_bt.Size = new System.Drawing.Size(34, 27);
            this.dms_bt.TabIndex = 45;
            this.dms_bt.TabStop = false;
            this.dms_bt.Text = "dms";
            this.dms_bt.UseVisualStyleBackColor = true;
            this.dms_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.dms_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // tanh_bt
            // 
            this.tanh_bt.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.tanh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tanh_bt.Location = new System.Drawing.Point(56, 322);
            this.tanh_bt.Name = "tanh_bt";
            this.tanh_bt.Size = new System.Drawing.Size(34, 27);
            this.tanh_bt.TabIndex = 37;
            this.tanh_bt.TabStop = false;
            this.tanh_bt.Text = "tanh";
            this.tanh_bt.UseVisualStyleBackColor = true;
            this.tanh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.tanh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // cosh_bt
            // 
            this.cosh_bt.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.cosh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cosh_bt.Location = new System.Drawing.Point(56, 322);
            this.cosh_bt.Name = "cosh_bt";
            this.cosh_bt.Size = new System.Drawing.Size(34, 27);
            this.cosh_bt.TabIndex = 36;
            this.cosh_bt.TabStop = false;
            this.cosh_bt.Text = "cosh";
            this.cosh_bt.UseVisualStyleBackColor = true;
            this.cosh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.cosh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // int_bt
            // 
            this.int_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.int_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.int_bt.Location = new System.Drawing.Point(56, 322);
            this.int_bt.Name = "int_bt";
            this.int_bt.Size = new System.Drawing.Size(34, 27);
            this.int_bt.TabIndex = 44;
            this.int_bt.TabStop = false;
            this.int_bt.Text = "Int";
            this.int_bt.UseVisualStyleBackColor = true;
            this.int_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.int_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // tan_bt
            // 
            this.tan_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tan_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tan_bt.Location = new System.Drawing.Point(56, 322);
            this.tan_bt.Name = "tan_bt";
            this.tan_bt.Size = new System.Drawing.Size(34, 27);
            this.tan_bt.TabIndex = 30;
            this.tan_bt.TabStop = false;
            this.tan_bt.Text = "tan";
            this.tan_bt.UseVisualStyleBackColor = true;
            this.tan_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.tan_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sinh_bt
            // 
            this.sinh_bt.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.sinh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sinh_bt.Location = new System.Drawing.Point(56, 322);
            this.sinh_bt.Name = "sinh_bt";
            this.sinh_bt.Size = new System.Drawing.Size(34, 27);
            this.sinh_bt.TabIndex = 35;
            this.sinh_bt.TabStop = false;
            this.sinh_bt.Text = "sinh";
            this.sinh_bt.UseVisualStyleBackColor = true;
            this.sinh_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sinh_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btFactorial
            // 
            this.btFactorial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFactorial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btFactorial.Location = new System.Drawing.Point(56, 322);
            this.btFactorial.Name = "btFactorial";
            this.btFactorial.Size = new System.Drawing.Size(34, 27);
            this.btFactorial.TabIndex = 32;
            this.btFactorial.TabStop = false;
            this.btFactorial.Text = "n!";
            this.btFactorial.UseVisualStyleBackColor = true;
            this.btFactorial.Click += new System.EventHandler(this.functionBT_Click);
            this.btFactorial.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // unknownPN
            // 
            this.unknownPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unknownPN.Controls.Add(this._byteRB);
            this.unknownPN.Controls.Add(this.qwordRB);
            this.unknownPN.Controls.Add(this._wordRB);
            this.unknownPN.Controls.Add(this.dwordRB);
            this.unknownPN.Location = new System.Drawing.Point(98, 356);
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
            this._byteRB.TabIndex = 1;
            this._byteRB.Text = "Byte";
            this._byteRB.UseVisualStyleBackColor = true;
            this._byteRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // qwordRB
            // 
            this.qwordRB.AutoSize = true;
            this.qwordRB.Checked = true;
            this.qwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qwordRB.Location = new System.Drawing.Point(7, 4);
            this.qwordRB.Name = "qwordRB";
            this.qwordRB.Size = new System.Drawing.Size(57, 17);
            this.qwordRB.TabIndex = 6;
            this.qwordRB.TabStop = true;
            this.qwordRB.Text = "Qword";
            this.qwordRB.UseVisualStyleBackColor = true;
            this.qwordRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // _wordRB
            // 
            this._wordRB.AutoSize = true;
            this._wordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._wordRB.Location = new System.Drawing.Point(7, 46);
            this._wordRB.Name = "_wordRB";
            this._wordRB.Size = new System.Drawing.Size(51, 17);
            this._wordRB.TabIndex = 2;
            this._wordRB.Text = "Word";
            this._wordRB.UseVisualStyleBackColor = true;
            this._wordRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // dwordRB
            // 
            this.dwordRB.AutoSize = true;
            this.dwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwordRB.Location = new System.Drawing.Point(7, 25);
            this.dwordRB.Name = "dwordRB";
            this.dwordRB.Size = new System.Drawing.Size(56, 17);
            this.dwordRB.TabIndex = 3;
            this.dwordRB.Text = "Dword";
            this.dwordRB.UseVisualStyleBackColor = true;
            this.dwordRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // basePN
            // 
            this.basePN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.basePN.Controls.Add(this.binRB);
            this.basePN.Controls.Add(this.octRB);
            this.basePN.Controls.Add(this.decRB);
            this.basePN.Controls.Add(this.hexRB);
            this.basePN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.basePN.Location = new System.Drawing.Point(12, 356);
            this.basePN.Name = "basePN";
            this.basePN.Size = new System.Drawing.Size(73, 91);
            this.basePN.TabIndex = 214;
            this.basePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // binRB
            // 
            this.binRB.AutoSize = true;
            this.binRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binRB.Location = new System.Drawing.Point(7, 67);
            this.binRB.Name = "binRB";
            this.binRB.Size = new System.Drawing.Size(39, 17);
            this.binRB.TabIndex = 119;
            this.binRB.Text = "Bin";
            this.binRB.UseVisualStyleBackColor = true;
            this.binRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // octRB
            // 
            this.octRB.AutoSize = true;
            this.octRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.octRB.Location = new System.Drawing.Point(7, 46);
            this.octRB.Name = "octRB";
            this.octRB.Size = new System.Drawing.Size(42, 17);
            this.octRB.TabIndex = 118;
            this.octRB.Text = "Oct";
            this.octRB.UseVisualStyleBackColor = true;
            this.octRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // decRB
            // 
            this.decRB.AutoSize = true;
            this.decRB.Checked = true;
            this.decRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decRB.Location = new System.Drawing.Point(7, 25);
            this.decRB.Name = "decRB";
            this.decRB.Size = new System.Drawing.Size(43, 17);
            this.decRB.TabIndex = 117;
            this.decRB.TabStop = true;
            this.decRB.Text = "Dec";
            this.decRB.UseVisualStyleBackColor = true;
            this.decRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // hexRB
            // 
            this.hexRB.AutoSize = true;
            this.hexRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexRB.Location = new System.Drawing.Point(7, 4);
            this.hexRB.Name = "hexRB";
            this.hexRB.Size = new System.Drawing.Size(44, 17);
            this.hexRB.TabIndex = 116;
            this.hexRB.Text = "Hex";
            this.hexRB.UseVisualStyleBackColor = true;
            this.hexRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // PNbinary
            // 
            this.PNbinary.BackColor = System.Drawing.Color.Transparent;
            this.PNbinary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNbinary.Location = new System.Drawing.Point(186, 397);
            this.PNbinary.Name = "PNbinary";
            this.PNbinary.Size = new System.Drawing.Size(385, 60);
            this.PNbinary.TabIndex = 212;
            // 
            // btnF
            // 
            this.btnF.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnF.Location = new System.Drawing.Point(99, 322);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(34, 27);
            this.btnF.TabIndex = 208;
            this.btnF.TabStop = false;
            this.btnF.Text = "F";
            this.btnF.UseVisualStyleBackColor = true;
            this.btnF.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnF.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btnB
            // 
            this.btnB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnB.Location = new System.Drawing.Point(99, 322);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(34, 27);
            this.btnB.TabIndex = 206;
            this.btnB.TabStop = false;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btnD
            // 
            this.btnD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnD.Location = new System.Drawing.Point(99, 322);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(34, 27);
            this.btnD.TabIndex = 207;
            this.btnD.TabStop = false;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnD.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // XorBT
            // 
            this.XorBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XorBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.XorBT.Location = new System.Drawing.Point(99, 322);
            this.XorBT.Name = "XorBT";
            this.XorBT.Size = new System.Drawing.Size(34, 27);
            this.XorBT.TabIndex = 205;
            this.XorBT.TabStop = false;
            this.XorBT.Text = "Xor";
            this.XorBT.UseVisualStyleBackColor = true;
            this.XorBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.XorBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // NotBT
            // 
            this.NotBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotBT.Location = new System.Drawing.Point(99, 322);
            this.NotBT.Name = "NotBT";
            this.NotBT.Size = new System.Drawing.Size(34, 27);
            this.NotBT.TabIndex = 203;
            this.NotBT.TabStop = false;
            this.NotBT.Text = "Not";
            this.NotBT.UseVisualStyleBackColor = true;
            this.NotBT.Click += new System.EventHandler(this.notBT_Click);
            this.NotBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // AndBT
            // 
            this.AndBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AndBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AndBT.Location = new System.Drawing.Point(99, 322);
            this.AndBT.Name = "AndBT";
            this.AndBT.Size = new System.Drawing.Size(34, 27);
            this.AndBT.TabIndex = 204;
            this.AndBT.TabStop = false;
            this.AndBT.Text = "And";
            this.AndBT.UseVisualStyleBackColor = true;
            this.AndBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.AndBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnE.Location = new System.Drawing.Point(99, 322);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(34, 27);
            this.btnE.TabIndex = 209;
            this.btnE.TabStop = false;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // RshBT
            // 
            this.RshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RshBT.Location = new System.Drawing.Point(99, 322);
            this.RshBT.Name = "RshBT";
            this.RshBT.Size = new System.Drawing.Size(34, 27);
            this.RshBT.TabIndex = 210;
            this.RshBT.TabStop = false;
            this.RshBT.Text = "Rsh";
            this.RshBT.UseVisualStyleBackColor = true;
            this.RshBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.RshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // RoRBT
            // 
            this.RoRBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.RoRBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoRBT.Location = new System.Drawing.Point(99, 322);
            this.RoRBT.Name = "RoRBT";
            this.RoRBT.Size = new System.Drawing.Size(34, 27);
            this.RoRBT.TabIndex = 211;
            this.RoRBT.TabStop = false;
            this.RoRBT.Text = "RoR";
            this.RoRBT.UseVisualStyleBackColor = true;
            this.RoRBT.Click += new System.EventHandler(this.rotateBT_Click);
            this.RoRBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // LshBT
            // 
            this.LshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LshBT.Location = new System.Drawing.Point(99, 322);
            this.LshBT.Name = "LshBT";
            this.LshBT.Size = new System.Drawing.Size(34, 27);
            this.LshBT.TabIndex = 200;
            this.LshBT.TabStop = false;
            this.LshBT.Text = "Lsh";
            this.LshBT.UseVisualStyleBackColor = true;
            this.LshBT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.LshBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // or_BT
            // 
            this.or_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.or_BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.or_BT.Location = new System.Drawing.Point(99, 322);
            this.or_BT.Name = "or_BT";
            this.or_BT.Size = new System.Drawing.Size(34, 27);
            this.or_BT.TabIndex = 199;
            this.or_BT.TabStop = false;
            this.or_BT.Text = "Or";
            this.or_BT.UseVisualStyleBackColor = true;
            this.or_BT.Click += new System.EventHandler(this.bitOperatorsBT_Click);
            this.or_BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // RoLBT
            // 
            this.RoLBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoLBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoLBT.Location = new System.Drawing.Point(99, 322);
            this.RoLBT.Name = "RoLBT";
            this.RoLBT.Size = new System.Drawing.Size(34, 27);
            this.RoLBT.TabIndex = 198;
            this.RoLBT.TabStop = false;
            this.RoLBT.Text = "RoL";
            this.RoLBT.UseVisualStyleBackColor = true;
            this.RoLBT.Click += new System.EventHandler(this.rotateBT_Click);
            this.RoLBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btnA
            // 
            this.btnA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnA.Location = new System.Drawing.Point(99, 322);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(34, 27);
            this.btnA.TabIndex = 201;
            this.btnA.TabStop = false;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnC.Location = new System.Drawing.Point(99, 322);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(34, 27);
            this.btnC.TabIndex = 202;
            this.btnC.TabStop = false;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.buttonAF_Click);
            this.btnC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // openproBT
            // 
            this.openproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openproBT.Location = new System.Drawing.Point(99, 322);
            this.openproBT.Name = "openproBT";
            this.openproBT.Size = new System.Drawing.Size(34, 27);
            this.openproBT.TabIndex = 199;
            this.openproBT.TabStop = false;
            this.openproBT.Text = "(";
            this.openproBT.UseVisualStyleBackColor = true;
            this.openproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // modproBT
            // 
            this.modproBT.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.modproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modproBT.Location = new System.Drawing.Point(99, 322);
            this.modproBT.Name = "modproBT";
            this.modproBT.Size = new System.Drawing.Size(34, 27);
            this.modproBT.TabIndex = 211;
            this.modproBT.TabStop = false;
            this.modproBT.Text = "Mod";
            this.modproBT.UseVisualStyleBackColor = true;
            this.modproBT.Click += new System.EventHandler(this.modproBT_Click);
            this.modproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // closeproBT
            // 
            this.closeproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.closeproBT.Location = new System.Drawing.Point(99, 322);
            this.closeproBT.Name = "closeproBT";
            this.closeproBT.Size = new System.Drawing.Size(34, 27);
            this.closeproBT.TabIndex = 205;
            this.closeproBT.TabStop = false;
            this.closeproBT.Text = ")";
            this.closeproBT.UseVisualStyleBackColor = true;
            this.closeproBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // Expsta_bt
            // 
            this.Expsta_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expsta_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Expsta_bt.Location = new System.Drawing.Point(141, 322);
            this.Expsta_bt.Name = "Expsta_bt";
            this.Expsta_bt.Size = new System.Drawing.Size(34, 27);
            this.Expsta_bt.TabIndex = 247;
            this.Expsta_bt.TabStop = false;
            this.Expsta_bt.Text = "Exp";
            this.Expsta_bt.UseVisualStyleBackColor = true;
            this.Expsta_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sigmax2BT
            // 
            this.sigmax2BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmax2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmax2BT.Image = ((System.Drawing.Image)(resources.GetObject("sigmax2BT.Image")));
            this.sigmax2BT.Location = new System.Drawing.Point(141, 322);
            this.sigmax2BT.Name = "sigmax2BT";
            this.sigmax2BT.Size = new System.Drawing.Size(34, 27);
            this.sigmax2BT.TabIndex = 245;
            this.sigmax2BT.TabStop = false;
            this.sigmax2BT.UseVisualStyleBackColor = true;
            this.sigmax2BT.Click += new System.EventHandler(this.sigmax2BT_Click);
            this.sigmax2BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sigman_1BT
            // 
            this.sigman_1BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigman_1BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigman_1BT.Image = ((System.Drawing.Image)(resources.GetObject("sigman_1BT.Image")));
            this.sigman_1BT.Location = new System.Drawing.Point(141, 322);
            this.sigman_1BT.Name = "sigman_1BT";
            this.sigman_1BT.Size = new System.Drawing.Size(34, 27);
            this.sigman_1BT.TabIndex = 244;
            this.sigman_1BT.TabStop = false;
            this.sigman_1BT.UseVisualStyleBackColor = true;
            this.sigman_1BT.Click += new System.EventHandler(this.sigman_1BT_Click);
            this.sigman_1BT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // AddstaBT
            // 
            this.AddstaBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddstaBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddstaBT.Location = new System.Drawing.Point(141, 322);
            this.AddstaBT.Name = "AddstaBT";
            this.AddstaBT.Size = new System.Drawing.Size(34, 27);
            this.AddstaBT.TabIndex = 243;
            this.AddstaBT.TabStop = false;
            this.AddstaBT.Text = "Add";
            this.AddstaBT.UseVisualStyleBackColor = true;
            this.AddstaBT.Click += new System.EventHandler(this.AddstaBT_Click);
            this.AddstaBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // xcross
            // 
            this.xcross.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xcross.Image = ((System.Drawing.Image)(resources.GetObject("xcross.Image")));
            this.xcross.Location = new System.Drawing.Point(141, 322);
            this.xcross.Name = "xcross";
            this.xcross.Size = new System.Drawing.Size(34, 27);
            this.xcross.TabIndex = 242;
            this.xcross.TabStop = false;
            this.xcross.UseVisualStyleBackColor = true;
            this.xcross.Click += new System.EventHandler(this.xcross_Click);
            this.xcross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sigmaxBT
            // 
            this.sigmaxBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmaxBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmaxBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmaxBT.Image")));
            this.sigmaxBT.Location = new System.Drawing.Point(141, 322);
            this.sigmaxBT.Name = "sigmaxBT";
            this.sigmaxBT.Size = new System.Drawing.Size(34, 27);
            this.sigmaxBT.TabIndex = 241;
            this.sigmaxBT.TabStop = false;
            this.sigmaxBT.UseVisualStyleBackColor = true;
            this.sigmaxBT.Click += new System.EventHandler(this.sigmaxBT_Click);
            this.sigmaxBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sigmanBT
            // 
            this.sigmanBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmanBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmanBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmanBT.Image")));
            this.sigmanBT.Location = new System.Drawing.Point(141, 322);
            this.sigmanBT.Name = "sigmanBT";
            this.sigmanBT.Size = new System.Drawing.Size(34, 27);
            this.sigmanBT.TabIndex = 240;
            this.sigmanBT.TabStop = false;
            this.sigmanBT.UseVisualStyleBackColor = true;
            this.sigmanBT.Click += new System.EventHandler(this.sigmanBT_Click);
            this.sigmanBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // CAD
            // 
            this.CAD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CAD.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.CAD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CAD.Location = new System.Drawing.Point(141, 322);
            this.CAD.Name = "CAD";
            this.CAD.Size = new System.Drawing.Size(34, 27);
            this.CAD.TabIndex = 248;
            this.CAD.TabStop = false;
            this.CAD.Text = "CAD";
            this.CAD.UseVisualStyleBackColor = true;
            this.CAD.Click += new System.EventHandler(this.CAD_Click);
            this.CAD.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // x2cross
            // 
            this.x2cross.BackColor = System.Drawing.SystemColors.Control;
            this.x2cross.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x2cross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2cross.Image = ((System.Drawing.Image)(resources.GetObject("x2cross.Image")));
            this.x2cross.Location = new System.Drawing.Point(141, 322);
            this.x2cross.Name = "x2cross";
            this.x2cross.Size = new System.Drawing.Size(34, 27);
            this.x2cross.TabIndex = 246;
            this.x2cross.TabStop = false;
            this.x2cross.UseVisualStyleBackColor = false;
            this.x2cross.Click += new System.EventHandler(this.x2cross_Click);
            this.x2cross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // clearsta
            // 
            this.clearsta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearsta.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearsta.Location = new System.Drawing.Point(141, 322);
            this.clearsta.Name = "clearsta";
            this.clearsta.Size = new System.Drawing.Size(34, 27);
            this.clearsta.TabIndex = 20;
            this.clearsta.TabStop = false;
            this.clearsta.Text = "C";
            this.clearsta.UseVisualStyleBackColor = true;
            this.clearsta.Click += new System.EventHandler(this.clear_Click);
            this.clearsta.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // inv_ChkBox
            // 
            this.inv_ChkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.inv_ChkBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inv_ChkBox.Location = new System.Drawing.Point(186, 322);
            this.inv_ChkBox.Name = "inv_ChkBox";
            this.inv_ChkBox.Size = new System.Drawing.Size(34, 27);
            this.inv_ChkBox.TabIndex = 250;
            this.inv_ChkBox.TabStop = false;
            this.inv_ChkBox.Text = "Inv";
            this.inv_ChkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.inv_ChkBox.UseVisualStyleBackColor = true;
            this.inv_ChkBox.CheckedChanged += new System.EventHandler(this.inv_ChkBox_CheckedChanged);
            // 
            // dnBT
            // 
            this.dnBT.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnBT.Location = new System.Drawing.Point(167, 3);
            this.dnBT.Name = "dnBT";
            this.dnBT.Size = new System.Drawing.Size(17, 16);
            this.dnBT.TabIndex = 1;
            this.dnBT.Text = "▼";
            this.toolTip2.SetToolTip(this.dnBT, "Down");
            this.dnBT.UseVisualStyleBackColor = true;
            this.dnBT.Click += new System.EventHandler(this.dnBT_Click);
            this.dnBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // upBT
            // 
            this.upBT.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upBT.Location = new System.Drawing.Point(147, 3);
            this.upBT.Name = "upBT";
            this.upBT.Size = new System.Drawing.Size(17, 16);
            this.upBT.TabIndex = 1;
            this.upBT.Text = "▲";
            this.toolTip2.SetToolTip(this.upBT, "Up");
            this.upBT.UseVisualStyleBackColor = true;
            this.upBT.Click += new System.EventHandler(this.upBT_Click);
            this.upBT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // fe_ChkBox
            // 
            this.fe_ChkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fe_ChkBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fe_ChkBox.Location = new System.Drawing.Point(186, 321);
            this.fe_ChkBox.Name = "fe_ChkBox";
            this.fe_ChkBox.Size = new System.Drawing.Size(34, 27);
            this.fe_ChkBox.TabIndex = 43;
            this.fe_ChkBox.TabStop = false;
            this.fe_ChkBox.Text = "F-E";
            this.fe_ChkBox.UseVisualStyleBackColor = true;
            this.fe_ChkBox.CheckedChanged += new System.EventHandler(this.fe_ChkBox_CheckChanged);
            // 
            // bracketTime_lb
            // 
            this.bracketTime_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bracketTime_lb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bracketTime_lb.Location = new System.Drawing.Point(13, 322);
            this.bracketTime_lb.Name = "bracketTime_lb";
            this.bracketTime_lb.Size = new System.Drawing.Size(34, 27);
            this.bracketTime_lb.TabIndex = 129;
            this.bracketTime_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bracketTime_lb.TextChanged += new System.EventHandler(this.bracketTime_lb_TextChanged);
            // 
            // cos_bt
            // 
            this.cos_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cos_bt.Location = new System.Drawing.Point(55, 322);
            this.cos_bt.Name = "cos_bt";
            this.cos_bt.Size = new System.Drawing.Size(34, 27);
            this.cos_bt.TabIndex = 29;
            this.cos_bt.TabStop = false;
            this.cos_bt.Text = "cos";
            this.cos_bt.UseCompatibleTextRendering = true;
            this.cos_bt.UseVisualStyleBackColor = true;
            this.cos_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.cos_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            // 
            // sin_bt
            // 
            this.sin_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sin_bt.Location = new System.Drawing.Point(56, 322);
            this.sin_bt.Name = "sin_bt";
            this.sin_bt.Size = new System.Drawing.Size(34, 27);
            this.sin_bt.TabIndex = 28;
            this.sin_bt.TabStop = false;
            this.sin_bt.Text = "sin";
            this.sin_bt.UseVisualStyleBackColor = true;
            this.sin_bt.Click += new System.EventHandler(this.functionBT_Click);
            this.sin_bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
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
            this.standardMI.Checked = true;
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
            this.unitConversionMI.Click += new System.EventHandler(this.unitConversionMI_Click);
            // 
            // dateCalculationMI
            // 
            this.dateCalculationMI.Index = 10;
            this.dateCalculationMI.RadioCheck = true;
            this.dateCalculationMI.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.dateCalculationMI.Text = "&Date calculation";
            this.dateCalculationMI.Click += new System.EventHandler(this.dateCalculationMI_Click);
            // 
            // worksheetsMI
            // 
            this.worksheetsMI.Index = 11;
            this.worksheetsMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mortgageMI,
            this.vehicleLeaseMI,
            this.feMPG_MI,
            this.fe_L100_MI});
            this.worksheetsMI.Text = "&Worksheets";
            // 
            // mortgageMI
            // 
            this.mortgageMI.Index = 0;
            this.mortgageMI.RadioCheck = true;
            this.mortgageMI.Text = "&Mortgage";
            // 
            // vehicleLeaseMI
            // 
            this.vehicleLeaseMI.Index = 1;
            this.vehicleLeaseMI.RadioCheck = true;
            this.vehicleLeaseMI.Text = "&Vehicle lease";
            // 
            // feMPG_MI
            // 
            this.feMPG_MI.Index = 2;
            this.feMPG_MI.RadioCheck = true;
            this.feMPG_MI.Text = "&Fuel economy (mpg)";
            // 
            // fe_L100_MI
            // 
            this.fe_L100_MI.Index = 3;
            this.fe_L100_MI.RadioCheck = true;
            this.fe_L100_MI.Text = "F&uel economy (L/100 km)";
            // 
            // editMI
            // 
            this.editMI.Index = 1;
            this.editMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyMI,
            this.pasteMI,
            this.sepMI3,
            this.historyOptionMI,
            this.datasetMI});
            this.editMI.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.editMI.Text = "&Edit";
            // 
            // copyMI
            // 
            this.copyMI.Index = 0;
            this.copyMI.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.copyMI.Text = "&Copy";
            this.copyMI.Click += new System.EventHandler(this.copyCTMN_Click);
            // 
            // pasteMI
            // 
            this.pasteMI.Enabled = false;
            this.pasteMI.Index = 1;
            this.pasteMI.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
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
            this.clearHistoryMI,
            this.sepMI4,
            this.standardExpr});
            this.historyOptionMI.Text = "&History";
            // 
            // copyHistoryMI
            // 
            this.copyHistoryMI.Index = 0;
            this.copyHistoryMI.Text = "Copy h&istory";
            this.copyHistoryMI.Click += new System.EventHandler(this.copyHistoryMI_Click);
            // 
            // editHistoryMI
            // 
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
            this.clearHistoryMI.Index = 4;
            this.clearHistoryMI.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftD;
            this.clearHistoryMI.Text = "C&lear";
            this.clearHistoryMI.Click += new System.EventHandler(this.clearHistoryMI_Click);
            // 
            // sepMI4
            // 
            this.sepMI4.Index = 5;
            this.sepMI4.Text = "-";
            // 
            // standardExpr
            // 
            this.standardExpr.Checked = true;
            this.standardExpr.Index = 6;
            this.standardExpr.Text = "Standard e&xpression";
            this.standardExpr.Click += new System.EventHandler(this.standardExpr_Click);
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
            // gridPanel
            // 
            this.gridPanel.BackColor = System.Drawing.Color.White;
            this.gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridPanel.Controls.Add(this.staDGV);
            this.gridPanel.Controls.Add(this.hisDGV);
            this.gridPanel.Controls.Add(this.dnBT);
            this.gridPanel.Controls.Add(this.upBT);
            this.gridPanel.Controls.Add(this.countlb);
            this.gridPanel.Location = new System.Drawing.Point(236, 291);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(190, 105);
            this.gridPanel.TabIndex = 186;
            // 
            // staDGV
            // 
            this.staDGV.AllowUserToAddRows = false;
            this.staDGV.AllowUserToDeleteRows = false;
            this.staDGV.AllowUserToResizeColumns = false;
            this.staDGV.AllowUserToResizeRows = false;
            this.staDGV.BackgroundColor = System.Drawing.Color.White;
            this.staDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.staDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.staDGV.ColumnHeadersVisible = false;
            this.staDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.staDGV.DefaultCellStyle = dataGridViewCellStyle1;
            this.staDGV.Location = new System.Drawing.Point(-1, 22);
            this.staDGV.MultiSelect = false;
            this.staDGV.Name = "staDGV";
            this.staDGV.ReadOnly = true;
            this.staDGV.RowHeadersVisible = false;
            this.staDGV.RowHeadersWidth = 20;
            this.staDGV.RowTemplate.Height = 20;
            this.staDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.staDGV.Size = new System.Drawing.Size(190, 82);
            this.staDGV.TabIndex = 0;
            this.staDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.statisticsDGV_CellBeginEdit);
            this.staDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.statisticsDGV_CellDoubleClick);
            this.staDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.statisticsDGV_CellEndEdit);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 190;
            // 
            // hisDGV
            // 
            this.hisDGV.AllowUserToAddRows = false;
            this.hisDGV.AllowUserToDeleteRows = false;
            this.hisDGV.AllowUserToResizeColumns = false;
            this.hisDGV.AllowUserToResizeRows = false;
            this.hisDGV.BackgroundColor = System.Drawing.Color.White;
            this.hisDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.hisDGV.ColumnHeadersVisible = false;
            this.hisDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hisDGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.hisDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.hisDGV.Location = new System.Drawing.Point(-1, 22);
            this.hisDGV.MultiSelect = false;
            this.hisDGV.Name = "hisDGV";
            this.hisDGV.ReadOnly = true;
            this.hisDGV.RowHeadersVisible = false;
            this.hisDGV.RowHeadersWidth = 35;
            this.hisDGV.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.hisDGV.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hisDGV.RowTemplate.Height = 20;
            this.hisDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.hisDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.hisDGV.Size = new System.Drawing.Size(190, 82);
            this.hisDGV.TabIndex = 0;
            this.hisDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.historyDGV_CellBeginEdit);
            this.hisDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellClick);
            this.hisDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellDoubleClick);
            this.hisDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellEndEdit);
            this.hisDGV.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.historyDGV_CellStateChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 204;
            // 
            // countlb
            // 
            this.countlb.AutoSize = true;
            this.countlb.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.countlb.Location = new System.Drawing.Point(4, 4);
            this.countlb.Name = "countlb";
            this.countlb.Size = new System.Drawing.Size(56, 13);
            this.countlb.TabIndex = 2;
            this.countlb.Text = "Count = 0";
            // 
            // screenPN
            // 
            this.screenPN.BackColor = System.Drawing.Color.White;
            this.screenPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenPN.ContextMenu = this.mainContextMenu;
            this.screenPN.Controls.Add(this.operator_lb);
            this.screenPN.Controls.Add(this.scr_lb);
            this.screenPN.Controls.Add(this.mem_lb);
            this.screenPN.Location = new System.Drawing.Point(12, 12);
            this.screenPN.Name = "screenPN";
            this.screenPN.Size = new System.Drawing.Size(190, 47);
            this.screenPN.TabIndex = 154;
            // 
            // operator_lb
            // 
            this.operator_lb.AutoSize = true;
            this.operator_lb.BackColor = System.Drawing.Color.Transparent;
            this.operator_lb.Location = new System.Drawing.Point(1, 3);
            this.operator_lb.Name = "operator_lb";
            this.operator_lb.Size = new System.Drawing.Size(15, 16);
            this.operator_lb.TabIndex = 21;
            this.operator_lb.Text = "E";
            this.operator_lb.Visible = false;
            this.operator_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.operator_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // scr_lb
            // 
            this.scr_lb.BackColor = System.Drawing.Color.Transparent;
            this.scr_lb.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scr_lb.Location = new System.Drawing.Point(18, 0);
            this.scr_lb.Name = "scr_lb";
            this.scr_lb.Size = new System.Drawing.Size(170, 45);
            this.scr_lb.TabIndex = 22;
            this.scr_lb.Text = "0";
            this.scr_lb.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.scr_lb.TextChanged += new System.EventHandler(this.scr_lb_TextChanged);
            this.scr_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.scr_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // mem_lb
            // 
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.Transparent;
            this.mem_lb.Location = new System.Drawing.Point(1, 26);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(18, 16);
            this.mem_lb.TabIndex = 21;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            this.mem_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // calc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 286);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.screenPN);
            this.Controls.Add(this.bracketTime_lb);
            this.Controls.Add(this.fe_ChkBox);
            this.Controls.Add(this.inv_ChkBox);
            this.Controls.Add(this.Expsta_bt);
            this.Controls.Add(this.sigmax2BT);
            this.Controls.Add(this.sigman_1BT);
            this.Controls.Add(this.AddstaBT);
            this.Controls.Add(this.xcross);
            this.Controls.Add(this.sigmaxBT);
            this.Controls.Add(this.sigmanBT);
            this.Controls.Add(this.CAD);
            this.Controls.Add(this.x2cross);
            this.Controls.Add(this.unknownPN);
            this.Controls.Add(this.basePN);
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
            this.Controls.Add(this.angleGB);
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
            this.Controls.Add(this.num0);
            this.Controls.Add(this.clearsta);
            this.Controls.Add(this.clearbt);
            this.Controls.Add(this.mem_minus_bt);
            this.Controls.Add(this.memrecall);
            this.Controls.Add(this.doidau);
            this.Controls.Add(this.madd);
            this.Controls.Add(this.num9);
            this.Controls.Add(this.mstore);
            this.Controls.Add(this.ce);
            this.Controls.Add(this.num6);
            this.Controls.Add(this.num8);
            this.Controls.Add(this.memclear);
            this.Controls.Add(this.num3);
            this.Controls.Add(this.backspace);
            this.Controls.Add(this.num5);
            this.Controls.Add(this.num7);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.num4);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.percent_bt);
            this.Controls.Add(this.PNbinary);
            this.Controls.Add(this.cos_bt);
            this.Controls.Add(this.sin_bt);
            this.Controls.Add(this.datecalcGB);
            this.Controls.Add(this.unitconvGB);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Menu = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(220, 315);
            this.Name = "calc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.Activated += new System.EventHandler(this.calc_Activated);
            this.Load += new System.EventHandler(this.calc_Load);
            this.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLostFocus);
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).EndInit();
            this.unitconvGB.ResumeLayout(false);
            this.unitconvGB.PerformLayout();
            this.datecalcGB.ResumeLayout(false);
            this.datecalcGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).EndInit();
            this.angleGB.ResumeLayout(false);
            this.angleGB.PerformLayout();
            this.unknownPN.ResumeLayout(false);
            this.unknownPN.PerformLayout();
            this.basePN.ResumeLayout(false);
            this.basePN.PerformLayout();
            this.gridPanel.ResumeLayout(false);
            this.gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.staDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisDGV)).EndInit();
            this.screenPN.ResumeLayout(false);
            this.screenPN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region form generation code by user
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
            this.doidau.Location = new Point(324, 98);
            this.madd.Location = new Point(324, 65);

            this.btdot.Location = new Point(285, 226);
            this.num9.Location = new Point(285, 130);
            this.mstore.Location = new Point(285, 65);
            this.clearbt.Location = new Point(285, 98);
            this.num6.Location = new Point(285, 162);
            this.num3.Location = new Point(285, 194);

            this.memrecall.Location = new Point(246, 65);
            this.ce.Location = new Point(246, 98);
            this.num8.Location = new Point(246, 130);
            this.num5.Location = new Point(246, 162);
            this.num2.Location = new Point(246, 194);

            this.memclear.Location = new Point(207, 65);
            this.backspace.Location = new Point(207, 98);
            this.num7.Location = new Point(207, 130);
            this.num4.Location = new Point(207, 162);
            this.num1.Location = new Point(207, 194);
            this.num0.Location = new Point(207, 226);

            this.exp_bt.Location = new Point(51, 226);
            this.sinh_bt.Location = new Point(51, 130);
            this.cosh_bt.Location = new Point(51, 162);
            this.tanh_bt.Location = new Point(51, 194);
            this.inv_ChkBox.Location = new Point(51, 98);

            this.int_bt.Location = new Point(12, 130);
            this.dms_bt.Location = new Point(12, 162);
            this.pi_bt.Location = new Point(12, 194);
            this.fe_ChkBox.Location = new Point(12, 226);
            this.angleGB.Location = new Point(12, 58);
            this.bracketTime_lb.Location = new Point(12, 98);

            this.scr_lb.Location = new Point(18, 0);
            this.scr_lb.Size = new Size(365, 45);
            if (isReturn0) this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);
            //
            // screenPN
            //
            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47);
            this.percent_bt.Enabled = false;

            hideSciComponent(true);
            //
            // Calculator
            //
            //this.Size = new Size(447, 340);
        }
        /// <summary>
        /// Sắp xếp lại các nút của form standard trở lại vị trí ban đầu
        /// </summary>
        private void initializedForm(bool isReturn0)
        {
            this.num1.Location = new Point(12, 194);
            this.num2.Location = new Point(51, 194);
            this.num3.Location = new Point(90, 194);
            this.num4.Location = new Point(12, 162);
            this.num5.Location = new Point(51, 162);
            this.num6.Location = new Point(90, 162);
            this.num7.Location = new Point(12, 130);
            this.num8.Location = new Point(51, 130);
            this.num9.Location = new Point(90, 130);
            this.num0.Location = new Point(12, 226);
            this.btdot.Location = new Point(90, 226);
            this.addbt.Location = new Point(129, 226);
            this.minusbt.Location = new Point(129, 194);
            this.mulbt.Location = new Point(129, 162);
            this.divbt.Location = new Point(129, 130);
            this.equal.Location = new Point(168, 194);
            this.invert_bt.Location = new Point(168, 162);
            this.percent_bt.Location = new Point(168, 130);
            this.backspace.Location = new Point(12, 98);
            this.ce.Location = new Point(51, 98);
            this.doidau.Location = new Point(129, 98);
            this.memclear.Location = new Point(12, 66);
            this.mstore.Location = new Point(51, 66);
            this.memrecall.Location = new Point(90, 66);
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
            //this.scr_lb.BackColor = Color.White;
            //this.scr_lb.Font = new Font("Consolas", 14.25F, (byte)0);
            this.scr_lb.Location = new Point(18, 0);
            this.scr_lb.Size = new Size(170, 45);
            if (isReturn0) this.scr_lb.Text = "0";
            //
            // form
            //
            //this.Size = new Size(222, 297);
        }
        /// <summary>
        /// sự kiện focused cho các nút trên form ban đầu.
        /// các sự kiện này không có trên designer
        /// </summary>
        private void FocusedEvents()
        {
            this.num1.GotFocus += new EventHandler(EnableKeyboard);
            this.num2.GotFocus += new EventHandler(EnableKeyboard);
            this.num1.GotFocus += new EventHandler(EnableKeyboard);
            this.num2.GotFocus += new EventHandler(EnableKeyboard);
            this.num3.GotFocus += new EventHandler(EnableKeyboard);
            this.num4.GotFocus += new EventHandler(EnableKeyboard);
            this.num5.GotFocus += new EventHandler(EnableKeyboard);
            this.num6.GotFocus += new EventHandler(EnableKeyboard);
            this.num7.GotFocus += new EventHandler(EnableKeyboard);
            this.num8.GotFocus += new EventHandler(EnableKeyboard);
            this.num9.GotFocus += new EventHandler(EnableKeyboard);
            this.num0.GotFocus += new EventHandler(EnableKeyboard);
            this.addbt.GotFocus += new EventHandler(EnableKeyboard);
            this.mulbt.GotFocus += new EventHandler(EnableKeyboard);
            this.divbt.GotFocus += new EventHandler(EnableKeyboard);
            this.minusbt.GotFocus += new EventHandler(EnableKeyboard);
            this.equal.GotFocus += new EventHandler(EnableKeyboard);
            this.invert_bt.GotFocus += new EventHandler(EnableKeyboard);
            this.madd.GotFocus += new EventHandler(EnableKeyboard);
            this.mem_minus_bt.GotFocus += new EventHandler(EnableKeyboard);
            this.memrecall.GotFocus += new EventHandler(EnableKeyboard);
            this.percent_bt.GotFocus += new EventHandler(EnableKeyboard);
            this.sqrt_bt.GotFocus += new EventHandler(EnableKeyboard);
            this.mstore.GotFocus += new EventHandler(EnableKeyboard);
            this.doidau.GotFocus += new EventHandler(EnableKeyboard);
            this.clearbt.GotFocus += new EventHandler(EnableKeyboard);
            this.ce.GotFocus += new EventHandler(EnableKeyboard);
            this.backspace.GotFocus += new EventHandler(EnableKeyboard);
            this.autocal_date.GotFocus += new EventHandler(EnableKeyboard);

            //this.dtP1.GotFocus += new EventHandler(DisableKeyboard);
            this.dtP1.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.dtP2.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.periodsDateUD.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.typeCB.GotFocus += new EventHandler(DisableKeyboard);
            this.result1.Enter += new EventHandler(DisableKeyboard);
            this.result2.Enter += new EventHandler(DisableKeyboard);
            //this.typeCB.MouseDown += new MouseEventHandler(DisableKeyboard);

            this.open_bracket.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.close_bracket.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.nvx_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.btFactorial.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this._3vx_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this._10x_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.x2_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.xn_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.x3_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.log_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.sinh_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.cosh_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.tanh_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.dms_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.pi_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.inv_ChkBox.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.fe_ChkBox.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.sin_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.cos_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.tan_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.ln_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.exp_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.gra_rb.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.rad_rb.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.deg_rb.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.screenPN.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.hisDGV.MouseDown +=new MouseEventHandler(EnableKeyboardAndChangeFocus);
            this.staDGV.MouseDown += new MouseEventHandler(EnableKeyboardAndChangeFocus);

            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)Handle);
            screenPN.ContextMenu = gridPanel.ContextMenu = mainContextMenu;
            hideStdComponent(standardMI.Checked);
            hideSciComponent(scientificMI.Checked);
            hideProComponent(programmerMI.Checked);
            hideStaComponent(statisticsMI.Checked);
        }
        /// <summary>
        /// khởi tạo mảng các label trong panel dưới màn hình của form programmer
        /// </summary>
        private void InitBitNumberArray()
        {
            #region init bit number
            int idex = 0, k = 0;
            bin_digit = new Label[16];
            for (int i = 1; i >= 0; i--)
                for (int j = 7; j >= 0; j--)
                {
                    k = 8 * i + j;  // k = 8j + j
                    bin_digit[k] = new Label();
                    PNbinary.Controls.Add(bin_digit[k]);
                    bin_digit[k].Text = "0000";
                    bin_digit[k].TabIndex = k;
                    bin_digit[k].Font = new Font("Consolas", 9F);
                    bin_digit[k].Size = new Size(40, 15);
                    //bin_digit[k].Location = new Point(374 - 53 * j, 34 - 32 * i);
                    bin_digit[k].Location = new Point(345 - 49 * j, 27 - 25 * i);
                    bin_digit[k].MouseMove += new MouseEventHandler(bit_digit_MouseMove);
                    bin_digit[k].Click += new EventHandler(bin_digit_Click);
                    bin_digit[k].MouseDown += new MouseEventHandler(EnableKeyboardAndChangeFocus);
                }
            #endregion

            #region init index binary number
            flagpoint = new Label[6];
            for (int i = 1; i >= 0; i--)
            {
                for (int j = 2; j >= 0; j--)
                {
                    k = 3 * i + j;  // k = 3j + j
                    idex = 16 * (k - (i == 1).GetHashCode()) - (k % 3 != 0).GetHashCode();
                    flagpoint[k] = new Label();
                    PNbinary.Controls.Add(flagpoint[k]);
                    flagpoint[k].Text = idex.ToString();
                    flagpoint[k].TabIndex = k;
                    if (j == 0)
                    {
                        flagpoint[k].Text = idex.ToString().PadLeft(4);
                    }
                    flagpoint[k].ForeColor = SystemColors.GrayText;
                    flagpoint[k].Font = new Font("Consolas", 9F);
                    flagpoint[k].Location = new Point(394 - 196 * j - 49 * (k % 3 == 0).GetHashCode(), 40 - 25 * i);
                }
            }

            #endregion
        }
        /// <summary>
        /// khởi tạo mảng các combobox
        /// </summary>
        private void InitComboBox()
        {
            fcb = new ComboBox[11];
            tcb = new ComboBox[11];
            for (int i = 0; i < fcb.Length; i++)
            {
                #region fcb properties
                fcb[i] = new ComboBox();
                unitconvGB.Controls.Add(fcb[i]);
                fcb[i].DropDownStyle = ComboBoxStyle.DropDownList;
                fcb[i].Font = new Font("Tahoma", 8.25F);
                fcb[i].TabIndex = 11;
                fcb[i].MaxDropDownItems = 14;
                fcb[i].Location = new Point(12, 113);
                //fcb[i].Name = "fromCB" + i;
                fcb[i].Size = new Size(330, 21);
                fcb[i].SelectedIndexChanged += new EventHandler(fromCB_SelectedIndexChanged);
                fcb[i].GotFocus += new EventHandler(DisableKeyboard);
                #endregion

                #region tcb properties
                tcb[i] = new ComboBox();
                unitconvGB.Controls.Add(tcb[i]);
                tcb[i].DropDownStyle = ComboBoxStyle.DropDownList;
                tcb[i].Font = new Font("Tahoma", 8.25F);
                //tcb[i].BackColor = SystemColors.Control;
                tcb[i].TabIndex = 13;
                tcb[i].MaxDropDownItems = 14;
                tcb[i].Location = new Point(12, 190);
                //tcb[i].Name = "toCB" + i;
                tcb[i].Size = new Size(330, 21);
                tcb[i].SelectedIndexChanged += new EventHandler(toCB_SelectedIndexChanged);
                //tcb[i].GotFocus += new EventHandler(DisableKeyboard);
                #endregion
            }
            object[][] obj = new object[11][];

            #region init item members
            obj[0] = new object[]{
            "Degree",
            "Gradian",
            "Radian"};
            obj[1] = new object[]{
            "Acres",
            "Hectares",
            "Square centimeter",
            "Square feet",
            "Square inch",
            "Square kilometer",
            "Square meters",
            "Square mile",
            "Square millimeter",
            "Square yard"};
            obj[2] = new object[]{
            "British Thermal Unit",
            "Calorie",
            "Electron-volts",
            "Foot-pound",
            "Joule",
            "Kilocalorie",
            "Kilojoule"};
            obj[3] = new object[]{
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
            "Yard"};
            obj[4] = new object[]{
            "BTU/minute",
            "Foot-pound/minute",
            "Horsepower",
            "Kilowatt",
            "Watt"};
            obj[5] = new object[]{
            "Atmosphere",
            "Bar",
            "Kilo Pascal",
            "Millimeter of mercury",
            "Pascal",
            "Pound per square inch (PSI)"};
            obj[6] = new object[]{
            "Degrees Celsius",
            "Degrees Fahrenheit",
            "Kelvin"};
            obj[7] = new object[]{
            "Day",
            "Hour",
            "Microsecond",
            "Milisecond",
            "Minute",
            "Second",
            "Week"};
            obj[8] = new object[]{
            "Centimeter per second",
            "Feet per second",
            "Kilometer per hour",
            "Knots",
            "Mach (at std.atm)",
            "Meter per second",
            "Miles per hour (mph)"};
            obj[9] = new object[]{
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
            "Quart (US)"};
            obj[10] = new object[]{
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
            "Tonne"};
            #endregion

            // --------------------------------------------

            for (int i = 0; i < fcb.Length; i++)
            {
                fcb[i].Items.AddRange(obj[i]);
                tcb[i].Items.AddRange(obj[i]);
            }
        }
        /// <summary>
        /// set lại location cho các control của form standard có history
        /// </summary>
        private void stdWithHistory()
        {
            this.sqrt_bt.Location = new Point(168, 202);
            this.clearbt.Location = new Point(90, 202);
            this.doidau.Location = new Point(129, 202);
            this.ce.Location = new Point(51, 202);
            this.backspace.Location = new Point(12, 202);

            this.invert_bt.Location = new Point(168, 266);
            this.mulbt.Location = new Point(129, 266);
            this.num6.Location = new Point(90, 266);
            this.num5.Location = new Point(51, 266);
            this.num4.Location = new Point(12, 266);

            this.equal.Location = new Point(168, 298);
            this.minusbt.Location = new Point(129, 298);
            this.num3.Location = new Point(90, 298);
            this.num2.Location = new Point(51, 298);
            this.num1.Location = new Point(12, 298);

            this.addbt.Location = new Point(129, 330);
            this.btdot.Location = new Point(90, 330);
            this.num0.Location = new Point(12, 330);

            this.divbt.Location = new Point(129, 234);
            this.num9.Location = new Point(90, 234);
            this.num8.Location = new Point(51, 234);
            this.num7.Location = new Point(12, 234);
            this.percent_bt.Location = new Point(168, 234);

            this.mstore.Location = new Point(51, 170);
            this.mem_minus_bt.Location = new Point(168, 170);
            this.memrecall.Location = new Point(90, 170);
            this.madd.Location = new Point(129, 170);
            this.memclear.Location = new Point(12, 170);

            this.gridPanel.Location = new Point(12, 12);
            this.gridPanel.Size = new Size(190, 105);
            this.gridPanel.Visible = true;

            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);
            this.scr_lb.Location = new Point(23, 0);
            this.scr_lb.Size = new Size(165, 45);
            //
            // Column1
            //
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 190;

            this.upBT.Location = new Point(screenPN.Size.Width - 43, upBT.Location.Y);
            this.dnBT.Location = new Point(screenPN.Size.Width - 23, dnBT.Location.Y);
            //this.Size = new Size(237, 470);
        }
        /// <summary>
        /// set lại location cho các control của form scientific có history
        /// </summary>
        private void sciWithHistory()
        {
            this.close_bracket.Location = new Point(168, 202);
            this._3vx_bt.Location = new Point(168, 298);
            this.btFactorial.Location = new Point(168, 234);
            this._10x_bt.Location = new Point(168, 330);
            this.nvx_bt.Location = new Point(168, 265);

            this.open_bracket.Location = new Point(129, 202);
            this.xn_bt.Location = new Point(129, 265);
            this.log_bt.Location = new Point(129, 330);
            this.x3_bt.Location = new Point(129, 298);
            this.x2_bt.Location = new Point(129, 234);

            this.pi_bt.Location = new Point(12, 298);
            this.int_bt.Location = new Point(12, 234);
            this.dms_bt.Location = new Point(12, 265);
            this.fe_ChkBox.Location = new Point(12, 330);
            this.angleGB.Location = new Point(12, 163);
            this.bracketTime_lb.Location = new Point(12, 202);

            this.modsciBT.Location = new Point(90, 330);
            this.ln_bt.Location = new Point(90, 202);
            this.cos_bt.Location = new Point(90, 265);
            this.sin_bt.Location = new Point(90, 234);
            this.tan_bt.Location = new Point(90, 298);

            this.cosh_bt.Location = new Point(51, 265);
            this.exp_bt.Location = new Point(51, 330);
            this.inv_ChkBox.Location = new Point(51, 202);
            this.tanh_bt.Location = new Point(51, 298);
            this.sinh_bt.Location = new Point(51, 234);

            this.sqrt_bt.Location = new Point(363, 202);
            this.percent_bt.Location = new Point(363, 234);
            this.invert_bt.Location = new Point(363, 265);
            this.equal.Location = new Point(363, 298);
            this.mem_minus_bt.Location = new Point(363, 170);

            this.divbt.Location = new Point(324, 234);
            this.mulbt.Location = new Point(324, 265);
            this.minusbt.Location = new Point(324, 298);
            this.addbt.Location = new Point(324, 330);
            this.doidau.Location = new Point(324, 202);
            this.madd.Location = new Point(324, 170);

            this.btdot.Location = new Point(285, 330);
            this.clearbt.Location = new Point(285, 202);
            this.num9.Location = new Point(285, 234);
            this.mstore.Location = new Point(285, 170);
            this.num6.Location = new Point(285, 265);
            this.num3.Location = new Point(285, 298);

            this.num0.Location = new Point(207, 330);
            this.memclear.Location = new Point(207, 170);
            this.backspace.Location = new Point(207, 202);
            this.num7.Location = new Point(207, 234);
            this.num4.Location = new Point(207, 265);
            this.num1.Location = new Point(207, 298);

            this.memrecall.Location = new Point(246, 170);
            this.ce.Location = new Point(246, 202);
            this.num8.Location = new Point(246, 234);
            this.num2.Location = new Point(246, 298);
            this.num5.Location = new Point(246, 265);
            //
            // scr_lb
            //
            this.scr_lb.Location = new Point(23, 0);
            this.scr_lb.Size = new Size(362, 45);
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
            this.percent_bt.Enabled = false;

            this.gridPanel.Location = new Point(12, 12);
            this.gridPanel.Size = new Size(385, gridPanel.Size.Height);
            this.gridPanel.Visible = true;
            this.hisDGV.Size = new Size(418, 90);

            this.upBT.Location = new Point(screenPN.Size.Width - 43, upBT.Location.Y);
            this.dnBT.Location = new Point(screenPN.Size.Width - 23, dnBT.Location.Y);
            //
            // Column1
            //
            this.Column1.Width = 383;
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
            this.memrecall.Location = new Point(246, 129);
            this.doidau.Location = new Point(324, 161);
            this.madd.Location = new Point(324, 129);
            this.mstore.Location = new Point(285, 129);
            this.memclear.Location = new Point(207, 129);
            this.ce.Location = new Point(246, 161);
            this.backspace.Location = new Point(207, 161);
            this.RoLBT.Location = new Point(90, 193);
            this.or_BT.Location = new Point(90, 225);
            this.LshBT.Location = new Point(90, 257);
            this.modsciBT.Location = new Point(129, 129);
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
            this.num0.Location = new Point(207, 289);
            this.num1.Location = new Point(207, 257);
            this.num2.Location = new Point(246, 257);
            this.num3.Location = new Point(285, 257);
            this.num4.Location = new Point(207, 225);
            this.num5.Location = new Point(246, 225);
            this.num6.Location = new Point(285, 225);
            this.num7.Location = new Point(207, 193);
            this.num8.Location = new Point(246, 193);
            this.num9.Location = new Point(285, 193);
            this.btnA.Location = new Point(168, 129);
            this.btnB.Location = new Point(168, 161);
            this.btnC.Location = new Point(168, 193);
            this.btnD.Location = new Point(168, 225);
            this.btnE.Location = new Point(168, 257);
            this.btnF.Location = new Point(168, 289);

            this.screenPN.Location = new Point(12, 12);
            this.screenPN.Size = new Size(385, 47);

            this.scr_lb.Location = new Point(18, 0);
            this.scr_lb.Size = new Size(365, 45);
            this.scr_lb.Text = "0";

            this.mem_lb.Visible = false;    // programmer không lưu memory
            gridPanel.Visible = false;

            this.sqrt_bt.Enabled = false;
            this.sqrt_bt.Location = new Point(363, 161);

            this.percent_bt.Enabled = false;
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
            this.Expsta_bt.Location = new Point(168, 202);
            this.clearsta.Location = new Point(90, 202);
            this.fe_ChkBox.Location = new Point(129, 202);
            this.CAD.Location = new Point(51, 202);
            this.backspace.Location = new Point(12, 202);

            this.sigmax2BT.Location = new Point(168, 266);
            this.sigmaxBT.Location = new Point(129, 266);
            this.num6.Location = new Point(90, 266);
            this.num5.Location = new Point(51, 266);
            this.num4.Location = new Point(12, 266);

            this.sigman_1BT.Location = new Point(168, 298);
            this.sigmanBT.Location = new Point(129, 298);
            this.num3.Location = new Point(90, 298);
            this.num2.Location = new Point(51, 298);
            this.num1.Location = new Point(12, 298);

            this.btdot.Location = new Point(90, 330);
            this.num0.Location = new Point(12, 330);
            this.AddstaBT.Location = new Point(168, 330);
            this.doidau.Location = new Point(129, 330);

            this.xcross.Location = new Point(129, 234);
            this.num9.Location = new Point(90, 234);
            this.num8.Location = new Point(51, 234);
            this.num7.Location = new Point(12, 234);
            this.x2cross.Location = new Point(168, 234);

            this.mstore.Location = new Point(51, 170);
            this.mem_minus_bt.Location = new Point(168, 170);
            this.memrecall.Location = new Point(90, 170);
            this.madd.Location = new Point(129, 170);
            this.memclear.Location = new Point(12, 170);

            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);
            this.scr_lb.Location = new Point(23, 0);
            this.scr_lb.Size = new Size(162, 46);

            this.gridPanel.Location = new Point(12, 12);
            this.screenPN.Location = new Point(12, 116);
            this.screenPN.Size = new Size(190, 47);
            this.scr_lb.Location = new Point(18, 0);
            this.scr_lb.Size = new Size(170, 45);
            this.staDGV.Size = new Size(190, 82);
            this.gridPanel.Size = new Size(190, 105);

            this.staDGV.Visible = this.countlb.Visible = true;
            this.hisDGV.Visible = false;
            //this.staDGV.CurrentCell = null;

            this.upBT.Location = new Point(screenPN.Size.Width - 43, upBT.Location.Y);
            this.dnBT.Location = new Point(screenPN.Size.Width - 23, dnBT.Location.Y);
        }

        #endregion

        #region mousedown event
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình
        /// </summary>
        private void EnableKeyboard(object sender, EventArgs e)
        {
            prcmdkey = true;
            fromTB_LostFocus(null, null);
        }
        /// <summary>
        /// Tắt tính năng bắt sự kiện phím của chương trình
        /// </summary>
        private void DisableKeyboard(object sender, EventArgs e)
        {
            prcmdkey = false;
            fromTB_LostFocus(null, null);
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi
        /// thuộc tính focus của 2 textbox trong form unit conversion, hàm bình thường
        /// </summary>
        private void EnableKeyboardAndChangeFocus()
        {
            prcmdkey = !hisDGV.IsCurrentCellInEditMode;
            fromTB_LostFocus(null, null);
            if (fromTB.Focused || toTB.Focused || typeCB.Focused) fromLB.Focus();
            bool bl = false;
            for (int i = 0; i < fcb.Length; i++)
            {
                bl = bl || (fcb[i].Focused || tcb[i].Focused);
            }
            if (bl) fromLB.Focus();
            if (calmethodCB.Focused || result2.Focused || result1.Focused
                || periodsDateUD.Focused || dtP1.Focused || dtP2.Focused)
            {
                label2.Focus();
            }
            if (dtP1.Focused || dtP2.Focused) label2.Focus();
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

        private Button num1;
        private Button num2;
        private Button num3;
        private Button num4;
        private Button num5;
        private Button num6;
        private Button num7;
        private Button num8;
        private Button num9;
        private Button num0;
        private Button btdot;
        private Button addbt;
        private Button minusbt;
        private Button mulbt;
        private Button divbt;
        private Button equal;
        private Label mem_lb;
        private Label operator_lb;
        private Label scr_lb;
        private Button invert_bt;
        private Button percent_bt;
        private Button backspace;
        private Button ce;
        private Button doidau;
        private Button memclear;
        private Button mstore;
        private Button memrecall;
        private Button sqrt_bt;
        private Button clearbt;
        private Button madd;
        private Button mem_minus_bt;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private IPanel screenPN;

        private Label bracketTime_lb;
        private GroupBox angleGB;
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
        private RadioButton deg_rb;
        private RadioButton rad_rb;
        private RadioButton gra_rb;
        private CheckBox inv_ChkBox;
        private CheckBox fe_ChkBox;
        private IDataGridView hisDGV;
        private DataGridViewTextBoxColumn Column1;
        private IPanel gridPanel;

        private Label label6;
        private CheckBox autocal_date;
        private ComboBox calmethodCB;
        private Button calculate_date;
        private TextBox result2;
        private Label label5;
        private Label label4;
        private DateTimePicker dtP2;
        private Label label3;
        private DateTimePicker dtP1;
        private Label label2;
        private TextBox result1;
        private NumericUpDown periodsDateUD;
        private NumericUpDown periodsYearUD;
        private NumericUpDown periodsMonthUD;
        private Label label1;
        private Label label7;
        private Label label8;
        private RadioButton subrb;
        private RadioButton addrb;

        private ComboBox[] fcb;
        private ComboBox[] tcb;
        private Label typeLB;
        private Label fromLB;
        private Label toLB;
        private ComboBox typeCB;
        private TextBox fromTB;
        private TextBox toTB;
        private GroupBox unitconvGB;
        private GroupBox datecalcGB;
        private Button invert_unit;
        private Button dnBT;
        private Button upBT;

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
        private MenuItem standardExpr;
        private MenuItem sepMI4;
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
        private MenuItem helpMI;
        private MenuItem helpTopicsTSMI;
        private MenuItem sepMI5;
        private MenuItem aboutMI;
        private MainMenu menuStrip1;
        private MenuItem worksheetsMI;
        private MenuItem mortgageMI;
        private MenuItem vehicleLeaseMI;
        private MenuItem feMPG_MI;
        private MenuItem fe_L100_MI;

        private ContextMenu mainContextMenu;
        private MenuItem copyCTMN;
        private MenuItem pasteCTMN;
        private MenuItem sepCTMN;
        private MenuItem showHistoryCTMN;
        private MenuItem hideHistoryCTMN;
        private MenuItem clearDatasetCTMN;
        private MenuItem clearHistoryCTMN;

        private Panel unknownPN;
        private RadioButton _byteRB;
        private RadioButton qwordRB;
        private RadioButton _wordRB;
        private RadioButton dwordRB;
        private Panel basePN;
        private RadioButton binRB;
        private RadioButton octRB;
        private RadioButton decRB;
        private RadioButton hexRB;
        private Panel PNbinary;
        private Button btnF;
        private Button btnB;
        private Button btnD;
        private Button XorBT;
        private Button NotBT;
        private Button AndBT;
        private Button btnE;
        private Button RshBT;
        private Button RoRBT;
        private Button LshBT;
        private Button or_BT;
        private Button RoLBT;
        private Button btnA;
        private Button btnC;
        private Button openproBT;
        private Button modproBT;
        private Button closeproBT;
        private Label[] bin_digit;
        private Label[] flagpoint;

        private IDataGridView staDGV;
        private Button Expsta_bt;
        private Button sigmax2BT;
        private Button sigman_1BT;
        private Button AddstaBT;
        private Button xcross;
        private Button sigmaxBT;
        private Button sigmanBT;
        private Button CAD;
        private Button x2cross;
        private Button clearsta;
        private Label countlb;
        private DataGridViewTextBoxColumn Column2;

        #endregion
    }
}