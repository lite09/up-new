//using CsvHelper;
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
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using stl;
//using System.Security.Cryptography;

namespace up
{

    public partial class Form1 : Form
    {
        List<Thread> th_s = new List<Thread>();
        const double day = 86400, start_time = 1608531900, day_live = 9; // 1607608999, 1607609202
        //int hi_time = 90;

        public Form_stl stl = new Form_stl();
        public Form2 set_easy;
        public Form3 set_full;
        public tre_folder_form tre_folder_form_obj;
        public Ssh_form ssh_form;
        public description description_form;

        public functions f;

        public tre_save tre_conf;

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
            DateTimeOffset dto = DateTimeOffset.Now; long start = dto.ToUnixTimeSeconds();  // отслеживание времени

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
                string ids_file = tre_conf.get_ids_dir + "\\" + Path.GetFileNameWithoutExtension(line.line_xml_file[int_index].Text.ToString()) + ".txt";
                if(type == "easy")
                    param = offer_min(new StringReader(xml), easy, ids_file);
                else
                    param = offer(new StringReader(xml), full, options_csv);

                List<xml_offer> p = param.ToList();
                //dto = DateTimeOffset.Now; long endl = dto.ToUnixTimeSeconds();

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
                    if (ssh_conf.on)
                        richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += "ssh: "));

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

            string save_folder = tre_conf.tre_folder + "\\Result";

            if (file_xml == "") return;
            
            if (type == "full") options_csv = fn.take_options(file_options);

            // xml, папка для сохранения результирующего фаила, удаление старых товаров(checkbox), сроки деактивации, соотнесение категории, папка с фаилами описания дополнительных полеи
            string folder_options = "";
            try   { folder_options = Path.GetDirectoryName(file_options); }
            catch {}
            string[] line_info;
            if (type == "full") {
                string[] line_info_time = { file_xml, save_folder, tre_conf.tre_full_del_old_itm_bool.ToString(), tre_conf.tre_full_del_old_itm_count, "", folder_options };
                line_info = line_info_time;
            }
            else
            {
                string[] line_info_time = { file_xml, save_folder, tre_conf.tre_easy_del_old_itm_bool.ToString(), tre_conf.tre_easy_del_old_itm_count, "", folder_options };
                line_info = line_info_time;
            }

            IEnumerable<xml_offer> param = null;
            DateTimeOffset dto = DateTimeOffset.Now; long start = dto.ToUnixTimeSeconds();  // отслеживание времени

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
                    try
                    {
                        xml = wc.DownloadString(url);
                    }
                    catch
                    {
                        richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += "Не удалось загрузить фаил " + url + "\r\n"));

                        return;
                    }
                }
                if (type == "easy")
                    param = offer_min(new StringReader(xml), easy, file_ids);
                else
                    param = offer(new StringReader(xml), full, options_csv);


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
                    if (ssh_conf.on)
                        richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += "ssh: "));

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
                                XElement el;
                                try
                                {
                                    el = XNode.ReadFrom(reader) as XElement;
                                    if (el == null)
                                        continue;
                                }
                                catch { continue; }
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

            bool categories = false;
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
                    if (rb_auto.Checked && tre_conf.tre_list_categoryes.Count > 0)
                    {
                        if (!mod_cat(ref tre_conf.tre_list_categoryes, offer)) offer.categoryId = "999999";
                    }
                    else if (categories)
                    {
                        if (!mod_cat(ref mod_catalog, offer)) offer.categoryId = "999999";
                    }
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

            if (rb_manual.Checked && tre_conf.get_ids_dir != null && mode.mode == "full")
            {
                string name = Path.GetFileNameWithoutExtension(line_info[0]) + ".txt";
                File.WriteAllText(tre_conf.get_ids_dir + "\\" + name, sb_time.ToString());
            }
            if (rb_auto.Checked && ids_easy_dir != "" && tre_folder_form_obj.tre_folder.Text != "" )
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
                    string copy_args = "-pw " + ssh_conf.pass + " -l " + ssh_conf.login + " \"" + xml_name + "\" " + ssh_conf.host + ":" + ssh_conf.save_folder;
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
                        //richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += "ssh: "));

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

                                //xml_offer offer = new xml_offer();
                                XElement el;
                                xml_offer offer = new xml_offer();
                                try
                                {
                                    el = XNode.ReadFrom(reader) as XElement;
                                    if (el == null)
                                        continue;
                                }
                                catch { continue; }

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
                                xml_offer offer = new xml_offer(); XElement el;
                                try
                                {
                                    el = XNode.ReadFrom(reader) as XElement;
                                    if (el == null)
                                        continue;
                                }
                                catch { continue; }

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

                                //  -------------------------------- описание -----------------------------------------
                                if (mode.use_xml_description)
                                {
                                    offer.description = el.Element("description").Value;
                                    offer.description = offer.description.Replace(";", " ");
                                    offer.description = offer.description.Replace("\r\n", " ");
                                    offer.description = offer.description.Replace("\n", " ");
                                }
                                else if (mode.use_option_description)
                                {
                                    try
                                    {
                                        offer.description = option.description;
                                    }
                                    catch { offer.description = ""; }
                                }
                                //  -------------------------------- описание -----------------------------------------


                                if (!mode.data_in_csv || option == null)
                                {
                                    try { offer.vendor = el.Element("vendor").Value; }
                                    catch { offer.vendor = ""; }
                                    i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Состав");
                                    offer.composition = (string)i.FirstOrDefault();
                                    //offer.description = el.Element("description").Value;
                                    //offer.description = offer.description.Replace(";", " ");
                                    //offer.description = offer.description.Replace("\n", " ");
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

                                    //if (mode.use_xml_description)
                                    //{
                                    //    offer.description = el.Element("description").Value;
                                    //    offer.description = offer.description.Replace(";", " ");
                                    //}
                                    //else if (mode.use_option_description)
                                    //{
                                    //    offer.description = option.description;
                                    //}
                                    //else
                                    //{
                                    //    if (option.description != "")
                                    //    {
                                    //        offer.description = option.description;
                                    //        offer.description = offer.description.Replace(";", " ");
                                    //        offer.description = offer.description.Replace("\r\n", "");
                                    //    }
                                    //    else
                                    //    {
                                    //        offer.description = el.Element("description").Value;
                                    //        offer.description = offer.description.Replace(";", " ");
                                    //    }
                                    //}

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
            set_easy.Focus();
        }

        private void ПраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            set_full.Show();
            set_full.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            save save_obj = new save(easy, full, ssh_conf, tre_conf);
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
            timer2.Enabled = true;
            DateTimeOffset dto = DateTimeOffset.Now; long now = dto.ToUnixTimeSeconds();
            double hi = (now - start_time) / day;
            if (hi > day_live || now < start_time)
                Close();

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(threads, threads);

            try     { token = File.ReadAllText("token.txt"); }
            catch   { token = ""; MessageBox.Show("Укажите токен в файле token.txt"); }

            f = new functions(this);

            set_easy = new Form2(this);
            set_full = new Form3(this);
            ssh_form = new Ssh_form(this);
            tre_folder_form_obj = new tre_folder_form(this);
            description_form = new description(this);

            line = new file_line(this);

            tre_conf = new tre_save();

            full = new configure("full", this);
            easy = new configure("easy", this);
            conf_options = new configure("options", this);

            string save_json;
            try     { save_json = File.ReadAllText("save.json"); }
            catch   { save_json = "";  }

            try {
                if (save_json != "")
                {
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

                        tre_conf = save_obj.tre_conf;

                        f.set_settings(ssh_form, set_easy, set_full, save_obj, tre_folder_form_obj);

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

            save save_obj = new save(easy, full, ssh_conf, tre_conf);
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

                        tre_conf = save_obj.tre_conf;

                        f.set_settings(ssh_form, set_easy, set_full, save_obj, tre_folder_form_obj);
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

        // clear form
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
            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void rb_auto_CheckedChanged(object sender, EventArgs e)
        {
            btn_make_tables.Enabled = true;
            btn_make_options.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
        }

        private void настройкиSshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ssh_form.Show(); //ssh_form.Visible = true;
            ssh_form.Focus();
        }

        private void структураПапокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tre_folder_form_obj.Show(); //tre_folder_form_obj.Visible = true;
            tre_folder_form_obj.Focus();
        }

        private void описаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            description_form.Show();
            //description_form.TopMost = true;
            //description_form.TopMost = false;
            description_form.Focus();
        }

        private void cb_ssh_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_ssh.Checked)
                ssh_conf.on = true;
            else
                ssh_conf.on = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            string time;
            double u_time;
            Regex reg_u_time = new Regex("unixtime:\\s*(\\d*)");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebClient wc = new WebClient();
            try
            {
                time = wc.DownloadString("http://worldtimeapi.org/api/timezone/Asia/Kamchatka.txt");
                u_time = Convert.ToInt32(reg_u_time.Match(time).Groups[1].Value);

                //u_time -= 7 * day;
                double hi = (u_time - start_time) / day;
                if (hi > day_live || u_time < start_time)
                    Close();
            }
            catch {}
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


public class dl_url { public string href { get; set; } }