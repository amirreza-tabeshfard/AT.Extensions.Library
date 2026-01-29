namespace AT.Extensions.Strings.Extraction;
public static class AsPrettyExtensions
{
    public static String AsPrettyJson(this String json)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        // ----------------------------------------------------------------------------------------------------
        const String INDENT_STRING = "    ";
        Int32 indentation = default;
        Int32 quoteCount = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == default
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;
            // ----------------------------------------------------------------------------------------------------
            return String.Concat(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static String AsPrettyXml(this String xmlString)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlString);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        doc.LoadXml(xmlString);
        // ----------------------------------------------------------------------------------------------------
        return AsPrettyXml(doc);
    }

    public static String AsPrettyXml(this System.Xml.XmlDocument doc)
    {
        ArgumentNullException.ThrowIfNull(doc);
        // ----------------------------------------------------------------------------------------------------
        StringWriter stringWriter = new();
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlTextWriter xmlTextWriter = new(stringWriter);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            doc.WriteTo(xmlTextWriter);
            // ----------------------------------------------------------------------------------------------------
            return stringWriter.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            stringWriter.Dispose();
        }
    }
}