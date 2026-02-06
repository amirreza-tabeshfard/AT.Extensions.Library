using System.Collections.Specialized;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Public Class
/// </summary>
public static partial class BuildXmlFromBlockchainTransactionExtensions
{
    public sealed class BlockchainTransaction
    {
        public required String TransactionId { get; init; }

        public required String From { get; init; }

        public required String To { get; init; }

        public required decimal Amount { get; init; }

        public required DateTimeOffset Timestamp { get; init; }

        public required String Payload { get; init; }
    }
}

/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromBlockchainTransactionExtensions
{
    private static XmlDocument InternalBuild(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            String xmlPayload;

            switch (source)
            {
                case String s:
                    {
                        xmlPayload = s;
                    }
                    break;

                case Byte[] bytes:
                    {
                        xmlPayload = Encoding.UTF8.GetString(bytes);
                    }
                    break;

                case MemoryStream ms:
                    {
                        xmlPayload = Encoding.UTF8.GetString(ms.ToArray());
                    }
                    break;

                case Stream stream:
                    {
                        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
                        xmlPayload = reader.ReadToEnd();
                    }
                    break;

                case XmlDocument xd:
                    return xd;

                case XDocument xdoc:
                    {
                        xmlPayload = xdoc.ToString(SaveOptions.DisableFormatting);
                    }
                    break;

                case FileInfo file:
                    {
                        if (!file.Exists)
                            throw new FileNotFoundException(
                                "The provided FileInfo does not point to an existing file.",
                                file.FullName);

                        xmlPayload = File.ReadAllText(file.FullName, Encoding.UTF8);
                    }
                    break;

                case Uri uri:
                    {
                        if (!uri.IsAbsoluteUri)
                            throw new InvalidOperationException("The provided Uri must be absolute.");

                        xmlPayload = $"<BlockchainTransaction><Uri>{SecurityElement.Escape(uri.ToString())}</Uri></BlockchainTransaction>";
                    }
                    break;

                case HttpRequestMessage http:
                    {
                        if (http.Content is null)
                            throw new InvalidOperationException("HttpRequestMessage.Content is null.");

                        xmlPayload = http.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                    break;

                case BlockchainTransaction tx:
                    {
                        xmlPayload = BuildFromDomain(tx);
                    }
                    break;

                case IDictionary<String, String> dict:
                    {
                        xmlPayload = BuildFromDictionary(dict);
                    }
                    break;

                case NameValueCollection nvc:
                    {
                        xmlPayload = BuildFromNameValueCollection(nvc);
                    }
                    break;

                case StringBuilder sb:
                    {
                        xmlPayload = sb.ToString();
                    }
                    break;

                case TextReader tr:
                    {
                        xmlPayload = tr.ReadToEnd();
                    }
                    break;

                default:
                    {
                        xmlPayload = $"<BlockchainTransaction><Raw>{SecurityElement.Escape(source.ToString() ?? String.Empty)}</Raw></BlockchainTransaction>";
                    }
                    break;
            }

            if (String.IsNullOrWhiteSpace(xmlPayload))
                throw new InvalidOperationException("The extracted payload is empty. Cannot build XML.");

            var doc = new XmlDocument
            {
                PreserveWhitespace = false
            };

            doc.LoadXml(xmlPayload);
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The provided source argument is invalid for blockchain XML construction.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The input source parameter was null. A valid blockchain transaction input is required.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("UTF8 decoding failed while converting byte payload to XML content.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The blockchain transaction file could not be located on disk.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("HTTP content could not be synchronized into an XML blockchain transaction.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O failure occurred while reading blockchain transaction data.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.StreamReader"))
        {
            throw new InvalidOperationException("The underlying stream was disposed before the blockchain transaction could be fully read.", ex);
        }
        catch (SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Security"))
        {
            throw new InvalidOperationException("Security restrictions prevented access to blockchain transaction resources.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("Unauthorized access while attempting to read blockchain transaction file.", ex);
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException("The provided data is not a valid XML payload for a blockchain transaction.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XmlDocument from blockchain transaction input. See inner exception for details.", ex);
        }
    }

    private static String BuildFromDomain(BlockchainTransaction bt)
    {
        if (bt is null)
            throw new ArgumentNullException(nameof(bt), "BlockchainTransaction instance cannot be null.");

        return $"""
                <BlockchainTransaction>
                    <TransactionId>{SecurityElement.Escape(bt.TransactionId)}</TransactionId>
                    <From>{SecurityElement.Escape(bt.From)}</From>
                    <To>{SecurityElement.Escape(bt.To)}</To>
                    <Amount>{bt.Amount}</Amount>
                    <Timestamp>{bt.Timestamp:O}</Timestamp>
                    <Payload>{SecurityElement.Escape(bt.Payload)}</Payload>
                </BlockchainTransaction>
                """;
    }

    private static String BuildFromDictionary(IDictionary<String, String> dict)
    {
        var sb = new StringBuilder();
        sb.Append("<BlockchainTransaction>");

        foreach (var kv in dict)
        {
            sb.Append('<').Append(kv.Key).Append('>')
              .Append(SecurityElement.Escape(kv.Value))
              .Append("</").Append(kv.Key).Append('>');
        }

        sb.Append("</BlockchainTransaction>");
        return sb.ToString();
    }

    private static String BuildFromNameValueCollection(NameValueCollection nvc)
    {
        var sb = new StringBuilder();
        sb.Append("<BlockchainTransaction>");

        foreach (String key in nvc.AllKeys)
        {
            sb.Append('<').Append(key).Append('>')
              .Append(SecurityElement.Escape(nvc[key]))
              .Append("</").Append(key).Append('>');
        }

        sb.Append("</BlockchainTransaction>");
        return sb.ToString();
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromBlockchainTransactionExtensions
{
    public static XmlDocument BuildXmlFromBlockchainTransaction(this String source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this Byte[] source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this MemoryStream source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this Stream source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this XmlDocument source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this XDocument source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this FileInfo source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this Uri source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this HttpRequestMessage source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this BlockchainTransaction source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this IDictionary<String, String> source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this NameValueCollection source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this StringBuilder source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this TextReader source)
    {
        return InternalBuild(source);
    }

    public static XmlDocument BuildXmlFromBlockchainTransaction(this Object source)
    {
        return InternalBuild(source);
    }
}