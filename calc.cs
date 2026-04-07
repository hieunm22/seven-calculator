using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class calc : Form
    {
        public calc()
        {
            InitializeComponent();  // Required method for Designer support
            HiddenProperties();     // sự kiện focused cho các nút trên form ban đầu
        }

        #region bien toan cuc

        string str = "0", sci_exp = "", pro_exp = "";
        bool confirm_num = true, prcmdkey = true, propertiesChange, sciexpAdd;
        int pre_bt = -1, pre_oprt;
        object[] initConfigValue, currentConfig;
        string[] resultCollection = new string[100];
        BigNumber mem_num = 0;
        Parser parser = new Parser();
        Exception pex = null;
        Control infoControl;

        #endregion

        #region cac su kien lien quan den form

        /// <summary>
        /// form load
        /// </summary>
        private void calc_Load(object sender, EventArgs e)
        {
            // đoạn này để phòng trừ trường hợp người dùng chạy chương trình 
            // từ 1 shortcut mà shortcut này có thuộc tính run là Maximized
            WindowState = FormWindowState.Normal;   // không được xóa dòng này (lý do ở trên)
            LoadInfo();
            getPaste(null, null);
            // lúc load thì dù có bật extra function hay không thì vẫn bật tính năng bắt sự kiện phím của chương trình
            if (!basicMI.Checked) EnableKeyboardAndChangeFocus();
        }
        /// <summary>
        /// di chuyển form mà không cần đưa con trỏ lên title bar
        /// </summary>
        private void MoveFormWithoutMouseAtTitleBar(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Misc.ReleaseCapture();
                Misc.SendMessage(Handle, 0xA1, 0x2, 1);
            }
        }
        /// <summary>
        /// thay đổi kích thước font để hiển thị đủ kết quả lên màn hình
        /// </summary>
        private void scr_lb_TextChanged(object sender, EventArgs e)
        {
            scr_lb.Font = fontChanged(scr_lb.Text.Length);
        }

        #endregion

        #region Nhap so va cac phep tinh
        private void numberinput_Click(object sender, EventArgs e)
        {
            if (pex == null)
            {
                var btn = sender as Button;
                if (programmerMI.Checked) numinput_pro(btn);
                else numinput(btn);
            }
        }

        private void changesignBT_Click(object sender, EventArgs e)
        {
            if (programmerMI.Checked) numinput_pro(sender);
            else
            {
                if (confirm_num) math_func(changesignBT);
                else numinput(sender);
            }
        }

        private void operatorBT_Click(object sender, EventArgs e)
        {
            isFuncClicked = false;
            if (pex == null)
            {
                int tab = ((Button)sender).TabIndex;
                if (standardMI.Checked) std_operation(tab);
                if (scientificMI.Checked) sci_operation(tab);
                if (programmerMI.Checked) pro_operation(tab);
            }
        }

        private void expressionTB_TextChanged(object sender, EventArgs e)
        {
            //expressionTB.SelectionStart = expressionTB.Text.Length;
            int len = scientificMI.Checked ? 56 : 25;
            if (expressionTB.Text.Length > len)
            {
                string std = expressionTB.Text;
                expressionTB.AllowTextChanged = false;
                expressionTB.Text = "..." + std.Substring(std.Length - len);
                toolTip2.SetToolTip(expressionTB, Misc.StandardExpression(sci_exp));
                expressionTB.AllowTextChanged = true;
            }
            else
            {
                toolTip2.SetToolTip(expressionTB, "");
            }
        }
        //
        // nut xoa bo nho
        //
        private void memclear_Click(object sender, EventArgs e)
        {
            mem_num = 0;
            currentConfig[4] = mem_num.StrValue;
            mem_lb.Visible = false;
            propertiesChange = confirm_num = true;
        }

        private void recallMemory()
        {
            str = mem_num.StrValue;

            if (!programmerMI.Checked) 
                DisplayToScreen();
            else
                ScreenToPanel();

            prevFunc = str;
            sciexpAdd = false;
            pre_bt = 25;
            confirm_num = true;
        }
        //
        // nut MR
        //
        private void memrecall_Click(object sender, EventArgs e)
        {
            if (pex == null) recallMemory();
        }
        //
        // 3 nut M+, M-, MS
        //
        private void memprocess_Click(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;
            mem_process(text);
        }
        #endregion

        #region Xu ly so hien tren man hinh
        //
        // nut bang
        //
        private void equal_Click(object sender, EventArgs e)
        {
            if (pex == null)
            {
                confirm_num = true;
                if (standardMI.Checked)       // standard.checked
                {
                    #region standard operation
                    switch (pre_bt)
                    {
                        case 12: case 13: case 14: case 15:
                            prevFunc = str;
                            break;
                    }
                    if (!sciexpAdd/* || pre_oprt != 0*/) sci_exp += prevFunc;

                    parser.EvaluateStd(sci_exp);
                    // form standard nay tinh chinh xac den 15 chu so nen dung kieu double de hien thi cung duoc
                    str = parser.StringResult;
                    expressionTB.Text = "";

                    #region đưa lên grid
                    hisDGV.AllowCellStateChanged = false;
                    hisDGV.Rows.Add();
                    hisDGV.Rows[hisDGV.RowCount - 1].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
                    hisDGV[0, hisDGV.RowCount - 1].Value = Misc.StandardExpression(sci_exp);
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.RowCount - 1];
                    rowIndex = hisDGV.CurrentCell.RowIndex;
                    hisDGV.AllowCellStateChanged = true;
                    countLB.Text = string.Format("Index = {0} / {0}", hisDGV.RowCount);
                    #endregion

                    sci_exp = "";
                    isFuncClicked = sciexpAdd = false;
                    #endregion
                }
                if (scientificMI.Checked)     // scientific.checked
                {
                    #warning hàm dấu bằng của scientific
                    #region scientific operation
                    if (pre_oprt == 0) sci_exp = prevFunc;
                    else
                    //if (pre_oprt != 0/* && !isFuncClicked*/)
                    {
                        if (!sciexpAdd) sci_exp += prevFunc;
                        // tu dong ngoac neu thieu
                        sci_exp = sci_exp.PadRight(openBRKLevel + sci_exp.Length, ')');
                    }

                    if (slow)
                    {
                        NewBGW(false);
                    }
                    else
                    {
                        InitCancelOperation();
                        pex = parser.EvaluateSci(sci_exp);
                    }
                    if (pex == null) str = parser.StringResult;
                    else str = pex.Message;
                    expressionTB.Text = "";

                    #region write to history panel
                    hisDGV.AllowCellStateChanged = false;
                    hisDGV.Rows.Add();
                    try
                    {
                        resultCollection[hisDGV.RowCount - 1] = str;
                    }
                    catch
                    {
                        MessageBox.Show("There are too many operations in the panel. Clear the operations and try again",
                            "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    hisDGV.Rows[hisDGV.RowCount - 1].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
                    hisDGV.Rows[hisDGV.RowCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    //hisDGV[0, hisDGV.RowCount - 1].Value = StandardExpression(sci_exp);
                    hisDGV[0, hisDGV.RowCount - 1].Value = Misc.StandardExpression(sci_exp);
                    string content = hisDGV[0, hisDGV.RowCount - 1].Value.ToString();
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.RowCount - 1];
                    //hisDGV.AllowCellDoubleClick = true;
                    hisDGV.AllowCellStateChanged = true;
                    rowIndex = hisDGV.CurrentCell.RowIndex;
                    ChangeDGVHeight(hisDGV);
                    countLB.Text = string.Format("Index = {0}/{0}", hisDGV.RowCount);
                    #endregion

                    sci_exp = power_Exp = mul_div_Exp = "";
                    prevFunc = str;
                    bracketTime_lb.Text = "";
                    sciexpAdd = isFuncClicked = false;
                    slow = false;

                    #endregion
                }
                if (programmerMI.Checked)     // programmer.checked
                {
                    #region programmer operation
                    ScreenToPanel();
                    pro_exp += decRB.Value;
                    if (pre_oprt == 0) goto jump;
                    pex = parser.EvaluatePro(pro_exp, (int)currentConfig[11] == 1);
                    if (pex == null)
                    {
                        str = parser.StringResult;
                        ScreenToPanel();
                    }
                    pro_exp = "";
                    bracketTime_lb.Text = "";
                    #endregion
                }

                DisplayToScreen();
                openBRKLevel = 0;
                jump: pre_oprt = 0;
                priority = 0;
                pre_priority = 0;
                pre_bt = 16;
            }
        }
        //
        // nut ham chuc nang
        //
        private void functionBT_Click(object sender, EventArgs e)
        {
            if (pex == null)
            {
                var btn = sender as Button;
                math_func(btn);
            }
        }
        //
        // nut exp
        //
        private void exp_bt_Click(object sender, EventArgs e)
        {
            if (pex == null && str != "0" && !str.Contains("e") && !confirm_num)
            {
                str = str + "e+0";
                scr_lb.Text = str;
                pre_bt = 33;
            }
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
                scr_num = scr_num / 100 * parser.StringResult;
                str = prevFunc = scr_num.StrValue;
            }
            DisplayToScreen();
            pre_bt = 18;
        }

        private void pi_bt_Click(object sender, EventArgs e)
        {            
            if (pre_oprt == 0 && isFuncClicked)
            {
                equal_Click(null, null);
            }
            confirm_num = true;
            if (/*isFuncClicked && */sciexpAdd)
            {
                sci_exp = sci_exp.Substring(0, sci_exp.Length - prevFunc.Length);
            }
            if (!inv_ChkBox.Checked)
            {
                str = "31415926535897932384626433832795".Insert(1, Misc.DecimalSeparator);
                //prevFunc = "pi";
            }
            else
            {
                str = "6283185307179586476925286766559".Insert(1, Misc.DecimalSeparator);
                //prevFunc = "2 * pi";
            }
            prevFunc = str;
            DisplayToScreen();
            sciexpAdd = false;

            expressionTB.Text = Misc.StandardExpression(sci_exp);

            inv_ChkBox.Checked = false;
            pre_bt = 144;
        }
        //
        // nút F-E
        //
        private void fe_ChkBox_CheckChanged(object sender, EventArgs e)
        {
            if (pex != null) fe_ChkBox.Checked = false;
            else
            {
                if (fe_ChkBox.Checked)
                {
                    scr_lb.Text = ((BigNumber)str).ToString();
                }
                else
                {
                    if (digitGroupingMI.Checked)
                        scr_lb.Text = Misc.Group(str);
                    else scr_lb.Text = str;
                }
            }

            scr_lb.Focus();
        }

        //CheckBox chkb;
        private void inv_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            scr_lb.Focus();
            if (pex != null) inv_ChkBox.Checked = false;
            if (inv_ChkBox.Checked && pex == null)
            {
                sin_bt.Font = new Font(sin_bt.Font.FontFamily, 7.25F);
                sin_bt.Text = "asin";
                cos_bt.Font = new Font(cos_bt.Font.FontFamily, 7.5F);
                cos_bt.Text = "acos";
                tan_bt.Font = new Font(tan_bt.Font.FontFamily, 7.5F);
                tan_bt.Text = "atan";
                sinh_bt.Font = new Font(sinh_bt.Font.FontFamily, 6.75F);
                sinh_bt.Text = "asinh";
                cosh_bt.Font = new Font(cosh_bt.Font.FontFamily, 6F);
                cosh_bt.Text = "acosh";
                toolTip2.SetToolTip(cosh_bt, cosh_bt.Text); // chữ quá bé, dùng tootltip để hiện rõ hơn
                tanh_bt.Font = new Font(sin_bt.Font.FontFamily, 6F);
                tanh_bt.Text = "atanh";
                toolTip2.SetToolTip(tanh_bt, tanh_bt.Text); // chữ quá bé, dùng tootltip để hiện rõ hơn
                ln_bt.Font = new Font(ln_bt.Font.FontFamily, 8.25F);
                ln_bt.Text = "eⁿ";
                int_bt.Font = new Font(int_bt.Font.FontFamily, 7.5F);
                int_bt.Text = "Frac";
                dms_bt.Text = "deg";
                pi_bt.Font = new Font(pi_bt.Font.FontFamily, 9F);
                pi_bt.Text = "2*π";
            }
            if (!inv_ChkBox.Checked && pex == null)
            {
                sin_bt.Font = new Font(sin_bt.Font.FontFamily, 8.25F);
                sin_bt.Text = "sin";
                cos_bt.Font = new Font(cos_bt.Font.FontFamily, 8.25F);
                cos_bt.Text = "cos";
                tan_bt.Font = new Font(tan_bt.Font.FontFamily, 8.25F);
                tan_bt.Text = "tan";
                sinh_bt.Font = new Font(sinh_bt.Font.FontFamily, 7.25F);
                sinh_bt.Text = "sinh";
                cosh_bt.Font = new Font(cosh_bt.Font.FontFamily, 7.5F);
                cosh_bt.Text = "cosh";
                toolTip2.SetToolTip(cosh_bt, "");
                tanh_bt.Font = new Font(sin_bt.Font.FontFamily, 7.5F);
                tanh_bt.Text = "tanh";
                toolTip2.SetToolTip(tanh_bt, "");
                ln_bt.Font = new Font(ln_bt.Font.FontFamily, 8.25F);
                ln_bt.Text = "ln";
                int_bt.Font = new Font(int_bt.Font.FontFamily, 8.25F);
                int_bt.Text = "Int";
                dms_bt.Text = "dms";
                pi_bt.Font = new Font(pi_bt.Font.FontFamily, 12F);
                pi_bt.Text = "π";
            }
        }
        //
        // nut x^n va nut nvx (can bac n cua x)
        //
        private void PowerBT_Click(object sender, EventArgs e)
        {
            var bt = sender as Button;
            sci_operation(bt.TabIndex);
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
            clear_num(pex != null);
        }
        //
        // nut xoa ki tu so vua nhap
        //
        private void backspace_Click(object sender, EventArgs e)
        {
            switch (pre_bt)
            {
                case 28: case 29: case 30: case 31: case 32: case 35: case 36:
                case 37: case 38: case 39: case 40: case 42: case 43: case 44:
                    return;
            }

            if (!confirm_num)
            {
                if (str.Length > 1)
                {
                    if (str.Length == 2 && str.StartsWith("-"))
                    {
                        str = "0";
                        confirm_num = true;
                    }
                    else
                    {
                        str = str.Substring(0, str.Length - 1);
                        if (str.ToLower().EndsWith("e+")) str += "0";
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
            prevFunc = str;
            if (programmerMI.Checked) ScreenToPanel();
            else DisplayToScreen();
            pre_bt = 22;
        }

        int openBRKLevel;
        string[] bracketExp = new string[20];
        //
        // nut mo ngoac
        //
        private void open_bracket_Click(object sender, EventArgs e)
        {
            if (!scientificMI.Checked) return;
            if (pre_oprt == 0)
            {
                //switch (pre_bt)
                //{
                //    case 28: case 29: case 30: case 31: case 32: case 35: case 36:
                //    case 37: case 38: case 39: case 40: case 42: case 43: case 44:
                //        equal_Click(null, null);
                //        return;
                //}
                if (sciexpAdd) equal_Click(null, null);
            }
            if (pex == null && openBRKLevel < 20)
            {
                str = "0";
                DisplayToScreen();
                mul_div_Exp = "";
                power_Exp = "";
                if (openBRKLevel == 0)
                {
                    bracketExp = new string[20];
                }
                if (openBRKLevel < 20)
                {
                    openBRKLevel++;
                    /*if (openBRKLevel == closeBRK) bracketTime_lb.Text = string.Format("(=1");
                    else*/
                    bracketTime_lb.Text = string.Format("(={0}", openBRKLevel);
                    bracketExp[openBRKLevel - 1] = "";
                }

                if (sciexpAdd && isFuncClicked)
                    sci_exp = sci_exp.Substring(0, sci_exp.Length - prevFunc.Length) + "(";
                else sci_exp += "(";
                prevFunc = "0";
                pre_bt = 153;
                isFuncClicked = false;
                expressionTB.Text = Misc.StandardExpression(sci_exp);
            }
        }
        //
        // nut dong ngoac
        //
        private void close_bracket_Click(object sender, EventArgs e)
        {
            if (!scientificMI.Checked) return;
            if (pex == null && openBRKLevel > 0)
            {
                mul_div_Exp = "";
                power_Exp = "";
                openBRKLevel--;
                if (openBRKLevel > 0)
                {
                    bracketTime_lb.Text = string.Format("(={0}", openBRKLevel);
                }
                else
                {
                    bracketTime_lb.Text = "";
                    if (openBRKLevel < 0) { pre_bt = 152; return; }
                }
                //--------------------------------------------------------------
                if (pre_bt != 152)
                {
                    if (!sciexpAdd)
                    {
                        sci_exp += prevFunc + ")";
                    }
                    else
                    {
                        sci_exp += ")";
                    }
                    sciexpAdd = true;
                    bracketExp[openBRKLevel] += prevFunc;
                    //prevFunc = "(" + bracketExp[openBRKLevel] + ")";
                }
                else
                {
                    sci_exp += ")";
                    //prevFunc = "(" + bracketExp[openBRKLevel] + ")";
                }
                prevFunc = "(" + bracketExp[openBRKLevel] + ")"; // string.Format("({0})", bracketExp[openBRKLevel]); cung duoc
                if (openBRKLevel > 0)
                    bracketExp[openBRKLevel - 1] += prevFunc;
                pex = parser.EvaluateSci(bracketExp[openBRKLevel]);
                str = parser.StringResult;
                //prevFunc = bracketExp[openBRKLevel];

                expressionTB.Text = Misc.StandardExpression(sci_exp);
                confirm_num = true;
                pre_bt = 152;
                isFuncClicked = false;
                DisplayToScreen();
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

        #region Cac menu item
        private void standardMI_Click(object sender, EventArgs e)
        {
            if (!standardMI.Checked) { stdLoad(false); clear_num(true); }
        }

        private void scientificMI_Click(object sender, EventArgs e)
        {
            if (!scientificMI.Checked) { sciLoad(false); clear_num(true); }
        }

        private void programmerMI_Click(object sender, EventArgs e)
        {
            if (!programmerMI.Checked) { proLoad(false); clear_num(true); }
        }

        private void statisticsMI_Click(object sender, EventArgs e)
        {
            if (!statisticsMI.Checked) { staLoad(false); clear_num(true); }
        }

        private void digitGroupingMI_Click(object sender, EventArgs e)
        {
            digitLoad(false);
        }

        private void digitLoad(bool isLoaded)
        {
            digitGroupingMI.Checked = !digitGroupingMI.Checked;
            currentConfig[1] = digitGroupingMI.Checked ? 1 : 0;

            if (!isLoaded) propertiesChange = true;

            GroupWorksheetsFunctionResult(toTB, unitconvPN);
            GroupWorksheetsFunctionResult(workSheetResultTB, workSheetPN);

            if (!programmerMI.Checked)
            {
                if (Misc.IsNumber(str)) DisplayToScreen();
            }
            else
            {
                if (digitGroupingMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = Misc.Group(str, 3, Misc.GroupSeparator);
                    if (octRB.Checked) scr_lb.Text = Misc.Group(str, 3, " ");
                    if (binRB.Checked || hexRB.Checked) scr_lb.Text = Misc.Group(str, 4, " ");
                }
                else
                {
                    if (decRB.Checked) scr_lb.Text = scr_lb.Text.Replace(Misc.GroupSeparator, "");
                    if (octRB.Checked || binRB.Checked || hexRB.Checked)
                        scr_lb.Text = scr_lb.Text.Replace(" ", "");
                }
            }
        }
        /// <summary>
        /// nhóm kết quả tính được ở các textbox extra function
        /// </summary>
        /// <param name="tb">textbox kết quả</param>
        /// <param name="pn">panel chứa kết quả đó</param>
        private void GroupWorksheetsFunctionResult(TextBox tb, IPanel pn)
        {
            if (Misc.IsNumber(tb.Text) && pn.Visible)
            {
                if (digitGroupingMI.Checked)
                {
                    if (!tb.Text.Contains("E")) tb.Text = Misc.Group(tb.Text);
                }
                else tb.Text = tb.Text.Replace(Misc.GroupSeparator, "");
            }
        }

        private void basicMI_Click(object sender, EventArgs e)
        {
            int his = historyMI.Checked && historyMI.Enabled ? 104 : 0;
            if (!basicMI.Checked)
            {
                currentConfig[2] = 0;
                basicMI.Checked = true;
                unitConversionMI.Checked = false;
                dateCalculationMI.Checked = false;
                fe_MPG_MI.Checked = false;
                feL100_MI.Checked = false;
                mortgageMI.Checked = false;
                vehicleLeaseMI.Checked = false;

                datecalcPN.Visible = false;
                unitconvPN.Visible = false;
                workSheetPN.Visible = false;
                if (standardMI.Checked)
                {
                    if (historyMI.Checked) stdWithHistory();
                    else initializedForm(false);
                    FormSizeChanged(new Size(216, 310 + his), false);
                }
                if (scientificMI.Checked)
                {
                    if (historyMI.Checked) sciWithHistory();
                    else scientificLoad(false);
                    FormSizeChanged(new Size(413, 310 + his), false);
                }
                if (programmerMI.Checked)
                {
                    programmerMode();
                    FormSizeChanged(new Size(413, 374), false);
                }
                if (statisticsMI.Checked)
                {
                    statisticsMode();
                    FormSizeChanged(new Size(216, 414), false);
                }
                DisplayToScreen();
                scr_lb.Focus();
                prcmdkey = true;
                propertiesChange = true;
            }
        }

        private void extraFunctionMI_Click(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            if (!mi.Checked) exFunc(mi, false);
        }

        private void copyCTMN_Click(object sender, EventArgs e)
        {
            if (pex == null)
                try { Clipboard.SetText(str); }
                catch { }
        }

        private void mainContextMenu_Popup(object sender, EventArgs e)
        {
            if (hisDGV.Visible) hisDGV.Focus();
            if (staDGV.Visible) staDGV.Focus();
            //contextMenuOptions(statisticsMI.Checked);
            historyAndDatasetOptionMI_Popup(null, null);
        }

        private void historyAndDatasetOptionMI_Popup(object sender, EventArgs e)
        {
            if (statisticsMI.Checked)
            {
                copyDatasetMI.Enabled = staDGV.RowCount > 0;
                editDatasetMI.Visible = !staDGV.IsCurrentCellInEditMode;
                editDatasetMI.Enabled = !staDGV.IsCurrentCellInEditMode && staDGV.RowCount > 0;
                commitDSMI.Visible = cancelEditDSMI.Enabled = staDGV.IsCurrentCellInEditMode;
                clearDatasetMI.Enabled = clearDatasetCTMN.Enabled = staDGV.RowCount > 0;

                clearHistoryCTMN.Visible = showHistoryCTMN.Visible = hideHistoryCTMN.Visible = false;
            }
            else
            {
                bool his = historyMI.Checked && historyMI.Enabled;
                copyHistoryMI.Enabled = hisDGV.RowCount > 0 && his;
                editHistoryMI.Visible = !hisDGV.IsCurrentCellInEditMode;
                editHistoryMI.Enabled = !hisDGV.IsCurrentCellInEditMode && hisDGV.RowCount > 0 && !standardMI.Checked && his;
                reCalculateMI.Visible = cancelEditHisMI.Enabled = hisDGV.IsCurrentCellInEditMode;

                showHistoryCTMN.Visible = !historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
                hideHistoryCTMN.Visible = historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
                clearHistoryCTMN.Visible = his;
                clearHistoryCTMN.Enabled = clearHistoryMI.Enabled = hisDGV.RowCount > 0 && his;
            }

            clearDatasetCTMN.Visible = clearDatasetMI.Visible = statisticsMI.Checked;
            getPaste(null, null);
        }

        private void getPaste(object sender, EventArgs e)
        {
            pasteMI.Enabled = pasteCTMN.Enabled = Clipboard.ContainsText(TextDataFormat.Text);
        }

        private void pasteCTMN_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText().ToUpper();
            data = data.Trim().Replace(" ", "").Replace("\r", "").Replace("\n", "");
            if (data.Length > 200) data = data.Substring(0, 200);
            int len = data.Length;
            Keys[] keyData = new Keys[len];
            // tạo bừa thôi, hàm ProcessCmdKey có dùng cái này đâu
            Message msg = Message.Create(IntPtr.Zero, 1, IntPtr.Zero, IntPtr.Zero);
            for (int i = 0; i < len; i++)
            {
                keyData[i] = (Keys)(data[i]);
                switch ((int)data[i])
                {
                    case 10: case 61: keyData[i] = Keys.Enter;
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
                    case 44: case 46: keyData[i] = Keys.Decimal;
                        break;
                    case 45: if (i > 0)
                        {
                            if (data[i - 1] != 'X' && data[i - 1] != 'E') keyData[i] = Keys.OemMinus;
                            else if (hexRB.Checked || programmerMI.Checked) keyData[i] = Keys.OemMinus;
                            else keyData[i] = Keys.F9;
                        }
                        break;
                    case 47: keyData[i] = Keys.Divide;
                        break;
                    case 59: keyData[i] = Keys.OemSemicolon;
                        break;
                    case 64: keyData[i] = Keys.Shift | Keys.D2;
                        break;
                    case 66: keyData[i] = Keys.Control | Keys.B;
                        break;
                    case 71: keyData[i] = Keys.Control | Keys.G;
                        break;
                    case 94: 
                        if (scientificMI.Checked) keyData[i] = Keys.Y;
                        if (programmerMI.Checked) keyData[i] = (Keys)65590;
                        break;
                    case 124: keyData[i] = (Keys)65756;
                        break;
                }
                ProcessCmdKey(ref msg, keyData[i]);
            }
            if (programmerMI.Checked) ScreenToPanel();
            else DisplayToScreen();
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
                    clipboard += item.Cells[0].Value + Environment.NewLine;
                }
                Clipboard.SetText(clipboard);
            }
            catch (Exception ex) //{ /*làm gì giờ*/ }
            {
                Clipboard.SetText(ex.Message);
            }
        }

        private void editHistoryMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            hisDGV.ReadOnly = false;
            hisDGV.BeginEdit(false);
        }

        private void copyDatasetMI_Click(object sender, EventArgs e)
        {
            // DO SOMETHING HERE
            string clipboard = "";
            try
            {
                foreach (DataGridViewRow item in staDGV.Rows)
                {
                    clipboard += item.Cells[0].Value + "\n";
                }
                Clipboard.SetText(clipboard);
            }
            catch (Exception ex) //{ /*làm gì giờ*/ }
            {
                Clipboard.SetText(ex.Message);
            }
        }

        private void editDatasetMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            staDGV.ReadOnly = false;
            staDGV.BeginEdit(false);
        }

        private void cancelEditDSMI_Click(object sender, EventArgs e)
        {
            //staDGV.CellEndEdit -= staDGV_CellEndEdit;
            staDGV.CancelEdit();
            staDGV.ReadOnly = true;
            staDGV[0, rowIndex].Selected = true;
            prcmdkey = true;
            dgv_OnEndEdit(staDGV);
            //staDGV.CellEndEdit += staDGV_CellEndEdit;
        }

        private void commitDSMI_Click(object sender, EventArgs e)
        {
            prcmdkey = true;
            staDGV.EndEdit();
            staDGV.ReadOnly = true;
        }

        private void aboutMI_Click(object sender, EventArgs e)
        {
            About ab = new About();
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

        private void preferrencesMI_Click(object sender, EventArgs e)
        {
            Preferences pre = new Preferences((int)currentConfig[8],
                (int)currentConfig[9] == 1, (int)currentConfig[10] == 1, (int)currentConfig[11] == 1);
            pre.DoCheck += new Preferences.ICheckChanged(pre_DoCheck);
            pre.ShowDialog();
        }

        private void pre_DoCheck(int spd, bool sign, bool fast, bool animate)
        {
            currentConfig[8] = spd;
            currentConfig[9] = animate ? 1 : 0;
            currentConfig[10] = fast ? 1 : 0;
            currentConfig[11] = sign ? 1 : 0;

            if (!currentConfig.Equals(initConfigValue)) propertiesChange = true;
            
            if (programmerMI.Checked) PanelToScreen(true);
        }
        #endregion

        #region date calculation
        private void DateCalculationControlVisible(int selectIndex)
        {
            bool visible = selectIndex == 0;
            addrb.Visible = subrb.Visible = yearAddSubLB.Visible = monthAddSubLB.Visible = dayAddSubLB.Visible = periodsDateUD.Visible = periodsMonthUD.Visible = periodsYearUD.Visible = selectIndex != 0;
            secondDate.Visible = dtP2.Visible = dateDifferenceLB.Visible = tbResult1.Visible = selectIndex == 0;
        }
        //
        // su kien datetimepicker thay doi
        //
        private void dtP_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(null, null);
            else
            { tbResult1.Text = ""; tbResult2.Text = ""; }
        }
        //
        // nut calculate
        //
        private void calculate_bt_Click(object sender, EventArgs e)
        {
            //if (prcmdkey) return;
            if (datemethodCB.SelectedIndex == 0)
            {
                int[] differ = Misc.differencesTimes(dtP2.Value, dtP1.Value);
                int diff = Misc.differenceBW2Dates(dtP1.Value, dtP2.Value);

                if (diff == 0)
                    tbResult1.Text = tbResult2.Text = "Same date";
                else if (diff == 1)
                    tbResult2.Text = "1 day";
                else tbResult2.Text = Misc.Group(diff) + " days";

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
                tbResult1.Text = text;
                #endregion

                if (tbResult1.Text == "") tbResult1.Text = tbResult2.Text;
            }
            else
            {
                DateTime dt = dtP1.Value;
                int sign = addrb.Checked ? 1 : -1;
                dt = dt.AddYears((int)periodsYearUD.Value * sign);
                dt = dt.AddMonths((int)periodsMonthUD.Value * sign);
                dt = dt.AddDays((double)periodsDateUD.Value * sign);
                tbResult2.Text = dt.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
            }
            if (sender is Button)
            {
                tbResult1.Focus();
                tbResult1.SelectionStart = 0;
                tbResult1.SelectionLength = 0;
            }
        }
        //
        // datemethodCB_SelectedIndexChanged
        //
        private void datemethodCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            currentConfig[7] = index;
            calMethod_SIC(false, index);
        }

        private void calMethod_SIC(bool isLoad, int selectIndex)
        {
            if (isLoad) autocal_date.Checked = ((int)initConfigValue[6] == 1);
            else propertiesChange = true;

            DateCalculationControlVisible(selectIndex);
            firstDate.Text = "From";
            if (datemethodCB.SelectedIndex == 0)
            {
                secondDate.Text = "To";
                addSubResultLB.Text = "Difference (days)";
            }
            if (datemethodCB.SelectedIndex == 1)
            {
                //secondDate.Text = "Periods:";
                addSubResultLB.Text = "Date:";
            }

            tbResult2.Text = "";
            tbResult1.Text = "";
            dtP_ValueChanged(null, null);
        }

        private void periods_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(null, null);
        }

        private void autocal_date_CheckedChanged(object sender, EventArgs e)
        {
            currentConfig[6] = autocal_date.Checked ? 1 : 0;
            if (tbResult1.Text == "") dtP_ValueChanged(null, null);
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
                tbResult2.Text = dt.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
            }
        }

        #endregion

        #region unit conversion
        //
        // fromTB_TextChanged
        //
        private void fromTB_TextChanged(object sender, EventArgs e)
        {
            if (fromTB.ForeColor == SystemColors.GrayText)
            {
                fromTB.ForeColor = SystemColors.ControlText;
                fromTB.Font = new Font(fromTB.Font, FontStyle.Regular);
            }
            toTB.Text = GetToTBText(fromTB.Text, fromTB.ForeColor);
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
                fromTB.Font = new Font(fromTB.Font, FontStyle.Italic);
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
                fromTB.Font = new Font(fromTB.Font, FontStyle.Regular);
            }
        }
        //
        // fromCB_SelectedIndexChanged
        //
        private void fromCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeUnitCB.SelectedIndex < 0) typeUnitCB.SelectedIndex = 0;
            invert_unit.Enabled = (fromCB.SelectedIndex != toCombobox.SelectedIndex);
            toTB.Text = GetToTBText(fromTB.Text, fromTB.ForeColor);
        }
        //
        // typeUnitCB_SelectedIndexChanged
        //
        private void typeUnitCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (typeFECB.SelectedIndex < 0) MessageBox.Show("Lỗi âm");
            currentConfig[5] = typeUnitCB.SelectedIndex;
            propertiesChange = invert_unit.Enabled = true;
            toCombobox.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            assignDefaultIndex(typeUnitCB.SelectedIndex);
            toCombobox.SelectedIndexChanged += fromCB_SelectedIndexChanged;
            fromCB_SelectedIndexChanged(null, null);
        }
        /// <summary>
        /// khởi tạo vị trí mặc định của combox kết quả
        /// </summary>
        private void assignDefaultIndex(int selectindex)
        {
            fromCB.Items.Clear();
            fromCB.Items.AddRange(unitTypeItemMember[selectindex]);
            fromCB.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            fromCB.SelectedIndex = 0;
            toCombobox.Items.Clear();
            toCombobox.Items.AddRange(unitTypeItemMember[selectindex]);
            switch (selectindex)
            {
                case 0: toCombobox.SelectedIndex = 2;
                    break;
                case 3: toCombobox.SelectedIndex = 9;
                    break;
                case 6: toCombobox.SelectedIndex = 1;
                    break;
                case 9: toCombobox.SelectedIndex = 3;
                    break;
                case 1: case 10: toCombobox.SelectedIndex = 6;
                    break;
                case 7: case 8: toCombobox.SelectedIndex = 5;
                    break;
                case 2: case 4: case 5: toCombobox.SelectedIndex = 4;
                    break;
            }
            fromCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;
        }
        //
        // typeCB_MouseDown
        //
        private void typeCB_MouseDown(object sender, MouseEventArgs e)
        {
            prcmdkey = false;
            //fromCB_SelectedIndexChanged(null, null);
        }
        /// <summary>
        /// tính kết quả thu được từ textbox giá trị cần tính
        /// </summary>
        /// <param name="fromstr">giá trị của chuỗi nhập vào</param>
        /// <param name="cl">màu của textbox</param>
        /// <returns>giá trị dạng chuỗi của khung kết quả</returns>
        private string GetToTBText(string fromstr, Color cl)
        {
            if (Misc.IsNumber(fromstr))// && fromTB.Text.Trim() != "")
            {
                double db = double.Parse(fromstr);
                int type = typeUnitCB.SelectedIndex;
                if (typeUnitCB.SelectedIndex != 6)
                    db *= Misc.getRate(type, fromCB.SelectedIndex, toCombobox.SelectedIndex);
                else
                    db = Misc.getTemperature(fromCB.SelectedIndex, toCombobox.SelectedIndex, db);

                if (double.Parse(fromTB.Text) < 0 && typeUnitCB.SelectedIndex != 6)
                    return "The input number must be a positive";
                if (digitGroupingMI.Checked)
                {
                    if (db.ToString().IndexOf("E") <= 0) return Misc.Group(db);
                    else return db.ToString().ToLower();
                }
                else
                    return db.ToString().Replace(Misc.GroupSeparator, "").ToLower();
            }
            else if ((fromstr == "Enter value" && cl == SystemColors.GrayText) || fromstr == "") return "";
            else return "Invalid input number";
        }
        //
        // nut doi gia tri 2 combobox
        //
        private void invert_unit_Click(object sender, EventArgs e)
        {
            //toTB.Text = GetToTBText(fromTB.Text);
            int temp = fromCB.SelectedIndex;
            // tam thoi chan su kien selectindexchanged de ham nay khong duoc tu dong goi 2 lan
            fromCB.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            fromCB.SelectedIndex = toCombobox.SelectedIndex;
            fromCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;
            toCombobox.SelectedIndex = temp;
            fromLB.Focus();
        }

        #endregion
        
        #region history controls

        private void hotkeyMI_Click(object sender, EventArgs e)
        {
            Info tt = new Info(infoControl);
            tt.Form_Closed += new Info.CustomHandler(tt_Form_Closed);
            tt.Show();
            // location la diem goc trai tren cua control so voi man hinh desktop
            Point p = new Point(location.X + (infoControl.Size.Width - tt.Size.Width) / 2, location.Y + infoControl.Size.Height);

            // man hinh lam viec (chi desktop, khong tinh taskbar va cac thu linh tinh khac)
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            if (p.X + tt.Size.Width >= screen.Width)    // vuot qua chieu rong ben phai
            {
                if (p.Y + tt.Size.Height >= screen.Height) p = new Point(location.X - tt.Size.Width, location.Y - tt.Size.Height - 10);
                p = new Point(location.X - tt.Size.Width, p.Y);
            }
            else if (p.Y + tt.Size.Height >= screen.Height)  // vuot qua chieu cao ben duoi
            {
                if (p.X + tt.Size.Width < screen.Width && p.X > 0) p = new Point(p.X, location.Y - tt.Size.Height - 10);
                if (p.X <= 0) p = new Point(location.X + infoControl.Size.Width + 5, location.Y - tt.Size.Height - 10);
            }
            else if (p.X <= 0)
            {
                p = new Point(location.X + infoControl.Size.Width + 5, p.Y);
            }

            tt.Location = p;
        }

        private void tt_Form_Closed(Keys keys)
        {
            Message msg = Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
            ProcessCmdKey(ref msg, keys);
        }

        private void angelPN_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            int tab = rb.TabIndex - 114;
            if (rb.Checked) parser.Mode = (Mode)tab;
        }

        private void historyMI_Click(object sender, EventArgs e)
        {
            formWithHistory(false);
        }

        private void clearHistoryMI_Click(object sender, EventArgs e)
        {
            hisDGV.AllowCellStateChanged = false;
            hisDGV.Rows.Clear();
            resultCollection = new string[100];
            hisDGV.AllowCellStateChanged = true;
            countLB.Text = "";
        }

        private void historyDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            hisDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            pex = null;
            prcmdkey = false;   // cai nay phai cho len truoc
            dgv_OnBeginEdit(sender);
            rowIndex = e.RowIndex;
            hisDGV.AllowCellDoubleClick = false;
            hisDGV.AllowCellClick = false;
            //oldValue = hisDGV[0, e.RowIndex].Value;
        }
        /// <summary>
        /// đổi màu cell để dễ nhận biết cell nào đang được edit
        /// </summary>
        private void dgv_OnBeginEdit(object sender)
        {
            //int row = e.RowIndex;
            inv_ChkBox.Checked = false;
            var dgv = sender as IDataGridView;
            dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            screenPN.BackColor = BackColor;
            if (dgv.RowCount < 4)
            {
                dgv.BackgroundColor = BackColor;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != rowIndex) dgv.Rows[i].DefaultCellStyle.BackColor = BackColor;
                }
            }
            else
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != rowIndex) dgv.Rows[i].DefaultCellStyle.BackColor = BackColor;
                }
            }
        }
        /// <summary>
        /// đổi trở về như cũ
        /// </summary>
        private void dgv_OnEndEdit(object sender)
        {
            inv_ChkBox.Checked = false;
            var dgv = sender as IDataGridView;
            try
            {
                dgv[0, rowIndex].Value = Misc.StandardExpression(dgv[0, rowIndex].Value.ToString());
            }
            catch { dgv[0, rowIndex].Value = "0"; }
            screenPN.BackColor = Color.White;
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

            ChangeDGVHeight(dgv);
        }
        /// <summary>
        /// thay doi kich thuoc cell de hien thi nhieu dong
        /// </summary>
        private void ChangeDGVHeight(IDataGridView dgv)
        {
            dgv.Rows[rowIndex].Height = (dgv[0, rowIndex].Value.ToString().Length / 51 + 1) * 20;
        }

        private void dGV_DoubleClick(object sender, EventArgs e)
        {
            var dgv = sender as IDataGridView;
            // không hỗ trợ edit history cho standard calculator
            if (!standardMI.Checked && dgv.CurrentCell != null && dgv.AllowCellDoubleClick)
            {
                dgv.ReadOnly = false;
                dgv.BeginEdit(false);
                prcmdkey = false;
            }
        }

        int rowIndex;   //không có column vì column lúc nào chả = 0 ???

        private void historyDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            hisDGV.AllowCellStateChanged = false;
            hisDGV.MouseDown += EnableKeyboardAndChangeFocus;
            dgv_OnEndEdit(sender);
            recalculate(e.RowIndex);
            hisDGV.AllowCellStateChanged = true;
            hisDGV.AllowCellDoubleClick = true;
            hisDGV.AllowCellClick = true;
            //configPath = Environment.GetFolderPath
        }

        private void historyDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hisDGV.AllowCellStateChanged = false;
            if (!hisDGV.IsCurrentCellInEditMode)
            {
                //expressionTB.Text = sci_exp = "";
                evaluateExpression(e.RowIndex, false);
                prevFunc = str;
                rowIndex = e.RowIndex;
                pre_bt = 16;
            }
            hisDGV.AllowCellStateChanged = true;
        }

        private void historyDGV_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (rowIndex != e.Cell.RowIndex && hisDGV.AllowCellStateChanged)
            {
                evaluateExpression(e.Cell.RowIndex, false);
                rowIndex = e.Cell.RowIndex;
            }
        }
        /// <summary>
        /// tìm kết quả của phép tính hiển thị trên history grid
        /// </summary>
        /// <param name="rowindex">dòng cần tính</param>
        /// <param name="isRecalculate">được tính lại hay đọc kết quả trong bộ nhớ</param>
        private void evaluateExpression(int rowindex, bool isRecalculate)
        {
            try
            {
                countLB.Text = string.Format("Index = {0} / {1}", rowindex + 1, hisDGV.RowCount);
                string expression = hisDGV[0, rowindex].Value.ToString();
                int matches = Misc.NumberOfOpenWOClose(expression);
                if (matches > 0)
                {
                    expression = expression.PadRight(matches + expression.Length, ')');
                    hisDGV[0, rowindex].Value = expression;
                }
                if (isRecalculate)
                {
                    if (standardMI.Checked) pex = parser.EvaluateStd(expression);
                    if (scientificMI.Checked)
                    {
                        // neu bieu thuc co chua ham giai thua thi moi dung backgroundwoker
                        if (expression.ToLower().Contains("fact"))
                        {
                            var reg = new System.Text.RegularExpressions.Regex(@"(fact)\(([^\(\)]+)\)(\^|!?)");
                            var m = reg.Match(expression);
                            parser.EvaluateSci(m.Groups[2].Value);
                            if (parser.BigNumberResult >= 1e5)
                                NewBGW(false);
                            else
                            {
                                //InitCancelOperation();
                                pex = parser.EvaluateSci(expression);
                            }
                        }
                        else pex = parser.EvaluateSci(expression);
                    }
                    if (pex != null)
                    {
                        scr_lb.Text = str = pex.Message;
                        scr_lb.Font = new Font("Consolas", pex.Message.Length > 40 ? 5.25F : 9.75F);
                    }
                    else
                    {
                        hisDGV.ReadOnly = prcmdkey = true;
                        isFuncClicked = false;
                        sci_exp = "";

                        prevFunc = parser.StringResult;
                        str = parser.StringResult;
                    }
                    resultCollection[rowindex] = str;
                    if (string.IsNullOrEmpty(str)) str = "0";
                }
                else
                {
                    if (scientificMI.Checked) str = resultCollection[rowindex].Trim();
                    if (standardMI.Checked) str = double.Parse(resultCollection[rowindex]).ToString();
                    if (BigNumber.IsNumber(str))
                        pex = null;
                    else
                        pex = new Exception(str);
                }
                confirm_num = true;
                DisplayToScreen();
            }
            catch { return; }
            //return pex;
        }
        //
        // nut up/down
        //
        private void directionBT_Click(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            ProcessUpOrDown(e.Delta / Math.Abs(e.Delta));
        }

        private void directionBT_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            ProcessUpOrDown(btn.TabIndex - 2);
        }
        #endregion

        #region programmer mode
        /// <summary>
        /// éo biết gọi là gì, đặt tạm thế
        /// </summary>
        int SizeBin = 64;
        string binnum64 = "0".PadLeft(64, '0');
        /// <summary>
        /// dựa trên sự thay đổi của số trên panel nhị phân mà trả về kết quả hiển thị trên màn hình chính
        /// </summary>
        /// <param name="bin64Updated">biến kiểm tra xem biến binnum64 đã được cập nhật chưa để đỡ mất công cập nhật lại lần nữa</param>
        private void PanelToScreen(bool bin64Updated)
        {
            if (!bin64Updated)
            {
                binnum64 = "";
                for (int i = 15; i >= 0; i--)
                {
                    binnum64 += bin_digit[i].Text;
                    //if (bin_digit[index].Visible) binnum64 += bin_digit[index].Text;
                    //else binnum64 += "0000";
                }
            }
            binRB.Value = Binary.standardString(binnum64);
            decRB.Value = Binary.other_to_dec(binRB.Value, 02, SizeBin, (int)currentConfig[11] == 1);
            octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, (int)currentConfig[11] == 1);
            hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, (int)currentConfig[11] == 1);
            if (binRB.Checked) str = binRB.Value;
            if (octRB.Checked) str = octRB.Value;
            if (decRB.Checked) str = decRB.Value;
            if (hexRB.Checked) str = hexRB.Value;
            confirm_num = true;
            prcmdkey = true;
            DisplayToScreen();
        }
        /// <summary>
        /// sự kiện scr_lb text changed thông qua thao tác nhập số
        /// </summary>
        private void ScreenToPanel()
        {
            try
            {
                if (binRB.Checked)
                {
                    binRB.Value = str;
                    decRB.Value = Binary.other_to_dec(binRB.Value, 02, SizeBin, (int)currentConfig[11] == 1);
                    octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, (int)currentConfig[11] == 1);
                    hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, (int)currentConfig[11] == 1);
                }
                if (octRB.Checked)
                {
                    octRB.Value = str;
                    decRB.Value = Binary.other_to_dec(octRB.Value, 08, SizeBin, (int)currentConfig[11] == 1);
                    binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, (int)currentConfig[11] == 1);
                    hexRB.Value = Binary.dec_to_other(decRB.Value, 16, SizeBin, (int)currentConfig[11] == 1);
                }
                if (decRB.Checked)
                {
                    BigNumber dec_Temp = str;
                    //if (dec_Temp < 0) dec_Temp += BigNumber.Two.Pow(SizeBin - 1);
                    binRB.Value = Binary.dec_to_other(dec_Temp.StrValue, 02, SizeBin, (int)currentConfig[11] == 1);
                    octRB.Value = Binary.dec_to_other(dec_Temp.StrValue, 08, SizeBin, (int)currentConfig[11] == 1);
                    decRB.Value = Binary.other_to_dec(binRB.Value, 02, SizeBin, (int)currentConfig[11] == 1);
                    hexRB.Value = Binary.dec_to_other(dec_Temp.StrValue, 16, SizeBin, (int)currentConfig[11] == 1);
                }
                if (hexRB.Checked)
                {
                    hexRB.Value = str;
                    decRB.Value = Binary.other_to_dec(hexRB.Value, 16, SizeBin, (int)currentConfig[11] == 1);
                    binRB.Value = Binary.dec_to_other(decRB.Value, 02, SizeBin, (int)currentConfig[11] == 1);
                    octRB.Value = Binary.dec_to_other(decRB.Value, 08, SizeBin, (int)currentConfig[11] == 1);
                }
                // đưa lên panel
                binnum64 = binRB.Value.PadLeft(64, '0');
                for (int i = 0; i < 16; i++)
                {
                    bin_digit[i].Text = binnum64.Substring(60 - i * 4, 4);
                }
            }
            catch
            {
                pex = new Exception("Overflow");
                str = pex.Message;
            }
            finally { DisplayToScreen(); }

        }
        /// <summary>
        /// sang hệ khác
        /// </summary>
        private void baseRB_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as IRadioButton;
            if (rb.Checked)
            {
                baseRBCheckedChanged();
                str = rb.Value;
                DisplayToScreen();
            }
            confirm_num = true;
        }

        private void bin_digit_MouseDown(object sender, MouseEventArgs e)
        {
            var lb = sender as ILabel;
            if (e.Button != MouseButtons.Left || e.X > 31) return;
            if (e.X >= 4)
            {
                int bitIndex = (e.X - 4) / 7;
                switch (lb.Text[bitIndex])
                {
                    case '0':
                        lb.Text = lb.Text.Remove(bitIndex, 1);  // khong dua ra ngoai duoc
                        lb.Text = lb.Text.Insert(bitIndex, "1");
                        break;
                    case '1':
                        lb.Text = lb.Text.Remove(bitIndex, 1);  // khong dua ra ngoai duoc
                        lb.Text = lb.Text.Insert(bitIndex, "0");
                        break;
                }
            }
            confirm_num = true;
            EnableKeyboardAndChangeFocus();
            PanelToScreen(false);
        }
        /// <summary>
        /// các nút từ A đến F
        /// </summary>
        private void buttonAF_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            buttonAF(btn.Text);
        }
        /// <summary>
        /// các radio control qword, dword...
        /// </summary>
        private void architectureRB_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb.Checked)
            {
                int bd = rb.TabIndex;
                for (int i = 0; i < 16; i++)
                {
                    bin_digit[i].Visible = i < bd;
                    if (i >= bd) bin_digit[i].Text = "0000";
                }

                for (int i = 0; i < 6; i++)
                    flagpoint[i].Visible = (i < bd / 2 - bd / 8);   // khong duoc rut gon bieu thuc nay

                if (SizeBin < 4 * bd)
                {
                    for (int i = bd - 1; i >= 0; i--)
                    {
                        if (decRB.Value.Contains("-"))
                        {
                            if (bin_digit[i].Text == "0000") bin_digit[i].Text = "1111";
                            else break;
                        }
                        else break;
                    }
                }
                SizeBin = 4 * bd;

                PanelToScreen(e == null);
            }
        }
        /// <summary>
        /// nút not
        /// </summary>
        private void notBT_Click(object sender, EventArgs e)
        {
            int notGroup = 0;
            for (int i = 0; i < 16; i++)
            {
                if (bin_digit[i].Visible)
                {
                    notGroup = int.Parse(bin_digit[i].Text);
                    notGroup = 1111 - notGroup;
                    bin_digit[i].Text = notGroup.ToString().PadLeft(4, '0');
                }
                else break;
            }
            PanelToScreen(false);
        }
        //
        // not RoL và RoR
        //
        private void rotateBT_Click(object sender, EventArgs e)
        {
            if (sender == RoLBT) binnum64 = binnum64.Substring(1) + binnum64[0].ToString();
            if (sender == RoRBT) binnum64 = binnum64[63].ToString() + binnum64.Substring(0, 63);
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(60 - 4 * i, 4);
            }
            if (qwordRB.Checked) architectureRB_CheckedChanged(qwordRB, null);
            if (dwordRB.Checked) architectureRB_CheckedChanged(dwordRB, null);
            if (_wordRB.Checked) architectureRB_CheckedChanged(_wordRB, null);
            if (_byteRB.Checked) architectureRB_CheckedChanged(_byteRB, null);
        }
        #endregion

        #region statistics mode
        //
        // nút xoá hết (CAD)
        //
        private void CAD_Click(object sender, EventArgs e)
        {
            staDGV.Rows.Clear();
            countLB.Text = "Count = 0";
        }
        //
        // nút add
        //
        private void AddstaBT_Click(object sender, EventArgs e)
        {
            staDGV.Rows.Add();
            staDGV.Rows[staDGV.RowCount - 1].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
            staDGV[0, staDGV.RowCount - 1].Value = str;
            staDGV[0, staDGV.RowCount - 1].Selected = true;
            staDGV.CurrentCell = staDGV[0, staDGV.RowCount - 1];
            countLB.Text = string.Format("Count = {0}", staDGV.RowCount);
            confirm_num = true;
            scr_lb.Text = str = "0";
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
            if (staDGV.RowCount <= 1)
            {
                scr_lb.TextChanged -= scr_lb_TextChanged;
                str = "Enter data to calculate";
                pex = new Exception(str);
                DisplayToScreen();
                scr_lb.TextChanged += scr_lb_TextChanged;
                scr_lb.Font = new Font(scr_lb.Font.FontFamily, 8f);
                return;
            }
            double resultSigmaN = sum(true) - sum(false) * sum(false) / staDGV.RowCount;
            resultSigmaN /= (staDGV.RowCount - 1);
            resultSigmaN = Math.Sqrt(resultSigmaN);
            str = result.ToString();
            DisplayToScreen();
        }
        //
        // sigma (n)
        //
        private void sigmanBT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0)
            {
                scr_lb.TextChanged -= scr_lb_TextChanged;
                str = "Enter data to calculate";
                pex = new Exception(str);
                DisplayToScreen();
                scr_lb.TextChanged += scr_lb_TextChanged;
                scr_lb.Font = new Font(scr_lb.Font.FontFamily, 9.75f);
                return;
            }
            double resultSigmaN = sum(true) - sum(false) * sum(false) / staDGV.RowCount;
            resultSigmaN /= staDGV.RowCount;
            resultSigmaN = Math.Sqrt(resultSigmaN);
            str = resultSigmaN.ToString();
            DisplayToScreen();
        }
        //
        // sigma (x²)
        //
        private void sigmax2BT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = sum(true).ToString();
            DisplayToScreen();
        }
        //
        // sigma (x)
        //
        private void sigmaxBT_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = sum(false).ToString();
            DisplayToScreen();
        }
        //
        // avarage (x²)
        //
        private void x2cross_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = (sum(true) / staDGV.RowCount).ToString();
            DisplayToScreen();
        }
        //
        // avarage (x)
        //
        private void xcross_Click(object sender, EventArgs e)
        {
            if (staDGV.RowCount == 0) return;
            str = (sum(false) / staDGV.RowCount).ToString();
            DisplayToScreen();
        }

        private void staDGV_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                rowIndex = e.Cell.RowIndex;
            }
            //else return;
        }

        private void staDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            staDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            prcmdkey = false;
            rowIndex = e.RowIndex;
            dgv_OnBeginEdit(sender);
            //statisticsOptionEnabled();
        }

        private void staDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                staDGV.MouseDown += EnableKeyboardAndChangeFocus;
                string vl = staDGV[0, e.RowIndex].Value.ToString();
                double.Parse(vl);
                vl = vl.Replace(",", Misc.DecimalSeparator).Replace(".", Misc.DecimalSeparator);
                staDGV[0, e.RowIndex].Value = vl;
                dgv_OnEndEdit(sender);
            }
            catch (FormatException)
            {
                staDGV[0, e.RowIndex].Value = "0";
            }
            //staDGV.MouseDown += EnableKeyboardAndChangeFocus;
            prcmdkey = true;
        }
        #endregion

        #region sub extra functions

        private void wsCalculateMethod(TextField[] tf, int wsType)
        {
            //if (prcmdkey) return;
            double[] db = new double[tf.Length - 1];
            for (int i = 0; i < tf.Length - 1; i++)
            {
                try
                {
                    if (tf[order[i]].TextBoxText == "" || tf[order[i]].ForeColor == SystemColors.GrayText)
                        throw new Exception();
                    toolTip1.Hide(tf[order[i]]);
                    db[i] = double.Parse(tf[order[i]].TextBoxText);
                    //tf[order[i]].TextBoxText = db[i].ToString();
                }
                catch (FormatException)
                {
                    toolTip1.Hide(tf[order[i]]);
                    toolTip1.Show("  This value is not valid  ", tf[order[i]], 155, -40, 3000);
                    tf[order[i]].TBFocus();
                    return;
                }
                catch (Exception)
                {
                    toolTip1.Hide(tf[order[i]]);
                    toolTip1.Show("This value can not be blank", tf[order[i]], 155, -40, 3000);
                    tf[order[i]].TBFocus();
                    return;
                }
            }

            foreach (var lbl in tf)
            {
                if (lbl.LabelText.EndsWith("*")) lbl.LabelText = lbl.LabelText.Substring(0, lbl.LabelText.Length - 2);
            }

            double result_WS = 0, smth = 0;
            if (wsType == 0)
            {
                #region mortgage calculation
                /* 
                 * with pp is the purchase price
                 *      dp is the down payment
                 *      t is term (in years) -> tm is the number of month in this term = t * 12
                 *      r is interest rate (in percent %) -> rm is interest rate of a month = r / 100 / 12
                 *      mp is monthly payment
                 * we have the formula:
                 *      mp = (pp - dp) * (1 + rm) ^ tm * rm / ((1 + rm) ^ tm - 1)
                 * so we can calculate the other varius from the formula above
                 * 
                 * */
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    case 0:
                        smth = Math.Pow(1 + db[2] / 1200, db[1] * 12);
                        result_WS = db[3] * (smth - 1);
                        result_WS /= (smth * db[2] / 1200);
                        result_WS = db[0] - result_WS;
                        break;
                    case 1:
                        smth = Math.Pow(1 + db[3] / 1200, db[2] * 12);
                        result_WS = (db[0] - db[1]) * smth * (db[3] / 1200);
                        result_WS /= (smth - 1);
                        break;
                    case 2:
                        smth = Math.Pow(1 + db[2] / 1200, db[1] * 12);
                        result_WS = db[3] * (smth - 1);
                        result_WS /= (smth * db[2] / 1200);
                        result_WS += db[0];
                        break;
                    case 3:
                        smth = db[3] / (db[3] - (db[0] - db[1]) * db[2] / 1200);
                        result_WS = Math.Log(smth) / Math.Log(1 + db[2] / 1200) / 12;
                        break;
                }
                mortgageTF[order[4]].AllowTextChanged = false;
                mortgageTF[order[4]].TBFont = new Font(mortgageTF[order[4]].Font, FontStyle.Regular);
                mortgageTF[order[4]].TBForeColor = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = mortgageTF[order[4]].TextBoxText = Misc.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = mortgageTF[order[4]].TextBoxText = result_WS.ToString();
                mortgageTF[order[4]].AllowTextChanged = true;
                #endregion
            }
            if (wsType == 1)
            {
                #region vehicle lease calculation
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    default: // chua co cong thuc tinh
                        break;
                }
                #endregion
            }
            if (wsType == 2)
            {
                #region fuel economy mpg calculation
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    case 0:
                        result_WS = db[0] * db[1];
                        break;
                    case 1: case 2:
                        result_WS = db[0] / db[1];
                        break;
                }
                fe_MPGTF[order[3]].AllowTextChanged = false;
                fe_MPGTF[order[3]].TBFont = new Font(fe_MPGTF[order[4]].Font, FontStyle.Regular);
                fe_MPGTF[order[3]].TBForeColor = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = fe_MPGTF[order[3]].TextBoxText = Misc.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = fe_MPGTF[order[3]].TextBoxText = result_WS.ToString();
                fe_MPGTF[order[3]].AllowTextChanged = true;
                #endregion
            }
            if (wsType == 3)
            {
                #region fuel economy l/100 calculation
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    case 0:
                        result_WS = db[0] / db[1] * 100;
                        break;
                    case 1:
                        result_WS = db[1] / db[0] * 100;
                        break;
                    case 2:
                        result_WS = db[0] * db[1] / 100;
                        break;
                }
                feL100TF[order[3]].AllowTextChanged = false;
                feL100TF[order[3]].TBFont = new Font(feL100TF[order[4]].Font, FontStyle.Regular);
                feL100TF[order[3]].TBForeColor = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = feL100TF[order[3]].TextBoxText = Misc.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = feL100TF[order[3]].TextBoxText = result_WS.ToString();
                feL100TF[order[3]].AllowTextChanged = true;
                #endregion
            }

            if (double.IsInfinity(result_WS))
            {
                toolTip1.Hide(workSheetResultTB);
                toolTip1.Show("Unable to calculate", workSheetResultTB, 155, -40, 3000);
                workSheetResultTB.Text = "";
                return;
            }
            workSheetResultTB.Focus();
        }

        int[] order = new int[] { };
        private void typeWorkSheetCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            workSheetCalculateBT.Enabled = !vehicleLeaseMI.Checked;
            if (mortgageMI.Checked)
            {
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    #region switch case
                    case 0:
                        order = new int[] { 0, 1, 2, 3, 4 };
                        break;
                    case 1:
                        order = new int[] { 0, 4, 1, 2, 3 };
                        break;
                    case 2:
                        order = new int[] { 4, 1, 2, 3, 0 };
                        break;
                    case 3:
                        order = new int[] { 0, 4, 2, 3, 1 };
                        break;
                    #endregion
                }
                orderControlLocation(mortgageTF);
            }
            if (vehicleLeaseMI.Checked)
            {
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    #region switch case
                    case 0:
                        order = new int[] { 0, 1, 2, 3, 4, 5 };
                        break;
                    case 1:
                        order = new int[] { 5, 1, 2, 3, 4, 0 };
                        break;
                    case 2:
                        order = new int[] { 0, 5, 1, 2, 3, 4 };
                        break;
                    case 3:
                        order = new int[] { 0, 5, 1, 3, 4, 2 };
                        break;
                    #endregion
                }
                orderControlLocation(VhTF);
            }
            if (fe_MPG_MI.Checked || feL100_MI.Checked)
            {
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    #region switch case
                    case 0:
                        order = new int[] { 0, 2, 1 };
                        break;
                    case 1:
                        order = new int[] { 1, 0, 2 };
                        break;
                    case 2:
                        order = new int[] { 1, 2, 0 };
                        break;
                    #endregion
                }
                if (fe_MPG_MI.Checked) orderControlLocation(fe_MPGTF);
                if (feL100_MI.Checked) orderControlLocation(feL100TF);
            }
        }

        private void orderControlLocation(TextField[] itf)
        {
            for (int i = 0; i < itf.Length; i++)
            {
                itf[order[i]].Location = new Point(9, 58 + 24 * i);
                itf[order[i]].TabIndex = 10 + i;
                itf[order[i]].Visible = i != itf.Length - 1;
            }
            if (Misc.IsNumber(itf[order[itf.Length - 1]].TextBoxText))
            {
                workSheetResultTB.Text = itf[order[itf.Length - 1]].TextBoxText;
            }
        }

        private void workSheetCalculateBT_Click(object sender, EventArgs e)
        {
            if (mortgageMI.Checked) wsCalculateMethod(mortgageTF, 0);
            //if (vehicleLeaseMI.Checked) mortgageBT_Click(VhTF, 1);    // uncomment khi nao co cong thuc
            if (fe_MPG_MI.Checked) wsCalculateMethod(fe_MPGTF, 2);
            if (feL100_MI.Checked) wsCalculateMethod(feL100TF, 3);
        }

        private void resultTextBox_GotFocus(object sender, EventArgs e)
        {
            DisableKeyboard(sender, e);
            var tb = sender as TextBox;
            tb.SelectionStart = 0;
            tb.SelectionLength = 0;
        }
        #endregion

        #region cac su kien keo theo
        private void screenPN_SizeChanged(object sender, EventArgs e)
        {
            scr_lb.Size = new Size(scientificMI.Checked || programmerMI.Checked ? 370 : 175, 45);
            expressionTB.Size = new Size(scr_lb.Size.Width - 5, 13);
        }

        private void unitconvGB_SizeChanged(object sender, EventArgs e)
        {
            workSheetPN.Size = datecalcPN.Size = unitconvPN.Size;
        }

        private void unitconvGB_LocationChanged(object sender, EventArgs e)
        {
            workSheetPN.Location = datecalcPN.Location = unitconvPN.Location;
        }

        private void screenPN_BackColorChanged(object sender, EventArgs e)
        {
            gridPanel.BackColor = hisDGV.BackgroundColor = staDGV.BackgroundColor = expressionTB.BackColor = gridPanel.BackColor = screenPN.BackColor;
        }
        #endregion
    }
}