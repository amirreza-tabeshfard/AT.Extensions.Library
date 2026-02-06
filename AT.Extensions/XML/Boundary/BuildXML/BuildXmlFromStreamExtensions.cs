using System.Net.Sockets;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromStreamExtensions
{
    private static XmlDocument BuildXmlFromStreamInternal(object source)
    {
        try
        {
            if (source is Stream s)
            {
                var xmlDoc = new XmlDocument();
                s.Position = 0;
                xmlDoc.Load(s);
                return xmlDoc;
            }
            else if (source is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The provided FileInfo does not exist.", fi.FullName);

                var xmlDoc = new XmlDocument();
                using (var fs = fi.OpenRead())
                {
                    xmlDoc.Load(fs);
                }
                return xmlDoc;
            }
            else if (source is TextReader tr)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(tr);
                return xmlDoc;
            }
            else if (source is XmlReader xr)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xr);
                return xmlDoc;
            }
            else if (source is XElement xe)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xe.ToString());
                return xmlDoc;
            }
            else if (source is XDocument xd)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xd.ToString());
                return xmlDoc;
            }
            else if (source is XmlDocument xd2)
                return xd2;
            else if (source is MemoryStream ms)
            {
                ms.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);
                return xmlDoc;
            }
            else if (source is NetworkStream ns)
            {
                ns.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(ns);
                return xmlDoc;
            }
            else if (source is StringReader sr)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(sr);
                return xmlDoc;
            }
            else if (source is FileStream fs2)
            {
                fs2.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(fs2);
                return xmlDoc;
            }
            else if (source is BufferedStream bs)
            {
                bs.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(bs);
                return xmlDoc;
            }
            else if (source is DirectoryInfo di)
                throw new InvalidOperationException("Cannot build XML from DirectoryInfo. Directory is not a valid XML source.");
            else if (source is HttpResponseMessage hr)
            {
                if (hr.Content == null)
                    throw new InvalidOperationException("HttpResponseMessage content is null.");

                using var stream = hr.Content.ReadAsStream();
                return BuildXmlFromStreamInternal(stream);
            }
            else if (source is HttpRequestMessage hreq)
            {
                if (hreq.Content == null)
                    throw new InvalidOperationException("HttpRequestMessage content is null.");

                using var stream = hreq.Content.ReadAsStream();
                return BuildXmlFromStreamInternal(stream);
            }
            else
                throw new InvalidOperationException($"Unsupported source type: {source.GetType().FullName}");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new ArgumentException("The provided argument 'source' is invalid or null.", ex.ParamName, ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message is not null && ex.Message.Contains("could not find"))
        {
            throw new DirectoryNotFoundException("The directory specified cannot be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals("FileInfo"))
        {
            throw new FileNotFoundException("The provided FileInfo does not exist.", ex.FileName, ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while accessing the stream or file.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message is not null && ex.Message.Contains("not a valid XML source"))
        {
            throw new InvalidOperationException("The provided source type cannot be used to build XML.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("unsupported"))
        {
            throw new NotSupportedException("The operation is not supported for the provided source type.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new ObjectDisposedException(ex.ObjectName, "The stream or resource has already been disposed and cannot be read.");
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("Failed to parse XML from the provided source.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while building XML from stream.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromStreamExtensions
{
    public static XmlDocument BuildXmlFromStream(this Stream stream)
    {
        return BuildXmlFromStreamInternal(stream);
    }

    public static XmlDocument BuildXmlFromStream(this FileInfo fileInfo)
    {
        return BuildXmlFromStreamInternal(fileInfo);
    }

    public static XmlDocument BuildXmlFromStream(this TextReader textReader)
    {
        return BuildXmlFromStreamInternal(textReader);
    }

    public static XmlDocument BuildXmlFromStream(this XmlReader xmlReader)
    {
        return BuildXmlFromStreamInternal(xmlReader);
    }

    public static XmlDocument BuildXmlFromStream(this MemoryStream memoryStream)
    {
        return BuildXmlFromStreamInternal(memoryStream);
    }

    public static XmlDocument BuildXmlFromStream(this NetworkStream networkStream)
    {
        return BuildXmlFromStreamInternal(networkStream);
    }

    public static XmlDocument BuildXmlFromStream(this StringReader stringReader)
    {
        return BuildXmlFromStreamInternal(stringReader);
    }

    public static XmlDocument BuildXmlFromStream(this FileStream fileStream)
    {
        return BuildXmlFromStreamInternal(fileStream);
    }

    public static XmlDocument BuildXmlFromStream(this BufferedStream bufferedStream)
    {
        return BuildXmlFromStreamInternal(bufferedStream);
    }

    public static XmlDocument BuildXmlFromStream(this DirectoryInfo directoryInfo)
    {
        return BuildXmlFromStreamInternal(directoryInfo);
    }

    public static XmlDocument BuildXmlFromStream(this HttpResponseMessage httpResponse)
    {
        return BuildXmlFromStreamInternal(httpResponse);
    }

    public static XmlDocument BuildXmlFromStream(this HttpRequestMessage httpRequest)
    {
        return BuildXmlFromStreamInternal(httpRequest);
    }

    public static XmlDocument BuildXmlFromStream(this XElement xElement)
    {
        return BuildXmlFromStreamInternal(xElement);
    }

    public static XmlDocument BuildXmlFromStream(this XDocument xDocument)
    {
        return BuildXmlFromStreamInternal(xDocument);
    }

    public static XmlDocument BuildXmlFromStream(this XmlDocument xmlDocument)
    {
        return BuildXmlFromStreamInternal(xmlDocument);
    }
}