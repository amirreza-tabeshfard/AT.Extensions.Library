using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromDynamicObjectExtenstions
{
    private static XDocument CoreConvert(Object source)
    {
        try
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            if (source is XDocument xd)
                return new XDocument(xd);

            if (source is XElement xe)
                return new XDocument(xe);

            if (source is XmlDocument xml)
                return XDocument.Parse(xml.OuterXml);

            if (source is JsonDocument json)
            {
                var root = new XElement("Root");
                BuildFromJson(json.RootElement, root);
                return new XDocument(root);
            }

            if (source is NameValueCollection nvc)
            {
                var root = new XElement("Root");

                foreach (String key in nvc.AllKeys)
                    root.Add(new XElement(key ?? "Item", nvc[key]));

                return new XDocument(root);
            }

            if (source is DataTable table)
                return ConvertTable(table);

            if (source is DataSet ds)
            {
                var root = new XElement("Root");

                foreach (DataTable dt in ds.Tables)
                    root.Add(ConvertTable(dt).Root);

                return new XDocument(root);
            }

            if (source is Stream stream)
            {
                using var reader = new StreamReader(stream, leaveOpen: true);
                return CoreConvert(reader.ReadToEnd());
            }

            if (source is TextReader tr)
                return CoreConvert(tr.ReadToEnd());

            if (source is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("Target file does not exist.", fi.FullName);

                return CoreConvert(File.ReadAllText(fi.FullName));
            }

            if (source is Uri uri)
                return CoreConvert(File.ReadAllText(uri.LocalPath));

            if (source is Hashtable ht)
            {
                var root = new XElement("Root");

                foreach (DictionaryEntry entry in ht)
                    root.Add(BuildElement(entry.Key.ToString(), entry.Value));

                return new XDocument(root);
            }

            if (source is OrderedDictionary od)
            {
                var root = new XElement("Root");

                foreach (DictionaryEntry entry in od)
                    root.Add(BuildElement(entry.Key.ToString(), entry.Value));

                return new XDocument(root);
            }

            if (source is IEnumerable seq && source is not String)
            {
                var root = new XElement("Root");

                foreach (var item in seq)
                    root.Add(BuildElement("Item", item));

                return new XDocument(root);
            }

            return ConvertByReflection(source);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The input parameter 'source' was null. A reference type instance is required.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("A data-layer error occurred while converting DataTable or DataSet to XML.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The target directory could not be located while reading file-based input.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified file was not found during XML construction.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the dynamic object graph.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("An I/O failure occurred while reading Stream, TextReader, FileInfo, or Uri input.", ex);
        }
        catch (JsonException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Json"))
        {
            throw new InvalidOperationException("JSON parsing failed. The provided JsonDocument contains invalid or unsupported structure.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("An unsupported operation was detected while enumerating or reflecting over the dynamic object.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A disposed Stream or TextReader instance was accessed during XML generation.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The file path exceeds the system-defined maximum length.", ex);
        }
        catch (ReflectionTypeLoadException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("One or more reflected runtime types failed to load while inspecting the dynamic object.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Security"))
        {
            throw new InvalidOperationException("Security restrictions prevented access to file system or reflected members.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the specified file or resource was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.Xml"))
        {
            throw new InvalidOperationException("Invalid XML content was detected while parsing XmlDocument or XElement.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected failure occurred while building XML from the dynamic object.", ex);
        }
    }

    private static XDocument ConvertTable(DataTable table)
    {
        var root = new XElement(table.TableName ?? "Table");

        foreach (DataRow row in table.Rows)
        {
            var rowEl = new XElement("Row");

            foreach (DataColumn col in table.Columns)
                rowEl.Add(new XElement(col.ColumnName, row[col]));

            root.Add(rowEl);
        }

        return new XDocument(root);
    }

    private static XDocument ConvertByReflection(Object obj)
    {
        var root = new XElement("Root");
        var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (props.Length == 0)
        {
            root.Value = obj.ToString();
            return new XDocument(root);
        }

        foreach (var p in props)
            root.Add(BuildElement(p.Name, p.GetValue(obj)));

        return new XDocument(root);
    }

    private static XElement BuildElement(String name, Object value)
    {
        if (value == null)
            return new XElement(name);

        if (value is IEnumerable seq && value is not String)
        {
            var el = new XElement(name);

            foreach (var item in seq)
                el.Add(BuildElement("Item", item));

            return el;
        }

        var props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (props.Length > 0 && !value.GetType().IsPrimitive && value is not String)
        {
            var el = new XElement(name);

            foreach (var p in props)
                el.Add(BuildElement(p.Name, p.GetValue(value)));

            return el;
        }

        return new XElement(name, value);
    }

    private static void BuildFromJson(JsonElement element, XElement parent)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var p in element.EnumerateObject())
            {
                var child = new XElement(p.Name);
                BuildFromJson(p.Value, child);
                parent.Add(child);
            }

            return;
        }

        if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                var child = new XElement("Item");
                BuildFromJson(item, child);
                parent.Add(child);
            }

            return;
        }

        parent.Value = element.ToString();
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromDynamicObjectExtenstions
{
    public static XDocument BuildXmlFromDynamicObject(this Object source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this ExpandoObject source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this NameValueCollection source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this DataTable source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this DataSet source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this XmlDocument source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this XDocument source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this XElement source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this JsonDocument source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this Stream source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this TextReader source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this FileInfo source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this Uri source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this Hashtable source)
    {
        return CoreConvert(source);
    }

    public static XDocument BuildXmlFromDynamicObject(this OrderedDictionary source)
    {
        return CoreConvert(source);
    }
}