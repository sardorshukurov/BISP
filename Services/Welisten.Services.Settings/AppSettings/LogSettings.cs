namespace Welisten.Services.Settings.AppSettings;

public class LogSettings
{
    public string Level { get; private set; } = string.Empty;
    public bool WriteToConsole { get; private set; }
    public bool WriteToFile { get; private set; }
    public string FileRollingInterval { get; private set; } = string.Empty;
    public string FileRollingSize { get; private set; } = string.Empty;
}

public enum LogLevel
{
    /// <summary>
    /// Anything and everything you might want to know about a running block of code.
    /// </summary>
    Verbose,

    /// <summary>
    /// Internal system events that aren't necessarily observable from the outside. 
    /// </summary>
    Debug,

    /// <summary>
    /// The lifeblood of operational intelligence - things happen.
    /// </summary>
    Information,

    /// <summary>
    /// Service is degraded or endangered.
    /// </summary>
    Warning,

    /// <summary>
    /// Functionality is unavailable, invariants are broken or data is lost.
    /// </summary>
    Error,

    /// <summary>
    /// If you have a pager, it goes off when one of these occurs.
    /// </summary>
    Fatal
}

public enum LogRollingInterval
{
    /// <summary>
    /// The log file will never roll; no time period information will be appended to the log file name.
    /// </summary>
    Infinite,

    /// <summary>
    /// Roll every year. Filenames will have a four-digit year appended in the pattern yyyy
    /// </summary>
    Year,

    /// <summary>
    /// Roll every calendar month. Filenames will have yyyyMM appended.
    /// </summary>
    Month,

    /// <summary>
    /// Roll every day. Filenames will have yyyyMMdd appended.
    /// </summary>
    Day,

    /// <summary>
    /// Roll every hour. Filenames will have yyyyMMddHH appended.
    /// </summary>
    Hour,

    /// <summary>
    /// Roll every minute. Filenames will have yyyyMMddHHmm appended.
    /// </summary>
    Minute
}