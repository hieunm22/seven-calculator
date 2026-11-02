namespace Calculator
{
    public class Constants
    {
        #region constants, static readonly
        /// <summary>
        /// số lần mở ngoặc tối đa
        /// </summary>
        public const int MAX_BRACKET_LEVEL = 25;
        /// <summary>
        /// list options name
        /// </summary>
        public static readonly string[] optionName = new string[]{

            #region init item members
            "CalculatorType",
            "UseSep",
            "ExtraFunction",
            "ShowHistory",
            "MemoryNumber",
            "ShowPreview",
            // --------------------------
            "TypeUnit",
            "AutoCalculate",
            "DateMethod",
            // --------------------------
            "CollapseSpeed",
            "FastFactorial",
            "SignInteger",
            "ReadDictionary",
            "StoreHistory",
            "FastInput",
            "CountMethod",
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        public static readonly string[][] unitTypeItemMember = new string[][]{

            #region init item members
            new string[]{
            "Degree",
            "Gradian",
            "Radian"},
            new string[]{
            "Acres",
            "Hectares",
            "Square centimeter",
            "Square feet",
            "Square inch",
            "Square kilometer",
            "Square meters",
            "Square mile",
            "Square millimeter",
            "Square yard"},
            new string[]{
            "Bit (b)",
            "Byte (B)",
            "Exabit (Eb)",
            "Exabyte (EB)",
            "Gigabit (Gb)",
            "Gigabyte (GB)",
            "Kilobit (Kb)",
            "Kilobyte (KB)",
            "Megabit (Mb)",
            "Megabyte (MB)",
            "Nibble",
            "Petabit (Pb)",
            "Petabyte (PB)",
            "Terabit (Tb)",
            "Terabyte (TB)",
            "Yottabit (Yb)",
            "Yottabyte (YB)",
            "Zettabit (Zb)",
            "Zettabyte (ZB)",
            },
            new string[]{
            "British Thermal Unit",
            "Calorie",
            "Electron-volts",
            "Foot-pound",
            "Joule",
            "Kilocalorie",
            "Kilojoule"},
            new string[]{
            "Angstrom",
            "Astronomical unit",
            "Centimeters",
            "Chain",
            "Fathom",
            "Feet",
            "Hand",
            "Inch",
            "Kilometers",
            "Light year",
            "Link",
            "Meter",
            "Microns",
            "Mile",
            "Millimeters",
            "Nanometers",
            "Nautical miles",
            "PICA",
            "Rods",
            "Span",
            "Yard"},
            new string[]{
            "BTU/minute",
            "Foot-pound/minute",
            "Horsepower",
            "Kilowatt",
            "Watt"},
            new string[]{
            "Atmosphere",
            "Bar",
            "Kilo Pascal",
            "Millimeter of mercury",
            "Pascal",
            "Pound per square inch (PSI)"},
            new string[]{
            "Degrees Celsius",
            "Degrees Fahrenheit",
            "Kelvin"},
            new string[]{
            "Day",
            "Hour",
            "Microsecond",
            "Milisecond",
            "Minute",
            "Second",
            "Week"},
            new string[]{
            "Centimeter per second",
            "Feet per second",
            "Kilometer per hour",
            "Knots",
            "Mach (at std.atm)",
            "Meter per second",
            "Miles per hour (mph)",
            "Speed of light (Celeritas)"},
            new string[]{
            "Cubic centimeter",
            "Cubic feet",
            "Cubic inch",
            "Cubic meter",
            "Cubic yard",
            "Fluid ounce (UK)",
            "Fluid ounce (US)",
            "Gallon (UK)",
            "Gallon (US)",
            "Liter",
            "Pint (UK)",
            "Pint (US)",
            "Quart (UK)",
            "Quart (US)"},
            new string[]{
            "Carat",
            "Centigram",
            "Decigram",
            "Dekagram",
            "Gram",
            "Hectogram",
            "Kilogram",
            "Long ton",
            "Milligram",
            "Ounce",
            "Pound",
            "Short ton",
            "Stone",
            "Tonne"}
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        public static readonly string[][] worksheetsLabel = new string[][]{

            #region init item members
            new string[]{
            "Purchase price",
            "Term (years)",
            "Interest rate (%)",
            "Monthly payment",
            "Down payment"},
            new string[]{
            "Lease value",
            "Payments per year",
            "Residual value",
            "Interest rate (%)",
            "Periodic payment",
            "Lease period"},
            new string[]{
            "Fuel used (gallons)",
            "Distance (miles)",
            "Fuel economy (mpg)"},
            new string[]{
            "Fuel used (liters)",
            "Distance (kilometers)",
            "Fuel economy (L/100 km)"}
            #endregion

        };
        /// <summary>
        /// combobox item member
        /// </summary>
        public static readonly string[][] worksheetsComboBoxItems = new string[][]{

            #region init item members
            new string[]{
            "Down Payment",
            "Monthly Payment",
            "Purchase price",
            "Term (years)"},
            new string[]{
            "Lease period",
            "Lease value",
            "Periodic payment",
            "Residual value"},
            new string[]{
            "Distance (miles)",
            "Fuel economy (mpg)",
            "Fuel used (gallons)"},
            new string[]{
            "Distance (kilometers)",
            "Fuel economy (L/100 km)",
            "Fuel used (liters)"}
            #endregion

        };
        /// <summary>
        /// độ rộng vùng nội dung standard/statistic không có extra function
        /// </summary>
        public const int APP_W1 = 214;
        /// <summary>
        /// độ rộng vùng nội dung scientific/programmer không có extra function
        /// </summary>
        public const int APP_W2 = 409;
        /// <summary>
        /// độ rộng vùng nội dung standard/statistic có extra function
        /// </summary>
        public const int APP_W3 = APP_W1 + 371;
        /// <summary>
        /// độ rộng vùng nội dung scientific/programmer có extra function
        /// </summary>
        public const int APP_W4 = APP_W2 + 371;

        /// <summary>
        /// chiều cao vùng nội dung standard/scientific không có history
        /// </summary>
        public const int APP_H1 = 265;
        /// <summary>
        /// chiều cao vùng nội dung programmer không có preview
        /// </summary>
        public const int APP_H2 = APP_H1 + 64;
        /// <summary>
        /// chiều cao vùng nội dung standard/scientific có history
        /// </summary>
        public const int APP_H3 = APP_H1 + 104;
        /// <summary>
        /// chiều cao vùng nội dung programmer có preview
        /// </summary>
        public const int APP_H4 = APP_H2 + 81;

        /// <summary>
        /// giá trị nhỏ nhất để tính giai thừa số lớn bằng thuật toán prime swing
        /// </summary>
        public const int MIN_FACTORIAL = 100000;

        #endregion
    }
}
