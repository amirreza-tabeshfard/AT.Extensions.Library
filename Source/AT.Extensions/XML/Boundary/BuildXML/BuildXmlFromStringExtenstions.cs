using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromStringExtenstions
{
    private static XmlDocument BuildXmlFromStringPrivate(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            String content = null;

            if (input is String s)
                content = s;
            else if (input is StringBuilder sb)
                content = sb.ToString();
            else if (input is Char[] chars)
                content = new String(chars);
            else if (input is XmlDocument xd)
                content = xd.OuterXml;
            else if (input is XElement xe)
                content = xe.ToString();
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, leaveOpen: true);
                content = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                content = tr.ReadToEnd();
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("File does not exist.", fi.FullName);

                using var reader = new StreamReader(fi.FullName);
                content = reader.ReadToEnd();
            }
            else if (input is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new ArgumentException("Uri must be absolute.", nameof(input));

                using var client = new System.Net.WebClient();
                content = client.DownloadString(uri);
            }
            else if (input is Byte[] bytes)
                content = Encoding.UTF8.GetString(bytes);
            else if (input is MemoryStream ms)
                content = Encoding.UTF8.GetString(ms.ToArray());
            else if (input is NameValueCollection nvc)
            {
                var sbNvc = new StringBuilder();
                sbNvc.Append("<root>");

                foreach (var key in nvc.AllKeys)
                    sbNvc.Append("<item key=\"").Append(key).Append("\">").Append(nvc[key]).Append("</item>");

                sbNvc.Append("</root>");
                content = sbNvc.ToString();
            }
            else if (input is Dictionary<String, String> dict)
            {
                var sbDict = new StringBuilder();
                sbDict.Append("<root>");

                foreach (var kvp in dict)
                    sbDict.Append("<item key=\"").Append(kvp.Key).Append("\">").Append(kvp.Value).Append("</item>");

                sbDict.Append("</root>");
                content = sbDict.ToString();
            }
            else if (input is List<String> list)
            {
                var sbList = new StringBuilder();
                sbList.Append("<root>");

                foreach (var item in list)
                    sbList.Append("<item>").Append(item).Append("</item>");

                sbList.Append("</root>");
                content = sbList.ToString();
            }
            else if (input is StringReader sr)
                content = sr.ReadToEnd();
            else
                throw new ArgumentException("Unsupported input type: " + input.GetType().FullName);

            if (String.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Input content is null, empty, or whitespace.");

            var doc = new XmlDocument();
            doc.LoadXml(content);
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The provided input argument is invalid or unsupported.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("content"))
        {
            throw new InvalidOperationException("The content derived from input is null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input cannot be null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException("The specified file does not exist.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the input.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("unsupported"))
        {
            throw new InvalidOperationException("The input type is not supported for XML conversion.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException("A required stream or reader was already disposed.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse the String into XML. Ensure the content is valid XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from String.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromStringExtenstions
{
    public static XmlDocument BuildXmlFromString(this String input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this StringBuilder input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this Char[] input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this XmlDocument input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this XElement input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this Stream input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this TextReader input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this FileInfo input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this Uri input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this Byte[] input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this MemoryStream input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this NameValueCollection input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this Dictionary<String, String> input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this List<String> input)
    {
        return BuildXmlFromStringPrivate(input);
    }

    public static XmlDocument BuildXmlFromString(this StringReader input)
    {
        return BuildXmlFromStringPrivate(input);
    }
}