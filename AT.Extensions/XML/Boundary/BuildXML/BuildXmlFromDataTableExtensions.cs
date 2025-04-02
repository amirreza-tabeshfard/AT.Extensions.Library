using System.Data;

namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromDataTableExtensions
    : Object
{
    public static String BuildXmlFromDataTable(this DataTable dataTable)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    cellElement.InnerText = row[column].ToString();
                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("The provided DataTable is null.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("Cannot cast"))
        {
            throw new InvalidOperationException("There was an issue with casting data during XML generation.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("DataColumn"))
        {
            throw new InvalidOperationException("One or more columns in the DataTable are invalid or incompatible with XML generation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set"))
        {
            throw new InvalidOperationException("A required object was not properly initialized during the XML generation process.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("XML"))
        {
            throw new InvalidOperationException("There was an error while creating or manipulating the XML document.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid format"))
        {
            throw new InvalidOperationException("There was an invalid format encountered while generating the XML.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("DataRow"))
        {
            throw new InvalidOperationException("An issue occurred while processing a DataRow during the XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from DataTable.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, Boolean formattedOutput)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootElement);

            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    cellElement.InnerText = row[column].ToString();
                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            System.Xml.XmlWriterSettings settings = new()
            {
                Indent = formattedOutput
            };

            using StringWriter stringWriter = new();
            using System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringWriter, settings);
            xmlDocument.WriteTo(xmlWriter);
            return stringWriter.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("The operation is not valid due to the current state of the object"))
        {
            throw new InvalidOperationException("Invalid operation during XML document creation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("column"))
        {
            throw new InvalidOperationException("Column index out of range in DataRow.", ex);
        }
        catch (FormatException ex) when (ex.Message.Equals("Invalid format"))
        {
            throw new InvalidOperationException("Error while converting cell value to string during XML creation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building XML from DataTable.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, String rootElementName)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    cellElement.InnerText = row[column].ToString();
                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("column"))
        {
            throw new InvalidOperationException("Column index out of range in DataRow.", ex);
        }
        catch (FormatException ex) when (ex.Message.Equals("Invalid format"))
        {
            throw new InvalidOperationException("Error while converting cell value to string during XML creation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building XML from DataTable with custom root name.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, String rootElementName, String[] columnNames)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    cellElement.InnerText = row[column].ToString();
                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("columnNames"))
        {
            throw new InvalidOperationException("Column names array cannot be null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("column"))
        {
            throw new InvalidOperationException("Column index out of range in DataRow.", ex);
        }
        catch (FormatException ex) when (ex.Message.Equals("Invalid format"))
        {
            throw new InvalidOperationException("Error while converting cell value to string during XML creation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no matching element"))
        {
            throw new InvalidOperationException("No matching columns found in the DataTable for the provided column names.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building filtered XML from DataTable.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, String rootElementName, Dictionary<String, String> columnDataTypes)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(columnDataTypes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    if (columnDataTypes.TryGetValue(column.ColumnName, out string? dataType))
                    {
                        switch (dataType)
                        {
                            case "int" when int.TryParse(row[column].ToString(), out int intValue):
                                {
                                    cellElement.InnerText = intValue.ToString();
                                }
                                break;

                            case "datetime" when DateTime.TryParse(row[column].ToString(), out DateTime dateValue):
                                {
                                    cellElement.InnerText = dateValue.ToString("yyyy-MM-dd");
                                }
                                break;
                        }
                    }
                    else
                        cellElement.InnerText = row[column].ToString();

                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("columnDataTypes"))
        {
            throw new InvalidOperationException("Column data types dictionary cannot be null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("column"))
        {
            throw new InvalidOperationException("Column index out of range in DataRow.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("Error while parsing data for column in the DataRow.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary"))
        {
            throw new InvalidOperationException("No entry found in columnDataTypes for the specified column.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("Specified cast is not valid"))
        {
            throw new InvalidOperationException("Error while casting data to the expected type for column.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building XML from DataTable with custom column data types.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, String rootElementName, IEnumerable<String> requiredColumns)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(requiredColumns);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   where requiredColumns.Contains(column.ColumnName)
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    cellElement.InnerText = row[column].ToString();
                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("requiredColumns"))
        {
            throw new InvalidOperationException("Required columns collection cannot be null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("column"))
        {
            throw new InvalidOperationException("Column index out of range in DataRow.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary"))
        {
            throw new InvalidOperationException("A required column is missing from the DataTable.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("Specified cast is not valid"))
        {
            throw new InvalidOperationException("Error while casting data to the expected type for column.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building XML with required columns from DataTable.", ex);
        }
    }

    public static String BuildXmlFromDataTable(this DataTable dataTable, String rootElementName, String dataFormat)
    {
        ArgumentNullException.ThrowIfNull(dataTable);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(dataFormat);
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFormat);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            xmlDocument.AppendChild(rootElement);
            
            foreach ((DataRow row, System.Xml.XmlElement rowElement) in from DataRow row in dataTable.Rows
                                                                        let rowElement = xmlDocument.CreateElement("Row")
                                                                        select (row, rowElement))
            {
                foreach ((DataColumn column, System.Xml.XmlElement cellElement) in from DataColumn column in dataTable.Columns
                                                                                   let cellElement = xmlDocument.CreateElement(column.ColumnName)
                                                                                   select (column, cellElement))
                {
                    switch (dataFormat.ToLower())
                    {
                        case "uppercase":
                            {
                                cellElement.InnerText = row[column].ToString().ToUpper();
                            }
                            break;

                        case "lowercase":
                            {
                                cellElement.InnerText = row[column].ToString().ToLower();
                            }
                            break;
                    }

                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataTable"))
        {
            throw new InvalidOperationException("DataTable parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataFormat") && ex.Message.Contains("Value cannot be null or empty"))
        {
            throw new InvalidOperationException("Data format cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dataFormat") && ex.Message.Contains("Value cannot be whitespace"))
        {
            throw new InvalidOperationException("Data format cannot be whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error while creating XML element"))
        {
            throw new InvalidOperationException("Error while creating XML element during the building process.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid format"))
        {
            throw new InvalidOperationException("The specified data format is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error while building formatted XML"))
        {
            throw new InvalidOperationException("Error while building formatted XML from DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("Null reference encountered during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while building formatted XML from DataTable.", ex);
        }
    }
}