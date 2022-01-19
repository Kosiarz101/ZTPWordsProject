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

        private string[] answers;

        private int points;
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
        public QuizWindow(ISong song, IMode mode, IDifficultyLevel difficultyLevel)
        {
            InitializeComponent();
            wordDatabase = WordDatabase.GetInstance();
            Song = song;
            if (song != null)
                Song.PlaySong();
            Mode = mode;
            Mode.SetTextBlock(modeTb);
            DifficultyLevel = difficultyLevel;
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
            answers = DifficultyLevel.Answers();
        }
    }
}
