using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToBytesExtensions
{
    private static Byte[] ProcessCompression(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            var xmlContent = String.Empty;

            if (input is XmlDocument xmlDocument)
                xmlContent = xmlDocument.OuterXml;
            else if (input is XmlElement xmlElement)
                xmlContent = xmlElement.OuterXml;
            else if (input is XmlNode xmlNode)
                xmlContent = xmlNode.OuterXml;
            else if (input is String xmlString)
                xmlContent = xmlString;
            else if (input is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (input is XmlNodeReader xmlNodeReader)
            {
                var sb = new StringBuilder();
                while (xmlNodeReader.Read())
                {
                    sb.Append(xmlNodeReader.ReadOuterXml());
                }
                xmlContent = sb.ToString();
            }
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                using var reader = new StreamReader(memoryStream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is XmlWriter xmlWriter)
                throw new InvalidOperationException("XmlWriter cannot be directly converted to bytes.");
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("File not found.", fileInfo.FullName);
             
                xmlContent = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            }
            else if (input is DirectoryInfo directoryInfo)
                throw new InvalidOperationException("Cannot compress an entire directory as XML.");
            else if (input is XmlAttribute xmlAttribute)
                xmlContent = xmlAttribute.OuterXml;
            else if (input is XmlNamedNodeMap xmlNamedNodeMap)
            {
                var sb = new StringBuilder();
                
                for (var i = 0; i < xmlNamedNodeMap.Count; i++)
                    sb.Append(xmlNamedNodeMap.Item(i)?.OuterXml);
             
                xmlContent = sb.ToString();
            }
            else if (input is XmlCDataSection xmlCDataSection)
                xmlContent = xmlCDataSection.OuterXml;
            else if (input is XmlComment xmlComment)
                xmlContent = xmlComment.OuterXml;
            else
                throw new NotSupportedException($"Type '{input.GetType()}' is not supported for XML compression.");

            var xmlBytes = Encoding.UTF8.GetBytes(xmlContent);

            using var compressedStream = new MemoryStream();
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
            {
                gzipStream.Write(xmlBytes, 0, xmlBytes.Length);
            }
            compressedStream.Position = 0;
            return compressedStream.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input reference is null. Provide a valid XML reference type.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("Directory path could not be found during XML processing.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"File '{ex.FileName}' not found while attempting XML compression.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("XmlWriter"))
        {
            throw new InvalidOperationException("XmlWriter cannot be converted to Byte array directly.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("DirectoryInfo"))
        {
            throw new InvalidOperationException("Cannot compress a directory as XML.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("UnsupportedType"))
        {
            throw new InvalidOperationException("The provided type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException($"Attempted to access a disposed Object '{ex.ObjectName}' during XML compression.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864)) // Example specific HResult
        {
            throw new InvalidOperationException("I/O error occurred while reading XML data from stream or file.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("Access denied when attempting to read XML file or directory.", ex);
        }
        catch (XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"Malformed XML detected at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error occurred during XML compression: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToBytesExtensions
{
    public static Byte[] CompressXmlToBytes(this XmlDocument xmlDocument)
    {
        return ProcessCompression(xmlDocument);
    }

    public static Byte[] CompressXmlToBytes(this XmlElement xmlElement)
    {
        return ProcessCompression(xmlElement);
    }

    public static Byte[] CompressXmlToBytes(this XmlNode xmlNode)
    {
        return ProcessCompression(xmlNode);
    }

    public static Byte[] CompressXmlToBytes(this String xmlString)
    {
        return ProcessCompression(xmlString);
    }

    public static Byte[] CompressXmlToBytes(this TextReader textReader)
    {
        return ProcessCompression(textReader);
    }

    public static Byte[] CompressXmlToBytes(this XmlNodeReader xmlNodeReader)
    {
        return ProcessCompression(xmlNodeReader);
    }

    public static Byte[] CompressXmlToBytes(this Stream xmlStream)
    {
        return ProcessCompression(xmlStream);
    }

    public static Byte[] CompressXmlToBytes(this MemoryStream memoryStream)
    {
        return ProcessCompression(memoryStream);
    }

    public static Byte[] CompressXmlToBytes(this XmlWriter xmlWriter)
    {
        return ProcessCompression(xmlWriter);
    }

    public static Byte[] CompressXmlToBytes(this FileInfo xmlFile)
    {
        return ProcessCompression(xmlFile);
    }

    public static Byte[] CompressXmlToBytes(this DirectoryInfo directoryInfo)
    {
        return ProcessCompression(directoryInfo);
    }

    public static Byte[] CompressXmlToBytes(this XmlAttribute xmlAttribute)
    {
        return ProcessCompression(xmlAttribute);
    }

    public static Byte[] CompressXmlToBytes(this XmlNamedNodeMap xmlNamedNodeMap)
    {
        return ProcessCompression(xmlNamedNodeMap);
    }

    public static Byte[] CompressXmlToBytes(this XmlCDataSection xmlCDataSection)
    {
        return ProcessCompression(xmlCDataSection);
    }

    public static Byte[] CompressXmlToBytes(this XmlComment xmlComment)
    {
        return ProcessCompression(xmlComment);
    }
}