using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    class AnswerIterator : IIterator
    {
        private int currentPosition = 0;
        private List<Answers> answers = new List<Answers>;
        public Answers GetNext()
        {
            if (!HasNext())
            {
                return null;
            }
            Answers answer = answers.Find(currentPosition);
            currentPosition++;

            return answer;

        }

        public bool HasNext()
        {
            currentPosition < answers.Count;

        }
    }
}
