using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromEnvironmentVariablesExtenstions
{
    private static XDocument BuildInternal(Object target)
    {
        try
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target), "Target instance cannot be null.");

            IDictionary variables = Environment.GetEnvironmentVariables();

            var root = new XElement("EnvironmentVariables");

            foreach (DictionaryEntry entry in variables)
            {
                var key = entry.Key?.ToString();

                if (String.IsNullOrWhiteSpace(key))
                    continue;

                var value = entry.Value?.ToString() ?? String.Empty;

                root.Add(new XElement("Variable", new XAttribute("Name", key), new XAttribute("Value", value)));
            }

            var result = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            if (target is XDocument xd)
            {
                xd.RemoveNodes();
                xd.Add(result.Root);
                return xd;
            }

            if (target is XElement xe)
            {
                xe.RemoveNodes();
                foreach (var n in result.Root!.Nodes())
                    xe.Add(n);

                return new XDocument(xe);
            }

            if (target is XmlDocument xmlDoc)
            {
                xmlDoc.RemoveAll();
                using var reader = result.CreateReader();
                xmlDoc.Load(reader);
                return XDocument.Parse(xmlDoc.OuterXml);
            }

            if (target is String path)
            {
                result.Save(path);
                return result;
            }

            if (target is FileInfo file)
            {
                result.Save(file.FullName);
                return result;
            }

            if (target is Stream stream)
            {
                stream.SetLength(0);
                result.Save(stream);
                return result;
            }

            if (target is TextWriter tw)
            {
                result.Save(tw);
                return result;
            }

            if (target is StringBuilder sb)
            {
                sb.Clear();
                using var sw = new StringWriter(sb);
                result.Save(sw);
                return result;
            }

            if (target is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file based URIs are supported.");

                result.Save(uri.LocalPath);
                return result;
            }

            if (target is DirectoryInfo dir)
            {
                if (!dir.Exists)
                    dir.Create();

                var filePath = System.IO.Path.Combine(dir.FullName, "environment.xml");
                result.Save(filePath);
                return result;
            }

            if (target is XmlWriter xw)
            {
                result.Save(xw);
                return result;
            }

            if (target is MemoryStream ms)
            {
                ms.SetLength(0);
                result.Save(ms);
                return result;
            }

            if (target is TextReader)
                return result;

            if (target is Assembly)
                return result;

            if (target is IServiceProvider)
                return result;

            throw new NotSupportedException($"The provided target type '{target.GetType().FullName}' is not supported.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("target", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An invalid argument was provided as target Object.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("target", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Target argument was null. A valid reference type instance is required.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The specified directory for writing environment.xml could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The specified file path does not exist while attempting to persist XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An invalid XML operation occurred while building the environment document.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An I/O error occurred while saving or writing the environment XML.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The provided target type is not supported by the environment XML builder.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A disposed stream or writer was used while generating environment XML.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The generated file path for environment.xml exceeds the maximum allowed length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Access to the target path or stream was denied while writing environment XML.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Malformed XML was produced while transforming environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from environment variables due to an unexpected runtime error. See inner exception for precise technical details.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromEnvironmentVariablesExtenstions
{
    public static XDocument BuildXmlFromEnvironmentVariables(this XDocument document)
    {
        return BuildInternal(document);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this XElement element)
    {
        return BuildInternal(element);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this XmlDocument document)
    {
        return BuildInternal(document);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this String filePath)
    {
        return BuildInternal(filePath);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this FileInfo fileInfo)
    {
        return BuildInternal(fileInfo);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this Stream stream)
    {
        return BuildInternal(stream);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this MemoryStream stream)
    {
        return BuildInternal(stream);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this TextWriter writer)
    {
        return BuildInternal(writer);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this TextReader reader)
    {
        return BuildInternal(reader);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this StringBuilder builder)
    {
        return BuildInternal(builder);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this Uri uri)
    {
        return BuildInternal(uri);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this DirectoryInfo directory)
    {
        return BuildInternal(directory);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this XmlWriter writer)
    {
        return BuildInternal(writer);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this Assembly assembly)
    {
        return BuildInternal(assembly);
    }

    public static XDocument BuildXmlFromEnvironmentVariables(this IServiceProvider serviceProvider)
    {
        return BuildInternal(serviceProvider);
    }
}