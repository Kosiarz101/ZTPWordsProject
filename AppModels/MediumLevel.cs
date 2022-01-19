using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class MediumLevel : IDifficultyLevel 
    {
        public int Answers()
        {
            return 3;
        }
    }
}
