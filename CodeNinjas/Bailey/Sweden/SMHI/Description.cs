namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Description"/> class.
    /// </summary>
    public class Description
    {
        /// <summary>
        ///     Gets or sets the Title of the Description.
        /// </summary>
        [JsonPropertyName("title")]
        public Title Title { get; set; } = new Title();

        /// <summary>
        ///     Gets or sets the Text of the Description.
        /// </summary>
        [JsonPropertyName("text")]
        public Text Text { get; set; } = new Text();
    }
}