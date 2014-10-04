namespace CSharpSample
{
    partial class MainForm
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
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(227, 38);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(121, 94);
            this.Button2.TabIndex = 9;
            this.Button2.Text = "Start Scan";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(287, 11);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(61, 20);
            this.Button1.TabIndex = 7;
            this.Button1.Text = "Browse";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(86, 12);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(195, 20);
            this.TextBox1.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 13);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "File to scan :";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "*.*";
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            this.OpenFileDialog1.Filter = "All files (*.*)|*.*";
            this.OpenFileDialog1.Title = "Select a file to scan...";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBox1);
            this.GroupBox1.Location = new System.Drawing.Point(13, 38);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(208, 94);
            this.GroupBox1.TabIndex = 8;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Scan Result";
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(6, 18);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(196, 69);
            this.ListBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 143);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.GroupBox1);
            this.Name = "Form1";
            this.Text = "ClamSharp CSharp Sample App";
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ListBox ListBox1;
    }
}

