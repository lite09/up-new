namespace up
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.аапToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.праToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.токенToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prefix_for_id = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CPU_get = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.ya = new System.Windows.Forms.CheckBox();
            this.shed = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_manual = new System.Windows.Forms.RadioButton();
            this.rb_auto = new System.Windows.Forms.RadioButton();
            this.hi = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.openFile_token = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "Простой";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Azure;
            this.richTextBox2.Location = new System.Drawing.Point(864, 33);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(320, 156);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Файл xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Linen;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1196, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "Меню";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.аапToolStripMenuItem,
            this.праToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.токенToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // аапToolStripMenuItem
            // 
            this.аапToolStripMenuItem.Name = "аапToolStripMenuItem";
            this.аапToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.аапToolStripMenuItem.Text = "Упрощенный режим";
            this.аапToolStripMenuItem.Click += new System.EventHandler(this.АапToolStripMenuItem_Click);
            // 
            // праToolStripMenuItem
            // 
            this.праToolStripMenuItem.Name = "праToolStripMenuItem";
            this.праToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.праToolStripMenuItem.Text = "Полный режим";
            this.праToolStripMenuItem.Click += new System.EventHandler(this.ПраToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.ОткрытьToolStripMenuItem_Click);
            // 
            // токенToolStripMenuItem
            // 
            this.токенToolStripMenuItem.Name = "токенToolStripMenuItem";
            this.токенToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.токенToolStripMenuItem.Text = "Токен";
            this.токенToolStripMenuItem.Click += new System.EventHandler(this.ТокенToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.ВыходToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Папка с файлами дополнительных полей";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(500, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Сохранение результата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Название:";
            // 
            // prefix_for_id
            // 
            this.prefix_for_id.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prefix_for_id.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.prefix_for_id.Location = new System.Drawing.Point(220, 44);
            this.prefix_for_id.Name = "prefix_for_id";
            this.prefix_for_id.Size = new System.Drawing.Size(281, 20);
            this.prefix_for_id.TabIndex = 39;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(495, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 20);
            this.button2.TabIndex = 40;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Количество потоков:";
            // 
            // CPU_get
            // 
            this.CPU_get.FormattingEnabled = true;
            this.CPU_get.Location = new System.Drawing.Point(459, 76);
            this.CPU_get.MaxDropDownItems = 32;
            this.CPU_get.Name = "CPU_get";
            this.CPU_get.Size = new System.Drawing.Size(42, 21);
            this.CPU_get.TabIndex = 46;
            this.CPU_get.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.CPU_get.Leave += new System.EventHandler(this.CPU_get_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Время упрощенного запуска";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(57, 146);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 13);
            this.label21.TabIndex = 57;
            this.label21.Text = "Время полного запуска";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(682, 231);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 27);
            this.label22.TabIndex = 68;
            this.label22.Text = "Удаление старых товаров";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(769, 231);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(92, 27);
            this.label23.TabIndex = 69;
            this.label23.Text = "Сроки деактивации";
            this.label23.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(880, 239);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(128, 13);
            this.label24.TabIndex = 72;
            this.label24.Text = "Соотнесение категорий";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 20);
            this.button3.TabIndex = 73;
            this.button3.Text = "Обычный";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // ya
            // 
            this.ya.AutoSize = true;
            this.ya.Location = new System.Drawing.Point(1055, 335);
            this.ya.Name = "ya";
            this.ya.Size = new System.Drawing.Size(64, 17);
            this.ya.TabIndex = 74;
            this.ya.Text = "ya диск";
            this.ya.UseVisualStyleBackColor = true;
            this.ya.CheckedChanged += new System.EventHandler(this.Ya_CheckedChanged);
            // 
            // shed
            // 
            this.shed.BackColor = System.Drawing.Color.Bisque;
            this.shed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shed.Location = new System.Drawing.Point(495, 83);
            this.shed.Name = "shed";
            this.shed.Size = new System.Drawing.Size(75, 45);
            this.shed.TabIndex = 76;
            this.shed.Text = "Запуск";
            this.shed.UseVisualStyleBackColor = false;
            this.shed.Click += new System.EventHandler(this.Shed_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_manual);
            this.groupBox1.Controls.Add(this.rb_auto);
            this.groupBox1.Controls.Add(this.hi);
            this.groupBox1.Controls.Add(this.test);
            this.groupBox1.Controls.Add(this.shed);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 162);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            // 
            // rb_manual
            // 
            this.rb_manual.AutoSize = true;
            this.rb_manual.Checked = true;
            this.rb_manual.Location = new System.Drawing.Point(495, 57);
            this.rb_manual.Name = "rb_manual";
            this.rb_manual.Size = new System.Drawing.Size(59, 17);
            this.rb_manual.TabIndex = 80;
            this.rb_manual.TabStop = true;
            this.rb_manual.Text = "manual";
            this.rb_manual.UseVisualStyleBackColor = true;
            // 
            // rb_auto
            // 
            this.rb_auto.AutoSize = true;
            this.rb_auto.Location = new System.Drawing.Point(495, 42);
            this.rb_auto.Name = "rb_auto";
            this.rb_auto.Size = new System.Drawing.Size(46, 17);
            this.rb_auto.TabIndex = 79;
            this.rb_auto.Text = "auto";
            this.rb_auto.UseVisualStyleBackColor = true;
            // 
            // hi
            // 
            this.hi.Location = new System.Drawing.Point(495, 134);
            this.hi.Name = "hi";
            this.hi.Size = new System.Drawing.Size(75, 23);
            this.hi.TabIndex = 78;
            this.hi.Text = "i";
            this.hi.UseVisualStyleBackColor = true;
            this.hi.Click += new System.EventHandler(this.hi_Click);
            // 
            // test
            // 
            this.test.BackColor = System.Drawing.Color.Bisque;
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.test.Location = new System.Drawing.Point(597, 83);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(175, 45);
            this.test.TabIndex = 77;
            this.test.Text = "Сформировать дополнительные фаилы";
            this.test.UseVisualStyleBackColor = false;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "save.sav";
            this.openFile.Filter = "Файл сохранения(*.sav)|*.sav|Все файлы(*.*)|*.*";
            // 
            // saveFile
            // 
            this.saveFile.DefaultExt = "json";
            this.saveFile.FileName = "save";
            this.saveFile.Filter = "Файл сохранения(*.sav)|*.sav|Все файлы(*.*)|*.*";
            // 
            // openFile_token
            // 
            this.openFile_token.FileName = "token.txt";
            this.openFile_token.Filter = "Текстовый файл(*.txt)|*.txt|Все файлы(*.*)|*.*";
            // 
            // timer1
            // 
            this.timer1.Interval = 900;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(1046, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(78, 69);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режимы";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(15, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1008, 39);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1196, 329);
            this.Controls.Add(this.ya);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CPU_get);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.prefix_for_id);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem аапToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem праToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox prefix_for_id;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CPU_get;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox ya;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button shed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem токенToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFile_token;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Button hi;
        private System.Windows.Forms.RadioButton rb_manual;
        private System.Windows.Forms.RadioButton rb_auto;
    }
}

