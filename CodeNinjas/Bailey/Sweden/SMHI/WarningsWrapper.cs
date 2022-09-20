namespace CodeNinjas.Bailey.Sweden.SMHI
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WarningsWrapper"/> class.
    /// </summary>
    /// <inheritdoc cref="LegacyAzureBase{T}"/>
    /// <inheritdoc cref="IWarningsWrapper"/>
    public class WarningsWrapper : LegacyAzureBase<WarningsWrapper>, IWarningsWrapper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WarningsWrapper"/> class.
        /// </summary>
        /// <inheritdoc cref="LegacyAzureClassBase{T}"/>
        public WarningsWrapper([Optional] ILogger<WarningsWrapper> logger)
            : base("Felsökning.Utilities.Sweden.SMHI", logger)
        {
            if (this.Logger != null)
            {
                var apiEntry = this.HttpClient.GetDeserializedAsync<WarningsApiEntry>("https://opendata-download-warnings.smhi.se/ibww/api/version/1.json", logger).Result;
                this.WarningReference = apiEntry.Warning;
            }
            else
            {
                var apiEntry = this.HttpClient.GetDeserializedAsync<WarningsApiEntry>("https://opendata-download-warnings.smhi.se/ibww/api/version/1.json").Result;
                this.WarningReference = apiEntry.Warning;
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="SMHI.WarningReference"/> reference.
        /// </summary>
        internal WarningReference WarningReference { get; set; }

        /// <summary>
        ///     Gets the most recently published Warnings from the SMHI API.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="WarningsResult"/></returns>
        /// <exception cref="StatusException">An error occurred on the underlying HTTP call[s].</exception>
        public async Task<List<WarningsResult>> GetRecentWarningsAsync()
        {
            if (this.Logger != null)
            {
                return await this.HttpClient.GetDeserializedAsync<List<WarningsResult>>(this.WarningReference.Href, Logger);
            }
            else
            {
                return await this.HttpClient.GetDeserializedAsync<List<WarningsResult>>(this.WarningReference.Href);
            }
        }
    }
}