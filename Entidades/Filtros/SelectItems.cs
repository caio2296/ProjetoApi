using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Filtros
{
    public class SelectItems
    {
        [JsonPropertyName("id_item")]
        public int IdItem { get; set; }
        [JsonPropertyName("id_filter")]
        public int IdFilter { get; set; }
        [JsonPropertyName("order")]
        public int Order { get; set; }
        [JsonPropertyName("defaultselected")]
        public bool defaultselected { get; set; }
        [JsonPropertyName("withfilters")]
        public bool withfilters { get; set; }
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("displaytext")]
        public string? Displaytext { get; set; }

    }
}
