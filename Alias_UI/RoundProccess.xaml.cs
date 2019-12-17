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
using System.Windows.Threading;
using Alias_Core;

namespace Alias_UI
{
    /// <summary>
    /// Interaction logic for RoundProccess.xaml
    /// </summary>
    public partial class RoundProccess : Window
    {
        private Game _game;
        private int _iter;
        private DateTime _startCountdown; // timer start
        private TimeSpan _startTimeSpan = TimeSpan.FromSeconds(60); // initial time to stop
        private TimeSpan _timeToEnd; // time to stop
        private TimeSpan _interval = TimeSpan.FromSeconds(1); // timer interval
        private DateTime _pauseTime;
        public GameManager manager = new GameManager();
        private string currentWord;
        private Round currentRound;
        private DispatcherTimer _timer;
        public RoundProccess(Game game, int iter)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = _interval;
            _timer.Tick += timerTicker;
            InitializeComponent();
            StartTimer(DateTime.Now);
            currentWord = manager.GenerateNewWord();
            CurrentWord.Text = currentWord;
            _game = game;
            //iter is ID of team in current game
            _iter = iter;
            _game.Rounds += 1;
            currentRound = new Round(_game.Teams[_iter].Name);
        }

        //Here almost all methods are related to timer
        public TimeSpan TimeToEnd
        {
            get
            {
                return _timeToEnd;
            }

            set
            {
                _timeToEnd = value;
                if (value.TotalSeconds <= 0)
                {
                    StopTimer();

                }
                var frmt = "mm\\:ss";
                TextTime.Text = value.ToString(frmt);

            }
        }

        private void timerTicker(object sender, EventArgs e)
        {
            TimeToEnd -= _interval;
        }


        public bool TimerIsEnabled
        {
            get { return _timer.IsEnabled; }
        }

        private void StopTimer()
        {
            if (TimerIsEnabled)
                _timer.Stop();
            RoundStatistics rs = new RoundStatistics(_game, _iter, currentRound);
            this.Close();
            rs.Show();
        }

        private void StartTimer(DateTime sDate)
        {
            _startCountdown = sDate;
            TimeToEnd = _startTimeSpan;
            _timer.Start();
        }

        private void PauseTimer()
        {
            _timer.Stop();
            _pauseTime = DateTime.Now;
            CurrentWord.Text = "Game paused";
            SkipWord_Button.Visibility = Visibility.Collapsed;
            NextWord_Button.Visibility = Visibility.Collapsed;
            ExplainWord.Visibility = Visibility.Collapsed;
        }

        private void ReleaseTimer()
        {
            var now = DateTime.Now;
            var elapsed = now.Subtract(_pauseTime);
            _startCountdown = _startCountdown.Add(elapsed);
            _timer.Start();
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            StartTimer(DateTime.Now);
            currentWord = manager.GenerateNewWord();
            CurrentWord.Text = currentWord;
        }

        private void PauseTimer_Click(object sender, RoutedEventArgs e)
        {
            PauseTimer();
        }

        private void ReleaseTimer_Click(object sender, RoutedEventArgs e)
        {
            ReleaseTimer();
            SkipWord_Button.Visibility = Visibility.Visible;
            NextWord_Button.Visibility = Visibility.Visible;
            ExplainWord.Visibility = Visibility.Visible;
            CurrentWord.Text = currentWord;
        }
            private void RoundEnd_Click(object sender, RoutedEventArgs e)
        {
            RoundStatistics rs = new RoundStatistics(_game, _iter, currentRound);
            this.Close();
            rs.Show();
            //TODO Timer with countdown - done
            //TODO Cards with random words
            //Buttons "Skip" and "Done"
            //Logic with score
        }

        private void FinishGame_Click(object sender, RoutedEventArgs e)
        {
            FinishGame fg = new FinishGame(_game);
            this.Close();
            fg.Show();
        }
        private void SkipWord_Click(object sender, RoutedEventArgs e)
        {
            _game = manager.SkipWord(_game, _iter);
            currentWord = manager.GenerateNewWord();
            CurrentWord.Text = currentWord;
            currentRound.WordsSkipped += 1;
        }
        private void NextWord_Click(object sender, RoutedEventArgs e)
        {
            _game = manager.NextWord(_game, _iter);
            currentWord = manager.GenerateNewWord();
            CurrentWord.Text = currentWord;
            currentRound.WordsGuessed += 1;
        }
    }
}
