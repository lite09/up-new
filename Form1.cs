using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using up;
using stl;
using System.Security.Cryptography;

namespace up
{

    public partial class Form1 : Form
    {
        List<Thread> th_s = new List<Thread>();

        public Form_stl stl = new Form_stl();
        public Form2 set_easy = new Form2();
        public Form3 set_full = new Form3();
        public Ssh_form ssh_form = new Ssh_form();

        public functions f;

        public configure easy;
        public configure full;
        public configure conf_options;
        
        public file_line line;

        public Ssh ssh_conf = new Ssh();

        public string token = null;
        readonly int CPU = Environment.ProcessorCount;
        public int threads = Environment.ProcessorCount;
        int threads_avariable, threads_all;

        public static List<string> prepositions = new List<string> {                            // список предлогов для обрывки фразы
            "A"/*латиница*/, "А"
        };
        public static List<string> stop_words = new List<string>                                // список стоп слов для обрывки фразы
        {
            "d\\s*=", "h\\s*=", "r\\s*=", "А\\.", "№", "SchE",
            "ш\\."
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (easy.exception_rules_xml == null)
            {
                MessageBox.Show("Установите поле фильтр исключений");
                return;
            }

            richTextBox2.Text = "";
            multi_offer(threads, "easy");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (full.exception_rules_xml == null)
            {
                MessageBox.Show("Установите поле фильтр исключений");
                return;
            }

            richTextBox2.Text = "";
            multi_offer(threads, "full");
        }

        public void multi_offer(int threads, string type)
        {
            //th_s.Clear();
            var th = ThreadPool.SetMaxThreads(threads, threads);

            for(int i = 0; i < line.line_xml_file.Count; i++)
            {
                Thread th_time = new Thread(new ParameterizedThreadStart(thead_offer));
                th_s.Add(th_time);
                object[] hi = { i, type };
                ThreadPool.QueueUserWorkItem(thead_offer, hi);
            }
        }


        // Добавить локацию файла csv, line_info)
        public void thead_offer(object info)
        {
            List<Options_up> options_csv = new List<Options_up>();
            object[] inf = info as object[];
            int int_index = Convert.ToInt32(inf[0]);
            string type = Convert.ToString(inf[1]);

            bool ya = false;

            //if (type == "full" && full.duration_of_deactivation == 0)
            //    full.duration_of_deactivation = 10;
            //else if (type == "easy" && easy.duration_of_deactivation == 0)
            //    easy.duration_of_deactivation = 10;

            string xml_name = "";
            try   { xml_name = line.line_xml_file[int_index].Text; }
            catch {}
            if (xml_name == "") return;

            options_csv = null;
            if (type == "full")
                ya = full.ya;
            else
                ya = easy.ya;

            //using (var writer = new StreamWriter("C:\\Users\\и\\Desktop\\i.csv", false, Encoding.GetEncoding(1251)))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //    csv.Configuration.Delimiter = ";";
            //    csv.WriteRecords(options_csv);
            //}

            string xml = "";

            if (ya)
            {
                string tmp = "tmp";
                string name = Path.GetFileName(line.line_xml_file[int_index].Text);

                DirectoryInfo di = new DirectoryInfo(tmp);
                    //di.Delete(true);
                if(!di.Exists)
                    di = Directory.CreateDirectory(tmp);

                bool save_file = f.ya_dl(line.line_xml_file[int_index].Text.ToString(), tmp);
                
                Thread.Sleep(90);

                if (save_file)
                {
                    try { xml = File.ReadAllText(tmp + "\\" + name, Encoding.UTF8); }
                    catch { MessageBox.Show("Ошибка чтения xml файла скаченного с яндекс диска"); }
                }
                else { MessageBox.Show("Не удалось загрузить xml файл с яндекс диска."); }
            }
            else
            {
                string file_xml = line.line_xml_file[int_index].Text.ToString();
                try { xml = File.ReadAllText(file_xml, Encoding.UTF8); }
                catch
                {
                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        WebClient wc = new WebClient();
                        wc.Encoding = Encoding.UTF8;
                        xml = wc.DownloadString(new Uri(file_xml));

                        //File.WriteAllText(@"C:\Users\и\Desktop\iso.xml", xml);

                        //using (var response = wr.GetResponse())
                        //using (var content = response.GetResponseStream())
                        //using (var reader = new StreamReader(content))
                        //{
                        //    var strContent = reader.ReadToEnd();
                        //}
                    }
                    catch { MessageBox.Show("Ошибка чтения xml файла по url"); }
                    //MessageBox.Show("Ошибка чтения xml файла");
                }
            }

            if (type == "full") options_csv = line.take_options_csv(int_index, this);

            //try { string str = line.line_modification_categories[int_index].ToString(); }
            //catch { line.line_modification_categories.Add(""); }


            string[] line_info = { line.line_xml_file[int_index].Text.ToString(), line.line_csv_save_file[int_index].Text.ToString(),
                                    line.line_old_itms_remove[int_index].Checked.ToString(), line.line_data_Live[int_index].Text.ToString(),
                                    line.line_modification_categories_file[int_index].Tag.ToString(), line.line_csv_folder[int_index].Text.ToString() };


            IEnumerable<xml_offer> param = null;
            if (xml != "")
            {
                string ids_file = easy.get_ids_dir + "\\" + Path.GetFileNameWithoutExtension(line.line_xml_file[int_index].Text.ToString()) + ".txt";
                if(type == "easy")
                    param = offer_min(new StringReader(xml), easy, ids_file);
                else
                    param = offer(new StringReader(xml), full, options_csv);


                DateTimeOffset dto = DateTimeOffset.Now; long start = dto.ToUnixTimeSeconds();

                List<xml_offer> p = param.ToList();
                dto = DateTimeOffset.Now; long endl = dto.ToUnixTimeSeconds();

                object am = new object();
/*                lock (am)
                {
                    richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += Path.GetFileName(line.line_xml_file[int_index].Text.ToString()) + "\tcount:  " + p.Count + "\t" +
                    "время:  " + (int)((endl - start) / 60) + ":" + (endl - start) % 60 + "\r\n"));
                }*/
                param = null;

                xml = null;


                if(type == "easy")
                    processing(ref p, easy, line_info);
                else
                    processing(ref p, full, line_info, options_csv);


                dto = DateTimeOffset.Now; long end = dto.ToUnixTimeSeconds();

                lock (am)
                {
                    richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += Path.GetFileName(line.line_xml_file[int_index].Text.ToString()) + "\tcount:  " + p.Count + "\t" +
                    "время:  " + (int)((end - start) / 60) + ":" + (end - start) % 60 + "\r\n"));
                }

