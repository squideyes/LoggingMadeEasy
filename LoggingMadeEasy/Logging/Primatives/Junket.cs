// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace LoggingMadeEasy;

public class Junket
{
    private int ordinal = -1;

    public required Tag AppName { get; init; }
    public required Tag UserId { get; init; }
    public required Guid JunketId { get; init; } = Guid.NewGuid();

    public int GetOrdinal() => Interlocked.Increment(ref ordinal);

    internal void Validate()
    {
        static string BadValue(string name, string kind) =>
            $"\"{name}\" may not be a default {kind}.";

        if (AppName.IsDefaultValue())
            throw new LoggingException(BadValue(nameof(AppName), "Tag"));

        if (UserId.IsDefaultValue())
            throw new LoggingException(BadValue(nameof(UserId), "Tag"));

        if (JunketId.IsDefaultValue())
            throw new LoggingException(BadValue(nameof(JunketId), "Guid"));
    }
}