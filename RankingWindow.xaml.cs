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
    /// Logika interakcji dla klasy RankingWindow.xaml
    /// </summary>
    public partial class RankingWindow : Window
    {
        RankingList RankingList { get; set; }
        int iteration = 0;
        readonly int slotsCount = 4;
        public RankingWindow()
        {
            InitializeComponent();
            RankingList = RankingList.getInstance();
            ShowRankingRecords();
        }
        private void ShowRankingRecords()
        {

            if (RankingList.GetCount() == 0)
            {
                StackP1tb1.Text = "You haven't played any round yet";
            }

            setRankingSlot(StackP1tb1, StackP1tb2, StackP1tb3, 0);      
            setRankingSlot(StackP2tb1, StackP2tb2, StackP2tb3, 1);
            setRankingSlot(StackP3tb1, StackP3tb2, StackP3tb3, 2);
            setRankingSlot(StackP4tb1, StackP4tb2, StackP4tb3, 3);

            if (iteration >= RankingList.GetCount() - slotsCount)
                bRight.Visibility = Visibility.Hidden;
            else
                bRight.Visibility = Visibility.Visible;

            if (iteration == 0)
                bLeft.Visibility = Visibility.Hidden;
            else
                bLeft.Visibility = Visibility.Visible;

        }
        private void setRankingSlot(TextBlock language, TextBlock points, TextBlock username, int shift)
        {
            try
            {
                Ranking ranking = RankingList.GetRanking(iteration + shift);
                language.Text = "Language: " + ranking.Language;
                points.Text = "Points: " + ranking.Points;
                username.Text = "Username: " + ranking.Username;
            }
            catch (Exception e)
            {
                if(e is IndexOutOfRangeException || e is ArgumentOutOfRangeException)
                {
                    language.Text = null;
                    points.Text = null;
                    username.Text = null;
                }
                else
                {
                    throw;
                }
                
            }
            
        }
        private void Right_Button_Click(object sender, RoutedEventArgs e)
        {
            iteration += slotsCount;
            ShowRankingRecords();
        }
        private void Left_Button_Click(object sender, RoutedEventArgs e)
        {
            iteration -= slotsCount;
            ShowRankingRecords();
        }
        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            RankingList.SaveToFile();   
            ShowRankingRecords();
        }
    }
}
