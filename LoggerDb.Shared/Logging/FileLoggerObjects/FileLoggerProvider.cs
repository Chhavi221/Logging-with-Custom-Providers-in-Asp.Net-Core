using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logger.Shared.Logging.FileLoggerObjects
{
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;

        public SemaphoreSlim WriteFileLock;

        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
        {
            WriteFileLock = new SemaphoreSlim(1, 1);
            Options = options.Value;

            if (!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory(Options.FolderPath);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
