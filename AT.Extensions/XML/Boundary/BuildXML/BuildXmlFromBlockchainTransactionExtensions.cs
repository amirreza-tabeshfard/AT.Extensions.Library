namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromBlockchainTransactionExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction", new System.Xml.Linq.XElement("Status", "Success"));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating the transaction XML.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction", new System.Xml.Linq.XElement("ID", transactionId.ToString()));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction ID.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, Decimal amount)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Amount", amount));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentNullException("The transaction amount is null, which is required for XML processing.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Decimal"))
        {
            throw new InvalidOperationException("The transaction amount is not in a valid decimal format.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The transaction amount exceeds the maximum allowable value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The transaction amount is below the minimum allowable value.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction data.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, Decimal amount, String currency)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Amount", amount),
                   new System.Xml.Linq.XElement("Currency", currency));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentNullException("The transaction amount is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentNullException("The currency value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentException("The currency value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Decimal"))
        {
            throw new InvalidOperationException("The transaction amount is not in a valid decimal format.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The transaction amount exceeds the maximum allowable value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The transaction amount is below the minimum allowable value.", ex);
        }
        catch (FormatException ex) when (currency.Length > 3)
        {
            throw new FormatException("The currency code should be a 3-letter ISO 4217 code.", ex);
        }
        catch (FormatException ex) when (currency.Length < 3)
        {
            throw new FormatException("The currency code should be exactly 3 letters long.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, Decimal amount, String currency, DateTime timestamp)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Amount", amount),
                   new System.Xml.Linq.XElement("Currency", currency),
                   new System.Xml.Linq.XElement("Timestamp", timestamp.ToString("o")));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentNullException("The transaction amount is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentNullException("The currency value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("timestamp"))
        {
            throw new ArgumentNullException("The timestamp value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentException("The currency value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("timestamp"))
        {
            throw new ArgumentException("The timestamp value is invalid or improperly formatted.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Decimal"))
        {
            throw new InvalidOperationException("The transaction amount is not in a valid decimal format.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The transaction amount exceeds the maximum allowable value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The transaction amount is below the minimum allowable value.", ex);
        }
        catch (FormatException ex) when (currency.Length > 3)
        {
            throw new FormatException("The currency code should be a 3-letter ISO 4217 code.", ex);
        }
        catch (FormatException ex) when (currency.Length < 3)
        {
            throw new FormatException("The currency code should be exactly 3 letters long.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (timestamp > DateTime.UtcNow.AddYears(10))
        {
            throw new ArgumentOutOfRangeException("The timestamp is too far in the future and is likely incorrect.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (timestamp < DateTime.UtcNow.AddYears(-100))
        {
            throw new ArgumentOutOfRangeException("The timestamp is too far in the past and may be invalid.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, String sender, String receiver)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(sender);
        ArgumentException.ThrowIfNullOrWhiteSpace(sender);
        ArgumentException.ThrowIfNullOrEmpty(receiver);
        ArgumentException.ThrowIfNullOrWhiteSpace(receiver);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Sender", sender),
                   new System.Xml.Linq.XElement("Receiver", receiver));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentNullException("The sender information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentNullException("The receiver information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentException("The sender value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentException("The receiver value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (sender.Length > 50)
        {
            throw new FormatException("The sender name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (receiver.Length > 50)
        {
            throw new FormatException("The receiver name exceeds the maximum length of 50 characters.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, String sender, String receiver, Decimal amount)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(sender);
        ArgumentException.ThrowIfNullOrWhiteSpace(sender);
        ArgumentException.ThrowIfNullOrEmpty(receiver);
        ArgumentException.ThrowIfNullOrWhiteSpace(receiver);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Sender", sender),
                   new System.Xml.Linq.XElement("Receiver", receiver),
                   new System.Xml.Linq.XElement("Amount", amount));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentNullException("The sender information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentNullException("The receiver information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentException("The sender value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentException("The receiver value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentOutOfRangeException("The amount cannot be negative or exceed the allowed limit.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (sender.Length > 50)
        {
            throw new FormatException("The sender name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (receiver.Length > 50)
        {
            throw new FormatException("The receiver name exceeds the maximum length of 50 characters.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The specified amount exceeds the maximum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The specified amount is lower than the minimum allowed decimal value.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, String sender, String receiver, Decimal amount, String currency)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(sender);
        ArgumentException.ThrowIfNullOrWhiteSpace(sender);
        ArgumentException.ThrowIfNullOrEmpty(receiver);
        ArgumentException.ThrowIfNullOrWhiteSpace(receiver);
        ArgumentException.ThrowIfNullOrEmpty(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Sender", sender),
                   new System.Xml.Linq.XElement("Receiver", receiver),
                   new System.Xml.Linq.XElement("Amount", amount),
                   new System.Xml.Linq.XElement("Currency", currency));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentNullException("The sender information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentNullException("The receiver information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentNullException("The currency value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentException("The sender value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentException("The receiver value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentException("The currency value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentOutOfRangeException("The amount cannot be negative or exceed the allowed limit.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (sender.Length > 50)
        {
            throw new FormatException("The sender name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (receiver.Length > 50)
        {
            throw new FormatException("The receiver name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (currency.Length > 10)
        {
            throw new FormatException("The currency code exceeds the maximum length of 10 characters.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The specified amount exceeds the maximum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The specified amount is lower than the minimum allowed decimal value.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, String sender, String receiver, Decimal amount, String currency, DateTime timestamp)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(sender);
        ArgumentException.ThrowIfNullOrWhiteSpace(sender);
        ArgumentException.ThrowIfNullOrEmpty(receiver);
        ArgumentException.ThrowIfNullOrWhiteSpace(receiver);
        ArgumentException.ThrowIfNullOrEmpty(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new System.Xml.Linq.XElement("Transaction",
                   new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                   new System.Xml.Linq.XElement("Sender", sender),
                   new System.Xml.Linq.XElement("Receiver", receiver),
                   new System.Xml.Linq.XElement("Amount", amount),
                   new System.Xml.Linq.XElement("Currency", currency),
                   new System.Xml.Linq.XElement("Timestamp", timestamp.ToString("o")));
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentNullException("The sender information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentNullException("The receiver information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentNullException("The currency value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentException("The sender value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentException("The receiver value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentException("The currency value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentOutOfRangeException("The amount cannot be negative or exceed the allowed limit.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("timestamp"))
        {
            throw new ArgumentOutOfRangeException("The timestamp value is out of the acceptable range.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (sender.Length > 50)
        {
            throw new FormatException("The sender name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (receiver.Length > 50)
        {
            throw new FormatException("The receiver name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (currency.Length > 10)
        {
            throw new FormatException("The currency code exceeds the maximum length of 10 characters.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The specified amount exceeds the maximum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The specified amount is lower than the minimum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (timestamp > DateTime.MaxValue)
        {
            throw new OverflowException("The specified timestamp exceeds the maximum allowed date.", ex);
        }
        catch (OverflowException ex) when (timestamp < DateTime.MinValue)
        {
            throw new OverflowException("The specified timestamp is lower than the minimum allowed date.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromBlockchainTransaction(this Object obj, Guid transactionId, String sender, String receiver, Decimal amount, String currency, DateTime timestamp, Dictionary<String, String> metadata)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(sender);
        ArgumentException.ThrowIfNullOrWhiteSpace(sender);
        ArgumentException.ThrowIfNullOrEmpty(receiver);
        ArgumentException.ThrowIfNullOrWhiteSpace(receiver);
        ArgumentException.ThrowIfNullOrEmpty(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement transactionElement = new System.Xml.Linq.XElement("Transaction",
                                                          new System.Xml.Linq.XElement("ID", transactionId.ToString()),
                                                          new System.Xml.Linq.XElement("Sender", sender),
                                                          new System.Xml.Linq.XElement("Receiver", receiver),
                                                          new System.Xml.Linq.XElement("Amount", amount),
                                                          new System.Xml.Linq.XElement("Currency", currency),
                                                          new System.Xml.Linq.XElement("Timestamp", timestamp.ToString("o")));

            foreach (KeyValuePair<String, String> item in metadata)
                transactionElement.Add(new System.Xml.Linq.XElement(item.Key, item.Value));
            
            return transactionElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new ArgumentNullException("The input object is null, which is required to build the XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("transactionId"))
        {
            throw new ArgumentNullException("The transaction ID is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentNullException("The sender information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentNullException("The receiver information is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentNullException("The currency value is null, which is required for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("metadata"))
        {
            throw new ArgumentNullException("The metadata dictionary is null, which is required for XML processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("sender"))
        {
            throw new ArgumentException("The sender value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("receiver"))
        {
            throw new ArgumentException("The receiver value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("currency"))
        {
            throw new ArgumentException("The currency value is empty or consists only of white spaces, which is not allowed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("metadata"))
        {
            throw new ArgumentException("The metadata dictionary contains invalid or missing key-value pairs.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("amount"))
        {
            throw new ArgumentOutOfRangeException("The amount cannot be negative or exceed the allowed limit.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("timestamp"))
        {
            throw new ArgumentOutOfRangeException("The timestamp value is out of the acceptable range.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("metadata"))
        {
            throw new KeyNotFoundException("A required key in the metadata dictionary was not found.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error processing transaction metadata"))
        {
            throw new InvalidOperationException("There was an issue with processing the metadata during XML generation.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Guid"))
        {
            throw new InvalidOperationException("The provided transaction ID is not in a valid GUID format.", ex);
        }
        catch (FormatException ex) when (sender.Length > 50)
        {
            throw new FormatException("The sender name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (receiver.Length > 50)
        {
            throw new FormatException("The receiver name exceeds the maximum length of 50 characters.", ex);
        }
        catch (FormatException ex) when (currency.Length > 10)
        {
            throw new FormatException("The currency code exceeds the maximum length of 10 characters.", ex);
        }
        catch (OverflowException ex) when (amount > Decimal.MaxValue)
        {
            throw new OverflowException("The specified amount exceeds the maximum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (amount < Decimal.MinValue)
        {
            throw new OverflowException("The specified amount is lower than the minimum allowed decimal value.", ex);
        }
        catch (OverflowException ex) when (timestamp > DateTime.MaxValue)
        {
            throw new OverflowException("The specified timestamp exceeds the maximum allowed date.", ex);
        }
        catch (OverflowException ex) when (timestamp < DateTime.MinValue)
        {
            throw new OverflowException("The specified timestamp is lower than the minimum allowed date.", ex);
        }
        catch (NullReferenceException ex) when (ex.TargetSite is not null && ex.TargetSite.Name.Equals("BuildXmlFromBlockchainTransaction"))
        {
            throw new InvalidOperationException("A null reference was encountered while constructing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence contains no elements"))
        {
            throw new InvalidOperationException("An invalid operation occurred due to an empty collection in XML generation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Data.Count > 0)
        {
            throw new InvalidOperationException("The system ran out of memory while processing the transaction XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the transaction details.", ex);
        }
    }
}