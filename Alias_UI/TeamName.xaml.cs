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
        private Game _game;
        //private List<Team> _list;
        private int _iter;
        private int _numteams;
        public TeamName(Game CurrentGame,  int iter, int numteams)
        {
            InitializeComponent();
            //_team = CurrentTeam;
            _game = CurrentGame;
            //_list = CurrentList;
            _iter = iter;
            _numteams = numteams;
            TextTeamNumber.Text = _iter.ToString();
            TextTeamNumber2.Text = _iter.ToString();
        }
        
        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamNameText.Visibility = Visibility.Visible;
            TeamNameBlock.Visibility = Visibility.Visible;
            AddTeamBtn.Visibility = Visibility.Visible;
            AddTeam.Visibility = Visibility.Collapsed;
            ChooseTeam.Visibility = Visibility.Collapsed;
            ChooseOption.Visibility = Visibility.Collapsed;
        }
        private void AddTeamByName_Click(object sender, RoutedEventArgs e)
        //In this method we write name to list and check if we should open TeamName window again
        //If all names are written, we open ReadyWindow 
        {
            _game.Teams[_iter-1].Name = TeamNameBlock.Text;
            _iter += 1;
            // GameManager.AllTeamsNames.Add(TeamNameBlock.Text);
            // List of names should be filled after game to avoid mistakes
            if (_iter <= _numteams)
            {
                TeamName tn = new TeamName(_game, _iter, _numteams);
                this.Close();
                tn.Show();
            }
            else
            {
                int i = 0;
                //i will be index of list element (team) we are working with
                ReadyWindow rw = new ReadyWindow(_game, i);
                this.Close();
                rw.Show();
            }
        }
        private void ChooseTeam_Click(object sender, RoutedEventArgs e)
        {
            SelectBlock.Visibility = Visibility.Visible;
            SelectBtn.Visibility = Visibility.Visible;
            CbTeam.Visibility = Visibility.Visible;
            AddTeam.Visibility = Visibility.Collapsed;
            ChooseTeam.Visibility = Visibility.Collapsed;
            ChooseOption.Visibility = Visibility.Collapsed;
            CbTeam.ItemsSource = GameManager.AllTeamsNames;
            //GameManager.AllTeamsNames is important list and we should fill it
            //after game because otherwise there's an opportunity to choose team
            //which was created in the same game
        }

        private void SelectTeam_Click(object sender, RoutedEventArgs e)
        {
            _game.Teams[_iter - 1].Name = CbTeam.Text;
            _iter += 1;
            if (_iter <= _numteams)
            {
                TeamName tn = new TeamName(_game, _iter, _numteams);
                this.Close();
                tn.Show();
            }
            else
            {
                this.Close();
                int i = 0;
                //i will be index of list element (team) we are working with
                ReadyWindow rw = new ReadyWindow(_game, i);
                this.Close();
                rw.Show();
            }
        }
    }
}
