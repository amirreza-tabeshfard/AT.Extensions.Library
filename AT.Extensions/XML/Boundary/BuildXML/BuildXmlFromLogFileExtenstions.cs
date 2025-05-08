namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromLogFileExtenstions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFilePath);
                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath"))
        {
            throw new InvalidOperationException("The log file path argument is invalid.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The directory containing the log file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The specified log file could not be found.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the log file.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The path to the log file is in an invalid format.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The path to the log file is too long.", ex);
        }
        catch (System.UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("You do not have permission to access the log file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.Xml"))
        {
            throw new InvalidOperationException("The log file is not a well-formed XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error building XML from log file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this Stream logStream)
    {
        ArgumentNullException.ThrowIfNull(logStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logStream)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logStream);
                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logStream"))
        {
            throw new InvalidOperationException("The log stream argument is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The stream is in an invalid state for reading.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The stream does not support the required operations.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The stream has been closed or disposed.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the stream.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Security"))
        {
            throw new InvalidOperationException("Access to the stream was denied due to security restrictions.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.Xml"))
        {
            throw new InvalidOperationException("The stream does not contain a well-formed XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error building XML from log stream.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, System.Xml.XmlDocument existingXmlDocument)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        ArgumentNullException.ThrowIfNull(existingXmlDocument);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = existingXmlDocument ?? new System.Xml.XmlDocument();
                xmlDocument.Load(logFilePath);
                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath"))
        {
            throw new ArgumentException("Invalid argument: logFilePath is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("existingXmlDocument"))
        {
            throw new ArgumentNullException("XML document reference is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new DirectoryNotFoundException("Directory containing the log file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(logFilePath))
        {
            throw new FileNotFoundException("The specified log file was not found on disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while accessing the log file.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML document is in an invalid state.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new PathTooLongException("The path to the log file exceeds the system-defined maximum length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new UnauthorizedAccessException("Access to the log file is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Xml.XmlException("The log file does not contain valid XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error building XML from log file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, System.Xml.XmlWriterSettings settings)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                
                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(new StringWriter(), settings))
                {
                    xmlDocument.Save(writer);
                }

                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath"))
        {
            throw new ArgumentException("Invalid argument: 'logFilePath' is either null, empty, or contains only white-space characters.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("settings"))
        {
            throw new ArgumentNullException("Invalid argument: 'settings' cannot be null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new DirectoryNotFoundException("The directory specified in the log file path could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new FileNotFoundException("The log file was not found at the specified path. Ensure the file exists and the path is correct.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while attempting to access the log file.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ObjectDisposedException("The XmlWriter or underlying stream was unexpectedly disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new OutOfMemoryException("There is insufficient memory to continue the execution of the program during XML document creation.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new UnauthorizedAccessException("Access to the log file path is denied. Check permissions and access rights.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Xml.XmlException("An error occurred while creating or saving the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error building XML with custom settings.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, String xpathQuery)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        ArgumentNullException.ThrowIfNull(xpathQuery);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFilePath);

                System.Xml.XmlNodeList? nodes = xmlDocument.SelectNodes(xpathQuery);
                return nodes == null ? throw new InvalidOperationException("The XPath query did not return any matching nodes. Ensure the query is correctly formulated and the XML content contains the expected structure.") : xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath"))
        {
            throw new ArgumentException("Invalid argument: logFilePath cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xpathQuery"))
        {
            throw new ArgumentNullException("The XPath query parameter cannot be null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new DirectoryNotFoundException("The directory specified in the log file path does not exist.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new FileNotFoundException("The specified log file could not be found. Ensure the file exists at the provided path.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new PathTooLongException("The log file path is too long. Please shorten the path and try again.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new UnauthorizedAccessException("Access to the log file was denied. Ensure the application has the necessary permissions.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.Xml"))
        {
            throw new System.Xml.XmlException("An error occurred while parsing the XML from the log file. Ensure the file contains valid XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The XPath query returned no results. Check the structure of the XML and the accuracy of the XPath.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new IOException("An I/O error occurred while reading the log file. Ensure the file is not in use or locked by another process.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new NotSupportedException("The log file path format is not supported.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the log file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, Func<System.Xml.XmlNode, Boolean> nodeFilter)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        ArgumentNullException.ThrowIfNull(nodeFilter);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFilePath);

                List<System.Xml.XmlNode> filteredNodes = new();
                
                filteredNodes.AddRange(from System.Xml.XmlNode node in xmlDocument.ChildNodes
                                       where nodeFilter(node)
                                       select node);

                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath"))
        {
            throw new ArgumentException("Invalid argument provided for log file path.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("nodeFilter"))
        {
            throw new ArgumentNullException("The node filter delegate cannot be null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new DirectoryNotFoundException("The directory specified in the log file path does not exist.", ex);
        }
        catch (FileLoadException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new FileLoadException("The log file could not be loaded properly. It may be locked or in use by another process.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new FileNotFoundException("Log file was not found during the loading process.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while accessing the log file.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidDataException("The log file contains invalid data and could not be processed.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new NotSupportedException("The log file format is not supported.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new PathTooLongException("The specified path to the log file is too long.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new UnauthorizedAccessException("Access to the log file was denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Xml.XmlException("An error occurred while parsing the log file as XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the log file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this FileStream logFileStream, Func<System.Xml.XmlNode, System.Xml.XmlNode> nodeTransformation)
    {
        ArgumentNullException.ThrowIfNull(logFileStream);
        ArgumentNullException.ThrowIfNull(nodeTransformation);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFileStream)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFileStream);
                
                foreach ((System.Xml.XmlNode node, System.Xml.XmlNode transformedNode) in from System.Xml.XmlNode node in xmlDocument.ChildNodes
                                                                                          let transformedNode = nodeTransformation(node)
                                                                                          select (node, transformedNode))
                {
                    node.ParentNode.ReplaceChild(transformedNode, node);
                }

                return xmlDocument;
            }
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFileStream"))
        {
            throw new InvalidOperationException("Log file stream cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("nodeTransformation"))
        {
            throw new InvalidOperationException("Node transformation function cannot be null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Error occurred while loading XML document from log file.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("I/O error occurred while accessing the log file.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Unauthorized access to the log file.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid operation while building XML from log file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from log file with node transformation.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, Boolean validateLog)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (validateLog)
            {
                if (!Path.GetExtension(logFilePath).Equals(".xml", StringComparison.CurrentCultureIgnoreCase))
                    throw new InvalidOperationException($"The log file '{logFilePath}' must be an XML file. The provided file has a '{Path.GetExtension(logFilePath)}' extension, which is invalid for XML processing.");
            }

            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFilePath);
                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"Invalid argument provided for 'logFilePath'. Ensure that the file path is neither null nor empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath", StringComparison.CurrentCultureIgnoreCase) && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException($"The 'logFilePath' cannot be null or contain only whitespace characters.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"Log file '{logFilePath}' was not found. Please ensure that the file exists at the specified location.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("must be an XML file", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"The file '{logFilePath}' is not a valid XML file. Please provide a file with the '.xml' extension.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"Error occurred while processing the XML file '{logFilePath}'. Please ensure the XML is well-formed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"Access to the log file '{logFilePath}' was denied. Please check file permissions.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException($"The log file '{logFilePath}' is being used by another process. Please close any applications that may be using the file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the log file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromLogFile(this String logFilePath, Action<String> logError)
    {
        ArgumentException.ThrowIfNullOrEmpty(logFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(logFilePath))
            throw new FileNotFoundException($"Log file missing: The system could not locate the log file at '{logFilePath}'. Verify that the file path is correct and the file has not been deleted or moved.", logFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            lock (logFilePath)
            {
                System.Xml.XmlDocument xmlDocument = new();
                xmlDocument.Load(logFilePath);
                return xmlDocument;
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath", StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"Invalid argument: The 'logFilePath' cannot be null or empty. Please ensure a valid file path is provided.");
            throw new InvalidOperationException($"Invalid argument provided for 'logFilePath'. Ensure that the file path is neither null nor empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("logFilePath", StringComparison.CurrentCultureIgnoreCase) && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            logError?.Invoke($"Invalid argument: The 'logFilePath' cannot contain only whitespace characters.");
            throw new InvalidOperationException($"The 'logFilePath' cannot be null or contain only whitespace characters.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"File not found: The system could not locate the log file at '{logFilePath}'. Please check the file path.");
            throw new InvalidOperationException($"Log file '{logFilePath}' was not found. Please ensure that the file exists at the specified location.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"Access denied: Permission to access the log file '{logFilePath}' was denied.");
            throw new InvalidOperationException($"Access to the log file '{logFilePath}' was denied. Please check file permissions.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file", StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"File is in use: The log file '{logFilePath}' is being used by another process.");
            throw new InvalidOperationException($"The log file '{logFilePath}' is being used by another process. Please close any applications that may be using the file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals(logFilePath, StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"XML parsing error: An issue occurred while processing the XML file '{logFilePath}'. Ensure the XML is well-formed.");
            throw new InvalidOperationException($"Error occurred while processing the XML file '{logFilePath}'. Please ensure the XML is well-formed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error building XML", StringComparison.CurrentCultureIgnoreCase))
        {
            logError?.Invoke($"Invalid operation: An error occurred while building the XML from the log file '{logFilePath}'.");
            throw new InvalidOperationException($"Error building XML with custom error handling.", ex);
        }
        catch (Exception ex)
        {
            logError?.Invoke($"Unexpected error: An unexpected error occurred while processing the log file '{logFilePath}'.");
            throw new InvalidOperationException("An unexpected error occurred while processing the log file.", ex);
        }
    }
}