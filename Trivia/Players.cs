using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        private int currentPlayer = 0;

        public List<Player> _players = new List<Player>();

        public void AddPlayerName(string playerName)
        {
            _players.Add(new Player {Name = playerName});
        }

        public int Count()
        {
            return _players.Count;
        }

        public void NextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == Count()) currentPlayer = 0;
        }

        public Player CurrentPlayer()
        {
            return _players[currentPlayer];
        }
    }
}