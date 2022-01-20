using Newtonsoft.Json;
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
    /// Logika interakcji dla klasy EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Window
    {
        List<Ranking> Rankings { get; set; }
        public EndScreen()
        {
            InitializeComponent();
        }
        public EndScreen(int points, string language, string difficulty, string mode)
        {
            InitializeComponent();
            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RankingDatabase.json");
            string serializedRanking = System.IO.File.ReadAllText(fullPath);
            Rankings = JsonConvert.DeserializeObject<List<Ranking>>(serializedRanking);

            pointsTb.Text = points.ToString() + "/20";
            languageTb.Text = language;
            difficultyTb.Text = difficulty;
            modeTb.Text = mode;

            SaveToFile(fullPath, points);
        }
        //Wyjdź do Menu Głównego
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        //Zapisz nowy obiekt klasy Ranking do pliku
        private void SaveToFile(string fullPath, int points)
        {
            Rankings.Add(new Ranking()
            {
                Language = languageTb.Text,
                Points = points,
                Username = "Admin"
            });
            string serializedRanking = JsonConvert.SerializeObject(Rankings, Formatting.Indented);
            System.IO.File.WriteAllText(fullPath, serializedRanking);
        }
    }
}
