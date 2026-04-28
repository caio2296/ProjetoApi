using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Row
    {
        public int id_row_schema { get; set; }
        public string? text { get; set; }
        public int? order { get; set; }
        public int? level { get; set; }
        public int? parent {  get; set; }
        public bool? enabled { get; set; }
        public ICollection<Tooltip>? tooltip { get; set; }
        public ICollection<LinkButton>? linkbuttons { get; set; }
        public ICollection<Node> nodes { get; set; }
        
    }

  
}
