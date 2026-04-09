using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Semaphore
    {
        public int id_semaphore { get; set; }
        public string description { get; set; }

        public ICollection<SemaphoreReference> references { get; set; }
    }

 
}
