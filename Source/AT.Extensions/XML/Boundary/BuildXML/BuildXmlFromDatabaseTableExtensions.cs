using System.Data;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromDatabaseTableExtensions
{
    private static XDocument Synchronize(Object source)
    {
        try
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            var table = ResolveToDataTable(source);

            if (table.Columns.Count == 0)
                throw new InvalidOperationException("Resolved DataTable contains no columns.");

            return BuildXml(table);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new ArgumentException("The provided source argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new ArgumentNullException("Source parameter was null during synchronization.", ex);
        }
        catch (DataException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new DataException("A data layer error occurred while resolving the database table.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("An invalid operation was detected while accessing database objects.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("Boundary.Xml"))
        {
            throw new NotSupportedException("The provided boundary type is not supported for XML synchronization.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("IDbCommand", StringComparison.Ordinal))
        {
            throw new ObjectDisposedException("Database command was disposed before execution.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("IDataReader", StringComparison.Ordinal))
        {
            throw new ObjectDisposedException("Data reader was disposed before table loading.", ex);
        }
        catch (DbException ex) when (ex.Source is not null && ex.Source.Equals("System.Data"))
        {
            throw new InvalidOperationException("A database provider error occurred during command execution.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("XML parsing failed while converting XML into DataTable.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("BuildXmlFromDatabaseTable failed while synchronizing database boundary objects.", ex);
        }
    }

    private static DataTable ResolveToDataTable(Object source)
    {
        switch (source)
        {
            case DataTable dt:
                return dt;

            case DataView dv:
                return dv.ToTable();

            case DataSet ds:
                {
                    if (ds.Tables.Count == 0)
                        throw new InvalidOperationException("DataSet does not contain any DataTable.");
                }
                return ds.Tables[0];

            case DataRow row:
                return row.Table ?? throw new InvalidOperationException("DataRow is not attached to a DataTable.");

            case DataRowCollection rows:
                {
                    if (rows.Count == 0)
                        throw new InvalidOperationException("DataRowCollection is empty.");
                }
                return rows[0].Table ?? throw new InvalidOperationException("DataRowCollection has no owning DataTable.");

            case IDataReader reader:
                return LoadFromReader(reader);

            case DbCommand dbCommand:
                return ExecuteCommand(dbCommand);

            case IDbCommand command:
                return ExecuteCommand(command);

            case DbDataAdapter adapter:
                return FillFromAdapter(adapter);

            case XmlDocument xml:
                return ConvertXmlToTable(XDocument.Parse(xml.OuterXml));

            case XDocument xdoc:
                return ConvertXmlToTable(xdoc);

            case System.Collections.IEnumerable enumerable:
                return ConvertEnumerableToTable(enumerable);

            case DbConnection:
            case IDbConnection:
                throw new InvalidOperationException("Connection alone is insufficient. A command or reader context is required.");

            default:
                throw new NotSupportedException($"Type '{source.GetType().FullName}' is not supported by BuildXmlFromDatabaseTable.");
        }
    }

    private static DataTable LoadFromReader(IDataReader reader)
    {
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    private static DataTable ExecuteCommand(IDbCommand command)
    {
        if (command.Connection == null)
            throw new InvalidOperationException("Command.Connection is null.");

        if (command.Connection.State != ConnectionState.Open)
            command.Connection.Open();

        using var reader = command.ExecuteReader();
        return LoadFromReader(reader);
    }

    private static DataTable FillFromAdapter(DbDataAdapter adapter)
    {
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static DataTable ConvertEnumerableToTable(System.Collections.IEnumerable enumerable)
    {
        var table = new DataTable("Items");
        table.Columns.Add("Value");

        foreach (Object item in enumerable)
            table.Rows.Add(item?.ToString());

        return table;
    }

    private static DataTable ConvertXmlToTable(XDocument doc)
    {
        if (doc.Root == null)
            throw new InvalidOperationException("XML document has no root element.");

        var table = new DataTable(doc.Root.Name.LocalName);
        bool initialized = false;

        foreach (var row in doc.Root.Elements())
        {
            if (!initialized)
            {
                foreach (var col in row.Elements())
                    table.Columns.Add(col.Name.LocalName);

                initialized = true;
            }

            var dr = table.NewRow();
            foreach (var col in row.Elements())
                dr[col.Name.LocalName] = col.Value;

            table.Rows.Add(dr);
        }

        return table;
    }

    private static XDocument BuildXml(DataTable table)
    {
        var root = new XElement(String.IsNullOrWhiteSpace(table.TableName) ? "Table" : table.TableName);

        foreach (DataRow row in table.Rows)
        {
            var rowElement = new XElement("Row");

            foreach (DataColumn col in table.Columns)
                rowElement.Add(new XElement(col.ColumnName, row[col]?.ToString()));

            root.Add(rowElement);
        }

        return new XDocument(root);
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromDatabaseTableExtensions
{
    public static XDocument BuildXmlFromDatabaseTable(this DataTable table)
    {
        return Synchronize(table);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DataView view)
    {
        return Synchronize(view);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DataSet dataSet)
    {
        return Synchronize(dataSet);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DbDataReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromDatabaseTable(this IDataReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DbCommand command)
    {
        return Synchronize(command);
    }

    public static XDocument BuildXmlFromDatabaseTable(this IDbCommand command)
    {
        return Synchronize(command);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DbDataAdapter adapter)
    {
        return Synchronize(adapter);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DataRow row)
    {
        return Synchronize(row);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DataRowCollection rows)
    {
        return Synchronize(rows);
    }

    public static XDocument BuildXmlFromDatabaseTable(this XmlDocument xml)
    {
        return Synchronize(xml);
    }

    public static XDocument BuildXmlFromDatabaseTable(this XDocument xml)
    {
        return Synchronize(xml);
    }

    public static XDocument BuildXmlFromDatabaseTable(this DbConnection connection)
    {
        return Synchronize(connection);
    }

    public static XDocument BuildXmlFromDatabaseTable(this IDbConnection connection)
    {
        return Synchronize(connection);
    }

    public static XDocument BuildXmlFromDatabaseTable(this System.Collections.IEnumerable enumerable)
    {
        return Synchronize(enumerable);
    }
}