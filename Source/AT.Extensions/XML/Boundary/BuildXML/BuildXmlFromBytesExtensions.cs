using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromBytesExtensions
{
    private static XDocument BuildInternal(Func<Byte[]?> byteProvider)
    {
        try
        {
            if (byteProvider == null)
                throw new ArgumentNullException(nameof(byteProvider), "Byte provider delegate is null.");

            var bytes = byteProvider();

            if (bytes == null || bytes.Length == 0)
                throw new InvalidOperationException("Input data resolved to null or empty Byte array.");

            using var ms = new MemoryStream(bytes);
            return XDocument.Load(ms);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("byteProvider"))
        {
            throw new InvalidOperationException("An invalid argument was supplied to the Byte provider delegate.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("byteProvider"))
        {
            throw new InvalidOperationException("The Byte provider delegate argument is null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Byte decoding failed due to invalid character sequence.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML loading failed due to an invalid operation in XML subsystem.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the XML Byte stream.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The underlying stream does not support the requested operation.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The memory stream was accessed after being disposed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the XML source was denied by the operating system.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The provided Byte payload does not represent a well-formed XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred during XML synchronization.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromBytesExtensions
{
    public static XDocument BuildXmlFromBytes(this Byte[] data)
    {
        return BuildInternal(() => data);
    }

    public static XDocument BuildXmlFromBytes(this MemoryStream stream)
    {
        return BuildInternal(() => stream?.ToArray());
    }

    public static XDocument BuildXmlFromBytes(this Stream stream)
    {
        return BuildInternal(() =>
            {
                if (stream == null) 
                    return null;

                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            });
    }

    public static XDocument BuildXmlFromBytes(this String input)
    {
        return BuildInternal(() =>
            {
                if (input == null) 
                    return null;

                return File.Exists(input)
                    ? File.ReadAllBytes(input)
                    : Encoding.UTF8.GetBytes(input);
            });
    }

    public static XDocument BuildXmlFromBytes(this FileInfo file)
    {
        return BuildInternal(() => file == null ? null : File.ReadAllBytes(file.FullName));
    }

    public static XDocument BuildXmlFromBytes(this Uri uri)
    {
        return BuildInternal(() =>
            {
                if (uri == null) 
                    return null;

                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file URIs are supported.");

                return File.ReadAllBytes(uri.LocalPath);
            });
    }

    public static XDocument BuildXmlFromBytes(this XmlDocument document)
    {
        return BuildInternal(() =>
            {
                if (document == null) 
                    return null;

                return Encoding.UTF8.GetBytes(document.OuterXml);
            });
    }

    public static XDocument BuildXmlFromBytes(this XDocument document)
    {
        return BuildInternal(() =>
            {
                if (document == null)
                    return null;

                return Encoding.UTF8.GetBytes(document.ToString(SaveOptions.DisableFormatting));
            });
    }

    public static XDocument BuildXmlFromBytes(this XElement element)
    {
        return BuildInternal(() =>
            {
                if (element == null) 
                    return null;

                return Encoding.UTF8.GetBytes(element.ToString(SaveOptions.DisableFormatting));
            });
    }

    public static XDocument BuildXmlFromBytes(this StringBuilder builder)
    {
        return BuildInternal(() =>
            {
                if (builder == null) 
                    return null;

                return Encoding.UTF8.GetBytes(builder.ToString());
            });
    }

    public static XDocument BuildXmlFromBytes(this TextReader reader)
    {
        return BuildInternal(() =>
            {
                if (reader == null) 
                    return null;

                return Encoding.UTF8.GetBytes(reader.ReadToEnd());
            });
    }

    public static XDocument BuildXmlFromBytes(this BinaryReader reader)
    {
        return BuildInternal(() =>
            {
                if (reader == null) 
                    return null;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            });
    }

    public static XDocument BuildXmlFromBytes(this XmlReader reader)
    {
        return BuildInternal(() =>
            {
                if (reader == null)
                    return null;

                var doc = XDocument.Load(reader);
                return Encoding.UTF8.GetBytes(doc.ToString(SaveOptions.DisableFormatting));
            });
    }

    public static XDocument BuildXmlFromBytes(this HttpResponseMessage response)
    {
        return BuildInternal(() =>
            {
                if (response == null) 
                    return null;
                
                return response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            });
    }

    public static XDocument BuildXmlFromBytes(this IEnumerable<Byte[]> chunks)
    {
        return BuildInternal(() =>
            {
                if (chunks == null)
                    return null;

                using var ms = new MemoryStream();
                
                foreach (var part in from part in chunks
                                     where part != null
                                     select part)
                {
                    ms.Write(part, 0, part.Length);
                }

                return ms.ToArray();
            });
    }
}