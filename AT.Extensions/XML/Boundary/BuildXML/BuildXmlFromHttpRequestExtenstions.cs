using Microsoft.AspNetCore.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromHttpRequestExtenstions
{
    private static XDocument Synchronize(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            if (source is HttpRequest httpRequest)
            {
                var root = new XElement("HttpRequest");

                root.Add(new XElement("Method", httpRequest.Method ?? String.Empty));
                root.Add(new XElement("Scheme", httpRequest.Scheme ?? String.Empty));
                root.Add(new XElement("Path", httpRequest.Path.ToString()));
                root.Add(new XElement("QueryString", httpRequest.QueryString.ToString()));

                var headersElement = new XElement("Headers");

                foreach (var h in httpRequest.Headers)
                    headersElement.Add(new XElement(h.Key, h.Value.ToString()));

                root.Add(headersElement);

                var contentType = httpRequest.ContentType ?? String.Empty;
                if (contentType.Contains("multipart/form-data", StringComparison.OrdinalIgnoreCase))
                {
                    var boundaryIndex = contentType.IndexOf("boundary=", StringComparison.OrdinalIgnoreCase);
                    if (boundaryIndex >= 0)
                        root.Add(new XElement("Boundary", contentType[(boundaryIndex + "boundary=".Length)..]));
                }

                if (httpRequest.Body != null && httpRequest.Body.CanRead)
                {
                    using var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, leaveOpen: true);
                    root.Add(new XElement("Body", reader.ReadToEnd()));
                    httpRequest.Body.Position = 0;
                }

                return new XDocument(root);
            }

            if (source is HttpContext context)
                return Synchronize(context.Request);

            if (source is HttpRequestMessage message)
            {
                var root = new XElement("HttpRequestMessage");

                root.Add(new XElement("Method", message.Method?.Method ?? String.Empty));
                root.Add(new XElement("Uri", message.RequestUri?.ToString() ?? String.Empty));

                var headers = new XElement("Headers");
                foreach (var h in message.Headers)
                    headers.Add(new XElement(h.Key, String.Join(",", h.Value)));
                root.Add(headers);

                if (message.Content != null)
                    root.Add(new XElement("Body", message.Content.ReadAsStringAsync().GetAwaiter().GetResult()));

                return new XDocument(root);
            }

            if (source is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
                var text = reader.ReadToEnd();
                return new XDocument(new XElement("Stream", text));
            }

            if (source is Byte[] bytes)
                return new XDocument(new XElement("Bytes", Encoding.UTF8.GetString(bytes)));

            if (source is String str)
                return new XDocument(new XElement("String", str));

            if (source is StringBuilder sb)
                return new XDocument(new XElement("StringBuilder", sb.ToString()));

            if (source is IFormCollection form)
            {
                var root = new XElement("Form");
                foreach (var f in form)
                    root.Add(new XElement(f.Key, f.Value.ToString()));
                return new XDocument(root);
            }

            if (source is IQueryCollection query)
            {
                var root = new XElement("Query");
                foreach (var q in query)
                    root.Add(new XElement(q.Key, q.Value.ToString()));
                return new XDocument(root);
            }

            if (source is IHeaderDictionary headerDictionary)
            {
                var root = new XElement("Headers");
                foreach (var h in headerDictionary)
                    root.Add(new XElement(h.Key, h.Value.ToString()));
                return new XDocument(root);
            }

            if (source is IDictionary<String, String> dict)
            {
                var root = new XElement("Dictionary");
                foreach (var kv in dict)
                    root.Add(new XElement(kv.Key, kv.Value));
                return new XDocument(root);
            }

            if (source is IEnumerable<KeyValuePair<String, String>> pairs)
            {
                var root = new XElement("Pairs");
                foreach (var kv in pairs)
                    root.Add(new XElement(kv.Key, kv.Value));
                return new XDocument(root);
            }

            if (source is Uri uri)
                return new XDocument(new XElement("Uri", uri.ToString()));

            if (source is XmlDocument xmlDoc)
                return XDocument.Parse(xmlDoc.OuterXml);

            throw new NotSupportedException($"The provided source type '{source.GetType().FullName}' is not supported.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("Failed to build XML: the provided input is null. A valid reference type must be provided.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(XDocument).FullName))
        {
            throw new InvalidOperationException("Failed to build XML: the source type is not supported for HTTP request XML conversion.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Stream"))
        {
            throw new InvalidOperationException("Failed to read the stream content from the provided source. Ensure the stream is readable and positioned correctly.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Failed to build XML: the underlying stream has been disposed and cannot be read.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse XML content from the provided source. Ensure the XML is well-formed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http.HttpRequestMessage"))
        {
            throw new InvalidOperationException("Failed to read content from HttpRequestMessage. The content might be null or improperly formatted.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Failed to decode bytes or stream content into text. Check that the encoding is UTF-8 or compatible.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from the provided HTTP-related source. See inner exception for exact technical reason.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromHttpRequestExtenstions
{
    public static XDocument BuildXmlFromHttpRequest(this HttpRequest request)
    {
        return Synchronize(request);
    }

    public static XDocument BuildXmlFromHttpRequest(this HttpContext context)
    {
        return Synchronize(context);
    }

    public static XDocument BuildXmlFromHttpRequest(this HttpRequestMessage requestMessage)
    {
        return Synchronize(requestMessage);
    }

    public static XDocument BuildXmlFromHttpRequest(this Stream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromHttpRequest(this MemoryStream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromHttpRequest(this Byte[] buffer)
    {
        return Synchronize(buffer);
    }

    public static XDocument BuildXmlFromHttpRequest(this String text)
    {
        return Synchronize(text);
    }

    public static XDocument BuildXmlFromHttpRequest(this StringBuilder builder)
    {
        return Synchronize(builder);
    }

    public static XDocument BuildXmlFromHttpRequest(this IFormCollection form)
    {
        return Synchronize(form);
    }

    public static XDocument BuildXmlFromHttpRequest(this IQueryCollection query)
    {
        return Synchronize(query);
    }

    public static XDocument BuildXmlFromHttpRequest(this IHeaderDictionary headers)
    {
        return Synchronize(headers);
    }

    public static XDocument BuildXmlFromHttpRequest(this IDictionary<String, String> dictionary)
    {
        return Synchronize(dictionary);
    }

    public static XDocument BuildXmlFromHttpRequest(this IEnumerable<KeyValuePair<String, String>> pairs)
    {
        return Synchronize(pairs);
    }

    public static XDocument BuildXmlFromHttpRequest(this Uri uri)
    {
        return Synchronize(uri);
    }

    public static XDocument BuildXmlFromHttpRequest(this XmlDocument xmlDocument)
    {
        return Synchronize(xmlDocument);
    }
}