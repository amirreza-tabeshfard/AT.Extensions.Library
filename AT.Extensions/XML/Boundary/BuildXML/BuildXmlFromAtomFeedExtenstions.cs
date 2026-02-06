using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Class
/// </summary>
public static partial class BuildXmlFromAtomFeedExtenstions
{
    public class CustomFeedModel
    {
        public required String Title { get; set; }

        public required String Description { get; set; }

        public required String Link { get; set; }

        public required IEnumerable<SyndicationItem>? Items { get; set; }
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromAtomFeedExtenstions
{
    private static XDocument BuildXmlInternal(Object source)
    {
        try
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            if (source is SyndicationFeed feed)
                return ConvertFeedToXDocument(feed);
            else if (source is Atom10FeedFormatter formatter)
            {
                using var ms = new MemoryStream();
                using var writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true });
                formatter.WriteTo(writer);
                writer.Flush();
                ms.Position = 0;
                return XDocument.Load(ms);
            }
            else if (source is Stream stream)
                return XDocument.Load(stream);
            else if (source is XmlDocument xmlDoc)
            {
                using var nodeReader = new XmlNodeReader(xmlDoc);
                return XDocument.Load(nodeReader);
            }
            else if (source is XDocument xDoc)
                return new XDocument(xDoc);
            else if (source is XmlReader reader)
                return XDocument.Load(reader);
            else if (source is TextReader textReader)
                return XDocument.Load(textReader);
            else if (source is CustomFeedModel customModel)
                return ConvertCustomModelToXDocument(customModel);
            else if (source is HttpResponseMessage response)
            {
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"HTTP response returned status code {response.StatusCode}.");

                return XDocument.Load(response.Content.ReadAsStream());
            }
            else if (source is XmlWriterSettings settings)
            {
                var dummyFeed = new SyndicationFeed("Dummy", "Dummy Feed", new Uri("http://localhost"));
                using var memStream = new MemoryStream();
                using (var writer = XmlWriter.Create(memStream, settings))
                {
                    dummyFeed.SaveAsAtom10(writer);
                    writer.Flush();
                }
                memStream.Position = 0;
                return XDocument.Load(memStream);
            }
            else if (source is XmlWriter)
                throw new NotSupportedException("Direct conversion from XmlWriter is not supported. Use XmlWriterSettings or feed input instead.");

            throw new NotSupportedException($"Type {source.GetType().FullName} is not supported for AtomFeed conversion.");
        }
        catch (ArgumentException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML related argument error occurred while building AtomFeed XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The provided AtomFeed source argument is null.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("HTTP transport failed while retrieving AtomFeed content.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("I/O failure occurred while reading or writing AtomFeed XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.ServiceModel.Syndication"))
        {
            throw new InvalidOperationException("Invalid AtomFeed state detected during XML generation.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("Direct conversion from XmlWriter is not supported. Use XmlWriterSettings or feed input instead."))
        {
            throw new InvalidOperationException("XmlWriter cannot be used directly for AtomFeed XML generation.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName.Equals("MemoryStream"))
        {
            throw new InvalidOperationException("MemoryStream was disposed before AtomFeed XML generation completed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the AtomFeed XML resource was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML detected while parsing AtomFeed content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building XML from AtomFeed.", ex);
        }
    }

    private static XDocument ConvertFeedToXDocument(SyndicationFeed feed)
    {
        using var ms = new MemoryStream();
        using var writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true });
        feed.SaveAsAtom10(writer);
        writer.Flush();
        ms.Position = 0;
        return XDocument.Load(ms);
    }

    private static XDocument ConvertCustomModelToXDocument(CustomFeedModel model)
    {
        var feed = new SyndicationFeed(model.Title, model.Description, new Uri(model.Link), model.Items);
        return ConvertFeedToXDocument(feed);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 13 )
/// </summary>
public static partial class BuildXmlFromAtomFeedExtenstions
{
    public static XDocument BuildXmlFromAtomFeed(this SyndicationFeed feed)
    {
        return BuildXmlInternal(feed);
    }

    public static XDocument BuildXmlFromAtomFeed(this Atom10FeedFormatter formatter)
    {
        return BuildXmlInternal(formatter);
    }

    public static XDocument BuildXmlFromAtomFeed(this Stream stream)
    {
        return BuildXmlInternal(stream);
    }

    public static XDocument BuildXmlFromAtomFeed(this XmlDocument xmlDocument)
    {
        return BuildXmlInternal(xmlDocument);
    }

    public static XDocument BuildXmlFromAtomFeed(this XDocument xDocument)
    {
        return BuildXmlInternal(xDocument);
    }

    public static XDocument BuildXmlFromAtomFeed(this XmlReader xmlReader)
    {
        return BuildXmlInternal(xmlReader);
    }

    public static XDocument BuildXmlFromAtomFeed(this TextReader textReader)
    {
        return BuildXmlInternal(textReader);
    }

    public static XDocument BuildXmlFromAtomFeed(this List<SyndicationItem> itemList)
    {
        return BuildXmlInternal(itemList);
    }

    public static XDocument BuildXmlFromAtomFeed(this CustomFeedModel customFeed)
    {
        return BuildXmlInternal(customFeed);
    }

    public static XDocument BuildXmlFromAtomFeed(this HttpResponseMessage response)
    {
        return BuildXmlInternal(response);
    }

    public static XDocument BuildXmlFromAtomFeed(this StreamReader streamReader)
    {
        return BuildXmlInternal(streamReader);
    }

    public static XDocument BuildXmlFromAtomFeed(this XmlWriterSettings settings)
    {
        return BuildXmlInternal(settings);
    }

    public static XDocument BuildXmlFromAtomFeed(this XmlWriter writer)
    {
        return BuildXmlInternal(writer);
    }
}