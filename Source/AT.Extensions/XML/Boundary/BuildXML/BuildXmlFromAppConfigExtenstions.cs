using System.Xml.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromAppConfigExtenstions
{
    private static XDocument BuildXmlInternal(Object configObject)
    {
        try
        {
            if (configObject == null)
                throw new ArgumentNullException(nameof(configObject), "Input configuration Object cannot be null.");

            XDocument xdoc = new(new XElement("Configuration"));

            if (configObject is NameValueCollection nameValues)
                foreach (String key in nameValues.AllKeys)
                {
                    xdoc.Root.Add(new XElement("AppSetting",
                        new XAttribute("Key", key),
                        new XAttribute("Value", nameValues[key] ?? String.Empty)));
                }
            else if (configObject is ConnectionStringSettingsCollection connections)
                foreach (ConnectionStringSettings conn in connections)
                {
                    if (conn == null)
                        continue;

                    xdoc.Root.Add(new XElement("ConnectionString",
                                                new XAttribute("Name", conn.Name),
                                                new XAttribute("ConnectionString", conn.ConnectionString ?? String.Empty),
                                                new XAttribute("ProviderName", conn.ProviderName ?? String.Empty)));
                }
            else if (configObject is ConnectionStringSettings conn)
            {
                xdoc.Root.Add(new XElement("ConnectionString",
                                            new XAttribute("Name", conn.Name),
                                            new XAttribute("ConnectionString", conn.ConnectionString ?? String.Empty),
                                            new XAttribute("ProviderName", conn.ProviderName ?? String.Empty)));
            }
            else if (configObject is KeyValueConfigurationElement kvElement)
            {
                xdoc.Root.Add(new XElement("AppSetting",
                                            new XAttribute("Key", kvElement.Key),
                                            new XAttribute("Value", kvElement.Value ?? String.Empty)));
            }
            else if (configObject is ConfigurationSection section)
            {
                var sectionAsCollection = ConfigurationManager.GetSection(section.SectionInformation.Name) as NameValueCollection;
                if (sectionAsCollection != null)
                {
                    XElement sectionNode = new XElement("CustomSection", new XAttribute("Name", section.SectionInformation.Name));

                    foreach (String key in sectionAsCollection.AllKeys)
                        sectionNode.Add(new XElement("Setting",
                                                      new XAttribute("Key", key),
                                                      new XAttribute("Value", sectionAsCollection[key] ?? String.Empty)));

                    xdoc.Root.Add(sectionNode);
                }
            }
            else
                throw new NotSupportedException($"Type '{configObject.GetType().FullName}' is not supported for XML conversion in .NET 8/9.");

            return xdoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(configObject)))
        {
            throw new InvalidOperationException($"Input configuration Object cannot be null. Parameter: {ex.ParamName}", ex);
        }
        catch (ConfigurationErrorsException ex) when (ex.Source is not null && ex.Source.Equals("System.Configuration"))
        {
            throw new InvalidOperationException($"Error reading configuration. Source: {ex.Source}", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Configuration"))
        {
            throw new InvalidOperationException($"Configuration section could not be cast to the expected type. Source: {ex.Source}", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("XmlExtensions"))
        {
            throw new InvalidOperationException($"Unsupported configuration Object type. Source: {ex.Source}", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Configuration"))
        {
            throw new InvalidOperationException($"Configuration contains an invalid format. Source: {ex.Source}", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals("System.Configuration"))
        {
            throw new InvalidOperationException($"Configuration contains a numeric value that is too large or too small. Source: {ex.Source}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XML from AppConfig. Detailed error: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromAppConfigExtenstions
{
    public static XDocument BuildXmlFromAppConfig(this Configuration config)
    {
        return BuildXmlInternal(config);
    }

    public static XDocument BuildXmlFromAppConfig(this KeyValueConfigurationCollection appSettings)
    {
        return BuildXmlInternal(appSettings);
    }

    public static XDocument BuildXmlFromAppConfig(this ConnectionStringSettingsCollection connectionStrings)
    {
        return BuildXmlInternal(connectionStrings);
    }

    public static XDocument BuildXmlFromAppConfig(this NameValueCollection nameValues)
    {
        return BuildXmlInternal(nameValues);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationSection section)
    {
        return BuildXmlInternal(section);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationSectionCollection sections)
    {
        return BuildXmlInternal(sections);
    }

    public static XDocument BuildXmlFromAppConfig(this ConnectionStringSettings connectionString)
    {
        return BuildXmlInternal(connectionString);
    }

    public static XDocument BuildXmlFromAppConfig(this KeyValueConfigurationElement element)
    {
        return BuildXmlInternal(element);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationElement element)
    {
        return BuildXmlInternal(element);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationElementCollection elements)
    {
        return BuildXmlInternal(elements);
    }

    public static XDocument BuildXmlFromAppConfig(this IConfigurationSectionHandler handler)
    {
        return BuildXmlInternal(handler);
    }

    public static XDocument BuildXmlFromAppConfig(this AppSettingsSection section)
    {
        return BuildXmlInternal(section);
    }

    public static XDocument BuildXmlFromAppConfig(this ConnectionStringsSection section)
    {
        return BuildXmlInternal(section);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationElement element, String name)
    {
        return BuildXmlInternal(element);
    }

    public static XDocument BuildXmlFromAppConfig(this ConfigurationSection section, String name)
    {
        return BuildXmlInternal(section);
    }
}