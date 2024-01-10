using HexAndBit;


List<TagList> tagListValue = new List<TagList>();

Console.Write("Enter HexadecimalValue: ");
var readHexaDecimalValue = Console.ReadLine()!;
var h = readHexaDecimalValue.Replace(" ", "");

RecursiveLoopTagValues(0, 2, h);
foreach (var tag in tagListValue)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Tag: {tag.TagValue}, Length: {tag.TagLentghValue},TagName : {tag.TagName}, Value: {tag.DecimalTagValue}");
    Console.ResetColor();
}

void RecursiveLoopTagValues(int x, int y, string h)
{
    if (h.Length == 0)
        return;

    string subHex = h.Substring(x, y);
    string value = ConverterHexAndBinary.HexToBinary2(subHex);
    string byteData = value.Substring(2);

    if (byteData == "011111")
    {
        RecursiveLoopTagValues(x + 2, y, h);
    }
    else
    {
        // TRY CATCH KONTROLÜ YAPILACAK

        try
        {
            x = x + 2;
            string tagValue = h.Substring(0, x);
            string tagLengthValue = h.Substring(x, 2);
            long decimalTagValue = ConverterHexAndBinary.HexToDecimal(tagLengthValue);
            string valueTag = h.Substring(x + 2, (int)decimalTagValue * 2);

            tagListValue.Add(new TagList
            {
                TagName= ConverterHexAndBinary.emvTagNames
                            .FirstOrDefault(x => x.Key.Equals(tagValue))
                            .Value?
                            .ToString() ?? "Unknown Tag",
                TagValue = tagValue,
                TagLentghValue = tagLengthValue,
                DecimalTagValue = valueTag,
            });

            string remaining = h.Substring(tagValue.Length + tagLengthValue.Length + (int)decimalTagValue * 2);

            if (IsConstructedTag(tagValue))
            {
                RecursiveLoopTagValues(0, 2, valueTag);
            }
            else if (remaining.Length > 0)
            {
                RecursiveLoopTagValues(0, 2, remaining);
            }
        }
        catch (Exception)
        {
            
            return;
        }
        
    }
}

// TRY CATCH KONTROLÜ YAPILACAK
static bool IsConstructedTag(string tag)
{
    try
    {
        int firstByte = Convert.ToInt32(tag.Length > 0 ? tag.Substring(0, 2) : "00", 16);

        // Constructed kontrolü yapılır. Bit 6 
        //0x20(yani 32 ondalık veya 00100000 ikilik)

        return (firstByte & 0x20) == 0x20;
    }
    catch (Exception)
    {

        return false;
    }
    
}
















