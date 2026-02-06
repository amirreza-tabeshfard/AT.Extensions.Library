using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Class
/// </summary>
public static partial class BuildXmlFromMobiFileExtenstions
{
    public class MobiFileData(String bookTitle,
                              String authorName,
                              String publisherName,
                              String chapterTitle,
                              Int32 chapterNumber,
                              String headerVersion,
                              String headerType,
                              String[] tocEntries,
                              String resourceName,
                              Byte[] resourceData)
    {
        public String BookTitle { get; } = bookTitle ?? throw new ArgumentNullException(nameof(bookTitle));

        public String AuthorName { get; } = authorName ?? throw new ArgumentNullException(nameof(authorName));
        
        public String PublisherName { get; } = publisherName ?? throw new ArgumentNullException(nameof(publisherName));
        
        public String ChapterTitle { get; } = chapterTitle ?? throw new ArgumentNullException(nameof(chapterTitle));
        
        public Int32 ChapterNumber { get; } = chapterNumber;
        
        public String HeaderVersion { get; } = headerVersion ?? throw new ArgumentNullException(nameof(headerVersion));
        
        public String HeaderType { get; } = headerType ?? throw new ArgumentNullException(nameof(headerType));
        
        public String[] TocEntries { get; } = tocEntries ?? throw new ArgumentNullException(nameof(tocEntries));
        
        public String ResourceName { get; } = resourceName ?? throw new ArgumentNullException(nameof(resourceName));
        
        public Byte[] ResourceData { get; } = resourceData ?? throw new ArgumentNullException(nameof(resourceData));
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromMobiFileExtenstions
{
    private static XDocument BuildXmlInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input argument cannot be null.");

            var root = new XElement("MobiFile");
            var xmlDoc = new XDocument(root);

            if (input is String path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"File not found at path: {path}");

                var bytes = File.ReadAllBytes(path);
                root.Add(new XElement("Content", Convert.ToBase64String(bytes)));
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException($"FileInfo does not exist: {fileInfo.FullName}");

                var bytes = File.ReadAllBytes(fileInfo.FullName);
                root.Add(new XElement("Content", Convert.ToBase64String(bytes)));
            }
            else if (input is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Stream is not readable.");

                using MemoryStream ms = new();
                stream.CopyTo(ms);
                root.Add(new XElement("Content", Convert.ToBase64String(ms.ToArray())));
            }
            else if (input is Byte[] bytesArray)
                root.Add(new XElement("Content", Convert.ToBase64String(bytesArray)));
            else if (input is MobiFileData mobi)
                root.Add(BuildXElementFromMobiData(mobi));
            else if (input is MobiFileData[] mobiArray)
            {
                foreach (var mobiItem in mobiArray)
                {
                    if (mobiItem == null)
                        throw new ArgumentNullException(nameof(mobiItem), "One of the MobiFileData elements is null.");

                    root.Add(BuildXElementFromMobiData(mobiItem));
                }
            }
            else if (input is String[] paths)
            {
                foreach (var p in paths)
                {
                    if (!File.Exists(p))
                        throw new FileNotFoundException($"File not found in array: {p}");

                    root.Add(new XElement("File", Convert.ToBase64String(File.ReadAllBytes(p))));
                }
            }
            else if (input is MemoryStream memStream)
                root.Add(new XElement("Content", Convert.ToBase64String(memStream.ToArray())));
            else if (input is FileStream fs)
            {
                using MemoryStream ms = new();
                fs.CopyTo(ms);
                root.Add(new XElement("Content", Convert.ToBase64String(ms.ToArray())));
            }
            else if (input is ReadOnlyMemory<Byte> rom)
                root.Add(new XElement("Content", Convert.ToBase64String(rom.ToArray())));
            else if (input is TextReader textReader)
                root.Add(new XElement("Content", textReader.ReadToEnd()));
            else if (input is Object obj)
                root.Add(new XElement("ObjectType", obj.GetType().FullName));
            else
                throw new NotSupportedException($"Type {input.GetType().FullName} is not supported for Mobi XML conversion.");

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Input parameter cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiItem"))
        {
            throw new InvalidOperationException($"One of the MobiFileData elements is null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"The specified file was not found: {ex.FileName}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Stream"))
        {
            throw new InvalidOperationException($"The provided stream cannot be read.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException($"The provided type is not supported for Mobi XML conversion.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"An I/O error occurred while reading the file or stream.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"Access to the file is denied.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error while building XML from Mobi file.", ex);
        }
    }

    private static XElement BuildXElementFromMobiData(MobiFileData mobi)
    {
        XElement element = new XElement("MobiData");
        element.Add(new XElement("BookTitle", mobi.BookTitle));
        element.Add(new XElement("Author", mobi.AuthorName));
        element.Add(new XElement("Publisher", mobi.PublisherName));
        element.Add(new XElement("ChapterTitle", mobi.ChapterTitle));
        element.Add(new XElement("ChapterNumber", mobi.ChapterNumber));
        element.Add(new XElement("HeaderVersion", mobi.HeaderVersion));
        element.Add(new XElement("HeaderType", mobi.HeaderType));
        element.Add(new XElement("TocEntries", String.Join(",", mobi.TocEntries)));
        element.Add(new XElement("ResourceName", mobi.ResourceName));
        element.Add(new XElement("ResourceData", Convert.ToBase64String(mobi.ResourceData)));
        return element;
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromMobiFileExtenstions
{
    public static XDocument BuildXmlFromMobiFile(this String filePath)
    {
        return BuildXmlInternal(filePath);
    }

    public static XDocument BuildXmlFromMobiFile(this FileInfo fileInfo)
    {
        return BuildXmlInternal(fileInfo);
    }

    public static XDocument BuildXmlFromMobiFile(this Stream fileStream)
    {
        return BuildXmlInternal(fileStream);
    }

    public static XDocument BuildXmlFromMobiFile(this Byte[] fileBytes)
    {
        return BuildXmlInternal(fileBytes);
    }

    public static XDocument BuildXmlFromMobiFile(this MobiFileData mobiData)
    {
        return BuildXmlInternal(mobiData);
    }

    public static XDocument BuildXmlFromMobiFile(this MobiFileData[] mobiArray)
    {
        return BuildXmlInternal(mobiArray);
    }

    public static XDocument BuildXmlFromMobiFile(this String[] filePaths)
    {
        return BuildXmlInternal(filePaths);
    }

    public static XDocument BuildXmlFromMobiFile(this MemoryStream memoryStream)
    {
        return BuildXmlInternal(memoryStream);
    }

    public static XDocument BuildXmlFromMobiFile(this FileStream fileStream)
    {
        return BuildXmlInternal(fileStream);
    }

    public static XDocument BuildXmlFromMobiFile(this Uri fileUri)
    {
        return BuildXmlInternal(fileUri);
    }

    public static XDocument BuildXmlFromMobiFile(this ReadOnlyMemory<Byte> readOnlyBytes)
    {
        return BuildXmlInternal(readOnlyBytes);
    }

    public static XDocument BuildXmlFromMobiFile(this TextReader reader)
    {
        return BuildXmlInternal(reader);
    }

    public static XDocument BuildXmlFromMobiFile(this TextWriter writer)
    {
        return BuildXmlInternal(writer);
    }

    public static XDocument BuildXmlFromMobiFile(this Object genericObject)
    {
        return BuildXmlInternal(genericObject);
    }
}