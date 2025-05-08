namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromIniExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root", new System.Xml.Linq.XElement("FilePath", iniFilePath));
            return xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException($"The INI file path is invalid: {ex.ParamName}", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException($"The INI file path is empty or consists only of whitespace: {ex.ParamName}", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("You do not have permission to access the INI file.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while processing the INI file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, String rootElementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new(rootElementName, new System.Xml.Linq.XElement("FilePath", iniFilePath));
            return xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException("The iniFilePath argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The rootElementName argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException("An ArgumentException was thrown. Please check the argument values.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("There was an issue with the format of the input data.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An error occurred while reading the ini file.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding ArgumentException.", ex);
        }
        catch (Exception ex) when (ex is not FormatException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding FormatException.", ex);
        }
        catch (Exception ex) when (ex is not System.IO.IOException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding IOException.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this Dictionary<String, String> settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");
            
            foreach (KeyValuePair<String, String> setting in settings)
                xml.Add(new System.Xml.Linq.XElement(setting.Key, setting.Value));
            
            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("settings"))
        {
            throw new InvalidOperationException("The settings argument cannot be null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A key was not found during processing the settings.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an issue creating the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation was attempted during XML creation.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding ArgumentNullException.", ex);
        }
        catch (Exception ex) when (ex is not KeyNotFoundException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding KeyNotFoundException.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding XmlException.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, Boolean prettyFormat)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root", new System.Xml.Linq.XElement("FilePath", iniFilePath));
            return prettyFormat ? new System.Xml.Linq.XElement("Root", new System.Xml.Linq.XText(xml.ToString())) : xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException("The iniFilePath argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an issue creating the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation was attempted while processing the XML.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding ArgumentException.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding XmlException.", ex);
        }
        catch (Exception ex) when (ex is not System.InvalidOperationException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding InvalidOperationException.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, Int32 maxSections)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new System.Xml.Linq.XElement("Root", new System.Xml.Linq.XElement("MaxSections", maxSections));
            return xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException("The iniFilePath argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an issue creating the XML structure.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The value provided for MaxSections is not in the correct format.", ex);
        }
        catch (OverflowException ex)
        {
            throw new InvalidOperationException("The MaxSections value exceeds the allowable range.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation was attempted while processing the XML.", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding ArgumentException.", ex);
        }
        catch (Exception ex) when (ex is not System.Xml.XmlException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding XmlException.", ex);
        }
        catch (Exception ex) when (ex is not FormatException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding FormatException.", ex);
        }
        catch (Exception ex) when (ex is not OverflowException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding OverflowException.", ex);
        }
        catch (Exception ex) when (ex is not System.InvalidOperationException)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding InvalidOperationException.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, List<String> sections)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");
            
            foreach (String section in sections)
                xml.Add(new System.Xml.Linq.XElement("Section", section));
            
            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sections"))
        {
            throw new InvalidOperationException("The sections argument cannot be null.", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new InvalidOperationException("The sections list contains values outside the expected range.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException("The iniFilePath argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException("An unexpected error occurred, excluding ArgumentException.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation was attempted while processing the XML.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an issue creating the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, String sectionFilter, String keyFilter)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        ArgumentException.ThrowIfNullOrEmpty(sectionFilter);
        ArgumentException.ThrowIfNullOrWhiteSpace(sectionFilter);
        ArgumentException.ThrowIfNullOrEmpty(keyFilter);
        ArgumentException.ThrowIfNullOrWhiteSpace(keyFilter);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root", new System.Xml.Linq.XElement("SectionFilter", sectionFilter), new System.Xml.Linq.XElement("KeyFilter", keyFilter));
            return xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("iniFilePath"))
        {
            throw new InvalidOperationException("The iniFilePath argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sectionFilter"))
        {
            throw new InvalidOperationException("The sectionFilter argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyFilter"))
        {
            throw new InvalidOperationException("The keyFilter argument is either null, empty, or contains only whitespace.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an issue creating the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromIni(this String iniFilePath, Func<String, Boolean> sectionProcessor)
    {
        ArgumentException.ThrowIfNullOrEmpty(iniFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(iniFilePath);
        ArgumentNullException.ThrowIfNull(sectionProcessor);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");
            IEnumerable<String> sections = File.ReadLines(iniFilePath);
            
            foreach (String? section in from section in sections
                                        where sectionProcessor(section)
                                        select section)
            {
                xml.Add(new System.Xml.Linq.XElement("ProcessedSection", section));
            }

            return xml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException("The provided file path is invalid or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException("The sectionProcessor function cannot be null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null)
        {
            throw new InvalidOperationException($"The INI file was not found: {ex.FileName}", ex);
        }
        catch (FileNotFoundException ex) when (!File.Exists(iniFilePath))
        {
            throw new InvalidOperationException($"The INI file was not found at the specified path: {iniFilePath}", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException($"The specified ini file was not found: {ex.FileName}", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("You do not have permission to access the specified file.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }
}