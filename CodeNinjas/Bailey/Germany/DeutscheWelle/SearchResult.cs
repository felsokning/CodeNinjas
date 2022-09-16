namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    public class SearchResult
    {
        /// <summary>
        ///     Gets or sets the integer mapping to the <see cref="DeutscheWelle.LanguageId"/>
        /// </summary>
        [JsonPropertyName("languageId")]
        public int LanguageId { get; set; }

        /// <summary>
        ///     Gets or sets the Pagination Info of the Search Result.
        /// </summary>
        [JsonPropertyName("paginationInfo")]
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; } = new List<Item>(0);

        [JsonPropertyName("trackingInfo")]
        public TrackingInfo TrackingInfo { get; set; } = new TrackingInfo();

        [JsonPropertyName("resultCount")]
        public int ResultCount { get; set; }

        [JsonPropertyName("filterParameters")]
        public FilterParameters FilterParameters { get; set; } = new FilterParameters();
    }
}
