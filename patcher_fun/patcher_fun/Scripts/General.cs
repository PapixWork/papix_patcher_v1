// Papix Work ~ https://metin2.dev/profile/47045-papix/
using System.Globalization;

namespace patcher_fun.Scripts
{
    internal class General
    {
        public static string GetLang()
        {
            return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}
