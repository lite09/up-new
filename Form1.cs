using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using up;


namespace up
{

    public partial class Form1 : Form
    {
        public Form2 set_easy = new Form2();

        public configure easy = new configure();
        public configure full = new configure();
        public file_line line;


        public string global_xml = null;
        public List<string> exception_name = new List<string>();
        public uint exception_any_side = 0, exception_sum_side = 0, exception_weight = 0;
        readonly int CPU = Environment.ProcessorCount;
        int threads = 1;

        public static List<string> prepositions = new List<string> {
            "A"/*латиница*/, "А"
        };
        public static List<string> stop_words = new List<string>
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
            richTextBox2.Text = "";

            if ((line.line_xml_file[0].Text == "") || (easy.exception_rules_xml == null))
            {
                MessageBox.Show("Установите поля с данными xml файлов и фильтра исключений");
                return;
            }

            multi_offer(threads);

        }

        public void multi_offer(int threads)
        {
            int count = 0;
            List<Thread> th_s = new List<Thread>();
            var th = ThreadPool.SetMaxThreads(threads, threads);

            for(int i = 0; i < line.line_xml_file.Count; i++)
            {
                //if (count < threads)
                //{
                    Thread th_time = new Thread(new ParameterizedThreadStart(thead_offer));
                th_s.Add(th_time);
                ThreadPool.QueueUserWorkItem(thead_offer, i);
                    //th_s[i].Start(i);
                //}
                //else
                //{
                //    while (count >= threads) Thread.Sleep(70);
                //    i--;
                //}
                //count++;
            }

        }

        public void thead_offer(object index)
        {
            int int_index = (int)index;
            if (line.line_xml_file[int_index].Text == "") return;
            string xml = "";
            try { xml = File.ReadAllText(line.line_xml_file[int_index].Text.ToString(), Encoding.UTF8); }
            catch { MessageBox.Show("Ошибка чтения xml файла"); }

;
            string[] line_info = { line.line_xml_file[int_index].Text.ToString(), line.line_csv_save_file[int_index].Text.ToString(),
                                    line.line_old_itms_remove[int_index].Checked.ToString(), line.line_data_Live[int_index].Text.ToString()};
            IEnumerable<xml_offer> param = null;
            if (xml != "")
            {
                param = offer_min(new StringReader(xml), easy);

                DateTimeOffset dto = DateTimeOffset.Now;
                long start = dto.ToUnixTimeSeconds();

                List<xml_offer> p = param.ToList();
                param = null;
                object am = new object();

                xml = null;

                processing_min(ref p, easy, line_info);

                dto = DateTimeOffset.Now;
                long end = dto.ToUnixTimeSeconds();

                lock (am)
                {
                    richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text += "count " + p.Count + "\t" +
                    "время " + (int)((end - start) / 60) + ":" + (end - start) % 60 + "\r\n"));
                }

