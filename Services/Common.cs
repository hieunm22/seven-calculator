using System;
using System.Runtime.InteropServices;

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
                    rates = new double[] { 1e-10, 149597870700, 0.01, 20.1168, 1.8288, 0.3048,
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
                    rates = new double[] { 0.01, 0.3048, 0.2777777777777778, 0.5144444444444444, 340.2933, 1, 0.44704, 299792458 };
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
    }
}