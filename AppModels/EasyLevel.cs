using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class EasyLevel : IDifficultyLevel
    {
        public int Answers()
        {
            return 2;
        }
    }
}
