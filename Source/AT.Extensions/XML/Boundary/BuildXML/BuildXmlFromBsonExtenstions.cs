using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromBsonExtenstions
{
    private static XDocument Synchronize(Object? input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            BsonDocument document;

            if (input is BsonDocument bsonDocument)
                document = bsonDocument;
            else if (input is RawBsonDocument rawBson)
                document = rawBson;
            else if (input is BsonArray bsonArray)
                document = new BsonDocument("Root", bsonArray);
            else if (input is BsonValue bsonValue)
                document = new BsonDocument("Root", bsonValue);
            else if (input is BsonBinaryData binaryData)
                document = BsonSerializer.Deserialize<BsonDocument>(new MemoryStream(binaryData.Bytes));
            else if (input is BsonJavaScript javaScript)
                document = new BsonDocument("JavaScript", new BsonString(javaScript.Code));
            else if (input is Byte[] bytes)
                document = BsonSerializer.Deserialize<BsonDocument>(new MemoryStream(bytes));
            else if (input is MemoryStream memoryStream)
                document = BsonSerializer.Deserialize<BsonDocument>(memoryStream);
            else if (input is Stream stream)
                document = BsonSerializer.Deserialize<BsonDocument>(stream);
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("The specified file does not exist.");

                using var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

                document = BsonSerializer.Deserialize<BsonDocument>(fs);
            }
            else if (input is String text)
                document = ParseFromString(text);
            else if (input is TextReader reader)
                document = ParseFromString(reader.ReadToEnd());
            else if (input is BinaryReader binaryReader)
                document = BsonSerializer.Deserialize<BsonDocument>(binaryReader.BaseStream);
            else if (input is BsonDocument[] documents)
                document = new BsonDocument("Root", new BsonArray(documents));
            else if (input is BsonReader bsonReader)
                document = BsonSerializer.Deserialize<BsonDocument>(bsonReader);
            else
                throw new NotSupportedException($"Unsupported input type: {input.GetType().FullName}");

            var json = document.ToJson(new JsonWriterSettings
            {
                OutputMode = JsonOutputMode.RelaxedExtendedJson
            });

            return JsonToXml(json);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input argument is invalid or malformed.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input argument was null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Text decoding failed while processing BSON payload.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Directory was not found while accessing BSON file.", ex);
        }
        catch (EndOfStreamException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Unexpected end of stream while deserializing BSON.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Specified BSON file does not exist.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("MongoDB.Bson"))
        {
            throw new InvalidOperationException("BSON format is invalid.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Invalid stream data encountered while reading BSON.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("I/O failure occurred during BSON processing.", ex);
        }
        catch (JsonException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Json"))
        {
            throw new InvalidOperationException("JSON conversion failed after BSON deserialization.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals(typeof(BsonSerializer).Namespace))
        {
            throw new InvalidOperationException("Unsupported BSON input type provided.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Stream was disposed before BSON deserialization completed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access denied while reading BSON source.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML generation failed after BSON synchronization.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to synchronize BSON to XML due to an unexpected runtime error.", ex);
        }
    }

    private static BsonDocument ParseFromString(String content)
    {
        if (String.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Input String is empty or whitespace.");

        // Heuristic: JSON vs Base64 BSON
        if (content.TrimStart().StartsWith("{"))
            return BsonDocument.Parse(content);

        try
        {
            var raw = Convert.FromBase64String(content);
            return BsonSerializer.Deserialize<BsonDocument>(raw);
        }
        catch
        {
            throw new FormatException(
                "String input is neither valid JSON nor Base64-encoded BSON.");
        }
    }

    private static XDocument JsonToXml(String json)
    {
        using var reader = System.Text.Json.JsonDocument.Parse(json);

        var root = new XElement("Root");
        BuildXmlElements(reader.RootElement, root);

        return new XDocument(root);
    }

    private static void BuildXmlElements(System.Text.Json.JsonElement element, XElement parent)
    {
        switch (element.ValueKind)
        {
            case System.Text.Json.JsonValueKind.Object:
                foreach (var prop in element.EnumerateObject())
                {
                    var child = new XElement(prop.Name);
                    parent.Add(child);
                    BuildXmlElements(prop.Value, child);
                }
                break;

            case System.Text.Json.JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    var child = new XElement("Item");
                    parent.Add(child);
                    BuildXmlElements(item, child);
                }
                break;

            default:
                parent.Value = element.ToString();
                break;
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromBsonExtenstions
{
    public static XDocument BuildXmlFromBson(this BsonDocument source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this RawBsonDocument source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonArray source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonValue source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonBinaryData source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonJavaScript source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this Byte[] source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this Stream source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this MemoryStream source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this FileInfo source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this String source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this TextReader source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BinaryReader source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonDocument[] source)
        => Synchronize(source);

    public static XDocument BuildXmlFromBson(this BsonReader source)
        => Synchronize(source);
}