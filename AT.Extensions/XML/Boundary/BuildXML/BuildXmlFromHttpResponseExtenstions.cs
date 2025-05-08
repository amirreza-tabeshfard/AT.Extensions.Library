namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromHttpResponseExtenstions
    : Object
{
    public static System.Xml.Linq.XDocument BuildXmlFromHttpResponse(this HttpResponseMessage response)
    {
        ArgumentNullException.ThrowIfNull(response);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = response.Content.ReadAsStringAsync().Result;
            return System.Xml.Linq.XDocument.Parse(content);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null and cannot be parsed into XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("There is an error in XML document"))
        {
            throw new InvalidOperationException("The content of the HTTP response contains invalid XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException("Malformed XML content in the HTTP response at a specific line.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("HTTP response content format is invalid for XML parsing.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate bytes"))
        {
            throw new InvalidOperationException("Failed to decode the HTTP response content to a valid string.", ex);
        }
        catch (System.AggregateException ex) when (ex.InnerException is System.Xml.XmlException)
        {
            throw new InvalidOperationException("The HTTP response content triggered an XML parsing error through an aggregated exception.", ex);
        }
        catch (TaskCanceledException ex) when (ex.Message.Contains("A task was canceled"))
        {
            throw new InvalidOperationException("Reading the HTTP response content was canceled.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("Unexpected end of stream while reading HTTP response content.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Content type is not supported"))
        {
            throw new InvalidOperationException("The HTTP response content type is not supported for XML parsing.", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("error occurred while sending the request"))
        {
            throw new InvalidOperationException("There was a problem sending or receiving the HTTP request.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while parsing HTTP response content to XML.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromHttpResponse(this HttpResponseMessage response, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] content = response.Content.ReadAsByteArrayAsync().Result;
            System.Xml.XmlDocument xmlDoc = new();
            using (MemoryStream stream = new(content))
            using (StreamReader reader = new(stream, encoding))
            {
                xmlDoc.Load(reader);
            }
            return xmlDoc;
        }
        catch (AggregateException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("Failed to read the HTTP response content due to an inner exception.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Invalid argument provided: {ex.ParamName}.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode the HTTP response content with the specified encoding.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the HTTP response content is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while building the XmlDocument from the HTTP response.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the HTTP response content.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The specified operation is not supported while reading the HTTP response content.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null)
        {
            throw new InvalidOperationException($"The object '{ex.ObjectName}' was disposed during the process of building the XmlDocument.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("There is not enough memory to build the XmlDocument from the HTTP response.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The HTTP response content is not a well-formed XML.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The operation to read the HTTP response content was canceled.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access occurred while processing the HTTP response.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XmlDocument from the HTTP response.", ex);
        }
    }

    public static String BuildXmlFromHttpResponse(this HttpResponseMessage response, Boolean formatted)
    {
        ArgumentNullException.ThrowIfNull(response);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(response.Content.ReadAsStringAsync().Result);

            if (formatted)
                return xml.ToString(System.Xml.Linq.SaveOptions.None);
            else
                return xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (AggregateException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("Failed to read the HTTP response content due to an inner exception.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Invalid argument provided: {ex.ParamName}.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the HTTP response content is invalid and cannot be parsed as XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while parsing or formatting the XML content.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the HTTP response content.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("An unsupported operation was attempted during XML building from the HTTP response.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null)
        {
            throw new InvalidOperationException($"The object '{ex.ObjectName}' was disposed during the XML parsing process.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("There is not enough memory available to parse and format the XML content.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The HTTP response content could not be parsed because it is not a well-formed XML.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The operation to read the HTTP response content was canceled.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access occurred while processing the HTTP response.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML string from the HTTP response.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHttpResponse(this HttpResponseMessage response, String rootElementName)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(response.Content.ReadAsStringAsync().Result);
            return xml.Element(rootElementName) ?? throw new InvalidOperationException($"Root element '{rootElementName}' not found.");
        }
        catch (AggregateException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("Failed to read the HTTP response content due to an inner exception.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Invalid argument provided: {ex.ParamName}.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the HTTP response content is invalid and cannot be parsed as XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while accessing or parsing the XML content.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the HTTP response content.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("The specified root element name was not found in the XML document.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("An unsupported operation was attempted while handling the XML document.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference occurred during the process of building the XML element.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null)
        {
            throw new InvalidOperationException($"The object '{ex.ObjectName}' was disposed during the XML parsing process.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("There is not enough memory to parse and process the XML document.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The HTTP response content is not a well-formed XML and cannot be parsed.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The operation to read the HTTP response content was canceled.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access occurred while processing the HTTP response.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XElement from the HTTP response.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromHttpResponse(this HttpResponseMessage response, Func<String, String> contentTransformer)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(contentTransformer);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = response.Content.ReadAsStringAsync().Result;
            String transformedContent = contentTransformer(content);
            return System.Xml.Linq.XDocument.Parse(transformedContent);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("contentTransformer"))
        {
            throw new InvalidOperationException("The content transformer function is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while reading the HTTP content.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The transformed content has an invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred during XML parsing.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred in the transformed content.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content was canceled.", ex);
        }
        catch (ObjectDisposedException ex)
        {
            throw new InvalidOperationException("The HTTP response content has already been disposed.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The HTTP content type is not supported for reading.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromHttpResponse(this HttpResponseMessage response, System.Xml.XmlReaderSettings settings)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            Stream content = response.Content.ReadAsStreamAsync().Result;
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(content, settings))
            {
                xmlDoc.Load(reader);
            }
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("settings"))
        {
            throw new InvalidOperationException("The XmlReaderSettings is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content stream.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while accessing the HTTP content stream.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred during the XML document loading process.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred while loading the XML document.", ex);
        }
        catch (ObjectDisposedException ex)
        {
            throw new InvalidOperationException("The HTTP response content stream has already been disposed.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The HTTP content stream type is not supported for reading.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content stream was canceled.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security error occurred while accessing the HTTP content stream.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access was attempted while reading the HTTP content stream.", ex);
        }
        catch (EndOfStreamException ex)
        {
            throw new InvalidOperationException("Unexpected end of stream occurred while reading the HTTP content.", ex);
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidOperationException("Invalid data was encountered while reading the HTTP content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromHttpResponse(this HttpResponseMessage response, Stream outputStream)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(outputStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Stream content = response.Content.ReadAsStreamAsync().Result;
            content.CopyTo(outputStream);
            outputStream.Position = 0;

            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.Load(outputStream);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("outputStream"))
        {
            throw new InvalidOperationException("The output stream is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content stream.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while accessing the HTTP content stream.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred during the XML document loading process.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("outputStream"))
        {
            throw new InvalidOperationException("The output stream has been disposed before use.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpContent"))
        {
            throw new InvalidOperationException("The HTTP content stream has been disposed before reading.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The stream does not support the required operation.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content stream was canceled.", ex);
        }
        catch (EndOfStreamException ex)
        {
            throw new InvalidOperationException("Unexpected end of stream occurred while copying the HTTP content.", ex);
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidOperationException("Invalid data was encountered while copying the HTTP content.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred while loading the XML document from the stream.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security error occurred while accessing or writing to the stream.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access was attempted while accessing or writing to the stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while copying the response and building the XML document.", ex);
        }
    }

    public static String BuildXmlFromHttpResponse(this HttpResponseMessage response, System.Text.Encoding encoding, Boolean formatted)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
            String content = encoding.GetString(bytes);
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(content);

            return formatted ? xml.ToString(System.Xml.Linq.SaveOptions.None) : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("The encoding is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content as byte array.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while reading the HTTP content as byte array.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bytes"))
        {
            throw new InvalidOperationException("Invalid byte array was provided for encoding.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("An error occurred while decoding the byte array using the specified encoding.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred during the XML parsing process.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred while parsing the content string.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The encoding does not support decoding the provided byte array.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpContent"))
        {
            throw new InvalidOperationException("The HTTP content stream has been disposed before reading as byte array.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content as byte array was canceled.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the encoded and formatted XML string.", ex);
        }
    }

    public static System.Xml.Linq.XElement? BuildXmlFromHttpResponse(this HttpResponseMessage response, String rootElementName, Boolean throwIfMissing)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(response.Content.ReadAsStringAsync().Result);
            System.Xml.Linq.XElement? element = xml.Element(rootElementName);

            if (element == null && throwIfMissing)
                throw new InvalidOperationException($"Required root element '{rootElementName}' not found.");

            return element;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name is null, empty, or consists only of white-space characters.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content as string.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while reading the HTTP content as string.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred during the XML parsing process.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpContent"))
        {
            throw new InvalidOperationException("The HTTP content stream has been disposed before reading as string.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred while parsing the HTTP content string.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content as string was canceled.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the System.Xml.Linq.XElement.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromHttpResponse(this HttpResponseMessage response, Action<System.Xml.XmlDocument> configure)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(configure);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            String content = response.Content.ReadAsStringAsync().Result;
            xmlDoc.LoadXml(content);
            configure(xmlDoc);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("response"))
        {
            throw new InvalidOperationException("The HTTP response is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("configure"))
        {
            throw new InvalidOperationException("The configure action is null.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.IO.IOException)
        {
            throw new InvalidOperationException("An IO error occurred while reading the HTTP content as string.", ex);
        }
        catch (AggregateException ex) when (ex.InnerException is System.Net.Http.HttpRequestException)
        {
            throw new InvalidOperationException("An HTTP request error occurred while reading the HTTP content as string.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred while loading the XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("An invalid operation occurred while executing the configure action.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpContent"))
        {
            throw new InvalidOperationException("The HTTP content stream has been disposed before reading as string.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML parsing error occurred while loading the HTTP content.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new InvalidOperationException("The task to read the HTTP content as string was canceled.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building and configuring the System.Xml.XmlDocument.", ex);
        }
    }
}