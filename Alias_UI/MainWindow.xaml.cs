using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToSetTeams_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            NumberOfTeamsText.Visibility = Visibility.Visible;
            NumberOfTeams.Visibility = Visibility.Visible;
            StartGame.Visibility = Visibility.Visible;
            
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Creating a game and filling it with data
            CurrentGame = new Game();
            string Input = NumberOfTeams.Text;
            int NumOfTeams = Convert.ToInt32(Input);
            CurrentGame.TeamsAmount = NumOfTeams;
            CurrentGame.StartDt = DateTime.Now;
            CurrentGame.Id = Game.CurrentId + 1;
            CurrentGame.Teams = new List<Team>();
            Game.CurrentId += 1;
            Team.CurrentIdNum = 1;
            // Filling list of teams for this game
            for(int j=0; j < NumOfTeams; j++)
            {
                Team t = new Team();
                t.CurrentId = Team.CurrentIdNum;
                t.TotalId = Team.TotalIdNum;
                Team.CurrentIdNum += 1;
                Team.TotalIdNum += 1;
                CurrentGame.Teams.Add(t);
                GameManager.AllTeams.Add(t);
            }
            int i = 1;
            //Opening windows with name choice, i is index that indicates number of opening of "TeamName" window
            TeamName tn = new TeamName(CurrentGame, i, NumOfTeams);
            this.Close();
            tn.Show(); 
            
        }

        private void Score_Click(object sender, RoutedEventArgs e)
        {
            ToSetTeams.Visibility = Visibility.Collapsed;
            Score.Visibility = Visibility.Collapsed;
            //ListView for teams: name, number of victories, total points, max points
        }
    }
}
