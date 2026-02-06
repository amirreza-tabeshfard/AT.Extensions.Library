using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromCsvFileExtenstions
{
    private static XDocument BuildXmlFromCsvInternal(Object input)
    {
        try
        {
            List<List<String>> csvData = new List<List<String>>();

            if (input is String path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"CSV file not found at path: {path}");
                csvData = ParseCsvLines(File.ReadAllLines(path));
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException($"CSV file not found: {fileInfo.FullName}");
                csvData = ParseCsvLines(File.ReadAllLines(fileInfo.FullName));
            }
            else if (input is Stream stream)
            {
                using var sr = new StreamReader(stream, leaveOpen: true);
                csvData = ParseCsvLines(ReadAllLines(sr));
            }
            else if (input is StreamReader srReader)
            {
                csvData = ParseCsvLines(ReadAllLines(srReader));
            }
            else if (input is TextReader textReader)
            {
                csvData = ParseCsvLines(ReadAllLines(textReader));
            }
            else if (input is IEnumerable<String> lines)
            {
                csvData = ParseCsvLines(lines);
            }
            else if (input is DataTable table)
            {
                foreach (DataRow row in table.Rows)
                {
                    var rowData = new List<String>();
                    foreach (var item in row.ItemArray)
                        rowData.Add(item?.ToString() ?? String.Empty);
                    csvData.Add(rowData);
                }
            }
            else if (input is List<List<String>> matrix)
            {
                csvData = matrix;
            }
            else if (input is MemoryStream ms)
            {
                using var sr = new StreamReader(ms, leaveOpen: true);
                csvData = ParseCsvLines(ReadAllLines(sr));
            }
            else if (input is StringBuilder sb)
            {
                csvData = ParseCsvLines(sb.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
            }
            else if (input is TextFieldParser parser)
            {
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    csvData.Add(new List<String>(fields));
                }
            }
            else
            {
                var str = input.ToString();
                if (String.IsNullOrWhiteSpace(str))
                    throw new ArgumentException("Input content is null or empty.");
                csvData = ParseCsvLines(str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
            }

            var xDoc = new XDocument();
            var root = new XElement("Root");
            foreach (var row in csvData)
            {
                var rowElement = new XElement("Row");
                for (int i = 0; i < row.Count; i++)
                    rowElement.Add(new XElement($"Col{i + 1}", row[i]));
                root.Add(rowElement);
            }
            xDoc.Add(root);
            return xDoc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Failed to build XML: The provided input argument is invalid.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException($"Failed to build XML: There was a data processing error while reading DataTable.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("path"))
        {
            throw new InvalidOperationException($"Failed to build XML: The specified directory path for the CSV file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Failed to build XML: The specified CSV file does not exist: {ex.FileName}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"Failed to build XML: An I/O error occurred while reading the CSV input.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Stream"))
        {
            throw new InvalidOperationException($"Failed to build XML: The provided Stream type is not supported for CSV input.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("conversion"))
        {
            throw new InvalidOperationException($"Failed to build XML: Numeric conversion in CSV caused an overflow.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("parsing"))
        {
            throw new InvalidOperationException($"Failed to build XML: CSV content has an invalid format that cannot be parsed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("TextFieldParser"))
        {
            throw new InvalidOperationException($"Failed to build XML: Error occurred while parsing CSV using TextFieldParser.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XML from CSV input due to an unexpected error: {ex.GetType().Name}", ex);
        }
    }

    private static List<List<String>> ParseCsvLines(IEnumerable<String> lines)
    {
        var result = new List<List<String>>();
        foreach (var line in lines)
        {
            var values = line.Split(',');
            result.Add(new List<String>(values));
        }
        return result;
    }

    private static IEnumerable<String> ReadAllLines(TextReader reader)
    {
        String? line;
        while ((line = reader.ReadLine()) is not null)
        {
            yield return line;
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 13 )
/// </summary>
public static partial class BuildXmlFromCsvFileExtenstions
{
    public static XDocument BuildXmlFromCsvFile(this String filePath)
    {
        return BuildXmlFromCsvInternal(filePath);
    }

    public static XDocument BuildXmlFromCsvFile(this Stream stream)
    {
        return BuildXmlFromCsvInternal(stream);
    }

    public static XDocument BuildXmlFromCsvFile(this TextReader reader)
    {
        return BuildXmlFromCsvInternal(reader);
    }

    public static XDocument BuildXmlFromCsvFile(this IEnumerable<String> lines)
    {
        return BuildXmlFromCsvInternal(lines);
    }

    public static XDocument BuildXmlFromCsvFile(this DataTable table)
    {
        return BuildXmlFromCsvInternal(table);
    }

    public static XDocument BuildXmlFromCsvFile(this List<List<String>> matrix)
    {
        return BuildXmlFromCsvInternal(matrix);
    }

    public static XDocument BuildXmlFromCsvFile(this FileInfo fileInfo)
    {
        return BuildXmlFromCsvInternal(fileInfo);
    }

    public static XDocument BuildXmlFromCsvFile(this MemoryStream memoryStream)
    {
        return BuildXmlFromCsvInternal(memoryStream);
    }

    public static XDocument BuildXmlFromCsvFile(this StringBuilder csvBuilder)
    {
        return BuildXmlFromCsvInternal(csvBuilder);
    }

    public static XDocument BuildXmlFromCsvFile(this StreamReader reader)
    {
        return BuildXmlFromCsvInternal(reader);
    }

    public static XDocument BuildXmlFromCsvFile(this List<String> lines)
    {
        return BuildXmlFromCsvInternal(lines);
    }

    public static XDocument BuildXmlFromCsvFile(this TextFieldParser parser)
    {
        return BuildXmlFromCsvInternal(parser);
    }

    public static XDocument BuildXmlFromCsvFile(this Object csvReader)
    {
        return BuildXmlFromCsvInternal(csvReader);
    }
}