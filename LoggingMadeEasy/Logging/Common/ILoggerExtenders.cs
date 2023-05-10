// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;

namespace LoggingMadeEasy;

public static class ILoggerExtenders
{
    public static void Log<T>(this ILogger logger, T logItem)
        where T : LogItemBase<T>
    {
        var (message, datas) = logItem.GetMessageAndDatas();

        switch (logItem.LogLevel)
        {
            case LogLevel.Debug:
                logger.Debug(message, datas.ToArray());
                break;
            case LogLevel.Info:
                logger.Information(message, datas.ToArray());
                break;
            case LogLevel.Warn:
                logger.Warning(message, datas.ToArray());
                break;
            case LogLevel.Error:
                logger.Error(message, datas.ToArray());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logItem));
        }
    }
}