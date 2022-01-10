using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class WordWithTranslations
    {
        public string Language { get; set; }
        public string Content { get; set; }
        public List<string> TranslationList { get; set; }
        public WordWithTranslations(string language, string content, List<string> translationList)
        {
            Language = language;
            Content = content;
            TranslationList = translationList;
        }
    }
}
