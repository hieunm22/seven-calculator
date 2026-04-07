using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Calculator
{
    partial class calc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region muParser Code
        private void muParserMethod()
        {
            try
            {
                m_parser = new Parser();
                m_parser.DefineFun("fun1", new Parser.Fun1Delegate(fun1));
                m_parser.DefineFun("fun2", new Parser.Fun2Delegate(fun2));
                m_parser.DefineFun("fun3", new Parser.Fun3Delegate(fun3));
                m_parser.DefineFun("fun4", new Parser.Fun4Delegate(fun4));
                m_parser.DefineFun("fun5", new Parser.Fun5Delegate(fun5));
                m_parser.DefineFun("prod", new Parser.MultFunDelegate(prod));
                m_parser.DefineOprt("%", new Parser.Fun2Delegate(fun2), 2);

                m_parser.DefinePostfixOprt("m", new Parser.Fun1Delegate(milli));
                m_parser.DefineInfixOprt("!", new Parser.Fun1Delegate(not), Parser.EPrec.prLOGIC);

                m_parser.DefineVar("ans", m_ans);
                m_parser.DefineVar("my_var1", m_val1);
                m_parser.DefineVar("my_var2", m_val2);
            }
            catch (ParserException exc)
            {
                DumpException(exc);
            }
        }

        private void DumpException(ParserException exc)
        {
            string sMsg;

            sMsg = "An error occured:\n";
            sMsg += string.Format("  Expression:  \"{0}\"\n", exc.Expression);
            sMsg += string.Format("  Message:     \"{0}\"\n", exc.Message);
            sMsg += string.Format("  Token:       \"{0}\"\n", exc.Token);
            sMsg += string.Format("  Position:      {0}\n", exc.Position);

            //meHistory.SelectionColor = System.Drawing.Color.Red;
            //meHistory.AppendText(sMsg);
            //meHistory.SelectionColor = System.Drawing.Color.Black;

            //meHistory.SelectionStart = meHistory.TextLength;
            //meHistory.ScrollToCaret();
        }

        private Parser m_parser;
        private ParserVariable m_val1 = new ParserVariable(0);
        private ParserVariable m_val2 = new ParserVariable(0);
        private ParserVariable m_ans = new ParserVariable(0);

        public double prod(double[] a, int size)
        {
            double val = 1;
            for (int i = 0; i < size; ++i)
                val *= a[i];

            return val;
        }

        public double strFun1(String str, double val1)
        {
            return val1 * 2;
        }

        public double strFun2(String str, double val1, double val2)
        {
            return val1 + val2;
        }

        public double strFun3(String str, double val1, double val2, double val3)
        {
            return val1 + val2 + val3;
        }

        public double milli(double val1)
        {
            return val1 / 1000.0;
        }

        public double not(double val1)
        {
            return (val1 == 0) ? 1 : 0;
        }

        public double fun1(double val1)
        {
            return val1 * 2;
        }

        public double fun2(double val1, double val2)
        {
            return val1 + val2;
        }

        public double fun3(double val1, double val2, double val3)
        {
            return val1 + val2 + val3;
        }

        public double fun4(double val1, double val2, double val3, double val4)
        {
            return val1 + val2 + val3 + val4;
        }

        public double fun5(double val1, double val2, double val3, double val4, double val5)
        {
            return val1 + val2 + val3 + val4 + val5;
        }
        #endregion

        #region dll import
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        IntPtr nextClipboardViewer;
        #endregion

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
            if (prcmdkey)
            {
                #region xu ly phim nhap vao
                int key_hc = keyData.GetHashCode(); // key_hc = key hashcode
                // alt F4 hoac ctrl W
                if (key_hc == 262259 || key_hc == 131159) this.Close();
                // cac phim so tu 1 den 10
                if (key_hc == 096 || key_hc == 48) numinput(0);
                if (key_hc == 097 || key_hc == 49) numinput(1);
                if ((key_hc == 098 || key_hc == 50) && num2.Enabled) numinput(2);
                if ((key_hc == 099 || key_hc == 51) && num3.Enabled) numinput(3);
                if ((key_hc == 100 || key_hc == 52) && num4.Enabled) numinput(4);
                if ((key_hc == 101 || key_hc == 53) && num5.Enabled) numinput(5);
                if ((key_hc == 102 || key_hc == 54) && num6.Enabled) numinput(6);
                if ((key_hc == 103 || key_hc == 55) && num7.Enabled) numinput(7);
                if ((key_hc == 104 || key_hc == 56) && num8.Enabled) numinput(8);
                if ((key_hc == 105 || key_hc == 57) && num9.Enabled) numinput(9);
                // cac phim A den F
                if (key_hc == 65 && btnA.Enabled && btnA.Visible) buttonAF(btnA.Text);
                if (key_hc == 66 && btnB.Enabled && btnB.Visible) buttonAF(btnB.Text);
                if (key_hc == 67 && btnC.Enabled && btnC.Visible) buttonAF(btnC.Text);
                if (key_hc == 68 && btnD.Enabled && btnD.Visible) buttonAF(btnD.Text);
                if (key_hc == 69 && btnE.Enabled && btnE.Visible) buttonAF(btnE.Text);
                if (key_hc == 70 && btnF.Enabled && btnF.Visible) buttonAF(btnF.Text);

                if (key_hc == 187 | key_hc == 13)    // = hoac enter 
                {
                    if (statisticsTSMI.Checked) statistics_add();
                    if (!statisticsTSMI.Checked) equalclicked();
                }
                if (key_hc == 110 | key_hc == 00190 && btdot.Enabled) numinput(10);  // .
                if (key_hc == 107 | key_hc == 65723 && addbt.Enabled)    // +
                {
                    if (standardTSMI.Checked) std_operation(12);
                    if (scientificTSMI.Checked) sci_operation(12);
                    if (programmerTSMI.Checked) pro_operation(12);
                }
                if (key_hc == 109 | key_hc == 00189 && minusbt.Enabled)    // -
                {
                    if (standardTSMI.Checked) std_operation(13);
                    if (scientificTSMI.Checked) sci_operation(13);
                    if (programmerTSMI.Checked) pro_operation(13);
                }
                if (key_hc == 106 | key_hc == 65592 && mulbt.Enabled)    // *
                {
                    if (standardTSMI.Checked) std_operation(14);
                    if (scientificTSMI.Checked) sci_operation(14);
                    if (programmerTSMI.Checked) pro_operation(14);
                }
                if (key_hc == 111 | key_hc == 00191 && divbt.Enabled)    // /
                {
                    if (standardTSMI.Checked) std_operation(15);
                    if (scientificTSMI.Checked) sci_operation(15);
                    if (programmerTSMI.Checked) pro_operation(15);
                }
                if (key_hc == 114 && scientificTSMI.Checked) deg_rb.Checked = true;
                if (key_hc == 115 && scientificTSMI.Checked) rad_rb.Checked = true;
                if (key_hc == 116)
                {
                    if (scientificTSMI.Checked) gra_rb.Checked = true;
                    if (programmerTSMI.Checked) { hexRB.Checked = true; basecheckchange(); }
                }
                if (key_hc == 117 && programmerTSMI.Checked)
                {
                    decRB.Checked = true; basecheckchange();
                }
                if (key_hc == 118 && programmerTSMI.Checked)
                {
                    octRB.Checked = true; basecheckchange();
                }
                if (key_hc == 119 && programmerTSMI.Checked)
                {
                    binRB.Checked = true; basecheckchange();
                }
                if (key_hc == 120 && doidau.Enabled) numinput(11);      // F9
                if (key_hc == 082 && invert_bt.Enabled) math_func(17);  // R
                if (key_hc == 112) helptopics();        // F1
                if (key_hc == 008) backspaceclicked();  // Backspace
                if (key_hc == 027) clear_num(true);     // Esc
                // cac to hop phim 
                if (key_hc == 262193) stdLoad();  // alt 1
                if (key_hc == 262194) sciLoad();  // alt 2
                if (key_hc == 262195) proLoad();  // alt 3
                if (key_hc == 262196) staLoad();  // alt 3
                if (key_hc == 131144 && historyTSMI.Enabled) formWithHistory();   // ctrl H
                if (key_hc == 131187) basicForm();

                if (key_hc == 131152) mem_process(1);   // ctrl P
                if (key_hc == 131153) mem_process(2);   // ctrl Q
                if (key_hc == 131149) mem_process(3);   // ctrl M
                if (key_hc == 131139) copyCommand();    // ctrl C
                if (key_hc == 131157) exFunc(unitConversionTSMI);
                if (key_hc == 131141) exFunc(dateCalculationTSMI);
                if (key_hc == 086) buttonFE();          // V
                if (key_hc == 089) sci_operation(30);   // Y
                if (key_hc == 131148)   // ctrl L
                {
                    mem_num = 0;
                    mem_lb.Visible = false;
                    confirm_num = true;
                }
                if (key_hc == 131154)   // ctrl R
                {
                    str = "" + mem_num;
                    scr_lb.Text = str;
                    confirm_num = true;
                }

                if (key_hc == 196676)   // ctrl shift d
                {
                    if (clearHistoryTSMI.Enabled && clearHistoryTSMI.Visible)
                    {
                        clear_history();
                    }
                    if (clearDatasetTSMI.Enabled && clearDatasetTSMI.Visible)
                    {
                        clear_statistics();
                    }
                }

                if (key_hc == 131158 && pasteTSMI.Enabled) pasteCommand();  // ctrl V
                #endregion
            }

            return prcmdkey;
        }
        /// <summary>
        /// clipboard monitor
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    try
                    {
                        IDataObject iData = new DataObject();
                        iData = Clipboard.GetDataObject();

                        if (iData.GetDataPresent(DataFormats.Text))
                        {
                            if (!programmerTSMI.Checked)
                                pasteTSMI.Enabled = misc.isNumber(Clipboard.GetText().Trim());
                            else
                            {
                                if (binRB.Checked)
                                    pasteTSMI.Enabled = Binary.CheckIsBin(Clipboard.GetText().Trim());
                                if (octRB.Checked)
                                    pasteTSMI.Enabled = Binary.CheckIsOct(Clipboard.GetText().Trim());
                                if (decRB.Checked)
                                    pasteTSMI.Enabled = Binary.CheckIsDec(Clipboard.GetText().Trim());
                                if (hexRB.Checked)
                                {
                                    pasteTSMI.Enabled = Binary.CheckIsHex(Clipboard.GetText().Trim());
                                    pasteTSMI.Enabled &= hexRB.Visible = true;
                                }
                            }
                        }
                        else pasteTSMI.Enabled = false;
                        pasteCTMN.Enabled = pasteTSMI.Enabled;
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

        #region user's method
        /// <summary>
        /// form standard
        /// </summary>
        private void stdLoad()
        {
            clearHistoryTableB4SwitchForm();
            int his = historyTSMI.Checked.GetHashCode();
            int exf = (dateCalculationTSMI.Checked || unitConversionTSMI.Checked).GetHashCode();
            if (!standardTSMI.Checked)
            {
                modeMethod(standardTSMI);
                EnableKeyboardAndChangeFocus();
                if (historyTSMI.Checked && historyTSMI.Enabled) stdWithHistory();
                else initializedForm(true);

                enableComponent();
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(false);
                datecalcGB.Location = new Point(234, 9);
                unitconvGB.Location = new Point(234, 9);
                datecalcGB.Size = new Size(345, 269 + 131 * his);
                unitconvGB.Size = new Size(345, 269 + 131 * his);
                datecalcGB.Visible = dateCalculationTSMI.Checked;
                unitconvGB.Visible = unitConversionTSMI.Checked;
                this.Size = new Size(237 + 363 * exf, 340 + 130 * his);
                clear_num(false);
                writeToRegistry(standardTSMI);
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
        }
        /// <summary>
        /// form scientific
        /// </summary>
        private void sciLoad()
        {
            clearHistoryTableB4SwitchForm();
            int his = historyTSMI.Checked.GetHashCode();
            bool ex = dateCalculationTSMI.Checked || unitConversionTSMI.Checked;
            if (!scientificTSMI.Checked)
            {
                modeMethod(scientificTSMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(true);
                hideProComponent(false);
                hideStaComponent(false);
                enableComponent();

                if (historyTSMI.Checked && historyTSMI.Enabled) sciWithHistory();
                else scientificLoad(true);

                datecalcGB.Visible = readFromRegistry("Date");
                unitconvGB.Visible = readFromRegistry("Unit");
                dateCalculationTSMI.Checked = datecalcGB.Visible;
                unitConversionTSMI.Checked = unitconvGB.Visible;
                datecalcGB.Location = new Point(446, 9);
                unitconvGB.Location = new Point(446, 9);
                datecalcGB.Size = new Size(345, 269 + 131 * his);
                unitconvGB.Size = new Size(345, 269 + 131 * his);
                //if (readFromRegistry("Date") || readFromRegistry("Unit"))
                if (ex) basicTSMI.Checked = false;

                this.Size = new Size(447 + 363 * ex.GetHashCode(), 340 + 130 * his);
                clear_num(true);
                writeToRegistry(scientificTSMI);
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
        }
        /// <summary>
        /// form programmer
        /// </summary>
        private void proLoad()
        {
            bool exf = dateCalculationTSMI.Checked || unitConversionTSMI.Checked;
            if (!programmerTSMI.Checked)
            {
                modeMethod(programmerTSMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(true);
                hideSciComponent(false);
                hideProComponent(true);
                hideStaComponent(false);
                datecalcGB.Visible = readFromRegistry("Date");
                unitconvGB.Visible = readFromRegistry("Unit");
                dateCalculationTSMI.Checked = datecalcGB.Visible;
                unitConversionTSMI.Checked = unitconvGB.Visible; 
                historyPN.Visible = false;
                programmerMode();
                this.Size = new Size(447 + 363 * exf.GetHashCode(), 420);
                writeToRegistry(programmerTSMI);
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
        }
        /// <summary>
        /// form statistics
        /// </summary>
        private void staLoad()
        {
            clearHistoryTableB4SwitchForm();
            int exf = (dateCalculationTSMI.Checked || unitConversionTSMI.Checked).GetHashCode();
            if (!statisticsTSMI.Checked)
            {
                modeMethod(statisticsTSMI);
                EnableKeyboardAndChangeFocus();
                hideStdComponent(false);
                hideSciComponent(false);
                hideProComponent(false);
                hideStaComponent(true);
                datecalcGB.Visible = readFromRegistry("Date");
                unitconvGB.Visible = readFromRegistry("Unit");
                dateCalculationTSMI.Checked = datecalcGB.Visible;
                unitConversionTSMI.Checked = unitconvGB.Visible;
                historyPN.Visible = false;
                statisticsMode();

                enableComponent();
                datecalcGB.Location = new Point(234, 9);
                unitconvGB.Location = new Point(234, 9);
                datecalcGB.Size = new Size(345, 400);
                unitconvGB.Size = new Size(345, 400);
                datecalcGB.Visible = dateCalculationTSMI.Checked;
                unitconvGB.Visible = unitConversionTSMI.Checked;
                this.Size = new Size(237 + 363 * exf, 470);
                clear_num(false);
                writeToRegistry(statisticsTSMI);
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
            }
        }
        /// <summary>
        /// khi chọn 3 dòng đầu tiên của menu view
        /// </summary>
        private void modeMethod(MenuItem mi)
        {
            prcmdkey = true;
            standardTSMI.Checked = false;
            scientificTSMI.Checked = false;
            programmerTSMI.Checked = false;
            statisticsTSMI.Checked = false;
            mi.Checked = true;

            screenPN.ContextMenu = contextMenu1;
            historyDGV.ContextMenu = contextMenu1;
            scr_lb.ContextMenu = screenPN.ContextMenu;
            pasteTSMI.Enabled = Binary.CheckIsHex(Clipboard.GetText().Trim());

            historyTSMI.Enabled = !programmerTSMI.Checked && !statisticsTSMI.Checked;
            datasetTSMI.Visible = statisticsTSMI.Checked;
            historyOptionTSMI.Visible = historyTSMI.Enabled;
            datasetTSMI.Visible = statisticsTSMI.Checked;
            sepTSMI3.Visible = (mi != programmerTSMI);
            pasteTSMI.Enabled = Binary.CheckIsDec(Clipboard.GetText().Trim());

            sepCTMN.Visible = sepTSMI3.Visible;
            showHistoryCTMN.Visible = !historyTSMI.Checked && (standardTSMI.Checked || scientificTSMI.Checked);
            hideHistoryCTMN.Visible = historyTSMI.Checked && (standardTSMI.Checked || scientificTSMI.Checked);
            clearDataSetCTMN.Visible = statisticsTSMI.Checked;
            clearHistoryCTMN.Visible = hideHistoryCTMN.Visible;
            clearDataSetCTMN.Enabled = false;
            clearHistoryCTMN.Enabled = false;
            pasteCTMN.Enabled = pasteTSMI.Enabled;
            
            clearHistoryTableB4SwitchForm();
        }
        /// <summary>
        /// form basic
        /// </summary>
        private void basicForm()
        {
            int his = (historyTSMI.Checked && historyTSMI.Enabled).GetHashCode();
            if (!basicTSMI.Checked)
            {
                basicTSMI.Checked = true;
                unitConversionTSMI.Checked = false;
                dateCalculationTSMI.Checked = false;
                if (standardTSMI.Checked)
                {
                    if (historyTSMI.Checked) stdWithHistory();
                    else initializedForm(false);
                    this.Size = new Size(237, 340 + 130 * his);
                    //hideSciComponent(false);
                    //hideProComponent(false);
                    //hideStaComponent(false);
                    //hideStdComponent(true);
                }
                if (scientificTSMI.Checked)
                {
                    if (historyTSMI.Checked) sciWithHistory();
                    else scientificLoad(false);
                    this.Size = new Size(447, 340 + 130 * his);
                    //hideProComponent(false);
                    //hideStaComponent(false);
                    //hideStdComponent(true);
                    //hideSciComponent(true);
                }
                if (programmerTSMI.Checked)
                {
                    programmerMode();
                    this.Size = new Size(447, 420);
                    //hideStaComponent(false);
                    //hideStdComponent(false);
                    //hideSciComponent(false);
                    //hideProComponent(true);

                    //clearbt.Visible = true;
                    //ce.Visible = true;
                    //addbt.Visible = true;
                    //mem_minus_bt.Visible = true;
                    //mulbt.Visible = true;
                    //minusbt.Visible = true;
                    //divbt.Visible = true;
                    //sqrt_bt.Visible = true;
                    //invert_bt.Visible = true;
                    //percent_bt.Visible = true;
                    //equal.Visible = true;
                }
                if (statisticsTSMI.Checked)
                {
                    statisticsMode();
                    this.Size = new Size(237, 470);
                    //hideStdComponent(false);
                    //hideSciComponent(false);
                    //hideProComponent(false);
                    //hideStaComponent(true);
                }
                datecalcGB.Visible = false;
                unitconvGB.Visible = false;
                displayToScreen();
                prcmdkey = true;
                writeToRegistry(basicTSMI);
            }
        }
        /// <summary>
        /// clear bảng history trước khi chuyển form
        /// </summary>
        private void clearHistoryTableB4SwitchForm()
        {
            while (historyDGV.Rows.Count > 0)
                historyDGV.Rows.RemoveAt(0);
        }
        /// <summary>
        /// nạp thông tin trong registry
        /// </summary>
        private void loadInfoFromRegistry(object sender, EventArgs e)
        {
            //if (readFromRegistry("Standard")) viewMode_Click(sender, e);
            if (readFromRegistry("Scientific")) scientificTSMI_Click(sender, e);
            if (readFromRegistry("Programmer")) programmerTSMI_Click(sender, e);
            if (readFromRegistry("Statistics")) statisticsTSMI_Click(sender, e);
            if (readFromRegistry("DigitGrouping")) digitGroupingTSMI_Click(sender, e);
            if (readFromRegistry("History") && historyTSMI.Enabled) historyTSMI_Click(sender, e);
            if (readFromRegistry("Date"))
            {
                dateCalculationTSMI_Click(sender, e);
                calmethodCB.SelectedIndex = readFromSubkey("Method", "DateCalculation");
                autocal_date.Checked = (readFromSubkey("AutoCalculate", "DateCalculation") == 1);
            }
            if (readFromRegistry("Unit"))
            {
                unitConversionTSMI_Click(sender, e);
                typeCB.SelectedIndex = readFromSubkey("Type", "UnitConversion");
            }
            btdot.Text = decimalSym;
        }
        /// <summary>
        /// lưu các thuộc tính checked của 1 tsmi vào registry
        /// </summary>
        private void writeToRegistry(MenuItem tsmi)
        {
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\Calculator", true);
            if (tsmi == standardTSMI)
            {
                regkey.SetValue("Standard", (int)1);
                regkey.SetValue("Scientific", (int)0);
                regkey.SetValue("Programmer", (int)0);
                regkey.SetValue("Statistics", (int)0);
                regkey.SetValue("Basic", (int)basicTSMI.Checked.GetHashCode());
                regkey.SetValue("Date", (int)dateCalculationTSMI.Checked.GetHashCode());
                regkey.SetValue("Unit", (int)unitConversionTSMI.Checked.GetHashCode());
            }
            if (tsmi == scientificTSMI)
            {
                regkey.SetValue("Standard", (int)0);
                regkey.SetValue("Scientific", (int)1);
                regkey.SetValue("Programmer", (int)0);
                regkey.SetValue("Statistics", (int)0);
                regkey.SetValue("Basic", (int)basicTSMI.Checked.GetHashCode());
                regkey.SetValue("Date", (int)dateCalculationTSMI.Checked.GetHashCode());
                regkey.SetValue("Unit", (int)unitConversionTSMI.Checked.GetHashCode());
            }
            if (tsmi == programmerTSMI)
            {
                regkey.SetValue("Standard", (int)0);
                regkey.SetValue("Scientific", (int)0);
                regkey.SetValue("Programmer", (int)1);
                regkey.SetValue("Statistics", (int)0);
                regkey.SetValue("Basic", (int)basicTSMI.Checked.GetHashCode());
                regkey.SetValue("Date", (int)dateCalculationTSMI.Checked.GetHashCode());
                regkey.SetValue("Unit", (int)unitConversionTSMI.Checked.GetHashCode());
            } 
            if (tsmi == statisticsTSMI)
            {
                regkey.SetValue("Standard", (int)0);
                regkey.SetValue("Scientific", (int)0);
                regkey.SetValue("Programmer", (int)0);
                regkey.SetValue("Statistics", (int)1);
                regkey.SetValue("Basic", (int)basicTSMI.Checked.GetHashCode());
                regkey.SetValue("Date", (int)dateCalculationTSMI.Checked.GetHashCode());
                regkey.SetValue("Unit", (int)unitConversionTSMI.Checked.GetHashCode());
            } 
            if (tsmi == digitGroupingTSMI)
            {
                regkey.SetValue("DigitGrouping", (int)digitGroupingTSMI.Checked.GetHashCode());
            }
            if (tsmi == historyTSMI)
            {
                regkey.SetValue("History", (int)historyTSMI.Checked.GetHashCode());
            } 
            if (tsmi == unitConversionTSMI)
            {
                regkey.SetValue("Standard", (int)standardTSMI.Checked.GetHashCode());
                regkey.SetValue("Scientific", (int)scientificTSMI.Checked.GetHashCode());
                regkey.SetValue("Programmer", (int)programmerTSMI.Checked.GetHashCode());
                regkey.SetValue("Basic", (int)0);
                regkey.SetValue("Date", (int)0);
                regkey.SetValue("Unit", (int)1);
            }
            if (tsmi == dateCalculationTSMI)
            {
                regkey.SetValue("Standard", (int)standardTSMI.Checked.GetHashCode());
                regkey.SetValue("Scientific", (int)scientificTSMI.Checked.GetHashCode());
                regkey.SetValue("Programmer", (int)programmerTSMI.Checked.GetHashCode());
                regkey.SetValue("Basic", (int)0);
                regkey.SetValue("Date", (int)1);
                regkey.SetValue("Unit", (int)0);
            }
            if (tsmi == basicTSMI)
            {
                regkey.SetValue("Standard", (int)standardTSMI.Checked.GetHashCode());
                regkey.SetValue("Scientific", (int)scientificTSMI.Checked.GetHashCode());
                regkey.SetValue("Programmer", (int)programmerTSMI.Checked.GetHashCode());
                regkey.SetValue("Basic", (int)1);
                regkey.SetValue("Date", (int)0);
                regkey.SetValue("Unit", (int)0);
            }

            regkey.Close();
        }
        /// <summary>
        /// đọc thuộc tính đã được ghi vào registry
        /// </summary>
        private bool readFromRegistry(string name)
        {
            bool ret = false;
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\Calculator");
            if (regkey == null)
            {
                regkey = Registry.LocalMachine.CreateSubKey("Software\\Calculator");
                regkey.SetValue("Standard", 1, RegistryValueKind.DWord);
                regkey.SetValue("Scientific", 0, RegistryValueKind.DWord);
                regkey.SetValue("Programmer", 0, RegistryValueKind.DWord);
                regkey.SetValue("Statistics", 0, RegistryValueKind.DWord);
                regkey.SetValue("DigitGrouping", 0, RegistryValueKind.DWord);
                regkey.SetValue("History", 0, RegistryValueKind.DWord);
                regkey.SetValue("Basic", 1, RegistryValueKind.DWord);
                regkey.SetValue("Date", 0, RegistryValueKind.DWord);
                regkey.SetValue("Unit", 0, RegistryValueKind.DWord);
                regkey.SetValue("MemoryNumber", (string) "0", RegistryValueKind.String);
            }
            if ((int)regkey.GetValue(name) == 1) ret = true;
            regkey.Close();
            return ret;
        }
        /// <summary>
        /// lấy thông tin về các checkbox, combobox trong registry
        /// </summary>
        private int readFromSubkey(string dwordValue, string subkeyValue)
        {
            int ret = 0;
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("SOFTWARE\\Calculator\\" + subkeyValue, true);
            if (regkey == null)
            {
                regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Calculator\\", true);
                regkey = regkey.CreateSubKey(subkeyValue);
                regkey.SetValue(dwordValue, (int)0, RegistryValueKind.DWord);
            }
            try
            {
                ret = (int)regkey.GetValue(dwordValue);
            }
            catch (NullReferenceException)
            {
                regkey.SetValue(dwordValue, (int)0, RegistryValueKind.DWord);
            }
            regkey.Close();
            return ret;
        }
        /// <summary>
        /// ghi thông tin về các thuộc tính các control vào registry
        /// </summary>
        private void writeToSubkey(MenuItem tsmi, string subkeyValue)
        {
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("SOFTWARE\\Calculator\\" + subkeyValue, true);
            if (regkey == null)
            {
                regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Calculator\\", true);
                regkey = regkey.CreateSubKey(subkeyValue);
            }
            if (tsmi == dateCalculationTSMI)
            {
                regkey.SetValue("Method", (int)calmethodCB.SelectedIndex, RegistryValueKind.DWord);
                regkey.SetValue("AutoCalculate", (int)autocal_date.Checked.GetHashCode(), RegistryValueKind.DWord);
            }
            if (tsmi == unitConversionTSMI)
            {
                regkey.SetValue("Type", (int)typeCB.SelectedIndex, RegistryValueKind.DWord);
            }
            regkey.Close();
        }
        /// <summary>
        /// lấy giá trị của số M trong registry
        /// </summary>
        private void getMemoryNumber()
        {
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\Calculator", true);
            if (regkey == null)
            {
                regkey = Registry.LocalMachine.CreateSubKey("Software\\Calculator");
                regkey.SetValue("MemoryNumber", (string) "0", RegistryValueKind.String);
            }

            string memory = (string) regkey.GetValue("MemoryNumber");
            if (memory == null)
            {
                regkey.SetValue("MemoryNumber", (string) "0", RegistryValueKind.String);
                memory = (string) regkey.GetValue("MemoryNumber");
            }
            try
            {
                mem_num = double.Parse(memory);
                mem_lb.Visible = (mem_num != 0);
                toolTip1.SetToolTip(mem_lb, "M = " + mem_num.ToString());
            }
            catch (FormatException)
            {
                mem_num = 0;
                regkey.SetValue("MemoryNumber", (string) "0");
            }
            regkey.Close();
        }
        /// <summary>
        /// set giá trị của số M trong registry
        /// </summary>
        private void setMemoryNumber()
        {
            RegistryKey regkey = null;
            regkey = Registry.LocalMachine.OpenSubKey("Software\\Calculator", true);
            regkey.SetValue("MemoryNumber", (string) mem_num.ToString());
            string memory = (string) regkey.GetValue("MemoryNumber");
            mem_lb.Visible = (mem_num != 0);
            toolTip1.SetToolTip(mem_lb, "M = " + mem_num.ToString());
            regkey.Close();
        }
        /// <summary>
        /// lấy kí tự phân cách giữa phần nguyên và phần lẻ của số
        /// </summary>
        private string getDecimalSym()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Control Panel\\International");
            return (string) reg.GetValue("sDecimal");
        }

        public string decimalSym
        {
            get { return getDecimalSym(); }
        }
        /// <summary>
        /// lấy kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        private string getThousandSym()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Control Panel\\International");
            return (string) reg.GetValue("sThousand");
        }

        public string thousandSym
        {
            get { return getThousandSym(); }
        }
        /// <summary>
        /// form with history - ctrl - H
        /// </summary>
        private void formWithHistory()
        {
            historyTSMI.Checked = !historyTSMI.Checked;
            clearHistoryCTMN.Visible = historyTSMI.Checked;
            clearHistoryCTMN.Enabled = (historyDGV.Rows.Count > 0);
            showHistoryCTMN.Visible = !historyTSMI.Checked;
            hideHistoryCTMN.Visible = historyTSMI.Checked;
            prcmdkey = true;
            int his = ((historyTSMI.Checked && historyTSMI.Enabled) || statisticsTSMI.Checked).GetHashCode();
            int sci = scientificTSMI.Checked.GetHashCode();
            int pro = programmerTSMI.Checked.GetHashCode();
            int ex = (dateCalculationTSMI.Checked || unitConversionTSMI.Checked).GetHashCode();

            datecalcGB.Location = new Point(234 + 212 * (sci + pro), 9);
            unitconvGB.Location = new Point(234 + 212 * (sci + pro), 9);
            datecalcGB.Size = new Size(345, 269 + 131 * his);
            unitconvGB.Size = new Size(345, 269 + 131 * his);
            if (historyTSMI.Checked && historyTSMI.Enabled)
            {
                if (standardTSMI.Checked) stdWithHistory();
                if (scientificTSMI.Checked) sciWithHistory();
                historyDGV.CurrentCell = null;
                currentCellToNull();
                hideProComponent(programmerTSMI.Checked);
                hideStaComponent(statisticsTSMI.Checked);
                hideSciComponent(scientificTSMI.Checked);   // phải đứng cuối
            }
            else
            {
                if (standardTSMI.Checked) initializedForm(false);
                if (scientificTSMI.Checked) scientificLoad(false);
                historyPN.Visible = false;
            }
            this.Size = new Size(237 + 210 * (sci + pro) + 363 * ex, 340 + 130 * his + 80 * pro);
            writeToRegistry(historyTSMI);
        }
        /// <summary>
        /// 2 tính năng date calculation và unit conversion
        /// </summary>
        private void exFunc(MenuItem tsmi)
        {
            int his = ((historyTSMI.Checked && historyTSMI.Enabled) 
                || statisticsTSMI.Checked).GetHashCode();
            int pro = programmerTSMI.Checked.GetHashCode();
            int sci = scientificTSMI.Checked.GetHashCode();
            //if (!tsmi.Checked)
            {
                datecalcGB.Visible = (tsmi == dateCalculationTSMI);
                unitconvGB.Visible = (tsmi == unitConversionTSMI);

                datecalcGB.Size = new Size(345, 269 + 81 * pro + 131 * his);
                unitconvGB.Size = new Size(345, 269 + 81 * pro + 131 * his);

                unitConversionTSMI.Checked = (tsmi == unitConversionTSMI);
                dateCalculationTSMI.Checked = (tsmi == dateCalculationTSMI);
                basicTSMI.Checked = false;
                if (tsmi == unitConversionTSMI)
                {
                    try
                    {
                        typeCB.SelectedIndex = readFromSubkey("Type", "UnitConversion");
                    }
                    catch { typeCB.SelectedIndex = 0; }
                    toTB.Text = getToTBText(fromTB.Text);
                    //this.AcceptButton = calculate_unit;
                }
                if (tsmi == dateCalculationTSMI)
                {
                    try
                    {
                        calmethodCB.SelectedIndex = readFromSubkey("Method", "DateCalculation");
                    }
                    catch { calmethodCB.SelectedIndex = 0; }
                    autocal_date.Checked = (readFromSubkey("AutoCalculate", "DateCalculation") == 1);
                    this.AcceptButton = calculate_date;
                }
                prcmdkey = !typeCB.Focused && !dtP1.Focused && !calmethodCB.Focused;
                this.Size = new Size(600 + 210 * (sci + pro), 340 + 130 * his + 80 * pro);

                writeToRegistry(tsmi);
            }
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
            inv_bt.Visible = bl;
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
            fe_bt.Visible = bl;
            dms_bt.Visible = bl;
            int_bt.Visible = bl;
            x2_bt.Visible = bl;
            x3_bt.Visible = bl;
            xn_bt.Visible = bl;
            btFactorial.Visible = bl;
            modsciBT.Visible = bl;
            open_bracket.Visible = bl;
            close_bracket.Visible = bl;
            nonameTB.Visible = bl;
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
            binaryPN.Visible = bl;
            basePN.Visible = bl;
            modsciBT.Visible = bl;
            RoRBT.Visible = bl;
            RoLBT.Visible = bl;
            RshBT.Visible = bl;
            LshBT.Visible = bl;
            orBT.Visible = bl;
            XorBT.Visible = bl;
            NotBT.Visible = bl;
            AndBT.Visible = bl;
            nonameTB.Visible = bl;
            openproBT.Visible = bl;
            closeproBT.Visible = bl;
            modproBT.Visible = bl;
            btnA.Visible = bl;
            btnB.Visible = bl;
            btnC.Visible = bl;
            btnD.Visible = bl;
            btnE.Visible = bl;
            btnF.Visible = bl;
            str = "0";
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
            statisticsPN.Visible = bl;
            CAD.Visible = bl;
            clearsta.Visible = bl;
            fe_bt.Visible = bl;
            Expsta_bt.Visible = bl;
            AddstaBT.Visible = bl;
            datasetTSMI.Visible = bl;
            str = "0";
            #endregion
        }
        /// <summary>
        /// enable các controls đã bị disable
        /// </summary>
        private void enableComponent()
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

            doidau.Enabled = true;
            btFactorial.Enabled = true;
            sqrt_bt.Enabled = true;
            percent_bt.Enabled = true;
            invert_bt.Enabled = true;
            btdot.Enabled = true;
        }
        /// <summary>
        /// sự kiện radio button của form programmer checked changed
        /// </summary>
        private void baseRBCheckedChanged()
        {
            this.num2.Location = new Point(267, 293);
            this.num2.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num3.Location = new Point(309, 293);
            this.num3.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num4.Location = new Point(225, 257);
            this.num4.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num5.Location = new Point(267, 257);
            this.num5.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num6.Location = new Point(309, 257);
            this.num6.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num7.Location = new Point(225, 221);
            this.num7.Enabled = (octRB.Checked || decRB.Checked || hexRB.Checked);

            this.num8.Location = new Point(267, 221);
            this.num8.Enabled = (decRB.Checked || hexRB.Checked);

            this.num9.Location = new Point(309, 221);
            this.num9.Enabled = (decRB.Checked || hexRB.Checked);
            // 
            // base buttons
            // 
            this.btnA.Location = new Point(183, 149);
            this.btnA.Enabled = (hexRB.Checked);

            this.btnB.Location = new Point(183, 185);
            this.btnB.Enabled = (hexRB.Checked);

            this.btnC.Location = new Point(183, 221);
            this.btnC.Enabled = (hexRB.Checked);

            this.btnD.Location = new Point(183, 257);
            this.btnD.Enabled = (hexRB.Checked);

            this.btnE.Location = new Point(183, 293);
            this.btnE.Enabled = (hexRB.Checked);

            this.btnF.Location = new Point(183, 329);
            this.btnF.Enabled = (hexRB.Checked);

            if (binRB.Checked) pasteTSMI.Enabled = Binary.CheckIsBin(Clipboard.GetText().Trim());
            if (octRB.Checked) pasteTSMI.Enabled = Binary.CheckIsOct(Clipboard.GetText().Trim());
            if (decRB.Checked) pasteTSMI.Enabled = Binary.CheckIsDec(Clipboard.GetText().Trim());
            if (hexRB.Checked) pasteTSMI.Enabled = Binary.CheckIsHex(Clipboard.GetText().Trim());
            pasteCTMN.Enabled = pasteTSMI.Enabled;
        }
        /// <summary>
        /// sin or arcsin
        /// </summary>
        private void invertFunction(bool inv)
        {
            if (inv)
            {
                sin_bt.ForeColor = Color.Red;
                cos_bt.ForeColor = Color.Red;
                tan_bt.ForeColor = Color.Red;
            }
            else
            {
                sin_bt.ForeColor = Color.Black;
                cos_bt.ForeColor = Color.Black;
                tan_bt.ForeColor = Color.Black;
            }
            inv_bt.Enabled = !inv;
        }
        /// <summary>
        /// nút F-E
        /// </summary>
        private void buttonFE()
        {
            invertFunction(false);
            double screenNumber = double.Parse(str);
            int base_ = 0;
            // khong co 'e'
            if (str.IndexOf('E') <= 0)
            {
                if (Math.Abs(screenNumber) >= 10)
                {
                    do
                    {
                        screenNumber /= 10;
                        base_++;
                    } while (Math.Abs(screenNumber) >= 10);
                    str = screenNumber + "E+" + base_;
                }
                if (Math.Abs(screenNumber) < 1 && screenNumber != 0)
                {
                    do
                    {
                        screenNumber *= 10;
                        base_--;
                    } while (Math.Abs(screenNumber) < 1);
                    str = screenNumber + "E" + base_;
                }
            }
            else { str = misc.ToDouble(str); }
            confirm_num = true;

            if (!digitGroupingTSMI.Checked)
                scr_lb.Text = str;
            else
            {
                if (str.IndexOf('E') < 0)
                    scr_lb.Text = misc.grouping(str);
                else
                    scr_lb.Text = str;
            }
        }
        /// <summary>
        /// trả về 1 chuỗi là cách hiển thị số thực kiểu Mỹ
        /// Vd ở VN 2,54 thì ở Mỹ là 2.54
        /// </summary>
        private string getUSNumber(object num)
        {
            return num.ToString().Replace(decimalSym, ".");
        }
        /// <summary>
        /// copy - ctrl c
        /// </summary>
        private void copyCommand()
        {
            Clipboard.SetText(str);
        }
        /// <summary>
        /// paste - ctrl v
        /// </summary>
        private void pasteCommand()
        {
            if (misc.isNumber(Clipboard.GetText().Trim()) && !programmerTSMI.Checked)
                str = Clipboard.GetText().Trim().Replace(thousandSym, "");
            if (programmerTSMI.Checked)
            {
                if (Binary.CheckIsBin(Clipboard.GetText().Trim()) && binRB.Checked)
                    str = Clipboard.GetText().Trim().Replace(thousandSym, "");
                if (Binary.CheckIsOct(Clipboard.GetText().Trim()) && octRB.Checked)
                    str = Clipboard.GetText().Trim().Replace(thousandSym, "");
                if (Binary.CheckIsDec(Clipboard.GetText().Trim()) && decRB.Checked)
                    str = Clipboard.GetText().Trim().Replace(thousandSym, "");
                if (Binary.CheckIsHex(Clipboard.GetText().Trim()) && hexRB.Checked)
                    str = Clipboard.GetText().Trim().Replace(thousandSym, "").ToUpper();
            }
            displayToScreen();
            if (programmerTSMI.Checked) screenToPanel();
        }
        /// <summary>
        /// xử lý số trên bộ nhớ
        /// </summary>
        private void mem_process(int meth)
        {
            double scr_num = 0;
            try
            {
                if (meth == 1)
                {
                    mem_num += double.Parse(scr_lb.Text);
                    scr_num = double.Parse(scr_lb.Text);
                }
                if (meth == 2)
                {
                    mem_num -= double.Parse(scr_lb.Text);
                    scr_num = double.Parse(scr_lb.Text);
                }
                if (meth == 3)
                {
                    mem_num = double.Parse(scr_lb.Text);
                }
                displayToScreen();
            }
            catch (Exception) { goto breakpoint; }

            mem_lb.Visible = (mem_num != 0);
            setMemoryNumber();
            toolTip1.SetToolTip(mem_lb, "M = " + mem_num);
            str = scr_lb.Text;
            breakpoint: confirm_num = true;
        }
        /// <summary>
        /// các hàm tính nâng cao
        /// </summary>
        private void math_func(int func_name)
        {
            double result = 0, inp_num = 0;
            decimal resultlong = 1;
            long somu = 0;
            try
            {
                inp_num = double.Parse(str);
            }
            catch (Exception) { goto breakpoint; }
            if (func_name == 17)    // 1/x
            {
                if (inp_num != 0)
                { 
                    result = 1 / inp_num; 
                    //str = "" + inp_num; 
                }
                else
                { 
                    str = "Infinity";
                    goto breakpoint;
                }
            }
            if (func_name == 19)
            {
                if (inp_num >= 0)
                {
                    result = Math.Sqrt(inp_num);
                    str = result.ToString();
                }
                else { str = "Math Error"; goto breakpoint; }
            }
            if (func_name == 28 && inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Sin(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Sin(inp_num);
                if (gra_rb.Checked) result = Math.Sin(inp_num / 200 * Math.PI);
            }
            if (func_name == 29 && inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Cos(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Cos(inp_num);
                if (gra_rb.Checked) result = Math.Cos(inp_num / 200 * Math.PI);
            }
            if (func_name == 30 && inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Tan(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Tan(inp_num);
                if (gra_rb.Checked) result = Math.Tan(inp_num / 200 * Math.PI);
            }

            if (func_name == 28 && !inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Asin(inp_num) / Math.PI * 180;
                if (rad_rb.Checked) result = Math.Asin(inp_num);
                if (gra_rb.Checked) result = Math.Asin(inp_num) / Math.PI * 200;
            }
            if (func_name == 29 && !inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Acos(inp_num) / Math.PI * 180; 
                if (rad_rb.Checked) result = Math.Acos(inp_num);
                if (gra_rb.Checked) result = Math.Acos(inp_num) / Math.PI * 200;
            }
            if (func_name == 30 && !inv_bt.Enabled)
            {
                if (deg_rb.Checked) result = Math.Atan(inp_num) / Math.PI * 180;
                if (rad_rb.Checked) result = Math.Atan(inp_num);
                if (gra_rb.Checked) result = Math.Atan(inp_num) / Math.PI * 200;
            }

            if (func_name == 31) result = Math.Log(inp_num, Math.E);
            if (func_name == 32)
            {
                if (inp_num - (long)inp_num == 0)
                {
                    #region calculate the factorial
                    //str = Number.Factorial(str, decimalSym);
                    for (long i = 1; i <= (long)inp_num; i++)
                    {
                        resultlong *= i;
                        if (resultlong > (decimal)1e20 && inp_num > 27)
                        {
                            do
                            {
                                resultlong /= (decimal)10;
                                somu++;
                            } while (resultlong > (decimal)10);
                        }
                    }
                    if (somu > 20)
                        while (resultlong > (decimal)10)
                        {
                            resultlong /= (decimal)10;
                            somu++;
                        } 
                    #endregion
                }
                else return;
            }
            if (func_name == 33) result = Math.Exp(inp_num);
            if (func_name == 38) result = inp_num * inp_num;
            if (func_name == 39) result = inp_num * inp_num * inp_num;
            if (func_name == 41)
            {
                result = Math.Exp(Math.Log10(inp_num) / 3 / Math.Log10(Math.E));
            }
            //if (func_name == "nvx") result = inp_num * inp_num * inp_num;
            if (func_name == 40) result = Miscellaneous.luythua(10, inp_num);
            if (func_name == 42) result = Math.Log10(inp_num);
            if (func_name == 35) result = Math.Sinh(inp_num);
            if (func_name == 36) result = Math.Cosh(inp_num);
            if (func_name == 37) result = Math.Tanh(inp_num);

            //if ("" + result != "NaN") str = "" + result;
            //else str = "Math Error";
            if (func_name != 32) str = "" + result;
            else
            {
                str = "" + resultlong;
                if (somu > 15) str += "E+" + somu;
            }
            invertFunction(false);

            breakpoint: displayToScreen();
            confirm_num = true;
        } 
        /// <summary>
        /// nút backspace
        /// </summary>
        private void backspaceclicked()
        {
            invertFunction(false);
            if (!confirm_num)
            {
                if (str.Length > 1)
                {
                    if (str.Length == 2 & str.StartsWith("-"))
                    {
                        str = "0";
                    }
                    else
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                    confirm_num = str.Length == 2 & str.StartsWith("-");
                }
                else
                {
                    str = "0";
                    confirm_num = true;
                }
            }
            else str = "0";
            if (programmerTSMI.Checked) screenToPanel();
            displayToScreen();
            pre_bt = 22;
        }
        /// <summary>
        /// nút bằng
        /// </summary>
        private void equalclicked()
        {
            invertFunction(false);
            // thay input_str = 2,3 bằng str_ = 2.3
            confirm_num = true;
            if (standardTSMI.Checked)       // standard.checked
            {
                #region standard operation
                if (pre_oprt >= 12)
                {
                    num_1 = result;
                    num_2 = double.Parse(str);
                }
                else result = double.Parse(str);
                string oper = "";
                if (pre_oprt == 12) { result = num_1 + num_2; oper = "+"; }
                if (pre_oprt == 13) { result = num_1 - num_2; oper = "-"; }
                if (pre_oprt == 14) { result = num_1 * num_2; oper = "*"; }
                if (pre_oprt == 15) { result = num_1 / num_2; oper = "/"; }

                #region thu gọn lại cho đỡ bị trồi ra
                if (historyDGV.Rows.Count < 4)
                {
                    historyDGV.Rows.Add();
                    clearHistoryBT.Enabled = true;
                    if (historyDGV[0, 0].Value != null)
                    {
                        //historyDGV.Rows.Add();
                        int count = historyDGV.Rows.Count;
                        //historyDGV[0, count - 2].Value = historyDGV[0, count - 1].Value;
                        if (oper != "")
                            historyDGV[0, count - 1].Value = getUSNumber("" + num_1) + oper + getUSNumber("" + num_2);
                        else historyDGV[0, count - 1].Value = getUSNumber("" + result);
                    }
                    else
                    {
                        historyDGV[0, 0].Value = getUSNumber("" + num_1) +
                            oper + getUSNumber("" + num_2);
                    }
                }
                else
                {
                    historyDGV[0, 0].Value = historyDGV[0, 1].Value;
                    historyDGV[0, 1].Value = historyDGV[0, 2].Value;
                    historyDGV[0, 2].Value = historyDGV[0, 3].Value;
                    if (oper != "")
                        historyDGV[0, historyDGV.Rows.Count - 1].Value =
                            getUSNumber("" + num_1) + oper + getUSNumber("" + num_2);
                    else historyDGV[0, historyDGV.Rows.Count - 1].Value = getUSNumber("" + result);
                }
                #endregion

                str = "" + result;
                historyDGV.CurrentCell = null;
                displayToScreen(); 
                #endregion
            }
            if (scientificTSMI.Checked)     // scientific.checked
            {
                #region scientific operation
                if (pre_oprt == 30 || pre_oprt == 34)
                {
                    expressionpow += str;
                    if (pre_oprt == 30)
                        str = "" + misc.power(expressionpow);
                    else
                        str = "" + misc.power_inv(expressionpow);
                    sci_expression += getUSNumber(str);
                    str = "" + misc.Evaluate(sci_expression);
                }
                else
                {
                    if (str[0] != '-') sci_expression += getUSNumber(str);
                    else sci_expression += "(" + getUSNumber(str) + ")";

                    str = "" + misc.Evaluate(sci_expression);
                }

                #region write to history panel
                clearHistoryBT.Enabled = true;
                if (historyDGV.Rows.Count < 4)
                {
                    historyDGV.Rows.Add();
                    if (historyDGV[0, 0].Value == null)
                        historyDGV.Rows[0].SetValues(sci_expression);
                    else
                    {
                        //historyDGV.Rows.Add();
                        int count = historyDGV.Rows.Count;
                        //historyDGV[0, count - 2].Value = historyDGV[0, count - 1].Value;
                        historyDGV[0, count - 1].Value = sci_expression;
                    }
                }
                else
                {
                    historyDGV[0, 0].Value = historyDGV[0, 1].Value;
                    historyDGV[0, 1].Value = historyDGV[0, 2].Value;
                    historyDGV[0, 2].Value = historyDGV[0, 3].Value;
                    historyDGV[0, 3].Value = sci_expression;
                }
                historyDGV.CurrentCell = null; 
                #endregion

                displayToScreen(); 
                #endregion
            }
            if (programmerTSMI.Checked)     // programmer.checked
            {
                #region programmer operation
                if (pre_oprt != 0)
                {
                    num1pro = resultpro;
                    num2pro = long.Parse(decnum);
                }
                if (pre_oprt >= 12)
                    num2pro = long.Parse(decnum);
                else
                    resultpro = long.Parse(decnum);
                if (pre_oprt == 12) resultpro = num1pro + num2pro;
                if (pre_oprt == 13) resultpro = num1pro - num2pro;
                if (pre_oprt == 14) resultpro = num1pro * num2pro;
                if (pre_oprt == 15) resultpro = num1pro / num2pro;

                programmerOperation();
                #endregion
            }
            displayToScreen();
            pre_oprt = 0;
            pre_bt = 16;
            num_1 = result;
            num_2 = 0;
            sci_expression = "";
            operator_lb.Visible = false;
        }
        /// <summary>
        /// statistics add
        /// </summary>
        private void statistics_add()
        {
            #region source cu
            //if (statisticsDGV.Rows.Count == 1 && statisticsDGV[0, 0].Value == null)
            //{
            //    statisticsDGV[0, 0].Value = str;
            //}
            //else
            //{
            //    statisticsDGV.Rows.Add();
            //    int sta_row = statisticsDGV.Rows.Count;
            //    statisticsDGV[0, sta_row - 2].Value = statisticsDGV[0, sta_row - 1].Value;
            //    statisticsDGV[0, sta_row - 1].Value = str;
            //}
            //countlb.Text = "Count = " + statisticsDGV.Rows.Count;
            //confirm_num = true; 
            #endregion
            statisticsDGV.Rows.Add();
            statisticsDGV[0, statisticsDGV.Rows.Count - 1].Value = str;
            confirm_num = true;
            statisticsDGV.CurrentCell = null;
            countlb.Text = "Count = " + statisticsDGV.Rows.Count;
            clearDataSetCTMN.Enabled = true;
            clearDatasetTSMI.Enabled = true;
        }
        /// <summary>
        /// đưa kết quả các phép tính +-*/ của form programmer lên màn hình
        /// </summary>
        private void programmerOperation()
        {
            binnum = ConvertNumber.dec_to_other("" + resultpro, 1);
            octnum = ConvertNumber.dec_to_other("" + resultpro, 2);
            decnum = "" + resultpro;
            hexnum = ConvertNumber.dec_to_other("" + resultpro, 4);
            binnum64 = binnum;
            while (binnum64.Length < 64) binnum64 = "0" + binnum64;
            for (int i = 0; i < 16; i++)
            {
                bit_digit[i].Text = binnum64.Substring(64 - (i + 1) * 4, 4);
            }

            if (binRB.Checked) { str = binnum; scr_lb.Text = str; }
            if (octRB.Checked) { str = octnum; scr_lb.Text = str; }
            if (decRB.Checked) { str = decnum; scr_lb.Text = str; }
            if (hexRB.Checked) { str = hexnum; scr_lb.Text = str; }
        }
        /// <summary>
        /// nút xoá số
        /// </summary>
        private void clear_num(bool ce_bt)
        {
            invertFunction(false);
            str = "0";
            scr_lb.Text = "0";
            confirm_num = true;
            if (ce_bt)
            {
                //if (scientificTSMI.Checked) label1.Text = "";
                sci_expression = "";
                num_1 = 0;
                num_2 = 0;
                result = 0;
                pre_oprt = 0;
                pre_bt = -1;
                operator_lb.Visible = false;
            }
            for (int i = 0; i < 16; i++) bit_digit[i].Text = "0000";
            screenToPanel();
        }
        /// <summary>
        /// hiển thị số lên màn hình
        /// </summary>
        private void displayToScreen()
        {
            if (programmerTSMI.Checked) // nhóm cho form programmer
            {
                if (digitGroupingTSMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = misc.grouping(str);
                    if (octRB.Checked) scr_lb.Text = misc.grouping(str, 3);
                    if (binRB.Checked || hexRB.Checked) 
                        scr_lb.Text = misc.grouping(str, 4);
                }
                else scr_lb.Text = misc.de_group(str);
            }
            else                        // nhóm cho form non-programmer
            {
                if (digitGroupingTSMI.Checked)
                {
                    if (str.IndexOf("Infinity") < 0)
                    {
                        if (str.IndexOf('E') < 0) 
                            scr_lb.Text = misc.grouping(str);
                        else 
                            scr_lb.Text = str;
                    }
                    else
                        scr_lb.Text = str;
                }
                else
                {
                    scr_lb.Text = misc.de_group(str);
                }
            }
        }
        /// <summary>
        /// nhập số
        /// </summary>
        private void numinput(int index)
        {
            int bigscreen = (scientificTSMI.Checked || programmerTSMI.Checked).GetHashCode();
            invertFunction(false);
            if (confirm_num)
            {
                pre_bt = -1;
            }
            if (index < 10)
            {
                if (confirm_num) str = "0";
                if (str.Length < 16 + bigscreen * 16 && str.IndexOf(decimalSym) < 0)
                {
                    if (str != "0") str += index;
                    else str = index.ToString();
                }
                if (str.IndexOf(decimalSym) > 0 && (str.Length < 16 + bigscreen * 16))
                {
                    str += index;
                }
                confirm_num = false;
            }
            if (index == 10)
            {
                if (confirm_num) str = "0" + decimalSym;
                if (str.IndexOf(decimalSym) < 0) str += decimalSym;
                confirm_num = false;
            }
            if (index == 11)
            {
                if (str != "0")
                {
                    if (str[0] != '-') str = "-" + str;
                    else str = str.Substring(1);
                }
            }
            pre_bt = index;
            if (programmerTSMI.Checked) screenToPanel();
            displayToScreen();
        }
        /// <summary>
        /// các nút từ A đến F
        /// </summary>
        private void buttonAF(string text)
        {
            if (confirm_num)
                str = text;
            else
                str += text;
            confirm_num = false;
            prcmdkey = true;
            displayToScreen();
            screenToPanel();
        }

        double num_1 = 0, num_2 = 0, result = 0;
        string pre_op = "";
        /// <summary>
        /// các toán tử +-*/ của form standard
        /// </summary>
        private void std_operation(int index)
        {
            if (index == 12) operator_lb.Text = "+";
            if (index == 13) operator_lb.Text = "-";
            if (index == 14) operator_lb.Text = "*";
            if (index == 15) operator_lb.Text = "/"; 
            if (misc.isNumber(scr_lb.Text))
            {
                confirm_num = true;
                if (pre_oprt == 0)
                {
                    num_1 = double.Parse(str);
                    result = num_1;
                }
                else
                    if (pre_bt < 12 || pre_bt > 15)
                    {
                        num_1 = result;
                        num_2 = double.Parse(scr_lb.Text);
                        if (pre_oprt == 12) { result = num_1 + num_2; pre_op = "+"; }
                        if (pre_oprt == 13) { result = num_1 - num_2; pre_op = "-"; }
                        if (pre_oprt == 14) { result = num_1 * num_2; pre_op = "*"; }
                        if (pre_oprt == 15) { result = num_1 / num_2; pre_op = "/"; }

                        #region thu gọn lại cho đỡ bị trồi ra
                        clearHistoryBT.Enabled = true;
                        if (historyDGV.Rows.Count < 4)
                        {
                            historyDGV.Rows.Add();
                            if (historyDGV[0, 0].Value != null)
                            {
                                //historyDGV.Rows.Add();
                                int count = historyDGV.Rows.Count;
                                //historyDGV[0, count - 2].Value = historyDGV[0, count - 1].Value;
                                historyDGV[0, count - 1].Value = getUSNumber("" + num_1) +
                                    pre_op + getUSNumber("" + num_2);
                            }
                            else
                            {
                                historyDGV[0, 0].Value = getUSNumber("" + num_1) +
                                    pre_op + getUSNumber("" + num_2);
                            }
                        }
                        else
                        {
                            historyDGV[0, 0].Value = historyDGV[0, 1].Value;
                            historyDGV[0, 1].Value = historyDGV[0, 2].Value;
                            historyDGV[0, 2].Value = historyDGV[0, 3].Value;
                            historyDGV[0, 3].Value = getUSNumber("" + num_1) +
                                operator_lb.Text + getUSNumber("" + num_2);
                        }
                        #endregion
                    }
                if (programmerTSMI.Checked) screenToPanel();
                str = "" + result;
                displayToScreen();
                historyDGV.CurrentCell = null;
                pre_oprt = index;
                pre_bt = index;
                operator_lb.Visible = true;
            }
        }
        /// <summary>
        /// các toán tử +-*/ của form scientific
        /// </summary>
        private void sci_operation(int index)
        {
            invertFunction(false);
            if (misc.isNumber(scr_lb.Text))
            {
                if (index == 12) operator_lb.Text = "+";
                if (index == 13) operator_lb.Text = "-";
                if (index == 14) operator_lb.Text = "*";
                if (index == 15) operator_lb.Text = "/";
                if (index == 30) operator_lb.Text = "^";
                if (index == 34) operator_lb.Text = "√";
                if (pre_oprt != 0)
                {
                    if (pre_bt < 12 || pre_bt > 15)
                    {
                        // sau = +-
                        if (index == 12 || index == 13)
                        {
                            if (pre_oprt == 30 || pre_oprt == 34)
                            {
                                //sci_expression += str_;
                                expressionpow += str;
                                if (pre_oprt == 30)
                                    str = "" + misc.power(expressionpow);
                                else
                                    str = "" + misc.power_inv(expressionpow);
                                sci_expression += getUSNumber(str);
                                str = misc.Evaluate(sci_expression).ToString();
                            }
                            else
                            {
                                if (str[0] != '-') sci_expression += getUSNumber(str);
                                else sci_expression += "(" + getUSNumber(str) + ")";

                                str = misc.Evaluate(sci_expression).ToString();
                            }
                            sci_expression += operator_lb.Text;
                        }
                        // sau = */
                        if (index == 14 || index == 15)
                        {
                            if (pre_oprt == 30 || pre_oprt == 34)
                            {
                                expressionpow += getUSNumber(str);
                                if (pre_oprt == 30)
                                    str = "" + misc.power(expressionpow);
                                else
                                    str = "" + misc.power_inv(expressionpow);
                                sci_expression += getUSNumber(str) + operator_lb.Text;
                            }
                            else 
                                sci_expression += getUSNumber(str) + operator_lb.Text;
                        }
                        // sau = x^y hoac y√x (x^1/y)
                        if (index == 30 || index == 34)
                        {
                            if (pre_oprt != index)
                                expressionpow = getUSNumber(str) + operator_lb.Text;
                            else
                            {
                                expressionpow += getUSNumber(str);
                                if (index == 30)
                                    expressionpow = misc.power(expressionpow) + operator_lb.Text;
                                else
                                    expressionpow = misc.power_inv(expressionpow) + operator_lb.Text;
                            }
                        }
                    }
                    else    // (pre_bt >= 12 && pre_bt <= 15)
                    {
                        sci_expression = sci_expression.Substring(0,
                            sci_expression.Length - 1) + operator_lb.Text;
                    }
                }
                else        // (pre_oprt == 0)
                {
                    if (index >= 12 && index <= 15)
                    {
                        sci_expression = getUSNumber(str) + operator_lb.Text;
                    }
                    if (index == 30 || index == 34)
                        expressionpow = str + operator_lb.Text;
                }
            }

            pre_bt = index;
            operator_lb.Visible = true;
            displayToScreen();
            pre_oprt = index;
            confirm_num = true;
        }

        long num1pro, num2pro = 0, resultpro = 0;
        /// <summary>
        /// các toán tử +-*/ của form programmer
        /// </summary>
        private void pro_operation(int index)
        {
            if (index == 12) operator_lb.Text = "+";
            if (index == 13) operator_lb.Text = "-";
            if (index == 14) operator_lb.Text = "*";
            if (index == 15) operator_lb.Text = "/";

            confirm_num = true;
            if (pre_oprt == 0)
            {
                num1pro = long.Parse(decnum);
                resultpro = num1pro;
                goto jump;
            }
            else
            {
                num1pro = resultpro;
                num2pro = long.Parse(decnum);
                if (pre_oprt == 12) resultpro = num1pro + num2pro;
                if (pre_oprt == 13) resultpro = num1pro - num2pro;
                if (pre_oprt == 14) resultpro = num1pro * num2pro;
                if (pre_oprt == 15) resultpro = (long)(num1pro / num2pro);
            }
            programmerOperation();
            jump: pre_oprt = index;
            pre_bt = index;
            operator_lb.Visible = true;
        }
        /// <summary>
        /// các toán tử bitwise (and, or, xor)
        /// </summary>
        private void bitwiseoperators()
        {
            // DO SOMETHING HERE
        }
        /// <summary>
        /// mở file help
        /// </summary>
        private void helptopics()
        {
            try
            {
                System.Diagnostics.Process.Start("shortcuts-key.pdf");
            }
            catch { }
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
            this.mem_lb = new System.Windows.Forms.Label();
            this.operator_lb = new System.Windows.Forms.Label();
            this.scr_lb = new System.Windows.Forms.Label();
            this.angleGB = new System.Windows.Forms.GroupBox();
            this.gra_rb = new System.Windows.Forms.RadioButton();
            this.rad_rb = new System.Windows.Forms.RadioButton();
            this.deg_rb = new System.Windows.Forms.RadioButton();
            this.nonameTB = new System.Windows.Forms.TextBox();
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
            this.fe_bt = new System.Windows.Forms.Button();
            this.exp_bt = new System.Windows.Forms.Button();
            this.modsciBT = new System.Windows.Forms.Button();
            this.ln_bt = new System.Windows.Forms.Button();
            this.dms_bt = new System.Windows.Forms.Button();
            this.tanh_bt = new System.Windows.Forms.Button();
            this.cosh_bt = new System.Windows.Forms.Button();
            this.inv_bt = new System.Windows.Forms.Button();
            this.int_bt = new System.Windows.Forms.Button();
            this.tan_bt = new System.Windows.Forms.Button();
            this.sinh_bt = new System.Windows.Forms.Button();
            this.cos_bt = new System.Windows.Forms.Button();
            this.sin_bt = new System.Windows.Forms.Button();
            this.btFactorial = new System.Windows.Forms.Button();
            this.screenPN = new System.Windows.Forms.Panel();
            this.historyPN = new System.Windows.Forms.Panel();
            this.dnBT = new System.Windows.Forms.Button();
            this.upBT = new System.Windows.Forms.Button();
            this.clearHistoryBT = new System.Windows.Forms.Button();
            this.historyDGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unknownPN = new System.Windows.Forms.Panel();
            this.byteRB = new System.Windows.Forms.RadioButton();
            this.qwordRB = new System.Windows.Forms.RadioButton();
            this.wordRB = new System.Windows.Forms.RadioButton();
            this.dwordRB = new System.Windows.Forms.RadioButton();
            this.basePN = new System.Windows.Forms.Panel();
            this.binRB = new System.Windows.Forms.RadioButton();
            this.octRB = new System.Windows.Forms.RadioButton();
            this.decRB = new System.Windows.Forms.RadioButton();
            this.hexRB = new System.Windows.Forms.RadioButton();
            this.binaryPN = new System.Windows.Forms.Panel();
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
            this.orBT = new System.Windows.Forms.Button();
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
            this.statisticsPN = new System.Windows.Forms.Panel();
            this.countlb = new System.Windows.Forms.Label();
            this.statisticsDGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clearsta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).BeginInit();
            this.unitconvGB.SuspendLayout();
            this.datecalcGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).BeginInit();
            this.angleGB.SuspendLayout();
            this.screenPN.SuspendLayout();
            this.historyPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyDGV)).BeginInit();
            this.unknownPN.SuspendLayout();
            this.basePN.SuspendLayout();
            this.statisticsPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statisticsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // num1
            // 
            this.num1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num1.Location = new System.Drawing.Point(12, 209);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(36, 30);
            this.num1.TabIndex = 1;
            this.num1.TabStop = false;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            this.num1.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num2
            // 
            this.num2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num2.Location = new System.Drawing.Point(54, 209);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(36, 30);
            this.num2.TabIndex = 2;
            this.num2.TabStop = false;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            this.num2.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num3
            // 
            this.num3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num3.Location = new System.Drawing.Point(96, 209);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(36, 30);
            this.num3.TabIndex = 3;
            this.num3.TabStop = false;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            this.num3.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num4
            // 
            this.num4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num4.Location = new System.Drawing.Point(12, 175);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(36, 30);
            this.num4.TabIndex = 4;
            this.num4.TabStop = false;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            this.num4.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num5
            // 
            this.num5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num5.Location = new System.Drawing.Point(54, 175);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(36, 30);
            this.num5.TabIndex = 5;
            this.num5.TabStop = false;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            this.num5.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num6
            // 
            this.num6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num6.Location = new System.Drawing.Point(96, 175);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(36, 30);
            this.num6.TabIndex = 6;
            this.num6.TabStop = false;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            this.num6.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num7
            // 
            this.num7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num7.Location = new System.Drawing.Point(12, 137);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(36, 30);
            this.num7.TabIndex = 7;
            this.num7.TabStop = false;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            this.num7.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num8
            // 
            this.num8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num8.Location = new System.Drawing.Point(54, 137);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(36, 30);
            this.num8.TabIndex = 8;
            this.num8.TabStop = false;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            this.num8.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num9
            // 
            this.num9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num9.Location = new System.Drawing.Point(96, 137);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(36, 30);
            this.num9.TabIndex = 9;
            this.num9.TabStop = false;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            this.num9.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // num0
            // 
            this.num0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.num0.Location = new System.Drawing.Point(12, 246);
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(77, 30);
            this.num0.TabIndex = 0;
            this.num0.TabStop = false;
            this.num0.Text = "0";
            this.num0.UseVisualStyleBackColor = true;
            this.num0.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // btdot
            // 
            this.btdot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btdot.Location = new System.Drawing.Point(96, 246);
            this.btdot.Name = "btdot";
            this.btdot.Size = new System.Drawing.Size(36, 30);
            this.btdot.TabIndex = 10;
            this.btdot.TabStop = false;
            this.btdot.Text = ".";
            this.btdot.UseVisualStyleBackColor = true;
            this.btdot.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // addbt
            // 
            this.addbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addbt.Location = new System.Drawing.Point(139, 246);
            this.addbt.Name = "addbt";
            this.addbt.Size = new System.Drawing.Size(36, 30);
            this.addbt.TabIndex = 12;
            this.addbt.TabStop = false;
            this.addbt.Text = "+";
            this.addbt.UseVisualStyleBackColor = true;
            this.addbt.Click += new System.EventHandler(this.operatorBT_Click);
            // 
            // minusbt
            // 
            this.minusbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.minusbt.Location = new System.Drawing.Point(139, 209);
            this.minusbt.Name = "minusbt";
            this.minusbt.Size = new System.Drawing.Size(36, 30);
            this.minusbt.TabIndex = 13;
            this.minusbt.TabStop = false;
            this.minusbt.Text = "-";
            this.minusbt.UseVisualStyleBackColor = true;
            this.minusbt.Click += new System.EventHandler(this.operatorBT_Click);
            // 
            // mulbt
            // 
            this.mulbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mulbt.Location = new System.Drawing.Point(139, 175);
            this.mulbt.Name = "mulbt";
            this.mulbt.Size = new System.Drawing.Size(36, 30);
            this.mulbt.TabIndex = 14;
            this.mulbt.TabStop = false;
            this.mulbt.Text = "*";
            this.mulbt.UseVisualStyleBackColor = true;
            this.mulbt.Click += new System.EventHandler(this.operatorBT_Click);
            // 
            // divbt
            // 
            this.divbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.divbt.Location = new System.Drawing.Point(139, 137);
            this.divbt.Name = "divbt";
            this.divbt.Size = new System.Drawing.Size(36, 30);
            this.divbt.TabIndex = 15;
            this.divbt.TabStop = false;
            this.divbt.Text = "/";
            this.divbt.UseVisualStyleBackColor = true;
            this.divbt.Click += new System.EventHandler(this.operatorBT_Click);
            // 
            // equal
            // 
            this.equal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.equal.Location = new System.Drawing.Point(182, 209);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(36, 67);
            this.equal.TabIndex = 16;
            this.equal.TabStop = false;
            this.equal.Text = "=";
            this.equal.UseVisualStyleBackColor = true;
            this.equal.Click += new System.EventHandler(this.equal_Click);
            // 
            // invert_bt
            // 
            this.invert_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.invert_bt.Location = new System.Drawing.Point(182, 175);
            this.invert_bt.Name = "invert_bt";
            this.invert_bt.Size = new System.Drawing.Size(36, 30);
            this.invert_bt.TabIndex = 17;
            this.invert_bt.TabStop = false;
            this.invert_bt.Text = "1/x";
            this.invert_bt.UseVisualStyleBackColor = true;
            this.invert_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // percent_bt
            // 
            this.percent_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.percent_bt.Location = new System.Drawing.Point(182, 137);
            this.percent_bt.Name = "percent_bt";
            this.percent_bt.Size = new System.Drawing.Size(36, 30);
            this.percent_bt.TabIndex = 18;
            this.percent_bt.TabStop = false;
            this.percent_bt.Text = "%";
            this.percent_bt.UseVisualStyleBackColor = true;
            this.percent_bt.Click += new System.EventHandler(this.percent_Click);
            // 
            // backspace
            // 
            this.backspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.backspace.Location = new System.Drawing.Point(12, 101);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(36, 30);
            this.backspace.TabIndex = 22;
            this.backspace.TabStop = false;
            this.backspace.Text = "←";
            this.backspace.UseVisualStyleBackColor = true;
            this.backspace.Click += new System.EventHandler(this.backspace_Click);
            // 
            // ce
            // 
            this.ce.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ce.Location = new System.Drawing.Point(54, 101);
            this.ce.Name = "ce";
            this.ce.Size = new System.Drawing.Size(36, 30);
            this.ce.TabIndex = 21;
            this.ce.TabStop = false;
            this.ce.Text = "CE";
            this.ce.UseVisualStyleBackColor = true;
            this.ce.Click += new System.EventHandler(this.ce_Click);
            // 
            // doidau
            // 
            this.doidau.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doidau.Location = new System.Drawing.Point(139, 101);
            this.doidau.Name = "doidau";
            this.doidau.Size = new System.Drawing.Size(36, 30);
            this.doidau.TabIndex = 11;
            this.doidau.TabStop = false;
            this.doidau.Text = "±";
            this.doidau.UseVisualStyleBackColor = true;
            this.doidau.Click += new System.EventHandler(this.numberinput_Click);
            // 
            // memclear
            // 
            this.memclear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memclear.Location = new System.Drawing.Point(12, 65);
            this.memclear.Name = "memclear";
            this.memclear.Size = new System.Drawing.Size(36, 30);
            this.memclear.TabIndex = 23;
            this.memclear.TabStop = false;
            this.memclear.Text = "MC";
            this.memclear.UseVisualStyleBackColor = true;
            this.memclear.Click += new System.EventHandler(this.memclear_Click);
            // 
            // mstore
            // 
            this.mstore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mstore.Location = new System.Drawing.Point(54, 65);
            this.mstore.Name = "mstore";
            this.mstore.Size = new System.Drawing.Size(36, 30);
            this.mstore.TabIndex = 24;
            this.mstore.TabStop = false;
            this.mstore.Text = "MS";
            this.mstore.UseVisualStyleBackColor = true;
            this.mstore.Click += new System.EventHandler(this.mstore_Click);
            // 
            // memrecall
            // 
            this.memrecall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memrecall.Location = new System.Drawing.Point(96, 65);
            this.memrecall.Name = "memrecall";
            this.memrecall.Size = new System.Drawing.Size(36, 30);
            this.memrecall.TabIndex = 25;
            this.memrecall.TabStop = false;
            this.memrecall.Text = "MR";
            this.memrecall.UseVisualStyleBackColor = true;
            this.memrecall.Click += new System.EventHandler(this.memrecall_Click);
            // 
            // sqrt_bt
            // 
            this.sqrt_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sqrt_bt.Location = new System.Drawing.Point(182, 101);
            this.sqrt_bt.Name = "sqrt_bt";
            this.sqrt_bt.Size = new System.Drawing.Size(36, 30);
            this.sqrt_bt.TabIndex = 19;
            this.sqrt_bt.TabStop = false;
            this.sqrt_bt.Text = "√";
            this.sqrt_bt.UseVisualStyleBackColor = true;
            this.sqrt_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // clearbt
            // 
            this.clearbt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearbt.Location = new System.Drawing.Point(96, 101);
            this.clearbt.Name = "clearbt";
            this.clearbt.Size = new System.Drawing.Size(36, 30);
            this.clearbt.TabIndex = 20;
            this.clearbt.TabStop = false;
            this.clearbt.Text = "C";
            this.clearbt.UseVisualStyleBackColor = true;
            this.clearbt.Click += new System.EventHandler(this.clear_Click);
            // 
            // madd
            // 
            this.madd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.madd.Location = new System.Drawing.Point(139, 65);
            this.madd.Name = "madd";
            this.madd.Size = new System.Drawing.Size(36, 30);
            this.madd.TabIndex = 26;
            this.madd.TabStop = false;
            this.madd.Text = "M+";
            this.madd.UseVisualStyleBackColor = true;
            this.madd.Click += new System.EventHandler(this.madd_Click);
            // 
            // mem_minus_bt
            // 
            this.mem_minus_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mem_minus_bt.Location = new System.Drawing.Point(182, 65);
            this.mem_minus_bt.Name = "mem_minus_bt";
            this.mem_minus_bt.Size = new System.Drawing.Size(36, 30);
            this.mem_minus_bt.TabIndex = 27;
            this.mem_minus_bt.TabStop = false;
            this.mem_minus_bt.Text = "M-";
            this.mem_minus_bt.UseVisualStyleBackColor = true;
            this.mem_minus_bt.Click += new System.EventHandler(this.mmul_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // autocal_date
            // 
            this.autocal_date.AutoSize = true;
            this.autocal_date.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autocal_date.Location = new System.Drawing.Point(12, 236);
            this.autocal_date.Name = "autocal_date";
            this.autocal_date.Size = new System.Drawing.Size(105, 18);
            this.autocal_date.TabIndex = 208;
            this.autocal_date.Text = "A&uto Calculate";
            this.autocal_date.UseVisualStyleBackColor = true;
            this.autocal_date.CheckedChanged += new System.EventHandler(this.autocal_cb_CheckedChanged);
            // 
            // calmethodCB
            // 
            this.calmethodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calmethodCB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calmethodCB.FormattingEnabled = true;
            this.calmethodCB.Items.AddRange(new object[] {
            "Calculate the difference between two dates",
            "Add or subtract days to a specified date"});
            this.calmethodCB.Location = new System.Drawing.Point(12, 52);
            this.calmethodCB.Name = "calmethodCB";
            this.calmethodCB.Size = new System.Drawing.Size(316, 22);
            this.calmethodCB.TabIndex = 200;
            this.calmethodCB.SelectedIndexChanged += new System.EventHandler(this.cal_method_SelectedIndexChanged);
            this.calmethodCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typeCB_MouseDown);
            // 
            // calculate_date
            // 
            this.calculate_date.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculate_date.Location = new System.Drawing.Point(253, 230);
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
            this.result2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result2.Location = new System.Drawing.Point(12, 180);
            this.result2.Name = "result2";
            this.result2.ReadOnly = true;
            this.result2.Size = new System.Drawing.Size(316, 22);
            this.result2.TabIndex = 207;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 14);
            this.label5.TabIndex = 67;
            this.label5.Text = "Difference (days)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 14);
            this.label4.TabIndex = 64;
            this.label4.Text = "Difference (years, months, weeks, days)";
            // 
            // dtP2
            // 
            this.dtP2.Checked = false;
            this.dtP2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtP2.Location = new System.Drawing.Point(229, 82);
            this.dtP2.Name = "dtP2";
            this.dtP2.Size = new System.Drawing.Size(99, 22);
            this.dtP2.TabIndex = 202;
            this.dtP2.ValueChanged += new System.EventHandler(this.dtP1_ValueChanged);
            this.dtP2.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(194, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 14);
            this.label3.TabIndex = 60;
            this.label3.Text = "To:";
            // 
            // dtP1
            // 
            this.dtP1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtP1.Location = new System.Drawing.Point(60, 82);
            this.dtP1.Name = "dtP1";
            this.dtP1.Size = new System.Drawing.Size(99, 22);
            this.dtP1.TabIndex = 201;
            this.dtP1.ValueChanged += new System.EventHandler(this.dtP1_ValueChanged);
            this.dtP1.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 14);
            this.label2.TabIndex = 59;
            this.label2.Text = "From:";
            // 
            // result1
            // 
            this.result1.BackColor = System.Drawing.Color.White;
            this.result1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result1.Location = new System.Drawing.Point(12, 130);
            this.result1.Name = "result1";
            this.result1.ReadOnly = true;
            this.result1.Size = new System.Drawing.Size(316, 22);
            this.result1.TabIndex = 206;
            // 
            // periodsDateUD
            // 
            this.periodsDateUD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsDateUD.Location = new System.Drawing.Point(268, 119);
            this.periodsDateUD.Maximum = new decimal(new int[] {
            730000,
            0,
            0,
            0});
            this.periodsDateUD.Name = "periodsDateUD";
            this.periodsDateUD.Size = new System.Drawing.Size(60, 22);
            this.periodsDateUD.TabIndex = 205;
            this.periodsDateUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsDateUD.ThousandsSeparator = true;
            this.periodsDateUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsDateUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // typeLB
            // 
            this.typeLB.AutoSize = true;
            this.typeLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLB.Location = new System.Drawing.Point(16, 23);
            this.typeLB.Name = "typeLB";
            this.typeLB.Size = new System.Drawing.Size(251, 14);
            this.typeLB.TabIndex = 8;
            this.typeLB.Text = "Select the type of unit you want to convert";
            // 
            // fromLB
            // 
            this.fromLB.AutoSize = true;
            this.fromLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromLB.Location = new System.Drawing.Point(16, 86);
            this.fromLB.Name = "fromLB";
            this.fromLB.Size = new System.Drawing.Size(34, 14);
            this.fromLB.TabIndex = 6;
            this.fromLB.Text = "From";
            // 
            // toLB
            // 
            this.toLB.AutoSize = true;
            this.toLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLB.Location = new System.Drawing.Point(16, 163);
            this.toLB.Name = "toLB";
            this.toLB.Size = new System.Drawing.Size(22, 14);
            this.toLB.TabIndex = 7;
            this.toLB.Text = "To";
            // 
            // typeCB
            // 
            this.typeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.typeCB.Location = new System.Drawing.Point(16, 53);
            this.typeCB.MaxDropDownItems = 11;
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(306, 22);
            this.typeCB.TabIndex = 9;
            this.typeCB.SelectedIndexChanged += new System.EventHandler(this.typeCB_SelectedIndexChanged);
            this.typeCB.GotFocus += new System.EventHandler(this.EnableKeyboard);
            // 
            // fromTB
            // 
            this.fromTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromTB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fromTB.Location = new System.Drawing.Point(16, 102);
            this.fromTB.MaxLength = 20;
            this.fromTB.Name = "fromTB";
            this.fromTB.Size = new System.Drawing.Size(306, 22);
            this.fromTB.TabIndex = 10;
            this.fromTB.Text = "Enter value";
            this.fromTB.TextChanged += new System.EventHandler(this.fromTB_TextChanged);
            this.fromTB.GotFocus += new System.EventHandler(this.fromTB_GotFocus);
            this.fromTB.LostFocus += new System.EventHandler(this.fromTB_LostFocus);
            // 
            // toTB
            // 
            this.toTB.BackColor = System.Drawing.SystemColors.Control;
            this.toTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toTB.Location = new System.Drawing.Point(16, 179);
            this.toTB.Name = "toTB";
            this.toTB.ReadOnly = true;
            this.toTB.Size = new System.Drawing.Size(306, 22);
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
            this.unitconvGB.Location = new System.Drawing.Point(234, 9);
            this.unitconvGB.Name = "unitconvGB";
            this.unitconvGB.Size = new System.Drawing.Size(345, 269);
            this.unitconvGB.TabIndex = 31;
            this.unitconvGB.TabStop = false;
            this.unitconvGB.Visible = false;
            this.unitconvGB.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // invert_unit
            // 
            this.invert_unit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invert_unit.Location = new System.Drawing.Point(16, 236);
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
            this.datecalcGB.Location = new System.Drawing.Point(234, 9);
            this.datecalcGB.Name = "datecalcGB";
            this.datecalcGB.Size = new System.Drawing.Size(345, 269);
            this.datecalcGB.TabIndex = 32;
            this.datecalcGB.TabStop = false;
            this.datecalcGB.Visible = false;
            this.datecalcGB.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // subrb
            // 
            this.subrb.AutoSize = true;
            this.subrb.Location = new System.Drawing.Point(254, 83);
            this.subrb.Name = "subrb";
            this.subrb.Size = new System.Drawing.Size(74, 20);
            this.subrb.TabIndex = 202;
            this.subrb.Text = "Subtract";
            this.subrb.UseVisualStyleBackColor = true;
            this.subrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // addrb
            // 
            this.addrb.AutoSize = true;
            this.addrb.Checked = true;
            this.addrb.Location = new System.Drawing.Point(176, 83);
            this.addrb.Name = "addrb";
            this.addrb.Size = new System.Drawing.Size(48, 20);
            this.addrb.TabIndex = 202;
            this.addrb.TabStop = true;
            this.addrb.Text = "Add";
            this.addrb.UseVisualStyleBackColor = true;
            this.addrb.CheckedChanged += new System.EventHandler(this.add_sub_CheckChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 14);
            this.label6.TabIndex = 74;
            this.label6.Text = "Select the date calculation you want";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(225, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 64;
            this.label8.Text = "Day(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(110, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 14);
            this.label7.TabIndex = 64;
            this.label7.Text = "Month(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "Year(s)";
            // 
            // periodsYearUD
            // 
            this.periodsYearUD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsYearUD.Location = new System.Drawing.Point(60, 119);
            this.periodsYearUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.periodsYearUD.Name = "periodsYearUD";
            this.periodsYearUD.Size = new System.Drawing.Size(44, 22);
            this.periodsYearUD.TabIndex = 203;
            this.periodsYearUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsYearUD.ThousandsSeparator = true;
            this.periodsYearUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsYearUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // periodsMonthUD
            // 
            this.periodsMonthUD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodsMonthUD.Location = new System.Drawing.Point(169, 119);
            this.periodsMonthUD.Maximum = new decimal(new int[] {
            24000,
            0,
            0,
            0});
            this.periodsMonthUD.Name = "periodsMonthUD";
            this.periodsMonthUD.Size = new System.Drawing.Size(44, 22);
            this.periodsMonthUD.TabIndex = 204;
            this.periodsMonthUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.periodsMonthUD.ThousandsSeparator = true;
            this.periodsMonthUD.ValueChanged += new System.EventHandler(this.periods_ValueChanged);
            this.periodsMonthUD.Enter += new System.EventHandler(this.DisableKeyboard);
            // 
            // mem_lb
            // 
            this.mem_lb.AutoSize = true;
            this.mem_lb.BackColor = System.Drawing.Color.White;
            this.mem_lb.Location = new System.Drawing.Point(2, 21);
            this.mem_lb.Name = "mem_lb";
            this.mem_lb.Size = new System.Drawing.Size(18, 16);
            this.mem_lb.TabIndex = 21;
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = false;
            this.mem_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.mem_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // operator_lb
            // 
            this.operator_lb.AutoSize = true;
            this.operator_lb.BackColor = System.Drawing.Color.White;
            this.operator_lb.Location = new System.Drawing.Point(2, 2);
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
            this.scr_lb.BackColor = System.Drawing.Color.White;
            this.scr_lb.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scr_lb.Location = new System.Drawing.Point(23, 9);
            this.scr_lb.Name = "scr_lb";
            this.scr_lb.Size = new System.Drawing.Size(178, 22);
            this.scr_lb.TabIndex = 22;
            this.scr_lb.Text = "0";
            this.scr_lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scr_lb.TextChanged += new System.EventHandler(this.scr_lb_TextChanged);
            this.scr_lb.GotFocus += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            this.scr_lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // angleGB
            // 
            this.angleGB.Controls.Add(this.gra_rb);
            this.angleGB.Controls.Add(this.rad_rb);
            this.angleGB.Controls.Add(this.deg_rb);
            this.angleGB.Location = new System.Drawing.Point(9, 298);
            this.angleGB.Name = "angleGB";
            this.angleGB.Size = new System.Drawing.Size(208, 36);
            this.angleGB.TabIndex = 128;
            this.angleGB.TabStop = false;
            // 
            // gra_rb
            // 
            this.gra_rb.AutoSize = true;
            this.gra_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gra_rb.Location = new System.Drawing.Point(143, 12);
            this.gra_rb.Name = "gra_rb";
            this.gra_rb.Size = new System.Drawing.Size(53, 17);
            this.gra_rb.TabIndex = 154;
            this.gra_rb.Text = "Grads";
            this.gra_rb.UseVisualStyleBackColor = true;
            this.gra_rb.CheckedChanged += new System.EventHandler(this.EnableKeyboard);
            // 
            // rad_rb
            // 
            this.rad_rb.AutoSize = true;
            this.rad_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_rb.Location = new System.Drawing.Point(83, 12);
            this.rad_rb.Name = "rad_rb";
            this.rad_rb.Size = new System.Drawing.Size(58, 17);
            this.rad_rb.TabIndex = 154;
            this.rad_rb.Text = "Radian";
            this.rad_rb.UseVisualStyleBackColor = true;
            this.rad_rb.CheckedChanged += new System.EventHandler(this.EnableKeyboard);
            // 
            // deg_rb
            // 
            this.deg_rb.AutoSize = true;
            this.deg_rb.Checked = true;
            this.deg_rb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deg_rb.Location = new System.Drawing.Point(13, 12);
            this.deg_rb.Name = "deg_rb";
            this.deg_rb.Size = new System.Drawing.Size(65, 17);
            this.deg_rb.TabIndex = 154;
            this.deg_rb.TabStop = true;
            this.deg_rb.Text = "Degrees";
            this.deg_rb.UseVisualStyleBackColor = true;
            this.deg_rb.CheckedChanged += new System.EventHandler(this.EnableKeyboard);
            // 
            // nonameTB
            // 
            this.nonameTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nonameTB.Enabled = false;
            this.nonameTB.Location = new System.Drawing.Point(9, 340);
            this.nonameTB.Multiline = true;
            this.nonameTB.Name = "nonameTB";
            this.nonameTB.ReadOnly = true;
            this.nonameTB.Size = new System.Drawing.Size(37, 30);
            this.nonameTB.TabIndex = 129;
            this.nonameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // close_bracket
            // 
            this.close_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_bracket.Location = new System.Drawing.Point(52, 340);
            this.close_bracket.Name = "close_bracket";
            this.close_bracket.Size = new System.Drawing.Size(37, 30);
            this.close_bracket.TabIndex = 152;
            this.close_bracket.Text = ")";
            this.close_bracket.UseVisualStyleBackColor = true;
            // 
            // open_bracket
            // 
            this.open_bracket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.open_bracket.Location = new System.Drawing.Point(52, 340);
            this.open_bracket.Name = "open_bracket";
            this.open_bracket.Size = new System.Drawing.Size(37, 30);
            this.open_bracket.TabIndex = 153;
            this.open_bracket.Text = "(";
            this.open_bracket.UseVisualStyleBackColor = true;
            // 
            // _10x_bt
            // 
            this._10x_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._10x_bt.Location = new System.Drawing.Point(52, 340);
            this._10x_bt.Name = "_10x_bt";
            this._10x_bt.Size = new System.Drawing.Size(37, 30);
            this._10x_bt.TabIndex = 40;
            this._10x_bt.Text = "10ⁿ";
            this._10x_bt.UseVisualStyleBackColor = true;
            this._10x_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // nvx_bt
            // 
            this.nvx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nvx_bt.Location = new System.Drawing.Point(52, 340);
            this.nvx_bt.Name = "nvx_bt";
            this.nvx_bt.Size = new System.Drawing.Size(37, 30);
            this.nvx_bt.TabIndex = 34;
            this.nvx_bt.Text = "ⁿ√x";
            this.nvx_bt.UseVisualStyleBackColor = true;
            this.nvx_bt.Click += new System.EventHandler(this.nvx_bt_Click);
            // 
            // xn_bt
            // 
            this.xn_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xn_bt.Location = new System.Drawing.Point(52, 340);
            this.xn_bt.Name = "xn_bt";
            this.xn_bt.Size = new System.Drawing.Size(37, 30);
            this.xn_bt.TabIndex = 30;
            this.xn_bt.Text = "xⁿ";
            this.xn_bt.UseVisualStyleBackColor = true;
            this.xn_bt.Click += new System.EventHandler(this.xn_bt_Click);
            // 
            // log_bt
            // 
            this.log_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.log_bt.Location = new System.Drawing.Point(52, 340);
            this.log_bt.Name = "log_bt";
            this.log_bt.Size = new System.Drawing.Size(37, 30);
            this.log_bt.TabIndex = 42;
            this.log_bt.Text = "log";
            this.log_bt.UseVisualStyleBackColor = true;
            this.log_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // _3vx_bt
            // 
            this._3vx_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this._3vx_bt.Location = new System.Drawing.Point(52, 340);
            this._3vx_bt.Name = "_3vx_bt";
            this._3vx_bt.Size = new System.Drawing.Size(37, 30);
            this._3vx_bt.TabIndex = 41;
            this._3vx_bt.Text = "³√x";
            this._3vx_bt.UseVisualStyleBackColor = true;
            this._3vx_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // x3_bt
            // 
            this.x3_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x3_bt.Location = new System.Drawing.Point(52, 340);
            this.x3_bt.Name = "x3_bt";
            this.x3_bt.Size = new System.Drawing.Size(37, 30);
            this.x3_bt.TabIndex = 39;
            this.x3_bt.Text = "x³";
            this.x3_bt.UseVisualStyleBackColor = true;
            this.x3_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // x2_bt
            // 
            this.x2_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2_bt.Location = new System.Drawing.Point(52, 340);
            this.x2_bt.Name = "x2_bt";
            this.x2_bt.Size = new System.Drawing.Size(37, 30);
            this.x2_bt.TabIndex = 38;
            this.x2_bt.Text = "x²";
            this.x2_bt.UseVisualStyleBackColor = true;
            this.x2_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // pi_bt
            // 
            this.pi_bt.Font = new System.Drawing.Font("Tempus Sans ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pi_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pi_bt.Location = new System.Drawing.Point(52, 340);
            this.pi_bt.Name = "pi_bt";
            this.pi_bt.Size = new System.Drawing.Size(37, 30);
            this.pi_bt.TabIndex = 144;
            this.pi_bt.Text = "π";
            this.pi_bt.UseVisualStyleBackColor = true;
            this.pi_bt.Click += new System.EventHandler(this.pi_bt_Click);
            // 
            // fe_bt
            // 
            this.fe_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fe_bt.Location = new System.Drawing.Point(52, 340);
            this.fe_bt.Name = "fe_bt";
            this.fe_bt.Size = new System.Drawing.Size(37, 30);
            this.fe_bt.TabIndex = 43;
            this.fe_bt.Text = "F-E";
            this.fe_bt.UseVisualStyleBackColor = true;
            this.fe_bt.Click += new System.EventHandler(this.fe_bt_Click);
            // 
            // exp_bt
            // 
            this.exp_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exp_bt.Location = new System.Drawing.Point(52, 340);
            this.exp_bt.Name = "exp_bt";
            this.exp_bt.Size = new System.Drawing.Size(37, 30);
            this.exp_bt.TabIndex = 33;
            this.exp_bt.Text = "exper";
            this.exp_bt.UseVisualStyleBackColor = true;
            this.exp_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // modsciBT
            // 
            this.modsciBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modsciBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modsciBT.Location = new System.Drawing.Point(52, 340);
            this.modsciBT.Name = "modsciBT";
            this.modsciBT.Size = new System.Drawing.Size(37, 30);
            this.modsciBT.TabIndex = 142;
            this.modsciBT.Text = "Mod";
            this.modsciBT.UseVisualStyleBackColor = true;
            // 
            // ln_bt
            // 
            this.ln_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ln_bt.Location = new System.Drawing.Point(52, 340);
            this.ln_bt.Name = "ln_bt";
            this.ln_bt.Size = new System.Drawing.Size(37, 30);
            this.ln_bt.TabIndex = 31;
            this.ln_bt.Text = "ln";
            this.ln_bt.UseVisualStyleBackColor = true;
            this.ln_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // dms_bt
            // 
            this.dms_bt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dms_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dms_bt.Location = new System.Drawing.Point(52, 340);
            this.dms_bt.Name = "dms_bt";
            this.dms_bt.Size = new System.Drawing.Size(37, 30);
            this.dms_bt.TabIndex = 135;
            this.dms_bt.Text = "dms";
            this.dms_bt.UseVisualStyleBackColor = true;
            this.dms_bt.Click += new System.EventHandler(this.dms_bt_Click);
            // 
            // tanh_bt
            // 
            this.tanh_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tanh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tanh_bt.Location = new System.Drawing.Point(52, 340);
            this.tanh_bt.Name = "tanh_bt";
            this.tanh_bt.Size = new System.Drawing.Size(37, 30);
            this.tanh_bt.TabIndex = 37;
            this.tanh_bt.Text = "tanh";
            this.tanh_bt.UseVisualStyleBackColor = true;
            this.tanh_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // cosh_bt
            // 
            this.cosh_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cosh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cosh_bt.Location = new System.Drawing.Point(52, 340);
            this.cosh_bt.Name = "cosh_bt";
            this.cosh_bt.Size = new System.Drawing.Size(37, 30);
            this.cosh_bt.TabIndex = 36;
            this.cosh_bt.Text = "cosh";
            this.cosh_bt.UseVisualStyleBackColor = true;
            this.cosh_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // inv_bt
            // 
            this.inv_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.inv_bt.Location = new System.Drawing.Point(52, 340);
            this.inv_bt.Name = "inv_bt";
            this.inv_bt.Size = new System.Drawing.Size(37, 30);
            this.inv_bt.TabIndex = 131;
            this.inv_bt.Text = "Inv";
            this.inv_bt.UseVisualStyleBackColor = true;
            this.inv_bt.Click += new System.EventHandler(this.inv_bt_Click);
            // 
            // int_bt
            // 
            this.int_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.int_bt.Location = new System.Drawing.Point(52, 340);
            this.int_bt.Name = "int_bt";
            this.int_bt.Size = new System.Drawing.Size(37, 30);
            this.int_bt.TabIndex = 130;
            this.int_bt.Text = "Int";
            this.int_bt.UseVisualStyleBackColor = true;
            this.int_bt.Click += new System.EventHandler(this.int_bt_Click);
            // 
            // tan_bt
            // 
            this.tan_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tan_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tan_bt.Location = new System.Drawing.Point(52, 340);
            this.tan_bt.Name = "tan_bt";
            this.tan_bt.Size = new System.Drawing.Size(37, 30);
            this.tan_bt.TabIndex = 30;
            this.tan_bt.Text = "tan";
            this.tan_bt.UseVisualStyleBackColor = true;
            this.tan_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // sinh_bt
            // 
            this.sinh_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sinh_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sinh_bt.Location = new System.Drawing.Point(52, 340);
            this.sinh_bt.Name = "sinh_bt";
            this.sinh_bt.Size = new System.Drawing.Size(37, 30);
            this.sinh_bt.TabIndex = 35;
            this.sinh_bt.Text = "sinh";
            this.sinh_bt.UseVisualStyleBackColor = true;
            this.sinh_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // cos_bt
            // 
            this.cos_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cos_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cos_bt.Location = new System.Drawing.Point(52, 340);
            this.cos_bt.Name = "cos_bt";
            this.cos_bt.Size = new System.Drawing.Size(37, 30);
            this.cos_bt.TabIndex = 29;
            this.cos_bt.Text = "cos";
            this.cos_bt.UseVisualStyleBackColor = true;
            this.cos_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // sin_bt
            // 
            this.sin_bt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sin_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sin_bt.Location = new System.Drawing.Point(52, 340);
            this.sin_bt.Name = "sin_bt";
            this.sin_bt.Size = new System.Drawing.Size(37, 30);
            this.sin_bt.TabIndex = 28;
            this.sin_bt.Text = "sin";
            this.sin_bt.UseVisualStyleBackColor = true;
            this.sin_bt.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // btFactorial
            // 
            this.btFactorial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btFactorial.Location = new System.Drawing.Point(52, 340);
            this.btFactorial.Name = "btFactorial";
            this.btFactorial.Size = new System.Drawing.Size(37, 30);
            this.btFactorial.TabIndex = 32;
            this.btFactorial.Text = "n!";
            this.btFactorial.UseVisualStyleBackColor = true;
            this.btFactorial.Click += new System.EventHandler(this.functionBT_Click);
            // 
            // screenPN
            // 
            this.screenPN.BackColor = System.Drawing.Color.White;
            this.screenPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenPN.Controls.Add(this.scr_lb);
            this.screenPN.Controls.Add(this.mem_lb);
            this.screenPN.Controls.Add(this.operator_lb);
            this.screenPN.Location = new System.Drawing.Point(12, 17);
            this.screenPN.Name = "screenPN";
            this.screenPN.Size = new System.Drawing.Size(206, 42);
            this.screenPN.TabIndex = 154;
            this.screenPN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // historyPN
            // 
            this.historyPN.BackColor = System.Drawing.Color.White;
            this.historyPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyPN.Controls.Add(this.dnBT);
            this.historyPN.Controls.Add(this.upBT);
            this.historyPN.Controls.Add(this.clearHistoryBT);
            this.historyPN.Controls.Add(this.historyDGV);
            this.historyPN.Location = new System.Drawing.Point(232, 310);
            this.historyPN.Name = "historyPN";
            this.historyPN.Size = new System.Drawing.Size(206, 132);
            this.historyPN.TabIndex = 186;
            // 
            // dnBT
            // 
            this.dnBT.Enabled = false;
            this.dnBT.Location = new System.Drawing.Point(176, 4);
            this.dnBT.Name = "dnBT";
            this.dnBT.Size = new System.Drawing.Size(26, 30);
            this.dnBT.TabIndex = 1;
            this.dnBT.Text = "▼";
            this.dnBT.UseVisualStyleBackColor = true;
            this.dnBT.Click += new System.EventHandler(this.dnBT_Click);
            // 
            // upBT
            // 
            this.upBT.Enabled = false;
            this.upBT.Location = new System.Drawing.Point(146, 4);
            this.upBT.Name = "upBT";
            this.upBT.Size = new System.Drawing.Size(26, 30);
            this.upBT.TabIndex = 1;
            this.upBT.Text = "▲";
            this.upBT.UseVisualStyleBackColor = true;
            this.upBT.Click += new System.EventHandler(this.upBT_Click);
            // 
            // clearHistoryBT
            // 
            this.clearHistoryBT.Enabled = false;
            this.clearHistoryBT.Location = new System.Drawing.Point(5, 4);
            this.clearHistoryBT.Name = "clearHistoryBT";
            this.clearHistoryBT.Size = new System.Drawing.Size(50, 30);
            this.clearHistoryBT.TabIndex = 1;
            this.clearHistoryBT.Text = "Clear";
            this.clearHistoryBT.UseVisualStyleBackColor = true;
            this.clearHistoryBT.Click += new System.EventHandler(this.clearHistoryBT_Click);
            this.clearHistoryBT.EnabledChanged += new System.EventHandler(this.clearHistoryBT_EnabledChanged);
            // 
            // historyDGV
            // 
            this.historyDGV.AllowUserToAddRows = false;
            this.historyDGV.AllowUserToDeleteRows = false;
            this.historyDGV.AllowUserToResizeColumns = false;
            this.historyDGV.AllowUserToResizeRows = false;
            this.historyDGV.BackgroundColor = System.Drawing.Color.White;
            this.historyDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.historyDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.historyDGV.ColumnHeadersVisible = false;
            this.historyDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.historyDGV.Location = new System.Drawing.Point(-1, 41);
            this.historyDGV.MultiSelect = false;
            this.historyDGV.Name = "historyDGV";
            this.historyDGV.ReadOnly = true;
            this.historyDGV.RowHeadersVisible = false;
            this.historyDGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.historyDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.historyDGV.Size = new System.Drawing.Size(206, 90);
            this.historyDGV.TabIndex = 0;
            this.historyDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.historyDGV_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 204;
            // 
            // unknownPN
            // 
            this.unknownPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unknownPN.Controls.Add(this.byteRB);
            this.unknownPN.Controls.Add(this.qwordRB);
            this.unknownPN.Controls.Add(this.wordRB);
            this.unknownPN.Controls.Add(this.dwordRB);
            this.unknownPN.Location = new System.Drawing.Point(95, 376);
            this.unknownPN.Name = "unknownPN";
            this.unknownPN.Size = new System.Drawing.Size(80, 102);
            this.unknownPN.TabIndex = 213;
            // 
            // byteRB
            // 
            this.byteRB.AutoSize = true;
            this.byteRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.byteRB.Location = new System.Drawing.Point(7, 73);
            this.byteRB.Name = "byteRB";
            this.byteRB.Size = new System.Drawing.Size(47, 17);
            this.byteRB.TabIndex = 1;
            this.byteRB.Text = "Byte";
            this.byteRB.UseVisualStyleBackColor = true;
            this.byteRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // qwordRB
            // 
            this.qwordRB.AutoSize = true;
            this.qwordRB.Checked = true;
            this.qwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qwordRB.Location = new System.Drawing.Point(7, 10);
            this.qwordRB.Name = "qwordRB";
            this.qwordRB.Size = new System.Drawing.Size(57, 17);
            this.qwordRB.TabIndex = 6;
            this.qwordRB.TabStop = true;
            this.qwordRB.Text = "Qword";
            this.qwordRB.UseVisualStyleBackColor = true;
            this.qwordRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // wordRB
            // 
            this.wordRB.AutoSize = true;
            this.wordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordRB.Location = new System.Drawing.Point(7, 52);
            this.wordRB.Name = "wordRB";
            this.wordRB.Size = new System.Drawing.Size(51, 17);
            this.wordRB.TabIndex = 2;
            this.wordRB.Text = "Word";
            this.wordRB.UseVisualStyleBackColor = true;
            this.wordRB.CheckedChanged += new System.EventHandler(this.unknownGroupRB_CheckedChanged);
            // 
            // dwordRB
            // 
            this.dwordRB.AutoSize = true;
            this.dwordRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwordRB.Location = new System.Drawing.Point(7, 31);
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
            this.basePN.Location = new System.Drawing.Point(9, 376);
            this.basePN.Name = "basePN";
            this.basePN.Size = new System.Drawing.Size(80, 102);
            this.basePN.TabIndex = 214;
            this.basePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnableKeyboardAndChangeFocus);
            // 
            // binRB
            // 
            this.binRB.AutoSize = true;
            this.binRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binRB.Location = new System.Drawing.Point(7, 73);
            this.binRB.Name = "binRB";
            this.binRB.Size = new System.Drawing.Size(39, 17);
            this.binRB.TabIndex = 0;
            this.binRB.Text = "Bin";
            this.binRB.UseVisualStyleBackColor = true;
            this.binRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // octRB
            // 
            this.octRB.AutoSize = true;
            this.octRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.octRB.Location = new System.Drawing.Point(7, 52);
            this.octRB.Name = "octRB";
            this.octRB.Size = new System.Drawing.Size(42, 17);
            this.octRB.TabIndex = 0;
            this.octRB.Text = "Oct";
            this.octRB.UseVisualStyleBackColor = true;
            this.octRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // decRB
            // 
            this.decRB.AutoSize = true;
            this.decRB.Checked = true;
            this.decRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decRB.Location = new System.Drawing.Point(7, 31);
            this.decRB.Name = "decRB";
            this.decRB.Size = new System.Drawing.Size(43, 17);
            this.decRB.TabIndex = 0;
            this.decRB.TabStop = true;
            this.decRB.Text = "Dec";
            this.decRB.UseVisualStyleBackColor = true;
            this.decRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // hexRB
            // 
            this.hexRB.AutoSize = true;
            this.hexRB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexRB.Location = new System.Drawing.Point(7, 10);
            this.hexRB.Name = "hexRB";
            this.hexRB.Size = new System.Drawing.Size(44, 17);
            this.hexRB.TabIndex = 0;
            this.hexRB.Text = "Hex";
            this.hexRB.UseVisualStyleBackColor = true;
            this.hexRB.CheckedChanged += new System.EventHandler(this.baseRB_CheckedChanged);
            // 
            // binaryPN
            // 
            this.binaryPN.BackColor = System.Drawing.Color.Transparent;
            this.binaryPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.binaryPN.Location = new System.Drawing.Point(182, 405);
            this.binaryPN.Name = "binaryPN";
            this.binaryPN.Size = new System.Drawing.Size(418, 73);
            this.binaryPN.TabIndex = 212;
            // 
            // btnF
            // 
            this.btnF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnF.Location = new System.Drawing.Point(95, 340);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(37, 30);
            this.btnF.TabIndex = 208;
            this.btnF.Text = "F";
            this.btnF.UseVisualStyleBackColor = true;
            this.btnF.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // btnB
            // 
            this.btnB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnB.Location = new System.Drawing.Point(95, 340);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(37, 30);
            this.btnB.TabIndex = 206;
            this.btnB.Text = "B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // btnD
            // 
            this.btnD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnD.Location = new System.Drawing.Point(95, 340);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(37, 30);
            this.btnD.TabIndex = 207;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // XorBT
            // 
            this.XorBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XorBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.XorBT.Location = new System.Drawing.Point(95, 340);
            this.XorBT.Name = "XorBT";
            this.XorBT.Size = new System.Drawing.Size(37, 30);
            this.XorBT.TabIndex = 205;
            this.XorBT.Text = "Xor";
            this.XorBT.UseVisualStyleBackColor = true;
            this.XorBT.Click += new System.EventHandler(this.XorBT_Click);
            // 
            // NotBT
            // 
            this.NotBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotBT.Location = new System.Drawing.Point(95, 340);
            this.NotBT.Name = "NotBT";
            this.NotBT.Size = new System.Drawing.Size(37, 30);
            this.NotBT.TabIndex = 203;
            this.NotBT.Text = "Not";
            this.NotBT.UseVisualStyleBackColor = true;
            this.NotBT.Click += new System.EventHandler(this.notBT_Click);
            // 
            // AndBT
            // 
            this.AndBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AndBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AndBT.Location = new System.Drawing.Point(95, 340);
            this.AndBT.Name = "AndBT";
            this.AndBT.Size = new System.Drawing.Size(37, 30);
            this.AndBT.TabIndex = 204;
            this.AndBT.Text = "And";
            this.AndBT.UseVisualStyleBackColor = true;
            this.AndBT.Click += new System.EventHandler(this.AndBT_Click);
            // 
            // btnE
            // 
            this.btnE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnE.Location = new System.Drawing.Point(95, 340);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(37, 30);
            this.btnE.TabIndex = 209;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // RshBT
            // 
            this.RshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RshBT.Location = new System.Drawing.Point(95, 340);
            this.RshBT.Name = "RshBT";
            this.RshBT.Size = new System.Drawing.Size(37, 30);
            this.RshBT.TabIndex = 210;
            this.RshBT.Text = "Rsh";
            this.RshBT.UseVisualStyleBackColor = true;
            this.RshBT.Click += new System.EventHandler(this.RshBT_Click);
            // 
            // RoRBT
            // 
            this.RoRBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoRBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoRBT.Location = new System.Drawing.Point(95, 340);
            this.RoRBT.Name = "RoRBT";
            this.RoRBT.Size = new System.Drawing.Size(37, 30);
            this.RoRBT.TabIndex = 211;
            this.RoRBT.Text = "RoR";
            this.RoRBT.UseVisualStyleBackColor = true;
            this.RoRBT.Click += new System.EventHandler(this.rotateBT_Click);
            // 
            // LshBT
            // 
            this.LshBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LshBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LshBT.Location = new System.Drawing.Point(95, 340);
            this.LshBT.Name = "LshBT";
            this.LshBT.Size = new System.Drawing.Size(37, 30);
            this.LshBT.TabIndex = 200;
            this.LshBT.Text = "Lsh";
            this.LshBT.UseVisualStyleBackColor = true;
            this.LshBT.Click += new System.EventHandler(this.LshBT_Click);
            // 
            // orBT
            // 
            this.orBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.orBT.Location = new System.Drawing.Point(95, 340);
            this.orBT.Name = "orBT";
            this.orBT.Size = new System.Drawing.Size(37, 30);
            this.orBT.TabIndex = 199;
            this.orBT.Text = "Or";
            this.orBT.UseVisualStyleBackColor = true;
            this.orBT.Click += new System.EventHandler(this.orBT_Click);
            // 
            // RoLBT
            // 
            this.RoLBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoLBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoLBT.Location = new System.Drawing.Point(95, 340);
            this.RoLBT.Name = "RoLBT";
            this.RoLBT.Size = new System.Drawing.Size(37, 30);
            this.RoLBT.TabIndex = 198;
            this.RoLBT.Text = "RoL";
            this.RoLBT.UseVisualStyleBackColor = true;
            this.RoLBT.Click += new System.EventHandler(this.rotateBT_Click);
            // 
            // btnA
            // 
            this.btnA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnA.Location = new System.Drawing.Point(95, 340);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(37, 30);
            this.btnA.TabIndex = 201;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // btnC
            // 
            this.btnC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnC.Location = new System.Drawing.Point(95, 340);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(37, 30);
            this.btnC.TabIndex = 202;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.buttonAF_Click);
            // 
            // openproBT
            // 
            this.openproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openproBT.Location = new System.Drawing.Point(95, 340);
            this.openproBT.Name = "openproBT";
            this.openproBT.Size = new System.Drawing.Size(37, 30);
            this.openproBT.TabIndex = 199;
            this.openproBT.Text = "(";
            this.openproBT.UseVisualStyleBackColor = true;
            // 
            // modproBT
            // 
            this.modproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modproBT.Location = new System.Drawing.Point(95, 340);
            this.modproBT.Name = "modproBT";
            this.modproBT.Size = new System.Drawing.Size(37, 30);
            this.modproBT.TabIndex = 211;
            this.modproBT.Text = "Mod";
            this.modproBT.UseVisualStyleBackColor = true;
            // 
            // closeproBT
            // 
            this.closeproBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeproBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.closeproBT.Location = new System.Drawing.Point(95, 340);
            this.closeproBT.Name = "closeproBT";
            this.closeproBT.Size = new System.Drawing.Size(37, 30);
            this.closeproBT.TabIndex = 205;
            this.closeproBT.Text = ")";
            this.closeproBT.UseVisualStyleBackColor = true;
            // 
            // Expsta_bt
            // 
            this.Expsta_bt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Expsta_bt.Location = new System.Drawing.Point(137, 340);
            this.Expsta_bt.Name = "Expsta_bt";
            this.Expsta_bt.Size = new System.Drawing.Size(36, 30);
            this.Expsta_bt.TabIndex = 247;
            this.Expsta_bt.TabStop = false;
            this.Expsta_bt.Text = "Exp";
            this.Expsta_bt.UseVisualStyleBackColor = true;
            // 
            // sigmax2BT
            // 
            this.sigmax2BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmax2BT.Image = ((System.Drawing.Image)(resources.GetObject("sigmax2BT.Image")));
            this.sigmax2BT.Location = new System.Drawing.Point(137, 340);
            this.sigmax2BT.Name = "sigmax2BT";
            this.sigmax2BT.Size = new System.Drawing.Size(36, 30);
            this.sigmax2BT.TabIndex = 245;
            this.sigmax2BT.TabStop = false;
            this.sigmax2BT.UseVisualStyleBackColor = true;
            this.sigmax2BT.Click += new System.EventHandler(this.sigmax2BT_Click);
            // 
            // sigman_1BT
            // 
            this.sigman_1BT.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigman_1BT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigman_1BT.Image = ((System.Drawing.Image)(resources.GetObject("sigman_1BT.Image")));
            this.sigman_1BT.Location = new System.Drawing.Point(137, 340);
            this.sigman_1BT.Name = "sigman_1BT";
            this.sigman_1BT.Size = new System.Drawing.Size(36, 30);
            this.sigman_1BT.TabIndex = 244;
            this.sigman_1BT.TabStop = false;
            this.sigman_1BT.UseVisualStyleBackColor = true;
            this.sigman_1BT.Click += new System.EventHandler(this.sigman_1BT_Click);
            // 
            // AddstaBT
            // 
            this.AddstaBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddstaBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddstaBT.Location = new System.Drawing.Point(137, 340);
            this.AddstaBT.Name = "AddstaBT";
            this.AddstaBT.Size = new System.Drawing.Size(36, 30);
            this.AddstaBT.TabIndex = 243;
            this.AddstaBT.TabStop = false;
            this.AddstaBT.Text = "Add";
            this.AddstaBT.UseVisualStyleBackColor = true;
            this.AddstaBT.Click += new System.EventHandler(this.AddstaBT_Click);
            // 
            // xcross
            // 
            this.xcross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xcross.Image = ((System.Drawing.Image)(resources.GetObject("xcross.Image")));
            this.xcross.Location = new System.Drawing.Point(137, 340);
            this.xcross.Name = "xcross";
            this.xcross.Size = new System.Drawing.Size(36, 30);
            this.xcross.TabIndex = 242;
            this.xcross.TabStop = false;
            this.xcross.UseVisualStyleBackColor = true;
            this.xcross.Click += new System.EventHandler(this.xcross_Click);
            // 
            // sigmaxBT
            // 
            this.sigmaxBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmaxBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmaxBT.Image")));
            this.sigmaxBT.Location = new System.Drawing.Point(137, 340);
            this.sigmaxBT.Name = "sigmaxBT";
            this.sigmaxBT.Size = new System.Drawing.Size(36, 30);
            this.sigmaxBT.TabIndex = 241;
            this.sigmaxBT.TabStop = false;
            this.sigmaxBT.UseVisualStyleBackColor = true;
            this.sigmaxBT.Click += new System.EventHandler(this.sigmaxBT_Click);
            // 
            // sigmanBT
            // 
            this.sigmanBT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sigmanBT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sigmanBT.Image = ((System.Drawing.Image)(resources.GetObject("sigmanBT.Image")));
            this.sigmanBT.Location = new System.Drawing.Point(137, 340);
            this.sigmanBT.Name = "sigmanBT";
            this.sigmanBT.Size = new System.Drawing.Size(36, 30);
            this.sigmanBT.TabIndex = 240;
            this.sigmanBT.TabStop = false;
            this.sigmanBT.UseVisualStyleBackColor = true;
            this.sigmanBT.Click += new System.EventHandler(this.sigmanBT_Click);
            // 
            // CAD
            // 
            this.CAD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CAD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CAD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CAD.Location = new System.Drawing.Point(137, 340);
            this.CAD.Name = "CAD";
            this.CAD.Size = new System.Drawing.Size(36, 30);
            this.CAD.TabIndex = 248;
            this.CAD.TabStop = false;
            this.CAD.Text = "CAD";
            this.CAD.UseVisualStyleBackColor = true;
            this.CAD.Click += new System.EventHandler(this.CAD_Click);
            // 
            // x2cross
            // 
            this.x2cross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.x2cross.Image = ((System.Drawing.Image)(resources.GetObject("x2cross.Image")));
            this.x2cross.Location = new System.Drawing.Point(137, 340);
            this.x2cross.Name = "x2cross";
            this.x2cross.Size = new System.Drawing.Size(36, 30);
            this.x2cross.TabIndex = 246;
            this.x2cross.TabStop = false;
            this.x2cross.UseVisualStyleBackColor = true;
            this.x2cross.Click += new System.EventHandler(this.x2cross_Click);
            // 
            // statisticsPN
            // 
            this.statisticsPN.BackColor = System.Drawing.Color.White;
            this.statisticsPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statisticsPN.Controls.Add(this.countlb);
            this.statisticsPN.Controls.Add(this.statisticsDGV);
            this.statisticsPN.Location = new System.Drawing.Point(444, 310);
            this.statisticsPN.Name = "statisticsPN";
            this.statisticsPN.Size = new System.Drawing.Size(206, 132);
            this.statisticsPN.TabIndex = 249;
            // 
            // countlb
            // 
            this.countlb.AutoSize = true;
            this.countlb.Location = new System.Drawing.Point(3, 5);
            this.countlb.Name = "countlb";
            this.countlb.Size = new System.Drawing.Size(65, 16);
            this.countlb.TabIndex = 1;
            this.countlb.Text = "Count = 0";
            // 
            // statisticsDGV
            // 
            this.statisticsDGV.AllowUserToAddRows = false;
            this.statisticsDGV.AllowUserToDeleteRows = false;
            this.statisticsDGV.AllowUserToResizeColumns = false;
            this.statisticsDGV.AllowUserToResizeRows = false;
            this.statisticsDGV.BackgroundColor = System.Drawing.Color.White;
            this.statisticsDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.statisticsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statisticsDGV.ColumnHeadersVisible = false;
            this.statisticsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.statisticsDGV.Location = new System.Drawing.Point(-1, 24);
            this.statisticsDGV.Name = "statisticsDGV";
            this.statisticsDGV.ReadOnly = true;
            this.statisticsDGV.RowHeadersVisible = false;
            this.statisticsDGV.RowHeadersWidth = 20;
            this.statisticsDGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statisticsDGV.Size = new System.Drawing.Size(206, 107);
            this.statisticsDGV.TabIndex = 0;
            this.statisticsDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.statisticsDGV_CellBeginEdit);
            this.statisticsDGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.statisticsDGV_RowsAdded);
            this.statisticsDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.statisticsDGV_CellEndEdit);
            this.statisticsDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.statisticsDGV_CellClick);
            this.statisticsDGV.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.statisticsDGV_RowsRemoved);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 204;
            // 
            // clearsta
            // 
            this.clearsta.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearsta.Location = new System.Drawing.Point(137, 340);
            this.clearsta.Name = "clearsta";
            this.clearsta.Size = new System.Drawing.Size(36, 30);
            this.clearsta.TabIndex = 20;
            this.clearsta.TabStop = false;
            this.clearsta.Text = "C";
            this.clearsta.UseVisualStyleBackColor = true;
            this.clearsta.Click += new System.EventHandler(this.clear_Click);
            // 
            // calc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ce;
            this.ClientSize = new System.Drawing.Size(231, 288);
            this.Controls.Add(this.statisticsPN);
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
            this.Controls.Add(this.binaryPN);
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
            this.Controls.Add(this.orBT);
            this.Controls.Add(this.RoLBT);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.historyPN);
            this.Controls.Add(this.screenPN);
            this.Controls.Add(this.angleGB);
            this.Controls.Add(this.nonameTB);
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
            this.Controls.Add(this.fe_bt);
            this.Controls.Add(this.exp_bt);
            this.Controls.Add(this.modsciBT);
            this.Controls.Add(this.ln_bt);
            this.Controls.Add(this.dms_bt);
            this.Controls.Add(this.tanh_bt);
            this.Controls.Add(this.cosh_bt);
            this.Controls.Add(this.inv_bt);
            this.Controls.Add(this.int_bt);
            this.Controls.Add(this.tan_bt);
            this.Controls.Add(this.sinh_bt);
            this.Controls.Add(this.cos_bt);
            this.Controls.Add(this.sin_bt);
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
            this.Controls.Add(this.unitconvGB);
            this.Controls.Add(this.datecalcGB);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "calc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.calc_Load);
            this.Enter += new System.EventHandler(this.EnableKeyboardAndChangeFocus);
            ((System.ComponentModel.ISupportInitialize)(this.periodsDateUD)).EndInit();
            this.unitconvGB.ResumeLayout(false);
            this.unitconvGB.PerformLayout();
            this.datecalcGB.ResumeLayout(false);
            this.datecalcGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodsYearUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodsMonthUD)).EndInit();
            this.angleGB.ResumeLayout(false);
            this.angleGB.PerformLayout();
            this.screenPN.ResumeLayout(false);
            this.screenPN.PerformLayout();
            this.historyPN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.historyDGV)).EndInit();
            this.unknownPN.ResumeLayout(false);
            this.unknownPN.PerformLayout();
            this.basePN.ResumeLayout(false);
            this.basePN.PerformLayout();
            this.statisticsPN.ResumeLayout(false);
            this.statisticsPN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statisticsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        #region form generation code
        /// <summary>
        /// khởi tạo menu chính cho chương trình
        /// </summary>
        private void InitializeAllMenus()
        {
            this.menuStrip1 = new MainMenu();
            this.viewTSMI = new MenuItem("&View");
            this.standardTSMI = new MenuItem("S&tandard");
            this.scientificTSMI = new MenuItem("&Scientific");
            this.programmerTSMI = new MenuItem("&Programmer");
            this.statisticsTSMI = new MenuItem("St&atistics");
            this.toolStripSeparator1 = new MenuItem("-");
            this.historyTSMI = new MenuItem("Histor&y");
            this.digitGroupingTSMI = new MenuItem("D&igit grouping");
            this.toolStripSeparator2 = new MenuItem("-");
            this.basicTSMI = new MenuItem("&Basic");
            this.unitConversionTSMI = new MenuItem("&Unit conversion");
            this.dateCalculationTSMI = new MenuItem("&Date calculation");
            this.editTSMI = new MenuItem("&Edit");
            this.copyTSMI = new MenuItem("&Copy");
            this.pasteTSMI = new MenuItem("&Paste");
            this.sepTSMI3 = new MenuItem("-");
            this.historyOptionTSMI = new MenuItem("His&tory");
            this.copyHistoryTSMI = new MenuItem("Copy history");
            this.clearHistoryTSMI = new MenuItem("Clear");
            this.datasetTSMI = new MenuItem("Datase&t");
            this.copyDatasetTSMI = new MenuItem("Copy Dataset");
            this.clearDatasetTSMI = new MenuItem("Clear");
            this.helpTSMI = new MenuItem("&Help");
            this.helpTopicsTSMI = new MenuItem("&Help Topics");
            this.toolStripSeparator4 = new MenuItem("-");
            this.aboutTSMI = new MenuItem("&About");

            this.contextMenu1 = new ContextMenu();
            this.copyCTMN = new MenuItem("&Copy");
            this.pasteCTMN = new MenuItem("&Paste");
            this.sepCTMN = new MenuItem("-");
            this.showHistoryCTMN = new MenuItem("&Show history");
            this.hideHistoryCTMN = new MenuItem("&Hide history");
            this.clearHistoryCTMN = new MenuItem("C&lear history");
            this.clearDataSetCTMN = new MenuItem("C&lear Dataset");
            // 
            // menuStrip1
            // 
            this.menuStrip1.MenuItems.AddRange(new MenuItem[] {
            this.viewTSMI,
            this.editTSMI,
            this.helpTSMI});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // viewTSMI
            // 
            this.viewTSMI.MenuItems.AddRange(new MenuItem[] {
            this.standardTSMI,
            this.scientificTSMI,
            this.programmerTSMI,
            this.statisticsTSMI,
            this.toolStripSeparator1,
            this.historyTSMI,
            this.digitGroupingTSMI,
            this.toolStripSeparator2,
            this.basicTSMI,
            this.unitConversionTSMI,
            this.dateCalculationTSMI});
            this.viewTSMI.Name = "viewTSMI";
            // 
            // standardTSMI
            // 
            this.standardTSMI.Checked = true;
            this.standardTSMI.Name = "standardTSMI";
            this.standardTSMI.Shortcut = Shortcut.Alt1;
            this.standardTSMI.RadioCheck = true;
            this.standardTSMI.Click += new EventHandler(viewMode_Click);
            // 
            // scientificTSMI
            // 
            this.scientificTSMI.Name = "scientificTSMI";
            this.scientificTSMI.Shortcut = Shortcut.Alt2;
            this.scientificTSMI.RadioCheck = true;
            this.scientificTSMI.Click += new EventHandler(scientificTSMI_Click);
            // 
            // programmerTSMI
            // 
            this.programmerTSMI.Name = "programmerTSMI";
            this.programmerTSMI.Shortcut = Shortcut.Alt3;
            this.programmerTSMI.RadioCheck = true;
            this.programmerTSMI.Click += new EventHandler(programmerTSMI_Click);
            // 
            // statisticsTSMI
            // 
            this.statisticsTSMI.Name = "statisticsTSMI";
            this.statisticsTSMI.Shortcut = Shortcut.Alt4;
            this.statisticsTSMI.RadioCheck = true;
            this.statisticsTSMI.Click += new EventHandler(statisticsTSMI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // historyTSMI
            // 
            this.historyTSMI.Name = "historyTSMI";
            this.historyTSMI.Shortcut = Shortcut.CtrlH;
            this.historyTSMI.Click += new EventHandler(this.historyTSMI_Click);
            // 
            // digitGroupingTSMI
            // 
            this.digitGroupingTSMI.Name = "digitGroupingTSMI";
            this.digitGroupingTSMI.ShowShortcut = false;
            this.digitGroupingTSMI.Shortcut = Shortcut.CtrlG;
            this.digitGroupingTSMI.Click += new EventHandler(this.digitGroupingTSMI_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // basicTSMI
            // 
            this.basicTSMI.Checked = true;
            this.basicTSMI.Name = "basicTSMI";
            this.basicTSMI.Shortcut = Shortcut.CtrlF4;
            this.basicTSMI.RadioCheck = true;
            this.basicTSMI.Click += new EventHandler(this.basicTSMI_Click);
            // 
            // unitConversionTSMI
            // 
            this.unitConversionTSMI.Name = "unitConversionTSMI";
            this.unitConversionTSMI.Shortcut = Shortcut.CtrlU;
            this.unitConversionTSMI.RadioCheck = true;
            this.unitConversionTSMI.Click += new EventHandler(this.unitConversionTSMI_Click);
            // 
            // dateCalculationTSMI
            // 
            this.dateCalculationTSMI.Name = "dateCalculationTSMI";
            this.dateCalculationTSMI.Shortcut = Shortcut.CtrlE;
            this.dateCalculationTSMI.RadioCheck = true;
            this.dateCalculationTSMI.Click += new EventHandler(this.dateCalculationTSMI_Click);
            // 
            // editTSMI
            // 
            this.editTSMI.MenuItems.AddRange(new MenuItem[] {
            this.copyTSMI,
            this.pasteTSMI,
            this.sepTSMI3,
            this.historyOptionTSMI,
            this.datasetTSMI});
            this.editTSMI.Name = "editTSMI";
            // 
            // copyTSMI
            // 
            this.copyTSMI.Name = "copyTSMI";
            this.copyTSMI.Shortcut = Shortcut.CtrlC;
            this.copyTSMI.Click += new EventHandler(this.copyTSMI_Click);
            // 
            // pasteTSMI
            // 
            this.pasteTSMI.Name = "pasteTSMI";
            this.pasteTSMI.Shortcut = Shortcut.CtrlV;
            this.pasteTSMI.Click += new EventHandler(this.pasteTSMI_Click);
            // 
            // sepTSMI3
            // 
            this.sepTSMI3.Name = "sepTSMI3";
            // 
            // historyOptionTSMI
            // 
            this.historyOptionTSMI.MenuItems.AddRange(new MenuItem[] {
            this.copyHistoryTSMI,
            this.clearHistoryTSMI});
            this.historyOptionTSMI.Name = "historyOptionTSMI";
            // 
            // copyHistoryTSMI
            // 
            this.copyHistoryTSMI.Enabled = false;
            this.copyHistoryTSMI.Name = "copyHistoryTSMI";
            this.copyHistoryTSMI.Click += new EventHandler(this.copyHistoryTSMI_Click);
            // 
            // clearHistoryTSMI
            // 
            this.clearHistoryTSMI.Enabled = false;
            this.clearHistoryTSMI.Name = "clearHistoryTSMI";
            this.clearHistoryTSMI.Shortcut = Shortcut.CtrlShiftD;
            this.clearHistoryTSMI.Click += new EventHandler(this.clearHistoryBT_Click);
            // 
            // historyOptionTSMI
            // 
            this.datasetTSMI.MenuItems.AddRange(new MenuItem[] {
            this.copyDatasetTSMI,
            this.clearDatasetTSMI});
            this.datasetTSMI.Name = "datasetTSMI";
            this.datasetTSMI.Visible = false;
            // 
            // copyDatasetTSMI
            // 
            this.copyDatasetTSMI.Enabled = false;
            this.copyDatasetTSMI.Name = "copyDatasetTSMI";
            this.copyDatasetTSMI.Click += new EventHandler(copyDatasetTSMI_Click);
            // 
            // clearHistoryTSMI
            // 
            this.clearDatasetTSMI.Enabled = false;
            this.clearDatasetTSMI.Name = "clearDatasetTSMI";
            this.clearDatasetTSMI.Shortcut = Shortcut.CtrlShiftD;
            this.clearDatasetTSMI.Click += new EventHandler(CAD_Click);
            // 
            // helpTSMI
            // 
            this.helpTSMI.MenuItems.AddRange(new MenuItem[] {
            this.helpTopicsTSMI,
            this.toolStripSeparator4,
            this.aboutTSMI});
            this.helpTSMI.Name = "helpTSMI";
            // 
            // helpTopicsTSMI
            // 
            this.helpTopicsTSMI.Name = "helpTopicsTSMI";
            this.helpTopicsTSMI.Shortcut = Shortcut.F1;
            this.helpTopicsTSMI.Click += new EventHandler(this.helpTopicsTSMI_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // aboutTSMI
            // 
            this.aboutTSMI.Name = "aboutTSMI";
            this.aboutTSMI.Click += new EventHandler(this.aboutTSMI_Click);

            this.contextMenu1.MenuItems.AddRange(new MenuItem[]{
            copyCTMN,
            pasteCTMN,
            sepCTMN,
            showHistoryCTMN,
            hideHistoryCTMN,
            clearHistoryCTMN,
            clearDataSetCTMN});
            //
            // copyCTMN
            //
            this.copyCTMN.Name = "copyCTMN";
            this.copyCTMN.Click += new EventHandler(copyTSMI_Click);
            //
            // pasteCTMN
            //
            this.pasteCTMN.Name = "pasteCTMN";
            this.pasteCTMN.Enabled = misc.isNumber(Clipboard.GetText().Trim());
            this.pasteCTMN.Click += new EventHandler(pasteTSMI_Click);
            //
            // sepCTMN
            //
            this.sepCTMN.Name = "sepCTMN";
            //
            // showHistoryCTMN
            //
            this.showHistoryCTMN.Name = "showHistoryCTMN";
            //this.showHistoryCTMN.Visible = true;
            this.showHistoryCTMN.Click += new EventHandler(historyTSMI_Click);
            //
            // hideHistoryCTMN
            //
            this.hideHistoryCTMN.Name = "hideHistoryCTMN";
            this.hideHistoryCTMN.Visible = false;
            this.hideHistoryCTMN.Click += new EventHandler(historyTSMI_Click);
            //
            // clearDataSetCTMN
            //
            this.clearDataSetCTMN.Name = "clearDataSetCTMN";
            this.clearDataSetCTMN.Visible = false;
            this.clearDataSetCTMN.Click += new EventHandler(CAD_Click);
            //
            // clearHistoryCTMN
            //
            this.clearHistoryCTMN.Name = "clearHistoryCTMN";
            this.clearHistoryCTMN.Visible = false;
            this.clearHistoryCTMN.Click += new EventHandler(clearHistoryBT_Click);
            // 
            // calc
            // 
            this.Menu = this.menuStrip1;
            this.scr_lb.ContextMenu = contextMenu1;
            this.screenPN.ContextMenu = contextMenu1;
        }
        /// <summary>
        /// Hàm do người dùng viết để sắp xếp và thêm các nút lên form scientific
        /// </summary>
        private void scientificLoad(bool isreturn0)
        {
            this.close_bracket.Location = new Point(183, 101);
            this.btFactorial.Location = new Point(183, 137);
            this._10x_bt.Location = new Point(183, 246);
            this._3vx_bt.Location = new Point(183, 209);
            this.nvx_bt.Location = new Point(183, 173);

            this.open_bracket.Location = new Point(141, 101);
            this.xn_bt.Location = new Point(141, 173);
            this.log_bt.Location = new Point(141, 246);
            this.x3_bt.Location = new Point(141, 209);
            this.x2_bt.Location = new Point(141, 137);

            this.modsciBT.Location = new Point(98, 246);
            this.ln_bt.Location = new Point(98, 101);
            this.tan_bt.Location = new Point(98, 209);
            this.cos_bt.Location = new Point(98, 173);
            this.sin_bt.Location = new Point(98, 137);

            this.sqrt_bt.Location = new Point(394, 101);
            this.percent_bt.Location = new Point(394, 137);
            this.invert_bt.Location = new Point(394, 173);
            this.equal.Location = new Point(394, 209);
            this.mem_minus_bt.Location = new Point(394, 65);

            this.divbt.Location = new Point(353, 137);
            this.mulbt.Location = new Point(353, 173);
            this.minusbt.Location = new Point(353, 209);
            this.addbt.Location = new Point(353, 246);
            this.doidau.Location = new Point(353, 101);
            this.madd.Location = new Point(353, 65);

            this.btdot.Location = new Point(312, 246);
            this.num9.Location = new Point(312, 137);
            this.mstore.Location = new Point(312, 65);
            this.clearbt.Location = new Point(312, 101);
            this.num6.Location = new Point(312, 173);
            this.num3.Location = new Point(312, 209);

            this.memrecall.Location = new Point(271, 65);
            this.ce.Location = new Point(271, 101);
            this.num8.Location = new Point(271, 137);
            this.num5.Location = new Point(271, 173);
            this.num2.Location = new Point(271, 209);

            this.memclear.Location = new Point(230, 65);
            this.backspace.Location = new Point(230, 101);
            this.num7.Location = new Point(230, 137);
            this.num4.Location = new Point(230, 173);
            this.num1.Location = new Point(230, 209);
            this.num0.Location = new Point(230, 246);

            this.exp_bt.Location = new Point(55, 246);
            this.sinh_bt.Location = new Point(55, 137);
            this.cosh_bt.Location = new Point(55, 173);
            this.tanh_bt.Location = new Point(55, 209);
            this.inv_bt.Location = new Point(55, 101);

            this.int_bt.Location = new Point(12, 137);
            this.dms_bt.Location = new Point(12, 173);
            this.pi_bt.Location = new Point(12, 209);
            this.fe_bt.Location = new Point(12, 246);
            this.angleGB.Location = new Point(12, 58);
            this.nonameTB.Location = new Point(12, 101);

            this.scr_lb.Size = new Size(393, 30);
            if (isreturn0) this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);
            // 
            // screenPN
            // 
            this.screenPN.Location = new Point(12, 17);
            this.screenPN.Size = new Size(418, 42);

            hideSciComponent(true);
            // 
            // Calculator
            // 
            //this.Size = new Size(447, 340);
        }
        /// <summary>
        /// Sắp xếp lại các nút của form standard trở lại vị trí ban đầu
        /// </summary>
        private void initializedForm(bool isreturn0)
        {
            this.num1.Location = new Point(12, 209);
            this.num2.Location = new Point(54, 209);
            this.num3.Location = new Point(96, 209);
            this.num4.Location = new Point(12, 173);
            this.num5.Location = new Point(54, 173);
            this.num6.Location = new Point(96, 173);
            this.num7.Location = new Point(12, 137);
            this.num8.Location = new Point(54, 137);
            this.num9.Location = new Point(96, 137);
            this.num0.Location = new Point(12, 246);
            this.btdot.Location = new Point(96, 246);
            this.addbt.Location = new Point(139, 246);
            this.minusbt.Location = new Point(139, 209);
            this.mulbt.Location = new Point(139, 173);
            this.divbt.Location = new Point(139, 137);
            this.equal.Location = new Point(182, 209);
            this.invert_bt.Location = new Point(182, 173);
            this.percent_bt.Location = new Point(182, 137);
            this.backspace.Location = new Point(12, 101);
            this.ce.Location = new Point(54, 101);
            this.doidau.Location = new Point(139, 101);
            this.memclear.Location = new Point(12, 65);
            this.mstore.Location = new Point(54, 65);
            this.memrecall.Location = new Point(96, 65);
            this.sqrt_bt.Location = new Point(182, 101);
            this.clearbt.Location = new Point(96, 101);
            this.madd.Location = new Point(139, 65);
            this.mem_minus_bt.Location = new Point(182, 65);
            this.mem_lb.Visible = (mem_num != 0);

            this.screenPN.Location = new Point(12, 17);
            this.screenPN.Size = new Size(206, 42);
            //
            // scr_lb
            //
            //this.scr_lb.BackColor = Color.White;
            //this.scr_lb.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.scr_lb.Location = new System.Drawing.Point(23, 9);
            this.scr_lb.Size = new System.Drawing.Size(178, 22);
            if (isreturn0) this.scr_lb.Text = "0";
            //
            // form
            //
            //this.Size = new Size(237, 340);
        }
        /// <summary>
        /// sự kiện focused cho các nút trên form ban đầu.
        /// các sự kiện này không thể gọi trên designer
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
            this.dtP1.GotFocus += new EventHandler(DisableKeyboard);
            this.dtP1.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.dtP2.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.periodsDateUD.MouseDown += new MouseEventHandler(DisableKeyboard);
            this.typeCB.GotFocus += new EventHandler(DisableKeyboard);
            //this.typeCB.MouseDown += new MouseEventHandler(DisableKeyboard);

            this.open_bracket.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.close_bracket.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.nvx_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.btFactorial.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this._3vx_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this._10x_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.x2_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.xn_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.x3_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.log_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.sinh_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.cosh_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.tanh_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.dms_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.pi_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.inv_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.fe_bt.GotFocus += new EventHandler(EnableKeyboardAndChangeFocus);
            this.sin_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.cos_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.tan_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.ln_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.exp_bt.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.gra_rb.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.rad_rb.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);
            this.deg_rb.GotFocus += new System.EventHandler(EnableKeyboardAndChangeFocus);

            this.result1.Enter += new EventHandler(DisableKeyboard);
            this.result2.Enter += new EventHandler(DisableKeyboard);
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            hideStdComponent(standardTSMI.Checked);
            hideSciComponent(scientificTSMI.Checked);
            hideProComponent(programmerTSMI.Checked);
            hideStaComponent(statisticsTSMI.Checked);
            historyPN.Visible = false;
        }
        /// <summary>
        /// khởi tạo mảng các combobox
        /// </summary>
        private void InitComboBox()
        {
            fcb = new ComboBox[11];
            tcb = new ComboBox[11];
            for (int i = 0; i < 11; i++)
            {
                #region fcb properties
                fcb[i] = new ComboBox();
                this.unitconvGB.Controls.Add(fcb[i]);
                fcb[i].DropDownStyle = ComboBoxStyle.DropDownList;
                fcb[i].Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                fcb[i].TabIndex = 11;
                fcb[i].MaxDropDownItems = 14;
                fcb[i].Location = new Point(16, 129);
                fcb[i].Name = "fromCB" + i;
                fcb[i].Size = new Size(306, 22);
                fcb[i].SelectedIndexChanged += new EventHandler(this.fromCB_SelectedIndexChanged);
                fcb[i].GotFocus += new EventHandler(this.DisableKeyboard); 
                #endregion

                #region tcb properties
                tcb[i] = new ComboBox();
                this.unitconvGB.Controls.Add(tcb[i]);
                tcb[i].DropDownStyle = ComboBoxStyle.DropDownList;
                tcb[i].Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                //tcb[i].BackColor = SystemColors.Control;
                tcb[i].TabIndex = 13;
                tcb[i].MaxDropDownItems = 14;
                tcb[i].Location = new Point(16, 206);
                tcb[i].Name = "toCB" + i;
                tcb[i].Size = new Size(306, 22);
                tcb[i].SelectedIndexChanged += new EventHandler(this.toCB_SelectedIndexChanged);
                tcb[i].GotFocus += new EventHandler(this.DisableKeyboard); 
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

            for (int i = 0; i < obj.Length; i++)
            {
                fcb[i].Items.AddRange(obj[i]);
                tcb[i].Items.AddRange(obj[i]);
            }
        }
        /// <summary>
        /// standard with history
        /// </summary>
        private void stdWithHistory()
        {
            this.screenPN.Location = new Point(12, 148);
            this.screenPN.Size = new Size(206, 42);

            this.sqrt_bt.Location = new Point(182, 232);
            this.clearbt.Location = new Point(96, 232);
            this.doidau.Location = new Point(139, 232);
            this.ce.Location = new Point(54, 232);
            this.backspace.Location = new Point(12, 232);

            this.invert_bt.Location = new Point(182, 304);
            this.mulbt.Location = new Point(139, 304);
            this.num6.Location = new Point(96, 304);
            this.num5.Location = new Point(54, 304);
            this.num4.Location = new Point(12, 304);

            this.equal.Location = new Point(182, 340);
            this.minusbt.Location = new Point(139, 340);
            this.num3.Location = new Point(96, 340);
            this.num2.Location = new Point(54, 340);
            this.num1.Location = new Point(12, 340);

            this.addbt.Location = new Point(139, 377);
            this.btdot.Location = new Point(96, 377);
            this.num0.Location = new Point(12, 377);

            this.divbt.Location = new Point(139, 268);
            this.num9.Location = new Point(96, 268);
            this.num8.Location = new Point(54, 268);
            this.num7.Location = new Point(12, 268);
            this.percent_bt.Location = new Point(182, 268);

            this.mstore.Location = new Point(54, 196);
            this.mem_minus_bt.Location = new Point(182, 196);
            this.memrecall.Location = new Point(96, 196);
            this.madd.Location = new Point(139, 196);
            this.memclear.Location = new Point(12, 196);

            this.historyPN.Location = new Point(12, 17);
            this.historyPN.Size = new Size(206, 132);
            this.historyPN.Visible = true;
            this.historyDGV.Location = new Point(-1, 41);

            this.scr_lb.Location = new Point(23, 9);
            this.scr_lb.Size = new Size(178, 22);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 204;
            //while (historyDGV.Rows.Count > 0) historyDGV.Rows.RemoveAt(0);

            this.upBT.Location = new Point(screenPN.Size.Width - 60, upBT.Location.Y);
            this.dnBT.Location = new Point(screenPN.Size.Width - 31, dnBT.Location.Y);
            //this.Size = new Size(237, 470);
        }
        /// <summary>
        /// scientific with history
        /// </summary>
        private void sciWithHistory()
        {
            //this.label1.Location = new Point(17, 122);		
            this.close_bracket.Location = new Point(183, 232);
            this._3vx_bt.Location = new Point(183, 340);
            this.btFactorial.Location = new Point(183, 268);
            this._10x_bt.Location = new Point(183, 377);
            this.nvx_bt.Location = new Point(183, 304);

            this.open_bracket.Location = new Point(141, 232);
            this.xn_bt.Location = new Point(141, 304);
            this.log_bt.Location = new Point(141, 377);
            this.x3_bt.Location = new Point(141, 340);
            this.x2_bt.Location = new Point(141, 268);

            this.pi_bt.Location = new Point(12, 340);
            this.int_bt.Location = new Point(12, 268);
            this.dms_bt.Location = new Point(12, 304);
            this.fe_bt.Location = new Point(12, 377);
            this.angleGB.Location = new Point(12, 190);
            this.nonameTB.Location = new Point(12, 232);

            this.modsciBT.Location = new Point(98, 377);
            this.ln_bt.Location = new Point(98, 232);
            this.cos_bt.Location = new Point(98, 304);
            this.sin_bt.Location = new Point(98, 268);
            this.tan_bt.Location = new Point(98, 340);

            this.cosh_bt.Location = new Point(55, 304);
            this.exp_bt.Location = new Point(55, 377);
            this.inv_bt.Location = new Point(55, 232);
            this.tanh_bt.Location = new Point(55, 340);
            this.sinh_bt.Location = new Point(55, 268);

            this.sqrt_bt.Location = new Point(394, 232);
            this.percent_bt.Location = new Point(394, 268);
            this.invert_bt.Location = new Point(394, 304);
            this.equal.Location = new Point(394, 340);
            this.mem_minus_bt.Location = new Point(394, 196);

            this.divbt.Location = new Point(353, 268);
            this.mulbt.Location = new Point(353, 304);
            this.minusbt.Location = new Point(353, 340);
            this.addbt.Location = new Point(353, 377);
            this.doidau.Location = new Point(353, 232);
            this.madd.Location = new Point(353, 196);

            this.btdot.Location = new Point(312, 377);
            this.clearbt.Location = new Point(312, 232);
            this.num9.Location = new Point(312, 268);
            this.mstore.Location = new Point(312, 196);
            this.num6.Location = new Point(312, 304);
            this.num3.Location = new Point(312, 340);

            this.num0.Location = new Point(230, 377);
            this.memclear.Location = new Point(230, 196);
            this.backspace.Location = new Point(230, 232);
            this.num7.Location = new Point(230, 268);
            this.num4.Location = new Point(230, 304);
            this.num1.Location = new Point(230, 340);

            this.memrecall.Location = new Point(271, 196);
            this.ce.Location = new Point(271, 232);
            this.num8.Location = new Point(271, 268);
            this.num2.Location = new Point(271, 340);
            this.num5.Location = new Point(271, 304);
            // 
            // scr_lb
            // 
            this.scr_lb.Size = new Size(393, 30);
            // 
            // mem_lb
            // 
            this.mem_lb.Size = new Size(18, 16);
            this.mem_lb.Text = "M";
            this.mem_lb.Visible = (mem_num != 0);
            // 
            // screenPN
            // 
            this.screenPN.Location = new Point(12, 148);
            this.screenPN.Size = new Size(418, 42);

            hideSciComponent(true);

            this.historyPN.Location = new Point(12, 17);
            this.historyPN.Size = new Size(418, 132);
            this.historyPN.Visible = true;
            this.historyDGV.Location = new Point(-1, 41);
            this.historyDGV.Size = new Size(418, 90);

            this.upBT.Location = new Point(screenPN.Size.Width - 60, upBT.Location.Y);
            this.dnBT.Location = new Point(screenPN.Size.Width - 31, dnBT.Location.Y);
            // 
            // Column1
            // 
            this.Column1.Width = 416;
            // 
            // Calculator
            // 
            //this.Size = new Size(447, 470);
        }
        /// <summary>
        /// khởi tạo mảng các label trong panel dưới màn hình của form programmer
        /// </summary>
        private void InitBitNumberArray()
        {
            #region init bit number
            bit_digit = new Label[16];
            for (int i = 1; i >= 0; i--)
                for (int j = 7; j >= 0; j--)
                {
                    int k = 8 * i + j;  // k = 8j + j
                    bit_digit[k] = new Label();
                    binaryPN.Controls.Add(bit_digit[k]);
                    bit_digit[k].Text = "0000";
                    bit_digit[k].TabIndex = k;
                    bit_digit[k].Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
                    bit_digit[k].Size = new Size(40, 15);
                    bit_digit[k].Location = new Point(374 - 53 * j, 34 - 32 * i);
                    bit_digit[k].MouseMove += new MouseEventHandler(bit_digit_MouseMove);
                    bit_digit[k].Click += new EventHandler(bit_digit_Click);
                    bit_digit[k].MouseDown += new MouseEventHandler(EnableKeyboardAndChangeFocus);
                }
            #endregion

            #region init index number
            flagpoint = new Label[6];
            for (int i = 1; i >= 0; i--)
                for (int j = 2; j >= 0; j--)
                {
                    int k = 3 * i + j;  // k = 3j + j
                    int idex = 16 * (k - (i == 1).GetHashCode()) - ((3 * i + j) % 3 != 0).GetHashCode();
                    flagpoint[k] = new Label();
                    binaryPN.Controls.Add(flagpoint[k]);
                    flagpoint[k].Text = "" + idex;
                    flagpoint[k].TabIndex = k;
                    if (i == 1 && j == 0) flagpoint[k].Text = "  " + idex;
                    if (i == 0 && j == 0) flagpoint[k].Text = "   " + idex;
                    flagpoint[k].ForeColor = SystemColors.GrayText;
                    flagpoint[k].Font = new Font("Consolas", 10.25F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
                    flagpoint[k].Location = new Point(428 - 213 * j - 54 * (k % 3 == 0).GetHashCode(), 47 - 30 * i);
                }
            //bit_digit[63].Location = new Point(12, 5);
            #endregion
        } 
        /// <summary>
        /// programmer mode
        /// </summary>
        private void programmerMode()
        {
            this.equal.Location = new Point(394, 293);
            this.divbt.Location = new Point(352, 221);
            this.mulbt.Location = new Point(352, 257);
            this.minusbt.Location = new Point(352, 293);
            this.addbt.Location = new Point(352, 329);
            this.clearbt.Location = new Point(309, 185);
            this.mem_minus_bt.Location = new Point(394, 149);
            this.memrecall.Location = new Point(267, 149);
            this.doidau.Location = new Point(352, 185);
            this.madd.Location = new Point(352, 149);
            this.mstore.Location = new Point(309, 149);
            this.memclear.Location = new Point(225, 149);
            this.ce.Location = new Point(267, 185);
            this.backspace.Location = new Point(225, 185);
            this.RoLBT.Location = new Point(98, 221);
            this.orBT.Location = new Point(98, 257);
            this.LshBT.Location = new Point(98, 293);
            this.modsciBT.Location = new Point(141, 149);
            this.RoRBT.Location = new Point(141, 221);
            this.RshBT.Location = new Point(141, 293);
            this.basePN.Location = new Point(12, 149);
            this.open_bracket.Location = new Point(98, 185);
            this.close_bracket.Location = new Point(141, 185);
            this.nonameTB.Location = new Point(98, 149);
            this.NotBT.Location = new Point(98, 329);
            this.unknownPN.Location = new Point(12, 257);
            this.AndBT.Location = new Point(141, 329);
            this.XorBT.Location = new Point(141, 257);
            this.binaryPN.Location = new Point(12, 67);
            this.modproBT.Location = new Point(141, 149);
            this.openproBT.Location = new Point(98, 185);
            this.closeproBT.Location = new Point(141, 185);
            this.num0.Location = new Point(225, 329);
            this.num1.Location = new Point(225, 293); 
            this.num2.Location = new Point(267, 293);
            this.num3.Location = new Point(309, 293);
            this.num4.Location = new Point(225, 257);
            this.num5.Location = new Point(267, 257);
            this.num6.Location = new Point(309, 257);
            this.num7.Location = new Point(225, 221);
            this.num8.Location = new Point(267, 221);
            this.num9.Location = new Point(309, 221);
            this.btnA.Location = new Point(183, 149);
            this.btnB.Location = new Point(183, 185);
            this.btnC.Location = new Point(183, 221);
            this.btnD.Location = new Point(183, 257);
            this.btnE.Location = new Point(183, 293);
            this.btnF.Location = new Point(183, 329);

            this.screenPN.Location = new Point(12, 17);
            this.screenPN.Size = new Size(418, 42);

            //this.scr_lb.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            //this.scr_lb.Location = new Point(20, 19);
            this.scr_lb.Size = new Size(393, 30);
            this.scr_lb.Text = "0";

            this.mem_lb.Visible = (mem_num != 0);

            this.sqrt_bt.Enabled = false;
            this.sqrt_bt.Location = new Point(394, 185);

            this.percent_bt.Enabled = false;
            this.percent_bt.Location = new Point(394, 221);

            this.invert_bt.Enabled = false;
            this.invert_bt.Location = new Point(394, 257);

            this.btdot.Enabled = false;
            this.btdot.Location = new Point(309, 329);

            this.percent_bt.Location = new Point(395, 221);
            this.percent_bt.Enabled = false;

            this.doidau.Enabled = false;

            datecalcGB.Size = new Size(345, 350);
            unitconvGB.Size = new Size(345, 350);
            datecalcGB.Location = new Point(446, 9);
            unitconvGB.Location = new Point(446, 9);

            baseRBCheckedChanged();
            copyHistoryTSMI.Enabled = false;
            clearHistoryBT.Enabled = false;
            // 
            // Programmer
            // 
            //this.Size = new Size(447, 420);
        }
        /// <summary>
        /// statistics mode
        /// </summary>
        private void statisticsMode()
        {
            this.statisticsPN.Location = new Point(12, 17);
            this.statisticsDGV.Location = new Point(-1, 24);
            this.screenPN.Location = new Point(12, 148);
            this.scr_lb.Location = new Point(23, 9);
            this.mem_lb.Location = new Point(2, 21);
            this.operator_lb.Location = new Point(2, 2);
            this.Expsta_bt.Location = new Point(182, 232);
            this.sigmax2BT.Location = new Point(182, 304);
            this.AddstaBT.Location = new Point(182, 377);
            this.xcross.Location = new Point(139, 268);
            this.sigmaxBT.Location = new Point(139, 304);
            this.sigmanBT.Location = new Point(139, 340);
            this.doidau.Location = new Point(139, 377);
            this.x2cross.Location = new Point(182, 268);
            this.sigman_1BT.Location = new Point(182, 341);
            this.x2cross.Location = new Point(182, 268);
            this.sigman_1BT.Location = new Point(182, 341);
            this.btdot.Location = new Point(96, 377);
            this.clearsta.Location = new Point(96, 232);
            this.mem_minus_bt.Location = new Point(182, 196);
            this.memrecall.Location = new Point(96, 196);
            this.fe_bt.Location = new Point(139, 232);
            this.madd.Location = new Point(139, 196);
            this.backspace.Location = new Point(12, 232);
            this.mstore.Location = new Point(54, 196);
            this.memclear.Location = new Point(12, 196);
            this.CAD.Location = new Point(54, 232);
            this.num8.Location = new Point(54, 268);
            this.num5.Location = new Point(54, 304);
            this.num2.Location = new Point(54, 340);
            this.num9.Location = new Point(96, 268);
            this.num6.Location = new Point(96, 304);
            this.num3.Location = new Point(96, 340);
            this.num7.Location = new Point(12, 268);
            this.num4.Location = new Point(12, 304);
            this.num1.Location = new Point(12, 340);
            this.num0.Location = new Point(12, 377);

            this.screenPN.Size = new Size(206, 42);
            this.scr_lb.Size = new Size(178, 22);

            this.sigman_1BT.Visible = true;
            this.sigmanBT.Visible = true;
            this.sigmaxBT.Visible = true;
            this.sigmax2BT.Visible = true;
            this.xcross.Visible = true;
            this.x2cross.Visible = true;
            this.statisticsPN.Visible = true;

            while (statisticsDGV.Rows.Count > 0) statisticsDGV.Rows.RemoveAt(0);
            this.statisticsDGV.CurrentCell = null;
        }

        #endregion
        
        #region mousedown event
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình
        /// </summary>
        private void EnableKeyboard(object sender, EventArgs e)
        {
            prcmdkey = true;
            notfromTB_MouseDown();
        }
        /// <summary>
        /// Tắt tính năng bắt sự kiện phím của chương trình
        /// </summary>
        private void DisableKeyboard(object sender, EventArgs e)
        {
            prcmdkey = false;
            notfromTB_MouseDown();
        }
        /// <summary>
        /// Bật tính năng bắt sự kiện phím của chương trình và thay đổi 
        /// thuộc tính focus của 2 textbox trong form unit conversion, hàm bình thường
        /// </summary>
        private void EnableKeyboardAndChangeFocus()
        {
            prcmdkey = true;
            notfromTB_MouseDown();
            if (fromTB.Focused || toTB.Focused || typeCB.Focused) fromLB.Focus();
            bool boolean = false;
            for (int i = 0; i < 11; i++)
            {
                boolean = boolean || (fcb[i].Focused || tcb[i].Focused);
            }
            if (boolean) fromLB.Focus();
            if (calmethodCB.Focused || result2.Focused || result1.Focused
                || periodsDateUD.Focused || dtP1.Focused || dtP2.Focused)
            {
                label2.Focus();
            }
            if (dtP1.Focused || dtP2.Focused) label2.Focus();
            if (historyTSMI.Checked && historyTSMI.Enabled)
            {
                historyDGV.CurrentCell = null;
                currentCellToNull();
            }
            if (statisticsTSMI.Checked) statisticsDGV.CurrentCell = null;
            //clearHistoryBT.Enabled = false;
            //clearHistoryTSMI.Enabled = false;
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
        /// <summary>
        /// fromTB mất focus
        /// </summary>
        private void notfromTB_MouseDown()
        {
            if (fromTB.Text == "" && fromTB.ForeColor == SystemColors.ControlText)
            {
                fromTB.Text = "Enter value";
                fromTB.ForeColor = SystemColors.GrayText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte) (0)));
            }
        }

        #endregion

        #region register component
        
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
        private Panel screenPN;
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

        private GroupBox angleGB;
        private TextBox nonameTB;
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
        private Button fe_bt;
        private Button exp_bt;
        private Button modsciBT;
        private Button ln_bt;
        private Button dms_bt;
        private Button tanh_bt;
        private Button cosh_bt;
        private Button inv_bt;
        private Button int_bt;
        private Button tan_bt;
        private Button sinh_bt;
        private Button cos_bt;
        private Button sin_bt;
        private Button btFactorial;
        private RadioButton deg_rb;
        private RadioButton rad_rb;
        private RadioButton gra_rb;

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
        private Panel historyPN;
        private Button dnBT;
        private Button upBT;
        private Button clearHistoryBT;
        private DataGridView historyDGV;
        private DataGridViewTextBoxColumn Column1;

        private MenuItem viewTSMI;
        private MenuItem standardTSMI;
        private MenuItem scientificTSMI;
        private MenuItem programmerTSMI;
        private MenuItem statisticsTSMI;
        private MenuItem toolStripSeparator1;
        private MenuItem historyTSMI;
        private MenuItem digitGroupingTSMI;
        private MenuItem toolStripSeparator2;
        private MenuItem basicTSMI;
        private MenuItem unitConversionTSMI;
        private MenuItem dateCalculationTSMI;
        private MenuItem editTSMI;
        private MenuItem copyTSMI;
        private MenuItem pasteTSMI;
        private MenuItem sepTSMI3;
        private MenuItem historyOptionTSMI;
        private MenuItem copyHistoryTSMI;
        private MenuItem clearHistoryTSMI;
        private MenuItem datasetTSMI;
        private MenuItem copyDatasetTSMI;
        private MenuItem clearDatasetTSMI;
        private MenuItem helpTSMI;
        private MenuItem helpTopicsTSMI;
        private MenuItem toolStripSeparator4;
        private MenuItem aboutTSMI;
        private MainMenu menuStrip1;

        private ContextMenu contextMenu1;
        private MenuItem copyCTMN;
        private MenuItem pasteCTMN;
        private MenuItem sepCTMN;
        private MenuItem showHistoryCTMN;
        private MenuItem hideHistoryCTMN;
        private MenuItem clearDataSetCTMN;
        private MenuItem clearHistoryCTMN;

        private Panel unknownPN;
        private RadioButton byteRB;
        private RadioButton qwordRB;
        private RadioButton wordRB;
        private RadioButton dwordRB;
        private Panel basePN;
        private RadioButton binRB;
        private RadioButton octRB;
        private RadioButton decRB;
        private RadioButton hexRB;
        private Panel binaryPN;
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
        private Button orBT;
        private Button RoLBT;
        private Button btnA;
        private Button btnC;
        private Button openproBT;
        private Button modproBT;
        private Button closeproBT;
        private Label[] bit_digit;
        private Label[] flagpoint;
        
        private Button Expsta_bt;
        private Button sigmax2BT;
        private Button sigman_1BT;
        private Button AddstaBT;
        private Button xcross;
        private Button sigmaxBT;
        private Button sigmanBT;
        private Button CAD;
        private Button x2cross;
        private Panel statisticsPN;
        private DataGridView statisticsDGV;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private Button clearsta;
        private Label countlb;
        #endregion
    }
}