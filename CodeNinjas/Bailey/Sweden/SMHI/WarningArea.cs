namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WarningArea"/> class.
    /// </summary>
    public class WarningArea
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("approximateStart")]
        public DateTime ApproximateStart { get; set; }

        [JsonPropertyName("published")]
        public DateTime Published { get; set; }

        [JsonPropertyName("normalProbability")]
        public bool NormalProbability { get; set; }

        [JsonPropertyName("areaName")]
        public AreaName AreaName { get; set; } = new AreaName();

        [JsonPropertyName("warningLevel")]
        public WarningLevel WarningLevel { get; set; } = new WarningLevel();

        [JsonPropertyName("eventDescription")]
        public EventDescription EventDescription { get; set; } = new EventDescription();

        [JsonPropertyName("affectedAreas")]
        public List<AffectedArea> AffectedAreas { get; set; } = new List<AffectedArea>(0);

        [JsonPropertyName("descriptions")]
        public List<Description> Descriptions { get; set; } = new List<Description>(0);

        [JsonPropertyName("area")]
        public Area Area { get; set; } = new Area();

        [JsonPropertyName("approximateEnd")]
        public DateTime? ApproximateEnd { get; set; }
    }
}