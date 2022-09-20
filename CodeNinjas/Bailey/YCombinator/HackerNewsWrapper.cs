namespace CodeNinjas.Bailey.YCombinator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HackerNewsWrapper"/> class,
    ///     which is used to wrap calls to the Firebase-implemented Hacker News API.
    /// </summary>
    /// <inheritdoc cref="LegacyAzureBase{T}"/>
    public class HackerNewsWrapper : LegacyAzureBase<HackerNewsWrapper>, IHackerNewsWrapper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HackerNewsWrapper"/> class,
        ///     which is used to wrap calls to the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="logger">An <see cref="OptionalAttribute"/> <see cref="ILogger{TCategoryName}"/> to use for logging.</param>
        /// <inheritdoc cref="AzureClassBase{T}"/>
        public HackerNewsWrapper([Optional] ILogger<HackerNewsWrapper> logger)
            : base("Felsökning.Utilities.YCombinator", logger)
        {
        }

        /// <summary>
        ///     Gets the list of top stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 500 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        public async Task<List<Story>> GetTopStoriesAsync([Optional] int numberOfStoriesToReturn)
        {
            var resultIds = new List<int>(0);
            var tasks = new List<Task<Story>>();
            if (this.Logger != null)
            {
                resultIds = await this.HttpClient.GetDeserializedAsync<List<int>>("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty", this.Logger);
            }
            else
            {
                resultIds = await this.HttpClient.GetDeserializedAsync<List<int>>("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty");
            }

            if (numberOfStoriesToReturn > 0)
            {
                resultIds = resultIds.OrderByDescending(x => x).Take(numberOfStoriesToReturn).ToList();
            }

            // Asynchronous Calls require either async collections or to await all of the results.
            if (this.Logger != null)
            {
                tasks = resultIds?.Select(r => this.HttpClient.GetDeserializedAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty", this.Logger)).ToList();
            }
            else
            {
                tasks = resultIds?.Select(r => this.HttpClient.GetDeserializedAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
            }

            var stories = await Task.WhenAll(tasks!);

            return stories.OrderByDescending(x => x.Time).ToList();
        }

        /// <summary>
        ///     Gets the list of stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 200 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        public async Task<List<Story>> ShowTopStoriesAsync([Optional] int numberOfStoriesToReturn)
        {
            var resultIds = new List<int>(0);
            var tasks = new List<Task<Story>>();
            if (this.Logger != null)
            {
                resultIds = await this.HttpClient.GetDeserializedAsync<List<int>>("https://hacker-news.firebaseio.com/v0/showstories.json?print=pretty", this.Logger);
            }
            else
            {
                resultIds = await this.HttpClient.GetDeserializedAsync<List<int>>("https://hacker-news.firebaseio.com/v0/showstories.json?print=pretty");
            }

            if (numberOfStoriesToReturn > 0)
            {
                resultIds = resultIds.OrderByDescending(x => x).Take(numberOfStoriesToReturn).ToList();
            }

            // Asynchronous Calls require either async collections or to await all of the results.
            if (this.Logger != null)
            {
                tasks = resultIds?.Select(r => this.HttpClient.GetDeserializedAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty", this.Logger)).ToList();
            }
            else
            {
                tasks = resultIds?.Select(r => this.HttpClient.GetDeserializedAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
            }

            var stories = await Task.WhenAll(tasks!);

            return stories.OrderByDescending(x => x.Time).ToList();
        }
    }
}