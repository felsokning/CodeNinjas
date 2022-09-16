namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    public class Item
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("teaserText")]
        public string TeaserText { get; set; } = string.Empty;

        [JsonPropertyName("displayDate")]
        public DateTime DisplayDate { get; set; }

        [JsonPropertyName("image")]
        public Image Image { get; set; } = new Image();

        [JsonPropertyName("reference")]
        public Reference Reference { get; set; } = new Reference();

        [JsonPropertyName("columnCount")]
        public int ColumnCount { get; set; }

        [JsonPropertyName("allowedColumnCounts")]
        public List<int> AllowedColumnCounts { get; set; } = new List<int>(0);

        [JsonPropertyName("commentsEnabled")]
        public bool CommentsEnabled { get; set; }
    }
}