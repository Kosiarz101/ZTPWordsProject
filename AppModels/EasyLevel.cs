using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class EasyLevel : IDifficultyLevel
    {
        public string[] Answers()
        {
            string[] answers = { "Odpowiedź 1", "Odpowiedź 2" };
            return answers;
        }
    }
}
