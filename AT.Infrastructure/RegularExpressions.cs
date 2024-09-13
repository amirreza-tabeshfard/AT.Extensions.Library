namespace AT.Infrastructure;
/// <summary>
/// It doesn't work in (.NET 5, .NET 6) version
/// </summary>
public static partial class RegularExpressions : Object
{
    [System.Text.RegularExpressions.GeneratedRegex(pattern: "\\w+", options: System.Text.RegularExpressions.RegexOptions.IgnoreCase, cultureName: "en-US")]
    public static partial System.Text.RegularExpressions.Regex AlphaNumeric();
}