using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Conversion;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
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
}