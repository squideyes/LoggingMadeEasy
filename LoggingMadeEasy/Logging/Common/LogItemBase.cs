// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;

namespace LoggingMadeEasy;

public abstract class LogItemBase<T>
{
    private readonly Dictionary<Tag, object> details = new();

    internal LogItemBase(Junket junket, 
        LogLevel logLevel, Tag? activity = null)
    {
        if (junket == null)
            throw new LoggingException("\"Junket\" may not be null.");

        junket.Validate();

        if (!logLevel.IsEnumValue())
            throw new LoggingException("\"LogLevel\" must be defined.");

        details.Add(Tag.From("UserId"), junket.UserId);

        details.Add(Tag.From("Ordinal"), junket.GetOrdinal());

        details.Add(Tag.From("JunketId"), 
            junket.JunketId.ToString("N"));

        details.Add(Tag.From("Activity"),
            activity == null ? nameof(T) : activity.ToString()!);

        LogLevel = logLevel;
    }

    internal LogLevel LogLevel { get; }

    protected virtual void AddDetails(Dictionary<Tag, object> details)
    {
    }

    internal (string Message, string?[] Datas) GetMessageAndDatas()
    {
        AddDetails(details);

        var message = new StringBuilder();

        var datas = new List<object>();

        foreach (var kv in details)
        {
            if (message!.Length > 0)
                message.Append(',');

            message!.Append($"{{@{kv.Key}}}");

            datas!.Add(kv.Value!.ToString()!);
        }

        return (message.ToString(),
            datas.Select(v => v.ToString()).ToArray());
    }
}