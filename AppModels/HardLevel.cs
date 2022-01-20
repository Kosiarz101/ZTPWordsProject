using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class HardLevel : IDifficultyLevel
    {
        private string poziomTrudności = "Trudny";
        public int Answers()
        {
            return 4;
        }

        public string getPoziomTrudności()
        {
            return poziomTrudności;
        }
    }
}
