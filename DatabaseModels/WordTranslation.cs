using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.DatabaseModels
{
    public class WordTranslation
    {
        public int WordId { get; set; }
        public virtual Word Word { get; set; }
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
    }
}
