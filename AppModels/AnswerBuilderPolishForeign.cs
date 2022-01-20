using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class AnswerBuilderPolishForeign : IAnswerBuilder
    {
        private string language;
        private WordDatabase instance;
        private List<WordWithTranslations> words;
        private static int pom = 0;
        private int pom2;
        private int pom3;
        private string question;
        private WordWithTranslations word;
        private string Answer;
        private List<string> answers = new List<string>();

        public void BuildAnswer(string language)
        {
            instance = WordDatabase.GetInstance();
            answers.Clear();
            words = instance.GetAllWordsFromLanguage(language);
            if (pom > words.Count())
                return;
            word = words.ElementAt(pom);
            question = word.Content;
            Answer = word.TranslationList.ElementAt(0);
            answers.Add(Answer);
            pom = (pom + 1) % words.Count;
        }
        public void BuildRandomAnswers()
        {
            if (pom > words.Count())
                return;
            for (int i = 0; i < 3; i++)
            {
                pom2 = words.Count();
                Random rnd = new Random();
                pom3 = rnd.Next(pom2);
                answers.Add(words.ElementAt(pom3).Content);
            }
        }
        public Answers GetResult()
        {
            return new Answers(answers, question);
        }
    }
}
