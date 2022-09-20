namespace CodeNinjas.Bailey.YCombinator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IHackerNewsWrapper"/> class.
    /// </summary>
    public interface IHackerNewsWrapper
    {
        /// <summary>
        ///     Gets the list of top stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 500 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        Task<List<Story>> GetTopStoriesAsync([Optional] int numberOfStoriesToReturn);

        /// <summary>
        ///     Gets the list of top stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 500 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        Task<List<Story>> ShowTopStoriesAsync([Optional] int numberOfStoriesToReturn);
    }
}