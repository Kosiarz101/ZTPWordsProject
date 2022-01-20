using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class HardestLevel : IDifficultyLevel
    {

        private string poziomTrudności = "Najtrudniejszy";
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
