namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Text"/> class.
    /// </summary>
    public class Text
    {
        /// <summary>
        ///     Gets or sets the Text in Swedish.
        /// </summary>
        [JsonPropertyName("sv")]
        public string Svenska { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Text in English.
        /// </summary>
        [JsonPropertyName("en")]
        public string English { get; set; } = string.Empty;
    }
}