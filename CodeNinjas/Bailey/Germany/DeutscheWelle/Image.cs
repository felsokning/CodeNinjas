namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    public class Image
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; } = new List<Size>(0);
    }
}
