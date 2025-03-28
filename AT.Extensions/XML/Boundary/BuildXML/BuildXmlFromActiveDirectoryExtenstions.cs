using System.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromActiveDirectoryExtenstions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher? directorySearcher = new(searchRoot: new System.DirectoryServices.DirectoryEntry(serverUrl));
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("ActiveDirectoryData");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement userElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                          let userElement = xmlDocument.CreateElement("User")
                                                                                                          select (result, userElement))
            {
                userElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(userElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("is null or empty"))
        {
            throw new Exception("The provided server URL is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("is white space"))
        {
            throw new Exception("The provided server URL contains only white spaces.", ex);
        }
        catch (UriFormatException ex) when (ex.Message.Contains("Invalid URI"))
        {
            throw new Exception("The server URL format is incorrect.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to the Active Directory server is denied.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024891)) // COM error code for access issues
        {
            throw new Exception("There was an issue with accessing the Active Directory service. Check server permissions.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024894)) // COM error code for network issues
        {
            throw new Exception("There was a network issue while accessing the Active Directory service.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("The Active Directory query encountered an invalid response.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("search"))
        {
            throw new Exception("There was an issue with executing the search on Active Directory.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Properties"))
        {
            throw new Exception("The 'Properties' field in Active Directory search result is null.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while fetching Active Directory data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, String groupName)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentException.ThrowIfNullOrEmpty(groupName);
        ArgumentException.ThrowIfNullOrWhiteSpace(groupName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                Filter = $"(&(objectClass=user)(memberOf=CN={groupName}))"
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("GroupUsers");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement userElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                          let userElement = xmlDocument.CreateElement("User")
                                                                                                          select (result, userElement))
            {
                userElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(userElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null.", ex);
        }
        catch (ArgumentNullException ex) when (string.Equals(ex.ParamName, "groupName", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided group name is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("is null or empty"))
        {
            throw new Exception("The provided server URL or group name is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("is white space"))
        {
            throw new Exception("The provided server URL or group name contains only white spaces.", ex);
        }
        catch (UriFormatException ex) when (ex.Message.Contains("Invalid URI"))
        {
            throw new Exception("The server URL format is incorrect.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024891)) // COM error code for access issues
        {
            throw new Exception("There was an issue with accessing the Active Directory service. Check server permissions.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024894)) // COM error code for network issues
        {
            throw new Exception("There was a network issue while accessing the Active Directory service.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("The Active Directory query encountered an invalid response.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("Filter"))
        {
            throw new Exception("The filter used in the Active Directory search is incorrect or malformed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to the Active Directory server is denied. Ensure you have the correct permissions.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("search"))
        {
            throw new Exception("There was an issue with executing the search on Active Directory.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Properties"))
        {
            throw new Exception("The 'Properties' field in Active Directory search result is null.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while fetching group data from Active Directory.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, Int32 maxResults)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        // ----------------------------------------------------------------------------------------------------
        if (maxResults <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxResults), "Max results must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl));
            directorySearcher.SizeLimit = maxResults;
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("ActiveDirectoryResults");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase) == false)
        {
            throw new Exception("A parameter is null or empty.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (string.Equals(ex.ParamName, "maxResults", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The maximum results limit must be greater than 0.", ex);
        }
        catch (UriFormatException ex) when (ex.Message.Contains("Invalid URI"))
        {
            throw new Exception("The server URL format is incorrect.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024891)) // COM error code for access issues
        {
            throw new Exception("There was an issue with accessing the Active Directory service due to access permissions.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.ErrorCode.Equals(-2147024894)) // COM error code for network issues
        {
            throw new Exception("A network issue occurred while accessing the Active Directory service.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("The Active Directory query encountered an invalid response.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("SizeLimit"))
        {
            throw new Exception("The provided result size limit exceeds the allowable range in Active Directory.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to the Active Directory server is denied. Please verify your credentials or permissions.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("FindAll"))
        {
            throw new Exception("An error occurred while attempting to retrieve results from Active Directory.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Properties"))
        {
            throw new Exception("The 'Properties' field in Active Directory search result is null or missing.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while retrieving data from Active Directory.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, String[] fields)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentNullException.ThrowIfNull(fields);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl));
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("ActiveDirectoryUsers");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement userElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                          let userElement = xmlDocument.CreateElement("User")
                                                                                                          select (result, userElement))
            {
                foreach (string? field in from string field in fields
                                          where result.Properties.Contains(field)
                                          select field)
                {
                    userElement.SetAttribute(field, result.Properties[field][0].ToString());
                }

                root.AppendChild(userElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null or empty.", ex);
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "fields", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The fields parameter is null.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new Exception("One or more arguments provided are invalid or empty.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("access"))
        {
            throw new Exception("An error occurred while connecting to Active Directory. Please check the server URL and credentials.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("An invalid request was made to Active Directory. Please verify the query or filter.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("Server"))
        {
            throw new Exception("A COM error occurred while accessing Active Directory. Ensure the server is reachable and operational.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("Connection"))
        {
            throw new Exception("A connection error occurred while attempting to access Active Directory. Please check network settings.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to Active Directory was denied. Please check your permissions.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("format"))
        {
            throw new Exception("An error occurred while building the XML document. Please check the document format.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("parse"))
        {
            throw new Exception("An error occurred while parsing the XML document. Please ensure the XML structure is valid.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while retrieving data from Active Directory.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, String filter, Int32 maxResults)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentException.ThrowIfNullOrEmpty(filter);
        ArgumentException.ThrowIfNullOrWhiteSpace(filter);
        // ----------------------------------------------------------------------------------------------------
        if (maxResults <= 0) 
            throw new ArgumentOutOfRangeException(nameof(maxResults), "Max results must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                Filter = filter,
                SizeLimit = maxResults
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("FilteredResults");


            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null or empty.", ex);
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "filter", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided filter is null or empty.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (string.Equals(ex.ParamName, "maxResults", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The maxResults parameter must be greater than 0.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("access"))
        {
            throw new Exception("An error occurred while connecting to Active Directory. Please check the server URL and credentials.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("An invalid request was made to Active Directory. Please verify the query or filter.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("Server"))
        {
            throw new Exception("A COM error occurred while accessing Active Directory. Ensure the server is reachable and operational.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("Connection"))
        {
            throw new Exception("A connection error occurred while attempting to access Active Directory. Please check network settings.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to Active Directory was denied. Please check your permissions.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("name"))
        {
            throw new Exception("The specified attribute 'name' was not found in the Active Directory results.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("format"))
        {
            throw new Exception("An error occurred while building the XML document due to format issues.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("parse"))
        {
            throw new Exception("An error occurred while parsing the XML document. Please ensure the XML structure is valid.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while filtering Active Directory results.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, System.DirectoryServices.SearchScope searchScope)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                SearchScope = searchScope
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("SearchResults");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null or empty. Please ensure the URL is correctly specified.", ex);
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(serverUrl))
        {
            throw new Exception("The provided server URL is either null or contains only whitespace. Ensure the server URL is valid and correctly formatted.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("connection"))
        {
            throw new Exception("An error occurred while connecting to Active Directory. Please check the server URL and ensure the directory service is accessible.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("authentication"))
        {
            throw new Exception("An error occurred while authenticating to Active Directory. Ensure your credentials are correct.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("timeout"))
        {
            throw new Exception("A timeout error occurred while accessing Active Directory. Please ensure the server is reachable and operational.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("server"))
        {
            throw new Exception("A COM error occurred while accessing Active Directory. Ensure that the server is reachable and operational.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to Active Directory was denied. Please verify your credentials and permissions.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("name"))
        {
            throw new Exception("The specified attribute 'name' was not found in the Active Directory results. Please ensure that the results contain the required attribute.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("format"))
        {
            throw new Exception("An error occurred while constructing the XML document due to format issues.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("parse"))
        {
            throw new Exception("An error occurred while parsing the XML document. Ensure that the XML structure is valid and properly formatted.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while performing the search operation in Active Directory.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, String fieldName, String fieldValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentException.ThrowIfNullOrEmpty(fieldName);
        ArgumentException.ThrowIfNullOrWhiteSpace(fieldName);
        ArgumentException.ThrowIfNullOrEmpty(fieldValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(fieldValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                Filter = $"(&({fieldName}={fieldValue}))"
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("FilteredResults");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "serverUrl", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided server URL is null or empty. Please ensure the URL is correctly specified.", ex);
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "fieldName", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided field name is null or empty. Ensure that a valid field name is specified.", ex);
        }
        catch (ArgumentException ex) when (string.Equals(ex.ParamName, "fieldValue", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("The provided field value is null or empty. Ensure that a valid field value is specified.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(fieldName))
        {
            throw new Exception("The provided parameter 'fieldName' contains only whitespace. Ensure 'fieldName' is properly provided.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(fieldValue))
        {
            throw new Exception("The provided parameter 'fieldValue' contains only whitespace. Ensure 'fieldValue' is properly provided.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("connection"))
        {
            throw new Exception("An error occurred while connecting to Active Directory. Please check the server URL and ensure the directory service is accessible.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex) when (ex.Message.Contains("filter"))
        {
            throw new Exception("An error occurred while applying the filter in Active Directory. Please verify the filter syntax is correct.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("timeout"))
        {
            throw new Exception("A timeout error occurred while accessing Active Directory. Please ensure the server is reachable and operational.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex) when (ex.Message.Contains("server"))
        {
            throw new Exception("A COM error occurred while accessing Active Directory. Ensure that the server is reachable and operational.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access"))
        {
            throw new Exception("Access to Active Directory was denied. Please verify your credentials and permissions.", ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(fieldName))
        {
            throw new Exception("The provided parameter 'fieldName' is invalid or null. Please ensure 'fieldName' is properly specified.", ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(fieldValue))
        {
            throw new Exception("The provided parameter 'fieldValue' is invalid or null. Please ensure 'fieldValue' is properly specified.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("filter"))
        {
            throw new Exception("The format of the field value is incorrect. Ensure it is correctly formatted for Active Directory queries.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("name"))
        {
            throw new Exception("The specified attribute 'name' was not found in the Active Directory results. Please ensure that the results contain the required attribute.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid"))
        {
            throw new Exception("An error occurred while constructing the XML document due to an invalid structure.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("parse"))
        {
            throw new Exception("An error occurred while parsing the XML document. Ensure that the XML structure is valid and properly formatted.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while filtering Active Directory by field.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, System.DirectoryServices.SearchScope searchScope, String filter)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentException.ThrowIfNullOrEmpty(filter);
        ArgumentException.ThrowIfNullOrWhiteSpace(filter);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                Filter = filter,
                SearchScope = searchScope
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("FilteredResults");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(serverUrl))
        {
            throw new Exception("The provided 'serverUrl' parameter is null or empty. Please ensure the server URL is correctly specified.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(serverUrl))
        {
            throw new Exception("The provided 'serverUrl' parameter contains only whitespace. Please provide a valid server URL.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(filter))
        {
            throw new Exception("The provided 'filter' parameter is null or empty. Please ensure the filter is properly provided.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(filter))
        {
            throw new Exception("The provided 'filter' parameter contains only whitespace. Please provide a valid filter.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex)
        {
            throw new Exception("A directory services error occurred while accessing Active Directory. Please check the server URL and ensure the directory service is accessible.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex)
        {
            throw new Exception("A COM error occurred while communicating with Active Directory. Ensure that the Active Directory service is reachable and operational.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception("Access to Active Directory was denied. Please verify your credentials and permissions.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("FindAll"))
        {
            throw new Exception("An invalid operation was performed while retrieving search results from Active Directory. Verify the search parameters and try again.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Properties"))
        {
            throw new Exception("An unexpected null reference occurred while accessing properties in the search results. Ensure the search results contain valid entries.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new Exception("An error occurred while constructing the XML document from the Active Directory search results.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while performing the Active Directory search and constructing the XML document.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromActiveDirectory(this String serverUrl, Object condition)
    {
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUrl);
        ArgumentException.ThrowIfNullOrEmpty(condition.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(condition.ToString());
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.DirectoryServices.DirectorySearcher directorySearcher = new(new System.DirectoryServices.DirectoryEntry(serverUrl))
            {
                Filter = condition.ToString()
            };
            System.DirectoryServices.SearchResultCollection results = directorySearcher.FindAll();

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = xmlDocument.CreateElement("CustomFilteredResults");
            
            foreach ((System.DirectoryServices.SearchResult result, System.Xml.XmlElement entryElement) in from System.DirectoryServices.SearchResult result in results
                                                                                                           let entryElement = xmlDocument.CreateElement("Entry")
                                                                                                           select (result, entryElement))
            {
                entryElement.SetAttribute("Name", result.Properties["name"][0].ToString());
                root.AppendChild(entryElement);
            }

            xmlDocument.AppendChild(root);
            return xmlDocument;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(serverUrl))
        {
            throw new Exception("The provided 'serverUrl' parameter is null or empty. Please ensure the server URL is properly provided.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(serverUrl))
        {
            throw new Exception("The provided 'serverUrl' parameter contains only whitespace. Please provide a valid server URL.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(condition.ToString()))
        {
            throw new Exception("The provided 'condition' parameter is null or empty. Please ensure the condition is properly provided.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(condition.ToString()))
        {
            throw new Exception("The provided 'condition' parameter contains only whitespace. Please provide a valid condition.", ex);
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException ex)
        {
            throw new Exception("A directory services error occurred while accessing Active Directory. Please check if the server URL is correct and the directory service is accessible.", ex);
        }
        catch (System.Runtime.InteropServices.COMException ex)
        {
            throw new Exception("A COM error occurred while communicating with Active Directory. Ensure that the Active Directory service is reachable and operational.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception("Access to Active Directory was denied. Please verify your credentials and permissions to access the Active Directory.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("FindAll"))
        {
            throw new Exception("An invalid operation was performed while retrieving search results from Active Directory. Verify that the condition and search parameters are correct.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Properties"))
        {
            throw new Exception("An unexpected null reference occurred while accessing properties in the search results. Ensure that the search results contain valid entries with required properties.", ex);
        }
        catch (FormatException ex)
        {
            throw new Exception("The filter condition could not be converted to a valid string format. Please check the provided condition.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new Exception("An error occurred while constructing the XML document from the Active Directory search results. Ensure the XML structure is valid.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while filtering Active Directory by custom condition.", ex);
        }
    }
}