namespace AT.Extensions.Strings.Collections;
public static class GetExtensions
{
    public static Int32[] GetAscii(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32[] asciiArr = new Int32[value.Length];
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i < value.Length; i++)
            asciiArr[i] = (Int32)value[i];
        // ----------------------------------------------------------------------------------------------------
        return asciiArr;
    }

    public static IList<String> GetPathParts(this String path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        // ----------------------------------------------------------------------------------------------------
        return path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries)
               .ToList();
    }
}