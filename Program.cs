using System;
using System.Collections.Generic;

namespace СS_LR1
{
    public class GameAccount
    {

        private String UserName;
        private int CurrentRating;
        private int GamesCount;

        public GameAccount(String UserName, int CurrentRating, int GamesCount)
        {
            this.UserName = UserName;
            this.CurrentRating = CurrentRating;
            this.GamesCount = GamesCount;
        }
        public String getName()
        {
            return this.UserName;
        }
        public void setName(String UserName)
        {
            this.UserName = UserName;
        }
        public int getCurrentRating()
        {
            return this.CurrentRating;
        }
        public void setCurrentRating(int CurrentRating)
        {
            this.CurrentRating = CurrentRating;
        }
        public int getGamesCount()
        {
            return this.GamesCount;
        }
        public void setGamesCount(int GamesCount)
        {
            this.GamesCount = GamesCount;
        }

        public void InputGameAccountInfo()
        {
            Console.WriteLine("\nUserName: " + UserName);
            Console.WriteLine("Rating: " + CurrentRating);
            Console.WriteLine("Games: " + GamesCount);
        }

        public void LoseGame(GameAccount Winner)
        {
            CurrentRating--;
            if (CurrentRating < 1)
            {
                CurrentRating = 1;
            }
            GamesCount++;
            Console.WriteLine("Player lose: " + UserName + " (" + CurrentRating + " MMR)");
        }

        public void WinGame(GameAccount Loser)
        {
            CurrentRating++;
            GamesCount++;
            Console.WriteLine("Player win: " + UserName + " (" + CurrentRating + " MMR)");
        }
    }
    public class main
    {
        public static void Main(String[] args)
        {
            const int StartRating = 10;
            var Account1 = new GameAccount("Funtom", StartRating, 0);
            var Account2 = new GameAccount("LolFM", StartRating, 0);
            //var Account3 = new GameAccount("HotWind", StartRating, 0);
            Account1.InputGameAccountInfo();
            Account2.InputGameAccountInfo();
            //Account3.InputGameAccountInfo();
            Game game = new Game();
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.PlayGame(Account1, Account2);
            game.GetStats();

            Account1.InputGameAccountInfo();
            Account2.InputGameAccountInfo();
        }
    }
    public class Game
    {
        private int GameID = 0;
        private List<GameHistory> GameHistory = new List<GameHistory>();

        public void PlayGame(GameAccount Account1, GameAccount Account2)
        {
            GameID++;
            Random Random = new Random();
            int WinOrLose = Random.Next(0, 2);
            Console.WriteLine("\nPlay Game id: " + GameID);
            if (WinOrLose > 0)
            {
                GameHistory GameResult = new GameHistory(Account1, Account2, Account1.getCurrentRating(), Account2.getCurrentRating());
                GameHistory.Add(GameResult);
                Account1.WinGame(Account2);
                Account2.LoseGame(Account1);
            }
            else
            {
                GameHistory GameResult = new GameHistory(Account2, Account1, Account2.getCurrentRating(), Account1.getCurrentRating());
                GameHistory.Add(GameResult);
                Account2.WinGame(Account1);
                Account1.LoseGame(Account2);
            }
        }

        public void GetStats()
        {
            Console.WriteLine("\nGame history: ");
            for (int i = 0; i < GameHistory.Count; i++)
            {
                Console.WriteLine("Game id: " + (i+1) + " | Win: " + GameHistory[i].getWinner() + " (" + GameHistory[i].getWinnerRating() + "+1) | Lose: " + GameHistory[i].getLoser() + " (" + GameHistory[i].getLoserRating() + "-1)");
            }
        }
    }
    public class GameHistory
    {
        private GameAccount Winner;
        private GameAccount Loser;
        private int WinnerRating;
        private int LoserRating;
        public GameHistory(GameAccount Winner, GameAccount Loser, int WinnerRating, int LoserRating)
        {
            this.Winner = Winner;
            this.Loser = Loser;
            this.WinnerRating = WinnerRating;
            this.LoserRating = LoserRating;
        }

        public String getWinner()
        {
            return Winner.getName();
        }

        public String getLoser()
        {
            return Loser.getName();
        }

        public int getWinnerRating()
        {
            return WinnerRating;
        }

        public int getLoserRating()
        {
            return LoserRating;
        }
    }
}