                p.Clear();
                //i = 0;
            }
        }

        public void tre_thread_offer(object info)
        {
            functions fn = new functions();
            List<Options_up> options_csv = new List<Options_up>();

            // хмл, фаил с опциями, режим
            object[] inf = info as object[];
            string file_xml     = Convert.ToString(inf[0]);
            string file_options = Convert.ToString(inf[1]);
            string type         = Convert.ToString(inf[2]);
            string file_ids     = Path.GetDirectoryName(file_xml) + "\\txt_simple\\id.txt";
            string xml          = "";
            // line_info  = new string[9];

            string save_folder = full.tre_folder + "\\Result";

            if (file_xml == "") return;
            
            if (type == "full") options_csv = fn.take_options(file_options);

            // xml, папка для сохранения результирующего фаила, удаление старых товаров(checkbox), сроки деактивации, соотнесение категории, папка с фаилами описания дополнительных полеи
            string folder_options = "";
            try   { folder_options = Path.GetDirectoryName(file_options); }
            catch {}
            string[] line_info;
            if (type == "full") {
                string[] line_info_time = { file_xml, save_folder, full.tre_del_old_itm_bool.ToString(), full.tre_del_old_itm_count, "", folder_options };
                line_info = line_info_time;
            }
            else
            {
                string[] line_info_time = { file_xml, save_folder, easy.tre_del_old_itm_bool.ToString(), easy.tre_del_old_itm_count, "", folder_options };
                line_info = line_info_time;
            }

            IEnumerable<xml_offer> param = null;
            if (file_xml != "")
            {
                xml = File.ReadAllText(file_xml);
                string url;
                if (xml.Length < 900)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    url = fn.get_url_in_file(file_xml);
                    WebClient wc = new WebClient();
                    wc.Encoding = Encoding.UTF8;
                    xml = wc.DownloadString(url);
                }
                if (type == "easy")
                    param = offer_min(new StringReader(xml), easy, file_ids);
                else
                    param = offer(new StringReader(xml), full, options_csv);


                DateTimeOffset dto = DateTimeOffset.Now; long start = dto.ToUnixTimeSeconds();

                List<xml_offer> p = param.ToList();
                 param = null;
                object am = new object();

                //file_xml = null;
                string save_urls_dir = Path.GetDirectoryName(file_xml) + "\\txt_simple";

                if (type == "easy")
                    processing(ref p, easy, line_info);
                else
                    processing(ref p, full, line_info, options_csv, save_urls_dir);


                dto = DateTimeOffset.Now; long end = dto.ToUnixTimeSeconds();

                lock (am)
                {
                    richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += Path.GetFileName(file_xml) + "\tcount:  " + p.Count + "\t" +
                    "время:  " + (int)((end - start) / 60) + ":" + (end - start) % 60 + "\r\n"));
                }

                p.Clear();
                //i = 0;
            }
        }

        bool mod_cat(ref List<string[]> mod_cat, xml_offer offer)
        {
            foreach (string[] cat in mod_cat)
            {
                if (offer.categoryId == cat[0])
                {
                    offer.categoryId = cat[1];
                    return true;
                }
            }
            return false;
        }


        IEnumerable<xml_offer> offer_min(StringReader string_xml, configure mode, string file_ids = "")
        {
            if (mode.mode != "easy") yield return null;

            functions fn = new functions();
            string[] ids = null;

            if (file_ids != "")
                ids = fn.file_get_ids(file_ids);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(string_xml, settings))
            {

                try     { reader.MoveToContent(); }
                catch   { MessageBox.Show("Не верный формат файла хмл"); }
            // Parse the file and display each of the nodes. 
            
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "offer")
                            {
                                xml_offer offer = new xml_offer();
                                XElement el = XNode.ReadFrom(reader) as XElement;
                                float a, b, c;

                                IEnumerable<XElement> i;

                                offer.id = offer.id_with_prefix = el.Attribute("id").Value;



                                //  --------------------------- фильтр исключений -------------------------------------
                                //if (offer.id == "118165")
                                //{
                                //}

                                if (ids != null)
                                {
                                    if (ids.Where(l => l == offer.id).FirstOrDefault() == null)
                                        continue;
                                }
                                else
                                {
                                    offer.name = el.Element("name").Value;

                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер упаковки");
                                    offer.packing_size = (string)i.FirstOrDefault();

                                    if (offer.packing_size == null)
                                        continue;

                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Вес");
                                    offer.weight = (string)i.FirstOrDefault();

                                    //  ---------------------------       delay       -------------------------------------
                                    if (mode.check_delivery_options)
                                    {
                                        try
                                        {
                                            offer.delivery_days = Convert.ToInt32(el.Element("delivery-options").Element("option").Attribute("days").Value);
                                            if (offer.delivery_days > 0)
                                                continue;
                                        }
                                        catch {}
                                    }
                                    //  --------------------------- фильтр исключений -------------------------------------

                                    //  --------------------------- фильтр исключений -------------------------------------
                                    //  ---------------------------      упаковка     -------------------------------------
                                    //15 см × 2 см × 0,3 см
                                    Regex side_of_pkg = new Regex(@"^(\S+)\s*см\s*\S\s*(\S+)\s*см\s*\S\s*(\S+)");
                                    Match rx_short_name = side_of_pkg.Match(offer.packing_size);

                                    // или continue
                                    if (rx_short_name.ToString() == "")
                                        continue;

                                    a = Convert.ToSingle(rx_short_name.Groups[1].Value);
                                    b = Convert.ToSingle(rx_short_name.Groups[2].Value);
                                    c = Convert.ToSingle(rx_short_name.Groups[3].Value);


                                    if ((a >= mode.exception_any_side) || (b >= mode.exception_any_side) || (c >= mode.exception_any_side))
                                        continue;
                                    if ((a + b + c) >= mode.exception_sum_side)
                                        continue;
                                    //  --------------------------- фильтр исключений -------------------------------------


                                    //  --------------------------- фильтр исключений -------------------------------------
                                    //  ---------------------------       масса       -------------------------------------
                                    if (Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture) >= mode.exception_weight)
                                        continue;
                                    //  --------------------------- фильтр исключений -------------------------------------


                                    //  --------------------------- фильтр исключений -------------------------------------
                                    //  ---------------------------        имя        -------------------------------------
                                    string exp = "";
                                    foreach (string name in mode.exception_name)
                                    {
                                        exp = @"^.*(" + name + ").*";
                                        Regex exp_name = new Regex(exp);
                                        //Match mi = exp_name.Match(offer.name);

                                        if (exp_name.IsMatch(offer.name))
                                        {
                                            //richTextBox1.Text = richTextBox1.Text + "\r\n" + offer.name;
                                            //richTextBox2.Text = richTextBox2.Text + "\r\n" + name;
                                            //richTextBox2.Text = richTextBox2.Text + "\r\n" + mi.Groups[0];
                                            goto end;
                                        }
                                    }
                                }
                                //  --------------------------- фильтр исключений -------------------------------------


                                //offer.categoryId = el.Element("categoryId").Value;
                                offer.available = el.Attribute("available").Value;
                                offer.price = offer.price_time = Convert.ToSingle(el.Element("price").Value, CultureInfo.InvariantCulture);

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "На складе");
                                offer.in_stock = (string)i.FirstOrDefault();

                                // Предоплата. Мин. партия 1 шт
                                offer.sales_notes = el.Element("sales_notes").Value;

                                if (el != null)
                                    yield return offer;
                            }

                            break;
                    }
                    end:;
                }
            }
        }

        void processing(ref List<xml_offer> offers, configure mode, string[] line_info, List<Options_up> options = null, string ids_easy_dir = "")
        {

            bool categories = true;
            float coeff = 1;
            string line_csv = null, date = null;
            string line_head;

            if (line_info[3] == "")
                line_info[3] = "10";

            if (mode.mode == "easy")
                line_head = "ID;AVAILABLE;QUANTITY;MIN_QUANTITY";
            else
                line_head = "ID;Торговые предложения;ARTNUMBER;FULL_NAME;proizvoditel;MATERIAL;ID_CATEGORY;AVAILABLE;QUANTITY;MIN_QUANTITY;WEIGHT_V;WEIGHT;PRODUCT_COLOR;SIZE";
            StringBuilder sb = new StringBuilder();


            //  ------------------------- получение списка категорий -------------------------
            List<string[]> mod_catalog = new List<string[]>();
            try
            {
                string catalogs = File.ReadAllText(line_info[4], Encoding.Default);

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
                    }
                    if (line[0] != "") mod_catalog.Add(cats);
                }
                mod_catalog.RemoveAt(0);
                categories = true;
            }
            catch { categories = false; }
            //  ------------------------- получение списка категорий -------------------------

            if (mode.mode == "easy")
            {
                if (easy.output_base_price)
                    line_head += ";BASE_PRICE;PRICE";
                else
                    line_head += ";PRICE";
            }
            else
            {
                if (full.output_base_price)
                    line_head += ";BASE_PRICE;PRICE;NAME;DESCRIPTION;DOP_IMG_1;DOP_IMG_2;DOP_IMG_3;DOP_IMG_4;DOP_IMG_5;LENGTH_PACK;WIDTH_PACK;HEIGHT_PACK;PACKAGE_SIZE;DELIVERY_PACKAGE_TYPE;DELIVERY_PACKAGE;SIZES_TABLE_LINK";
                else
                    line_head += ";PRICE;NAME;DESCRIPTION;DOP_IMG_1;DOP_IMG_2;DOP_IMG_3;DOP_IMG_4;DOP_IMG_5;LENGTH_PACK;WIDTH_PACK;HEIGHT_PACK;PACKAGE_SIZE;DELIVERY_PACKAGE_TYPE;DELIVERY_PACKAGE;SIZES_TABLE_LINK";
            }

            // дата
            if (line_info[2] == "True")
            {
                line_head += ";DATE_OF_CREATION;DEACTIVATION_PERIOD";
                date = DateTime.Now.ToString("dd/MM/yyyy");
            }

            // ------------------------------ добавление полей заголовка из дополнительного фаила ------------------------------
            List<string> op_head_list = new List<string>();
            if (line_info[5] != "" && mode.mode == "full")
            {
                string op_head = "";
                try
                 { op_head = File.ReadAllText(line_info[5] + "\\" + Path.GetFileNameWithoutExtension(line_info[0]) + ".csv", Encoding.Default); }
                catch { MessageBox.Show("Фаил с описанием дополнительных полей не наиден"); }

                if (op_head != "" || op_head != null)
                {
                    Regex line = new Regex("(.*)\r\n");
                    string head_time = line.Match(op_head).Groups[1].Value;

                    string[] tmp_head_xml = line_head.Split(';');
                    string[] tmp_head_op  = head_time.ToString().Split(';');

                    //  получение полей заголовка фаила описания (доп фаила)

                    //op_head = op_head.Remove(op_head.Length - 3, 3);
                    var l = tmp_head_op.Except(tmp_head_xml);
                    op_head_list = l.ToList();
                    //  получение полей заголовка фаила описания (доп фаила)
                    op_head = "";
                    foreach (string item in l)
                        op_head += item + ";";

                    line_head += ";" + op_head.Remove(op_head.Length - 1, 1);
                }
            }
            // ------------------------------ добавление полей заголовка из дополнительного фаила ------------------------------

            sb.AppendLine(line_head);

            foreach (xml_offer offer in offers)
            {
                if ((offer.id_with_prefix == offer.id) && mode.prefix_for_id != "") {
                    offer.id_with_prefix = mode.prefix_for_id + offer.id;
                } 


                //  ------------------------------  Получение цвета ------------------------------
                if (mode.color_from_YML)
                {
                    if (offer.product_color == "" || offer.product_color == null)
                        offer.product_color = f.color_from_name(offer.name);
                }
                //  ------------------------------  Получение цвета ------------------------------


                //  ------------------------------  Изменение цвета ------------------------------
                if (mode.color_YML)
                {
                    foreach (var col in mode.color)
                    {
                        if (offer.product_color == col[0])
                        {
                            offer.product_color = col[1];

                            break;
                        }
                    }
                }
                //  ------------------------------  Изменение цвета ------------------------------

                //  -----------------------------------  gred ------------------------------------
                if (mode.gred)
                {
                    foreach (var item in mode.gred_list)
                    {

                        if (offer.vendor.ToLower() == item.Key.GetValue(0).ToString().ToLower())
                        {
                            if (item.Value.Count == 0)
                            {
                                offer.gred = item.Key.GetValue(1).ToString();
                            }
                            else
                            {
                                foreach (var i in item.Value.ToArray())
                                {
                                    if (offer.categoryId == i)
                                    {
                                        offer.gred = item.Key.GetValue(1).ToString();

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                //  -----------------------------------  gred ------------------------------------


                //  -----------------------------  Сокращение имени  -----------------------------
                if (mode.use_short_name)
                {
                    offer.short_name = offer.name_short(offer.name);
                    if (mode.add_articule_to_short_name) offer.short_name += ", арт. " + offer.id;
                }
                //  -----------------------------  Сокращение имени  -----------------------------

                //  ----------------------------  Сокращение состава  ----------------------------
                offer.composition = f.clear_Composition(offer.composition);
                //  ----------------------------  Сокращение состава  ----------------------------

                //  --------------------------  минимальное количество  --------------------------
                if (mode.get_min_sale)
                {
                    Regex num = new Regex(@"\S+\s+(\d+)\s*шт");
                    Match num_m = num.Match(offer.sales_notes);
                    if (num_m.Success)
                    {
                        double num_int = Convert.ToDouble(num_m.Groups[1].Value);
                        if (num_int > 1)
                        {
                            offer.min_quantity = (int)num_int;
                            offer.price_time = offer.price * (float)num_int;
                            offer.a *= Convert.ToSingle(Math.Pow(num_int, 1/3f)); offer.a = Convert.ToSingle(Math.Round(offer.a, 2));
                            offer.b *= Convert.ToSingle(Math.Pow(num_int, 1/3f)); offer.b = Convert.ToSingle(Math.Round(offer.b, 2));
                            offer.c *= Convert.ToSingle(Math.Pow(num_int, 1/3f)); offer.c = Convert.ToSingle(Math.Round(offer.c, 2));
                            if (mode.use_short_name)
                                offer.short_name += ", " + num_int + " шт. в комплекте";
                        }
                    }
                }
                //  --------------------------  минимальное количество  --------------------------
                
                
                //  --------------------------     добавка процента     --------------------------
                foreach (float []range in mode.coefficient)
                {
                    if ((offer.price >= range[0]) && (offer.price <= range[1]))
                    {
                        coeff = range[2];
                        break;
                    }
                }
                offer.price_new = offer.price_time * coeff;
                //  --------------------------     добавка процента     --------------------------


                //  -------------------------  корректировка количества  -------------------------
                foreach (string[] str in mode.quality_correct)
                    if (offer.in_stock == str[0])
                    {
                        offer.in_stock = str[1];
                        break;
                    }
                //  -------------------------  корректировка количества  -------------------------







                if (mode.mode == "easy")
                {
                    //  --------------------------     вывод информации     --------------------------
                    if (mode.output_base_price)
                    {
                        line_csv = offer.id_with_prefix + ";" + offer.available + ";" + offer.in_stock + ";" + offer.min_quantity + ";" +
                            offer.price + ";" +  offer.price_new;
                        //line_csv = string.Format("{0};{1};{2};{3};;{4};{5};{6}",
                        //    offer.id, offer.categoryId, offer.available, offer.in_stock, offer.price, offer.price_time, offer.price_new);
                    }
                    else
                    {
                        line_csv = offer.id_with_prefix + ";" + offer.available + ";" + offer.in_stock + ";;" +
                            offer.price_new;
                        //line_csv = string.Format("{0};{1};{2};{3};;{4}",
                        //    offer.id, offer.categoryId, offer.available, offer.in_stock, offer.price_new);
                    }

                    if (line_info[2] == "True")
                        line_csv += ";" + date + ";" + line_info[3];

                    sb.AppendLine(line_csv);
                    //  --------------------------     вывод информации     --------------------------
                }
                else
                {

                    //  --------------------------   режим mode = "full"    --------------------------
                    //  ---------------------   корректирова массы и габаритов   ---------------------

                    //offer.a_new = offer.a;
                    //offer.b_new = offer.b;
                    //offer.c_new = offer.c;

                    const float coefficient = 1.3f;
                    offer.a_new = Convert.ToSingle(Math.Round(offer.a * coefficient, 2));
                    offer.b_new = Convert.ToSingle(Math.Round(offer.b * coefficient, 2));
                    offer.c_new = Convert.ToSingle(Math.Round(offer.c * coefficient, 2));

                    bool no_coeffecient = true;
                    //if (offer.weight_new == 0)
                    //{
                        offer.weight_new = Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture);

                        foreach (var item in mode.coefficient_package_mass)
                        {
                            if (item.category_id == Convert.ToInt32(offer.categoryId))
                            {

                                offer.weight_new = Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture) * item.coefficient_of_massa;
                                offer.weight_new = Convert.ToSingle(Math.Round(offer.weight_new, 2));

                                offer.a_new = Convert.ToSingle(Math.Round(offer.a * item.coefficient_of_dimensions, 2));
                                offer.b_new = Convert.ToSingle(Math.Round(offer.b * item.coefficient_of_dimensions, 2));
                                offer.c_new = Convert.ToSingle(Math.Round(offer.c * item.coefficient_of_dimensions, 2));

                                no_coeffecient = false;

                                break;
                            }
                        }

                        if (no_coeffecient)
                            offer.weight_new = Convert.ToSingle(Math.Round(offer.weight_new * coefficient, 2));
                    //}
                        
                    //  ---------------------   корректирова массы и габаритов   ---------------------



                    //  --------------------------   обновление категорий   --------------------------
                    if (rb_auto.Checked && full.tre_list_categoryes.Count > 0)
                        if (!mod_cat(ref full.tre_list_categoryes, offer)) offer.categoryId = "999999";
                    else if (categories)
                        if (!mod_cat(ref mod_catalog, offer)) offer.categoryId = "999999";

                    //  --------------------------   обновление категорий   --------------------------


                    //  --------------------------   обновление картинок    --------------------------
                    if (mode.no_watermark)
                    {
                        for (int index = 0; index < offer.pictures.Count; index++)
                        {
                            if (index >= 5) break;
                            Regex img = new Regex("(^.*)\\.(...)$");
                            Match img_m = img.Match(offer.pictures[index]);

                            if (img_m.Success)
                                offer.pictures[index] = img_m.Groups[1] + "-nw." + img_m.Groups[2];
                        }
                    }

                    if (offer.pictures.Count < 5)
                    {
                        int pic = 5 - offer.pictures.Count;
                        for (int i_pic = 0; i_pic < pic; i_pic++)
                            offer.pictures.Add("");
                    }
                    //  --------------------------   обновление картинок    --------------------------


                    //  --------------------------     вывод информации     --------------------------
                    Options_up op = new Options_up();     // строка из дополнительного фаила соответсвующии текущей записи из хмл фаила
                    try { op = options.Find(l => l.artnumber == offer.id); }
                    catch {}
                    if (op != null && op.torg_predl != "")
                        line_csv = offer.id_with_prefix + ";" + op.torg_predl + ";";
                    else
                        line_csv = offer.id_with_prefix + ";;";
                    line_csv += offer.id + ";" + offer.name + ";" + offer.vendor + ";" + offer.composition + ";" +
                        offer.categoryId + ";" + offer.available + ";" + offer.in_stock + ";";

                    if (offer.min_quantity != 0)
                        line_csv += offer.min_quantity + ";";
                    else
                        line_csv += ";";

                    line_csv += offer.weight + ";";

                    if (offer.weight_new == 0)
                        line_csv += ";";
                    else
                        line_csv += offer.weight_new + ";";
                    line_csv += offer.product_color + ";" + offer.size + ";";

                    if (mode.output_base_price)
                        line_csv += offer.price + ";" + offer.price_new + ";" ;
                    else
                        line_csv += + offer.price_new + ";" ;

                    line_csv += offer.short_name + ";" + offer.description + ";" + offer.pictures[0] + ";" + offer.pictures[1] + ";" + offer.pictures[2] + ";" + offer.pictures[3] + ";" + offer.pictures[5-1] + ";";

                    if (offer.a_new == 0) line_csv += ";";
                    else                  line_csv += offer.a_new + ";";

                    if (offer.b_new == 0) line_csv += ";";
                    else                  line_csv += offer.b_new + ";";

                    if (offer.c_new == 0) line_csv += ";";
                    else                  line_csv += offer.c_new + ";";

                    if ((offer.packing_size == null || offer.packing_size == "") && (offer.a_new == 0 && offer.b_new == 0 && offer.c_new == 0))
                        line_csv += ";";
                    else
                    {
                        if (offer.a_new != 0 && offer.b_new != 0 && offer.c_new != 0)
                        {
                            offer.packing_size = offer.a_new + "x" + offer.b_new + "x" + offer.c_new;
                            line_csv += offer.packing_size + ";";
                        }
                        else
                            line_csv += ";";
                    }

                    line_csv += offer.type_of_package + ";" + offer.composition_of_package+ ";" + offer.gred;

                    if (line_info[2] == "True")
                        line_csv += ";" + date + ";" + line_info[3];

                    line_csv += ";";
                    if (op != null)
                    {
                        foreach (string tl in op_head_list)
                        {
                            string value = op.get_property(tl.ToLower(), op);
                            line_csv += value + ";";
                            //if (tl == "TORCEVOJ_KARMAN")
                            //{
                            //}
                        }
                    }

                    sb.AppendLine(line_csv);
                    //  --------------------------     вывод информации     --------------------------
                }
            }

            string xml_name = Path.GetFileNameWithoutExtension(line_info[0]);

            string lf = line_info[1];
            Regex chk_slash = new Regex(@".*(\\)$");
            Match rx_chk_slash = chk_slash.Match(lf);

            if (lf != "")
                if (!mode.ya) {
                    if (!rx_chk_slash.Success)
                        lf += "\\";
                    xml_name = lf + xml_name + ".csv";
                }
                else
                    xml_name += ".csv";


            if (mode.ya)
            {
                File.WriteAllText("tmp\\" + xml_name, sb.ToString(), Encoding.UTF8);
                f.ya_up("tmp\\" + xml_name, lf);
            }
            else
            {
                try { File.WriteAllText(xml_name, sb.ToString(), Encoding.UTF8); }
                catch { MessageBox.Show("Проверьте правильность пути для сохранения csv файла"); }
            }

            // ---------------------------------------- сохраниение id для упощеного режима и сохранение url без описания
            functions fn = new functions();
            string[] ids = fn.get_ids(offers);
            StringBuilder sb_time = new StringBuilder();
            foreach (string i in ids)
                sb_time.Append(i + "\r\n");

            if (rb_manual.Checked && full.save_ids_dir != null && mode.mode == "full")
            {
                string name = Path.GetFileNameWithoutExtension(line_info[0]) + ".txt";
                File.WriteAllText(full.save_ids_dir + "\\" + name, sb_time.ToString());
            }
            if (rb_auto.Checked && ids_easy_dir != "" && set_full.tre_folder.Text != "" )
            {
                File.WriteAllText(ids_easy_dir + "\\id.txt", sb_time.ToString());
                sb_time.Clear();

                if(options != null)
                {
                    string [] op_ids = fn.get_options_ids(options);
                    string [] urls_not_founded = fn.get_urls(ids, op_ids, offers);

                    foreach (string i in urls_not_founded)
                        sb_time.Append(i + "\r\n");

                    File.WriteAllText(Path.GetDirectoryName(ids_easy_dir) + "\\txt_input\\urls.txt", sb_time.ToString());
                }
            }
            // ---------------------------------------- сохраниение id для упощеного режима и сохранение url без описания

            // - копируем на сервер говый файл
            if (ssh_conf.on)
            {
                try
                {
                    string copy_args = "-pw " + ssh_conf.pass + " -l " + ssh_conf.login + " " + xml_name + " " + ssh_conf.host + ":" + ssh_conf.save_folder;
                    //System.Diagnostics.Process.Start("pscp.exe", copy);
                    //pscp.exe -pw litelite -l lite c:\card.txt 192.168.9.35:/home/lite/time/hi

                    using (System.Diagnostics.Process copy = new System.Diagnostics.Process())
                    {
                        //System.Diagnostics.Process process = new System.Diagnostics.Process();
                        copy.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        copy.StartInfo.FileName = "pscp.exe";
                        copy.StartInfo.Arguments = copy_args;
                        //copy.StartInfo.UseShellExecute = false;
                        //copy.StartInfo.RedirectStandardOutput = true;
                        copy.Start();
                        //richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += copy.StandardOutput.ReadToEnd()));

                        copy.WaitForExit();
                    }
                }
                catch { MessageBox.Show("Неудалось отправить файл: " + Path.GetFileName(xml_name) + " на сервер по протоколу ssh"); }
            }
        }

        public IEnumerable<int> offer_get_id(StringReader string_xml)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(string_xml, settings))
            {
                try   { reader.MoveToContent(); }
                catch { MessageBox.Show("Не верный формат файла хмл"); }

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "offer") {

                                xml_offer offer = new xml_offer();
                                XElement el = XNode.ReadFrom(reader) as XElement;
                                int offer_id = Convert.ToInt32(el.Attribute("id").Value);

                                if (el != null)
                                    yield return offer_id;
                            }

                            break;
                    }
                }
            }
        }

        IEnumerable<xml_offer> offer(StringReader string_xml, configure mode, List<Options_up> ops_csv)
        {
            if (mode.mode != "full") Close();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(string_xml, settings))
            {
                try   { reader.MoveToContent(); }
                catch { MessageBox.Show("Не верный формат файла xml"); }

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "offer")
                            {
                                xml_offer offer = new xml_offer();
                                XElement el = XNode.ReadFrom(reader) as XElement;
                                float a, b, c;

                                IEnumerable<XElement> i;
                                offer.id = offer.id_with_prefix = el.Attribute("id").Value;

                                Options_up option = null;
                                if (ops_csv != null)
                                {
                                    //option = new Options_up();
                                    //option = ops_csv.Find(op => op.id == Convert.ToInt32(offer.id));
                                    foreach (var item in ops_csv)
                                    {
                                        if (offer.id == item.artnumber)
                                        {
                                            option = new Options_up();
                                            option = item;
                                            break;
                                        }
                                    }
                                }

                                //  option поле из фаила описания
                                if (mode.data_in_csv && option != null && option.WEIGHT != "")/* && (Convert.ToInt32(offer.id) == option.id))*/
                                {
                                    //offer.packing_size = option.WIDTH_PACK + " см × " + option.LENGTH_PACK + " см × " + option.HEIGHT_PACK + " см";
                                    offer.weight = option.WEIGHT_V;
                                    offer.weight = offer.weight.Replace(" ", "");
                                    offer.weight_new = Convert.ToSingle(option.WEIGHT);
                                }
                                else
                                {

                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Вес");
                                    offer.weight = (string)i.FirstOrDefault();
                                }


                                offer.name = el.Element("name").Value;
                                offer.name = offer.name.Replace(";", " ");

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер упаковки");
                                offer.packing_size = (string)i.FirstOrDefault();


                                //if ((offer.packing_size == null) && (option == null) ||
                                //        (offer.packing_size == null) && (option.LENGTH_PACK == "" && option.WIDTH_PACK == "" && option.HEIGHT_PACK == ""))
                                //    continue;

                                if (mode.check_delivery_options)
                                {
                                    try
                                    {
                                        offer.delivery_days = Convert.ToInt32(el.Element("delivery-options").Element("option").Attribute("days").Value);
                                        if (offer.delivery_days > 0)
                                            continue;
                                    }
                                    catch {}
                                }

                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------      упаковка     -------------------------------------
                                //15 см × 2 см × 0,3 см
                                a = b = c = 0;


                                if (mode.transform_packing_size)
                                {
                                    if (mode.data_in_csv && option != null)
                                    {
                                        if ((option.WIDTH_PACK != "") && (option.LENGTH_PACK != "") && (option.HEIGHT_PACK != ""))
                                        {
                                            a = Convert.ToSingle(option.LENGTH_PACK);
                                            b = Convert.ToSingle(option.WIDTH_PACK);
                                            c = Convert.ToSingle(option.HEIGHT_PACK);

                                            offer.type_of_package = option.DELIVERY_PACKAGE_TYPE;
                                            offer.composition_of_package = option.DELIVERY_PACKAGE;
                                        }
                                    }
                                    else
                                    {
                                        if (offer.packing_size != null)
                                        {
                                            Regex side_of_pkg = new Regex(@"^(\S+)\s*см\s*\S\s*(\S+)\s*см\s*\S\s*(\S+)");
                                            Match rx_short_name = side_of_pkg.Match(offer.packing_size);

                                            if (rx_short_name.ToString() == "")
                                            {
                                                side_of_pkg = new Regex(@"^(\S+)\s*см\s*\S\s*(\S+)\s*");
                                                rx_short_name = side_of_pkg.Match(offer.packing_size);

                                                if (rx_short_name.ToString() != "")
                                                {
                                                    if (!mode.del_not_full_packing_size)
                                                    {
                                                        a = Convert.ToSingle(rx_short_name.Groups[1].Value);
                                                        b = Convert.ToSingle(rx_short_name.Groups[2].Value);
                                                        c = 10;

                                                        offer.type_of_package = mode.type_of_package;
                                                        offer.composition_of_package = mode.composition_of_package;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                a = Convert.ToSingle(rx_short_name.Groups[1].Value);
                                                b = Convert.ToSingle(rx_short_name.Groups[2].Value);
                                                c = Convert.ToSingle(rx_short_name.Groups[3].Value);

                                                offer.type_of_package = mode.type_of_package;
                                                offer.composition_of_package = mode.composition_of_package;
                                            }
                                        }
                                    }
                                }
                                else {
                                    if(option != null)
                                    {
                                        if ((option.WIDTH_PACK != "") && (option.LENGTH_PACK != "") && (option.HEIGHT_PACK != ""))
                                        {
                                            a = Convert.ToSingle(option.LENGTH_PACK);
                                            b = Convert.ToSingle(option.WIDTH_PACK);
                                            c = Convert.ToSingle(option.HEIGHT_PACK);

                                            offer.type_of_package = option.DELIVERY_PACKAGE_TYPE;
                                            offer.composition_of_package = option.DELIVERY_PACKAGE;
                                        }
                                        else {
                                        }
                                    }
                                    else
                                    {
                                    }
                                }

                                if ((a >= mode.exception_any_side) || (b >= mode.exception_any_side) || (c >= mode.exception_any_side))
                                    continue;
                                if ((a + b + c) >= mode.exception_sum_side)
                                    continue;
                                //  --------------------------- фильтр исключений -------------------------------------
                                offer.a = a * 10; offer.b = b * 10; offer.c = c * 10;

                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------       масса       -------------------------------------
                                    if (Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture) >= mode.exception_weight)
                                        continue;
                                //  --------------------------- фильтр исключений -------------------------------------



                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------        имя        -------------------------------------
                                string exp = "";
                                foreach (string name in mode.exception_name)
                                {
                                    exp = @"^.*(" + name + ").*";
                                    Regex exp_name = new Regex(exp);

                                    if (exp_name.IsMatch(offer.name))
                                        goto end;
                                }

                                //  --------------------------- фильтр исключений -------------------------------------



                                offer.available = el.Attribute("available").Value;
                                offer.url = el.Element("url").Value;
                                offer.price = offer.price_time = Convert.ToSingle(el.Element("price").Value, CultureInfo.InvariantCulture);
                                //offer.currencyId = el.Element("currencyId").Value;
                                offer.categoryId = el.Element("categoryId").Value;
                                //offer.delivery = el.Element("delivery").Value;
                                //if (mode.use_short_name)
                                //{
                                //    offer.short_name = offer.name_short(offer.name);
                                //    if (mode.add_articule_to_short_name) offer.short_name += ", арт. " + offer.id;
                                //}

                                if (!mode.data_in_csv || option == null)
                                {
                                    try { offer.vendor = el.Element("vendor").Value; }
                                    catch { offer.vendor = ""; }
                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Состав");
                                    offer.composition = (string)i.FirstOrDefault();
                                    offer.description = el.Element("description").Value;
                                    offer.description = offer.description.Replace(";", " ");
                                    offer.description = offer.description.Replace("\n", " ");
                                }
                                else
                                {
                                    if (option.proizvoditel != "")
                                        offer.vendor = option.proizvoditel;
                                    else
                                    {
                                        try     { offer.vendor = el.Element("vendor").Value; }
                                        catch   { offer.vendor = ""; }
                                    }

                                    if (option.MATERIAL != "")
                                        offer.composition = option.MATERIAL;
                                    else if (option.SOSTAV != "")
                                        offer.composition = option.MATERIAL = option.SOSTAV;
                                    else
                                    {
                                        i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Состав");
                                        offer.composition = option.MATERIAL = (string)i.FirstOrDefault();
                                    }

                                    if (mode.use_xml_description)
                                    {
                                        offer.description = el.Element("description").Value;
                                        offer.description = offer.description.Replace(";", " ");
                                    }
                                    else
                                    {
                                        if (option.description != "")
                                        {
                                            offer.description = option.description;
                                            offer.description = offer.description.Replace(";", " ");
                                            offer.description = offer.description.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            offer.description = el.Element("description").Value;
                                            offer.description = offer.description.Replace(";", " ");
                                        }
                                    }

                                    if (option.artnumber != "")
                                        offer.id_with_prefix = option.artnumber;
                                    //  ------------------------------  Получение цвета ------------------------------
                                    if (option.PRODUCT_COLOR != "")
                                        offer.product_color = option.PRODUCT_COLOR;
                                    else
                                        if (mode.color_from_YML)
                                            offer.product_color = f.color_from_name(offer.name);
                                    //  ------------------------------  Получение цвета ------------------------------
                                    // offer.type_of_package = option.DELIVERY_PACKAGE_TYPE;       //  дубль
                                    // offer.composition = option.MATERIAL;
                                }

                                //offer.vendorCode = el.Element("vendorCode").Value;
                                offer.sales_notes = el.Element("sales_notes").Value;

                                if (mode.gred)
                                {
                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер");
                                    offer.size = (string)i.FirstOrDefault();
                                }

                                //  ------------------------------  Получение цвета ------------------------------
                                if (option != null && option.PRODUCT_COLOR != "")
                                    offer.product_color = option.PRODUCT_COLOR;
                                else
                                    if (mode.color_from_YML)
                                        offer.product_color = f.color_from_name(offer.name);
                                //  ------------------------------  Получение цвета ------------------------------


                                //i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Индивидуальная упаковка");
                                //offer.individual_packing = (string)i.FirstOrDefault();

                                //i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Сертификат");
                                //offer.certificate = (string)i.FirstOrDefault();



                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "На складе");
                                offer.in_stock = (string)i.FirstOrDefault();

                                offer.pictures = offer.pictures_to_str(el.Elements("picture"));

                              //  richTextBox2.Text = richTextBox2.Text + "\r\n" + offer.name;

                                //foreach (string pic in offer.pictures) richTextBox1.Text = richTextBox1.Text + "\r\n" + pic;

                                if (el != null)
                                    yield return offer;
                            }

                        break;
                    }
                end:;
                }
            }
        }


        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void АапToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set_easy.Show();
            set_easy.Visible = true;
        }

        private void ПраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set_full.Show();
            set_full.Visible = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            save save_obj = new save(easy, full, ssh_conf);
            string json = JsonConvert.SerializeObject(save_obj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("save.json", json);

            //JsonSerializer l = new JsonSerializer();
            //using (TextWriter fs = new StreamWriter("save.json"))
            //{
            //    l.Serialize(fs, full);
            //}
        }

        private void Xml_file_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            threads = Convert.ToInt32(CPU_get.SelectedItem.ToString());
            ThreadPool.SetMaxThreads(threads, threads);
        }

        private void CPU_get_Leave(object sender, EventArgs e)
        {
            try
            {
                threads = Convert.ToInt32(CPU_get.Text);
                if ((threads > CPU) || (threads <= 0))
                    threads = CPU;
            }
            catch { threads = CPU; }
        }

        private void get_stop_words()
        {
            string stop_wrd = File.ReadAllText("stop words.csv", Encoding.Default);
            //richTextBox1.Text = stop_wrd;

            Regex short_name = new Regex("(.*)\r\n");
            MatchCollection words = short_name.Matches(stop_wrd);
            //richTextBox2.Text = words.Count.ToString();

            Regex sub_string = new Regex(";");
            string[] line;

            int i = -1;
            foreach (Match m in words)
            {
                i++;
                if (i == 0) continue;
                line = sub_string.Split(m.Groups[1].Value);
                line[1] = line[1].Replace(":", "\\:");
                line[1] = line[1].Replace("(", "\\(");
                line[1] = line[1].Replace(".", "\\.");
                line[1] = line[1].Replace("=", "\\=");
                line[1] = line[1].Replace(" ", "\\s+");

                if (line[0] != "") prepositions.Add(line[0]);
                if (line[1] != "") stop_words.Add(line[1]);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            const double day = 86400, start_time = 1606755508;
            DateTimeOffset dto = DateTimeOffset.Now; long now = dto.ToUnixTimeSeconds();
            double hi = (now - start_time) / day;
            if ((now - start_time) / day > 7)
                Close();

            //Form cli = new Form();
            //cli.Show();
            //cli.ControlBox = false;
            //cli.Visible = false;
            //cli.Location = new Point(700, 700);
            //cli.ShowDialog();

            //File.WriteAllText(@"C:\Users\и\Desktop\sPCK.txt", xml);

            //webClient.DownloadFile(new Uri(link), )

            //int minWorker, minIOC;
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(threads, threads);
            //ThreadPool.SetMaxThreads(1, 900);
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //ThreadPool.GetMaxThreads(out minWorker, out minIOC);

            //int[] item = { 1, 2, 3, 5, 7, 7, 7, 7, 7, 7 };
            //Parallel.ForEach(item, (it) => {
            //    for (;;){}
            //}) ;

            try     { token = File.ReadAllText("token.txt"); }
            catch   { token = ""; MessageBox.Show("Укажите токен в файле token.txt"); }

            f = new functions(this);

            //bool name;
            //name = f.ya_dl("/csv/odezhda-i-obuv.csv", @"C:\Users\и\Desktop\save");
            //name = f.ya_dl("/csv/xml.csv", @"C:\Users\и\Desktop\save");
            //name = f.ya_up(@"C:\Users\и\Documents\l\info_comments.cpp", @"/save");

            set_easy.Owner = this;
            set_full.Owner = this;
            ssh_form.Owner      = this;

            //set_easy.Show();
            //set_full.Show();

            /*int x_e = set_easy.Location.X;
            int y_e = set_easy.Location.Y;
            int x_f = set_full.Location.X;
            int y_f = set_full.Location.Y;
            set_easy.Location = new Point(x_e + 9000, y_e);
            set_full.Location = new Point(x_f + 9000, y_f);
            set_easy.Visible = false;
            set_full.Visible = false;
            set_easy.Location = new Point(x_e, y_e);
            set_full.Location = new Point(x_f, y_f);*/

            line = new file_line(this);

            full = new configure("full", this);
            easy = new configure("easy", this);
            conf_options = new configure("options", this);

            //deserialize

            string save_json;
            try     { save_json = File.ReadAllText("save.json"); }
            catch   { save_json = "";  }

            try {
                if (save_json != "")
                {
                    //configure s_obj = JsonConvert.DeserializeObject<configure>(save_json);
                    save save_obj = JsonConvert.DeserializeObject<save>(save_json);

                    if (save_obj.e != null && save_obj.f != null)
                    {
                        save_obj.f.load_gred();
                        configure time_easy = easy, time_full = full;

                        easy = save_obj.e;
                        easy.days = time_easy.days;
                        easy.time_sh = time_easy.time_sh;

                        full = save_obj.f;
                        full.days = time_full.days;
                        full.time_sh = time_full.time_sh;

                        f.set_settings(ssh_form, set_easy, set_full, save_obj);

                        if (easy.ya == true || full.ya == true)
                        {
                            ya.Checked = true;
                            easy.ya = true;
                            full.ya = true;
                        }
                    }
                    if (save_obj.ssh != null)
                        ssh_conf = save_obj.ssh;
                }
            }
            catch(ArgumentException err) { MessageBox.Show(err.ToString()); }

            groupBox1.SendToBack();

            get_stop_words();
            for (int i = 1; i <= CPU; ++i)
                CPU_get.Items.Add(i);
        }


        private void Ya_CheckedChanged(object sender, EventArgs e)
        {
            if (ya.Checked)
            {
                easy.ya = true;
                full.ya = true;
            }
            else
            {
                easy.ya = false;
                full.ya = false;
            }
        }


        // сохранений настроек
        private void Button2_Click(object sender, EventArgs e)
        {

            string name = prefix_for_id.Text;
            if (name == "")
                name = "save";

            saveFile.FileName = name;

            if (saveFile.ShowDialog() == DialogResult.Cancel)
                return;

            name = saveFile.FileName;

            save save_obj = new save(easy, full, ssh_conf);
            string json = JsonConvert.SerializeObject(save_obj, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(name, json);
            //MessageBox.Show("Файл сохранен");
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string save_json;

            if (openFile.ShowDialog() == DialogResult.Cancel)
                return;

            string name = openFile.FileName;
            try   { save_json = File.ReadAllText(name); }
            catch { save_json = ""; MessageBox.Show("Не удалось загрузить файл настроек"); }

            try
            {
                if (save_json != "")
                {
                    save save_obj = JsonConvert.DeserializeObject<save>(save_json);

                    if (save_obj.e != null && save_obj.f != null)
                    {
                        configure e_time = easy, f_time = full;
                        // e_time = clone.Clone(easy); f_time = clone.Clone(full);
                        save_obj.f.load_gred();

                        ctrls(set_easy.Controls); ctrls(set_full.Controls);

                        easy = save_obj.e;
                        easy.time_sh = e_time.time_sh;
                        easy.days = e_time.days;

                        full = save_obj.f;
                        full.time_sh = f_time.time_sh;
                        full.days = f_time.days;

                        f.set_settings(ssh_form, set_easy, set_full, save_obj);
                        if (easy.ya == true || full.ya == true)
                        {
                            ya.Checked = true;
                            easy.ya = true;
                            full.ya = true;
                        }
                    }
                }
            }
            catch (ArgumentException err) { MessageBox.Show(err.ToString()); }
        }

        public void ctrl(Control cont)
        {
            if (cont is TextBox)
            {
                TextBox textBox = (TextBox)cont;
                textBox.Text = "";
                return;
            }

            if (cont is ComboBox)
            {
                ComboBox comboBox = (ComboBox)cont;
                if (comboBox.Items.Count > 0)
                    comboBox.SelectedIndex = 0;
                return;
            }

            if (cont is CheckBox)
            {
                CheckBox checkBox = (CheckBox)cont;
                checkBox.Checked = false;
                return;
            }

            if (cont is ListBox)
            {
                ListBox listBox = (ListBox)cont;
                listBox.Items.Clear();
                return;
            }

            if (cont is RichTextBox)
            {
                RichTextBox listBox = (RichTextBox)cont;
                listBox.Text = "";
                return;
            }
        }
        public void ctrls(Control.ControlCollection cont)
        {
            foreach (Control control in cont)
            {
                ctrl(control);
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control item in gb.Controls)
                        ctrl(item);
                }
            }
        }

        private void ТокенToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFile_token.ShowDialog() == DialogResult.Cancel)
                return;

            token = File.ReadAllText(openFile_token.FileName);
            File.WriteAllText("token.txt", token);
        }

        // ------------------------------------------------------------------- переменные для таимера -------------------------------------------------------------------
        Dictionary<string, string> week = new Dictionary<string, string>
        {
            { "Вс", "Sunday" }, { "Пн", "Monday" }, { "Вт", "Tuesday" }, { "Ср", "Wednesday" }, { "Чт", "Thursday" }, { "Пт", "Friday" }, { "Сб", "Saturday" }
        };
        struct timer_st
        {
            public bool easy, full, options;
        };
        timer_st tm = new timer_st();
        // ------------------------------------------------------------------- переменные для таимера -------------------------------------------------------------------
        private void Timer1_Tick(object sender, EventArgs e)
        {
            ThreadPool.GetAvailableThreads(out threads_avariable, out threads_all);
            //string load = null;

            //if (threads_all - threads_avariable == 0)
            //{
            //    load = threads_avariable + "avr, " + threads_all + "all. " + "Очередь свободна";
            //    MessageBox.Show(load);
            //}
            //else
            //{
            //    load = "В очереди " + Convert.ToString(threads_all - threads_avariable) + "поток(а)";
            //    MessageBox.Show(load);
            //}



            DateTimeOffset time = DateTimeOffset.Now;
            string day_of_week = time.DayOfWeek.ToString();
            int hour = time.Hour,
                min = time.Minute;

            var day_list = week.Where(l => l.Value == day_of_week).FirstOrDefault();
            string day = day_list.Key;

            Label label_time = easy.days.Where(lb => lb.Text == day).FirstOrDefault();
            if ((int)label_time.Tag == 1)
            {
                if (Convert.ToInt32(easy.time_sh[0].Text) == hour && Convert.ToInt32(easy.time_sh[1].Text) == min)
                {
                    if (threads_all - threads_avariable == 0)
                    {
                        if (easy.exception_rules_xml == null)
                        {
                            MessageBox.Show("Установите поле фильтр исключений для простого режима");
                            return;
                        }

                        if (!tm.easy)
                        {
                            //MessageBox.Show("time!");
                            richTextBox2.Text = "";
                            if (rb_auto.Checked)
                            {
                                functions fn = new functions(this);
                                fn.tre_threads_offer("easy");
                            }
                            else
                                multi_offer(threads, "easy");

                            tm.easy = true;
                        }

                    }
                }
                else if (tm.easy)
                    tm.easy = false;
            }

            label_time = full.days.Where(lb => lb.Text == day).FirstOrDefault();
            if ((int)label_time.Tag == 1)
            {
                if (Convert.ToInt32(full.time_sh[0].Text) == hour && Convert.ToInt32(full.time_sh[1].Text) == min)
                {

                    if (threads_all - threads_avariable == 0)
                    {
                        if (full.exception_rules_xml == null)
                        {
                            MessageBox.Show("Установите поле фильтр исключений для полного режима");
                            return;
                        }
                        if (!tm.full)
                        {
                            //MessageBox.Show("time full!");
                            richTextBox2.Text = "";
                            if (rb_auto.Checked)
                            {
                                functions fn = new functions(this);
                                fn.tre_threads_offer("full");
                            }
                            else
                                multi_offer(threads, "full");

                            tm.full = true;
                        }
                    }
                }
                else if (tm.full)
                    tm.full = false;
            }

            // --------------------------------------------------------- запуск создания дополнительных фаилов ---------------------------------------------------------
            label_time = conf_options.days.Where(lb => lb.Text == day).FirstOrDefault();
            if ((int)label_time.Tag == 1)
            {
                if (Convert.ToInt32(conf_options.time_sh[0].Text) == hour && Convert.ToInt32(conf_options.time_sh[1].Text) == min)
                {
                    if (!tm.options)
                    {
                        richTextBox2.Text = "";

                        functions fn = new functions(this);
                        fn.tre_threads_options();
                        tm.options = true;
                    }
                }
                else if (tm.options)
                    tm.options = false;
            }
            // --------------------------------------------------------- запуск создания дополнительных фаилов ---------------------------------------------------------
        }

        bool timer_logic = false;

        private void test_Click(object sender, EventArgs e)
        {
            f.tre_threads_options();
        }
        
        private void hi_Click(object sender, EventArgs e)
        {
            f.tre_threads_offer("full");
        }

        private void rb_manual_CheckedChanged(object sender, EventArgs e)
        {
            btn_make_tables.Enabled = false;
            btn_make_options.Enabled = false;
        }

        private void rb_auto_CheckedChanged(object sender, EventArgs e)
        {
            btn_make_tables.Enabled = true;
            btn_make_options.Enabled = true;
        }

        private void настройкиSshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ssh_form.Show(); ssh_form.Visible = true;
        }

        private void cb_ssh_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_ssh.Checked)
                ssh_conf.on = true;
            else
                ssh_conf.on = false;
        }

        private void Shed_Click(object sender, EventArgs e)
        {
            if (!timer_logic)
            {
                timer1.Start();
                timer_logic = true;
                shed.Text = "Стоп";
                shed.BackColor = Color.Coral;
            }
            else
            {
                timer1.Stop();
                timer_logic = false;
                shed.Text = "Запуск";
                shed.BackColor = Color.Bisque;
            }
        }
    }
}

