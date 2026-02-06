using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromMarkdownExtensions
{
    private static XDocument BuildXmlFromMarkdownInternal(Object input)
    {
        try
        {
            String markdownText = String.Empty;

            if (input is String s)
                markdownText = s;
            else if (input is FileInfo f)
            {
                if (!f.Exists)
                    throw new FileNotFoundException($"File not found: {f.FullName}");

                markdownText = File.ReadAllText(f.FullName);
            }
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream);
                markdownText = reader.ReadToEnd();
            }
            else if (input is TextReader reader)
            {
                markdownText = reader.ReadToEnd();
            }
            else if (input is IEnumerable<String> lines)
            {
                markdownText = String.Join(Environment.NewLine, lines);
            }
            else if (input is Byte[] bytes)
            {
                markdownText = System.Text.Encoding.UTF8.GetString(bytes);
            }
            else if (input is Uri uri)
            {
                using var client = new HttpClient();
                markdownText = client.GetStringAsync(uri).GetAwaiter().GetResult();
            }
            else if (input is IEnumerable<FileInfo> files)
            {
                List<String> combined = new List<String>();

                foreach (var file in files)
                {
                    if (!file.Exists)
                        throw new FileNotFoundException($"File not found: {file.FullName}");

                    combined.Add(File.ReadAllText(file.FullName));
                }
                markdownText = String.Join(Environment.NewLine, combined);
            }
            else
                throw new ArgumentException($"Unsupported input type: {input.GetType().FullName}");

            if (String.IsNullOrWhiteSpace(markdownText))
                throw new InvalidOperationException("Markdown content is empty or null");

            return ConvertMarkdownToXml(markdownText);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Argument provided to BuildXmlFromMarkdownInternal is not supported. Parameter: {ex.ParamName}", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Markdown file could not be found: {ex.FileName}", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException($"Failed to fetch Markdown content from URI. Source: {ex.Source}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Markdown content is empty or null"))
        {
            throw new InvalidOperationException("Markdown content provided is empty or null, cannot convert to XML.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"IO error occurred while reading Markdown content. Source: {ex.Source}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("Specified method is not supported"))
        {
            throw new InvalidOperationException("The operation attempted on the Markdown input is not supported.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error occurred while converting Markdown to XML: {ex.Message}", ex);
        }
    }

    private static XDocument ConvertMarkdownToXml(String markdown)
    {
        XDocument doc = new XDocument(new XElement("Markdown"));

        String[] lines = markdown.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        
        foreach (String line in lines)
        {
            if (line.StartsWith("#"))
                doc.Root.Add(new XElement("Heading", line.Trim()));
            else if (line.StartsWith("-") || line.StartsWith("*"))
                doc.Root.Add(new XElement("ListItem", line.Trim()));
            else if (!String.IsNullOrWhiteSpace(line))
                doc.Root.Add(new XElement("Paragraph", line.Trim()));
        }

        return doc;
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromMarkdownExtensions
{
    public static XDocument BuildXmlFromMarkdown(this String markdownContent)
    {
        return BuildXmlFromMarkdownInternal(markdownContent);
    }

    public static XDocument BuildXmlFromMarkdown(this FileInfo markdownFile)
    {
        return BuildXmlFromMarkdownInternal(markdownFile);
    }

    public static XDocument BuildXmlFromMarkdown(this Stream markdownStream)
    {
        return BuildXmlFromMarkdownInternal(markdownStream);
    }

    public static XDocument BuildXmlFromMarkdown(this TextReader markdownReader)
    {
        return BuildXmlFromMarkdownInternal(markdownReader);
    }

    public static XDocument BuildXmlFromMarkdown(this IEnumerable<String> markdownLines)
    {
        return BuildXmlFromMarkdownInternal(markdownLines);
    }

    public static XDocument BuildXmlFromMarkdown(this Uri markdownUri)
    {
        return BuildXmlFromMarkdownInternal(markdownUri);
    }

    public static XDocument BuildXmlFromMarkdown(this Byte[] markdownBytes)
    {
        return BuildXmlFromMarkdownInternal(markdownBytes);
    }

    public static XDocument BuildXmlFromMarkdown(this Object markdownObject)
    {
        return BuildXmlFromMarkdownInternal(markdownObject);
    }

    public static XDocument BuildXmlFromMarkdown(this List<String> markdownList)
    {
        return BuildXmlFromMarkdownInternal(markdownList);
    }

    public static XDocument BuildXmlFromMarkdown(this MemoryStream markdownMemoryStream)
    {
        return BuildXmlFromMarkdownInternal(markdownMemoryStream);
    }

    public static XDocument BuildXmlFromMarkdown(this StringReader markdownStringReader)
    {
        return BuildXmlFromMarkdownInternal(markdownStringReader);
    }

    public static XDocument BuildXmlFromMarkdown(this StreamReader markdownStreamReader)
    {
        return BuildXmlFromMarkdownInternal(markdownStreamReader);
    }

    public static XDocument BuildXmlFromMarkdown(this FileInfo[] markdownFiles)
    {
        return BuildXmlFromMarkdownInternal(markdownFiles);
    }

    public static XDocument BuildXmlFromMarkdown(this IEnumerable<FileInfo> markdownFiles)
    {
        return BuildXmlFromMarkdownInternal(markdownFiles);
    }

    public static XDocument BuildXmlFromMarkdown(this TextReader[] markdownReaders)
    {
        return BuildXmlFromMarkdownInternal(markdownReaders);
    }
}