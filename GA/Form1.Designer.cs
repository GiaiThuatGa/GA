namespace GA
{
    partial class Form1
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
            this.btnReadText = new System.Windows.Forms.Button();
            this.btnWriteText = new System.Windows.Forms.Button();
            this.btnTinhKC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadText
            // 
            this.btnReadText.Location = new System.Drawing.Point(14, 16);
            this.btnReadText.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnReadText.Name = "btnReadText";
            this.btnReadText.Size = new System.Drawing.Size(200, 100);
            this.btnReadText.TabIndex = 0;
            this.btnReadText.Text = "Đọc Text";
            this.btnReadText.UseVisualStyleBackColor = true;
            this.btnReadText.Click += new System.EventHandler(this.btnReadText_Click);
            // 
            // btnWriteText
            // 
            this.btnWriteText.Location = new System.Drawing.Point(249, 16);
            this.btnWriteText.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnWriteText.Name = "btnWriteText";
            this.btnWriteText.Size = new System.Drawing.Size(200, 100);
            this.btnWriteText.TabIndex = 0;
            this.btnWriteText.Text = "Ghi Text";
            this.btnWriteText.UseVisualStyleBackColor = true;
            this.btnWriteText.Click += new System.EventHandler(this.btnWriteText_Click);
            // 
            // btnTinhKC
            // 
            this.btnTinhKC.Location = new System.Drawing.Point(14, 143);
            this.btnTinhKC.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnTinhKC.Name = "btnTinhKC";
            this.btnTinhKC.Size = new System.Drawing.Size(435, 100);
            this.btnTinhKC.TabIndex = 0;
            this.btnTinhKC.Text = "Tính khoảng cách";
            this.btnTinhKC.UseVisualStyleBackColor = true;
            this.btnTinhKC.Click += new System.EventHandler(this.btnTinhKC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 257);
            this.Controls.Add(this.btnTinhKC);
            this.Controls.Add(this.btnWriteText);
            this.Controls.Add(this.btnReadText);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadText;
        private System.Windows.Forms.Button btnWriteText;
        private System.Windows.Forms.Button btnTinhKC;
    }
}

