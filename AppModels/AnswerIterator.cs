using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    class AnswerIterator : IIterator
    {
        private int currentPosition = 0;
        private List<Answers> answers = new List<Answers>();

        public AnswerIterator(List<Answers> answers)
        {
            this.answers = answers;
        }
        public Answers GetNext()
        {
            if (!HasNext())
            {
                return null;
            }
            Answers answer = answers[currentPosition];
            currentPosition++;

            return answer;

        }

        public bool HasNext()
        {
            if(currentPosition < answers.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

    }
}
