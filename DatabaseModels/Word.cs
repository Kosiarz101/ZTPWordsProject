using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.DatabaseModels
{
    public class Word
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public virtual ICollection<WordTranslation> WordTranslations { get; set; }
    }
}