public static class clone
{
    public static T Clone<T>(this T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException("The type must be serializable.", "source");
        }


        if (Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new MemoryStream())
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
}

public class Web_cl : System.Net.WebClient
{
    public int Timeout { get; set; } = 1700000;
    protected override WebRequest GetWebRequest(Uri uri)
    {
        WebRequest lWebRequest = base.GetWebRequest(uri);
        lWebRequest.Timeout = Timeout;
        ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
        ((HttpWebRequest)lWebRequest).AllowWriteStreamBuffering = false;

        return lWebRequest;
    }
}

public class functions
{
    public Form1 f = new Form1();
    public functions() {}
    public functions(Form1 form)
    {
        f = form;
    }
    //public functions(){}
    public string clear_Composition(string comp)
    {
        if (comp != null)
        comp = Regex.Replace(comp, "\\d*%", "");

        return comp;
    }
    public string color_from_name(string name)
    {
        //Regex col = new Regex("(цвет.*),");
        //Regex col = new Regex(@"цвет[:punct:]?\s+(.[^,\d*(]*)(\s*)");
        Regex col = new Regex(@"цвет[:punct:]?\s+(.[^,\d*(]*)");
        Match m = col.Match(name);
        name = m.Groups[1].Value;

        col = new Regex("(.*[^\\s*$])");
        m = col.Match(name);
        name = m.Groups[1].Value;

        return name;
    }
    public bool ya_dl(string file, string save = "")
    {
        try
        {

            //Form cl = new Form();
            //cl.Size = new Size(270, 50);
            //cl.Text = "Скачевание " + Path.GetFileName(file);
            //cl.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //ProgressBar l = new ProgressBar();
            //l.Width = 125;
            //l.Visible = true;

            //Label lb = new Label();
            //lb.Size = new Size(135, 20);
            //lb.Location = new Point(125, 2);

            //cl.Controls.Add(l);
            //cl.Controls.Add(lb);
            //cl.Show();
            //cl.Refresh();

            //Form cli = new Form();
            //cli.Show();
            //cli.ControlBox = false;
            //cli.Location = new Point(700, 700);
            //cli.Visible = false;

            // ---------------------------------------------- загрузка ----------------------------------------------
            WebClient w_cl = new WebClient();
            //w_cl.DownloadProgressChanged += (a, e) =>
            //{
            //    /*lb.Invoke((MethodInvoker)(() => { */
            //    lb.Text = "   " + e.BytesReceived / 1024 + " of " + e.TotalBytesToReceive / 1024;/* }));*/
            //                                                                                     /*l.Invoke((MethodInvoker)(() => { */
            //    l.Value = e.ProgressPercentage;/* }));*/
            //};

            //w_cl.DownloadFileCompleted += (s, e) =>
            //{
            //    cli.Close();
            //    cl.Close();
            //};

            if (save != "") save += "\\";

            if (is_xml(file))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                w_cl.DownloadFile/*Async*/(new Uri(file), save + Path.GetFileName(file));
            }
            else
            {
                string link = "https://cloud-api.yandex.net:443/v1/disk/resources/download?path=" + file;

                HttpWebRequest get = (HttpWebRequest)HttpWebRequest.Create(link);
                get.ContentType = "application/json";
                get.Headers.Add(HttpRequestHeader.Authorization, f.token);
                HttpWebResponse answer = (HttpWebResponse)get.GetResponse();

                StreamReader reader = new StreamReader(answer.GetResponseStream());
                StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());
                answer.Close();

                string dl = output.ToString();
                dl_url url_i = JsonConvert.DeserializeObject<dl_url>(dl);
                string time = url_i.href;

                w_cl.DownloadFile/*Async*/(new Uri(time), save + Path.GetFileName(file));
            }
            //Thread.Sleep(470);
            ////cli.ShowDialog();
            //cli.ShowDialog();
            //Thread.Sleep(470);
            // ---------------------------------------------- загрузка ----------------------------------------------
            
