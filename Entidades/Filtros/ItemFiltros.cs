using System.Text.Json.Serialization;

namespace Entidades.Filtros
{
    public class ItemFiltros
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("typectrl")]
        public string Description { get; set; }
    }
}
