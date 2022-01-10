using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ZTPWordsProject.AppModels
{
    public class LearningMode : IMode
    {
        public bool CheckAnswer(ref int points, string userAnswer, string correctAnswer)
        {
            if (userAnswer == correctAnswer)
                return true;
            else
                return false;
        }

        public void SetTextBlock(TextBlock textBlock)
        {
            textBlock.Text = "Nauki";
        }
    }
}
