using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entidades
{
    public class CalendarModel
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("calendarBar")]
        public CalendarBar CalendarBar { get; set; }

        [JsonPropertyName("shiftBar")]
        public ShiftBar ShiftBar { get; set; }

        [JsonPropertyName("dateMin")]
        public string DateMin { get; set; }

        [JsonPropertyName("dateMax")]
        public string DateMax { get; set; }

        [JsonPropertyName("fiscalYearStart")]
        public string FiscalYearStart { get; set; }
    }

    public class CalendarBar
    {
        [JsonPropertyName("datetime")]
        public TimeItem Datetime { get; set; }

        [JsonPropertyName("day")]
        public TimeItem Day { get; set; }

        [JsonPropertyName("week")]
        public TimeItem Week { get; set; }

        [JsonPropertyName("month")]
        public TimeItem Month { get; set; }

        [JsonPropertyName("year")]
        public TimeItem Year { get; set; }

        [JsonPropertyName("fiscalYear")]
        public TimeItem FiscalYear { get; set; }

        [JsonPropertyName("defaultSelection")]
        public DefaultSelection DefaultSelection { get; set; }
    }

    public class TimeItem
    {
        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("range")]
        public bool Range { get; set; }

        [JsonPropertyName("rangeStart")]
        public string RangeStart { get; set; }

        [JsonPropertyName("rangeEnd")]
        public string RangeEnd { get; set; }
    }

    public class DefaultSelection
    {
        [JsonPropertyName("selection")]
        public string Selection { get; set; }

        [JsonPropertyName("range")]
        public bool Range { get; set; }

        [JsonPropertyName("dateStart")]
        public string DateStart { get; set; }

        [JsonPropertyName("dateEnd")]
        public string DateEnd { get; set; }
    }

    public class ShiftBar
    {
        [JsonPropertyName("descriptions")]
        public List<string> Descriptions { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("range")]
        public bool Range { get; set; }

        [JsonPropertyName("rangeStart")]
        public string RangeStart { get; set; }

        [JsonPropertyName("rangeEnd")]
        public string RangeEnd { get; set; }

        [JsonPropertyName("typeCtrl")]
        public string TypeCtrl { get; set; }
    }
}
