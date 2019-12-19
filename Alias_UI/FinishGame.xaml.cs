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
        private GameManager _gameManager;
        private string Winners;
        private int maxPoints;
        public FinishGame(GameManager manager)
        {
            InitializeComponent();
            _gameManager = manager;
            listWinners = _gameManager.ChooseWinner(_gameManager.CurrentGame);
            Winners = _gameManager.StringWinner(listWinners);
            listTeams = _gameManager.CurrentGame.Teams;
            StartTimeText.Text = _gameManager.CurrentGame.StartDt.ToString();
            EndTimeText.Text = _gameManager.CurrentGame.EndDt.ToString();
            NumTeamsText.Text = _gameManager.CurrentGame.TeamsAmount.ToString();
            WinnerText.Text = Winners;
            ListViewTeams.ItemsSource = listTeams;
            maxPoints = _gameManager.MaxPointsByGame(_gameManager.CurrentGame);
            MaximumPointsText.Text = maxPoints.ToString();
            _gameManager.IncrementGames(listTeams);
            GameManager.AllGames.Add(_gameManager.CurrentGame);
            _gameManager.SaveData();
        }
        //ToDo show game statistics
        //ToDo new game button
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void SameTeams_Click(object sender, RoutedEventArgs e)
        {
            Game newGame = _gameManager.NewGameSameTeams(_gameManager.CurrentGame);
            int iter = 0;
            _gameManager.CurrentGame = newGame;
            ReadyWindow rw = new ReadyWindow(_gameManager, iter);
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