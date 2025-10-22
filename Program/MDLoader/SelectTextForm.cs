using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MDLoader
{
    public partial class SelectTextForm : Form
    {
        public string SelectedText { get; private set; }

        public SelectTextForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数：设置标题、提示文字和下拉选项
        /// </summary>
        public SelectTextForm(string title, string label, IEnumerable<string> options)
        {
            InitializeComponent();
            this.Text = title;
            lab_name.Text = label;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
            foreach (var opt in options)
                comboBox1.Items.Add(opt);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void SelectTextForm_Load(object sender, EventArgs e)
        {
            // 可在此初始化样式或快捷键
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                SelectedText = comboBox1.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择一个项目。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 静态函数：显示选择对话框并返回所选文本
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="label">提示文字</param>
        /// <param name="options">下拉选项列表</param>
        /// <returns>返回所选文本，若取消则返回 null</returns>
        public static string ShowSelectDialog(string title, string label, IEnumerable<string> options)
        {
            using (var dlg = new SelectTextForm(title, label, options))
            {
                return dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedText : null;
            }
        }
    }
}
