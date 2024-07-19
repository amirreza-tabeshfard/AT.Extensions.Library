using System.Xml;

namespace AT.Extensions.XML;
public static class Extensions : Object
{
    public static void TraverseXml(this XmlNode rootXmlNode, Action<XmlNode> action)
    {
        rootXmlNode.ChildNodes.Cast<XmlNode>().ToList().ForEach(x =>
        {
            action(x);
            TraverseXml(x, action);
        });
    }

    public static IEnumerable<XmlNode> FindElements(this XmlDocument xmlDocument, Func<XmlNode, Boolean> whereExression)
    {
        var foundXmlNodes = new List<XmlNode>();
        xmlDocument.FirstChild.ParentNode.TraverseXml(x =>
        {
            foundXmlNodes.Add(x);
        });

        return foundXmlNodes.Where(whereExression);
    }

    public static IEnumerable<XmlNode> FindElementsByAttributeValue(this XmlDocument xmlDocument, String attributeName, String attributeValue)
    {
        List<XmlNode> foundXmlNodes = new List<XmlNode>();
        xmlDocument.FirstChild.ParentNode.TraverseXml(x =>
        {
            XmlAttribute? foundAttribute = x.Attributes?.Cast<XmlAttribute>()
                                                          .FirstOrDefault(attribute => attribute.Name.Equals(attributeName) &&
                                                           attribute.InnerText == attributeValue);
            if (foundAttribute == default)
                return;

            foundXmlNodes.Add(x);
        });

        return foundXmlNodes;
    }

    public static IEnumerable<XmlNode> FindElementsByAttributes(this XmlDocument xmlDocument, Func<XmlAttribute, Boolean> whereExpression)
    {
        var foundXmlNodes = new List<XmlNode>();
        xmlDocument.FirstChild.ParentNode.TraverseXml(x =>
        {
            XmlAttribute? foundAttribute = x.Attributes?.Cast<XmlAttribute>()
                                                            .FirstOrDefault(whereExpression);
            if (foundAttribute == default)
                return;

            foundXmlNodes.Add(x);
        });

        return foundXmlNodes;
    }

    public static void RemoveElements(this XmlDocument xmlDocument, Func<XmlNode, Boolean> whereExpression)
    {
        var elementsToRemove = xmlDocument.FindElements(whereExpression);
        if (!elementsToRemove.Any())
            return;

        xmlDocument.TraverseXml(x =>
        {
            foreach (XmlNode node in elementsToRemove)
            {
                if (x != node)
                    continue;

                x.ParentNode.RemoveChild(x);
            }
        });
    }

    public static IEnumerable<XmlNode> Flatten(this XmlDocument xmlDocument)
    {
        var flattenedXml = new List<XmlNode>();
        xmlDocument.FirstChild.ParentNode.TraverseXml(x =>
        {
            flattenedXml.Add(x);
        });

        return flattenedXml;
    }

    public static void AlphabetizeElementChildren(this XmlDocument xmlDocument, Func<XmlNode, Boolean> whereExpression)
    {
        var elementsToAlphabetize = xmlDocument.FindElements(whereExpression);
        elementsToAlphabetize.ToList().ForEach(x =>
        {
            if (!x.HasChildNodes)
                return;

            IOrderedEnumerable<XmlNode> orderedChildNodes = x.ChildNodes.Cast<XmlNode>().ToList().OrderBy(node => node.Name);
            x.ChildNodes.Cast<XmlNode>().ToList().ForEach(child => x.RemoveChild(child));
            orderedChildNodes.ToList().ForEach(newChild => x.AppendChild(newChild));
        });
    }

    public static XmlNode ReadServiceConfigFile(this String filePath, String fileName, String extension, String singleNode)
    {
        if (String.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load($"{fileName}.{extension}");

        if (xmlDocument == default)
        {
            throw new ArgumentException($"从文件{filePath}中未读取到任何配置");
        }

        XmlNode? root = xmlDocument.SelectSingleNode(singleNode);

        if (root == default)
        {
            throw new ArgumentException($"从文件{filePath}中未读取到任何配置");
        }

        return root;
    }

    public static XmlDocument GetXmlDocFromFile(this String filePath)
    {
        XmlDocument document = new();
        document.Load(filePath);
        return document;
    }

    public static String GetNodeInnerTextFromString(this String xmlString, String stringTargetNode)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlString);

        XmlNode targetNode = doc.SelectSingleNode(stringTargetNode);

        return targetNode.InnerText;
    }

    public static String GetNodeTextFromFile(this String filePath, String stringTargetNode)
    {
        return filePath
               .GetXmlDocFromFile()
               .GetXmlNode(stringTargetNode)
               .GetNodeTextValue()
               ;
    }

    public static XmlNode GetXmlNode(this XmlDocument xmlDoc, String stringTargetNode)
    {
        return xmlDoc.SelectSingleNode(stringTargetNode);
    }

    public static String GetNodeTextValue(this XmlNode node)
    {
        return node.InnerText;
    }
}