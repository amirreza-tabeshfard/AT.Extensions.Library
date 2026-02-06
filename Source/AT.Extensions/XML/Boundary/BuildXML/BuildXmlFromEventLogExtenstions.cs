using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromEventLogExtenstions
{
    private static XDocument ConvertToXDocument(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input argument cannot be null.");

            if (input is EventLog log)
                return new XDocument(new XElement("EventLog", log.Entries.Cast<EventLogEntry>().Select(ToElement)));

            if (input is EventLogEntry entry)
                return new XDocument(new XElement("EventLog", ToElement(entry)));

            if (input is EventLogEntryCollection collection)
                return new XDocument(new XElement("EventLog", collection.Cast<EventLogEntry>().Select(ToElement)));

            if (input is IEnumerable<EventLogEntry> enumerable)
                return new XDocument(new XElement("EventLog", enumerable.Select(ToElement)));

            if (input is EventRecord record)
                return new XDocument(new XElement("EventLog", new XElement("Event", new XElement("Id", record.Id), new XElement("Level", record.LevelDisplayName), new XElement("Provider", record.ProviderName), new XElement("TimeCreated", record.TimeCreated), new XElement("Message", record.FormatDescription()))));

            if (input is String path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("XML file path does not exist.", path);

                return XDocument.Load(path);
            }

            if (input is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException("Provided FileInfo does not exist.", file.FullName);

                return XDocument.Load(file.FullName);
            }

            if (input is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                return XDocument.Load(memoryStream);
            }

            if (input is Stream stream)
                return XDocument.Load(stream);

            if (input is XDocument xdoc)
                return new XDocument(xdoc);

            if (input is XmlDocument xmlDoc)
                return XDocument.Parse(xmlDoc.OuterXml);

            if (input is XElement element)
                return new XDocument(element);

            if (input is Byte[] buffer)
            {
                using var ms = new MemoryStream(buffer);
                return XDocument.Load(ms);
            }

            throw new NotSupportedException($"Unsupported input type '{input.GetType().FullName}'. This overload of BuildXmlFromEventLog cannot handle it.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The provided input argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument was null and cannot be processed.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The specified directory for the XML source was not found.", ex);
        }
        catch (FileLoadException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML file was found but could not be loaded.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The specified XML file does not exist.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content format is invalid.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The provided stream or buffer contains invalid data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred during document construction.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("A low-level I/O error occurred while reading XML data.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The provided input type is not supported by BuildXmlFromEventLog.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The provided stream was already disposed.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The specified file path exceeds the supported length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the specified file or stream was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML is malformed or violates XML well-formedness rules.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to convert input into XDocument. Root cause: {ex.Message}", ex);
        }
    }

    private static XDocument Synchronize(XDocument document)
    {
        if (document.Root is null)
            throw new InvalidOperationException("Generated XML does not contain a root element.");

        var orderedEvents = document
                            .Root
                            .Elements("Event")
                            .OrderBy(e => (DateTime?)e.Element("TimeGenerated") ?? DateTime.MinValue)
                            .ThenBy(e => (String?)e.Element("Source") ?? String.Empty)
                            .ToList();

        document.Root.ReplaceNodes(orderedEvents);

        return document;
    }

    private static XElement ToElement(EventLogEntry entry)
    {
        return new XElement("Event",
               new XElement("Index", entry.Index),
               new XElement("EntryType", entry.EntryType.ToString()),
               new XElement("Source", entry.Source),
               new XElement("MachineName", entry.MachineName),
               new XElement("TimeGenerated", entry.TimeGenerated),
               new XElement("Message", entry.Message));
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromEventLogExtenstions
{
    public static XDocument BuildXmlFromEventLog(this EventLog log)
    {
        return Synchronize(ConvertToXDocument(log));
    }

    public static XDocument BuildXmlFromEventLog(this EventLogEntry entry)
    {
        return Synchronize(ConvertToXDocument(entry));
    }

    public static XDocument BuildXmlFromEventLog(this EventLogEntryCollection entries)
    {
        return Synchronize(ConvertToXDocument(entries));
    }

    public static XDocument BuildXmlFromEventLog(this IEnumerable<EventLogEntry> entries)
    {
        return Synchronize(ConvertToXDocument(entries));
    }

    public static XDocument BuildXmlFromEventLog(this IReadOnlyCollection<EventLogEntry> entries)
    {
        return Synchronize(ConvertToXDocument(entries));
    }

    public static XDocument BuildXmlFromEventLog(this List<EventLogEntry> entries)
    {
        return Synchronize(ConvertToXDocument(entries));
    }

    public static XDocument BuildXmlFromEventLog(this EventRecord record)
    {
        return Synchronize(ConvertToXDocument(record));
    }

    public static XDocument BuildXmlFromEventLog(this String xmlFilePath)
    {
        return Synchronize(ConvertToXDocument(xmlFilePath));
    }

    public static XDocument BuildXmlFromEventLog(this FileInfo file)
    {
        return Synchronize(ConvertToXDocument(file));
    }

    public static XDocument BuildXmlFromEventLog(this Stream stream)
    {
        return Synchronize(ConvertToXDocument(stream));
    }

    public static XDocument BuildXmlFromEventLog(this MemoryStream stream)
    {
        return Synchronize(ConvertToXDocument(stream));
    }

    public static XDocument BuildXmlFromEventLog(this XDocument document)
    {
        return Synchronize(ConvertToXDocument(document));
    }

    public static XDocument BuildXmlFromEventLog(this XmlDocument document)
    {
        return Synchronize(ConvertToXDocument(document));
    }

    public static XDocument BuildXmlFromEventLog(this XElement element)
    {
        return Synchronize(ConvertToXDocument(element));
    }

    public static XDocument BuildXmlFromEventLog(this Byte[] buffer)
    {
        return Synchronize(ConvertToXDocument(buffer));
    }
}