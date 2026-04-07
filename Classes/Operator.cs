namespace Calculator
{
    public class Operator
    {
        /// <summary>
        /// hàm khởi tạo mặc định
        /// </summary>
        public Operator()
        {
            PText = "";
            Index = -1;
        }
        /// <summary>
        /// hàm khởi tạo với tham số là text của phép tính
        /// </summary>
        /// <param name="text">text của phép tính</param>
        /// <param name="mode">mức ưu tiên của toán tử "^". 6 => scientific, 0 => programmer</param>
        public Operator(string text, int mode)
        {
            PText = text;
            switch (text)
            {
                case "^":   // xor = 1, ^ = 6
                    Index = mode;
                    break;
                case "yroot": 
                    Index = 6;
                    break;
                case "*": case "/": case "mod": 
                    Index = 5;
                    break;
                case "+": case "-": 
                    Index = 4;
                    break;
                case "<<": case ">>": 
                    Index = 3;
                    break;
                case "&":
                    Index = 2;
                    break;
                case "|":   // or = 0
                    Index = 0;
                    break;
                default:    // null or empty
                    Index = -1;
                    break;
            }
        }
        /// <summary>
        /// mức độ ưu tiên của phép tính
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// text của phép tính
        /// </summary>
        public string PText { get; set; }
        /// <summary>
        /// tạo bản sao cho đối tượng hiện tại
        /// </summary>
        public Operator Clone()
        {
            Operator clone = new Operator();
            clone.Index = this.Index;
            clone.PText = this.PText;
            return clone;
            //return this;
        }

        public override string ToString()
        {
            return string.Format("Index = {0}, PText = \"{1}\"", Index, PText);
        }
    }
}
