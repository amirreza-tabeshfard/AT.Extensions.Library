using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementFromObjectExtensions
   
{
    private static XElement TryConvertToXElement(String elementName, Object value)
    {
        try
        {
            return new XElement(elementName, value);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(elementName))
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "ArgumentException: Invalid argument name."));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(elementName))
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "ArgumentNullException: Argument cannot be null."));
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "FormatException: Invalid format for XML element."));
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "InvalidOperationException: Operation is not valid in current state."));
        }
        catch (NullReferenceException)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "NullReferenceException: Attempted to use an object reference that is null."));
        }
        catch (OverflowException)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "OverflowException: Value caused an arithmetic overflow."));
        }
        catch (XmlException)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "XmlException: XML parsing or creation error occurred."));
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "FormatException: System.Xml format error."));
        }
        catch (InvalidCastException)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "InvalidCastException: Invalid cast encountered."));
        }
        catch (IOException)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", "IOException: Input/output error occurred."));
        }
        catch (Exception ex)
        {
            return new XElement("Error",
                new XAttribute("Type", elementName),
                new XAttribute("Message", ex.Message));
        }
    }

    private static XElement BuildXmlElementInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            return input switch
            {
                XElement xe => new XElement(xe),
                XmlDocument xd => XElement.Parse(xd.OuterXml),
                XmlElement xe => XElement.Parse(xe.OuterXml),
                XmlNode xn => XElement.Parse(xn.OuterXml),
                String xmlString => XElement.Parse(xmlString),
                XDocument xdoc => new XElement(xdoc.Root),
                _ => throw new NotSupportedException($"Type '{input.GetType()}' is not supported by BuildXmlElementFromObject."),
            };
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input argument cannot be null.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("is not supported by BuildXmlElementFromObject"))
        {
            throw new InvalidOperationException("Unsupported input type provided to BuildXmlElementInternal.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("XML parsing failed inside BuildXmlElementInternal.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Invalid XML format encountered during parsing.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error building XElement"))
        {
            throw new InvalidOperationException("An error occurred specifically during the creation of the XElement.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error occurred while building XElement from type '{input?.GetType().Name}'.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 16 )
/// </summary>
public static partial class BuildXmlElementFromObjectExtensions
   
{
    public static XElement BuildXmlElementFromObject(this Object input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this String input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this StringBuilder input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlDocument input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlElement input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlNode input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XDocument input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XElement input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlAttribute input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlCDataSection input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlComment input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlText input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlProcessingInstruction input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlDocumentFragment input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlEntityReference input)
    {
        return BuildXmlElementInternal(input);
    }

    public static XElement BuildXmlElementFromObject(this XmlNotation input)
    {
        return BuildXmlElementInternal(input);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementFromObjectExtensions
   
{
    public static XElement BuildXmlElementFromObject(this Byte value)
    {
        return TryConvertToXElement(nameof(Byte), value);
    }

    public static XElement BuildXmlElementFromObject(this SByte value)
    {
        return TryConvertToXElement(nameof(SByte), value);
    }

    public static XElement BuildXmlElementFromObject(this Int16 value)
    {
        return TryConvertToXElement(nameof(Int16), value);
    }

    public static XElement BuildXmlElementFromObject(this UInt16 value)
    {
        return TryConvertToXElement(nameof(UInt16), value);
    }

    public static XElement BuildXmlElementFromObject(this Int32 value)
    {
        return TryConvertToXElement(nameof(Int32), value);
    }

    public static XElement BuildXmlElementFromObject(this UInt32 value)
    {
        return TryConvertToXElement(nameof(UInt32), value);
    }

    public static XElement BuildXmlElementFromObject(this Int64 value)
    {
        return TryConvertToXElement(nameof(Int64), value);
    }

    public static XElement BuildXmlElementFromObject(this UInt64 value)
    {
        return TryConvertToXElement(nameof(Int64), value);
    }

    public static XElement BuildXmlElementFromObject(this IntPtr value)
    {
        return TryConvertToXElement(nameof(IntPtr), value);
    }

    public static XElement BuildXmlElementFromObject(this UIntPtr value)
    {
        return TryConvertToXElement(nameof(UIntPtr), value);
    }

    public static XElement BuildXmlElementFromObject(this Single value)
    {
        return TryConvertToXElement(nameof(Single), value);
    }

    public static XElement BuildXmlElementFromObject(this Double value)
    {
        return TryConvertToXElement(nameof(Double), value);
    }

    public static XElement BuildXmlElementFromObject(this Decimal value)
    {
        return TryConvertToXElement(nameof(Decimal), value);
    }

    public static XElement BuildXmlElementFromObject(this Boolean value)
    {
        return TryConvertToXElement(nameof(Boolean), value);
    }

    public static XElement BuildXmlElementFromObject(this Char value)
    {
        return TryConvertToXElement(nameof(Char), value);
    }

    public static XElement BuildXmlElementFromObject(this DateTime value)
    {
        return TryConvertToXElement(nameof(DateTime), value.ToString("o"));
    }

    public static XElement BuildXmlElementFromObject(this TimeSpan value)
    {
        return TryConvertToXElement(nameof(TimeSpan), value.ToString("c"));
    }

    public static XElement BuildXmlElementFromObject(this Guid value)
    {
        return TryConvertToXElement(nameof(Guid), value.ToString());
    }

    public static XElement BuildXmlElementFromObject(this DayOfWeek value)
    {
        return TryConvertToXElement(nameof(DayOfWeek), value.ToString());
    }
}