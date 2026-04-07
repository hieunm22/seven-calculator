using System;

namespace Calculator
{
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
            decimal realNumber = decimal.Parse(decimalNumber);
            decimal addNumber = 1;
            for (int i = 0; i < size; i++)
            {
                addNumber *= 2; // addNumber = 2 ^ size
            }
            if (realNumber < 0) realNumber += addNumber;

            // đoạn này dùng built-in method cho tiện
            if (dest == 2 || dest == 8)  // 10 -> 2 || 10 -> 8
            {
                switch (size)
                {
                    case 08:
                        if (realNumber <= sbyte.MaxValue && isSign)
                            return Convert.ToString((sbyte)realNumber, dest);
                        else
                            return Convert.ToString((byte)realNumber, dest);
                    case 16:
                        if (realNumber <= short.MaxValue && isSign)
                            return Convert.ToString((short)realNumber, dest);
                        else
                            return Convert.ToString((ushort)realNumber, dest);
                    case 32:
                        if (realNumber <= int.MaxValue && isSign)
                            return Convert.ToString((int)realNumber, dest);
                        else
                            return Convert.ToString((int)realNumber, dest);
                    // không có case 64 trong này vì phần này dùng chung cho cả dest = 16 -> phần này viết ở ngoài
                }
            }

            byte remainder = 0;
            var rs = new System.Text.StringBuilder();
            while (realNumber > 0)
            {
                remainder = (byte)(realNumber % dest);
                realNumber = Math.Floor(realNumber / dest);
                rs = rs.Insert(0, remainder.ToString("X")); // ToString("X") để convert 1 -> 1, 2 -> 2, ..., 10 -> a, 11 -> b,...
            }
            if (dest == 02 && rs.Length > 64) throw new Exception("Overflow");
            if (dest == 08 && rs.Length > 22) throw new Exception("Overflow");
            if (dest == 16 && rs.Length > 16) throw new Exception("Overflow");
            return rs.ToString();
        }
        /// <summary>
        /// đổi 1 số từ hệ khác 10 sang hệ 10
        /// </summary>
        public static string other_to_dec(string decimalNumber, int from/*, int dest*/, int Size, bool isSign)
        {
            // from nguon, dest dich
            decimal lResult = 0m;
            decimal basement = 1m;
            char[] memberChar = decimalNumber.ToCharArray();

            for (int i = memberChar.Length - 1; i >= 0; i--)
            {
                int hexValue = (memberChar[i] > '9') ? (memberChar[i] & ~0x20) - 'A' + 10 : (memberChar[i] - '0');
                lResult += hexValue * basement;
                basement *= from;
            }
            if (memberChar.Length == Size && memberChar[0] == '1')
            {
                // khi memberChar.Length == Size thì basement đã có giá trị = 2 ^ Size rồi, không cần phải Math.Pow nữa
                if (lResult > 0 && isSign) lResult -= basement;   // Convert.ToDecimal(Math.Pow(2d, Size));
                if (lResult < 0 && !isSign) lResult += basement;  // Convert.ToDecimal(Math.Pow(2d, Size));
            }
            return lResult.ToString();
        }
    }
}
