// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using LoggingMadeEasy;
using Microsoft.Extensions.Configuration;

var logger = CreateLogger();

var junket = new Junket()
{
    AppName = Tag.From("Demo"),
    UserId = Tag.From("ABC123"),
    JunketId = Guid.NewGuid()
};

logger.Log(new SimpleLogItem(junket, Tag.From("PickSong"),
    "Picked \"New York, New York\"")
{
    ExtraInfos = new Dictionary<Tag, object>()
    {
        { Tag.From("Title"), "New York, New York" },
        { Tag.From("Composers"), "John Kander" },
        { Tag.From("Lyricists"), "Fred Ebb" },
        { Tag.From("Producers"), "Ralph Burns" }
    }
});

Log.Logger.Log(new SimpleLogItem(junket, Tag.From("PlaySong"),
    "Played \"New York, New York\"", LogLevel.Warn));

try
{
    try
    {
        throw new FileNotFoundException(
            "The \"SomeFile.json\" file could not be found!");
    }
    catch (Exception inner)
    {
        throw new ApplicationException("Oopsie!!", inner);
    }
}
catch (Exception outer)
{
    logger.Log(new ErrorOccured(junket, outer)
    {
        ExtraInfos = new Dictionary<Tag, object>()
        {
            { Tag.From("StartedOn"), DateTime.UtcNow },
            { Tag.From("Elapsed"), DateTime.Now - DateTime.UtcNow },
            { Tag.From("ProjectId"), "XXX-YYY-ZZZ-01" }
        }
    });
}

Log.CloseAndFlush();

static ILogger CreateLogger()
{
    var config = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    return Log.Logger = SerilogHelper.GetStandardLogger(new Uri(
        config["Logging:Seq:ApiUri"]), config["Logging:Seq:ApiKey"]);
}