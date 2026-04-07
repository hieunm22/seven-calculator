using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Preferences : ExtraForm
    {
        int inputMethod;
        decimal transfer;
        bool mod = false, f3, sign, readDict, readDictChanged, storeHistory;

        public Preferences(params object[] paras)
        {
            InitializeComponent();
            LoadSettings(paras);
            this.label1.MouseDown += MoveForm;
            this.label2.MouseDown += MoveForm;
        }
        /// <summary>
        /// nạp cấu hình từ form chính
        /// </summary>
        /// <param name="paras">Speed, Animate, FastFact, IsSign, ReadDict, StoreHistory, FastInput, CountMethod</param>
        private void LoadSettings(params object[] paras)
        {
            nudCollapsedSpd.Value = transfer = (int)paras[0];
            ckbFastFact.Checked = f3 = (bool)paras[1];
            ckbUsedSign.Checked = sign = (bool)paras[2];
            ckbReadDict.Checked = readDict = (bool)paras[3];
            ckbStoreHistory.Checked = storeHistory = (bool)paras[4];
            cbbInputMethod.SelectedIndex = inputMethod = (int)paras[5];
        }
        /// <summary>
        /// thay đổi cấu hình trên form
        /// </summary>
        public delegate void PreferencesChanged(bool readdict, params object[] paras);
        /// <summary>
        /// sự kiện thay đổi cấu hình trên form
        /// </summary>
        public event PreferencesChanged DoCheck;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (mod)
            {
                if (DoCheck != null) DoCheck(
                    // nếu checkbox readdict bị thay đổi value so với ban đầu load form
                    readDictChanged,
                    // các giá trị checkbox, combobox,... trên form cấu hình
                    (int)nudCollapsedSpd.Value, 
                    //--------------------------------
                    ckbFastFact.Checked, 
                    ckbUsedSign.Checked, 
                    ckbReadDict.Checked,
                    ckbStoreHistory.Checked,
                    //--------------------------------
                    cbbInputMethod.SelectedIndex
                    );
            }
            this.Close();
        }
        /// <summary>
        /// nút cancel
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// numericupdown speed
        /// </summary>
        private void nudCollapsedSpd_ValueChanged(object sender, EventArgs e)
        {
            if (mod && nudCollapsedSpd.Value == transfer) { mod = false; return; }
            mod |= nudCollapsedSpd.Value != transfer;
        }
        /// <summary>
        /// check chọn fast factorial
        /// </summary>
        private void ckbFastFact_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && ckbFastFact.Checked == f3) { mod = false; return; }
            mod |= f3 != ckbFastFact.Checked;
        }
        /// <summary>
        /// check chọn số nguyên có dấu
        /// </summary>
        private void ckbUsedSign_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && ckbUsedSign.Checked == sign) { mod = false; return; }
            mod |= sign != ckbUsedSign.Checked;
        }
        /// <summary>
        /// check chọn đọc kết quả từ dictionary
        /// </summary>
        private void readDictChkB_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && ckbReadDict.Checked == readDict) { mod = false; return; }
            readDictChanged = readDict != ckbReadDict.Checked;
            mod |= readDictChanged;
        }
        /// <summary>
        /// check chọn lưu kết quả vào history
        /// </summary>
        private void ckbStoreHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (mod && ckbStoreHistory.Checked == storeHistory) { mod = false; return; }
            mod |= storeHistory != ckbStoreHistory.Checked;
        }
        /// <summary>
        /// combobox selected index change
        /// </summary>
        private void countMethodCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_SelectedIndexChanged(sender, inputMethod);
        }
        /// <summary>
        /// hàm chung của virtual combobox selected index change
        /// </summary>
        /// <param name="sender">combobox</param>
        /// <param name="index">biến toàn cục tương đương</param>
        private void cb_SelectedIndexChanged(object sender, int index)
        {
            ComboBox cb = sender as ComboBox;
            if (mod && cb.SelectedIndex == index) { mod = false; return; }
            mod |= cb.SelectedIndex != index;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            nudCollapsedSpd.Value = 10;
            ckbFastFact.Checked = false;
            ckbUsedSign.Checked = true;
            ckbReadDict.Checked = true;
            ckbStoreHistory.Checked = false;
            cbbInputMethod.SelectedIndex = 0;
            mod |= nudCollapsedSpd.Value != transfer;
            mod |= f3 != ckbFastFact.Checked;
            mod |= sign != ckbUsedSign.Checked;
            mod |= storeHistory != ckbStoreHistory.Checked;
            mod |= readDictChanged;
            mod |= cbbInputMethod.SelectedIndex != inputMethod;
        }
    }
}