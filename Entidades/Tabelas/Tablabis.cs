using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Tablabis
    {
        public int id_tablabis { get; set; }
        public string description { get; set; }
        public bool enable { get; set; }
        public  ICollection<Template> templates { get; set; }
        public ICollection<Semaphore> semaphores { get; set; }
    }

}
