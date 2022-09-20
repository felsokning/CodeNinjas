namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeutscheWelleWrapper"/> class,
    ///     which is used to wrap calls to the Deutsche Welle API.
    /// </summary>
    /// <inheritdoc cref="AzureBase{T}"/>
    public class DeutscheWelleWrapper : AzureBase<DeutscheWelleWrapper>, IDeutscheWelleWrapper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeutscheWelleWrapper"/> class,
        ///     which is used to wrap calls to the Deutsche Welle API.
        /// </summary>
        /// <param name="logger">An <see cref="OptionalAttribute"/> <see cref="ILogger{TCategoryName}"/> to use for logging.</param>
        /// <inheritdoc cref="AzureClassBase{T}"/>
        public DeutscheWelleWrapper([Optional] ILogger<DeutscheWelleWrapper> logger)
            : base("Felsökning.Utilities.Germany", logger)
        {
        }

        /// <summary>
        ///     Obtains the latest news articles from the Deutsche Welle API.
        /// </summary>
        /// <param name="languageId">The <see cref="LanguageId"/> used to target a specific language in the query.</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/></returns>
        /// <exception cref="StatusException">An exception when the HTTP Request is aborted.</exception>
        public async Task<SearchResult> GetLatestArticlesAsync(LanguageId languageId)
        {
            if (languageId == LanguageId.None)
            {
                Logger?.LogError("LanguageId 0 has not been implemented on the Deutsche Welle API");
                throw new InvalidOperationException("LanguageId 0 has not been implemented on the Deutsche Welle API");
            }

            if (Logger != null)
            {
                return await HttpClient.GetDeserializedAsync<SearchResult>($"https://api.dw.com/api/search/global?terms=*&contentTypes=Article&languageId={(int)languageId}&sortByDate=true&startDate={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}&endDate={DateTime.UtcNow.ToString("yyyy-MM-dd")}", Logger);
            }
            else
            {
                return await HttpClient.GetDeserializedAsync<SearchResult>($"https://api.dw.com/api/search/global?terms=*&contentTypes=Article&languageId={(int)languageId}&sortByDate=true&startDate={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}&endDate={DateTime.UtcNow.ToString("yyyy-MM-dd")}");
            }
        }
    }
}