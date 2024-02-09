namespace AT.Extensions.Strings.Extraction;
public static class StringBuilderExtensions : Object
{
    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0)
    {
        return input.AppendFormat(format, arg0).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0, object arg1)
    {
        return input.AppendFormat(format, arg0, arg1).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, object arg0, object arg1, object arg2)
    {
        return input.AppendFormat(format, arg0, arg1, arg2).AppendLine();
    }

    public static System.Text.StringBuilder AppendLineFormat(this System.Text.StringBuilder input, String format, params object[] args)
    {
        return input.AppendFormat(format, args).AppendLine();
    }
}