using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Node
    {
        public int id_node { get; set; }
        public string? measure { get; set; }
        public string? format_text { get; set; }
        public Indicator? indicator { get; set; }
        public Variation? variation { get; set; }
        public Unit? unit { get; set; }
    }

 
}
