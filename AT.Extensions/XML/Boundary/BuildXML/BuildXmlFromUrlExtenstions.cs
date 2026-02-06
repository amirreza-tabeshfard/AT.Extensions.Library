using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromUrlExtenstions
{
    private static XmlDocument BuildXmlFromUrlInternal(Object input)
    {
        try
        {
            var xmlDoc = new XmlDocument();

            if (input is String urlString)
            {
                if (String.IsNullOrWhiteSpace(urlString))
                    throw new ArgumentException("URL String cannot be null or empty.");

                using var httpClient = new HttpClient();
                var xmlContent = httpClient.GetStringAsync(urlString).GetAwaiter().GetResult();
                xmlDoc.LoadXml(xmlContent);
                return xmlDoc;
            }
            else if (input is HttpClient client)
                throw new ArgumentException("Cannot determine URL from HttpClient. Provide a String or Uri.");
            else if (input is WebClient webClient)
                throw new ArgumentException("Cannot determine URL from WebClient. Provide a String or Uri.");
            else if (input is Uri uri)
            {
                using var httpClient = new HttpClient();
                var xmlContent = httpClient.GetStringAsync(uri).GetAwaiter().GetResult();
                xmlDoc.LoadXml(xmlContent);
                return xmlDoc;
            }
            else if (input is XmlDocument existingXmlDoc)
                return existingXmlDoc;
            else if (input is XDocument xDoc)
            {
                using var reader = xDoc.CreateReader();
                xmlDoc.Load(reader);
                return xmlDoc;
            }
            else if (input is XmlElement xmlElement)
            {
                xmlDoc.AppendChild(xmlDoc.ImportNode(xmlElement, true));
                return xmlDoc;
            }
            else if (input is Stream stream)
            {
                xmlDoc.Load(stream);
                return xmlDoc;
            }
            else if (input is TextReader textReader)
            {
                xmlDoc.Load(textReader);
                return xmlDoc;
            }
            else if (input is HttpRequestMessage request)
                throw new ArgumentException("Cannot build XML directly from HttpRequestMessage. Provide a valid URL.");
            else if (input is HttpResponseMessage response)
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                
                var xmlContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                xmlDoc.LoadXml(xmlContent);
                return xmlDoc;
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
                
                xmlDoc.Load(fileInfo.FullName);
                return xmlDoc;
            }
            else if (input is UriBuilder uriBuilder)
            {
                using var httpClient = new HttpClient();
                var xmlContent = httpClient.GetStringAsync(uriBuilder.Uri).GetAwaiter().GetResult();
                xmlDoc.LoadXml(xmlContent);
                return xmlDoc;
            }
            else if (input is HttpContent content)
            {
                var xmlContent = content.ReadAsStringAsync().GetAwaiter().GetResult();
                xmlDoc.LoadXml(xmlContent);
                return xmlDoc;
            }
            else if (input is XmlNode xmlNode)
            {
                xmlDoc.AppendChild(xmlDoc.ImportNode(xmlNode, true));
                return xmlDoc;
            }
            else
                throw new ArgumentException($"Unsupported type {input.GetType().FullName} for XML building.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("urlString"))
        {
            throw new InvalidOperationException($"URL argument cannot be null or empty. Parameter: {ex.ParamName}", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("request"))
        {
            throw new InvalidOperationException("Cannot build XML directly from HttpRequestMessage. Provide a valid URL.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("client"))
        {
            throw new InvalidOperationException("Cannot determine URL from HttpClient. Provide a String or Uri.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("webClient"))
        {
            throw new InvalidOperationException("Cannot determine URL from WebClient. Provide a String or Uri.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException($"Unsupported argument provided: {ex.ParamName}", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals("fileInfo"))
        {
            throw new InvalidOperationException($"File not found: {ex.FileName}", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"HTTP request failed. Source: {ex.Source}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"XML processing failed. Source: {ex.Source}", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A required Object reference was null during XML building.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The provided input type is not supported for XML building.", ex);
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException("Failed to parse XML content from the provided input.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("I/O error occurred while loading XML from stream or file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XML from input due to an unexpected error. Source: {ex.Source}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromUrlExtenstions
{
    public static XmlDocument BuildXmlFromUrl(this String url)
    {
        return BuildXmlFromUrlInternal(url);
    }

    public static XmlDocument BuildXmlFromUrl(this Uri uri)
    {
        return BuildXmlFromUrlInternal(uri.ToString());
    }

    public static XmlDocument BuildXmlFromUrl(this HttpClient client)
    {
        return BuildXmlFromUrlInternal(client);
    }

    public static XmlDocument BuildXmlFromUrl(this WebClient webClient)
    {
        return BuildXmlFromUrlInternal(webClient);
    }

    public static XmlDocument BuildXmlFromUrl(this XmlDocument xmlDocument)
    {
        return BuildXmlFromUrlInternal(xmlDocument);
    }

    public static XmlDocument BuildXmlFromUrl(this XDocument xDocument)
    {
        return BuildXmlFromUrlInternal(xDocument);
    }

    public static XmlDocument BuildXmlFromUrl(this XmlElement xmlElement)
    {
        return BuildXmlFromUrlInternal(xmlElement);
    }

    public static XmlDocument BuildXmlFromUrl(this Stream stream)
    {
        return BuildXmlFromUrlInternal(stream);
    }

    public static XmlDocument BuildXmlFromUrl(this TextReader textReader)
    {
        return BuildXmlFromUrlInternal(textReader);
    }

    public static XmlDocument BuildXmlFromUrl(this HttpRequestMessage request)
    {
        return BuildXmlFromUrlInternal(request);
    }

    public static XmlDocument BuildXmlFromUrl(this HttpResponseMessage response)
    {
        return BuildXmlFromUrlInternal(response);
    }

    public static XmlDocument BuildXmlFromUrl(this FileInfo fileInfo)
    {
        return BuildXmlFromUrlInternal(fileInfo);
    }

    public static XmlDocument BuildXmlFromUrl(this UriBuilder uriBuilder)
    {
        return BuildXmlFromUrlInternal(uriBuilder.ToString());
    }

    public static XmlDocument BuildXmlFromUrl(this HttpContent httpContent)
    {
        return BuildXmlFromUrlInternal(httpContent);
    }

    public static XmlDocument BuildXmlFromUrl(this XmlNode xmlNode)
    {
        return BuildXmlFromUrlInternal(xmlNode);
    }
}