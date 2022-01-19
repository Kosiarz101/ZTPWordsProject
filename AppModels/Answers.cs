using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class Answers
    {
        private List<string> AAnswers;
        private string Question;
        //private string[] abnswers = new string[4];

        public Answers(List<string> answers, string question)
        {
            AAnswers = answers;
            Question = question;
            /*for(int i=0; i<AAnswers.Count(); i++)
            {
                abnswers[i] = AAnswers.ElementAt(i);
            }*/
        }

        public List<string> GetAnswers()
        {
            return AAnswers;
        }

        public string GetQuestion()
        {
            return Question;
        }
    }
}
