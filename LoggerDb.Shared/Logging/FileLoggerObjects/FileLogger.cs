using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Logger.Shared.Logging.FileLoggerObjects
{
    public class FileLogger : ILogger
    {
        protected readonly FileLoggerProvider _loggerFileProvider;

        public FileLogger([NotNull] FileLoggerProvider loggerFileProvider)
        {
            _loggerFileProvider = loggerFileProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var threadId = Thread.CurrentThread.ManagedThreadId; 

            Task.Run(async () =>
            {
                var fullFilePath = _loggerFileProvider.Options.FolderPath + "/" + _loggerFileProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd")); // Get the full log file path. Seperated by day.
                var logRecord = string.Format("{0} [{1}] [{2}] {3} {4}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z") + "]", threadId, logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : ""); // Format the log entry.

                try
                {
                    await _loggerFileProvider.WriteFileLock.WaitAsync();

                    
                    using (var streamWriter = new StreamWriter(fullFilePath, true))
                    {
                        await streamWriter.WriteLineAsync(logRecord);
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    _loggerFileProvider.WriteFileLock.Release();
                }
            });
        }
    }
}
