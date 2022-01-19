using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class AnswerDirector
    {
        public void ConstructAnswer(IAnswerBuilder builder)
        {
            builder.BuildAnswer();
            builder.BuildRandomAnswers();
        }
    }
}
