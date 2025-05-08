namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromEnvironmentVariablesExtenstions
    : Object
{
    public static String BuildXmlFromEnvironmentVariables(this String xmlRoot)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
            System.Xml.Linq.XElement root = new(xmlRoot);

            foreach (Object? key in envVars.Keys)
                root.Add(new System.Xml.Linq.XElement(name: key.ToString(), content: envVars[key]?.ToString()));

            return root.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from environment variables.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromEnvironmentVariables(this String xmlRoot, IEnumerable<String> keys)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(keys);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(xmlRoot);
            
            foreach ((String key, String value) in from key in keys
                                                   let value = Environment.GetEnvironmentVariable(key)
                                                   where value != null
                                                   select (key, value))
            {
                root.Add(new System.Xml.Linq.XElement(key, value));
            }

            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keys"))
        {
            throw new ArgumentNullException("The keys collection cannot be null.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while processing the keys collection.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while generating XML from environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from environment variables with specified keys.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromEnvironmentVariables(this String xmlRoot, Dictionary<String, String> defaultValues)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(defaultValues);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(xmlRoot);
            
            foreach ((String key, String value) in from key in defaultValues.Keys
                                                   let value = Environment.GetEnvironmentVariable(key) ?? defaultValues[key]
                                                   select (key, value))
            {
                root.Add(new System.Xml.Linq.XElement(key, value));
            }

            return root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("defaultValues"))
        {
            throw new ArgumentNullException("The defaultValues dictionary cannot be null.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A specified key was not found in the defaultValues dictionary.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while accessing default values.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while generating XML from environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from environment variables with default values.", ex);
        }
    }

    public static byte[] BuildXmlFromEnvironmentVariables(this String xmlRoot, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return encoding.GetBytes(BuildXmlFromEnvironmentVariables(xmlRoot));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new ArgumentNullException("The encoding parameter cannot be null.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("An encoding error occurred while converting the XML to bytes.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while processing the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while generating the XML byte array.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the XML byte array from environment variables.", ex);
        }
    }

    public static String BuildXmlFromEnvironmentVariables(this String xmlRoot, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
            System.Xml.Linq.XElement root = new(xmlRoot);
            
            foreach (Object? key in from Object? key in envVars.Keys
                                    where key.ToString().StartsWith(prefix)
                                    select key)
            {
                root.Add(new System.Xml.Linq.XElement(key.ToString(), envVars[key]?.ToString()));
            }

            return root.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("prefix"))
        {
            throw new ArgumentNullException("The prefix parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("prefix"))
        {
            throw new ArgumentException("The prefix parameter cannot be empty or whitespace.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while processing the XML structure.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while generating the XML String.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the XML String from environment variables.", ex);
        }
    }

    public static Dictionary<String, String> BuildXmlFromEnvironmentVariables(this String xmlRoot, IEnumerable<String> keys, String defaultValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(keys);
        ArgumentException.ThrowIfNullOrEmpty(defaultValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(defaultValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Dictionary<String, String> result = new();
            
            foreach (String key in keys)
                result[key] = Environment.GetEnvironmentVariable(key) ?? defaultValue;
            
            return result;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keys"))
        {
            throw new ArgumentNullException("The keys parameter cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("defaultValue"))
        {
            throw new ArgumentNullException("The defaultValue parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("defaultValue"))
        {
            throw new ArgumentException("The defaultValue parameter cannot be empty or whitespace.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while retrieving environment variable values.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A specified key was not found in the environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while retrieving environment variable values.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while retrieving environment variable values.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromEnvironmentVariables(this String xmlRoot, System.Text.RegularExpressions.Regex regex)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(regex);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
            System.Xml.Linq.XElement root = new(xmlRoot);
            
            foreach (Object? key in from Object? key in envVars.Keys
                                    where regex.IsMatch(key.ToString())
                                    select key)
            {
                root.Add(new System.Xml.Linq.XElement(key.ToString(), envVars[key]?.ToString()));
            }

            return root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("regex"))
        {
            throw new ArgumentNullException("The regex parameter cannot be null.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Access to environment variables is denied.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to access environment variables.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while processing environment variables.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("A specified key was not found in the environment variables.", ex);
        }
        catch (System.Text.RegularExpressions.RegexMatchTimeoutException ex)
        {
            throw new InvalidOperationException("The regex operation timed out while matching environment variables.", ex);
        }
        catch (System.Runtime.InteropServices.ExternalException ex)
        {
            throw new InvalidOperationException("An external error occurred while accessing environment variables.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing environment variables.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while filtering environment variables using regex.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while filtering environment variables using regex.", ex);
        }
    }

    public static void BuildXmlFromEnvironmentVariables(this String xmlRoot, Stream outputStream)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        ArgumentNullException.ThrowIfNull(outputStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlString = BuildXmlFromEnvironmentVariables(xmlRoot);
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            outputStream.Write(bytes, 0, bytes.Length);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("outputStream"))
        {
            throw new ArgumentNullException("The outputStream parameter cannot be null.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while writing XML to the stream.", ex);
        }
        catch (ObjectDisposedException ex)
        {
            throw new InvalidOperationException("The output stream has been disposed and cannot be written to.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The output stream does not support writing.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("The process does not have permission to write to the output stream.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security error occurred while accessing the output stream.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the XML data.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("An error occurred during UTF-8 encoding of the XML string.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while writing XML to the stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while writing XML to the stream.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromEnvironmentVariables(this String xmlRoot, Int32 maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlRoot);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlRoot);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
            
            foreach ((Object key, String value) in from Object? key in envVars.Keys
                                                   let value = envVars[key]?.ToString()
                                                   where !String.IsNullOrEmpty(value) && value.Length <= maxLength
                                                   select (key, value))
            {
                new System.Xml.Linq.XElement(xmlRoot).Add(new System.Xml.Linq.XElement(key.ToString(), value));
            }

            return new System.Xml.Linq.XElement(xmlRoot);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentNullException("The xmlRoot parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlRoot"))
        {
            throw new ArgumentException("The xmlRoot parameter cannot be empty or whitespace.", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new InvalidOperationException("The provided maxLength is out of a valid range.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Environment"))
        {
            throw new InvalidOperationException("An error occurred while accessing environment variables.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while processing environment variables.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException("The key was not found in the environment variables collection.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("There was an error in processing one of the environment variable values.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the XML structure.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference occurred while processing the environment variables.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML based on the max length filter.", ex);
        }
    }
}