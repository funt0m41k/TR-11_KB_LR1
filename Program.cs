using System;
using System.Collections.Generic;

namespace СS_LR2
{
    public abstract class Player
    {
        public abstract void LoseGame(GameInfo Game);
        public abstract void WinGame(GameInfo Game);
        public abstract void PlayGame(GameAccount Player2, GameInfo Game);
        public abstract void InputGameAccountInfo();
    }
    public class GameAccount : Player
    {
        public String UserName { get; set; }
        public int CurrentRating { get; set; }
        public int RatingMode { get; set; } //1-3
        private List<GameInfo> GameUserHistory = new List<GameInfo>();
        public GameAccount(String UserName, int CurrentRating, int RatingMode)
        {
            this.UserName = UserName;
            this.CurrentRating = CurrentRating;
            this.RatingMode = RatingMode;
            if (RatingMode < 1 || RatingMode > 3)
            {
                this.RatingMode = 1;
            }
        }
        public override void InputGameAccountInfo()
        {
            Console.WriteLine("\nUserName: " + UserName);
            Console.WriteLine("Rating: " + CurrentRating);
            Console.WriteLine("Games: " + GameUserHistory.Count);
            if (RatingMode == 1)
            {
                Console.WriteLine("User have normal rating mode");
            }
            if (RatingMode == 2)
            {
                Console.WriteLine("User have boost rating mode");
            }
            if (RatingMode == 3)
            {
                Console.WriteLine("User have smurf rating mode");
            }
        }
        public override void LoseGame(GameInfo Game)
        {
            if (RatingMode == 1)
            {
                CurrentRating -= Game.GameRating;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
                Console.WriteLine("Player lose: " + UserName + " (" + Game.LoserRating + " -" + Game.GameRating + ")");
            }
            if (RatingMode == 2)
            {
                CurrentRating -= Game.GameRating / 2;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
                Console.WriteLine("Player lose: " + UserName + " (" + Game.LoserRating + " -" + Game.GameRating/2 + ")");
            }
            if (RatingMode == 3)
            {
                CurrentRating -= Game.GameRating * 2;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
                Console.WriteLine("Player lose: " + UserName + " (" + Game.LoserRating + " -" + Game.GameRating*2 + ")");
            }
            GameUserHistory.Add(Game);
        }
        public override void WinGame(GameInfo Game)
        {
            if (RatingMode == 1)
            {
                CurrentRating += Game.GameRating;
                Console.WriteLine("Player win: " + UserName + " (" + Game.WinnerRating + " +" + Game.GameRating + ")");
            }
            if (RatingMode == 2)
            {
                CurrentRating += Game.GameRating * 2;
                Console.WriteLine("Player win: " + UserName + " (" + Game.WinnerRating + " +" + Game.GameRating*2 + ")");
            }
            if (RatingMode == 3)
            {
                CurrentRating += Game.GameRating / 2;
                Console.WriteLine("Player win: " + UserName + " (" + Game.WinnerRating + " +" + Game.GameRating/2 + ")");
            }
            GameUserHistory.Add(Game);
        }
        public override void PlayGame(GameAccount Player2, GameInfo Game)
        {
            Console.WriteLine("Player: " + UserName + " played with: " + Player2.UserName);
            GameUserHistory.Add(Game);
        }
        public void GetStats()
        {
            Console.WriteLine("\n" + UserName + " Game history: ");
            for (int i = 0; i < GameUserHistory.Count; i++)
            {
                if (GameUserHistory[i].GameMode == 1)
                {
                    Console.WriteLine("Game id: " + GameUserHistory[i].GameID + " | Win: " + GameUserHistory[i].Winner.UserName + " (" + GameUserHistory[i].WinnerRating + " +" + GameUserHistory[i].GameRating + ") | Lose: " + GameUserHistory[i].Loser.UserName + " (" + GameUserHistory[i].LoserRating + " -" + GameUserHistory[i].GameRating + ")");
                }
                if (GameUserHistory[i].GameMode == 2)
                {
                    Console.WriteLine("Game id: " + GameUserHistory[i].GameID + " | Player1: " + GameUserHistory[i].Winner.UserName + " | Player2: " + GameUserHistory[i].Loser.UserName);
                }
            }
        }
    }
    public class main
    {
        public static void Main(String[] args)
        {
            const int StartRating = 10;
            var Account1 = new GameAccount("Funtom", StartRating, 1);
            var Account2 = new GameAccount("LolFM", StartRating, 2);
            var Account3 = new GameAccount("HotWind", StartRating, 3);
            Account1.InputGameAccountInfo();
            Account2.InputGameAccountInfo();
            Account3.InputGameAccountInfo();
            var game = new GamePlay();
            game.PlayClassicGame(Account1, Account2, 5);
            game.PlayClassicGame(Account2, Account3, 4);
            game.PlayClassicGame(Account3, Account1, 3);

            game.PlayLobbyGame(Account1, Account2);
            game.PlayLobbyGame(Account2, Account3);
            game.PlayLobbyGame(Account1, Account3);

            game.PlayTrainingGame(Account1, Account2);
            game.PlayTrainingGame(Account2, Account3);
            game.PlayTrainingGame(Account3, Account1);

            game.GetStats();
            Account1.GetStats();
            Account2.GetStats();
            Account3.GetStats();

            Account1.InputGameAccountInfo();
            Account2.InputGameAccountInfo();
            Account3.InputGameAccountInfo();
        }
    }
    abstract class Game
    {
        public abstract GameInfo PlayGame(int ID, GameAccount Account1, GameAccount Account2, int Rating);
    }
    public class GamePlay
    {
        public int ID = 21122;
        public List<GameInfo> GamesHistory = new List<GameInfo>();
        public void PlayClassicGame(GameAccount Account1, GameAccount Account2, int Rating)
        {
            GameClassic ClassicGame = new GameClassic();
            GamesHistory.Add(ClassicGame.PlayGame(ID, Account1, Account2, Rating));
            ID++;
        }
        public void PlayTrainingGame(GameAccount Account1, GameAccount Account2)
        {
            GameTraining TrainingGame = new GameTraining();
            GamesHistory.Add(TrainingGame.PlayGame(ID, Account1, Account2, 0));
            ID++;
        }
        public void PlayLobbyGame(GameAccount Account1, GameAccount Account2)
        {
            GameLobby LobbyGame = new GameLobby();
            GamesHistory.Add(LobbyGame.PlayGame(ID, Account1, Account2, 0));
            ID++;
        }
        public void GetStats()
        {
            Console.WriteLine("\nGame history: ");
            for (int i = 0; i < GamesHistory.Count; i++)
            {
                if (GamesHistory[i].GameMode == 1)
                {
                    Console.WriteLine("Game id: " + GamesHistory[i].GameID + " | Win: " + GamesHistory[i].Winner.UserName + " (" + GamesHistory[i].WinnerRating + " +" + GamesHistory[i].GameRating + ") | Lose: " + GamesHistory[i].Loser.UserName + " (" + GamesHistory[i].LoserRating + " -" + GamesHistory[i].GameRating + ")");
                }
                if (GamesHistory[i].GameMode == 2)
                {
                    Console.WriteLine("Game id: " + GamesHistory[i].GameID + " | Player1: " + GamesHistory[i].Winner.UserName + " | Player2: " + GamesHistory[i].Loser.UserName);
                }
            }
        }
    }
    class GameClassic : Game
    {
        public override GameInfo PlayGame(int ID, GameAccount Account1, GameAccount Account2, int Rating)
        {
            Random Random = new Random();
            int WinOrLose = Random.Next(0, 2);
            if (WinOrLose > 0)
            {
                GameInfo GameResult = new GameInfo(ID, 1, Rating, Account1, Account2, Account1.CurrentRating, Account2.CurrentRating);
                Console.WriteLine("\nPlay Game id: " + ID);
                Account1.WinGame(GameResult);
                Account2.LoseGame(GameResult);
                return GameResult;
            }
            else
            {
                GameInfo GameResult = new GameInfo(ID, 1, Rating, Account2, Account1, Account2.CurrentRating, Account1.CurrentRating);
                Console.WriteLine("\nPlay Game id: " + ID);
                Account2.WinGame(GameResult);
                Account1.LoseGame(GameResult);
                return GameResult;
            }
        }
    }
    class GameTraining : Game 
    {
        public override GameInfo PlayGame(int ID, GameAccount Account1, GameAccount Account2, int Rating)
        {
            Random Random = new Random();
            int WinOrLose = Random.Next(0, 2);
            if (WinOrLose > 0)
            {
                GameInfo GameResult = new GameInfo(ID, 1, 0, Account1, Account2, Account1.CurrentRating, Account2.CurrentRating);
                Console.WriteLine("\nPlay Game id: " + ID);
                Account1.WinGame(GameResult);
                Account2.LoseGame(GameResult);
                return GameResult;
            }
            else
            {
                GameInfo GameResult = new GameInfo(ID, 1, 0, Account2, Account1, Account2.CurrentRating, Account1.CurrentRating);
                Console.WriteLine("\nPlay Game id: " + ID);
                Account2.WinGame(GameResult);
                Account1.LoseGame(GameResult);
                return GameResult;
            }
        }
    }
    class GameLobby : Game
    {
        public override GameInfo PlayGame(int ID, GameAccount Account1, GameAccount Account2, int Rating)
        {
            GameInfo GameResult = new GameInfo(ID, 2, Account1, Account2);
            Console.WriteLine("\nPlay Game id: " + ID);
            Account1.PlayGame(Account2, GameResult);
            Account2.PlayGame(Account1, GameResult);
            return GameResult;
        }
    }
    public class GameInfo
    {
        public int GameID { get; set; }
        public int GameMode { get; set; }
        public GameAccount Winner { get; set; }
        public GameAccount Loser { get; set; }
        public int WinnerRating { get; set; }
        public int LoserRating { get; set; }
        public int GameRating { get; set; }
        public GameInfo(int GameID, int GameMode, int GameRating, GameAccount Winner, GameAccount Loser, int WinnerRating, int LoserRating)
        {
            this.GameID = GameID;
            this.GameMode = GameMode;
            this.GameRating = GameRating;
            this.Winner = Winner;
            this.Loser = Loser;
            this.WinnerRating = WinnerRating;
            this.LoserRating = LoserRating;
        }
        public GameInfo(int GameID, int GameMode, GameAccount Winner, GameAccount Loser)
        {
            this.GameID = GameID;
            this.GameMode = GameMode;
            this.Winner = Winner;
            this.Loser = Loser;
        }
    }
}
