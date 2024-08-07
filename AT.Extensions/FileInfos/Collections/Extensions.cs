﻿using AT.Extensions.FileInfos.Boundary;
using AT.Extensions.FileInfos.Comparison;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Collections;
public static class Extensions : Object
{
    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, out Int32 count)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.DeleteFiles(null, out count, Array.Empty<String>());
    }

    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, out Int32 count)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.DeleteFiles(searchOption, out count, Array.Empty<String>());
    }

    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, out Int32 count, params String[] patterns)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        Dictionary<FileInfo, Boolean>? keyValues;
        List<Dictionary<FileInfo, Boolean>>? result = default;
        FileInfo[]? getFiles;
        List<FileInfo>? files = new();
        count = default;
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.GetFiles();
            files.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        // ----------------------------------------------------------------------------------------------------
        if (files is not null)
            if (files.Any())
            {
                keyValues = new();
                result = new();
                foreach (FileInfo fileInfo in files)
                {
                    keyValues.Add(fileInfo, true);
                    result.Add(keyValues);
                    fileInfo.Delete();
                    count++;
                }
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateFiles(null, Array.Empty<String>());
    }

    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateFiles(searchOption, Array.Empty<String>());
    }

    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<FileInfo>? getFiles = default;
        List<FileInfo> result = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.EnumerateFiles();
            result.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
        {
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.EnumerateFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Count() != 0)
                        result.AddRange(getFiles);
            }
        }
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.EnumerateFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Count() != 0)
                        result.AddRange(getFiles);
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetFiles(null, Array.Empty<String>());
    }

    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetFiles(searchOption, Array.Empty<String>());
    }

    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        if (directoryInfo is null)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        FileInfo[]? result = default;
        FileInfo[]? getFiles = default;
        List<FileInfo> files = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.GetFiles();
            files.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        // ----------------------------------------------------------------------------------------------------
        if (files is not null)
            if (files.Any())
                result = files.ToArray();
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static IEnumerable<String?>? GetStringLines(this FileInfo file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }

    public static IEnumerable<String?>? GetStringLines(this FileInfo file, System.Text.Encoding encoding)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        else if (encoding is null)
            throw new ArgumentNullException(nameof(encoding));
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = new(file.FullName, encoding);
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }

    public static byte[] ReadAllBytes(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllBytes(filePath);
    }

    public static IEnumerable<byte>? ReadAllBytes(this FileInfo file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using FileStream stream = file.ThrowIfNotFound().OpenRead();
        using BinaryReader reader = new(stream);
        while (!reader.IsEOF())
            yield return reader.ReadByte();
    }

    public static IEnumerable<byte[]>? ReadAllBytes(this FileInfo file, Int32 length)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using FileStream stream = file.ThrowIfNotFound().OpenRead();
        using BinaryReader reader = new BinaryReader(stream);
        while (!reader.IsEOF())
            yield return reader.ReadBytes(length);
    }

    public static String[] ReadAllLines(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath);
    }

    public static String[] ReadAllLines(this String filePath, System.Text.Encoding encoding)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath, encoding);
    }

    public static IEnumerable<String?>? ReadAllLines(this FileInfo file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.ThrowIfNotFound().OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }

    public static IEnumerable<String?>? ReadAllLines(this FileInfo file, Action<StreamReader>? initializer, Int32 bufferSize = 3 * AT.Infrastructure.DataLength.Bytes.MB)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = new(new BufferedStream(file.ThrowIfNotFound().Open(FileMode.Open, FileAccess.Read, FileShare.Read), bufferSize));
        initializer?.Invoke(reader);
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }
}