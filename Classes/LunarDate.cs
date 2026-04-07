namespace Calculator
{
    /// <summary>
    /// lớp đối tượng ngày âm lịch
    /// </summary>
    public class LunarDate
    {
        public LunarDate(int dd, int mm, int yy, int leap, int jd)
        {
            Day = dd;
            Month = mm;
            Year = yy;
            Leap = leap;
            JulianDate = jd;
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
            return string.Format("{0} tháng {1} năm {2} ({3})", Day, Month, Year, YearName);
        }
    }
}
