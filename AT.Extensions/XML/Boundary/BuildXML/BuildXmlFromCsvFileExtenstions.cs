namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromCsvFileExtenstions
    : Object
{
    #region Private Method(s)
    
    private static String ConvertToXml(String[] lines, Char delimiter = ',')
    {
        ArgumentNullException.ThrowIfNull(lines);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.Linq.XElement xmlDoc = new("Root");
        String[] headers = lines[0].Split(delimiter);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 1; i < lines.Length; i++)
        {
            System.Xml.Linq.XElement row = new("Row");
            String[] values = lines[i].Split(delimiter);
            
            for (Int32 j = 0; j < headers.Length && j < values.Length; j++)
                row.Add(new System.Xml.Linq.XElement(headers[j], values[j]));
            
            xmlDoc.Add(row);
        }
        // ----------------------------------------------------------------------------------------------------
        return xmlDoc.ToString();
    } 

    #endregion

    public static String BuildXmlFromCsvFile(this String filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException("CSV file not found.", filePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = File.ReadAllLines(filePath);
            return ConvertToXml(lines);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is empty or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(filePath))
        {
            throw new FileNotFoundException("The specified CSV file does not exist.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the specified file is denied. Check file permissions.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException("The specified file path is too long.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException("The directory for the specified file path was not found.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864))
        {
            // ERROR_SHARING_VIOLATION
            throw new IOException("The file is in use by another process.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            // ERROR_FILE_NOT_FOUND
            throw new IOException("Unexpected file not found error occurred while reading the file.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while accessing the file.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV file format is invalid.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred during XML conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while processing the CSV file.", ex);
        }
    }

    public static String BuildXmlFromCsvFile(this String filePath, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException("CSV file not found.", filePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = File.ReadAllLines(filePath, encoding);
            return ConvertToXml(lines);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(encoding)))
        {
            throw new ArgumentException("The provided encoding is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is empty or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(filePath))
        {
            throw new FileNotFoundException("The specified CSV file does not exist.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the specified file is denied. Check file permissions.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException("The specified file path is too long.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException("The directory for the specified file path was not found.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The file path format is not supported.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864))
        {
            // ERROR_SHARING_VIOLATION
            throw new IOException("The file is in use by another process.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            // ERROR_FILE_NOT_FOUND
            throw new IOException("Unexpected file not found error occurred while reading the file.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while accessing the file.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new System.Text.DecoderFallbackException("The specified encoding could not decode the file content properly.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV file format is invalid.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred during XML conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while processing the CSV file.", ex);
        }
    }

    public static String BuildXmlFromCsvFile(this String filePath, Char delimiter)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException("CSV file not found.", filePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = File.ReadAllLines(filePath);
            return ConvertToXml(lines, delimiter);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is empty or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(filePath))
        {
            throw new FileNotFoundException("The specified CSV file does not exist.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the specified file is denied. Check file permissions.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException("The specified file path is too long.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException("The directory for the specified file path was not found.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The file path format is not supported.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864))
        {
            throw new IOException("The file is in use by another process.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024784))
        {
            throw new IOException("Unexpected file not found error occurred while reading the file.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while accessing the file.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV file format is invalid.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The delimiter might be incorrect, causing unexpected column misalignment.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred during XML conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while processing the CSV file.", ex);
        }
    }

    public static String BuildXmlFromCsvFile(this String filePath, Char delimiter, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException("CSV file not found.", filePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String[] lines = File.ReadAllLines(filePath, encoding);
            return ConvertToXml(lines, delimiter);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(encoding)))
        {
            throw new ArgumentException("The provided encoding is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(filePath)))
        {
            throw new ArgumentException("The provided file path is empty or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(filePath))
        {
            throw new FileNotFoundException("The specified CSV file does not exist.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the specified file is denied. Check file permissions.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException("The specified file path is too long.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException("The directory for the specified file path was not found.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The file path format is not supported.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864)) // ERROR_SHARING_VIOLATION
        {
            throw new IOException("The file is in use by another process.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024784)) // ERROR_FILE_NOT_FOUND
        {
            throw new IOException("Unexpected file not found error occurred while reading the file.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while accessing the file.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new System.Text.DecoderFallbackException("The specified encoding could not decode the file content properly.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV file format is invalid.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("The delimiter might be incorrect, causing unexpected column misalignment.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred during XML conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while processing the CSV file.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this Stream csvStream)
    {
        ArgumentNullException.ThrowIfNull(csvStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new StreamReader(csvStream);
            List<string> lines = new();
            while (!reader.EndOfStream)
                lines.Add(item: reader.ReadLine());

            return System.Xml.Linq.XDocument.Parse(ConvertToXml(lines.ToArray()));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvStream)))
        {
            throw new ArgumentException("The provided CSV stream is null.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(csvStream)))
        {
            throw new ObjectDisposedException(nameof(csvStream), "The CSV stream has been disposed before reading.");
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The provided stream does not support reading.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024891)) // ERROR_ACCESS_DENIED
        {
            throw new IOException("Access to the stream is denied.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864)) // ERROR_SHARING_VIOLATION
        {
            throw new IOException("The stream is being used by another process.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while reading the CSV stream.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new System.Text.DecoderFallbackException("The encoding used for the stream could not properly decode the content.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV stream format is invalid.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("CSV parsing resulted in unexpected column misalignment.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV to XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV stream.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this Stream csvStream, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(csvStream);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new StreamReader(csvStream, encoding);
            List<string> lines = new();
            
            while (!reader.EndOfStream)
                lines.Add(reader.ReadLine());

            return System.Xml.Linq.XDocument.Parse(ConvertToXml(lines.ToArray()));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvStream)))
        {
            throw new ArgumentException("The provided CSV stream is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(encoding)))
        {
            throw new ArgumentException("The provided encoding is null.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(csvStream)))
        {
            throw new ObjectDisposedException(nameof(csvStream), "The CSV stream has been disposed before reading.");
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The provided stream does not support reading.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024891))
        {
            throw new IOException("Access to the stream is denied.", ex);
        }
        catch (IOException ex) when (ex.HResult.Equals(-2147024864))
        {
            // ERROR_SHARING_VIOLATION
            throw new IOException("The stream is being used by another process.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while reading the CSV stream.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new System.Text.DecoderFallbackException("The encoding used for the stream could not properly decode the content.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV stream format is invalid.", ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException("CSV parsing resulted in unexpected column misalignment.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV to XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV stream.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this String[] csvLines)
    {
        ArgumentNullException.ThrowIfNull(csvLines);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Parse(ConvertToXml(csvLines));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvLines)))
        {
            throw new ArgumentException("The provided CSV lines are null.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV data format is invalid.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV data to XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV data.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this String[] csvLines, Char delimiter)
    {
        ArgumentNullException.ThrowIfNull(csvLines);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Parse(ConvertToXml(csvLines, delimiter));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvLines)))
        {
            throw new ArgumentException("The provided CSV lines are null.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV data format is invalid. Please check the delimiter or the data structure.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV data to XML. The XML generation failed.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV data with the given delimiter.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Equals("Input string was not in a correct format"))
        {
            throw new ArgumentException("There was an issue with the format of the CSV data or delimiter.", ex);
        }
        catch (OverflowException ex)
        {
            throw new OverflowException("An overflow error occurred while processing CSV data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV data.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this List<String> csvLines)
    {
        ArgumentNullException.ThrowIfNull(csvLines);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Parse(ConvertToXml(csvLines.ToArray()));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvLines)))
        {
            throw new ArgumentException("The provided CSV lines list is null.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV data format is invalid. Ensure the data is properly structured.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV data to XML. Please check the XML generation logic.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV data.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Equals("Input string was not in a correct format"))
        {
            throw new ArgumentException("There was an issue with the format of the CSV data.", ex);
        }
        catch (OverflowException ex)
        {
            throw new OverflowException("An overflow error occurred while processing the CSV data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV data.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromCsvFile(this List<String> csvLines, Char delimiter)
    {
        ArgumentNullException.ThrowIfNull(csvLines);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Xml.Linq.XDocument.Parse(ConvertToXml(csvLines.ToArray(), delimiter));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(csvLines)))
        {
            throw new ArgumentException("The provided CSV lines list is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(delimiter)))
        {
            throw new ArgumentException("The provided delimiter is null.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The CSV data format is invalid. Please check the format of the data.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while converting CSV data to XML. Please ensure the XML generation logic is correct.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the CSV data.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Equals("Input string was not in a correct format"))
        {
            throw new ArgumentException("There was an issue with the format of the CSV data.", ex);
        }
        catch (OverflowException ex)
        {
            throw new OverflowException("An overflow error occurred while processing the CSV data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while handling the CSV data.", ex);
        }
    }
}