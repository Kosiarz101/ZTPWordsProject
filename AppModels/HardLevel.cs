using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class HardLevel : IDifficultyLevel
    {
        public int Answers()
        {
            return 4;
        }
    }
}
