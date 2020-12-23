namespace up
{
    partial class Ssh_form
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
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.host = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.save_folder = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(17, 20);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 14);
            this.label22.TabIndex = 70;
            this.label22.Text = "Логин";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(17, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 71;
            this.label1.Text = "Пароль";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(17, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 72;
            this.label2.Text = "Порт";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 73;
            this.label3.Text = "Адрес сервера";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(17, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 14);
            this.label4.TabIndex = 74;
            this.label4.Text = "Папка сохранения";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.login.Location = new System.Drawing.Point(139, 14);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(220, 20);
            this.login.TabIndex = 75;
            this.login.Leave += new System.EventHandler(this.login_Leave);
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pass.Location = new System.Drawing.Point(139, 44);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(220, 20);
            this.pass.TabIndex = 76;
            this.pass.UseSystemPasswordChar = true;
            this.pass.Leave += new System.EventHandler(this.pass_Leave);
            // 
            // host
            // 
            this.host.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.host.Location = new System.Drawing.Point(139, 73);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(220, 20);
            this.host.TabIndex = 77;
            this.host.Leave += new System.EventHandler(this.host_Leave);
            // 
            // port
            // 
            this.port.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.port.Location = new System.Drawing.Point(139, 101);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(220, 20);
            this.port.TabIndex = 78;
            this.port.Leave += new System.EventHandler(this.port_Leave);
            // 
            // save_folder
            // 
            this.save_folder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.save_folder.Location = new System.Drawing.Point(139, 125);
            this.save_folder.Name = "save_folder";
            this.save_folder.Size = new System.Drawing.Size(220, 20);
            this.save_folder.TabIndex = 79;
            this.save_folder.Leave += new System.EventHandler(this.save_folder_Leave);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(160, 154);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(62, 23);
            this.ok.TabIndex = 80;
            this.ok.Text = "Ок";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // Ssh_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(368, 187);
            this.ControlBox = false;
            this.Controls.Add(this.ok);
            this.Controls.Add(this.save_folder);
            this.Controls.Add(this.port);
            this.Controls.Add(this.host);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.login);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label22);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ssh_form";
            this.Text = "Настройки ssh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox login;
        public System.Windows.Forms.TextBox pass;
        public System.Windows.Forms.TextBox host;
        public System.Windows.Forms.TextBox port;
        public System.Windows.Forms.TextBox save_folder;
        private System.Windows.Forms.Button ok;
    }
}