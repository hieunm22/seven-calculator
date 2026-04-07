using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Calculator
{
    /// <summary>
    /// tổng hợp các method
    /// </summary>
    public static class Common
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        /// <summary>
        /// Software\SevenCalculator
        /// </summary>
        private const string RegistryPath = "Software\\SevenCalculator";
        /// <summary>
        /// Conversions > Preferences > FactorialDictionary > HistoryExpressions
        /// </summary>
        private static readonly string[] KeyNames = new string[] {
            "Conversions",
            "Preferences",
            "FactorialDictionary",
            "HistoryExpressions",
        };
        /// <summary>
        /// chia hàng đơn vị của một số thực thành từng nhóm 3 số
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
            output = Group(output, GroupSeparator, 3);
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
        /// <param name="sep">ký tự phân cách từng nhóm</param>
        /// <param name="num">số kí tự của từng nhóm</param>
        public static string Group(object input, string sep, int num)
        {
            string outp = input.ToString();
            // them ky tu phan cach nhom vao giua nhom 3 so o xau ket qua
            for (int i = outp.Length - num; i > 0; i -= num)
            {
                outp = outp.Insert(i, sep);
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
            //Regex reg = new Regex(@"\(\(   (.*)   \)\)");         // ((mot_bieu_thuc))
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
        /// số lần mở ngoặc mà chưa đóng ngoặc
        /// </summary>
        /// <param name="expression">biểu thức cần tính</param>
        public static int NumberOfOpenWOClose(string expression)
        {
            return expression.Replace(")", "").Length - expression.Replace("(", "").Length;
        }
        /// <summary>
        /// xác định vị trí của dấu ) trong 1 chuỗi
        /// </summary>
        /// <param name="exp">chuỗi cần tìm</param>
        /// <param name="openIndex">vị trí dấu mở ngoặc</param>
        /// <param name="sub">biểu thức trong cặp dấu đóng mở ngoặc</param>
        /// <returns>vị trí của dấu ) trong chuỗi</returns>
        public static int GetCloseBracketIndex(string exp, int openIndex, ref string sub)
        {
            var regEx = new Regex(@"\(([^\(\)]+)\)(\^|!?)", RegexOptions.IgnoreCase);
            var m = regEx.Match(exp, openIndex);
            sub = m.Groups[1].Value;
            return openIndex + sub.Length + 1;
        }
        /// <summary>
        /// trả về giá trị là khoảng thời gian giữa 2 ngày trong control datetimempicker
        /// </summary>
        public static int DifferenceBW2Dates(DateTime dt1, DateTime dt2)
        {
            if (dt1 > dt2) { DateTime dtp1_Temp = dt1; dt1 = dt2; dt2 = dtp1_Temp; }
            dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
            dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day);
            return (dt2 - dt1).Days;
        }
        /// <summary>
        /// chênh lệch giữa 2 ngày theo năm, tháng, tuần, ngày
        /// </summary>
        /// <param name="dtp1">ngày thứ nhất, luôn là ngày phía trước dt2, dtp1 ≤ dtp2</param>
        /// <param name="dtp2">ngày thứ hai,  luôn là ngày phía sau   dt1, dtp2 > dtp1</param>
        public static int[] DifferencesTimes(DateTime dtp1, DateTime dtp2)
        {
            DateTime dtp1_Temp;
            if (dtp1 > dtp2) { dtp1_Temp = dtp1; dtp1 = dtp2; dtp2 = dtp1_Temp; }
            dtp1_Temp = dtp1;
            int[] differ = new int[4];
            // differ[0] - Year
            differ[0] = dtp2.Year - dtp1_Temp.Year;
            dtp1_Temp = dtp1.AddYears(differ[0]);
            if (dtp1_Temp.DayOfYear > dtp2.DayOfYear) differ[0]--;

            // differ[1] - Month
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[1] = dtp2.Month - dtp1_Temp.Month;
            if (DateTime.IsLeapYear(dtp1.Year) && DateTime.IsLeapYear(dtp2.Year))
            {
                if (dtp1.Day > dtp2.Day) differ[1]--;
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
                // năm nhuận +366, năm thường +365
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
        public static double GetRate(int type, int from, int to)
        {
            switch (type)
            {
                case 0:     // Angle
                    rates = new double[] { Math.PI / 180, Math.PI / 200, 1 };
                    break;
                case 1:     // Area
                    rates = new double[] { 4046.8564224, 1e4, 1e-4, 0.09290304, 6.4516E-4, 1e6, 1, 2589988.110336, 1e-6, 0.83612736 };
                    break;
                case 2:     // Data
                    //rates = new double[] { 0.125, 1, 1.25e17, 1e18, 1.25e8, 1e9, 125, 1e3, 
                    //    125000, 1e6, 0.5, 1.25e14, 1e15, 1.25e11, 1e12, 1.25e23, 1e24, 1.25e20, 1e21 };     // rates by google
                    rates = new double[] { 0.125, 1, 1.25e17, Math.Pow(2, 60), 1.25e8, Math.Pow(2, 30), 125, Math.Pow(2, 10), 125000, Math.Pow(2, 20), 
                        0.5, 1.25e14, Math.Pow(2, 50), 1.25e11, Math.Pow(2, 40), 1.25e23, Math.Pow(2, 80), 1.25e20, Math.Pow(2, 70) };     // rates standard
                    break;
                case 3:     // Energy
                    rates = new double[] { 1055.05585, 4.1868, 1.60217653e-19, 1.3558179483314, 1, 4186.8, 1e3 };
                    break;
                case 4:     // Length
                    rates = new double[] { 1e-10, 0.01, 20.1168, 1.8288, 0.3048,
                0.1016, 0.0254, 1e3, 9.4605284e15, 0.201168, 1, 1e-6,1609.344, 1e-3, 1e-9,1852, 0.0042175176, 5.0292, 0.2286, 0.9144};
                    break;
                case 5:     // Power
                    rates = new double[] { 17.58426666666667, 0.0225969658055233, 745.6998715822702, 1000.0, 1 };
                    break;
                case 6:     // Pressure
                    rates = new double[] { 101325.0, 1e5, 1e3, 133.322368, 1, 6894.75729 };
                    break;
                case 8:     // Time
                    rates = new double[] { 8.64e4, 3.6e3, 1e-6, 1e-3, 60.0, 1, 6.048e5 };
                    break;
                case 9:     // Velocity
                    rates = new double[] { 0.01, 0.3048, 0.2777777777777778, 0.5144444444444444, 340.2933, 1, 0.44704 };
                    break;
                case 10:    // Volume
                    rates = new double[] { 1e-6, 2.8316846592e-2, 1.6387064e-5, 1, 0.764554857984, 2.84130625e-5,
                        2.95735295625e-5, 4.54609e-3, 3.785411784e-3, 1e-3, 5.6826125e-4, 4.73176473e-4, 1.1365225e-3, 9.46352946e-4 };
                    break;
                case 11:    // Weight / Mass
                    rates = new double[] { 2e-4, 1e-5, 1e-4, 1e-2, 1e-3, 0.1, 1, 1016.0469088, 1e-6,
                        0.028349523125, 0.45359237, 907.18474, 6.35029318/*= 1 / 0.157473044418*/, 1e3 };
                    break;
            }
            double rateResult = 1d;
            if (from >= 0 && to >= 0) rateResult = rates[from] / rates[to];
            return rateResult;
        }
        /// <summary>
        /// đổi giữa các đơn vị đo nhiệt độ
        /// </summary>
        public static double GetTemperature(int i1, int i2, double inp)
        {
            if (i1 == i2) return inp;
            switch (10 * i1 + i2)
            {
                case 01: return 1.8 * inp + 32;             // °C -> °F
                case 02: return inp + 273;                  // °C -> °K
                case 10: return 5.0 / 9 * (inp - 32);       // °F -> °C
                case 12: return 5.0 / 9 * (inp - 32) + 273; // °F -> °K
                case 20: return inp - 273;                  // °K -> °C
                case 21: return 1.8 * (inp - 273) + 32;     // °K -> °F
                default: return 1d;
            }
        }

        /*---------------------------------------------------------------------*/

        /// <summary>
        /// lấy đường dẫn registry key cần truy cập
        /// </summary>
        private static string GetRegistryPath(int index)
        {
            return string.Format("{0}\\{1}", RegistryPath, KeyNames[index]);
        }
        /// <summary>
        /// mở đường dẫn registry key cần truy cập
        /// </summary>
        private static RegistryKey OpenSubKey(int index)
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(index), true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(index));
            }
            return reg;
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
            reg = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistryPath);
                reg.SetValue(optionName[Config._00_CalculatorType], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._01_UseSep], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._02_ExtraFunction], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._03_ShowHistory], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._05_ShowPreview], 0, RegistryValueKind.DWord);
                result[Config._00_CalculatorType] = 0;
                result[Config._01_UseSep] = 0;
                result[Config._02_ExtraFunction] = 0;
                result[Config._03_ShowHistory] = 0;
                result[Config._05_ShowPreview] = 0;
                // ---------------------------
                result[Config._04_MemoryNumber] = "0";
                reg.SetValue(optionName[Config._04_MemoryNumber], result[Config._04_MemoryNumber], RegistryValueKind.String);
            }
            else
            {
                result[Config._00_CalculatorType] = GetValueFromValueKey(optionName[Config._00_CalculatorType], reg, 0, 3, 0);
                result[Config._01_UseSep] = GetValueFromValueKey(optionName[Config._01_UseSep], reg, 0, 1, 0);
                result[Config._02_ExtraFunction] = GetValueFromValueKey(optionName[Config._02_ExtraFunction], reg, 0, 7, 0);
                result[Config._03_ShowHistory] = GetValueFromValueKey(optionName[Config._03_ShowHistory], reg, 0, 1, 0);
                result[Config._04_MemoryNumber] = GetValueFromValueKey(optionName[Config._04_MemoryNumber], reg);
                result[Config._05_ShowPreview] = GetValueFromValueKey(optionName[Config._05_ShowPreview], reg, 0, 1, 0);
            }
            string[] convValues = optionName.SubArray(0, 6);
            DeleteNonUsedValue(reg, convValues);
            DeleteNonUsedKey(reg, KeyNames);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(0), true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(0));
                reg.SetValue(optionName[Config._06_TypeUnit], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._07_AutoCalculate], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._08_DateMethod], 0, RegistryValueKind.DWord);
                result[Config._06_TypeUnit] = 0;
                result[Config._07_AutoCalculate] = 0;
                result[Config._08_DateMethod] = 0;
            }
            else
            {
                result[Config._06_TypeUnit] = GetValueFromValueKey(optionName[Config._06_TypeUnit], reg, 0, 11, 0);
                result[Config._07_AutoCalculate] = GetValueFromValueKey(optionName[Config._07_AutoCalculate], reg, 0, 1, 0);
                result[Config._08_DateMethod] = GetValueFromValueKey(optionName[Config._08_DateMethod], reg, 0, 2, 0);
            }

            convValues = optionName.SubArray(6, 3);
            DeleteNonUsedValue(reg, convValues);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(1), true);
            if (reg == null)    // create and assign default value
            {
                result[Config._09_CollapseSpeed] = 10;
                result[Config._10_FastFactorial] = 0;
                result[Config._11_SignInteger] = 1;
                result[Config._12_ReadDictionary] = 1;
                result[Config._13_StoreHistory] = 0;
                result[Config._14_FastInput] = 0;
                result[Config._15_CountMethod] = 1;
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(1));
                reg.SetValue(optionName[Config._09_CollapseSpeed], (int)result[Config._09_CollapseSpeed], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._10_FastFactorial], (int)result[Config._10_FastFactorial], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._11_SignInteger], (int)result[Config._11_SignInteger], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._12_ReadDictionary], (int)result[Config._12_ReadDictionary], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._13_StoreHistory], (int)result[Config._13_StoreHistory], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._14_FastInput], (int)result[Config._14_FastInput], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._15_CountMethod], (int)result[Config._15_CountMethod], RegistryValueKind.DWord);
            }
            else
            {
                result[Config._09_CollapseSpeed] = GetValueFromValueKey(optionName[Config._09_CollapseSpeed], reg, 4, 12, 10);
                result[Config._10_FastFactorial] = GetValueFromValueKey(optionName[Config._10_FastFactorial], reg, 0, 1, 0);
                result[Config._11_SignInteger] = GetValueFromValueKey(optionName[Config._11_SignInteger], reg, 0, 1, 1);
                result[Config._12_ReadDictionary] = GetValueFromValueKey(optionName[Config._12_ReadDictionary], reg, 0, 1, 1);
                result[Config._13_StoreHistory] = GetValueFromValueKey(optionName[Config._13_StoreHistory], reg, 0, 1, 0);
                result[Config._14_FastInput] = GetValueFromValueKey(optionName[Config._14_FastInput], reg, 0, 1, 0);
                result[Config._15_CountMethod] = GetValueFromValueKey(optionName[Config._15_CountMethod], reg, 0, 1, 1);
            }
            convValues = optionName.SubArray(9);
            DeleteNonUsedValue(reg, convValues);

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
            string rs = (string)reg.GetValue(optionName, "A non-number cannot be assigned here");
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
            var reg = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
            if (reg == null) return;
            DeleteNonUsedKey(reg, KeyNames);
            string[] convValues = optName.SubArray(0, 6);
            DeleteNonUsedValue(reg, convValues);

            reg.SetValue(optName[Config._00_CalculatorType], config[Config._00_CalculatorType], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._01_UseSep], config[Config._01_UseSep], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._02_ExtraFunction], config[Config._02_ExtraFunction], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._03_ShowHistory], config[Config._03_ShowHistory], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._04_MemoryNumber], config[Config._04_MemoryNumber], RegistryValueKind.String);
            reg.SetValue(optName[Config._05_ShowPreview], config[Config._05_ShowPreview], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(0), true);
            if (reg == null) return;
            reg.SetValue(optName[Config._06_TypeUnit], config[Config._06_TypeUnit], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._07_AutoCalculate], config[Config._07_AutoCalculate], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._08_DateMethod], config[Config._08_DateMethod], RegistryValueKind.DWord);

            convValues = optName.SubArray(6, 3);
            DeleteNonUsedValue(reg, convValues);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(1), true);
            if (reg == null) return;
            reg.SetValue(optName[Config._09_CollapseSpeed], config[Config._09_CollapseSpeed], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._10_FastFactorial], config[Config._10_FastFactorial], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._11_SignInteger], config[Config._11_SignInteger], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._12_ReadDictionary], config[Config._12_ReadDictionary], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._13_StoreHistory], config[Config._13_StoreHistory], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._14_FastInput], config[Config._14_FastInput], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._15_CountMethod], config[Config._15_CountMethod], RegistryValueKind.DWord);

            convValues = optName.SubArray(9);
            DeleteNonUsedValue(reg, convValues);
        }
        /// <summary>
        /// get subarray from an existing array
        /// </summary>
        /// <typeparam name="T">type of array</typeparam>
        /// <param name="data">source array</param>
        /// <param name="index">index from source array</param>
        /// <param name="length">length of new array, -1 if user leave blank when calling this function</param>
        /// <returns>destination array</returns>
        private static T[] SubArray<T>(this T[] data, int index, int length = -1)
        {
            if (length == -1)
            {
                length = data.Length - index;
            }
            return data.Skip(index).Take(length).ToArray();
        }
        /// <summary>
        /// xoá các value thừa trong key hiện tại của reg
        /// </summary>
        /// <param name="reg">registry key hiện tại</param>
        /// <param name="optName">list tên option</param>
        private static void DeleteNonUsedValue(RegistryKey reg, string[] optName)
        {
            string[] subValues = reg.GetValueNames();
            var listToDelete = subValues.Where(w => !optName.Contains(w));
            foreach (string key in listToDelete)
            {
                reg.DeleteValue(key);
            }
        }
        /// <summary>
        /// xoá các key thừa trong key hiện tại của reg
        /// </summary>
        /// <param name="reg">registry key hiện tại</param>
        /// <param name="optName">list tên option</param>
        private static void DeleteNonUsedKey(RegistryKey reg, string[] optName)
        {
            string[] subValues = reg.GetSubKeyNames();
            var listToDelete = subValues.Where(w => !optName.Contains(w));
            foreach (string key in listToDelete)
            {
                reg.DeleteSubKey(key);
            }
        }
        /// <summary>
        /// lấy ra property Value của object FactorialObject và đưa vào 1 mảng
        /// </summary>
        /// <param name="dict">list FactorialObject cần lấy</param>
        public static string[] SelectValue(this List<FactorialObject> dict)
        {
            return dict.Select(s => s.Value).ToArray();
        }
        /// <summary>
        /// nạp giá trị giai thừa số lớn đã tính được trước đó vào biến
        /// </summary>
        public static void ReadDictionary(ref List<FactorialObject> dict)
        {
            RegistryKey reg = OpenSubKey(2);

            string[] namelist = reg.GetValueNames();
            string[] selected = dict.SelectValue();
            // list những giá trị chưa có trong dictionary memory
            var existedInNameList = namelist.Where(w => !selected.Contains(w));
            // chỉ thêm những giá trị chưa có vào dictionary memory
            // còn có rồi thì ưu tiên những giá trị đã được tính lại
            foreach (string item in existedInNameList)
            {
                try
                {
                    dict.Add(new FactorialObject()
                    {
                        Value = item,
                        Result = reg.GetValue(item) as string,
                        IsRecalculate = false,    // hơi thừa
                    });
                }
                catch { }
            }
            reg.Close();
        }
        /// <summary>
        /// lưu từ điển giai thừa vào registry trước khi thoát
        /// </summary>
        public static void SaveDictionary(List<FactorialObject> factDict)
        {
            RegistryKey reg = OpenSubKey(2);
            foreach (FactorialObject fo in factDict)
            {
                reg.SetValue(fo.Value, fo.Result);
            }
            reg.Close();
        }

        /// <summary>
        /// load history từ registry khi onload
        /// </summary>
        /// <returns></returns>
        public static HistoryObject[] ReadHistory()
        {
            RegistryKey reg = OpenSubKey(3);
            var names = reg.GetValueNames();
            HistoryObject[] ho = new HistoryObject[100];
            string[] keyValues = new string[names.Length];
            //Parser p = new Parser();
            for (int i = 0; i < names.Length; i++)
            {
                ho[i] = new HistoryObject();
                ho[i].Expression = Convert.ToString(reg.GetValue(names[i]));
                ho[i].Result = names[i].Substring(3);
                //p.EvaluateSci(ho[i].Expression);
                //ho[i].Result = p.ToString();
            }
            reg.Close();
            return ho;
        }
        /// <summary>
        /// lưu lịch sử các phép tính khoa học
        /// </summary>
        public static void SaveHistory(HistoryObject[] ho)
        {
            #region xoá key đi rồi tạo lại
            //try
            //{
            //    Registry.CurrentUser.DeleteSubKey(GetRegistryPath(3));
            //}
            //catch { /* không tìm thấy để xoá thì thôi */ }

            //RegistryKey reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(3)); 
            #endregion

            #region không xoá key, chỉ xoá hết value
            RegistryKey reg = OpenSubKey(3);
            string[] namelist = reg.GetValueNames();
            foreach (string name in namelist)
            {
                reg.DeleteValue(name);
            } 
            #endregion

            for (int i = 0; i < ho.Length; i++)
            {
                if (ho[i] == null) break;
                reg.SetValue(string.Format("{0:00}_{1}", i, ho[i].Result), ho[i].Expression);
            }
            reg.Close();
        }
        /// <summary>
        /// xoá lịch sử các phép tính khoa học
        /// </summary>
        public static void ClearHistory()
        {
            RegistryKey reg = OpenSubKey(3);
            string[] values = reg.GetValueNames();
            foreach (string value in values)
            {
                reg.DeleteValue(value);
            }
            reg.Close();
        }
        /// <summary>
        /// tìm hạng của mảng 1 chiều biến kiểu byte (index của phần tử khác 0 cuối cùng trong mảng)
        /// </summary>
        /// <param name="arr">mảng 1 chiều</param>
        /// <returns>hạng của mảng</returns>
        public static int GetArrayRank(this byte[] arr)
        {
            int index = Array.FindIndex(arr.Reverse().ToArray(), i => i != 0);
            return arr.Count() - index - 1;
        }

        public static bool EndsWith(this System.Text.StringBuilder sb, string test)
        {
            if (sb.Length < test.Length)
                return false;

            string end = sb.ToString(sb.Length - test.Length, test.Length);
            return end.Equals(test);
        }

        public static bool StartsWith(this System.Text.StringBuilder sb, string test)
        {
            if (sb.Length < test.Length)
                return false;

            string start = sb.ToString(0, test.Length);
            return start.Equals(test);
        }

    }
}