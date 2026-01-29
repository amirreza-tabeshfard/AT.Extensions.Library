using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementWithInnerTextExtensions
{
    private static String ConvertValueToString(Object value)
    {
        try
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), "Boundary value cannot be null.");

            return value switch
            {
                DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
                IFormattable formattable => formattable.ToString(default, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? throw new InvalidOperationException("Value conversion failed.")
            };
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(value)))
        {
            throw new InvalidOperationException("The provided argument is invalid for XML Boundary conversion.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(value)))
        {
            throw new InvalidOperationException("The Boundary value argument is null.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The value format is not valid for invariant XML conversion.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals(typeof(Object).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The value cannot be cast to a formattable or String representation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals(typeof(String).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The conversion operation produced an invalid state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The current value type does not support invariant formatting.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(Object).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The referenced Object was disposed before XML Boundary synchronization.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Numeric overflow occurred during XML Boundary conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to convert value of type '{value?.GetType().Name}' to XML Boundary inner text.", ex);
        }
    }

    private static XElement BuildBoundary(Object value)
    {
        var innerText = ConvertValueToString(value);
        return new XElement("Boundary", innerText);
    }

    private static String? Synchronize(Object value)
    {
        try
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), "Boundary reference cannot be null.");

            return value switch
            {
                XElement xe => xe.ToString(SaveOptions.DisableFormatting),
                XDocument xd => xd.ToString(SaveOptions.DisableFormatting),
                XAttribute xa => xa.ToString(),
                XmlDocument xml => xml.OuterXml,
                FileInfo fi => fi.FullName,
                DirectoryInfo di => di.FullName,
                CultureInfo ci => ci.Name,
                Uri uri => uri.AbsoluteUri,
                StringBuilder sb => sb.ToString(),
                TextReader tr => tr.ReadToEnd(),
                TextWriter tw => tw.ToString(),
                Exception ex => ex.ToString(),
                IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? throw new InvalidOperationException("Reference conversion returned null.")
            };
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(value)))
        {
            throw new InvalidOperationException("Invalid argument was provided for Boundary synchronization.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(value)))
        {
            throw new InvalidOperationException("Boundary reference value is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals(typeof(DirectoryInfo).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Target directory was not found during Boundary synchronization.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals(typeof(FileInfo).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Target file was not found during Boundary synchronization.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Invariant formatting failed for the provided reference.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals(typeof(object).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Reference type cannot be converted to a supported Boundary representation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals(typeof(string).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Invalid operation occurred while producing Boundary inner text.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals(typeof(TextReader).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("I/O failure occurred while reading Boundary content.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The reference type does not support invariant formatting.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(TextReader).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The underlying reader or writer was disposed before synchronization.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals(typeof(IFormattable).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Numeric overflow occurred during Boundary synchronization.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals(typeof(FileInfo).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("Unauthorized access to file system resource during Boundary synchronization.", ex);
        }
        catch (UriFormatException ex) when (ex.Source is not null && ex.Source.Equals(typeof(Uri).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("The URI reference format is invalid for Boundary synchronization.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals(typeof(XmlDocument).Assembly.GetName().Name))
        {
            throw new InvalidOperationException("XML serialization failed during Boundary synchronization.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Synchronize failed for reference type '{value?.GetType().FullName}'.", ex);
        }
    }
}

public static partial class BuildXmlElementWithInnerTextExtensions
{
    public static XElement BuildXmlElementWithInnerText(this String value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this StringBuilder value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this Uri value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this Version value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this XElement value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this XDocument value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this XmlDocument value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this XAttribute value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this FileInfo value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this DirectoryInfo value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this CultureInfo value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this Exception value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this Object value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this TextReader value)
    {
        return new XElement("Boundary", Synchronize(value));
    }

    public static XElement BuildXmlElementWithInnerText(this TextWriter value)
    {
        return new XElement("Boundary", Synchronize(value));
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementWithInnerTextExtensions
{
    public static XElement BuildXmlElementWithInnerText(this Byte value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this SByte value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Int16 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this UInt16 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Int32 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this UInt32 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Int64 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this UInt64 value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this IntPtr value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this UIntPtr value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Single value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Double value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Decimal value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Boolean value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Char value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this DateTime value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this TimeSpan value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this Guid value)
    {
        return BuildBoundary(value);
    }

    public static XElement BuildXmlElementWithInnerText(this DayOfWeek value)
    {
        return BuildBoundary(value);
    }
}