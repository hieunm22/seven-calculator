using System;

namespace Calculator
{
    class Number
    {
        /// <summary>
        /// phép cộng 2 số nguyên
        /// </summary>
        private static string Add(string so1, string so2)
        {
            string result = "";
            int max = so1.Length;
            bool nho = false;
            if (so2.Length > so1.Length) max = so2.Length;
            while (so1.Length < max) so1 = "0" + so1;
            while (so2.Length < max) so2 = "0" + so2;
            int[] i3 = new int[64];
            for (int i = max - 1; i >= 0; i--)
            {
                i3[i] = so1[i] + so2[i] - 96 + nho.GetHashCode();
                nho = (i3[i] >= 10);
                if (nho && i > 0) i3[i] -= 10;
                result = i3[i] + result;
            }
            return result;
        }
        /// <summary>
        /// phép cộng 2 số thực
        /// </summary>
        public static string Add(string so1, string so2, string decimalsym)
        {
            string result = "";
            bool containsdot = so1.IndexOf(decimalsym) > 0 || so2.IndexOf(decimalsym) > 0;
            if (containsdot)
            {
                string nguyen1 = "", nguyen2 = "", le1 = "", le2 = "";
                if (so1.IndexOf(decimalsym) > 0)
                {
                    nguyen1 = so1.Substring(0, so1.IndexOf(decimalsym));
                    le1 = so1.Substring(so1.IndexOf(decimalsym) + 1);
                }
                else { nguyen1 = so1; le1 = "0"; }
                if (so2.IndexOf(decimalsym) > 0)
                {
                    nguyen2 = so2.Substring(0, so2.IndexOf(decimalsym));
                    le2 = so2.Substring(so2.IndexOf(decimalsym) + 1);
                }
                else { nguyen2 = so2; le2 = "0"; }
                while (le1.Length > le2.Length) le2 += "0";
                while (le1.Length < le2.Length) le1 += "0";
                if (Add(le1, le2).Length != le1.Length) // phần lẻ cộng có nhớ
                    result = Add(Add(nguyen1, nguyen2), "1") + decimalsym + Add(le1, le2).Substring(1);
                else    // phần lẻ cộng không nhớ
                    result = Add(nguyen1, nguyen2) + decimalsym + Add(le1, le2);
            }
            else
            {
                result = Add(so1, so2);
            }
            return result;
        }

        /*-----------------------------------------------------------*/

        /// <summary>
        /// phép trừ 2 số nguyên
        /// </summary>
        private static string Minus(string so1, string so2)
        {
            string result = "";
            double d1 = double.Parse(so1), d2 = double.Parse(so2);
            if (d1 > d2)
            {
                int max = so1.Length;
                bool nho = false;
                if (so2.Length > so1.Length) max = so2.Length;
                while (so1.Length < max) so1 = "0" + so1;
                while (so2.Length < max) so2 = "0" + so2;
                int[] i3 = new int[32];
                for (int i = max - 1; i >= 0; i--)
                {
                    i3[i] = so1[i] - so2[i] - nho.GetHashCode();
                    nho = (i3[i] < 0);
                    if (nho && i != 0) i3[i] += 10;
                    result = i3[i] + result;
                }
            }
            else
            {
                result = "-" + Minus(so2, so1);
            }
            return result;
        }
        /// <summary>
        /// phép trừ 2 số thực
        /// </summary>
        public static string Minus(string so1, string so2, string @decimal)
        {
            string result = "";
            bool containsdot = so1.IndexOf(@decimal) > 0 || so2.IndexOf(@decimal) > 0;
            if (containsdot)
            {
                string nguyen1 = "", nguyen2 = "", le1 = "", le2 = "";
                if (so1.IndexOf(@decimal) > 0)
                {
                    nguyen1 = so1.Substring(0, so1.IndexOf(@decimal));
                    le1 = so1.Substring(so1.IndexOf(@decimal) + 1);
                }
                else { nguyen1 = so1; le1 = "0"; }
                if (so2.IndexOf(@decimal) > 0)
                {
                    nguyen2 = so2.Substring(0, so2.IndexOf(@decimal));
                    le2 = so2.Substring(so2.IndexOf(@decimal) + 1);
                }
                else { nguyen2 = so2; le2 = "0"; }
                while (le1.Length > le2.Length) le2 += "0";
                while (le1.Length < le2.Length) le1 += "0";
                if (Minus(le1, le2).Length != le1.Length) // phép trừ phần lẻ có nhớ
                {
                    string str = "1";
                    while (str.Length <= le1.Length) str += "0";
                    result = Minus(Minus(nguyen1, nguyen2), "1") + @decimal + 
                        Minus(str, Minus(le1, le2).Substring(1)).Substring(1);
                    result = result.Substring( (result[0] == '0' || 
                        (result[0] == '-' && result[1] == '0') ).GetHashCode());
                }
                else    // phép trừ phần lẻ không nhớ
                    result = Minus(nguyen1, nguyen2) + @decimal + Minus(le1, le2);
            }
            else
            {
                result = Minus(so1, so2);
            }
            return result;
        }

