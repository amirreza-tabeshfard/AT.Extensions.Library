namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlDocumentFromObjectExtensions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this Object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Serialization.XmlSerializer serializer = new(obj.GetType());
            System.Xml.XmlDocument xmlDocument = new();

            using StringWriter stringWriter = new();
            using System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, obj);

            xmlDocument.LoadXml(stringWriter.ToString());
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The provided object cannot be null. Please ensure the object passed is not null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The value of the property cannot be serialized"))
        {
            throw new InvalidOperationException("Serialization failed due to a non-serializable property or field in the object. Please check all properties for serializability.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The object cannot be serialized"))
        {
            throw new InvalidOperationException("Serialization failed because the object itself is not serializable. Ensure the object has the [Serializable] attribute.", ex);
        }
        catch (InvalidOperationException ex) when (ex.InnerException is not null && ex.InnerException.GetType().Equals(typeof(System.Reflection.TargetException)))
        {
            throw new InvalidOperationException("Serialization failed due to an invalid target for serialization. Ensure that the target object is valid for serialization.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber == 0 && ex.LinePosition == 0)
        {
            throw new System.Xml.XmlException("The generated XML is empty or malformed. Check the object structure before serialization.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The character data is invalid"))
        {
            throw new System.Xml.XmlException("The generated XML contains invalid character data. Please review the object's properties for invalid characters.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The string contains an invalid character"))
        {
            throw new System.Xml.XmlException("The generated XML contains invalid characters that could not be parsed. Ensure all string data in the object is valid.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file"))
        {
            throw new IOException("An I/O error occurred, possibly due to access restrictions while processing the XML document. Check file access permissions.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("disk full"))
        {
            throw new IOException("An I/O error occurred due to a full disk while processing the XML document. Ensure sufficient disk space is available.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("network path"))
        {
            throw new IOException("An I/O error occurred due to network path issues while processing the XML document. Verify network access and paths.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access to the path is denied"))
        {
            throw new UnauthorizedAccessException("An I/O error occurred due to access denial to a file or resource while processing the XML document. Check user access permissions.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("An unauthorized access error occurred while processing the XML document. Ensure that the necessary permissions are granted.", ex);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new Exception("An unexpected error occurred due to an invalid operation while converting object to System.Xml.XmlDocument.", ex);
        }
        catch (Exception ex) when (ex is not IOException)
        {
            throw new Exception("An unexpected error occurred due to an I/O issue while converting object to System.Xml.XmlDocument. This could be caused by file or network issues.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new Exception("An unexpected error occurred due to a null argument while converting object to System.Xml.XmlDocument.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new Exception("An unexpected error occurred due to an XML parsing issue while converting object to System.Xml.XmlDocument. This might be due to malformed XML or invalid characters.", ex);
        }
        catch (Exception ex) when (ex is not UnauthorizedAccessException)
        {
            throw new Exception("An unexpected error occurred due to unauthorized access while converting object to System.Xml.XmlDocument. This could be due to insufficient file or resource permissions.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new Exception("An unexpected error occurred due to an argument issue while converting object to System.Xml.XmlDocument. Please check the arguments passed to the method.", ex);
        }
        catch (Exception ex) when (ex is not NullReferenceException)
        {
            throw new Exception("An unexpected error occurred due to a null reference issue while converting object to System.Xml.XmlDocument.", ex);
        }
        catch (Exception ex) when (ex is not OverflowException)
        {
            throw new Exception("An unexpected error occurred due to an overflow issue while converting object to System.Xml.XmlDocument. This may indicate that the data exceeded expected limits.", ex);
        }
        catch (Exception ex) when (ex is not TimeoutException)
        {
            throw new Exception("An unexpected error occurred due to a timeout while converting object to System.Xml.XmlDocument. This might be caused by long-running operations.", ex);
        }
        catch (Exception ex) when (ex is not KeyNotFoundException)
        {
            throw new Exception("An unexpected error occurred due to a missing key or resource while converting object to System.Xml.XmlDocument.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this Object obj, String rootName)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = obj.BuildXmlDocumentFromObject();
        System.Xml.XmlElement root = doc.DocumentElement;
        root.SetAttribute("RootName", rootName);
        // ----------------------------------------------------------------------------------------------------
        return doc;
    }

    public static void BuildXmlDocumentFromObject(this Object obj, String filePath, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument xmlDoc = obj.BuildXmlDocumentFromObject();
        using StreamWriter writer = new(filePath, false, encoding);
        xmlDoc.Save(writer);
    }

    public static System.Xml.Linq.XDocument BuildXmlDocumentFromObject(this Object obj, Boolean useLinq)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        if (useLinq)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new(obj.GetType());
                System.Xml.Linq.XDocument doc = new();
                using (System.Xml.XmlWriter writer = doc.CreateWriter())
                {
                    serializer.Serialize(writer, obj);
                }
                return doc;
            }
            catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
            {
                throw new InvalidOperationException("The provided object is null, unable to serialize.", ex);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("An error occurred while serializing"))
            {
                throw new InvalidOperationException("An error occurred while serializing the object.", ex);
            }
            catch (System.Xml.XmlException ex) when (ex.Message.Contains("Failed to write XML due to an XML format issue"))
            {
                throw new InvalidOperationException("Failed to write XML due to an XML format issue.", ex);
            }
            catch (System.Runtime.Serialization.SerializationException ex) when (ex.Message.Contains("The object may not be serializable"))
            {
                throw new InvalidOperationException("Serialization failed. The object may not be serializable.", ex);
            }
            catch (System.Xml.XmlException ex) when (ex.Message.Contains("The element is not well-formed"))
            {
                throw new InvalidOperationException("XML is not well-formed. Please verify the object's structure.", ex);
            }
            catch (System.Xml.XmlException ex) when (ex.Message.Contains("The document is empty"))
            {
                throw new InvalidOperationException("The XML document is empty. Ensure the object has data.", ex);
            }
            catch (System.Xml.XmlException ex) when (ex.Message.Contains("The string contains an invalid character"))
            {
                throw new InvalidOperationException("The generated XML contains invalid characters. Please review the object's properties for invalid characters.", ex);
            }
            catch (System.Runtime.Serialization.SerializationException ex) when (ex.Message.Contains("cannot be serialized"))
            {
                throw new InvalidOperationException("The object cannot be serialized. Ensure all properties are serializable.", ex);
            }
            catch (IOException ex) when (ex.Message.Contains("disk full"))
            {
                throw new InvalidOperationException("An I/O error occurred due to a full disk. Ensure sufficient disk space is available.", ex);
            }
            catch (IOException ex) when (ex.Message.Contains("network path"))
            {
                throw new InvalidOperationException("An I/O error occurred due to network path issues while processing the XML document. Verify network access and paths.", ex);
            }
            catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access to the path is denied"))
            {
                throw new InvalidOperationException("An I/O error occurred due to access denial to a file or resource while processing the XML document. Check user access permissions.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidOperationException("An unauthorized access error occurred while processing the XML document. Ensure that the necessary permissions are granted.", ex);
            }
            catch (Exception ex) when (ex is not InvalidOperationException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to an invalid operation while creating System.Xml.Linq.XDocument.", ex);
            }
            catch (Exception ex) when (ex is not IOException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to an I/O issue while creating System.Xml.Linq.XDocument. This could be caused by file or network issues.", ex);
            }
            catch (Exception ex) when (ex is not ArgumentNullException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to a null argument while creating System.Xml.Linq.XDocument.", ex);
            }
            catch (Exception ex) when (ex is not System.Xml.XmlException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to an XML parsing issue while creating System.Xml.Linq.XDocument. This might be due to malformed XML or invalid characters.", ex);
            }
            catch (Exception ex) when (ex is not UnauthorizedAccessException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to unauthorized access while creating System.Xml.Linq.XDocument. This could be due to insufficient file or resource permissions.", ex);
            }
            catch (Exception ex) when (ex is not ArgumentException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to an argument issue while creating System.Xml.Linq.XDocument. Please check the arguments passed to the method.", ex);
            }
            catch (Exception ex) when (ex is not NullReferenceException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to a null reference issue while creating System.Xml.Linq.XDocument.", ex);
            }
            catch (Exception ex) when (ex is not OverflowException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to an overflow issue while creating System.Xml.Linq.XDocument. This may indicate that the data exceeded expected limits.", ex);
            }
            catch (Exception ex) when (ex is not TimeoutException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to a timeout while creating System.Xml.Linq.XDocument. This might be caused by long-running operations.", ex);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new InvalidOperationException("An unexpected error occurred due to a missing key or resource while creating System.Xml.Linq.XDocument.", ex);
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return System.Xml.Linq.XDocument.Parse(obj.BuildXmlDocumentFromObject(true).ToString());
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this Object obj, System.Xml.XmlWriterSettings settings)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        using (MemoryStream memoryStream = new())
        using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(memoryStream, settings))
        {
            System.Xml.Serialization.XmlSerializer serializer = new(obj.GetType());
            serializer.Serialize(writer, obj);
            memoryStream.Position = 0;
            doc.Load(memoryStream);
        }
        return doc;
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this Dictionary<String, String> dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        System.Xml.XmlElement root = doc.CreateElement("Root");
        doc.AppendChild(root);
        foreach ((KeyValuePair<string, string> kvp, System.Xml.XmlElement element) in from KeyValuePair<string, string> kvp in dictionary
                                                                                      let element = doc.CreateElement(kvp.Key)
                                                                                      select (kvp, element))
        {
            element.InnerText = kvp.Value;
            root.AppendChild(element);
        }
        // ----------------------------------------------------------------------------------------------------
        return doc;
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject<T>(this List<T> list, String rootElement)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentException.ThrowIfNullOrEmpty(rootElement);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElement);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        System.Xml.XmlElement root = doc.CreateElement(rootElement);
        doc.AppendChild(root);
        // ----------------------------------------------------------------------------------------------------
        foreach ((T item, System.Xml.XmlElement element) in from T? item in list
                                                            let element = doc.CreateElement(typeof(T).Name)
                                                            select (item, element))
        {
            element.InnerText = item.ToString();
            root.AppendChild(element);
        }
        // ----------------------------------------------------------------------------------------------------
        return doc;
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this String xmlString)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlString);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlString);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        try
        {
            doc.LoadXml(xmlString);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString"))
        {
            throw new InvalidOperationException("Input string cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(ex.Message))
        {
            throw new InvalidOperationException("Input string cannot be null, empty, or whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'root' element is missing"))
        {
            throw new InvalidOperationException("The XML is missing the root element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("There is an error in XML document"))
        {
            throw new InvalidOperationException("The XML document is not well-formed. Please verify the XML structure.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new InvalidOperationException("The XML contains invalid characters. Please check for non-UTF-8 characters or invalid XML formatting.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("The XML document is incomplete or unexpectedly terminated. Please ensure the XML string is fully formed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("data type mismatch"))
        {
            throw new InvalidOperationException("The XML document contains a data type mismatch. Please verify the content types in the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Invalid content"))
        {
            throw new InvalidOperationException("The XML contains invalid content or structure that prevents it from being loaded.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Element is not allowed"))
        {
            throw new InvalidOperationException("An element is not allowed in the current XML context. Review the XML schema or structure.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new InvalidOperationException("An unexpected error occurred while loading XML. Please verify the input XML string.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to a general issue while loading XML.", ex);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML input string.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new InvalidOperationException("An unexpected error occurred while handling XML loading operations.", ex);
        }
        catch (Exception ex) when (ex is not UnauthorizedAccessException)
        {
            throw new InvalidOperationException("An error occurred due to unauthorized access while attempting to load the XML document.", ex);
        }
        catch (Exception ex) when (ex is not FormatException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to a formatting issue while processing the XML string.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to a null argument passed during the XML loading process.", ex);
        }
        catch (Exception ex) when (ex is not IOException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to an I/O issue while loading the XML document.", ex);
        }
        catch (Exception ex) when (ex is not TimeoutException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to a timeout while loading the XML document.", ex);
        }
        catch (Exception ex) when (ex is not OverflowException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to an overflow condition while loading the XML document.", ex);
        }
        catch (Exception ex) when (ex is not IndexOutOfRangeException)
        {
            throw new InvalidOperationException("An unexpected error occurred due to an index out of range while processing the XML document.", ex);
        }
        // ----------------------------------------------------------------------------------------------------
        return doc;
    }

    public static System.Xml.XmlDocument BuildXmlDocumentFromObject(this Stream xmlStream)
    {
        ArgumentNullException.ThrowIfNull(xmlStream);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument doc = new();
        try
        {
            doc.Load(xmlStream);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlStream"))
        {
            throw new InvalidOperationException("The provided XML stream is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.Message.Contains("value cannot be null"))
        {
            throw new InvalidOperationException("The XML stream cannot be null. Please provide a valid stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The document is empty"))
        {
            throw new InvalidOperationException("The XML stream is empty. Please provide a non-empty stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The XML data at the root level is invalid. Please check the structure of the XML stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new InvalidOperationException("The XML stream contains invalid characters. Please verify the character encoding or XML structure.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Unexpected end of file"))
        {
            throw new InvalidOperationException("The XML stream appears to be incomplete. Please ensure the XML is fully transmitted.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The ':' character, hexadecimal value"))
        {
            throw new InvalidOperationException("The XML stream contains an invalid character sequence. Please verify the XML format.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file"))
        {
            throw new InvalidOperationException("I/O error occurred while accessing the XML stream. Ensure the stream is not locked or in use by another process.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The network path was not found"))
        {
            throw new InvalidOperationException("Network error occurred while accessing the XML stream. Please verify the network path is valid and accessible.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The file exists"))
        {
            throw new InvalidOperationException("I/O error occurred due to file existence issues. Please verify the stream source.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not System.Xml.XmlException && ex is not IOException)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML stream.", ex);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new InvalidOperationException("An unknown error occurred during the XML processing operation.", ex);
        }
        catch (Exception ex) when (ex is not UnauthorizedAccessException)
        {
            throw new InvalidOperationException("Access denied while processing the XML stream. Ensure the necessary permissions are granted.", ex);
        }
        catch (Exception ex) when (ex is not FileNotFoundException)
        {
            throw new InvalidOperationException("File not found while loading the XML stream. Please verify the stream source path.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentOutOfRangeException)
        {
            throw new InvalidOperationException("An error occurred due to an out-of-range issue while processing the XML stream.", ex);
        }
        catch (Exception ex) when (ex is not TimeoutException)
        {
            throw new InvalidOperationException("The operation timed out while processing the XML stream. Please try again later.", ex);
        }
        catch (Exception ex) when (ex is not FormatException)
        {
            throw new InvalidOperationException("A format error occurred while processing the XML stream. Please verify the data format.", ex);
        }
        catch (Exception ex) when (ex is not OverflowException)
        {
            throw new InvalidOperationException("An overflow error occurred while processing the XML stream. Please check the input size.", ex);
        }
        catch (Exception ex) when (ex is not IOException)
        {
            throw new InvalidOperationException("I/O operation failed while handling the XML stream. Please check the system resources.", ex);
        }
        // ----------------------------------------------------------------------------------------------------
        return doc;
    }
}