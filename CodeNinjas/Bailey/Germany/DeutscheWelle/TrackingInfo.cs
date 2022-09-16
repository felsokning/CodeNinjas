namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TrackingInfo"/> class.
    /// </summary>
    public class TrackingInfo
    {
        [JsonPropertyName("level2")]
        public string Level2 { get; set; } = string.Empty;

        [JsonPropertyName("page")]
        public string Page { get; set; } = string.Empty;

        [JsonPropertyName("customCriteria")]
        public CustomCriteria CustomCriteria { get; set; } = new CustomCriteria();
    }
}
