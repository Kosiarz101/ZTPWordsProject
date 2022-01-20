using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZTPWordsProject
{
    /// <summary>
    /// Logika interakcji dla klasy EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Window
    {
        public EndScreen()
        {
            InitializeComponent();
        }
        public EndScreen(int points, string language, string difficulty, string mode)
        {
            InitializeComponent();
            pointsTb.Text = points.ToString() + "/20";
            languageTb.Text = language;
            difficultyTb.Text = difficulty;
            modeTb.Text = mode;
        }
    }
}
