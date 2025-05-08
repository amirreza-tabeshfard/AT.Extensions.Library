namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromJsonExtensions
    : Object
{
    #region Private Method(s)
    
    private static void ParseJsonElement(System.Xml.Linq.XElement parent, System.Text.Json.JsonElement element)
    {
        switch (element.ValueKind)
        {
            case System.Text.Json.JsonValueKind.Object:
                {
                    foreach ((System.Text.Json.JsonProperty property, System.Xml.Linq.XElement child) in from property in element.EnumerateObject()
                                                                                                         let child = new System.Xml.Linq.XElement(property.Name)
                                                                                                         select (property, child))
                    {
                        ParseJsonElement(child, property.Value);
                        parent.Add(child);
                    }
                }
                break;

            case System.Text.Json.JsonValueKind.Array:
                {
                    foreach ((System.Text.Json.JsonElement item, System.Xml.Linq.XElement child) in from item in element.EnumerateArray()
                                                                                                    let child = new System.Xml.Linq.XElement("Item")
                                                                                                    select (item, child))
                    {
                        ParseJsonElement(child, item);
                        parent.Add(child);
                    }
                }
                break;

            case System.Text.Json.JsonValueKind.String:
            case System.Text.Json.JsonValueKind.Number:
            case System.Text.Json.JsonValueKind.True:
            case System.Text.Json.JsonValueKind.False:
                {
                    parent.Value = element.ToString();
                }
                break;

            case System.Text.Json.JsonValueKind.Null:
                {
                    parent.Value = String.Empty;
                }
                break;

            default:
                throw new NotSupportedException($"Unsupported System.Text.Json.JsonValueKind: {element.ValueKind}");
        }
    } 

    #endregion

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this String json)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.Json.JsonDocument jsonDoc = System.Text.Json.JsonDocument.Parse(json);
            System.Xml.Linq.XElement root = new("Root");
            ParseJsonElement(root, jsonDoc.RootElement);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("json"))
        {
            throw new ArgumentException("Input JSON string is null or empty.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid"))
        {
            throw new FormatException("Input JSON string format is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("JSON parsing exceeded the depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot get the value from the JSON element.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JSON structure is not valid or expected token is missing.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JSON contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JSON value could not be converted properly.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Cannot create a JSON value from a"))
        {
            throw new NotSupportedException("Unsupported type found when creating JSON value.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while processing JSON.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred while parsing JSON.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system exception occurred during JSON processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this Stream jsonStream)
    {
        ArgumentNullException.ThrowIfNull(jsonStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new(jsonStream, System.Text.Encoding.UTF8);
            String json = reader.ReadToEnd();
            return json.BuildXmlFromJson();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonStream"))
        {
            throw new ArgumentNullException("Input stream is null.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate"))
        {
            throw new System.Text.DecoderFallbackException("Encoding issue occurred while reading the stream.", ex);
        }
        catch (EndOfStreamException ex) when (ex.Message.Contains("unexpected end"))
        {
            throw new EndOfStreamException("Unexpected end of stream encountered.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid"))
        {
            throw new FormatException("Stream content has an invalid JSON format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("JSON parsing from stream exceeded the depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot get value from JSON element read from stream.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("I/O"))
        {
            throw new IOException("I/O error occurred while reading from the stream.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("Invalid JSON structure in the stream.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("Unterminated string found in the JSON stream.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("Conversion failure occurred while reading JSON from stream.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Stream does not support reading"))
        {
            throw new NotSupportedException("The input stream does not support reading.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new ObjectDisposedException("Input stream has been disposed before reading.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while reading from the stream.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during JSON stream parsing.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing the stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON stream.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this Byte[] jsonBytes)
    {
        ArgumentNullException.ThrowIfNull(jsonBytes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            return json.BuildXmlFromJson();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonBytes"))
        {
            throw new ArgumentNullException("Input byte array is null.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate"))
        {
            throw new System.Text.DecoderFallbackException("Encoding issue occurred while converting bytes to string.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid"))
        {
            throw new FormatException("The byte array content is not a valid JSON format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("JSON parsing from bytes exceeded the depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot get value from JSON element built from bytes.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("Invalid JSON structure detected in the byte array.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("Unterminated string found in the JSON byte array.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("Conversion failure occurred while processing JSON from bytes.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while processing the byte array.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during JSON byte array parsing.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing the byte array.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON bytes.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this System.Text.Json.JsonDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Root");
            ParseJsonElement(root, document.RootElement);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("document"))
        {
            throw new ArgumentNullException("Input JsonDocument is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JsonDocument exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from JsonDocument root element.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument contains invalid JSON value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument structure is invalid or missing expected tokens.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("JsonDocument"))
        {
            throw new ObjectDisposedException("JsonDocument has been disposed before parsing.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while building XML from JsonDocument.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JsonDocument.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JsonDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JsonDocument.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this System.Text.Json.JsonElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Root");
            ParseJsonElement(root, element);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new ArgumentNullException("Input JsonElement is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JsonElement exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from JsonElement.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JsonElement contains invalid JSON value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JsonElement contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JsonElement structure is invalid or missing expected tokens.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported value kind"))
        {
            throw new NotSupportedException("JsonElement contains unsupported value kind.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("JsonElement"))
        {
            throw new ObjectDisposedException("JsonElement has been disposed before processing.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while building XML from JsonElement.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JsonElement.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JsonElement.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JsonElement.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this String json, String rootName)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.Json.JsonDocument jsonDoc = System.Text.Json.JsonDocument.Parse(json);
            System.Xml.Linq.XElement root = new(rootName);
            ParseJsonElement(root, jsonDoc.RootElement);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("json"))
        {
            throw new ArgumentException("Input JSON string is null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentException("Input root name string is null or empty.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new FormatException("Input JSON string has invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JSON string exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from the parsed JsonDocument.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JSON string contains invalid value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JSON string contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JSON structure is invalid or missing expected tokens.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported value kind"))
        {
            throw new NotSupportedException("JSON contains unsupported value kind.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("JsonDocument"))
        {
            throw new ObjectDisposedException("JsonDocument has been disposed before processing.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while building XML from JSON string.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JSON string.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JSON string.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON string.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this Stream jsonStream, String rootName)
    {
        ArgumentNullException.ThrowIfNull(jsonStream);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new(jsonStream, System.Text.Encoding.UTF8);
            String json = reader.ReadToEnd();
            return json.BuildXmlFromJson(rootName);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentException("Input root name string is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonStream"))
        {
            throw new ArgumentNullException("Input JSON stream is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new FormatException("Input JSON string read from stream has invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JSON string from stream exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from the parsed JsonDocument.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new IOException("An unexpected IO error occurred while reading the JSON stream.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("closed"))
        {
            throw new IOException("JSON stream is closed during reading.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JSON stream contains invalid value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JSON stream contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JSON structure read from stream is invalid or missing expected tokens.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported value kind"))
        {
            throw new NotSupportedException("JSON stream contains unsupported value kind.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("StreamReader"))
        {
            throw new ObjectDisposedException("StreamReader has been disposed before reading the JSON stream.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while reading the JSON stream.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JSON stream.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JSON stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON stream.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this Byte[] jsonBytes, String rootName)
    {
        ArgumentNullException.ThrowIfNull(jsonBytes);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            return json.BuildXmlFromJson(rootName);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentException("Input root name string is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonBytes"))
        {
            throw new ArgumentNullException("Input JSON byte array is null.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate bytes"))
        {
            throw new System.Text.DecoderFallbackException("Failed to decode JSON bytes using UTF-8 encoding.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new FormatException("Input JSON string parsed from bytes has invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JSON string from bytes exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from the parsed JsonDocument.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JSON bytes contain invalid value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JSON bytes contain an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JSON structure parsed from bytes is invalid or missing expected tokens.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported value kind"))
        {
            throw new NotSupportedException("JSON bytes contain unsupported value kind.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while processing JSON bytes.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JSON bytes.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JSON bytes.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JSON bytes.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this System.Text.Json.JsonDocument document, String rootName)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(rootName);
            ParseJsonElement(root, document.RootElement);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentException("Input root name string is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("document"))
        {
            throw new ArgumentNullException("Input JsonDocument is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new FormatException("The JsonDocument contains an invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("Parsing JsonDocument exceeded the maximum depth limit.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot get the value"))
        {
            throw new InvalidOperationException("Cannot retrieve a value from the JsonDocument.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("The JSON value could not be converted"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument contains invalid value types.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unterminated string"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument contains an unterminated string.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Expected"))
        {
            throw new Newtonsoft.Json.JsonException("JsonDocument structure is invalid or missing expected tokens.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported value kind"))
        {
            throw new NotSupportedException("JsonDocument contains unsupported value kind.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while processing JsonDocument.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("Stack overflow occurred during processing JsonDocument.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while processing JsonDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JsonDocument.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromJson(this System.Text.Json.JsonElement element, String rootName)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(rootName);
            ParseJsonElement(root, element);
            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentException("Root name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new ArgumentNullException("JsonElement cannot be null.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Unexpected character"))
        {
            throw new Newtonsoft.Json.JsonException("The JsonElement contains unexpected characters that cannot be parsed.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex) when (ex.Message.Contains("Invalid value"))
        {
            throw new Newtonsoft.Json.JsonException("The JsonElement contains an invalid value type.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot process"))
        {
            throw new InvalidOperationException("Unable to process the JsonElement due to an unexpected error.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Depth limit"))
        {
            throw new InvalidOperationException("The JsonElement exceeds the maximum allowed depth for XML conversion.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid JSON"))
        {
            throw new FormatException("The JsonElement contains an invalid JSON format that could not be parsed.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("null value"))
        {
            throw new NullReferenceException("A required value was found to be null during the XML building process.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("System ran out of memory while processing the JsonElement.", ex);
        }
        catch (StackOverflowException ex) when (ex.Message.Contains("stack"))
        {
            throw new StackOverflowException("A stack overflow occurred while processing the JsonElement.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred while building XML from JsonElement.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from JsonElement with root name.", ex);
        }
    }
}