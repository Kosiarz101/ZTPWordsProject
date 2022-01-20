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

namespace ZTPWordsProject
{
    /// <summary>
    /// Logika interakcji dla klasy QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private WordDatabase wordDatabase;
        //Przechouwje numer obecnego pytania
        public int Index { get; set; } = 0;
        private ISong Song { get; set; }
        public IMode Mode { get; set; }
        private IDifficultyLevel DifficultyLevel { get; set; }

        private List<Answers> ListofQuestions { get; set; } = new List<Answers>();

        private List<string> QuestionAnswers { get; set; }

        private string[] answers;

        private int points;

        public int number;

        public string question { get; set; }
        private string UserAnswer { get; set; }

        public string language { get; set; }
        //Zarządca we wzorcu Memento
        private List<IMemento> mementoList = new List<IMemento>();

        public QuizWindow()
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();           
        }
        public QuizWindow(ISong song, IMode mode)
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
            Song = song;
            if(song != null)
                Song.PlaySong();
            Mode = mode;
            Mode.SetTextBlock(modeTb);
        }
        public QuizWindow(ISong song, IMode mode, IDifficultyLevel difficultyLevel, List<Answers> answers, string llanguage)
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
            Song = song;
            if (song != null)
                Song.PlaySong();
            Mode = mode;
            Mode.SetTextBlock(modeTb);
            DifficultyLevel = difficultyLevel;
            for (int i = 0; i < 20; i++)
            {
                ListofQuestions.Add(answers[i]);
            }
            number = SetDifficultyLevel();
            DataContext = this;
            ShowAnswers();
            language = llanguage;
            setInformationAboutGame();
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(Index==19)
            {
                EndScreen endScreen = new EndScreen(points, "Angielski", modeTb.Text, difficultyTb.Text);
                endScreen.Show();
                this.Close();
            }           
            if(Mode.CheckAnswer(ref points, UserAnswer, ListofQuestions[Index].GetAnswers()[0]))
            {
                //Jeżeli została już ustawiona pamiątka, wczytaj ją
                if (mementoListCount() > Index + 1)
                {
                    SetState((Memento)GetState(Index + 1));
                    ShowAnswers();
                }
                //Jeśli nie, utwórz nową pamiątkę
                else
                {                   
                    if (mementoListCount() == Index)
                        AddState(SaveState());
                    Index++;
                    UserAnswer = null;
                    ShowAnswers();
                    //tbQuestion.Text = words[Index];
                    //tbAnswer.Text = "";
                }
                questionNumberTb.Text = (Index + 1).ToString() + "/20";
            }
        }
        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(Index != 0)
                SetState((Memento)GetState(Index - 1));           
            ShowAnswers();
        }
        //Wczytaj stan z pamiątki
        private void SetState(Memento memento)
        {
            Index = memento.Index;
            UserAnswer = memento.UserAnswer;
            ShowAnswers();
            //tbAnswer.Text = memento.Answer;
            //tbQuestion.Text = words[Index];
        }
        //Zapisz stan w pamiątce
        public IMemento SaveState()
        {
            return new Memento(UserAnswer, Index);
        }
        //Wewnętrzna klasa pamiątki
        private class Memento : IMemento
        {
            public string UserAnswer { get; set; }
            public int Index { get; set; }
            public Memento(string answer, int index)
            {
                UserAnswer = answer;
                Index = index;
            }
            public void SetState(string answer, int index)
            {
                UserAnswer = answer;
                Index = index;
            }
        }

        //Dodaje nową pamiątkę do zarządcy
        public void AddState(IMemento memento)
        {
            mementoList.Add(memento);
        }
        //Zwraca konkretną pamiątkę
        public IMemento GetState(int index)
        {
            return mementoList.ElementAt(index);
        }
        //Zwraca ilość pamiątek
        public int mementoListCount()
        {
            return mementoList.Count;
        }

        public void ShowAnswers()
        {
            if (Index > 19)
            {
                return;
            }        
            questionNumberTb.Text = (Index + 1).ToString() + "/20";
            Answers DaQuestion = ListofQuestions.ElementAt(Index);
            questionContentTb.Text = DaQuestion.GetQuestion();
            QuestionAnswers = DaQuestion.GetAnswers();
            if (number == 2)
            {
                QuestionAnswers.RemoveAt(3);
                QuestionAnswers.RemoveAt(2);
                QuestionAnswers.Add("x");
                QuestionAnswers.Add("x");

            }
            else if (number == 3)
            {
                QuestionAnswers.RemoveAt(3);
                QuestionAnswers.Add("x");
            }
            setTextBlockAnswers();
        }

        public int SetDifficultyLevel()
        {
            number = DifficultyLevel.Answers();
            return number;
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TextBlock textBlock = (TextBlock)button.Content;
            UserAnswer = textBlock.Text;
            buttonColor(FirstAnswer);
            buttonColor(SecondAnswer);
            buttonColor(ThirdAnswer);
            buttonColor(FourthAnswer);
            button.Background = Brushes.Blue;
        }
        private void buttonColor(Button button)
        {
            TextBlock tb = (TextBlock)button.Content;
            if (UserAnswer != null && tb.Text == UserAnswer)
                button.Background = Brushes.Blue;
            else
                button.Background = Brushes.Gray;
        }
        private void setTextBlockAnswers()
        {
            buttonColor(FirstAnswer);
            buttonColor(SecondAnswer);
            buttonColor(ThirdAnswer);
            buttonColor(FourthAnswer);

            Random rnd = new Random();

            TextBlock tb = (TextBlock)FirstAnswer.Content;
            tb.Text = QuestionAnswers[0];
            tb = (TextBlock)SecondAnswer.Content;
            tb.Text = QuestionAnswers[1];
            tb = (TextBlock)ThirdAnswer.Content;
            tb.Text = QuestionAnswers[2];
            tb = (TextBlock)FourthAnswer.Content;
            tb.Text = QuestionAnswers[3];
        }

        public string setDifficultyLevel()
        {
            return DifficultyLevel.getPoziomTrudności();
        }


        private void setInformationAboutGame()
        {

            TextBlock tb = difficultyTb;
            tb.Text = setDifficultyLevel();
            TextBlock tr = translationSiteTb;
            tr.Text = language;
        }
    }
}