            return true;
        }
        catch (WebException err) {
            MessageBox.Show("Проверьте параметры аутентификации и пути к имени файла для загрузки файла на яндекс диск");
            MessageBox.Show(err.ToString());
            return false;
        }
    }
    public bool ya_up(string file, string dir)
    {
        //string link = @"https://cloud-api.yandex.net:443/v1/disk/resources/upload?path=/xml/hi";
        string link = "https://cloud-api.yandex.net/v1/disk/resources/upload?path=" + dir;
        if (!Regex.IsMatch(dir, @"/$"))
            link += @"/";
        link += Path.GetFileName(file) + "&overwrite=true";

        WebResponse answer = null;
        WebRequest get = WebRequest.Create(link);
        //get.ContentType = "application/json";
        get.Headers.Add(HttpRequestHeader.Authorization, f.token);
        try   { answer = get.GetResponse(); }
        catch { MessageBox.Show("Неудалось получить ответ от яндекс диска"); }

        StreamReader reader = new StreamReader(answer.GetResponseStream());
        StringBuilder output = new StringBuilder();
        output.Append(reader.ReadToEnd());
        answer.Close();

        string dl = output.ToString();
        dl_url url_i = JsonConvert.DeserializeObject<dl_url>(dl);
        string time = url_i.href;

        //Form cl = new Form();
        //cl.Size = new Size(270, 50);
        //cl.Text = "Загрузка " + Path.GetFileName(file);
        //cl.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        //ProgressBar l = new ProgressBar();
        //l.Width = 125;
        //l.Visible = true;

        //Label lb = new Label();
        //lb.Size = new Size(135, 20);
        //lb.Location = new Point(125, 2);

        //cl.Controls.Add(l);
        //cl.Controls.Add(lb);
        //cl.Show();
        //cl.Refresh();

        ////MessageBox.Show("hi");
        //Form cli = new Form();
        //cli.Show();
        //cli.ControlBox = false;
        //cli.Location = new Point(7000, 7000);
        //cli.Visible = false;
        System.Net.ServicePointManager.SetTcpKeepAlive(false, 0, 0);
        System.Net.ServicePointManager.MaxServicePointIdleTime = 2000;
        Web_cl w_cl = new Web_cl();

        //w_cl.UploadProgressChanged += (a, e) =>
        //{
        //    lb.Text = "   " + e.BytesSent / 1024 + " of " + e.TotalBytesToSend / 1024;
        //    l.Value = Convert.ToInt32(e.BytesSent * 100 / e.TotalBytesToSend);
        //};

        //w_cl.UploadFileCompleted += (s, e) =>
        //{
        //    cli.Close();
        //    cl.Close();
        //};

        //Random rnd = new Random();
        //int rn = rnd.Next(0, 570);
        //Thread.Sleep(rn);
        try
        {
            w_cl.UploadFile/*Async*/(new Uri(time), file);
        }
        catch(WebException err)
        {
            MessageBox.Show("Ошибка загрузки");
        }
        //Thread.Sleep(rn);
        //cli.ShowDialog();

        return true;
    }
    public void set_settings(Ssh_form ssh, Form2 e, Form3 f3, save data)
    {
        // ---------------------------------------------------------- ssh -----------------------------------------------------------
        if (data.ssh != null)
        {
            ssh.login.Text = data.ssh.login;
            ssh.pass.Text = data.ssh.pass;
            ssh.host.Text = data.ssh.host;
            ssh.port.Text = data.ssh.port;
            ssh.save_folder.Text = data.ssh.save_folder;
            if (data.ssh.on)
                f.cb_ssh.Checked = true;
            else
                f.cb_ssh.Checked = false;
        }
        // ---------------------------------------------------------- ssh -----------------------------------------------------------
        // ---------------------------------------------------------- easy ----------------------------------------------------------
        if (data.e.prefix_for_id != "")
            e.prefix_for_id.Text = data.e.prefix_for_id;

        if (data.e.exception_rules_xml != null && data.e.exception_name.Count > 0)
        {
            e.exception_rules_xml.Items.Clear();
            e.exception_rules_xml.Items.Add(Path.GetFileName(data.e.exception_rules_xml));
        }

        if (data.e.check_delivery_options)
            try { e.exclude_in_other_store.Checked = true; } catch {}

        if (data.e.get_min_sale)
            try { e.correction_of_quantity.Checked = true; } catch {}

        if (data.e.output_base_price)
            try { e.use_base_price.Checked = true; } catch {}

        if (data.e.file_to_create_new_price != null && data.e.coefficient.Count > 0)
        {
            e.new_price.Items.Clear();
            e.new_price.Items.Add(Path.GetFileName(data.e.file_to_create_new_price));
        }

        if (data.e.file_to_create_new_quality != null && data.e.quality_correct.Count > 0)
        {
            e.correction_quantity.Items.Clear();
            e.correction_quantity.Items.Add(Path.GetFileName(data.e.file_to_create_new_quality));
        }
        if (data.e.get_ids_dir != null)
            e.tb_ids_folder.Text = data.e.get_ids_dir;
        // ---------------------------------------- tree del_old_itm -----------------------------------------
        if (data.e.tre_del_old_itm_bool)
        {
            e.label22.Enabled = true;
            e.tb_del_old_itm.Enabled = true;
            try { e.cb_del_old_itm.Checked = true; } catch {}
        }
        else
        {
            e.label22.Enabled = false;
            e.tb_del_old_itm.Enabled = false;
            try { e.cb_del_old_itm.Checked = false; } catch {}
        }
        if (data.e.tre_del_old_itm_count != "")
            e.tb_del_old_itm.Text = data.e.tre_del_old_itm_count;
        // ---------------------------------------- tree del_old_itm -----------------------------------------
        // ---------------------------------------------------------- easy ----------------------------------------------------------
        // ---------------------------------------------------------- full ----------------------------------------------------------
        if (data.f.prefix_for_id != "")
            f3.prefix_for_id.Text = data.f.prefix_for_id;

        if (data.f.exception_rules_xml != null && data.f.exception_name.Count > 0)
        {
            f3.exception_rules_xml.Items.Clear();
            f3.exception_rules_xml.Items.Add(Path.GetFileName(data.f.exception_rules_xml));
        }

        if (data.f.data_in_csv)
        {
            try { f3.data_in_csv.Checked = true; } catch {}
            f3.use_xml_description.Enabled = true;
        }

        if (data.f.use_xml_description)
            try { f3.use_xml_description.Checked = true; } catch {}

        if (data.f.no_watermark)
            try { f3.no_watermark.Checked = true; } catch {}

        if (data.f.use_short_name) {
            try { f3.use_short_name.Checked = true; } catch {}
            f3.add_articule_to_short_name.Enabled = true;
        }

        if (data.f.add_articule_to_short_name)
            try { f3.add_articule_to_short_name.Checked = true; } catch {}

        if (data.f.check_delivery_options)
            try { f3.exclude_in_other_store.Checked = true; } catch {}

        if (data.f.get_min_sale)
            try { f3.correction_of_quantity.Checked = true; } catch {}

        if (data.f.output_base_price)
            try { f3.use_base_price.Checked = true; } catch {}

        if (data.f.file_to_create_new_price != null && data.f.coefficient.Count > 0)
        {
            f3.new_price.Items.Clear();
            f3.new_price.Items.Add(Path.GetFileName(data.f.file_to_create_new_price));
        }

        if (data.f.file_to_create_new_quality != null && data.f.quality_correct.Count > 0)
        {
            f3.correction_quantity.Items.Clear();
            f3.correction_quantity.Items.Add(Path.GetFileName(data.f.file_to_create_new_quality));
        }

        if (data.f.gred)
            try { f3.gred.Checked = true; } catch {}

        if (data.f.gred_file != null && data.f.gls != null)
        {
            f3.gred_file.Items.Clear();
            f3.gred_file.Items.Add(Path.GetFileName(data.f.gred_file));
        }

        if (data.f.transform_packing_size)
        {
            f3.del_not_full_packing_size.Enabled = true;
            try { f3.transform_packing_size.Checked = true; } catch {}
        }

        if (data.f.del_not_full_packing_size)
            try { f3.del_not_full_packing_size.Checked = true; } catch {}

        if (data.f.type_of_package != null)
            f3.type_of_package.Text = data.f.type_of_package;

        if (data.f.composition_of_package != null)
            f3.composition_of_package.Text = data.f.composition_of_package;

        if (data.f.file_coefficient_package_mass != null && data.f.coefficient_package_mass.Count > 0)
        {
            f3.coefficient_package_mass.Items.Clear();
            f3.coefficient_package_mass.Items.Add(Path.GetFileName(data.f.file_coefficient_package_mass));
        }

        if (data.f.color_YML)
            try { f3.color_YML.Checked = true; } catch {}

        if (data.f.file_colors != null && data.f.color.Count > 0)
        {
            f3.color.Items.Clear();
            f3.color.Items.Add(Path.GetFileName(data.f.file_colors));
        }

        if (data.f.color_from_YML)
            try { f3.color_from_YML.Checked = true; } catch {}
        if (data.f.tre_folder != "")
            f3.tre_folder.Text = data.f.tre_folder;
        if (data.f.file_head_options != null)
        {
            f3.options_lb.Items.Clear();
            f3.options_lb.Items.Add(Path.GetFileName(data.f.file_head_options));
        }
        // ---------------------------------------- tree mode каталог ----------------------------------------
        if (data.f.tre_bool_mod_catalog)
        {
            f3.label_mod_catalog.Enabled = true;
            f3.list_mod_catalog.Enabled  = true;
            try { f3.bool_mod_catalog.Checked = true; } catch {}
        }
        else
        {
            f3.label_mod_catalog.Enabled = false;
            f3.list_mod_catalog.Enabled  = false;
            try { f3.bool_mod_catalog.Checked = false; } catch {}
        }
        if (data.f.tre_list_categoryes.Count > 0)
        {
            f3.list_mod_catalog.Items.Clear();
            f3.list_mod_catalog.Items.Add(Path.GetFileName(data.f.file_list_mod_catalog));
        }
        // ---------------------------------------- tree mode каталог ----------------------------------------
        // ---------------------------------------- tree del_old_itm -----------------------------------------
        if (data.f.tre_del_old_itm_bool)
        {
            f3.label22.Enabled = true;
            f3.tb_del_old_itm.Enabled = true;
            try { f3.cb_del_old_itm.Checked = true; } catch {}
        }
        else
        {
            f3.label22.Enabled = false;
            f3.tb_del_old_itm.Enabled = false;
            try { f3.cb_del_old_itm.Checked = false; } catch {}
        }
        if (data.f.tre_del_old_itm_count != "")
            f3.tb_del_old_itm.Text = data.f.tre_del_old_itm_count;
        // ---------------------------------------- tree del_old_itm -----------------------------------------
        if (data.f.save_ids_dir != null)
            f3.tb_save_ids_dir.Text = data.f.save_ids_dir;
        // ---------------------------------------------------------- full ----------------------------------------------------------

        }
    public void clear_configure(string mode)
    {
        configure time_easy = f.easy, time_full = f.full;

        if (mode == "easy")
        {
            f.easy = new configure("easy");
            f.easy.days = time_easy.days;
            f.easy.time_sh = time_easy.time_sh;
        }
        if (mode == "full")
        {
            f.full = new configure("full");
            f.full.days = time_full.days;
            f.full.time_sh = time_full.time_sh;
        }
    }
    private bool is_xml(string xml)
    {
        for (int i = 0; i < f.line.line_xml_file.Count; i++)
            if (f.line.line_xml_file[i].Text == xml)
                return true;

        return false;
    }
    public List<Options_up> take_options(string file_options)
    {
        List<Options_up> options = new List<Options_up>();
        if (file_options == "") return null;
        try
        {
            using (var reader = new StreamReader(file_options, Encoding.GetEncoding(1251)))
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
                options = l.ToList();
            }

            return options;
        }
        catch (BadDataException i)
        {
            MessageBox.Show("Ошибка загрузки данных из таблицы дополнительных полеи\r\n" + i.ToString());
            return null;
        }
    }
    public void tre_threads_options()
    {
        cfg_data cfg = new cfg_data();
        cfg.options = f.full.head_options;
        cfg.prepositions = Form1.prepositions;
        cfg.stop_words = Form1.stop_words;
        cfg.coefficients = f.full.coefficient;
        cfg.coefficients_volume_and_mass = f.full.coefficient_package_mass;

        string[] files_xml = Directory.GetDirectories(f.full.tre_folder + "\\XML");

        bool th = ThreadPool.SetMaxThreads(f.threads, f.threads);


        foreach (string dir in files_xml)
        {
            object[] hi = { Directory.GetFiles(dir).First(), dir + "\\Tmp_files", dir + "\\Dop_file", cfg, "cfg" };
            ThreadPool.QueueUserWorkItem(f.stl.make_op, hi);
            Thread.Sleep(1800);
        }
    }
    public void tre_threads_offer(string type)
    {
        //string type = "full";
        string tre_folder = f.full.tre_folder;
        string[] tre_xml_dirs = {};
        try { tre_xml_dirs = Directory.GetDirectories(f.full.tre_folder + "\\XML"); }
        catch
        {
            MessageBox.Show("Укажите правильный путь до корневой папки");
            return;
        }
        string[] tre_xmls = new string[tre_xml_dirs.Length];
        string[] tre_ops = new string[tre_xml_dirs.Length];
        for (int i = 0; i < tre_xml_dirs.Length; i++)
        {
            tre_xmls[i] = Directory.GetFiles(tre_xml_dirs[i])[0];
            tre_ops[i] = tre_xml_dirs[i] + "\\Dop_file\\";
            try   { tre_ops[i] += Path.GetFileName(Directory.GetFiles(tre_ops[i])[0]); }
            catch { tre_ops[i] = ""; }

        }

        bool th = ThreadPool.SetMaxThreads(f.threads, f.threads);
        for (int i = 0; i < tre_xmls.Length; i++)
        {
            object[] hi = { tre_xmls[i], tre_ops[i], type };
            ThreadPool.QueueUserWorkItem(f.tre_thread_offer, hi);
        }
    }
    public string[] get_ids(List<xml_offer> xmls)
    {
        string[] ids;
        try { ids = new string[xmls.Count]; }
        catch { return null; }

        for (int i = 0; i < xmls.Count; i++)
            ids[i] = xmls[i].id;

        return ids;
    }
    public string[] file_get_ids(string file_ids)
    {
        string data_ids;
        try { data_ids = File.ReadAllText(file_ids); }
        catch { return null; }

        Regex lines = new Regex("(\\d*)\r\n");
        MatchCollection m_ids = lines.Matches(data_ids);
        string[] ids;
        try { ids = new string[m_ids.Count]; }
        catch { return null; }

        for (int i = 0; i < m_ids.Count; i++)
            ids[i] = m_ids[i].Groups[1].Value;

        return ids;
    }
    public string [] get_options_ids(List<Options_up> op)
    {
        string[] op_ids;
        try   { op_ids = new string[op.Count]; }
        catch { return null; }

        for (int i = 0; i <  op.Count; i++)
            op_ids[i] = op[i].artnumber;

        return op_ids;
    }
    public string[] get_urls (string []xml_ids, string[] option_ids, List<xml_offer> offers)
    {
        string[] urls = xml_ids.Except(option_ids).ToArray();

        if (urls == null)
            return null;

        string url/*, id_op*/;
        Regex r_url = new Regex(@"(.*/\d+)");

        for (int i = 0; i < urls.Length; i++)
        {
            xml_offer offer = offers.Find(ul => ul.id == urls[i]);
            url = r_url.Match(offer.url).Groups[1].Value;
            urls[i] = url;
        }

        return urls;
    }
    public string get_url_in_file(string file_name)
    {
        string  url = null;
        try   { url = File.ReadAllText(file_name); }
        catch { return null; }

        Regex get_url = new Regex("URL=(.*)\r\n");
        url = get_url.Match(url).Groups[1].Value;

        return url;
    }
}
public class xml_offer
{

