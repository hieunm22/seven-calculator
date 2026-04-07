namespace Calculator
{
    /// <summary>
    /// object lưu các biểu thức và kết quả của các biểu thức đó
    /// </summary>
    public class HistoryModel
    {
        /// <summary>
        /// biểu thức
        /// </summary>
        public string Expression { get; set; }

        private string result = "0";
        /// <summary>
        /// kết quả
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} = {1}", Expression, Result);
        }
    }
}
