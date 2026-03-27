using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Filtros
{
    public class RadioItems
    {
        [JsonPropertyName("id_item")]
        public int Id_Item { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        [JsonPropertyName("statuscheck")]
        public bool StatusCheck
        {
            get; set;
        }
    }
}
