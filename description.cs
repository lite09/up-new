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
            form1.full.description = tb_description.Text;
        }
    }
}
