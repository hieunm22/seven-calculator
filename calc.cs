using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class calc : Form
    {
        public calc()
        {
            InitializeComponent();  // Required method for Designer support
            HiddenProperties();     // properties ẩn cho các control trên form chính
        }

        #region bien toan cuc
        StringBuilder screenStr = new StringBuilder("0");
        string curOperand = "0";
        StringBuilder mainExp = new StringBuilder();
        bool confirmNumber = true, prcmdkey = true, expAdded = false, propertiesChange = false;
        int pre_bt = -1;
        object[] initConfig, currentConfig;
        HistoryModel[] historyObj = new HistoryModel[100];
        MathParserService parser = new MathParserService();
        Exception pex = null;
        Control infoControl;

        #endregion

        #region cac su kien lien quan den form
        /// <summary>
        /// form load
        /// </summary>
        private void calc_Load(object sender, EventArgs e)
        {
            /*
             * đoạn này để phòng trừ trường hợp người dùng chạy chương trình
             * từ 1 shortcut mà shortcut này có thuộc tính run là Maximized
             * */
            WindowState = FormWindowState.Normal;   // không được xóa dòng này (lý do ở trên)
            LoadInfo();
            AdministratorCheck();
            if ((int)initConfig[Config._11_ReadDictionary] == 1)
                RegistryService.ReadDictionary(ref factDict);
            if ((int)initConfig[Config._12_StoreHistory] == 1)
            {
                historyObj = RegistryService.ReadHistory();
                if ((int)initConfig[Config._00_CalculatorType] == 1)
                {
                    DisplayHistoryOnLoad(historyObj);
                }
            }
            getPaste(null, null);
            // lúc load thì dù có bật extra function hay không thì vẫn bật tính năng bắt sự kiện phím của chương trình
            if (!basicMI.Checked)
                EnableKeyboardAndChangeFocus();
        }
        /// <summary>
        /// hiển thị lịch sử các phép tính onload
        /// </summary>
        private void DisplayHistoryOnLoad(HistoryModel[] ho)
        {
            for (int i = 0; i < ho.Length; i++)
            {
                if (ho[i] == null) break;
                hisDGV.Rows.Add();
                hisDGV[0, i].Value = MathService.StandardExpression(ho[i].Expression);
                ChangeDGVHeight(hisDGV);
            }
            if (hisDGV.RowCount > 0)
            {
                hisDGV.CurrentCell = hisDGV[0, 0];
                //hisDGV_SetStatusLabel();
                countLB.Text = string.Format("Value = 1 / {0}", hisDGV.RowCount);
            }
            //screenStr = historyObj[0];
            //DisplayToScreen();
        }
        /// <summary>
        /// kiểm tra chương trình có được chạy dưới quyền admin: không cho phép sửa cấu hình khi chưa chạy dưới quyền admin
        /// </summary>
        private void AdministratorCheck()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);

            sepMI6.Visible = preferencesMI.Enabled = preferencesMI.Visible = isAdministrator;
        }
        /// <summary>
        /// thay đổi kích thước font để hiển thị đủ kết quả lên màn hình
        /// </summary>
        private void scr_lb_TextChanged(object sender, EventArgs e)
        {
            scr_lb.Font = fontChanged(scr_lb.Text.Length);
        }
        /// <summary>
        /// gọi các property ẩn của các control
        /// </summary>
        private void HiddenProperties()
        {
            // set context menu to all buttons that are children of main form
            Controls.OfType<Button>().ToList().ForEach(fe => fe.ContextMenu = helpCTMN);
            //Controls.OfType<Button>().All(a => { a.ContextMenu = helpCTMN; return true; });

            inv_ChkBox.ContextMenu = helpCTMN;
            fe_ChkBox.ContextMenu = helpCTMN;
            bracketTime_lb.ContextMenu = helpCTMN;

            screenPN.ContextMenu = gridPanel.ContextMenu = mainContextMenu;

            // những menuitem có hotkey "độc", không thể khai báo trên designer
            copyMI.Text = "&Copy\tCtrl+C";
            pasteMI.Text = "&Paste\tCtrl+V";
            cancelEditHisMI.Text = "Ca&ncel edit\tEsc";
            reCalculateMI.Text = "Re&calculate\t=";

            cancelEditDSMI.Text = "Ca&ncel edit\tEsc";
            commitDSMI.Text = "C&ommit\t=";

            clearDatasetMI.Text = "C&lear\tD";

            NewPriorityExpression();

            //parser.Transfer += new MathParserService.SendValue(parser_Transfer);
            parser.UpdateDictEvent += new MathParserService.UpdateDictionary(parser_UpdateDictEvent);
            hisDGV.MouseWheel += new MouseEventHandler(directionBT_Click);
            staDGV.MouseWheel += new MouseEventHandler(directionBT_Click);
        }

        #endregion

        #region Nhap so va cac phep tinh
        private void numberinput_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (pex == null && btn.Enabled)
            {
                if (programmerMI.Checked) numinput_pro(btn);
                else numinput(btn);
            }
        }

        private void changesignBT_Click(object sender, EventArgs e)
        {
            if (programmerMI.Checked) numinput_pro(sender);
            else
            {
                if (confirmNumber) MathFunction(changesignBT);
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
                expressionTB.Text = "« " + std.Substring(std.Length - len); // alt 174
                toolTip2.SetToolTip(expressionTB, mainExp.ToString());
                expressionTB.AllowTextChanged = true;
            }
            else
            {
                toolTip2.SetToolTip(expressionTB, "");
            }
        }
      
        BigNumber mem_num = 0;
        //
        // nut xoa bo nho
        //
        private void memclearBT_Click(object sender, EventArgs e)
        {
            mem_num = 0;
            currentConfig[Config._04_MemoryNumber] = mem_num.StrValue;
            mem_lb.Visible = false;
            propertiesChange = confirmNumber = true;
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
                if (standardMI.Checked)       // standard.checked
                {
                    #region standard operation
                    if (Array.IndexOf(new int[] { 12, 13, 14, 15 }, pre_bt) >= 0) curOperand = screenStr.ToString();
                    if (!expAdded) mainExp = mainExp.Append(curOperand);

                    switch (curPriority[0].PText)
                    {
                        case "+": screenStr = new StringBuilder((leftNum + (BigNumber)screenStr.ToString()).StrValue);
                            break;
                        case "-": screenStr = new StringBuilder((leftNum - (BigNumber)screenStr.ToString()).StrValue);
                            break;
                        case "*": screenStr = new StringBuilder((leftNum * (BigNumber)screenStr.ToString()).StrValue);
                            break;
                        case "/": if (screenStr != new StringBuilder("0"))
                            {
                                screenStr = new StringBuilder((leftNum / (BigNumber)screenStr.ToString()).Round(15).StrValue);
                            }
                            else
                            {
                                pex = new DivideByZeroException("Cannot divided by zero");
                            }
                            break;
                    }

                    expressionTB.Text = "";

                    #region đưa lên grid
                    hisDGV.AllowCellStateChanged = false;
                    hisDGV.Rows.Add();
                    try
                    {
                        historyObj[hisDGV.RowCount - 1] = new HistoryModel();
                        historyObj[hisDGV.RowCount - 1].Expression = mainExp.ToString();
                        historyObj[hisDGV.RowCount - 1].Result = screenStr.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("There are too many operations in the panel. Clear the operations and try again",
                            "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    hisDGV[0, hisDGV.RowCount - 1].Value = mainExp.ToString().Trim();
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.RowCount - 1];
                    hisDGV.AllowCellStateChanged = true;
                    ChangeDGVHeight(hisDGV);
                    //hisDGV_SetStatusLabel();
                    countLB.Text = string.Format("Value = {0} / {0}", hisDGV.RowCount);
                    #endregion

                    mainExp = new StringBuilder();
                    isFuncClicked = false;
                    confirmNumber = true;
                    #endregion
                }
                if (scientificMI.Checked)     // scientific.checked
                {
                    #region scientific operation
                    // neu la phep tinh dau bieu thuc
                    if (curPriority[openBrkLevel].Index < 0)
                    {
                        if (openBrkLevel == 0) mainExp = new StringBuilder(curOperand);
                    }
                    else
                    {
                        if (!expAdded) mainExp = mainExp.Append(curOperand);
                    }

                    // tu dong ngoac neu thieu
                    mainExp = new StringBuilder(mainExp.ToString().PadRight(openBrkLevel + mainExp.Length, ')'));

                    pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()), true);

                    if (pex == null) screenStr = new StringBuilder(parser.StringResult);
                    else screenStr = new StringBuilder(pex.Message);
                    expressionTB.Text = "";

                    #region write to history panel
                    hisDGV.AllowCellStateChanged = false;
                    hisDGV.Rows.Add();
                    try
                    {
                        historyObj[hisDGV.RowCount - 1] = new HistoryModel();
                        historyObj[hisDGV.RowCount - 1].Expression = mainExp.ToString();
                        historyObj[hisDGV.RowCount - 1].Result = screenStr.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("There are too many operations in the panel. Clear the operations and try again",
                            "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    hisDGV[0, hisDGV.RowCount - 1].Value = mainExp.ToString().Trim();
                    hisDGV.CurrentCell = hisDGV[0, hisDGV.RowCount - 1];
                    //hisDGV.AllowCellDoubleClick = true;
                    hisDGV.AllowCellStateChanged = true;
                    ChangeDGVHeight(hisDGV);
                    //hisDGV_SetStatusLabel();
                    countLB.Text = string.Format("Value = {0} / {0}", hisDGV.RowCount);
                    #endregion

                    mainExp = new StringBuilder();
                    curOperand = screenStr.ToString();
                    bracketTime_lb.Text = "";
                    isFuncClicked = false;
                    confirmNumber = true;

                    #endregion
                }
                if (programmerMI.Checked)     // programmer.checked
                {
                    #region programmer operation
                    ScreenToPanel();
                    if (!expAdded) mainExp = mainExp.Append(curOperand);
                    if (curPriority[openBrkLevel].Index < 0) goto jump;
                    //if (prePriority[openBrkLevel].Value < 0) goto jump;
                    pex = parser.EvaluatePro(mainExp.ToString(), (int)currentConfig[Config._10_SignInteger] == 1);
                    if (pex == null)
                    {
                        screenStr = new StringBuilder(parser.StringResult);
                        ScreenToPanel();
                    }
                    mainExp = new StringBuilder();
                    curOperand = decRB.Value;
                    bracketTime_lb.Text = "";
                    confirmNumber = true;
                    #endregion
                }

                expAdded = false;
                DisplayToScreen();
                openBrkLevel = 0;
                leftNum = 0;
                jump: bracketExp = new string[Constants.MAX_BRACKET_LEVEL + 1];
                NewPriorityExpression();
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
                MathFunction(btn);
            }
        }
        //
        // nut exp
        //
        private void exp_bt_Click(object sender, EventArgs e)
        {
            if (pex == null && screenStr != new StringBuilder("0") && !screenStr.ToString().Contains("e") && !confirmNumber)
            {
                screenStr = screenStr.Append("e+0");
                scr_lb.Text = screenStr.ToString();
                pre_bt = 33;
            }
        }
        //
        // nut %
        //
        private void percent_bt_Click(object sender, EventArgs e)
        {
            confirmNumber = true;
            if (curPriority[openBrkLevel].Index >= 0)
            {
                curOperand = (leftNum * double.Parse(scr_lb.Text) / 100).ToString();
                screenStr = new StringBuilder(curOperand);
            }
            DisplayToScreen();
            pre_bt = 18;
        }
        //
        // nut pi
        //
        private void pi_bt_Click(object sender, EventArgs e)
        {
            if (prePriority[openBrkLevel].Index < 0 && curPriority[openBrkLevel].Index < 0 && isFuncClicked)
            {
                equal_Click(equalBT, null);
            }
            confirmNumber = true;
            if (/*isFuncClicked && */expAdded)
            {
                mainExp = new StringBuilder(mainExp.ToString().Substring(0, mainExp.Length - curOperand.Length));
            }
            if (!inv_ChkBox.Checked)
            {
                screenStr = new StringBuilder("31415926535897932384626433832795").Insert(1, MathService.DecimalSeparator);
                //curOperand = "pi";
            }
            else
            {
                screenStr = new StringBuilder("6283185307179586476925286766559").Insert(1, MathService.DecimalSeparator);
                //curOperand = "2 * pi";
            }
            curOperand = screenStr.ToString();
            DisplayToScreen();
            expAdded = false;

            expressionTB.Text = mainExp.ToString();

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
                    scr_lb.Text = ((BigNumber)screenStr.ToString()).ToString();
                }
                else
                {
                    if (digitGroupingMI.Checked)
                        scr_lb.Text = MathService.Group(screenStr.ToString());
                    else scr_lb.Text = screenStr.ToString();
                }
            }

            scr_lb.Focus();
        }

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
                btFactorial.Text = Config.FAST_FACTORIAL_TEXT;
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
                btFactorial.Text = "n!";
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
        private void backspacebt_Click(object sender, EventArgs e)
        {
            if (isFuncClicked) return;

            if (!confirmNumber)
            {
                if ((screenStr.Length == 2 && (screenStr.StartsWith("-") || screenStr.ToString().EndsWith(MathService.DecimalSeparator))) || screenStr.Length == 1)
                {
                    screenStr = new StringBuilder("0");
                    confirmNumber = true;
                }
                if ((screenStr.Length == 2 && !screenStr.StartsWith("-") && !screenStr.ToString().EndsWith(MathService.DecimalSeparator)) || screenStr.Length > 2)
                {
                    if (!screenStr.ToString().EndsWith("e+0"))
                    {
                        screenStr = new StringBuilder(screenStr.ToString(0, screenStr.Length - 1));
                        if (screenStr.ToString().ToLower().EndsWith("e+")) screenStr = screenStr.Append("0");
                        if (screenStr.ToString().ToLower().EndsWith("e-")) screenStr = new StringBuilder(screenStr.ToString(0, screenStr.Length - 1)).Append("+0");
                    }
                    else screenStr = new StringBuilder(screenStr.ToString(0, screenStr.Length - 3));
                }
            }
            else screenStr = new StringBuilder("0");
            curOperand = screenStr.ToString();
            if (programmerMI.Checked) ScreenToPanel();
            else DisplayToScreen();
            pre_bt = 22;
        }

        int openBrkLevel;
        string[] bracketExp = new string[Constants.MAX_BRACKET_LEVEL + 1];
        //
        // nut mo ngoac
        //
        private void open_bracket_Click(object sender, EventArgs e)
        {
            if (pex == null && openBrkLevel < Constants.MAX_BRACKET_LEVEL)
            {
                //screenStr = new StringBuilder("0");
                DisplayToScreen();
                if (openBrkLevel < Constants.MAX_BRACKET_LEVEL)
                {
                    bracketTime_lb.Text = string.Format("(={0}", ++openBrkLevel);
                }

                if (expAdded)
                {
                    mainExp = new StringBuilder(mainExp.ToString().Substring(0, mainExp.Length - curOperand.Length)).Append("(");
                    //bracketExp[openBrkLevel] = bracketExp[openBrkLevel].Substring(0, bracketExp[openBrkLevel].Length - curOperand.Length) + "(";
                }
                else
                {
                    mainExp = mainExp.Append("(");
                    //bracketExp[openBrkLevel] += "(";  // khong can nua
                }
                curOperand = "0";

                if (scientificMI.Checked) { isFuncClicked = false; expressionTB.Text = mainExp.ToString(); }

                expAdded = false;
                curOperand = screenStr.ToString();
                confirmNumber = true;
                pre_bt = 152;
            }
        }
        //
        // nut dong ngoac
        //
        private void close_bracket_Click(object sender, EventArgs e)
        {
            if (pex == null && openBrkLevel > 0)
            {
                priorityExpression[openBrkLevel] = new string[7];
                //--------------------------------------------------------------
                if (pre_bt != 153)
                {
                    if (!expAdded)
                    {
                        mainExp = mainExp.Append(curOperand);
                        bracketExp[openBrkLevel] += curOperand;
                    }

                    expAdded = true;
                }
                mainExp = mainExp.Append(")");

                curOperand = "(" + bracketExp[openBrkLevel] + ")"; // string.Format("({0})", bracketExp[openBrkLevel]); cung duoc
                if (openBrkLevel > 0)
                    bracketExp[openBrkLevel - 1] += curOperand;
                pex = parser.EvaluateSci(bracketExp[openBrkLevel], true);
                screenStr = new StringBuilder(parser.StringResult);
                // gan cac bien mang ve gia tri mac dinh
                bracketExp[openBrkLevel] = null;
                lowestOperator[openBrkLevel] = 7;
                prePriority[openBrkLevel] = new OperatorModel();
                curPriority[openBrkLevel] = new OperatorModel();

                if (scientificMI.Checked)
                {
                    expressionTB.Text = mainExp.ToString().Trim();
                    isFuncClicked = false;
                }

                if (--openBrkLevel > 0)
                {
                    bracketTime_lb.Text = string.Format("(={0}", openBrkLevel);
                }
                else
                {
                    bracketTime_lb.Text = "";
                }
            }

            confirmNumber = true;
            pre_bt = 153;
            DisplayToScreen();
        }
        //
        // label so openBrkLevel
        //
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
        //
        // menu standard
        //
        private void standardMI_Click(object sender, EventArgs e)
        {
            if (!standardMI.Checked) stdLoad(false);
        }
        //
        // menu scientific
        //
        private void scientificMI_Click(object sender, EventArgs e)
        {
            if (!scientificMI.Checked) sciLoad(false);
        }
        //
        // menu programmer
        //
        private void programmerMI_Click(object sender, EventArgs e)
        {
            if (!programmerMI.Checked) proLoad(false);
        }
        //
        // menu statistics
        //
        private void statisticsMI_Click(object sender, EventArgs e)
        {
            if (!statisticsMI.Checked) staLoad(false);
        }
        //
        // menu digit grouping
        //
        private void digitGroupingMI_Click(object sender, EventArgs e)
        {
            digitLoad(false);
        }
        //
        // menu basic
        //
        private void basicMI_Click(object sender, EventArgs e)
        {
            bool his = historyMI.Checked && historyMI.Enabled;
            if (!basicMI.Checked)
            {
                currentConfig[Config._02_ExtraFunction] = 0;
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
                    FormSizeChanged(new Size(Constants.APP_W1, his ? Constants.APP_H3 : Constants.APP_H1), false);
                }
                if (scientificMI.Checked)
                {
                    if (historyMI.Checked) sciWithHistory();
                    else scientificLoad(false);
                    FormSizeChanged(new Size(Constants.APP_W2, his ? Constants.APP_H3 : Constants.APP_H1), false);
                }
                if (programmerMI.Checked)
                {
                    programmerMode(showPreviewPanelCTMN.Visible);
                    FormSizeChanged(new Size(Constants.APP_W2, (int)currentConfig[Config._05_ShowPreview] == 1 ? Constants.APP_H4 : Constants.APP_H2), false);
                }
                if (statisticsMI.Checked)
                {
                    statisticsMode();
                    FormSizeChanged(new Size(Constants.APP_W1, Constants.APP_H3), false);
                }
                DisplayToScreen();
                scr_lb.Focus();
                prcmdkey = true;
                propertiesChange = true;
            }
        }
        //
        // menu các chức năng
        //
        private void extraFunctionMI_Click(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            if (!mi.Checked) exFunc(mi, false);
        }
        //
        // menu copy
        //
        private void copyCTMN_Click(object sender, EventArgs e)
        {
            if (pex == null)
                try { Clipboard.SetText(screenStr.ToString()); }
                catch { }
        }
        //
        // su kien context menu xo ra
        //
        private void mainContextMenu_Popup(object sender, EventArgs e)
        {
            if (hisDGV.Visible) hisDGV.Focus();
            if (staDGV.Visible) staDGV.Focus();
            //contextMenuOptions(statisticsMI.Checked);
            historyAndDatasetOptionMI_Popup(null, null);
        }
        //
        // su kien history/dataset menu xo ra
        //
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
                editHistoryMI.Enabled = !hisDGV.IsCurrentCellInEditMode && hisDGV.RowCount > 0 && his;
                reCalculateMI.Visible = cancelEditHisMI.Enabled = hisDGV.IsCurrentCellInEditMode;

                showHistoryCTMN.Visible = !historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
                hideHistoryCTMN.Visible = historyMI.Checked && (standardMI.Checked || scientificMI.Checked);
                clearHistoryCTMN.Visible = his;
                clearHistoryCTMN.Enabled = clearHistoryMI.Enabled = hisDGV.RowCount > 0 && his;
            }

            showPreviewPanelCTMN.Visible = programmerMI.Checked && previewPanelHeight == 0;
            hidePreviewPanelCTMN.Visible = programmerMI.Checked && previewPanelHeight != 0;
            previewMI.Checked = hidePreviewPanelCTMN.Visible;
            clearDatasetCTMN.Visible = clearDatasetMI.Visible = statisticsMI.Checked;
            getPaste(null, null);
        }
        //
        // ham paste
        //
        private void getPaste(object sender, EventArgs e)
        {
            pasteMI.Enabled = pasteCTMN.Enabled = Clipboard.ContainsText(TextDataFormat.Text);
        }
        //
        // menu paste
        //
        private void pasteCTMN_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText().ToUpper();
            PasteData(data);
        }
        //
        // menu show preview
        //
        private void previewPanelCTMN_Click(object sender, EventArgs e)
        {
            propertiesChange = true;

            previewPanelHeight = showPreviewPanelCTMN.Visible ? 81 : 0;
            showPreviewPanelCTMN.Visible = !showPreviewPanelCTMN.Visible;
            hidePreviewPanelCTMN.Visible = !showPreviewPanelCTMN.Visible;

            formWithPreview(false);
        }
        //
        // ham reCalculate
        //
        private void reCalculate_Click(object sender, EventArgs e)
        {
            reCalculate(hisDGV.CurrentCell.RowIndex);
        }
        //
        // menu cancel edit history
        //
        private void cancelEditHisMI_Click(object sender, EventArgs e)
        {
            hisDGV.CancelEdit();
            hisDGV.ReadOnly = true;
            hisDGV[0, hisDGV.CurrentCell.RowIndex].Selected = true;
            prcmdkey = true;
            dgv_OnEndEdit(hisDGV);
        }
        //
        // menu copy history
        //
        private void copyHistoryMI_Click(object sender, EventArgs e)
        {
            string clipboard = "";
            try
            {
                foreach (DataGridViewRow row in hisDGV.Rows)
                {
                    clipboard += row.Cells[0].Value.ToString().Replace("\n", "") + Environment.NewLine;
                }
                Clipboard.SetText(clipboard);
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        //
        // menu edit history
        //
        private void editHistoryMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            hisDGV.ReadOnly = false;
            hisDGV.BeginEdit(false);
        }
        //
        // menu copy dataset
        //
        private void copyDatasetMI_Click(object sender, EventArgs e)
        {
            // DO SOMETHING HERE
            StringBuilder clipboard = new StringBuilder();
            try
            {
                foreach (DataGridViewRow item in staDGV.Rows)
                {
                    clipboard = clipboard.AppendFormat("{0}_{1}\n", 
                        item.Cells[0].Value, 
                        item.Cells[0].ToolTipText.Substring("Frequence: ".Length)
                        );
                }
                Clipboard.SetText(clipboard.ToString());
            }
            catch (Exception ex) //{ /*làm gì giờ*/ }
            {
                Clipboard.SetText(ex.Message);
            }
        }
        //
        // menu edit dataset
        //
        private void editDatasetMI_Click(object sender, EventArgs e)
        {
            prcmdkey = false;
            staDGV.ReadOnly = false;
            staDGV.BeginEdit(false);
        }
        //
        // menu cancel edit dataset
        //
        private void cancelEditDSMI_Click(object sender, EventArgs e)
        {
            //staDGV.CellEndEdit -= staDGV_CellEndEdit;
            staDGV.IsCancelEdit = true;
            staDGV.CancelEdit();
            staDGV.ReadOnly = true;
            staDGV[0, staDGV.CurrentCell.RowIndex].Selected = true;
            prcmdkey = true;
            dgv_OnEndEdit(staDGV);
            //staDGV.CellEndEdit += staDGV_CellEndEdit;
        }
        //
        // menu commit dataset
        //
        private void commitDSMI_Click(object sender, EventArgs e)
        {
            prcmdkey = true;
            staDGV.EndEdit();
            staDGV.ReadOnly = true;
        }
        //
        // menu about
        //
        private void aboutMI_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }
        //
        // menu help topics
        //
        private void helpTopicsMI_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("shortcuts-key.pdf");
            }
            catch 
            {
                System.Diagnostics.Process.Start("http://www.shortcutmania.com/Windows-7-Calculator-Keyboard-Shortcuts.htm");
            }
        }
        //
        // menu preferrences
        //
        private void preferrencesMI_Click(object sender, EventArgs e)
        {
            Preferences pre = new Preferences(
                currentConfig[Config._09_CollapseSpeed],
                (int)currentConfig[Config._10_SignInteger] == 1,
                (int)currentConfig[Config._11_ReadDictionary] == 1,
                (int)currentConfig[Config._12_StoreHistory] == 1, 
                currentConfig[Config._13_FastInput]
                );
            pre.DoCheck += new Preferences.PreferencesChanged(pre_DoCheck);
            pre.ShowDialog();
        }
        //
        // sự kiện kick nút ok trên form preferrences
        //
        private void pre_DoCheck(bool isReadDictChanged, params object[] paras)
        {
            if (isReadDictChanged)
            {
                if ((bool)paras[3])
                {
                    RegistryService.ReadDictionary(ref factDict);
                }
                else    // phải giữ lại những gì vừa tính
                {
                    factDict = factDict.Where(w => w.IsRecalculate).ToList();

                    #region non-linq
                    //// lấy logic là fo.IsRecalculate = true lúc nào cũng trên đầu
                    //for (int i = 0; i < factDict.Length; i++)
                    //{
                    //    if (!factDict[i].IsRecalculate)
                    //    {
                    //        Array.Resize<FactorialObject>(ref factDict, i);
                    //        //initDictLength = i;
                    //        break;
                    //    }
                    //} 
                    #endregion
                }
            }
            currentConfig[Config._09_CollapseSpeed] = (int)paras[Config._09_CollapseSpeed - 9];
            currentConfig[Config._10_SignInteger] = (bool)paras[Config._10_SignInteger - 9] ? 1 : 0;
            currentConfig[Config._11_ReadDictionary] = (bool)paras[Config._11_ReadDictionary - 9] ? 1 : 0;
            currentConfig[Config._12_StoreHistory] = (bool)paras[Config._12_StoreHistory - 9] ? 1 : 0;
            currentConfig[Config._13_FastInput] = (int)paras[Config._13_FastInput - 9];
            if (statisticsMI.Checked)
            {
                DisplayCountNumber();
            }

            if (!currentConfig.Equals(initConfig)) propertiesChange = true;

            if (programmerMI.Checked) PanelToScreen(true);
        }
        /// <summary>
        /// hiển thị dòng ∑n = ... ở form statistic dựa trên cấu hình
        /// </summary>
        /// <param name="countMethod">cấu hình count method</param>
        private void DisplayCountNumber()
        {
            //countLB.Text = string.Format("∑n = {0}, Id = {1} / {2}", total, staDGV.CurrentCell.RowIndex + 1, staDGV.RowCount);
            countLB.Text = string.Format("∑n = {0}", total);
        }
        #endregion

        #region date calculation
        private void DateCalculationControlVisible(int selectIndex)
        {
            secondDate.Visible = dtP2.Visible = lbResult1.Visible = tbResult1.Visible = selectIndex == 0;
            addrb.Visible = subrb.Visible = yearAddSubLB.Visible = monthAddSubLB.Visible = dayAddSubLB.Visible = periodsDateUD.Visible = periodsMonthUD.Visible = periodsYearUD.Visible = selectIndex == 1;
        }
        //
        // su kien datetimepicker thay doi
        //
        private void dtP_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked)
            {
                if (dateCalculationMI.Checked) dateCalculationBT_Click(null, null);
            }
            else
            { tbResult1.Text = ""; tbResult2.Text = ""; }
        }
        //
        // nut calculate
        //
        private void dateCalculationBT_Click(object sender, EventArgs e)
        {
            //if (prcmdkey) return;
            if (datemethodCB.SelectedIndex == 0)
            {
                int[] differ = Common.DifferencesTimes(dtP2.Value, dtP1.Value);
                int diff = Common.DifferenceBW2Dates(dtP1.Value, dtP2.Value);

                if (diff == 0)
                    tbResult1.Text = tbResult2.Text = "Same dates";
                else if (diff == 1)
                    tbResult2.Text = "1 day";
                else tbResult2.Text = string.Format("{0} days", MathService.Group(diff));

                #region hiển thị kết quả lên textbox
                StringBuilder text = new StringBuilder();
                if (differ[0] != 0)
                {
                    if (differ[0] != 1) text = new StringBuilder(string.Format("{0} years", differ[0]));
                    else text = new StringBuilder("1 year");
                }
                if (differ[1] != 0)
                {
                    if (differ[0] != 0) text = text.Append(", ");
                    if (differ[1] != 1) text = text.AppendFormat("{0} months", differ[1]);
                    else text = text.Append("1 month");
                }
                if (differ[2] != 0)
                {
                    if (differ[0] != 0 || differ[1] != 0) text = text.Append(", ");
                    if (differ[2] != 1) text = text.AppendFormat("{0} weeks", differ[2]);
                    else text = text.Append("1 week");
                }
                if (differ[3] != 0)
                {
                    if (differ[0] != 0 || differ[1] != 0 || differ[2] != 0) text = text.Append(", ");
                    if (differ[3] != 1) text = text.AppendFormat("{0} days", differ[3]);
                    else text = text.Append("1 day");
                }
                tbResult1.Text = text.ToString();
                #endregion

                if (tbResult1.Text == "") tbResult1.Text = tbResult2.Text;
                tbResult2.ForeColor = Color.Black;
            }
            else if (datemethodCB.SelectedIndex == 1)
            {
                DateTime dt = dtP1.Value;
                int sign = addrb.Checked ? 1 : -1;
                dt = dt.AddYears((int)periodsYearUD.Value * sign);
                dt = dt.AddMonths((int)periodsMonthUD.Value * sign);
                dt = dt.AddDays((double)periodsDateUD.Value * sign);
                tbResult2.Text = dt.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
                tbResult2.ForeColor = Color.Black;
            }
            else if (datemethodCB.SelectedIndex == 2)
            {
                LunarDateModel lunar = ConvertDateService.cd.Solar2Lunar(dtP1.Value);
                tbResult2.Text = lunar.ToString();
                // tô đỏ những ngày lễ/rằm/đầu tháng trong âm lịch
                var dm = lunar.Day.ToString() + lunar.Month.ToString();
                if (lunar.Day == 1 || lunar.Day == 15 || dm == "103" || dm == "11" || dm == "21" || dm == "31")
                {
                    tbResult2.ForeColor = Color.Red;
                }
                else
                {
                    tbResult2.ForeColor = Color.Black;
                }
            }
            if (sender is Button)
            {
                tbResult2.Focus();
                tbResult2.SelectionStart = 0;
                tbResult2.SelectionLength = 0;
            }
        }
        //
        // datemethodCB_SelectedIndexChanged
        //
        private void datemethodCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            currentConfig[Config._08_DateMethod] = index;
            calMethod_SIC(false, index);
        }
        //
        // thay doi gia tri combobox calculate method
        //
        private void calMethod_SIC(bool isLoad, int selectIndex)
        {
            if (isLoad) autocal_date.Checked = ((int)initConfig[Config._07_AutoCalculate] == 1);
            else propertiesChange = true;

            DateCalculationControlVisible(selectIndex);
            firstDate.Text = "From";
            switch (datemethodCB.SelectedIndex)
            {
                case 0:
                    firstDate.Text = "From";
                    dtP1.Location = new Point(60, 63);
                    lbResult2.Text = "Difference (days)";
                    break;
                case 1:
                    firstDate.Text = "From";
                    dtP1.Location = new Point(60, 63);
                    lbResult2.Text = "Date";
                    break;
                case 2:
                    firstDate.Text = "Solar date";
                    dtP1.Location = new Point(75, 63);
                    lbResult2.Text = "Lunar date";
                    break;
            }

            tbResult2.Text = "";
            tbResult1.Text = "";
            dtP_ValueChanged(dtP2, null);
        }
        //
        // thay doi gia tri cua 3 numericupdown
        //
        private void periods_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) dateCalculationBT_Click(null, null);
        }
        //
        // check chon auto calculate checkbox
        //
        private void autocal_date_CheckedChanged(object sender, EventArgs e)
        {
            currentConfig[Config._07_AutoCalculate] = autocal_date.Checked ? 1 : 0;
            if (tbResult1.Text == "") dtP_ValueChanged(dtP2, e);
            propertiesChange = true;
        }
        //
        // check chon add/sub radio button
        //
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
            toolTip1.Hide(fromTB);
            if (fromTB.ForeColor == SystemColors.GrayText)
            {
                fromTB.ForeColor = SystemColors.ControlText;
                fromTB.Font = new Font(fromTB.Font, FontStyle.Regular);
            }
            string tinh = GetToTBText(fromTB.Text, fromTB.ForeColor);
            if (tinh == "Invalid input number")
            {
                toTB.Text = "";
                Point pt = fromTB.GetPositionFromCharIndex(fromTB.Text.Length - 1);
                toolTip1.Show(" This value is not valid ", fromTB, pt.X - 5, -40, 3000);
            }
            else
            {
                toTB.Text = tinh;
            }
        }
        //
        // from textbox lost focus
        //
        private void fromTB_LostFocus(object sender, EventArgs e)
        {
            if (fromTB.ForeColor == SystemColors.GrayText) toTB.Text = "";
        }
        //
        // from textbox focused
        //
        private void fromTB_GotFocus(object sender, EventArgs e)
        {
            prcmdkey = false;
        }
        //
        // fromCB_SelectedIndexChanged
        //
        private void fromCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeUnitCB.SelectedIndex < 0) typeUnitCB.SelectedIndex = 0;
            invert_unit.Enabled = (fromCB.SelectedIndex != toCB.SelectedIndex);
            string tinh = GetToTBText(fromTB.Text, fromTB.ForeColor);
            if (tinh == "Invalid input number")
            {
                fromTB.Text = "";
                toTB.Text = "";
                Point pt = fromTB.GetPositionFromCharIndex(fromTB.Text.Length - 1);
                toolTip1.Hide(fromTB);
                toolTip1.Show(" This value is not valid ", fromTB, pt.X - 5, -40, 3000);
            }
            else
            {
                toTB.Text = tinh;
            }
        }
        //
        // typeUnitCB_SelectedIndexChanged
        //
        private void typeUnitCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (typeFECB.SelectedIndex < 0) MessageBox.Show("Lỗi âm");
            currentConfig[Config._06_TypeUnit] = typeUnitCB.SelectedIndex;
            propertiesChange = invert_unit.Enabled = true;
            toCB.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            assignDefaultIndex(typeUnitCB.SelectedIndex);
            toCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;
            fromCB_SelectedIndexChanged(fromCB, null);
        }
        /// <summary>
        /// khởi tạo vị trí mặc định của combox kết quả
        /// </summary>
        private void assignDefaultIndex(int selectindex)
        {
            fromCB.Items.Clear();
            fromCB.Items.AddRange(Constants.unitTypeItemMember[selectindex]);
            fromCB.MainDataSource = Constants.unitTypeItemMember[selectindex];
            fromCB.SelectedIndexChanged -= fromCB_SelectedIndexChanged;
            fromCB.SelectedIndex = 0;
            toCB.Items.Clear();
            toCB.Items.AddRange(Constants.unitTypeItemMember[selectindex]);
            toCB.MainDataSource = Constants.unitTypeItemMember[selectindex];

            switch (typeUnitCB.Items[selectindex].ToString())
            {
                case "Angle": toCB.SelectedIndex = toCB.Items.IndexOf("Radian");
                    break;
                case "Area": toCB.SelectedIndex = toCB.Items.IndexOf("Square meters");
                    break;
                case "Data": toCB.SelectedIndex = toCB.Items.IndexOf("Byte (B)");
                    break;
                case "Energy": toCB.SelectedIndex = toCB.Items.IndexOf("Joule");
                    break;
                case "Length": toCB.SelectedIndex = toCB.Items.IndexOf("Meter");
                    break;
                case "Power": toCB.SelectedIndex = toCB.Items.IndexOf("Watt");
                    break;
                case "Pressure": toCB.SelectedIndex = toCB.Items.IndexOf("Pascal");
                    break;
                case "Temperature": toCB.SelectedIndex = toCB.Items.IndexOf("Degrees Fahrenheit");
                    break;
                case "Time": toCB.SelectedIndex = toCB.Items.IndexOf("Second");
                    break;
                case "Velocity": toCB.SelectedIndex = toCB.Items.IndexOf("Meter per second");
                    break;
                case "Volume": toCB.SelectedIndex = toCB.Items.IndexOf("Cubic meter");
                    break;
                case "Weight / Mass": toCB.SelectedIndex = toCB.Items.IndexOf("Kilogram");
                    break;
            }
            if (toCB.SelectedIndex == -1) toCB.SelectedIndex = 0;
            fromCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;
        }
        //
        // typeCB_MouseDown
        //
        private void typeCB_MouseDown(object sender, MouseEventArgs e)
        {
            prcmdkey = false;
            //fromCB_SelectedIndexChanged(fromCB, null);
        }
        /// <summary>
        /// tính kết quả thu được từ textbox giá trị cần tính
        /// </summary>
        /// <param name="fromstr">giá trị của chuỗi nhập vào</param>
        /// <param name="color">màu của textbox</param>
        /// <returns>giá trị dạng chuỗi của khung kết quả</returns>
        private string GetToTBText(string fromstr, Color color)
        {
            if (MathService.IsNumber(fromstr))// && fromTB.Text.Trim() != "")
            {
                double db = double.Parse(fromstr);
                string cbbText = typeUnitCB.Text;
                if (cbbText != "Temperature")
                    db *= Common.GetRate(typeUnitCB.SelectedIndex, fromCB.SelectedIndex, toCB.SelectedIndex);
                else
                    db = Common.GetTemperature(fromCB.SelectedIndex, toCB.SelectedIndex, db);

                if (double.Parse(fromTB.Text) < 0 && typeUnitCB.SelectedIndex != 6)
                    return "The input number must be a positive";
                if (digitGroupingMI.Checked)
                {
                    if (db.ToString().IndexOf("E") <= 0) return MathService.Group(db);
                    else return db.ToString().ToLower();
                }
                else
                    return db.ToString().Replace(MathService.GroupSeparator, "").ToLower();
            }
            else if ((fromstr == "Enter value" && color == SystemColors.GrayText) || fromstr == "") return "";
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
            fromCB.SelectedIndex = toCB.SelectedIndex;
            fromCB.SelectedIndexChanged += fromCB_SelectedIndexChanged;
            toCB.SelectedIndex = temp;
            fromLB.Focus();
        }

        #endregion

        #region history controls

        private void hotkeyMI_Click(object sender, EventArgs e)
        {
            Info tt = new Info(infoControl);
            tt.Form_Closed += new Info.SendKeyHandler(tt_Form_Closed);
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
        /// <summary>
        /// truyền phím được bấm ở form info về form chính
        /// </summary>
        private void tt_Form_Closed(Keys keys)
        {
            Message msg = Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
            ProcessCmdKey(ref msg, keys);
        }

        private void angelPN_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            int tab = rb.TabIndex - 114;
            if (rb.Checked) parser.Mode = (TrigonometryMode)tab;
        }
        /// <summary>
        /// mở panel history
        /// </summary>
        private void historyMI_Click(object sender, EventArgs e)
        {
            formWithHistory(false);
        }

        private void previewMI_Click(object sender, EventArgs e)
        {
            previewPanelCTMN_Click(null, null);
        }
        /// <summary>
        /// xoá lịch sử trên grid, đồng thời xoá luôn trên registry nếu có đặt cấu hình
        /// </summary>
        private void clearHistoryMI_Click(object sender, EventArgs e)
        {
            hisDGV.AllowCellStateChanged = false;
            hisDGV.Rows.Clear();
            historyObj = new HistoryModel[100];
            hisDGV.AllowCellStateChanged = true;
            countLB.Text = "";
            if ((int)currentConfig[Config._12_StoreHistory] == 1)
            {
                RegistryService.ClearHistory();
            }
        }
        /// <summary>
        /// định dạng font cho row mới thêm trên history grid
        /// </summary>
        private void hisDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            hisDGV.Rows[hisDGV.RowCount - 1].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
            hisDGV.Rows[hisDGV.RowCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
        }
        /// <summary>
        /// cell begin edit history grid
        /// </summary>
        private void historyDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            hisDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            pex = null;
            prcmdkey = false;   // cai nay phai cho len truoc
            dgv_OnBeginEdit(sender);
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
            dgv.Rows[dgv.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.White;
            screenPN.BackColor = BackColor;
            if (dgv.RowCount < 4)
            {
                dgv.BackgroundColor = BackColor;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != dgv.CurrentCell.RowIndex) dgv.Rows[i].DefaultCellStyle.BackColor = BackColor;
                }
            }
            else
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (i != dgv.CurrentCell.RowIndex) dgv.Rows[i].DefaultCellStyle.BackColor = BackColor;
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
                dgv[0, dgv.CurrentCell.RowIndex].Value = MathService.StandardExpression(dgv[0, dgv.CurrentCell.RowIndex].Value.ToString());
            }
            catch { dgv[0, dgv.CurrentCell.RowIndex].Value = "0"; }
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
            if (dgv == staDGV && !dgv.IsCancelEdit)
            {
                dgv.IsCancelEdit = false;
                if ((int)currentConfig[Config._13_FastInput] == 1)
                {
                    addStaWithFrequence(true);
                } 
            }
        }
        /// <summary>
        /// thay doi kich thuoc cell de hien thi nhieu dong
        /// </summary>
        private void ChangeDGVHeight(IDataGridView dgv)
        {
            int maxCharPerLine = 0;
            if (standardMI.Checked || statisticsMI.Checked) maxCharPerLine = 26;
            if (scientificMI.Checked || programmerMI.Checked) maxCharPerLine = 53;

            string cell = dgv[0, dgv.RowCount - 1].Value.ToString();
            dgv.Rows[dgv.RowCount - 1].Height = (cell.Length / maxCharPerLine + 1) * 20;
        }
        /// <summary>
        /// double click để edit dòng hiện tại
        /// </summary>
        private void dGV_DoubleClick(object sender, EventArgs e)
        {
            var dgv = sender as IDataGridView;
            // không hỗ trợ edit history cho standard calculator
            if (/*(scientificMI.Checked || statisticsMI.Checked) && */dgv.RowCount != 0 && dgv.AllowCellDoubleClick)
            {
                dgv.ReadOnly = false;
                dgv.BeginEdit(false);
                prcmdkey = false;
            }
        }

        private void hisDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            hisDGV.AllowCellStateChanged = false;
            hisDGV.MouseDown += EnableKeyboardAndChangeFocus;
            hisDGV[0, e.RowIndex].Value = StandardPasteData(hisDGV[0, e.RowIndex].Value);
            dgv_OnEndEdit(sender);
            reCalculate(e.RowIndex);
            hisDGV.AllowCellStateChanged = true;
            hisDGV.AllowCellDoubleClick = true;
            hisDGV.AllowCellClick = true;
            //configPath = Environment.GetFolderPath
        }

        private void hisDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hisDGV.AllowCellStateChanged = false;
            if (!hisDGV.IsCurrentCellInEditMode)
            {
                //hisDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
                //expressionTB.Text = mainExp = new StringBuilder();
                //clear_num(true);
                evaluateExpression(e.RowIndex, false);
                curOperand = screenStr.ToString();
                pre_bt = 16;
            }
            hisDGV.AllowCellStateChanged = true;
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
                //countLB.Text = string.Format("Value = {0} / {1}", rowindex + 1, hisDGV.RowCount);
                string expression = hisDGV[0, rowindex].Value.ToString();
                int matches = MathService.NumberOfOpenWOClose(expression);
                if (matches > 0)
                {
                    expression = expression.PadRight(matches + expression.Length, ')');
                    hisDGV[0, rowindex].Value = expression;
                }
                if (isRecalculate)
                {
                    // neu bieu thuc co chua ham giai thua thi moi dung backgroundwoker
                    if (expression.ToLower().Contains("fact"))
                    {
                        // neu bieu thuc co tham so da duoc dinh nghia truoc do
                        if (CheckExpressionConditions(expression, false))
                        {
                            pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()), scientificMI.Checked);
                        }
                        else
                        {
                            // check bieu thuc xem co tham so nao can phai dung backgroundwoker khong
                            if (!CheckExpressionConditions(expression, true))
                            {
                                pex = parser.EvaluateSci(SimplyFactorialExpression(mainExp.ToString()), scientificMI.Checked);
                            }
                            else ShowFactorialProgress_Expr();
                        }
                    }
                    else
                    {
                        pex = parser.EvaluateSci(expression, scientificMI.Checked);
                    }

                    if (pex != null)
                    {
                        scr_lb.Text = pex.Message;
                        screenStr = new StringBuilder(pex.Message);
                        scr_lb.Font = new Font("Consolas", pex.Message.Length > 40 ? 5.25F : 9.75F);
                    }
                    else
                    {
                        hisDGV.ReadOnly = prcmdkey = true;
                        isFuncClicked = false;
                        mainExp = new StringBuilder();

                        screenStr = new StringBuilder(parser.StringResult);
                        curOperand = parser.StringResult;
                    }
                    historyObj[rowindex].Expression = expression;
                    historyObj[rowindex].Result = screenStr.ToString();
                    if (string.IsNullOrEmpty(screenStr.ToString())) screenStr = new StringBuilder("0");
                }
                else
                {
                    if (standardMI.Checked || scientificMI.Checked) screenStr = new StringBuilder(historyObj[rowindex].Result.Trim());
                    if (BigNumber.IsNumber(screenStr.ToString()))
                        pex = null;
                    else
                        pex = new Exception(screenStr.ToString());
                }
                confirmNumber = true;
                //hisDGV_SetStatusLabel();
                DisplayToScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// kiểm tra tất cả giá trị trong hàm fact có vượt quá 100000 (1e5) hoặc đã được định nghĩa trước đó hay chưa
        /// kiểm tra biểu thức cần tính xem có cần phải show progress hay không
        /// </summary>
        /// <param name="expression">biểu thức cần kiểm tra</param>
        /// <param name="showPrg">true nếu là show progress, false nếu cần kiểm tra có tham số nào không được định nghĩa trước đó hay không</param>
        private bool CheckExpressionConditions(string expression, bool showPrg)
        {
            int searchIndex = 0;
            mainExp = new StringBuilder(expression);
            int open = 0;
            open = expression.IndexOf("fact(", searchIndex) + 4;
            while (open >= 4)
            {
                //if (open < 4) break;
                string subExp = "";
                int close = MathService.GetCloseBracketIndex(expression, open, ref subExp);
                searchIndex = close + 1;
                //string subExp = expression.Substring(open + 1, close - open - 1);
                parser.EvaluateSci(subExp);
                // neu co 1 ket qua >= 100000 (1e5) thi return true luon
                if (showPrg)
                    if (parser.BigNumberResult >= 1e5 && Array.IndexOf(factDict.SelectValue(), parser.StringResult) < 0) return true;
                if (!showPrg)
                    if (!factDict.SelectValue().Contains(parser.StringResult)) return false;
                open = expression.IndexOf("fact(", searchIndex) + 4;
            }
            return !showPrg;
        }
        //
        // nut up/down
        //
        private void directionBT_Click(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            ProcessUpOrDown(e.Delta / Math.Abs(e.Delta), false);
        }
        //
        // nut up/down
        //
        private void directionBT_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            ProcessUpOrDown(btn.TabIndex - 2, false);
        }
        #endregion

        #region programmer mode
        /// <summary>
        /// éo biết gọi là gì, đặt tạm thế
        /// </summary>
        int SizeBin = 64;
        int previewPanelHeight = 0;
        string binnum64 = new string('0', 64);
        /// <summary>
        /// dựa trên sự thay đổi của số trên panel nhị phân mà trả về kết quả hiển thị trên màn hình chính
        /// </summary>
        /// <param name="bin64Updated">biến kiểm tra xem biến binnum64 đã được cập nhật chưa để không phải cập nhật lại lần nữa</param>
        private void PanelToScreen(bool bin64Updated)
        {
            try
            {
                if (!bin64Updated)
                {
                    binnum64 = "";
                    for (int i = 15; i >= 0; i--)
                    {
                        binnum64 += bin_digit[i].Text;
                    }
                }
                binRB.Value = binnum64.TrimStart('0');
                if (binRB.Value == "")
                {
                    binRB.Value = "0";
                }
                decRB.Value = BinaryService.OthersToDecimal(binRB.Value, 02, SizeBin, (int)currentConfig[Config._10_SignInteger] == 1);
                octRB.Value = BinaryService.DecimalToOthers(decRB.Value, 08, SizeBin, (int)currentConfig[Config._10_SignInteger] == 1);
                hexRB.Value = BinaryService.DecimalToOthers(decRB.Value, 16, SizeBin, (int)currentConfig[Config._10_SignInteger] == 1);
                ToPreviewPanel();
                if (binRB.Checked) screenStr = new StringBuilder(binRB.Value);
                if (octRB.Checked) screenStr = new StringBuilder(octRB.Value);
                if (decRB.Checked) screenStr = new StringBuilder(decRB.Value);
                if (hexRB.Checked) screenStr = new StringBuilder(hexRB.Value);
                curOperand = screenStr.ToString();
                confirmNumber = true;
                prcmdkey = true;
                DisplayToScreen();
            }
            catch (Exception e)
            {
                pex = e;
                screenStr = new StringBuilder(e.Message);
            }
            finally { DisplayToScreen(); }
        }
        /// <summary>
        /// sự kiện scr_lb text changed thông qua thao tác nhập số
        /// </summary>
        private void ScreenToPanel(bool isUpdated = false)
        {
            try
            {
                bool signNum = (int)currentConfig[Config._10_SignInteger] == 1;
                if (binRB.Checked)
                {
                    if (!isUpdated) binRB.Value = screenStr.ToString();
                    if (!isUpdated) decRB.Value = BinaryService.OthersToDecimal(binRB.Value, 02, SizeBin, signNum);
                    octRB.Value = BinaryService.DecimalToOthers(decRB.Value, 08, SizeBin, signNum);
                    hexRB.Value = BinaryService.DecimalToOthers(decRB.Value, 16, SizeBin, signNum);
                }
                if (octRB.Checked)
                {
                    octRB.Value = screenStr.ToString();
                    if (!isUpdated) decRB.Value = BinaryService.OthersToDecimal(octRB.Value, 08, SizeBin, signNum);
                    if (!isUpdated) binRB.Value = BinaryService.DecimalToOthers(decRB.Value, 02, SizeBin, signNum);
                    hexRB.Value = BinaryService.DecimalToOthers(decRB.Value, 16, SizeBin, signNum);
                }
                if (decRB.Checked)
                {
                    if (!isUpdated) binRB.Value = BinaryService.DecimalToOthers(screenStr.ToString(), 02, SizeBin, signNum);
                    octRB.Value = BinaryService.DecimalToOthers(screenStr.ToString(), 08, SizeBin, signNum);
                    if (!isUpdated) decRB.Value = BinaryService.OthersToDecimal(binRB.Value, 02, SizeBin, signNum);
                    hexRB.Value = BinaryService.DecimalToOthers(screenStr.ToString(), 16, SizeBin, signNum);
                }
                if (hexRB.Checked)
                {
                    hexRB.Value = screenStr.ToString();
                    if (!isUpdated) decRB.Value = BinaryService.OthersToDecimal(hexRB.Value, 16, SizeBin, signNum);
                    if (!isUpdated) binRB.Value = BinaryService.DecimalToOthers(decRB.Value, 02, SizeBin, signNum);
                    octRB.Value = BinaryService.DecimalToOthers(decRB.Value, 08, SizeBin, signNum);
                }
                ToPreviewPanel();

                // đưa lên panel
                binnum64 = binRB.Value.PadLeft(64, '0');
                for (int i = 0; i < 16; i++)
                {
                    bin_digit[i].Text = binnum64.Substring(60 - i * 4, 4);
                }
            }
            catch
            {
                pex = new OverflowException("Overflow");
                screenStr = new StringBuilder(pex.Message);
            }
            finally { DisplayToScreen(); }
        }
        /// <summary>
        /// đưa giá trị các phân hệ khác lên panel preview
        /// </summary>
        private void ToPreviewPanel()
        {
            hex_prvLb.Text = string.Format("Hex: {0}", MathService.Group(hexRB.Value, " ", 4));
            dec_prvLb.Text = string.Format("Dec: {0}", MathService.Group(decRB.Value, MathService.GroupSeparator, 3));
            oct_prvLb.Text = string.Format("Oct: {0}", MathService.Group(octRB.Value, " ", 3));
            bin_prvLb.Text = string.Format("Bin: {0}", MathService.Group(binRB.Value, " ", 4));
        }
        /// <summary>
        /// preview label của số nhị phân text changed
        /// </summary>
        private void prvlbBin_TextChanged(object sender, EventArgs e)
        {
            if (bin_prvLb.Text.Length > 69)
            {
                bin_prvLb.Height = 30;
            }
            else
            {
                bin_prvLb.Height = 13;
            }
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
                //var a = Controls.Find("prvlb" + rb.Text, true);
                //if (a.Length > 0) a[0].ForeColor = Color.Red;
                screenStr = new StringBuilder(rb.Value);
                DisplayToScreen();
            }
            confirmNumber = true;
        }
        /// <summary>
        /// click chuột vào các số nhị phân trên panel nhị phân
        /// </summary>
        private void bin_digit_MouseDown(object sender, MouseEventArgs e)
        {
            if (pex != null) return;
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
            confirmNumber = true;
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
                binnum64 = "";
                for (int i = 0; i < 16; i++)
                {
                    bin_digit[i].Visible = i < bd;
                    if (i >= bd) bin_digit[i].Text = "0000";
                    binnum64 = bin_digit[i].Text + binnum64;
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
                PanelToScreen(false);
            }
        }
        /// <summary>
        /// nút not
        /// </summary>
        private void notBT_Click(object sender, EventArgs e)
        {
            int not = 0;
            for (int i = 0; i < 16; i++)
            {
                if (bin_digit[i].Visible)
                {
                    not = int.Parse(bin_digit[i].Text);
                    not = 1111 - not;
                    bin_digit[i].Text = not.ToString().PadLeft(4, '0');
                }
                else break;
            }
            PanelToScreen(false);
        }
        /// <summary>
        /// not RoL và RoR
        /// </summary>
        private void rotateBT_Click(object sender, EventArgs e)
        {
            if (sender == RoLBT) binnum64 = binnum64.Substring(1) + binnum64[0].ToString();
            if (sender == RoRBT) binnum64 = binnum64[63].ToString() + binnum64.Substring(0, 63);
            for (int i = 0; i < 16; i++)
            {
                bin_digit[i].Text = binnum64.Substring(60 - 4 * i, 4);
            }
            if (qwordRB.Checked) architectureRB_CheckedChanged(qwordRB, e);
            if (dwordRB.Checked) architectureRB_CheckedChanged(dwordRB, e);
            if (_wordRB.Checked) architectureRB_CheckedChanged(_wordRB, e);
            if (_byteRB.Checked) architectureRB_CheckedChanged(_byteRB, e);
            PanelToScreen(true);
        }
        /// <summary>
        /// click vào label preview
        /// </summary>
        private void prvlb_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            string rbName = lbl.Name.Substring(0, 3) + "RB";
            IRadioButton rb = basePN.Controls[rbName] as IRadioButton;
            rb.Checked = true;
            baseRB_CheckedChanged(rb, e);
        }
        #endregion

        #region statistics mode
        /// <summary>
        /// biến lưu tổng Σn của các số trên grid
        /// </summary>
        double total = 0;

        List<StatisticModel> statisticItems = new List<StatisticModel>();
        //
        // nút xoá hết (CAD)
        //
        private void CAD_Click(object sender, EventArgs e)
        {
            staDGV.Rows.Clear();
            countLB.Text = "∑n = 0";
            total = 0;
        }
        //
        // nút add
        //
        private void AddstaBT_Click(object sender, EventArgs e)
        {
            if (pex == null)
            {
                if ((ModifierKeys != Keys.None && (int)currentConfig[Config._13_FastInput] != 1)       // ctrl enter và tick chọn (nhập frequency cho từng số)            
                    || (ModifierKeys == Keys.None && (int)currentConfig[Config._13_FastInput] == 1))   // hoặc enter và untick chọn (nhập frequency cho từng số)
                {
                    var dr = addStaWithFrequence(false) ;
                    if (dr == DialogResult.OK)
                    {
                        DisplayCountNumber();
                    }
                }
                else
                {
                    AddStaFQ(1);
                }
            }
        }
        /// <summary>
        /// hiện bảng nhập tần suất của số trên màn hình
        /// </summary>
        /// <param name="isUpdate">true nếu là cập nhật lại giá trị và tần suất của số đó, false nếu là add bình thường</param>
        private DialogResult addStaWithFrequence(bool isUpdate)
        {
            Frequence frq = new Frequence();
            if (isUpdate)
            {
                string curFQ = staDGV[0, staDGV.CurrentCell.RowIndex].ToolTipText;
                curFQ = curFQ.Substring(curFQ.LastIndexOf(' '));
                frq.OldFQ = double.Parse(curFQ);
            }
            frq.IsUpdate = isUpdate;
            frq.AddFrequence += new Frequence.SendFrequence(frq_GetFrequence);
            return frq.ShowDialog();
        }
        /// <summary>
        /// truyền giá trị từ bảng tần suất sang form chính
        /// </summary>
        /// <param name="fq">giá trị tần suất từ bảng nhập</param>
        /// <param name="isUpdate">true nếu là cập nhật lại giá trị và tần suất của số đó, false nếu là add bình thường</param>
        private void frq_GetFrequence(int fq, bool isUpdate)
        {
            if (!isUpdate) 
                AddStaFQ(fq);
            else 
                UpdateStaFQ(fq);
            screenPN.Focus();
            //DisplayCountNumber();
        }
        /// <summary>
        /// thêm giá trị mới với tần suất đã nhập
        /// </summary>
        /// <param name="frequence">tần suất</param>
        private void AddStaFQ(int frequence)
        {
            var existItem = statisticItems.FirstOrDefault(f => f.Value.ToString() == screenStr.ToString());
            DataGridViewCell cell;
            if (existItem != null)
            {
                existItem.Frequence += frequence;
                cell = staDGV[0, existItem.Id - 1];
            }
            else
            {
                var newItem = new StatisticModel()
                {
                    Id = statisticItems.Count + 1,
                    Value = double.Parse(screenStr.ToString()),
                    Frequence = frequence,
                };
                statisticItems.Add(newItem);
                staDGV.Rows.Add();
                staDGV.Rows[staDGV.RowCount - 1].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
                cell = staDGV[0, staDGV.RowCount - 1];
            }
            cell.Value = screenStr.ToString();
            cell.Selected = true;
            cell.ToolTipText = string.Format("Frequence: {0}", existItem != null ? existItem.Frequence : frequence);
            staDGV.CurrentCell = cell;
            total += frequence;
            screenStr = new StringBuilder("0");
            DisplayToScreen();
            DisplayCountNumber();
            confirmNumber = true;
        }
        /// <summary>
        /// thêm giá trị hiện tại với tần suất
        /// </summary>
        /// <param name="fq">tần suất</param>
        private void UpdateStaFQ(double fq)
        {
            string oldFQ = staDGV[0, staDGV.CurrentCell.RowIndex].ToolTipText;
            oldFQ = oldFQ.Substring(oldFQ.LastIndexOf(' '));
            total -= double.Parse(oldFQ);
            staDGV.Rows[staDGV.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font("Consolas", 9.75F);
            //staDGV[0, staDGV.CurrentCell.RowIndex].Value = screenStr.ToString();
            //screenStr = staDGV[0, staDGV.CurrentCell.RowIndex].Value.ToString();
            DisplayToScreen();
            total += fq;
            staDGV[0, staDGV.CurrentCell.RowIndex].ToolTipText = string.Format("Frequence: {0}", fq);
            screenStr = new StringBuilder("0");
            DisplayToScreen();
            confirmNumber = true;
        }
        //
        // sigma (n-1)
        //
        private void sigman_1BT_Click(object sender, EventArgs e)
        {
            if (total <= 1)
            {
                scr_lb.TextChanged -= scr_lb_TextChanged;
                screenStr = new StringBuilder("Enter data to calculate");
                pex = new Exception("Enter data to calculate");
                DisplayToScreen();
                scr_lb.TextChanged += scr_lb_TextChanged;
                scr_lb.Font = new Font(scr_lb.Font.FontFamily, 8f);
                return;
            }
            double resultSigmaN = sum(true) - sum(false) * sum(false) / total;
            resultSigmaN /= total - 1;
            resultSigmaN = Math.Sqrt(resultSigmaN);
            screenStr = new StringBuilder(resultSigmaN.ToString());
            DisplayToScreen();
        }
        //
        // sigma (n)
        //
        private void sigmanBT_Click(object sender, EventArgs e)
        {
            if (total == 0)
            {
                scr_lb.TextChanged -= scr_lb_TextChanged;
                screenStr = new StringBuilder("Enter data to calculate");
                pex = new Exception("Enter data to calculate");
                DisplayToScreen();
                scr_lb.TextChanged += scr_lb_TextChanged;
                scr_lb.Font = new Font(scr_lb.Font.FontFamily, 9.75f);
                return;
            }
            double resultSigmaN = sum(true) - sum(false) * sum(false) / total;
            resultSigmaN /= total;
            resultSigmaN = Math.Sqrt(resultSigmaN);
            screenStr = new StringBuilder(resultSigmaN.ToString());
            DisplayToScreen();
        }
        //
        // sigma (x²)
        //
        private void sigmax2BT_Click(object sender, EventArgs e)
        {
            if (total == 0) return;
            var squareSum = sum(true);
            screenStr = new StringBuilder(squareSum.ToString());
            DisplayToScreen();
        }
        //
        // sigma (x)
        //
        private void sigmaxBT_Click(object sender, EventArgs e)
        {
            if (total == 0) return;
            screenStr = new StringBuilder(sum(false).ToString());
            DisplayToScreen();
        }
        //
        // avarage (x²)
        //
        private void x2cross_Click(object sender, EventArgs e)
        {
            if (total == 0) return;
            screenStr = new StringBuilder((sum(true) / total).ToString());
            DisplayToScreen();
        }
        //
        // avarage (x)
        //
        private void xcross_Click(object sender, EventArgs e)
        {
            if (total == 0) return;
            screenStr = new StringBuilder((sum(false) / total).ToString());
            DisplayToScreen();
        }

        private void staDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            staDGV.MouseDown -= EnableKeyboardAndChangeFocus;
            prcmdkey = false;
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
                vl = vl.Replace(",", MathService.DecimalSeparator).Replace(".", MathService.DecimalSeparator);
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

        private void staDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayCountNumber();
        }
        #endregion

        #region sub extra functions

        private void wsCalculateMethod(TextField[] tf, int wsType)
        {
            if (prcmdkey) return;
            double[] db = new double[tf.Length - 1];
            for (int i = 0; i < tf.Length - 1; i++)
            {
                #region assign textbox value to double array
                try
                {
                    if (tf[order[i]].TextBoxText == "" || tf[order[i]].TextBoxText == "Enter value")
                        throw new Exception("This value cannot be blank");
                    toolTip1.Hide(tf[order[i]]);
                    db[i] = double.Parse(tf[order[i]].TextBoxText);
                    //tf[order[i]].TextBoxText = db[i].ToString();
                }
                catch (FormatException)
                {
                    toolTip1.Hide(tf[order[i]]);
                    toolTip1.Show(" This value is not valid ", tf[order[i]], 155, -40, 3000);
                    tf[order[i]].TBFocus();
                    return;
                }
                catch (Exception ex)
                {
                    toolTip1.Hide(tf[order[i]]);
                    toolTip1.Show(ex.Message, tf[order[i]], 155, -40, 3000);
                    tf[order[i]].TBFocus();
                    return;
                }
                #endregion
            }

            foreach (var lbl in tf)
            {
                if (lbl.LabelText.EndsWith("*")) lbl.LabelText = lbl.LabelText.Substring(0, lbl.LabelText.Length - 2);
            }

            double result_WS = 0, smth = 0;
            int len = order.Length - 1;
            if (wsType == 0)
            {
                #region mortgage calculation
                /*
                 * with pp is the purchase price
                 *      dp is the down payment
                 *      t is term (in years) -> tm is the number of months in this term = t * 12
                 *      r is interest rate (in percent %) -> rm is interest rate in a month = r / 100 / 12
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
                        result_WS /= smth * db[2] / 1200;
                        result_WS = db[0] - result_WS;
                        break;
                    case 1:
                        smth = Math.Pow(1 + db[3] / 1200, db[2] * 12);
                        result_WS = (db[0] - db[1]) * smth * (db[3] / 1200);
                        result_WS /= smth - 1;
                        break;
                    case 2:
                        smth = Math.Pow(1 + db[2] / 1200, db[1] * 12);
                        result_WS = db[3] * (smth - 1);
                        result_WS /= smth * db[2] / 1200;
                        result_WS += db[0];
                        break;
                    case 3:
                        smth = db[3] / (db[3] - (db[0] - db[1]) * db[2] / 1200);
                        result_WS = Math.Log(smth) / Math.Log(1 + db[2] / 1200) / 12;
                        break;
                }
                mortgageTF[order[len]].AllowTextChanged = false;
                //mortgageTF[order[len]].TBFont = new Font(mortgageTF[order[len]].Font, FontStyle.Regular);
                //mortgageTF[order[len]].Fore_Color = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = mortgageTF[order[len]].TextBoxText = MathService.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = mortgageTF[order[len]].TextBoxText = result_WS.ToString();
                mortgageTF[order[len]].AllowTextChanged = true;
                #endregion
            }
            if (wsType == 1)
            {
                #region vehicle lease calculation
                /*
                 * with p is periodic payment
                 *      lv is the lease value
                 *      lp is lease period (in years)
                 *      n is payments per year = 12 * n in months
                 *      rv is residual value
                 *      r is interest rate (in percent %)
                 * we have the formula:
                 *      numerator = (lv - rv + rv * r / lp)
                 *      denominator = (1 - (1 + r / lp) ^ -(n*lp)) / r * lp
                 *      p = numerator / denominator
                 * so we can calculate the other varius from the formula above
                 *
                 * */
                switch (typeWorkSheetCB.SelectedIndex)
                {
                    case 0:
                        //db = new double[5] { lv, n, rv, r, p };
                        var periodicRate = db[3] / db[1];
                        result_WS = Math.Log(1 - periodicRate * (db[0] - db[2]) / (db[4] - db[2] * periodicRate));
                        result_WS /= -db[1];
                        result_WS /= Math.Log(1 + periodicRate);
                        break;
                    case 1:
                        //db = new double[5] { lp, n, rv, r, p };
                        periodicRate = db[3] / db[1];
                        result_WS = db[4] * (1 - Math.Pow(1 + periodicRate, -db[1] * db[0])) / periodicRate;
                        result_WS += db[2] / Math.Pow(1 + periodicRate, db[1] * db[0]);
                        break;
                    case 2:
                        //db = new double[5] { lv, lp, n, rv, r };
                        //result_WS = (lv - rv) / (lp * n) + rv * (rv / n);
                        result_WS = (db[0] - db[3]) / (db[1] * db[2]) + db[3] * (db[4] / db[2]);
                        break;
                    case 3:
                        //db = new double[5] { lv, lp, n, r, p };
                        periodicRate = db[3] / db[2];
                        var depreciationValue = db[4] * (1 - Math.Pow(1 + periodicRate, -db[1] * db[2])) / periodicRate;
                        result_WS = db[0] - depreciationValue;
                        break;
                    default: // chua co cong thuc tinh => ケメノ
                        break;
                }
                VhTF[order[len]].AllowTextChanged = false;
                //mortgageTF[order[len]].TBFont = new Font(mortgageTF[order[len]].Font, FontStyle.Regular);
                //mortgageTF[order[len]].Fore_Color = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = VhTF[order[len]].TextBoxText = MathService.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = VhTF[order[len]].TextBoxText = result_WS.ToString();
                VhTF[order[len]].AllowTextChanged = true;
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
                fe_MPGTF[order[len]].AllowTextChanged = false;
                //fe_MPGTF[order[len]].TBFont = new Font(fe_MPGTF[order[len]].Font, FontStyle.Regular);
                //fe_MPGTF[order[len]].Fore_Color = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = fe_MPGTF[order[len]].TextBoxText = MathService.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = fe_MPGTF[order[len]].TextBoxText = result_WS.ToString();
                fe_MPGTF[order[len]].AllowTextChanged = true;
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
                feL100TF[order[len]].AllowTextChanged = false;
                //feL100TF[order[len]].TBFont = new Font(feL100TF[order[len]].Font, FontStyle.Regular);
                //feL100TF[order[len]].Fore_Color = SystemColors.WindowText;
                if (digitGroupingMI.Checked)
                    workSheetResultTB.Text = feL100TF[order[len]].TextBoxText = MathService.Group(result_WS.ToString());
                else
                    workSheetResultTB.Text = feL100TF[order[len]].TextBoxText = result_WS.ToString();
                feL100TF[order[len]].AllowTextChanged = true;
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
        /// <summary>
        /// đánh lại thứ tự và vị trí các field của các form worksheet
        /// </summary>
        /// <param name="itf">các field của các form worksheet</param>
        private void orderControlLocation(TextField[] itf)
        {
            for (int i = 0; i < itf.Length; i++)
            {
                itf[order[i]].Location = new Point(9, 58 + 24 * i);
                itf[order[i]].TabIndex = 10 + i;
                itf[order[i]].Visible = i != itf.Length - 1;
            }
            //if (MathService.IsNumber(itf[order[itf.Length - 1]].TextBoxText))
            //{
            //    workSheetResultTB.Text = itf[order[itf.Length - 1]].TextBoxText;
            //}
            workSheetResultTB.Text = "";
        }
        /// <summary>
        /// nút calculate của các form worksheet
        /// </summary>
        private void workSheetCalculateBT_Click(object sender, EventArgs e)
        {
            if (mortgageMI.Checked) wsCalculateMethod(mortgageTF, 0);
            if (vehicleLeaseMI.Checked) wsCalculateMethod(VhTF, 1);
            if (fe_MPG_MI.Checked) wsCalculateMethod(fe_MPGTF, 2);
            if (feL100_MI.Checked) wsCalculateMethod(feL100TF, 3);
        }
        /// <summary>
        /// sự kiện gotfocus của result textbox
        /// </summary>
        private void resultTextBox_GotFocus(object sender, EventArgs e)
        {
            DisableKeyboard(sender, e);
            var tb = sender as TextBox;
            tb.SelectionStart = 0;
            tb.SelectionLength = 0;
        }
        /// <summary>
        /// cập nhật giá trị hàm giai thừa lớn vào bộ nhớ
        /// </summary>
        /// <param name="input">đầu vào</param>
        /// <param name="output">đầu ra</param>
        private void parser_UpdateDictEvent(BigNumber input, BigNumber output)
        {
            if (!factDict.SelectValue().Contains(input.StrValue))
            {
                factDict.Add(new FactorialModel()
                {
                    Value = input.StrValue,
                    Result = output.StrValue,
                    IsRecalculate = true,    // hơi thừa
                });
            }
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

        private void hisDGV_CurrentCellChanged(object sender, EventArgs e)
        {
            if (hisDGV.CurrentCell == null) return;
            countLB.Text = string.Format("Id = {0} / {1}", hisDGV.CurrentCell.RowIndex + 1, hisDGV.RowCount);
        }
        #endregion
    }
}