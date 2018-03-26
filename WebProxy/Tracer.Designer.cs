namespace WebProxy
{
    partial class RequestListener
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.monitorTxt = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.responseTxt = new System.Windows.Forms.RichTextBox();
            this.requestTxt = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.monitorTxt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 25);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(130, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Monitor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStop.Location = new System.Drawing.Point(205, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 25);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // monitorTxt
            // 
            this.monitorTxt.BackColor = System.Drawing.SystemColors.HighlightText;
            this.monitorTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.monitorTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monitorTxt.Location = new System.Drawing.Point(0, 0);
            this.monitorTxt.Name = "monitorTxt";
            this.monitorTxt.Size = new System.Drawing.Size(280, 22);
            this.monitorTxt.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.responseTxt);
            this.panel2.Controls.Add(this.requestTxt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 237);
            this.panel2.TabIndex = 4;
            // 
            // responseTxt
            // 
            this.responseTxt.Dock = System.Windows.Forms.DockStyle.Right;
            this.responseTxt.Location = new System.Drawing.Point(141, 0);
            this.responseTxt.Name = "responseTxt";
            this.responseTxt.Size = new System.Drawing.Size(139, 237);
            this.responseTxt.TabIndex = 4;
            this.responseTxt.Text = "";
            // 
            // requestTxt
            // 
            this.requestTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requestTxt.Location = new System.Drawing.Point(0, 0);
            this.requestTxt.Name = "requestTxt";
            this.requestTxt.Size = new System.Drawing.Size(280, 237);
            this.requestTxt.TabIndex = 3;
            this.requestTxt.Text = "";
            // 
            // RequestListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RequestListener";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox monitorTxt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox responseTxt;
        private System.Windows.Forms.RichTextBox requestTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStop;
    }
}

