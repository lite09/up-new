using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace up
{
    public partial class Form2 : Form
    {
        public Form1 f1;
        public Form2()
        {
            InitializeComponent();
        }

        private void Exception_rules_xml_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Exception_rules_xml_DragDrop(object sender, DragEventArgs e)
        {

            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name;
            Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
            f1.easy.exception_rules_xml = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            exception_rules_xml.Items.Clear();
            exception_rules_xml.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string exceptions = File.ReadAllText(f1.easy.exception_rules_xml, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(exceptions);
            Regex sub_string = new Regex(";");
            string[] line;

            int i = -1;
            f1.easy.exception_name.Clear();
            foreach (Match m in words)
            {
                i++;
                if (i == 0) continue;

                line = sub_string.Split(m.Groups[1].Value);
                if (i == 1)
                    if (line[1] != "")
                    {
                        f1.easy.exception_any_side = Convert.ToUInt32(line[1]);
                        f1.easy.exception_sum_side = Convert.ToUInt32(line[2]);
                        f1.easy.exception_weight = Convert.ToUInt32(line[3]);
                    }
                if (line[0] != "") f1.easy.exception_name.Add(line[0]);
            }
            richTextBox1.Text = "";
            foreach (string str in f1.easy.exception_name)
                richTextBox1.Text = richTextBox1.Text + "\r\n " + str;

            //----------------------------------------чтение исключающих данных ----------------------------------------
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner;
        }

        private void Prefix_for_id_Leave(object sender, EventArgs e)
        {
            f1.easy.prefix_for_id = prefix_for_id.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void Exclude_in_other_store_CheckedChanged(object sender, EventArgs e)
        {
            if (exclude_in_other_store.Checked) f1.easy.check_delivery_options = true;
            else f1.easy.check_delivery_options = false;
        }

        private void Correction_of_quantity_CheckedChanged(object sender, EventArgs e)
        {
            if (correction_of_quantity.Checked) f1.easy.get_min_sale = true;
            else f1.easy.get_min_sale = false;
        }

        private void Use_base_price_CheckedChanged(object sender, EventArgs e)
        {
            if (use_base_price.Checked) f1.easy.output_base_price = true;
            else f1.easy.output_base_price = false;
        }

        private void New_price_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name;
            Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
            f1.easy.file_to_create_new_price = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            new_price.Items.Clear();
            new_price.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string cf = File.ReadAllText(f1.easy.file_to_create_new_price, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f1.easy.coefficient.Clear();
            foreach (Match m in words)
            {
                line = sub_string.Split(m.Groups[1].Value);
                float[] coeff = new float[3];
                if (line[1] != "")
                {
                    coeff[0] = Convert.ToSingle(line[0], CultureInfo.InvariantCulture);
                    coeff[1] = Convert.ToSingle(line[1], CultureInfo.InvariantCulture);
                    coeff[2] = Convert.ToSingle(line[2], CultureInfo.InvariantCulture);
                }
                if (line[0] != "") f1.easy.coefficient.Add(coeff);
            }
            foreach (float[] str in f1.easy.coefficient)
                richTextBox1.Text = richTextBox1.Text + str[0] + ":" + str[1] + " : " + str[2] + "\r\n";
            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
        }

        private void New_price_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Correction_quantity_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Correction_quantity_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name;
            Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
            f1.easy.file_to_create_new_quality = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            correction_quantity.Items.Clear();
            correction_quantity.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string cf = File.ReadAllText(f1.easy.file_to_create_new_quality, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f1.easy.quality_correct.Clear();
            foreach (Match m in words)
            {
                line = sub_string.Split(m.Groups[1].Value);
                string[] quality = new string[2];
                if (line[0] != "")
                {
                    quality[0] = line[0];
                    quality[1] = line[1];
                }
                if (line[0] != "") f1.easy.quality_correct.Add(quality);
            }
            foreach (string[] str in f1.easy.quality_correct)
                richTextBox1.Text = richTextBox1.Text + str[0] + ":" + str[1] + "\r\n";
            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //var pt = f1.set_easy.Owner;
            //f1.set_easy.Dispose(false);
            //f1.set_easy = new Form2();
            //f1.set_easy.Owner = pt;
            //f1.set_easy.Hide(); // f1.set_easy.Show();


            foreach (Control control in f1.set_easy.Controls)
                f1.ctrl(control);

            f1.f.clear_configure("easy");
        }


        // --------------------------- чтение id для easy mode ---------------------------
        private void tb_ids_folder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        
        private void tb_ids_folder_DragDrop(object sender, DragEventArgs e)
        {
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (Directory.Exists(file_name[0]))
                tb_ids_folder.Text = f1.easy.get_ids_dir = file_name[0];
            else
                tb_ids_folder.Text = f1.easy.get_ids_dir = Path.GetDirectoryName(file_name[0]);
        }

        private void tb_ids_folder_Leave(object sender, EventArgs e)
        {
            f1.easy.get_ids_dir = tb_ids_folder.Text;
        }

        private void cb_del_old_itm_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_del_old_itm.Checked)
            {
                f1.easy.tre_del_old_itm_bool = true;
                label22.Enabled = true;
                tb_del_old_itm.Enabled = true;
            }
            else
            {
                f1.easy.tre_del_old_itm_bool = false;
                label22.Enabled = false;
                tb_del_old_itm.Enabled = false;
            }
        }
        // --------------------------- чтение id для easy mode ---------------------------

        private void tb_del_old_itm_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(tb_del_old_itm.Text, out _))
                f1.easy.tre_del_old_itm_count = tb_del_old_itm.Text;
            else
                f1.easy.tre_del_old_itm_count = tb_del_old_itm.Text = "";
        }
    }
}
