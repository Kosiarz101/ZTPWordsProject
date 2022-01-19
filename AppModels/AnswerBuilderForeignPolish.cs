using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class AnswerBuilderForeignPolish : IAnswerBuilder
    {
        public void BuildAnswer()
        {

        }
        public void BuildRandomAnswers()
        {

        }
        public WordWithTranslations GetResult()
        {
            return new WordWithTranslations(null, null, null);
        }
    }
}
