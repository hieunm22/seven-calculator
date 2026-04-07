using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace Calculator
{
    /// <summary>
    /// thao tác với registry
    /// </summary>
    public static class RegistryService
    {
        /// <summary>
        /// Software\SevenCalculator
        /// </summary>
        private const string RegistryPath = "Software\\SevenCalculator";
        /// <summary>
        /// Conversions > Preferences > FactorialDictionary > HistoryExpressions
        /// </summary>
        private static readonly string[] KeyNames = new string[] {
            "Conversions",
            "Preferences",
            "FactorialDictionary",
            "HistoryExpressions",
        };
        /// <summary>
        /// lấy đường dẫn registry key cần truy cập
        /// </summary>
        private static string GetRegistryPath(int index)
        {
            return string.Format("{0}\\{1}", RegistryPath, KeyNames[index]);
        }
        /// <summary>
        /// mở đường dẫn registry key cần truy cập
        /// </summary>
        private static RegistryKey OpenSubKey(int index)
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(index), true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(index));
            }
            return reg;
        }
        /// <summary>
        /// đọc thông tin từ regedit
        /// </summary>
        /// <param name="optionName">list option name</param>
        /// <returns>kết quả đọc được từ regedit</returns>
        public static object[] ReadFromRegedit(string[] optionName)
        {
            object[] result = new object[optionName.Length];
            RegistryKey reg = Registry.CurrentUser;
            reg = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(RegistryPath);
                reg.SetValue(optionName[Config._00_CalculatorType], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._01_UseSep], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._02_ExtraFunction], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._03_ShowHistory], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._05_ShowPreview], 0, RegistryValueKind.DWord);
                result[Config._00_CalculatorType] = 0;
                result[Config._01_UseSep] = 0;
                result[Config._02_ExtraFunction] = 0;
                result[Config._03_ShowHistory] = 0;
                result[Config._05_ShowPreview] = 0;
                // ---------------------------
                result[Config._04_MemoryNumber] = "0";
                reg.SetValue(optionName[Config._04_MemoryNumber], result[Config._04_MemoryNumber], RegistryValueKind.String);
            }
            else
            {
                result[Config._00_CalculatorType] = GetValueFromValueKey(optionName[Config._00_CalculatorType], reg, 0, 3, 0);
                result[Config._01_UseSep] = GetValueFromValueKey(optionName[Config._01_UseSep], reg, 0, 1, 0);
                result[Config._02_ExtraFunction] = GetValueFromValueKey(optionName[Config._02_ExtraFunction], reg, 0, 7, 0);
                result[Config._03_ShowHistory] = GetValueFromValueKey(optionName[Config._03_ShowHistory], reg, 0, 1, 0);
                result[Config._04_MemoryNumber] = GetValueFromValueKey(optionName[Config._04_MemoryNumber], reg);
                result[Config._05_ShowPreview] = GetValueFromValueKey(optionName[Config._05_ShowPreview], reg, 0, 1, 0);
            }
            string[] convValues = optionName.SubArray(0, 6);
            DeleteNonUsedValue(reg, convValues);
            DeleteNonUsedKey(reg, KeyNames);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(0), true);
            if (reg == null)    // create and assign default value
            {
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(0));
                reg.SetValue(optionName[Config._06_TypeUnit], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._07_AutoCalculate], 0, RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._08_DateMethod], 0, RegistryValueKind.DWord);
                result[Config._06_TypeUnit] = 0;
                result[Config._07_AutoCalculate] = 0;
                result[Config._08_DateMethod] = 0;
            }
            else
            {
                result[Config._06_TypeUnit] = GetValueFromValueKey(optionName[Config._06_TypeUnit], reg, 0, 11, 0);
                result[Config._07_AutoCalculate] = GetValueFromValueKey(optionName[Config._07_AutoCalculate], reg, 0, 1, 0);
                result[Config._08_DateMethod] = GetValueFromValueKey(optionName[Config._08_DateMethod], reg, 0, 2, 0);
            }

            convValues = optionName.SubArray(6, 3);
            DeleteNonUsedValue(reg, convValues);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(1), true);
            if (reg == null)    // create and assign default value
            {
                result[Config._09_CollapseSpeed] = 10;
                result[Config._10_SignInteger] = 1;
                result[Config._11_ReadDictionary] = 1;
                result[Config._12_StoreHistory] = 0;
                result[Config._13_FastInput] = 0;
                result[Config._14_CountMethod] = 1;
                reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(1));
                reg.SetValue(optionName[Config._09_CollapseSpeed], (int)result[Config._09_CollapseSpeed], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._10_SignInteger], (int)result[Config._10_SignInteger], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._11_ReadDictionary], (int)result[Config._11_ReadDictionary], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._12_StoreHistory], (int)result[Config._12_StoreHistory], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._13_FastInput], (int)result[Config._13_FastInput], RegistryValueKind.DWord);
                reg.SetValue(optionName[Config._14_CountMethod], (int)result[Config._14_CountMethod], RegistryValueKind.DWord);
            }
            else
            {
                result[Config._09_CollapseSpeed] = GetValueFromValueKey(optionName[Config._09_CollapseSpeed], reg, 4, 12, 10);
                result[Config._10_SignInteger] = GetValueFromValueKey(optionName[Config._10_SignInteger], reg, 0, 1, 1);
                result[Config._11_ReadDictionary] = GetValueFromValueKey(optionName[Config._11_ReadDictionary], reg, 0, 1, 1);
                result[Config._12_StoreHistory] = GetValueFromValueKey(optionName[Config._12_StoreHistory], reg, 0, 1, 0);
                result[Config._13_FastInput] = GetValueFromValueKey(optionName[Config._13_FastInput], reg, 0, 1, 0);
                result[Config._14_CountMethod] = GetValueFromValueKey(optionName[Config._14_CountMethod], reg, 0, 1, 1);
            }
            convValues = optionName.SubArray(9);
            DeleteNonUsedValue(reg, convValues);

            reg.Close();

            return result;
        }
        /// <summary>
        /// đọc thông tin từ dword value
        /// </summary>
        /// <param name="optionName">tên dword</param>
        /// <param name="reg">đường dẫn tới key chứa dword</param>
        /// <param name="minValue">giá trị nhỏ nhất cho phép của dword</param>
        /// <param name="maxValue">giá trị lớn nhất cho phép của dword</param>
        /// <param name="defaultValue">giá trị mặc định sẽ được gán nếu giá trị đọc được nằm ngoài khoảng min-max trên</param>
        /// <returns>giá trị đọc được từ dword value</returns>
        static object GetValueFromValueKey(string optionName, RegistryKey reg, int minValue, int maxValue, int defaultValue)
        {
            int rs = (int)reg.GetValue(optionName, -1);
            if (rs > maxValue || rs < minValue)
            {
                rs = defaultValue;
                reg.SetValue(optionName, defaultValue, RegistryValueKind.DWord);
            }
            return rs;
        }
        /// <summary>
        /// đọc thông tin từ string value
        /// </summary>
        /// <param name="optionName">tên dword</param>
        /// <param name="reg">đường dẫn tới key chứa dword</param>
        /// <returns>giá trị đọc được từ dword value</returns>
        static object GetValueFromValueKey(string optionName, RegistryKey reg)
        {
            string rs = (string)reg.GetValue(optionName, "A non-number cannot be assigned here");
            if (!BigNumber.IsNumber(rs))
            {
                reg.SetValue(optionName, "0", RegistryValueKind.String);
                rs = "0";
            }
            return rs;
        }
        /// <summary>
        /// lưu vào registry trước khi thoát
        /// </summary>
        /// <param name="optName">list tên dword</param>
        /// <param name="config">list giá trị được ghi</param>
        public static void SaveToRegistryBeforeExit(string[] optName, object[] config)
        {
            var reg = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
            if (reg == null) return;
            DeleteNonUsedKey(reg, KeyNames);
            string[] convValues = optName.SubArray(0, 6);
            DeleteNonUsedValue(reg, convValues);

            reg.SetValue(optName[Config._00_CalculatorType], config[Config._00_CalculatorType], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._01_UseSep], config[Config._01_UseSep], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._02_ExtraFunction], config[Config._02_ExtraFunction], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._03_ShowHistory], config[Config._03_ShowHistory], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._04_MemoryNumber], config[Config._04_MemoryNumber], RegistryValueKind.String);
            reg.SetValue(optName[Config._05_ShowPreview], config[Config._05_ShowPreview], RegistryValueKind.DWord);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(0), true);
            if (reg == null) return;
            reg.SetValue(optName[Config._06_TypeUnit], config[Config._06_TypeUnit], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._07_AutoCalculate], config[Config._07_AutoCalculate], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._08_DateMethod], config[Config._08_DateMethod], RegistryValueKind.DWord);

            convValues = optName.SubArray(6, 3);
            DeleteNonUsedValue(reg, convValues);

            reg = Registry.CurrentUser.OpenSubKey(GetRegistryPath(1), true);
            if (reg == null) return;
            reg.SetValue(optName[Config._09_CollapseSpeed], config[Config._09_CollapseSpeed], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._10_SignInteger], config[Config._10_SignInteger], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._11_ReadDictionary], config[Config._11_ReadDictionary], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._12_StoreHistory], config[Config._12_StoreHistory], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._13_FastInput], config[Config._13_FastInput], RegistryValueKind.DWord);
            reg.SetValue(optName[Config._14_CountMethod], config[Config._14_CountMethod], RegistryValueKind.DWord);

            convValues = optName.SubArray(9);
            DeleteNonUsedValue(reg, convValues);
        }
        /// <summary>
        /// get subarray from an existing array
        /// </summary>
        /// <typeparam name="T">type of array</typeparam>
        /// <param name="data">source array</param>
        /// <param name="index">index from source array</param>
        /// <param name="length">length of new array, -1 if user leave blank when calling this function</param>
        /// <returns>destination array</returns>
        private static T[] SubArray<T>(this T[] data, int index, int length = -1)
        {
            if (length == -1)
            {
                length = data.Length - index;
            }
            return data.Skip(index).Take(length).ToArray();
        }
        /// <summary>
        /// xoá các value thừa trong key hiện tại của reg
        /// </summary>
        /// <param name="reg">registry key hiện tại</param>
        /// <param name="optName">list tên option</param>
        private static void DeleteNonUsedValue(RegistryKey reg, string[] optName)
        {
            string[] subValues = reg.GetValueNames();
            var listToDelete = subValues.Where(w => !optName.Contains(w));
            foreach (string key in listToDelete)
            {
                reg.DeleteValue(key);
            }
        }
        /// <summary>
        /// xoá các key thừa trong key hiện tại của reg
        /// </summary>
        /// <param name="reg">registry key hiện tại</param>
        /// <param name="optName">list tên option</param>
        private static void DeleteNonUsedKey(RegistryKey reg, string[] optName)
        {
            string[] subValues = reg.GetSubKeyNames();
            var listToDelete = subValues.Where(w => !optName.Contains(w));
            foreach (string key in listToDelete)
            {
                reg.DeleteSubKey(key);
            }
        }
        /// <summary>
        /// lấy ra property Value của object FactorialObject và đưa vào 1 mảng
        /// </summary>
        /// <param name="dict">list FactorialObject cần lấy</param>
        public static string[] SelectValue(this List<FactorialModel> dict)
        {
            return dict.Select(s => s.Value).ToArray();
        }
        /// <summary>
        /// nạp giá trị giai thừa số lớn đã tính được trước đó vào biến
        /// </summary>
        public static void ReadDictionary(ref List<FactorialModel> dict)
        {
            RegistryKey reg = OpenSubKey(2);

            string[] namelist = reg.GetValueNames();
            string[] selected = dict.SelectValue();
            // list những giá trị chưa có trong dictionary memory
            var existedInNameList = namelist.Where(w => !selected.Contains(w));
            // chỉ thêm những giá trị chưa có vào dictionary memory
            // còn có rồi thì ưu tiên những giá trị đã được tính lại
            foreach (string item in existedInNameList)
            {
                try
                {
                    dict.Add(new FactorialModel()
                    {
                        Value = item,
                        Result = reg.GetValue(item) as string,
                        IsRecalculate = false,    // hơi thừa
                    });
                }
                catch { }
            }
            reg.Close();
        }
        /// <summary>
        /// lưu từ điển giai thừa vào registry trước khi thoát
        /// </summary>
        public static void SaveDictionary(List<FactorialModel> factDict)
        {
            RegistryKey reg = OpenSubKey(2);
            foreach (FactorialModel fo in factDict)
            {
                reg.SetValue(fo.Value, fo.Result);
            }
            reg.Close();
        }

        /// <summary>
        /// load history từ registry khi onload
        /// </summary>
        /// <returns></returns>
        public static HistoryModel[] ReadHistory()
        {
            RegistryKey reg = OpenSubKey(3);
            var names = reg.GetValueNames();
            HistoryModel[] ho = new HistoryModel[100];
            string[] keyValues = new string[names.Length];
            //MathParserService p = new MathParserService();
            for (int i = 0; i < names.Length; i++)
            {
                ho[i] = new HistoryModel();
                ho[i].Expression = Convert.ToString(reg.GetValue(names[i]));
                ho[i].Result = names[i].Substring(3);
                //p.EvaluateSci(ho[i].Expression);
                //ho[i].Result = p.ToString();
            }
            reg.Close();
            return ho;
        }
        /// <summary>
        /// lưu lịch sử các phép tính khoa học
        /// </summary>
        public static void SaveHistory(HistoryModel[] ho)
        {
            #region xoá key đi rồi tạo lại
            //try
            //{
            //    Registry.CurrentUser.DeleteSubKey(GetRegistryPath(3));
            //}
            //catch { /* không tìm thấy để xoá thì thôi */ }

            //RegistryKey reg = Registry.CurrentUser.CreateSubKey(GetRegistryPath(3)); 
            #endregion

            #region không xoá key, chỉ xoá hết value
            RegistryKey reg = OpenSubKey(3);
            string[] namelist = reg.GetValueNames();
            foreach (string name in namelist)
            {
                reg.DeleteValue(name);
            } 
            #endregion

            for (int i = 0; i < ho.Length; i++)
            {
                if (ho[i] == null) break;
                reg.SetValue(string.Format("{0:00}_{1}", i, ho[i].Result), ho[i].Expression);
            }
            reg.Close();
        }
        /// <summary>
        /// xoá lịch sử các phép tính khoa học
        /// </summary>
        public static void ClearHistory()
        {
            RegistryKey reg = OpenSubKey(3);
            string[] values = reg.GetValueNames();
            foreach (string value in values)
            {
                reg.DeleteValue(value);
            }
            reg.Close();
        }
    }
}