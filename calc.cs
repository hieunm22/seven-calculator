using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator
{
    public partial class calc : Form
    {
        public calc()
        {
            InitializeComponent();  // Required method for Designer support
            InitComboBox();         // khởi tạo mảng combobox của form unit calculation
            InitBitNumberArray();   // khởi tạo mảng nhị phân form programmer
            FocusedEvents();        // sự kiện focused cho các nút trên form ban đầu
        }

        #region local varial
        string str = "0", sci_exp = "";
        bool confirm_num = true, prcmdkey = true, isMathError = false, propertiesChange = false;
        int pre_bt = -1, pre_oprt = 0, pre_priority = 0, priority = 0;
        BigNumber mem_num = "0";
        Parser parser = new Parser();
        Exception pex = null;
        #endregion

        #region cac su kien lien quan den form
        /// <summary>
        /// form load
        /// </summary>
        private void calc_Load(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
            }
            contextMenuVisible();
            loadInfoFromFile();
            getMemoryNumber();
            // giá trị của prcmdkey tuỳ thuộc vào control nào của calculator được focused
            if (!basicMI.Checked)
                prcmdkey = dtP1.Focused || dtP2.Focused || fromTB.Focused || toTB.Focused;
            getPaste();
        }
        /// <summary>
        /// COMMENT HERE
        /// </summary>
        private void calc_Activated(object sender, EventArgs e)
        {
            btdot.Text = Misc.DecimalSym;
            //scr_lb.Focus();
        }
        /// <summary>
        /// thay đổi kích thước font để hiển thị đủ kết quả lên màn hình
        /// </summary>
        private void scr_lb_TextChanged(object sender, EventArgs e)
        {
            scr_lb.Font = fontChanged(scr_lb.Text.Length);
        }

        private void buttonLostFocus(object sender, MouseEventArgs e)
        {
            scr_lb.Focus();
        }
        #endregion

        #region Nhap so va cac phep tinh
        private void numberinput_Click(object sender, EventArgs e)
        {
            if (!isMathError)
            {
                Button btn = (Button)sender;
                if (programmerMI.Checked) numinput_pro(btn.TabIndex);
                else numinput(btn.TabIndex);
            }
        }

        private void operatorBT_Click(object sender, EventArgs e)
        {
            isFuncClicked = false;
            if (!isMathError)
            {
                int tab = ((Button)sender).TabIndex;
                if (standardMI.Checked) std_operation(tab);
                if (scientificMI.Checked) sci_operation(tab);
                if (programmerMI.Checked) pro_operation(tab);
            }
        }
        #endregion

        #region Xu ly so hien tren man hinh
        //
        // nut bang
        //
        private void equal_Click(object sender, EventArgs e)
        {
            if (!isMathError)
            {
                confirm_num = true;
                if (standardMI.Checked)       // standard.checked
                {
                    #region standard operation
                    if (sci_exp == "") sci_exp = preFunc;
                    if (pre_oprt != 0) sci_exp += " " + preFunc;

                    parser.EvaluateStd(sci_exp);
                    // form standard nay tinh chinh xac den 15 chu so nen dung kieu double de hien thi cung duoc
                    str = parser.strResult;

                    #region đưa lên grid
                    hisDGV.CellStateChanged -= historyDGV_CellStateChanged;
                    hisDGV.Rows.Add();
                    hisDGV.CellStateChanged += historyDGV_CellStateChanged;
                    hisDGV[0, hisDGV.Rows.Count - 1].Value = sci_exp;
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.Rows.Count - 1];
                    rowIndex = hisDGV.CurrentCell.RowIndex;
                    #endregion

                    sci_exp = "";
                    displayToScreen();
                    historyOptionEnabled();
                    #endregion
                }
                if (scientificMI.Checked)     // scientific.checked
                {
#warning hàm dấu bằng của scientific
                    #region scientific operation
                    if (sci_exp == "") sci_exp = preFunc;
                    while (sci_exp[0] == '(' && sci_exp[sci_exp.Length - 1] == ')')
                    {
                        sci_exp = sci_exp.Substring(1, sci_exp.Length - 2);
                    }
                    if (deg_rb.Checked) parser.Mode = Mode.DEG;
                    if (rad_rb.Checked) parser.Mode = Mode.RAD;
                    if (gra_rb.Checked) parser.Mode = Mode.GRA;

                    if (pre_oprt != 0) sci_exp += " " + preFunc;

                    parser.EvaluateSci(sci_exp);
                    BigNumber result_Sci = parser.strResult;
                    str = result_Sci.StringValue;

                    #region write to history panel
                    hisDGV.CellStateChanged -= historyDGV_CellStateChanged;
                    hisDGV.Rows.Add();
                    //hisDGV[0, hisDGV.Rows.Count - 1].Value = standardExpressionString(sci_exp);
                    hisDGV[0, hisDGV.Rows.Count - 1].Value = sci_exp;
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.Rows.Count - 1];
                    hisDGV.CellStateChanged += historyDGV_CellStateChanged;
                    rowIndex = hisDGV.CurrentCell.RowIndex;
                    #endregion

                    sci_exp = powerFunc = mulDivFunc = "";
                    isFuncClicked = false;
                    displayToScreen();
                    historyOptionEnabled();

                    #endregion
                }
                if (programmerMI.Checked)     // programmer.checked
                {
                    #region programmer operation
                    screenToPanel();
                    if (pre_oprt != 0)
                    {
                        num1pro = resultpro;
                        num2pro = decnum;
                    }
                    else goto jump;
                    //if (pre_oprt >= 12 && pre_oprt <= 15)
                    //    num2pro = decnum;
                    //else
                    //    resultpro = decnum;
                    if (pre_oprt == 00) resultpro = str;
                    if (pre_oprt == 12) resultpro = num1pro + num2pro;
                    if (pre_oprt == 13) resultpro = num1pro - num2pro;
                    if (pre_oprt == 14) resultpro = num1pro * num2pro;
                    if (pre_oprt == 15)
                    {
                        if (num2pro != 0)
                        {
                            resultpro = num1pro / num2pro;
                            resultpro = resultpro.IntString;    // kết quả phép chia này lấy phân nguyên
                        }
                        else
                        {
                            scr_lb.Text = "Cannot devide by zero";
                            goto jump;
                        }
                    }
                    if (pre_oprt == 211) resultpro = num1pro - num2pro * (num1pro / num2pro).Floor();                     // mod
                    if (pre_oprt == 199) resultpro = ulong.Parse(num1pro.StringValue) | ulong.Parse(num2pro.StringValue); // or
                    if (pre_oprt == 204) resultpro = ulong.Parse(num1pro.StringValue) & ulong.Parse(num2pro.StringValue); // and
                    if (pre_oprt == 205) resultpro = ulong.Parse(num1pro.StringValue) ^ ulong.Parse(num2pro.StringValue); // xor
                    if (pre_oprt == 200 || pre_oprt == 210)
                    {
                        // se co exception khi num1pro hoac num2pro vuot qua 2^32
                        if (pre_oprt == 200) resultpro = (ulong)(int.Parse(num1pro.StringValue) << int.Parse(num2pro.StringValue)); // Lsh
                        if (pre_oprt == 210) resultpro = (ulong)(int.Parse(num1pro.StringValue) >> int.Parse(num2pro.StringValue)); // Rsh
                        if (dwordRB.Checked) resultpro = uint.Parse(resultpro.StringValue);
                        if (_wordRB.Checked) resultpro = ushort.Parse(resultpro.StringValue);
                        if (_byteRB.Checked) resultpro = byte.Parse(resultpro.StringValue);
                    }
                    programmerOperation();
                    #endregion
                }
                displayToScreen();
            jump: pre_oprt = 0;
                pre_bt = 16;
                operator_lb.Visible = false; 
            }
        }
        //
        // nut ham chuc nang
        //
        private void functionBT_Click(object sender, EventArgs e)
        {
            if (!isMathError)
            {
                int tabIndex = ((Button)sender).TabIndex;
                math_func((Button)sender);
                pre_bt = tabIndex;
            }
        }
        //
        // nut exp
        //
        private void exp_bt_Click(object sender, EventArgs e)
        {

        }
        //
        // nut %
        //
        private void percent_Click(object sender, EventArgs e)
        {
            confirm_num = true;
            if (pre_oprt != 0)
            {
                BigNumber scr_num = scr_lb.Text;
                parser.EvaluateStd(sci_exp.Substring(0, sci_exp.Length - 1));
                scr_num = scr_num / 100.0 * parser.strResult;  // 1 hoac 2 cung duoc
                str = preFunc = scr_num.StringValue;
            }
            displayToScreen();
            pre_bt = 18;
        }

        private void pi_bt_Click(object sender, EventArgs e)
        {
            confirm_num = true;
            if (!inv_ChkBox.Checked)
            {
                str = (BigNumber.BN_PI).StringValue;
                preFunc = "pi";
            }
            else
            {
                str = (2 * BigNumber.BN_PI).StringValue;
                preFunc = "2 * pi";
            }
            displayToScreen();
            isFuncClicked = true;
            if (inv_ChkBox.Checked) inv_ChkBox.Checked = false;
            pre_bt = 144;
        }

        private void fe_ChkBox_CheckChanged(object sender, EventArgs e)
        {
            if (fe_ChkBox.Checked)
            {
                scr_lb.Text = new BigNumber(str).ToString();
            }
            else
            {
                if (digitGroupingMI.Checked) scr_lb.Text = Misc.grouping(str);
                else scr_lb.Text = str;
            }
        }

        private void inv_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inv_ChkBox.Checked)
            {
                sin_bt.Font = new Font("Tahoma", 8.25F);
                sin_bt.Text = "asin";
                cos_bt.Font = new Font("Tahoma", 7.5F);
                cos_bt.Text = "acos";
                tan_bt.Font = new Font("Tahoma", 7.5F);
                tan_bt.Text = "atan";
                sinh_bt.Font = new Font("Tahoma", 6.75F);
                sinh_bt.Text = "asinh";
                cosh_bt.Font = new Font("Tahoma", 6F);
                cosh_bt.Text = "acosh";
                tanh_bt.Font = new Font("Tahoma", 6F);
                tanh_bt.Text = "atanh";
                ln_bt.Text = "eⁿ";
                int_bt.Font = new Font("Tahoma", 9F);
                int_bt.Text = "Frac";
                int_bt.Font = new Font("Tahoma", 7.5F);
                dms_bt.Text = "deg";
                pi_bt.Text = "2*π";
                pi_bt.Font = new Font(pi_bt.Font.FontFamily, 9F);
            }
            else
            {
                sin_bt.Font = new Font("Tahoma", 9.75F);
                sin_bt.Text = "sin";
                cos_bt.Font = new Font("Tahoma", 9F);
                cos_bt.Text = "cos";
                tan_bt.Font = new Font("Tahoma", 9F);
                tan_bt.Text = "tan";
                sinh_bt.Font = new Font("Tahoma", 8.25F);
                sinh_bt.Text = "sinh";
                cosh_bt.Font = new Font("Tahoma", 7.5F);
                cosh_bt.Text = "cosh";
                tanh_bt.Font = new Font("Tahoma", 7.5F);
                tanh_bt.Text = "tanh";
                ln_bt.Text = "ln";
                int_bt.Font = new Font("Tahoma", 9.75F);
                int_bt.Text = "Int";
                int_bt.Font = new Font("Tahoma", 9.75F);
                dms_bt.Text = "dms";
                pi_bt.Text = "π";
                pi_bt.Font = new Font(pi_bt.Font.FontFamily, 12F);
            }
        }
        //
        // nut x^n
        //
        private void xn_bt_Click(object sender, EventArgs e)
        {
            sci_operation(30);
        }
        //
        // nut nvx (can bac n cua x)
        //
        private void nvx_bt_Click(object sender, EventArgs e)
        {
            sci_operation(34);
        }
        //
        // nut xoa toan bo
        //
        private void clear_Click(object sender, EventArgs e)
        {
            clear_num(true);
        }
        //
        // nut xoa so vua nhap
        //
        private void ce_Click(object sender, EventArgs e)
        {
            clear_num(false);
        }
        //
        // nut xoa ki tu so vua nhap
        //
        private void backspace_Click(object sender, EventArgs e)
        {
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
                    confirm_num = (str.Length == 2 & str.StartsWith("-"));
                }
                else
                {
                    str = "0";
                    confirm_num = true;
                }
            }
            else str = "0";
            if (programmerMI.Checked) screenToPanel();
            else displayToScreen();
            pre_bt = 22;
        }

        byte openBRK = 0, closeBRK = 0;
        string[] bracketExp = new string[20];
        private void open_bracket_Click(object sender, EventArgs e)
        {
            if (!isMathError)
            {
                str = "0";
                displayToScreen();
                if (openBRK - closeBRK < 20)
                {
                    openBRK++;
                    if (openBRK == closeBRK) bracketTime_lb.Text = string.Format("(=1");
                    else bracketTime_lb.Text = string.Format("(={0}", openBRK - closeBRK);
                }
            }
        }

        private void close_bracket_Click(object sender, EventArgs e)
        {
            if (!isMathError)
            {
                if (openBRK - closeBRK > 1)
                {
                    closeBRK++;
                    bracketTime_lb.Text = string.Format("(={0}", openBRK - closeBRK);
                }
                else
                {
                    //openTime = 0;
                    bracketTime_lb.Text = "";
                }
            }
        }

        private void bracketTime_lb_TextChanged(object sender, EventArgs e)
        {
            switch (bracketTime_lb.Text.Length)
            {
                case 3:
                    bracketTime_lb.Font = new Font(Font.FontFamily, 9F);
                    break;
                case 4:
                    bracketTime_lb.Font = new Font(Font.FontFamily, 8.25F);
                    break;
            }
        }
        #endregion

        #region Xu ly so M
        private void clearMemory()
        {
            if (standardMI.Checked || scientificMI.Checked)
            {
                mem_num = 0;
                mem_lb.Visible = false;
                propertiesChange = confirm_num = true;
            }
        }
        //
        // nut xoa bo nho
        //
        private void memclear_Click(object sender, EventArgs e)
        {
            clearMemory();
        }

        private void recallMemory()
        {
            if (standardMI.Checked || scientificMI.Checked && mem_lb.Visible)
            {
                str = mem_num.StringValue;
            }
            else
            {
                str = "0";
            }
            displayToScreen();
            confirm_num = true;
        }        //
        // nut goi so trong bo nho ra man hinh
        //
        private void memrecall_Click(object sender, EventArgs e)
        {
            if (!isMathError) recallMemory();
        }
        //
        // nut cong so them tren man hinh bo nho
        //
        private void madd_Click(object sender, EventArgs e)
        {
            /*if (standardMI.Checked || scientificMI.Checked) */
            mem_process(1);
        }
        //
        // nut bot di so tren man hinh bo nho
        //
        private void mmul_Click(object sender, EventArgs e)
        {
            /*if (standardMI.Checked || scientificMI.Checked) */
            mem_process(2);
        }
        //
        // nut luu so tren man hinh bo nho
        //
        private void mstore_Click(object sender, EventArgs e)
        {
            /*if (standardMI.Checked || scientificMI.Checked) */
            mem_process(3);
        }
        #endregion

        #region Cac menu item duoc click
        private void standardMI_Click(object sender, EventArgs e)
        {
            if (!standardMI.Checked) stdLoad(false);
        }

        private void scientificMI_Click(object sender, EventArgs e)
        {
            if (!scientificMI.Checked) sciLoad(false);
        }

        private void programmerMI_Click(object sender, EventArgs e)
        {
            if (!programmerMI.Checked) proLoad(false);
        }

        private void statisticsMI_Click(object sender, EventArgs e)
        {
            if (!statisticsMI.Checked) staLoad(false);
        }

        private void digitGroupingMI_Click(object sender, EventArgs e)
        {
            digitLoad(false);
        }

        private void digitLoad(bool isLoaded)
        {
            digitGroupingMI.Checked = !digitGroupingMI.Checked;
            if (!isLoaded) propertiesChange = true;
            if (Misc.isNumber(toTB.Text) && unitConversionMI.Checked)
            {
                if (digitGroupingMI.Checked)
                {
                    if (!toTB.Text.Contains("E")) toTB.Text = Misc.grouping(toTB.Text);
                }
                else toTB.Text = Misc.de_group(toTB.Text);
            }
            if (!programmerMI.Checked)
            {
                if (Misc.isNumber(str)) displayToScreen();
            }
            else
            {
                if (digitGroupingMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = Misc.grouping(str);
                    if (octRB.Checked) scr_lb.Text = Misc.grouping(str, 3);
                    if (binRB.Checked || hexRB.Checked)
                        scr_lb.Text = Misc.grouping(str, 4);
                }
                else scr_lb.Text = Misc.de_group(scr_lb.Text);
            }
        }

        private void basicMI_Click(object sender, EventArgs e)
        {
            int his = (historyMI.Checked && historyMI.Enabled).GetHashCode();
            if (!basicMI.Checked)
            {
                basicMI.Checked = true;
                unitConversionMI.Checked = false;
                dateCalculationMI.Checked = false;
                if (standardMI.Checked)
                {
                    if (historyMI.Checked) stdWithHistory();
                    else initializedForm(false);
                    this.Size = new Size(216, 315 + 105 * his);
                }
                if (scientificMI.Checked)
                {
                    if (historyMI.Checked) sciWithHistory();
                    else scientificLoad(false);
                    this.Size = new Size(413, 315 + 105 * his);
                    //this.Size = new Size(415 + 363 * ex.GetHashCode(), 315 + 105 * his);
                }
                if (programmerMI.Checked)
                {
                    programmerMode();
                    this.Size = new Size(413, 374);
                }
                if (statisticsMI.Checked)
                {
                    statisticsMode();
                    this.Size = new Size(216, 420);
                }
                datecalcGB.Visible = false;
                unitconvGB.Visible = false;
                displayToScreen();
                prcmdkey = true;
                propertiesChange = true;
            }
        }

        private void unitConversionMI_Click(object sender, EventArgs e)
        {
            exFunc(unitConversionMI, false);
        }

        private void dateCalculationMI_Click(object sender, EventArgs e)
        {
            exFunc(dateCalculationMI, false);
        }

        private void copyCTMN_Click(object sender, EventArgs e)
        {
            /*if (Misc.isNumber(str) && !isMathError) */Clipboard.SetText(str);
        }        

        private void getPaste()
        {
            IDataObject iData = new DataObject();
            iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text))
            {
                if (!programmerMI.Checked)
                {
                    pasteMI.Enabled = (iData != null);
                }
                else
                {
                    if (binRB.Checked)
                        pasteMI.Enabled = Binary.CheckIsBin(Clipboard.GetText().Trim());
                    if (octRB.Checked)
                        pasteMI.Enabled = Binary.CheckIsOct(Clipboard.GetText().Trim());
                    if (decRB.Checked)
                        pasteMI.Enabled = Binary.CheckIsDec(Clipboard.GetText().Trim());
                    if (hexRB.Checked)
                    {
                        pasteMI.Enabled = Binary.CheckIsHex(Clipboard.GetText().Trim());
                        pasteMI.Enabled &= hexRB.Visible = true;
                    }
                }
            }
            else pasteMI.Enabled = false;
            pasteCTMN.Enabled = pasteMI.Enabled;
        }

        private void pasteCTMN_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText().ToUpper();
            //if (data.Contains("\n"))
            //{
            //    data = data.Substring(0, data.IndexOf("\n"));
            //}
            //if (data.Contains("="))
            //{
            //    data = data.Substring(0, data.IndexOf("=") + 1);
            //}
            int len = data.Length;
            Keys[] keyData = new Keys[len];
            Message msg = Message.Create(nextClipboardViewer, 1, nextClipboardViewer, nextClipboardViewer); // tạo bừa thôi, hàm ProcessCmdKey có dùng cái này đâu
            for (int i = 0; i < len; i++)
            {
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
                    case 40: keyData[i] = Keys.OemOpenBrackets;
                        break;
                    case 41: keyData[i] = Keys.OemCloseBrackets;
                        break;
                    case 43: keyData[i] = Keys.Add;
                        break;
                    case 42: keyData[i] = Keys.Multiply;
                        break;
                    case 44:
                    case 46: keyData[i] = Keys.Decimal;
                        break;
                    case 45: keyData[i] = Keys.OemMinus;
                        break;
                    case 47: keyData[i] = Keys.Divide;
                        break;
                    case 64: keyData[i] = Keys.Shift | Keys.D2;
                        break;
                    case 94: keyData[i] = Keys.Y;
                        break;
                }
                ProcessCmdKey(ref msg, keyData[i]);
            }
            if (programmerMI.Checked) screenToPanel();
            else displayToScreen();
        }

        private void standardExpr_Click(object sender, EventArgs e)
        {
            standardExpr.Checked = !standardExpr.Checked;
        }

        private void reCalculate_Click(object sender, EventArgs e)
        {
            recalculate(rowIndex);
        }

        private void cancelEditHisMI_Click(object sender, EventArgs e)
        {
            //hisDGV.CellEndEdit -= historyDGV_CellEndEdit;
            hisDGV.CancelEdit();
            //hisDGV.CellEndEdit -= historyDGV_CellEndEdit;
            hisDGV.ReadOnly = true;
            //hisDGV.CellEndEdit += historyDGV_CellEndEdit;
            hisDGV[0, rowIndex].Selected = true;
            historyOptionEnabled();
            prcmdkey = true;
            dgv_OnEndEdit(hisDGV);
            //hisDGV.CellEndEdit += historyDGV_CellEndEdit;
        }

        private void copyHistoryMI_Click(object sender, EventArgs e)
        {
            string clipboard = "";
            try
            {
                foreach (DataGridViewRow item in hisDGV.Rows)
                {
                    clipboard += item.Cells[0].Value + "\n";
                }
                Clipboard.SetText(clipboard);
            }
            catch (Exception ex)
            //{ /*làm gì giờ*/ }
            {
                Clipboard.SetText(ex.Message);
            }
        }

        private void editHistoryMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            hisDGV.ReadOnly = false;
            hisDGV.BeginEdit(false);
            historyOptionEnabled();
        }

        private void copyDatasetMI_Click(object sender, EventArgs e)
        {
            // DO SOMETHING HERE
        }

        private void editDatasetMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            staDGV.ReadOnly = false;
            staDGV.BeginEdit(false);
            statisticsOptionEnabled();
        }

        private void cancelEditDSMI_Click(object sender, EventArgs e)
        {
            //staDGV.CellEndEdit -= statisticsDGV_CellEndEdit;
            staDGV.CancelEdit();
            staDGV.ReadOnly = true;
            staDGV[0, rowIndex].Selected = true;
            statisticsOptionEnabled();
            prcmdkey = true;
            dgv_OnEndEdit(staDGV);
            //staDGV.CellEndEdit += statisticsDGV_CellEndEdit;
        }

        private void commitDSMI_Click(object sender, EventArgs e)
        {
            prcmdkey = true;
            staDGV.EndEdit();
            staDGV.ReadOnly = true;
            statisticsOptionEnabled();
        }

        private void aboutMI_Click(object sender, EventArgs e)
        {
            Form ab = new About();
            ab.ShowDialog();
        }

        private void helpTopicsMI_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("shortcuts-key.pdf");
            }
            catch { }
        }
        #endregion

        #region date calculation
        private void control_visible(bool tf)
        {
            addrb.Visible = !tf;
            subrb.Visible = !tf;
            label1.Visible = !tf;
            label7.Visible = !tf;
            label8.Visible = !tf;
            periodsDateUD.Visible = !tf;
            periodsMonthUD.Visible = !tf;
            periodsYearUD.Visible = !tf;
            label3.Visible = tf;
            dtP2.Visible = tf;
            label4.Visible = tf;
            result1.Visible = tf;
        }
        //
        // su kien datetimepiker thay doi
        //
        private void dtP_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(null, null);
            else
            { result1.Text = ""; result2.Text = ""; }
        }
        //
        // nut calculate
        //
        private void calculate_bt_Click(object sender, EventArgs e)
        {
            int[] differ = new int[4];

            if (calmethodCB.SelectedIndex == 0)
            {
                differ = Misc.differencesTimes(dtP2.Value, dtP1.Value);
                int diff = Misc.differenceBW2Dates(dtP1.Value, dtP2.Value);

                if (diff == 1)
                    result2.Text = "1 day";
                else if (diff > 1)
                    result2.Text = Misc.grouping(diff) + " days";
                else result1.Text = result2.Text = "Same date";
            }

            #region hiển thị kết quả lên textbox
            string text = "";
            if (differ[0] != 0)
            {
                if (differ[0] != 1) text = differ[0] + " years";
                else text = "1 year";
            }
            if (differ[0] != 0 && differ[1] != 0) text += ", ";
            if (differ[1] != 0)
            {
                if (differ[1] != 1) text += differ[1] + " months";
                else text += "1 month";
            }
            if ((differ[0] != 0 || differ[1] != 0) && differ[2] != 0) text += ", ";
            if (differ[2] != 0)
            {
                if (differ[2] != 1) text += differ[2] + " weeks";
                else text += "1 week";
            }
            if ((differ[0] != 0 || differ[1] != 0 || differ[2] != 0) && differ[3] != 0) text += ", ";
            if (differ[3] != 0)
            {
                if (differ[3] != 1) text += differ[3] + " days";
                else text += "1 day";
            }
            result1.Text = text;
            #endregion

            if (result1.Text == "") result1.Text = result2.Text;
            if (calmethodCB.SelectedIndex == 1)
            {
                DateTime dt = new DateTime();
                if (addrb.Checked)
                {
                    dt = dtP1.Value.AddYears((int)periodsYearUD.Value);
                    dt = dt.AddMonths((int)periodsMonthUD.Value);
                    dt = dt.AddDays((double)periodsDateUD.Value);
                }
                if (subrb.Checked)
                {
                    dt = dtP1.Value.AddYears(-(int)periodsYearUD.Value);
                    dt = dt.AddMonths(-(int)periodsMonthUD.Value);
                    dt = dt.AddDays(-(double)periodsDateUD.Value);
                }
                result2.Text = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
            }
        }

        private void cal_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            autocal_date.Checked = (readFromFile("AutoCalculate") == 1);
            propertiesChange = true;

            control_visible(calmethodCB.SelectedIndex == 0);
            periodsDateUD.Visible = (calmethodCB.SelectedIndex != 0);
            label2.Text = "From:";
            if (calmethodCB.SelectedIndex == 0)
            {
                label3.Text = "To: ";
                label5.Text = "Difference (days)";
            }
            if (calmethodCB.SelectedIndex == 1)
            {
                label3.Text = "Periods:";
                label5.Text = "Date:";
            }

            result2.Text = ""; // or calculate_bt_Click(sender, rowindex);
            result1.Text = "";
            dtP_ValueChanged(null, null);
        }

        private void periods_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(null, null);
        }

        private void autocal_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (result1.Text == "") dtP_ValueChanged(null, null);
            propertiesChange = true;
        }

        private void add_sub_CheckChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked)
            {
                DateTime dt = new DateTime();
                if (addrb.Checked)
                {
                    dt = dtP1.Value.AddYears((int)periodsYearUD.Value);
                    dt = dt.AddMonths((int)periodsMonthUD.Value);
                    dt = dt.AddDays((double)periodsDateUD.Value);
                }
                if (subrb.Checked)
                {
                    dt = dtP1.Value.AddYears(-(int)periodsYearUD.Value);
                    dt = dt.AddMonths(-(int)periodsMonthUD.Value);
                    dt = dt.AddDays(-(double)periodsDateUD.Value);
                }
                result2.Text = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
            }
        }

        #endregion

        #region unit conversion
        bool tocb = false, frcb = false;
        //
        // fromTB_TextChanged
        //
        private void fromTB_TextChanged(object sender, EventArgs e)
        {
            if (fromTB.ForeColor == SystemColors.GrayText)
            {
                fromTB.ForeColor = SystemColors.ControlText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Regular);
            }
            toTB.Text = getToTBText(fromTB.Text, fromTB.ForeColor);
        }
        //
        // from textbox lost focus
        //
        private void fromTB_LostFocus(object sender, EventArgs e)
        {
            if (fromTB.Text == "")
            {
                fromTB.TextChanged -= fromTB_TextChanged;
                fromTB.Text = "Enter value";
                fromTB.TextChanged += fromTB_TextChanged;
                fromTB.ForeColor = SystemColors.GrayText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Italic);
            }
            if (fromTB.ForeColor == SystemColors.GrayText) toTB.Text = "";
        }
        //
        // from textbox focused
        //
        private void fromTB_GotFocus(object sender, EventArgs e)
        {
            prcmdkey = false;
            if (fromTB.Text == "Enter value" && fromTB.ForeColor == SystemColors.GrayText)
            {
                fromTB.TextChanged -= fromTB_TextChanged;
                fromTB.Text = "";
                fromTB.TextChanged += fromTB_TextChanged;
                toTB.Text = "";
                fromTB.ForeColor = SystemColors.ControlText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Regular);
            }
        }
        //
        // fromLB_Click
        //
        private void fromLB_Click(object sender, EventArgs e)
        {
            if (fromTB.Focused)
            {
                if (fromTB.ForeColor != SystemColors.GrayText) fromTB.SelectAll();
            }
            else fromTB.Focus();
        }
        //
        // fromCB_SelectedIndexChanged
        //
        private void fromCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frcb)
            {
                int type = typeCB.SelectedIndex + (typeCB.SelectedIndex < 0).GetHashCode();
                if (typeCB.SelectedIndex < 0) typeCB.SelectedIndex = 0;
                invert_unit.Enabled = (fcb[type].SelectedIndex != tcb[type].SelectedIndex);
                toTB.Text = getToTBText(fromTB.Text, fromTB.ForeColor);
            }
        }
        //
        // toCB_SelectedIndexChanged
        //
        private void toCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tocb) fromCB_SelectedIndexChanged(sender, e);
        }
        //
        // typeCB_SelectedIndexChanged
        //
        private void typeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertiesChange = invert_unit.Enabled = true;
            tocb = frcb = false;
            assignDefaultIndex();
            tocb = frcb = true;
            fromCB_SelectedIndexChanged(sender, e);
        }
        /// <summary>
        /// khởi tạo vị trí mặc định của combox kết quả
        /// </summary>
        private void assignDefaultIndex()
        {
            for (int i = 0; i < typeCB.Items.Count; i++)
            {
                fcb[i].Visible = (typeCB.SelectedIndex == i);
                tcb[i].Visible = (typeCB.SelectedIndex == i);
                if (typeCB.SelectedIndex == i) fcb[i].SelectedIndex = 0;
            }
            // khởi tạo vị trí mặc định của combox kết quả
            tcb[00].SelectedIndex = 2;
            tcb[01].SelectedIndex = 6;
            tcb[02].SelectedIndex = 4;
            tcb[03].SelectedIndex = 9;
            tcb[04].SelectedIndex = 4;
            tcb[05].SelectedIndex = 4;
            tcb[06].SelectedIndex = 0;
            tcb[06].SelectedIndex = 1;
            tcb[07].SelectedIndex = 5;
            tcb[08].SelectedIndex = 5;
            tcb[09].SelectedIndex = 3;
            tcb[10].SelectedIndex = 6;
            tocb = frcb = true;
        }
        //
        // typeCB_MouseDown
        //
        private void typeCB_MouseDown(object sender, MouseEventArgs e)
        {
            prcmdkey = false;
            fromCB_SelectedIndexChanged(sender, e);
        }
        /// <summary>
        /// tính kết quả thu được từ textbox giá trị cần tính
        /// </summary>
        /// <param name="fromstr">giá trị của chuỗi nhập vào</param>
        /// <returns>giá trị dạng chuỗi của khung kết quả</returns>
        private string getToTBText(string fromstr, Color cl)
        {
            string ret_string = "";
            if (Misc.isNumber(fromstr))// && fromTB.Text.Trim() != "")
            {
                double db = double.Parse(fromstr);
                int type = typeCB.SelectedIndex;
                if (typeCB.SelectedIndex != 6)
                    db *= Misc.getRate(type, fcb[type].SelectedIndex, tcb[type].SelectedIndex);
                else
                    db = Misc.getTemperature(fcb[type].SelectedIndex, tcb[type].SelectedIndex, db);

                if (digitGroupingMI.Checked)
                    if (db.ToString().IndexOf("E") <= 0) ret_string = Misc.grouping(db);
                    else ret_string = db.ToString();
                else
                    ret_string = Misc.de_group(db.ToString());

                if (double.Parse(fromTB.Text) < 0 && typeCB.SelectedIndex != 6)
                    ret_string = "The input number must be a positive";
            }
            else if ((fromstr == "Enter value" && cl == SystemColors.GrayText) || fromstr == "") return "";
            else return "Invalid input number";
            //if (fromstr != "Enter value" && fromTB.ForeColor == SystemColors.GrayText)
            //{
            //    fromTB.ForeColor = SystemColors.ControlText;
            //    fromTB.Font = new Font("Tahoma", 9F);
            //}
            return ret_string;
        }
        //
        // nut doi gia tri 2 combobox
        //
        private void invert_unit_Click(object sender, EventArgs e)
        {
            //toTB.Text = getToTBText(fromTB.Text);
            int temp = fcb[typeCB.SelectedIndex].SelectedIndex;
            int type = typeCB.SelectedIndex;
            // tam thoi chan su kien selectindexchanged de ham nay khong duoc tu dong goi 2 lan
            fcb[type].SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            fcb[type].SelectedIndex = tcb[type].SelectedIndex;
            fcb[type].SelectedIndexChanged += fromCB_SelectedIndexChanged;
            tcb[type].SelectedIndex = temp;
            fromLB.Focus();
        }

        #endregion

        #region history controls

        private void historyMI_Click(object sender, EventArgs e)
        {
            formWithHistory(false);
        }

        private void clearHistoryMI_Click(object sender, EventArgs e)
        {
            hisDGV.CellStateChanged -= historyDGV_CellStateChanged;
            hisDGV.Rows.Clear();
            hisDGV.CellStateChanged += historyDGV_CellStateChanged; 
            historyOptionEnabled();
        }

        object oldValue;
        private void historyDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            hisDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            oldValue = hisDGV[0, e.RowIndex].Value;
            dgv_OnBeginEdit(sender, e);
            prcmdkey = false;
            rowIndex = e.RowIndex;
            //oldValue = hisDGV[0, e.RowIndex].Value;
        }
        /// <summary>
        /// đổi màu cell để dễ nhận biết cell nào đang được edit
        /// </summary>
        private void dgv_OnBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            IDataGridView dgv = (IDataGridView)sender;
            dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
            if (dgv.RowCount < 4)
            {
                dgv.BackgroundColor = SystemColors.ControlDark;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != row) dgv.Rows[i].DefaultCellStyle.BackColor = SystemColors.ControlDark;
                }
            }
            else
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != row) dgv.Rows[i].DefaultCellStyle.BackColor = SystemColors.ControlDark;
                }
            }
            historyOptionEnabled();
        }
        /// <summary>
        /// đổi trở về như cũ
        /// </summary>
        private void dgv_OnEndEdit(object sender)
        {
            IDataGridView dgv = (IDataGridView)sender;
            if (dgv.RowCount < 4)
            {
                dgv.BackgroundColor = Color.White;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            else
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            dgv.ReadOnly = true;

            #region thay doi kich thuoc cell de hien thi nhieu dong
            //string content = dgv[0, rowIndex].Value.ToString();
            //for (int i = 1; i < (int)(content.Length / 70) + 1; i++)
            //{
            //    content = content.Insert(70 * i, Environment.NewLine);
            //}
            ////dgv.Rows[rowIndex].Height = (int)(content.Length / 70 + 1) * 22;
            //dgv[0, rowIndex].Value = content;
            //hisDGV.AutoResizeRow(rowIndex);
            //dgv.Rows[rowIndex].Height = (int)(dgv.Rows[rowIndex].Height / 22 + 1) * 22;
            #endregion
        }
        /// <summary>
        /// thêm/bớt các dấu cách, dấu ngoặc, dấu * vào giữa các số và phép tính cho dễ nhìn
        /// ("2+3   *          4-        5" thành "2 + 3 * 4 - 5")
        /// </summary>
        /// <param name="resultBW">chuỗi biểu thức cần chuẩn hoá</param>
        /// <returns>chuỗi biểu thức đã được chuẩn hoá</returns>
        private string standardExpressionString(string Expression)
        {
            string result = Expression.Trim().ToLower().Replace(Misc.DecimalSym, ",");
            while (result.Contains("  ")) result = result.Replace("  ", " ");
            result = result.Replace(" ", "");
            result = result.Replace("yroot", " yroot ");     //√
            result = result.Replace("mod", " mod ");
            result = result.Replace("%", " % ");
            result = result.Replace(".", Misc.DecimalSym);
            result = result.Replace(",", Misc.DecimalSym);
            result = Misc.InsertBracket(result);
            result = Misc.InsertMultiplyChar(result);
            result = Misc.RemoveBracket(result);
            while (result.Contains("-+")) result = result.Replace("-+", "-");
            while (result.Contains("--")) result = result.Replace("--", "+");
            while (result.Contains("++")) result = result.Replace("++", "+");
            while (result.Contains("+-")) result = result.Replace("+-", "-");   // de cai nay o cuoi la tam yen tam
            result = result.Replace("+", " + ");
            result = result.Replace("-", " - ");
            result = result.Replace("*", " * ");
            result = result.Replace("/", " / ");
            result = result.Replace("^", " ^ ");
            result = result.Replace("e - ", "e-");
            result = result.Replace("e + ", "e+");
            result = result.Replace("( - ", "(-");
            result = result.Replace("( + ", "(");
            if (result.IndexOf(" - ") == 0) result = "-" + result.Substring(3);
            if (result.IndexOf(" + ") == 0) result = result.Substring(3);
            return result.Replace(",", Misc.DecimalSym);
        }

        private void historyDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (scientificMI.Checked)   // không hỗ trợ edit history cho standard calculator
            {
                hisDGV.ReadOnly = false;
                hisDGV.BeginEdit(false);
                historyOptionEnabled();
                prcmdkey = false;
            }
        }

        int rowIndex = 0;   //không có column vì column lúc nào chả = 0 ???

        private void historyDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            hisDGV.CellStateChanged -= historyDGV_CellStateChanged;
            hisDGV.MouseDown += EnableKeyboardAndChangeFocus;
            dgv_OnEndEdit(sender);
            recalculate(e.RowIndex);
            hisDGV.CellStateChanged += historyDGV_CellStateChanged;
        }

        private void historyDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hisDGV.CellStateChanged -= historyDGV_CellStateChanged;
            if (!hisDGV.IsCurrentCellInEditMode)
            {
                sci_exp = hisDGV[0, e.RowIndex].Value.ToString();
                evaluateExpression(e.RowIndex);
                historyOptionEnabled();
                rowIndex = e.RowIndex;
            }
            hisDGV.CellStateChanged += historyDGV_CellStateChanged;
        }

        private void historyDGV_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (rowIndex != e.Cell.RowIndex)
            {
                evaluateExpression(e.Cell.RowIndex);
                rowIndex = e.Cell.RowIndex;
            }
        }
        /// <summary>
        /// tìm kết quả của phép tính hiển thị trên history grid
        /// </summary>
        /// <param name="rowindex">dòng cần tính</param>
        private Exception evaluateExpression(int rowindex)
        {
            prcmdkey = hisDGV.ReadOnly;
            historyOptionEnabled();
            if (deg_rb.Checked) parser.Mode = Mode.DEG;
            if (rad_rb.Checked) parser.Mode = Mode.RAD;
            if (gra_rb.Checked) parser.Mode = Mode.GRA;
            string expression = "";
            try
            {
                expression = hisDGV[0, rowindex].Value.ToString();
                if (standardExpr.Checked)
                {
                    expression = standardExpressionString(expression);
                    hisDGV[0, rowindex].Value = expression;
                }
            }
            catch { expression = ""; }
            try
            {
                if (scientificMI.Checked) pex = parser.EvaluateSci(expression/*.Replace(".", ",")*/);
                if (standardMI.Checked) pex = parser.EvaluateStd(expression/*.Replace(".", ",")*/);
                if (pex != null) throw pex;
            }
            catch (Exception)
            {
                scr_lb.Text = str = pex.Message;
                scr_lb.Font = new Font("Consolas", 11.75F);
                disableAllFunction(true);
                return pex;
            }
            scr_lb.Font = fontChanged(scr_lb.Text.Length);
            isMathError = false;
            BigNumber result_Sci = parser.strResult;
            str = result_Sci.StringValue;
            if (string.IsNullOrEmpty(str)) str = "0";
            displayToScreen();
            return pex;
        }
        /// <summary>
        /// COMMENT HERE
        /// </summary>
        private void historyOptionEnabled()
        {
            bool his = historyMI.Checked && historyMI.Enabled;
            copyHistoryMI.Enabled = hisDGV.Rows.Count > 0 && his;
            editHistoryMI.Visible = prcmdkey;
            editHistoryMI.Enabled = prcmdkey && hisDGV.RowCount > 0 && his;
            reCalculateMI.Visible = !prcmdkey && his;
            cancelEditHisMI.Enabled = !prcmdkey && his;
            clearHistoryCTMN.Visible = historyMI.Checked;
            showHistoryCTMN.Visible = !historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
            hideHistoryCTMN.Visible = historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
            clearHistoryCTMN.Enabled = hisDGV.Rows.Count > 0;
            clearHistoryMI.Enabled = hisDGV.Rows.Count > 0 && his;
        }
        /// <summary>
        /// COMMENT HERE
        /// </summary>
        private void statisticsOptionEnabled()
        {
            copyDatasetMI.Enabled = staDGV.Rows.Count > 0;
            editDatasetMI.Visible = !staDGV.IsCurrentCellInEditMode;
            editDatasetMI.Enabled = !staDGV.IsCurrentCellInEditMode && staDGV.RowCount > 0;   // && staDGV.CurrentCell != null;
            commitDSMI.Visible = staDGV.IsCurrentCellInEditMode;
            cancelEditDSMI.Enabled = staDGV.IsCurrentCellInEditMode;
            clearDatasetMI.Enabled /*= clearDatasetCTMN.Enabled*/ = staDGV.Rows.Count > 0;
            //clearHistoryCTMN.Visible = showHistoryCTMN.Visible = hideHistoryCTMN.Visible = false;
        }
        //
        // nut up
        //
        private void upBT_Click(object sender, EventArgs e)
        {
            if (hisDGV.CurrentCell != null && (standardMI.Checked || scientificMI.Checked))
            {
                if (hisDGV.CurrentCell.RowIndex >= 1)
                {
                    rowIndex = hisDGV.CurrentRow.Index - 1;// rowIndex--;
                    hisDGV[0, hisDGV.CurrentRow.Index - 1].Selected = true;
                    evaluateExpression(rowIndex);
                }
            }
            if (staDGV.CurrentCell != null && statisticsMI.Checked)
            {
                if (staDGV.CurrentCell.RowIndex >= 1)
                {
                    staDGV[0, staDGV.CurrentRow.Index - 1].Selected = true;
                    rowIndex = staDGV.CurrentRow.Index;// rowIndex--;
                }
            }
        }
        //
        // nut down
        //
        private void dnBT_Click(object sender, EventArgs e)
        {
            if (hisDGV.CurrentCell != null && (standardMI.Checked || scientificMI.Checked))
            {
                if (hisDGV.CurrentCell.RowIndex <= hisDGV.Rows.Count - 2)
                {
                    hisDGV[0, hisDGV.CurrentCell.RowIndex + 1].Selected = true;
                    rowIndex = hisDGV.CurrentRow.Index;// rowIndex++;
                    evaluateExpression(rowIndex);
                }
            }
            if (staDGV.CurrentCell != null && statisticsMI.Checked)
            {
                if (staDGV.CurrentCell.RowIndex <= staDGV.RowCount - 2)
                {
                    staDGV[0, staDGV.CurrentCell.RowIndex + 1].Selected = true;
                    rowIndex = staDGV.CurrentRow.Index;// rowIndex++;
                }
            }
        }
        #endregion

        #region programmer mode
        /// <summary>
        /// éo biết gọi là gì, đặt tạm thế
        /// </summary>
        int SizeBin = 0;
        string binnum64 = "0", binnum = "0", decnum = "0", octnum = "0", hexnum = "0";
        /// <summary>
        /// dựa trên sự thay đổi của số trên panel nhị phân mà trả về kết quả hiển thị trên màn hình chính
        /// </summary>
        private void panelToScreen()
        {
            binnum64 = "";
            for (int i = 15; i >= 0; i--) binnum64 += bin_digit[i].Text;
            binnum = Binary.standardString(binnum64);
            decnum = Binary.other_to_dec(binnum, 02, SizeBin);
            octnum = Binary.dec_to_other(decnum, 08, SizeBin);
            hexnum = Binary.dec_to_other(decnum, 16, SizeBin);
            if (binRB.Checked) str = binnum;
            if (octRB.Checked) str = octnum;
            if (decRB.Checked) str = decnum;
            if (hexRB.Checked) str = hexnum;
            confirm_num = true;
            prcmdkey = true;
            displayToScreen();
        }
        /// <summary>
        /// sự kiện scr_lb text changed thông qua thao tác nhập số
        /// </summary>
        private void screenToPanel()
        {
            //if (confirm_num)
            {
                if (binRB.Checked)
                {
                    binnum = str;
                    decnum = Binary.other_to_dec(binnum, 02, SizeBin);
                    octnum = Binary.dec_to_other(decnum, 08, SizeBin);
                    hexnum = Binary.dec_to_other(decnum, 16, SizeBin);
                }
                if (octRB.Checked)
                {
                    octnum = str;
                    decnum = Binary.other_to_dec(octnum, 08, SizeBin);
                    binnum = Binary.dec_to_other(decnum, 02, SizeBin);
                    hexnum = Binary.dec_to_other(decnum, 16, SizeBin);
                }
                if (decRB.Checked)
                {
                    decnum = str;
                    BigNumber dec_Temp = decnum;
                    if (dec_Temp < 0) dec_Temp += BigNumber.Two.Pow(SizeBin);
                    binnum = Binary.dec_to_other(dec_Temp.StringValue, 02, SizeBin);
                    octnum = Binary.dec_to_other(dec_Temp.StringValue, 08, SizeBin);
                    hexnum = Binary.dec_to_other(dec_Temp.StringValue, 16, SizeBin);
                }
                if (hexRB.Checked)
                {
                    hexnum = str;
                    decnum = Binary.other_to_dec(hexnum, 16, SizeBin);
                    binnum = Binary.dec_to_other(decnum, 02, SizeBin);
                    octnum = Binary.dec_to_other(decnum, 08, SizeBin);
                }
            }
            //else    // chỉ convert từ hệ hiện tại sang decimal
            //{
            //    if (binRB.Checked) binnum = str;
            //    if (decRB.Checked) binnum = Binary.dec_to_other(str, 2, SizeBin);
            //    if (octRB.Checked)
            //    {
            //        binnum = Binary.other_to_dec(str, 8, SizeBin);      // 8 -> 10
            //        binnum = Binary.dec_to_other(binnum, 2, SizeBin);   // 10 -> 2
            //    }
            //    if (hexRB.Checked)
            //    {
            //        binnum = Binary.other_to_dec(str, 16, SizeBin);     // 16 -> 10
            //        binnum = Binary.dec_to_other(binnum, 2, SizeBin);   // 10 -> 2
            //    }
            //}
            // đưa lên panel
            binnum64 = binnum.PadLeft(64, '0');
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(64 - (i + 1) * 4, 4);
            }
            displayToScreen();
        }

        private void modproBT_Click(object sender, EventArgs e)
        {
            pro_operation(211);
        }
        /// <summary>
        /// sang hệ khác
        /// </summary>
        private void baseRB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked) { baseRBCheckedChanged(); panelToScreen(); }
            confirm_num = true;
        }

        private void bin_digit_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            int bitIndex = -1;
            if (mouseX >= 03 && mouseX <= 10) bitIndex = 0;
            if (mouseX >= 11 && mouseX <= 18) bitIndex = 1;
            if (mouseX >= 19 && mouseX <= 26) bitIndex = 2;
            if (mouseX >= 27 && mouseX <= 34) bitIndex = 3;

            if (bitIndex >= 0)
            {
                if (lb.Text[bitIndex] == '0')
                {
                    bin_digit[bd_tabindex].Text = bin_digit[bd_tabindex].Text.Remove(bitIndex, 1);
                    bin_digit[bd_tabindex].Text = bin_digit[bd_tabindex].Text.Insert(bitIndex, "1");
                    confirm_num = true;
                    panelToScreen();
                    return;
                }
                if (lb.Text[bitIndex] == '1')
                {
                    bin_digit[bd_tabindex].Text = bin_digit[bd_tabindex].Text.Remove(bitIndex, 1);
                    bin_digit[bd_tabindex].Text = bin_digit[bd_tabindex].Text.Insert(bitIndex, "0");
                }
            }
            confirm_num = true;
            panelToScreen();
        }

        int mouseX = 0, bd_tabindex = 0;
        private void bit_digit_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            bd_tabindex = ((Label)sender).TabIndex;
        }
        /// <summary>
        /// các nút từ A đến F
        /// </summary>
        private void buttonAF_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            buttonAF(btn.Text);
        }
        /// <summary>
        /// các radio button qword, dword...
        /// </summary>
        private void unknownGroupRB_CheckedChanged(object sender, EventArgs e)
        {
            //qword = 6
            //dword = 3
            //word  = 2
            //byte  = 1
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                int tabIndex = rb.TabIndex; // tabIndex của các RB này được đặt sao cho phù hợp với số nhị phân hiển thị trên panel
                int bd = (int)Binary.powan(2, tabIndex - 2 * (tabIndex > 3).GetHashCode());
                if (rb.Checked)
                {
                    for (int i = 0; i < bd; i++) bin_digit[i].Visible = true;
                    for (int i = bd; i < 16; i++)
                    {
                        bin_digit[i].Visible = false;
                        bin_digit[i].Text = "0000";
                    }
                    for (int i = 0; i < 6; i++) flagpoint[i].Visible = (i < tabIndex);
                }
                SizeBin = 4 * bd;
                panelToScreen();
            }
        }
        /// <summary>
        /// nút not
        /// </summary>
        private void notBT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                if (bin_digit[i].Visible)
                {
                    int notEachGroup = int.Parse(bin_digit[i].Text);
                    notEachGroup = 1111 - notEachGroup;
                    bin_digit[i].Text = notEachGroup.ToString().PadLeft(4, '0');
                }
                else break;
            }
            panelToScreen();
        }
        //
        // not RoL và RoR
        //
        private void rotateBT_Click(object sender, EventArgs e)
        {
            if (sender == RoLBT) binnum64 = binnum64.Substring(1, 63) + binnum64[0].ToString();
            if (sender == RoRBT) binnum64 = binnum64[63].ToString() + binnum64.Substring(0, 63);
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(60 - 4 * i, 4);
            }
            if (qwordRB.Checked) unknownGroupRB_CheckedChanged(qwordRB, null);
            if (dwordRB.Checked) unknownGroupRB_CheckedChanged(dwordRB, null);
            if (_wordRB.Checked) unknownGroupRB_CheckedChanged(_wordRB, null);
            if (_byteRB.Checked) unknownGroupRB_CheckedChanged(_byteRB, null);
        }
        /// <summary>
        /// hàm thực hiện các phép tính khoa học
        /// </summary>
        private void bitWiseOperators()
        {
            operator_lb.Visible = false;
            ulong resultBW = 0;
            switch (pre_oprt)
            {
                case 0:
                    num1pro = resultpro = str;
                    break;
                case 12:
                    str = (num1pro + str).StringValue;
                    num1pro = str;
                    break;
                case 13:
                    str = (num1pro - str).StringValue;
                    num1pro = str;
                    break;
                case 14:
                    str = (num1pro * str).StringValue;
                    num1pro = str;
                    break;
                case 15:
                    str = (num1pro / str).StringValue;
                    num1pro = str;
                    break;
                case 204:   // and
                    binnum = Binary.dec_to_other(num1pro.StringValue, 2, SizeBin);
                    if (qwordRB.Checked)
                    {
                        resultBW = ulong.Parse(num1pro.StringValue) & ulong.Parse(str);
                    }
                    if (dwordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) & int.Parse(str));
                    }
                    if (_wordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) & int.Parse(str));
                    }
                    if (_byteRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) & int.Parse(str));
                    }
                    str = resultBW.ToString();
                    break;
                case 199:   // or
                    binnum = Binary.dec_to_other(num1pro.StringValue, 2, SizeBin);
                    if (qwordRB.Checked)
                    {
                        resultBW = ulong.Parse(num1pro.StringValue) | ulong.Parse(str);
                    }
                    if (dwordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) | int.Parse(str));
                    }
                    if (_wordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) | int.Parse(str));
                    }
                    if (_byteRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) | int.Parse(str));
                    }
                    str = resultBW.ToString();
                    break;
                case 205:   // xor
                    binnum = Binary.dec_to_other(num1pro.StringValue, 2, SizeBin);
                    if (qwordRB.Checked)
                    {
                        resultBW = ulong.Parse(num1pro.StringValue) ^ ulong.Parse(str);
                    }
                    if (dwordRB.Checked)
                    {
                        resultBW = (ulong)(uint.Parse(num1pro.StringValue) ^ uint.Parse(str));
                    }
                    if (_wordRB.Checked)
                    {
                        resultBW = (ulong)(ushort.Parse(num1pro.StringValue) ^ ushort.Parse(str));
                    }
                    if (_byteRB.Checked)
                    {
                        resultBW = (ulong)(byte.Parse(num1pro.StringValue) ^ byte.Parse(str));
                    }
                    str = resultBW.ToString();
                    break;
                case 200:   // Lsh
                    binnum = Binary.dec_to_other(num1pro.StringValue, 2, SizeBin);
                    if (qwordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) << int.Parse(str));
                    }
                    if (dwordRB.Checked)
                    {
                        resultBW = (uint)(int.Parse(num1pro.StringValue) << int.Parse(str));
                    }
                    if (_wordRB.Checked)
                    {
                        resultBW = (ushort)(int.Parse(num1pro.StringValue) << int.Parse(str));
                    }
                    if (_byteRB.Checked)
                    {
                        resultBW = (byte)(int.Parse(num1pro.StringValue) << int.Parse(str));
                    }
                    str = resultBW.ToString();
                    break;
                case 210:   // Rsh
                    binnum = Binary.dec_to_other(num1pro.StringValue, 2, SizeBin);
                    if (qwordRB.Checked)
                    {
                        resultBW = (ulong)(int.Parse(num1pro.StringValue) >> int.Parse(str));
                    }
                    if (dwordRB.Checked)
                    {
                        resultBW = (uint)(int.Parse(num1pro.StringValue) >> int.Parse(str));
                    }
                    if (_wordRB.Checked)
                    {
                        resultBW = (ushort)(int.Parse(num1pro.StringValue) >> int.Parse(str));
                    }
                    if (_byteRB.Checked)
                    {
                        resultBW = (byte)(int.Parse(num1pro.StringValue) >> int.Parse(str));
                    }
                    str = resultBW.ToString();
                    break;
            }
            confirm_num = true;
            screenToPanel();
        }
        //
        // nut and, or, xor, Lsh & Rsh
        //
        private void bitOperatorsBT_Click(object sender, EventArgs e)
        {
            bitWiseOperators();
            pre_oprt = ((Button)sender).TabIndex;
        }
        #endregion

        #region statistics mode
        //
        // nút xoá hết (CAD)
        //
        private void CAD_Click(object sender, EventArgs e)
        {
            staDGV.Rows.Clear();
            countlb.Text = "Count = 0";
            contextMenuVisible();
            statisticsOptionEnabled();
        }
        //
        // nút add
        //
        private void AddstaBT_Click(object sender, EventArgs e)
        {
            staDGV.Rows.Add();
            staDGV[0, staDGV.RowCount - 1].Value = str;
            staDGV[0, staDGV.RowCount - 1].Selected = true;
            staDGV.CurrentCell = staDGV[0, staDGV.RowCount - 1];
            countlb.Text = string.Format("Count = {0}", staDGV.RowCount);
            contextMenuVisible();
            confirm_num = true;
            scr_lb.Text = str = "0";
            statisticsOptionEnabled();
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
                double number = double.Parse(staDGV[0, i].Value.ToString());
                if (isSquare) sum += number * number;
                else sum += number;
            }
            return sum;
        }
        //
        // sigma (n-1)
        //
        private void sigman_1BT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount <= 1) return;
            double result = sum(true) - sum(false) * sum(false) / staDGV.RowCount;
            result /= (staDGV.RowCount - 1);
            result = Math.Sqrt(result);
            str = result.ToString();
            displayToScreen();
        }
        //
        // sigma (n)
        //
        private void sigmanBT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            double result = sum(true) - sum(false) * sum(false) / staDGV.RowCount;
            result /= staDGV.RowCount;
            result = Math.Sqrt(result);
            str = result.ToString();
            displayToScreen();
        }
        //
        // sigma (x^2)
        //
        private void sigmax2BT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = sum(true).ToString();
            displayToScreen();
        }
        //
        // sigma (x)
        //
        private void sigmaxBT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = sum(false).ToString();
            displayToScreen();
        }
        //
        // avarage (x^2)
        //
        private void x2cross_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = (sum(true) / staDGV.RowCount).ToString();
            displayToScreen();
        }
        //
        // avarage (x)
        //
        private void xcross_Click(object sender, EventArgs e)
        {
            if (staDGV.Rows.Count == 0) return;
            str = (sum(false) / staDGV.RowCount).ToString();
            displayToScreen();
        }

        private void statisticsDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double.Parse(staDGV[0, e.RowIndex].Value.ToString());
                string vl = staDGV[0, e.RowIndex].Value.ToString();
                vl = vl.Replace(",", Misc.DecimalSym).Replace(".", Misc.DecimalSym);
                staDGV[0, e.RowIndex].Value = vl;
            }
            catch (FormatException)
            {
                staDGV[0, e.RowIndex].Value = "0";
            }
            prcmdkey = true;
            dgv_OnEndEdit(sender);
            statisticsOptionEnabled();
            //staDGV.MouseDown += EnableKeyboardAndChangeFocus;
        }

        private void statisticsDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            prcmdkey = false;
            staDGV.ReadOnly = false;
            staDGV.BeginEdit(false);
        }

        private void statisticsDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //staDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            dgv_OnBeginEdit(sender, e);
            prcmdkey = false;
        }
        #endregion
    }
}