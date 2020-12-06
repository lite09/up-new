namespace up
{
    partial class tre_folder_form
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
            this.tb_easy_del_old_itm = new System.Windows.Forms.TextBox();
            this.tb_save_ids_dir = new System.Windows.Forms.TextBox();
            this.list_mod_catalog = new System.Windows.Forms.ListBox();
            this.options_lb = new System.Windows.Forms.ListBox();
            this.tre_folder = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tb_full_del_old_itm = new System.Windows.Forms.TextBox();
            this.cb_full_del_old_itm = new System.Windows.Forms.CheckBox();
            this.lb_deact_full = new System.Windows.Forms.Label();
            this.tre_folder_label = new System.Windows.Forms.Label();
            this.cb_easy_del_old_itm = new System.Windows.Forms.CheckBox();
            this.bool_mod_catalog = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.options_label = new System.Windows.Forms.Label();
            this.lb_save_ids = new System.Windows.Forms.Label();
            this.label_mod_catalog = new System.Windows.Forms.Label();
            this.hi = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_easy_del_old_itm
            // 
            this.tb_easy_del_old_itm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tb_easy_del_old_itm.Location = new System.Drawing.Point(158, 103);
            this.tb_easy_del_old_itm.Name = "tb_easy_del_old_itm";
            this.tb_easy_del_old_itm.Size = new System.Drawing.Size(191, 20);
            this.tb_easy_del_old_itm.TabIndex = 77;
            this.tb_easy_del_old_itm.Leave += new System.EventHandler(this.tb_easy_del_old_itm_Leave);
            // 
            // tb_save_ids_dir
            // 
            this.tb_save_ids_dir.AllowDrop = true;
            this.tb_save_ids_dir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tb_save_ids_dir.Location = new System.Drawing.Point(531, 19);
            this.tb_save_ids_dir.Name = "tb_save_ids_dir";
            this.tb_save_ids_dir.Size = new System.Drawing.Size(191, 20);
            this.tb_save_ids_dir.TabIndex = 76;
            this.tb_save_ids_dir.DragDrop += new System.Windows.Forms.DragEventHandler(this.tb_save_ids_dir_DragDrop);
            this.tb_save_ids_dir.DragEnter += new System.Windows.Forms.DragEventHandler(this.tb_save_ids_dir_DragEnter);
            this.tb_save_ids_dir.Leave += new System.EventHandler(this.tb_save_ids_dir_Leave);
            // 
            // list_mod_catalog
            // 
            this.list_mod_catalog.AllowDrop = true;
            this.list_mod_catalog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.list_mod_catalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_mod_catalog.FormattingEnabled = true;
            this.list_mod_catalog.Location = new System.Drawing.Point(531, 55);
            this.list_mod_catalog.Name = "list_mod_catalog";
            this.list_mod_catalog.Size = new System.Drawing.Size(191, 28);
            this.list_mod_catalog.TabIndex = 75;
            this.list_mod_catalog.DragDrop += new System.Windows.Forms.DragEventHandler(this.list_mod_catalog_DragDrop);
            this.list_mod_catalog.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_mod_catalog_DragEnter);
            // 
            // options_lb
            // 
            this.options_lb.AllowDrop = true;
            this.options_lb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.options_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.options_lb.FormattingEnabled = true;
            this.options_lb.Location = new System.Drawing.Point(170, 67);
            this.options_lb.Name = "options_lb";
            this.options_lb.Size = new System.Drawing.Size(190, 28);
            this.options_lb.TabIndex = 74;
            this.options_lb.DragDrop += new System.Windows.Forms.DragEventHandler(this.options_lb_DragDrop);
            this.options_lb.DragEnter += new System.Windows.Forms.DragEventHandler(this.options_lb_DragEnter);
            // 
            // tre_folder
            // 
            this.tre_folder.AllowDrop = true;
            this.tre_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tre_folder.Location = new System.Drawing.Point(169, 31);
            this.tre_folder.Name = "tre_folder";
            this.tre_folder.Size = new System.Drawing.Size(191, 20);
            this.tre_folder.TabIndex = 73;
            this.tre_folder.DragDrop += new System.Windows.Forms.DragEventHandler(this.tre_folder_DragDrop);
            this.tre_folder.DragEnter += new System.Windows.Forms.DragEventHandler(this.tre_folder_DragEnter);
            this.tre_folder.Leave += new System.EventHandler(this.tre_folder_Leave);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tb_full_del_old_itm);
            this.groupBox7.Controls.Add(this.list_mod_catalog);
            this.groupBox7.Controls.Add(this.cb_full_del_old_itm);
            this.groupBox7.Controls.Add(this.lb_deact_full);
            this.groupBox7.Controls.Add(this.tre_folder_label);
            this.groupBox7.Controls.Add(this.tb_easy_del_old_itm);
            this.groupBox7.Controls.Add(this.cb_easy_del_old_itm);
            this.groupBox7.Controls.Add(this.tb_save_ids_dir);
            this.groupBox7.Controls.Add(this.bool_mod_catalog);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.options_label);
            this.groupBox7.Controls.Add(this.lb_save_ids);
            this.groupBox7.Controls.Add(this.label_mod_catalog);
            this.groupBox7.Location = new System.Drawing.Point(12, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(728, 143);
            this.groupBox7.TabIndex = 78;
            this.groupBox7.TabStop = false;
            // 
            // tb_full_del_old_itm
            // 
            this.tb_full_del_old_itm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tb_full_del_old_itm.Location = new System.Drawing.Point(531, 103);
            this.tb_full_del_old_itm.Name = "tb_full_del_old_itm";
            this.tb_full_del_old_itm.Size = new System.Drawing.Size(191, 20);
            this.tb_full_del_old_itm.TabIndex = 80;
            this.tb_full_del_old_itm.Leave += new System.EventHandler(this.tb_full_del_old_itm_Leave);
            // 
            // cb_full_del_old_itm
            // 
            this.cb_full_del_old_itm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cb_full_del_old_itm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_full_del_old_itm.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_full_del_old_itm.ForeColor = System.Drawing.Color.DarkGreen;
            this.cb_full_del_old_itm.Location = new System.Drawing.Point(510, 102);
            this.cb_full_del_old_itm.Name = "cb_full_del_old_itm";
            this.cb_full_del_old_itm.Size = new System.Drawing.Size(17, 22);
            this.cb_full_del_old_itm.TabIndex = 79;
            this.cb_full_del_old_itm.UseVisualStyleBackColor = false;
            this.cb_full_del_old_itm.CheckedChanged += new System.EventHandler(this.cb_full_del_old_itm_CheckedChanged);
            // 
            // lb_deact_full
            // 
            this.lb_deact_full.Location = new System.Drawing.Point(371, 103);
            this.lb_deact_full.Name = "lb_deact_full";
            this.lb_deact_full.Size = new System.Drawing.Size(111, 26);
            this.lb_deact_full.TabIndex = 78;
            this.lb_deact_full.Text = "Сроки деактивации, обычныи режим";
            this.lb_deact_full.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tre_folder_label
            // 
            this.tre_folder_label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tre_folder_label.ForeColor = System.Drawing.Color.DarkGreen;
            this.tre_folder_label.Location = new System.Drawing.Point(8, 20);
            this.tre_folder_label.Name = "tre_folder_label";
            this.tre_folder_label.Size = new System.Drawing.Size(122, 19);
            this.tre_folder_label.TabIndex = 52;
            this.tre_folder_label.Text = "Структура папок";
            // 
            // cb_easy_del_old_itm
            // 
            this.cb_easy_del_old_itm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cb_easy_del_old_itm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_easy_del_old_itm.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_easy_del_old_itm.ForeColor = System.Drawing.Color.DarkGreen;
            this.cb_easy_del_old_itm.Location = new System.Drawing.Point(134, 102);
            this.cb_easy_del_old_itm.Name = "cb_easy_del_old_itm";
            this.cb_easy_del_old_itm.Size = new System.Drawing.Size(17, 22);
            this.cb_easy_del_old_itm.TabIndex = 70;
            this.cb_easy_del_old_itm.UseVisualStyleBackColor = false;
            this.cb_easy_del_old_itm.CheckedChanged += new System.EventHandler(this.cb_easy_del_old_itm_CheckedChanged);
            // 
            // bool_mod_catalog
            // 
            this.bool_mod_catalog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bool_mod_catalog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bool_mod_catalog.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bool_mod_catalog.ForeColor = System.Drawing.Color.DarkGreen;
            this.bool_mod_catalog.Location = new System.Drawing.Point(510, 58);
            this.bool_mod_catalog.Name = "bool_mod_catalog";
            this.bool_mod_catalog.Size = new System.Drawing.Size(17, 22);
            this.bool_mod_catalog.TabIndex = 56;
            this.bool_mod_catalog.UseVisualStyleBackColor = false;
            this.bool_mod_catalog.CheckedChanged += new System.EventHandler(this.bool_mod_catalog_CheckedChanged);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(9, 103);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 26);
            this.label22.TabIndex = 69;
            this.label22.Text = "Сроки деактивации, простой режим";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // options_label
            // 
            this.options_label.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.options_label.ForeColor = System.Drawing.Color.DarkGreen;
            this.options_label.Location = new System.Drawing.Point(9, 66);
            this.options_label.Name = "options_label";
            this.options_label.Size = new System.Drawing.Size(100, 17);
            this.options_label.TabIndex = 55;
            this.options_label.Text = "Таблица свойств";
            // 
            // lb_save_ids
            // 
            this.lb_save_ids.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_save_ids.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_save_ids.Location = new System.Drawing.Point(371, 26);
            this.lb_save_ids.Name = "lb_save_ids";
            this.lb_save_ids.Size = new System.Drawing.Size(121, 16);
            this.lb_save_ids.TabIndex = 58;
            this.lb_save_ids.Text = "Папка сохранения id";
            // 
            // label_mod_catalog
            // 
            this.label_mod_catalog.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_mod_catalog.ForeColor = System.Drawing.Color.DarkGreen;
            this.label_mod_catalog.Location = new System.Drawing.Point(371, 61);
            this.label_mod_catalog.Name = "label_mod_catalog";
            this.label_mod_catalog.Size = new System.Drawing.Size(138, 17);
            this.label_mod_catalog.TabIndex = 56;
            this.label_mod_catalog.Text = "Соотнесение категорий";
            // 
            // hi
            // 
            this.hi.Location = new System.Drawing.Point(350, 170);
            this.hi.Name = "hi";
            this.hi.Size = new System.Drawing.Size(75, 23);
            this.hi.TabIndex = 79;
            this.hi.Text = "Ok";
            this.hi.UseVisualStyleBackColor = true;
            this.hi.Click += new System.EventHandler(this.hi_Click);
            // 
            // clear
            // 
            this.clear.BackgroundImage = global::up.Properties.Resources._6412491_preview;
            this.clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clear.Location = new System.Drawing.Point(717, 170);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(23, 23);
            this.clear.TabIndex = 80;
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // tre_folder_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(750, 198);
            this.ControlBox = false;
            this.Controls.Add(this.clear);
            this.Controls.Add(this.hi);
            this.Controls.Add(this.options_lb);
            this.Controls.Add(this.tre_folder);
            this.Controls.Add(this.groupBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "tre_folder_form";
            this.Text = "Структура папок";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tb_easy_del_old_itm;
        public System.Windows.Forms.TextBox tb_save_ids_dir;
        public System.Windows.Forms.ListBox list_mod_catalog;
        public System.Windows.Forms.ListBox options_lb;
        public System.Windows.Forms.TextBox tre_folder;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label tre_folder_label;
        public System.Windows.Forms.CheckBox cb_easy_del_old_itm;
        public System.Windows.Forms.CheckBox bool_mod_catalog;
        private System.Windows.Forms.Label options_label;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label lb_save_ids;
        public System.Windows.Forms.Label label_mod_catalog;
        private System.Windows.Forms.Button hi;
        public System.Windows.Forms.TextBox tb_full_del_old_itm;
        public System.Windows.Forms.CheckBox cb_full_del_old_itm;
        public System.Windows.Forms.Label lb_deact_full;
        private System.Windows.Forms.Button clear;
    }
}