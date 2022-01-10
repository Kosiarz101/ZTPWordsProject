using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTPWordsProject.DatabaseModels
{
    public static class Words
    {
        public static bool AddWord(Word word, ApplicationDbContext context)
        {
            if(context.Words.Any(x => x.Content == word.Content))
            {
                return false;
            }
            context.Words.Add(word);
            context.SaveChanges();
            return true;
        }
        public static void RemoveWordByContent(string content, string language, ApplicationDbContext context)
        {
            Word word = context.Words.Where(x => x.Content == content && x.Language == language).FirstOrDefault();
            if(word == null)
            {
                return;
            }
            context.Words.Remove(word);
            context.SaveChanges();
        }
    }
}
