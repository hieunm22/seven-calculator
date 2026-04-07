using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Calculator
{
    // true.GetHashCode() = 1
    // false.GetHashCode() = 0
    /// <summary>
    /// tổng hợp các method
    /// </summary>
    class Misc
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
        public static string Group(string input, int num, string sep)
        {
            string outp = input;
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
            while (stdStr.Contains("  ")) stdStr = stdStr.Replace("  ", " ");
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
            if (stdStr.StartsWith(" - ")) stdStr = "-" + stdStr.Substring(3);
            if (stdStr.StartsWith(" + ")) stdStr = stdStr.Substring(3);
            return stdStr.Trim();
        }
        /// <summary>
        /// kiểm tra xem năm đưa vào có phải năm nhuận hay không (is Bissextile)
        /// </summary>
        /// <param name="year">năm đưa vào</param>
        /// <returns>true nếu year là năm nhuận, false nếu là năm thường</returns>
        private static bool isBis(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
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
            // differ[0] - year
            differ[0] = dtp2.Year - dtp1_Temp.Year;
            dtp1_Temp = dtp1.AddYears(differ[0]);
            if (dtp1_Temp.DayOfYear > dtp2.DayOfYear) differ[0]--;

            // differ[1] - month
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[1] = dtp2.Month - dtp1_Temp.Month;
            if (isBis(dtp1.Year) && isBis(dtp2.Year))   // chưa rõ là && hay ||
            {
                if (dtp1/*_Temp*/.Day > dtp2.Day) differ[1]--;
            }
            else
            {
                if (dtp1_Temp.Day > dtp2.Day) differ[1]--;
            }
            if (differ[1] < 0) differ[1] += 12;

            // differ[3] - day
            dtp1_Temp = dtp1_Temp.AddMonths(differ[1]);
            int diff3 = dtp2.DayOfYear - dtp1_Temp.DayOfYear;
            // neu diff3 < 0 thi moi +365/366, khong thi thoi
            if (diff3 < 0)
            {
                // năm nhuận + 366, năm thường +365
                diff3 += (isBis(dtp1_Temp.Year) ? 366 : 365);
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
        /// <param name="optionName"></param>
        /// <returns></returns>
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
                result[7] = GetValueFromValueKey(optionName[7], reg, 0, 1, 0);
            }

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\OtherOptions", true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey("Software\\SevenCalculator\\OtherOptions");
                reg.SetValue(optionName[08], 10, RegistryValueKind.DWord);
                reg.SetValue(optionName[09], 1, RegistryValueKind.DWord);
                reg.SetValue(optionName[10], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[11], 1, RegistryValueKind.DWord);
                result[08] = 10;
                result[09] = 1;
                result[10] = 0;
                result[11] = 1;
            }
            else
            {
                result[08] = GetValueFromValueKey(optionName[08], reg, 0, 12, 10);
                result[09] = GetValueFromValueKey(optionName[09], reg, 0, 1, 1);
                result[10] = GetValueFromValueKey(optionName[10], reg, 0, 1, 0);
                result[11] = GetValueFromValueKey(optionName[11], reg, 0, 1, 1);
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
            reg.SetValue(optName[0], config[0], RegistryValueKind.DWord);
            reg.SetValue(optName[1], config[1], RegistryValueKind.DWord);
            reg.SetValue(optName[2], config[2], RegistryValueKind.DWord);
            reg.SetValue(optName[3], config[3], RegistryValueKind.DWord);
            reg.SetValue(optName[4], config[4], RegistryValueKind.String);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\UnitConversion", true);
            reg.SetValue(optName[5], config[5], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\DateCalculation", true);
            reg.SetValue(optName[6], config[6], RegistryValueKind.DWord);
            reg.SetValue(optName[7], config[7], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey("Software\\SevenCalculator\\OtherOptions", true);
            reg.SetValue(optName[08], config[08], RegistryValueKind.DWord);
            reg.SetValue(optName[09], config[09], RegistryValueKind.DWord);
            reg.SetValue(optName[10], config[10], RegistryValueKind.DWord);
            reg.SetValue(optName[11], config[11], RegistryValueKind.DWord);
        }

        public static int NumberOfOpenWOClose(string expression)
        {
            var regEx = new Regex(@"\(");
            int matches = regEx.Matches(expression).Count;
            regEx = new Regex(@"\)");
            matches -= regEx.Matches(expression).Count;
            return matches;
        }

        public static double Round(double resultMort)
        {
            double result = Math.Round(resultMort, 6);
            if (Math.Abs(result - resultMort) < Math.Pow(10, -6 - 3)) return result;
            return resultMort;
        }

        public static int CloseBracket(string input, string openBracket, string closeBracket, int openIndex)
        {
            int openLevel = 1;
            int open = openIndex;
            int close = 0;
            int start = open;
            while (openLevel > 0)
            {
                close = input.IndexOf(closeBracket, start + closeBracket.Length);
                open = input.IndexOf(openBracket, start + openBracket.Length);
                if (open < close && open > 0)
                {
                    openLevel++;
                    start = open;
                }
                else
                {
                    openLevel--;
                    start = close;
                }
            }
            return start;
        }
    }
    /// <summary>
    /// lớp các phương thức liên quan đến hệ cơ số
    /// </summary>
    class Binary
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
                        string result = "";
                        while (realNumber > 0)
                        {
                            remainder = byte.Parse((realNumber - dest * (realNumber / dest).Floor()).IntString);
                            realNumber = (realNumber / dest).Floor();
                            result = remainder.ToString() + result;
                        }
                        if (dest == 2 && result.Length > 64) throw new Exception("Overflow");
                        if (dest == 8 && result.Length > 22) throw new Exception("Overflow");
                        return result;
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
            long result = 0;
            BigNumber bs = 1;
            char[] memberChar = decimalNumber.ToCharArray();
            //if (memberChar.Length < Size) Size = memberChar.Length;
            for (int i = memberChar.Length - 1; i >= 0; i--)
            {
                if (memberChar[i] < 58 && memberChar[i] >= 48)
                {
                    result += (long)ulong.Parse(((memberChar[i] - 48) * bs).StrValue);
                }
                else if (memberChar[i] < 71 && memberChar[i] >= 65) //A-F
                {
                    result += (long)ulong.Parse(((memberChar[i] - 55) * bs).StrValue);
                }
                else    //a-f
                {
                    result += (long)ulong.Parse(((memberChar[i] - 87) * bs).StrValue);
                }
                bs *= from;
            }
            BigNumber ketqua = result;
            if (Size == memberChar.Length && memberChar[0] == '1' )
            {
                if (result > 0 && isSign) ketqua -= BigNumber.Two.Pow(Size);
                if (result < 0 && !isSign) ketqua += BigNumber.Two.Pow(Size);
            }
            return ketqua.StrValue;
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
}