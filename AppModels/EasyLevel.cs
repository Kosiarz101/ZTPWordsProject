using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class EasyLevel : IDifficultyLevel
    {
        private string poziomTrudności = "Łatwy";
        public int Answers()
        {
            return 2;
        }

        public string getPoziomTrudności()
        {
            return poziomTrudności;
        }
    }
}
