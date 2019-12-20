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
    /// Interaction logic for TeamName.xaml
    /// </summary>
    public partial class TeamName : Window
    {
        //private Team _team;
        private GameManager _gameManager;
        //private List<Team> _list;
        private int _iter;
        private int _numteams;
        public TeamName(GameManager manager, int iter, int numteams)
        {
            InitializeComponent();
            //_team = CurrentTeam;
            _gameManager = manager;
            //_list = CurrentList;
            _iter = iter;
            _numteams = numteams;
            TextTeamNumber.Text = _iter.ToString();
            TextTeamNumber2.Text = _iter.ToString();
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeam.Visibility = Visibility.Collapsed;
            ChooseTeam.Visibility = Visibility.Collapsed;
            ChooseOption.Visibility = Visibility.Collapsed;
            TeamNameText.Visibility = Visibility.Visible;
            TeamNameBlock.Visibility = Visibility.Visible;
            AddTeamBtn.Visibility = Visibility.Visible;
            BackToSelectFromAdd.Visibility = Visibility.Visible;
        }
        private void AddTeamByName_Click(object sender, RoutedEventArgs e)
        //In this method we write name to list and check if we should open TeamName window again
        //If all names are written, we open ReadyWindow 
        {
            if (TeamNameBlock.Text != "")
            {
                bool error = false;
                for (int i=0; i < _iter; i++)
                {
                    if (_gameManager.CurrentGame.Teams[i].Name == TeamNameBlock.Text)
                        error = true;
                }
                foreach(string name in GameManager.AllTeamsNames)
                {
                    if (name == TeamNameBlock.Text)
                        error = true;
                }
                if (error)
                    MessageBox.Show("Team with this name already exists, try again");
                else
                {
                    _gameManager.CurrentGame.Teams[_iter - 1].Name = TeamNameBlock.Text;
                    GameManager.AllTeams.Add(_gameManager.CurrentGame.Teams[_iter - 1]);
                    _iter += 1;
                    // GameManager.AllTeamsNames.Add(TeamNameBlock.Text);
                    // List of names should be filled after game to avoid mistakes
                    if (_iter <= _numteams)
                    {
                        TeamName tn = new TeamName(_gameManager, _iter, _numteams);
                        this.Close();
                        tn.Show();
                    }
                    else
                    {
                        int i = 0;
                        //i will be index of list element (team) we are working with
                        ReadyWindow rw = new ReadyWindow(_gameManager, i);
                        this.Close();
                        rw.Show();
                    }
                }
            }
            else
                MessageBox.Show("Enter name of team");
        }
        private void ChooseTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeam.Visibility = Visibility.Collapsed;
            ChooseTeam.Visibility = Visibility.Collapsed;
            ChooseOption.Visibility = Visibility.Collapsed;
            SelectBlock.Visibility = Visibility.Visible;
            SelectBtn.Visibility = Visibility.Visible;
            CbTeam.Visibility = Visibility.Visible;
            CbTeam.ItemsSource = GameManager.AllTeamsNames;
            BackToSelectFromList.Visibility = Visibility.Visible;
        }

        private void SelectTeam_Click(object sender, RoutedEventArgs e)
        {
            string name = CbTeam.Text;
            _gameManager.CurrentGame.Teams[_iter - 1] = _gameManager.TeamByName(name);
            _iter += 1;
            int id = _gameManager.TeamIdByName(name, GameManager.AllTeamsNames);
            GameManager.AllTeamsNames.RemoveAt(id);
            // GameManager.AllTeamsNames.Add(TeamNameBlock.Text);
            // List of names should be filled after game to avoid mistakes
            if (_iter <= _numteams)
            {
                TeamName tn = new TeamName(_gameManager, _iter, _numteams);
                this.Close();
                tn.Show();
            }
            else
            {
                int i = 0;
                //i will be index of list element (team) we are working with
                ReadyWindow rw = new ReadyWindow(_gameManager, i);
                this.Close();
                rw.Show();
            }
        }

        private void BackToSelectFromAdd_Click(object sender, RoutedEventArgs e)
        {
            TeamNameText.Visibility = Visibility.Collapsed;
            TeamNameBlock.Visibility = Visibility.Collapsed;
            AddTeamBtn.Visibility = Visibility.Collapsed;
            BackToSelectFromAdd.Visibility = Visibility.Collapsed;
            AddTeam.Visibility = Visibility.Visible;
            ChooseOption.Visibility = Visibility.Visible;
            ChooseTeam.Visibility = Visibility.Visible;
        }

        private void BackToSelectFromList_Click(object sender, RoutedEventArgs e)
        {
            SelectBlock.Visibility = Visibility.Collapsed;
            SelectBtn.Visibility = Visibility.Collapsed;
            CbTeam.Visibility = Visibility.Collapsed;
            BackToSelectFromList.Visibility = Visibility.Collapsed;
            AddTeam.Visibility = Visibility.Visible;
            ChooseOption.Visibility = Visibility.Visible;
            ChooseTeam.Visibility = Visibility.Visible;



        }
    }
}
