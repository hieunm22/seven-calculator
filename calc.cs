using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Calculator
{
    public partial class calc : Form
    {
        public calc()
        {
            InitializeComponent();
            InitializeAllMenus();
            InitComboBox();
            InitBitNumberArray(); 
            FocusedEvents();
        }

        #region local varial
        string str = "0", sci_expression = "", expressionpow = "";
        bool confirm_num = true, prcmdkey = true;
        int pre_bt = -1, pre_oprt = 0;
        double mem_num = 0;
        Miscellaneous misc = new Miscellaneous();
        #endregion

        /// <summary>
        /// form load
        /// </summary>
        private void calc_Load(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
            }
            pasteTSMI.Enabled = misc.isNumber(Clipboard.GetText().Trim());
            getThousandSym();
            loadInfoFromRegistry(sender, e);
            clearHistoryCTMN.Visible = historyTSMI.Checked;
            getMemoryNumber();
            if (!basicTSMI.Checked)
                prcmdkey = dtP1.Focused || dtP2.Focused || fromTB.Focused;
            else prcmdkey = true;
        }
        /// <summary>
        /// thay đổi kích thước font để hiển thị đủ kết quả
        /// </summary>
        private void scr_lb_TextChanged(object sender, EventArgs e)
        {
            bool bl = scientificTSMI.Checked || programmerTSMI.Checked;
            bl = (scr_lb.Text.Length > 16 + 20 * bl.GetHashCode());
            bool bl2 = scr_lb.Text.Length > 48;
            scr_lb.Font = new Font("Consolas", 14.25F - 3F * (bl.GetHashCode() + bl2.GetHashCode()),
                FontStyle.Regular, GraphicsUnit.Point, (byte)0);
        }

        #region Nhap so va cac phep tinh
        private void numberinput_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            numinput(btn.TabIndex);
        }

        private void operatorBT_Click(object sender, EventArgs e)
        {
            int tab = ((Button)sender).TabIndex;
            if (standardTSMI.Checked) std_operation(tab);
            if (scientificTSMI.Checked) sci_operation(tab);
            if (programmerTSMI.Checked) pro_operation(tab);
        } 
        #endregion

        #region Xu ly so hien tren man hinh
        //
        // nut bang
        //
        private void equal_Click(object sender, EventArgs e)
        {
            equalclicked();
        }
        //
        // nut ham chuc nang
        //
        private void functionBT_Click(object sender, EventArgs e)
        {
            int tabindex = ((Button)sender).TabIndex;
            math_func(tabindex);
            pre_bt = tabindex;
        }
        //
        // nut %
        //
        private void percent_Click(object sender, EventArgs e)
        {
            confirm_num = true;
            double scr_num = double.Parse(scr_lb.Text);
            scr_num /= 100;
            str = scr_num.ToString();
            displayToScreen();
            pre_bt = 18;
        }

        private void pi_bt_Click(object sender, System.EventArgs e)
        {
            confirm_num = true;
            str = "3141592653589793238462643".Insert(1, decimalSym);
            scr_lb.Text = str;
            pre_bt = 3;
        }

        private void inv_bt_Click(object sender, EventArgs e)
        {
            invertFunction(true);
        }

        private void dms_bt_Click(object sender, System.EventArgs e)
        {

        }

        private void fe_bt_Click(object sender, EventArgs e)
        {
            buttonFE();
        }

        private void xn_bt_Click(object sender, System.EventArgs e)
        {
            sci_operation(30);
        }

        private void nvx_bt_Click(object sender, System.EventArgs e)
        {
            sci_operation(34);
        }
        //
        // nut xoa so vua nhap
        //
        private void clear_Click(object sender, EventArgs e)
        {
            clear_num(false);
        }
        //
        // nut xoa toan bo
        //
        private void ce_Click(object sender, EventArgs e)
        {
            clear_num(true);
        }
        //
        // nut xoa ki tu so vua nhap
        //
        private void backspace_Click(object sender, EventArgs e)
        {
            backspaceclicked();
        }
    
        private void int_bt_Click(object sender, EventArgs e)
        {
            if (str.Length > 2 && str.IndexOf(decimalSym) > 0) 
                str = str.Substring(0, str.IndexOf(decimalSym));
            displayToScreen();
        }
        
        #endregion

        #region Xu ly so M
        //
        // nut xoa bo nho
        //
        private void memclear_Click(object sender, EventArgs e)
        {
            if (standardTSMI.Checked || scientificTSMI.Checked)
            {
                mem_num = 0;
                mem_lb.Visible = false;
                confirm_num = true;
                setMemoryNumber(); 
            }
        }
        //
        // nut goi so trong bo nho ra man hinh
        //
        private void memrecall_Click(object sender, EventArgs e)
        {
            if (standardTSMI.Checked || scientificTSMI.Checked)
            {
                str = mem_num.ToString();
                displayToScreen();
                confirm_num = true;
                setMemoryNumber(); 
            }
        }
        //
        // nut cong so them tren man hinh bo nho
        //
        private void madd_Click(object sender, EventArgs e)
        {
            if (standardTSMI.Checked || scientificTSMI.Checked) mem_process(1); 
        }
        //
        // nut bot di so tren man hinh bo nho
        //
        private void mmul_Click(object sender, EventArgs e)
        {
            if (standardTSMI.Checked || scientificTSMI.Checked) mem_process(2); 
        }
        //
        // nut luu so tren man hinh bo nho
        //
        private void mstore_Click(object sender, EventArgs e)
        {
            if (standardTSMI.Checked || scientificTSMI.Checked) mem_process(3); 
        }
        #endregion

        #region Cac menu item duoc click
        private void viewMode_Click(object sender, EventArgs e)
        {
            stdLoad();            
        }

        private void scientificTSMI_Click(object sender, EventArgs e)
        {
            sciLoad();
        }

        private void programmerTSMI_Click(object sender, EventArgs e)
        {
            proLoad();
        }

        private void statisticsTSMI_Click(object sender, EventArgs e)
        {
            staLoad();
        }

        private void digitGroupingTSMI_Click(object sender, EventArgs e)
        {
            digitGroupingTSMI.Checked = !digitGroupingTSMI.Checked;
            writeToRegistry(digitGroupingTSMI);
            if (misc.isNumber(toTB.Text) && unitConversionTSMI.Checked)
            {
                if (digitGroupingTSMI.Checked) toTB.Text = misc.grouping(toTB.Text);
                else toTB.Text = misc.de_group(toTB.Text);
            } 
            if (!programmerTSMI.Checked)
            {
                if (misc.isNumber(str)) displayToScreen();
            }
            else
            {
                if (digitGroupingTSMI.Checked)
                {
                    if (decRB.Checked) scr_lb.Text = misc.grouping(str);
                    if (octRB.Checked) scr_lb.Text = misc.grouping(str, 3);
                    if (binRB.Checked || hexRB.Checked)
                        scr_lb.Text = misc.grouping(str, 4);
                }
                else scr_lb.Text = misc.de_group(scr_lb.Text);
            }
        }

        private void basicTSMI_Click(object sender, EventArgs e)
        {
            basicForm();
        }

        private void unitConversionTSMI_Click(object sender, EventArgs e)
        {
            exFunc(unitConversionTSMI);
        }

        private void dateCalculationTSMI_Click(object sender, EventArgs e)
        {
            //date_cal();
            exFunc(dateCalculationTSMI);
        }

        private void copyTSMI_Click(object sender, EventArgs e)
        {
            copyCommand();
        }

        private void pasteTSMI_Click(object sender, EventArgs e)
        {
            pasteCommand();
        }

        private void copyDatasetTSMI_Click(object sender, EventArgs e)
        {
            // DO SOMETHING HERE
        }

        private void aboutTSMI_Click(object sender, EventArgs e)
        {
            Form ab = new About();
            ab.ShowDialog();
        }

        private void helpTopicsTSMI_Click(object sender, EventArgs e)
        {
            helptopics();
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

        private void dtP1_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(sender, e);
            else 
                { result1.Text = ""; result2.Text = ""; }
        }

        private void datecalc_Load(object sender, EventArgs e)
        {
            calmethodCB.SelectedIndex = 0;
        }

        private void calculate_bt_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            int[] differ = misc.differencesTimes(dtP1.Value, dtP2.Value);
            int d1 = dtP1.Value.Year, d2 = dtP2.Value.Year;
            if (d1 > d2 || (d1 == d2 && dtP1.Value.DayOfYear > dtP2.Value.DayOfYear))
            {
                differ = misc.differencesTimes(dtP2.Value, dtP1.Value);
            }

            if (calmethodCB.SelectedIndex == 0)
            {
                double diff = misc.differenceBW2Dates(dtP1.Value, dtP2.Value);
                if (diff == 1)
                    result2.Text = "1 day";
                else
                {
                    if (diff > 1)
                        result2.Text = misc.grouping(misc.differenceBW2Dates(dtP1.Value, dtP2.Value)) + " days";
                    else
                    {
                        result1.Text = "Same date";
                        result2.Text = "Same date";
                    }
                }

                #region hiển thị kết quả lên textbox
                result1.Text = "";
                if (differ[0] != 0)
                {
                    if (differ[0] != 1) result1.Text = differ[0] + " years";
                    else result1.Text = "1 year";
                }
                if (differ[0] != 0 && differ[1] != 0) result1.Text += ", ";
                if (differ[1] != 0)
                {
                    if (differ[1] != 1) result1.Text += differ[1] + " months";
                    else result1.Text += "1 month";
                }
                if ((differ[0] != 0 || differ[1] != 0) && differ[2] != 0) result1.Text += ", ";
                if (differ[2] != 0)
                {
                    if (differ[2] != 1) result1.Text += differ[2] + " weeks";
                    else result1.Text += "1 week";
                }
                if ((differ[0] != 0 || differ[1] != 0 || differ[2] != 0) && differ[3] != 0) result1.Text += ", ";
                if (differ[3] != 0)
                {
                    if (differ[3] != 1) result1.Text += differ[3] + " days";
                    else result1.Text += "1 day";
                }
                #endregion

                if (result1.Text == "") result1.Text = result2.Text;
            }
            if (calmethodCB.SelectedIndex == 1)
            {
                RegistryKey reg = Registry.CurrentUser;
                reg = reg.OpenSubKey("Control Panel\\International");
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
                result2.Text = dt.ToString((string)reg.GetValue("sLongDate"));
            }
        }

        private void cal_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            autocal_date.Checked = (readFromSubkey("AutoCalculate", "DateCalculation") == 1);
            writeToSubkey(dateCalculationTSMI, "DateCalculation");
            if (calmethodCB.SelectedIndex == 0)
            {
                control_visible(true);
                periodsDateUD.Visible = false;
                label2.Text = "From:";
                label3.Text = "To: ";
                label5.Text = "Difference (days)";
            }

            if (calmethodCB.SelectedIndex == 1)
            {
                control_visible(false);
                periodsDateUD.Visible = true;
                label2.Text = "From:";
                label3.Text = "Periods:";
                label5.Text = "Date:";
            }

            result2.Text = ""; // or calculate_bt_Click(sender, e);
            result1.Text = "";
            dtP1_ValueChanged(sender, e);
        }

        private void periods_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_date.Checked) calculate_bt_Click(sender, e);
        }

        private void autocal_cb_CheckedChanged(object sender, EventArgs e)
        {
            dtP1_ValueChanged(sender, e);
            writeToSubkey(dateCalculationTSMI, "DateCalculation");
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
                RegistryKey reg = Registry.CurrentUser;
                reg = reg.OpenSubKey("Control Panel\\International");
                result2.Text = dt.ToString((string)reg.GetValue("sLongDate")); 
            }
        }

        #endregion

        #region unit conversion
        //
        // fromTB_TextChanged
        //
        private void fromTB_TextChanged(object sender, EventArgs e)
        {
            toTB.Text = getToTBText(fromTB.Text);
        }
        //
        // from textbox lost focus
        //
        private void fromTB_LostFocus(object sender, EventArgs e)
        {
            if (fromTB.Text == "")
            {
                fromTB.Text = "Enter value";
                fromTB.ForeColor = SystemColors.GrayText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte) (0)));
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
                fromTB.Text = "";
                toTB.Text = "";
                fromTB.ForeColor = SystemColors.ControlText;
                fromTB.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            }
        }
        //
        // fromCB_SelectedIndexChanged
        //
        private void fromCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = typeCB.SelectedIndex + (typeCB.SelectedIndex < 0).GetHashCode();
            if (typeCB.SelectedIndex < 0) typeCB.SelectedIndex = 0;
            invert_unit.Enabled = (fcb[type].SelectedIndex != tcb[type].SelectedIndex);
            toTB.Text = getToTBText(fromTB.Text);
            if (toTB.Text == "Invalid input number. Please try again" && fromTB.ForeColor == SystemColors.GrayText)
            {
                toTB.Text = "";
            }
        }
        //
        // toCB_SelectedIndexChanged
        //
        private void toCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fromCB_SelectedIndexChanged(sender, e);
        }
        //
        // typeCB_SelectedIndexChanged
        //
        private void typeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            writeToSubkey(unitConversionTSMI, "UnitConversion");
            invert_unit.Enabled = true;
            for (int i = 0; i < typeCB.Items.Count; i++)
            {
                fcb[i].Visible = (typeCB.SelectedIndex == i);
                tcb[i].Visible = (typeCB.SelectedIndex == i);
                if (typeCB.SelectedIndex == i) fcb[i].SelectedIndex = 0;
                tcb[00].SelectedIndex = 2;
                tcb[01].SelectedIndex = 6;
                tcb[02].SelectedIndex = 4;
                tcb[03].SelectedIndex = 9;
                tcb[04].SelectedIndex = 4;
                tcb[05].SelectedIndex = 4;
                tcb[06].SelectedIndex = 0;
                fcb[06].SelectedIndex = 1;
                tcb[07].SelectedIndex = 5;
                tcb[08].SelectedIndex = 5;
                tcb[09].SelectedIndex = 3;
                tcb[10].SelectedIndex = 6;
            }
            fromCB_SelectedIndexChanged(sender, e);
        }
        //
        // typeCB_MouseDown
        //
        private void typeCB_MouseDown(object sender, MouseEventArgs e)
        {
            prcmdkey = false;
            fromCB_SelectedIndexChanged(sender, e);
        }
        //
        // auto calculate
        //
        private string getToTBText(string fromstr)
        {
            string ret_string = "";
            if (misc.isNumber(fromstr))
            {
                double db = double.Parse(fromstr);
                int type = typeCB.SelectedIndex;
                if (typeCB.SelectedIndex != 6)
                    db *= misc.getRate(type, fcb[type].SelectedIndex, tcb[type].SelectedIndex);
                else
                    db = misc.getTemperature(fcb[type].SelectedIndex, tcb[type].SelectedIndex, db);

                if (digitGroupingTSMI.Checked)
                    if (("" + db).IndexOf("E") <= 0) ret_string = misc.grouping("" + db);
                    else ret_string = "" + db;
                else
                    ret_string = misc.de_group("" + db);

                if (double.Parse(fromTB.Text) < 0 && typeCB.SelectedIndex != 6)
                    ret_string = "The input number must be a positive";
            }
            else ret_string = "Invalid input number. Please try again";
            if ((fromstr == "Enter value" && fromTB.ForeColor == SystemColors.GrayText) || fromstr == "")
            {
                ret_string = "";
            }
            return ret_string;
        }
        //
        // nut doi gia tri 2 combobox
        //
        private void invert_unit_Click(object sender, EventArgs e)
        {
            toTB.Text = getToTBText(fromTB.Text);
            int temp = fcb[typeCB.SelectedIndex].SelectedIndex;
            int type = typeCB.SelectedIndex;
            fcb[type].SelectedIndex = tcb[type].SelectedIndex;
            tcb[type].SelectedIndex = temp;
            fromLB.Focus();
        }

        #endregion

        #region history controls

        private void historyTSMI_Click(object sender, EventArgs e)
        {
            formWithHistory();
        }

        private void clearHistoryBT_EnabledChanged(object sender, EventArgs e)
        {
            clearHistoryTSMI.Enabled = clearHistoryBT.Enabled;
            clearHistoryCTMN.Enabled = clearHistoryTSMI.Enabled;
        }

        private void currentCellToNull()
        {
            upBT.Enabled = false;
            dnBT.Enabled = false;
        }

        //int historycount = 0;

        private void historyDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            prcmdkey = true;
            string expression = (string)historyDGV[0, e.RowIndex].Value;
            int count = historyDGV.Rows.Count;
            clearHistoryBT.Enabled = (count > 1 || (count == 1 && historyDGV[0, 0].Value != null));
            copyHistoryTSMI.Enabled = (expression != null);
            str = "" + misc.Evaluate(expression);
            if (str == "") str = "0";
            upBT.Enabled = (e.RowIndex >= 1 && expression != null);
            dnBT.Enabled = (e.RowIndex < historyDGV.Rows.Count - 1 && expression != null);
            displayToScreen();
        }

        private void clearHistoryBT_Click(object sender, EventArgs e)
        {
            clear_history();
        }

        private void clear_history()
        {
            int count = historyDGV.Rows.Count;
            while (count > 1)
            {
                historyDGV.Rows.RemoveAt(0);
                count = historyDGV.Rows.Count;
            } 
            if (count == 1 && historyDGV[0, 0].Value != null)
            {
                historyDGV.Rows.RemoveAt(0);
            } 
            historyDGV.CurrentCell = null;
            clearHistoryBT.Enabled = false;
            upBT.Enabled = false;
            dnBT.Enabled = false;
        } 

        private void upBT_Click(object sender, EventArgs e)
        {
            object temp = historyDGV[0, historyDGV.CurrentCell.RowIndex].Value;
            historyDGV[0, historyDGV.CurrentCell.RowIndex].Value = historyDGV[0, historyDGV.CurrentCell.RowIndex - 1].Value;
            historyDGV[0, historyDGV.CurrentCell.RowIndex - 1].Value = temp;
            historyDGV.CurrentCell = historyDGV[0, historyDGV.CurrentCell.RowIndex - 1];
            upBT.Enabled = (historyDGV.CurrentCell.RowIndex >= 1);
            dnBT.Enabled = (historyDGV.CurrentCell.RowIndex <= 2);
        }

        private void dnBT_Click(object sender, EventArgs e)
        {
            int rowid = historyDGV.CurrentCell.RowIndex;
            object temp = historyDGV[0, rowid].Value;
            historyDGV[0, rowid].Value = historyDGV[0, rowid + 1].Value;
            historyDGV[0, rowid + 1].Value = temp;
            historyDGV.CurrentCell = historyDGV[0, rowid + 1];
            rowid++;
            upBT.Enabled = (rowid >= 1);
            dnBT.Enabled = (rowid < historyDGV.Rows.Count - 1);
            //if (rowid < 3) dnBT.Enabled = (historyDGV[0, rowid + 1].Value != null);
        }

        private void copyHistoryTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText("" + historyDGV.CurrentCell.Value);
            }
            catch { }
        }

        #endregion

        #region programmer mode
        
        string binnum64 = "0000000000000000000000000000000000000000000000000000000000000000";   // 64 số 0
        string binnum = "", decnum = "", octnum = "", hexnum = "";        /// <summary>
        /// sự kiện scr_lb text change thông qua thao tác thay đổi giá trị trên panel
        /// </summary>
        private void panelToScreen()
        {
            binnum64 = "";
            for (int i = 15; i >= 0; i--) binnum64 += bit_digit[i].Text;
            binnum = binnum64;
            while (binnum[0] == '0' && binnum.Length > 1)
                binnum = binnum.Substring(1, binnum.Length - 1);
            decnum = ConvertNumber.other_to_dec(binnum, 1);
            octnum = ConvertNumber.bin_to_other(binnum, 2);
            hexnum = ConvertNumber.bin_to_other(binnum, 4);
            if (binRB.Checked) str = binnum;
            if (octRB.Checked) str = octnum;
            if (decRB.Checked) str = decnum;
            if (hexRB.Checked) str = hexnum;
            confirm_num = true;
            prcmdkey = true;
            displayToScreen();
        }
        /// <summary>
        /// sự kiện scr_lb text change thông qua thao tác nhập số
        /// </summary>
        private void screenToPanel()
        {
            if (binRB.Checked)
            {
                binnum = str;
                decnum = ConvertNumber.other_to_dec(binnum, 1);
                octnum = ConvertNumber.dec_to_other(decnum, 2);
                hexnum = ConvertNumber.dec_to_other(decnum, 4);
            }
            if (octRB.Checked)
            {
                octnum = str;
                decnum = ConvertNumber.other_to_dec(str, 2);
                binnum = ConvertNumber.dec_to_other(decnum, 1);
                hexnum = ConvertNumber.dec_to_other(decnum, 4);
            }
            if (decRB.Checked)
            {
                decnum = str;
                binnum = ConvertNumber.dec_to_other(decnum, 1);
                octnum = ConvertNumber.dec_to_other(decnum, 2);
                hexnum = ConvertNumber.dec_to_other(decnum, 4);
            }
            if (hexRB.Checked)
            {
                hexnum = str;
                decnum = ConvertNumber.other_to_dec(hexnum, 4);
                binnum = ConvertNumber.dec_to_other(decnum, 1);
                octnum = ConvertNumber.dec_to_other(decnum, 2);
            }
            int len = binnum.Length;
            binnum64 = binnum;
            for (int i = 0; i < 64 - len; i++) binnum64 = "0" + binnum64;
            for (int i = 0; i < 16; i++)
            {
                bit_digit[i].Text = binnum64.Substring(64 - (i + 1) * 4, 4);
            }
            displayToScreen();
        }

        private void basecheckchange()
        {
            baseRBCheckedChanged();
            panelToScreen(); 
        }
        /// <summary>
        /// sang hệ khác
        /// </summary>
        private void baseRB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked) basecheckchange();
        }

        private void bit_digit_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            int index_bit = -1;
            if (location >= 03 && location <= 10) index_bit = 0;
            if (location >= 11 && location <= 18) index_bit = 1;
            if (location >= 19 && location <= 26) index_bit = 2;
            if (location >= 27 && location <= 34) index_bit = 3;

            if (index_bit >= 0)
            {
                if (lb.Text[index_bit] == '0')
                {
                    bit_digit[bd_tabindex].Text = bit_digit[bd_tabindex].Text.Remove(index_bit, 1);
                    bit_digit[bd_tabindex].Text = bit_digit[bd_tabindex].Text.Insert(index_bit, "1");
                    goto breakpoint;
                }
                if (lb.Text[index_bit] == '1')
                {
                    bit_digit[bd_tabindex].Text = bit_digit[bd_tabindex].Text.Remove(index_bit, 1);
                    bit_digit[bd_tabindex].Text = bit_digit[bd_tabindex].Text.Insert(index_bit, "0");
                }
            }
            breakpoint: confirm_num = true;
            panelToScreen();
        }

        int location = 0, bd_tabindex = 0;
        private void bit_digit_MouseMove(object sender, MouseEventArgs e)
        {
            location = e.X;
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
            RadioButton rb = (RadioButton)sender;
            int tabindex = rb.TabIndex;
            int bd = (int)ConvertNumber.powan(2, tabindex - 2 * (tabindex > 3).GetHashCode());
            if (rb.Checked)
            {
                for (int i = 0; i < bd; i++) bit_digit[i].Visible = true;
                for (int i = bd; i < 16; i++) bit_digit[i].Visible = false;
                for (int i = 0; i < tabindex; i++) flagpoint[i].Visible = true;
                for (int i = tabindex; i < 6; i++) flagpoint[i].Visible = false;
            }
        }
        /// <summary>
        /// nút not
        /// </summary>
        private void notBT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                int notEachGroup = int.Parse(bit_digit[i].Text);
                notEachGroup = 1111 - notEachGroup;
                bit_digit[i].Text = "" + notEachGroup;
                while (bit_digit[i].Text.Length < 4)
                    bit_digit[i].Text = "0" + bit_digit[i].Text;
            }
            panelToScreen();
        }

        private void rotateBT_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == RoLBT) binnum64 = binnum64.Substring(1, 63) + binnum64[0].ToString();
            if (btn == RoRBT) binnum64 = binnum64[63].ToString() + binnum64.Substring(0, 63);
            for (int i = 0; i < 16; i++)
            {
                bit_digit[i].Text = binnum64.Substring(60 - 4 * i, 4);
            }
            panelToScreen();
        }

        private void orBT_Click(object sender, EventArgs e)
        {

        }

        private void XorBT_Click(object sender, EventArgs e)
        {

        }

        private void AndBT_Click(object sender, EventArgs e)
        {

        }

        private void LshBT_Click(object sender, EventArgs e)
        {

        }

        private void RshBT_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region statistics mode
        private void CAD_Click(object sender, EventArgs e)
        {
            clear_statistics();
        }

        private void clear_statistics()
        {
            while (statisticsDGV.Rows.Count > 0) statisticsDGV.Rows.RemoveAt(0);
            //statisticsDGV[0, 0].Value = null;
            countlb.Text = "Count = 0";
            clearDataSetCTMN.Enabled = false;
            clearDatasetTSMI.Enabled = false;
            copyDatasetTSMI.Enabled = false;
        }

        private void AddstaBT_Click(object sender, EventArgs e)
        {
            statistics_add();
        }

        private void sigman_1BT_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count <= 1) return;
            double result = sum(true) - sum(false) * sum(false) / statisticsDGV.Rows.Count;
            result /= (statisticsDGV.Rows.Count - 1);
            result = Math.Sqrt(result);
            str = "" + result;
            displayToScreen();
        }

        private void sigmanBT_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count == 0) return;
            double result = sum(true) - sum(false) * sum(false) / statisticsDGV.Rows.Count; 
            result /= statisticsDGV.Rows.Count;
            result = Math.Sqrt(result);
            str = "" + result;
            displayToScreen();
        }

        private double sum(bool isx2)
        {
            double sum = 0;
            if (statisticsDGV.Rows.Count == 0) return 0;
            for (int i = 0; i < statisticsDGV.Rows.Count; i++)
            {
                double number = double.Parse("" + statisticsDGV[0, i].Value);
                if (isx2) sum += number * number;
                else sum += number;
            }
            return sum;
        }

        private void sigmax2BT_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count == 0) return;
            str = "" + sum(true);
            displayToScreen();
        }

        private void sigmaxBT_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count == 0) return;
            str = "" + sum(false);
            displayToScreen();
        } 

        private void x2cross_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count == 0) return;
            str = "" + (sum(true) / statisticsDGV.Rows.Count);
            displayToScreen();
        }

        private void xcross_Click(object sender, EventArgs e)
        {
            if (statisticsDGV.Rows.Count == 0) return;
            str = "" + (sum(false) / statisticsDGV.Rows.Count);
            displayToScreen();
        }

        double currentvalue;
        private void statisticsDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                currentvalue = double.Parse("" + statisticsDGV[0, e.RowIndex].Value);
            }
            catch { }
            prcmdkey = false;
        }

        private void statisticsDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double temp = double.Parse("" + statisticsDGV[0, e.RowIndex].Value);
            }
            catch (FormatException)
            {
                statisticsDGV[0, e.RowIndex].Value = currentvalue;
            }
            prcmdkey = true;
        }

        private void statisticsDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            statisticsDGV.ReadOnly = statisticsDGV.Rows.Count == 1;
        }
        
        private void statisticsDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            statisticsDGV.ReadOnly = statisticsDGV.Rows.Count == 1;
        }

        private void statisticsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = statisticsDGV.Rows.Count;
            copyDatasetTSMI.Enabled = (count > 1 || (count == 1 && statisticsDGV[0, 0].Value != null));
        }
        #endregion
    }
}