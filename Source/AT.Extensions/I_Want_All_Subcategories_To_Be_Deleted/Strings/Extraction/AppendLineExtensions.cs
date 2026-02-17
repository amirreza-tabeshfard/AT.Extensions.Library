namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class AppendLineExtensions
{
    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(format);
        // ----------------------------------------------------------------------------------------------------
        return input.AppendFormat(format, arg0).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0, object arg1)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(format);
        // ----------------------------------------------------------------------------------------------------
        return input.AppendFormat(format, arg0, arg1).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0, object arg1, object arg2)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(format);
        // ----------------------------------------------------------------------------------------------------
        return input.AppendFormat(format, arg0, arg1, arg2).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(format);
        // ----------------------------------------------------------------------------------------------------
        return input.AppendFormat(format, args).AppendLine();
    }
}