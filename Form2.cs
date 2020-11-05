﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            f1.exception_name.Clear();
            foreach (Match m in words)
            {
                i++;
                if (i == 0) continue;

                line = sub_string.Split(m.Groups[1].Value);
                if (i == 1)
                    if (line[1] != "")
                    {
                        f1.exception_any_side = Convert.ToUInt32(line[1]);
                        f1.exception_sum_side = Convert.ToUInt32(line[2]);
                        f1.exception_weight = Convert.ToUInt32(line[3]);
                    }
                if (line[0] != "") f1.exception_name.Add(line[0]);
            }
            richTextBox1.Text = "";
            foreach (string str in f1.exception_name)
                richTextBox1.Text = richTextBox1.Text + "\r\n " + str;

            //----------------------------------------чтение исключающих данных ----------------------------------------
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner;
        }

        private void Prefix_for_id_Leave(object sender, EventArgs e)
        {
            f1.easy.prefix_for_id = f1.full.prefix_for_id = prefix_for_id.Text;
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
            if (correction_of_quantity.Checked) f1.easy.quantity_change = true;
            else f1.easy.quantity_change = false;
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
                richTextBox1.Text = richTextBox1.Text + str[0] + ":" + str[1] + ":" + str[2] + "\r\n";
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
    }
}
