using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace up
{
    [Serializable]
    public class configure
    {
        public List<float[]> coefficient = new List<float[]>();
        public List<string[]> quality_correct = new List<string[]>();
        public List<string> exception_name = new List<string>();
        public List<string[]> color = new List<string[]>();


        [NonSerialized] public Dictionary<string[], List<string>> gred_list = new Dictionary<string[], List<string>>();
        public string gls;

        public void load_gred()
        {
            if (gls == null) return;
            Regex get_line = new Regex("(.*)\r\n");
            MatchCollection words = get_line.Matches(gls);
            Regex sub_string = new Regex(";");
            string[] line;

            foreach (Match m in words)
            {
                line = sub_string.Split(m.Groups[1].Value);
                string[] gred = new string[2];
                List<string> gred_l = new List<string>();
                if (line[0] != "")
                {
                    gred[0] = line[0].Trim();
                    gred[1] = line[1].Trim();
                    MatchCollection matchs = Regex.Matches(line[2].Replace(" ", ""), "\\d+");
                    foreach (var item in matchs)
                        gred_l.Add(item.ToString());

                }
                if (line[0] != "") gred_list.Add(gred, gred_l);
            }
        }

        // sheduller
        [NonSerialized] public TextBox[] time_sh = new TextBox[2];
        [NonSerialized] public Label[] days = new Label[7];

        public uint exception_any_side = 0, exception_sum_side = 0, exception_weight = 0;

        public string mode;
        public string prefix_for_id = "";
        public string file_to_create_new_price;         // файл с данными для формирования розничных цен
        public string file_to_create_new_quality;       // файл с корректировкой количества прописью в цифры
        public string exception_rules_xml;              // файл с исключающими данными
        public string gred_file;                        // файл с размерными сетками
        public string file_coefficient_package_mass;    // файл с коэффициентами габаритов и массы
        public string file_colors;                      // фаил с заменой цветов

        // Коэффициенты для габаритов и массы
        public List<cfg_data.coefficient_of_package>
            coefficient_package_mass = new List<cfg_data.coefficient_of_package>();
        public string type_of_package;              // Тип упаковки, DELIVERY_PACKAGE_TYPE
        public string composition_of_package;           // Состав упаковки, DELIVERY_PACKAGE

        public bool color_YML;
        public bool color_from_YML;                     // получение цвета из имени
        public bool gred;
        public bool data_in_csv;                        // приоритет данных из дополнительного csv файла
        public bool use_short_name;
        public bool add_articule_to_short_name;
        public bool check_delivery_options;             // исключит товар с других складов
        public bool get_min_sale;                       // умножать стоимость на мин. кол-во
        public bool output_base_price;                  // вывод базовой цены в результирующий файл
                                                        // public bool quantity_change;                 // замена слов на цифры
        public bool no_watermark;
        public bool use_xml_description;                // вкл - описание берется из xml и csv, выкл - только csv
                                                        //public bool use_xml_color;                    // отключенна
        public bool transform_packing_size;             // Изменение данных упаковки из xml, корректировка не полных габаритов
        public bool del_not_full_packing_size;          // Удаление не полных габаритов

        //public bool date_of_deactivation;
        //public int duration_of_deactivation;

        public bool ya = false;

        public configure() { }
        public configure(string s) { mode = s; }
        public configure(string mode_local, Form1 f)
        {
            int x = 198, y = 107, x_t = 0, a_side = 35;
            string[] days_txt = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };

            mode = mode_local;
            if (mode == "full")
                y += 31;
            if (mode == "options")
                y += 62;

            for (int i = 0; i < 2; i++)
            {
                time_sh[i] = new TextBox();
                if (i == 0)
                    time_sh[i].Text = "0";
                else
                    time_sh[i].Text = "00";
                time_sh[i].TextAlign = HorizontalAlignment.Center;
                time_sh[i].Size = new Size(a_side, 20);
                time_sh[i].Location = new Point(x + x_t, y); x_t += a_side;

                f.Controls.Add(time_sh[i]);
            }
            x_t = 0;

            for (int i = 0; i < 7; i++)
            {
                days[i] = new Label();
                days[i].Tag = 0;
                days[i].Text = days_txt[i];
                days[i].Size = new Size(23, 15);
                days[i].Location = new Point(x + 7 + a_side * 2 + x_t, y + 2); x_t += 29;
                days[i].TextAlign = ContentAlignment.MiddleCenter;
                days[i].BorderStyle = BorderStyle.FixedSingle;
                //days[i].

                days[i].ForeColor = Color.FromArgb(45, 45, 45);
                days[i].BackColor = Color.FromArgb(255, 224, 192);

                days[i].Click += (a, e) =>
                {
                    Label l = (Label)a;

                    if ((int)l.Tag == 0)
                    {
                        l.BackColor = Color.Yellow;
                        //l.BackColor = Color.LawnGreen;
                        l.BorderStyle = BorderStyle.Fixed3D;
                        l.Tag = 1;
                    }
                    else
                    {
                        l.BackColor = Color.FromArgb(255, 224, 192);
                        l.BorderStyle = BorderStyle.FixedSingle;
                        //l.BorderStyle = BorderStyle.None;
                        l.Tag = 0;
                    }
                };


                //days[i].Click += (a, e) => iClick(a, e, (int)days[i].Tag);

                //void iClick(object e, EventArgs ik, int num)
                //{
                //    days[num].BackColor = Color.Cyan;
                //};

                f.Controls.Add(days[i]);
            }
        }
    }
}
