using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementFromXmlStringExtensions
{
    private static XmlElement SafeParse<T>(T input, Func<T, StringReader> readerFactory)
    {
        try
        {
            using StringReader reader = readerFactory(input);
            XmlDocument doc = new();
            doc.Load(reader);

            if (doc.DocumentElement == null)
                throw new XmlException("The XML document was loaded successfully, but it does not contain a valid root element. This may indicate that the input XML is empty, improperly formatted, or missing the root-level tag. Please ensure that the XML input has a single well-formed root element.");

            return doc.DocumentElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("reader"))
        {
            throw new InvalidOperationException("An argument exception occurred while loading the XML reader.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("s"))
        {
            throw new InvalidOperationException("The input string for the XML content is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred within the System.Xml namespace.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A not supported operation was attempted while parsing XML.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("StringReader"))
        {
            throw new InvalidOperationException("The StringReader object was disposed before XML parsing could complete.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An I/O error occurred while loading the XML content.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML parsing error occurred while trying to load the document.", ex);
        }
        catch (XPathException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XPath-related error occurred during XML parsing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XmlElement from input of type {typeof(T).Name}.", ex);
        }
    }

    private static XmlElement ConvertToXmlElement<T>(T value, Func<T, String> converter, String elementName)
    {
        try
        {
            String content = converter(value);
            XmlDocument xmlDoc = new();
            XmlElement element = xmlDoc.CreateElement(elementName);
            element.InnerText = content;
            return element;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The specified element name is invalid for creating an XML element.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name provided is null. A valid name must be specified to create an XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred while creating or configuring the XML element.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The operation attempted is not supported by the current XML implementation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference occurred while accessing the XML document or element structure.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The content produced by the converter function is in an invalid format and could not be assigned to the XML element.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML-specific error occurred while creating or manipulating the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to convert value of type '{typeof(T).Name}' to XmlElement.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 13 )
/// </summary>
public static partial class BuildXmlElementFromXmlStringExtensions
{
    public static XmlElement BuildXmlElementFromXmlString(this Object obj)
    {
        return SafeParse(obj, x => new StringReader(x.ToString() ?? String.Empty));
    }

    public static XmlElement BuildXmlElementFromXmlString(this String xml)
    {
        return SafeParse(xml, x => new StringReader(x));
    }

    public static XmlElement BuildXmlElementFromXmlString(this StringBuilder sb)
    {
        return SafeParse(sb, x => new StringReader(x.ToString()));
    }

    public static XmlElement BuildXmlElementFromXmlString(this XDocument doc)
    {
        return SafeParse(doc, x => new StringReader(x.ToString()));
    }

    public static XmlElement BuildXmlElementFromXmlString(this XmlDocument doc)
    {
        return SafeParse(doc, x => new StringReader(x.OuterXml));
    }

    public static XmlElement BuildXmlElementFromXmlString(this XmlNode node)
    {
        return SafeParse(node, x => new StringReader(x.OuterXml));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Stream stream)
    {
        return SafeParse(stream, x =>
        {
            if (!x.CanRead)
                throw new InvalidOperationException("Stream is not readable.");
            // ----------------------------------------------------------------------------------------------------
            using StreamReader reader = new(x, System.Text.Encoding.UTF8);
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(reader.ReadToEnd());
        });
    }

    public static XmlElement BuildXmlElementFromXmlString(this MemoryStream ms)
    {
        return SafeParse(ms, x =>
        {
            x.Position = 0;
            using StreamReader reader = new(x, System.Text.Encoding.UTF8);
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(reader.ReadToEnd());
        });
    }

    public static XmlElement BuildXmlElementFromXmlString(this TextReader reader)
    {
        return SafeParse(reader, x =>
        {
            String content = x.ReadToEnd();
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(content);
        });
    }

    public static XmlElement BuildXmlElementFromXmlString(this FileInfo file)
    {
        return SafeParse(file, x =>
        {
            if (!x.Exists)
                throw new FileNotFoundException("File not found.", x.FullName);
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(File.ReadAllText(x.FullName));
        });
    }

    public static XmlElement BuildXmlElementFromXmlString(this Uri uri)
    {
        return SafeParse(uri, x =>
        {
            if (!x.IsAbsoluteUri || String.IsNullOrWhiteSpace(x.AbsoluteUri))
                throw new ArgumentException("Invalid URI for XML input.");
            // ----------------------------------------------------------------------------------------------------
            using StreamReader wc = new StreamReader(x.AbsoluteUri);
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(wc.ReadToEnd());
        });
    }

    public static XmlElement BuildXmlElementFromXmlString(this XElement xElem)
    {
        return SafeParse(xElem, x => new StringReader(x.ToString()));
    }

    public static XmlElement BuildXmlElementFromXmlString(this XmlReader xmlReader)
    {
        return SafeParse(xmlReader, x =>
        {
            XmlDocument doc = new();
            doc.Load(x);
            // ----------------------------------------------------------------------------------------------------
            return new StringReader(doc.OuterXml);
        });
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementFromXmlStringExtensions
{
    public static XmlElement BuildXmlElementFromXmlString(this Byte value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Byte));
    }
    
    public static XmlElement BuildXmlElementFromXmlString(this SByte value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(SByte));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Int16 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Int16));
    }

    public static XmlElement BuildXmlElementFromXmlString(this UInt16 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(UInt16));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Int32 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Int32));
    }

    public static XmlElement BuildXmlElementFromXmlString(this UInt32 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(UInt32));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Int64 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Int64));
    }

    public static XmlElement BuildXmlElementFromXmlString(this UInt64 value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Int64));
    }

    public static XmlElement BuildXmlElementFromXmlString(this IntPtr value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(IntPtr));
    }

    public static XmlElement BuildXmlElementFromXmlString(this UIntPtr value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(UIntPtr));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Single value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Single));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Double value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Double));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Decimal value)
    {
        return ConvertToXmlElement(value, x => x.ToString(System.Globalization.CultureInfo.InvariantCulture), nameof(Decimal));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Boolean value)
    {
        return ConvertToXmlElement(value, x => x.ToString().ToLower(), nameof(Boolean));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Char value)
    {
        return ConvertToXmlElement(value, x => XmlConvert.EncodeName(x.ToString()), nameof(Char));
    }

    public static XmlElement BuildXmlElementFromXmlString(this DateTime value)
    {
        return ConvertToXmlElement(value, x => x.ToString("o", System.Globalization.CultureInfo.InvariantCulture), nameof(DateTime));
    }

    public static XmlElement BuildXmlElementFromXmlString(this TimeSpan value)
    {
        return ConvertToXmlElement(value, x => x.ToString("c", System.Globalization.CultureInfo.InvariantCulture), nameof(TimeSpan));
    }

    public static XmlElement BuildXmlElementFromXmlString(this Guid value)
    {
        return ConvertToXmlElement(value, x => x.ToString(), nameof(Guid));
    }

    public static XmlElement BuildXmlElementFromXmlString(this DayOfWeek value)
    {
        return ConvertToXmlElement(value, x => x.ToString(), nameof(DayOfWeek));
    }
}