using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZTPWordsProject.DatabaseModels;

namespace ZTPWordsProject.AppModels
{
    public class WordDatabase
    {
        private static WordDatabase instance = null;
        private readonly ApplicationDbContext context;
        WordDatabase()
        {
            context = new ApplicationDbContext();
        }
        public static WordDatabase GetInstance()
        {
            if(instance == null)
            {
                instance = new WordDatabase();
            }
            return instance;
        }
        public bool AddWord(Word word)
        {
            if (context.Words.Any(x => x.Content == word.Content && x.Language == word.Language) || word.Content == "")
            {
                return false;
            }
            Words.AddWord(word, context);
            return true;
        }
        public bool AddTranslationToWord(string content, string language, string translation)
        {
            WordTranslation wordTranslation;
            //Wyszukaj słowo w bazie danych
            Word word = context.Words.Where(x => x.Content == content && x.Language == language).FirstOrDefault();
            if (word == null)
                return false;
            //Wyszukaj tłumaczenie w bazie danych
            Translation translationDatabase = context.Translations.Where(x => x.Content == translation).FirstOrDefault();
            //Jeżeli tłumaczenia nie ma, dodaj je
            if(translationDatabase == null)
            {
                translationDatabase = new Translation()
                {
                    Content = translation
                };
                context.Translations.Add(translationDatabase);
                context.SaveChanges();
                translationDatabase = context.Translations.Where(x => x.Content == translation).FirstOrDefault();
            }
            //Jeżeli istnieje, sprawdź czy nie ma już połączenia słowa z danym tłumaczeniem. Jeżeli istnieje, zwróć fałsz
            else
            {
                wordTranslation = context.WordTranslations.Find(translationDatabase.Id, word.Id);
                if(wordTranslation != null)
                {
                    return false;
                }
            }
            //Dodaj tabelę pośrednicząca (połącz tłumaczenie ze słowem)
            wordTranslation = new WordTranslation()
            {
                WordId = word.Id,
                TranslationId = translationDatabase.Id
            };
            context.WordTranslations.Add(wordTranslation);
            context.SaveChanges();
            return true;
        }
        public void RemoveTranslationFromWord(string content, string language, string translation)
        {
            WordTranslation wordTranslation;
            //Wyszukaj słowo w bazie danych
            Word word = context.Words.Where(x => x.Content == content && x.Language == language).FirstOrDefault();
            if (word == null)
                return;
            //Wyszukaj tłumaczenie w bazie danych
            Translation translationDatabase = context.Translations.Where(x => x.Content == translation).FirstOrDefault();
            //Znajdź połączenie słowa z tłumaczeniem w bazie danych
            wordTranslation = context.WordTranslations.Find(translationDatabase.Id, word.Id);
            //Usuń z bazy danych
            context.WordTranslations.Remove(wordTranslation);
            context.SaveChanges();
            //Jeżeli tłumaczenie nie jest połączone z żadnych słowem, usuń je
            if(!context.WordTranslations.Any(x => x.TranslationId == translationDatabase.Id))
            {
                context.Translations.Remove(translationDatabase);
                context.SaveChanges();
            }
        }
        public List<string> GetAllLanguages()
        {
            List<string> languages = context.Words.Select(x => x.Language).Distinct().ToList();
            return languages;
        }
        public WordWithTranslations GetWordWithTranslationsByContent(string content)
        {
            Word word = context.Words.Where(x => x.Content == content).FirstOrDefault();
            if(word == null)
            {
                return null;
            }
            List<string> translations = context.WordTranslations
                .Where(x => x.WordId == word.Id)
                .Include(x => x.Translation)
                .Select(x => x.Translation.Content)
                .ToList();
            WordWithTranslations wordWithTranslations = new WordWithTranslations(word.Language, word.Content, translations);
            return wordWithTranslations;
        }
        public List<WordWithTranslations> GetWordWithTranslationsBySearch(string content, string language)
        {
            List<WordWithTranslations> wordWithTranslationsList = new List<WordWithTranslations>();
            List<Word> words;

            if (language == "Wszystkie")
                words = context.Words.Where(x => x.Content.Contains(content)).ToList();
            else
                words = context.Words.Where(x => x.Content.Contains(content) && x.Language == language).ToList();

            if(words == null)
            {
                return null;
            }
            foreach(Word word in words)
            {
                List<string> translations = context.WordTranslations
                .Where(x => x.WordId == word.Id)
                .Include(x => x.Translation)
                .Select(x => x.Translation.Content)
                .ToList();
                WordWithTranslations wordWithTranslations = new WordWithTranslations(word.Language, word.Content, translations);
                wordWithTranslationsList.Add(wordWithTranslations);
            }
            return wordWithTranslationsList;
        }
        public void RemoveWordByContent(string content, string language)
        {
            Word word = context.Words.Where(x => x.Content == content && x.Language == language).FirstOrDefault();
            if (word == null)
                return;
            context.Words.Remove(word);
            context.SaveChanges();
        }

        public List<WordWithTranslations> GetAllWordsFromLanguage(string language)
        {
            List<WordWithTranslations> wordWithTranslationsList = new List<WordWithTranslations>();
            List<Word> words;

            if (language == "Wszystkie")
                words = context.Words.ToList();
            else
                words = context.Words.Where(x => x.Language == language).ToList();
            if (words == null)
            {
                return null;
            }
            foreach (Word word in words)
            {
                List<string> translations = context.WordTranslations
                .Where(x => x.WordId == word.Id)
                .Include(x => x.Translation)
                .Select(x => x.Translation.Content)
                .ToList();
                WordWithTranslations wordWithTranslations = new WordWithTranslations(word.Language, word.Content, translations);
                wordWithTranslationsList.Add(wordWithTranslations);
            }

            return wordWithTranslationsList;
        }
    }
}
