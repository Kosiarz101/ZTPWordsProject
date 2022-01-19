using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZTPWordsProject.AppModels;
using ZTPWordsProject.DatabaseModels;

namespace ZTPWordsProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordDatabase wordDatabase;
        private ISongCreator songCreator;
        private ISong song;
        public MainWindow()
        {
            InitializeComponent();
            string songName = "Battlefield1942Theme.mp3";
            if (songName.Substring(songName.Length - 3) == "mp3")
            {
                songCreator = new Mp3SongCreator();
            }
            else if (songName.Substring(songName.Length - 3) == "wav")
            {
                songCreator = new WavSongCreator();
            }
            //song = songCreator.CreateSong(songName);
            //song.PlaySong();
            //mediaElement1.Source = new Uri(@"F:\Users\Kosiarz\Documents\Projekty\C#\WPF\ZTPWordsProject\bin\Debug\netcoreapp3.1\Songs\Battlefield1942Theme.mp3");
            //mediaElement1.Play();
            wordDatabase = WordDatabase.GetInstance();
            Word word = new Word()
            {
                Content = "Car",
                Language = "Angielski"
            };
            bool isEverythingGood = Words.AddWord(word, new ApplicationDbContext());
            //Words.RemoveWordByContent("Car", "Angielski", new ApplicationDbContext());
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
            this.Close();
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            DatabaseWindow databaseWindow = new DatabaseWindow();
            databaseWindow.Show();
            this.Close();
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            RankingWindow rankingWindow = new RankingWindow();
            rankingWindow.Show();
            this.Close();
        }
    }
}
