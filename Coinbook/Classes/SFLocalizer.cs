using Syncfusion.Windows.Forms;

namespace Coinbook
{
    /// <summary>
    /// LocalizationProvider für Syncfusion Controls
    /// </summary>
    public class SFLocalizer : ILocalizationProvider
    {
        public string GetLocalizedString(System.Globalization.CultureInfo culture, string name, object obj)
        {

            switch (name)
            {

                // Yes Button in German Language
                case ResourceIdentifiers.Yes:
                    return LanguageHelper.Localization.GetTranslation("Keys", "msgYes");

                // No Button in German Language
                case ResourceIdentifiers.No:
                    return LanguageHelper.Localization.GetTranslation("Keys", "msgNo");

                // default
                default:
                    return string.Empty;
            }
        }
    }
}
