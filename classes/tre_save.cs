using System.Collections.Generic;

namespace up
{
    public class tre_save
    {
        //tre_save(Form1 form1) {}
        // ------------------------------ config for folder tre ------------------------------
        public string tre_folder;                                           // основная папка с хмл, доп фаилами, результирующей папкой ...
        public bool tre_bool_mod_catalog = false;
        public bool tre_easy_del_old_itm_bool = false;
        public bool tre_full_del_old_itm_bool = false;
        //public bool tre_easy_del_old_itm_bool = false;
        public List<string[]> head_options = new List<string[]>();   // список полей заголовка для замены на латиницу
        public List<string[]> tre_list_categoryes = new List<string[]>();   // спикок соотнесения категорий
        public string file_head_options;                                    // фаил со списоком полей заголовка для замены на латиницу
        public string file_list_mod_catalog = "";                     // путь до фаила спикока соотнесения категорий
        public string tre_full_del_old_itm_count = "";                     // количество днеи до деактивации, обычныи режим
        public string tre_easy_del_old_itm_count = "";                     // количество днеи до деактивации, простой режим
                                                                           // ------------------------------ config for folder tre ------------------------------

        // --------------------------- сохранение id для easy mode ---------------------------
        public string save_ids_dir;
        public string get_ids_dir;
        // --------------------------- сохранение id для easy mode ---------------------------
    }
}
