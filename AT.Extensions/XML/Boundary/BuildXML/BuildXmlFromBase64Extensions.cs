using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromBase64Extensions
{
    private static XmlDocument InternalBuild(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            String base64;

            switch (source)
            {
                case String s:
                    {
                        base64 = s;
                    }
                    break;

                case Byte[] bytes:
                    {
                        base64 = Convert.ToBase64String(bytes);
                    }
                    break;

                case MemoryStream ms:
                    {
                        base64 = Convert.ToBase64String(ms.ToArray());
                    }
                    break;

                case FileStream fs:
                    {
                        using var m = new MemoryStream();
                        fs.CopyTo(m);
                        base64 = Convert.ToBase64String(m.ToArray());
                    }
                    break;

                case Stream stream:
                    {
                        using var m = new MemoryStream();
                        stream.CopyTo(m);
                        base64 = Convert.ToBase64String(m.ToArray());
                    }
                    break;

                case FileInfo fi:
                    {
                        base64 = Convert.ToBase64String(File.ReadAllBytes(fi.FullName));
                    }
                    break;

                case Uri uri:
                    {
                        base64 = File.Exists(uri.LocalPath)
                                 ? Convert.ToBase64String(File.ReadAllBytes(uri.LocalPath))
                                 : throw new FileNotFoundException("The file specified by the Uri was not found.", uri.LocalPath);
                    }
                    break;

                case StringBuilder sb:
                    {
                        base64 = sb.ToString();
                    }
                    break;

                case TextReader tr:
                    {
                        base64 = tr.ReadToEnd();
                    }
                    break;

                case XmlReader xr:
                    {
                        base64 = xr.ReadOuterXml();
                    }
                    break;

                case XmlDocument xd:
                    {
                        base64 = xd.OuterXml;
                    }
                    break;

                case XDocument xdoc:
                    {
                        base64 = xdoc.ToString(SaveOptions.DisableFormatting);
                    }
                    break;

                case XElement xe:
                    {
                        base64 = xe.ToString(SaveOptions.DisableFormatting);
                    }
                    break;

                case HttpContent hc:
                    {
                        base64 = hc.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                    break;

                default:
                    throw new NotSupportedException($"The provided input type '{source.GetType().FullName}' is not supported.");
            }

            if (String.IsNullOrWhiteSpace(base64))
                throw new InvalidOperationException("Resolved Base64 content is empty.");

            Byte[] xmlBytes;

            try
            {
                xmlBytes = Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                throw new FormatException("Input content is not a valid Base64 String.");
            }

            var xmlString = Encoding.UTF8.GetString(xmlBytes);

            if (String.IsNullOrWhiteSpace(xmlString))
                throw new InvalidOperationException("Decoded XML content is empty.");

            var doc = new XmlDocument
            {
                PreserveWhitespace = false
            };

            doc.LoadXml(xmlString);
            return doc;
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("UTF8 decoding failed while converting Base64 content to XML.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File", StringComparison.Ordinal))
        {
            throw new FileNotFoundException("Referenced file could not be located on disk.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Convert", StringComparison.Ordinal))
        {
            throw new FormatException("Provided input is not valid Base64 encoded content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("HTTP content could not be read synchronously.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("XML parsing failed due to malformed XML structure.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new IOException("Stream or file IO operation failed while resolving Base64 input.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("Boundary.XmlBoundaryExtensions", StringComparison.Ordinal))
        {
            throw new NotSupportedException("The provided reference type is not supported by BuildXmlFromBase64.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new ObjectDisposedException(ex.ObjectName, "The provided stream or reader was already disposed.");
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File", StringComparison.Ordinal))
        {
            throw new UnauthorizedAccessException("Access to the specified file path was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new XmlException("Decoded content is not valid XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("BuildXmlFromBase64 failed due to an unexpected runtime error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromBase64Extensions
{
    public static XmlDocument BuildXmlFromBase64(this String input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this Byte[] input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this Stream input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this MemoryStream input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this FileStream input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this FileInfo input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this Uri input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this StringBuilder input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this TextReader input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this XmlReader input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this XmlDocument input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this XDocument input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this XElement input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this StreamReader input)
    {
        return InternalBuild(input);
    }

    public static XmlDocument BuildXmlFromBase64(this HttpContent input)
    {
        return InternalBuild(input);
    }
}