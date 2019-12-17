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
        private Game _game;
        public FinishGame(Game game)
        {
            InitializeComponent();
            _game = game;
        }
        //ToDo show game statistics
        //ToDo new game button
    }
}
