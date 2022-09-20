namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    public class PaginationInfo
    {
        [JsonPropertyName("availableItems")]
        public int AvailableItems { get; set; }

        [JsonPropertyName("availablePages")]
        public int AvailablePages { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("itemsOnPage")]
        public int ItemsOnPage { get; set; }

        [JsonPropertyName("firstItem")]
        public int FirstItem { get; set; }

        [JsonPropertyName("lastItem")]
        public int LastItem { get; set; }
    }
}
