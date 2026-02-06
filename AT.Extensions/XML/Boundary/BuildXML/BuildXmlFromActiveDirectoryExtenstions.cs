using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Class
/// </summary>
public static partial class BuildXmlFromActiveDirectoryExtenstions
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class Group
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string[] Members { get; set; } = Array.Empty<string>();
    }

    public class OrganizationalUnit
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }

    public class Computer
    {
        public string Name { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public DateTime LastLogon { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class SecurityGroup
    {
        public string Name { get; set; } = string.Empty;

        public string[] Members { get; set; } = Array.Empty<string>();
    }

    public class DistributionGroup
    {
        public string Name { get; set; } = string.Empty;

        public string[] Members { get; set; } = Array.Empty<string>();
    }

    public class Domain
    {
        public string Name { get; set; } = string.Empty;

        public string[] DomainControllers { get; set; } = Array.Empty<string>();
    }

    public class Site
    {
        public string Name { get; set; } = string.Empty;

        public string[] Subnets { get; set; } = Array.Empty<string>();
    }

    public class Subnet
    {
        public string Address { get; set; } = string.Empty;

        public string Mask { get; set; } = string.Empty;
    }

    public class Policy
    {
        public string PolicyName { get; set; } = string.Empty;

        public string[] Settings { get; set; } = Array.Empty<string>();
    }

    public class ServiceAccount
    {
        public string Name { get; set; } = string.Empty;

        public string[] Privileges { get; set; } = Array.Empty<string>();
    }

    public class OUHierarchy
    {
        public string RootOU { get; set; } = string.Empty;

        public string[] ChildOUs { get; set; } = Array.Empty<string>();
    }

    public class Trust
    {
        public string SourceDomain { get; set; } = string.Empty;

        public string TargetDomain { get; set; } = string.Empty;

        public string TrustType { get; set; } = string.Empty;
    }

    public class Schema
    {
        public string Name { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;
    }

    public class DNSZone
    {
        public string Name { get; set; } = string.Empty;

        public string[] Records { get; set; } = Array.Empty<string>();
    }

    public class Role
    {
        public string Name { get; set; } = string.Empty;

        public string[] Permissions { get; set; } = Array.Empty<string>();
    }

    public class LoginHistory
    {
        public string UserName { get; set; } = string.Empty;

        public DateTime[] Logins { get; set; } = Array.Empty<DateTime>();
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromActiveDirectoryExtenstions
{
    private static XElement SynchronizeInternal(object boundary)
    {
        try
        {
            if (boundary == null)
                throw new ArgumentNullException(nameof(boundary), "Input object cannot be null.");

            return boundary switch
            {
                User u => new XElement("User", new XElement("Id", u.Id), new XElement("Name", u.Name), new XElement("Email", u.Email)),

                Group g => new XElement("Group", new XElement("Id", g.Id), new XElement("Name", g.Name), new XElement("Members", string.Join(",", g.Members))),
                
                OrganizationalUnit o => new XElement("OrganizationalUnit", new XElement("Id", o.Id), new XElement("Name", o.Name)),
                
                Computer c => new XElement("Computer", new XElement("Name", c.Name), new XElement("OS", c.OperatingSystem), new XElement("LastLogon", c.LastLogon)),
                
                Contact co => new XElement("Contact", new XElement("Name", co.Name), new XElement("Email", co.Email)),
                
                SecurityGroup sg => new XElement("SecurityGroup", new XElement("Name", sg.Name), new XElement("Members", string.Join(",", sg.Members))),
                
                DistributionGroup dg => new XElement("DistributionGroup", new XElement("Name", dg.Name), new XElement("Members", string.Join(",", dg.Members))),
                
                Domain d => new XElement("Domain", new XElement("Name", d.Name), new XElement("DomainControllers", string.Join(",", d.DomainControllers))),
                
                Site s => new XElement("Site", new XElement("Name", s.Name), new XElement("Subnets", string.Join(",", s.Subnets))),
                
                Subnet sn => new XElement("Subnet", new XElement("Address", sn.Address), new XElement("Mask", sn.Mask)),
                
                Policy p => new XElement("Policy", new XElement("Name", p.PolicyName), new XElement("Settings", string.Join(",", p.Settings))),
                
                ServiceAccount sa => new XElement("ServiceAccount", new XElement("Name", sa.Name), new XElement("Privileges", string.Join(",", sa.Privileges))),
                
                OUHierarchy ouh => new XElement("OUHierarchy", new XElement("RootOU", ouh.RootOU), new XElement("ChildOUs", string.Join(",", ouh.ChildOUs))),
                
                Trust t => new XElement("Trust", new XElement("SourceDomain", t.SourceDomain), new XElement("TargetDomain", t.TargetDomain), new XElement("TrustType", t.TrustType)),
                
                Schema sch => new XElement("Schema", new XElement("Name", sch.Name), new XElement("Version", sch.Version)),
                
                DNSZone dz => new XElement("DNSZone", new XElement("Name", dz.Name), new XElement("Records", string.Join(",", dz.Records))),
                
                Role r => new XElement("Role", new XElement("Name", r.Name), new XElement("Permissions", string.Join(",", r.Permissions))),
                
                LoginHistory lh => new XElement("LoginHistory", new XElement("UserName", lh.UserName), new XElement("Logins", string.Join(",", lh.Logins))),
                
                _ => throw new InvalidOperationException($"Unsupported boundary type: {boundary.GetType().Name}")
            };
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundary"))
        {
            throw new ApplicationException("Boundary object was null during synchronization.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("Id"))
        {
            throw new ApplicationException("Identifier value was outside the allowed range.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("One or more boundary fields had an invalid format.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("Collection indexing failed while building XML elements.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("LINQ to XML entered an invalid state while creating XElement.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Unsupported boundary type"))
        {
            throw new ApplicationException("The provided boundary type is not mapped to any XML representation.", ex);
        }
        catch (MemberAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("Failed to access one or more boundary properties.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("A required boundary member was null during XML projection.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("Insufficient memory while constructing XML structure.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("Numeric overflow occurred while projecting boundary values.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("XML layer rejected generated content as invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Unexpected failure occurred while synchronizing boundary objects to XML.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 18 )
/// </summary>
public static partial class BuildXmlFromActiveDirectoryExtenstions
{
    public static XElement BuildXmlFromActiveDirectory(this User user)
    {
        return SynchronizeInternal(user);
    }

    public static XElement BuildXmlFromActiveDirectory(this Group group)
    {
        return SynchronizeInternal(group);
    }

    public static XElement BuildXmlFromActiveDirectory(this OrganizationalUnit ou)
    {
        return SynchronizeInternal(ou);
    }

    public static XElement BuildXmlFromActiveDirectory(this Computer computer)
    {
        return SynchronizeInternal(computer);
    }

    public static XElement BuildXmlFromActiveDirectory(this Contact contact)
    {
        return SynchronizeInternal(contact);
    }

    public static XElement BuildXmlFromActiveDirectory(this SecurityGroup securityGroup)
    {
        return SynchronizeInternal(securityGroup);
    }

    public static XElement BuildXmlFromActiveDirectory(this DistributionGroup distGroup)
    {
        return SynchronizeInternal(distGroup);
    }

    public static XElement BuildXmlFromActiveDirectory(this Domain domain)
    {
        return SynchronizeInternal(domain);
    }

    public static XElement BuildXmlFromActiveDirectory(this Site site)
    {
        return SynchronizeInternal(site);
    }

    public static XElement BuildXmlFromActiveDirectory(this Subnet subnet)
    {
        return SynchronizeInternal(subnet);
    }

    public static XElement BuildXmlFromActiveDirectory(this Policy policy)
    {
        return SynchronizeInternal(policy);
    }

    public static XElement BuildXmlFromActiveDirectory(this ServiceAccount serviceAccount)
    {
        return SynchronizeInternal(serviceAccount);
    }

    public static XElement BuildXmlFromActiveDirectory(this OUHierarchy ouHierarchy)
    {
        return SynchronizeInternal(ouHierarchy);
    }

    public static XElement BuildXmlFromActiveDirectory(this Trust trust)
    {
        return SynchronizeInternal(trust);
    }

    public static XElement BuildXmlFromActiveDirectory(this Schema schema)
    {
        return SynchronizeInternal(schema);
    }

    public static XElement BuildXmlFromActiveDirectory(this DNSZone dnsZone)
    {
        return SynchronizeInternal(dnsZone);
    }

    public static XElement BuildXmlFromActiveDirectory(this Role role)
    {
        return SynchronizeInternal(role);
    }

    public static XElement BuildXmlFromActiveDirectory(this LoginHistory loginHistory)
    {
        return SynchronizeInternal(loginHistory);
    }
}