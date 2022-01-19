using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class HardestLevel : IDifficultyLevel
    {
        public int Answers()
        {
            return 4;
        }
    }
}
