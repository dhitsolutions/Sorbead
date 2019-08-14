using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    class MultiLanguageTools
    {
        InputLanguage ArabicInput;
        InputLanguage EnglishInput;
        InputLanguage OriginalInput;
        public void loadevent()
        {
            OriginalInput = InputLanguage.CurrentInputLanguage;
            ArabicInput = InputLanguage.CurrentInputLanguage;
            EnglishInput = InputLanguage.CurrentInputLanguage;
            int count = InputLanguage.InstalledInputLanguages.Count;
            for (int i = 0; i < count; i++)
            {
                if (InputLanguage.InstalledInputLanguages[i].Culture.DisplayName == "Arabic (Saudi Arabia)")
                    ArabicInput = InputLanguage.InstalledInputLanguages[i];
                

            }
        }
        public void changelang(TextBox txt)
        {
            InputLanguage.CurrentInputLanguage = ArabicInput;
            txt.RightToLeft = RightToLeft.Yes;
        }
        public void SetOrignallang(TextBox txt)
        {
            InputLanguage.CurrentInputLanguage = OriginalInput;
            
        }

    }
}
