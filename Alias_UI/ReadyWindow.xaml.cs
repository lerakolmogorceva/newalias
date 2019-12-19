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
    /// Interaction logic for ReadyWindow.xaml
    /// </summary>
    public partial class ReadyWindow : Window
    {
        private GameManager _gameManager;
        private int _iter;
        public ReadyWindow(GameManager manager, int iter)
        {
            InitializeComponent();
            _gameManager = manager;
            _iter = iter;
            TeamNameInsert.Text = _gameManager.CurrentGame.Teams[_iter].Name;
        }
        private void StartRound_Click(object sender, RoutedEventArgs e)
        {
            RoundProccess round = new RoundProccess(_gameManager, _iter);
            //_iter is a team current index in list, when all teams playes, _iter = 0 and game continues
            this.Close();
            round.Show();
            //ToDo add "End game" button

        }
    }
}
