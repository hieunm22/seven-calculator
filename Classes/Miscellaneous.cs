using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Calculator
{
    // true.GetHashCode() = 1
    // false.GetHashCode() = 0
    class Miscellaneous
    {
        /// <summary>
        /// chia các hàng đơn vị của 1 số thực thành từng nhóm
        /// </summary>
        /// <param dwordname="obj">chuỗi cần chia</param>
        public string grouping(object obj)
        {
            string outp = "";
            int comma = ("" + obj).IndexOf(getDecimalSym());
            int exper = ("" + obj).IndexOf("E");
            if (comma > 0) outp = ("" + obj).Substring(0, comma);
            else outp = "" + obj;
            if (exper > 0) outp = ("" + obj).Substring(0, exper);
            if (("" + obj)[0] == '-') outp = outp.Substring(1); 
            //int bit = outp.Length - 3;
            // them 1 dau cach vao giua nhom 3 so o xau ket qua
            //while (bit > 0 && outp.IndexOf(getDecimalSym()) < 0)
            //{
            //    outp = outp.Insert(bit, getThousandSym());
            //    bit -= 3;
            //}
            int temp = outp.Length;
            for (int i = 3; i < temp; i = i + 3)
            {
                outp = outp.Insert(temp - i, getThousandSym());
            }

            if (comma > 0 && exper < 0) outp += ("" + obj).Substring(comma);
            if (exper > 0) outp += ("" + obj).Substring(exper);
            if (("" + obj)[0] == '-') outp = "-" + outp;
            return outp;
        }
        /// <summary>
        /// chia các hàng đơn vị của 1 số thực thành từng nhóm
        /// </summary>
        /// <param name="inp">chuỗi cần chia</param>
        /// <param name="num">số kí tự của từng nhóm</param>
        /// <returns></returns>
        public string grouping(string inp, int num)
        {
            string outp = inp;
            int bit = outp.Length - num;
            //bool spacement = false;
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
        public string de_group(string inputstr)
        {
            int inp_length = inputstr.Length;
            // bo cac dau . o xau dau vao
            while (inputstr.IndexOf(getThousandSym()) > 0) 
                inputstr = inputstr.Replace(getThousandSym(), "");
            while (inputstr.IndexOf(" ") > 0) inputstr = inputstr.Replace(" ", "");
            return inputstr;
        }
        /// <summary>
        /// kiểm tra xem 1 xâu nhập vào có phải là số hay không
        /// </summary>
        /// <param dwordname="input_str">chuỗi cần kiểm tra</param>
        public bool isNumber(string str)
        {
            try
            {
                str = str.Trim();
                double foo = double.Parse(str);
                return (true);
            }
            catch
            {
                return (false);
            }
        }
        /// <summary>
        /// lấy dấu phân cách phần nguyên và phần lẻ của 1 số
        /// </summary>
        private string getDecimalSym()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Control Panel\\International");
            string ret = reg.GetValue("sDecimal").ToString();
            reg.Close();
            return ret;
        }
        /// <summary>
        /// lấy dấu phân cách hàng đơn vị của 1 số
        /// </summary>
        private string getThousandSym()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Control Panel\\International");
            return (string) reg.GetValue("sThousand");
        }
        /// <summary>
        /// kiểm tra xem năm đưa vào có phải năm nhuận hay không (is Bissextile)
        /// </summary>
        /// <param dwordname="year">true nếu year là năm nhuận, false nếu ngược lại</param>
        private bool isBis(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
        /// <summary>
        /// trả về giá trị là khoảng thời gian giữa 2 ngày trong control datetimempicker
        /// </summary>
        public int differenceBW2Dates(DateTime dtp1, DateTime dtp2)
        {
            double dist = dtp2.Subtract(dtp1).Days;
            dist = Math.Round(dist);
            return (int)Math.Abs(dist);
        }
        /// <summary>
        /// chênh lệch giữa 2 ngày theo năm, tháng, tuần, ngày
        /// </summary>
        /// <param dwordname="dtp1">ngày thứ nhất, luôn là ngày phía trước dtp2</param>
        /// <param dwordname="dtp2">ngày thứ hai, luôn là ngày phía sau dtp1</param>
        public int[] differencesTimes(DateTime dtp1, DateTime dtp2)
        {
            int[] differ = new int[4];
            DateTime dtp1_Temp = dtp1;
            // differ[0] - year - DONE
            differ[0] = dtp2.Year - dtp1_Temp.Year;
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[0] = differ[0] - (dtp1_Temp.DayOfYear > dtp2.DayOfYear).GetHashCode();

            // differ[1] - month - DONE
            dtp1_Temp = dtp1.AddYears(differ[0]);
            differ[1] = dtp2.Month - dtp1_Temp.Month;
            differ[1] = differ[1] - (dtp1_Temp.Day > dtp2.Day).GetHashCode();
            differ[1] = differ[1] + 12 * (differ[1] < 0).GetHashCode();

            // differ[3] - day - DONE
            int diff3 = 0;
            dtp1_Temp = dtp1_Temp.AddMonths(differ[1]);
            diff3 = dtp2.DayOfYear - dtp1_Temp.DayOfYear;
            // neu la nam nhuan thi +366, khong thi 365      
            // neu diff3 < 0 thi moi +365/366, khong thi thoi
            diff3 += (365 + isBis(dtp1_Temp.Year).GetHashCode()) * (diff3 < 0).GetHashCode();
            differ[3] = diff3 % 7;

            // differ[2], từ 3 mới suy ra 2 - week - ALWAYS DONE
            differ[2] = diff3 / 7;
            return differ;
        }

        /*---------------------------------------------------------------------*/

        /// <summary>
        /// biến lưu trữ tỷ lệ giữa các đơn vị đo
        /// </summary>
        double[][] rates = new double[11][];
        /// <summary>
        /// lấy tỷ lệ giữa các đơn vị đo
        /// </summary>
        public double getRate(int type, int i1, int i2)
        {
            rates[00] = new double[] { Math.PI / 180, Math.PI / 200, 1.0 };
            rates[01] = new double[] { 4046.8564224, 1e4, 
                1e-4, 0.09290304, 6.4516E-4, 1e6, 1.0, 2589988.110336, 1e-6, 0.83612736 };
            rates[02] = new double[] { 1055.05585, 4.184, 1.60217646e-19, 1.35581795, 1.0, 4184.0, 1e3 };
            rates[03] = new double[] { 1e-10, 0.01, 20.1168, 1.8288, 0.3048, 
                0.1016, 0.0254, 1e3, 0.201168, 1.0, 1e-6,1609.344,
                1e-3, 1e-9,1852, 0.004233333, 5.0292, 0.2286, 0.9144};
            rates[04] = new double[] { 17.5842642, 0.0225969658, 745.699872, 1000.0, 1.0 };
            rates[05] = new double[] { 101325.0, 1e5, 1e3, 133.322368, 1.0, 6894.75729 };
            rates[07] = new double[] { 8.64e4, 3.6e3, 1e-6, 1e-3, 60.0, 1.0, 6.048e5 };
            rates[08] = new double[] { 0.01, 0.3048, 1000.0 / 3600, 1852.0, 340.2933, 1.0, 0.44704 };
            rates[09] = new double[] { 1e-6, 2.8316846592e-2, 1.6387064e-5, 1.0, 0.764554857984, 2.84130625e-5, 2.957352956e-5,
                4.54609e-3, 3.785412e-3, 1e-3, 5.50610471358e-4, 4.5464e-4, 1.1365255e-3, 9.46352946e-4 };
            rates[10] = new double[] { 2e-4, 1e-5, 1e-4, 1e-2, 1e-3, 0.1, 1.0, 1016.04691, 1e-6, 
                0.0283495231, 0.45359237, 907.18474, /*1 / 0.157473044418 =*/6.35029318, 1e3 };
            double rr = 1;
            if (i1 >= 0 && i2 >= 0) rr = rates[type][i1] / rates[type][i2];
            return (double) rr;
        }
        /// <summary>
        /// đổi giữa các đơn vị đo nhiệt độ
        /// </summary>
        public double getTemperature(int i1, int i2, double inp)
        {
            double rr = 1;
            if (i1 == 0)    // °C
            {
                if (i2 == 1) rr = 1.8 * inp + 32;   // °F
                if (i2 == 2) rr = inp + 273;        // °K
            }
            if (i1 == 1)    // °F
            {
                if (i2 == 0) rr = 5F / 9F * (inp - 32);         // °C
                if (i2 == 2) rr = 5F / 9F * (inp - 32) + 273;   // °K
            }
            if (i1 == 2)    // °K
            {
                if (i2 == 0) rr = inp - 273;                // °C
                if (i2 == 1) rr = 1.8 * (inp - 273) + 32;   // °F
            }
            if (i1 == i2) rr = inp;
            return (double) rr;
        }
        /// <summary>
        /// cắt các số 0 thừa ở đầu và cuối xâu
        /// </summary>
        public string splitNumber(string input_str)
        {
            string result = input_str;
            if (result.IndexOf(getDecimalSym()) >= 0)
            {
                while (result[result.Length - 1] == '0' && result.Length > 1)
                {
                    result = result.Substring(0, result.Length - 1);
                }
                if (result[result.Length - 1] == getDecimalSym()[0])
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            return result;
        }
        /// <summary>
        /// input_str = "1e16",  return 10000000000000000,  tạm hiểu thế
        /// input_str = "1e-16", return 0.0000000000000001, tạm hiểu thế
        /// </summary>
        public string ToDouble(string input_str)
        {
            string result = input_str.Substring(0, input_str.IndexOf('E'));
            int base_ = int.Parse(input_str.Substring(input_str.IndexOf('E') + 1));
            int afterDecSym = (result.IndexOf(getDecimalSym()) > 0).GetHashCode(); // 1 = có dấu ',', 0 = không có dấu ','
            afterDecSym *= (result.Length - result.IndexOf(getDecimalSym()) - 1);
            //result = wrap.Remove((n = (wrap.IndexOf(getDecimalSym()) >= 0).GetHashCode()), n);
            if (result.IndexOf(getDecimalSym()) >= 0)
            {
                result = result.Remove(result.IndexOf(getDecimalSym()), 1);
            }
            for (int i = afterDecSym * (base_ >= 0).GetHashCode(); i < Math.Abs(base_); i++)
            {
                if (base_ < 0)
                    result = "0" + result;
                else
                    result += "0";
            }
            if (result.IndexOf("-") > 0)
            {
                result = result.Remove(result.IndexOf("-"), 1);
                result = "-" + result.Insert(1, getDecimalSym());
            }

            //if (base_ < 0) result = result.Insert(1, getDecimalSym());

            return result;
        }	
        /// <summary>
        /// hàm tính luỹ thừa của a^m
        /// </summary>
        /// <param dwordname="a">cơ số của luỹ thừa</param>
        /// <param dwordname="m">số mũ của luỹ thừa</param>
        /// <returns>kết quả của a^m</returns>
        public static double luythua(double a, double m)
        {
            int i = 0;
            double r = 1, s = Math.Abs(m), mu = 1;

            if (m == 0) mu = 1;
            if (Math.Abs(m) == 1) r = a;
            if (Math.Abs(m) > 1)
            {
                s = (double) ((int) m);
                if (m - s == 0)
                    for (i = 0; i < s; i++) r *= a;
                else
                {
                    for (i = 0; i < Math.Abs(m) - 1; i++) r *= a;
                    r = r * Math.Exp(Math.Abs(m - s) * Math.Log(a) / Math.Log(Math.E));
                }
            }
            if (Math.Abs(m) < 1) r = Math.Exp(s * Math.Log(a) / Math.Log(Math.E));
            if (m > 0) mu = r; else mu = 1 / r;
            return mu;
        }
        /// <summary>
        /// input_str = "2^5" (2 mũ 5), return 32, tạm hiểu thế
        /// </summary>
        public double power(string input_str)
        {
            int daumu = input_str.IndexOf('^');
            if (daumu > 0)
            {
                double d1 = double.Parse(input_str.Substring(0, daumu));
                double d2 = double.Parse(input_str.Substring(daumu + 1));
                return luythua(d1, d2);
            }
            else return 1;
        }
        /// <summary>
        /// input_str = "32√5" - căn bậc 5 của 32, return 2, tạm hiểu thế
        /// </summary>
        public double power_inv(string input_str)
        {
            int daumu = input_str.IndexOf('√');
            if (daumu > 0)
            {
                double d1 = double.Parse(input_str.Substring(0, daumu));
                double d2 = double.Parse(input_str.Substring(daumu + 1));
                return luythua(d1, 1 / d2);
            }
            else return 1;
        }
    }
    /// <summary>
    /// lớp các phương thức liên quan đến hệ cơ số
    /// </summary>
    class Binary
    {
        /// <summary>
        /// kiểm tra 1 xâu có phải kiểu bin hay không
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
            Regex reg = new Regex("^[0-9A-Fa-f]+$");

            return reg.IsMatch(str);
        }

        /// <summary>
        /// tính a^n với a, n nguyên
        /// </summary>
        public static ulong powan(int coso, int somu)
        {
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
        public static string dec_to_other(string phannguyen, /*int cs, */int bs)
        {
            // cs nguon, bs dich
            ulong sothuc = ulong.Parse(phannguyen);
            int bit = 0, i = 0, dich = 10;  // i - bien chay, bit - do dai xau ket qua
            ulong[] array = new ulong[64];
            string[] mangchar = new string[64];
            string ketqua = "";
            if (bs == 1) dich = 2;
            if (bs == 2) dich = 8;
            if (bs == 4) return sothuc.ToString("X2");
            if (sothuc == 0) ketqua = "0";
            for (i = 0; i <= 31; i++) array[i] = 16;
            while (sothuc != 0)
            {
                array[bit] = sothuc % (ulong)dich;
                sothuc = sothuc / (ulong)dich;
                bit++;
            }
            for (i = bit - 1; i >= 0; i--)
            {
                if (bs == 4)  // neu ket qua muon dua ra la so hexa
                {
                    mangchar[i] = (array[i]).ToString();
                }
                else         // neu ket qua muon dua ra khong phai la so hexa
                {
                    mangchar[i] = (array[i]).ToString();
                }
                ketqua += mangchar[i];
            }
            return ketqua;
        }
        /// <summary>
        /// đổi 1 số từ hệ khác 10 sang hệ 10
        /// </summary>
        public static string other_to_dec(string phannguyen, int cs/*, int bs*/)
        {
            // cs nguon, bs dich
            int i = 0, dich = 2;
            ulong result = 0;
            ulong[] array = new ulong[64];
            char[] mangchar = new char[64];
            string ketqua;
            if (cs == 2) dich = 8;
            if (cs == 3) dich = 10;
            if (cs == 4) dich = 16;
            // lay tung chu so cua xau input ghi ra array
            for (i = 0; i <= phannguyen.Length - 1; i++)
            {
                mangchar[i] = phannguyen[i];
                if ((mangchar[i] >= 48) && (mangchar[i] <= 57)) array[i] = (ulong)(mangchar[i] - 48);
                if ((mangchar[i] >= 65) && (mangchar[i] <= 70)) array[i] = (ulong)(mangchar[i] - 55);
            }
            for (i = phannguyen.Length - 1; i >= 0; i--)
            {
                result += array[i] * powan(dich, phannguyen.Length - i - 1);
            }
            ketqua = (result).ToString();
            return ketqua;
        }
        /// <summary>
        /// đổi 1 số từ hệ 2 sang các hệ khác
        /// </summary>
        public static string bin_to_other(string binnum, int cs)
        {
            // cs nguon, bs dich
            string binnum64 = binnum, ketqua = "";
            string[] hex = new string[16];
            string[] oct = new string[22];
            for (int i = binnum.Length; i < 64; i++) binnum64 = "0" + binnum64;

            #region sang hệ 8
            if (cs == 2)
            {
                for (int i = 0; i < 21; i++)
                {
                    oct[i] = binnum64.Substring(61 - 3 * i, 3);
                    if (oct[i] == "000") ketqua = "0" + ketqua;
                    if (oct[i] == "001") ketqua = "1" + ketqua;
                    if (oct[i] == "010") ketqua = "2" + ketqua;
                    if (oct[i] == "011") ketqua = "3" + ketqua;
                    if (oct[i] == "100") ketqua = "4" + ketqua;
                    if (oct[i] == "101") ketqua = "5" + ketqua;
                    if (oct[i] == "110") ketqua = "6" + ketqua;
                    if (oct[i] == "111") ketqua = "7" + ketqua;
                }
                oct[21] = binnum64[0].ToString();
                if (oct[21] == "1") ketqua = "1" + ketqua;
            }
            #endregion

            #region sang hệ 16
            if (cs == 4)
            {
                for (int i = 0; i < 16; i++)
                {
                    hex[i] = binnum64.Substring(60 - 4 * i, 4);
                    if (hex[i] == "0000") ketqua = "0" + ketqua;
                    if (hex[i] == "0001") ketqua = "1" + ketqua;
                    if (hex[i] == "0010") ketqua = "2" + ketqua;
                    if (hex[i] == "0011") ketqua = "3" + ketqua;
                    if (hex[i] == "0100") ketqua = "4" + ketqua;
                    if (hex[i] == "0101") ketqua = "5" + ketqua;
                    if (hex[i] == "0110") ketqua = "6" + ketqua;
                    if (hex[i] == "0111") ketqua = "7" + ketqua;
                    if (hex[i] == "1000") ketqua = "8" + ketqua;
                    if (hex[i] == "1001") ketqua = "9" + ketqua;
                    if (hex[i] == "1010") ketqua = "A" + ketqua;
                    if (hex[i] == "1011") ketqua = "B" + ketqua;
                    if (hex[i] == "1100") ketqua = "C" + ketqua;
                    if (hex[i] == "1101") ketqua = "D" + ketqua;
                    if (hex[i] == "1110") ketqua = "E" + ketqua;
                    if (hex[i] == "1111") ketqua = "F" + ketqua;
                }
            }
            #endregion

            return standardString(ketqua);
        }
        /// <summary>
        /// chuẩn hoá xâu - cắt những số 0 thừa ở đầu xâu
        /// </summary>
        private static string standardString(string str)
        {
            string result = str;
            while (result[0] == '0' && result.Length > 1)
                result = result.Substring(1, result.Length - 1);
            return result;
        }
    }
}