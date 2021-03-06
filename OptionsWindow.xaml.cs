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
using ZTPWordsProject.AppModels;

namespace ZTPWordsProject
{
    /// <summary>
    /// Logika interakcji dla klasy OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        WordDatabase wordDatabase;
        ISongCreator songCreator;
        ISong song;
        IMode mode;
        AnswerDirector answerDirector = new AnswerDirector();
        IAnswerBuilder builder;
        List<Answers> answers = new List<Answers>();
        IDifficultyLevel difficultyLevel;

        List<string> difficulties;
        List<string> modes;
        List<string> languages;
        List<string> translationSites;
        List<string> songs;

        int difficultyCounter = 0;
        int modeCounter = 0;
        int translationSiteCounter = 0;
        int songCounter = 0;
        public OptionsWindow()
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
            difficulties = new List<string>()
            {
                "Łatwy", "Średni", "Trudny", "Najtrudniejszy"
            };
            modes = new List<string>()
            {
                "Testu", "Nauki"
            };
            translationSites = new List<string>()
            {
                "Polski-Obcy", "Obcy-Polski"
            };
            songs = new List<string>()
            {
                "Battlefield1942Theme.mp3", "GameplayHipHopTheme.wav", "Brak"
            };
            languages = new List<string>();
           
            languages = wordDatabase.GetAllLanguages();
            languages.Add("Wszystkie");

            SetValues();
            SetParameters();
        }
        //Ustawia domyślne wartości
        public void SetParameters()
        {
            modeTb.Text = modes[modeCounter];
            difficultyTb.Text = difficulties[difficultyCounter];
            translationTb.Text = translationSites[translationSiteCounter];
            languageComboBox.SelectedItem = "Wszystkie";
            ChangeMusicTb.Text = songs[songCounter];
        }
        //Ustawia zakres wartości dla sliderów oraz comboboxa
        public void SetValues()
        {
            slDifficultyOption.Value = 0;
            slDifficultyOption.Minimum = 0;
            slDifficultyOption.Maximum = difficulties.Count - 1;

            slModeOption.Value = 0;
            slModeOption.Minimum = 0;
            slModeOption.Maximum = modes.Count - 1;

            slTranslationOption.Value = 0;
            slTranslationOption.Minimum = 0;
            slTranslationOption.Maximum = translationSites.Count - 1;

            for(int i=0; i<languages.Count; i++)
            {
                languageComboBox.Items.Add(languages[i]);                
            }          

            slChangeMusicOption.Value = 0;
            slChangeMusicOption.Minimum = 0;
            slChangeMusicOption.Maximum = songs.Count - 1;
        }
        //Powraca do głównego ekranu
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void slModeOption_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            modeCounter = (int)slModeOption.Value;
            modeTb.Text = modes[modeCounter];
        }

        private void slChangeMusicOption_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            songCounter = (int)slChangeMusicOption.Value;
            ChangeMusicTb.Text = songs[songCounter];
        }

        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            languageTb.Text = (sender as ComboBox).SelectedItem as string;
        }

        private void slDifficultyOption_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            difficultyCounter = (int)slDifficultyOption.Value;
            difficultyTb.Text = difficulties[difficultyCounter];
        }
        private void slTranslationOption_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            translationSiteCounter = (int)slTranslationOption.Value;
            translationTb.Text = translationSites[translationSiteCounter];
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //Rozpączenie grania muzyki
            string songName = songs[songCounter];
            if (songName.Substring(songName.Length - 3) == "mp3")
            {
                songCreator = new Mp3SongCreator();
            }
            else if (songName.Substring(songName.Length - 3) == "wav")
            {
                songCreator = new WavSongCreator();
            }

            //Wybranie odpowiedniego trybu
            if (modes[modeCounter] == "Testu")
                mode = new TestMode();
            else
                mode = new LearningMode();

            if (songCreator != null)
                song = songCreator.CreateSong(songName);
            else
                song = null;

            if (difficulties[difficultyCounter] == "Łatwy")
            {
                difficultyLevel = new EasyLevel();
            }
            else if (difficulties[difficultyCounter] == "Średni")
            {
                difficultyLevel = new MediumLevel();
            }
            else if (difficulties[difficultyCounter] == "Trudny")
            {
                difficultyLevel = new HardLevel();
            }
            else
            {
                difficultyLevel = new HardestLevel();
            }

            if (translationSites[translationSiteCounter] == "Polski-Obcy")
            {                             
                for(int i=0; i<20; i++)
                {
                    builder = new AnswerBuilderPolishForeign();
                    answerDirector.ConstructAnswer(builder, languageTb.Text);
                    answers.Add(builder.GetResult());
                }               
            }                
            else
            {                
                for (int i = 0; i < 20; i++)
                {
                    builder = new AnswerBuilderForeignPolish();
                    answerDirector.ConstructAnswer(builder, languageTb.Text);
                    answers.Add(builder.GetResult());
                }
            }

            QuizWindow quizWindow = new QuizWindow(song, mode, difficultyLevel, answers, languageTb.Text);
            quizWindow.Show();
            this.Close();
        }       
    }
}
