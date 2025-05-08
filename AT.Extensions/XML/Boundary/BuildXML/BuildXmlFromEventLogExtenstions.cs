namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromEventLogExtenstions
    : Object
{
    public static String BuildXmlFromEventLog(this System.Diagnostics.EventLog log)
    {
        ArgumentNullException.ThrowIfNull(log);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("System.Diagnostics.EventLog",
                new System.Xml.Linq.XElement("LogDisplayName", log.LogDisplayName),
                new System.Xml.Linq.XElement("EntriesCount", log.Entries.Count)
            ));

            return doc.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ApplicationException("Parameter 'log' was null when building XML from EventLog.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot access a disposed object"))
        {
            throw new ApplicationException("Attempted to access a disposed EventLog object while building XML.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("access"))
        {
            throw new ApplicationException("Insufficient permissions to access EventLog entries.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new ApplicationException("Malformed XML structure encountered while building EventLog XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals("System.Diagnostics.EventLog"))
        {
            throw new ApplicationException("XML source for EventLog is invalid.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(5))
        {
            throw new ApplicationException("Access denied when attempting to read EventLog entries.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(2))
        {
            throw new ApplicationException("The specified EventLog does not exist.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("System.Diagnostics.EventLog"))
        {
            throw new ApplicationException("Cannot access EventLog object because it has been disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new ApplicationException("System ran out of memory while generating XML from EventLog.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access"))
        {
            throw new ApplicationException("Unauthorized access when reading EventLog for XML generation.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("EventLog"))
        {
            throw new ApplicationException("An unknown error occurred while processing the EventLog.", ex);
        }
    }

    public static String BuildXmlFromEventLog(this System.Diagnostics.EventLog log, Int32 maxEntries)
    {
        ArgumentNullException.ThrowIfNull(log);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder builder = new();
            builder.Append("<System.Diagnostics.EventLog>");
            builder.AppendFormat("<MaxEntries>{0}</MaxEntries>", maxEntries);
            builder.Append("</System.Diagnostics.EventLog>");
            return builder.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ArgumentException("EventLog parameter is null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("maxEntries"))
        {
            throw new ArgumentException("MaxEntries is out of the allowed range.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The event log is closed"))
        {
            throw new ArgumentException("The event log is closed and cannot be accessed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot access a disposed object"))
        {
            throw new ArgumentException("The event log object has been disposed.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("access") && ex.Message.Contains("denied"))
        {
            throw new ArgumentException("Access to the event log is denied.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The specified log file is not accessible"))
        {
            throw new ArgumentException("The specified log file is not accessible.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new ArgumentException("XML formatting error encountered while building the string.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Index > 0)
        {
            throw new ArgumentException("Encoding error occurred while constructing the XML.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory") || ex.Message.Contains("allocation"))
        {
            throw new ArgumentException("Out of memory while building the XML string.", ex);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unable to convert System.Diagnostics.EventLog to XML String with max entries.", ex);
        }
    }

    public static Stream BuildXmlFromEventLog(this System.Diagnostics.EventLog log, String sourceFilter)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentException.ThrowIfNullOrEmpty(sourceFilter);
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceFilter);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            List<System.Diagnostics.EventLogEntry> filteredEntries = new();

            filteredEntries.AddRange(from System.Diagnostics.EventLogEntry entry in log.Entries
                                     where entry.Source == sourceFilter
                                     select entry);

            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("FilteredLog",
                new System.Xml.Linq.XElement("Source", sourceFilter),
                new System.Xml.Linq.XElement("Count", filteredEntries.Count)
            ));

            MemoryStream memoryStream = new();
            StreamWriter writer = new(memoryStream);
            writer.Write(doc.ToString());
            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new ArgumentNullException("The event log parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sourceFilter"))
        {
            throw new ArgumentException("The source filter parameter is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ArgumentException("The event log parameter is invalid or empty.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while accessing the event log entries.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("Error while creating XML stream from filtered System.Diagnostics.EventLog.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access is denied while trying to read the event log.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new System.Security.SecurityException("The operation is not allowed due to security restrictions.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("There is insufficient memory to complete the operation.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the event log.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromEventLog(this System.Diagnostics.EventLog log, DateTime fromTime)
    {
        ArgumentNullException.ThrowIfNull(log);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            List<System.Xml.Linq.XElement> entries = new();

            entries.AddRange(from System.Diagnostics.EventLogEntry entry in log.Entries
                             where entry.TimeGenerated >= fromTime
                             select new System.Xml.Linq.XElement("Entry",
                                                                 new System.Xml.Linq.XElement("Time", entry.TimeGenerated),
                                                                 new System.Xml.Linq.XElement("Message", entry.Message)));

            return new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement("RecentEvents", entries));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ArgumentException("The provided EventLog instance is null.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.PermissionType is not null)
        {
            throw new InvalidOperationException("Insufficient permissions to access the event log entries.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("collection was modified"))
        {
            throw new InvalidOperationException("The event log entries collection was modified during enumeration.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(5))
        {
            throw new UnauthorizedAccessException("Access to the event log is denied due to insufficient privileges.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(2))
        {
            throw new InvalidOperationException("The event log source was not found.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber.Equals(0))
        {
            throw new InvalidOperationException("An error occurred while constructing the XML document from the event log entries.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new FormatException("One or more event log messages contain invalid XML characters.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("The system ran out of memory while processing event log entries.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("The event log is closed"))
        {
            throw new InvalidOperationException("The event log is closed and cannot be accessed.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("enumeration has either not started or has already finished"))
        {
            throw new InvalidOperationException("Invalid enumeration state encountered while accessing event log entries.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document from the event log.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromEventLog(this System.Diagnostics.EventLog log, String user, Boolean includeMessages)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentException.ThrowIfNullOrEmpty(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(user);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xml = new();
            System.Xml.XmlElement root = xml.CreateElement("UserLog");
            xml.AppendChild(root);

            foreach ((System.Diagnostics.EventLogEntry entry, System.Xml.XmlElement entryNode) in from System.Diagnostics.EventLogEntry entry in log.Entries
                                                                                                  where entry.UserName == user
                                                                                                  let entryNode = xml.CreateElement("Entry")
                                                                                                  select (entry, entryNode))
            {
                entryNode.SetAttribute("Time", entry.TimeGenerated.ToString());
                if (includeMessages)
                {
                    System.Xml.XmlElement messageNode = xml.CreateElement("Message");
                    messageNode.InnerText = entry.Message;
                    entryNode.AppendChild(messageNode);
                }

                root.AppendChild(entryNode);
            }

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ApplicationException("The EventLog object provided is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("user"))
        {
            throw new ApplicationException("The username provided is null or empty.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.PermissionType is not null)
        {
            throw new InvalidOperationException("Insufficient permissions to access the event log entries.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new ApplicationException("The EventLog has been closed or is not accessible.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new ApplicationException("An error occurred while creating or manipulating the XML document.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(5))
        {
            throw new ApplicationException("Access denied while accessing EventLog entries.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(87))
        {
            throw new ApplicationException("The parameter passed to a system call was incorrect while accessing the EventLog.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(2))
        {
            throw new ApplicationException("The system cannot find the file specified, possibly due to missing EventLog.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while building the XML document from the EventLog.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromEventLog(this System.Diagnostics.EventLog log, System.Diagnostics.EventLogEntryType typeFilter)
    {
        ArgumentNullException.ThrowIfNull(log);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            List<System.Xml.Linq.XElement> entries = new();

            entries.AddRange(from System.Diagnostics.EventLogEntry entry in log.Entries
                             where entry.EntryType == typeFilter
                             select new System.Xml.Linq.XElement("Entry", new System.Xml.Linq.XElement("Source", entry.Source), new System.Xml.Linq.XElement("Message", entry.Message)));

            return new System.Xml.Linq.XElement("FilteredByType", entries);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ArgumentNullException("The EventLog instance provided is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message is not null && ex.Message.Contains("collection was modified"))
        {
            throw new InvalidOperationException("The EventLog entries collection was modified during enumeration.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(5))
        {
            throw new UnauthorizedAccessException("Access to the event log is denied. Ensure appropriate permissions.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(2))
        {
            throw new System.IO.FileNotFoundException("The specified event log does not exist.", ex);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode.Equals(87))
        {
            throw new ArgumentException("Invalid parameter passed to the event log API.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber.Equals(0))
        {
            throw new FormatException("Invalid XML format generated while building XElement.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message is not null && ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("Insufficient memory while constructing the XElement structure.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.PermissionType is not null)
        {
            throw new InvalidOperationException("Insufficient permissions to access the event log entries.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("EventLog"))
        {
            throw new ObjectDisposedException("The EventLog object has been disposed and cannot be accessed.", ex);
        }
        catch (Exception ex) when (ex.HResult.Equals(-2146233088)) // E_FAIL or general COM exception
        {
            throw new System.Runtime.InteropServices.COMException("A general COM error occurred while accessing the EventLog.", ex);
        }
        catch (Exception ex)
        {
            throw new FormatException("Failed to create System.Xml.Linq.XElement from System.Diagnostics.EventLog by EntryType.", ex);
        }
    }

    public static MemoryStream BuildXmlFromEventLog(this System.Diagnostics.EventLog log, String logName, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentException.ThrowIfNullOrEmpty(logName);
        ArgumentException.ThrowIfNullOrWhiteSpace(logName);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("LogName",
                                                                             new System.Xml.Linq.XElement("Name", logName),
                                                                             new System.Xml.Linq.XElement("System.Text.Encoding", encoding.EncodingName)));

            MemoryStream memStream = new();
            using StreamWriter writer = new(memStream, encoding, leaveOpen: true);
            writer.Write(doc.ToString());
            writer.Flush();
            memStream.Position = 0;
            return memStream;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new InvalidDataException("EventLog object cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidDataException("Encoding cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logName"))
        {
            throw new InvalidDataException("Log name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logName"))
        {
            throw new InvalidDataException("Log name cannot be whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Could not find the assembly"))
        {
            throw new InvalidDataException("An error occurred while finding necessary assembly for the operation.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file because it is being used by another process"))
        {
            throw new InvalidDataException("The stream is already in use, unable to access the file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'LogName' element is not valid"))
        {
            throw new InvalidDataException("Error in XML formatting for 'LogName'.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Unable to build MemoryStream from System.Diagnostics.EventLog using encoding.", ex);
        }
    }

    public static System.Xml.XmlWriter BuildXmlFromEventLog(this System.Diagnostics.EventLog log, System.Xml.XmlWriterSettings settings)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            MemoryStream stream = new();
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(stream, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("System.Diagnostics.EventLog");

            writer.WriteElementString("EntryCount", log.Entries.Count.ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            stream.Position = 0;

            return writer;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new ArgumentNullException($"Argument {ex.ParamName} cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new ArgumentException("Invalid argument passed to the method.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("operation"))
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the EventLog.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access"))
        {
            throw new UnauthorizedAccessException("Access to the EventLog is denied.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("stream"))
        {
            throw new IOException("Error during stream processing while creating XML writer.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("creating XmlWriter"))
        {
            throw new System.Xml.XmlException("Error occurred while creating the XML Writer from the EventLog.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the EventLog.", ex);
        }
    }

    public static Boolean BuildXmlFromEventLog(this System.Diagnostics.EventLog log, String path, Boolean overwriteIfExists, String tag)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentException.ThrowIfNullOrEmpty(tag);
        ArgumentException.ThrowIfNullOrWhiteSpace(tag);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (File.Exists(path) && !overwriteIfExists)
                return false;

            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement(tag, new System.Xml.Linq.XElement("Entries", log.Entries.Count)));

            doc.Save(path);
            return true;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new ArgumentNullException("One or more required arguments are null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new ArgumentException("The path argument is either null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("tag"))
        {
            throw new ArgumentException("The tag argument is either null or empty.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Could not save"))
        {
            throw new UnauthorizedAccessException("Permission denied while saving the XML to the specified file path.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("file"))
        {
            throw new IOException("An I/O error occurred while trying to access the specified file.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new FileNotFoundException("The specified file path could not be found.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("XDocument"))
        {
            throw new InvalidOperationException("An error occurred while creating or saving the XDocument object.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("EventLog"))
        {
            throw new SystemException("An unexpected system error occurred while working with the EventLog.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the EventLog.", ex);
        }
    }

    public static void BuildXmlFromEventLog(this System.Diagnostics.EventLog log, Action<System.Xml.Linq.XDocument> onBuildComplete)
    {
        ArgumentNullException.ThrowIfNull(log);
        ArgumentNullException.ThrowIfNull(onBuildComplete);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("EventLogSummary", 
                                                new System.Xml.Linq.XElement("LogName", log.Log), 
                                                new System.Xml.Linq.XElement("GeneratedAt", DateTime.UtcNow)));

            onBuildComplete?.Invoke(doc);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("log"))
        {
            throw new ArgumentNullException("The EventLog object is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("onBuildComplete"))
        {
            throw new ArgumentNullException("The callback function (onBuildComplete) is null.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred during XML document creation.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("A format error occurred while processing the XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while building the XML document.", ex);
        }
    }
}