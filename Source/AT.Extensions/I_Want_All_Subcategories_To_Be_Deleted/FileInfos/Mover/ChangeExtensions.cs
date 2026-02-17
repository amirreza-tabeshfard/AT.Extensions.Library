namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Mover;
public static class ChangeExtensions
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