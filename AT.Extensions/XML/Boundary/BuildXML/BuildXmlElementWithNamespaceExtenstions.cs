using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Properties
/// </summary>
public static partial class BuildXmlElementWithNamespaceExtenstions
{
    public static XNamespace? InjectedNamespace { get; set; }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementWithNamespaceExtenstions
{
    private static XElement Synchronize(Object value)
    {
        if (InjectedNamespace is null)
            throw new InvalidOperationException("XML namespace has not been injected. Please set XmlBoundaryExtensions.InjectedNamespace before calling BuildXmlElementWithNamespace. (LIKE: BuildXmlElementWithNamespaceExtenstions.InjectedNamespace = XNamespace.Get(\"https://example.com/schema\");)");

        if (value is null)
            throw new ArgumentNullException(nameof(value), "Input value cannot be null.");

        var serializedValue = ConvertValueSafely(value);

        var elementName = value.GetType().Name;

        return new XElement(InjectedNamespace + elementName, serializedValue);
    }

    private static String ConvertValueSafely(Object value)
    {
        try
        {
            return value switch
            {
                DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? throw new InvalidOperationException("Value conversion resulted in null String.")
            };
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Invalid argument provided for parameter '{ex.ParamName}'.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Null value detected for parameter '{ex.ParamName}'.", ex);
        }
        catch (CultureNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Invariant culture could not be resolved during value formatting.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Character decoding failed while converting the value to string.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Character encoding failed while converting the value to string.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Formatting failed for value of type '{value.GetType().FullName}'.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Invalid cast encountered while converting value of type '{value.GetType().FullName}'.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Invalid operation detected during conversion of type '{value.GetType().FullName}'.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"The type '{value.GetType().FullName}' does not support string conversion.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The underlying object was disposed before conversion could complete.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Numeric overflow occurred while converting value of type '{value.GetType().FullName}'.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error occurred while converting value of type '{value.GetType().FullName}'.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 14 )
/// </summary>
public static partial class BuildXmlElementWithNamespaceExtenstions
{
    public static XElement BuildXmlElementWithNamespace(this String value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XmlDocument value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XDocument value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XElement value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XmlElement value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XmlNode value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Stream value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this TextReader value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this StringBuilder value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this FileInfo value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Uri value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XmlReader value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this XStreamingElement value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Byte[] value)
    {
        return Synchronize(value);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementWithNamespaceExtenstions
{
    public static XElement BuildXmlElementWithNamespace(this Byte value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this SByte value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Int16 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this UInt16 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Int32 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this UInt32 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Int64 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this UInt64 value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this IntPtr value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this UIntPtr value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Single value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Double value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Decimal value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Boolean value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Char value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this DateTime value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this TimeSpan value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this Guid value)
    {
        return Synchronize(value);
    }

    public static XElement BuildXmlElementWithNamespace(this DayOfWeek value)
    {
        return Synchronize(value);
    }
}