using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;

namespace Calculator
{
    public partial class calc : Form
    {
        public calc()
        {
            InitializeComponent();
            mouseDownEvents();
        }

        #region local varial
        string str = "0", sci_expression = "", sci_expression_ = "";
        bool dot_bl = false, confirm_num = false, prcmdkey = true;
        int pre_bt = -1, pre_oprt = 0, dot = 0;
        double mem_num = 0, num_1 = 0, num_2 = 0, result = 0;
        Code cod = new Code();
        VsaEngine vsa = VsaEngine.CreateEngine();
        #endregion

        #region calc events
        /// <summary>
        /// form được load
        /// </summary>
        private void calc_Load(object sender, EventArgs e)
        {
            calc_Activated(sender, e);
            if (readFromRegistry("Standard")) standardTSMI_Click(sender, e);
            if (readFromRegistry("Scientific")) scientificTSMI_Click(sender, e);
            if (readFromRegistry("Date")) dateCalculationTSMI_Click(sender, e);
            if (readFromRegistry("Unit")) unitConversionTSMI_Click(sender, e);
            if (!basicTSMI.Checked)
                prcmdkey = dtP1.Focused || dtP2.Focused || fromTB.Focused;
            else prcmdkey = true;
        }
        /// <summary>
        /// form được actived
        /// </summary>
        private void calc_Activated(object sender, EventArgs e)
        {
            checkClipboard(false);
            getDecimalSym();
            getGroupSym();
            getMemoryNumber();
        }
        #endregion
        
        /// <summary>
        /// process a command key
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (prcmdkey)
            {
                #region ProcessCmdKey
                int key_hc = keyData.GetHashCode(); // key_hc = key hashcode
                // alt F4 hoac ctrl W
                if (key_hc == 262259 || key_hc == 131159) this.Close();
                // cac phim so ben phai 
                if (key_hc >= 96 && key_hc <= 105) numinput(key_hc - 96);
                // cac phim so ben trai
                if (key_hc >= 48 && key_hc <= 57) numinput(key_hc - 48);
                if (key_hc == 187 | key_hc == 13) equalclicked();   // = hoac enter 
                if (key_hc == 110 | key_hc == 00190) numinput(10);  // .
                if (key_hc == 107 | key_hc == 65723)
                    if (scientificTSMI.Checked) operators_sci(12); else operators(12);  // +
                if (key_hc == 109 | key_hc == 00189)
                    if (scientificTSMI.Checked) operators_sci(13); else operators(13);  // -
                if (key_hc == 106 | key_hc == 65592)
                    if (scientificTSMI.Checked) operators_sci(14); else operators(14);  // *
                if (key_hc == 111 | key_hc == 00191)
                    if (scientificTSMI.Checked) operators_sci(15); else operators(15);  // /
                if (key_hc == 120) numinput(11);        // F9
                if (key_hc == 082) math_func("inv");    // R
                if (key_hc == 008) backspaceclicked();  // Backspace
                if (key_hc == 027) clear_num(true);     // Esc
                if (key_hc == 112) helptopics();    //F1
                // cac to hop phim 
                if (key_hc == 262193) stdLoad();  // alt 1
                if (key_hc == 262194) sciLoad();  // alt 2
                if (key_hc == 131140) mem_process(1);   // ctrl D
                if (key_hc == 131149) mem_process(2);   // ctrl M
                if (key_hc == 131155) mem_process(3);   // ctrl S
                if (key_hc == 131139) copyCommand();    // ctrl C
                if (key_hc == 131157) exFunc(unitConversionTSMI, unitconvGB, 615);
                if (key_hc == 131141) exFunc(dateCalculationTSMI, datecalcGB, 645);
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
                if (key_hc == 131158)  // ctrl V
                {
                    if (cod.IsNumber(Clipboard.GetText().Trim()))
                        str = Clipboard.GetText().Trim();
                    displayToScreen();
                }  
                #endregion
            }

            return prcmdkey;
        }

