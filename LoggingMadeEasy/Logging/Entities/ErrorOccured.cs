// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace LoggingMadeEasy;

public class ErrorOccured : LogItemBase<ErrorOccured>
{
    public ErrorOccured(Junket junket, Exception error)
        : base(junket, LogLevel.Error)
    {
        Error = new Error(error, Debugger.IsAttached);
    }

    public Error? Error { get; }    

    public Dictionary<Tag, object>? ExtraInfos { get; init; }

    protected override void AddDetails(Dictionary<Tag, object> details)
    {
        details.Add(Tag.From(nameof(Error)), Error!);

        foreach(var kv in ExtraInfos!)
            details.Add(kv.Key, kv.Value);
    }
}