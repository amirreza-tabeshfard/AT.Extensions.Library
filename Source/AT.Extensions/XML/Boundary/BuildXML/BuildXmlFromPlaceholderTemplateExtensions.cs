using System.Collections.Specialized;
using System.Data;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// public Class
/// </summary>
public static partial class BuildXmlFromPlaceholderTemplateExtensions
{
    public sealed class Boundary
    {
        public required String TemplateXml { get; set; }

        public required IDictionary<String, String> Placeholders { get; set; }
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromPlaceholderTemplateExtensions
{
    private static XDocument ConvertAndSynchronize(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference is null.");

            if (input is Boundary boundary)
            {
                if (String.IsNullOrWhiteSpace(boundary.TemplateXml))
                    throw new InvalidOperationException("Boundary.TemplateXml is empty or whitespace.");

                if (boundary.Placeholders is null)
                    throw new InvalidOperationException("Boundary.Placeholders is null.");

                var xml = boundary.TemplateXml;

                foreach (var kv in boundary.Placeholders)
                {
                    if (kv.Key is null)
                        continue;

                    var token = "{{" + kv.Key + "}}";
                    var value = kv.Value ?? String.Empty;
                    xml = xml.Replace(token, SecurityElement.Escape(value));
                }

                return XDocument.Parse(xml, LoadOptions.PreserveWhitespace);
            }

            if (input is XDocument xd)
                return new XDocument(xd);

            if (input is XElement xe)
                return new XDocument(xe);

            if (input is XmlDocument xmlDoc)
                return XDocument.Parse(xmlDoc.OuterXml, LoadOptions.PreserveWhitespace);

            if (input is XmlNode xmlNode)
                return XDocument.Parse(xmlNode.OuterXml, LoadOptions.PreserveWhitespace);

            if (input is XPathDocument xpd)
            {
                using var reader = xpd.CreateNavigator().ReadSubtree();
                return XDocument.Load(reader, LoadOptions.PreserveWhitespace);
            }

            if (input is XmlReader xr)
                return XDocument.Load(xr, LoadOptions.PreserveWhitespace);

            if (input is Stream stream)
            {
                using var sr = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                return XDocument.Parse(sr.ReadToEnd(), LoadOptions.PreserveWhitespace);
            }
            
            if (input is TextReader tr)
            {
                return XDocument.Parse(tr.ReadToEnd(), LoadOptions.PreserveWhitespace);
            }

            if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", fi.FullName);

                var text = File.ReadAllText(fi.FullName, Encoding.UTF8);
                return XDocument.Parse(text, LoadOptions.PreserveWhitespace);
            }

            if (input is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new InvalidOperationException("Uri must be absolute.");

                var text = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
                return XDocument.Parse(text, LoadOptions.PreserveWhitespace);
            }

            if (input is NameValueCollection nvc)
            {
                var root = new XElement("Root");

                foreach (var key in nvc.AllKeys)
                {
                    if (key is null)
                        continue;

                    root.Add(new XElement(key, nvc[key]));
                }

                return new XDocument(root);
            }

            if (input is DataTable dt)
            {
                using var sw = new StringWriter();
                dt.WriteXml(sw, XmlWriteMode.WriteSchema);
                return XDocument.Parse(sw.ToString(), LoadOptions.PreserveWhitespace);
            }

            if (input is DataSet ds)
            {
                using var sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                return XDocument.Parse(sw.ToString(), LoadOptions.PreserveWhitespace);
            }

            if (input is String str)
            {
                if (String.IsNullOrWhiteSpace(str))
                    throw new InvalidOperationException("Input String is empty or whitespace.");

                return XDocument.Parse(str, LoadOptions.PreserveWhitespace);
            }

            throw new NotSupportedException($"Unsupported input type: {input.GetType().FullName}");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input object cannot be null.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("Failed to convert DataTable or DataSet to XML.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"The specified file was not found: {ex.FileName}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("empty or whitespace"))
        {
            throw new InvalidOperationException("The provided XML string or template is empty or whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Placeholders is null"))
        {
            throw new InvalidOperationException("The boundary object's placeholders cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Uri must be absolute"))
        {
            throw new InvalidOperationException("The provided Uri is not absolute.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported input type"))
        {
            throw new InvalidOperationException("The input type is not supported for XML conversion.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse the XML content.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the input.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from placeholder template. See inner exception for exact cause.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromPlaceholderTemplateExtensions
{
    public static XDocument BuildXmlFromPlaceholderTemplate(this String input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XDocument input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XElement input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XmlDocument input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XmlNode input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XmlReader input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this XPathDocument input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this Stream input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this TextReader input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this FileInfo input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this Uri input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this NameValueCollection input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this DataTable input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this DataSet input)
    {
        return ConvertAndSynchronize(input);
    }

    public static XDocument BuildXmlFromPlaceholderTemplate(this Boundary input)
    {
        return ConvertAndSynchronize(input);
    }
}