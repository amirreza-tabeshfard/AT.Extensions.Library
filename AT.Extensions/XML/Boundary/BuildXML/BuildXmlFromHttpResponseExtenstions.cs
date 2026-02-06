using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromHttpResponseExtenstions
{
    private static XDocument BuildXmlInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            if (input is HttpResponseMessage httpResponse)
                return BuildXmlInternal(httpResponse.Content);

            if (input is HttpContent httpContent)
                return BuildXmlInternal(httpContent.ReadAsStringAsync().GetAwaiter().GetResult());

            if (input is String str)
                return XDocument.Parse(str);

            if (input is Stream stream)
            {
                long originalPosition = 0;

                if (stream.CanSeek)
                {
                    originalPosition = stream.Position;
                    stream.Position = 0;
                }

                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, leaveOpen: true);

                if (stream.CanSeek)
                    stream.Position = originalPosition;

                return XDocument.Parse(reader.ReadToEnd());
            }

            if (input is XmlDocument xmlDoc)
                return XDocument.Parse(xmlDoc.OuterXml);

            if (input is XDocument xDoc)
                return xDoc;

            if (input is StringBuilder sb)
                return XDocument.Parse(sb.ToString());

            if (input is Byte[] bytes)
                return XDocument.Parse(Encoding.UTF8.GetString(bytes));

            if (input is Char[] chars)
                return XDocument.Parse(new String(chars));

            if (input is MemoryStream memStream)
            {
                long originalPosition = memStream.Position;
                memStream.Position = 0;

                using var reader = new StreamReader(memStream, Encoding.UTF8, true, 1024, leaveOpen: true);
                memStream.Position = originalPosition;
                return XDocument.Parse(reader.ReadToEnd());
            }

            if (input is TextReader textReader)
                return XDocument.Load(textReader);

            if (input is XmlReader xmlReader)
                return XDocument.Load(xmlReader);

            if (input is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException($"File not found: {file.FullName}");

                return XDocument.Load(file.FullName);
            }

            if (input is Uri uri)
            {
                using var client = new HttpClient();
                return XDocument.Parse(client.GetStringAsync(uri).GetAwaiter().GetResult());
            }

            throw new NotSupportedException($"The input type '{input.GetType()}' is not supported for XML conversion.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The provided input argument is null, which is not allowed. See inner exception for details.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"The specified file was not found: {ex.FileName}. See inner exception for details.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("HTTP request failed while trying to retrieve content. See inner exception for details.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML conversion. See inner exception for details.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse XML from the input provided. See inner exception for details.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the input stream or file. See inner exception for details.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML processing. See inner exception for details.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the file or resource is denied. See inner exception for details.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML. See inner exception for details.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromHttpResponseExtenstions
{

    public static XDocument BuildXmlFromHttpResponse(this HttpResponseMessage response)
    {
        return BuildXmlInternal(response);
    }

    public static XDocument BuildXmlFromHttpResponse(this String responseBody)
    {
        return BuildXmlInternal(responseBody);
    }

    public static XDocument BuildXmlFromHttpResponse(this Stream responseStream)
    {
        return BuildXmlInternal(responseStream);
    }

    public static XDocument BuildXmlFromHttpResponse(this HttpContent httpContent)
    {
        return BuildXmlInternal(httpContent);
    }

    public static XDocument BuildXmlFromHttpResponse(this XmlDocument xmlDoc)
    {
        return BuildXmlInternal(xmlDoc);
    }

    public static XDocument BuildXmlFromHttpResponse(this XDocument xDoc)
    {
        return BuildXmlInternal(xDoc);
    }

    public static XDocument BuildXmlFromHttpResponse(this StringBuilder stringBuilder)
    {
        return BuildXmlInternal(stringBuilder);
    }

    public static XDocument BuildXmlFromHttpResponse(this Byte[] byteArray)
    {
        return BuildXmlInternal(byteArray);
    }

    public static XDocument BuildXmlFromHttpResponse(this Char[] charArray)
    {
        return BuildXmlInternal(charArray);
    }

    public static XDocument BuildXmlFromHttpResponse(this MemoryStream memoryStream)
    {
        return BuildXmlInternal(memoryStream);
    }

    public static XDocument BuildXmlFromHttpResponse(this TextReader textReader)
    {
        return BuildXmlInternal(textReader);
    }

    public static XDocument BuildXmlFromHttpResponse(this XmlReader xmlReader)
    {
        return BuildXmlInternal(xmlReader);
    }

    public static XDocument BuildXmlFromHttpResponse(this FileInfo file)
    {
        return BuildXmlInternal(file);
    }

    public static XDocument BuildXmlFromHttpResponse(this Uri uri)
    {
        return BuildXmlInternal(uri);
    }

    public static XDocument BuildXmlFromHttpResponse(this Object obj)
    {
        return BuildXmlInternal(obj);
    }
}