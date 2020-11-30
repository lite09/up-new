using System;
using System.Windows.Forms;


namespace up
{
    public partial class Ssh_form : Form
    {
        Form1 f;
        public Ssh_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void login_Leave(object sender, EventArgs e)
        {
            f.ssh_conf.login = login.Text;
        }

        private void pass_Leave(object sender, EventArgs e)
        {
            f.ssh_conf.pass = pass.Text;
        }

        private void host_Leave(object sender, EventArgs e)
        {
            f.ssh_conf.host = host.Text;
        }

        private void port_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(port.Text, out _))
                f.ssh_conf.port = port.Text;
            else
                f.ssh_conf.port = port.Text = "";
        }

        private void save_folder_Leave(object sender, EventArgs e)
        {
            f.ssh_conf.save_folder = save_folder.Text;
        }

        private void ssh_form_Load(object sender, EventArgs e)
        {
            f = (Form1)this.Owner;
        }
    }
}
