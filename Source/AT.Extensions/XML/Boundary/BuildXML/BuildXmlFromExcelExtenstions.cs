using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromExcelExtenstions
{
    private static XDocument Synchronize(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            if (source is String path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Excel file path does not exist.", path);

                using var reader = new StreamReader(path, Encoding.UTF8, true);
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("Excel FileInfo does not exist.", fileInfo.FullName);

                using var reader = fileInfo.OpenText();
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is MemoryStream ms)
            {
                ms.Position = 0;
                using var reader = new StreamReader(ms, Encoding.UTF8, true, leaveOpen: true);
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Provided stream is not readable.");

                using var reader = new StreamReader(stream, Encoding.UTF8, true, leaveOpen: true);
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is Byte[] bytes)
            {
                using var localStream = new MemoryStream(bytes);
                using var reader = new StreamReader(localStream, Encoding.UTF8, true);
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is DataTable table)
                return BuildFromDataTable(table);

            if (source is DataSet dataSet)
            {
                if (dataSet.Tables.Count == 0)
                    throw new InvalidOperationException("DataSet does not contain any DataTable.");

                return BuildFromDataTable(dataSet.Tables[0]);
            }

            if (source is TextReader textReader)
                return BuildFromCsvLines(ReadAllLines(textReader));

            if (source is StreamReader streamReader)
                return BuildFromCsvLines(ReadAllLines(streamReader));

            if (source is BinaryReader binaryReader)
            {
                var bytesLocal = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
                using var localStream = new MemoryStream(bytesLocal);
                using var reader = new StreamReader(localStream, Encoding.UTF8, true);
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is IReadOnlyList<String> roLines)
                return BuildFromCsvLines(roLines);

            if (source is List<String> lines)
                return BuildFromCsvLines(lines);

            if (source is StringBuilder sb)
            {
                using var reader = new StringReader(sb.ToString());
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            if (source is XmlDocument xmlDocument)
                return XDocument.Parse(xmlDocument.OuterXml);

            if (source is DirectoryInfo directory)
            {
                if (!directory.Exists)
                    throw new DirectoryNotFoundException("Provided directory does not exist.");

                var firstFile = directory.GetFiles("*.csv").FirstOrDefault() ?? throw new InvalidOperationException("Directory does not contain any CSV files to simulate Excel input.");
                using var reader = firstFile.OpenText();
                return BuildFromCsvLines(ReadAllLines(reader));
            }

            throw new NotSupportedException($"Unsupported Excel boundary type: {source.GetType().FullName}");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Invalid argument provided for Excel synchronization boundary.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Excel input source was null during synchronization.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Specified directory boundary could not be located while resolving Excel input.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Excel file boundary was not found on disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Low-level I/O failure occurred while reading Excel boundary.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Invalid operation detected during Excel to XML synchronization pipeline.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Provided Excel boundary type is not supported by the synchronization engine.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A disposed stream or reader was accessed while processing Excel input.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Access to Excel file or directory boundary was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Malformed XML content detected while normalizing Excel boundary.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unhandled failure occurred during BuildXmlFromExcel synchronization stage.", ex);
        }
    }

    private static IReadOnlyList<String> ReadAllLines(TextReader reader)
    {
        var result = new List<String>();
        String? line;

        while ((line = reader.ReadLine()) != null)
        {
            if (!String.IsNullOrWhiteSpace(line))
                result.Add(line);
        }

        if (result.Count == 0)
            throw new InvalidOperationException("Input does not contain any readable rows.");

        return result;
    }

    private static XDocument BuildFromCsvLines(IReadOnlyList<String> lines)
    {
        var headers = lines[0].Split(',');

        if (headers.Length == 0)
            throw new InvalidOperationException("CSV header row is empty.");

        var root = new XElement("Excel");

        for (int i = 1; i < lines.Count; i++)
        {
            var values = lines[i].Split(',');
            var row = new XElement("Row");

            for (int c = 0; c < headers.Length; c++)
            {
                var columnName = headers[c].Trim();
                var value = c < values.Length ? values[c].Trim() : String.Empty;

                row.Add(new XElement(columnName, value));
            }

            root.Add(row);
        }

        return new XDocument(root);
    }

    private static XDocument BuildFromDataTable(DataTable table)
    {
        if (table.Columns.Count == 0)
            throw new InvalidOperationException("DataTable does not contain any columns.");

        var root = new XElement("Excel");

        foreach (DataRow dataRow in table.Rows)
        {
            var row = new XElement("Row");

            foreach (DataColumn column in table.Columns)
                row.Add(new XElement(column.ColumnName, dataRow[column]?.ToString() ?? String.Empty));

            root.Add(row);
        }

        return new XDocument(root);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromExcelExtenstions
{
    public static XDocument BuildXmlFromExcel(this String excelPath)
    {
        return Synchronize(excelPath);
    }

    public static XDocument BuildXmlFromExcel(this FileInfo excelFile)
    {
        return Synchronize(excelFile);
    }

    public static XDocument BuildXmlFromExcel(this Stream excelStream)
    {
        return Synchronize(excelStream);
    }

    public static XDocument BuildXmlFromExcel(this MemoryStream excelMemoryStream)
    {
        return Synchronize(excelMemoryStream);
    }

    public static XDocument BuildXmlFromExcel(this Byte[] excelBytes)
    {
        return Synchronize(excelBytes);
    }

    public static XDocument BuildXmlFromExcel(this DataTable table)
    {
        return Synchronize(table);
    }

    public static XDocument BuildXmlFromExcel(this DataSet dataSet)
    {
        return Synchronize(dataSet);
    }

    public static XDocument BuildXmlFromExcel(this TextReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromExcel(this StreamReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromExcel(this BinaryReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromExcel(this IReadOnlyList<String> csvLines)
    {
        return Synchronize(csvLines);
    }

    public static XDocument BuildXmlFromExcel(this List<String> csvLines)
    {
        return Synchronize(csvLines);
    }

    public static XDocument BuildXmlFromExcel(this StringBuilder csvBuilder)
    {
        return Synchronize(csvBuilder);
    }

    public static XDocument BuildXmlFromExcel(this XmlDocument xmlSeed)
    {
        return Synchronize(xmlSeed);
    }

    public static XDocument BuildXmlFromExcel(this DirectoryInfo directory)
    {
        return Synchronize(directory);
    }
}