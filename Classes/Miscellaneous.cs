using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator
{
    // true.GetHashCode() = 1
    // false.GetHashCode() = 0
    class Misc
    {
        /// <summary>
        /// chia các hàng đơn vị của mot_so_thuc thành từng nhóm 3 số
        /// </summary>
        /// <param name="obj">chuỗi cần chia</param>
        /// <returns>1000000000 thành 1.000.000.000</returns>
        public static string grouping(object obj)
        {
            if (obj.ToString() == "0") return "0";
            string output = obj.ToString();
            // biến này lưu giá trị phần nguyên của obj

            int comma = output.IndexOf(DecimalSym);
            if (comma > 0) output = output.Substring(0, comma);	//cắt phần lẻ trước khi chia

            if (obj.ToString()[0] == '-') output = output.Substring(1);	// bỏ dấu âm ở đầu nếu có
            int temp = output.Length; // phải lưu độ dài biến output vì khi chia, độ dài biến output thay đổi liên tục
            for (int i = 3; i < temp; i += 3)   // bắt đầu chia
            {
                output = output.Insert(temp - i, ThousandSym);
            }
            if (comma > 0)  //thêm phần lẻ vào chuỗi đã được nhóm
            {
                output += obj.ToString().Substring(comma);
            }
            if (obj.ToString()[0] == '-') output = "-" + output;

            return output;
        }
        /// <summary>
        /// chia các hàng đơn vị của 1 số thực thành từng nhóm
        /// </summary>
        /// <param name="input">chuỗi cần chia</param>
        /// <param name="num">số kí tự của từng nhóm</param>
        public static string grouping(string input, int num)
        {
            string outp = input;
            int bit = outp.Length - num;
            // them 1 dau cach vao giua nhom 3 so o xau ket qua
            while (bit > 0)
            {
                outp = outp.Insert(bit, " ");
                bit -= num;
            }
            return outp;
        }
        /// <summary>
        /// loại bỏ các dấu phân cách hàng đơn vị của 1 số
        /// </summary>
        /// <param dwordname="inputstr">xâu số nhập vào</param>
        public static string de_group(string inputstr)
        {
            int inp_length = inputstr.Length;
            // bo cac dau . o xau dau vao
            while (inputstr.IndexOf(ThousandSym) > 0) inputstr = inputstr.Replace(ThousandSym, "");
            while (inputstr.IndexOf(" ") > 0) inputstr = inputstr.Replace(" ", "");
            return inputstr;
        }
        /// <summary>
        /// kiểm tra xem 1 xâu nhập vào có phải là số hay không
        /// </summary>
        /// <param dwordname="input_str">chuỗi cần kiểm tra</param>
        public static bool isNumber(string str)
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
        private static string getDecimalSym()
        {
            return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }
        /// <summary>
        /// kí tự phân cách giữa phần nguyên và phần lẻ của số
        /// </summary>
        public static string DecimalSym
        {
            get { return getDecimalSym(); }
        }
        /// <summary>
        /// lấy kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        private static string getThousandSym()
        {
            return CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
        }
        /// <summary>
        /// kí tự phân cách giữa các hàng đơn vị với nhau: tỷ, triệu, nghìn
        /// </summary>
        public static string ThousandSym
        {
            get { return getThousandSym(); }
        }
        /// <summary>
        /// thêm dấu * vào các vị trí thích hợp
        /// </summary>
        /// <param name="expression">biểu thức cần chèn dấu *</param>
        public static string InsertMultiplyChar(string expression)
        {
            string result = expression;
            Regex regEx = new Regex(@"(?<=[\d\)])(?=[a-df-z\(])|(?<=pi)(?=[^\+\-\*\/\\^!)])|"
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
            string result = expression;
            Regex reg = new Regex(@"(-|)([a-z]{2,})([\+-]?\d+,*\d*[eE][\+-]?\d+|[\+-]?\d+,*\d*)");
            Match m = reg.Match(result);
            while (m.Success)
            {
                result = result.Replace(m.Value, string.Format("{0}{1}({2})", m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value));
                m = reg.Match(result);
            }
            return result;
        }
        /// <summary>
        /// xoá các dấu ngoặc thừa trong biểu thức
        /// 2 + sqr((((-3)))) --> 2 + sqr(-3)
        /// 2 + ((((3)))) --> 2 + 3
        /// </summary>
        /// <param name="pattern">chuỗi nhận dạng</param>
        /// <param name="expression">chuỗi cần thay thế</param>
        public static string RemoveBracket(string expression)
        {
            // "((-7))" -> xoa bot 1 cap dau ngoac -> (-7)
            Regex reg = new Regex(@"(\(\()([\+-]?\d+,*\d*[eE][\+-]?\d+|[\-\+]?\d+,*\d*)(\)\))");    // ((mot_so_thuc))
            Match m = reg.Match(expression);
            while (m.Success)
            {
                expression = expression.Replace(m.Value, m.Value.Substring(1, m.Value.Length - 2));
                m = reg.Match(expression);
            }
            return expression;
        }
        /// <summary>
        /// kiểm tra xem năm đưa vào có phải năm nhuận hay không (is Bissextile)
        /// </summary>
        /// <param name="year">năm đưa vào</param>
        /// <returns>true nếu year là năm nhuận, false nếu ngược lại</returns>
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
            dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);
            double dist = (dt2 - dt1).Days;
            return (int)dist;

            #region CODE thủ công
            //if (dt1 > dt2) { DateTime dtp1_Temp = dt1; dt1 = dt2; dt2 = dtp1_Temp; }
            //int dist = 0;
            //if (dt1.Year == dt2.Year) return dt2.DayOfYear - dt1.DayOfYear;
            //else
            //{
            //   //tinh so ngay tinh tu ngay dung truoc den ngay 31/12 cua nam do
            //    dist = 365 - dt1.DayOfYear + isBis(dt1.Year).GetHashCode();

            //   if (dt2.Year - dt1.Year == 1) dist += dt2.DayOfYear;
            //   else
            //   {
            //       for (int j = dt1.Year + 1; j <= dt2.Year - 1; j++)
            //       {
            //           dist += 365 + isBis(j).GetHashCode();
            //       }
            //       dist += dt2.DayOfYear;
            //   }
            //}
            //return dist;
            #endregion
        }
        /// <summary>
        /// chênh lệch giữa 2 ngày theo năm, tháng, tuần, ngày
        /// </summary>
        /// <param dwordname="dt1">ngày thứ nhất, luôn là ngày phía trước dt2, dtp1 < dtp2</param>
        /// <param dwordname="dt2">ngày thứ hai,  luôn là ngày phía sau   dt1, dtp1 < dtp2</param>
        public static int[] differencesTimes(DateTime dtp1, DateTime dtp2)
        {
			DateTime dtp1_Temp;
            if (dtp1 > dtp2) { dtp1_Temp = dtp1; dtp1 = dtp2; dtp2 = dtp1_Temp; }
            int[] differ = new int[4];
            dtp1_Temp = dtp1;
            // differ[0] - year - DONE
            differ[0] = dtp2.Year - dtp1_Temp.Year;
            dtp1_Temp = dtp1.AddYears(differ[0]);
            if (dtp1_Temp.DayOfYear > dtp2.DayOfYear) differ[0]--;

            // differ[1] - month - DONE
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[1] = dtp2.Month - dtp1_Temp.Month;
            if (isBis(dtp1.Year) && isBis(dtp2.Year))   // chua ro la && hay ||
            {
                if (dtp1/*_Temp*/.Day > dtp2.Day) differ[1]--;
            }
            else
            {
                if (dtp1_Temp.Day > dtp2.Day) differ[1]--;
            }
            if (differ[1] < 0) differ[1] += 12;

            // differ[3] - day - DONE
            int diff3 = 0;
            dtp1_Temp = dtp1_Temp.AddMonths(differ[1]);
            diff3 = dtp2.DayOfYear - dtp1_Temp.DayOfYear;
            // neu diff3 < 0 thi moi +365/366, khong thi thoi
            if (diff3 < 0)
            {
                // năm nhuận + 366, năm thường +365
                //if (isBis(dtp1_Temp.Year)) diff3 += 366;
                //else diff3 += 365;
                diff3 += (365 + isBis(dtp1_Temp.Year).GetHashCode());
            }
            differ[3] = diff3 % 7;

            // differ[2], từ 3 mới suy ra 2 - week - ALWAYS DONE
            differ[2] = diff3 / 7;
            return differ;
        }

        /*---------------------------------------------------------------------*/

        /// <summary>
        /// biến lưu trữ tỷ lệ giữa các đơn vị đo
        /// </summary>
        static double[][] rates = new double[11][];
        /// <summary>
        /// lấy tỷ lệ giữa các đơn vị đo
        /// </summary>
        public static double getRate(int type, int from, int to)
        {
            rates[00] = new double[] { Math.PI / 180, Math.PI / 200, 1.0 };
            rates[01] = new double[] { 4046.8564224, 1e4,
                1e-4, 0.09290304, 6.4516E-4, 1e6, 1.0, 2589988.110336, 1e-6, 0.83612736 };
            rates[02] = new double[] { 1055.05585, 4.1868, 1.60217653e-19, 1.3558179483314, 1.0, 4186.8, 1e3 };
            rates[03] = new double[] { 1e-10, 0.01, 20.1168, 1.8288, 0.3048,
                0.1016, 0.0254, 1e3, 0.201168, 1.0, 1e-6,1609.344,
                1e-3, 1e-9,1852, 0.0042175176, 5.0292, 0.2286, 0.9144};
            //rates[04] = new double[] { 17.5842642, 0.0225969658, 745.699872, 1000.0, 1.0 };
            rates[04] = new double[] { 17.58426666666667, 0.0225969658055233, 745.6998715822702, 1000.0, 1.0 };
            rates[05] = new double[] { 101325.0, 1e5, 1e3, 133.322368, 1.0, 6894.75729 };
            rates[07] = new double[] { 8.64e4, 3.6e3, 1e-6, 1e-3, 60.0, 1.0, 6.048e5 };
            rates[08] = new double[] { 0.01, 0.3048, 0.2777777777777778, 0.5144444444444444, 340.2933, 1.0, 0.44704 };
            rates[09] = new double[] { 1e-6, 2.8316846592e-2, 1.6387064e-5, 1.0, 0.764554857984, 2.84130625e-5, 2.95735295625e-5,
                4.54609e-3, 3.785411784e-3, 1e-3, 5.6826125e-4, 4.73176473e-4, 1.1365225e-3, 9.46352946e-4 };
            rates[10] = new double[] { 2e-4, 1e-5, 1e-4, 1e-2, 1e-3, 0.1, 1.0, 1016.0469088, 1e-6,
                0.028349523125, 0.45359237, 907.18474, /*1 / 0.157473044418 =*/6.35029318, 1e3 };
            double rateResult = 1;
            if (from >= 0 && to >= 0) rateResult = rates[type][from] / rates[type][to];
            return rateResult;
        }
        /// <summary>
        /// đổi giữa các đơn vị đo nhiệt độ
        /// </summary>
        public static double getTemperature(int i1, int i2, double inp)
        {
            double temperature = 1;
            switch (i1)
            {
                case 0:    // °C
                    if (i2 == 1) temperature = 1.8 * inp + 32;   // °F
                    if (i2 == 2) temperature = inp + 273;        // °K
                    break;
                case 1:    // °F
                    if (i2 == 0) temperature = 5F / 9F * (inp - 32);         // °C
                    if (i2 == 2) temperature = 5F / 9F * (inp - 32) + 273;   // °K
                    break;
                case 2:    // °K
                    if (i2 == 0) temperature = inp - 273;                // °C
                    if (i2 == 1) temperature = 1.8 * (inp - 273) + 32;   // °F
                    break;
            }
            if (i1 == i2) temperature = inp;
            return (double) temperature;
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
        /// tính a^n với a, n nguyên
        /// </summary>
        public static ulong powan(int coso, int somu)
        {
            if (somu == 64) return ulong.MaxValue;
            ulong luythua = 1;
            for (int j = 1; j <= somu; j++)
            {
                luythua *= (ulong)coso;
            }
            return luythua;
        }
        /// <summary>
        /// đổi 1 số từ hệ 10 sang các hệ khác
        /// </summary>
        public static string dec_to_other(string decimalNumber, /*int from, */int dest, int size)
        {
            // from nguon, dest dest
            if (decimalNumber == "0") return "0";
            string result = string.Empty;
            BigNumber realNumber = decimalNumber;

            int remainder;
            if (dest != 2)
            {
                //result = Convert.ToString((long)realNumber, dest).ToUpper();

                #region code moi
                while (realNumber > 0)
                {
                    remainder = int.Parse((realNumber - dest * (realNumber / dest).Floor()).IntString);
                    realNumber = (realNumber / dest).Floor();
                    if (remainder < 10)
                        result = remainder.ToString() + result;
                    else
                        result = Convert.ToChar(remainder + 55) + result;
                }
                #endregion
            }
            else
            {
                switch (size)
                {
                    case 08:
                        return Convert_Byte(ulong.Parse(realNumber.IntString));
                    case 16:
                        return Convert_Word(ulong.Parse(realNumber.IntString));
                    case 32:
                        return ConvertDword(ulong.Parse(realNumber.IntString));
                    case 64:
                        return ConvertQword(ulong.Parse(realNumber.IntString));
                }
            }
            return result;
        }
        /// <summary>
        /// đổi 1 số từ hệ khác 10 sang hệ 10
        /// </summary>
        public static string other_to_dec(string decimalNumber, int from/*, int dest*/, int Size)
        {
            // from nguon, dest dich
            ulong result = 0;
            BigNumber bs = 1;
            char[] memberChar = decimalNumber.ToCharArray();
            if (memberChar.Length < Size) Size = memberChar.Length;
            for (int i = Size - 1; i >= 0; i--)
            {
                if (memberChar[i] < 58 && memberChar[i] >= 48)
                    result += ulong.Parse(((memberChar[i] - 48) * bs).StringValue);
                else if (memberChar[i] < 71 && memberChar[i] >= 65)
                    result += ulong.Parse(((memberChar[i] - 55) * bs).StringValue);
                else
                    result += ulong.Parse(((memberChar[i] - 87) * bs).StringValue);
                bs *= from;
            }
            return result.ToString();
            //return Convert.ToUInt64(decimalNumber, from).ToString();
        }
        /// <summary>
        /// chuẩn hoá xâu - cắt những số 0 thừa ở đầu xâu
        /// </summary>
        public static string standardString(string str)
        {
            string result = str.TrimStart(new char[] { '0' });
            if (result == "") result = "0";
            return result;
        }

        #region code cu
        //public static string Convert_Byte(ulong decimalNumber)
        //{
        //    Byte n = (Byte)decimalNumber;
        //    string result = Convert.ToString(n, 2);
        //    return result;
        //}

        //public static string Convert_Word(ulong decimalNumber)
        //{
        //    UInt16 n = (UInt16)decimalNumber;
        //    string result = Convert.ToString(n, 2);
        //    return result;
        //}

        //public static string ConvertDword(ulong decimalNumber)
        //{
        //    UInt32 n = (UInt32)decimalNumber;
        //    string result = Convert.ToString(n, 2);
        //    return result;
        //}

        //public static string ConvertQword(ulong decimalNumber)
        //{
        //    UInt64 n = decimalNumber;
        //    string result = Convert.ToString((Int64)n, 2);
        //    return result;
        //}
        #endregion

        public static string Convert_Byte(ulong decimalNumber)
        {
            Byte n = (Byte)decimalNumber;
            string result = Convert.ToString(n, 2);
            return result;
        }

        public static string Convert_Word(ulong decimalNumber)
        {
            UInt16 n = (UInt16)decimalNumber;
            string result = Convert.ToString(n, 2);
            return result;
        }

        public static string ConvertDword(ulong decimalNumber)
        {
            UInt32 n = (UInt32)decimalNumber;
            string result = Convert.ToString(n, 2);
            return result;
        }

        public static string ConvertQword(ulong decimalNumber)
        {
            string result = string.Empty;
            BigNumber realNumber = decimalNumber.ToString();

            int remainder;
            while (realNumber > 0)
            {
                remainder = int.Parse((realNumber - 2 * (realNumber / 2).Floor()).IntString);
                realNumber = (realNumber / 2).Floor();
                result = remainder.ToString() + result;
            }
            return result;
        }
    }
}