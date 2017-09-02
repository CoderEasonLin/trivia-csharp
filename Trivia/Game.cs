using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        bool isGettingOutOfPenaltyBox;

        private readonly Players _players;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast("Rock Question " + i);
            }
            _players = new Players();
        }

        public bool add(String playerName)
        {
            _players.AddPlayerName(playerName);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count());
            return true;
        }

        public void roll(int roll)
        {
            Console.WriteLine(_players.CurrentPlayer().Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players.CurrentPlayer().IsPenaltized)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players.CurrentPlayer().Name + " is getting out of the penalty box");

                    _players.CurrentPlayer().Move(roll);

                    Console.WriteLine(_players.CurrentPlayer().Name
                                      + "'s new location is "
                                      + _players.CurrentPlayer().Place);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(_players.CurrentPlayer().Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _players.CurrentPlayer().Move(roll);

                Console.WriteLine(_players.CurrentPlayer().Name
                                  + "'s new location is "
                                  + _players.CurrentPlayer().Place);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        private String currentCategory()
        {
            Dictionary<int, string> categoryMapping = new Dictionary<int, string>
            {
                {0, "Pop"},
                {4, "Pop"},
                {8, "Pop"},
                {1, "Science"},
                {5, "Science"},
                {9, "Science"},
                {2, "Sports"},
                {6, "Sports"},
                {10, "Sports"},
            };

            if (categoryMapping.ContainsKey(_players.CurrentPlayer().Place))
                return categoryMapping[_players.CurrentPlayer().Place];
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (_players.CurrentPlayer().IsPenaltized)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players.CurrentPlayer().WonCoin();
                    Console.WriteLine(_players.CurrentPlayer().Name
                                      + " now has "
                                      + _players.CurrentPlayer().Purse
                                      + " Gold Coins.");

                    bool winner = didPlayerWin();
                    _players.NextPlayer();
                    return winner;
                }
                else
                {
                    _players.NextPlayer();
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was correct!!!!");
                _players.CurrentPlayer().WonCoin();
                Console.WriteLine(_players.CurrentPlayer().Name
                                  + " now has "
                                  + _players.CurrentPlayer().Purse
                                  + " Gold Coins.");

                bool winner = didPlayerWin();
                _players.NextPlayer();

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players.CurrentPlayer().Name + " was sent to the penalty box");
            _players.CurrentPlayer().SetPenaltized();

            _players.NextPlayer();
            return true;
        }


        private bool didPlayerWin()
        {
            return !(_players.CurrentPlayer().Purse == 6);
        }
    }
}