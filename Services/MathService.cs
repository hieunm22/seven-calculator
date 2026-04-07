using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    /// <summary>
    /// tổng hợp các method
    /// </summary>
    public static class MathService
    {
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
                //expression = expression.Substring(0, m.Id) + m.Value.Substring(1, m.Value.Length - 2) + expression.Substring(m.Id + m.Value.Length);
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
        /// tìm hạng của mảng 1 chiều biến kiểu byte (index của phần tử khác 0 cuối cùng trong mảng)
        /// </summary>
        /// <param name="arr">mảng 1 chiều</param>
        /// <returns>hạng của mảng</returns>
        public static int GetArrayRank(this byte[] arr)
        {
            int index = Array.FindIndex(arr.Reverse().ToArray(), i => i != 0);
            return arr.Count() - index - 1;
        }
        /// <summary>
        /// kiểm tra 1 chuỗi có phải bắt đầu của 1 đối tượng <see cref="StringBuilder"/>
        /// </summary>
        /// <param name="sb">chuỗi cần kiểm tra</param>
        /// <param name="test">substring cần kiểm tra</param>
        /// <returns>true nếu test là substring của sb, còn lại là false</returns>
        public static bool StartsWith(this StringBuilder sb, string test)
        {
            if (sb.Length < test.Length)
                return false;

            string start = sb.ToString(0, test.Length);
            return start.Equals(test);
        }
    }
}