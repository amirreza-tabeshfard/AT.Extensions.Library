namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromHttpRequestExtenstions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Request", new System.Xml.Linq.XElement("Method", request.Method), new System.Xml.Linq.XElement("Path", request.Path));

            return root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The HttpRequest parameter is null while building XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Failed to build XML from Microsoft.AspNetCore.Http.HttpRequest."))
        {
            throw new InvalidOperationException("An invalid operation occurred during building XML from HttpRequest.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpRequest"))
        {
            throw new InvalidOperationException("The HttpRequest object was disposed before building XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("An XML processing error occurred while building XML from HttpRequest.", ex);
        }
        catch (System.IO.IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new InvalidOperationException("An IO error occurred while accessing HttpRequest data to build XML.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format error occurred while building XML from HttpRequest.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A security error occurred while trying to build XML from HttpRequest.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Unauthorized access error occurred while building XML from HttpRequest.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unsupported operation was attempted while building XML from HttpRequest.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while creating XElement during XML building from HttpRequest.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unexpected system error occurred while building XML from HttpRequest.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from HttpRequest.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, String rootName)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(rootName, new System.Xml.Linq.XElement("Method", request.Method), new System.Xml.Linq.XElement("Path", request.Path));
            return root;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new InvalidOperationException("The rootName parameter is null, empty, or whitespace while building XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The HttpRequest parameter is null while building XML.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format error occurred while building XML from HttpRequest with custom root.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Failed to build XML from Microsoft.AspNetCore.Http.HttpRequest with custom root."))
        {
            throw new InvalidOperationException("An invalid operation occurred during building XML from HttpRequest with custom root.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unsupported operation was attempted while building XML from HttpRequest with custom root.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpRequest"))
        {
            throw new InvalidOperationException("The HttpRequest object was disposed before building XML with custom root.", ex);
        }
        catch (System.IO.IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new InvalidOperationException("An IO error occurred while accessing HttpRequest data to build XML with custom root.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A security error occurred while trying to build XML from HttpRequest with custom root.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while creating XElement during XML building from HttpRequest with custom root.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("An XML processing error occurred while building XML from HttpRequest with custom root.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Unauthorized access error occurred while building XML from HttpRequest with custom root.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unexpected system error occurred while building XML from HttpRequest with custom root.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from HttpRequest with custom root.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, Boolean includeHeaders)
    {
        ArgumentNullException.ThrowIfNull(request);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Request", new System.Xml.Linq.XElement("Method", request.Method), new System.Xml.Linq.XElement("Path", request.Path));

            if (includeHeaders)
            {
                System.Xml.Linq.XElement headers = new("Headers");

                foreach (KeyValuePair<String, Microsoft.Extensions.Primitives.StringValues> header in request.Headers)
                    headers.Add(new System.Xml.Linq.XElement(header.Key, header.Value.ToString()));

                root.Add(headers);
            }

            return root;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is invalid while building XML with headers.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is null while building XML with headers.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format error occurred while processing XML data during building XML with headers.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid operation occurred while creating XElement during building XML with headers.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("Microsoft.AspNetCore.Http"))
        {
            throw new InvalidOperationException("A header key was not found during building XML with headers.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unsupported operation was attempted while building XML with headers.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpRequest"))
        {
            throw new InvalidOperationException("The HttpRequest object was disposed before building XML with headers.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The system ran out of memory while building XML with headers.", ex);
        }
        catch (System.IO.IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new InvalidOperationException("An IO error occurred while reading HttpRequest headers to build XML.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A security error occurred while accessing HttpRequest data to build XML with headers.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An XML-related invalid operation occurred during building XML with headers.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("An XML parsing error occurred while building XML with headers.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Unauthorized access error occurred while building XML with headers.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unexpected system error occurred while building XML with headers.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML with headers.", ex);
        }
    }

    public static String BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = request.BuildXmlFromHttpRequest();
            return xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is invalid while building XML string.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("The encoding parameter is invalid while building XML string.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is null while building XML string.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("The encoding parameter is null while building XML string.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("An encoder fallback occurred while processing the encoding during building XML string.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format error occurred while processing the XML during building XML string.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid operation occurred while creating or processing XElement during building XML string.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpRequest"))
        {
            throw new InvalidOperationException("The HttpRequest object was disposed while building XML string.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The system ran out of memory while building XML string.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new InvalidOperationException("An IO error occurred while accessing HttpRequest during building XML string.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("An XML parsing error occurred while building XML string.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Unauthorized access error occurred while building XML string.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unexpected system error occurred while building XML string.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML string.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, Int32 maxHeaderCount)
    {
        ArgumentNullException.ThrowIfNull(request);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            _ = Convert.ToUInt32(maxHeaderCount);
            System.Xml.Linq.XElement headers = new("Headers");

            Int32 counter = 0;
            foreach (KeyValuePair<String, Microsoft.Extensions.Primitives.StringValues> header in request.Headers)
            {
                if (counter++ >= maxHeaderCount)
                    break;

                headers.Add(new System.Xml.Linq.XElement(header.Key, header.Value.ToString()));
            }

            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("Request",
                                                new System.Xml.Linq.XElement("Method", request.Method),
                                                new System.Xml.Linq.XElement("Path", request.Path), headers));

            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is invalid while building the XML document.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("The request parameter is null while building the XML document.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("maxHeaderCount"))
        {
            throw new InvalidOperationException("The maxHeaderCount parameter is out of range. It must be non-negative.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format error occurred while constructing the XML document.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid operation occurred while creating or processing the XML document.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A required key was not found while processing the headers to build the XML document.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("HttpRequest"))
        {
            throw new InvalidOperationException("The HttpRequest object was disposed while building the XML document.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The system ran out of memory while building the XML document.", ex);
        }
        catch (System.IO.IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new InvalidOperationException("An IO error occurred while accessing HttpRequest data during building the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("An XML parsing error occurred while building the XML document.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Unauthorized access error occurred while building the XML document.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unexpected system error occurred while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building the XML document.", ex);
        }
    }

    public static MemoryStream BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, Boolean formatted, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = request.BuildXmlFromHttpRequest();
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms, encoding);
            writer.Write(xml.ToString(formatted ? System.Xml.Linq.SaveOptions.None : System.Xml.Linq.SaveOptions.DisableFormatting));
            writer.Flush();
            ms.Position = 0;
            return ms;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("The provided encoding parameter is invalid for writing the XML memory stream.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("Encoding failed during writing the XML content into the memory stream.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while writing the XML to the memory stream.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("MemoryStream"))
        {
            throw new InvalidOperationException("The memory stream was disposed before writing the XML content.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("StreamWriter"))
        {
            throw new InvalidOperationException("The stream writer was disposed before writing the XML content.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to write the XML content into the memory stream.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access occurred while attempting to write the XML to the memory stream.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Failed to generate valid XML content from the HTTP request.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML memory stream from Microsoft.AspNetCore.Http.HttpRequest.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, IDictionary<String, String> customAttributes)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(customAttributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Request", new System.Xml.Linq.XElement("Method", request.Method), new System.Xml.Linq.XElement("Path", request.Path));

            foreach (KeyValuePair<String, String> attribute in customAttributes)
                root.Add(new System.Xml.Linq.XElement(attribute.Key, attribute.Value));

            return root;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("Request argument is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("customAttributes"))
        {
            throw new InvalidOperationException("Custom attributes argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("Request cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("customAttributes"))
        {
            throw new InvalidOperationException("Custom attributes cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("request", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid operation while processing request.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("customAttributes", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid operation while processing custom attributes.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Request"))
        {
            throw new InvalidOperationException("The request object was disposed.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to build the XML.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML error occurred while building the document.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("A format error occurred while processing attributes.", ex);
        }
        catch (InvalidCastException ex)
        {
            throw new InvalidOperationException("An invalid type cast occurred during XML construction.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("An unsupported operation was attempted during XML construction.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access detected while processing the request.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHttpRequest(this Microsoft.AspNetCore.Http.HttpRequest request, String boundaryName, Boolean includeFormFields)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(boundaryName);
        ArgumentException.ThrowIfNullOrWhiteSpace(boundaryName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new System.Xml.Linq.XElement(boundaryName, 
                                                                         new System.Xml.Linq.XElement("Method", request.Method), 
                                                                         new System.Xml.Linq.XElement("Path", request.Path));

            if (includeFormFields && request.HasFormContentType)
            {
                System.Xml.Linq.XElement formElement = new System.Xml.Linq.XElement("FormFields");
                
                foreach (KeyValuePair<String, Microsoft.Extensions.Primitives.StringValues> field in request.Form)
                    formElement.Add(new System.Xml.Linq.XElement(field.Key, field.Value));
                
                root.Add(formElement);
            }

            return root;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundaryName"))
        {
            throw new InvalidOperationException("Boundary name argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("Request cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundaryName"))
        {
            throw new InvalidOperationException("Boundary name cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("A format error occurred while processing form fields.", ex);
        }
        catch (InvalidCastException ex)
        {
            throw new InvalidOperationException("An invalid type cast occurred during form field processing.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Form", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid operation while accessing form fields.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Path", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid operation while accessing request path.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Method", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid operation while accessing request method.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A specified form field key was not found.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Request"))
        {
            throw new InvalidOperationException("The request object was disposed.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to build the XML.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the form data.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("An unsupported operation was attempted during XML construction.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Unauthorized access detected while accessing the request.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML error occurred while building the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML with boundary and form fields.", ex);
        }
    }
}