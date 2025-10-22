namespace MDLoader
{
    partial class InputTextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_name = new System.Windows.Forms.Label();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_name.Location = new System.Drawing.Point(36, 29);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(82, 24);
            this.lab_name.TabIndex = 0;
            this.lab_name.Text = "label1";
            // 
            // txt_input
            // 
            this.txt_input.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_input.Location = new System.Drawing.Point(40, 68);
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(586, 35);
            this.txt_input.TabIndex = 1;
            // 
            // btn_submit
            // 
            this.btn_submit.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_submit.Location = new System.Drawing.Point(382, 136);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(102, 39);
            this.btn_submit.TabIndex = 2;
            this.btn_submit.Text = "确定";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_cancel.Location = new System.Drawing.Point(512, 136);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(102, 39);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // frm_InputText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 208);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.txt_input);
            this.Controls.Add(this.lab_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frm_InputText";
            this.Text = "frm_InputText";
            this.Load += new System.EventHandler(this.frm_InputText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_cancel;
    }
}