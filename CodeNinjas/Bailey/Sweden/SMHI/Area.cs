namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Area"/> class.
    /// </summary>
    public class Area
    {
        /// <summary>
        ///     Gets or sets the Coordinate Reference System of the Area.
        /// </summary>
        [JsonPropertyName("crs")]
        public Crs Crs { get; set; } = new Crs();

        /// <summary>
        ///     Gets or sets the type of the Area.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the geometry of the area.
        /// </summary>
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; } = new Geometry();

        /// <summary>
        ///     Gets or sets the properties of the Area.
        /// </summary>
        [JsonPropertyName("properties")]
        public Properties Properties { get; set; } = new Properties();
    }
}