namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WarningsApiEntry"/> class.
    /// </summary>
    public class WarningsApiEntry
    {
        /// <summary>
        ///     Gets or sets the resource identifier.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the description of the resource.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the summary, which extends the description.
        /// </summary>
        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("warning")]
        public WarningReference Warning { get; set; } = new WarningReference();

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; } = new Metadata();

        [JsonPropertyName("cap")]
        public Cap Cap { get; set; } = new Cap();
    }
}
