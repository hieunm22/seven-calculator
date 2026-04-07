using System;
using System.Text.RegularExpressions;

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
            // biến này lưu giá trị phần nguyên của comboBoxItemMember

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
            // them 1 dau cach vao giua nhom 3 so o xau ket qua
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
        public static string InsertMultiplyChar(string expression)
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
        public static string InsertBracket(string expression)
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
        public static string RemoveBracket(string expression)
        {
            // "((-7))" -> xoa bot 1 capacity dau ngoac -> (-7)
            Regex reg = new Regex(@"(\(\()([\+-]?\d+,*\d*[eE][\+-]?\d+|[\-\+]?\d+,*\d*)(\)\))");    // ((mot_bieu_thuc))
            expression = expression.Replace(DecimalSeparator, ",");
            Match m = reg.Match(expression);
            while (m.Success)
            {
                expression = expression.Replace(m.Value, m.Value.Substring(1, m.Value.Length - 2));
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
            differ[3] = diff3 % 7;

            // differ[2], từ 3 mới suy ra 2 - week
            differ[2] = diff3 / 7;
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
            if (type == 0) rates = new double[] { Math.PI / 180, Math.PI / 200, 1 };
            if (type == 1) rates = new double[] { 4046.8564224, 1e4,
                1e-4, 0.09290304, 6.4516E-4, 1e6, 1, 2589988.110336, 1e-6, 0.83612736 };
            if (type == 2) rates = new double[] { 1055.05585, 4.1868, 1.60217653e-19, 1.3558179483314, 1, 4186.8, 1e3 };
            if (type == 3) rates = new double[] { 1e-10, 0.01, 20.1168, 1.8288, 0.3048,
                0.1016, 0.0254, 1e3, 0.201168, 1, 1e-6,1609.344, 1e-3, 1e-9,1852, 0.0042175176, 5.0292, 0.2286, 0.9144};
            if (type == 4) rates = new double[] { 17.58426666666667, 0.0225969658055233, 745.6998715822702, 1000.0, 1 };
            if (type == 5) rates = new double[] { 101325.0, 1e5, 1e3, 133.322368, 1, 6894.75729 };
            if (type == 7) rates = new double[] { 8.64e4, 3.6e3, 1e-6, 1e-3, 60.0, 1, 6.048e5 };
            if (type == 8) rates = new double[] { 0.01, 0.3048, 0.2777777777777778, 0.5144444444444444, 340.2933, 1, 0.44704 };
            if (type == 9) rates = new double[] { 1e-6, 2.8316846592e-2, 1.6387064e-5, 1, 0.764554857984, 2.84130625e-5, 2.95735295625e-5,
                4.54609e-3, 3.785411784e-3, 1e-3, 5.6826125e-4, 4.73176473e-4, 1.1365225e-3, 9.46352946e-4 };
            if (type == 10) rates = new double[] { 2e-4, 1e-5, 1e-4, 1e-2, 1e-3, 0.1, 1, 1016.0469088, 1e-6,
                0.028349523125, 0.45359237, 907.18474, /*1 / 0.157473044418 =*/6.35029318, 1e3 };
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

        public static void WriteAllText(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
        }
    }
    /// <summary>
    /// lớp các phương thức liên quan đến hệ cơ số
    /// </summary>
    class Binary
    {
        /// <summary>
        /// kiểm tra 1 xâu có phải kiểu decimalNumber hay không
        /// </summary>
        public static bool CheckIsBin(string str)
        {
            Regex reg = new Regex("^[0-1]+$");

            return reg.IsMatch(str);
        }
        /// <summary>
        /// kiểm tra 1 xâu có phải kiểu oct hay không
        /// </summary>
        public static bool CheckIsOct(string str)
        {
            Regex reg = new Regex("^[0-7]+$");

            return reg.IsMatch(str);
        }
        /// <summary>
        /// kiểm tra 1 xâu có phải kiểu dec hay không
        /// </summary>
        public static bool CheckIsDec(string str)
        {
            Regex reg = new Regex("^[0-9]+$");

            return reg.IsMatch(str);
        }
        /// <summary>
        /// kiểm tra 1 xâu có phải kiểu hex hay không
        /// </summary>
        public static bool CheckIsHex(string str)
        {
            Regex reg = new Regex("^[0-9,A-F,a-f]+$");

            return reg.IsMatch(str);
        }
        /// <summary>
        /// đổi 1 số từ hệ 10 sang các hệ khác
        /// </summary>
        public static string dec_to_other(string decimalNumber, /*int from, */int dest, int size, bool isSign)
        {
            // from nguon, dest dest
            if (decimalNumber == "0") return "0";
            BigNumber realNumber = decimalNumber;
            if (realNumber < 0) realNumber += BigNumber.Two__.Pow(size);

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
                if (result > 0 && isSign) ketqua -= BigNumber.Two__.Pow(Size);
                if (result < 0 && !isSign) ketqua += BigNumber.Two__.Pow(Size);
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