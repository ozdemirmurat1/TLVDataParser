using HexAndBit;


Console.Write("Enter HexadecimalValue:");

var readHexaDecimalValue = Console.ReadLine()!;
var h = readHexaDecimalValue.Trim();

var tagListValue = new List<TagList>();

int x = 0;
int y = 2;
int count = 0;

var tagValues = RecursiveLoopTagValues(x, y, h);

string RecursiveLoopTagValues(int x, int y, string h)
{
    string byteData = "";
    string subHex = "";
    string tagValue = "";
    string tagLengthValue = "";
    string value = "";
    string valueTag = "";
    long decimalTagValue;
    string gg = "";

    ;


    // YAPILACAK
    if (h.Length == 0)
        return Dondur();
    subHex = h.Substring(x, y);



    if (subHex.Length > 0)
        value = ConverterHexAndBinary.HexToBinary2(subHex);


    byteData = value.Substring(2);
    // Geri kalan işlemler


    if ((byteData == "011111"))
    {
        RecursiveLoopTagValues(x + 2, y, h);

    }
    else
    {
        x = x + 2;

        tagValue = h?.Substring(0, x);
        // Geri kalan işlemler

        tagLengthValue = h?.Substring(x, 2);


        decimalTagValue = ConverterHexAndBinary.HexToDecimal(tagLengthValue);


        // Sıkıntı çıkartan yer
        valueTag = (h?.Substring(x + 2, (int)decimalTagValue * 2))!;

        tagListValue.Add(new TagList
        {
            TagValue = tagValue,
            TagLentghValue = tagLengthValue,
            DecimalTagValue = valueTag,
        });



        gg = h?.Substring(tagValue.Length + tagLengthValue.Length + (int)decimalTagValue * 2);


        var foundItem = tagListValue.FirstOrDefault(x => x.TagValue == "70");

        // 9F'Lİ datanın buraya girmesi lazım.

        if (foundItem != null && count == 0)
        {
            if (gg.Length > 0 && count != 0)
            {
                RecursiveLoopTagValues(x = 0, y, gg);
                count++;
            }

            else if (foundItem != null && count == 0)
                count++;
            RecursiveLoopTagValues(x = 0, y, valueTag);

        }
        else
        {
            RecursiveLoopTagValues(x = 0, y, gg);
        }

    }
    return "";

}

string Dondur()
{
    foreach (var item in tagListValue)
    {
        Console.WriteLine("Tag: " + " " + item.TagValue + " " + "Length" + " " + item.TagLentghValue + " " + "Value" + " " + item.DecimalTagValue);
    }

    return string.Empty;
}















