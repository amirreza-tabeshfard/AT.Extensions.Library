using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromMultipleDictionariesExtenstions
{
    private static String ConvertToXmlInternal(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference is null.");

            var document = new XDocument();
            var root = new XElement("MultipleDictionaries");
            document.Add(root);

            if (input is IDictionary<String, String>)
            {
                var single = input as IDictionary<String, String>;
                var container = new XElement("Dictionary");

                foreach (var kv in single)
                    container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", kv.Value ?? String.Empty)));

                root.Add(container);
            }
            else if (input is IDictionary<String, Object>)
            {
                var singleObj = input as IDictionary<String, Object>;
                var container = new XElement("Dictionary");

                foreach (var kv in singleObj)
                {
                    var value = kv.Value != null ? kv.Value.ToString() : String.Empty;
                    container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", value)));
                }

                root.Add(container);
            }
            else if (input is IEnumerable<Dictionary<String, String>>)
            {
                var list = input as IEnumerable<Dictionary<String, String>>;
                foreach (var dict in list)
                {
                    var container = new XElement("Dictionary");

                    foreach (var kv in dict)
                        container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", kv.Value ?? String.Empty)));

                    root.Add(container);
                }
            }
            else if (input is IEnumerable<IDictionary<String, String>>)
            {
                var list = input as IEnumerable<IDictionary<String, String>>;
                foreach (var dict in list)
                {
                    var container = new XElement("Dictionary");

                    foreach (var kv in dict)
                        container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", kv.Value ?? String.Empty)));

                    root.Add(container);
                }
            }
            else if (input is IDictionary<String, Dictionary<String, String>>)
            {
                var nested = input as IDictionary<String, Dictionary<String, String>>;
                foreach (var outer in nested)
                {
                    var container = new XElement("Dictionary", new XAttribute("Name", outer.Key));

                    foreach (var kv in outer.Value)
                        container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", kv.Value ?? String.Empty)));

                    root.Add(container);
                }
            }
            else if (input is IDictionary<String, IDictionary<String, String>>)
            {
                var nested = input as IDictionary<String, IDictionary<String, String>>;
                foreach (var outer in nested)
                {
                    var container = new XElement("Dictionary", new XAttribute("Name", outer.Key));

                    foreach (var kv in outer.Value)
                        container.Add(new XElement("Item", new XAttribute("Key", kv.Key), new XAttribute("Value", kv.Value ?? String.Empty)));

                    root.Add(container);
                }
            }
            else
            {
                throw new NotSupportedException("The provided reference type is not supported by BuildXmlFromMultipleDictionaries.");
            }

            return document.ToString();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Invalid argument provided. The input reference contains malformed dictionary data.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input reference is null. XML synchronization cannot proceed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Invalid XML state detected while building document structure.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Unsupported reference type supplied. Multiple dictionary synchronization failed.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A null reference was encountered while iterating dictionary elements.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.ReaderWriter"))
        {
            throw new InvalidOperationException("XML generation failed due to invalid element or attribute construction.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML synchronization failed due to an unexpected runtime error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromMultipleDictionariesExtenstions
{
    public static String BuildXmlFromMultipleDictionaries(this Dictionary<String, String> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IDictionary<String, String> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this Dictionary<String, Object> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IDictionary<String, Object> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this List<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IEnumerable<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this ICollection<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IReadOnlyCollection<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this Dictionary<String, Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IDictionary<String, IDictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this List<IDictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this IEnumerable<IDictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this Queue<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this Stack<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }

    public static String BuildXmlFromMultipleDictionaries(this HashSet<Dictionary<String, String>> input)
    {
        return ConvertToXmlInternal(input);
    }
}