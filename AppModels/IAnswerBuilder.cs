using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public interface IAnswerBuilder
    {
        public void BuildAnswer(string language);
        public void BuildRandomAnswers();
        public Answers GetResult();
    }
}
