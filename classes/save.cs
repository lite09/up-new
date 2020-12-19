using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace up
{
    public class save
    {
        public tre_save tre_conf { get; set; }
        public configure e { get; set; }
        public configure f { get; set; }
        public Ssh ssh { get; set; }
        public save(/*file_line line, */configure easy, configure full, Ssh ssh_conf, tre_save t_s)
        {
            /*l = line; */
            e = easy; f = full; ssh = ssh_conf; tre_conf = t_s;
        }
    }
}
