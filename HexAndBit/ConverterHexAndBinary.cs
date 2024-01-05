using System.Text;

namespace HexAndBit
{
    public static class ConverterHexAndBinary
    {
        public static string HexToBinary(string hex)
        {
            var list=new List<string>();

            if (string.IsNullOrEmpty(hex))
                return hex;

            var trimHex=hex.Trim();
            string binary = "";
            foreach (char c in trimHex)
            {
                int value = Convert.ToInt32(c.ToString(),16);
                binary += Convert.ToString(value,2).PadLeft(4, '0');
                
            }

            list.Add(binary);
            return binary;
        }

        public static string HexToBinary2(string hex)
        {
            if (string.IsNullOrEmpty(hex) || !IsValidHex(hex))
            {
                Console.WriteLine("Hata: Geçerli bir onaltılık sayı değil.");
                return hex;
            }

            string binaryString = string.Join(string.Empty,
                hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            return binaryString;
        }

        private static bool IsValidHex(string hex)
        {
            foreach (char c in hex)
            {
                if (!IsHexadecimalDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsHexadecimalDigit(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'A' && c <= 'F') ||
                   (c >= 'a' && c <= 'f');
        }

        public static long HexToDecimal(string hex)
        {
            long decimalValue = 0;

            if (IsValidHex(hex))
            {
                decimalValue = HexToDecimall(hex);
                return decimalValue;
            }
            else
            {
                Console.WriteLine("Hatalı giriş! Geçerli bir onaltılık sayı giriniz.");
            }

            return decimalValue;
        }
        static long HexToDecimall(string hex)
        {
            return Convert.ToInt64(hex, 16);
        }
        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static List<string> BitleriGruplaraAyir(string bitDizisi)
        {
            var list=new List<string>();
            int grupSayisi = bitDizisi.Length / 8;
            string[] grupluBitler = new string[grupSayisi];

            for (int i = 0; i < grupSayisi; i++)
            {
                grupluBitler[i] = bitDizisi.Substring(i * 8, 8);
                list.Add(bitDizisi.Substring(i * 8, 8));
            }

            return list;
        }

        public static List<TLVElement> ParseTLV(string hexData)
        {
            List<TLVElement> tlvElements = new List<TLVElement>();
            int index = 0;

            while (index < hexData.Length)
            {
                string tag = hexData.Substring(index, 2);
                index += 2;

                int length = Convert.ToInt32(hexData.Substring(index, 2), 16);
                index += 2;

                string value = hexData.Substring(index, length * 2);
                index += length * 2;

                TLVElement tlvElement = new TLVElement(tag, length, value);
                tlvElements.Add(tlvElement);
            }

            return tlvElements;
        }
    }

    public class TLVElement
    {
        public string Tag { get; }
        public int Length { get; }
        public string Value { get; }

        public TLVElement(string tag, int length, string value)
        {
            Tag = tag;
            Length = length;
            Value = value;
        }

    }

}
