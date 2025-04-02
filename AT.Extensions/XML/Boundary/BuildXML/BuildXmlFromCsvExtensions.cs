namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromCsvExtensions
    : Object
{
    public static String BuildXmlFromCsv(this String filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
                throw new InvalidOperationException("CSV file must have at least one data row.");

            String[] headers = lines[0].Split(',');
            System.Xml.Linq.XElement xml = new("Root", lines
                                                       .Skip(1)
                                                       .Select(line => new System.Xml.Linq.XElement("Row", line
                                                                                                           .Split(',')
                                                                                                           .Select((value, index) => new System.Xml.Linq.XElement(headers[index], value)))));
            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentNullException(nameof(filePath), "File path cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("File path cannot be empty or contain only white spaces.", nameof(filePath));
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(filePath))
        {
            throw new FileNotFoundException($"The specified file '{filePath}' was not found.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the file is denied. Check file permissions.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An error occurred while accessing the file. Ensure the file is not in use.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The CSV file format is invalid. Ensure all rows have the correct number of columns.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV file must have at least one data row."))
        {
            throw new InvalidOperationException("The CSV file must contain at least one row of data after the headers.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid CSV format. Ensure the file follows the correct structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the CSV file.", ex);
        }
    }

    public static String BuildXmlFromCsv(this String csvContent, Char delimiter)
    {
        ArgumentException.ThrowIfNullOrEmpty(csvContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(csvContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = csvContent.Split('\n');
            if (lines.Length < 2)
                throw new InvalidOperationException("CSV content must have at least one data row.");

            String[] headers = lines[0].Split(delimiter);
            System.Xml.Linq.XElement xml = new("Root", lines
                                                       .Skip(1)
                                                       .Select(line => new System.Xml.Linq.XElement("Row", line
                                                                                                           .Split(delimiter)
                                                                                                           .Select((value, index) => new System.Xml.Linq.XElement(headers[index], value)))));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvContent)))
        {
            throw new ArgumentNullException(nameof(csvContent), "CSV content cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvContent)))
        {
            throw new ArgumentException("CSV content cannot be empty or contain only white spaces.", nameof(csvContent));
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(delimiter)))
        {
            throw new ArgumentException("Delimiter cannot be an invalid character.", nameof(delimiter));
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV content must have at least one data row."))
        {
            throw new InvalidOperationException("The CSV content must contain at least one row of data after the headers.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The CSV format is invalid. Ensure all rows have the correct number of columns.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid CSV format. Ensure the content follows the correct structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the CSV content.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsv(this StreamReader csvStream)
    {
        ArgumentNullException.ThrowIfNull(csvStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = csvStream.ReadToEnd().Split('\n');
            if (lines.Length < 2)
                throw new InvalidOperationException("CSV content must have at least one data row.");

            String[] headers = lines[0].Split(',');

            System.Xml.Linq.XDocument xml = new("Root", lines
                                                       .Skip(1)
                                                       .Select(line => new System.Xml.Linq.XElement("Row", line
                                                                                                           .Split(',')
                                                                                                           .Select((value, index) => new System.Xml.Linq.XElement(headers[index], value)))));
            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvStream)))
        {
            throw new ArgumentNullException(nameof(csvStream), "CSV stream cannot be null.");
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(csvStream)))
        {
            throw new ObjectDisposedException(nameof(csvStream), "The CSV stream has been closed or disposed.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV content must have at least one data row."))
        {
            throw new InvalidOperationException("The CSV content must contain at least one row of data after the headers.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An error occurred while reading the CSV stream. Ensure the stream is accessible and not in use.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The CSV format is invalid. Ensure all rows have the correct number of columns.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid CSV format. Ensure the content follows the correct structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the CSV content.", ex);
        }
    }

    public static String BuildXmlFromCsv(this System.Data.DataTable table)
    {
        ArgumentNullException.ThrowIfNull(table);
        // ----------------------------------------------------------------------------------------------------
        using StringWriter writer = new();
        // ----------------------------------------------------------------------------------------------------
        try
        {
            table.WriteXml(writer);
            return writer.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The DataTable cannot be null.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("DataTable must have at least one row."))
        {
            throw new InvalidOperationException("The DataTable must contain at least one row to generate XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("DataTable has no columns."))
        {
            throw new InvalidOperationException("The DataTable must have at least one column to generate XML.", ex);
        }
        catch (System.Data.DataException ex)
        {
            throw new System.Data.DataException("An error occurred while processing the DataTable. Ensure the data structure is valid.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(writer)))
        {
            throw new ObjectDisposedException(nameof(writer), "The StringWriter instance has been disposed before writing XML.");
        }
        catch (IOException ex)
        {
            throw new IOException("An error occurred while writing XML data. Ensure sufficient memory and stream availability.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating XML from DataTable.", ex);
        }
    }

    public static String BuildXmlFromCsv(this IEnumerable<String> csvLines)
    {
        ArgumentNullException.ThrowIfNull(csvLines);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = csvLines.ToArray();
            
            if (lines.Length < 2)
                throw new InvalidOperationException("CSV content must have at least one data row.");

            String[] headers = lines[0].Split(',');

            System.Xml.Linq.XElement xml = new("Root", lines
                                                       .Skip(1)
                                                       .Select(line => new System.Xml.Linq.XElement("Row", line
                                                                                                           .Split(',')
                                                                                                           .Select((value, index) => new System.Xml.Linq.XElement(headers[index], value)))));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvLines)))
        {
            throw new ArgumentNullException(nameof(csvLines), "The CSV lines collection cannot be null.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV content must have at least one data row."))
        {
            throw new InvalidOperationException("The CSV content must contain at least one row of data after the headers.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            throw new InvalidOperationException("The provided CSV lines collection is empty.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid CSV format. Ensure the content follows the correct structure.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The CSV format is invalid. Ensure all rows have the correct number of columns.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("The operation ran out of memory while processing CSV lines. Consider optimizing memory usage.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating XML from CSV lines.", ex);
        }
    }

    public static String BuildXmlFromCsv(this IEnumerable<Dictionary<String, String>> csvData)
    {
        ArgumentNullException.ThrowIfNull(csvData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root", csvData
                                                       .Select(row => new System.Xml.Linq.XElement("Row", row
                                                                                                          .Select(kvp => new System.Xml.Linq.XElement(kvp.Key, kvp.Value)))));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvData)))
        {
            throw new ArgumentNullException(nameof(csvData), "The CSV data collection cannot be null.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            throw new InvalidOperationException("The provided CSV data collection is empty.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("A required key is missing in one of the dictionary entries.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid CSV format. Ensure the dictionary structure is correct.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while generating XML from the CSV data.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("The operation ran out of memory while processing the CSV data. Consider optimizing memory usage.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating XML from CSV data.", ex);
        }
    }

    public static void BuildXmlFromCsv(this String csvFilePath, String xmlFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(csvFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(csvFilePath);
        ArgumentException.ThrowIfNullOrEmpty(xmlFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlContent = csvFilePath.BuildXmlFromCsv();
            File.WriteAllText(xmlFilePath, xmlContent);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(xmlFilePath)))
        {
            throw new ArgumentNullException(nameof(xmlFilePath), "The XML file path cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvFilePath)))
        {
            throw new ArgumentNullException(nameof(csvFilePath), "The CSV file path cannot be null.");
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(csvFilePath))
        {
            throw new FileNotFoundException("The specified CSV file was not found.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains(xmlFilePath))
        {
            throw new UnauthorizedAccessException("Access to the XML file path is denied. Check permissions.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains(csvFilePath))
        {
            throw new IOException("An I/O error occurred while reading the CSV file.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains(xmlFilePath))
        {
            throw new IOException("An I/O error occurred while writing the XML file.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains(xmlFilePath))
        {
            throw new DirectoryNotFoundException("The directory for the XML file path does not exist.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV content must have at least one data row."))
        {
            throw new InvalidOperationException("The provided CSV file is empty or does not contain data rows.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while generating XML from the CSV file.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("The operation ran out of memory while processing the CSV file. Consider optimizing memory usage.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating and saving XML from the CSV file.", ex);
        }
    }

    public static MemoryStream BuildXmlFromCsvToStream(this String csvContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(csvContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(csvContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(csvContent.BuildXmlFromCsv(','));
            MemoryStream stream = new();
            xml.Save(stream);
            stream.Position = 0;
            return stream;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvContent)))
        {
            throw new ArgumentException("The CSV content cannot be null, empty, or contain only whitespace.", nameof(csvContent), ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CSV content must have at least one data row."))
        {
            throw new InvalidOperationException("The provided CSV content is empty or does not contain valid data rows.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV content to XML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("The operation ran out of memory while processing the CSV content. Consider optimizing memory usage.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while writing XML to the memory stream.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while generating the XML stream.", ex);
        }
    }
}