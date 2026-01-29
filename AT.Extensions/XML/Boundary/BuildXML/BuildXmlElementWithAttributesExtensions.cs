using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlElementWithAttributesExtensions
{
    private static XmlElement CreateXmlElementWithAttributes(Object input, Func<XmlDocument, Object, XmlElement> buildFunc)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            return buildFunc(doc, input);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("buildFunc"))
        {
            throw new InvalidOperationException("The build function argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("buildFunc"))
        {
            throw new InvalidOperationException("Build function cannot be null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input argument is out of range.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The requested XML operation is not supported.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("XmlDocument"))
        {
            throw new InvalidOperationException("The XmlDocument instance was already disposed.", ex);
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("An exception was thrown by the invoked build function.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Unauthorized access while processing XML.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML was produced by the build function.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XmlElement.", ex);
        }
    }

    private static XmlElement CreateXmlElement<T>(T value, String elementName, Action<XmlElement, T> attributeBuilder)
        where T : struct
    {
        try
        {
            var doc = new XmlDocument();
            var element = doc.CreateElement(elementName);
            attributeBuilder(element, value);
            return element;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Element name argument is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Argument exception raised from XML subsystem.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeBuilder"))
        {
            throw new InvalidOperationException("Attribute builder delegate is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value"))
        {
            throw new InvalidOperationException("Input value is null.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid format encountered while building XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML document is in an invalid state.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Invalid operation detected in core runtime.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Null reference encountered during XML element creation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Null reference detected in core library.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML detected while creating element.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.ReaderWriter"))
        {
            throw new InvalidOperationException("XML reader/writer failure occurred.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error while building XmlElement for type {typeof(T).Name}.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlElementWithAttributesExtensions

{
    public static XmlElement BuildXmlElementWithAttributes(this String input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("String");
            element.InnerText = input;
            element.SetAttribute("Length", input.Length.ToString());
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this StringBuilder input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var value = input.ToString();
            var element = doc.CreateElement("StringBuilder");
            element.InnerText = value;
            element.SetAttribute("Capacity", input.Capacity.ToString());
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Uri input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Uri");
            element.SetAttribute("AbsoluteUri", input.AbsoluteUri);
            element.SetAttribute("Host", input.Host);
            element.SetAttribute("Scheme", input.Scheme);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this XmlDocument input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("XmlDocument");
            element.SetAttribute("DocumentElement", input.DocumentElement?.Name ?? "None");
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this FileInfo input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("File");
            element.SetAttribute("Name", input.Name);
            element.SetAttribute("Length", input.Length.ToString());
            element.SetAttribute("Extension", input.Extension);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this DirectoryInfo input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Directory");
            element.SetAttribute("Name", input.Name);
            element.SetAttribute("FullPath", input.FullName);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Version input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Version");
            element.SetAttribute("Major", input.Major.ToString());
            element.SetAttribute("Minor", input.Minor.ToString());
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this DateTimeOffset input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("DateTimeOffset");
            element.SetAttribute("DateTime", input.ToString("o"));
            element.SetAttribute("Offset", input.Offset.ToString());
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this TimeZoneInfo input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("TimeZone");
            element.SetAttribute("Id", input.Id);
            element.SetAttribute("DisplayName", input.DisplayName);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this XDocument input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("XDocument");
            element.SetAttribute("Root", input.Root?.Name.LocalName ?? "None");
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this CultureInfo input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Culture");
            element.SetAttribute("Name", input.Name);
            element.SetAttribute("DisplayName", input.DisplayName);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Exception input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Exception");
            element.SetAttribute("Message", input.Message);
            element.SetAttribute("Type", input.GetType().FullName);
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Assembly input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("Assembly");
            element.SetAttribute("FullName", input.FullName);
            element.SetAttribute("Location", input.Location ?? "Unknown");
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this HttpRequestMessage input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("HttpRequest");
            element.SetAttribute("Method", input.Method.Method);
            element.SetAttribute("RequestUri", input.RequestUri?.ToString() ?? "null");
            return element;
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this XmlElement input)
    {
        return CreateXmlElementWithAttributes(input, (doc, obj) =>
        {
            var element = doc.CreateElement("XmlElementWrapper");
            element.SetAttribute("OriginalName", input.Name);
            element.InnerXml = input.OuterXml;
            return element;
        });
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlElementWithAttributesExtensions

{
    public static XmlElement BuildXmlElementWithAttributes(this Byte value)
    {
        return CreateXmlElement(value, "Byte", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Binary", Convert.ToString(val, 2).PadLeft(8, '0'));
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this SByte value)
    {
        return CreateXmlElement(value, "SByte", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("Binary", Convert.ToString(val, 2).PadLeft(8, '0'));
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Int16 value)
    {
        return CreateXmlElement(value, "Int16", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("IsEven", (val % 2 == 0).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this UInt16 value)
    {
        return CreateXmlElement(value, "UInt16", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("IsEven", (val % 2 == 0).ToString());
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Int32 value)
    {
        return CreateXmlElement(value, "Int32", (el, val) =>
                {
                    el.InnerText = val.ToString();
                    el.SetAttribute("IsPositive", (val >= 0).ToString());
                });
    }

    public static XmlElement BuildXmlElementWithAttributes(this UInt32 value)
    {
        return CreateXmlElement(value, "UInt32", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("IsPositive", (val >= 0).ToString());
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Int64 value)
    {
        return CreateXmlElement(value, "Int64", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Bytes", BitConverter.GetBytes(val).Length.ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this UInt64 value)
    {
        return CreateXmlElement(value, "UInt64", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("Bytes", BitConverter.GetBytes(val).Length.ToString());
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this IntPtr value)
    {
        return CreateXmlElement(value, "IntPtr", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("PointerSize", IntPtr.Size.ToString());
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this UIntPtr value)
    {
        return CreateXmlElement(value, "UIntPtr", (el, val) =>
        {
            el.InnerText = val.ToString();
            el.SetAttribute("PointerSize", UIntPtr.Size.ToString());
        });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Single value)
    {
        return CreateXmlElement(value, "Single", (el, val) =>
            {
                el.InnerText = val.ToString("R");
                el.SetAttribute("IsNaN", float.IsNaN(val).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Double value)
    {
        return CreateXmlElement(value, "Double", (el, val) =>
            {
                el.InnerText = val.ToString("R");
                el.SetAttribute("IsInfinity", double.IsInfinity(val).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Decimal value)
    {
        return CreateXmlElement(value, "Decimal", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Precision", Decimal.GetBits(val)[3].ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Boolean value)
    {
        return CreateXmlElement(value, "Boolean", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Negation", (!val).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Char value)
    {
        return CreateXmlElement(value, "Char", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Unicode", ((int)val).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this DateTime value)
    {
        return CreateXmlElement(value, "DateTime", (el, val) =>
            {
                el.InnerText = val.ToString("o");
                el.SetAttribute("Kind", val.Kind.ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this TimeSpan value)
    {
        return CreateXmlElement(value, "TimeSpan", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("TotalSeconds", val.TotalSeconds.ToString("F2"));
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this Guid value)
    {
        return CreateXmlElement(value, "Guid", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("IsEmpty", (val == Guid.Empty).ToString());
            });
    }

    public static XmlElement BuildXmlElementWithAttributes(this DayOfWeek value)
    {
        return CreateXmlElement(value, "DayOfWeek", (el, val) =>
            {
                el.InnerText = val.ToString();
                el.SetAttribute("Numeric", ((int)val).ToString());
            });
    }
}