    public string id, id_with_prefix, url;
    public int delivery_days, min_quantity;
    public float price, price_time, price_new, weight_new, a, b, c, a_new, b_new, c_new;

    public string available, categoryId, name, short_name, vendor, description, sales_notes,
        composition,                    // cостав
        product_color,
        size,
        gred,
        packing_size,
        //individual_packing,             // индивидуальная упаковка
        //certificate,
        weight,                         // вес
        in_stock,                       // на складе
        type_of_package,                // Тип упаковки, DELIVERY_PACKAGE_TYPE
        composition_of_package;         // Состав упаковки, DELIVERY_PACKAGE
    public List<string> pictures;

    public List<string> pictures_to_str(IEnumerable<XElement> pics)
    {
        List<string> pics_st = new List<string>();
        foreach(XElement el in pics) pics_st.Add((string)el);

        return pics_st;
    }


    // создание короткого имени
    public string name_short(string name)
    {
        //Regex short_name = new Regex(",");
        //string[] rx_short_name = short_name.Split(name);
        //return rx_short_name[0];

        //  до запятой -----------------------------------------------
        Regex short_name = new Regex("^([^,])*");
        Match rx_short_name = short_name.Match(name);
        name = rx_short_name.Groups[0].Value;
        // -----------------------------------------------------------

        //  предлоги -------------------------------------------------

        string preposition = null, time_name = null;
        foreach (string str in Form1.prepositions) {
            preposition = @"(.*)(\s+" + str + @"\s+)|\s*\S*|($)";
            //preposition = @"^(.*)(\s+с\s+)|\s*\S*|($)";
            short_name = new Regex(preposition);
            rx_short_name = short_name.Match(name);
            time_name = name;
            name = rx_short_name.Groups[1].Value;
            if (name == "") name = time_name;
        }
        // -----------------------------------------------------------

        //  цифры ----------------------------------------------------
        short_name = new Regex(@"^(\D*)((\s+\S*\d+)|($))");
        rx_short_name = short_name.Match(name);/*(\s)**/
        name = rx_short_name.Groups[1].Value;
        // -----------------------------------------------------------


        //  стоп слова -----------------------------------------------
        string stop_word_regex = null;
        foreach (string str in Form1.stop_words)
        {
            stop_word_regex = @"^(.*)" + str;
            //stop_word_regex = @"^(.*)(\s+с\s+)|.*|($)";
            short_name = new Regex(stop_word_regex);
            rx_short_name = short_name.Match(name);
            time_name = name;
            name = rx_short_name.Groups[1].Value;
            if (name == "") name = time_name;
        }
        // -----------------------------------------------------------

        //  обрезка пробелов в конце строки --------------------------
        short_name = new Regex(@"^(\D*[^\s*$])");
        rx_short_name = short_name.Match(name);
        name = rx_short_name.Groups[1].Value;
        // -----------------------------------------------------------

        while (name.Length > 60)
        {
            short_name = new Regex(@"^(.*)\s+\S+");
            rx_short_name = short_name.Match(name);
            name = rx_short_name.Groups[1].Value;
        }

        return name;
    }

}

