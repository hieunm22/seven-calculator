using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Calculator
{
    /// <summary>
    /// tổng hợp các method
    /// </summary>
    public static class Misc
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        /// <summary>
        /// chia các hàng đơn vị của một số thực thành từng nhóm 3 số
        /// </summary>
        /// <param name="obj">chuỗi cần chia</param>
        /// <returns>1000000000 thành 1.000.000.000</returns>
        public static string Group(object obj)
        {
            if (obj.ToString() == "0") return "0";
            string output = obj.ToString();

            int comma = output.IndexOf(DecimalSeparator);
            if (comma > 0) output = output.Substring(0, comma);	//cắt phần lẻ trước khi chia

            if (output[0] == '-') output = output.Substring(1);	// bỏ dấu âm ở đầu nếu có
            output = Group(output, 3, GroupSeparator);
            if (comma > 0)  //thêm phần lẻ vào chuỗi đã được nhóm
            {
                output += obj.ToString().Substring(comma);
            }
            if (obj.ToString()[0] == '-') output = "-" + output;

            return output;
        }
        /// <summary>
        /// chia các hàng đơn vị của 1 số nguyên thành từng nhóm
        /// </summary>
        /// <param name="input">chuỗi cần chia</param>
        /// <param name="num">số kí tự của từng nhóm</param>
        /// <param name="sep">ký tự phân cách từng nhóm</param>
        public static string Group(object input, int num, string sep)
        {
            string outp = input.ToString();
            int bit = outp.Length - num;
            // them ky tu phan cach nhom vao giua nhom 3 so o xau ket qua
            while (bit > 0)
            {
                outp = outp.Insert(bit, sep);
                bit -= num;
            }
            return outp;
        }
        /// <summary>
        /// kiểm tra xem 1 xâu nhập vào có phải là số hay không
        /// </summary>
        /// <param name="str">chuỗi cần kiểm tra</param>
        public static bool IsNumber(string str)
        {
            try
            {
                double.Parse(str.Trim());
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// lấy kí tự phân cách giữa phần nguyên và phần lẻ của số
        /// </summary>
        private static string getDecimalSeparator()
        {
            return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }
        /// <summary>
        /// kí tự phân cách giữa phần nguyên và phần lẻ của số
        /// </summary>
        public static string DecimalSeparator
        {
            get { return getDecimalSeparator(); }
        }
        /// <summary>
        /// lấy kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        private static string getGroupSeparator()
        {
            return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
        }
        /// <summary>
        /// kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        public static string GroupSeparator
        {
            get { return getGroupSeparator(); }
        }
        /// <summary>
        /// thêm dấu * vào các vị trí thích hợp
        /// </summary>
        /// <param name="expression">biểu thức cần chèn dấu *</param>
        static string InsertMultiplyChar(string expression)
        {
            string result = expression;
            //[a-z]{2,}
            var regEx = new Regex(@"(?<=[\d\)])(?=[a-df-z\(])|(?<=pi)(?=[^\+\-\*\/\\^!\%\ )])|"
                + @"(?<=\))(?=\d)|(?<=[^\/\*\+\-])(?=expert)", RegexOptions.IgnoreCase);//(?=exp)
            result = regEx.Replace(result, "*");
            return result;
        }
        /// <summary>
        /// thêm dấu ngoặc vào các vị trí đặt hàm
        /// 1 + ln2 --> 1 + ln(2)
        /// </summary>
        /// <param name="expression">chuỗi cần thay thế</param>
        static string InsertBracket(string expression)
        {
            string result = expression.Replace(DecimalSeparator, ",");
            var reg = new Regex(@"(-|)([a-z]{2,})([\+-]?\d+,*\d*[eE][\+-]?\d+|[\+-]?\d+,*\d*)");
            var m = reg.Match(result);
            while (m.Success)
            {
                result = result.Replace(m.Value, m.Groups[1].Value + m.Groups[2].Value + "(" + m.Groups[3].Value + ")");
                m = reg.Match(result);
            }
            return result.Replace(",", DecimalSeparator);
        }
        /// <summary>
        /// xoá các dấu ngoặc thừa trong biểu thức
        /// 2 + sqr((((-3)))) --> 2 + sqr(-3)
        /// 2 + ((((3)))) --> 2 + 3
        /// </summary>
        /// <param name="expression">chuỗi cần thay thế</param>
        static string RemoveBracket(string expression)
        {
            // "((-7))" -> xoa bot 1 capacity dau ngoac -> (-7)
            //Regex reg = new Regex(@"\(\(   (.*)   \)\)");    // ((mot_bieu_thuc))
            Regex reg = new Regex(@"\(\(([^\(\)]+)\)\)(\^|!?)");    // ((mot_bieu_thuc))
            expression = expression.Replace(DecimalSeparator, ",");
            Match m = reg.Match(expression);
            while (m.Success)
            {
                expression = expression.Replace(m.Value, m.Value.Substring(1, m.Value.Length - 2));
                //expression = expression.Substring(0, m.Index) + m.Value.Substring(1, m.Value.Length - 2) + expression.Substring(m.Index + m.Value.Length);
                m = reg.Match(expression);
            }
            return expression.Replace(",", DecimalSeparator);
        }
        /// <summary>
        /// thêm/bớt các dấu cách, dấu ngoặc, dấu * vào giữa các số và phép tính cho dễ nhìn
        /// ("2+3   *          4-        5" thành "2 + 3 * 4 - 5")
        /// </summary>
        /// <param name="Expression">chuỗi biểu thức cần chuẩn hoá</param>
        /// <returns>chuỗi biểu thức đã được chuẩn hoá</returns>
        public static string StandardExpression(string Expression)
        {
            string stdStr = Expression.Trim().ToLower();
            //while (stdStr.Contains("  ")) stdStr = stdStr.Replace("  ", " ");
            stdStr = stdStr.Replace("\n", "");
            stdStr = stdStr.Replace(" ", "");
            stdStr = stdStr.Replace("yroot", " yroot ");     //√
            stdStr = stdStr.Replace("mod", " mod ").Replace("%", " mod ");
            stdStr = InsertBracket(stdStr);
            stdStr = InsertMultiplyChar(stdStr);
            stdStr = RemoveBracket(stdStr);
            while (stdStr.Contains("-+")) stdStr = stdStr.Replace("-+", "-");
            while (stdStr.Contains("--")) stdStr = stdStr.Replace("--", "+");
            while (stdStr.Contains("++")) stdStr = stdStr.Replace("++", "+");
            while (stdStr.Contains("+-")) stdStr = stdStr.Replace("+-", "-");   // de cai nay o cuoi la tam yen tam
            stdStr = stdStr.Replace("+", " + ");
            stdStr = stdStr.Replace("-", " - ");
            stdStr = stdStr.Replace("*", " * ");
            stdStr = stdStr.Replace("/", " / ");
            stdStr = stdStr.Replace("^", " ^ ");
            stdStr = stdStr.Replace("e - ", "e-");
            stdStr = stdStr.Replace("e + ", "e+");
            stdStr = stdStr.Replace("( - ", "(-");
            stdStr = stdStr.Replace("( + ", "(");
            stdStr = stdStr.Replace(".", DecimalSeparator);
            stdStr = stdStr.Replace(",", DecimalSeparator);
            while (stdStr.Contains("  ")) stdStr = stdStr.Replace("  ", " ");
            stdStr = stdStr.Replace("* - ", "* -");
            stdStr = stdStr.Replace("/ - ", "/ -");
            stdStr = stdStr.Replace("mod - ", "mod -");
            stdStr = stdStr.Replace("yroot - ", "yroot -");
            if (stdStr.StartsWith(" - ")) stdStr = "-" + stdStr.Substring(3);
            if (stdStr.StartsWith(" + ")) stdStr = stdStr.Substring(3);
            return stdStr.Trim();
        }
        /// <summary>
        /// trả về giá trị là khoảng thời gian giữa 2 ngày trong control datetimempicker
        /// </summary>
        public static int differenceBW2Dates(DateTime dt1, DateTime dt2)
        {
            if (dt1 > dt2) { DateTime dtp1_Temp = dt1; dt1 = dt2; dt2 = dtp1_Temp; }
            dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day/*, 0, 0, 0*/);
            dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day/*, 0, 0, 0*/);
            return (dt2 - dt1).Days;
        }
        /// <summary>
        /// chênh lệch giữa 2 ngày theo năm, tháng, tuần, ngày
        /// </summary>
        /// <param name="dtp1">ngày thứ nhất, luôn là ngày phía trước dt2, dtp1 ≤ dtp2</param>
        /// <param name="dtp2">ngày thứ hai,  luôn là ngày phía sau   dt1, dtp2 > dtp1</param>
        public static int[] differencesTimes(DateTime dtp1, DateTime dtp2)
        {
            DateTime dtp1_Temp;
            if (dtp1 > dtp2) { dtp1_Temp = dtp1; dtp1 = dtp2; dtp2 = dtp1_Temp; }
            int[] differ = new int[4];
            dtp1_Temp = dtp1;
            // differ[0] - Year
            differ[0] = dtp2.Year - dtp1_Temp.Year;
            dtp1_Temp = dtp1.AddYears(differ[0]);
            if (dtp1_Temp.DayOfYear > dtp2.DayOfYear) differ[0]--;

            // differ[1] - Month
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[1] = dtp2.Month - dtp1_Temp.Month;
            if (DateTime.IsLeapYear(dtp1.Year) && DateTime.IsLeapYear(dtp2.Year))   // chưa rõ là && hay ||
            {
                if (dtp1/*_Temp*/.Day > dtp2.Day) differ[1]--;
            }
            else
            {
                if (dtp1_Temp.Day > dtp2.Day) differ[1]--;
            }
            if (differ[1] < 0) differ[1] += 12;

            // differ[3] - Day
            dtp1_Temp = dtp1_Temp.AddMonths(differ[1]);
            int diff3 = dtp2.DayOfYear - dtp1_Temp.DayOfYear;
            // neu diff3 < 0 thi moi +365/366, khong thi thoi
            if (diff3 < 0)
            {
                // năm nhuận + 366, năm thường +365
                diff3 += DateTime.IsLeapYear(dtp1_Temp.Year) ? 366 : 365;
            }
            // differ[2]
            differ[2] = diff3 / 7;
            // differ[3]
            differ[3] = diff3 % 7;
            return differ;
        }

        /*---------------------------------------------------------------------*/

        /// <summary>
        /// biến lưu trữ tỷ lệ giữa các đơn vị đo
        /// </summary>
        static double[] rates;
        /// <summary>
        /// lấy tỷ lệ giữa các đơn vị đo
        /// </summary>
        public static double getRate(int type, int from, int to)
        {
            switch (type)
            {
                case 0:
                    rates = new double[] { Math.PI / 180, Math.PI / 200, 1 };
                    break;
                case 1:
                    rates = new double[] { 4046.8564224, 1e4, 1e-4, 0.09290304, 6.4516E-4, 1e6, 1, 2589988.110336, 1e-6, 0.83612736 };
                    break;
                case 2:
                    rates = new double[] { 1055.05585, 4.1868, 1.60217653e-19, 1.3558179483314, 1, 4186.8, 1e3 };
                    break;
                case 3:
                    rates = new double[] { 1e-10, 0.01, 20.1168, 1.8288, 0.3048,
                0.1016, 0.0254, 1e3, 0.201168, 1, 1e-6,1609.344, 1e-3, 1e-9,1852, 0.0042175176, 5.0292, 0.2286, 0.9144};
                    break;
                case 4:
                    rates = new double[] { 17.58426666666667, 0.0225969658055233, 745.6998715822702, 1000.0, 1 };
                    break;
                case 5:
                    rates = new double[] { 101325.0, 1e5, 1e3, 133.322368, 1, 6894.75729 };
                    break;
                case 7:
                    rates = new double[] { 8.64e4, 3.6e3, 1e-6, 1e-3, 60.0, 1, 6.048e5 };
                    break;
                case 8:
                    rates = new double[] { 0.01, 0.3048, 0.2777777777777778, 0.5144444444444444, 340.2933, 1, 0.44704 };
                    break;
                case 9:
                    rates = new double[] { 1e-6, 2.8316846592e-2, 1.6387064e-5, 1, 0.764554857984, 2.84130625e-5,
                        2.95735295625e-5, 4.54609e-3, 3.785411784e-3, 1e-3, 5.6826125e-4, 4.73176473e-4, 1.1365225e-3, 9.46352946e-4 };
                    break;
                case 10:
                    rates = new double[] { 2e-4, 1e-5, 1e-4, 1e-2, 1e-3, 0.1, 1, 1016.0469088, 1e-6,
                        0.028349523125, 0.45359237, 907.18474, /*1 / 0.157473044418 =*/6.35029318, 1e3 };
                    break;
            }
            double rateResult = 1;
            if (from >= 0 && to >= 0) rateResult = rates[from] / rates[to];
            return rateResult;
        }
        /// <summary>
        /// đổi giữa các đơn vị đo nhiệt độ
        /// </summary>
        public static double getTemperature(int i1, int i2, double inp)
        {
            if (i1 == i2) return inp;
            double temperature = 1;
            switch (i1.ToString() + i2.ToString())
            {
                case "01": temperature = 1.8 * inp + 32;            // °C -> °F
                    break;
                case "02": temperature = inp + 273;                 // °C -> °K
                    break;
                case "10": temperature = 5F / 9 * (inp - 32);       // °F -> °C
                    break;
                case "12": temperature = 5F / 9 * (inp - 32) + 273; // °F -> °K
                    break;
                case "20": temperature = inp - 273;                 // °K -> °C
                    break;
                case "21": temperature = 1.8 * (inp - 273) + 32;    // °K -> °F
                    break;
            }
            return temperature;
        }
        /// <summary>
        /// đọc thông tin từ regedit
        /// </summary>
        /// <param name="optionName">list option name</param>
        /// <returns>kết quả đọc được từ regedit</returns>
        public static object[] ReadFromRegedit(string[] optionName)
        {
            object[] result = new object[optionName.Length];
            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator");
                for (int i = 0; i < 4; i++)
                {
                    reg.SetValue(optionName[i], 0, RegistryValueKind.DWord);
                    result[i] = 0;
                }
                result[4] = "0";
                reg.SetValue(optionName[4], result[4], RegistryValueKind.String);
            }
            else
            {
                result[0] = GetValueFromValueKey(optionName[0], reg, 0, 3, 0);
                result[1] = GetValueFromValueKey(optionName[1], reg, 0, 1, 0);
                result[2] = GetValueFromValueKey(optionName[2], reg, 0, 7, 0);
                result[3] = GetValueFromValueKey(optionName[3], reg, 0, 1, 0);
                result[4] = GetValueFromValueKey(optionName[4], reg);
            }

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\UnitConversion", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\UnitConversion");
                reg.SetValue(optionName[5], 0, RegistryValueKind.DWord);
                result[5] = 0;
            }
            else
            {
                result[5] = GetValueFromValueKey(optionName[5], reg, 0, 11, 0);
            }

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\DateCalculation", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\DateCalculation");
                reg.SetValue(optionName[6], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[7], 0, RegistryValueKind.DWord);
                result[6] = 0;
                result[7] = 0;
            }
            else
            {
                result[6] = GetValueFromValueKey(optionName[6], reg, 0, 1, 0);
                result[7] = GetValueFromValueKey(optionName[7], reg, 0, 2, 0);
            }

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\OtherOptions", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\OtherOptions");
                reg.SetValue(optionName[08], 10, RegistryValueKind.DWord);
                reg.SetValue(optionName[09], 1, RegistryValueKind.DWord);
                reg.SetValue(optionName[10], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[11], 1, RegistryValueKind.DWord);
                reg.SetValue(optionName[12], 1, RegistryValueKind.DWord);
                result[08] = 10;
                result[09] = 1;
                result[10] = 0;
                result[11] = 1;
                result[12] = 1;
            }
            else
            {
                result[08] = GetValueFromValueKey(optionName[08], reg, 0, 12, 10);
                result[09] = GetValueFromValueKey(optionName[09], reg, 0, 1, 1);
                result[10] = GetValueFromValueKey(optionName[10], reg, 0, 1, 0);
                result[11] = GetValueFromValueKey(optionName[11], reg, 0, 1, 1);
                result[12] = GetValueFromValueKey(optionName[12], reg, 0, 1, 1);
            }
            reg.Close();

            return result;
        }
        /// <summary>
        /// đọc thông tin từ dword value
        /// </summary>
        /// <param name="optionName">tên dword</param>
        /// <param name="reg">đường dẫn tới key chứa dword</param>
        /// <param name="minValue">giá trị nhỏ nhất cho phép của dword</param>
        /// <param name="maxValue">giá trị lớn nhất cho phép của dword</param>
        /// <param name="defaultValue">giá trị mặc định sẽ được gán nếu giá trị đọc được nằm ngoài khoảng min-max trên</param>
        /// <returns>giá trị đọc được từ dword value</returns>
        static object GetValueFromValueKey(string optionName, RegistryKey reg, int minValue, int maxValue, int defaultValue)
        {
            int rs = (int)reg.GetValue(optionName, -1);
            if (rs > maxValue || rs < minValue)
            {
                rs = defaultValue;
                reg.SetValue(optionName, defaultValue, RegistryValueKind.DWord);
            }
            return rs;
        }
        /// <summary>
        /// đọc thông tin từ string value
        /// </summary>
        /// <param name="optionName">tên dword</param>
        /// <param name="reg">đường dẫn tới key chứa dword</param>
        /// <returns>giá trị đọc được từ dword value</returns>
        static object GetValueFromValueKey(string optionName, RegistryKey reg)
        {
            string rs = (string)reg.GetValue(optionName, "A non-number can be assigned here");
            if (!BigNumber.IsNumber(rs))
            {
                reg.SetValue(optionName, "0", RegistryValueKind.String);
                rs = "0";
            }
            return rs;
        }
        /// <summary>
        /// lưu vào registry trước khi thoát
        /// </summary>
        /// <param name="optName">list tên dword</param>
        /// <param name="config">list giá trị được ghi</param>
        public static void SaveToRegistryBeforeExit(string[] optName, object[] config)
        {
            var reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator", true);
            if (reg == null) return;
            reg.SetValue(optName[0], config[0], RegistryValueKind.DWord);
            reg.SetValue(optName[1], config[1], RegistryValueKind.DWord);
            reg.SetValue(optName[2], config[2], RegistryValueKind.DWord);
            reg.SetValue(optName[3], config[3], RegistryValueKind.DWord);
            reg.SetValue(optName[4], config[4], RegistryValueKind.String);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\UnitConversion", true);
            if (reg == null) return;
            reg.SetValue(optName[5], config[5], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\DateCalculation", true);
            if (reg == null) return;
            reg.SetValue(optName[6], config[6], RegistryValueKind.DWord);
            reg.SetValue(optName[7], config[7], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\OtherOptions", true);
            if (reg == null) return;
            reg.SetValue(optName[08], config[08], RegistryValueKind.DWord);
            reg.SetValue(optName[09], config[09], RegistryValueKind.DWord);
            reg.SetValue(optName[10], config[10], RegistryValueKind.DWord);
            reg.SetValue(optName[11], config[11], RegistryValueKind.DWord);
            reg.SetValue(optName[12], config[12], RegistryValueKind.DWord);
        }
        /// <summary>
        /// số lần mở ngoặc mà chưa đóng ngặc
        /// </summary>
        /// <param name="expression">biểu thức cần tính</param>
        public static int NumberOfOpenWOClose(string expression)
        {
            var regEx = new Regex(@"\(");
            int matches = regEx.Matches(expression).Count;
            regEx = new Regex(@"\)");
            matches -= regEx.Matches(expression).Count;
            return matches;
        }

        public static int GetCloseBracketIndex(string exp, int openIndex)
        {
            int openLevel = 0;
            do
            {
                openIndex = exp.IndexOfAny(new char[] { '(', ')' }, openIndex + 1);
                if (exp[openIndex] == '(')
                {
                    openLevel++;
                }
                else if (exp[openIndex] == ')')
                {
                    openLevel--;
                }
            } while (openLevel >= 0);
            return openIndex;
        }
        /// <summary>
        /// nạp giá trị giai thừa số lớn đã tính được trước đó vào biến
        /// </summary>
        public static void ReadDictionary(ref string[] vl, ref string[] rs, ref int initDictLength)
        {
            #region code doc tu file
            //try
            //{
            //    string[] lines = File.ReadAllLines(DictionaryPath);
            //    for (int i = 0; i < lines.Length; i++)
            //    {
            //        string[] split = lines[i].Split(';');
            //        Array.Resize<string>(ref fact_Value, i + 1);
            //        Array.Resize<string>(ref factResult, i + 1);
            //        fact_Value[i] = split[0].Trim();
            //        factResult[i] = split[1].Trim();
            //    }
            //}
            //catch (FileNotFoundException) { }
            //catch (Exception)
            //{
            //    MessageBox.Show("The dictionary file contains invalid syntax.", "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //} 
            #endregion

            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\FactorialDictionary", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\FactorialDictionary");
            }

            string[] namelist = reg.GetValueNames();
            initDictLength = namelist.Length;
            for (int i = 0; i < namelist.Length; i++)
            {
                try
                {
                    Array.Resize<string>(ref vl, i + 1);
                    Array.Resize<string>(ref rs, i + 1);
                    vl[i] = namelist[i];
                    rs[i] = (string)reg.GetValue(vl[i]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            reg.Close();
        }
        /// <summary>
        /// lưu từ điển giai thừa vào registry trước khi thoát
        /// </summary>
        public static void SaveDictionary(int initDictLength, string[] fact_Value, string[] factResult)
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\FactorialDictionary", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\FactorialDictionary");
            }
            for (int i = initDictLength; i < fact_Value.Length; i++)
            {
                reg.SetValue(fact_Value[i], factResult[i]);
            }
            reg.Close();
        }
    }
    /// <summary>
    /// lớp các phương thức liên quan đến hệ cơ số
    /// </summary>
    public static class Binary
    {
        /// <summary>
        /// đổi 1 số từ hệ 10 sang các hệ khác
        /// </summary>
        public static string dec_to_other(string decimalNumber, /*int from, */int dest, int size, bool isSign)
        {
            // from nguon, dest dest
            if (decimalNumber == "0") return "0";
            BigNumber realNumber = decimalNumber;
            if (realNumber < 0) realNumber += BigNumber.Two.Pow(size);

            //byte remainder;

            if (dest == 2 || dest == 8)  // 10 -> 2 || 10 -> 8
            {
                switch (size)
                {
                    case 08:
                        if (realNumber <= sbyte.MaxValue && isSign)
                            return Convert.ToString(sbyte.Parse(realNumber.IntString), dest);
                        else
                            return Convert.ToString(byte.Parse(realNumber.IntString), dest);
                    case 16:
                        if (realNumber <= short.MaxValue && isSign)
                            return Convert.ToString(short.Parse(realNumber.IntString), dest);
                        else
                            return Convert.ToString(ushort.Parse(realNumber.IntString), dest);
                    case 32:
                        if (realNumber <= int.MaxValue && isSign)
                            return Convert.ToString(int.Parse(realNumber.IntString), dest);
                        else
                            return Convert.ToString(uint.Parse(realNumber.IntString), dest);
                    case 64:
                        byte remainder;
                        string rs = "";
                        while (realNumber > 0)
                        {
                            remainder = byte.Parse((realNumber - dest * (realNumber / dest).Floor()).IntString);
                            realNumber = (realNumber / dest).Floor();
                            rs = remainder.ToString() + rs;
                        }
                        if (dest == 2 && rs.Length > 64) throw new Exception("Overflow");
                        if (dest == 8 && rs.Length > 22) throw new Exception("Overflow");
                        return rs;
                }
            }

            if (dest == 16) // 10 -> 16
            {
                try
                {
                    return ulong.Parse(realNumber.StrValue).ToString("X2").TrimStart('0');  // bat buoc phai la ulong
                }
                catch { throw new Exception("Overflow"); }
            }
            return decimalNumber;
        }
        /// <summary>
        /// đổi 1 số từ hệ khác 10 sang hệ 10
        /// </summary>
        public static string other_to_dec(string decimalNumber, int from/*, int dest*/, int Size, bool isSign)
        {
            // from nguon, dest dich
            long lResult = 0;
            BigNumber bs = 1;
            char[] memberChar = decimalNumber.ToCharArray();
            //if (memberChar.Length < Size) Size = memberChar.Length;
            for (int i = memberChar.Length - 1; i >= 0; i--)
            {
                if (memberChar[i] < 58 && memberChar[i] >= 48)
                {
                    lResult += (long)ulong.Parse(((memberChar[i] - 48) * bs).StrValue);
                }
                else if (memberChar[i] < 71 && memberChar[i] >= 65) //A-F
                {
                    lResult += (long)ulong.Parse(((memberChar[i] - 55) * bs).StrValue);
                }
                else    //a-f
                {
                    lResult += (long)ulong.Parse(((memberChar[i] - 87) * bs).StrValue);
                }
                bs *= from;
            }
            BigNumber result = lResult;
            if (Size == memberChar.Length && memberChar[0] == '1')
            {
                if (lResult > 0 && isSign) result -= BigNumber.Two.Pow(Size);
                if (lResult < 0 && !isSign) result += BigNumber.Two.Pow(Size);
            }
            return result.StrValue;
        }
        /// <summary>
        /// chuẩn hoá xâu - cắt những số 0 thừa ở đầu xâu
        /// </summary>
        public static string standardString(string str)
        {
            string result = str.TrimStart('0');
            if (result == "") return "0";
            return result;
        }
    }
    /// <summary>
    /// lớp đối tượng ngày âm lịch
    /// </summary>
    public class LunarDate
    {
        public LunarDate(int dd, int mm, int yy, int leap, int jd)
        {
            this.Day = dd;
            this.Month = mm;
            this.Year = yy;
            this.Leap = leap;
            this.JulianDate = jd;
        }
        /// <summary>
        /// ngày âm lịch
        /// </summary>
        public int Day { get; set; }
        public int JulianDate { get; set; }
        public int Leap { get; set; }
        /// <summary>
        /// tháng âm lịch
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// năm âm lịch
        /// </summary>
        public int Year { get; set; }

        private string[] CAN = new string[10] { "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý" };
        private string[] CHI = new string[12] { "Tí", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi" };
        /// <summary>
        /// tên năm âm lịch
        /// </summary>
        public string YearName
        {
            get
            {
                int can = (this.Year - 4) % 10;
                int chi = (this.Year - 4) % 12;
                return string.Format("{0} {1}", CAN[can], CHI[chi]);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Day, Month, Year);
        }
    }

    public class ConvertDate
    {
        #region constant
        private readonly int[][] TK = new int[][]{
            new int[] {
		        0x226da2, 0x4695d0, 0x3349dc, 0x5849b0, 0x42a4b0, 0x2aaab8, 0x506a50, 0x3ab540, 0x24bb44, 0x48ab6,
		        0x3495b0, 0x205372, 0x464970, 0x2e64f9, 0x5454b0, 0x3e6a50, 0x296c57, 0x4c5ac0, 0x36ab60, 0x2386e,
		        0x4892e0, 0x30c97c, 0x56c960, 0x40d4a0, 0x2adaa8, 0x4eb550, 0x3a56a0, 0x24adb5, 0x4c25d0, 0x3492e,
		        0x1ed2b2, 0x44a950, 0x2ed4d9, 0x52b2a0, 0x3cb550, 0x285757, 0x4e4da0, 0x36a5b0, 0x225574, 0x4852b,
		        0x33a93c, 0x566930, 0x406aa0, 0x2aada8, 0x50ab50, 0x3a4b60, 0x24aae4, 0x4aa570, 0x365270, 0x1f526,
		        0x42e530, 0x2e6cba, 0x5456a0, 0x3c5b50, 0x294ad6, 0x4e4ae0, 0x38a4e0, 0x20d4d4, 0x46d260, 0x30d53,
		        0x56b520, 0x3eb6a0, 0x2b56a9, 0x505570, 0x3c49d0, 0x25a1b5, 0x4aa4b0, 0x34aa50, 0x1eea51, 0x42b52,
		        0x2cb5aa, 0x52ab60, 0x3e95b0, 0x284b76, 0x4e4970, 0x3864b0, 0x22b4b3, 0x466a50, 0x306b3b, 0x565ac,
		        0x40ab60, 0x2b2ad8, 0x5049e0, 0x3aa4d0, 0x24d4b5, 0x48b250, 0x32b520, 0x1cf522, 0x42b5a0, 0x2c95e,
		        0x5295b0, 0x3e49b0, 0x28a576, 0x4ca4b0, 0x36aa50, 0x20ba54, 0x466d40, 0x2ead6c, 0x54ab60, 0x409370
	        }, 
            new int[] {
		        0x2d49b8, 0x504970, 0x3a64b0, 0x246ca5, 0x48da50, 0x325aa0, 0x1cd6c1, 0x42a6e0, 0x2e92fb, 0x5292e,
		        0x3cc960, 0x26d557, 0x4cd4a0, 0x34d550, 0x215553, 0x4656a0, 0x30a6d0, 0x1aa5d1, 0x4092b0, 0x2aa5b,
		        0x50a950, 0x38b2a0, 0x23b2a5, 0x48ad50, 0x344da0, 0x1ccba1, 0x42a570, 0x2e52f9, 0x545270, 0x3c693,
		        0x266b37, 0x4c6aa0, 0x36ab50, 0x205753, 0x464b60, 0x30a67c, 0x56a2e0, 0x3ed160, 0x28e968, 0x4ed4a,
		        0x38daa0, 0x225ea5, 0x4856d0, 0x344ae0, 0x1f85d2, 0x42a2d0, 0x2cd17a, 0x52aa50, 0x3cb520, 0x24d74,
		        0x4aada0, 0x3655d0, 0x2253b3, 0x4645b0, 0x30a2b0, 0x1ba2b1, 0x40aa50, 0x28b559, 0x4e6b20, 0x38ad6,
		        0x255365, 0x489370, 0x344570, 0x1ea573, 0x4452b0, 0x2c6a6a, 0x50d950, 0x3c5aa0, 0x27aac7, 0x4aa6e,
		        0x3652e0, 0x20cae3, 0x46a560, 0x2ed2bb, 0x54d2a0, 0x3ed550, 0x2a5ad9, 0x4e56a0, 0x38a6d0, 0x2455d,
		        0x4a52b0, 0x32a8d0, 0x1ce552, 0x42b2a0, 0x2cb56a, 0x50ad50, 0x3c4da0, 0x26a7a6, 0x4ca570, 0x3651b,
		        0x21a174, 0x466530, 0x316a9c, 0x545aa0, 0x3eab50, 0x2a2bd9, 0x502b60, 0x38a370, 0x2452e5, 0x48d160
	        },
            new int[] {
		        0x32e4b0, 0x1c7523, 0x40daa0, 0x2d5b4b, 0x5256d0, 0x3c2ae0, 0x26a3d7, 0x4ca2d0, 0x36d150, 0x1ed95,
		        0x44b520, 0x2eb69c, 0x54ada0, 0x3e55d0, 0x2b25b9, 0x5045b0, 0x3aa2b0, 0x22aab5, 0x48a950, 0x32b52,
		        0x1ceaa1, 0x40ab60, 0x2c55bc, 0x524b70, 0x3e4570, 0x265377, 0x4c52b0, 0x366950, 0x216954, 0x445aa,
		        0x2eab5c, 0x54a6e0, 0x404ae0, 0x28a5e8, 0x4ea560, 0x38d2a0, 0x22eaa6, 0x46d550, 0x3256a0, 0x1d95a,
		        0x4295d0, 0x2c4afb, 0x5249b0, 0x3ca4d0, 0x26d2d7, 0x4ab2a0, 0x34b550, 0x205d54, 0x462da0, 0x2e95b,
		        0x1b1571, 0x4049b0, 0x2aa4f9, 0x4e64b0, 0x386a90, 0x22aea6, 0x486b50, 0x322b60, 0x1caae2, 0x42937,
		        0x2f496b, 0x50c960, 0x3ae4d0, 0x266b27, 0x4adaa0, 0x345ad0, 0x2036d3, 0x4626e0, 0x3092e0, 0x18d2d,
		        0x3ec950, 0x28d4d9, 0x4eb4a0, 0x36b690, 0x2355a6, 0x4855b0, 0x3425d0, 0x1ca5b2, 0x4292b0, 0x2ca97,
		        0x526950, 0x3a74a0, 0x24b5a8, 0x4aab60, 0x3653b0, 0x202b74, 0x462570, 0x3052b0, 0x1ad2b1, 0x3e695,
		        0x286ad9, 0x4e5aa0, 0x38ab50, 0x224ed5, 0x484ae0, 0x32a370, 0x1f44e3, 0x40d2a0, 0x2bd94b, 0x50b550
	        }, 
            new int[] {
		        0x3c56a0, 0x2497a7, 0x4a95d0, 0x364ae0, 0x20a9b4, 0x44a4d0, 0x2ed250, 0x19aaa1, 0x3eb550, 0x2856d,
		        0x4e2da0, 0x3895b0, 0x244b75, 0x484970, 0x32a4b0, 0x1cb4b4, 0x426a90, 0x2aad5c, 0x505b50, 0x3c2b6,
		        0x2695e8, 0x4a92f0, 0x364970, 0x206964, 0x44d4a0, 0x2cea5c, 0x52d690, 0x3e56d0, 0x2b2b5a, 0x4e26e,
		        0x3892e0, 0x22cad6, 0x48c950, 0x30d4a0, 0x1af4a2, 0x40b590, 0x2c56dc, 0x5055b0, 0x3c25d0, 0x2693b,
		        0x4c92b0, 0x34a950, 0x1fb155, 0x446ca0, 0x2ead50, 0x192b61, 0x3e4bb0, 0x2a25f9, 0x502570, 0x3852b,
		        0x22aaa6, 0x46e950, 0x326aa0, 0x1abaa3, 0x40ab50, 0x2c4b7b, 0x524ae0, 0x3aa570, 0x2652d7, 0x4ad26,
		        0x34d950, 0x1e5d55, 0x4456a0, 0x2e96d0, 0x1a55d2, 0x3e4ae0, 0x28a4fa, 0x4ea4d0, 0x38d250, 0x20d69,
		        0x46b550, 0x3235a0, 0x1caba2, 0x4095b0, 0x2d49bc, 0x524970, 0x3ca4b0, 0x24b2b8, 0x4a6a50, 0x346d4,
		        0x1fab54, 0x442ba0, 0x2e9370, 0x2e52f2, 0x544970, 0x3c64e9, 0x60d4a0, 0x4aea50, 0x373aa6, 0x5a56d,
		        0x462b60, 0x3185e3, 0x5692e0, 0x3ec97b, 0x64a950, 0x4ed4a0, 0x38daa8, 0x5cb550, 0x4856b0, 0x342da4
	        },
            new int[] {
		        0x58a5d0, 0x4292d0, 0x2cd2b2, 0x52a950, 0x3cb4d9, 0x606aa0, 0x4aad50, 0x365756, 0x5c4ba0, 0x44a5b,
		        0x314573, 0x5652b0, 0x41a94b, 0x62e950, 0x4e6aa0, 0x38ada8, 0x5e9b50, 0x484b60, 0x32aae4, 0x58a4f,
		        0x445260, 0x2bd262, 0x50d550, 0x3d5a9a, 0x6256a0, 0x4a96d0, 0x3749d6, 0x5c49e0, 0x46a4d0, 0x2ed4d,
		        0x54d250, 0x3ed53b, 0x64b540, 0x4cb5a0, 0x3995a8, 0x5e95b0, 0x4a49b0, 0x32a974, 0x58a4b0, 0x42aa5,
		        0x2cea51, 0x506d40, 0x3aadbb, 0x622b60, 0x4c9370, 0x364af6, 0x5c4970, 0x4664b0, 0x3074a3, 0x52da5,
		        0x3e6b5b, 0x6456d0, 0x502ae0, 0x3893e7, 0x5e92e0, 0x48c960, 0x33d155, 0x56d4a0, 0x40da50, 0x2d355,
		        0x5256a0, 0x3aa6fa, 0x6225d0, 0x4c92d0, 0x36aab6, 0x5aa950, 0x44b4a0, 0x2ebaa4, 0x54ad50, 0x3f55a,
		        0x644ba0, 0x4ea5b0, 0x3b5278, 0x5e52b0, 0x486930, 0x327555, 0x586aa0, 0x40ab50, 0x2c5b52, 0x524b6,
		        0x3da56a, 0x60a4f0, 0x4c5260, 0x34ea66, 0x5ad530, 0x445aa0, 0x2eb6a3, 0x5496d0, 0x404ae0, 0x28c9d,
		        0x4ea4d0, 0x38d2d8, 0x5eb250, 0x46b520, 0x31d545, 0x56ada0, 0x4295d0, 0x2c55b2, 0x5249b0, 0x3ca4f9
	        },
            new int[] {
		        0x62a4b0, 0x4caa50, 0x37b457, 0x5c6b40, 0x46ada0, 0x305b64, 0x569370, 0x424970, 0x2cc971, 0x5064b,
		        0x3a6aa8, 0x5eda50, 0x4a5aa0, 0x32aec5, 0x58a6e0, 0x4492f0, 0x3052e2, 0x52c960, 0x3dd49a, 0x62d4a,
		        0x4cd550, 0x365b57, 0x5c56a0, 0x46a6d0, 0x3295d4, 0x5692d0, 0x40a95c, 0x2ad4b0, 0x50b2a0, 0x38b5a,
		        0x5ead50, 0x4a4da0, 0x34aba4, 0x58a570, 0x4452b0, 0x2eb273, 0x546930, 0x3c6abb, 0x626aa0, 0x4cab5,
		        0x394b57, 0x5c4b60, 0x46a570, 0x3252e4, 0x56d160, 0x3ee93c, 0x64d520, 0x4edaa0, 0x3b5b29, 0x5e56d,
		        0x4a4ae0, 0x34a5d5, 0x5aa2d0, 0x42d150, 0x2cea52, 0x52b520, 0x3cd6ab, 0x60ada0, 0x4c55d0, 0x384bb,
		        0x5e45b0, 0x46a2b0, 0x30d2b4, 0x56aa50, 0x41b52c, 0x646b20, 0x4ead60, 0x3a55e9, 0x609370, 0x4a457,
		        0x34a575, 0x5a52b0, 0x446a50, 0x2d5a52, 0x525aa0, 0x3dab4b, 0x62a6e0, 0x4c92e0, 0x36c6e6, 0x5ca56,
		        0x46d4a0, 0x2eeaa5, 0x54d550, 0x4056a0, 0x2ad5a1, 0x4ea5d0, 0x3b52d9, 0x6052b0, 0x4aa950, 0x32d55,
		        0x58b2a0, 0x42b550, 0x2e6d52, 0x524da0, 0x3da5cb, 0x62a570, 0x4e51b0, 0x36a977, 0x5c6530, 0x466a90
	        }, 
            new int[] {
		        0x30baa3, 0x56ab50, 0x422ba0, 0x2cab61, 0x52a370, 0x3c51e8, 0x60d160, 0x4ae4b0, 0x376926, 0x58daa0,
		        0x445b50, 0x3116d2, 0x562ae0, 0x3ea2e0, 0x28e2d2, 0x4ec950, 0x38d556, 0x5cb520, 0x46b690, 0x325da4,
		        0x5855d0, 0x4225d0, 0x2ca5b3, 0x52a2b0, 0x3da8b7, 0x60a950, 0x4ab4a0, 0x35b2a5, 0x5aad50, 0x4455b0,
		        0x302b74, 0x562570, 0x4052f9, 0x6452b0, 0x4e6950, 0x386d56, 0x5e5aa0, 0x46ab50, 0x3256d4, 0x584ae0,
		        0x42a570, 0x2d4553, 0x50d2a0, 0x3be8a7, 0x60d550, 0x4a5aa0, 0x34ada5, 0x5a95d0, 0x464ae0, 0x2eaab4,
		        0x54a4d0, 0x3ed2b8, 0x64b290, 0x4cb550, 0x385757, 0x5e2da0, 0x4895d0, 0x324d75, 0x5849b0, 0x42a4b0,
		        0x2da4b3, 0x506a90, 0x3aad98, 0x606b50, 0x4c2b60, 0x359365, 0x5a9370, 0x464970, 0x306964, 0x52e4a0,
		        0x3cea6a, 0x62da90, 0x4e5ad0, 0x392ad6, 0x5e2ae0, 0x4892e0, 0x32cad5, 0x56c950, 0x40d4a0, 0x2bd4a3,
		        0x50b690, 0x3a57a7, 0x6055b0, 0x4c25d0, 0x3695b5, 0x5a92b0, 0x44a950, 0x2ed954, 0x54b4a0, 0x3cb550,
		        0x286b52, 0x4e55b0, 0x3a2776, 0x5e2570, 0x4852b0, 0x32aaa5, 0x56e950, 0x406aa0, 0x2abaa3, 0x50ab50
	        }, 
            new int[] {
		        0x3c4bd8, 0x624ae0, 0x4ca570, 0x3854d5, 0x5cd260, 0x44d950, 0x315554, 0x5656a0, 0x409ad0, 0x2a55d2,
		        0x504ae0, 0x3aa5b6, 0x60a4d0, 0x48d250, 0x33d255, 0x58b540, 0x42d6a0, 0x2cada2, 0x5295b0, 0x3f4977,
		        0x644970, 0x4ca4b0, 0x36b4b5, 0x5c6a50, 0x466d50, 0x312b54, 0x562b60, 0x409570, 0x2c52f2, 0x504970,
		        0x3a6566, 0x5ed4a0, 0x48ea50, 0x336a95, 0x585ad0, 0x442b60, 0x2f86e3, 0x5292e0, 0x3dc8d7, 0x62c950,
		        0x4cd4a0, 0x35d8a6, 0x5ab550, 0x4656a0, 0x31a5b4, 0x5625d0, 0x4092d0, 0x2ad2b2, 0x50a950, 0x38b557,
		        0x5e6ca0, 0x48b550, 0x355355, 0x584da0, 0x42a5b0, 0x2f4573, 0x5452b0, 0x3ca9a8, 0x60e950, 0x4c6aa0,
		        0x36aea6, 0x5aab50, 0x464b60, 0x30aae4, 0x56a570, 0x405260, 0x28f263, 0x4ed940, 0x38db47, 0x5cd6a0,
		        0x4896d0, 0x344dd5, 0x5a4ad0, 0x42a4d0, 0x2cd4b4, 0x52b250, 0x3cd558, 0x60b540, 0x4ab5a0, 0x3755a6,
		        0x5c95b0, 0x4649b0, 0x30a974, 0x56a4b0, 0x40aa50, 0x29aa52, 0x4e6d20, 0x39ad47, 0x5eab60, 0x489370,
		        0x344af5, 0x5a4970, 0x4464b0, 0x2c74a3, 0x50ea50, 0x3d6a58, 0x6256a0, 0x4aaad0, 0x3696d5, 0x5c92e0
	        }, 
            new int[] {
		        0x46c960, 0x2ed954, 0x54d4a0, 0x3eda50, 0x2a7552, 0x4e56a0, 0x38a7a7, 0x5ea5d0, 0x4a92b0, 0x32aab5,
		        0x58a950, 0x42b4a0, 0x2cbaa4, 0x50ad50, 0x3c55d9, 0x624ba0, 0x4ca5b0, 0x375176, 0x5c5270, 0x466930,
		        0x307934, 0x546aa0, 0x3ead50, 0x2a5b52, 0x504b60, 0x38a6e6, 0x5ea4e0, 0x48d260, 0x32ea65, 0x56d520,
		        0x40daa0, 0x2d56a3, 0x5256d0, 0x3c4afb, 0x6249d0, 0x4ca4d0, 0x37d0b6, 0x5ab250, 0x44b520, 0x2edd25,
		        0x54b5a0, 0x3e55d0, 0x2a55b2, 0x5049b0, 0x3aa577, 0x5ea4b0, 0x48aa50, 0x33b255, 0x586d20, 0x40ad60,
		        0x2d4b63, 0x525370, 0x3e49e8, 0x60c970, 0x4c54b0, 0x3768a6, 0x5ada50, 0x445aa0, 0x2fa6a4, 0x54aad0,
		        0x4052e0, 0x28d2e3, 0x4ec950, 0x38d557, 0x5ed4a0, 0x46d950, 0x325d55, 0x5856a0, 0x42a6d0, 0x2c55d4,
		        0x5252b0, 0x3ca9b8, 0x62a930, 0x4ab490, 0x34b6a6, 0x5aad50, 0x4655a0, 0x2eab64, 0x54a570, 0x4052b0,
		        0x2ab173, 0x4e6930, 0x386b37, 0x5e6aa0, 0x48ad50, 0x332ad5, 0x582b60, 0x42a570, 0x2e52e4, 0x50d160,
		        0x3ae958, 0x60d520, 0x4ada90, 0x355aa6, 0x5a56d0, 0x462ae0, 0x30a9d4, 0x54a2d0, 0x3ed150, 0x28e952
	        },
            new int[] {
			    0x4eb520, 0x38d727, 0x5eada0, 0x4a55b0, 0x362db5, 0x5a45b0, 0x44a2b0, 0x2eb2b4, 0x54a950, 0x3cb559,
			    0x626b20, 0x4cad50, 0x385766, 0x5c5370, 0x484570, 0x326574, 0x5852b0, 0x406950, 0x2a7953, 0x505aa0,
			    0x3baaa7, 0x5ea6d0, 0x4a4ae0, 0x35a2e5, 0x5aa550, 0x42d2a0, 0x2de2a4, 0x52d550, 0x3e5abb, 0x6256a0,
			    0x4c96d0, 0x3949b6, 0x5e4ab0, 0x46a8d0, 0x30d4b5, 0x56b290, 0x40b550, 0x2a6d52, 0x504da0, 0x3b9567,
			    0x609570, 0x4a49b0, 0x34a975, 0x5a64b0, 0x446a90, 0x2cba94, 0x526b50, 0x3e2b60, 0x28ab61, 0x4c9570,
			    0x384ae6, 0x5cd160, 0x46e4a0, 0x2eed25, 0x54da90, 0x405b50, 0x2c36d3, 0x502ae0, 0x3a93d7, 0x6092d0,
			    0x4ac950, 0x32d556, 0x58b4a0, 0x42b690, 0x2e5d94, 0x5255b0, 0x3e25fa, 0x6425b0, 0x4e92b0, 0x36aab6,
			    0x5c6950, 0x4674a0, 0x31b2a5, 0x54ad50, 0x4055a0, 0x2aab73, 0x522570, 0x3a5377, 0x6052b0, 0x4a6950,
			    0x346d56, 0x585aa0, 0x42ab50, 0x2e56d4, 0x544ae0, 0x3ca570, 0x2864d2, 0x4cd260, 0x36eaa6, 0x5ad550,
			    0x465aa0, 0x30ada5, 0x5695d0, 0x404ad0, 0x2aa9b3, 0x50a4d0, 0x3ad2b7, 0x5eb250, 0x48b540, 0x33d556
	        }
        };/* Years 2100-2199 */
        #endregion

        private static LunarDate lunarDate = new LunarDate(0, 0, 0, 0, 0);

        public static ConvertDate cd = new ConvertDate();

        public LunarDate Solar2Lunar(DateTime dt)
        {
            int dd = dt.Day, mm = dt.Month, yyyy = dt.Year;
            if (yyyy < 1200 || 2199 < yyyy)
            {
                //throw new Exception("Current year must not exceed the year 2199!");
                return new LunarDate(0, 0, 0, 0, 0);
            }
            LunarDate[] ly = getYearInfo(yyyy);
            int jd = jdn(dd, mm, yyyy);
            if (jd < ly[0].JulianDate)
            {
                ly = getYearInfo(yyyy - 1);
            }
            return lunarDate = findLunarDate(jd, ly);
        }

        private LunarDate[] getYearInfo(int yyyy)
        {
            int yearCode = 0;
            // thế kỷ của năm hiện tại
            int currentCentury = yyyy / 100 + 1;
            // năm cuối cùng của thế kỷ đó
            int finalYearOfCurrentCentury = (currentCentury - 1) * 100;

            yearCode = TK[currentCentury - 13][yyyy - finalYearOfCurrentCentury];
            return decodeLunarYear(yyyy, yearCode);
        }

        private LunarDate[] decodeLunarYear(int yy, int k)
        {
            var ly = new System.Collections.Generic.List<LunarDate>();
            // số ngày trong 1 tháng âm lịch
            int[] monthLengths = new int[] { 29, 30 };
            int[] regularMonths = new int[12];
            int offsetOfTet = k >> 17;
            // tháng nhuận của năm hiện hành
            int leapMonth = k & 0xf;
            int leapMonthLength = monthLengths[k >> 16 & 0x1];
            int solarNY = jdn(1, 1, yy);
            int currentJD = solarNY + offsetOfTet;
            int j = k >> 4;
            for (int i = 0; i < 12; i++)
            {
                regularMonths[12 - i - 1] = monthLengths[j & 0x1];
                j >>= 1;
            }
            if (leapMonth == 0)
            {
                for (int mm = 1; mm <= 12; mm++)
                {
                    ly.Add(new LunarDate(1, mm, yy, 0, currentJD));
                    currentJD += regularMonths[mm - 1];
                }
            }
            else
            {
                for (int mm = 1; mm <= leapMonth; mm++)
                {
                    ly.Add(new LunarDate(1, mm, yy, 0, currentJD));
                    currentJD += regularMonths[mm - 1];
                }
                ly.Add(new LunarDate(1, leapMonth, yy, 1, currentJD));
                currentJD += leapMonthLength;
                for (int mm = leapMonth + 1; mm <= 12; mm++)
                {
                    ly.Add(new LunarDate(1, mm, yy, 0, currentJD));
                    currentJD += regularMonths[mm - 1];
                }
            }
            return ly.ToArray();
        }

        private int jdn(int dd, int mm, int yy)
        {
            int a = INT((14 - mm) / 12);
            int y = yy + 4800 - a;
            int m = mm + 12 * a - 3;
            int jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - INT(y / 100) + INT(y / 400) - 32045;
            if (jd < 2299161)
            {
                jd = dd + INT((153 * m + 2) / 5) + 365 * y + INT(y / 4) - 32083;
            }
            return jd;
        }
        /// <summary>
        /// tìm giá trị phần nguyên
        /// </summary>
        /// <param name="d">số cần tìm phần nguyên</param>
        /// <returns>kết quả</returns>
        private int INT(double d)
        {
            return (int)Math.Floor(d);
        }

        private const int LAST_DAY = 0x2685B1;
        private const int FIRST_DAY = 0x20F31C;

        private LunarDate findLunarDate(int jd, LunarDate[] ly)
        {
            if (jd > LAST_DAY || jd < FIRST_DAY || ly[0].JulianDate > jd)
            {
                return new LunarDate(0, 0, 0, 0, jd);
            }
            int i = ly.Length - 1;
            while (jd < ly[i].JulianDate)
            {
                i--;
            }
            int off = jd - ly[i].JulianDate;
            //LunarDate ret = new LunarDate(ly[i].Day + off, ly[i].Month, ly[i].Year, ly[i].Leap, jd);
            return new LunarDate(ly[i].Day + off, ly[i].Month, ly[i].Year, ly[i].Leap, jd);
        }
    }
}