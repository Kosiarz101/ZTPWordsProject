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

        public string Question;
        private List<Answers> ListofQuestions = new List<Answers>();

        private List<string> QuestionAnswers { get; set; }

        private string[] answers;

        private int points;

        public int number;

        public string question { get; set; }
        private string UserAnswer { get; set; }
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
        public QuizWindow(ISong song, IMode mode, IDifficultyLevel difficultyLevel, List<Answers> answers)
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
            ShowAnswers();
            //this.DataContext = this;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(Index==19)
            {
                EndScreen endScreen = new EndScreen();
                endScreen.Show();
                this.Close();
            }
            if(Mode.CheckAnswer(ref points, "kurwa", "kurwa"))
            {
                //Jeżeli została już ustawiona pamiątka, wczytaj ją
                if (mementoListCount() > Index + 1)
                {
                    SetState((Memento)GetState(Index + 1));
                }
                //Jeśli nie, utwórz nową pamiątkę
                else
                {
                    if (mementoListCount() == Index)
                        AddState(SaveState());
                    Index++;
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
            questionNumberTb.Text = (Index + 1).ToString() + "/20";
        }
        //Wczytaj stan z pamiątki
        private void SetState(Memento memento)
        {
            Index = memento.Index;
            ShowAnswers();
            //tbAnswer.Text = memento.Answer;
            //tbQuestion.Text = words[Index];
        }
        //Zapisz stan w pamiątce
        public IMemento SaveState()
        {
            return new Memento("answer", Index);
        }
        //Wewnętrzna klasa pamiątki
        private class Memento : IMemento
        {
            public string Answer { get; set; }
            public int Index { get; set; }
            public Memento(string answer, int index)
            {
                Answer = answer;
                Index = index;
            }
            public void SetState(string answer, int index)
            {
                Answer = answer;
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
            Answers DaQuestion = ListofQuestions.ElementAt(Index);
            question = DaQuestion.GetQuestion();
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

        }

        public int SetDifficultyLevel()
        {
            number = DifficultyLevel.Answers();
            return number;
        }
    }
}
