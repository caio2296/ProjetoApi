using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Col
    {
        public int id_col_schema { get; set; }
        public string? text { get; set; }
        public int? order { get; set; }
        public int? level { get; set; }
        public bool? collapsed { get; set; }
    }


}
