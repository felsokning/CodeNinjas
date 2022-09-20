namespace CodeNinjas
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Http1"/> class.
    /// </summary>
    public class Http1 : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Http2Base"/> class.
        /// </summary>
        /// <param name="productInfoString">The assembly reference/name to include in the header.</param>
        public Http1(string productInfoString)
        {
            HttpClient = new HttpClient
            {
                // Disallow protocol downgrade[s] - which is a common attack vector.
                DefaultRequestVersion = HttpVersion.Version11,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher
            };

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(Uri.EscapeDataString(productInfoString), "1.0.0"));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Contact", Uri.EscapeDataString("nuget@felsokning.se")));
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Website", Uri.EscapeDataString("https://www.nuget.org/profiles/felsokning")));
        }

        /// <summary>
        ///     Used to detect redundant calls of disposing.
        /// </summary>
        internal bool disposed;

        /// <summary>
        ///     Gets or sets a singleton instance of <see cref="System.Net.Http.HttpClient"/> used by the class.
        /// </summary>
        public HttpClient HttpClient { get; set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if disposing was called.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    HttpClient.DefaultRequestHeaders.Clear();
                    HttpClient.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}