        /*-----------------------------------------------------------*/

        /// <summary>
        /// phép nhân 2 số nguyên < 10
        /// </summary>
        public static string Multiply(char c1, char c2)
        {
            return ((c1 - 48) * (c2 - 48)).ToString();
        }
        /// <summary>
        /// phép nhân 1 số nguyên > 10 với 1 số nguyên < 10
        /// </summary>
        public static string Multiply(string so1, char c)
        {
            string result = "0";
            int i = so1.Length - 1;
            while (i >= 0)
            {
                int w = i;
                string temp = Multiply(c, so1[i]);
                while (w < so1.Length - 1)
                {
                    temp += '0';
                    w++;
                }
                result = Number.Add(result, temp);
                i--;
            }
            return result;
        }
        /// <summary>
        /// nhân 2 số nguyên
        /// </summary>
        public static string Multiply(string so1, string so2)
        {
            string result = "0";
            int i;

            so1 = justifyZero(so1);
            so2 = justifyZero(so2);

            int bLength = so2.Length;
            i = bLength - 1;

            while (i >= 0)
            {

                int w = i;
                //lay tung ki tu trong so2 nhan voi so1
                string temp = Multiply(so1, so2[i]);

                while (w < bLength - 1)
                {
                    temp += '0';
                    w++;
                }
                result = Add(result, temp);
                i--;
            }
            return result;
        }
        /// <summary>
        /// phép nhân 1 số thực với 1 số nguyên < 10
        /// </summary>
        public static string Multiply(string so1, char c, string @decimal)
        {
            string result = "";
            int dot = so1.IndexOf(@decimal);
            string nguyen = so1.Substring(0, dot), le = so1.Substring(dot + 1), nho = "0";
            string ketqua = Multiply(le, c);
            if (ketqua.Length > le.Length)        
            {

                result = ketqua.Substring(1);
                nho = ketqua[0].ToString();
            }
            result = Add(Multiply(nguyen, c), nho) + @decimal + ketqua.Substring(ketqua.Length - le.Length);
            while (result[result.Length - 1] == '0')
            {
                result = result.Substring(0, result.Length - 1);
            }
            if (result[result.Length - 1] == @decimal[0]) result = result.Substring(0, result.Length - 1);
            return result;
        }
        /// <summary>
        /// phép nhân 1 số thực với 1 số thực
        /// </summary>
        public static string Multiply(string so1, string so2, string @decimal)
        {
            string result = "0";
            if (so2.IndexOf(@decimal) < 0) // so2 là số nguyên
            {
                for (int i = so2.Length - 1; i >= 0; i--)
                {
                    string temp = Multiply(so1, so2[i], @decimal);
                    int dot = temp.IndexOf(@decimal);
                    int vitrichen = dot + so2.Length - 1 - i;
                    if (vitrichen != dot && vitrichen > 0)
                    {
                        temp = temp.Remove(dot, 1);
                        if (vitrichen < temp.Length)
                            temp = temp.Insert(vitrichen, @decimal);
                        if (dot + so2.Length - 1 - i > temp.Length)
                        {
                            while (temp.Length < vitrichen) temp += "0";
                            temp = temp.Insert(dot + so2.Length - 1 - i, @decimal);
                        }
                    }
                    result = Add(result, temp, @decimal);
                } 
            }
            else    // so2 là số hữu tỉ
            {
                result = Multiply(so1, so2.Remove(so2.IndexOf(@decimal), 1), @decimal);
                result = moveDotToLeft(result, so2.IndexOf(@decimal), @decimal);
                // dịch dấu phẩy sang trái
            }
            return result;
        }
        /// <summary>
        /// dịch dấu phẩy sang trái
        /// </summary>
        /// <param name="input">xâu cần dịch dấu phẩy</param>
        /// <param name="shift">số chữ số phải dịch</param>
        private static string moveDotToLeft(string input, int shift, string @decimal)
        {
            string result = input;
            int dot = result.IndexOf(@decimal);
            if (dot < 0) dot = result.Length;
            if (shift < dot)
            {
                if (dot != result.Length) result = result.Remove(dot, 1);
                result = result.Insert(dot - shift, @decimal);
            }
            else
            {

            }
            return result;
        }
        
