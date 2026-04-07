namespace Calculator
{
    /// <summary>
    /// các loại text gợi ý trên textbox control
    /// </summary>
    public enum SuggestType
    {
        /// <summary>
        /// không dùng
        /// </summary>
        None,
        /// <summary>
        /// bị mất khi focus
        /// </summary>
        WatermarkText,
        /// <summary>
        /// bị mất khi nhập text
        /// </summary>
        PlaceHolder,
    }
}
