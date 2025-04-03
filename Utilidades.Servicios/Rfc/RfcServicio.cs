using System.Text;
using System.Text.RegularExpressions;

namespace Utilidades.Servicios.Rfc
{
    public class RfcServicio
    {
        public static string CalculateRFCHomonym(TipoPersona tipoPersona, string name, string firstSurrname, string secondSurrname, DateTime birthDate)
        {
            name = CleadString(name).ToUpper().RemoveAccents();
            firstSurrname = CleadString(firstSurrname).ToUpper().RemoveAccents() ?? string.Empty;
            secondSurrname = CleadString(secondSurrname).ToUpper().RemoveAccents() ?? string.Empty;

            var rfcBase = GenerateRFCBase(tipoPersona, name, firstSurrname, secondSurrname, birthDate);

            var fullName = $"{firstSurrname} {secondSurrname} {name}";

            string rfcHomonymKey = GenerateHomonymKeyOld(rfcBase, fullName);
            string digitVerify = CalculateCheckDigitOld(rfcHomonymKey);

            return $"{rfcHomonymKey}{digitVerify[0]}";
        }

        private static string GenerateRFCBase(TipoPersona tipoPersona, string name, string firstSurrname, string secondSurrname, DateTime birthDate)
        {
            var rfcBase = "";
            var homonymKey = "";
            var checkDigit = "";
            switch (tipoPersona)
            {
                case TipoPersona.Fisica:
                    rfcBase = GenerateRFCFisica(name, firstSurrname, secondSurrname, birthDate);
                    //homonymKey = GenerateHomonymKey(rfcBase);
                    //checkDigit = CalculateCheckDigit(rfcBase + homonymKey);
                    return rfcBase;
                case TipoPersona.Moral:
                    return GenerateRFCMoral(name, birthDate);
                default:
                    throw new ArgumentException("Invalid Person Tipe");
            }

        }

        private static string GenerateRFCFisica(string name, string? firstSurrname, string? secondSurrname, DateTime birthDate)
        {

            name = CleadString(name);
            firstSurrname = CleadString(firstSurrname) ?? string.Empty;
            secondSurrname = CleadString(secondSurrname) ?? string.Empty;

            string letters = GetRFCLetters(name, firstSurrname, secondSurrname);

            string date = birthDate.ToString("yyMMdd");

            string rfc = letters + date;

            return rfc;
        }

        private static string GenerateRFCMoral(string name, DateTime constitutionDate)
        {
            name = CleadString(name);
            string letters = name.Substring(0, Math.Min(3, name.Length)).ToUpper();

            string date = constitutionDate.ToString("yyMMdd");

            string rfc = letters + date;

            return rfc;
        }

        private static string CleadString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            string normalized = text.Normalize(NormalizationForm.FormD);
            Regex regx = new Regex(@"[^a-zA-Z\s]");
            string cleanText = regx.Replace(normalized, "").ToUpper();
            return cleanText;
        }

        private static string GetRFCLetters(string name, string firstSurrname, string secondSurrname)
        {
            char firstLetterFirstSurrname = !string.IsNullOrEmpty(firstSurrname) ? firstSurrname[0] : secondSurrname[1];
            char firstInternVowel = !string.IsNullOrEmpty(firstSurrname) ? GetFirstVowel(firstSurrname).ToCharArray()[0] : GetFirstVowel(secondSurrname).ToCharArray()[1];
            char firstLetterSecondSirrname = !string.IsNullOrEmpty(secondSurrname) ? secondSurrname[0] : firstSurrname[1];

            char firstLetterName = name[0];

            return $"{firstLetterFirstSurrname}{firstInternVowel}{firstLetterSecondSirrname}{firstLetterName}".ToUpper();
        }

        private static string GetFirstVowel(string text)
        {
            foreach (char c in text.ToLower())
            {
                if ("aeiou".Contains(c))
                {
                    return c.ToString().ToUpper();
                }
            }
            return "X";
        }

        private static string GenerateHomonymKeyOld(string baseRFC, string fullName)
        {
            string convertExhibitOne = "0";
            char[] fullnameCharArray = fullName.ToCharArray();
            foreach (char c in fullnameCharArray)
                convertExhibitOne = $"{convertExhibitOne}{c.AnexoOne()}";

            int sum = 0;
            char[] convertExhibitOneCharArray = convertExhibitOne.ToCharArray();
            for (int i = 0; i < convertExhibitOneCharArray.Length - 1; i++)
            {
                char aux = convertExhibitOneCharArray[i + 1];
                int value1 = Convert.ToInt32($"{convertExhibitOneCharArray[i]}{aux}");
                int value2 = Convert.ToInt32($"{aux}");
                int mult = value1 * value2;
                sum += mult;
            }

            int thirtyFourNumber = 34;
            int result = Convert.ToInt32(sum.ToString()[^3..]);
            int cociente = result / thirtyFourNumber;
            int residuo = result % thirtyFourNumber;

            string rfcHomonym = $"{baseRFC}{cociente.ToString().AnexoTwo()}{residuo.ToString().AnexoTwo()}";
            var tuple = rfcHomonym.SeparateText();
            rfcHomonym = $"{tuple.alphaPart.DisadvantagesWords()}{tuple.numericPart}";

            return rfcHomonym;
        }

        private static string CalculateCheckDigitOld(string rfcHomonymKey)
        {
            int result = 0;
            int thirteenNumber = 13;
            char[] rfchomonymCharArray = rfcHomonymKey.ToCharArray();
            foreach (var c in rfchomonymCharArray)
            {
                string aux = c.AnexoThree();
                result += Convert.ToInt32(aux) * thirteenNumber;
                thirteenNumber--;
            }

            int elevenNumber = 11;
            int residuo = result % elevenNumber;
            string digitVerify = "0";

            if (residuo == 10)
            {
                digitVerify = "A";
            }
            else if (residuo > 0)
            {
                digitVerify = (elevenNumber - residuo).ToString();
            }

            return digitVerify;
        }
    }//end class
}