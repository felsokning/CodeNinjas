namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AreaName"/> class.
    /// </summary>
    public class AreaName
    {
        /// <summary>
        ///     Gets or sets the Area Name in Swedish.
        /// </summary>
        [JsonPropertyName("sv")]
        public string Svenska { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Area Name in English.
        /// </summary>
        [JsonPropertyName("en")]
        public string English { get; set; } = string.Empty;
    }
}