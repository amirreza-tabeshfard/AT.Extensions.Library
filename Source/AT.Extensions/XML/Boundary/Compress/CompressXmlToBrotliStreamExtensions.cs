using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToBrotliStreamExtensions
{
    private static Stream ConvertAndCompress(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            var xmlText = String.Empty;

            if (input is String s)
                xmlText = s;
            else if (input is XDocument xd)
                xmlText = xd.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xe)
                xmlText = xe.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlDocument xdoc)
                xmlText = xdoc.OuterXml;
            else if (input is XmlElement xel)
                xmlText = xel.OuterXml;
            else if (input is TextReader tr)
                xmlText = tr.ReadToEnd();
            else if (input is StringBuilder sb)
                xmlText = sb.ToString();
            else if (input is Byte[] bytes)
                xmlText = Encoding.UTF8.GetString(bytes);
            else if (input is Stream stream)
            {
                if (stream.CanSeek)
                    stream.Position = 0;

                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlText = reader.ReadToEnd();
            }
            else if (input is FileInfo fi)
                xmlText = File.ReadAllText(fi.FullName, Encoding.UTF8);
            else if (input is Uri uri)
                xmlText = uri.IsFile ? File.ReadAllText(uri.LocalPath, Encoding.UTF8) : uri.ToString();
            else if (input is XmlReader xr)
            {
                var doc = XDocument.Load(xr);
                xmlText = doc.ToString(SaveOptions.DisableFormatting);
            }
            else if (input is Exception ex)
                xmlText = ex.ToString();
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlText))
                throw new InvalidOperationException("The XML content is empty or whitespace.");

            var outputStream = new MemoryStream();
            var buffer = Encoding.UTF8.GetBytes(xmlText);

            using (var brotli = new BrotliStream(outputStream, CompressionLevel.Optimal, true))
            {
                brotli.Write(buffer, 0, buffer.Length);
            }

            outputStream.Position = 0;
            return outputStream;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument was null and cannot be processed for XML compression.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("buffer"))
        {
            throw new InvalidOperationException("The byte buffer size is outside the valid range during compression.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Failed to decode byte sequence into UTF-8 XML content.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The directory for the XML file source was not found.", ex);
        }
        catch (EndOfStreamException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.StreamReader"))
        {
            throw new InvalidOperationException("Unexpected end of stream encountered while reading XML content.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The XML file specified as input could not be found.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content format is invalid and cannot be parsed.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression.BrotliStream"))
        {
            throw new InvalidOperationException("The data provided to the Brotli compressor is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred during XML materialization.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while processing XML input or output streams.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Object"))
        {
            throw new InvalidOperationException("The provided reference type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("The input stream was disposed before XML compression completed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Insufficient memory available during XML compression.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The file path for the XML source exceeds the supported length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("Access to the XML file source was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A low-level XML parsing error occurred.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML to Brotli stream compression failed due to an unexpected runtime error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToBrotliStreamExtensions
{
    public static Stream CompressXmlToBrotliStream(this String input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this XDocument input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this XElement input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this XmlDocument input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this XmlElement input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this Stream input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this MemoryStream input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this Byte[] input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this TextReader input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this StringBuilder input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this FileInfo input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this Uri input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this XmlReader input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this Object input)
    {
        return ConvertAndCompress(input);
    }

    public static Stream CompressXmlToBrotliStream(this Exception input)
    {
        return ConvertAndCompress(input);
    }
}