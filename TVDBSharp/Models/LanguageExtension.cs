
namespace TVDBSharp.Models
{
    public static class LanguageExtension
    {
        public static string ToShort(this Language val)
        {
            switch (val)
            {
                case Language.Chinese:
                    return "zh";
                case Language.Croatian:
                    return "hr";
                case Language.Czech:
                    return "cs";
                case Language.Dansk:
                    return "da";
                case Language.Deutsch:
                    return "de";
                case Language.English:
                    return "en";
                case Language.Español:
                    return "es";
                case Language.Français:
                    return "fr";
                case Language.Greek:
                    return "el";
                case Language.Hebrew:
                    return "he";
                case Language.Italiano:
                    return "it";
                case Language.Japanese:
                    return "ja";
                case Language.Korean:
                    return "ko";
                case Language.Magyar:
                    return "hu";
                case Language.Nederlands:
                    return "nl";
                case Language.Norsk:
                    return "no";
                case Language.Polski:
                    return "pl";
                case Language.Portuguese:
                    return "pt";
                case Language.Russian:
                    return "ru";
                case Language.Slovenian:
                    return "sl";
                case Language.Suomeksi:
                    return "fi";
                case Language.Svenska:
                    return "sv";
                case Language.Turkish:
                    return "tr";
                default:
                    return null;
            }
        }

        public static int? ToID(this Language val)
        {
            switch (val)
            {
                case Language.Chinese:
                    return 27;
                case Language.Croatian:
                    return 31;
                case Language.Czech:
                    return 28;
                case Language.Dansk:
                    return 10;
                case Language.Deutsch:
                    return 14;
                case Language.English:
                    return 7;
                case Language.Español:
                    return 16;
                case Language.Français:
                    return 17;
                case Language.Greek:
                    return 20;
                case Language.Hebrew:
                    return 24;
                case Language.Italiano:
                    return 15;
                case Language.Japanese:
                    return 25;
                case Language.Korean:
                    return 32;
                case Language.Magyar:
                    return 19;
                case Language.Nederlands:
                    return 13;
                case Language.Norsk:
                    return 9;
                case Language.Polski:
                    return 18;
                case Language.Portuguese:
                    return 26;
                case Language.Russian:
                    return 22;
                case Language.Slovenian:
                    return 30;
                case Language.Suomeksi:
                    return 11;
                case Language.Svenska:
                    return 8;
                case Language.Turkish:
                    return 21;
                default:
                    return null;
            }
        }

        public static Language? ToLanguage(this string val)
        {
            switch (val)
            {
                case "zh":
                    return Language.Chinese;
                case "hr":
                    return Language.Croatian;
                case "cs":
                    return Language.Czech;
                case "da":
                    return Language.Dansk;
                case "de":
                    return Language.Deutsch;
                case "en":
                    return Language.English;
                case "es":
                    return Language.Español;
                case "fr":
                    return Language.Français;
                case "el":
                    return Language.Greek;
                case "he":
                    return Language.Hebrew;
                case "it":
                    return Language.Italiano;
                case "ja":
                    return Language.Japanese;
                case "ko":
                    return Language.Korean;
                case "hu":
                    return Language.Magyar;
                case "nl":
                    return Language.Nederlands;
                case "no":
                    return Language.Norsk;
                case "pl":
                    return Language.Polski;
                case "pt":
                    return Language.Portuguese;
                case "ru":
                    return Language.Russian;
                case "sl":
                    return Language.Slovenian;
                case "fi":
                    return Language.Suomeksi;
                case "sv":
                    return Language.Svenska;
                case "tr":
                    return Language.Turkish;
                default:
                    return null;
            }
        }
    }
}
