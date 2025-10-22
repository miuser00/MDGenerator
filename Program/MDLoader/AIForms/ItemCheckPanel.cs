using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDLoader
{
    public partial class ItemCheckPanel : UserControl
    {
        public string OriginalText { get; set; }
        public string CorrectedText { get; set; }
        public string ReasonText { get; set; }

        public ItemCheckPanel()
        {
            InitializeComponent();

        }
        public void SetText(string original,string corrected,string reason)
        {
            rtb_content.Text = $"{original}\n{corrected}\n修改理由: {reason}";
            OriginalText = original;
            CorrectedText = corrected;
            ReasonText = reason;
            AdjustRichTextBoxHeight(rtb_content);
        }
        /// <summary>
        /// 在 RichTextBox 中搜索指定文字并修改颜色
        /// </summary>
        /// <param name="rtb">目标 RichTextBox</param>
        /// <param name="word">要搜索的文字</param>
        /// <param name="color">修改为的颜色</param>
        public void HighlightWord(string word, Color color)
        {
            if (string.IsNullOrEmpty(word)) return;

            int startIndex = 0;

            // 保存原始选择，避免干扰用户光标
            int originalSelectionStart = rtb_content.SelectionStart;
            int originalSelectionLength = rtb_content.SelectionLength;

            rtb_content.SuspendLayout();

            while (startIndex < rtb_content.Text.Length)
            {
                int wordStartIndex = rtb_content.Text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase);
                if (wordStartIndex == -1)
                    break;

                rtb_content.Select(wordStartIndex, word.Length);
                rtb_content.SelectionColor = color;

                startIndex = wordStartIndex + word.Length;
            }

            // 恢复原始光标选择
            rtb_content.Select(originalSelectionStart, originalSelectionLength);
            rtb_content.SelectionColor = rtb_content.ForeColor;

            rtb_content.ResumeLayout();
        }

        private void AdjustRichTextBoxHeight(RichTextBox rtb)
        {
            // 基础测量
            int baseHeight = TextRenderer.MeasureText(
                rtb.Text + " ",
                rtb.Font,
                new Size(rtb.Width, int.MaxValue),
                TextFormatFlags.WordBreak
            ).Height;

            // 获取当前行数
            int lineCount = rtb.GetLineFromCharIndex(rtb.TextLength) + 1;

            // 每行补偿系数（经验值，建议 1.5~2.5）
            float lineCompensation = 4;

            int compensatedHeight = (int)(baseHeight + lineCount * lineCompensation + 15);

            // 应用结果
            this.Height = compensatedHeight;
        }
        private void pan_checkitem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rtb_content_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemCheckPanel_SizeChanged(object sender, EventArgs e)
        {
            AdjustRichTextBoxHeight(rtb_content);
        }

        private void rtb_content_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
