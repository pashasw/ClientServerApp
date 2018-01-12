namespace ClientApp
{
    partial class Form2
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
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordClient = new System.Windows.Forms.TextBox();
            this.LoginClient = new System.Windows.Forms.TextBox();
            this.PasswordAgainClient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(34, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 24);
            this.button2.TabIndex = 13;
            this.button2.Text = "Регистрация";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Введите Пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Введите Логин";
            // 
            // PasswordClient
            // 
            this.PasswordClient.Location = new System.Drawing.Point(12, 98);
            this.PasswordClient.Multiline = true;
            this.PasswordClient.Name = "PasswordClient";
            this.PasswordClient.Size = new System.Drawing.Size(172, 23);
            this.PasswordClient.TabIndex = 9;
            // 
            // LoginClient
            // 
            this.LoginClient.Location = new System.Drawing.Point(12, 37);
            this.LoginClient.Multiline = true;
            this.LoginClient.Name = "LoginClient";
            this.LoginClient.Size = new System.Drawing.Size(172, 23);
            this.LoginClient.TabIndex = 8;
            // 
            // PasswordAgainClient
            // 
            this.PasswordAgainClient.Location = new System.Drawing.Point(12, 140);
            this.PasswordAgainClient.Multiline = true;
            this.PasswordAgainClient.Name = "PasswordAgainClient";
            this.PasswordAgainClient.Size = new System.Drawing.Size(172, 23);
            this.PasswordAgainClient.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Повторите Пароль";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(208, 222);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordAgainClient);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PasswordClient);
            this.Controls.Add(this.LoginClient);
            this.Name = "Form2";
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordClient;
        public System.Windows.Forms.TextBox LoginClient;
        private System.Windows.Forms.TextBox PasswordAgainClient;
        private System.Windows.Forms.Label label1;
    }
}