        #region Cac ham duoc su dung nhieu lan
        /// <summary>
        /// form standard
        /// </summary>
        private void stdLoad()
        {
            if (!standardTSMI.Checked)
            {
                standardTSMI.Checked = true;
                scientificTSMI.Checked = false;
                InitializedForm();
                hideComponent(false);
                clear_num(false);
                writeToRegistry(standardTSMI);
            }
        }
        /// <summary>
        /// form scientific
        /// </summary>
        private void sciLoad()
        {
            if (!scientificTSMI.Checked)
            {
                standardTSMI.Checked = false;
                scientificTSMI.Checked = true;
                ScientificLoad();
                datecalcGB.Visible = false;
                unitconvGB.Visible = false;
                dateCalculationTSMI.Checked = false;
                unitConversionTSMI.Checked = false;
                basicTSMI.Checked = true;
                clear_num(true);
                scientificEvents();
            }
            writeToRegistry(scientificTSMI);
        }
        /// <summary>
        /// lưu các thuộc tính checked của 1 tsmi vào registry
        /// </summary>
        private void writeToRegistry(ToolStripMenuItem tsmi)
        {
            RegistryKey regkey = Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\Calculator", true);
            if (tsmi == standardTSMI)
            {
                regkey.SetValue("Standard", (int) 1);
                regkey.SetValue("Scientific", (int) 0);
                regkey.SetValue("Basic", (int) 1);
                regkey.SetValue("Date", (int) 0);
                regkey.SetValue("Unit", (int) 0);
            }
            if (tsmi == scientificTSMI)
            {
                regkey.SetValue("Standard", (int) 0);
                regkey.SetValue("Scientific", (int) 1);
                regkey.SetValue("Basic", (int) 1);
                regkey.SetValue("Date", (int) 0);
                regkey.SetValue("Unit", (int) 0);
            }
            if (tsmi == unitConversionTSMI)
            {
                regkey.SetValue("Standard", (int) 1);
                regkey.SetValue("Scientific", (int) 0);
                regkey.SetValue("Basic", (int) 0);
                regkey.SetValue("Date", (int) 0);
                regkey.SetValue("Unit", (int) 1);
            }
            if (tsmi == dateCalculationTSMI)
            {
                regkey.SetValue("Standard", (int) 1);
                regkey.SetValue("Scientific", (int) 0);
                regkey.SetValue("Basic", (int) 0);
                regkey.SetValue("Date", (int) 1);
                regkey.SetValue("Unit", (int) 0);
            }
            if (tsmi == basicTSMI)
            {
                regkey.SetValue("Standard", (int) 1);
                regkey.SetValue("Scientific", (int) 0);
                regkey.SetValue("Basic", (int) 1);
                regkey.SetValue("Date", (int) 0);
                regkey.SetValue("Unit", (int) 0);
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
                regkey.SetValue("Basic", 1, RegistryValueKind.DWord);
                regkey.SetValue("Date", 0, RegistryValueKind.DWord);
                regkey.SetValue("Unit", 0, RegistryValueKind.DWord);
                regkey.SetValue("MemoryNumber", (string) "0", RegistryValueKind.String);
            }
            if ((int) regkey.GetValue(name) == 1) ret = true;
            regkey.Close();
            return ret;
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
        /// kiểm tra nội dung trong clipboard
        /// </summary>
        private void checkClipboard(bool chk_clb)
        {
            string clbstr = Clipboard.GetText();
            if (cod.IsNumber(clbstr)) pasteTSMI.Enabled = true;
            else pasteTSMI.Enabled = false;
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
        /// <summary>
        /// lấy kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        private string getGroupSym()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Control Panel\\International");
            return (string)reg.GetValue("sThousand");
        }
        /// <summary>
        /// 2 tính năng date calculation và unit conversion
        /// </summary>
        private void exFunc(ToolStripMenuItem tsmi, GroupBox gb, int width)
        {
            if (!tsmi.Checked)
            {
                stdLoad();
                this.Size = new System.Drawing.Size(width, 340);
                datecalcGB.Visible = false;
                unitconvGB.Visible = false;
                gb.Visible = true;
                this.menuStrip1.Size = new System.Drawing.Size(width, 24);
                unitConversionTSMI.Checked = false;
                dateCalculationTSMI.Checked = false;
                basicTSMI.Checked = false;
                tsmi.Checked = true;
                if (tsmi == unitConversionTSMI) typeCB.SelectedIndex = 0;
                if (tsmi == dateCalculationTSMI) cal_method.SelectedIndex = 0;
                writeToRegistry(tsmi);
            }
		}
        /// <summary>
        /// ẩn các control của form scientific
        /// </summary>
        private void hideComponent(bool visi)
        {
            #region Hide scientific functions
            angleGB.Visible = visi;
            inv_bt.Visible = visi;
            sin_bt.Visible = visi;
            cos_bt.Visible = visi;
            tan_bt.Visible = visi;
            sinh_bt.Visible = visi;
            cosh_bt.Visible = visi;
            tanh_bt.Visible = visi;
            _10x_bt.Visible = visi;
            _3vx_bt.Visible = visi;
            nvx_bt.Visible = visi;
            log_bt.Visible = visi;
            ln_bt.Visible = visi;
            mod_bt.Visible = visi;
            exp_bt.Visible = visi;
            pi_bt.Visible = visi;
            fe_bt.Visible = visi;
            dms_bt.Visible = visi;
            int_bt.Visible = visi;
            x2_bt.Visible = visi;
            x3_bt.Visible = visi;
            xn_bt.Visible = visi;
            btFactorial.Visible = visi;
            open_bracket.Visible = visi;
            close_bracket.Visible = visi;
            nonameTB.Visible = visi;
            str = "0";
            mem_lb.BringToFront();
            #endregion
        }
        /// <summary>
        /// copy - ctrl c
        /// </summary>
        private void copyCommand()
        {
            try
            {
                Clipboard.SetText(scr_lb.Text);
                if (Clipboard.GetText() != "0") pasteTSMI.Enabled = true;
                else pasteTSMI.Enabled = false;
            }
            catch (Exception)
            {
                pasteTSMI.Enabled = false;
            }
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
        private void math_func(string func_name)
        {
            double result = 0, inp_num = 0;
            try {
                inp_num = double.Parse(scr_lb.Text);
            }
            catch (Exception) { goto jump; }
            if (func_name == "inv")
            {
                if (inp_num != 0) 
                    { result = 1 / inp_num; str = "" + inp_num; }
                else 
                    { str = "Infinity"; goto breakpoint; }
            }
            if (func_name == "sqrt")
            {
                if (inp_num >= 0)
                {
                    result = Math.Sqrt(inp_num);
                    str = result.ToString();
                }
                else { str = "Err"; goto breakpoint; }
            }
            if (func_name == "sin")
            {
                if (deg_rb.Checked) result = Math.Sin(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Sin(inp_num);
                if (gra_rb.Checked) result = Math.Sin(inp_num / 200 * Math.PI);
            }
            if (func_name == "cos")
            {
                if (deg_rb.Checked) result = Math.Cos(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Cos(inp_num);
                if (gra_rb.Checked) result = Math.Cos(inp_num / 200 * Math.PI);
            }
            if (func_name == "tan")
            {
                if (deg_rb.Checked) result = Math.Tan(inp_num / 180 * Math.PI);
                if (rad_rb.Checked) result = Math.Tan(inp_num);
                if (gra_rb.Checked) result = Math.Tan(inp_num / 200 * Math.PI);
            }
            if (func_name == "ln ") result = Math.Log(inp_num, Math.E);
            if (func_name == "fac")
            {
                if (inp_num - (int)inp_num != 0)
                {
                    str = "Err";
                    goto breakpoint;
                }
                else
                {
                    result = 1;
                    for (int i = 1; i <= (int)inp_num; i++) result *= i;
                }
            }
            if (func_name == "exp") result = Math.Exp(inp_num);
            if (func_name == "x^2") result = inp_num * inp_num;
            if (func_name == "x^3") result = inp_num * inp_num * inp_num;
            if (func_name == "3vx")
            {
                result = Math.Exp(Math.Log10(inp_num) / 3 / Math.Log10(Math.E));
            }
            if (func_name == "nvx") result = inp_num * inp_num * inp_num;
            if (func_name == "10n") result = Math.Pow(10, inp_num);
            if (func_name == "log") result = Math.Log10(inp_num);
            if (func_name == "sinh") result = Math.Sinh(inp_num);
            if (func_name == "cosh") result = Math.Cosh(inp_num);
            if (func_name == "tanh") result = Math.Tanh(inp_num);

            if ("" + result != "NaN") str = "" + result;
            else str = "Err";

            breakpoint: if (str.Length > 16) str = str.Substring(0, 16);
            displayToScreen();
            jump: confirm_num = true;
        }
        /// <summary>
        /// nút backspace
        /// </summary>
        private void backspaceclicked()
        {
            if (!confirm_num)
            {
                if (str.Length == 2 & str.StartsWith("-"))
                {
                    str = "0";
                    scr_lb.Text = "0";
                    confirm_num = true;
                }
                if (str.Length > 1)
                {
                    if (!str.EndsWith(getDecimalSym()))
                    {
                        str = str.Substring(0, str.Length - 1);
                        displayToScreen();
                    }
                    else
                    {
                        //scr_lb.Text = input_str.Substring(0, input_str.Length - 2);
                        str = str.Substring(0, str.Length - 1);
                        dot = 0;
                        dot_bl = false;
                    }
                }
                else
                {
                    str = "0";
                    scr_lb.Text = "0";
                    confirm_num = true;
                }
                //if (input_str[input_str.Length - 1] == ',')
                //{
                //    scr_lb.Text = input_str.Substring(0, input_str.Length - 2);
                //    input_str = input_str.Substring(0, input_str.Length - 1);
                //}
            }
            else { str = "0"; scr_lb.Text = "0"; }

            if (scr_lb.Text.EndsWith(getDecimalSym()))
            {
                scr_lb.Text = scr_lb.Text.Substring(0, scr_lb.Text.Length - 1);
            }
            //else input_str = "0";
            //scr_lb.Text = input_str;
            pre_bt = 22;
        }
        /// <summary>
        /// nút bằng
        /// </summary>
        private void equalclicked()
        {
            // thay str = 2,3 bằng str_ = 2.3
            string str_ = str;
            if (str.IndexOf('.') > 0) str_.Replace(getDecimalSym(), ".");

            confirm_num = true;
            if (standardTSMI.Checked)
            {
                if (pre_oprt != 0)
                {
                    num_1 = result;
                    num_2 = double.Parse(str);
                }
                if (pre_oprt >= 12) num_2 = double.Parse(scr_lb.Text);
                else
                    try
                    {
                        result = double.Parse(str);
                    }
                    catch (Exception) { goto jump; }
                if (pre_oprt == 12) result = num_1 + num_2;
                if (pre_oprt == 13) result = num_1 - num_2;
                if (pre_oprt == 14) result = num_1 * num_2;
                if (pre_oprt == 15) result = num_1 / num_2;
                //if (result.ToString() == "Infinity") input_str = "Err";
                //else 
                str = "" + result; 
            }
            else
            {
                str = Eval.JScriptEvaluate(sci_expression + str_, vsa).ToString();
                sci_expression = str;
            }
            displayToScreen();
            pre_oprt = 0;
            pre_bt = 16;
            num_1 = result;
            num_2 = 0;
            sci_expression = "";
            sci_expression_ = "";
            jump: operator_lb.Visible = false;
        }
        /// <summary>
        /// nút xoá số
        /// </summary>
        private void clear_num(bool ce_bt)
        {
            str = "0";
            scr_lb.Text = "0";
            dot = 0;
            dot_bl = false;
            confirm_num = false;
            if (ce_bt)
            {
                sci_expression = "";
                num_1 = 0;
                num_2 = 0;
                result = 0;
                pre_bt = -1;
                pre_oprt = 0;
                operator_lb.Visible = false;
            }
        }
        /// <summary>
        /// hiển thị số lên màn hình
        /// </summary>
        private void displayToScreen()
        {
            if (str.IndexOf(getGroupSym()) > 0 && cod.IsNumber(str) && str.IndexOf("Inf") < 0)
            {
                if (digitGroupingTSMI.Checked)
                    scr_lb.Text = str;
                else
                    scr_lb.Text = cod.de_group(str);
            }
            if (str.IndexOf(getGroupSym()) < 0 && cod.IsNumber(str) && str.IndexOf("Inf") < 0)
            {
                if (digitGroupingTSMI.Checked)
                    scr_lb.Text = cod.grouping(str);
                else
                    scr_lb.Text = str;
            }
            if (str.IndexOf("Inf") >= 0 || str.IndexOf("Err") >= 0) scr_lb.Text = str;
        }
        /// <summary>
        /// nhập số
        /// </summary>
        private void numinput(int index)
        {
            if (confirm_num)
            {
                dot = 0;
                // bien dot_bl kiem tra viec them ',' + so vao khi 1 so duoc bam
                dot_bl = false; 
                pre_bt = -1;
            }
            if (index < 10)
            {
                if (confirm_num) str = "0";
                if ((dot == 0 || dot == 2) && (str.Length <= 15))
                    if (str != "0") str += index.ToString();
                    else str = index.ToString();
                if ((dot == 1) && (str.Length <= 15))
                {
                    if (!dot_bl) str += /*getDecimalSym()*/"." + index.ToString();
                    else str += index.ToString();
                    dot_bl = true;
                }
                confirm_num = false;
            }
            if (index == 10)
            {
                if (confirm_num) str = "0";
                if ((pre_bt != 10) && (dot < 2)) dot++;
                confirm_num = false;
            }
            if (index == 11)
            {
                if ((str != "0") || (dot == 1))
                    if (str[0] != '-') str = "-" + str;
                    else str = str.Substring(1, str.Length - 1);
            }
            pre_bt = index;
            //if (digitGroupingTSMI.Checked)
            //    input_str = cod.grouping(input_str);
            //else
            //    input_str = cod.de_group(input_str);
            displayToScreen();
        }
        /// <summary>
        /// các toán tử +-*/ của form standard
        /// </summary>
        /// <param name="index">chỉ số của toán tử</param>
        private void operators(int index)
        {
            if (cod.IsNumber(scr_lb.Text))
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
                        num_2 = double.Parse(str);
                        if (pre_oprt == 12) result = num_1 + num_2;
                        if (pre_oprt == 13) result = num_1 - num_2;
                        if (pre_oprt == 14) result = num_1 * num_2;
                        if (pre_oprt == 15) result = num_1 / num_2;
                    }

                str = "" + result;
                displayToScreen();
                pre_oprt = index;
                pre_bt = index;
                if (index == 12) operator_lb.Text = "+";
                if (index == 13) operator_lb.Text = "-";
                if (index == 14) operator_lb.Text = "*";
                if (index == 15) operator_lb.Text = "/";
                operator_lb.Visible = true;
            }
        }
        /// <summary>
        /// các toán tử của form scientific
        /// </summary>
        /// <param name="index">chỉ số của toán tử</param>
        private void operators_sci(int index)
        {
            string str_ = str;
            if (str.IndexOf('.') > 0) str_.Replace(getDecimalSym(), ".");

            if (cod.IsNumber(scr_lb.Text))
            {
                if (index == 12) operator_lb.Text = "+";
                if (index == 13) operator_lb.Text = "-";
                if (index == 14) operator_lb.Text = "*";
                if (index == 15) operator_lb.Text = "/";
                if (pre_oprt != 0)
                {
                    if (pre_bt < 12 || pre_bt > 15)
                    {
                        sci_expression += str_;
                        // sau = +-
                        if (index <= 13)
                        {
                            str = Eval.JScriptEvaluate(sci_expression, vsa).ToString();
                            sci_expression_ = "";
                        }
                        // sau = */
                        if (index > 13 && index != 30 && index != 34)
                        {
                            sci_expression_ = str_;
                            str = Eval.JScriptEvaluate(sci_expression_, vsa).ToString();
                            sci_expression_ += operator_lb.Text;
                        }
                        // sau = x^y
                        if (index == 30)
                        {
                            if (pre_oprt == 12 || pre_oprt == 13)   // trc = +-
                            {
                                num_1 = double.Parse(str);
                            }
                            if (pre_oprt == 14 || pre_oprt == 15)   // trc = */
                            {
                                num_1 = double.Parse(str);
                            }
                        }
                        if (index == 34)    // sau = x^1/y
                        {

                        }
                        if (index != 30 && index != 34) sci_expression += operator_lb.Text;
                    }
                    else
                    {
                        sci_expression = sci_expression.Substring(0,
                            sci_expression.Length - 1) + operator_lb.Text;
                    }

                }
                else
                {
                    if (index == 14 || index == 15) 
                        sci_expression_ = str_ + operator_lb.Text;
                    sci_expression = str + operator_lb.Text;
                }
                displayToScreen();
                pre_bt = index;
                operator_lb.Visible = true;
            }

            displayToScreen();
            pre_oprt = index;            
            confirm_num = true;
        }
        /// <summary>
        /// mở file help
        /// </summary>
        private static void helptopics()
        {
            try
            {
                System.Diagnostics.Process.Start("calc.chm");
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Lỗi không tìm thấy file. Hãy kiểm tra lại.", "Có lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Cac phim so duoc bam
        private void num0_Click(object sender, EventArgs e)
        {
            numinput(0);
        }

        private void num1_Click(object sender, EventArgs e)
        {
            numinput(1);
        }

        private void num2_Click(object sender, EventArgs e)
        {
            numinput(2);
        }

        private void num3_Click(object sender, EventArgs e)
        {
            numinput(3);
        }

        private void num4_Click(object sender, EventArgs e)
        {
            numinput(4);
        }

        private void num5_Click(object sender, EventArgs e)
        {
            numinput(5);
        }

        private void num6_Click(object sender, EventArgs e)
        {
            numinput(6);
        }

        private void num7_Click(object sender, EventArgs e)
        {
            numinput(7);
        }

        private void num8_Click(object sender, EventArgs e)
        {
            numinput(8);
        }

        private void num9_Click(object sender, EventArgs e)
        {
            numinput(9);
        }

        private void btdot_Click(object sender, EventArgs e)
        {
            numinput(10);
        }
        //
        // nut doi dau
        //
        private void doidau_Click(object sender, EventArgs e)
        {
            numinput(11);
        }
        #endregion

        #region Cac nut bieu dien cac phep tinh
        private void add_Click(object sender, EventArgs e)
        {
            if (scientificTSMI.Checked) operators_sci(12); else operators(12);  // +
        }

        private void dev_Click(object sender, EventArgs e)
        {
            if (scientificTSMI.Checked) operators_sci(13); else operators(13);  // -
        }

        private void mul_Click(object sender, EventArgs e)
        {
            if (scientificTSMI.Checked) operators_sci(14); else operators(14);  // *
        }

        private void div_Click(object sender, EventArgs e)
        {
            if (scientificTSMI.Checked) operators_sci(15); else operators(15);  // /
        }
        #endregion

        #region Cac nut ghi nhan ket qua
        //
        // nut bang
        //
        private void equal_Click(object sender, EventArgs e)
        {
            equalclicked();
        }
        //
        // nut nghich dao
        //
        private void inv_Click(object sender, EventArgs e)
        {
            math_func("inv");
            pre_bt = 17;
        }
        //
        // nut sin
        //
        private void sin_bt_Click(object sender, EventArgs e)
        {
            math_func("sin");
            pre_bt = 28;
        }
        //
        // nut cos
        //
        private void cos_bt_Click(object sender, EventArgs e)
        {
            math_func("cos");
            pre_bt = 29;
        }
        //
        // nut tan
        //
        private void tan_bt_Click(object sender, EventArgs e)
        {
            math_func("tan");
            pre_bt = 30;
        }
        //
        // nut ln
        //
        private void ln_bt_Click(object sender, EventArgs e)
        {
            math_func("ln ");
            pre_bt = 31;
        }
        //
        // nut log
        //
        private void btFactorial_Click(object sender, EventArgs e)
        {
            math_func("fac");
            pre_bt = 32;
        }
        //
        // nut exp
        //
        private void exp_bt_Click(object sender, EventArgs e)
        {
            math_func("exp");
            pre_bt = 33;
        }
        //
        // nut khai can
        //
        private void sqrtnumstore_Click(object sender, EventArgs e)
        {
            math_func("sqrt");
            pre_bt = 19;
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
            scr_lb.Text = str;
            pre_bt = 18;
        }


        private void pi_bt_Click(object sender, System.EventArgs e)
        {
            confirm_num = true;
            str = "" + Math.PI;
            scr_lb.Text = str;
        }

        private void dms_bt_Click(object sender, System.EventArgs e)
        {

        }

        private void sinh_bt_Click(object sender, System.EventArgs e)
        {
            math_func("sinh");
        }

        private void cosh_bt_Click(object sender, System.EventArgs e)
        {
            math_func("cosh");
        }

        private void tanh_bt_Click(object sender, System.EventArgs e)
        {
            math_func("tanh");
        }

        private void log_bt_Click(object sender, System.EventArgs e)
        {
            math_func("log");
        }

        private void x3_bt_Click(object sender, System.EventArgs e)
        {
            math_func("x^3");
        }

        private void xn_bt_Click(object sender, System.EventArgs e)
        {
            operators_sci(30);
        }

        private void x2_bt_Click(object sender, System.EventArgs e)
        {
            math_func("x^2");
        }

        private void _10x_bt_Click(object sender, System.EventArgs e)
        {
            math_func("10n");
        }

        private void _3vx_bt_Click(object sender, System.EventArgs e)
        {
            math_func("3vx");
        }

        private void nvx_bt_Click(object sender, System.EventArgs e)
        {
            operators_sci(34);
        }

        #endregion

        #region Xu ly so nhap
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
        #endregion

        #region Xu ly so tren bo nho
        //
        // nut xoa bo nho
        //
        private void memclear_Click(object sender, EventArgs e)
        {
            mem_num = 0;
            mem_lb.Visible = false;
            confirm_num = true;
            setMemoryNumber();
        }
        //
        // nut goi so trong bo nho ra man hinh
        //
        private void memrecall_Click(object sender, EventArgs e)
        {
            str = mem_num.ToString();
            displayToScreen();
            confirm_num = true;
            setMemoryNumber();
        }
        //
        // nut cong so them tren man hinh bo nho
        //
        private void madd_Click(object sender, EventArgs e)
        {
            mem_process(1);
        }
        //
        // nut bot di so tren man hinh bo nho
        //
        private void mmul_Click(object sender, EventArgs e)
        {
            mem_process(2);
        }
        //
        // nut luu so tren man hinh bo nho
        //
        private void mstore_Click(object sender, EventArgs e)
        {
            mem_process(3);
        }
        #endregion

        #region Cac menu item duoc bam
        private void standardTSMI_Click(object sender, EventArgs e)
        {
            stdLoad();
            prcmdkey = true;
        }

        private void scientificTSMI_Click(object sender, EventArgs e)
        {
            sciLoad();
        }

        private void digitGroupingTSMI_Click(object sender, EventArgs e)
        {
            digitGroupingTSMI.Checked = !digitGroupingTSMI.Checked;
            if (cod.IsNumber(str)) displayToScreen();
            if (digitGroupingTSMI.Checked && cod.IsNumber(fromTB.Text))
            {
                toTB.Text = cod.grouping(toTB.Text); 
            }
            if (!digitGroupingTSMI.Checked && cod.IsNumber(fromTB.Text))
            {
                toTB.Text = cod.de_group(toTB.Text); 
            }
        }

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!basicTSMI.Checked)
            {
                basicTSMI.Checked = true;
                unitConversionTSMI.Checked = false;
                dateCalculationTSMI.Checked = false;
                InitializedForm();
                datecalcGB.Visible = false;
                displayToScreen();
                prcmdkey = true;
                writeToRegistry(basicTSMI);
            }
            else prcmdkey = false;
        }

        private void unitConversionTSMI_Click(object sender, EventArgs e)
        {
            exFunc(unitConversionTSMI, unitconvGB, 615);
        }

        private void dateCalculationTSMI_Click(object sender, EventArgs e)
        {
            //date_cal();
            exFunc(dateCalculationTSMI, datecalcGB, 600);
        }

        private void copyTSMI_Click(object sender, EventArgs e)
        {
            if (cod.IsNumber(str)) Clipboard.SetText(scr_lb.Text);
            pasteTSMI.Enabled = cod.IsNumber(str);
        }

        private void pasteTSMI_Click(object sender, EventArgs e)
        {
            str = Clipboard.GetText().Trim();
            scr_lb.Text = str;
        }

        private void viewClipboardTSMI_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (Clipboard.GetText().Trim() == "") Clipboard.SetText("null");
                MessageBox.Show(Clipboard.GetText(), "Calculator",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Clipboard doesn't contain text", "Calculator",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        #region radiobutton checkedchanged
        private void deg_rb_CheckedChanged(object sender, System.EventArgs e)
        {
            confirm_num = true;
        }

        private void rad_rb_CheckedChanged(object sender, System.EventArgs e)
        {
            confirm_num = true;
        }

        private void gra_rb_CheckedChanged(object sender, System.EventArgs e)
        {
            confirm_num = true;
        } 
        #endregion

        #region date calculation

        private void control_visible(bool tf)
        {
            periodsUD.Visible = !tf;
            dtP2.Visible = tf;
            label5.Visible = tf;
            result2.Visible = tf;
            result1.Visible = tf;
        }

        private void datetimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (autocal_cb.Checked) calculate_bt_Click(sender, e);
        }

        private void datetimePicker2_ValueChanged(object sender, System.EventArgs e)
        {
            datetimePicker1_ValueChanged(sender, e);
        }

        private void datecalc_Load(object sender, EventArgs e)
        {
            cal_method.SelectedIndex = 0;
        }

        private void calculate_bt_Click(object sender, EventArgs e)
        {
            if (cal_method.SelectedIndex == 0)
            {
                result1.Text = "Those two days above are "
                    + cod.differBW2Dates(dtP1, dtP2)
                    + " day(s) difference";
            }
            if (cal_method.SelectedIndex == 1)
            {
                resultDTP.Value = cod.afterAPeriod(dtP1, periodsUD);
            }
        }

        private void cal_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cal_method.SelectedIndex == 0)
            {
                control_visible(true);
                periodsUD.Visible = false;
                label2.Text = "From:";
                label3.Text = "To: ";
                label4.Text = "Difference (days)";
            }

            if (cal_method.SelectedIndex == 1)
            {
                control_visible(false);
                periodsUD.Visible = true;
                label2.Text = "From:";
                label3.Text = "Periods:";
                label4.Text = "The new date is:";
            }

            datetimePicker1_ValueChanged(sender, e);
        }

