﻿using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace up
{
    public class functions
    {
        public Form1 f = new Form1();
        public functions() {}
        public functions(Form1 form)
        {
            f = form;
        }
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
            catch (WebException err)
            {
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
            try { answer = get.GetResponse(); }
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
            catch (WebException err)
            {
                MessageBox.Show("Ошибка загрузки");
            }
            //Thread.Sleep(rn);
            //cli.ShowDialog();

            return true;
        }
        public void set_settings(Ssh_form ssh, Form2 e, Form3 f3, save data, tre_folder_form tre_form_obj, description description_form)
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
                try { e.exclude_in_other_store.Checked = true; } catch { }

            if (data.e.get_min_sale)
                try { e.correction_of_quantity.Checked = true; } catch { }

            if (data.e.output_base_price)
                try { e.use_base_price.Checked = true; } catch { }

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
            if (data.tre_conf.get_ids_dir != null)
                e.tb_ids_folder.Text = data.tre_conf.get_ids_dir;
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
            }

            if (data.f.use_xml_description)
                try { f3.use_xml_description.Checked = true; } catch {}
            else if (data.f.use_option_description)
                try { f3.use_option_description.Checked = true; } catch {}
            else
            {
                try { f3.none_description.Checked = true; } catch {}
                data.f.none_description = true;
                data.f.use_xml_description = false;
                data.f.use_option_description = false;
            }

            if (data.f.no_watermark)
                try { f3.no_watermark.Checked = true; } catch {}

            if (data.f.use_short_name)
            {
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
                try { f3.color_YML.Checked = true; } catch { }

            if (data.f.file_colors != null && data.f.color.Count > 0)
            {
                f3.color.Items.Clear();
                f3.color.Items.Add(Path.GetFileName(data.f.file_colors));
            }

            if (data.f.color_from_YML)
                try { f3.color_from_YML.Checked = true; } catch { }
            if (data.tre_conf.tre_folder != "")
                tre_form_obj.tre_folder.Text = data.tre_conf.tre_folder;
            if (data.tre_conf.file_head_options != null)
            {
                tre_form_obj.options_lb.Items.Clear();
                tre_form_obj.options_lb.Items.Add(Path.GetFileName(data.tre_conf.file_head_options));
            }
            // ---------------------------------------- tree mode каталог ----------------------------------------
            if (data.tre_conf.tre_bool_mod_catalog)
            {
                tre_form_obj.label_mod_catalog.Enabled = true;
                tre_form_obj.list_mod_catalog.Enabled = true;
                try { tre_form_obj.bool_mod_catalog.Checked = true; } catch { }
            }
            else
            {
                tre_form_obj.label_mod_catalog.Enabled = false;
                tre_form_obj.list_mod_catalog.Enabled = false;
                try { tre_form_obj.bool_mod_catalog.Checked = false; } catch { }
            }
            if (data.tre_conf.tre_list_categoryes.Count > 0)
            {
                tre_form_obj.list_mod_catalog.Items.Clear();
                tre_form_obj.list_mod_catalog.Items.Add(Path.GetFileName(data.tre_conf.file_list_mod_catalog));
            }
            // ---------------------------------------- tree mode каталог ----------------------------------------
            // ---------------------------------------- tree del_old_itm -----------------------------------------
            if (data.tre_conf.tre_easy_del_old_itm_bool)
            {
                tre_form_obj.label22.Enabled = true;
                tre_form_obj.tb_easy_del_old_itm.Enabled = true;
                try { tre_form_obj.cb_easy_del_old_itm.Checked = true; } catch { }
            }
            else
            {
                tre_form_obj.label22.Enabled = false;
                tre_form_obj.tb_easy_del_old_itm.Enabled = false;
                try { tre_form_obj.cb_easy_del_old_itm.Checked = false; } catch { }
            }
            if (data.tre_conf.tre_easy_del_old_itm_count != "")
                tre_form_obj.tb_easy_del_old_itm.Text = data.tre_conf.tre_easy_del_old_itm_count;

            if (data.tre_conf.tre_full_del_old_itm_bool)
            {
                tre_form_obj.lb_deact_full.Enabled = true;
                tre_form_obj.tb_full_del_old_itm.Enabled = true;
                try { tre_form_obj.cb_full_del_old_itm.Checked = true; } catch { }
            }
            else
            {
                tre_form_obj.lb_deact_full.Enabled = false;
                tre_form_obj.tb_full_del_old_itm.Enabled = false;
                try { tre_form_obj.cb_full_del_old_itm.Checked = false; } catch { }
            }
            if (data.tre_conf.tre_full_del_old_itm_count != "")
                tre_form_obj.tb_full_del_old_itm.Text = data.tre_conf.tre_full_del_old_itm_count;
            // ---------------------------------------- tree del_old_itm -----------------------------------------
            // -------------------------------------------- описание ---------------------------------------------
            if (data.d_s.lines_description != null && data.d_s.lines_description.Count > 0)
                f.description_form.rtb_description.Text = string.Join("\r\n", data.d_s.lines_description);
            if (data.d_s.file_name_options != null && data.d_s.file_name_options != "")
                description_form.l_bo.Items.Add(data.d_s.file_name_options);
            // -------------------------------------------- описание ---------------------------------------------
            // ---------------------------------------------------------- full ----------------------------------------------------------

        }
        static public void clear_configure(string mode, Form1 f)
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
            if (mode == "tre_folder")
                f.tre_conf = new tre_save();
            if (mode == "desc")
                f.desc_save = new description_save();

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
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    Encoding = Encoding.GetEncoding(1251),
                    //var info = new List<string>();
                    BadDataFound = data => {
                        //info.Add(data.RawRecord);
                    },
                };
                using (var reader = new StreamReader(file_options, Encoding.GetEncoding(1251)))
                using (var csv = new CsvReader(reader, config))
                {
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
            cfg.options = f.tre_conf.head_options;
            cfg.prepositions = Form1.prepositions;
            //cfg.stop_words = Form1.stop_words;
            cfg.coefficients = f.full.coefficient;
            cfg.coefficients_volume_and_mass = f.full.coefficient_package_mass;

            string[] files_xml = {};
            try
            {
                files_xml = Directory.GetDirectories(f.tre_conf.tre_folder + "\\XML");
            }
            catch { MessageBox.Show("Не правильный путь к основной папке"); }

            bool th = ThreadPool.SetMaxThreads(f.threads, f.threads);


            foreach (string dir in files_xml)
            {
                object[] hi = { "" };
                try
                {
                    object[] hi_tmp = { Directory.GetFiles(dir).First(), dir + "\\Tmp_files", dir + "\\Dop_file", cfg, "cfg", f.desc_save.lines_description, f.desc_save.options };
                    hi = hi_tmp;
                    object i = new object();
                    lock(i) {
                        ThreadPool.QueueUserWorkItem(f.stl.make_op, hi);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка формирования доп файла, файл xml:" + Convert.ToString(hi[0]));
                }
                //Thread.Sleep(14000);
            }
        }
        public void tre_threads_offer(string type)
        {
            //string type = "full";
            string tre_folder = f.tre_conf.tre_folder;
            string[] tre_xml_dirs = { };
            try { tre_xml_dirs = Directory.GetDirectories(f.tre_conf.tre_folder + "\\XML"); }
            catch
            {
                MessageBox.Show("Укажите правильный путь до корневой папки");
                return;
            }
            string[] tre_xmls = new string[tre_xml_dirs.Length];
            string[] tre_ops = new string[tre_xml_dirs.Length];
            for (int i = 0; i < tre_xml_dirs.Length; i++)
            {
                try
                {
                    tre_xmls[i] = Directory.GetFiles(tre_xml_dirs[i])[0];
                }
                catch { continue; }

                tre_ops[i] = tre_xml_dirs[i] + "\\Dop_file\\";
                try { tre_ops[i] += Path.GetFileName(Directory.GetFiles(tre_ops[i])[0]); }
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
        public string[] get_options_ids(List<Options_up> op)
        {
            string[] op_ids;
            try { op_ids = new string[op.Count]; }
            catch { return null; }

            for (int i = 0; i < op.Count; i++)
                op_ids[i] = op[i].artnumber;

            return op_ids;
        }
        public string[] get_urls(string[] xml_ids, string[] option_ids, List<xml_offer> offers)
        {
            string[] urls = xml_ids.Except(option_ids).ToArray();

            if (urls == null)
                return null;

            string url/*, id_op*/;
            Regex r_url = new Regex(@"(.*/\d+)/");

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
            string url = null;
            try { url = File.ReadAllText(file_name); }
            catch { return null; }

            Regex get_url = new Regex("URL=(.*)\r\n");
            url = get_url.Match(url).Groups[1].Value;

            return url;
        }
        public void live(object obj)
        {
            object[] inf = obj as object[];
            Form1 this_form = (Form1)inf[2];
            string time;
            double u_time, hi;
            const double day = 86400;
            double start_time = Convert.ToDouble(inf[0]), day_live = Convert.ToDouble(inf[1]);
            Regex reg_u_time = new Regex("unixtime:\\s*(\\d*)");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            WebClient wc = new WebClient();
            try
            {
                time = wc.DownloadString("http://worldtimeapi.org/api/timezone/Asia/Kamchatka.txt");
                u_time = Convert.ToInt32(reg_u_time.Match(time).Groups[1].Value);
                //this_form.richTextBox2.Text = u_time.ToString();
                //this_form.richTextBox2.Invoke((MethodInvoker)(() => this_form.richTextBox2.Text += u_time.ToString() + "\r\n"));
                hi = (u_time - start_time) / day;

                if (hi > day_live || u_time < start_time)
                    this_form.Invoke((MethodInvoker)(() => this_form.Close()));
            }
            catch {}
        }
        public static int get_id(string url)
        {
            Regex r_id = new Regex("(.*)\\/(\\d*)");
            try
            {
                Match m_id = r_id.Match(url);

                return Convert.ToInt32(m_id.Groups[2].Value);
            }
            catch{}

            return 0;
        }

        public static string[,] get_sim_to_ch(string file)
        {
            try
            {
                string txt = File.ReadAllText(file, Encoding.UTF8); txt += "\r\n";
                string[] lines = Regex.Matches(txt, "(.*)\\r\\n").Cast<Match>().Select(l => l.Value.Trim()).ToArray();
                string[] ch = new string[2];
                int sum = 0;
                List <string[]> l_sim_to_ch = new List<string[]>();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == "") continue;
                    sum++;
                    ch[0] = Regex.Match(lines[i], "(^\\S*)\\|").Groups[1].Value;
                    ch[1] = Regex.Match(lines[i], @"\S*\|(\S+)$").Groups[1].Value;
                    l_sim_to_ch.Add(new string[] { ch[0], ch[1] });
                }

                string[,] sim_to_ch = new string[sum, 2];
                for (int i = 0; i < l_sim_to_ch.Count; i++)
                {
                    sim_to_ch[i, 0] = l_sim_to_ch[i][0];
                    sim_to_ch[i, 1] = l_sim_to_ch[i][1];
                }

                return sim_to_ch;
            }
            catch { MessageBox.Show("Не удалось прочитать символы подстановки"); return null; }
        }

        public static string utf_to_asci(string s, string [,] temls)
        {
            for (int i = 0; i < temls.Length / 2; i++)
                s = s.Replace(temls[i, 0], temls[i, 1]);

            return s;
        }

        // clear form
        static public void ctrl(Control cont)
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
        static public void ctrls(Control.ControlCollection cont)
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
    }
}