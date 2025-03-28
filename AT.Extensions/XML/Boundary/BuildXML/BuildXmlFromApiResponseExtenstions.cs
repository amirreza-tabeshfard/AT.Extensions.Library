namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromApiResponseExtenstions
    : Object
{
    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this String response)
    {
        ArgumentException.ThrowIfNullOrEmpty(response);
        ArgumentException.ThrowIfNullOrWhiteSpace(response);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Parse(response);
        }
        catch (ArgumentException argEx) when (string.IsNullOrEmpty(response))
        {
            throw new InvalidOperationException("The response cannot be null or empty.", argEx);
        }
        catch (ArgumentException argEx) when (string.IsNullOrWhiteSpace(response))
        {
            throw new InvalidOperationException("The response cannot be empty or contain only whitespace.", argEx);
        }
        catch (ArgumentNullException argNullEx) when (response is null)
        {
            throw new InvalidOperationException("The response cannot be null.", argNullEx);
        }
        catch (System.Xml.XmlException xmlEx) when (response.Contains("<"))
        {
            throw new InvalidOperationException("Invalid XML response. The XML format is incorrect.", xmlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the response.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this String response, String rootName)
    {
        ArgumentException.ThrowIfNullOrEmpty(response);
        ArgumentException.ThrowIfNullOrWhiteSpace(response);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(response);
            doc.Root.Name = rootName;
            return doc;
        }
        catch (ArgumentException argEx) when (string.IsNullOrEmpty(response))
        {
            throw new InvalidOperationException("The response cannot be null or empty.", argEx);
        }
        catch (ArgumentException argEx) when (string.IsNullOrWhiteSpace(response))
        {
            throw new InvalidOperationException("The response cannot be empty or contain only whitespace.", argEx);
        }
        catch (ArgumentException argEx) when (string.IsNullOrEmpty(rootName))
        {
            throw new InvalidOperationException("The root name cannot be null or empty.", argEx);
        }
        catch (ArgumentException argEx) when (string.IsNullOrWhiteSpace(rootName))
        {
            throw new InvalidOperationException("The root name cannot be empty or contain only whitespace.", argEx);
        }
        catch (ArgumentNullException argNullEx)
        {
            throw new InvalidOperationException("A required argument was null.", argNullEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("The response does not contain valid XML.", xmlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this HttpResponseMessage response)
    {
        ArgumentNullException.ThrowIfNull(response);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = response.Content.ReadAsStringAsync().Result;
            return System.Xml.Linq.XDocument.Parse(content);
        }
        catch (ArgumentNullException argEx) when (response is null)
        {
            throw new InvalidOperationException("The HTTP response is null.", argEx);
        }
        catch (AggregateException aggEx)
        {
            throw new InvalidOperationException("An error occurred while asynchronously reading the HTTP response content.", aggEx);
        }
        catch (HttpRequestException httpEx)
        {
            throw new InvalidOperationException("Error occurred while reading the HTTP response content.", httpEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("The HTTP response content is not a valid XML.", xmlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while parsing the HttpResponseMessage to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Load(stream);
        }
        catch (ArgumentNullException argEx) when (stream is null)
        {
            throw new InvalidOperationException("The stream is null.", argEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("The stream does not contain valid XML.", xmlEx);
        }
        catch (IOException ioEx)
        {
            throw new InvalidOperationException("An error occurred while reading the stream.", ioEx);
        }
        catch (UnauthorizedAccessException uaEx)
        {
            throw new InvalidOperationException("Access to the stream is unauthorized.", uaEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while parsing the stream to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this System.Xml.XmlReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Load(reader);
        }
        catch (ArgumentNullException argEx) when (reader is null)
        {
            throw new InvalidOperationException("The XmlReader is null.", argEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("The XmlReader does not contain valid XML.", xmlEx);
        }
        catch (IOException ioEx)
        {
            throw new InvalidOperationException("An error occurred while reading from the XmlReader.", ioEx);
        }
        catch (UnauthorizedAccessException uaEx)
        {
            throw new InvalidOperationException("Access to the XmlReader is unauthorized.", uaEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while parsing the XmlReader to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this Byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlString = System.Text.Encoding.UTF8.GetString(data);
            return System.Xml.Linq.XDocument.Parse(xmlString);
        }
        catch (ArgumentNullException argEx) when (data is null)
        {
            throw new InvalidOperationException("The byte array is null.", argEx);
        }
        catch (System.Text.DecoderFallbackException decodeEx)
        {
            throw new InvalidOperationException("The byte array could not be decoded to a valid UTF-8 string.", decodeEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("The byte array does not contain valid XML.", xmlEx);
        }
        catch (ArgumentException argEx)
        {
            throw new InvalidOperationException("The byte array contains invalid data for XML parsing.", argEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while parsing the byte array to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this Dictionary<String, String> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("Root", data.Select(kv => new System.Xml.Linq.XElement(kv.Key, kv.Value))));
            return doc;
        }
        catch (ArgumentNullException argEx) when (data is null)
        {
            throw new InvalidOperationException("The dictionary is null.", argEx);
        }
        catch (ArgumentException argEx)
        {
            throw new InvalidOperationException("The dictionary contains invalid data.", argEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("An error occurred while creating XML from the dictionary.", xmlEx);
        }
        catch (InvalidOperationException opEx)
        {
            throw new InvalidOperationException("There was an invalid operation during the XML creation.", opEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting the dictionary to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this Object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Serialization.XmlSerializer serializer = new(obj.GetType());
            using StringWriter stream = new();
            serializer.Serialize(stream, obj);
            return System.Xml.Linq.XDocument.Parse(stream.ToString());
        }
        catch (ArgumentNullException argEx) when (obj == null)
        {
            throw new InvalidOperationException("The object to serialize is null.", argEx);
        }
        catch (InvalidOperationException invalidOpEx)
        {
            throw new InvalidOperationException("The object type is invalid for serialization.", invalidOpEx);
        }
        catch (IOException ioEx)
        {
            throw new InvalidOperationException("An error occurred while writing the serialized object to the stream.", ioEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("An error occurred during XML serialization.", xmlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while serializing the object to XML.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this System.Xml.Linq.XElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XDocument(element);
        }
        catch (ArgumentNullException argNullEx) when (element is null)
        {
            throw new InvalidOperationException("The provided XElement is null.", argNullEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("XML processing error occurred.", xmlEx);
        }
        catch (InvalidOperationException invalidOpEx)
        {
            throw new InvalidOperationException("There was an invalid operation while processing the XElement to XML.", invalidOpEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XElement.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromApiResponse(this IEnumerable<KeyValuePair<String, String>> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("Root", data.Select(kv => new System.Xml.Linq.XElement(kv.Key, kv.Value))));
            return doc;
        }
        catch (ArgumentNullException argNullEx) when (data is null)
        {
            throw new InvalidOperationException("The provided data collection is null.", argNullEx);
        }
        catch (ArgumentException argEx)
        {
            throw new InvalidOperationException("The data collection contains invalid elements.", argEx);
        }
        catch (System.Xml.XmlException xmlEx)
        {
            throw new InvalidOperationException("There was an error while creating the XML structure.", xmlEx);
        }
        catch (InvalidOperationException invalidOpEx)
        {
            throw new InvalidOperationException("An error occurred during XML document creation.", invalidOpEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting List<KeyValuePair> to XML.", ex);
        }
    }
}