        private void songayUpDown_ValueChanged(object sender, EventArgs e)
        {
            datetimePicker1_ValueChanged(sender, e);
        }

        private void autocal_cb_CheckedChanged(object sender, EventArgs e)
        {
            datetimePicker1_ValueChanged(sender, e);
        }

        #endregion

        #region unit conversion
        //int f0 = 0, f1 = 0, f2 = 0, t0 = 0, t1 = 0, t2 = 0;
        private void fromTB_TextChanged(object sender, EventArgs e)
        {
            string fromstr = fromTB.Text;
            if (fromstr == "" || (fromstr == "Enter value" && fromTB.ForeColor == SystemColors.GrayText))
            {
                toTB.Text = "0";
                goto loop;
            }
            if (cod.IsNumber(fromstr))
            {
                double db = 0;
                if (typeCB.SelectedIndex == 0)
                {
                    db = double.Parse(fromstr) * cod.GetLengthRate(fromCB0.SelectedIndex, toCB0.SelectedIndex);
                }
                if (typeCB.SelectedIndex == 1 && cod.IsNumber(fromstr))
                {
                    db = cod.GetTemperature(fromCB1.SelectedIndex, toCB1.SelectedIndex, double.Parse(fromstr));
                }

                if (("" + db).IndexOf('E') > 0)
                {
                    toTB.Text = cod.ToDouble(db.ToString());//String.Format("{0:.#}", db);
                }
                else toTB.Text = "" + db;
                if (digitGroupingTSMI.Checked)
                    toTB.Text = cod.grouping(toTB.Text);
                else
                    toTB.Text = cod.de_group(toTB.Text);
            }
            else toTB.Text = "Invalid input number. Please try again";
            loop: /*end event*/;
        }

        private void fromTB_Enter(object sender, EventArgs e)
        {
            if (fromTB.ForeColor == SystemColors.GrayText)
            {
                fromTB.ForeColor = Color.Black;
                fromTB.Text = "";
            }
            prcmdkey = false;
        }

        private void toCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fromTB_TextChanged(sender, e);
        }

        private void fromCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fromTB_TextChanged(sender, e);
        }

        private void typeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fromCB0.Visible = (typeCB.SelectedIndex == 0);
            fromCB1.Visible = (typeCB.SelectedIndex == 1);
            fromCB2.Visible = (typeCB.SelectedIndex == 2);
            toCB0.Visible = (typeCB.SelectedIndex == 0);
            toCB1.Visible = (typeCB.SelectedIndex == 1);
            toCB2.Visible = (typeCB.SelectedIndex == 2);
            fromCB0.SelectedIndex = 0;
            fromCB1.SelectedIndex = 0;
            fromCB2.SelectedIndex = 0;
            toCB0.SelectedIndex = 0;
            toCB1.SelectedIndex = 0;
            toCB2.SelectedIndex = 0;
            fromTB_TextChanged(sender, e);
        }

        #endregion
    }
}