namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Crs"/> class.
    /// </summary>
    public class Crs
    {
        /// <summary>
        ///     Gets or sets the CRS type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the properties of the CRS.
        /// </summary>
        [JsonPropertyName("properties")]
        public Properties Properties { get; set; } = new Properties();
    }
}
