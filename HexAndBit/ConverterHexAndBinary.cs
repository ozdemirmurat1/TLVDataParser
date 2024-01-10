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
            if (string.IsNullOrEmpty(hex))
            {
                Console.WriteLine("Hata: Hex değeri boş veya null olamaz.");
                throw new ArgumentException("Hata: Hex değeri boş veya null olamaz.");
            }

            if (!IsValidHex(hex))
            {
                Console.WriteLine("Hata: Geçerli bir onaltılık sayı değil.");
                throw new ArgumentException("Hata: Geçerli bir onaltılık sayı değil.");
            }

            try
            {
                string binaryString = string.Join(string.Empty,
                    hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                return binaryString;
            }
            catch (Exception ex)
            {
                // Burada, dönüşüm sırasında oluşabilecek herhangi bir hatayı yakalayıp konsola yazdırıyoruz.
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                throw new ArgumentException($"Bir hata oluştu: { ex.Message }");
            }
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

        public static Dictionary<string, string> emvTagNames = new Dictionary<string, string>
    {
        {"9F01", "Acquirer Identifier"},
        {"9F40", "Additional Terminal Capabilities"},
        {"81", "Amount, Authorised (Binary)"},
        {"9F02", "Amount, Authorised (Numeric)"},
        {"9F04", "Amount, Other (Binary)"},
        {"9F03", "Amount, Other (Numeric)"},
        {"9F3A", "Amount, Reference Currency"},
        {"9F26", "Application Cryptogram"},
        {"9F42", "Application Currency Code"},
        {"9F44", "Application Currency Exponent"},
        {"9F05", "Application Discretionary Data"},
        {"5F25", "Application Effective Date"},
        {"5F24", "Application Expiration Date"},
        {"94", "Application File Locator (AFL)"},
        {"4F", "Application Identifier (AID) – card"},
        {"9F06", "Application Identifier (AID) – terminal"},
        {"82", "Application Interchange Profile"},
        {"50", "Application Label"},
        {"9F12", "Application Preferred Name"},
        {"5A", "Application Primary Account Number (PAN)"},
        {"5F34", "Application Primary Account Number (PAN) Sequence Number"},
        {"87", "Application Priority Indicator"},
        {"9F3B", "Application Reference Currency"},
        {"9F43", "Application Reference Currency Exponent"},
        {"61", "Application Template"},
        {"9F36", "Application Transaction Counter (ATC)"},
        {"9F07", "Application Usage Control"},
        {"9F08", "Application Version Number"},
        {"9F09", "Application Version Number"},
        {"89", "Authorisation Code"},
        {"8A", "Authorisation Response Code"},
        {"5F54", "Bank Identifier Code (BIC)"},
        {"8C", "Card Risk Management Data Object List 1 (CDOL1)"},
        {"8D", "Card Risk Management Data Object List 2 (CDOL2)"},
        {"5F20", "Cardholder Name"},
        {"9F0B", "Cardholder Name Extended"},
        {"8E", "Cardholder Verification Method (CVM) List"},
        {"9F34", "Cardholder Verification Method (CVM) Results"},
        {"8F", "Certification Authority Public Key Index"},
        {"9F22", "Certification Authority Public Key Index"},
        {"83", "Command Template"},
        {"9F27", "Cryptogram Information Data"},
        {"9F45", "Data Authentication Code"},
        {"84", "Dedicated File (DF) Name"},
        {"9D", "Directory Definition File (DDF) Name"},
        {"73", "Directory Discretionary Template"},
        {"9F49", "Dynamic Data Authentication Data Object List (DDOL)"},
        {"70", "EMV Proprietary Template"},
        {"BF0C", "File Control Information (FCI) Issuer Discretionary Data"},
        {"A5", "File Control Information (FCI) Proprietary Template"},
        {"6F", "File Control Information (FCI) Template"},
        {"9F4C", "ICC Dynamic Number"},
        {"9F2D", "Integrated Circuit Card (ICC) PIN Encipherment Public Key Certificate"},
        {"9F2E", "Integrated Circuit Card (ICC) PIN Encipherment Public Key Exponent"},
        {"9F2F", "Integrated Circuit Card (ICC) PIN Encipherment Public Key Remainder"},
        {"9F46", "Integrated Circuit Card (ICC) Public Key Certificate"},
        {"9F47", "Integrated Circuit Card (ICC) Public Key Exponent"},
        {"9F48", "Integrated Circuit Card (ICC) Public Key Remainder"},
        {"9F1E", "Interface Device (IFD) Serial Number"},
        {"5F53", "International Bank Account Number (IBAN)"},
        {"9F0D", "Issuer Action Code – Default"},
        {"9F0E", "Issuer Action Code – Denial"},
        {"9F0F", "Issuer Action Code – Online"},
        {"9F10", "Issuer Application Data"},
        {"91", "Issuer Authentication Data"},
        {"9F11", "Issuer Code Table Index"},
        {"5F28", "Issuer Country Code"},
        {"5F55", "Issuer Country Code (alpha2 format)"},
        {"5F56", "Issuer Country Code (alpha3 format)"},
        {"42", "Issuer Identification Number (IIN)"},
        {"90", "Issuer Public Key Certificate"},
        {"9F32", "Issuer Public Key Exponent"},
        {"92", "Issuer Public Key Remainder"},
        {"86", "Issuer Script Command"},
        {"9F18", "Issuer Script Identifier"},
        {"71", "Issuer Script Template 1"},
        {"72", "Issuer Script Template 2"},
        {"5F50", "Issuer URL"},
        {"5F2D", "Language Preference"},
        {"9F13", "Last Online Application Transaction Counter (ATC) Register"},
        {"9F4D", "Log Entry"},
        {"9F4F", "Log Format"},
        {"9F14", "Lower Consecutive Offline Limit"},
        {"9F15", "Merchant Category Code"},
        {"9F16", "Merchant Identifier"},
        {"9F4E", "Merchant Name and Location"},
        {"9F17", "Personal Identification Number (PIN) Try Counter"},
        {"9F39", "Point-of-Service (POS) Entry Mode"},
        {"9F38", "Processing Options Data Object List (PDOL)"},
        {"80", "Response Message Template Format 1"},
        {"77", "Response Message Template Format 2"},
        {"5F30", "Service Code"},
        {"88", "Short File Identifier (SFI)"},
        {"9F4B", "Signed Dynamic Application Data"},
        {"93", "Signed Static Application Data"},
        {"9F4A", "Static Data Authentication Tag List"},
        {"9F33", "Terminal Capabilities"},
        {"9F1A", "Terminal Country Code"},
        {"9F1B", "Terminal Floor Limit"},
        {"9F1C", "Terminal Identification"},
        {"9F1D", "Terminal Risk Management Data"},
        {"9F35", "Terminal Type"},
        {"95", "Terminal Verification Results"},
        {"9F1F", "Track 1 Discretionary Data"},
        {"9F20", "Track 2 Discretionary Data"},
        {"57", "Track 2 Equivalent Data"},
        {"98", "Transaction Certificate (TC) Hash Value"},
        {"97", "Transaction Certificate Data Object List (TDOL)"},
        {"5F2A", "Transaction Currency Code"},
        {"5F36", "Transaction Currency Exponent"},
        {"9A", "Transaction Date"},
        {"99", "Transaction Personal Identification Number (PIN) Data"},
        {"9F3C", "Transaction Reference Currency Code"},
        {"9F3D", "Transaction Reference Currency Exponent"},
        {"9F41", "Transaction Sequence Counter"},
        {"9B", "Transaction Status Information"},
        {"9F21", "Transaction Time"},
        {"9C", "Transaction Type"},
        {"9F37", "Unpredictable Number"},
        {"9F23", "Upper Consecutive Offline Limit"},
        // Diğer tag'lar...
    };
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
