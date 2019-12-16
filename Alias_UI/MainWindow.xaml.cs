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

namespace Alias_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            ActiveGame activeGame = new ActiveGame();
            this.Close();
            activeGame.Show();
        }

        private void Score_Click(object sender, RoutedEventArgs e)
        {
            //TODO: ScoreWindow
        }
    }
}
