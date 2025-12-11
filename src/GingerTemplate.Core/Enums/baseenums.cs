namespace GingerTemplate.Core.Enums;

/// <summary>
/// Base enumerations used across the application.
/// </summary>
/// <remarks>
/// This file contains foundational enums that are utilized in multiple modules.
/// </remarks>

public enum BaseStatus
{
    Enabled,
    Disabled,
    Pending,
    Archived
}

public enum PriorityLevel
{
    Low,
    Medium,
    High,
    Critical
}

public enum EnvironmentType
{
    Development,
    Staging,
    Production,
    Testing
}

public enum LogLevel
{
    Trace,
    Debug,
    Info,
    Warn,
    Error,
    Fatal
}

public enum NotificationType
{
    Email,
    SMS,
    PushNotification,
    InApp
}

public enum AccessLevel
{
    Read,
    Write,
    Execute,
    Admin
}

public enum ThemeMode
{
    Light,
    Dark,
    SystemDefault
}

public enum DataFormat
{
    Json,
    Xml,
    Csv,
    Yaml
}

public enum SortOrder
{
    Ascending,
    Descending
}

public enum FileType
{
    Document,
    Image,
    Video,
    Audio,
    Archive
}

public enum ConnectionStatus
{
    Connected,
    Disconnected,
    Connecting,
    Reconnecting
}

public enum UsStates
{
    Alabama,
    Alaska,
    Arizona,
    Arkansas,
    California,
    Colorado,
    Connecticut,
    Delaware,
    Florida,
    Georgia,
    Hawaii,
    Idaho,
    Illinois,
    Indiana,
    Iowa,
    Kansas,
    Kentucky,
    Louisiana,
    Maine,
    Maryland,
    Massachusetts,
    Michigan,
    Minnesota,
    Mississippi,
    Missouri,
    Montana,
    Nebraska,
    Nevada,
    NewHampshire,
    NewJersey,
    NewMexico,
    NewYork,
    NorthCarolina,
    NorthDakota,
    Ohio,
    Oklahoma,
    Oregon,
    Pennsylvania,
    RhodeIsland,
    SouthCarolina,
    SouthDakota,
    Tennessee,
    Texas,
    Utah,
    Vermont,
    Virginia,
    Washington,
    WestVirginia,
    Wisconsin,
    Wyoming
}

public enum CurrencyType
{
    USD,
    EUR,
    GBP,
    JPY,
    AUD,
    CAD,
    CHF,
    CNY,
    SEK,
    NZD
}

public enum WeekDays
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

public enum MonthOfYear
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}