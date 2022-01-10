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
    /// Logika interakcji dla klasy DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        WordDatabase wordDatabase;
        List<WordWithTranslations> wordWithTranslationsList;
        List<string> modes;
        string chosenContent = "";
        public DatabaseWindow()
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
            modes = new List<string>()
            {
                "Szukaj", "Dodaj Słowo", "Usuń Słowo"
            };
            SetParameters();
            SetButtonsLanguage();
            languageName.Text = "Wszystkie";
        }
        private void SetParameters()
        {
            foreach(string mode in modes)
                ModeComboBox.Items.Add(mode);
            ModeComboBox.SelectedItem = "Szukaj";
        }
        //Wczytaj wszystkie języki z bazy danych jako przyciski
        public void SetButtonsLanguage()
        {
            LanguageSP.Children.Clear();
            List<string> languages = wordDatabase.GetAllLanguages();
            languages.Add("Wszystkie");
            for(int i=0; i<languages.Count; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = languages[i];
                textBlock.FontSize = 16;
                Button button = new Button();
                button.MinHeight = 60;
                button.Content = textBlock;
                button.Click += btn_Click;
                LanguageSP.Children.Add(button);
            }
        }
        public void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TextBlock textBlock = (TextBlock)button.Content;
            languageName.Text = textBlock.Text;
        }    
        //Kiedy przycisk wykonaj zostanie naciśnięty
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonsLanguage();
            WordsSP.Children.Clear();
            TranslationSP.Children.Clear();
            string option = (ModeComboBox as ComboBox).SelectedItem as string;
            switch (option)
            {
                case "Szukaj":
                    Search();
                    break;
                case "Dodaj Słowo":
                    AddWord();
                    break;
                case "Usuń Słowo":
                    DeleteWord();
                    break;
            }           
        }
        //Usuń Słowo
        private void DeleteWord()
        {
            string search = searchTb.Text;
            wordWithTranslationsList = wordDatabase.GetWordWithTranslationsBySearch(search, languageName.Text);
            foreach (WordWithTranslations word in wordWithTranslationsList)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = word.Content;
                textBlock.FontSize = 16;
                Button button = new Button();
                button.MinHeight = 60;
                button.Content = textBlock;
                button.Click += btnDeleteWord_Click;
                button.Style = this.FindResource("ButtonBackgroundInvisible") as Style;
                WordsSP.Children.Add(button);
            }           
        }
        //Usuń wybrane przez użytkownika słowo
        private void btnDeleteWord_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TextBlock textBlock = (TextBlock)button.Content;
            string content = textBlock.Text;

            WordWithTranslations word = wordWithTranslationsList.Where(x => x.Content == content).FirstOrDefault();
            wordDatabase.RemoveWordByContent(word.Content, word.Language);
            Search();
        }

        private void AddWord()
        {
            Word word = new Word()
            {
                Language = languageName.Text,
                Content = searchTb.Text
            };
            wordDatabase.AddWord(word);
        }
        //Wyszukaj słowa
        private void Search()
        {
            WordsSP.Children.Clear();
            string search = searchTb.Text;
            wordWithTranslationsList = wordDatabase.GetWordWithTranslationsBySearch(search, languageName.Text);
            foreach (WordWithTranslations word in wordWithTranslationsList)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = word.Content;
                textBlock.FontSize = 16;
                Button button = new Button();
                button.MinHeight = 60;
                button.Content = textBlock;
                button.Click += btnWord_Click;
                button.Style = this.FindResource("ButtonBackgroundInvisible") as Style;
                WordsSP.Children.Add(button);
            }
        }
        //Kiedy słowo zostało wybrane
        public void btnWord_Click(object sender, RoutedEventArgs e)
        {           
            Button button = (Button)sender;
            TextBlock textBlock = (TextBlock)button.Content;
            chosenContent = textBlock.Text;

            ShowTranslations();
        }
        //Kiedy tłumaczenie zostało wybrane - odłącz tłumaczenie od słowa
        public void btnTranslation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TextBlock textBlock = (TextBlock)button.Content;
            string translation = textBlock.Text;

            WordWithTranslations word = wordWithTranslationsList.Where(x => x.Content == chosenContent).FirstOrDefault();
            wordDatabase.RemoveTranslationFromWord(word.Content, word.Language, translation);
            ShowTranslations();
        }
        //Dodaj tłumaczenie do słowa
        private void AddTranslationButton_Click(object sender, RoutedEventArgs e)
        {
            if(TranslationSP.Children.Count == 0)
            {
                translationMessage.Text = "Wybierz słowo";
            }
            else if(AddTranslationTb.Text == "")
            {
                translationMessage.Text = "Najpierw podaj tłumaczenie";
            }
            else
            {
                WordWithTranslations word = wordWithTranslationsList.Where(x => x.Content == chosenContent).FirstOrDefault();
                if(!wordDatabase.AddTranslationToWord(word.Content, word.Language, AddTranslationTb.Text))
                {
                    translationMessage.Text = "Podane tłumaczenie jest już dodane";
                }
                ShowTranslations();
            }
        }
        //Pokaż wszystkie tłumaczenia
        private void ShowTranslations()
        {
            TranslationSP.Children.Clear();
            TextBlock textBlock;

            WordWithTranslations word = wordWithTranslationsList
                .Where(x => x.Content == chosenContent)
                .FirstOrDefault();

            wordWithTranslationsList = wordDatabase.GetWordWithTranslationsBySearch(word.Content, word.Language);

            word = wordWithTranslationsList.Where(x => x.Content == chosenContent).FirstOrDefault();
            if (word.TranslationList.Count == 0)
            {
                textBlock = new TextBlock();
                textBlock.Text = "There's no translation for this word yet";
                textBlock.FontSize = 16;
                textBlock.MinHeight = 60;
                TranslationSP.Children.Add(textBlock);
            }
            foreach (string translation in word.TranslationList)
            {
                textBlock = new TextBlock();
                textBlock.Text = translation;
                textBlock.FontSize = 16;
                Button button = new Button();
                button.MinHeight = 60;
                button.Content = textBlock;
                button.Click += btnTranslation_Click;
                button.Style = this.FindResource("ButtonBackgroundInvisibleRed") as Style;
                TranslationSP.Children.Add(button);
            }
        }
    }
}
