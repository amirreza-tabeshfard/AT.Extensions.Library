using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class LimitExtensions
{
    public static String Limit(this String value, Int32 maxLength, String? suffix = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (suffix.IsNotNullOrEmpty())
            maxLength = maxLength - suffix.Length;

        if (value.Length <= maxLength)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(value.Substring(0, maxLength).Trim(), suffix ?? String.Empty);
    }

    public static String LimitLength(this String value, Int32 maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= maxLength)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(0, maxLength);
    }

    public static String LimitSentenceLength(this String value, Int32 maximumLenght)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= maximumLenght) 
            return value;
        // ----------------------------------------------------------------------------------------------------
        String[] words = value.Split(' ');
        String paragraphToReturn = String.Empty;
        // ----------------------------------------------------------------------------------------------------
        foreach (String word in words)
        {
            if ((paragraphToReturn.Length + word.Length + 3) > maximumLenght)
            {
                paragraphToReturn = paragraphToReturn.Trim() + "...";
                break;
            }
            paragraphToReturn += word + " ";
        }
        // ----------------------------------------------------------------------------------------------------
        return paragraphToReturn;
    }
}