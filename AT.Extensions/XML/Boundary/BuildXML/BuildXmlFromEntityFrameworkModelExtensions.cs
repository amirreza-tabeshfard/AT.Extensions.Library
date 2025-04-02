using Microsoft.EntityFrameworkCore;

namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromEntityFrameworkModelExtensions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromEntityFrameworkModel(this DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement root = xmlDocument.CreateElement("Entities");
            xmlDocument.AppendChild(root);

            foreach (Microsoft.EntityFrameworkCore.Metadata.IEntityType entity in context.Model.GetEntityTypes())
            {
                System.Xml.XmlElement entityElement = xmlDocument.CreateElement(entity.Name);
                root.AppendChild(entityElement);
            }

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("context"))
        {
            throw new ArgumentNullException("The provided DbContext instance is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Sequence contains no elements"))
        {
            throw new InvalidOperationException("The Entity Framework model does not contain any entity types.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("An attempt was made to use the model before it was initialized"))
        {
            throw new InvalidOperationException("The Entity Framework model is not properly initialized.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Root element is missing"))
        {
            throw new System.Xml.XmlException("An issue occurred while creating the XML document. The root element is missing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Invalid XML character"))
        {
            throw new System.Xml.XmlException("An invalid XML character was detected while generating the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Invalid name character"))
        {
            throw new System.Xml.XmlException("An entity type contains an invalid name character, which is not allowed in XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Only one root element is allowed"))
        {
            throw new System.Xml.XmlException("The XML document cannot have more than one root element.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Equals("Object reference not set to an instance of an Object."))
        {
            throw new NullReferenceException("A null reference was encountered while generating the XML document.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("Specified method is not supported."))
        {
            throw new NotSupportedException("A method or operation used during XML generation is not supported.", ex);
        }
        catch (Exception ex) when (ex.Message.Equals("Error generating XML from Entity Framework Model."))
        {
            throw new Exception("An unexpected error occurred while generating XML from the Entity Framework model.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unknown error occurred during XML generation.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromEntityFrameworkModel(this DbContext context, String tableName)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(tableName);
        // ----------------------------------------------------------------------------------------------------
        Microsoft.EntityFrameworkCore.Metadata.IEntityType? entityType = context
                                                                         .Model
                                                                         .GetEntityTypes()
                                                                         .FirstOrDefault(e => e.GetTableName()
                                                                         ?.Equals(tableName, StringComparison.OrdinalIgnoreCase) == true);
        // ----------------------------------------------------------------------------------------------------
        ArgumentNullException.ThrowIfNull(entityType);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Type entityClrType = entityType.ClrType;

            if ((context.GetType().GetMethod("Set")?.MakeGenericMethod(entityClrType).Invoke(context, null) as IQueryable) == null)
                throw new InvalidOperationException("Unable to retrieve DbSet for the specified table.");


            System.Xml.Linq.XDocument xml = new(new System.Xml.Linq.XElement(tableName,
                                                                             (context
                                                                                .GetType()
                                                                                .GetMethod("Set")
                                                                                ?.MakeGenericMethod(entityClrType)
                                                                                .Invoke(context, null) as IQueryable
                                                                             )
                                                                             .Cast<Object>()
                                                                             .ToList()
                                                                             .Select(entity => new System.Xml.Linq.XElement("Row", entityType.GetProperties()
                                                                                               .Select(prop => new System.Xml.Linq.XElement(prop.Name,
                                                                                                                                            entityClrType.GetProperty(prop.Name)
                                                                                                                                            ?.GetValue(entity) ?? "NULL"))))));

            return xml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("context", StringComparison.Ordinal))
        {
            throw new ArgumentNullException("The DbContext instance cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("tableName", StringComparison.Ordinal))
        {
            throw new ArgumentNullException("The table name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("tableName", StringComparison.Ordinal))
        {
            throw new ArgumentException("The table name provided is invalid or whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Unable to retrieve DbSet for the specified table.", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Failed to retrieve DbSet. Ensure the table name is correct and mapped in the model.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Error generating XML for the specified table.", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the XML document.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("ClrType", StringComparison.Ordinal))
        {
            throw new NullReferenceException("The entity type does not have a valid CLR type mapping.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("GetProperty", StringComparison.Ordinal))
        {
            throw new NullReferenceException("Failed to retrieve property information for the entity.", ex);
        }
        catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null && ex.InnerException.GetType().Equals(typeof(InvalidOperationException)))
        {
            throw new InvalidOperationException("An error occurred while invoking the DbSet method. Ensure that the entity type is correctly mapped.", ex);
        }
        catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null && ex.InnerException.GetType().Equals(typeof(ArgumentException)))
        {
            throw new ArgumentException("Invalid arguments were passed while invoking the DbSet method.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("IQueryable", StringComparison.Ordinal))
        {
            throw new InvalidCastException("Failed to cast the result to IQueryable. Ensure that the table entity is mapped correctly.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The specified table does not contain any records.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while processing the XML conversion.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromEntityFrameworkModel(this DbContext context, Type entityType)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(entityType);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement root = xmlDocument.CreateElement(entityType.Name);
            xmlDocument.AppendChild(root);

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("context"))
        {
            throw new InvalidOperationException("DbContext is null. Ensure that the DbContext instance is properly provided.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("entityType"))
        {
            throw new InvalidOperationException("Entity type is null. Ensure that a valid entity type is provided.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Error generating XML for the given entity type."))
        {
            throw new InvalidOperationException("An invalid operation occurred during the XML generation process. Please check the entity type and database connection.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an error while working with XML. The XML document could not be generated.", ex);
        }
        catch (System.Reflection.ReflectionTypeLoadException ex)
        {
            throw new InvalidOperationException("An error occurred while reflecting on the entity type. Ensure that the correct type is provided.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the XML document.", ex);
        }
    }

    public static String BuildXmlFromEntityFrameworkModel(this DbContext context, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = context.BuildXmlFromEntityFrameworkModel();
            using StringWriter stringWriter = new();
            using System.Xml.XmlTextWriter xmlTextWriter = new(stringWriter);
            xmlDoc.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("context"))
        {
            throw new ArgumentNullException("The provided DbContext instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new ArgumentNullException("The provided encoding instance is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Object reference not set"))
        {
            throw new InvalidOperationException("The Entity Framework context may not be properly initialized.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            throw new InvalidOperationException("The database model appears to be empty or improperly configured.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end"))
        {
            throw new System.Xml.XmlException("Error while parsing XML: Unexpected end of file detected.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("Error while parsing XML: Invalid character found in the document.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("StringWriter"))
        {
            throw new ObjectDisposedException("The StringWriter instance was disposed before use.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("XmlTextWriter"))
        {
            throw new ObjectDisposedException("The XmlTextWriter instance was disposed before use.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("disk full"))
        {
            throw new IOException("Unable to write XML to string due to insufficient disk space.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("permission denied"))
        {
            throw new IOException("Insufficient permissions to write XML to string.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting XML to String.", ex);
        }
    }

    public static System.Xml.XmlElement BuildXmlFromEntityFrameworkModel(this DbContext context, Type entityType, System.Xml.XmlDocument xmlDoc)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(entityType);
        ArgumentNullException.ThrowIfNull(xmlDoc);
        // ----------------------------------------------------------------------------------------------------
        if (!context.Model.GetEntityTypes().Any(e => e.ClrType == entityType))
            throw new ArgumentException("The specified type is not part of the DbContext model.", nameof(entityType));
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement(entityType.Name);

            System.Reflection.MethodInfo setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!.MakeGenericMethod(entityType);
            Object? dbSet = setMethod.Invoke(context, null);

            System.Reflection.MethodInfo toListMethod = typeof(Enumerable).GetMethod("ToList")!.MakeGenericMethod(entityType);

            if (toListMethod.Invoke(null, [dbSet]) is not IEnumerable<Object> entityList)
                return rootElement;

            foreach ((Object entity, System.Xml.XmlElement entityElement) in from entity in entityList
                                                                             let entityElement = xmlDoc.CreateElement("Entity")
                                                                             select (entity, entityElement))
            {
                foreach ((Object value, System.Xml.XmlElement propertyElement) in from System.Reflection.PropertyInfo property in entityType.GetProperties()
                                                                                  let value = property.GetValue(entity)
                                                                                  let propertyElement = xmlDoc.CreateElement(property.Name)
                                                                                  select (value, propertyElement))
                {
                    propertyElement.InnerText = value?.ToString() ?? "null";
                    entityElement.AppendChild(propertyElement);
                }

                rootElement.AppendChild(entityElement);
            }

            return rootElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(entityType)))
        {
            throw new ArgumentException("The specified type is not part of the DbContext model.", nameof(entityType), ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Method 'Set' on type 'DbContext'"))
        {
            throw new InvalidOperationException("Failed to retrieve DbSet for the specified entity type.", ex);
        }
        catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null)
        {
            throw new System.Reflection.TargetInvocationException("An error occurred while invoking a method via reflection.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("MakeGenericMethod"))
        {
            throw new NullReferenceException("Failed to create a generic method for the entity type. Ensure the type is valid.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Invoke"))
        {
            throw new NullReferenceException("Method invocation via reflection returned null. Check the method signature.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("ToList"))
        {
            throw new InvalidCastException("The retrieved DbSet could not be converted to a list. Ensure the entity type is correct.", ex);
        }
        catch (System.Reflection.TargetException ex) when (ex.Message.Contains("Invoke"))
        {
            throw new System.Reflection.TargetException("A method was invoked on an incompatible object. Verify the DbContext instance.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new System.Xml.XmlException("Failed to create XML elements. Ensure the XML document is properly initialized.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating XML from entity type.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromEntityFrameworkModel(this System.Data.DataTable table)
    {
        ArgumentNullException.ThrowIfNull(table);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            using (StringWriter writer = new())
            {
                table.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema);
                xmlDocument.LoadXml(writer.ToString());
            }
            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The DataTable parameter is null. Parameter name: {ex.ParamName}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("cannot be written"))
        {
            throw new InvalidOperationException("An error occurred while writing the XML data from the DataTable.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Invalid character"))
        {
            throw new InvalidOperationException("The XML generated from the DataTable contains invalid characters.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("sharing violation"))
        {
            throw new InvalidOperationException("There was an issue with file access while generating XML from DataTable.", ex);
        }
        catch (System.Data.DataException ex) when (ex.Message.Contains("WriteXml"))
        {
            throw new InvalidOperationException("The DataTable could not be serialized to XML due to data structure issues.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid format"))
        {
            throw new InvalidOperationException("The XML format is invalid while generating from DataTable.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from the DataTable.", ex);
        }
    }

    public static String BuildXmlFromEntityFrameworkModel(this System.Data.DataTable table, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {

            using MemoryStream memoryStream = new();
            table.BuildXmlFromEntityFrameworkModel().Save(memoryStream);
            Byte[] xmlBytes = memoryStream.ToArray();
            return encoding.GetString(xmlBytes);
        }
        catch (IOException ioEx) when (ioEx.Message.Contains("The process cannot access the file"))
        {
            throw new InvalidOperationException("Error: Unable to access the file during XML saving.", ioEx);
        }
        catch (ArgumentNullException argNullEx) when (argNullEx.ParamName is not null && argNullEx.ParamName.Equals("table"))
        {
            throw new ArgumentException("Error: The DataTable parameter is null.", argNullEx);
        }
        catch (ArgumentException argEx) when (argEx.Message.Contains("table"))
        {
            throw new InvalidOperationException("Error: Invalid DataTable provided for XML conversion.", argEx);
        }
        catch (ObjectDisposedException objDisposedEx) when (objDisposedEx.Message.Contains("Cannot access a disposed object"))
        {
            throw new InvalidOperationException("Error: The DataTable or XML object was disposed before conversion.", objDisposedEx);
        }
        catch (OutOfMemoryException outOfMemoryEx) when (outOfMemoryEx.Message.Contains("insufficient memory"))
        {
            throw new InvalidOperationException("Error: Insufficient memory to convert the DataTable to XML.", outOfMemoryEx);
        }
        catch (InvalidOperationException invalidOpEx) when (invalidOpEx.Message.Contains("Cannot save"))
        {
            throw new InvalidOperationException("Error: Failed to save the XML document to memory.", invalidOpEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("General error occurred during XML conversion.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromEntityFrameworkModel(this DbContext context, List<String> tableNames)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(tableNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement root = xmlDocument.CreateElement("Entities");
            xmlDocument.AppendChild(root);
            
            foreach (System.Xml.XmlElement? element in from String tableName in tableNames
                                                       let element = xmlDocument.CreateElement(tableName)
                                                       select element)
            {
                root.AppendChild(element);
            }

            return xmlDocument;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter '{ex.ParamName}' cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error generating XML for multiple tables"))
        {
            throw new InvalidOperationException("An error occurred while generating the XML document for the specified tables. Please check the input data and table names.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The document could not be processed"))
        {
            throw new InvalidOperationException("There was an error processing the XML document. The structure of the document might be invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("tableNames"))
        {
            throw new InvalidOperationException("The 'tableNames' argument is invalid. Please ensure the list is populated with valid table names.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.Message.Contains("Index was out of range"))
        {
            throw new InvalidOperationException("The specified index for accessing the table names was out of range. Please check the range and input parameters.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the XML document.", ex);
        }
    }
}