using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromRssFeedExtenstions
{
    private static XDocument ExecuteWithExceptionHandling(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException("Input cannot be null for BuildXmlFromRssFeed.");

            if (input is String urlString)
                return XDocument.Parse(new HttpClient().GetStringAsync(urlString).Result);
            else if (input is Uri uri)
                return XDocument.Parse(new HttpClient().GetStringAsync(uri).Result);
            else if (input is Stream stream)
                return XDocument.Load(stream);
            else if (input is TextReader reader)
                return XDocument.Load(reader);
            else if (input is XmlDocument xmlDoc)
            {
                using var nodeReader = new XmlNodeReader(xmlDoc);
                return XDocument.Load(nodeReader);
            }
            else if (input is XDocument xDoc)
                return new XDocument(xDoc);
            else if (input is IEnumerable<String> urlList)
            {
                var doc = new XDocument(new XElement("Feeds"));

                foreach (var url in urlList)
                {
                    var feedDoc = ExecuteWithExceptionHandling(url);
                    doc.Root.Add(feedDoc.Root);
                }

                return doc;
            }
            else if (input is IEnumerable<Uri> uriList)
            {
                var doc = new XDocument(new XElement("Feeds"));
                
                foreach (var uriItem in uriList)
                {
                    var feedDoc = ExecuteWithExceptionHandling(uriItem);
                    doc.Root.Add(feedDoc.Root);
                }
                
                return doc;
            }
            else if (input is IEnumerable<XmlDocument> xmlDocs)
            {
                var doc = new XDocument(new XElement("Feeds"));
                
                foreach (var xmlDocItem in xmlDocs)
                {
                    var feedDoc = ExecuteWithExceptionHandling(xmlDocItem);
                    doc.Root.Add(feedDoc.Root);
                }
                
                return doc;
            }
            else if (input is IEnumerable<XDocument> xDocs)
            {
                var doc = new XDocument(new XElement("Feeds"));
                
                foreach (var xDocItem in xDocs)
                    doc.Root.Add(new XDocument(xDocItem).Root);
                
                return doc;
            }
            else if (input is IDictionary<String, String> metadata)
            {
                var doc = new XDocument(new XElement("FeedMetadata"));
                
                foreach (var kvp in metadata)
                    doc.Root.Add(new XElement(kvp.Key, kvp.Value));
                
                return doc;
            }
            else if (input is XmlReader xmlReader)
            {
                return XDocument.Load(xmlReader);
            }
            else if (input is IEnumerable<Stream> streams)
            {
                var doc = new XDocument(new XElement("Feeds"));

                foreach (var streamItem in streams)
                    doc.Root.Add(ExecuteWithExceptionHandling(streamItem).Root);
                
                return doc;
            }
            else if (input is IEnumerable<TextReader> readers)
            {
                var doc = new XDocument(new XElement("Feeds"));
                
                foreach (var readerItem in readers)
                    doc.Root.Add(ExecuteWithExceptionHandling(readerItem).Root);
                
                return doc;
            }
            else
            {
                throw new ArgumentException($"Unsupported input type '{input.GetType().FullName}' for BuildXmlFromRssFeed.");
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Invalid argument provided to BuildXmlFromRssFeed. Parameter: {ex.ParamName}.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input cannot be null for BuildXmlFromRssFeed.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("Failed to fetch RSS feed content via HTTP. Check the URL or network connectivity.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("IO error occurred while reading the feed stream or reader.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid XML operation while processing the feed.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse XML content from the RSS feed.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided input type is not supported for BuildXmlFromRssFeed.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error occurred while building XML from RSS feed. Source: {ex.Source}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromRssFeedExtenstions
{
    public static XDocument BuildXmlFromRssFeed(this String feedUrl)
    {
        return ExecuteWithExceptionHandling(feedUrl);
    }

    public static XDocument BuildXmlFromRssFeed(this Uri feedUri)
    {
        return ExecuteWithExceptionHandling(feedUri);
    }

    public static XDocument BuildXmlFromRssFeed(this Stream feedStream)
    {
        return ExecuteWithExceptionHandling(feedStream);
    }

    public static XDocument BuildXmlFromRssFeed(this TextReader reader)
    {
        return ExecuteWithExceptionHandling(reader);
    }

    public static XDocument BuildXmlFromRssFeed(this XmlDocument xmlDoc)
    {
        return ExecuteWithExceptionHandling(xmlDoc);
    }

    public static XDocument BuildXmlFromRssFeed(this XDocument xDoc)
    {
        return ExecuteWithExceptionHandling(xDoc);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<String> urls)
    {
        return ExecuteWithExceptionHandling(urls);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<Uri> uris)
    {
        return ExecuteWithExceptionHandling(uris);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<XmlDocument> xmlDocs)
    {
        return ExecuteWithExceptionHandling(xmlDocs);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<XDocument> xDocs)
    {
        return ExecuteWithExceptionHandling(xDocs);
    }

    public static XDocument BuildXmlFromRssFeed(this Object feedObject)
    {
        return ExecuteWithExceptionHandling(feedObject);
    }

    public static XDocument BuildXmlFromRssFeed(this IDictionary<String, String> feedMetadata)
    {
        return ExecuteWithExceptionHandling(feedMetadata);
    }

    public static XDocument BuildXmlFromRssFeed(this XmlReader xmlReader)
    {
        return ExecuteWithExceptionHandling(xmlReader);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<Stream> feedStreams)
    {
        return ExecuteWithExceptionHandling(feedStreams);
    }

    public static XDocument BuildXmlFromRssFeed(this IEnumerable<TextReader> readers)
    {
        return ExecuteWithExceptionHandling(readers);
    }
}