using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class AnswerDirector
    {
        public void ConstructAnswer(IAnswerBuilder builder, string language)
        {
            builder.BuildAnswer(language);
            builder.BuildRandomAnswers();
            builder.GetResult();
        }
    }
}
