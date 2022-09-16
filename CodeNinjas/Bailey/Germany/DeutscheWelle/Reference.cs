namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Reference"/> class.
    /// </summary>
    public class Reference
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
