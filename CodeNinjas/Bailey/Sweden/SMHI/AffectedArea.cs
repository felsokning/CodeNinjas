namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AffectedArea"/> class.
    /// </summary>
    public class AffectedArea
    {
        /// <summary>
        ///     Gets or sets the ID of the Affected Area.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the Affected Area in Swedish.
        /// </summary>
        [JsonPropertyName("sv")]
        public string Svenska { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the Affected Area in English.
        /// </summary>
        [JsonPropertyName("en")]
        public string English { get; set; } = string.Empty;
    }
}