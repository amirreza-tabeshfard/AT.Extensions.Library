using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromMultipleObjectsExtenstions
{
    private static XDocument ConvertInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            var root = new XElement("Root");

            if (input is XDocument)
            {
                var doc = input as XDocument;
                return new XDocument(doc);
            }

            if (input is XElement)
            {
                var el = input as XElement;
                root.Add(new XElement(el));
                return new XDocument(root);
            }

            if (input is XmlDocument)
            {
                var xml = input as XmlDocument;
                var xdoc = XDocument.Parse(xml.OuterXml);
                return xdoc;
            }

            if (input is DataSet)
            {
                var ds = input as DataSet;
                
                foreach (DataTable table in ds.Tables)
                    AppendDataTable(root, table);

                return new XDocument(root);
            }

            if (input is DataTable)
            {
                var dt = input as DataTable;
                AppendDataTable(root, dt);
                return new XDocument(root);
            }

            if (input is NameValueCollection)
            {
                var nvc = input as NameValueCollection;
                
                foreach (var key in nvc.AllKeys)
                    root.Add(new XElement(key ?? "Key", nvc[key] ?? String.Empty));

                return new XDocument(root);
            }

            if (input is IDictionary)
            {
                var dict = input as IDictionary;
                
                foreach (DictionaryEntry entry in dict)
                    root.Add(new XElement(entry.Key?.ToString() ?? "Key", entry.Value?.ToString() ?? String.Empty));

                return new XDocument(root);
            }

            if (input is IEnumerable)
            {
                var enumerable = input as IEnumerable;

                foreach (var item in enumerable)
                {
                    if (item == null)
                        continue;

                    var itemElement = new XElement(item.GetType().Name);
                    var props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var prop in props)
                    {
                        if (!prop.CanRead)
                            continue;

                        var value = prop.GetValue(item);
                        itemElement.Add(new XElement(prop.Name, value ?? String.Empty));
                    }

                    root.Add(itemElement);
                }

                return new XDocument(root);
            }

            var single = new XElement(input.GetType().Name);
            var properties = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in properties)
            {
                if (!p.CanRead)
                    continue;

                var v = p.GetValue(input);
                single.Add(new XElement(p.Name, v ?? String.Empty));
            }

            root.Add(single);
            return new XDocument(root);
        }
        catch (AmbiguousMatchException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("Multiple matching members were found during reflection. Ensure object properties are uniquely defined.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.Source is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Invalid argument detected. The provided input reference is not compatible with the XML conversion pipeline.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.Source is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The input parameter is null. A non-null reference type is required to build XML.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("A data-layer failure occurred while processing DataTable or DataSet. Verify table schema and row values.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid XML format detected while parsing XmlDocument content.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Type casting failed. One or more objects cannot be converted to the expected reference types.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Linq"))
        {
            throw new InvalidOperationException("Invalid operation occurred during XML materialization. Enumeration state may be corrupted.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections"))
        {
            throw new InvalidOperationException("A required dictionary key was not found while building XML nodes.", ex);
        }
        catch (MemberAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("Reflection failed due to inaccessible members. Ensure all properties are public and readable.", ex);
        }
        catch (MethodAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("Method access violation encountered during property inspection.", ex);
        }
        catch (MissingMethodException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("A required accessor method was not found while reading object properties.", ex);
        }
        catch (TargetInvocationException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("An exception was thrown by a property getter during reflection-based extraction.", ex);
        }
        catch (TargetParameterCountException ex) when (ex.Source is not null && ex.Source.Equals("System.Reflection"))
        {
            throw new InvalidOperationException("Unexpected parameter count detected while invoking reflected members.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML processing failed. The generated structure is not well-formed.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from provided multiple objects. Verify that all input objects are reference types and contain readable public properties.", ex);
        }
    }

    private static void AppendDataTable(XElement root, DataTable table)
    {
        var tableElement = new XElement(table.TableName);

        foreach (DataRow row in table.Rows)
        {
            var rowElement = new XElement("Row");

            foreach (DataColumn col in table.Columns)
                rowElement.Add(new XElement(col.ColumnName, row[col] ?? String.Empty));

            tableElement.Add(rowElement);
        }

        root.Add(tableElement);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromMultipleObjectsExtenstions
{
    public static XDocument BuildXmlFromMultipleObjects(this Object[] input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this List<Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this IList<Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this IEnumerable<Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this Dictionary<String, Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this DataTable input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this DataSet input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this XmlDocument input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this XDocument input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this XElement input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this NameValueCollection input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this ArrayList input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this Queue<Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this Stack<Object> input)
    {
        return ConvertInternal(input);
    }

    public static XDocument BuildXmlFromMultipleObjects(this HashSet<Object> input)
    {
        return ConvertInternal(input);
    }
}