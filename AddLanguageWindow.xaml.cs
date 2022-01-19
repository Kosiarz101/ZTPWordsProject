using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTPWordsProject.AppModels;
using ZTPWordsProject.DatabaseModels;

namespace ZTPWordsProject
{
    /// <summary>
    /// Logika interakcji dla klasy AddLanguageWindow.xaml
    /// </summary>
    public partial class AddLanguageWindow : Window
    {
        WordDatabase wordDatabase;
        public AddLanguageWindow()
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            if(languageTb.Text == "" || wordTb.Text == "")
            {
                errorTb.Text = "Musisz podać język oraz słowo";
                return;
            }
            List<String> languages = wordDatabase.GetAllLanguages();
            if(languages.Any(x => x == languageTb.Text))
            {
                errorTb.Text = "Język już jest w bazie danych";
            }
            else
            {
                Word word = new Word();
                word.Language = languageTb.Text;
                word.Content = wordTb.Text;
                wordDatabase.AddWord(word);
                DialogResult = true;
            }
            
        }
    }
}
