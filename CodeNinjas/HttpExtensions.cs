namespace CodeNinjas
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpExtensions"/> class, 
    ///     which is used to extend Http-Related classes.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        ///     Adds the given header name and value to the <see cref="HttpClient"/>.
        ///     <para>WARNING: The existing header of the same name will be removed, if it exists.</para>
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static void AddHeader(this HttpClient httpClient, string name, string value, [Optional] ILogger azLogger)
        {
            httpClient.RemoveHeader(name);
            httpClient.DefaultRequestHeaders.Add(name: name, value: value);
            if (azLogger != null)
            {
                var httpHeaderLog = new { Name = name, Value = value };
                azLogger.LogInformation(ILogging.HttpHeaderEntryTemplate, new object[] { $"Successfully added the '{name}' header with the value of '{value}'", JsonSerializer.Serialize(httpHeaderLog) });
            }
        }

        /// <summary>
        ///     Generates a new request id for the given <see cref="HttpClient"/> for tracking/tracing reasons.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static string GenerateNewRequestId(this HttpClient httpClient, [Optional] ILogger azLogger)
        {
            var generatedRequestId = Guid.NewGuid().ToString();
            httpClient.AddHeader("X-Request-ID", generatedRequestId, azLogger);
            return generatedRequestId;
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     <para>We only check for successful HTTP responses. Any continuations must be handled by the caller.</para>
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <see cref="{T}"/></returns>
        public static async Task<T> GetDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Get,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUrl);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}", JsonSerializer.Serialize(httpResponseLog) });

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Get,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Deserializes data and sends the patch request to update the object.
        /// </summary>
        /// <typeparam name="T">The base type to be deserialized and patched.</typeparam>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="typeObject">The object to be deserialized and patched.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <see cref="{T}"/></returns>
        public static async Task<T> PatchDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, T typeObject, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Patch,
                    Body = typeObject,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpRequestMessage httpRequestMessage = new(method: new HttpMethod("PATCH"), requestUri: requestUrl)
                {
                    Content = new StringContent(content: JsonSerializer.Serialize(typeObject))
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Patch,
                        Body = typeObject,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Patch,
                        Body = typeObject,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Patch,
                    Body = typeObject,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Deserializes data and sends the patch request to update the object.
        /// </summary>
        /// <typeparam name="T">The base type to be deserialized and patched.</typeparam>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="typeObject">The object to be deserialized and patched.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task PatchDataAsync<T>(this HttpClient httpClient, string requestUrl, T typeObject, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Patch,
                    Body = typeObject,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpRequestMessage httpRequestMessage = new(method: new HttpMethod("PATCH"), requestUri: requestUrl)
                {
                    Content = new StringContent(content: JsonSerializer.Serialize(typeObject))
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);
                var httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Patch,
                        Body = typeObject,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Patch,
                    Body = typeObject,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be posted, in string form.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = httpContent.ReadAsStringAsync().Result,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        Body = httpContent.ReadAsStringAsync().Result,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = httpContent.ReadAsStringAsync().Result,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be posted, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = stringContent,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        Body = stringContent,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = stringContent,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be put.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Put,
                    Body = httpContent.ReadAsStringAsync().Result,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Put,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        Body = httpContent.ReadAsStringAsync().Result,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = httpContent.ReadAsStringAsync().Result,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be put, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutDeserializedAsync<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType, [Optional] ILogger azLogger)
        {
            var requestId = string.Empty;
            try
            {
                if (azLogger != null)
                {
                    requestId = httpClient.GenerateNewRequestId(azLogger);
                }
                else
                {
                    requestId = httpClient.GenerateNewRequestId();
                }

                var httpRequestLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Put,
                    Body = stringContent,
                };

                azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Requesting data from '{requestUrl}'", JsonSerializer.Serialize(httpRequestLog) });
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Get,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogInformation(ILogging.HttpRequestEntryTemplate, new object[] { $"Received and parsing/returning data from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    var httpResponseLog = new
                    {
                        Url = requestUrl,
                        RequestId = requestId,
                        Method = HttpMethod.Post,
                        Body = httpContent.ReadAsStringAsync().Result,
                        httpResponseMessage.StatusCode,
                        Content = httpResponseMessageContent,
                    };

                    azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", JsonSerializer.Serialize(httpResponseLog) });
                    throw new StatusException($"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}'", httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                var httpResponseLog = new
                {
                    Url = requestUrl,
                    RequestId = requestId,
                    Method = HttpMethod.Post,
                    Body = stringContent,
                    thrownException.StatusCode,
                    Content = thrownException.Message,
                };

                azLogger?.LogError(ILogging.HttpRequestEntryTemplate, new object[] { $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.", JsonSerializer.Serialize(httpResponseLog) });
                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Removes the given header, if it exists.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to be removed from the <see cref="HttpClient.DefaultRequestHeaders"/> context.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static void RemoveHeader(this HttpClient httpClient, string name, [Optional] ILogger azLogger)
        {
            if (httpClient.DefaultRequestHeaders.Contains(name: name))
            {
                httpClient.DefaultRequestHeaders.Remove(name: name);
                if (azLogger != null)
                {
                    var httpHeaderLog = new { Name = name };
                    azLogger.LogInformation(ILogging.HttpHeaderEntryTemplate, new object[] { $"Successfully removed the '{name}' header", JsonSerializer.Serialize(httpHeaderLog) });
                }
            }
        }
    }
}