namespace CodeNinjas.Bailey.YCombinator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Story"/> class.
    /// </summary>
    public class Story
    {
        /// <summary>
        ///     Gets or sets who posted the story.
        /// </summary>
        [JsonPropertyName("by")]
        public string By { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the story's descendents.
        /// </summary>
        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }

        /// <summary>
        ///     Gets or sets the Id of the story.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the children of the story.
        /// </summary>
        [JsonPropertyName("kids")]
        public List<int> Kids { get; set; } = new List<int>(0);

        /// <summary>
        ///     Gets or sets the story's score.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        ///     Gets or sets when the story was posted by the user.
        /// </summary>
        [JsonPropertyName("time")]
        public int Time { get; set; }

        /// <summary>
        ///     Gets or sets the Title of the story.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the story's type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the URL to the story.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}