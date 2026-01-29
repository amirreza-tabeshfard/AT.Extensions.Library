using System.Globalization;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementExtensions
{
    private static XElement CreateElement(String elementName, Object value)
    {
        try
        {
            return new XElement(elementName, value ?? String.Empty);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new ArgumentException("Invalid XML element name provided.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new ArgumentNullException("XML element name cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Failed to create XElement due to invalid operation in XML context.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NotSupportedException("The provided value type is not supported by XElement.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ObjectDisposedException("An object used in XML creation has already been disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new OutOfMemoryException("Insufficient memory while creating XElement.", ex);
        }
        catch (SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new SecurityException("Security restrictions prevented XML element creation.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("An error occurred while creating XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while creating XElement.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlElementExtensions
   
{
    public static XElement BuildXmlElement(this Object value)
    {
        return CreateElement("ObjectElement", value.ToString());
    }

    public static XElement BuildXmlElement(this String value)
    {
        return CreateElement("StringElement", value);
    }

    public static XElement BuildXmlElement(this StringBuilder value)
    {
        return CreateElement("StringBuilderElement", value);
    }

    public static XElement BuildXmlElement(this Uri value)
    {
        return CreateElement("UriElement", value.AbsoluteUri);
    }

    public static XElement BuildXmlElement(this DateTime? value)
    {
        return CreateElement("DateTimeElement", value.Value.ToString("o"));
    }

    public static XElement BuildXmlElement(this XElement value)
    {
        return CreateElement("XElementElement", value.ToString());
    }

    public static XElement BuildXmlElement(this XDocument value)
    {
        return CreateElement("XDocumentElement", value.Root);
    }

    public static XElement BuildXmlElement(this Exception value)
    {
        return CreateElement("Message", value.Message);
    }

    public static XElement BuildXmlElement(this Type value)
    {
        return CreateElement("TypeElement", value.FullName);
    }

    public static XElement BuildXmlElement(this MemoryStream value)
    {
        value.Position = 0;
        using var reader = new StreamReader(value);
        String content = reader.ReadToEnd();
        return CreateElement("MemoryStreamElement", content);
    }

    public static XElement BuildXmlElement(this StreamReader value)
    {
        return CreateElement("StreamReaderElement", value.ReadToEnd());
    }

    public static XElement BuildXmlElement(this CultureInfo value)
    {
        return CreateElement("CultureElement", value.Name);
    }

    public static XElement BuildXmlElement(this FileInfo value)
    {
        return CreateElement("FileInfoElement", value.FullName);
    }

    public static XElement BuildXmlElement(this DirectoryInfo value)
    {
        return CreateElement("DirectoryInfoElement", value.FullName);
    }

    public static XElement BuildXmlElement(this Version value)
    {
        return CreateElement("VersionElement", value.ToString());
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementExtensions
{
    public static XElement BuildXmlElement(this Byte value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this SByte value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Int16 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this UInt16 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Int32 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this UInt32 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Int64 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this UInt64 value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this IntPtr value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this UIntPtr value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Single value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Double value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Decimal value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Boolean value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Char value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this DateTime value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this TimeSpan value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this Guid value)
    {
        return CreateElement("Boundary", value);
    }

    public static XElement BuildXmlElement(this DayOfWeek value)
    {
        return CreateElement("Boundary", value);
    }
}