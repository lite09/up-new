using System.Collections.Generic;
using System.IO;
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

        private void tb_description_Leave(object sender, System.EventArgs e)
        {
            form1.full.description = rtb_description.Text;
        }

        private void l_bo_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void l_bo_DragDrop(object sender, DragEventArgs e)
        {
            string[] file_name = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //List<string> options = new List<string>();
            int i = File.ReadAllLines(file_name[0]).Length;
            form1.desc_save.options = new string[i];

            string name = Path.GetFileName(file_name[0]);
            form1.desc_save.file_name_options = name;
            l_bo.Items.Clear();
            l_bo.Items.Add(name);
        }
    }
}
