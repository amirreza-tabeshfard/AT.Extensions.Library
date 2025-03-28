namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromAtomFeedExtenstions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlFromAtomFeed(this String atomFeed)
    {
        ArgumentException.ThrowIfNullOrEmpty(atomFeed);
        ArgumentException.ThrowIfNullOrWhiteSpace(atomFeed);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xmlFeed = System.Xml.Linq.XElement.Parse(atomFeed);
            return xmlFeed;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(atomFeed))
        {
            throw new ArgumentException("Atom Feed input is null or empty.", nameof(atomFeed), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(atomFeed))
        {
            throw new ArgumentException("Atom Feed input contains only white spaces.", nameof(atomFeed), ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new FormatException("Failed to parse Atom Feed due to invalid XML format.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentException("Atom Feed input is null.", nameof(atomFeed), ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the Atom Feed.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromAtomFeed(this String atomFeed, String xmlNamespace)
    {
        ArgumentException.ThrowIfNullOrEmpty(atomFeed);
        ArgumentException.ThrowIfNullOrWhiteSpace(atomFeed);
        ArgumentException.ThrowIfNullOrEmpty(xmlNamespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlNamespace);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xmlFeed = System.Xml.Linq.XElement.Parse(atomFeed);
            xmlFeed.Name = System.Xml.Linq.XNamespace.Get(xmlNamespace) + xmlFeed.Name.LocalName;
            return xmlFeed;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(atomFeed))
        {
            throw new ArgumentException("Atom Feed input is null or empty.", nameof(atomFeed), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(atomFeed))
        {
            throw new ArgumentException("Atom Feed input contains only white spaces.", nameof(atomFeed), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace is null or empty.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace contains only white spaces.", nameof(xmlNamespace), ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new FormatException("Failed to parse Atom Feed due to invalid XML format.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentException("A required input parameter (atomFeed or xmlNamespace) is null.", ex.ParamName, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the Atom Feed with Namespace.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromAtomFeed(this String atomFeed, String xmlNamespace, String metadata)
    {
        ArgumentException.ThrowIfNullOrEmpty(atomFeed);
        ArgumentException.ThrowIfNullOrWhiteSpace(atomFeed);
        ArgumentException.ThrowIfNullOrEmpty(xmlNamespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlNamespace);
        ArgumentException.ThrowIfNullOrEmpty(metadata);
        ArgumentException.ThrowIfNullOrWhiteSpace(metadata);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xmlFeed = System.Xml.Linq.XElement.Parse(atomFeed);
            xmlFeed.Name = System.Xml.Linq.XNamespace.Get(xmlNamespace) + xmlFeed.Name.LocalName;

            System.Xml.Linq.XElement metadataElement = new("Metadata", metadata);
            xmlFeed.Add(metadataElement);

            return xmlFeed;
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(atomFeed))
        {
            throw new ArgumentException("Atom Feed input is null or empty.", nameof(atomFeed), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace is null or empty.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(metadata))
        {
            throw new ArgumentException("Metadata is null or empty.", nameof(metadata), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(atomFeed))
        {
            throw new ArgumentException("Atom Feed input contains only white spaces.", nameof(atomFeed), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace contains only white spaces.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(metadata))
        {
            throw new ArgumentException("Metadata contains only white spaces.", nameof(metadata), ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new FormatException("Failed to parse Atom Feed due to invalid XML format.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while modifying the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the Atom Feed with Metadata.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromAtomFeed(this String atomFeed, String xmlNamespace, String metadata, DateTime timestamp)
    {
        ArgumentException.ThrowIfNullOrEmpty(atomFeed);
        ArgumentException.ThrowIfNullOrWhiteSpace(atomFeed);
        ArgumentException.ThrowIfNullOrEmpty(xmlNamespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlNamespace);
        ArgumentException.ThrowIfNullOrEmpty(metadata);
        ArgumentException.ThrowIfNullOrWhiteSpace(metadata);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xmlFeed = System.Xml.Linq.XElement.Parse(atomFeed);
            xmlFeed.Name = System.Xml.Linq.XNamespace.Get(xmlNamespace) + xmlFeed.Name.LocalName;

            System.Xml.Linq.XElement metadataElement = new("Metadata", metadata);
            System.Xml.Linq.XElement timestampElement = new("Timestamp", timestamp.ToString("o"));

            xmlFeed.Add(metadataElement);
            xmlFeed.Add(timestampElement);

            return xmlFeed;
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(atomFeed))
        {
            throw new ArgumentException("Atom Feed input is null or empty.", nameof(atomFeed), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(atomFeed))
        {
            throw new ArgumentException("Atom Feed input contains only white spaces.", nameof(atomFeed), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace is null or empty.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace contains only white spaces.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(metadata))
        {
            throw new ArgumentException("Metadata is null or empty.", nameof(metadata), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(metadata))
        {
            throw new ArgumentException("Metadata contains only white spaces.", nameof(metadata), ex);
        }
        catch (ArgumentException ex) when (timestamp == default)
        {
            throw new ArgumentException("Timestamp cannot be default DateTime.", nameof(timestamp), ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new FormatException("Failed to parse Atom Feed due to invalid XML format.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while modifying the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the Atom Feed with Metadata and Timestamp.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromAtomFeed(this String atomFeed, String xmlNamespace, String metadata, DateTime timestamp, String id)
    {
        ArgumentException.ThrowIfNullOrEmpty(atomFeed);
        ArgumentException.ThrowIfNullOrWhiteSpace(atomFeed);
        ArgumentException.ThrowIfNullOrEmpty(xmlNamespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlNamespace);
        ArgumentException.ThrowIfNullOrEmpty(metadata);
        ArgumentException.ThrowIfNullOrWhiteSpace(metadata);
        ArgumentException.ThrowIfNullOrEmpty(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xmlFeed = System.Xml.Linq.XElement.Parse(atomFeed);
            xmlFeed.Name = System.Xml.Linq.XNamespace.Get(xmlNamespace) + xmlFeed.Name.LocalName;

            System.Xml.Linq.XElement metadataElement = new("Metadata", metadata);
            System.Xml.Linq.XElement timestampElement = new("Timestamp", timestamp.ToString("o"));
            System.Xml.Linq.XElement idElement = new("ID", id);

            xmlFeed.Add(metadataElement);
            xmlFeed.Add(timestampElement);
            xmlFeed.Add(idElement);

            return xmlFeed;
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(atomFeed))
        {
            throw new ArgumentException("Atom Feed input is null or empty.", nameof(atomFeed), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(atomFeed))
        {
            throw new ArgumentException("Atom Feed input contains only white spaces.", nameof(atomFeed), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace is null or empty.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(xmlNamespace))
        {
            throw new ArgumentException("XML Namespace contains only white spaces.", nameof(xmlNamespace), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(metadata))
        {
            throw new ArgumentException("Metadata is null or empty.", nameof(metadata), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(metadata))
        {
            throw new ArgumentException("Metadata contains only white spaces.", nameof(metadata), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("ID is null or empty.", nameof(id), ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("ID contains only white spaces.", nameof(id), ex);
        }
        catch (ArgumentException ex) when (timestamp == default)
        {
            throw new ArgumentException("Timestamp cannot be default DateTime.", nameof(timestamp), ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new FormatException("Failed to parse Atom Feed due to invalid XML format.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An error occurred while modifying the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the Atom Feed with Namespace, Metadata, Timestamp, and ID.", ex);
        }
    }
}