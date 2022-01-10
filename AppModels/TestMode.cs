using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ZTPWordsProject.AppModels
{
    public class TestMode : IMode
    {
        public bool CheckAnswer(ref int points, string userAnswer, string correctAnswer)
        {
            if(userAnswer == correctAnswer)
                points++;
            return true;
        }

        public void SetTextBlock(TextBlock textBlock)
        {
            textBlock.Text = "Testu";
        }
    }
}