//[Serializable]
//public struct coefficient_of_package
//{
//    public int category_id;    public float coefficient_of_massa;     public float coefficient_of_dimensions;
//}


[Serializable]
public class configure
{
    public List<float[]>  coefficient       = new List<float[]>();
    public List<string[]> quality_correct   = new List<string[]>();
    public List<string>   exception_name    = new List<string>();
    public List<string[]> color             = new List<string[]>();
    public List<string[]> head_options      = new List<string[]>();     // список полей заголовка для замены на латиницу

    // ------------------------------ config for folder tre ------------------------------
    public bool tre_bool_mod_catalog = false;
    public bool tre_del_old_itm_bool = false;
    //public bool tre_easy_del_old_itm_bool = false;
    public List<string[]> tre_list_categoryes = new List<string[]>();   // спикок соотнесения категорий
    public string file_list_mod_catalog = "";                           // путь до фаила спикока соотнесения категорий
    public string tre_del_old_itm_count = "";                           // количество днеи до деактивации
    //public string tre_easy_del_old_itm_count = "";                      // количество днеи до деактивации, простой режим
    // ------------------------------ config for folder tre ------------------------------

    // --------------------------- сохранение id для easy mode ---------------------------
    public string save_ids_dir;
    public string get_ids_dir;
    // --------------------------- сохранение id для easy mode ---------------------------

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
    public string tre_folder;                       // основная папка с хмл, доп фаилами, результирующей папкой ...
    public string file_head_options;                // фаил со списоком полей заголовка для замены на латиницу

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

