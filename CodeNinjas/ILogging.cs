namespace CodeNinjas
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ILogging"/> class.
    /// </summary>
    public static class ILogging
    {
        /// <summary>
        ///     The Assembly Loading Template used by the main inheritable classes.
        /// </summary>
        public const string AssemblyLoadingTemplate = "{AssemblyName} was initialized.";

        /// <summary>
        ///     The Base Logging Template used by any other classes.
        /// </summary>
        public const string BaseLogEntryTemplate = "[{AssemblyName}.{MethodName}]: {Message}";

        /// <summary>
        ///     The Logging Template used by the HttpExtensions class for any Http Requests.
        /// </summary>
        public const string HttpHeaderEntryTemplate = "[{AssemblyName}.{MethodName}]: {Message} {HttpHeaderLog}";

        /// <summary>
        ///     The Logging Template used by the HttpExtensions class for any Http Requests.
        /// </summary>
        public const string HttpRequestEntryTemplate = "[{AssemblyName}.{MethodName}]: {Message} {HttpLog}";

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, EventId eventId, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, eventId, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, Exception exception, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, exception, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, eventId, exception, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the debug event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogDebug(this ILogger logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Debug, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the error event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogError(this ILogger logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            argsArray.AddRange(args);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Error, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the information event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogInformation(this ILogger logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Information, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the warning event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILogger logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Warning, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, EventId eventId, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, eventId, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, Exception exception, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, exception, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, EventId eventId, Exception exception, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Critical, eventId, exception, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the debug event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogDebug<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Debug, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the error event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogError<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Error, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the information event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogInformation<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Information, message, argsArray.ToArray());
        }

        /// <summary>
        ///     Formats the string before logging the warning event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
            var argsArray = new List<object>(0);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
            if (methodName.Contains("MoveNext"))
            {
                // Need to go deeper
                stackFrame = new(2);
                methodName = stackFrame.GetMethod().Name;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            argsArray.Add(assemblyName!);
            argsArray.Add(methodName);
            if (args != null && args.Any())
            {
                argsArray.AddRange(args);
            }

            logger.Log(LogLevel.Warning, message, argsArray.ToArray());
        }
    }
}