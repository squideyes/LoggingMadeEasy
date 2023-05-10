// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using static LoggingMadeEasy.LogLevel;

namespace LoggingMadeEasy;

public static class SerilogHelper
{
    public static Logger GetStandardLogger(Uri seqApiUri, 
        string seqApiKey = null!, LogLevel minLogLevel = Info,
        Dictionary<string, object> enrichWith = null!)
    {
        var config = GetStandardConfig(
            seqApiUri, seqApiKey, minLogLevel, enrichWith);

        return config.CreateLogger();
    }

    public static LoggerConfiguration GetStandardConfig(Uri seqUri, 
        string seqApiKey = null!, LogLevel minLogLevel = Info, 
        Dictionary<string, object> enrichWith = null!)
    {
        if (seqUri == null)
            throw new ArgumentNullException(nameof(seqUri));

        if (!seqUri.IsAbsoluteUri)
            throw new ArgumentOutOfRangeException(nameof(seqUri));

        var config = new LoggerConfiguration()
            .Destructure.ByTransforming<DateOnly>(
                v => v.ToString("MM/dd/yyyy"))
            .Destructure.ByTransforming<DateTime>(
                v => v.ToString("MM/dd/yyyy HH:mm:ss.fff"))
            .Destructure.ByTransforming<Enum>(
                v => v.ToString())
            .Destructure.ByTransforming<Tag>(
                v => v.ToString())
            .Destructure.ByTransforming<TimeOnly>(
                v => v.ToString("HH:mm:ss.fff"))
            .Destructure.ByTransforming<TimeSpan>(
                v => v.ToString(@"d\.hh\:mm\:ss\.fff"))
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}")
            .WriteTo.Seq(seqUri.AbsoluteUri, apiKey: seqApiKey,
                restrictedToMinimumLevel: LogEventLevel.Debug)
            .MinimumLevel.Is(minLogLevel.ToLogEventLevel());

        if (enrichWith != null)
        {
            foreach (var kv in enrichWith)
                config.Enrich.WithProperty(kv.Key, kv.Value);
        }

        config.WriteTo.Seq(seqUri.AbsoluteUri,
            apiKey: seqApiKey.Convert(v => string.IsNullOrWhiteSpace(v) ? null : v),
            restrictedToMinimumLevel: LogEventLevel.Debug);

        config.WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}");

        return config;
    }
}