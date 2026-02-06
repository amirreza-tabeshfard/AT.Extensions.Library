using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromDataTableExtensions
{
    private static XDocument ConvertInternal(Object source)
    {
        try
        {
            var dataSet = NormalizeToDataSet(source);

            var root = new XElement("DataSet");
            
            foreach (var (table, tableElement) in from DataTable table in dataSet.Tables
                                                  let tableName = String.IsNullOrWhiteSpace(table.TableName) ? "Table" : table.TableName
                                                  let tableElement = new XElement(tableName)
                                                  select (table, tableElement))
            {
                foreach (var (row, rowElement) in from DataRow row in table.Rows
                                                  let rowElement = new XElement("Row")
                                                  select (row, rowElement))
                {
                    foreach (DataColumn column in table.Columns)
                        rowElement.Add(new XElement(column.ColumnName, row[column] == DBNull.Value ? null : row[column]));

                    tableElement.Add(rowElement);
                }

                root.Add(tableElement);
            }

            return new XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rows"))
        {
            throw new InvalidOperationException($"XML synchronization failed: DataRow array cannot be empty. Parameter='{ex.ParamName}'.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException($"XML synchronization failed: Source Object cannot be null. Parameter='{ex.ParamName}'.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("DataRelationCollection is empty; cannot determine parent DataSet."))
        {
            throw new InvalidOperationException("XML synchronization failed: DataRelationCollection is empty; cannot determine owning DataSet.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Unable to determine owning DataSet from DataRelationCollection."))
        {
            throw new InvalidOperationException("XML synchronization failed: Cannot determine owning DataSet from DataRelationCollection.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Cannot determine owning DataTable from DataColumnCollection."))
        {
            throw new InvalidOperationException("XML synchronization failed: Cannot determine owning DataTable from DataColumnCollection.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("DataViewManager does not contain a DataSet."))
        {
            throw new InvalidOperationException("XML synchronization failed: DataViewManager has no associated DataSet.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("DataViewSettingCollection is configuration-only and cannot be converted to XML."))
        {
            throw new InvalidOperationException("XML synchronization failed: DataViewSettingCollection cannot be converted to XML.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("DataTableMappingCollection is metadata-only and cannot be synchronized."))
        {
            throw new InvalidOperationException("XML synchronization failed: DataTableMappingCollection cannot be synchronized to XML.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("DataViewSetting represents configuration and cannot be converted to DataTable."))
        {
            throw new InvalidOperationException("XML synchronization failed: DataViewSetting cannot be converted to DataTable.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"XML synchronization failed: Unexpected error. SourceType='{source.GetType().FullName}'. Detail='{ex}'", ex);
        }
    }

    private static DataSet NormalizeToDataSet(Object source)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source is DataTableReader tableReader)
        {
            var trTable = new DataTable("TableReader");
            
            for (int i = 0; i < tableReader.FieldCount; i++)
                trTable.Columns.Add(tableReader.GetName(i), tableReader.GetFieldType(i));

            while (tableReader.Read())
            {
                var row = trTable.NewRow();
                for (int i = 0; i < tableReader.FieldCount; i++)
                    row[i] = tableReader.GetValue(i);
                trTable.Rows.Add(row);
            }

            return NormalizeToDataSet(trTable);
        }

        if (source is DataTable dt)
        {
            var ds1 = new DataSet("NormalizedDataSet");
            ds1.Tables.Add(dt.Copy());
            return ds1;
        }

        if (source is DataSet ds)
            return ds.Copy();

        if (source is DataView dv)
            return NormalizeToDataSet(dv.ToTable());

        if (source is DataRow dr)
            return NormalizeToDataSet(dr.Table);

        if (source is DataRow[] rows)
        {
            if (rows.Length == 0) 
                throw new ArgumentException("DataRow array is empty.");

            var temp = rows[0].Table.Clone();
            
            foreach (var r in rows) 
                temp.ImportRow(r);
            
            return NormalizeToDataSet(temp);
        }

        if (source is IDataReader reader)
        {
            var readerTable = new DataTable("ReaderTable");
            readerTable.Load(reader);
            return NormalizeToDataSet(readerTable);
        }

        if (source is DataTableCollection tables)
        {
            var dsTables = new DataSet("TableCollectionDataSet");
            
            for (int i = 0; i < tables.Count; i++)
                dsTables.Tables.Add(tables[i].Copy());
            
            return dsTables;
        }

        if (source is DataRelationCollection relations)
        {
            if (relations.Count == 0)
                throw new InvalidOperationException("DataRelationCollection is empty; cannot determine parent DataSet.");
            
            var firstRelation = relations[0];
            
            if (firstRelation.ParentTable?.DataSet == null)
                throw new InvalidOperationException("Unable to determine owning DataSet from DataRelationCollection.");
            
            return NormalizeToDataSet(firstRelation.ParentTable.DataSet);
        }

        if (source is DataColumnCollection columns)
        {
            if (columns.Count == 0 || columns[0].Table == null)
                throw new InvalidOperationException("Cannot determine owning DataTable from DataColumnCollection.");
            
            return NormalizeToDataSet(columns[0].Table);
        }

        if (source is DataViewManager manager)
        {
            if (manager.DataSet == null)
                throw new InvalidOperationException("DataViewManager does not contain a DataSet.");
           
            return NormalizeToDataSet(manager.DataSet);
        }

        if (source is DataViewSettingCollection)
            throw new NotSupportedException("DataViewSettingCollection is configuration-only and cannot be converted to XML.");

        if (source is IDataAdapter adapter)
        {
            var dsAdapter = new DataSet("AdapterDataSet");
            adapter.Fill(dsAdapter);
            return dsAdapter;
        }

        if (source is DataTableMappingCollection)
            throw new NotSupportedException("DataTableMappingCollection is metadata-only and cannot be synchronized.");

        if (source is DataViewSetting)
            throw new NotSupportedException("DataViewSetting represents configuration and cannot be converted to DataTable.");

        throw new NotSupportedException($"Unsupported source type: {source.GetType().FullName}");
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromDataTableExtensions
{
    public static XDocument BuildXmlFromDataTable(this DataTable table)
    {
        ArgumentNullException.ThrowIfNull(table);
        return ConvertInternal(table);
    }

    public static XDocument BuildXmlFromDataTable(this DataSet dataSet)
    {
        ArgumentNullException.ThrowIfNull(dataSet);
        return ConvertInternal(dataSet);
    }

    public static XDocument BuildXmlFromDataTable(this DataView view)
    {
        ArgumentNullException.ThrowIfNull(view);
        return ConvertInternal(view);
    }

    public static XDocument BuildXmlFromDataTable(this DataRow row)
    {
        ArgumentNullException.ThrowIfNull(row);
        return ConvertInternal(row);
    }

    public static XDocument BuildXmlFromDataTable(this DataRow[] rows)
    {
        ArgumentNullException.ThrowIfNull(rows);
        return ConvertInternal(rows);
    }

    public static XDocument BuildXmlFromDataTable(this IDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return ConvertInternal(reader);
    }

    public static XDocument BuildXmlFromDataTable(this DataTableCollection tables)
    {
        ArgumentNullException.ThrowIfNull(tables);
        return ConvertInternal(tables);
    }

    public static XDocument BuildXmlFromDataTable(this DataRelationCollection relations)
    {
        ArgumentNullException.ThrowIfNull(relations);
        return ConvertInternal(relations);
    }

    public static XDocument BuildXmlFromDataTable(this DataColumnCollection columns)
    {
        ArgumentNullException.ThrowIfNull(columns);
        return ConvertInternal(columns);
    }

    public static XDocument BuildXmlFromDataTable(this DataViewManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        return ConvertInternal(manager);
    }

    public static XDocument BuildXmlFromDataTable(this DataViewSettingCollection settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return ConvertInternal(settings);
    }

    public static XDocument BuildXmlFromDataTable(this DataTableReader tableReader)
    {
        ArgumentNullException.ThrowIfNull(tableReader);
        return ConvertInternal(tableReader);
    }
    
    public static XDocument BuildXmlFromDataTable(this IDataAdapter adapter)
    {
        ArgumentNullException.ThrowIfNull(adapter);
        return ConvertInternal(adapter);
    }

    public static XDocument BuildXmlFromDataTable(this DataTableMappingCollection mappings)
    {
        ArgumentNullException.ThrowIfNull(mappings);
        return ConvertInternal(mappings);
    }

    public static XDocument BuildXmlFromDataTable(this DataViewSetting setting)
    {
        ArgumentNullException.ThrowIfNull(setting);
        return ConvertInternal(setting);
    }
}