using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Alias_Core
{
    public class GameManager
    {
        public static string PathToHome = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

        public static string DataFolder = @"Alias_Logic\Data\";

        public static string WordlistFilename = @"Words.txt";
        public static string TeamlistFilename = @"Teams.json";
        public static string GamesFilename = @"Games.json";

        public static string WordlistFile = Path.Combine(PathToHome, DataFolder, WordlistFilename);
        public static string TeamsFile = Path.Combine(PathToHome, DataFolder, TeamlistFilename);
        public static string GamesFile = Path.Combine(PathToHome, DataFolder, GamesFilename);

        public List<Game> AllGames;
        public List<Team> AllTeams;

        public static List<string> Words;
        public static List<string> AllTeamsNames;

        public Game CurrentGame;

        public int CurrentNumberOfTeams;
        public int CurrentGameId;
        public int CurrentRound;
        

        public GameManager()
        {
            LoadData();
            SaveData(GamesFile, AllGames);
        }

        private void LoadData()
        {
            AllGames = Deserialize<List<Game>>(GamesFile) ?? new List<Game>();
            AllTeams = Deserialize<List<Team>>(TeamsFile) ?? new List<Team>();
            Words = new List<string>(File.ReadAllLines(WordlistFile, Encoding.UTF8));

            if (AllTeams.Count > 0) {
                foreach (Team team in AllTeams)
                {
                    AllTeamsNames.Add(team.Name);
                }
            }
            else
                AllTeamsNames = new List<string>();

            CurrentNumberOfTeams = AllTeams.Last().Id += 1;
            CurrentGameId = AllGames.Last().Id += 1;

            CurrentGame = new Game()
            {
                Id = CurrentGameId,
                TeamsAmount = CurrentNumberOfTeams
            };

            
        }

        S
        private void SaveData(string filename, List<T> data)
        {
            var data = new ParkingData
            {
                Capacity = parkingCapacity,
                ActiveSessions = activeSessions,
                PastSessions = pastSessions
            };

            Serialize(filename, data);
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
        public string RandomizeWord()
        {
            Random random = new Random();
            int id = random.Next(Words.Count);
            return Words[id];
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
