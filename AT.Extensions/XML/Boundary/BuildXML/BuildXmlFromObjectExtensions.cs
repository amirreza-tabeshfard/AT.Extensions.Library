using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromObjectExtensions
{
    private static XDocument ConvertToXDocument(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference is null.");

            if (input is XDocument xd)
                return xd;

            if (input is XElement xe)
                return new XDocument(xe);

            if (input is XmlDocument xmlDoc)
                return XDocument.Parse(xmlDoc.OuterXml);

            if (input is String str)
            {
                if (String.IsNullOrWhiteSpace(str))
                    throw new InvalidOperationException("Provided String is empty or whitespace.");

                return XDocument.Parse(str);
            }

            if (input is Byte[] bytes)
                return XDocument.Parse(Encoding.UTF8.GetString(bytes));

            if (input is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Stream is not readable.");

                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                var content = reader.ReadToEnd();

                if (String.IsNullOrWhiteSpace(content))
                    throw new InvalidOperationException("Stream produced empty content.");

                return XDocument.Parse(content);
            }

            if (input is TextReader textReader)
            {
                var content = textReader.ReadToEnd();

                if (String.IsNullOrWhiteSpace(content))
                    throw new InvalidOperationException("TextReader produced empty content.");

                return XDocument.Parse(content);
            }

            if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("Target file does not exist.", fileInfo.FullName);

                var content = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);

                if (String.IsNullOrWhiteSpace(content))
                    throw new InvalidOperationException("File contains no readable XML.");

                return XDocument.Parse(content);
            }

            if (input is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new InvalidOperationException("Uri must be absolute.");

                var content = File.ReadAllText(uri.LocalPath, Encoding.UTF8);

                if (String.IsNullOrWhiteSpace(content))
                    throw new InvalidOperationException("Uri resolved to empty content.");

                return XDocument.Parse(content);
            }

            if (input is DataSet dataSet)
            {
                using var writer = new StringWriter();
                dataSet.WriteXml(writer);
                var xml = writer.ToString();

                if (String.IsNullOrWhiteSpace(xml))
                    throw new InvalidOperationException("DataSet serialization produced empty XML.");

                return XDocument.Parse(xml);
            }

            if (input is DataTable dataTable)
            {
                using var writer = new StringWriter();
                dataTable.WriteXml(writer, XmlWriteMode.WriteSchema);
                var xml = writer.ToString();

                if (String.IsNullOrWhiteSpace(xml))
                    throw new InvalidOperationException("DataTable serialization produced empty XML.");

                return XDocument.Parse(xml);
            }

            if (input is NameValueCollection nvc)
            {
                var root = new XElement("NameValueCollection");

                foreach (var key in nvc.AllKeys)
                    if (key != null)
                        root.Add(new XElement(key, nvc[key]));

                return new XDocument(root);
            }

            if (input is System.Collections.IEnumerable enumerable)
            {
                var root = new XElement("Enumerable");

                foreach (var item in enumerable)
                    if (item != null)
                        root.Add(new XElement("Item", item.ToString()));

                return new XDocument(root);
            }

            var fallback = new XElement("Object", input.ToString());
            return new XDocument(fallback);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Invalid argument detected for input object.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input object reference was null.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("Data layer failed while serializing dataset or datatable.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("UTF8 decoding failed while converting byte array to string.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Target directory was not found while resolving file or uri.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Target file was not found during XML loading.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML content format is invalid.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Stream or file contains invalid data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML parsing failed due to invalid operation.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("I/O failure occurred while reading stream or file.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Provided input type is not supported for XML conversion.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Attempted to read from a disposed stream or reader.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("File path exceeds system supported length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access denied while reading file or uri resource.", ex);
        }
        catch (UriFormatException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Provided Uri format is invalid.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML content detected.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unhandled failure occurred while building XML from object.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromObjectExtensions
{
    public static XDocument BuildXmlFromObject(this Object input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this String input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this XmlDocument input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this XDocument input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this XElement input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this Stream input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this MemoryStream input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this TextReader input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this FileInfo input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this Uri input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this DataSet input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this DataTable input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this Byte[] input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this NameValueCollection input)
    {
        return ConvertToXDocument(input);
    }

    public static XDocument BuildXmlFromObject(this System.Collections.IEnumerable input)
    {
        return ConvertToXDocument(input);
    }
}