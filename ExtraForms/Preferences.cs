using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Preferences : BaseForm
    {
        int inputMethod;
        decimal transfer;
        bool mod = false, sign, readDict, readDictChanged, storeHistory;

        public Preferences(params object[] paras)
        {
            InitializeComponent();
            LoadSettings(paras);
            label1.MouseDown += MoveForm;
            label2.MouseDown += MoveForm;
        }
        /// <summary>
        /// nạp cấu hình từ form chính
        /// </summary>
        /// <param name="paras">Speed, Animate, FastFact, IsSign, ReadDict, StoreHistory, FastInput, CountMethod</param>
        private void LoadSettings(params object[] paras)
        {
            nudCollapsedSpd.Value = transfer = (int)paras[0];
            ckbUsedSign.Checked = sign = (bool)paras[1];
            ckbReadDict.Checked = readDict = (bool)paras[2];
            ckbStoreHistory.Checked = storeHistory = (bool)paras[3];
            cbbInputMethod.SelectedIndex = inputMethod = (int)paras[4];
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
                    ckbUsedSign.Checked, 
                    ckbReadDict.Checked,
                    ckbStoreHistory.Checked,
                    //--------------------------------
                    cbbInputMethod.SelectedIndex
                    );
            }
            Close();
        }
        /// <summary>
        /// nút cancel
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            ComboBox cb = sender as ComboBox;
            if (mod && cb.SelectedIndex == inputMethod) { mod = false; return; }
            mod |= cb.SelectedIndex != inputMethod;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            nudCollapsedSpd.Value = 10;
            ckbUsedSign.Checked = true;
            ckbReadDict.Checked = true;
            ckbStoreHistory.Checked = false;
            cbbInputMethod.SelectedIndex = 0;
            mod |= nudCollapsedSpd.Value != transfer;
            mod |= sign != ckbUsedSign.Checked;
            mod |= storeHistory != ckbStoreHistory.Checked;
            mod |= readDictChanged;
            mod |= cbbInputMethod.SelectedIndex != inputMethod;
        }
    }
}