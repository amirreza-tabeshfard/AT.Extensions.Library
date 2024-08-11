﻿namespace AT.Extensions.FileInfos.Collections;
public static class GetStringLinesExtensions : Object
{
    public static IEnumerable<String?>? GetStringLines(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }

    public static IEnumerable<String?>? GetStringLines(this FileInfo file, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = new(file.FullName, encoding);
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }
}