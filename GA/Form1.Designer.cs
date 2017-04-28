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
            this.btnReadText.Location = new System.Drawing.Point(58, 30);
            this.btnReadText.Name = "btnReadText";
            this.btnReadText.Size = new System.Drawing.Size(75, 42);
            this.btnReadText.TabIndex = 0;
            this.btnReadText.Text = "Đọc Text";
            this.btnReadText.UseVisualStyleBackColor = true;
            this.btnReadText.Click += new System.EventHandler(this.btnReadText_Click);
            // 
            // btnWriteText
            // 
            this.btnWriteText.Location = new System.Drawing.Point(195, 30);
            this.btnWriteText.Name = "btnWriteText";
            this.btnWriteText.Size = new System.Drawing.Size(75, 42);
            this.btnWriteText.TabIndex = 0;
            this.btnWriteText.Text = "Ghi Text";
            this.btnWriteText.UseVisualStyleBackColor = true;
            this.btnWriteText.Click += new System.EventHandler(this.btnWriteText_Click);
            // 
            // btnTinhKC
            // 
            this.btnTinhKC.Location = new System.Drawing.Point(58, 130);
            this.btnTinhKC.Name = "btnTinhKC";
            this.btnTinhKC.Size = new System.Drawing.Size(102, 42);
            this.btnTinhKC.TabIndex = 0;
            this.btnTinhKC.Text = "Tính khoảng cách";
            this.btnTinhKC.UseVisualStyleBackColor = true;
            this.btnTinhKC.Click += new System.EventHandler(this.btnTinhKC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 281);
            this.Controls.Add(this.btnTinhKC);
            this.Controls.Add(this.btnWriteText);
            this.Controls.Add(this.btnReadText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadText;
        private System.Windows.Forms.Button btnWriteText;
        private System.Windows.Forms.Button btnTinhKC;
    }
}

