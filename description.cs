using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace up
{
    public partial class description : Form
    {
        Form1 form1;

        public description(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }

        private void ok_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
        }

        private void l_bo_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void l_bo_DragDrop(object sender, DragEventArgs e)
        {
            //List<string> options = new List<string>();

            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            form1.desc_save.options = File.ReadAllLines(file_name[0]);

            string name = Path.GetFileNameWithoutExtension(file_name[0]);
            form1.desc_save.file_name_options = name;
            l_bo.Items.Clear();
            l_bo.Items.Add(name);
        }

        private void rtb_description_Leave(object sender, System.EventArgs e)
        {
            string[] description_teplate = rtb_description.Lines;
            form1.desc_save.lines_description = description_teplate.ToList();

            //Regex getline = new Regex(@"(<.[^/]*/\S[^<]+>*)");
            //MatchCollection lines = Regex.Matches(description_teplate, getline.ToString(), RegexOptions.Multiline);
            //form1.desc_save = lines.Cast<Match>().Select(m => m.Value.Trim()).ToList();
        }

        private void clear_Click(object sender, System.EventArgs e)
        {
            foreach (Control control in Controls)
            {
                functions.ctrl(control);
                if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    foreach (Control item in gb.Controls)
                        functions.ctrl(item);
                }
            }

            functions.clear_configure("desc", form1);
        }
    }
}
