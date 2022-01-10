using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTPWordsProject.DatabaseModels
{
    public static class PolishTranslations
    {
        public static bool AddWord(Translation word, ApplicationDbContext context)
        {
            if (context.Translations.Any(x => x.Content == word.Content))
            {
                return false;
            }
            context.Translations.Add(word);
            context.SaveChanges();
            return true;
        }
        public static void RemoveWordByContent(string content, ApplicationDbContext context)
        {
            Translation word = context.Translations.Where(x => x.Content == content).FirstOrDefault();
            if (word == null)
            {
                return;
            }
            context.Translations.Remove(word);
            context.SaveChanges();
        }
    }
}
