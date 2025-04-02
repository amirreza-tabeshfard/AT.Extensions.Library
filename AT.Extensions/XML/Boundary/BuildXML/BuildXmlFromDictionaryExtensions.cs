namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromDictionaryExtensions
    : Object
{
    public static String BuildXmlFromDictionary(this Dictionary<String, String> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<String, String> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while constructing the XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred during XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<String, Int32> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<string, int> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while constructing the XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing the dictionary data.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred during XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<String, Double> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<string, double> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (DivideByZeroException ex) when (ex.Message.Contains("divide", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A division by zero error occurred while processing numerical values.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while constructing the XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing the dictionary data.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred during XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<String, DateTime> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<string, DateTime> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value.ToString("yyyy-MM-dd")}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while converting DateTime to string.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing the dictionary data.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing the DateTime values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Int32, String> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<int, string> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred due to an invalid key in the dictionary.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while processing dictionary values.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing dictionary data.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing dictionary keys or values.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Int32, Int32> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<int, int> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred due to an invalid key in the dictionary.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while processing dictionary values.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing dictionary data.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(xmlString)))
        {
            throw new InvalidOperationException("The StringBuilder object was disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing dictionary keys or values.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A stack overflow error occurred due to excessive recursion or memory use.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Int32, Double> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<int, double> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred due to an invalid key in the dictionary.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while converting dictionary values to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing dictionary data.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(xmlString)))
        {
            throw new InvalidOperationException("The StringBuilder object was disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing dictionary values.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A stack overflow error occurred due to excessive recursion or memory use.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Int32, DateTime> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<int, DateTime> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value.ToString("yyyy-MM-dd")}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred due to an invalid key in the dictionary.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while converting DateTime values to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing dictionary data.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(xmlString)))
        {
            throw new InvalidOperationException("The StringBuilder object was disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing dictionary values.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A stack overflow error occurred due to excessive recursion or memory use.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<DateTime, String> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<DateTime, string> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key.ToString("yyyy-MM-dd")}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dictionary)))
        {
            throw new ArgumentNullException(nameof(dictionary), "The provided dictionary is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred due to an invalid key in the dictionary.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("encoding", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An encoding error occurred while processing the XML content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("format", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A format error occurred while converting DateTime keys to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation was encountered during XML generation.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A key was not found while building the XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A null reference occurred while processing dictionary data.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(xmlString)))
        {
            throw new InvalidOperationException("The StringBuilder object was disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An overflow error occurred while processing dictionary values.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A stack overflow error occurred due to excessive recursion or memory use.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("system", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("A system error occurred while processing the XML.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unauthorized access detected while generating the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("XML", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while constructing the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<DateTime, Int32> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<DateTime, Int32> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key.ToString("yyyy-MM-dd")}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dictionary"))
        {
            throw new InvalidOperationException("The provided dictionary is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid date format"))
        {
            throw new InvalidOperationException("The dictionary contains invalid date formats.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Item has already been added"))
        {
            throw new InvalidOperationException("Duplicate entries found in the dictionary.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory limit exceeded while building XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error while building XML from dictionary"))
        {
            throw new InvalidOperationException("An error occurred during the XML building process.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<DateTime, Double> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<DateTime, Double> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key.ToString("yyyy-MM-dd")}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dictionary"))
        {
            throw new InvalidOperationException("The provided dictionary is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid date format"))
        {
            throw new InvalidOperationException("The dictionary contains invalid date formats.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Item has already been added"))
        {
            throw new InvalidOperationException("Duplicate entries found in the dictionary.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory limit exceeded while building XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error while building XML from dictionary"))
        {
            throw new InvalidOperationException("An error occurred during the XML building process.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("Value was either too large or too small for a Double"))
        {
            throw new InvalidOperationException("One of the dictionary values is outside the range of a Double.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<DateTime, DateTime> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<DateTime, DateTime> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key.ToString("yyyy-MM-dd")}\">{item.Value.ToString("yyyy-MM-dd")}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dictionary"))
        {
            throw new InvalidOperationException("The provided dictionary is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("DateTime"))
        {
            throw new InvalidOperationException("Error formatting DateTime to string. Please check the date format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error while building XML from dictionary"))
        {
            throw new InvalidOperationException("An error occurred while processing the dictionary to XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("StringBuilder"))
        {
            throw new InvalidOperationException("StringBuilder initialization failed. Null reference encountered.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("key"))
        {
            throw new InvalidOperationException("Error processing the dictionary key.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("Item"))
        {
            throw new InvalidOperationException("Error appending an item to the XML string.", ex);
        }
        catch (Exception ex) when (ex is ArgumentNullException)
        {
            throw new InvalidOperationException("A null argument was provided, specifically the dictionary argument.", ex);
        }
        catch (Exception ex) when (ex is FormatException)
        {
            throw new InvalidOperationException("A formatting error occurred, possibly related to DateTime conversion.", ex);
        }
        catch (Exception ex) when (ex is InvalidOperationException)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the dictionary to XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<String, Boolean> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<String, Boolean> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' cannot be null while building XML from dictionary.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("Error in formatting while building XML from dictionary.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("Arithmetic operation resulted in an overflow"))
        {
            throw new InvalidOperationException("Overflow occurred while building XML from dictionary.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("key"))
        {
            throw new InvalidOperationException($"The dictionary contains invalid keys while building XML: {ex.ParamName}", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A required key was not found while building XML from dictionary.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Out of memory while building XML from dictionary.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Boolean, String> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<Boolean, String> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' cannot be null while building XML from dictionary.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("key"))
        {
            throw new InvalidOperationException($"The dictionary contains invalid keys while building XML: {ex.ParamName}", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("Error in formatting while building XML from dictionary.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("Arithmetic operation resulted in an overflow"))
        {
            throw new InvalidOperationException("Overflow occurred while building XML from dictionary.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Out of memory while building XML from dictionary.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A required key was not found while building XML from dictionary.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from dictionary.", ex);
        }
    }

    public static String BuildXmlFromDictionary(this Dictionary<Boolean, Int32> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder? xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = new("<Root>");

            foreach (KeyValuePair<Boolean, Int32> item in dictionary)
                xmlString.Append($"<Item key=\"{item.Key}\">{item.Value}</Item>");

            xmlString.Append("</Root>");
            return xmlString.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' cannot be null while building XML from dictionary.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("key"))
        {
            throw new InvalidOperationException($"The dictionary contains invalid keys while building XML: {ex.ParamName}", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("Error in formatting while building XML from dictionary.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("Arithmetic operation resulted in an overflow"))
        {
            throw new InvalidOperationException("Overflow occurred while building XML from dictionary.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("Unable to cast"))
        {
            throw new InvalidOperationException("Invalid type conversion while building XML from dictionary.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Out of memory while building XML from dictionary.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A required key was not found while building XML from dictionary.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' is out of range while building XML from dictionary.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The operation is not supported while building XML from dictionary.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from dictionary.", ex);
        }
    }
}