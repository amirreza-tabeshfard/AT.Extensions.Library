using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Class
/// </summary>
public static partial class BuildXmlFromLogFileExtenstions
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }

        public String Level { get; set; } = null!;
        
        public String Message { get; set; } = null!;
    }

    public class LogFileContainer
    {
        public IEnumerable<String> Logs { get; set; } = null!;
    }

    public class CustomLogStream(IEnumerable<String> lines)
    {
        private readonly IEnumerable<String> _lines = lines;

        public IEnumerable<String> ReadLines() => _lines;
    }

    public class LogBoundaryCategory
    {
        public IEnumerable<String> Logs { get; set; } = null!;
    }

    public class XmlLogSettings
    {
        public IEnumerable<String> LogSource { get; set; } = null!;
    }

    public class LogPathSettings
    {
        public IEnumerable<String> Paths { get; set; } = null!;
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromLogFileExtenstions
{
    private static XDocument ProcessLogToXml(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input argument cannot be null.");

            var doc = new XDocument(new XElement("Logs"));

            if (input is String filePath)
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Log file not found at path: {filePath}");

                foreach (var line in File.ReadAllLines(filePath))
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException($"Log file does not exist: {fileInfo.FullName}");

                foreach (var line in File.ReadAllLines(fileInfo.FullName))
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream);
                String line;

                while ((line = reader.ReadLine()) != null)
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is TextReader textReader)
            {
                String line;

                while ((line = textReader.ReadLine()) != null)
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is Byte[] bytes)
            {
                using var ms = new MemoryStream(bytes);
                using var reader = new StreamReader(ms);
                String line;

                while ((line = reader.ReadLine()) != null)
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is IEnumerable<String> stringEnumerable)
            {
                foreach (var logLine in stringEnumerable)
                {
                    if (logLine == null) 
                        throw new InvalidDataException("Log line cannot be null.");

                    doc.Root.Add(new XElement("Log", logLine));
                }
            }
            else if (input is IEnumerable<LogEntry> logEntryEnumerable)
            {
                foreach (var entry in logEntryEnumerable)
                {
                    if (entry == null)
                        throw new InvalidDataException("LogEntry cannot be null.");

                    doc.Root.Add(new XElement("LogEntry", new XElement("Timestamp", entry.Timestamp), new XElement("Level", entry.Level), new XElement("Message", entry.Message)));
                }
            }
            else if (input is LogFileContainer container)
            {
                if (container.Logs == null) 
                    throw new InvalidDataException("LogFileContainer.Logs cannot be null.");

                foreach (var logLine in container.Logs)
                    doc.Root.Add(new XElement("Log", logLine));
            }
            else if (input is String[] stringArray)
            {
                foreach (var logLine in stringArray)
                {
                    if (logLine == null) 
                        throw new InvalidDataException("Log line in array cannot be null.");

                    doc.Root.Add(new XElement("Log", logLine));
                }
            }
            else if (input is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                using var reader = new StreamReader(memoryStream);
                String line;
                while ((line = reader.ReadLine()) != null)
                    doc.Root.Add(new XElement("Log", line));
            }
            else if (input is CustomLogStream customLog)
            {
                foreach (var logLine in customLog.ReadLines())
                    doc.Root.Add(new XElement("Log", logLine));
            }
            else if (input is LogBoundaryCategory boundary)
            {
                if (boundary.Logs == null)
                    throw new InvalidDataException("Boundary category logs cannot be null.");

                foreach (var logLine in boundary.Logs)
                    doc.Root.Add(new XElement("Log", logLine));
            }
            else if (input is XmlLogSettings settings)
            {
                if (settings.LogSource == null)
                    throw new InvalidDataException("XmlLogSettings.LogSource cannot be null.");

                foreach (var logLine in settings.LogSource)
                    doc.Root.Add(new XElement("Log", logLine));
            }
            else if (input is LogPathSettings pathSettings)
            {
                if (pathSettings.Paths == null)
                    throw new InvalidDataException("LogPathSettings.Paths cannot be null.");

                foreach (var filePathItem in pathSettings.Paths)
                {
                    if (!File.Exists(filePathItem))
                        throw new FileNotFoundException($"Log file not found at path: {filePathItem}");

                    foreach (var line in File.ReadAllLines(filePathItem))
                        doc.Root.Add(new XElement("Log", line));
                }
            }
            else
            {
                throw new NotSupportedException($"Input type {input.GetType().FullName} is not supported for XML conversion.");
            }

            return doc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Failed to build XML from log file: Input argument is null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Failed to build XML from log file: Log file not found at specified path '{ex.FileName}'.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("Log line cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: A log line was null in the provided input.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("LogEntry cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: A LogEntry Object in the input is null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("LogFileContainer.Logs cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: The Logs property in LogFileContainer is null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("Log line in array cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: A log line in the array input is null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("Boundary category logs cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: Logs in the boundary category are null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("XmlLogSettings.LogSource cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: LogSource property in XmlLogSettings is null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message is not null && ex.Message.Contains("LogPathSettings.Paths cannot be null."))
        {
            throw new InvalidOperationException("Failed to build XML from log file: Paths property in LogPathSettings is null.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("is not supported"))
        {
            throw new InvalidOperationException($"Failed to build XML from log file: {ex.Message}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Length > 0)
        {
            throw new InvalidOperationException($"Failed to build XML from log file: IO operation failed. Source: {ex.Source}", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Length > 0)
        {
            throw new InvalidOperationException($"Failed to build XML from log file: Access to file denied. Source: {ex.Source}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XML from log file: An unexpected error occurred.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromLogFileExtenstions
{
    public static XDocument BuildXmlFromLogFile(this String logFilePath)
    {
        return ProcessLogToXml(logFilePath);
    }

    public static XDocument BuildXmlFromLogFile(this FileInfo logFile)
    {
        return ProcessLogToXml(logFile);
    }

    public static XDocument BuildXmlFromLogFile(this Stream logStream)
    {
        return ProcessLogToXml(logStream);
    }

    public static XDocument BuildXmlFromLogFile(this TextReader logReader)
    {
        return ProcessLogToXml(logReader);
    }

    public static XDocument BuildXmlFromLogFile(this Byte[] logBytes)
    {
        return ProcessLogToXml(logBytes);
    }

    public static XDocument BuildXmlFromLogFile(this IEnumerable<String> logLines)
    {
        return ProcessLogToXml(logLines);
    }

    public static XDocument BuildXmlFromLogFile(this LogEntry[] logEntries)
    {
        return ProcessLogToXml(logEntries);
    }

    public static XDocument BuildXmlFromLogFile(this IEnumerable<LogEntry> logEntries)
    {
        return ProcessLogToXml(logEntries);
    }

    public static XDocument BuildXmlFromLogFile(this LogFileContainer logContainer)
    {
        return ProcessLogToXml(logContainer);
    }

    public static XDocument BuildXmlFromLogFile(this String[] logLines)
    {
        return ProcessLogToXml(logLines);
    }

    public static XDocument BuildXmlFromLogFile(this MemoryStream logMemoryStream)
    {
        return ProcessLogToXml(logMemoryStream);
    }

    public static XDocument BuildXmlFromLogFile(this CustomLogStream customLog)
    {
        return ProcessLogToXml(customLog);
    }

    public static XDocument BuildXmlFromLogFile(this LogBoundaryCategory boundaryCategory)
    {
        return ProcessLogToXml(boundaryCategory);
    }

    public static XDocument BuildXmlFromLogFile(this XmlLogSettings logSettings)
    {
        return ProcessLogToXml(logSettings);
    }

    public static XDocument BuildXmlFromLogFile(this LogPathSettings logPathSettings)
    {
        return ProcessLogToXml(logPathSettings);
    }
}