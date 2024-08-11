namespace AT.Extensions.FileInfos.Mover;
public static class ChangeExtensions : Object
{
    #region Method(s): Private

    private static T ParamNotNull<T>(this T? obj, String ParameterName, String? Message = null)
        where T : class
    {
        return obj ?? throw new ArgumentException(Message ?? $"Missing reference for parameter {ParameterName}", ParameterName);
    }

    #endregion

    public static FileInfo ChangeExtension(this FileInfo file, String? extension)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(Path.ChangeExtension(file.ParamNotNull(nameof(file)).FullName, extension));
    }
}