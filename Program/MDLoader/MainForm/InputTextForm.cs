using System;
using System.Windows.Forms;

namespace MDLoader
{
    public partial class InputTextForm : Form
    {
        // 用户输入的文本
        public string InputText { get; private set; }

        // 静态方法，方便直接调用
        public static string ShowDialog(string title, string prompt, string defaultText = "")
        {
            using (var form = new InputTextForm())
            {
                form.Text = title;
                form.lab_name.Text = prompt;
                form.txt_input.Text = defaultText;
                form.txt_input.SelectAll();
                form.StartPosition = FormStartPosition.CenterParent;

                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                    return form.InputText;
                else
                    return null;
            }
        }

        public InputTextForm()
        {
            InitializeComponent();

            // 默认行为：按下 Enter 确定，Esc 取消
            this.AcceptButton = btn_submit;
            this.CancelButton = btn_cancel;

            // 绑定事件
            btn_submit.Click += btn_submit_Click;
            btn_cancel.Click += btn_cancel_Click;
        }

        private void frm_InputText_Load(object sender, EventArgs e)
        {
            // 可选：加载时自动聚焦输入框
            txt_input.Focus();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            InputText = txt_input.Text.Trim();
            if (string.IsNullOrEmpty(InputText))
            {
                MessageBox.Show("输入不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_input.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
