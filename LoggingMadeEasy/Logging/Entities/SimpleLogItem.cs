// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace LoggingMadeEasy;

public class SimpleLogItem : LogItemBase<SimpleLogItem>
{
    public SimpleLogItem(Junket junket, Tag activity, 
        string message = null!, LogLevel logLevel = LogLevel.Info)
        : base(junket, logLevel, activity)
    {
        Message = message;
    }

    public string Message { get; }

    public Dictionary<Tag, object>? ExtraInfos { get; init; }

    protected override void AddDetails(Dictionary<Tag, object> details)
    {
        details.Add(Tag.From(nameof(Message)), Message);

        if (ExtraInfos is null)
            return;

        foreach (var kv in ExtraInfos!)
            details.Add(kv.Key, kv.Value);
    }
}