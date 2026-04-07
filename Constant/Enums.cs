namespace Calculator
{
    public enum TrigonometryMode
    {
        DEG = 0,
        RAD = 1,
        GRA = 2
    }

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
