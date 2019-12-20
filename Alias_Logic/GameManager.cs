using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alias_Core
{
    public class GameManager
    {
        public static string PathToHome = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

        public static string DataFolder = @"Alias_Logic\Data\";

        public static string WordlistFilename = @"Words.txt";
        public static string GameDataFilename = @"GameData.json";

        public static string WordlistFile = Path.Combine(PathToHome, DataFolder, WordlistFilename);
        public static string GameDataFile = Path.Combine(PathToHome, DataFolder, GameDataFilename);
        public static List<Game> AllGames = new List<Game>();
        public static List<Team> AllTeams = new List<Team>();
        public static List<string> AllTeamsNames = new List<string>();
        public static List<string> Words;
        public Game CurrentGame;

        //public int CurrentNumberOfTeams;
        //public int CurrentGameId;
        //public int CurrentRound;
        public GameManager()
        {
            LoadData();
        }
        private void LoadData()
        {
            var data = Deserialize<GameData>(GameDataFile);
            AllGames = data._allGames;
            AllTeams = data._allTeams;
            Words = new List<string>(File.ReadAllLines(WordlistFile, Encoding.UTF8));

            if (AllTeams.Count > 0)
            {
                foreach (Team team in AllTeams)
                {
                    AllTeamsNames.Add(team.Name);
                }
            }

            //CurrentNumberOfTeams = AllTeams.Last().CurrentId += 1;
            //CurrentGameId = AllGames.Last().Id += 1;
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
        public static void EndGame(GameManager _gameManager)
        {
            _gameManager.IncrementGames(_gameManager.CurrentGame.Teams);
            GameManager.AllGames.Add(_gameManager.CurrentGame);
            _gameManager.SaveData();
            Team.CurrentIdNum = 0;
        }
        public Team TeamByName(string name)
        {
            Team team = new Team();
            foreach(Team t in AllTeams)
            {
                if (t.Name == name)
                {
                    team = t;
                    team.CurrentId = Team.CurrentIdNum + 1;
                    team.CurrentScore = 0;
                }
            }
            return team;
        }

        public int TeamIdByName(string name, List<string> list)
        {
            int id = 0;
            for (int i = 0; i < list.Count; i++)
            {  
                if (list[i] == name)
                    id = i; 
            }
            return id;
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
        private class GameData
        {
            public List<Team> _allTeams { get; set; }
            public List<Game> _allGames { get; set; }
        }
        public void SaveData()
        {
            var data = new GameData
            {
                _allTeams = AllTeams,
                _allGames = AllGames
            };
            Serialize(GameDataFile, data);
        }

        private T Deserialize<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        private void Serialize<T>(string fileName, T data)
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }


        //TODO: Main Logic for Alias(Randomize cards with actions and words) 
        //TODO: CreateSession for current game
        //TODO: Make data files with cards and another one with teams that participated in previous games(in Data folder)
    }
}
