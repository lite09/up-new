using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace up
{
    public partial class Form3 : Form
    {
        public Form1 f;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Form1 form1)
        {
            InitializeComponent();
            f = form1;
        }

        private void Prefix_for_id_Leave(object sender, EventArgs e)
        {
            f.full.prefix_for_id = prefix_for_id.Text;
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
            f.full.exception_rules_xml = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            exception_rules_xml.Items.Clear();
            exception_rules_xml.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string exceptions = File.ReadAllText(f.full.exception_rules_xml, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(exceptions);
            Regex sub_string = new Regex(";");
            string[] line;

            int i = -1;
            f.full.exception_name.Clear();
            foreach (Match m in words)
            {
                i++;
                if (i == 0) continue;

                line = sub_string.Split(m.Groups[1].Value);
                if (i == 1)
                    if (line[1] != "")
                    {
                        f.full.exception_any_side = Convert.ToUInt32(line[1]);
                        f.full.exception_sum_side = Convert.ToUInt32(line[2]);
                        f.full.exception_weight = Convert.ToUInt32(line[3]);
                    }
                if (line[0] != "") f.full.exception_name.Add(line[0]);
            }
            richTextBox1.Text = "";
            foreach (string str in f.full.exception_name)
                richTextBox1.Text = richTextBox1.Text + "\r\n " + str;

            //----------------------------------------чтение исключающих данных ----------------------------------------
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            richTextBox1.Text = "";
        }

        private void Exclude_in_other_store_CheckedChanged(object sender, EventArgs e)
        {
            if (exclude_in_other_store.Checked) f.full.check_delivery_options = true;
            else f.full.check_delivery_options = false;
        }

        private void Correction_of_quantity_CheckedChanged(object sender, EventArgs e)
        {
            if (correction_of_quantity.Checked) f.full.get_min_sale = true;
            else f.full.get_min_sale = false;
        }

        private void Use_base_price_CheckedChanged(object sender, EventArgs e)
        {
            if (use_base_price.Checked) f.full.output_base_price = true;
            else f.full.output_base_price = false;
        }

        private void New_price_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void New_price_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name;
            Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
            f.full.file_to_create_new_price = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            new_price.Items.Clear();
            new_price.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string cf = File.ReadAllText(f.full.file_to_create_new_price, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f.full.coefficient.Clear();
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
                if (line[0] != "") f.full.coefficient.Add(coeff);
            }
            foreach (float[] str in f.full.coefficient)
                richTextBox1.Text = richTextBox1.Text + str[0] + ":" + str[1] + ":" + str[2] + "\r\n";
            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
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
            f.full.file_to_create_new_quality = file_name[0];
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            correction_quantity.Items.Clear();
            correction_quantity.Items.Add(name);

            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
            string cf = File.ReadAllText(f.full.file_to_create_new_quality, Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f.full.quality_correct.Clear();
            foreach (Match m in words)
            {
                line = sub_string.Split(m.Groups[1].Value);
                string[] quality = new string[2];
                if (line[0] != "")
                {
                    quality[0] = line[0];
                    quality[1] = line[1];
                }
                if (line[0] != "") f.full.quality_correct.Add(quality);
            }
            foreach (string[] str in f.full.quality_correct)
                richTextBox1.Text = richTextBox1.Text + str[0] + ":" + str[1] + "\r\n";
            //  ---------------------------------------- чтение исключающих данных ----------------------------------------
        }

        private void Data_in_csv_CheckedChanged(object sender, EventArgs e)
        {
            //f = (Form1)this.Owner;
            if (data_in_csv.Checked)
            {
                f.full.data_in_csv = true;
                use_xml_description.Enabled = true;
            }
            else
            {
                f.full.data_in_csv = false;
                use_xml_description.Enabled = false;
            }
        }

        private void Use_short_name_CheckedChanged(object sender, EventArgs e)
        {
            if (use_short_name.Checked)
            {
                f.full.use_short_name = true;
                add_articule_to_short_name.Enabled = true;
            }
            else
            {
                f.full.use_short_name = false;
                add_articule_to_short_name.Enabled = false;
            }
        }

        private void Add_articule_to_short_name_CheckedChanged(object sender, EventArgs e)
        {
            if (add_articule_to_short_name.Checked) f.full.add_articule_to_short_name = true;
            else f.full.add_articule_to_short_name = false;
        }

        private void Use_xml_description_CheckedChanged(object sender, EventArgs e)
        {
            if (use_xml_description.Checked) f.full.use_xml_description = true;
            else f.full.use_xml_description = false;
        }

        private void No_watermark_CheckedChanged(object sender, EventArgs e)
        {
            if (no_watermark.Checked) f.full.no_watermark = true;
            else f.full.no_watermark = false;
        }

        private void Transform_packing_size_CheckedChanged(object sender, EventArgs e)
        {
            if (transform_packing_size.Checked)
            {
                f.full.transform_packing_size = true;
                del_not_full_packing_size.Enabled = true;
            }
            else
            {
                f.full.transform_packing_size = false;
                del_not_full_packing_size.Enabled = false;
            }
        }

        private void Del_not_full_packing_size_CheckedChanged(object sender, EventArgs e)
        {
            if (del_not_full_packing_size.Checked) f.full.del_not_full_packing_size = true;
            else f.full.del_not_full_packing_size = false;
        }

        private void Type_of_package_Leave(object sender, EventArgs e)
        {
            f.full.type_of_package = type_of_package.Text;
        }

        private void Composition_of_package_Leave(object sender, EventArgs e)
        {
            f.full.composition_of_package = composition_of_package.Text;
        }

        private void Coefficient_package_mass_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Coefficient_package_mass_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            coefficient_package_mass.Items.Clear();
            coefficient_package_mass.Items.Add(Path.GetFileName(file_name[0]));
            f.full.file_coefficient_package_mass = file_name[0];

            //  ---------------------------------------------- чтение данных ----------------------------------------------
            string cf = null;
            try { cf = File.ReadAllText(file_name[0], Encoding.Default); }
            catch { MessageBox.Show("Не удалось загрузить файл"); }

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line; int i = 0;

            f.full.coefficient_package_mass.Clear();
            foreach (Match m in words)
            {
                if (i == 0) { i++; continue; }
                line = sub_string.Split(m.Groups[1].Value);
                cfg_data.coefficient_of_package info = new cfg_data.coefficient_of_package();
                if (line[0] != "")
                {
                    info.category_id = Convert.ToInt32(line[0]);
                    info.coefficient_of_massa = Convert.ToSingle(line[1]);
                    info.coefficient_of_dimensions = Convert.ToSingle(line[2]);
                }
                if (line[0] != "") f.full.coefficient_package_mass.Add(info);
            }
            foreach (var str in f.full.coefficient_package_mass)
                richTextBox1.Text = richTextBox1.Text + " id: " + str.category_id + "\t\tмасса: " + str.coefficient_of_massa + 
                    "\tгабариты: " + str.coefficient_of_dimensions + "\r\n";
            //  ---------------------------------------------- чтение данных ----------------------------------------------
        }

        private void Gred_CheckedChanged(object sender, EventArgs e)
        {
            if (gred.Checked) f.full.gred = true;
            else f.full.gred = false;
        }

        private void Color_YML_CheckedChanged(object sender, EventArgs e)
        {
            if (color_YML.Checked) f.full.color_YML = true;
            else f.full.color_YML = false;
        }

        private void Color_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Gred_file_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Color_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name = Path.GetFileName(file_name[0]);
            f.full.file_colors = file_name[0];

            color.Items.Clear();
            color.Items.Add(name);

            //  ---------------------------------------- чтение данных ----------------------------------------
            string cf = File.ReadAllText(file_name[0], Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f.full.color.Clear();
            int i = 0;
            foreach (Match m in words)
            {
                if (i == 0) { i++; continue; }

                line = sub_string.Split(m.Groups[1].Value);
                string[] color = new string[2];
                if (line[0] != "")
                {
                    color[0] = line[0];
                    color[1] = line[1];
                }
                if (line[0] != "") f.full.color.Add(color);
            }
            foreach (string[] str in f.full.color)
                richTextBox1.Text = richTextBox1.Text + str[0] + " : " + str[1] + "\r\n";
            //  ---------------------------------------- чтение данных ----------------------------------------
        }

        private void Gred_file_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "";
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name = Path.GetFileName(file_name[0]);
            f.full.gred_file = file_name[0];

            gred_file.Items.Clear();
            gred_file.Items.Add(name);

            //  ---------------------------------------- чтение данных ----------------------------------------
            string cf = File.ReadAllText(file_name[0], Encoding.Default);

            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(cf);
            Regex sub_string = new Regex(";");
            string[] line;

            f.full.gred_list.Clear();
            f.full.gls = "";
            int i = 0;
            foreach (Match m in words)
            {
                if (i == 0) { i++; continue; }

                line = sub_string.Split(m.Groups[1].Value);
                f.full.gls += m.Groups[0].Value;
                string[] gred = new string[2];
                List<string> gred_list = new List<string>();
                if (line[0] != "")
                {
                    gred[0] = line[0].Trim();
                    gred[1] = line[1].Trim();
                    MatchCollection matchs = Regex.Matches(line[2].Replace(" ", ""), "\\d+");
                    foreach (var item in matchs)
                        gred_list.Add(item.ToString());

                }
                if (line[0] != "") f.full.gred_list.Add(gred, gred_list);
            }
            string[] s = new string[2], s3;
            foreach (var str in f.full.gred_list)
            {
                s[0] = str.Key.GetValue(0).ToString();
                s[1] = str.Key.GetValue(1).ToString();
                richTextBox1.Text = richTextBox1.Text + s[0] + " : " + s[1] + " ";
                s3 = str.Value.ToArray();
                foreach(string stl in s3)
                    richTextBox1.Text = richTextBox1.Text + ", " + stl;
                richTextBox1.Text += "\r\n";
            }
            //  ---------------------------------------- чтение данных ----------------------------------------
        }

        private void Color_from_YML_CheckedChanged(object sender, EventArgs e)
        {
            if (color_from_YML.Checked) f.full.color_from_YML = true;
            else f.full.color_from_YML = false;
        }


        private void Clear_Click(object sender, EventArgs e)
        {
            foreach (Control control in f.set_full.Controls)
            {
                f.ctrl(control);
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control item in gb.Controls)
                        f.ctrl(item);
                }
            }

            f.f.clear_configure("full");
        }
    }
}
