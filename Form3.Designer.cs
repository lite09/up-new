namespace up
{
    partial class Form3
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.prefix_for_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exception_rules_xml = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.composition_of_package = new System.Windows.Forms.TextBox();
            this.type_of_package = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.coefficient_package_mass = new System.Windows.Forms.ListBox();
            this.del_not_full_packing_size = new System.Windows.Forms.CheckBox();
            this.transform_packing_size = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.color_from_YML = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.color = new System.Windows.Forms.ListBox();
            this.color_YML = new System.Windows.Forms.CheckBox();
            this.clear = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.correction_quantity = new System.Windows.Forms.ListBox();
            this.new_price = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.add_articule_to_short_name = new System.Windows.Forms.CheckBox();
            this.use_short_name = new System.Windows.Forms.CheckBox();
            this.no_watermark = new System.Windows.Forms.CheckBox();
            this.use_base_price = new System.Windows.Forms.CheckBox();
            this.use_xml_description = new System.Windows.Forms.CheckBox();
            this.correction_of_quantity = new System.Windows.Forms.CheckBox();
            this.exclude_in_other_store = new System.Windows.Forms.CheckBox();
            this.data_in_csv = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.gred_file = new System.Windows.Forms.ListBox();
            this.gred = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tre_folder_label = new System.Windows.Forms.Label();
            this.tre_folder = new System.Windows.Forms.TextBox();
            this.options_lb = new System.Windows.Forms.ListBox();
            this.options_label = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(340, 660);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.Location = new System.Drawing.Point(374, 8);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(364, 204);
            this.richTextBox1.TabIndex = 45;
            this.richTextBox1.Text = "";
            // 
            // prefix_for_id
            // 
            this.prefix_for_id.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.prefix_for_id.Location = new System.Drawing.Point(132, 12);
            this.prefix_for_id.Name = "prefix_for_id";
            this.prefix_for_id.Size = new System.Drawing.Size(220, 20);
            this.prefix_for_id.TabIndex = 37;
            this.prefix_for_id.Leave += new System.EventHandler(this.Prefix_for_id_Leave);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(13, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Префикс для id";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 44);
            this.label2.TabIndex = 35;
            this.label2.Text = "Фильтр исключений";
            // 
            // exception_rules_xml
            // 
            this.exception_rules_xml.AllowDrop = true;
            this.exception_rules_xml.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.exception_rules_xml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.exception_rules_xml.FormattingEnabled = true;
            this.exception_rules_xml.Location = new System.Drawing.Point(133, 52);
            this.exception_rules_xml.Name = "exception_rules_xml";
            this.exception_rules_xml.Size = new System.Drawing.Size(220, 28);
            this.exception_rules_xml.TabIndex = 34;
            this.exception_rules_xml.DragDrop += new System.Windows.Forms.DragEventHandler(this.Exception_rules_xml_DragDrop);
            this.exception_rules_xml.DragEnter += new System.Windows.Forms.DragEventHandler(this.Exception_rules_xml_DragEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.composition_of_package);
            this.groupBox2.Controls.Add(this.type_of_package);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.coefficient_package_mass);
            this.groupBox2.Controls.Add(this.del_not_full_packing_size);
            this.groupBox2.Controls.Add(this.transform_packing_size);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(374, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 213);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Правила габаритов YML";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.DarkGreen;
            this.label7.Location = new System.Drawing.Point(6, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 20);
            this.label7.TabIndex = 54;
            this.label7.Text = "Упаковка товара";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.DarkGreen;
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Тип упаковки";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // composition_of_package
            // 
            this.composition_of_package.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.composition_of_package.Location = new System.Drawing.Point(159, 122);
            this.composition_of_package.Name = "composition_of_package";
            this.composition_of_package.Size = new System.Drawing.Size(189, 21);
            this.composition_of_package.TabIndex = 53;
            this.composition_of_package.Leave += new System.EventHandler(this.Composition_of_package_Leave);
            // 
            // type_of_package
            // 
            this.type_of_package.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.type_of_package.Location = new System.Drawing.Point(159, 95);
            this.type_of_package.Name = "type_of_package";
            this.type_of_package.Size = new System.Drawing.Size(189, 21);
            this.type_of_package.TabIndex = 49;
            this.type_of_package.Leave += new System.EventHandler(this.Type_of_package_Leave);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.label1.Location = new System.Drawing.Point(12, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 39);
            this.label1.TabIndex = 52;
            this.label1.Text = "Таблица множителей габаритов и массы";
            // 
            // coefficient_package_mass
            // 
            this.coefficient_package_mass.AllowDrop = true;
            this.coefficient_package_mass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.coefficient_package_mass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coefficient_package_mass.FormattingEnabled = true;
            this.coefficient_package_mass.ItemHeight = 15;
            this.coefficient_package_mass.Location = new System.Drawing.Point(159, 163);
            this.coefficient_package_mass.Name = "coefficient_package_mass";
            this.coefficient_package_mass.Size = new System.Drawing.Size(189, 32);
            this.coefficient_package_mass.TabIndex = 52;
            this.coefficient_package_mass.DragDrop += new System.Windows.Forms.DragEventHandler(this.Coefficient_package_mass_DragDrop);
            this.coefficient_package_mass.DragEnter += new System.Windows.Forms.DragEventHandler(this.Coefficient_package_mass_DragEnter);
            // 
            // del_not_full_packing_size
            // 
            this.del_not_full_packing_size.BackColor = System.Drawing.Color.WhiteSmoke;
            this.del_not_full_packing_size.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.del_not_full_packing_size.Enabled = false;
            this.del_not_full_packing_size.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.del_not_full_packing_size.ForeColor = System.Drawing.Color.DarkGreen;
            this.del_not_full_packing_size.Location = new System.Drawing.Point(6, 56);
            this.del_not_full_packing_size.Name = "del_not_full_packing_size";
            this.del_not_full_packing_size.Size = new System.Drawing.Size(342, 22);
            this.del_not_full_packing_size.TabIndex = 50;
            this.del_not_full_packing_size.Text = "Удаление полупустых полей габаритовYML";
            this.del_not_full_packing_size.UseVisualStyleBackColor = false;
            this.del_not_full_packing_size.CheckedChanged += new System.EventHandler(this.Del_not_full_packing_size_CheckedChanged);
            // 
            // transform_packing_size
            // 
            this.transform_packing_size.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transform_packing_size.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.transform_packing_size.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transform_packing_size.ForeColor = System.Drawing.Color.DarkGreen;
            this.transform_packing_size.Location = new System.Drawing.Point(6, 28);
            this.transform_packing_size.Name = "transform_packing_size";
            this.transform_packing_size.Size = new System.Drawing.Size(342, 22);
            this.transform_packing_size.TabIndex = 49;
            this.transform_packing_size.Text = "Изменение габаритовYML";
            this.transform_packing_size.UseVisualStyleBackColor = false;
            this.transform_packing_size.CheckedChanged += new System.EventHandler(this.Transform_packing_size_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.color_from_YML);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.color);
            this.groupBox4.Controls.Add(this.color_YML);
            this.groupBox4.Location = new System.Drawing.Point(374, 437);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 126);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Цвета";
            // 
            // color_from_YML
            // 
            this.color_from_YML.BackColor = System.Drawing.Color.WhiteSmoke;
            this.color_from_YML.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.color_from_YML.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.color_from_YML.ForeColor = System.Drawing.Color.DarkGreen;
            this.color_from_YML.Location = new System.Drawing.Point(9, 91);
            this.color_from_YML.Name = "color_from_YML";
            this.color_from_YML.Size = new System.Drawing.Size(342, 22);
            this.color_from_YML.TabIndex = 55;
            this.color_from_YML.Text = "Получить цвет из имени в YML";
            this.color_from_YML.UseVisualStyleBackColor = false;
            this.color_from_YML.CheckedChanged += new System.EventHandler(this.Color_from_YML_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.label8.Location = new System.Drawing.Point(12, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 52);
            this.label8.TabIndex = 55;
            this.label8.Text = "Таблица стандартизации цветов";
            // 
            // color
            // 
            this.color.AllowDrop = true;
            this.color.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.color.FormattingEnabled = true;
            this.color.Location = new System.Drawing.Point(159, 57);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(192, 28);
            this.color.TabIndex = 52;
            this.color.DragDrop += new System.Windows.Forms.DragEventHandler(this.Color_DragDrop);
            this.color.DragEnter += new System.Windows.Forms.DragEventHandler(this.Color_DragEnter);
            // 
            // color_YML
            // 
            this.color_YML.BackColor = System.Drawing.Color.WhiteSmoke;
            this.color_YML.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.color_YML.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.color_YML.ForeColor = System.Drawing.Color.DarkGreen;
            this.color_YML.Location = new System.Drawing.Point(9, 19);
            this.color_YML.Name = "color_YML";
            this.color_YML.Size = new System.Drawing.Size(342, 22);
            this.color_YML.TabIndex = 55;
            this.color_YML.Text = "Использование и стандартизация цветов из YML";
            this.color_YML.UseVisualStyleBackColor = false;
            this.color_YML.CheckedChanged += new System.EventHandler(this.Color_YML_CheckedChanged);
            // 
            // clear
            // 
            this.clear.BackgroundImage = global::up.Properties.Resources._6412491_preview;
            this.clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clear.Location = new System.Drawing.Point(714, 660);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(23, 23);
            this.clear.TabIndex = 51;
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox6.Location = new System.Drawing.Point(5, 200);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(353, 59);
            this.groupBox6.TabIndex = 51;
            this.groupBox6.TabStop = false;
            // 
            // correction_quantity
            // 
            this.correction_quantity.AllowDrop = true;
            this.correction_quantity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.correction_quantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.correction_quantity.FormattingEnabled = true;
            this.correction_quantity.Location = new System.Drawing.Point(157, 379);
            this.correction_quantity.Name = "correction_quantity";
            this.correction_quantity.Size = new System.Drawing.Size(191, 28);
            this.correction_quantity.TabIndex = 42;
            this.correction_quantity.DragDrop += new System.Windows.Forms.DragEventHandler(this.Correction_quantity_DragDrop);
            this.correction_quantity.DragEnter += new System.Windows.Forms.DragEventHandler(this.Correction_quantity_DragEnter);
            // 
            // new_price
            // 
            this.new_price.AllowDrop = true;
            this.new_price.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.new_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.new_price.FormattingEnabled = true;
            this.new_price.Location = new System.Drawing.Point(157, 334);
            this.new_price.Name = "new_price";
            this.new_price.Size = new System.Drawing.Size(192, 28);
            this.new_price.TabIndex = 41;
            this.new_price.DragDrop += new System.Windows.Forms.DragEventHandler(this.New_price_DragDrop);
            this.new_price.DragEnter += new System.Windows.Forms.DragEventHandler(this.New_price_DragEnter);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.label4.Location = new System.Drawing.Point(10, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 39);
            this.label4.TabIndex = 43;
            this.label4.Text = "Формирование розничных цен";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.label5.Location = new System.Drawing.Point(9, 379);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 30);
            this.label5.TabIndex = 44;
            this.label5.Text = "Преобразование количества";
            // 
            // add_articule_to_short_name
            // 
            this.add_articule_to_short_name.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.add_articule_to_short_name.Enabled = false;
            this.add_articule_to_short_name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_articule_to_short_name.ForeColor = System.Drawing.Color.DarkGreen;
            this.add_articule_to_short_name.Location = new System.Drawing.Point(6, 235);
            this.add_articule_to_short_name.Name = "add_articule_to_short_name";
            this.add_articule_to_short_name.Size = new System.Drawing.Size(342, 22);
            this.add_articule_to_short_name.TabIndex = 49;
            this.add_articule_to_short_name.Text = "Добавитть артикул для поля Name";
            this.add_articule_to_short_name.UseVisualStyleBackColor = true;
            this.add_articule_to_short_name.CheckedChanged += new System.EventHandler(this.Add_articule_to_short_name_CheckedChanged);
            // 
            // use_short_name
            // 
            this.use_short_name.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.use_short_name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.use_short_name.ForeColor = System.Drawing.Color.DarkGreen;
            this.use_short_name.Location = new System.Drawing.Point(6, 207);
            this.use_short_name.Name = "use_short_name";
            this.use_short_name.Size = new System.Drawing.Size(342, 22);
            this.use_short_name.TabIndex = 48;
            this.use_short_name.Text = "Использовать короткие названия товаров";
            this.use_short_name.UseVisualStyleBackColor = true;
            this.use_short_name.CheckedChanged += new System.EventHandler(this.Use_short_name_CheckedChanged);
            // 
            // no_watermark
            // 
            this.no_watermark.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.no_watermark.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.no_watermark.ForeColor = System.Drawing.Color.DarkGreen;
            this.no_watermark.Location = new System.Drawing.Point(6, 179);
            this.no_watermark.Name = "no_watermark";
            this.no_watermark.Size = new System.Drawing.Size(342, 22);
            this.no_watermark.TabIndex = 51;
            this.no_watermark.Text = "Преобразование картинок (правила nw)";
            this.no_watermark.UseVisualStyleBackColor = true;
            this.no_watermark.CheckedChanged += new System.EventHandler(this.No_watermark_CheckedChanged);
            // 
            // use_base_price
            // 
            this.use_base_price.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.use_base_price.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.use_base_price.ForeColor = System.Drawing.Color.DarkGreen;
            this.use_base_price.Location = new System.Drawing.Point(6, 291);
            this.use_base_price.Name = "use_base_price";
            this.use_base_price.Size = new System.Drawing.Size(342, 22);
            this.use_base_price.TabIndex = 40;
            this.use_base_price.Text = "Записывать цену из YML в поле \"Базовая цена\" ";
            this.use_base_price.UseVisualStyleBackColor = true;
            this.use_base_price.CheckedChanged += new System.EventHandler(this.Use_base_price_CheckedChanged);
            // 
            // use_xml_description
            // 
            this.use_xml_description.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.use_xml_description.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.use_xml_description.Enabled = false;
            this.use_xml_description.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.use_xml_description.ForeColor = System.Drawing.Color.DarkGreen;
            this.use_xml_description.Location = new System.Drawing.Point(6, 151);
            this.use_xml_description.Name = "use_xml_description";
            this.use_xml_description.Size = new System.Drawing.Size(342, 22);
            this.use_xml_description.TabIndex = 50;
            this.use_xml_description.Text = "Включать описание из YML";
            this.use_xml_description.UseVisualStyleBackColor = true;
            this.use_xml_description.CheckedChanged += new System.EventHandler(this.Use_xml_description_CheckedChanged);
            // 
            // correction_of_quantity
            // 
            this.correction_of_quantity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.correction_of_quantity.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.correction_of_quantity.ForeColor = System.Drawing.Color.DarkGreen;
            this.correction_of_quantity.Location = new System.Drawing.Point(6, 263);
            this.correction_of_quantity.Name = "correction_of_quantity";
            this.correction_of_quantity.Size = new System.Drawing.Size(342, 22);
            this.correction_of_quantity.TabIndex = 39;
            this.correction_of_quantity.Text = "Умножать минимальное количество на цену";
            this.correction_of_quantity.UseVisualStyleBackColor = true;
            this.correction_of_quantity.CheckedChanged += new System.EventHandler(this.Correction_of_quantity_CheckedChanged);
            // 
            // exclude_in_other_store
            // 
            this.exclude_in_other_store.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exclude_in_other_store.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exclude_in_other_store.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exclude_in_other_store.ForeColor = System.Drawing.Color.DarkGreen;
            this.exclude_in_other_store.Location = new System.Drawing.Point(6, 95);
            this.exclude_in_other_store.Name = "exclude_in_other_store";
            this.exclude_in_other_store.Size = new System.Drawing.Size(342, 22);
            this.exclude_in_other_store.TabIndex = 38;
            this.exclude_in_other_store.Text = "Исключить товары с других складов              ";
            this.exclude_in_other_store.UseVisualStyleBackColor = false;
            this.exclude_in_other_store.CheckedChanged += new System.EventHandler(this.Exclude_in_other_store_CheckedChanged);
            // 
            // data_in_csv
            // 
            this.data_in_csv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.data_in_csv.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.data_in_csv.ForeColor = System.Drawing.Color.DarkGreen;
            this.data_in_csv.Location = new System.Drawing.Point(6, 123);
            this.data_in_csv.Name = "data_in_csv";
            this.data_in_csv.Size = new System.Drawing.Size(342, 22);
            this.data_in_csv.TabIndex = 48;
            this.data_in_csv.Text = "Приоритет данных из доп. таблицы";
            this.data_in_csv.UseVisualStyleBackColor = true;
            this.data_in_csv.CheckedChanged += new System.EventHandler(this.Data_in_csv_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.data_in_csv);
            this.groupBox1.Controls.Add(this.exclude_in_other_store);
            this.groupBox1.Controls.Add(this.correction_of_quantity);
            this.groupBox1.Controls.Add(this.use_xml_description);
            this.groupBox1.Controls.Add(this.use_base_price);
            this.groupBox1.Controls.Add(this.no_watermark);
            this.groupBox1.Controls.Add(this.use_short_name);
            this.groupBox1.Controls.Add(this.add_articule_to_short_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.new_price);
            this.groupBox1.Controls.Add(this.correction_quantity);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 425);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox5.Location = new System.Drawing.Point(5, 255);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(353, 166);
            this.groupBox5.TabIndex = 55;
            this.groupBox5.TabStop = false;
            // 
            // gred_file
            // 
            this.gred_file.AllowDrop = true;
            this.gred_file.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gred_file.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gred_file.FormattingEnabled = true;
            this.gred_file.Location = new System.Drawing.Point(157, 57);
            this.gred_file.Name = "gred_file";
            this.gred_file.Size = new System.Drawing.Size(192, 28);
            this.gred_file.TabIndex = 56;
            this.gred_file.DragDrop += new System.Windows.Forms.DragEventHandler(this.Gred_file_DragDrop);
            this.gred_file.DragEnter += new System.Windows.Forms.DragEventHandler(this.Gred_file_DragEnter);
            // 
            // gred
            // 
            this.gred.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gred.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gred.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gred.ForeColor = System.Drawing.Color.DarkGreen;
            this.gred.Location = new System.Drawing.Point(7, 19);
            this.gred.Name = "gred";
            this.gred.Size = new System.Drawing.Size(342, 22);
            this.gred.TabIndex = 58;
            this.gred.Text = "Включать размеры и размерные сетки из YML";
            this.gred.UseVisualStyleBackColor = false;
            this.gred.CheckedChanged += new System.EventHandler(this.Gred_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.label9.Location = new System.Drawing.Point(10, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 52);
            this.label9.TabIndex = 57;
            this.label9.Text = "Соотнесение таблиц размеров для ID категорий";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.gred);
            this.groupBox3.Controls.Add(this.gred_file);
            this.groupBox3.Location = new System.Drawing.Point(4, 437);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 126);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Размеры и размерные сетки";
            // 
            // tre_folder_label
            // 
            this.tre_folder_label.AutoSize = true;
            this.tre_folder_label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tre_folder_label.ForeColor = System.Drawing.Color.DarkGreen;
            this.tre_folder_label.Location = new System.Drawing.Point(12, 578);
            this.tre_folder_label.Name = "tre_folder_label";
            this.tre_folder_label.Size = new System.Drawing.Size(122, 19);
            this.tre_folder_label.TabIndex = 52;
            this.tre_folder_label.Text = "Структура папок";
            // 
            // tre_folder
            // 
            this.tre_folder.AllowDrop = true;
            this.tre_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tre_folder.Location = new System.Drawing.Point(161, 579);
            this.tre_folder.Name = "tre_folder";
            this.tre_folder.Size = new System.Drawing.Size(564, 20);
            this.tre_folder.TabIndex = 53;
            this.tre_folder.DragDrop += new System.Windows.Forms.DragEventHandler(this.tre_folder_DragDrop);
            this.tre_folder.DragEnter += new System.Windows.Forms.DragEventHandler(this.tre_folder_DragEnter);
            this.tre_folder.Leave += new System.EventHandler(this.tre_folder_Leave);
            // 
            // options_lb
            // 
            this.options_lb.AllowDrop = true;
            this.options_lb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.options_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.options_lb.FormattingEnabled = true;
            this.options_lb.Location = new System.Drawing.Point(162, 605);
            this.options_lb.Name = "options_lb";
            this.options_lb.Size = new System.Drawing.Size(191, 28);
            this.options_lb.TabIndex = 54;
            this.options_lb.DragDrop += new System.Windows.Forms.DragEventHandler(this.options_lb_DragDrop);
            this.options_lb.DragEnter += new System.Windows.Forms.DragEventHandler(this.options_lb_DragEnter);
            // 
            // options_label
            // 
            this.options_label.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.options_label.ForeColor = System.Drawing.Color.DarkGreen;
            this.options_label.Location = new System.Drawing.Point(14, 613);
            this.options_label.Name = "options_label";
            this.options_label.Size = new System.Drawing.Size(113, 20);
            this.options_label.TabIndex = 55;
            this.options_label.Text = "Таблица свойств";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(747, 695);
            this.ControlBox = false;
            this.Controls.Add(this.options_label);
            this.Controls.Add(this.options_lb);
            this.Controls.Add(this.tre_folder);
            this.Controls.Add(this.tre_folder_label);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.prefix_for_id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exception_rules_xml);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form3";
            this.Text = "Полный режим";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox prefix_for_id;
        public System.Windows.Forms.ListBox exception_rules_xml;
        public System.Windows.Forms.CheckBox transform_packing_size;
        public System.Windows.Forms.CheckBox del_not_full_packing_size;
        public System.Windows.Forms.TextBox composition_of_package;
        public System.Windows.Forms.TextBox type_of_package;
        public System.Windows.Forms.ListBox coefficient_package_mass;
        public System.Windows.Forms.ListBox color;
        public System.Windows.Forms.CheckBox color_YML;
        public System.Windows.Forms.CheckBox color_from_YML;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.ListBox correction_quantity;
        public System.Windows.Forms.ListBox new_price;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox add_articule_to_short_name;
        public System.Windows.Forms.CheckBox use_short_name;
        public System.Windows.Forms.CheckBox no_watermark;
        public System.Windows.Forms.CheckBox use_base_price;
        public System.Windows.Forms.CheckBox use_xml_description;
        public System.Windows.Forms.CheckBox correction_of_quantity;
        public System.Windows.Forms.CheckBox exclude_in_other_store;
        public System.Windows.Forms.CheckBox data_in_csv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.ListBox gred_file;
        public System.Windows.Forms.CheckBox gred;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label tre_folder_label;
        public System.Windows.Forms.TextBox tre_folder;
        private System.Windows.Forms.Label options_label;
        public System.Windows.Forms.ListBox options_lb;
    }
}