        /*-----------------------------------------------------------*/
        /// <summary>
        /// chia 2 số nguyên - KHÓ ĐÉO CHỊU ĐƯỢC, NGHỈ
        /// </summary>
        public static string Division(string so1, string so2)
        {
            so1 = Number.justifyZero(so1);
            so2 = Number.justifyZero(so2);

            if (Number.Compare(so1, so2) == -1)
            {
                return "0";
            }
            if (Number.Compare(so1, so2) == 0)
            {
                return so1;
            }
            else
            {
                #region copied code
                //for (int i = 0; i < so2.Length - 1; i++)
                //{
                //    temp = temp + so1[i];
                //}

                //for (int i = so2.Length - 1; i < so1.Length; i++)
                //{
                //    int count = 0;
                //    temp = temp + so1[i];
                //    // thực hiện temp = temp - so2 cho đến khi temp < so2
                //    while (Number.Compare(temp, so2) >= 0)  // temp >= so2
                //    {
                //        count += 1;
                //        temp = Number.Minus(temp, so2);     // temp = temp - so2
                //    }

                //    quotient += count.ToString();
                //} 
                #endregion


            }

            return justifyZero("1");
        }

        /*-----------------------------------------------------------*/

        /// <summary>
        /// loại bỏ những chữ số 0 vô nghĩa ở đầu
        /// </summary>
        public static string justifyZero(string str)
        {
            while ((str.Length > 1) && (str[0] == '0'))
            {
                str = str.Substring(1, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// so sánh 2 số nguyên
        /// </summary>
        public static int Compare(string so1, string so2)
        {
            so1 = Number.justifyZero(so1);
            so2 = Number.justifyZero(so2);

            if (so1.Length > so2.Length) return 1;
            if (so1.Length < so2.Length) return -1;
            else
            {
                for (int i = 0; i < so1.Length; i++)
                {
                    if (so1[i] > so2[i]) return 1;
                    if (so1[i] < so2[i]) return -1;
                }
                return 0;
            }
        }
        /// <summary>
        /// tính giai thừa
        /// </summary>
        public static string Factorial(string num, string @decimal)
        {
            justifyZero(num);
            string ketqua = "1";
            if (num == "0")
            {
                return ketqua;
            }
            else
            {
                string temp = "1";
                int coso = 0;
                while (Compare(num, temp) == 1)
                {
                    temp = Add(temp, "1");
                    ketqua = Multiply(ketqua, temp);
                    if (ketqua.Length > 32)
                    {
                        coso += (ketqua.Length - 32);
                        ketqua = rutgonso(ketqua);
                    }
                }
                for (int i = 0; i < coso; i++) ketqua += "0";
            }
            return rutgonso(ketqua, @decimal);
        }
        /// <summary>
        /// lấy 32 chữ số đầu tiên của số nguyên lớn 
        /// và trả về kiểu string có dạng dấu chấm động
        /// </summary>
        private static string rutgonso(string num, string @decimal)
        {
            string ketqua = num;
            if (ketqua.Length > 32)
            {
                if (ketqua.IndexOf(@decimal) < 0)
                {
                    int thua = ketqua.Length - 1;
                    ketqua = ketqua.Substring(0, 27/* - thua.ToString().Length*/);
                    //strResult = Add(strResult, (num[32] >= 53).GetHashCode().ToString());
                    ketqua = ketqua.Insert(1, @decimal) + "E+" + thua;
                    while (ketqua.IndexOf("0E") > 0) ketqua = ketqua.Replace("0E", "E");
                }
                else
                {

                }
            }
            return ketqua;
        }
        /// <summary>
        /// lấy 32 chữ số đầu tiên của số nguyên lớn 
        /// </summary>
        private static string rutgonso(string num)
        {
            string ketqua = num;
            if (ketqua.Length > 32)
            {
                ketqua = ketqua.Substring(0, 32);
            }
            return ketqua;
        }
    }
}
