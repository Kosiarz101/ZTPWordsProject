using System;
using System.Collections.Generic;
using System.Text;

namespace ZTPWordsProject.AppModels
{
    public class MediumLevel : IDifficultyLevel 
    {
        private string poziomTrudności = "Średni";
        public int Answers()
        {
            return 3;
        }
        public string getPoziomTrudności()
        {
            return poziomTrudności;
        }
    }
}
