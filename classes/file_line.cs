using CsvHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace up
{
    //[Serializable]
    public partial class file_line
    {
        public List<TextBox> line_xml_file;
        public List<TextBox> line_csv_folder;
        public List<Options_up> options_csv;
        public List<TextBox> line_csv_save_file;
        public List<CheckBox> line_old_itms_remove;
        public List<TextBox> line_data_Live;
        public List<ListBox> line_modification_categories_file;
        public List<string> line_modification_categories;
        Button bt;
        int line_count, line_step = 30, x_start = 12;

        public file_line(Form1 f)
        {
            line_xml_file = new List<TextBox>();
            line_csv_folder = new List<TextBox>();
            options_csv = new List<Options_up>();
            line_csv_save_file = new List<TextBox>();
            line_old_itms_remove = new List<CheckBox>();
            line_data_Live = new List<TextBox>();
            line_modification_categories_file = new List<ListBox>();
            line_modification_categories = new List<string>();
            line_count = 0;

            bt = new Button();
            bt.Location = new Point(x_start, 273);
            bt.Text = "Добавить";
            bt.Click += (a, e) =>
            {
                add_line(f);
            };
            f.Controls.Add(bt);

            add_line(f);
            add_line(f);
            add_line(f);

        }
        public void add_line(Form1 f)
        {
            TextBox[] tb_main = new TextBox[3];
            int x = x_start, y = 275 + line_count * line_step, i = 0;

            for (int index = 0; index < 3; index++) tb_main[index] = new TextBox();

            foreach (TextBox tb_i in tb_main)
            {
                tb_i.Location = new Point(x, y); x += 220; // y = 273
                tb_i.Size = new Size(216, 20);
                tb_i.AllowDrop = true;

                tb_i.DragDrop += (a, e) =>
                {
                    string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    tb_i.Text = file_name[0];
                    //f.global_xml = file_name[0];
                };

                tb_i.Leave += (a, e) =>
                {
                    tb_i.Tag = tb_i.Text;
                };


                tb_i.DragEnter += (a, e) =>
                {
                    e.Effect = DragDropEffects.All;
                };

                if (i == 0)
                    line_xml_file.Add(tb_i);
                else if (i == 1)
                    line_csv_folder.Add(tb_i);
                else
                    line_csv_save_file.Add(tb_i);
                i++;

                f.Controls.Add(tb_i);
            }

            CheckBox cb = new CheckBox();
            TextBox tb = new TextBox();


            x = 725; i = 0;
            for (int index = 0; index < 2; index++)
            {
                if (i == 0)
                {
                    cb.Location = new Point(x, y + 2); x += 75; // y = 277
                    cb.Size = new Size(14, 14);
                    line_old_itms_remove.Add(cb);
                }
                else
                {
                    tb.Location = new Point(x, y);    // y = 273
                    tb.Size = new Size(27, 20);
                    line_data_Live.Add(tb);
                }

                f.Controls.Add(cb);
                f.Controls.Add(tb);
                i++;
            }

            ListBox lb = new ListBox();
            lb.Location = new Point(867, y);      // y = 265
            lb.Size = new Size(153, 27);
            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.AllowDrop = true;
            lb.Tag = "";

            lb.DragEnter += (a, e) => { e.Effect = DragDropEffects.All; };
            lb.DragDrop += (a, e) => {
                string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string name;
                Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
                Match rx_short_name = short_name.Match(file_name[0]);
                name = rx_short_name.Groups[3].Value;
                lb.Items.Clear();
                lb.Items.Add(name);
                lb.Tag = file_name[0];
                //line_modification_categories.Add(file_name[0]);
            };

            line_modification_categories_file.Add(lb);
            f.Controls.Add(lb);

            bt.Location = new Point(x_start, Convert.ToInt32(bt.Location.Y.ToString()) + line_step);
            f.Height += line_step;
            line_count++;
        }
        public List<Options_up> take_options_csv(int index, Form1 f)
        {
            string name_xml = Path.GetFileNameWithoutExtension(line_xml_file[index].Text);
            string name_csv = name_xml + ".csv";
            functions fn = new functions(f);

            string tmp = @"tmp\l";
            if (f.full.ya)
            {
                DirectoryInfo di = new DirectoryInfo(tmp);
                if (!di.Exists)
                    di = Directory.CreateDirectory(tmp);
                // /csv//xml.csv
                if (Regex.IsMatch(line_csv_folder[index].Text, @"/$"))
                    name_csv = line_csv_folder[index].Text + name_csv;
                else
                    name_csv = line_csv_folder[index].Text + "/" + name_csv;

                bool save = false; ;
                if (line_csv_folder[index].Text != "")
                    save = fn.ya_dl(name_csv, tmp);
                if (!save && line_csv_folder[index].Text != "")
                    MessageBox.Show("Проверьте параметры аутентефикации и соответсвие ссылок для яндекс диска");
            }
            else
            {
                if (line_csv_folder[index].Text == "")
                    name_csv = Path.GetDirectoryName(line_xml_file[index].Text) + "\\" + name_csv;
                else
                    name_csv = line_csv_folder[index].Text.ToString() + "\\" + name_csv;
                // name_csv = line_csv_folder[index].Text.ToString();
            }

            try
            {
                string r;
                if (f.full.ya)
                    r = tmp + @"\" + Path.GetFileName(name_csv);
                else
                    r = name_csv;

                using (var reader = new StreamReader(r, Encoding.GetEncoding(1251)))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.HeaderValidated = null;
                    csv.Configuration.MissingFieldFound = null;
                    csv.Configuration.Encoding = Encoding.GetEncoding(1251);
                    var info = new List<string>();
                    csv.Configuration.BadDataFound = data => {
                        info.Add(data.RawRecord);
                    };

                    var l = csv.GetRecords<Options_up>();
                    options_csv = l.ToList();
                }

                List<int> offer_ids = new List<int>();
                string xml = "";

                if (f.full.ya)
                {
                    try { xml = File.ReadAllText(@"tmp\" + Path.GetFileName(f.line.line_xml_file[index].Text), Encoding.UTF8); }
                    catch { MessageBox.Show("Ошибка чтения xml файла с яндекс диска, проверьте аутентификационные и указывающие путь к файлу данные"); }
                }
                else
                {
                    try { xml = File.ReadAllText(f.line.line_xml_file[index].Text.ToString(), Encoding.UTF8); }
                    catch
                    {
                        try
                        {
                            xml = new WebClient().DownloadString(f.line.line_xml_file[index].Text.ToString());
                        }
                        catch { MessageBox.Show("Ошибка чтения xml файла по url"); }
                        //MessageBox.Show("Ошибка чтения xml файла");
                    }
                }
                offer_ids = f.offer_get_id(new StringReader(xml)).ToList();

                //DateTimeOffset dto = DateTimeOffset.Now; long start = dto.ToUnixTimeSeconds();

                //foreach (Options_up item in options_csv)
                //{
                //    Regex id_tmpl = new Regex(@"(\d*)$");
                //    Match id_m = id_tmpl.Match(item.ID);
                //    item.id = Convert.ToInt32(id_m.Groups[0].Value);
                //}

                List<Options_up> clear_csv = new List<Options_up>();

                clear_csv = options_csv.FindAll((op) =>
                {
                    foreach (int item in offer_ids)
                    {
                        if (Convert.ToInt32(op.ARTNUMBER) == item)
                            return true;
                    }
                    return false;
                }
                );

                return clear_csv;
            }
            catch/*(BadDataException i)*/
            {
                //MessageBox.Show(i.ToString());
                return null;
            }
        }
    }
}
