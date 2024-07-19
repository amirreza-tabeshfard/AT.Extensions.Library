namespace AT.Infrastructure;
public struct TextElementSegment
{
    private readonly Int32 offset;
    private readonly Int32 length;

    public TextElementSegment(Int32 offset, Int32 length)
    {
        if (offset < 0)
            throw new ArgumentOutOfRangeException("offset should be larger or equal to 0");
        if (length <= 0)
            throw new ArgumentOutOfRangeException("length should be larger than 0");

        this.offset = offset;
        this.length = length;
    }

    public Int32 Offset { get { return offset; } }

    public Int32 Length { get { return length; } }
}