                p.Clear();
                //i = 0;
            }
        }


        IEnumerable<xml_offer> offer_min(StringReader string_xml, configure mode)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(string_xml, settings))
            {

                try     { reader.MoveToContent(); }
                catch   { MessageBox.Show("Не верный формат файла xml"); }
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

                                offer.name = el.Element("name").Value;

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер упаковки");
                                offer.packing_size = (string)i.FirstOrDefault();

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Вес");
                                offer.weight = (string)i.FirstOrDefault();


                                if (offer.packing_size == null)
                                    continue;

                                //  --------------------------- фильтр исключений -------------------------------------
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


                                if ((a >= exception_any_side) || (b >= exception_any_side) || (c >= exception_any_side))
                                    continue;
                                if ((a + b + c) >= exception_sum_side)
                                    continue;
                                //  --------------------------- фильтр исключений -------------------------------------


                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------       масса       -------------------------------------
                                if (Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture) >= exception_weight)
                                    continue;
                                //  --------------------------- фильтр исключений -------------------------------------



                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------        имя        -------------------------------------
                                string exp = "";
                                foreach (string name in exception_name)
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
                                //  --------------------------- фильтр исключений -------------------------------------



                                offer.id = el.Attribute("id").Value;
                                offer.categoryId = el.Element("categoryId").Value;
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

        void processing_min(ref List<xml_offer> offers, configure mode, string[] line_info)
        {
            int i = 0;
            float coeff = 1;
            string line_csv = null, date = null;
            string
                line_head = "\"ID\";ID_CATEGORY;AVAILABLE;QUANTITY;MIN_QUANTITY";
            StringBuilder sb = new StringBuilder();


            if (easy.output_base_price)
                line_head += ";BASE_PRICE;time_price;PRICE";
            else
                line_head += ";PRICE";

            // задается параметром
            if (line_info[2] == "True")
            {
                line_head += ";DATE_OF_CREATION;DEACTIVATION_PERIOD";
                date = DateTime.Now.ToString("dd/MM/yyyy");
            }

            sb.AppendLine(line_head);

            foreach (xml_offer offer in offers)
            {
                if (mode.prefix_for_id != null) {
                    offer.id = mode.prefix_for_id + offer.id;
                }

                //  --------------------------  минимальное количество  --------------------------
                if (mode.quantity_change)
                {
                    Regex num = new Regex(@"\S+\s+(\d+)\s*шт");
                    Match num_m = num.Match(offer.sales_notes);
                    if (num_m.Success)
                    {
                        int num_int = Convert.ToInt32(num_m.Groups[1].Value);
                        if (num_int > 1)
                        {
                            offer.price_time = offer.price * num_int;
                            //offer.name += ", " + num_int + " шт. в комплекте";
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
                foreach (string[] str in easy.quality_correct)
                    if (offer.in_stock == str[0])
                    {
                        offer.in_stock = str[1];
                        break;
                    }
                //  -------------------------  корректировка количества  -------------------------

                //  --------------------------     вывод информации     --------------------------
                if (easy.output_base_price)
                {
                    line_csv = offer.id + ";" + offer.categoryId + ";" + offer.available + ";" + offer.in_stock + ";;" +
                        offer.price + ";" + offer.price_time + ";" + offer.price_new;
                    //line_csv = string.Format("{0};{1};{2};{3};;{4};{5};{6}",
                    //    offer.id, offer.categoryId, offer.available, offer.in_stock, offer.price, offer.price_time, offer.price_new);
                }
                else
                {
                    line_csv = offer.id + ";" + offer.categoryId + ";" + offer.available + ";" + offer.in_stock + ";;" +
                        offer.price_new;
                    //line_csv = string.Format("{0};{1};{2};{3};;{4}",
                    //    offer.id, offer.categoryId, offer.available, offer.in_stock, offer.price_new);
                }

                if (line_info[2] == "True")
                    line_csv += ";" + date + ";" + line_info[3];

                sb.AppendLine(line_csv);
                //  --------------------------     вывод информации     --------------------------

            }
            //File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Grid.csv");
            // исключение

            string xml_name = line_info[0];
            Regex short_name = new Regex(@"(.*)\\((.*)\.)");
            Match rx_short_name = short_name.Match(xml_name);
            xml_name = line_info[1] + "" + rx_short_name.Groups[3].Value;
            xml_name += ".csv";

            File.WriteAllText(xml_name, sb.ToString(), Encoding.Default);

        }

        IEnumerable<xml_offer> offer(StringReader string_xml, configure mode)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (XmlReader reader = XmlReader.Create(string_xml, settings))
            {


                reader.MoveToContent();
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

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер упаковки");
                                offer.packing_size = (string)i.FirstOrDefault();

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Вес");
                                offer.weight = (string)i.FirstOrDefault();

                                offer.name = el.Element("name").Value;


                                if (offer.packing_size == null)
                                    continue;

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
                                Regex side_of_pkg = new Regex(@"^(\S+)\s*см\s*\S\s*(\S+)\s*см\s*\S\s*(\S+)");
                                Match rx_short_name = side_of_pkg.Match(offer.packing_size);

                                // или continue
                                if ((rx_short_name.ToString() == "") && (mode.mode == "easy"))
                                    continue;

                                if (rx_short_name.ToString() == "")
                                {
                                    side_of_pkg = new Regex(@"^(\S+)\s*см\s*\S\s*(\S+)\s*");
                                    rx_short_name = side_of_pkg.Match(offer.packing_size);
                                    if (rx_short_name.ToString() == "")
                                        continue;
                                    a = Convert.ToSingle(rx_short_name.Groups[1].Value);
                                    b = Convert.ToSingle(rx_short_name.Groups[2].Value);
                                    c = 10;
                                }
                                else
                                {
                                    a = Convert.ToSingle(rx_short_name.Groups[1].Value);
                                    b = Convert.ToSingle(rx_short_name.Groups[2].Value);
                                    c = Convert.ToSingle(rx_short_name.Groups[3].Value);
                                }



                                if ((a >= exception_any_side) || (b >= exception_any_side) || (c >= exception_any_side))
                                    continue;
                                if ((a + b + c) >= exception_sum_side)
                                    continue;
                                //  --------------------------- фильтр исключений -------------------------------------


                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------       масса       -------------------------------------
                                if (Convert.ToSingle(offer.weight, CultureInfo.InvariantCulture) >= exception_weight)
                                    continue;
                                //  --------------------------- фильтр исключений -------------------------------------



                                //  --------------------------- фильтр исключений -------------------------------------
                                //if (mode.check_delivery_options) {}
                                //  --------------------------- фильтр исключений -------------------------------------




                                //  --------------------------- фильтр исключений -------------------------------------
                                //  ---------------------------        имя        -------------------------------------
                                string exp = "";
                                foreach (string name in exception_name)
                                {
                                    exp = @"^.*(" + name + ").*";
                                    Regex exp_name = new Regex(exp);

                                    if (exp_name.IsMatch(offer.name))
                                    {
                                        //richTextBox1.Text = richTextBox1.Text + "\r\n" + offer.name;
                                        //richTextBox2.Text = richTextBox2.Text + "\r\n" + name;
                                        continue;
                                    }
                                }

                                //  --------------------------- фильтр исключений -------------------------------------



                                offer.id = el.Attribute("id").Value;
                                offer.available = el.Attribute("available").Value;
                                offer.url = el.Element("url").Value;
                                offer.price = Convert.ToSingle(el.Element("price").Value, CultureInfo.InvariantCulture);
                                offer.currencyId = el.Element("currencyId").Value;
                                offer.categoryId = el.Element("categoryId").Value;
                                offer.delivery = el.Element("delivery").Value;
                                if ((mode.mode == "full") && (mode.use_short_name))
                                {
                                    offer.short_name = offer.name_short(offer.name);
                                    if (mode.add_articule_to_short_name) offer.short_name += ", арт. " + offer.id;
                                }
                                try { offer.vendor = el.Element("vendor").Value; }
                                catch { offer.vendor = ""; }
                                offer.vendorCode = el.Element("vendorCode").Value;
                                offer.description = el.Element("description").Value;
                                offer.sales_notes = el.Element("sales_notes").Value;

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Состав");
                                offer.composition = (string)i.FirstOrDefault();

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Размер");
                                offer.size = (string)i.FirstOrDefault();



                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Индивидуальная упаковка");
                                offer.individual_packing = (string)i.FirstOrDefault();

                                i = el.Elements("param").Where(e => (string)e.Attribute("name") == "Сертификат");
                                offer.certificate = (string)i.FirstOrDefault();



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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Xml_file_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            threads = Convert.ToInt32(CPU_get.SelectedItem.ToString());
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
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.GetMinThreads(out minWorker, out minIOC);

            set_easy.Owner = this;
            line = new file_line(this);

            full = new configure();
            full.mode = "full";
            easy = new configure();
            easy.mode = "easy";

            get_stop_words();
            for (int i = 1; i <= CPU; ++i)
                CPU_get.Items.Add(i);


            //int iam, am;
            //ThreadPool.SetMaxThreads(9, 7);
            //ThreadPool.GetMaxThreads(out iam, out am);
            //MessageBox.Show(iam.ToString());
        }
    }
}

class xml_offer
{

    public string id;
    public int delivery_days;
    public float price, price_time, price_new;
    public string available, url, currencyId, categoryId, delivery, name, short_name, vendor, vendorCode, description, sales_notes,
        composition,        // cостав
        size,
        packing_size,
        individual_packing, // индивидуальная упаковка
        certificate,
        weight,             // вес
        in_stock;           // на складе
    public List<string> pictures;

    public List<string> pictures_to_str(IEnumerable<XElement> pics)
    {
        List<string> pics_st = new List<string>();
        foreach(XElement el in pics) pics_st.Add((string)el);

        return pics_st;
    }

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

class change
{
    class csv
    {

    }

    void load_csv(List<xml_offer> offres) {

    }
}

public class configure
{
    public List<float[]> coefficient = new List<float[]> {};
    public List<string[]> quality_correct = new List<string[]> {};
    public string mode;
    public string prefix_for_id;
    public string file_to_create_new_price;
    public string file_to_create_new_quality;
    public string exception_rules_xml;
    public bool data_in_csv;
    public bool use_short_name;
    public bool add_articule_to_short_name;
    public bool check_delivery_options;
    public bool get_min_sale;
    public bool output_base_price;
    public bool quantity_change;
    public bool use_no_watermark;
    public bool use_xml_description;    // вкл - описание берется из xml и csv, выкл - только csv
    public bool use_xml_color;

    public bool date_of_deactivation;
    public int duration_of_deactivation;
}

public partial class file_line
{
    public List<TextBox> line_xml_file;
    public List<TextBox> line_csv_file;
    public List<TextBox> line_csv_save_file;
    public List<CheckBox> line_old_itms_remove;
    public List<TextBox> line_data_Live;
    public List<ListBox> line_modification_categories_file;
    public List<string> line_modification_categories;
    Button bt;
    int line_count, line_step = 30, x_start = 15;

    public file_line(Form1 f)
    {
        line_xml_file = new List<TextBox>();
        line_csv_file = new List<TextBox>();
        line_csv_save_file = new List<TextBox>();
        line_old_itms_remove = new List<CheckBox>();
        line_data_Live = new List<TextBox>();
        line_modification_categories_file = new List<ListBox>();
        line_modification_categories = new List<string>();
        line_count = 0;

        bt = new Button();
        bt.Location = new Point(x_start, 275);
        bt.Text = "Добавить";
        bt.Click += (a, e) =>
        {
            add_line(f);
        };
        f.Controls.Add(bt);

        add_line(f);

    }
    public void add_line(Form1 f)
    {
        TextBox[] tb_main = new TextBox[3];
        int x = x_start, y = 273 + line_count * line_step, i = 0;

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

            tb_i.DragEnter += (a, e) =>
            {
                e.Effect = DragDropEffects.All;
            };

            if (i == 0)
                line_xml_file.Add(tb_i);
            else if (i == 1)
                line_csv_file.Add(tb_i);
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
                cb.CheckedChanged += (a, e) =>
                {
                    if (cb.Checked)
                        f.easy.date_of_deactivation = true;
                    else
                        f.easy.date_of_deactivation = false;
                };
                line_old_itms_remove.Add(cb);
            }
            else
            {
                tb.Location = new Point(x, y);    // y = 273
                tb.Size = new Size(27, 20);
                tb.Leave += (a, e) =>
                {
                    try
                    {
                        f.easy.duration_of_deactivation = Convert.ToInt32(tb.Text.ToString().Trim());
                    }
                    catch
                    {
                        f.easy.duration_of_deactivation = 0;
                    }
                };
                line_data_Live.Add(tb);
            }

            f.Controls.Add(cb);
            f.Controls.Add(tb);
            i++;
        }

        ListBox lb = new ListBox();
        lb.Location = new Point(867, y - 8);      // y = 265
        lb.Size = new Size(157, 28);
        lb.BorderStyle = BorderStyle.FixedSingle;
        lb.AllowDrop = true;
        lb.DragEnter += (a, e) => { e.Effect = DragDropEffects.All; };
        lb.DragDrop += (a, e) => {
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string name;
            Regex short_name = new Regex(@"(.*)(\\)((.*)*$)");
            Match rx_short_name = short_name.Match(file_name[0]);
            name = rx_short_name.Groups[3].Value;
            lb.Items.Clear();
            lb.Items.Add(name);
            line_modification_categories.Add(file_name[0]);
        };

        line_modification_categories_file.Add(lb);
        f.Controls.Add(lb);

        bt.Location = new Point(x_start, Convert.ToInt32(bt.Location.Y.ToString()) + line_step);
        f.Height += line_step;
        line_count++;
    }
}

