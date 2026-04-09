using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Template
    {
        public int id_template { get; set; }
        public string? description { get; set; }
        public string? align { get; set; }
        public bool? showindicators { get; set; }
        public int? showunits { get; set; }
        public bool? showsemaphores { get; set; }
        public bool? scrolling { get; set; }

        public ICollection<Row>? rows { get; set; }
        public ICollection<Col>? cols { get; set; }

      
    }


}
