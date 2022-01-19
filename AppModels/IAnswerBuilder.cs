using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public interface IAnswerBuilder
    {
        public void BuildAnswer();
        public void BuildRandomAnswers();
        public WordWithTranslations GetResult();
    }
}
