﻿using System;
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
    /// Interaction logic for RoundStatistics.xaml
    /// </summary>
    public partial class RoundStatistics : Window
    {
        private Game _game;
        private int _iter;
        private Round _round;
        public RoundStatistics(Game game, int iter, Round round)
        {
            InitializeComponent();
            _game = game;
            _iter = iter;
            _round = round;
            round.PointsTotal = round.WordsGuessed - round.WordsSkipped;
            if (round.PointsTotal < 0)
                round.PointsTotal = 0;
            TeamNameText.Text = _round.TeamName;
            WordsSkippedText.Text = _round.WordsSkipped.ToString();
            WordsGuessedText.Text = _round.WordsGuessed.ToString();
            TotalPointsText.Text = _round.PointsTotal.ToString();
            CurrentScoreText.Text = _game.Teams[_iter].CurrentScore.ToString();
        }
        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            if (_iter < _game.Teams.Count - 1)
                _iter += 1;
            else
                _iter = 0;
            ReadyWindow rw = new ReadyWindow(_game, _iter);
            this.Close();
            rw.Show();
            //TODO show statistics of round
        }
        private void FinishGame_Click(object sender, RoutedEventArgs e)
        {
            _game.EndDt = DateTime.Now;
            FinishGame fg = new FinishGame(_game);
            this.Close();
            fg.Show();
        }
        //ToDo presentation of round statistics
    }
}