    public configure() {}
    public configure(string s) { mode = s; }
    public configure(string mode_local, Form1 f)
    {
        int x = 220, y = 107, x_t = 0, a_side = 35;
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
            if(Regex.IsMatch(line_csv_folder[index].Text, @"/$"))
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
                catch {
                    try
                    {
                        xml = new WebClient().DownloadString(f.line.line_xml_file[index].Text.ToString());
                    }
                    catch { MessageBox.Show("Ошибка чтения xml файла по url"); }
                    MessageBox.Show("Ошибка чтения xml файла");
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
        catch/*(BadDataException i)*/ {
            //MessageBox.Show(i.ToString());
            return null;
        }
    }
}

public class option_csv
{
    public string ID { get; set; }
    public int id;
    public string proizvoditel { get; set; }
    public string MATERIAL { get; set; }
    public string DESCRIPTION { get; set; }
    public string LENGTH_PACK { get; set; }
    public string WIDTH_PACK { get; set; }
    public string HEIGHT_PACK { get; set; }
    public string WEIGHT_V { get; set; }
    public string DELIVERY_PACKAGE_TYPE { get; set; }
    public string DELIVERY_PACKAGE { get; set; }
    public string WEIGHT { get; set; }
    public string PRODUCT_COLOR { get; set; }
}

//[Serializable]
//[DataContract]
public class save
{
    public configure e { get; set; }
    public configure f { get; set; }
    public Ssh ssh { get; set; }
    public save(/*file_line line, */configure easy, configure full, Ssh ssh_conf)
    {
        /*l = line; */e = easy; f = full; ssh = ssh_conf;
    }

    public void load()
    {
        //
    }
}

public class dl_url { public string href { get; set; } }