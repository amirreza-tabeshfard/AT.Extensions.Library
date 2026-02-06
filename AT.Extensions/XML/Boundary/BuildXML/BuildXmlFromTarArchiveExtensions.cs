using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromTarArchiveExtensions
{
    private static XmlDocument ProcessTarArchive(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input for BuildXmlFromTarArchive cannot be null.");

            var xmlDoc = new XmlDocument();

            if (input is String filePath)
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Tar file not found at path: {filePath}");
                
                using var fileStream = File.OpenRead(filePath);
                xmlDoc.LoadXml(ExtractAndConvertToXml(fileStream));
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException($"Tar file not found at FileInfo: {fileInfo.FullName}");
                
                using var fileStream = fileInfo.OpenRead();
                xmlDoc.LoadXml(ExtractAndConvertToXml(fileStream));
            }
            else if (input is Stream stream)
            {
                xmlDoc.LoadXml(ExtractAndConvertToXml(stream));
            }
            else if (input is Byte[] bytes)
            {
                using var memStream = new MemoryStream(bytes);
                xmlDoc.LoadXml(ExtractAndConvertToXml(memStream));
            }
            else if (input is MemoryStream memoryStream)
            {
                xmlDoc.LoadXml(ExtractAndConvertToXml(memoryStream));
            }
            else if (input is List<String> filePaths)
            {
                var combinedXml = String.Empty;
                foreach (var path in filePaths)
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException($"Tar file not found at path: {path}");
                    
                    using var fs = File.OpenRead(path);
                    combinedXml += ExtractAndConvertToXml(fs);
                }
                xmlDoc.LoadXml($"<Root>{combinedXml}</Root>");
            }
            else if (input is XmlDocument existingXml)
            {
                xmlDoc = existingXml;
            }
            else if (input is XDocument xDoc)
            {
                using var reader = xDoc.CreateReader();
                xmlDoc.Load(reader);
            }
            else if (input is Uri uri)
            {
                if (uri.Scheme == "file")
                {
                    var localPath = uri.LocalPath;
                    
                    if (!File.Exists(localPath))
                        throw new FileNotFoundException($"Tar file not found at URI path: {localPath}");
                    
                    using var fs = File.OpenRead(localPath);
                    xmlDoc.LoadXml(ExtractAndConvertToXml(fs));
                }
                else
                    throw new NotSupportedException($"Only file:// URIs are supported. Provided URI: {uri}");
            }
            else if (input is DirectoryInfo directory)
            {
                if (!directory.Exists)
                    throw new DirectoryNotFoundException($"Directory not found: {directory.FullName}");
                
                var combinedXml = String.Empty;
                
                foreach (var file in directory.GetFiles("*.tar"))
                {
                    using var fs = file.OpenRead();
                    combinedXml += ExtractAndConvertToXml(fs);
                }

                xmlDoc.LoadXml($"<Root>{combinedXml}</Root>");
            }
            else if (input is TextReader textReader)
            {
                using var memStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textReader.ReadToEnd()));
                xmlDoc.LoadXml(ExtractAndConvertToXml(memStream));
            }
            else if (input is XmlReader xmlReader)
                xmlDoc.Load(xmlReader);
            else if (input is IEnumerable<FileInfo> files)
            {
                var combinedXml = String.Empty;

                foreach (var file in files)
                {
                    if (!file.Exists)
                        throw new FileNotFoundException($"Tar file not found at FileInfo: {file.FullName}");
                    
                    using var fs = file.OpenRead();
                    combinedXml += ExtractAndConvertToXml(fs);
                }

                xmlDoc.LoadXml($"<Root>{combinedXml}</Root>");
            }
            else if (input is IEnumerable<Stream> streams)
            {
                var combinedXml = String.Empty;
                
                foreach (var s in streams)
                    combinedXml += ExtractAndConvertToXml(s);
                
                xmlDoc.LoadXml($"<Root>{combinedXml}</Root>");
            }
            else
                throw new NotSupportedException($"Unsupported input type for BuildXmlFromTarArchive: {input.GetType().FullName}");

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("BuildXmlFromTarArchive failed: The input parameter is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message is not null && ex.Message.Contains("Directory not found"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: {ex.Message}", ex);
        }
        catch (FileNotFoundException ex) when (ex.Message is not null && ex.Message.Contains("Tar file not found at path"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: {ex.Message}", ex);
        }
        catch (FileNotFoundException ex) when (ex.Message is not null && ex.Message.Contains("Tar file not found at FileInfo"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message is not null && ex.Message.Contains("Error extracting and converting TAR to XML"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed during TAR extraction: {ex.Message}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("Only file:// URIs are supported"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: Unsupported URI scheme. {ex.Message}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("Unsupported input type"))
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: Input type not supported. {ex.Message}", ex);
        }
        catch (XmlException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: Invalid XML content. {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"BuildXmlFromTarArchive failed: Unexpected error occurred. {ex.Message}", ex);
        }
    }

    private static String ExtractAndConvertToXml(Stream tarStream)
    {
        try
        {
            if (tarStream is null)
                throw new ArgumentNullException(nameof(tarStream), "Tar stream cannot be null.");

            using var reader = new StreamReader(tarStream);
            var content = reader.ReadToEnd();

            var sanitizedContent = System.Security.SecurityElement.Escape(content);
            return $"<TarContent>{sanitizedContent}</TarContent>";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error extracting and converting TAR to XML. Details: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromTarArchiveExtensions
{
    public static XmlDocument BuildXmlFromTarArchive(this String tarFilePath)
    {
        return ProcessTarArchive(tarFilePath);
    }

    public static XmlDocument BuildXmlFromTarArchive(this FileInfo tarFile)
    {
        return ProcessTarArchive(tarFile);
    }

    public static XmlDocument BuildXmlFromTarArchive(this Stream tarStream)
    {
        return ProcessTarArchive(tarStream);
    }

    public static XmlDocument BuildXmlFromTarArchive(this Byte[] tarBytes)
    {
        return ProcessTarArchive(tarBytes);
    }

    public static XmlDocument BuildXmlFromTarArchive(this MemoryStream tarMemoryStream)
    {
        return ProcessTarArchive(tarMemoryStream);
    }

    public static XmlDocument BuildXmlFromTarArchive(this List<String> tarFilePaths)
    {
        return ProcessTarArchive(tarFilePaths);
    }

    public static XmlDocument BuildXmlFromTarArchive(this XmlDocument xmlTemplate)
    {
        return ProcessTarArchive(xmlTemplate);
    }

    public static XmlDocument BuildXmlFromTarArchive(this XDocument xDoc)
    {
        return ProcessTarArchive(xDoc);
    }

    public static XmlDocument BuildXmlFromTarArchive(this Object tarObject)
    {
        return ProcessTarArchive(tarObject);
    }

    public static XmlDocument BuildXmlFromTarArchive(this Uri tarUri)
    {
        return ProcessTarArchive(tarUri);
    }

    public static XmlDocument BuildXmlFromTarArchive(this DirectoryInfo tarDirectory)
    {
        return ProcessTarArchive(tarDirectory);
    }

    public static XmlDocument BuildXmlFromTarArchive(this TextReader tarReader)
    {
        return ProcessTarArchive(tarReader);
    }

    public static XmlDocument BuildXmlFromTarArchive(this XmlReader xmlReader)
    {
        return ProcessTarArchive(xmlReader);
    }

    public static XmlDocument BuildXmlFromTarArchive(this IEnumerable<FileInfo> tarFiles)
    {
        return ProcessTarArchive(tarFiles);
    }

    public static XmlDocument BuildXmlFromTarArchive(this IEnumerable<Stream> tarStreams)
    {
        return ProcessTarArchive(tarStreams);
    }

}