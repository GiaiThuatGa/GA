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
            this.btnTimTram = new System.Windows.Forms.Button();
            this.pnlbitmap = new System.Windows.Forms.Panel();
            this.pnlbitmap.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadText
            // 
            this.btnReadText.Location = new System.Drawing.Point(15, 6);
            this.btnReadText.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadText.Name = "btnReadText";
            this.btnReadText.Size = new System.Drawing.Size(110, 52);
            this.btnReadText.TabIndex = 0;
            this.btnReadText.Text = "Đọc Text";
            this.btnReadText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReadText.UseVisualStyleBackColor = true;
            this.btnReadText.Click += new System.EventHandler(this.btnReadText_Click);
            // 
            // btnTimTram
            // 
            this.btnTimTram.Location = new System.Drawing.Point(137, 6);
            this.btnTimTram.Margin = new System.Windows.Forms.Padding(6);
            this.btnTimTram.Name = "btnTimTram";
            this.btnTimTram.Size = new System.Drawing.Size(110, 52);
            this.btnTimTram.TabIndex = 0;
            this.btnTimTram.Text = "Tìm trạm";
            this.btnTimTram.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTimTram.UseVisualStyleBackColor = true;
            this.btnTimTram.Click += new System.EventHandler(this.btnTimTram_Click);
            // 
            // pnlbitmap
            // 
            this.pnlbitmap.AutoScroll = true;
            this.pnlbitmap.Controls.Add(this.btnTimTram);
            this.pnlbitmap.Controls.Add(this.btnReadText);
            this.pnlbitmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbitmap.Location = new System.Drawing.Point(0, 0);
            this.pnlbitmap.Name = "pnlbitmap";
            this.pnlbitmap.Size = new System.Drawing.Size(2548, 1383);
            this.pnlbitmap.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2548, 1383);
            this.Controls.Add(this.pnlbitmap);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlbitmap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadText;
        private System.Windows.Forms.Button btnTimTram;
        private System.Windows.Forms.Panel pnlbitmap;
    }
}

