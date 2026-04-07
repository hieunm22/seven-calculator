namespace Calculator
{
    /// <summary>
    /// object lưu giá trị và kết quả các giai thừa số lớn
    /// </summary>
    public class FactorialObject
    {
        /// <summary>
        /// số cần tính giai thừa
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// kết quả
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// biến kiểm tra số đó có được phải được tính lại hay không
        /// </summary>
        public bool IsRecalculate { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", IsRecalculate ? 1 : 0, Value, Result);
        }
    }
}
