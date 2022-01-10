using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ZTPWordsProject.AppModels
{
    public interface IMode
    {
        public bool CheckAnswer(ref int points, string userAnswer, string correctAnswer);
        public void SetTextBlock(TextBlock textBlock);
    }
}
