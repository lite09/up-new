using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace up
{
    public partial class tre_folder_form : Form
    {
        Form1 form1;
        public tre_folder_form(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }

        private void tre_folder_Leave(object sender, EventArgs e)
        {
            form1.tre_conf.tre_folder = tre_folder.Text;
        }

        private void tre_folder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void tre_folder_DragDrop(object sender, DragEventArgs e)
        {
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (Directory.Exists(file_name[0]))
                tre_folder.Text = form1.tre_conf.tre_folder = file_name[0];
            else
                tre_folder.Text = form1.tre_conf.tre_folder = Path.GetDirectoryName(file_name[0]);
        }

        private void options_lb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void options_lb_DragDrop(object sender, DragEventArgs e)
        {
            // --------------------------- обнуление ---------------------------
            options_lb.Items.Clear();
            form1.tre_conf.head_options.Clear();
            // --------------------------- обнуление ---------------------------

            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            form1.tre_conf.file_head_options = file_name[0];
            options_lb.Items.Add(Path.GetFileName(form1.tre_conf.file_head_options));

            string fileText = System.IO.File.ReadAllText(file_name[0], Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(fileText);
            Regex sub_string = new Regex(";");
            string[] line;

            int i = 0;
            foreach (Match m in words)
            {
                if (i == 0) { i++; continue; }

                line = sub_string.Split(m.Groups[1].Value);
                string[] ops = new string[2];
                if (line[0] != "")
                {
                    ops[0] = line[0];
                    ops[1] = line[1];
                    form1.tre_conf.head_options.Add(ops);
                }
            }
        }

        private void bool_mod_catalog_CheckedChanged(object sender, EventArgs e)
        {
            if (bool_mod_catalog.Checked)
            {
                form1.tre_conf.tre_bool_mod_catalog = true;
                label_mod_catalog.Enabled = true;
                list_mod_catalog.Enabled = true;
            }
            else
            {
                form1.tre_conf.tre_bool_mod_catalog = false;
                label_mod_catalog.Enabled = false;
                list_mod_catalog.Enabled = false;
            }
        }

        private void list_mod_catalog_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void list_mod_catalog_DragDrop(object sender, DragEventArgs e)
        {
            // ----------------------------- обнуление -----------------------------
            form1.tre_conf.tre_list_categoryes.Clear();
            list_mod_catalog.Items.Clear();
            // ----------------------------- обнуление -----------------------------

            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            form1.tre_conf.file_list_mod_catalog = file_name[0];
            list_mod_catalog.Items.Add(Path.GetFileName(form1.tre_conf.file_list_mod_catalog));

            string catalogs = File.ReadAllText(form1.tre_conf.file_list_mod_catalog, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(catalogs);
            Regex sub_string = new Regex(";");
            string[] line;

            foreach (Match m in words)
            {
                line = sub_string.Split(m.Groups[1].Value);
                string[] cats = new string[2];
                if (line[0] != "")
                {
                    cats[0] = line[0];
                    cats[1] = line[1];
                    form1.tre_conf.tre_list_categoryes.Add(cats);
                }

            }
            form1.tre_conf.tre_list_categoryes.RemoveAt(0);
        }

        private void tb_save_ids_dir_Leave(object sender, EventArgs e)
        {
            form1.tre_conf.save_ids_dir = tb_save_ids_dir.Text;
        }

        private void tb_save_ids_dir_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void tb_save_ids_dir_DragDrop(object sender, DragEventArgs e)
        {
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (Directory.Exists(file_name[0]))
                tb_save_ids_dir.Text = form1.tre_conf.save_ids_dir = file_name[0];
            else
                tb_save_ids_dir.Text = form1.tre_conf.save_ids_dir = Path.GetDirectoryName(file_name[0]);
        }

        private void hi_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void tb_easy_del_old_itm_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(tb_easy_del_old_itm.Text, out _))
                form1.tre_conf.tre_easy_del_old_itm_count = tb_easy_del_old_itm.Text;
            else
                form1.tre_conf.tre_easy_del_old_itm_count = tb_easy_del_old_itm.Text = "";
        }

        private void cb_easy_del_old_itm_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_easy_del_old_itm.Checked)
            {
                form1.tre_conf.tre_easy_del_old_itm_bool = true;
                label22.Enabled = true;
                tb_easy_del_old_itm.Enabled = true;
            }
            else
            {
                form1.tre_conf.tre_easy_del_old_itm_bool = false;
                label22.Enabled = false;
                tb_easy_del_old_itm.Enabled = false;
            }
        }

        private void cb_full_del_old_itm_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_full_del_old_itm.Checked)
            {
                form1.tre_conf.tre_full_del_old_itm_bool = true;
                lb_deact_full.Enabled = true;
                tb_full_del_old_itm.Enabled = true;
            }
            else
            {
                form1.tre_conf.tre_full_del_old_itm_bool = false;
                lb_deact_full.Enabled = false;
                tb_full_del_old_itm.Enabled = false;
            }
        }

        private void tb_full_del_old_itm_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(tb_full_del_old_itm.Text, out _))
                form1.tre_conf.tre_full_del_old_itm_count = tb_full_del_old_itm.Text;
            else
                form1.tre_conf.tre_full_del_old_itm_count = tb_full_del_old_itm.Text = "";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                form1.ctrl(control);
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control item in gb.Controls)
                        form1.ctrl(item);
                }
            }

            form1.f.clear_configure("tre_folder");
        }
    }
}