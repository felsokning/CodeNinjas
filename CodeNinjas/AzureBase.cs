namespace CodeNinjas
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AzureBase{T}"/> class.
    /// </summary>
    /// <inheritdoc cref="Http2"/>
    public class AzureBase<T> : Http2
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AzureClassBase{T}"/> class.
        /// </summary>
        /// <param name="productInfoString">The Product Info to pass into the base class.</param>
        /// <param name="logger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> passed by the caller for logging in Azure.</param>
        public AzureBase(string productInfoString, [Optional] ILogger<T> logger)
            : base(productInfoString)
        {
            if (logger != null)
            {
                Logger = logger;
                Logger.BeginScope(productInfoString);
                Logger.LogInformation(ILogging.AssemblyLoadingTemplate, new object[] { productInfoString });

                // Generate a Correlation ID per class instantiation.
                var correlationId = Guid.NewGuid().ToString();
                HttpClient.AddHeader("X-Correlation-ID", correlationId, logger);
            }
            else
            {
                // Generate a Correlation ID per class instantiation.
                var correlationId = Guid.NewGuid().ToString();
                HttpClient.AddHeader("X-Correlation-ID", correlationId);
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="ILogger{TCategoryName}"/> to used by the class[es].
        /// </summary>
        public ILogger<T>? Logger { get; set; }
    }
}