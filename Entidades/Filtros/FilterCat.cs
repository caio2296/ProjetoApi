using System.Text.Json.Serialization;

namespace Entidades.Filtros
{
    public class FilterCat: ItemFiltros
    {
        [JsonPropertyName("css")]
        public int Css { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("parentcontrolid")]
        public int? Id_Parent { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("checkstatus")]
        public bool? CheckStatus { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("imageurl")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("controlsbyrow")]
        public bool ControlsByRow { get; set; }

        [JsonPropertyName("children")]
        public List<FilterCat>? Children { get; set; }

        [JsonPropertyName("labelbyrow")]
        public bool? LabelByRow { get; set; }

        [JsonPropertyName("order")]
        public int? Order { get; set; }

        [JsonPropertyName("id_ctrl_depend")]
        public int? Id_Filter_Dependent { get; set; }

        [JsonPropertyName("disabledctrls")]
        public bool DisabledCtrls { get; set; }

        [JsonPropertyName("maxctrlsbygroup")]
        public int? MaxCtrlsByGroup { get; set; }

        [JsonPropertyName("checked")]
        public bool? Checked { get; set; }

        [JsonPropertyName("imageoverurl")]
        public string? ImageOverUrl { get; set; }

        [JsonPropertyName("radioitems")]
        public List<RadioItems>? RadioItems { get; set; }

        [JsonPropertyName("selectItems")]
        public List<SelectItems>? SelectItems { get; set; }

    }
}
