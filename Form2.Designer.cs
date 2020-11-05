namespace up
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.correction_quantity = new System.Windows.Forms.ListBox();
            this.new_price = new System.Windows.Forms.ListBox();
            this.use_base_price = new System.Windows.Forms.CheckBox();
            this.correction_of_quantity = new System.Windows.Forms.CheckBox();
            this.exclude_in_other_store = new System.Windows.Forms.CheckBox();
            this.prefix_for_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exception_rules_xml = new System.Windows.Forms.ListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(15, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 42);
            this.label5.TabIndex = 30;
            this.label5.Text = "Преобразование количества";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(15, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 39);
            this.label4.TabIndex = 29;
            this.label4.Text = "Формирование розничных цен";
            // 
            // correction_quantity
            // 
            this.correction_quantity.AllowDrop = true;
            this.correction_quantity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.correction_quantity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.correction_quantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.correction_quantity.FormattingEnabled = true;
            this.correction_quantity.Location = new System.Drawing.Point(134, 318);
            this.correction_quantity.Name = "correction_quantity";
            this.correction_quantity.Size = new System.Drawing.Size(220, 28);
            this.correction_quantity.TabIndex = 28;
            this.correction_quantity.DragDrop += new System.Windows.Forms.DragEventHandler(this.Correction_quantity_DragDrop);
            this.correction_quantity.DragEnter += new System.Windows.Forms.DragEventHandler(this.Correction_quantity_DragEnter);
            // 
            // new_price
            // 
            this.new_price.AllowDrop = true;
            this.new_price.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.new_price.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.new_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.new_price.FormattingEnabled = true;
            this.new_price.Location = new System.Drawing.Point(134, 265);
            this.new_price.Name = "new_price";
            this.new_price.Size = new System.Drawing.Size(220, 28);
            this.new_price.TabIndex = 27;
            this.new_price.DragDrop += new System.Windows.Forms.DragEventHandler(this.New_price_DragDrop);
            this.new_price.DragEnter += new System.Windows.Forms.DragEventHandler(this.New_price_DragEnter);
            // 
            // use_base_price
            // 
            this.use_base_price.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.use_base_price.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.use_base_price.ForeColor = System.Drawing.Color.DarkGreen;
            this.use_base_price.Location = new System.Drawing.Point(12, 213);
            this.use_base_price.Name = "use_base_price";
            this.use_base_price.Size = new System.Drawing.Size(342, 24);
            this.use_base_price.TabIndex = 26;
            this.use_base_price.Text = "Записывать цену из YML в поле \"Базовая цена\" ";
            this.use_base_price.UseVisualStyleBackColor = true;
            this.use_base_price.CheckedChanged += new System.EventHandler(this.Use_base_price_CheckedChanged);
            // 
            // correction_of_quantity
            // 
            this.correction_of_quantity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.correction_of_quantity.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.correction_of_quantity.ForeColor = System.Drawing.Color.DarkGreen;
            this.correction_of_quantity.Location = new System.Drawing.Point(12, 159);
            this.correction_of_quantity.Name = "correction_of_quantity";
            this.correction_of_quantity.Size = new System.Drawing.Size(342, 24);
            this.correction_of_quantity.TabIndex = 25;
            this.correction_of_quantity.Text = "Умножать минимальное количество на цену";
            this.correction_of_quantity.UseVisualStyleBackColor = true;
            this.correction_of_quantity.CheckedChanged += new System.EventHandler(this.Correction_of_quantity_CheckedChanged);
            // 
            // exclude_in_other_store
            // 
            this.exclude_in_other_store.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exclude_in_other_store.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exclude_in_other_store.ForeColor = System.Drawing.Color.DarkGreen;
            this.exclude_in_other_store.Location = new System.Drawing.Point(12, 101);
            this.exclude_in_other_store.Name = "exclude_in_other_store";
            this.exclude_in_other_store.Size = new System.Drawing.Size(342, 34);
            this.exclude_in_other_store.TabIndex = 24;
            this.exclude_in_other_store.Text = "Исключить товары с других складов              ";
            this.exclude_in_other_store.UseVisualStyleBackColor = true;
            this.exclude_in_other_store.CheckedChanged += new System.EventHandler(this.Exclude_in_other_store_CheckedChanged);
            // 
            // prefix_for_id
            // 
            this.prefix_for_id.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.prefix_for_id.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.prefix_for_id.Location = new System.Drawing.Point(133, 12);
            this.prefix_for_id.Name = "prefix_for_id";
            this.prefix_for_id.Size = new System.Drawing.Size(220, 20);
            this.prefix_for_id.TabIndex = 23;
            this.prefix_for_id.Leave += new System.EventHandler(this.Prefix_for_id_Leave);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(14, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Префикс для id";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 44);
            this.label2.TabIndex = 21;
            this.label2.Text = "Фильтр исключений";
            // 
            // exception_rules_xml
            // 
            this.exception_rules_xml.AllowDrop = true;
            this.exception_rules_xml.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.exception_rules_xml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.exception_rules_xml.FormattingEnabled = true;
            this.exception_rules_xml.Location = new System.Drawing.Point(134, 52);
            this.exception_rules_xml.Name = "exception_rules_xml";
            this.exception_rules_xml.Size = new System.Drawing.Size(220, 28);
            this.exception_rules_xml.TabIndex = 20;
            this.exception_rules_xml.DragDrop += new System.Windows.Forms.DragEventHandler(this.Exception_rules_xml_DragDrop);
            this.exception_rules_xml.DragEnter += new System.Windows.Forms.DragEventHandler(this.Exception_rules_xml_DragEnter);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.Location = new System.Drawing.Point(375, 8);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(288, 338);
            this.richTextBox1.TabIndex = 31;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(292, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(679, 401);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.correction_quantity);
            this.Controls.Add(this.new_price);
            this.Controls.Add(this.use_base_price);
            this.Controls.Add(this.correction_of_quantity);
            this.Controls.Add(this.exclude_in_other_store);
            this.Controls.Add(this.prefix_for_id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exception_rules_xml);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form2";
            this.ShowInTaskbar = false;
            this.Text = "Упрощенный режим";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox correction_quantity;
        private System.Windows.Forms.ListBox new_price;
        private System.Windows.Forms.CheckBox use_base_price;
        private System.Windows.Forms.CheckBox correction_of_quantity;
        private System.Windows.Forms.CheckBox exclude_in_other_store;
        private System.Windows.Forms.TextBox prefix_for_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox exception_rules_xml;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
    }
}