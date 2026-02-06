using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Strings.Collections;
public static class ToExtenstions
{
    public static byte[] ToBytes(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Byte[] bytes = new Byte[value.Length * sizeof(Char)];
        Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
        // ----------------------------------------------------------------------------------------------------
        return bytes;
    }

    public static ICollection<String> ToChunks(this String value, Int32 chunkSize)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Collections.ObjectModel.Collection<String> result = new();
        if (value != null)
        {
            Int32 cnt = 0;
            System.Text.StringBuilder stringBuilder = new();
            foreach (Char ch in value)
            {
                stringBuilder.Append(ch);
                if (++cnt == chunkSize)
                {
                    cnt = 0;
                    result.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }
            if (cnt > 0)
                result.Add(stringBuilder.ToString());
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static List<String> ToCollection(this String value, Char delimiter = ',')
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!value.Contains(delimiter))
            return new List<String> { value };
        // ----------------------------------------------------------------------------------------------------
        return value.Split(delimiter).ToList();
    }

    public static String[] ToDelimitedArray(this String value, Char delimiter = ',')
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String[] array = value.Split(delimiter);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i < array.Length; i++)
            array[i] = array[i].Trim();
        // ----------------------------------------------------------------------------------------------------
        return array;
    }

    public static List<String> Tokenize(this String value, Func<Char, Boolean> predicate)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        List<String> tokens = new();
        String? token;
        Int32 pos = 0;
        // ----------------------------------------------------------------------------------------------------
        while ((token = value.GetNextToken(predicate, ref pos)) != null)
            tokens.Add(token);
        // ----------------------------------------------------------------------------------------------------
        return tokens;
    }

    public static List<String> Tokenize(this String value, String delimiterChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        HashSet<Char> hashSet = new(delimiterChars, Comparison.StartsWithExtensions.CharComparer.GetEqualityComparer(ignoreCase));
        List<String> tokens = new();
        String? token;
        Int32 pos = 0;
        // ----------------------------------------------------------------------------------------------------
        while ((token = value.GetNextToken(hashSet.Contains, ref pos)) != null)
            tokens.Add(token);
        // ----------------------------------------------------------------------------------------------------
        return tokens;
    }

    public static List<String> ToList(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new List<String> { value };
    }

    public static (MemoryStream MemoryStream, System.Text.Encoding Encoding) ToMemoryStream(this String value, System.Text.Encoding? encoding = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        MemoryStream memoryStream = new MemoryStream();
        (Stream Stream, System.Text.Encoding Encoding) streamInfo = value.ToStream(encoding);
        streamInfo.Stream.CopyTo(memoryStream);
        // ----------------------------------------------------------------------------------------------------
        return (memoryStream, streamInfo.Encoding);
    }

    public static (Stream Stream, System.Text.Encoding Encoding) ToStream(this String value, System.Text.Encoding? encoding = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (encoding is null)
            encoding = System.Text.Encoding.UTF8;
        // ----------------------------------------------------------------------------------------------------
        byte[] bytes = encoding.GetBytes(value);
        // ----------------------------------------------------------------------------------------------------
        return (new MemoryStream(bytes), encoding);
    }

    public static IEnumerable<String> ToTextElements(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(value);
        // ----------------------------------------------------------------------------------------------------
        while (elementEnumerator.MoveNext())
        {
            String textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }
}