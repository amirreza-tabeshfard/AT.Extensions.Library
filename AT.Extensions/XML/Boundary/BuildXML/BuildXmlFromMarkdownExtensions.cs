namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromMarkdownExtensions
    : Object
{
    public static String BuildXmlFromMarkdown(this String[] markdownTexts)
    {
        ArgumentNullException.ThrowIfNull(markdownTexts);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("MarkdownCollection");
            xmlDoc.AppendChild(rootElement);

            foreach ((String markdownText, System.Xml.XmlElement paragraph) in from String markdownText in markdownTexts
                                                                               let paragraph = xmlDoc.CreateElement("Paragraph")
                                                                               select (markdownText, paragraph))
            {
                paragraph.InnerText = markdownText;
                rootElement.AppendChild(paragraph);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownTexts"))
        {
            throw new InvalidOperationException("The parameter 'markdownTexts' is invalid.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid casting occurred while handling XML elements.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML construction.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered during XML document creation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Out of memory while creating or manipulating XML elements.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Security exception occurred during XML processing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML error occurred while building the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while converting markdown array to XML.", ex);
        }
    }

    public static String BuildXmlFromMarkdown(this String markdownText, String language)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentException.ThrowIfNullOrEmpty(language);
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("Markdown");
            rootElement.SetAttribute("Language", language);
            xmlDoc.AppendChild(rootElement);

            System.Xml.XmlElement paragraph = xmlDoc.CreateElement("Paragraph");
            paragraph.InnerText = markdownText;
            rootElement.AppendChild(paragraph);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("language"))
        {
            throw new InvalidOperationException("The parameter 'language' is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The parameter 'markdownText' is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML document creation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered while creating XML elements.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Out of memory while building the XML structure.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A security exception occurred while working with XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML-related error occurred during document processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while converting markdown with language to XML.", ex);
        }
    }

    public static String BuildXmlFromMarkdown(this String markdownText, DateTime timestamp)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("Markdown");
            rootElement.SetAttribute("Timestamp", timestamp.ToString("o"));
            xmlDoc.AppendChild(rootElement);

            System.Xml.XmlElement paragraph = xmlDoc.CreateElement("Paragraph");
            paragraph.InnerText = markdownText;
            rootElement.AppendChild(paragraph);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The parameter 'markdownText' is invalid.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A formatting error occurred while converting timestamp to String.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML document generation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered while creating XML elements.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Out of memory during XML document creation.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A security exception occurred while processing the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML error occurred during document building.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while converting markdown with timestamp to XML.", ex);
        }
    }

    public static String BuildXmlFromMarkdown(this String markdownText, String title, Boolean includeTitle = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentException.ThrowIfNullOrEmpty(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("Markdown");
            xmlDoc.AppendChild(rootElement);

            if (includeTitle && !String.IsNullOrEmpty(title))
            {
                System.Xml.XmlElement titleElement = xmlDoc.CreateElement("Title");
                titleElement.InnerText = title;
                rootElement.AppendChild(titleElement);
            }

            System.Xml.XmlElement paragraph = xmlDoc.CreateElement("Paragraph");
            paragraph.InnerText = markdownText;
            rootElement.AppendChild(paragraph);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The parameter 'markdownText' is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("title"))
        {
            throw new InvalidOperationException("The parameter 'title' is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML document generation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered while creating or appending XML elements.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Out of memory during XML processing.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A security exception occurred during XML element manipulation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML error occurred during document building.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while converting markdown with title to XML.", ex);
        }
    }

    public static String BuildXmlFromMarkdown(this String markdownText, Boolean includeTitle = false, Boolean includeTimestamp = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("Markdown");
            xmlDoc.AppendChild(rootElement);

            if (includeTitle)
            {
                System.Xml.XmlElement titleElement = xmlDoc.CreateElement("Title");
                titleElement.InnerText = "Default Title";
                rootElement.AppendChild(titleElement);
            }

            if (includeTimestamp)
            {
                System.Xml.XmlElement timestampElement = xmlDoc.CreateElement("Timestamp");
                timestampElement.InnerText = DateTime.UtcNow.ToString("o");
                rootElement.AppendChild(timestampElement);
            }

            System.Xml.XmlElement paragraph = xmlDoc.CreateElement("Paragraph");
            paragraph.InnerText = markdownText;
            rootElement.AppendChild(paragraph);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The parameter 'markdownText' is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML construction.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference occurred while building the XML structure.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The system ran out of memory during XML generation.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A security violation occurred during XML document creation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML-specific error occurred while constructing the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while converting markdown with optional elements to XML.", ex);
        }
    }

    public static String BuildXmlFromMarkdown(this String markdownText, Object complexModel)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentNullException.ThrowIfNull(complexModel);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("Markdown");
            xmlDoc.AppendChild(rootElement);

            System.Xml.XmlElement paragraph = xmlDoc.CreateElement("Paragraph");
            paragraph.InnerText = markdownText;
            rootElement.AppendChild(paragraph);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The 'markdownText' parameter is invalid or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("complexModel"))
        {
            throw new InvalidOperationException("The 'complexModel' parameter cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML processing.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference occurred while building the XML content.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("System ran out of memory during XML document creation.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Security exception occurred during XML document creation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML-specific error occurred while generating the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error occurred while processing markdown with complex model to XML.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(String markdownText)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootElement);

            System.Xml.XmlElement markdownXml = xmlDocument.CreateElement("Markdown");
            markdownXml.InnerText = markdownText;
            rootElement.AppendChild(markdownXml);

            return xmlDocument;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The 'markdownText' parameter is invalid or empty.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred while constructing the XML structure.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered during XML document construction.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("System ran out of memory while generating XML from markdown.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Security violation occurred during XML creation process.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML-specific error occurred while parsing the markdown content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(System.Text.StringBuilder markdownText)
    {
        ArgumentNullException.ThrowIfNull(markdownText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return BuildXmlFromMarkdown(markdownText.ToString());
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The 'markdownText' parameter is invalid or empty.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid operation occurred while converting StringBuilder to XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("A null reference occurred while accessing the StringBuilder instance.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("System ran out of memory during conversion from StringBuilder to XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML error occurred while building XML from markdown content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(FileInfo markdownFile)
    {
        ArgumentNullException.ThrowIfNull(markdownFile);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (!markdownFile.Exists)
                throw new FileNotFoundException($"The markdown file '{markdownFile.FullName}' could not be found. Please verify that the file exists and the path is correct.");

            String markdownText = File.ReadAllText(markdownFile.FullName);
            return BuildXmlFromMarkdown(markdownText);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("Invalid file path was provided when attempting to read the markdown file.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The directory specified in the markdown file path could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The markdown file was not found on the disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the markdown file.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The file path format is not supported.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The markdown file path exceeds the maximum supported length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("You do not have the required permission to access the markdown file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The markdown content could not be processed into a valid XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(StreamReader markdownReader)
    {
        ArgumentNullException.ThrowIfNull(markdownReader);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String markdownText = markdownReader.ReadToEnd();
            return BuildXmlFromMarkdown(markdownText);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownReader"))
        {
            throw new InvalidOperationException("The provided StreamReader is null.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The StreamReader has been disposed and can no longer be used.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading from the markdown stream.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The system ran out of memory while processing the markdown stream.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Access to the markdown stream is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The markdown content could not be processed into a valid XML document.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The operation could not be completed due to an invalid state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown stream.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(String markdownText, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = BuildXmlFromMarkdown(markdownText);
            Byte[] byteArray = encoding.GetBytes(markdownText);
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("The provided encoding is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is either empty or contains only whitespace.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The system ran out of memory while processing the markdown text.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("There was an issue encoding the markdown text with the provided encoding.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The operation could not be completed due to an invalid state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown with encoding.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(String markdownText, IEnumerable<String> tags)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentNullException.ThrowIfNull(tags);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = BuildXmlFromMarkdown(markdownText);

            if (tags?.Any() ?? false)
            {
                System.Xml.XmlElement tagsElement = xmlDocument.CreateElement("Tags");
                
                foreach ((String tag, System.Xml.XmlElement tagElement) in from String tag in tags
                                                                           let tagElement = xmlDocument.CreateElement("Tag")
                                                                           select (tag, tagElement))
                {
                    tagElement.InnerText = tag;
                    tagsElement.AppendChild(tagElement);
                }

                xmlDocument.DocumentElement?.AppendChild(tagsElement);
            }

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("tags"))
        {
            throw new InvalidOperationException("The provided tags collection is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is either empty or contains only whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred while manipulating XML elements.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("A null reference occurred while building the XML structure.", ex);
        }
        catch (System.InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("An invalid cast occurred while processing the XML structure.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML parsing error occurred while processing the markdown text.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown with tags.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(String markdownText, Boolean includeMetadata)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = BuildXmlFromMarkdown(markdownText);
            
            if (includeMetadata)
            {
                System.Xml.XmlElement metadataElement = xmlDocument.CreateElement("Metadata");
                metadataElement.InnerText = "Generated at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                xmlDocument.DocumentElement?.AppendChild(metadataElement);
            }

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("includeMetadata"))
        {
            throw new InvalidOperationException("The 'includeMetadata' parameter is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is either empty or contains only whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred while manipulating XML elements.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("A null reference occurred while building the XML structure.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML parsing error occurred while processing the markdown text.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown with metadata.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMarkdown(String markdownText, Stream additionalDataStream)
    {
        ArgumentException.ThrowIfNullOrEmpty(markdownText);
        ArgumentException.ThrowIfNullOrWhiteSpace(markdownText);
        ArgumentNullException.ThrowIfNull(additionalDataStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = BuildXmlFromMarkdown(markdownText);
            if (additionalDataStream is not null)
            {
                using StreamReader reader = new(additionalDataStream);
                String additionalData = reader.ReadToEnd();
                System.Xml.XmlElement additionalDataElement = xmlDocument.CreateElement("AdditionalData");
                additionalDataElement.InnerText = additionalData;
                xmlDocument.DocumentElement?.AppendChild(additionalDataElement);
            }

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("additionalDataStream"))
        {
            throw new InvalidOperationException("The 'additionalDataStream' parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("markdownText"))
        {
            throw new InvalidOperationException("The provided markdown text is either empty or contains only whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred while manipulating XML elements during stream processing.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An error occurred while reading from the additional data stream.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("A null reference occurred while trying to append additional data to the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML parsing error occurred while processing the markdown text or additional data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from Markdown with additional stream data.", ex);
        }
    }
}