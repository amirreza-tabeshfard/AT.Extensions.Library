using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromHtmlExtensions
{
    private static XDocument Synchronize(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            String html;

            if (source is String s)
                html = s;
            else if (source is StringBuilder sb)
                html = sb.ToString();
            else if (source is Byte[] bytes)
                html = Encoding.UTF8.GetString(bytes);
            else if (source is Char[] chars)
                html = new String(chars);
            else if (source is List<Char> charList)
                html = new String(charList.ToArray());
            else if (source is ReadOnlyCollection<Char> roChars)
                html = new String(new List<Char>(roChars).ToArray());
            else if (source is TextReader reader)
                html = reader.ReadToEnd();
            else if (source is Stream stream)
            {
                using var sr = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
                html = sr.ReadToEnd();
            }
            else if (source is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("HTML file was not found.", fi.FullName);

                html = File.ReadAllText(fi.FullName, Encoding.UTF8);
            }
            else if (source is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new InvalidOperationException("Uri must be absolute.");

                using var client = new HttpClient();
                html = client.GetStringAsync(uri).GetAwaiter().GetResult();
            }
            else if (source is HttpResponseMessage response)
            {
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"HTTP request failed with status code {response.StatusCode}.");

                html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            else if (source is HttpContent content)
                html = content.ReadAsStringAsync().GetAwaiter().GetResult();
            else if (source is XmlReader xmlReader)
                return XDocument.Load(xmlReader);
            else
                throw new NotSupportedException($"Input type '{source.GetType().FullName}' is not supported for HTML conversion.");

            if (String.IsNullOrWhiteSpace(html))
                throw new InvalidOperationException("HTML content is empty or whitespace.");

            var wrapped = $"<root>{html}</root>";

            try
            {
                return XDocument.Parse(wrapped, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
            }
            catch (Exception ex)
            {
                throw new FormatException("HTML content could not be converted into valid XML. Ensure the HTML is well-formed.", ex);
            }
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The provided input source was null. A valid HTML source is required.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The specified HTML file could not be located on disk.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("HTML parsing failed because the content is not well-formed XML.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("An HTTP error occurred while retrieving HTML content from a remote endpoint.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("The HTTP operation was in an invalid state during HTML retrieval.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O failure occurred while reading the HTML source.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The provided input type is not supported for HTML to XML synchronization.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("The underlying stream was already disposed before HTML could be read.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the HTML file or resource was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML reader failed while loading content derived from HTML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("BuildXmlFromHtml failed during synchronization boundary.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromHtmlExtensions
{
    public static XDocument BuildXmlFromHtml(this String html)
    {
        return Synchronize(html);
    }

    public static XDocument BuildXmlFromHtml(this Stream htmlStream)
    {
        return Synchronize(htmlStream);
    }

    public static XDocument BuildXmlFromHtml(this FileInfo htmlFile)
    {
        return Synchronize(htmlFile);
    }

    public static XDocument BuildXmlFromHtml(this Uri htmlUri)
    {
        return Synchronize(htmlUri);
    }

    public static XDocument BuildXmlFromHtml(this StringBuilder htmlBuilder)
    {
        return Synchronize(htmlBuilder);
    }

    public static XDocument BuildXmlFromHtml(this Byte[] htmlBytes)
    {
        return Synchronize(htmlBytes);
    }

    public static XDocument BuildXmlFromHtml(this Char[] htmlChars)
    {
        return Synchronize(htmlChars);
    }

    public static XDocument BuildXmlFromHtml(this TextReader htmlReader)
    {
        return Synchronize(htmlReader);
    }

    public static XDocument BuildXmlFromHtml(this HttpResponseMessage response)
    {
        return Synchronize(response);
    }

    public static XDocument BuildXmlFromHtml(this HttpContent content)
    {
        return Synchronize(content);
    }

    public static XDocument BuildXmlFromHtml(this MemoryStream memoryStream)
    {
        return Synchronize(memoryStream);
    }

    public static XDocument BuildXmlFromHtml(this FileStream fileStream)
    {
        return Synchronize(fileStream);
    }

    public static XDocument BuildXmlFromHtml(this XmlReader xmlReader)
    {
        return Synchronize(xmlReader);
    }

    public static XDocument BuildXmlFromHtml(this List<Char> htmlCharList)
    {
        return Synchronize(htmlCharList);
    }

    public static XDocument BuildXmlFromHtml(this ReadOnlyCollection<Char> htmlReadOnlyChars)
    {
        return Synchronize(htmlReadOnlyChars);
    }
}