using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlDocumentFromObjectExtensions
   
{
    private static XmlDocument SerializeToXmlDocument<T>(T obj)
    {
        try
        {
            System.Xml.Serialization.XmlSerializer serializer = new(typeof(T));
            using MemoryStream stream = new();
            serializer.Serialize(stream, obj);
            stream.Position = 0;

            XmlDocument xmlDoc = new();
            xmlDoc.Load(stream);

            return xmlDoc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("Invalid argument provided for serialization.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Serialization failed due to XML structure issues.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Serialization"))
        {
            throw new InvalidOperationException("Serialization failed due to an issue with XmlSerializer.", ex);
        }
        catch (InvalidOperationException ex) when (ex.InnerException is not null && ex.InnerException.GetType().Equals(typeof(InvalidCastException)))
        {
            throw new InvalidOperationException("A casting error occurred during serialization.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Serialization"))
        {
            throw new InvalidOperationException("Serialization not supported for the given type.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Insufficient memory while serializing the Object.", ex);
        }
        catch (SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Security"))
        {
            throw new InvalidOperationException("Security error occurred during serialization.", ex);
        }
        catch (SerializationException ex) when (ex.Source is not null && ex.Source.Equals("System.Runtime.Serialization"))
        {
            throw new InvalidOperationException("An Object could not be serialized correctly due to a serialization issue.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The resulting XML is not well-formed or could not be loaded.", ex);
        }
        catch (XmlSchemaValidationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML Schema validation failed during XML document creation.", ex);
        }
        catch (XsltException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Xsl"))
        {
            throw new InvalidOperationException("An error occurred during an XSLT transformation within XML processing.", ex);
        }
        catch (XPathException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.XPath"))
        {
            throw new InvalidOperationException("An XPath error occurred during XML document loading.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during XML serialization.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 9 )
/// </summary>
public static partial class BuildXmlDocumentFromObjectExtensions
   
{
    public static XmlDocument BuildXmlDocumentFromObject(this Object value)
    {
        return SerializeToXmlDocument(value);
    }
    
    public static XmlDocument BuildXmlDocumentFromObject(this String value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this StringBuilder value)
    {
        return SerializeToXmlDocument(value.ToString());
    }

    public static XmlDocument BuildXmlDocumentFromObject(this MemoryStream value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this XmlDocument value)
    {
        try
        {
            return (XmlDocument)value.Clone();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlDocument"))
        {
            throw new ApplicationException("Invalid argument passed to XmlDocument clone operation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The XmlDocument is in an invalid state for cloning.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Cloning is not supported for this XmlDocument instance.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The XmlDocument instance is null or contains null references.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("System ran out of memory during XmlDocument cloning.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("An XML error occurred during cloning of the XmlDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Cloning XmlDocument failed.", ex);
        }
    }

    public static XmlDocument BuildXmlDocumentFromObject(this XmlElement value)
    {
        try
        {
            XmlDocument doc = new();
            doc.AppendChild(doc.ImportNode(value, true));
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("node"))
        {
            throw new ApplicationException("The provided XmlElement is not valid for import into the XmlDocument.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The XmlElement is not associated with a valid XmlDocument context.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The XmlElement contains unsupported node types for import.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The XmlElement is null or has null references during import.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Insufficient memory while importing XmlElement into XmlDocument.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("An XML-related error occurred during conversion of XmlElement to XmlDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to convert XmlElement to XmlDocument.", ex);
        }
    }

    public static XmlDocument BuildXmlDocumentFromObject(this StreamReader value)
    {
        try
        {
            XmlDocument doc = new();
            doc.Load(value);
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("reader"))
        {
            throw new ApplicationException("Invalid StreamReader provided for loading XmlDocument.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Directory not found while accessing the StreamReader source.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("File associated with StreamReader was not found.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Invalid operation occurred while loading XmlDocument from StreamReader.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("I/O error occurred while reading from StreamReader.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The operation is not supported for the current StreamReader state.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("StreamReader is null or contains null references.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("System ran out of memory while loading XmlDocument from StreamReader.", ex);
        }
        catch (SecurityException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Security error occurred while accessing the StreamReader source.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Unauthorized access to the StreamReader source.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Malformed XML encountered while loading XmlDocument from StreamReader.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Loading XmlDocument from StreamReader failed.", ex);
        }
    }

    public static XmlDocument BuildXmlDocumentFromObject(this TextReader value)
    {
        try
        {
            XmlDocument doc = new();
            doc.Load(value);
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("reader"))
        {
            throw new ApplicationException("Invalid TextReader provided for loading XmlDocument.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Invalid operation occurred while loading XmlDocument from TextReader.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("I/O error occurred while reading from TextReader.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("The operation is not supported for the current TextReader state.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("TextReader is null or contains null references.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("System ran out of memory while loading XmlDocument from TextReader.", ex);
        }
        catch (SecurityException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Security error occurred while accessing the TextReader source.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Unauthorized access to the TextReader source.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("Malformed XML encountered while loading XmlDocument from TextReader.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Loading XmlDocument from TextReader failed.", ex);
        }
    }

    public static XmlDocument BuildXmlDocumentFromObject(this FileInfo value)
    {
        if (!value.Exists)
            throw new FileNotFoundException($"The file '{value.FullName}' does not exist. Please verify the file path and try again.", value.FullName);

        if (value.Attributes.HasFlag(FileAttributes.Directory))
            throw new InvalidOperationException($"The path '{value.FullName}' points to a directory, not a file.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            XmlDocument doc = new();
            doc.Load(value.FullName);
            return doc;
        }

        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileInfo"))
        {
            throw new InvalidOperationException($"The path '{value.FullName}' points to a directory instead of a file. Please ensure the correct file path is provided.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("XmlDocument"))
        {
            throw new XmlException($"Failed to parse the file '{value.FullName}' as an XML document. The file might be corrupted or not well-formed.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileInfo"))
        {
            throw new IOException($"An error occurred while accessing the file '{value.FullName}'. Please ensure the file is not in use or locked by another process.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value"))
        {
            throw new ArgumentException($"The file '{value.FullName}' has an invalid or unsupported format. Please check the file and try again.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileInfo"))
        {
            throw new UnauthorizedAccessException($"Access to the file '{value.FullName}' is denied. Ensure you have the necessary permissions to read the file.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while loading the XML document from the file.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Value Types )
/// ( Total Methods: 19 )
/// </summary>
public static partial class BuildXmlDocumentFromObjectExtensions
   
{
    public static XmlDocument BuildXmlDocumentFromObject(this Byte value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this SByte value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Int16 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this UInt16 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Int32 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this UInt32 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Int64 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this UInt64 value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this IntPtr value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this UIntPtr value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Single value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Double value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Decimal value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Boolean value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Char value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this DateTime value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this TimeSpan value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this Guid value)
    {
        return SerializeToXmlDocument(value);
    }

    public static XmlDocument BuildXmlDocumentFromObject(this DayOfWeek value)
    {
        return SerializeToXmlDocument(value);
    }
}