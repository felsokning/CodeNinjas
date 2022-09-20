namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterParameters"/> class.
    /// </summary>
    public class FilterParameters
    {
        [JsonPropertyName("terms")]
        public string Terms { get; set; } = string.Empty;

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("sortByDate")]
        public bool SortByDate { get; set; }

        [JsonPropertyName("contentTypes")]
        public List<string> ContentTypes { get; set; } = new List<string>(0);

        [JsonPropertyName("programIds")]
        public List<object> ProgramIds { get; set; } = new List<object>(0);

        [JsonPropertyName("categoryIds")]
        public List<object> CategoryIds { get; set; } = new List<object>(0);

        [JsonPropertyName("contentIds")]
        public List<object> ContentIds { get; set; } = new List<object>(0);
    }
}