﻿using System;
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
using Alias_Core;

namespace Alias_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game CurrentGame { get; set; }
        public GameManager _gameManager = new GameManager();
        private List<Team> listTeams = new List<Team>();
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void ToSetTeams_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            NumberOfTeamsText.Visibility = Visibility.Visible;
            NumberOfTeamsPanel.Visibility = Visibility.Visible;
            StartGame.Visibility = Visibility.Visible;

        }

        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
            int NumOfTeams;
            _gameManager.CurrentGame = new Game();
            string Input = NumberOfTeams.Text;
            if (int.TryParse(Input, out NumOfTeams))
            {
                if (NumOfTeams > 1)
                {

                    _gameManager.CurrentGame.TeamsAmount = NumOfTeams;
                    _gameManager.CurrentGame.StartDt = DateTime.Now;
                    _gameManager.CurrentGame.Id = Game.CurrentId + 1; ;
                    _gameManager.CurrentGame.Teams = new List<Team>();
                    //Team.CurrentIdNum = 1;
                    // Filling list of teams for this game
                    for (int j = 0; j < NumOfTeams; j++)
                    {
                        Team t = new Team();
                        t.CurrentId = Team.CurrentIdNum + 1;
                        Team.CurrentIdNum += 1;
                        _gameManager.CurrentGame.Teams.Add(t);
                    }
                    int i = 1;
                    //Opening windows with name choice, i is index that indicates number of opening of "TeamName" window
                    TeamName tn = new TeamName(_gameManager, i, NumOfTeams);
                    this.Close();
                    tn.Show();
                }
                else
                {
                    MessageBox.Show("Enter non-negative number more than 1");
                    NumberOfTeams.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Incorrect input, try again");
                NumberOfTeams.Text = "";
            }

        }

        private void Score_Click(object sender, RoutedEventArgs e)
        {
            listTeams = GameManager.AllTeams;
            ToSetTeams.Visibility = Visibility.Collapsed;
            Score.Visibility = Visibility.Collapsed;
            AliasText.Visibility = Visibility.Collapsed;
            ScoreTable.ItemsSource = listTeams;
            ScoreTable.Visibility = Visibility.Visible;
            ScoreTableText.Visibility = Visibility.Visible;
            BackToMenu.Visibility = Visibility.Visible;

        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            ScoreTable.Visibility = Visibility.Collapsed;
            ScoreTableText.Visibility = Visibility.Collapsed;
            BackToMenu.Visibility = Visibility.Collapsed;
            AliasText.Visibility = Visibility.Visible;
            Score.Visibility = Visibility.Visible;
            ToSetTeams.Visibility = Visibility.Visible;
        }
    }
}
