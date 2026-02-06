using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromEntityFrameworkModelExtensions
{
    private static XDocument Synchronize(Object? input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Boundary root is null. A valid Entity Framework model reference is required.");

            IEnumerable<IEntityType> entities;

            if (input is DbContext db)
                entities = db.Model.GetEntityTypes();
            else if (input is IModel model)
                entities = model.GetEntityTypes();
            else if (input is IMutableModel mutableModel)
                entities = ((IModel)mutableModel).GetEntityTypes();
            else if (input is IReadOnlyModel readOnlyModel)
                entities = ((IModel)readOnlyModel).GetEntityTypes();
            else if (input is IEntityType entity)
                entities = new List<IEntityType> { entity };
            else if (input is IMutableEntityType mutableEntity)
                entities = new List<IEntityType> { (IEntityType)mutableEntity };
            else if (input is IReadOnlyEntityType readOnlyEntity)
                entities = new List<IEntityType> { (IEntityType)readOnlyEntity };
            else if (input is IEnumerable<IEntityType> entityEnumerable)
                entities = entityEnumerable;
            else if (input is IEnumerable<IMutableEntityType> mutableEnumerable)
                entities = mutableEnumerable.Select(e => (IEntityType)e);
            else if (input is IEnumerable<IReadOnlyEntityType> readOnlyEnumerable)
                entities = readOnlyEnumerable.Select(e => (IEntityType)e);
            else if (input is ModelBuilder builder)
                entities = ((IModel)builder.Model).GetEntityTypes();
            else
                throw new NotSupportedException($"Unsupported EF boundary type: {input.GetType().FullName}");

            var list = entities.ToList();

            if (list.Count == 0)
                throw new InvalidOperationException("No entity types discovered in provided Entity Framework boundary.");

            return new XDocument(
                new XElement("EntityFrameworkModel",
                    list.Select(e =>
                        new XElement("Entity",
                            new XAttribute("Name", e.Name),
                            new XElement("Table", e.GetTableName() ?? String.Empty),
                            new XElement("Schema", e.GetSchema() ?? String.Empty),
                            new XElement("PrimaryKey",
                                e.FindPrimaryKey()?.Properties.Select(p =>
                                    new XElement("Key",
                                        new XAttribute("Name", p.Name),
                                        new XAttribute("ClrType", p.ClrType.FullName ?? String.Empty)))
                                ?? Enumerable.Empty<XElement>()),
                            new XElement("Properties",
                                e.GetProperties().Select(p =>
                                    new XElement("Property",
                                        new XAttribute("Name", p.Name),
                                        new XAttribute("ClrType", p.ClrType.FullName ?? String.Empty),
                                        new XAttribute("Nullable", p.IsNullable),
                                        new XAttribute("MaxLength", p.GetMaxLength()?.ToString() ?? String.Empty))))
                    ))));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The EF boundary root argument was null. A DbContext, IModel, or EntityType reference is required.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The provided EF boundary argument is out of the supported range.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("Microsoft.EntityFrameworkCore", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Failed to cast EF Core metadata types. This usually indicates incompatible EntityType boundaries.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("Microsoft.EntityFrameworkCore", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("EF Core reported an invalid operation while resolving entity metadata.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Linq", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("LINQ failed while materializing entity collections from the EF model.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An unsupported EF boundary type was supplied to the synchronization engine.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("Microsoft.EntityFrameworkCore", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A null reference was encountered while traversing EF Core metadata. An entity or property definition is incomplete.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("Microsoft.EntityFrameworkCore", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The DbContext or EF model was already disposed during XML synchronization.", ex);
        }
        catch (OperationCanceledException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The EF model synchronization process was canceled before completion.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Access to EF metadata was denied during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Entity Framework model synchronization to XML failed. Inspect inner exception for precise diagnostic details.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromEntityFrameworkModelExtensions
{
    public static XDocument BuildXmlFromEntityFrameworkModel(this DbContext context)
    {
        return Synchronize(context);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IModel model)
    {
        return Synchronize(model);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IMutableModel model)
    {
        return Synchronize(model);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IReadOnlyModel model)
    {
        return Synchronize(model);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IEntityType entity)
    {
        return Synchronize(entity);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IMutableEntityType entity)
    {
        return Synchronize(entity);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IReadOnlyEntityType entity)
    {
        return Synchronize(entity);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IEnumerable<IEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IEnumerable<IMutableEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IEnumerable<IReadOnlyEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IReadOnlyCollection<IEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IReadOnlyCollection<IMutableEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this IReadOnlyCollection<IReadOnlyEntityType> entities)
    {
        return Synchronize(entities);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this ModelBuilder builder)
    {
        return Synchronize(builder);
    }

    public static XDocument BuildXmlFromEntityFrameworkModel(this Object efBoundaryRoot)
    {
        return Synchronize(efBoundaryRoot);
    }
}