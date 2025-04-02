using System.Data;

namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromDatabaseTableExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlFromDatabaseTable(this DataTable table)
    {
        ArgumentNullException.ThrowIfNull(table);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while processing the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input String was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while converting DataTable values to XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while iterating through DataTable rows or columns.", ex);
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while generating XML.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while processing the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static String BuildXmlFromDatabaseTable(this DataTable table, Boolean includeHeader)
    {
        ArgumentNullException.ThrowIfNull(table);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return includeHeader ? xml.ToString() : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Column name already belongs to this DataTable", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("A duplicate column name exists in the DataTable, which is not allowed for XML conversion.", ex);
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while generating XML.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while processing the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input String was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while converting DataTable values to XML.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while processing the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during XML generation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while iterating through DataTable rows or columns.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDatabaseTable(this DataTable table, List<String> columnNames)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(columnNames)))
        {
            throw new ArgumentNullException(nameof(columnNames), "The provided columnNames list is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Column name already belongs to this DataTable", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("A duplicate column name exists in the DataTable, which is not allowed for XML conversion.", ex);
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while generating XML.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while processing the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while converting DataTable values to XML.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while processing the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during XML generation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while iterating through DataTable rows or columns.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static String BuildXmlFromDatabaseTable(this DataTable table, List<String> columnNames, Boolean includeHeader)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return includeHeader ? xml.ToString() : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(columnNames)))
        {
            throw new ArgumentNullException(nameof(columnNames), "The provided columnNames list is null.");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Column name already belongs to this DataTable", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("A duplicate column name exists in the DataTable, which is not allowed for XML conversion.", ex);
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while generating XML.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while processing the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while converting DataTable values to XML.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while processing the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during XML generation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while iterating through DataTable rows or columns.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rowFilter)))
        {
            throw new ArgumentNullException(nameof(rowFilter), "The provided row filter function is null.");
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while filtering rows.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while filtering rows from the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while filtering the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during row filtering.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while filtering or accessing DataTable rows.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable row filtering.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static String BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter, Boolean includeHeader)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return includeHeader ? xml.ToString() : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rowFilter)))
        {
            throw new ArgumentNullException(nameof(rowFilter), "The provided row filter function is null.");
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while filtering rows.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while filtering rows from the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while filtering the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during row filtering.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while filtering or accessing DataTable rows.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable row filtering.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter, List<String> columnNames)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rowFilter)))
        {
            throw new ArgumentNullException(nameof(rowFilter), "The provided row filter function is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(columnNames)))
        {
            throw new ArgumentNullException(nameof(columnNames), "The provided column names list is null.");
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while filtering rows.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while filtering rows from the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while filtering the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during row filtering.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while filtering or accessing DataTable rows.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable row filtering.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static String BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter, List<String> columnNames, Boolean includeHeader)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return includeHeader ? xml.ToString() : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rowFilter)))
        {
            throw new ArgumentNullException(nameof(rowFilter), "The provided row filter function is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(columnNames)))
        {
            throw new ArgumentNullException(nameof(columnNames), "The provided column names list is null.");
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while filtering rows.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while filtering rows from the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while filtering the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during row filtering.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while filtering or accessing DataTable rows.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable row filtering.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter, List<String> columnNames, DateTime fromDate, DateTime toDate)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                              && row.Field<DateTime>("DateColumn") >= fromDate
                                                              && row.Field<DateTime>("DateColumn") <= toDate
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return xml;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while building XML from the database table", ex);
        }
    }

    public static String BuildXmlFromDatabaseTable(this DataTable table, Func<DataRow, Boolean> rowFilter, List<String> columnNames, DateTime fromDate, DateTime toDate, Boolean includeHeader)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(rowFilter);
        ArgumentNullException.ThrowIfNull(columnNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Table", from row in table.AsEnumerable()
                                                        where rowFilter(row)
                                                              && row.Field<DateTime>("DateColumn") >= fromDate
                                                              && row.Field<DateTime>("DateColumn") <= toDate
                                                        select new System.Xml.Linq.XElement("Row", from column in table.Columns.Cast<DataColumn>()
                                                                                                   where columnNames.Contains(column.ColumnName)
                                                                                                   select new System.Xml.Linq.XElement(column.ColumnName, row[column])));

            return includeHeader ? xml.ToString() : xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(table)))
        {
            throw new ArgumentNullException(nameof(table), "The provided DataTable is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rowFilter)))
        {
            throw new ArgumentNullException(nameof(rowFilter), "The provided row filter function is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(columnNames)))
        {
            throw new ArgumentNullException(nameof(columnNames), "The provided column names list is null.");
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(fromDate)))
        {
            throw new ArgumentOutOfRangeException(nameof(fromDate), "The 'fromDate' value is out of the acceptable range.");
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(toDate)))
        {
            throw new ArgumentOutOfRangeException(nameof(toDate), "The 'toDate' value is out of the acceptable range.");
        }
        catch (ConstraintException ex) when (ex.Message.Contains("This constraint cannot be enabled as not all values have unique values", StringComparison.OrdinalIgnoreCase))
        {
            throw new ConstraintException("A DataTable constraint violation occurred while filtering rows.", ex);
        }
        catch (DataException ex) when (ex.Message.Contains("Data error occurred", StringComparison.OrdinalIgnoreCase))
        {
            throw new DataException("A general data error occurred while filtering rows from the DataTable.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format", StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("A formatting error occurred while processing DataTable values.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Message.Contains("Index was outside the bounds of the array", StringComparison.OrdinalIgnoreCase))
        {
            throw new IndexOutOfRangeException("An index was out of range while accessing DataTable rows or columns.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("specified cast is not valid", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidCastException("A data type conversion error occurred while filtering the DataTable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The DataTable structure was modified during row filtering.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Enumeration has either not started or has already finished", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An issue occurred while enumerating through the DataTable rows.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary", StringComparison.OrdinalIgnoreCase))
        {
            throw new KeyNotFoundException("One or more specified column names do not exist in the DataTable.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object", StringComparison.OrdinalIgnoreCase))
        {
            throw new NullReferenceException("A null reference error occurred while filtering or accessing DataTable rows.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("arithmetic operation resulted in an overflow", StringComparison.OrdinalIgnoreCase))
        {
            throw new OverflowException("A numeric overflow occurred while processing DataTable values.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory to continue the execution", StringComparison.OrdinalIgnoreCase))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing DataTable row filtering.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("A DataTable column name is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Invalid characters were found in DataTable values during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML file while processing DataTable conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the database table.", ex);
        }
    }
}