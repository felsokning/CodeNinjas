namespace CodeNinjas.Bailey.Germany.DeutscheWelle
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IDeutscheWelleWrapper"/> interface,
    ///     which is used to wrap calls to the Deutsche Welle API.
    /// </summary>
    public interface IDeutscheWelleWrapper
    {
        /// <summary>
        ///     Obtains the latest news articles from the Deutsche Welle API.
        /// </summary>
        /// <param name="languageId">The <see cref="LanguageId"/> used to target a specific language in the query.</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/></returns>
        /// <exception cref="StatusException">An exception when the HTTP Request is aborted.</exception>
        Task<SearchResult> GetLatestArticlesAsync(LanguageId languageId);
    }
}