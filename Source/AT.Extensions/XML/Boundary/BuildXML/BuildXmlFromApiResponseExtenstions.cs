using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromApiResponseExtenstions
{
    private static XDocument ConvertInsideBoundary(Object boundary)
    {
        try
        {
            switch (boundary)
            {
                case XDocument xd:
                    return xd;

                case XElement xe:
                    return new XDocument(xe);

                case XmlDocument xmlDoc:
                    return XDocument.Parse(xmlDoc.OuterXml);

                case XmlReader reader:
                    return XDocument.Load(reader);

                case String s:
                    return XDocument.Parse(s);

                case StringBuilder sb:
                    return XDocument.Parse(sb.ToString());

                case Byte[] bytes:
                    return XDocument.Parse(Encoding.UTF8.GetString(bytes));

                case Stream stream:
                    {
                        using StreamReader sr = new(stream, leaveOpen: true);
                        return XDocument.Parse(sr.ReadToEnd());
                    }

                case TextReader tr:
                    return XDocument.Parse(tr.ReadToEnd());

                case FileInfo fi:
                    {
                        if (!fi.Exists)
                            throw new FileNotFoundException("Specified file does not exist.", fi.FullName);
                    }
                    return XDocument.Load(fi.FullName);

                case DirectoryInfo di:
                    throw new InvalidOperationException("DirectoryInfo cannot be converted to XML. A file path is required.");

                case Uri uri:
                    {
                        if (!uri.IsAbsoluteUri)
                            throw new InvalidOperationException("URI must be absolute.");
                    }
                    return XDocument.Load(uri.AbsoluteUri);

                case HttpResponseMessage response:
                    {
                        if (!response.IsSuccessStatusCode)
                            throw new InvalidOperationException($"HTTP request failed with status code {response.StatusCode}.");
                        var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return XDocument.Parse(content);
                    }

                default:
                    throw new NotSupportedException($"Type '{boundary.GetType().FullName}' is not supported for XML conversion.");
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundary"))
        {
            throw new InvalidOperationException("The boundary argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundary"))
        {
            throw new InvalidOperationException("The boundary argument was null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("Byte decoding failed while converting UTF8 content.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file could not be found.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("HTTP transport layer failed while retrieving XML content.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Low-level I/O error occurred while reading XML data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML subsystem entered an invalid operational state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The provided boundary type is not supported for XML conversion.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("A stream or reader was already disposed during XML processing.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML source was denied by the operating system.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The input payload is not a well-formed XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred during XML synchronization boundary.", ex);
        }
    }

    private static XDocument Synchronize(Object boundary)
    {
        if (boundary is null)
            throw new ArgumentNullException(nameof(boundary), "Boundary input cannot be null.");

        return ConvertInsideBoundary(boundary);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromApiResponseExtenstions
{
    public static XDocument BuildXmlFromApiResponse(this String input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this StringBuilder input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this Stream input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this TextReader input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this XmlDocument input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this XDocument input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this Byte[] input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this MemoryStream input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this Uri input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this HttpResponseMessage input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this FileInfo input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this DirectoryInfo input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this XmlReader input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this XElement input)
    {
        return Synchronize(input);
    }

    public static XDocument BuildXmlFromApiResponse(this Object input)
    {
        return Synchronize(input);
    }
}