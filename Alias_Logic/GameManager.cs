using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_Core
{
    public class GameManager
    {
        public static List<Game> AllGames = new List<Game>();
        public static List<Team> AllTeams = new List<Team>();
        public static List<string> AllTeamsNames = new List<string>();
        public static List<string> Words = new List<string>();
         public GameManager()
        {
            //Words = SerializeWords();
            //Init all data
        }
        public Game SkipWord(Game game, int id)
        {
            if (game.Teams[id].CurrentScore > 0)
                game.Teams[id].CurrentScore -= 1;
            return game;
        }
        public Game NextWord(Game game, int id)
        {
            game.Teams[id].CurrentScore += 1;
            return game;
        }
        //public List<string> SerializeWords() this method will be invoked inside the constructor
        public string GenerateNewWord()
        {
            Words.Add("team");
            Words.Add("money");
            Words.Add("love");
            Words.Add("happiness");
            Words.Add("music");
            Words.Add("poetry");
            Words.Add("friend");
            Words.Add("school");
            Words.Add("code");
            Random random = new Random();
            int id = random.Next(Words.Count);
            string word = Words[id];
            Words.RemoveAt(id);
            return word;
        }
        public List<Team> ChooseWinner(Game game)
        {
            int max = 0;
            var listWinners = new List<Team>();
            foreach(Team t in game.Teams)
            {
                if (t.CurrentScore > max)
                {
                    max = t.CurrentScore;
                }
            }
            foreach(Team t in game.Teams)
            {
                if (t.CurrentScore == max)
                {
                    t.Victories += 1;
                    listWinners.Add(t);
                }
            }
            return listWinners;
        }
        public string StringWinner(List<Team> list)
        {
            string result = "";
            if (list.Count == 1)
                result = list[0].Name;
            else
            {
                for(int i=0; i < list.Count - 1; i++)
                {
                    result += list[i].Name;
                    result += ", ";
                }
                result += list[list.Count - 1].Name;
            }
            return result;
        }

        public void IncrementGames(List<Team> list)
        {
            foreach (Team t in list)
            {
                t.Games += 1;
                t.TotalScore += t.CurrentScore;
            }
        }
        public int MaxPointsByGame(Game game)
        {
            int max = 0;
            foreach(Team t in game.Teams)
            {
                if (t.CurrentScore > max)
                    max = t.CurrentScore;
            }
            return max;
        }
        public Game NewGameSameTeams(Game game)
        {
            var newGame = new Game();
            newGame.Teams = game.Teams;
            foreach(Team t in newGame.Teams)
            {
                t.CurrentScore = 0;
            }
            newGame.StartDt = DateTime.Now;
            newGame.TeamsAmount = game.TeamsAmount;
            newGame.Id = Game.CurrentId + 1;
            Game.CurrentId += 1;
            return newGame;
        }

        //TODO: Main Logic for Alias(Randomize cards with actions and words) 
        //TODO: CreateSession for current game
        //TODO: Make data files with cards and another one with teams that participated in previous games(in Data folder)
    }
}
