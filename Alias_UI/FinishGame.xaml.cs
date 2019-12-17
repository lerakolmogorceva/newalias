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
using Alias_Core;

namespace Alias_UI
{
    /// <summary>
    /// Interaction logic for FinishGame.xaml
    /// </summary>
    public partial class FinishGame : Window
    {
        private List<Team> listTeams;
        private List<Team> listWinners;
        private Game _game;
        private string Winners;
        private int maxPoints;
        private GameManager manager;
        public FinishGame(Game game)
        {
            InitializeComponent();
            _game = game;
            manager = new GameManager();
            listWinners = manager.ChooseWinner(_game);
            Winners = manager.StringWinner(listWinners);
            listTeams = _game.Teams;
            StartTimeText.Text = _game.StartDt.ToString();
            EndTimeText.Text = _game.EndDt.ToString();
            NumTeamsText.Text = _game.TeamsAmount.ToString();
            WinnerText.Text = Winners;
            ListViewTeams.ItemsSource = listTeams;
            maxPoints = manager.MaxPointsByGame(_game);
            MaximumPointsText.Text = maxPoints.ToString();
            manager.IncrementGames(listTeams);
        }
        //ToDo show game statistics
        //ToDo new game button
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void SameTeams_Click(object sender, RoutedEventArgs e)
        {
            Game newGame = manager.NewGameSameTeams(_game);
            int iter = 0;
            ReadyWindow rw = new ReadyWindow(newGame, iter);
            this.Close();
            rw.Show();
        }
        private void NewTeams_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}