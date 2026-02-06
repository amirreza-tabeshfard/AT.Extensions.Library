using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class ConvertExtensions
{
    #region Enum(s)

    public enum WordCase
    {
        AllLower = 0,
        AllUpper = 1,
        Title = 2,
        Sentence = 3
    }

    #endregion

    #region Method(s): Private

    private static String ConvertToWords(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String pattern = "(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])";
            return System.Text.RegularExpressions.Regex.Replace(value, pattern, " ").RemoveExcessWhiteSpace();
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }

    #endregion

    public static String Convert(this String value, System.Text.Encoding targetEncoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(targetEncoding);
        // ----------------------------------------------------------------------------------------------------
        return Convert(value, targetEncoding, System.Text.Encoding.Unicode);
    }

    public static String Convert(this String value, System.Text.Encoding targetEncoding, System.Text.Encoding sourceEncoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(targetEncoding);
        ArgumentNullException.ThrowIfNull(sourceEncoding);
        // ----------------------------------------------------------------------------------------------------
        byte[] sourceBytes = sourceEncoding.GetBytes(value);
        byte[] targetBytes = System.Text.Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);
        String result = targetEncoding.GetString(targetBytes, 0, targetBytes.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String ConvertCRLFToBreaks(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new System.Text.RegularExpressions.Regex("(\r\n|\n)").Replace(value, "<br/>");
    }

    public static String ConvertPascalOrCamelCaseToWords(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return ConvertToWords(value);
        }
        catch (Exception ex)
        {
            Exception except = new Exception(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static String ConvertPascalOrCamelCaseToWords(this String value, WordCase wordCase)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String output = ConvertToWords(value);
            switch (wordCase)
            {
                case WordCase.AllLower:
                    {
                        output = output.ToLower();
                    }
                    break;

                case WordCase.AllUpper:
                    {
                        output = output.ToUpper();
                    }
                    break;

                case WordCase.Title:
                    {
                        output = output.ToTitleCase();
                    }
                    break;

                case WordCase.Sentence:
                    {
                        output = output.ToSentenceCase();
                    }
                    break;
            }
            return output;
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }

    public static String ConvertToCamelCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            value.ToTitleCase();
            value = value.Remove(1).ToLower() + value.Substring(1);
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", String.Empty).Trim();
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }

    public static String ConvertToPascalCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            value.ToTitleCase();
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", String.Empty).Trim();
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }
}